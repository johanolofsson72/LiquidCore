
/***********************************************************************
 * LiquidCore Framework
 * (C) Johan Olofsson 2007-2008
 * 
 * CoreLib components
 * Web defined components
 * 
 * Special thanks to Benzi K. Ahmed and the Citrus framework components
 * 
 * *********************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Web;
using System.Reflection;
using System.Xml;
using System.Configuration;

//DomainObject
namespace LiquidCore.CoreLib
{
    public abstract class DomainObject
    {
        #region Properties and fields

        internal Wrap<DomainObjectState> _state = new Wrap<DomainObjectState>(DomainObjectState.Unintialized);
        /// <summary>
        /// Gets the current state of the object
        /// </summary>
        public DomainObjectState ObjectState
        {
            get { return _state.Value; }
        }

        /// <summary>
        /// A mechanism for data transfer to the IDomainObject methods        
        /// </summary>
        protected NameValueSet DataFields = new NameValueSet();

        private Wrap<DomainObject> _container = new Wrap<DomainObject>();
        /// <summary>
        /// The DomainObject container
        /// </summary>
        protected DomainObject Container
        {
            get { return _container.Value; }
            set { _container.Value = value; }
        }

        /// <summary>
        /// Flag to determine whether the container domain
        /// object must be notified when this domain object 
        /// undergoes a state change
        /// </summary>
        protected bool NotifyContainerOnStateChange = false;

        /// <summary>
        /// Flag to determine whether the container domain
        /// object must be marked as dirty when this domain object 
        /// undergoes a state change
        /// </summary>
        protected bool MarkContainerOnStateChange = false;

        /// <summary>
        /// Flag to determine whether to set self as dirty
        /// when the container changes
        /// </summary>
        protected bool MarkSelfAsDirtyOnContainerChange = false;

        /// <summary>
        /// Flag to determine whether to set self as dirty
        /// when the container's state changes
        /// </summary>
        protected bool MarkSelfAsDirtyOnContainerStateChange = false;

        /// <summary>
        /// Flag to determine if container state changes are being monitored
        /// </summary>
        private bool MonitoringContainerStateChange = false;

        #endregion

        #region Ctor

        /// <summary>
        /// The initial state of the object must be specified
        /// </summary>
        /// <param name="initialState">The starting state of the object</param>
        /// <param name="container">The parent domain object container</param>
        public DomainObject(DomainObject container, DomainObjectState initialState)
        {
            // A new instance of a domain object must reflect a new
            // entity or an existing entity
            if (initialState != DomainObjectState.Clean && initialState != DomainObjectState.New)
            {
                throw new ArgumentException("The initial object state must be either DomainObjectState.New or DomainObjectState.Clean", "initialState");
            }

            // The new domain object must support the 
            // IDomainObject interface
            if (!(this is IDomainObject))
            {
                throw new ArgumentException("DomainObjects must support the IDomainObject interface explicitly", GetType().FullName);
            }

            // TODO: Ensure that the IDomainObject interface has been explicity implemented

            _state.Value = initialState;
            _state.ValueUpdating += new Wrap<DomainObjectState>.ValueUpdateEventHandler(ValidateStateChange);


            // Save the container
            _container.Value = container;

            // Any changes made to the container will
            // mark this object as dirty
            _container.ValueUpdated += new Wrap<DomainObject>.ValueUpdateEventHandler(ContainerChanged);
        }

        /// <summary>
        /// The initial state of the object must be specified
        /// </summary>
        /// <param name="initialState"></param>
        public DomainObject(DomainObjectState initialState) : this(null, initialState) { }

        /// <summary>
        /// Hide the default ctor
        /// </summary>
        private DomainObject() : this(DomainObjectState.New) { }

        #endregion

        #region Methods

        /// <summary>
        /// The object has undergone updation
        /// This method will be called by the container's
        /// model objects
        /// </summary>
        internal void SetAsDirty()
        {
            // New objects always remain new, only
            // previously clean objects can become dirty
            if (_state.Value == DomainObjectState.New)
                return;

            _state.Value = DomainObjectState.Dirty;
        }

        /// <summary>
        /// Deletes the current instance
        /// </summary>
        public void Delete()
        {
            _state.Value = DomainObjectState.ToDelete;
            this.DataFields["Recursive"] = true; 
            CommitCurrentState();
        }

        /// <summary>
        /// Deletes the current instance (overload)
        /// </summary>
        public void Delete(bool recursive)
        {
            _state.Value = DomainObjectState.ToDelete;
            this.DataFields["Recursive"] = recursive; 
            CommitCurrentState();
        }
        
        /// <summary>
        /// Saves the changes that were made
        /// </summary>
        public void Save()
        {
            CommitCurrentState();
        }

        /// <summary>
        /// Commits the current state
        /// Based on the current state of the object, invokes
        /// the appropriate IDomainObject method
        /// </summary>
        private void CommitCurrentState()
        {
            switch (_state.Value)
            {
                // If we have a new object,
                // create it
                case DomainObjectState.New:
                    ((IDomainObject)this).CreateEntity();
                    _state.Value = DomainObjectState.Clean;
                    break;

                // If the object is clean, do
                // nothing
                case DomainObjectState.Clean:
                    break;

                // If the object is dirty
                // update it
                case DomainObjectState.Dirty:
                    ((IDomainObject)this).UpdateEntity();
                    _state.Value = DomainObjectState.Clean;
                    break;

                // If the object is to be deleted
                // delete it
                case DomainObjectState.ToDelete:
                    ((IDomainObject)this).DeleteEntity();
                    DataFields.Clear();
                    _state.Value = DomainObjectState.Deleted;
                    break;

                // If the object has been deleted,
                // no more commits can happen
                case DomainObjectState.Deleted:
                    throw new InvalidOperationException("Deleted objects cannot saved");
            }
        }

        /// <summary>
        /// Marks the current domain object as being dirty when
        /// its container changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ContainerChanged(object sender, ValueUpdateEventArgs<DomainObject> e)
        {
            // Reset the internal flag for monitoring container state changes
            MonitoringContainerStateChange = false;

            // If the current object has to be set as dirty
            // do it
            if (MarkSelfAsDirtyOnContainerChange)
            {
                SetAsDirty();
            }

            // Hook up the state change handler for the container's state change
            if (null != Container && !MonitoringContainerStateChange)
            {
                Container._state.ValueUpdated += new Wrap<DomainObjectState>.ValueUpdateEventHandler(ContainerStateChanged);
                MonitoringContainerStateChange = true;
            }
        }

        /// <summary>
        /// Handler for monitoring state changes made to the container
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ContainerStateChanged(object sender, ValueUpdateEventArgs<DomainObjectState> e)
        {
            if (MarkSelfAsDirtyOnContainerStateChange)
            {
                SetAsDirty();
            }
        }

        /// <summary>
        /// Validates the state transitions.
        /// Throws exceptions if any invalid state changes occur
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ValidateStateChange(object sender, ValueUpdateEventArgs<DomainObjectState> e)
        {
            // Cannot delete a new object that is not saved
            if (e.OldItem == DomainObjectState.New && e.NewItem == DomainObjectState.ToDelete)
            {
                throw new Exception("New objects cannot be deleted");
            }

            // A deleted object cannot be deleted again
            if (e.OldItem == DomainObjectState.Deleted && e.NewItem == DomainObjectState.ToDelete)
            {
                throw new Exception("Deleted objects cannot be deleted again");
            }

            // At this stage we have a valid state change
            // So notify the parent container (if any) and if required
            if (_container != null && NotifyContainerOnStateChange)
            {
                Container.OnContainedObjectStateChanged(this, e.OldItem, e.NewItem);
            }
            // If required mark the container as dirty also
            if (_container != null && MarkContainerOnStateChange)
            {
                Container.SetAsDirty();
            }
        }

        /// <summary>
        /// Method to notify the container that a contained object
        /// has undergone a state change
        /// Override this in case a domain object needs to process changes
        /// being made to its contained domain objects
        /// </summary>
        /// <param name="containedObject">The contained object that has changed state</param>
        /// <param name="oldState">The previous state</param>
        /// <param name="newState">The new state</param>
        protected virtual void OnContainedObjectStateChanged(DomainObject containedObject, DomainObjectState oldState, DomainObjectState newState) { }

        #endregion
    }
}

//DomainObjectState
namespace LiquidCore.CoreLib
{
    /// <summary>
    /// The various states of an object
    /// </summary>
    public enum DomainObjectState
    {
        /// <summary>
        /// A new object state
        /// </summary>
        New,
        /// <summary>
        /// An up-to-date object state
        /// </summary>
        Clean,
        /// <summary>
        /// A modified object state
        /// </summary>
        Dirty,
        /// <summary>
        /// A marked for destroy object state
        /// </summary>
        ToDelete,
        /// <summary>
        /// A destroyed object state
        /// </summary>
        Deleted,
        /// <summary>
        /// An invalid object state
        /// </summary>
        Unintialized
    }
}

//IDomainObject
namespace LiquidCore.CoreLib
{
    /// <summary>
    /// The IDomainObject contract to be supported by
    /// all DomainObject classes
    /// </summary>
    public interface IDomainObject
    {
        /// <summary>
        /// Creates the entity in the underlying data store
        /// </summary>
        void CreateEntity();

        /// <summary>
        /// Loads the existing entity from the underlying data store
        /// </summary>
        void LoadEntity(ModelObject source);

        /// <summary>
        /// Updates the current entity to the underlying data store
        /// </summary>
        void UpdateEntity();

        /// <summary>
        /// Deletes the existing entity from the underlying data store
        /// </summary>
        void DeleteEntity();
    }
}

//ModelObject
namespace LiquidCore.CoreLib
{
    /// <summary>
    /// Class to abtract domain model objects
    /// </summary>
    public abstract class ModelObject
    {
        #region Properties and fields

        private DomainObject _container = null;
        /// <summary>
        /// The container for this model
        /// </summary>
        protected DomainObject Container
        {
            get { return _container; }
        }

        #endregion

        #region Ctor

        private ModelObject() { }

        /// <summary>
        /// Contructs a new model object for the 
        /// associated container domain object 
        /// </summary>
        /// <param name="container">The container domain object</param>
        public ModelObject(DomainObject container)
        {
            if (null == container)
                throw new ArgumentException("Container cannot be null", "container");

            // Now determine how to set up the values for this model
            // based on the container's initial state
            switch (container.ObjectState)
            {
                case DomainObjectState.New:
                    // In case the state is new, we do not need to
                    // perform anything
                    break;
                case DomainObjectState.Clean:
                    // If the the container is clean (i.e. an existing
                    // model object is being loaded), ask the container
                    // to load this model
                    ((IDomainObject)container).LoadEntity(this);
                    break;
                default:
                    throw new Exception("The container domain object's state " + container.ObjectState + " is invalid for creating a model object. The domain object state must be either new or clean");
            }

            _container = container;
        }

        #endregion

        #region Methods

        protected void NotifyContainer(object sender, ValueUpdateEventArgs<string> e)
        {
            // Notify the container that it has become dirty
            _container.SetAsDirty();
        }

        protected void NotifyContainer(object sender, ValueUpdateEventArgs<int> e)
        {
            // Notify the container that it has become dirty
            _container.SetAsDirty();
        }

        protected void NotifyContainer(object sender, ValueUpdateEventArgs<int[]> e)
        {
            // Notify the container that it has become dirty
            _container.SetAsDirty();
        }

        protected void NotifyContainer(object sender, ValueUpdateEventArgs<bool> e)
        {
            // Notify the container that it has become dirty
            _container.SetAsDirty();
        }

        protected void NotifyContainer(object sender, ValueUpdateEventArgs<DateTime> e)
        {
            // Notify the container that it has become dirty
            _container.SetAsDirty();
        }

        protected void NotifyContainer(object sender, ValueUpdateEventArgs<List<Site>> e)
        {
            // Notify the container that it has become dirty
            _container.SetAsDirty();
        }

        protected void NotifyContainer(object sender, ValueUpdateEventArgs<List<Setting>> e)
        {
            // Notify the container that it has become dirty
            _container.SetAsDirty();
        }

        protected void NotifyContainer(object sender, ValueUpdateEventArgs<List<Page>> e)
        {
            // Notify the container that it has become dirty
            _container.SetAsDirty();
        }

        protected void NotifyContainer(object sender, ValueUpdateEventArgs<List<Module>> e)
        {
            // Notify the container that it has become dirty
            _container.SetAsDirty();
        }

        protected void NotifyContainer(object sender, ValueUpdateEventArgs<List<Objects.Item>> e)
        {
            // Notify the container that it has become dirty
            _container.SetAsDirty();
        }

        protected void NotifyContainer(object sender, ValueUpdateEventArgs<List<Model>> e)
        {
            // Notify the container that it has become dirty
            _container.SetAsDirty();
        }

        protected void NotifyContainer(object sender, ValueUpdateEventArgs<List<ModelItem>> e)
        {
            // Notify the container that it has become dirty
            _container.SetAsDirty();
        }

        protected void NotifyContainer(object sender, ValueUpdateEventArgs<List<ModDef>> e)
        {
            // Notify the container that it has become dirty
            _container.SetAsDirty();
        }

        protected void NotifyContainer(object sender, ValueUpdateEventArgs<List<Menu>> e)
        {
            // Notify the container that it has become dirty
            _container.SetAsDirty();
        }

        protected void NotifyContainer(object sender, ValueUpdateEventArgs<List<List>> e)
        {
            // Notify the container that it has become dirty
            _container.SetAsDirty();
        }

        protected void NotifyContainer(object sender, ValueUpdateEventArgs<List<UserTypes.UserType>> e)
        {
            // Notify the container that it has become dirty
            _container.SetAsDirty();
        }

        protected void NotifyContainer(object sender, ValueUpdateEventArgs<List<Roles.Role>> e)
        {
            // Notify the container that it has become dirty
            _container.SetAsDirty();
        }

        protected void NotifyContainer(object sender, ValueUpdateEventArgs<List<Users.User>> e)
        {
            // Notify the container that it has become dirty
            _container.SetAsDirty();
        }

        protected void NotifyContainer(object sender, ValueUpdateEventArgs<List<Status.Item>> e)
        {
            // Notify the container that it has become dirty
            _container.SetAsDirty();
        }


        #endregion
    }
}

//NameValueSet
namespace LiquidCore.CoreLib
{
    /// <summary>
    /// A name value set data structure
    /// </summary>
    public class NameValueSet
    {
        /// <summary>
        /// The collection of elements
        /// </summary>
        private Dictionary<string, object> elements = new Dictionary<string, object>();

        /// <summary>
        /// Provides a mechanism for storing and retrieving
        /// elements from the bean bag
        /// </summary>
        /// <param name="key">The key for the element to retrieve</param>
        /// <returns>The element for the key provided, null if key is not contained</returns>
        public object this[string key]
        {
            get
            {
                if (elements.ContainsKey(key))
                {
                    return elements[key];
                }
                else
                {
                    return null;
                }
            }
            set
            {
                elements[key] = value;
            }
        }
        /// <summary>
        /// Clears the elements in the bean bag
        /// </summary>
        public void Clear() { elements.Clear(); }
    }
}

//TypedObject
namespace LiquidCore.CoreLib
{
    /// <summary>
    /// Implements a generic class that handles
    /// implicit type conversions
    /// </summary>
    /// <typeparam name="Type"></typeparam>
    public class TypedObject<Type>
    {
        /// <summary>
        /// Constructs a typed object
        /// </summary>
        /// <param name="value"></param>
        public TypedObject(object value)
        {
            this._value = value;
        }

        private object _value;
        /// <summary>
        /// The value type
        /// </summary>
        public Type Value
        {
            get
            {
                return (Type)_value;
            }
        }
    }
}

//ValueUpdateEventArgs
namespace LiquidCore.CoreLib
{
    public class ValueUpdateEventArgs<Item> : EventArgs
    {
        Item _oldItem;

        public Item OldItem
        {
            get { return _oldItem; }
            set { _oldItem = value; }
        }

        Item _newItem;

        public Item NewItem
        {
            get { return _newItem; }
            set { _newItem = value; }
        }

        public ValueUpdateEventArgs(Item oldItem, Item newItem)
        {
            this._oldItem = oldItem;
            this._newItem = newItem;
        }
    }
}

//Wrap
namespace LiquidCore.CoreLib
{
    /// <summary>
    /// Generic wrap class for arbitrary types
    /// </summary>
    /// <typeparam name="ItemType"></typeparam>
    public class Wrap<ItemType>
    {
        #region Properties and fields

        private ItemType _value;
        /// <summary>
        /// The wrapped item
        /// </summary>
        public ItemType Value
        {
            set
            {
                ItemType oldValue = _value;
                ItemType newValue = value;

                // Check if the old and new values are the same, in case yes,
                // we do not need to modify
                if (oldValue != null && oldValue.Equals(newValue)) return;

                OnValueUpdating(new ValueUpdateEventArgs<ItemType>(oldValue, newValue));

                // Check if the old and new values are the same, done again
                // as newValue can be changed as it is a reference parameter
                if (oldValue != null && oldValue.Equals(newValue)) return;

                _value = newValue;

                OnValueUpdated(new ValueUpdateEventArgs<ItemType>(oldValue, newValue));
            }
            get
            {
                return _value;
            }
        }

        #endregion

        #region Ctor

        public Wrap(params object[] args)
        {
            if (args.Length > 0) Value = (ItemType)Activator.CreateInstance(typeof(ItemType), args);
        }

        public Wrap(ItemType t)
        {
            Value = t;
        }

        private Wrap()
        {
            _value = default(ItemType);
        }

        #endregion

        #region Event handling

        /// <summary>
        /// Event fired when the value of the the item has 
        /// been updated
        /// </summary>
        public event ValueUpdateEventHandler ValueUpdated;

        /// <summary>
        /// Event fired when the value of the the item 
        /// is going to be updated
        /// </summary>
        public event ValueUpdateEventHandler ValueUpdating;

        /// <summary>
        /// Fires up the ValueUpdating event
        /// </summary>
        /// <param name="e"></param>
        /// <param name="overrideValue"></param>
        void OnValueUpdating(ValueUpdateEventArgs<ItemType> e)
        {
            if (ValueUpdating != null)
            {
                ValueUpdating(this, e);
            }
        }

        /// <summary>
        /// Fires up the ValueUpdated event
        /// </summary>
        /// <param name="e"></param>
        void OnValueUpdated(ValueUpdateEventArgs<ItemType> e)
        {
            if (ValueUpdated != null)
            {
                ValueUpdated(this, e);
            }
        }

        /// <summary>
        /// Delegate definition for updated event handlers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void ValueUpdateEventHandler(
            object sender,
            ValueUpdateEventArgs<ItemType> e);

        #endregion

        #region Overrides and operator overloads

        /// <summary>
        /// Returns the type of the wrapped item
        /// </summary>
        /// <returns></returns>
        public new System.Type GetType()
        {
            return typeof(ItemType);
        }

        /// <summary>
        /// The string form of the wrapped item
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (Value == null)
            {
                return null;
            }
            else
            {
                return Value.ToString();
            }
        }

        public override bool Equals(object obj)
        {
            return Value.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(Wrap<ItemType> x, ItemType y)
        {
            if ((x == null && y == null) || x.Value == null && y == null)
                return true;
            else if (((x == null || x.Value == null) && y != null) || ((x != null || x.Value != null) && y == null))
                return false;
            else
                return x.Value.Equals(y);
        }

        public static bool operator !=(Wrap<ItemType> x, ItemType y)
        {
            return !(x == y);
        }

        public static implicit operator ItemType(Wrap<ItemType> y)
        {
            return y._value;
        }

        #endregion
    }
}


