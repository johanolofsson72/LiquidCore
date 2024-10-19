
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
using System.Reflection;

// BusinessLayer
namespace LiquidCore
{
    using LiquidCore.Definition;
    using System.Security.Cryptography;

    // Sida vid sida classer, dessa ska skrivas om,
    // för att de ska vara class in class istället,
    // inget viktigt men osnyggt...
    public class Sites : SitesDefinition
    {
        public Sites() : base() { }
        public Sites(String Alias) : base(Alias) { }
    }
    public class Site : SiteDefinition
    {
        static string CLASSNAME = "[Namespace::LiquidCore][Class::Site]";
        public int Id
        {
            get { return base.Details.Id; }
            set { base.Details.Id = value; }
        }
        public int Status
        {
            get { return base.Details.Status; }
            set { base.Details.Status = value; }
        }
        public int Language
        {
            get { return base.Details.Language; }
            set { base.Details.Language = value; }
        }
        public int ParentId
        {
            get { return base.Details.ParentId; }
            set { base.Details.ParentId = value; }
        }
        public int Order
        {
            get { return base.Details.Order; }
            set { base.Details.Order = value; }
        }
        public string Title
        {
            get { return base.Details.Title; }
            set { base.Details.Title = value; }
        }
        public string Alias
        {
            get { return base.Details.Alias; }
            set { base.Details.Alias = value; }
        }
        public string Description
        {
            get { return base.Details.Description; }
            set { base.Details.Description = value; }
        }
        public int[] AuthorizedRoles
        {
            get { return base.Details.AuthorizedRoles; }
        }
        public string Theme
        {
            get { return base.Details.Theme; }
            set { base.Details.Theme = value; }
        }
        public string Structure
        {
            get { return base.Details.Structure; }
            set { base.Details.Structure = value; }
        }
        public DateTime CreatedDate
        {
            get { return base.Details.CreatedDate; }
            set { base.Details.CreatedDate = value; }
        }
        public string CreatedBy
        {
            get { return base.Details.CreatedBy; }
            set { base.Details.CreatedBy = value; }
        }
        public DateTime UpdatedDate
        {
            get { return base.Details.UpdatedDate; }
            set { base.Details.UpdatedDate = value; }
        }
        public string UpdatedBy
        {
            get { return base.Details.UpdatedBy; }
            set { base.Details.UpdatedBy = value; }
        }
        public bool Hidden
        {
            get { return base.Details.Hidden; }
            set { base.Details.Hidden = value; }
        }
        public bool Deleted
        {
            get { return base.Details.Deleted; }
            set { base.Details.Deleted = value; }
        }

        public bool HasChildren
        {
            get
            {
                return LiquidCore.Data.SiteData.FindChildren(base.Details);
            }
        }
        public bool HasParent
        {
            get
            {
                return base.Details.ParentId.Equals(0) ? false : true; 
            }
        }

        public Settings Settings
        {
            get { return new Settings(this.Id, 0, 0, 0); }
        }

        public Pages Pages
        {
            
            // kolla om detta är late eller early binding...
            get { return new Pages(this.Id, 0); }
        }

        public Site() : base() { }
        public Site(Int32 Id) : base(Id) { }

        public void AddAuthorizedRole(Int32 RolId)
        {
            string FUNCTIONNAME = CLASSNAME + "[Function::AddAuthorizedRole]";
            try
            {
                if (base.Details.Id.Equals(0))
                    throw new Exception("This is not created yet, bad bad programmer...");
                LiquidCore.Data.SiteData.AddAuthorizedRole(base.Details, RolId);
            }
            catch (Exception ex)
            {
                Error.Report(ex, FUNCTIONNAME, "");
            }
        }
        public void DeleteAuthorizedRole(Int32 RolId)
        {
            string FUNCTIONNAME = CLASSNAME + "[Function::DeleteAuthorizedRole]";
            try
            {
                if (base.Details.Id.Equals(0))
                    throw new Exception("This is not created yet, bad bad programmer...");
                LiquidCore.Data.SiteData.DeleteAuthorizedRole(base.Details, RolId);
            }
            catch (Exception ex)
            {
                Error.Report(ex, FUNCTIONNAME, "");
            }
        }
        public void ChangeOrderUp()
        {
            string FUNCTIONNAME = CLASSNAME + "[Function::ChangeOrderUp]";
            try
            {
                if (base.Details.Id.Equals(0))
                    throw new Exception("This is not created yet, bad bad programmer...");
                if (this.Order > 1)
                    this.Order = this.Order - 3;
                this.Save();
            }
            catch (Exception ex)
            {
                Error.Report(ex, FUNCTIONNAME, "");
            }
        }
        public void ChangeOrderDown()
        {
            string FUNCTIONNAME = CLASSNAME + "[Function::ChangeOrderDown]";
            try
            {
                if (base.Details.Id.Equals(0))
                    throw new Exception("This is not created yet, bad bad programmer...");
                this.Order = this.Order + 3;
                this.Save();
            }
            catch (Exception ex)
            {
                Error.Report(ex, FUNCTIONNAME, "");
            }
        }


        public void CreateSetting(String Name, String Value)
        {
            string FUNCTIONNAME = CLASSNAME + "[Function::CreateSetting]";
            try
            {
                if (this.Id.Equals(0))
                    throw new Exception("This is not created yet, bad bad programmer...");
                Setting s = new Setting();
                s.Status = 1;
                s.Language = 1;
                s.SitId = this.Id;
                s.PagId = 0;
                s.ModId = 0;
                s.Pointer = 0;
                s.Title = Name;
                s.Alias = "Site-Setting";
                s.Description = Name;
                s.Value = Value;
                s.Save();
            }
            catch (Exception ex)
            {
                Error.Report(ex, FUNCTIONNAME, "");
            }
        }
        public void CreateSetting(String Name)
        {
            CreateSetting(Name, String.Empty);
        }
        public void SaveSetting(String Name, String Value)
        {
            string FUNCTIONNAME = CLASSNAME + "[Function::SaveSetting]";
            try
            {
                Boolean Exist = false;
                foreach (Setting s in this.Settings)
                    if (s.Title.ToLower().Equals(Name.ToLower()))
                    {
                        s.Value = Value;
                        s.Save();
                        Exist = true;
                    }
                if (!Exist)
                    CreateSetting(Name, Value);
            }
            catch (Exception ex)
            {
                Error.Report(ex, FUNCTIONNAME, "");
            }
        }

        public String GetSetting(String Name)
        {
            string FUNCTIONNAME = CLASSNAME + "[Function::ChangeSetting]";
            try
            {
                foreach (Setting s in this.Settings)
                    if (s.Title.ToLower().Equals(Name.ToLower()))
                        return s.Value;
                return String.Empty;
            }
            catch (Exception ex)
            {
                Error.Report(ex, FUNCTIONNAME, "");
                return String.Empty;
            }
        }

    }
    public class Settings : SettingsDefinition
    {
        public Settings() : base() { }
        public Settings(Int32 SitId, Int32 PagId, Int32 ModId, Int32 ParentId) : base(SitId, PagId, ModId, ParentId) { }
        public Settings(String Alias) : base(Alias) { }
        public Settings(Int32 PointerId) : base(PointerId) { }

    }
    public class Setting : SettingDefinition
    {
        static string CLASSNAME = "[Namespace::LiquidCore][Class::Setting]";
        public int Id
        {
            get { return base.Details.Id; }
            set { base.Details.Id = value; }
        }
        public int Status
        {
            get { return base.Details.Status; }
            set { base.Details.Status = value; }
        }
        public int Language
        {
            get { return base.Details.Language; }
            set { base.Details.Language = value; }
        }
        public int SitId
        {
            get { return base.Details.SitId; }
            set { base.Details.SitId = value; }
        }
        public int PagId
        {
            get { return base.Details.PagId; }
            set { base.Details.PagId = value; }
        }
        public int ModId
        {
            get { return base.Details.ModId; }
            set { base.Details.ModId = value; }
        }
        public int ParentId
        {
            get { return base.Details.ParentId; }
            set { base.Details.ParentId = value; }
        }
        public int Order
        {
            get { return base.Details.Order; }
            set { base.Details.Order = value; }
        }
        public int Pointer
        {
            get { return base.Details.Pointer; }
            set { base.Details.Pointer = value; }
        }
        public string Title
        {
            get { return base.Details.Title; }
            set { base.Details.Title = value; }
        }
        public string Alias
        {
            get { return base.Details.Alias; }
            set { base.Details.Alias = value; }
        }
        public string Description
        {
            get { return base.Details.Description; }
            set { base.Details.Description = value; }
        }
        public string Value
        {
            get { return base.Details.Value; }
            set { base.Details.Value = value; }
        }
        public DateTime CreatedDate
        {
            get { return base.Details.CreatedDate; }
            set { base.Details.CreatedDate = value; }
        }
        public string CreatedBy
        {
            get { return base.Details.CreatedBy; }
            set { base.Details.CreatedBy = value; }
        }
        public DateTime UpdatedDate
        {
            get { return base.Details.UpdatedDate; }
            set { base.Details.UpdatedDate = value; }
        }
        public string UpdatedBy
        {
            get { return base.Details.UpdatedBy; }
            set { base.Details.UpdatedBy = value; }
        }
        public bool Hidden
        {
            get { return base.Details.Hidden; }
            set { base.Details.Hidden = value; }
        }
        public bool Deleted
        {
            get { return base.Details.Deleted; }
            set { base.Details.Deleted = value; }
        }

        public bool HasChildren
        {
            get
            {
                return LiquidCore.Data.SettingData.FindChildren(base.Details);
            }
        }
        public bool HasParent
        {
            get
            {
                return base.Details.ParentId.Equals(0) ? false : true;
            }
        }

        public Setting() : base() { }
        public Setting(Int32 Id) : base(Id) { }
		public Setting(DataRow dr) : base(dr) { }
        public Setting(String Alias) : base(Alias) { }
        public void ChangeOrderUp()
        {
            string FUNCTIONNAME = CLASSNAME + "[Function::ChangeOrderUp]";
            try
            {
                if (base.Details.Id.Equals(0))
                    throw new Exception("This is not created yet, bad bad programmer...");
                if (this.Order > 1)
                    this.Order = this.Order - 3;
                this.Save();
            }
            catch (Exception ex)
            {
                Error.Report(ex, FUNCTIONNAME, "");
            }
        }
        public void ChangeOrderDown()
        {
            string FUNCTIONNAME = CLASSNAME + "[Function::ChangeOrderDown]";
            try
            {
                if (base.Details.Id.Equals(0))
                    throw new Exception("This is not created yet, bad bad programmer...");
                this.Order = this.Order + 3;
                this.Save();
            }
            catch (Exception ex)
            {
                Error.Report(ex, FUNCTIONNAME, "");
            }
        }
    }
    public class Pages : PagesDefinition
    {
        public Pages() : base() { }
        public Pages(Int32 SitId, Int32 ParentId) : base(SitId, ParentId) { }
        public Pages(String Alias) : base(Alias) { }
    }
    public class Page : PageDefinition
    {
        static string CLASSNAME = "[Namespace::LiquidCore][Class::Page]";
        public int Id
        {
            get { return base.Details.Id; }
            set { base.Details.Id = value; }
        }
        public int SitId
        {
            get { return base.Details.SitId; }
            set { base.Details.SitId = value; }
        }
        public int Status
        {
            get { return base.Details.Status; }
            set { base.Details.Status = value; }
        }
        public int Language
        {
            get { return base.Details.Language; }
            set { base.Details.Language = value; }
        }
        public int ModelId
        {
            get { return base.Details.ModelId; }
            set { base.Details.ModelId = value; }
        }
        public int ParentId
        {
            get { return base.Details.ParentId; }
            set { base.Details.ParentId = value; }
        }
        public int Order
        {
            get { return base.Details.Order; }
            set { base.Details.Order = value; }
        }
        public string Title
        {
            get { return base.Details.Title; }
            set { base.Details.Title = value; }
        }
        public string Alias
        {
            get { return base.Details.Alias; }
            set { base.Details.Alias = value; }
        }
        public string Description
        {
            get { return base.Details.Description; }
            set { base.Details.Description = value; }
        }
        public int[] AuthorizedRoles
        {
            get { return base.Details.AuthorizedRoles; }
        }
        public string Template
        {
            get { return base.Details.Template; }
            set { base.Details.Template = value; }
        }
        public DateTime CreatedDate
        {
            get { return base.Details.CreatedDate; }
            set { base.Details.CreatedDate = value; }
        }
        public string CreatedBy
        {
            get { return base.Details.CreatedBy; }
            set { base.Details.CreatedBy = value; }
        }
        public DateTime UpdatedDate
        {
            get { return base.Details.UpdatedDate; }
            set { base.Details.UpdatedDate = value; }
        }
        public string UpdatedBy
        {
            get { return base.Details.UpdatedBy; }
            set { base.Details.UpdatedBy = value; }
        }
        public bool Hidden
        {
            get { return base.Details.Hidden; }
            set { base.Details.Hidden = value; }
        }
        public bool Deleted
        {
            get { return base.Details.Deleted; }
            set { base.Details.Deleted = value; }
        }

        public bool HasChildren
        {
            get
            {
                return LiquidCore.Data.PageData.FindChildren(base.Details);
            }
        }
        public bool HasParent
        {
            get
            {
                return base.Details.ParentId.Equals(0) ? false : true;
            }
        }

        public Settings Settings
        {
            get { return new Settings(this.SitId, this.Id, 0, 0); }
        }

        public Modules Modules
        {
            get { return new Modules(this.SitId, this.Id, 0, true); }
        }

        public Page() : base() { }
        public Page(Int32 Id) : base(Id) { }

        public void AddAuthorizedRole(Int32 RolId)
        {
            string FUNCTIONNAME = CLASSNAME + "[Function::AddAuthorizedRole]";
            try
            {
                if (base.Details.Id.Equals(0))
                    throw new Exception("This is not created yet, bad bad programmer...");
                LiquidCore.Data.PageData.AddAuthorizedRole(base.Details, RolId);
            }
            catch (Exception ex)
            {
                Error.Report(ex, FUNCTIONNAME, "");
            }
        }
        public void DeleteAuthorizedRole(Int32 RolId)
        {
            string FUNCTIONNAME = CLASSNAME + "[Function::DeleteAuthorizedRole]";
            try
            {
                if (base.Details.Id.Equals(0))
                    throw new Exception("This is not created yet, bad bad programmer...");
                LiquidCore.Data.PageData.DeleteAuthorizedRole(base.Details, RolId);
            }
            catch (Exception ex)
            {
                Error.Report(ex, FUNCTIONNAME, "");
            }
        }
        public void ChangeOrderUp()
        {
            string FUNCTIONNAME = CLASSNAME + "[Function::ChangeOrderUp]";
            try
            {
                if (base.Details.Id.Equals(0))
                    throw new Exception("This is not created yet, bad bad programmer...");
                if (this.Order > 1)
                    this.Order = this.Order - 3;
                this.Save();
            }
            catch (Exception ex)
            {
                Error.Report(ex, FUNCTIONNAME, "");
            }
        }
        public void ChangeOrderDown()
        {
            string FUNCTIONNAME = CLASSNAME + "[Function::ChangeOrderDown]";
            try
            {
                if (base.Details.Id.Equals(0))
                    throw new Exception("This is not created yet, bad bad programmer...");
                this.Order = this.Order + 3;
                this.Save();
            }
            catch (Exception ex)
            {
                Error.Report(ex, FUNCTIONNAME, "");
            }
        }

    }
    public class Modules : ModulesDefinition
    {
        public Modules() : base() { }
        public Modules(Int32 SitId, Int32 PagId, Int32 ParentId, Boolean IncludeAggregated) : base(SitId, PagId, ParentId, IncludeAggregated) { }
        public Modules(Int32 Status, Int32 SitId, Int32 PagId, Int32 ParentId, Boolean IncludeAggregated) : base(Status, SitId, PagId, ParentId, IncludeAggregated) { }
        public Modules(String Alias) : base(Alias) { }
    }
    public class Module : ModuleDefinition
    {
        static string CLASSNAME = "[Namespace::LiquidCore][Class::Module]";
        public int Id
        {
            get { return base.Details.Id; }
            set { base.Details.Id = value; }
        }
        public int SitId
        {
            get { return base.Details.SitId; }
            set { base.Details.SitId = value; }
        }
        public int PagId
        {
            get { return base.Details.PagId; }
            set { base.Details.PagId = value; }
        }
        public int MdeId
        {
            get { return base.Details.MdeId; }
            set { base.Details.MdeId = value; }
        }
        public int Status
        {
            get { return base.Details.Status; }
            set { base.Details.Status = value; }
        }
        public int Language
        {
            get { return base.Details.Language; }
            set { base.Details.Language = value; }
        }
        public int ParentId
        {
            get { return base.Details.ParentId; }
            set { base.Details.ParentId = value; }
        }
        public int Order
        {
            get { return base.Details.Order; }
            set { base.Details.Order = value; }
        }
        public int MobileOrder
        {
            get { return base.Details.MobileOrder; }
            set { base.Details.MobileOrder = value; }
        }
		public int Revision
		{
			get { return base.Details.Revision; }
			set { base.Details.Revision = value; }
		}
        public string Title
        {
            get { return base.Details.Title; }
            set { base.Details.Title = value; }
        }
        public string Alias
        {
            get { return base.Details.Alias; }
            set { base.Details.Alias = value; }
        }
        public string Description
        {
            get { return base.Details.Description; }
            set { base.Details.Description = value; }
        }
        public int[] AuthorizedRoles
        {
            get { return base.Details.AuthorizedRoles; }
        }
        public string Src
        {
            get { return base.Details.Src; }
            set { base.Details.Src = value; }
        }
        public string ContentPane
        {
            get { return base.Details.ContentPane; }
            set { base.Details.ContentPane = value; }
        }
        public bool AllPages
        {
            get { return base.Details.AllPages; }
            set { base.Details.AllPages = value; }
        }
        public bool SSL
        {
            get { return base.Details.SSL; }
            set { base.Details.SSL = value; }
        }
        public int CacheTime
        {
            get { return base.Details.CacheTime; }
            set { base.Details.CacheTime = value; }
        }
        public DateTime CreatedDate
        {
            get { return base.Details.CreatedDate; }
            set { base.Details.CreatedDate = value; }
        }
        public string CreatedBy
        {
            get { return base.Details.CreatedBy; }
            set { base.Details.CreatedBy = value; }
        }
        public DateTime UpdatedDate
        {
            get { return base.Details.UpdatedDate; }
            set { base.Details.UpdatedDate = value; }
        }
        public string UpdatedBy
        {
            get { return base.Details.UpdatedBy; }
            set { base.Details.UpdatedBy = value; }
        }
        public bool Hidden
        {
            get { return base.Details.Hidden; }
            set { base.Details.Hidden = value; }
        }
        public bool MobileHidden
        {
            get { return base.Details.MobileHidden; }
            set { base.Details.MobileHidden = value; }
        }
        public bool Deleted
        {
            get { return base.Details.Deleted; }
            set { base.Details.Deleted = value; }
        }

        public bool HasChildren
        {
            get
            {
                return LiquidCore.Data.ModuleData.FindChildren(base.Details);
            }
        }
        public bool HasParent
        {
            get
            {
                return base.Details.ParentId.Equals(0) ? false : true;
            }
        }

        public Settings Settings
        {
            get { return new Settings(this.SitId, this.PagId, this.Id, 0); }
        }

        public Module() : base() { }
        public Module(Int32 Id) : base(Id) { }
		public Module(DataRow dr) : base(dr) { }

        public void AddAuthorizedRole(Int32 RolId)
        {
            string FUNCTIONNAME = CLASSNAME + "[Function::AddAuthorizedRole]";
            try
            {
                if (base.Details.Id.Equals(0))
                    throw new Exception("This is not created yet, bad bad programmer...");
                LiquidCore.Data.ModuleData.AddAuthorizedRole(base.Details, RolId);
            }
            catch (Exception ex)
            {
                Error.Report(ex, FUNCTIONNAME, "");
            }
        }
        public void DeleteAuthorizedRole(Int32 RolId)
        {
            string FUNCTIONNAME = CLASSNAME + "[Function::DeleteAuthorizedRole]";
            try
            {
                if (base.Details.Id.Equals(0))
                    throw new Exception("This is not created yet, bad bad programmer...");
                LiquidCore.Data.ModuleData.DeleteAuthorizedRole(base.Details, RolId);
            }
            catch (Exception ex)
            {
                Error.Report(ex, FUNCTIONNAME, "");
            }
        }
        public void ChangeOrderUp()
        {
            string FUNCTIONNAME = CLASSNAME + "[Function::ChangeOrderUp]";
            try
            {
                if (base.Details.Id.Equals(0))
                    throw new Exception("This is not created yet, bad bad programmer...");
                if (this.Order > 1)
                {
                    this.Order = this.Order - 3;
                    this.Save();
                }
            }
            catch (Exception ex)
            {
                Error.Report(ex, FUNCTIONNAME, "");
            }
        }
        public void ChangeOrderDown()
        {
            string FUNCTIONNAME = CLASSNAME + "[Function::ChangeOrderDown]";
            try
            {
                if (base.Details.Id.Equals(0))
                    throw new Exception("This is not created yet, bad bad programmer...");
                this.Order = this.Order + 3;
                this.Save();
            }
            catch (Exception ex)
            {
                Error.Report(ex, FUNCTIONNAME, "");
            }
        }
		public void ChangeOrderUpMobile()
		{
			string FUNCTIONNAME = CLASSNAME + "[Function::ChangeOrderUpMobile]";
			try
			{
				if (base.Details.Id.Equals(0))
					throw new Exception("This is not created yet, bad bad programmer...");
				if (this.MobileOrder > 1)
				{
					this.MobileOrder = this.MobileOrder - 3;
					this.Save();
				}
			}
			catch (Exception ex)
			{
				Error.Report(ex, FUNCTIONNAME, "");
			}
		}
		public void ChangeOrderDownMobile()
		{
			string FUNCTIONNAME = CLASSNAME + "[Function::ChangeOrderDownMobile]";
			try
			{
				if (base.Details.Id.Equals(0))
					throw new Exception("This is not created yet, bad bad programmer...");
				this.MobileOrder = this.MobileOrder + 3;
				this.Save();
			}
			catch (Exception ex)
			{
				Error.Report(ex, FUNCTIONNAME, "");
			}
		}

    }

    [ObsoleteAttribute("LiquidCore.Object has been deprecated. Please investigate the use of LiquidCore.Objects.Item instead.")]
    public class Object : ObjectDefinition
    {
        static string CLASSNAME = "[Namespace::LiquidCore][Class::Object]";

        private Settings _settings;

        public int Id
        {
            get { return base.Details.Id; }
            set { base.Details.Id = value; }
        }
        public int SitId
        {
            get { return base.Details.SitId; }
            set { base.Details.SitId = value; }
        }
        public int PagId
        {
            get { return base.Details.PagId; }
            set { base.Details.PagId = value; }
        }
        public int ModId
        {
            get { return base.Details.ModId; }
            set { base.Details.ModId = value; }
        }
        public int Status
        {
            get { return base.Details.Status; }
            set { base.Details.Status = value; }
        }
        public int Language
        {
            get { return base.Details.Language; }
            set { base.Details.Language = value; }
        }
        public int Type
        {
            get { return base.Details.Type; }
            set { base.Details.Type = value; }
        }
        public int ParentId
        {
            get { return base.Details.ParentId; }
            set { base.Details.ParentId = value; }
        }
        public int Order
        {
            get { return base.Details.Order; }
            set { base.Details.Order = value; }
        }
        public string Title
        {
            get { return base.Details.Title; }
            set { base.Details.Title = value; }
        }
        public string Alias
        {
            get { return base.Details.Alias; }
            set { base.Details.Alias = value; }
        }
        public string Description
        {
            get { return base.Details.Description; }
            set { base.Details.Description = value; }
        }
        public string Value1
        {
            get { return base.Details.Value1; }
            set { base.Details.Value1 = value; }
        }
        public string Value2
        {
            get { return base.Details.Value2; }
            set { base.Details.Value2 = value; }
        }
        public string Value3
        {
            get { return base.Details.Value3; }
            set { base.Details.Value3 = value; }
        }
        public string Value4
        {
            get { return base.Details.Value4; }
            set { base.Details.Value4 = value; }
        }
        public string Value5
        {
            get { return base.Details.Value5; }
            set { base.Details.Value5 = value; }
        }
        public string Value6
        {
            get { return base.Details.Value7; }
            set { base.Details.Value7 = value; }
        }
        public string Value8
        {
            get { return base.Details.Value8; }
            set { base.Details.Value8 = value; }
        }
        public string Value9
        {
            get { return base.Details.Value9; }
            set { base.Details.Value9 = value; }
        }
        public string Value10
        {
            get { return base.Details.Value10; }
            set { base.Details.Value10 = value; }
        }
        public string Value11
        {
            get { return base.Details.Value11; }
            set { base.Details.Value11 = value; }
        }
        public string Value12
        {
            get { return base.Details.Value12; }
            set { base.Details.Value12 = value; }
        }
        public string Value13
        {
            get { return base.Details.Value13; }
            set { base.Details.Value13 = value; }
        }
        public string Value14
        {
            get { return base.Details.Value14; }
            set { base.Details.Value14 = value; }
        }
        public string Value15
        {
            get { return base.Details.Value15; }
            set { base.Details.Value15 = value; }
        }
        public string Value16
        {
            get { return base.Details.Value16; }
            set { base.Details.Value16 = value; }
        }
        public string Value17
        {
            get { return base.Details.Value17; }
            set { base.Details.Value17 = value; }
        }
        public string Value18
        {
            get { return base.Details.Value18; }
            set { base.Details.Value18 = value; }
        }
        public string Value19
        {
            get { return base.Details.Value19; }
            set { base.Details.Value19 = value; }
        }
        public string Value20
        {
            get { return base.Details.Value20; }
            set { base.Details.Value20 = value; }
        }
        public string Value21
        {
            get { return base.Details.Value21; }
            set { base.Details.Value21 = value; }
        }
        public string Value22
        {
            get { return base.Details.Value22; }
            set { base.Details.Value22 = value; }
        }
        public string Value23
        {
            get { return base.Details.Value23; }
            set { base.Details.Value23 = value; }
        }
        public string Value24
        {
            get { return base.Details.Value24; }
            set { base.Details.Value24 = value; }
        }
        public string Value25
        {
            get { return base.Details.Value25; }
            set { base.Details.Value25 = value; }
        }
        public string Value26
        {
            get { return base.Details.Value26; }
            set { base.Details.Value26 = value; }
        }
        public string Value27
        {
            get { return base.Details.Value27; }
            set { base.Details.Value27 = value; }
        }
        public string Value28
        {
            get { return base.Details.Value28; }
            set { base.Details.Value28 = value; }
        }
        public string Value29
        {
            get { return base.Details.Value29; }
            set { base.Details.Value29 = value; }
        }
        public string Value30
        {
            get { return base.Details.Value30; }
            set { base.Details.Value30 = value; }
        }
        public string Value31
        {
            get { return base.Details.Value31; }
            set { base.Details.Value31 = value; }
        }
        public string Value32
        {
            get { return base.Details.Value32; }
            set { base.Details.Value32 = value; }
        }
        public string Value33
        {
            get { return base.Details.Value33; }
            set { base.Details.Value33 = value; }
        }
        public string Value34
        {
            get { return base.Details.Value34; }
            set { base.Details.Value34 = value; }
        }
        public string Value35
        {
            get { return base.Details.Value35; }
            set { base.Details.Value35 = value; }
        }
        public string Value36
        {
            get { return base.Details.Value36; }
            set { base.Details.Value36 = value; }
        }
        public string Value37
        {
            get { return base.Details.Value37; }
            set { base.Details.Value37 = value; }
        }
        public string Value38
        {
            get { return base.Details.Value38; }
            set { base.Details.Value38 = value; }
        }
        public string Value39
        {
            get { return base.Details.Value39; }
            set { base.Details.Value39 = value; }
        }
        public string Value40
        {
            get { return base.Details.Value40; }
            set { base.Details.Value40 = value; }
        }
        public string Value41
        {
            get { return base.Details.Value41; }
            set { base.Details.Value41 = value; }
        }
        public string Value42
        {
            get { return base.Details.Value42; }
            set { base.Details.Value42 = value; }
        }
        public string Value43
        {
            get { return base.Details.Value43; }
            set { base.Details.Value43 = value; }
        }
        public string Value44
        {
            get { return base.Details.Value44; }
            set { base.Details.Value44 = value; }
        }
        public string Value45
        {
            get { return base.Details.Value45; }
            set { base.Details.Value45 = value; }
        }
        public string Value46
        {
            get { return base.Details.Value46; }
            set { base.Details.Value46 = value; }
        }
        public string Value47
        {
            get { return base.Details.Value47; }
            set { base.Details.Value47 = value; }
        }
        public string Value48
        {
            get { return base.Details.Value48; }
            set { base.Details.Value48 = value; }
        }
        public string Value49
        {
            get { return base.Details.Value49; }
            set { base.Details.Value49 = value; }
        }
        public string Value50
        {
            get { return base.Details.Value50; }
            set { base.Details.Value50 = value; }
        }
        public DateTime CreatedDate
        {
            get { return base.Details.CreatedDate; }
            set { base.Details.CreatedDate = value; }
        }
        public string CreatedBy
        {
            get { return base.Details.CreatedBy; }
            set { base.Details.CreatedBy = value; }
        }
        public DateTime UpdatedDate
        {
            get { return base.Details.UpdatedDate; }
            set { base.Details.UpdatedDate = value; }
        }
        public string UpdatedBy
        {
            get { return base.Details.UpdatedBy; }
            set { base.Details.UpdatedBy = value; }
        }
        public bool Hidden
        {
            get { return base.Details.Hidden; }
            set { base.Details.Hidden = value; }
        }
        public bool Deleted
        {
            get { return base.Details.Deleted; }
            set { base.Details.Deleted = value; }
        }

        public bool HasChildren
        {
            get
            {
                return LiquidCore.Data.ObjectData.FindChildren(base.Details);
            }
        }
        public bool HasParent
        {
            get
            {
                return base.Details.ParentId.Equals(0) ? false : true;
            }
        }

        public Settings Settings
        {
            get { return _settings; }
        }
        public void CreateSetting(String Name, String Value)
        {
            string FUNCTIONNAME = CLASSNAME + "[Function::CreateSetting]";
            try
            {
                if (base.Details.Id.Equals(0))
                    throw new Exception("This is not created yet, bad bad programmer...");
                Setting s = new Setting();
                s.Status = 1;
                s.Language = 1;
                s.SitId = base.Details.SitId;
                s.PagId = 0;
                s.ModId = 0;
                s.Pointer = base.Details.Id;
                s.Title = Name;
                s.Alias = "Object-Setting";
                s.Description = Name;
                s.Value = Value;
                s.Save();
            }
            catch (Exception ex)
            {
                Error.Report(ex, FUNCTIONNAME, "");
            }
        }
        public void CreateSetting(String Name)
        {
            CreateSetting(Name, String.Empty);
        }
        public void SaveSetting(String Name, String Value)
        {
            string FUNCTIONNAME = CLASSNAME + "[Function::SaveSetting]";
            try
            {
                Boolean Exist = false;
                foreach (Setting s in this.Settings)
                    if (s.Title.ToLower().Equals(Name.ToLower()))
                    {
                        s.Value = Value;
                        s.Save();
                        Exist = true;
                    }
                if (!Exist)
                    CreateSetting(Name, Value);
            }
            catch (Exception ex)
            {
                Error.Report(ex, FUNCTIONNAME, "");
            }
        }
        public String GetSetting(String Name)
        {
            string FUNCTIONNAME = CLASSNAME + "[Function::ChangeSetting]";
            try
            {
                foreach (Setting s in this.Settings)
                    if (s.Title.ToLower().Equals(Name.ToLower()))
                        return s.Value;
                return String.Empty;
            }
            catch (Exception ex)
            {
                Error.Report(ex, FUNCTIONNAME, "");
                return String.Empty;
            }
        }

        public Object() : base() { }
        public Object(Int32 Id) : base(Id) { }

        public void ChangeOrderUp()
        {
            string FUNCTIONNAME = CLASSNAME + "[Function::ChangeOrderUp]";
            try
            {
                if (base.Details.Id.Equals(0))
                    throw new Exception("This is not created yet, bad bad programmer...");
                if (this.Order > 1)
                    this.Order = this.Order - 3;
                this.Save();
            }
            catch (Exception ex)
            {
                Error.Report(ex, FUNCTIONNAME, "");
            }
        }
        public void ChangeOrderDown()
        {
            string FUNCTIONNAME = CLASSNAME + "[Function::ChangeOrderDown]";
            try
            {
                if (base.Details.Id.Equals(0))
                    throw new Exception("This is not created yet, bad bad programmer...");
                this.Order = this.Order + 3;
                this.Save();
            }
            catch (Exception ex)
            {
                Error.Report(ex, FUNCTIONNAME, "");
            }
        }
    }

    public class Models : ModelsDefinition
    {
        public Models() : base() { }
        public Models(Int32 SitId, Int32 ParentId) : base(SitId, ParentId) { }
    }
    public class Model : ModelDefinition
    {
        static string CLASSNAME = "[Namespace::LiquidCore][Class::Model]";
        public int Id
        {
            get { return base.Details.Id; }
            set { base.Details.Id = value; }
        }
        public int SitId
        {
            get { return base.Details.SitId; }
            set { base.Details.SitId = value; }
        }
        public int Status
        {
            get { return base.Details.Status; }
            set { base.Details.Status = value; }
        }
        public int Language
        {
            get { return base.Details.Language; }
            set { base.Details.Language = value; }
        }
        public int ParentId
        {
            get { return base.Details.ParentId; }
            set { base.Details.ParentId = value; }
        }
        public int Order
        {
            get { return base.Details.Order; }
            set { base.Details.Order = value; }
        }
        public string Title
        {
            get { return base.Details.Title; }
            set { base.Details.Title = value; }
        }
        public string Alias
        {
            get { return base.Details.Alias; }
            set { base.Details.Alias = value; }
        }
        public string Description
        {
            get { return base.Details.Description; }
            set { base.Details.Description = value; }
        }
        public DateTime CreatedDate
        {
            get { return base.Details.CreatedDate; }
            set { base.Details.CreatedDate = value; }
        }
        public string CreatedBy
        {
            get { return base.Details.CreatedBy; }
            set { base.Details.CreatedBy = value; }
        }
        public DateTime UpdatedDate
        {
            get { return base.Details.UpdatedDate; }
            set { base.Details.UpdatedDate = value; }
        }
        public string UpdatedBy
        {
            get { return base.Details.UpdatedBy; }
            set { base.Details.UpdatedBy = value; }
        }
        public bool Hidden
        {
            get { return base.Details.Hidden; }
            set { base.Details.Hidden = value; }
        }
        public bool Deleted
        {
            get { return base.Details.Deleted; }
            set { base.Details.Deleted = value; }
        }

        public bool HasChildren
        {
            get
            {
                return LiquidCore.Data.ModelData.FindChildren(base.Details);
            }
        }
        public bool HasParent
        {
            get
            {
                return base.Details.ParentId.Equals(0) ? false : true;
            }
        }

        public Model() : base() { }
        public Model(Int32 Id) : base(Id) { }

        public void ChangeOrderUp()
        {
            string FUNCTIONNAME = CLASSNAME + "[Function::ChangeOrderUp]";
            try
            {
                if (base.Details.Id.Equals(0))
                    throw new Exception("This is not created yet, bad bad programmer...");
                if (this.Order > 1)
                    this.Order = this.Order - 3;
                this.Save();
            }
            catch (Exception ex)
            {
                Error.Report(ex, FUNCTIONNAME, "");
            }
        }
        public void ChangeOrderDown()
        {
            string FUNCTIONNAME = CLASSNAME + "[Function::ChangeOrderDown]";
            try
            {
                if (base.Details.Id.Equals(0))
                    throw new Exception("This is not created yet, bad bad programmer...");
                this.Order = this.Order + 3;
                this.Save();
            }
            catch (Exception ex)
            {
                Error.Report(ex, FUNCTIONNAME, "");
            }
        }
    }
    public class ModelItems : ModelItemsDefinition
    {
        public ModelItems() : base() { }
        public ModelItems(Int32 SitId, Int32 MdlId, Int32 ParentId) : base(SitId, MdlId, ParentId) { }
    }
    public class ModelItem : ModelItemDefinition
    {
        static string CLASSNAME = "[Namespace::LiquidCore][Class::ModelItem]";
        public int Id
        {
            get { return base.Details.Id; }
            set { base.Details.Id = value; }
        }
        public int SitId
        {
            get { return base.Details.SitId; }
            set { base.Details.SitId = value; }
        }
        public int MdlId
        {
            get { return base.Details.MdlId; }
            set { base.Details.MdlId = value; }
        }
        public int MdeId
        {
            get { return base.Details.MdeId; }
            set { base.Details.MdeId = value; }
        }
        public int Status
        {
            get { return base.Details.Status; }
            set { base.Details.Status = value; }
        }
        public int Language
        {
            get { return base.Details.Language; }
            set { base.Details.Language = value; }
        }
        public int ParentId
        {
            get { return base.Details.ParentId; }
            set { base.Details.ParentId = value; }
        }
        public int Order
        {
            get { return base.Details.Order; }
            set { base.Details.Order = value; }
        }
        public string ContentPane
        {
            get { return base.Details.ContentPane; }
            set { base.Details.ContentPane = value; }
        }
        public DateTime CreatedDate
        {
            get { return base.Details.CreatedDate; }
            set { base.Details.CreatedDate = value; }
        }
        public string CreatedBy
        {
            get { return base.Details.CreatedBy; }
            set { base.Details.CreatedBy = value; }
        }
        public DateTime UpdatedDate
        {
            get { return base.Details.UpdatedDate; }
            set { base.Details.UpdatedDate = value; }
        }
        public string UpdatedBy
        {
            get { return base.Details.UpdatedBy; }
            set { base.Details.UpdatedBy = value; }
        }
        public bool Hidden
        {
            get { return base.Details.Hidden; }
            set { base.Details.Hidden = value; }
        }
        public bool Deleted
        {
            get { return base.Details.Deleted; }
            set { base.Details.Deleted = value; }
        }

        public bool HasChildren
        {
            get
            {
                return LiquidCore.Data.ModelItemData.FindChildren(base.Details);
            }
        }
        public bool HasParent
        {
            get
            {
                return base.Details.ParentId.Equals(0) ? false : true;
            }
        }

        public ModelItem() : base() { }
        public ModelItem(Int32 Id) : base(Id) { }

        public void ChangeOrderUp()
        {
            string FUNCTIONNAME = CLASSNAME + "[Function::ChangeOrderUp]";
            try
            {
                if (base.Details.Id.Equals(0))
                    throw new Exception("This is not created yet, bad bad programmer...");
                if (this.Order > 1)
                    this.Order = this.Order - 3;
                this.Save();
            }
            catch (Exception ex)
            {
                Error.Report(ex, FUNCTIONNAME, "");
            }
        }
        public void ChangeOrderDown()
        {
            string FUNCTIONNAME = CLASSNAME + "[Function::ChangeOrderDown]";
            try
            {
                if (base.Details.Id.Equals(0))
                    throw new Exception("This is not created yet, bad bad programmer...");
                this.Order = this.Order + 3;
                this.Save();
            }
            catch (Exception ex)
            {
                Error.Report(ex, FUNCTIONNAME, "");
            }
        }
    }
    public class ModDefs : ModDefsDefinition
    {
        public ModDefs() : base() { }
        public ModDefs(Int32 SitId, Int32 ParentId) : base(SitId, ParentId) { }
    }
    public class ModDef : ModDefDefinition
    {
        static string CLASSNAME = "[Namespace::LiquidCore][Class::ModDef]";
        public int Id
        {
            get { return base.Details.Id; }
            set { base.Details.Id = value; }
        }
        public int SitId
        {
            get { return base.Details.SitId; }
            set { base.Details.SitId = value; }
        }
        public int Status
        {
            get { return base.Details.Status; }
            set { base.Details.Status = value; }
        }
        public int Language
        {
            get { return base.Details.Language; }
            set { base.Details.Language = value; }
        }
        public int ParentId
        {
            get { return base.Details.ParentId; }
            set { base.Details.ParentId = value; }
        }
        public int Order
        {
            get { return base.Details.Order; }
            set { base.Details.Order = value; }
        }
        public string Title
        {
            get { return base.Details.Title; }
            set { base.Details.Title = value; }
        }
        public string Alias
        {
            get { return base.Details.Alias; }
            set { base.Details.Alias = value; }
        }
        public string Description
        {
            get { return base.Details.Description; }
            set { base.Details.Description = value; }
        }
        public string Src
        {
            get { return base.Details.Src; }
            set { base.Details.Src = value; }
        }
        public int CacheTime
        {
            get { return base.Details.CacheTime; }
            set { base.Details.CacheTime = value; }
        }
        public string Iconfile
        {
            get { return base.Details.Iconfile; }
            set { base.Details.Iconfile = value; }
        }
        public DateTime CreatedDate
        {
            get { return base.Details.CreatedDate; }
            set { base.Details.CreatedDate = value; }
        }
        public string CreatedBy
        {
            get { return base.Details.CreatedBy; }
            set { base.Details.CreatedBy = value; }
        }
        public DateTime UpdatedDate
        {
            get { return base.Details.UpdatedDate; }
            set { base.Details.UpdatedDate = value; }
        }
        public string UpdatedBy
        {
            get { return base.Details.UpdatedBy; }
            set { base.Details.UpdatedBy = value; }
        }
        public bool Hidden
        {
            get { return base.Details.Hidden; }
            set { base.Details.Hidden = value; }
        }
        public bool Deleted
        {
            get { return base.Details.Deleted; }
            set { base.Details.Deleted = value; }
        }

        public bool HasChildren
        {
            get
            {
                return LiquidCore.Data.ModDefData.FindChildren(base.Details);
            }
        }
        public bool HasParent
        {
            get
            {
                return base.Details.ParentId.Equals(0) ? false : true;
            }
        }

        public ModDef() : base() { }
        public ModDef(Int32 Id) : base(Id) { }

        public void ChangeOrderUp()
        {
            string FUNCTIONNAME = CLASSNAME + "[Function::ChangeOrderUp]";
            try
            {
                if (base.Details.Id.Equals(0))
                    throw new Exception("This is not created yet, bad bad programmer...");
                if (this.Order > 1)
                    this.Order = this.Order - 3;
                this.Save();
            }
            catch (Exception ex)
            {
                Error.Report(ex, FUNCTIONNAME, "");
            }
        }
        public void ChangeOrderDown()
        {
            string FUNCTIONNAME = CLASSNAME + "[Function::ChangeOrderDown]";
            try
            {
                if (base.Details.Id.Equals(0))
                    throw new Exception("This is not created yet, bad bad programmer...");
                this.Order = this.Order + 3;
                this.Save();
            }
            catch (Exception ex)
            {
                Error.Report(ex, FUNCTIONNAME, "");
            }
        }
    }

    // Klasser som följer den nya standarden...
    public class Objects : ObjectsDefinition
    {
        public Objects() : base() { }
        public Objects(Int32 SitId, Int32 PagId, Int32 ModId, Int32 ParentId) : base(SitId, PagId, ModId, ParentId) { }
        public Objects(String Alias) : base(Alias) { }
        public Objects(String Alias, Param[] SettingParameters) : base(Alias, SettingParameters) { }
        public Objects(Int32 ParentId) : base(ParentId) { }
        public class Item : ObjectDefinition
        {
            static string CLASSNAME = "[Namespace::LiquidCore.Object][Class::Item]";

            private Settings _settings;

            public int Id
            {
                get { return base.Details.Id; }
                set { base.Details.Id = value; }
            }
            public int SitId
            {
                get { return base.Details.SitId; }
                set { base.Details.SitId = value; }
            }
            public int PagId
            {
                get { return base.Details.PagId; }
                set { base.Details.PagId = value; }
            }
            public int ModId
            {
                get { return base.Details.ModId; }
                set { base.Details.ModId = value; }
            }
            public int Status
            {
                get { return base.Details.Status; }
                set { base.Details.Status = value; }
            }
            public int Language
            {
                get { return base.Details.Language; }
                set { base.Details.Language = value; }
            }
            public int Type
            {
                get { return base.Details.Type; }
                set { base.Details.Type = value; }
            }
            public int ParentId
            {
                get { return base.Details.ParentId; }
                set { base.Details.ParentId = value; }
            }
            public int Order
            {
                get { return base.Details.Order; }
                set { base.Details.Order = value; }
            }
            public string Title
            {
                get { return base.Details.Title; }
                set { base.Details.Title = value; }
            }
            public string Alias
            {
                get { return base.Details.Alias; }
                set { base.Details.Alias = value; }
            }
            public string Description
            {
                get { return base.Details.Description; }
                set { base.Details.Description = value; }
            }
            public string Value1
            {
                get { return base.Details.Value1; }
                set { base.Details.Value1 = value; }
            }
            public string Value2
            {
                get { return base.Details.Value2; }
                set { base.Details.Value2 = value; }
            }
            public string Value3
            {
                get { return base.Details.Value3; }
                set { base.Details.Value3 = value; }
            }
            public string Value4
            {
                get { return base.Details.Value4; }
                set { base.Details.Value4 = value; }
            }
            public string Value5
            {
                get { return base.Details.Value5; }
                set { base.Details.Value5 = value; }
            }
            public string Value6
            {
                get { return base.Details.Value6; }
                set { base.Details.Value6 = value; }
            }
            public string Value7
            {
                get { return base.Details.Value7; }
                set { base.Details.Value7 = value; }
            }
            public string Value8
            {
                get { return base.Details.Value8; }
                set { base.Details.Value8 = value; }
            }
            public string Value9
            {
                get { return base.Details.Value9; }
                set { base.Details.Value9 = value; }
            }
            public string Value10
            {
                get { return base.Details.Value10; }
                set { base.Details.Value10 = value; }
            }
            public string Value11
            {
                get { return base.Details.Value11; }
                set { base.Details.Value11 = value; }
            }
            public string Value12
            {
                get { return base.Details.Value12; }
                set { base.Details.Value12 = value; }
            }
            public string Value13
            {
                get { return base.Details.Value13; }
                set { base.Details.Value13 = value; }
            }
            public string Value14
            {
                get { return base.Details.Value14; }
                set { base.Details.Value14 = value; }
            }
            public string Value15
            {
                get { return base.Details.Value15; }
                set { base.Details.Value15 = value; }
            }
            public string Value16
            {
                get { return base.Details.Value16; }
                set { base.Details.Value16 = value; }
            }
            public string Value17
            {
                get { return base.Details.Value17; }
                set { base.Details.Value17 = value; }
            }
            public string Value18
            {
                get { return base.Details.Value18; }
                set { base.Details.Value18 = value; }
            }
            public string Value19
            {
                get { return base.Details.Value19; }
                set { base.Details.Value19 = value; }
            }
            public string Value20
            {
                get { return base.Details.Value20; }
                set { base.Details.Value20 = value; }
            }
            public string Value21
            {
                get { return base.Details.Value21; }
                set { base.Details.Value21 = value; }
            }
            public string Value22
            {
                get { return base.Details.Value22; }
                set { base.Details.Value22 = value; }
            }
            public string Value23
            {
                get { return base.Details.Value23; }
                set { base.Details.Value23 = value; }
            }
            public string Value24
            {
                get { return base.Details.Value24; }
                set { base.Details.Value24 = value; }
            }
            public string Value25
            {
                get { return base.Details.Value25; }
                set { base.Details.Value25 = value; }
            }
            public string Value26
            {
                get { return base.Details.Value26; }
                set { base.Details.Value26 = value; }
            }
            public string Value27
            {
                get { return base.Details.Value27; }
                set { base.Details.Value27 = value; }
            }
            public string Value28
            {
                get { return base.Details.Value28; }
                set { base.Details.Value28 = value; }
            }
            public string Value29
            {
                get { return base.Details.Value29; }
                set { base.Details.Value29 = value; }
            }
            public string Value30
            {
                get { return base.Details.Value30; }
                set { base.Details.Value30 = value; }
            }
            public string Value31
            {
                get { return base.Details.Value31; }
                set { base.Details.Value31 = value; }
            }
            public string Value32
            {
                get { return base.Details.Value32; }
                set { base.Details.Value32 = value; }
            }
            public string Value33
            {
                get { return base.Details.Value33; }
                set { base.Details.Value33 = value; }
            }
            public string Value34
            {
                get { return base.Details.Value34; }
                set { base.Details.Value34 = value; }
            }
            public string Value35
            {
                get { return base.Details.Value35; }
                set { base.Details.Value35 = value; }
            }
            public string Value36
            {
                get { return base.Details.Value36; }
                set { base.Details.Value36 = value; }
            }
            public string Value37
            {
                get { return base.Details.Value37; }
                set { base.Details.Value37 = value; }
            }
            public string Value38
            {
                get { return base.Details.Value38; }
                set { base.Details.Value38 = value; }
            }
            public string Value39
            {
                get { return base.Details.Value39; }
                set { base.Details.Value39 = value; }
            }
            public string Value40
            {
                get { return base.Details.Value40; }
                set { base.Details.Value40 = value; }
            }
            public string Value41
            {
                get { return base.Details.Value41; }
                set { base.Details.Value41 = value; }
            }
            public string Value42
            {
                get { return base.Details.Value42; }
                set { base.Details.Value42 = value; }
            }
            public string Value43
            {
                get { return base.Details.Value43; }
                set { base.Details.Value43 = value; }
            }
            public string Value44
            {
                get { return base.Details.Value44; }
                set { base.Details.Value44 = value; }
            }
            public string Value45
            {
                get { return base.Details.Value45; }
                set { base.Details.Value45 = value; }
            }
            public string Value46
            {
                get { return base.Details.Value46; }
                set { base.Details.Value46 = value; }
            }
            public string Value47
            {
                get { return base.Details.Value47; }
                set { base.Details.Value47 = value; }
            }
            public string Value48
            {
                get { return base.Details.Value48; }
                set { base.Details.Value48 = value; }
            }
            public string Value49
            {
                get { return base.Details.Value49; }
                set { base.Details.Value49 = value; }
            }
            public string Value50
            {
                get { return base.Details.Value50; }
                set { base.Details.Value50 = value; }
            }
            public DateTime CreatedDate
            {
                get { return base.Details.CreatedDate; }
                set { base.Details.CreatedDate = value; }
            }
            public string CreatedBy
            {
                get { return base.Details.CreatedBy; }
                set { base.Details.CreatedBy = value; }
            }
            public DateTime UpdatedDate
            {
                get { return base.Details.UpdatedDate; }
                set { base.Details.UpdatedDate = value; }
            }
            public string UpdatedBy
            {
                get { return base.Details.UpdatedBy; }
                set { base.Details.UpdatedBy = value; }
            }
            public bool Hidden
            {
                get { return base.Details.Hidden; }
                set { base.Details.Hidden = value; }
            }
            public bool Deleted
            {
                get { return base.Details.Deleted; }
                set { base.Details.Deleted = value; }
            }

            public bool HasChildren
            {
                get
                {
                    return LiquidCore.Data.ObjectData.FindChildren(base.Details);
                }
            }
            public bool HasParent
            {
                get
                {
                    return base.Details.ParentId.Equals(0) ? false : true;
                }
            }

            public Settings Settings
            {
                get 
				{
					if (_settings == null)
					{
						_settings = new Settings(base.Details.Id);
					}
					return _settings;
				}
            }
            public void CreateSetting(String Name, String Value)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::CreateSetting]";
                try
                {
                    if (base.Details.Id.Equals(0))
                        throw new Exception("This is not created yet, bad bad programmer...");
                    Setting s = new Setting();
                    s.Status = 1;
                    s.Language = 1;
                    s.SitId = base.Details.SitId;
                    s.PagId = 0;
                    s.ModId = 0;
                    s.Pointer = base.Details.Id;
                    s.Title = Name;
                    s.Alias = "Object-Setting";
                    s.Description = Name;
                    s.Value = Value;
                    s.Save();
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public void CreateSetting(String Name, String Value, String Description)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::CreateSetting]";
                try
                {
                    if (base.Details.Id.Equals(0))
                        throw new Exception("This is not created yet, bad bad programmer...");
                    Setting s = new Setting();
                    s.Status = 1;
                    s.Language = 1;
                    s.SitId = base.Details.SitId;
                    s.PagId = 0;
                    s.ModId = 0;
                    s.Pointer = base.Details.Id;
                    s.Title = Name;
                    s.Alias = "Object-Setting";
                    s.Description = Description;
                    s.Value = Value;
                    s.Save();
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public void CreateSetting(String Name)
            {
                CreateSetting(Name, String.Empty);
            }
            public void SaveSetting(String Name, String Value)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::SaveSetting]";
                try
                {
                    Boolean Exist = false;
                    foreach (Setting s in this.Settings)
                        if (s.Title.ToLower().Equals(Name.ToLower()))
                        {
                            s.Value = Value;
                            s.Save();
                            Exist = true;
                        }
                    if (!Exist)
                        CreateSetting(Name, Value);
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public void SaveSetting(String Name, String Description, String Value)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::SaveSetting]";
                try
                {
                    Boolean Exist = false;
                    foreach (Setting s in this.Settings)
                        if (s.Title.ToLower().Equals(Name.ToLower()) && s.Description.ToLower().Equals(Description.ToLower()))
                        {
                            s.Value = Value;
                            s.Save();
                            Exist = true;
                        }
                    if (!Exist)
                        CreateSetting(Name, Value);
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public String GetSetting(String Name)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::GetSetting]";
                try
                {
                    foreach (Setting s in this.Settings)
                        if (s.Title.ToLower().Equals(Name.ToLower()))
                            return s.Value;
                    return String.Empty;
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                    return String.Empty;
                }
            }
            public String GetSetting(String Name, String Description)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::GetSetting]";
                try
                {
                    foreach (Setting s in this.Settings)
                        if (s.Title.ToLower().Equals(Name.ToLower()) && s.Description.ToLower().Equals(Description.ToLower()))
                            return s.Value;
                    return String.Empty;
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                    return String.Empty;
                }
            }
            public String[] GetSettings(String Description)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::GetSettings]";
                ArrayList a = new ArrayList();
                try
                {
                    foreach (Setting s in this.Settings)
                        if (s.Description.ToLower().Equals(Description.ToLower()))
                            a.Add(s.Value);
                    return (string[])a.ToArray(typeof(string));
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                    return (string[])a.ToArray(typeof(string));
                }
            }

            public Item() : base() { }
            public Item(Int32 Id) : base(Id) { }

            public void ChangeOrderUp()
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::ChangeOrderUp]";
                try
                {
                    if (base.Details.Id.Equals(0))
                        throw new Exception("This is not created yet, bad bad programmer...");
                    if (this.Order > 1)
                        this.Order = this.Order - 3;
                    this.Save();
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public void ChangeOrderDown()
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::ChangeOrderDown]";
                try
                {
                    if (base.Details.Id.Equals(0))
                        throw new Exception("This is not created yet, bad bad programmer...");
                    this.Order = this.Order + 3;
                    this.Save();
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
        }
    }
    public class Aggregation
    { 
        public static void Add(Int32 SitId, Int32 PagId, Int32 ModId)
        {
            LiquidCore.Data.ModuleData.AddAggregation(SitId, PagId, ModId);
        }
        public static void Delete(Int32 SitId, Int32 PagId, Int32 ModId)
        {
            LiquidCore.Data.ModuleData.DeleteAggregation(SitId, PagId, ModId);
        }
        public static List<Module> GetAggregatableModules(Int32 SitId)
        {
            List<Module> _modules = new List<Module>();
            CoreLib.NameValueSet DataFields = new global::LiquidCore.CoreLib.NameValueSet();
            DataFields["SitId"] = SitId;
            DataFields["GetAggregatableModules"] = 1;
            LiquidCore.Data.ModuleData.LoadAll(ref _modules, DataFields);
            return _modules;  
        }
    }
    public class Menu : MenuDefinition
    {
        static string CLASSNAME = "[Namespace::LiquidCore][Class::Menu]";
        public Menu() : base() { }
        public Menu(Int32 SitId, Int32 ParentId) : base(SitId, ParentId) { }
		public Menu(Int32 SitId, Int32 ParentId, Int32 Status) : base(SitId, ParentId, Status) { }
        public Menu(String Alias) : base(Alias) { }
        public class Item : MenuDefinition.ItemDefinition
        {
            static string CLASSNAME = "[Namespace::LiquidCore.Menu][Class::Item]";
            private Settings _settings;
            private Modules _modules;
            private Int32[] _parents;
            public int Id
            {
                get { return base.Details.Id; }
                set { base.Details.Id = value; }
            }
            public int SitId
            {
                get { return base.Details.SitId; }
                set { base.Details.SitId = value; }
            }
            public int Status
            {
                get { return base.Details.Status; }
                set { base.Details.Status = value; }
            }
            public int Language
            {
                get { return base.Details.Language; }
                set { base.Details.Language = value; }
            }
            public int ModelId
            {
                get { return base.Details.ModelId; }
                set { base.Details.ModelId = value; }
            }
            public int ParentId
            {
                get { return base.Details.ParentId; }
                set { base.Details.ParentId = value; }
            }
            public int Order
            {
                get { return base.Details.Order; }
                set { base.Details.Order = value; }
            }
            public string Title
            {
                get { return base.Details.Title; }
                set { base.Details.Title = value; }
            }
            public string Alias
            {
                get { return base.Details.Alias; }
                set { base.Details.Alias = value; }
            }
            public string Description
            {
                get { return base.Details.Description; }
                set { base.Details.Description = value; }
            }
            public int[] AuthorizedRoles
            {
                get { return base.Details.AuthorizedRoles; }
            }
            public int[] AuthorizedEditRoles
            {
                get { return base.Details.AuthorizedEditRoles; }
            }
            public string Template
            {
                get { return base.Details.Template; }
                set { base.Details.Template = value; }
            }
            public DateTime CreatedDate
            {
                get { return base.Details.CreatedDate; }
                set { base.Details.CreatedDate = value; }
            }
            public string CreatedBy
            {
                get { return base.Details.CreatedBy; }
                set { base.Details.CreatedBy = value; }
            }
            public DateTime UpdatedDate
            {
                get { return base.Details.UpdatedDate; }
                set { base.Details.UpdatedDate = value; }
            }
            public string UpdatedBy
            {
                get { return base.Details.UpdatedBy; }
                set { base.Details.UpdatedBy = value; }
            }
            public bool Hidden
            {
                get { return base.Details.Hidden; }
                set { base.Details.Hidden = value; }
            }
            public bool MobileHidden
            {
                get { return base.Details.MobileHidden; }
                set { base.Details.MobileHidden = value; }
            }
            public bool Deleted
            {
                get { return base.Details.Deleted; }
                set { base.Details.Deleted = value; }
            }

            public bool HasChildren
            {
                get
                {
                    return LiquidCore.Data.MenuData.FindChildren(base.Details);
                }
            }
            public bool HasParent
            {
                get
                {
                    return base.Details.ParentId.Equals(0) ? false : true;
                }
            }
            public int Level
            {
                get
                {
                    if (!this.ParentId.Equals(0))
                        return (this.Parents.Length + 1);
                    return 1;
                }
            }
            public Int32[] Parents
            {
                get
                {
                    string FUNCTIONNAME = CLASSNAME + "[Function::Parents]";
                    try
                    {
                        if (base.Details.Id.Equals(0))
                            throw new Exception("This is not created yet, bad bad programmer...");
                        return LiquidCore.Data.MenuData.GetParents(base.Details);
                    }
                    catch (Exception ex)
                    {
                        Error.Report(ex, FUNCTIONNAME, "");
                        return new Int32[0];
                    }
                }
            }

            public Settings Settings
            {
                get 
				{
					if (_settings == null)
					{
						_settings = new Settings(this.SitId, this.Id, 0, 0);
					}
					return _settings;
				}
            }

            public Modules Modules
            {
                get
				{
					if (_modules == null)
					{
						_modules = new Modules(this.SitId, this.Id, 0, false);
					}
					return _modules;
				}
            }

            public Item() : base()  { }
            public Item(Int32 Id) : base(Id) { }
			public Item(DataRow dr) : base(dr) { }
			public Item(bool PreventSortOnSave) : base(PreventSortOnSave) { }

            public int[] GetAuthorizedViewRoles()
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::GetAuthorizedViewRoles]";
                try
                {
                    if (base.Details.Id.Equals(0))
                        throw new Exception("This is not created yet, bad bad programmer...");
                    return base.Details.AuthorizedRoles;
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                    return new int[0];
                }
            }
            public void AddAuthorizedViewRole(Int32 RolId)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::AddAuthorizedViewRole]";
                try
                {
                    if (base.Details.Id.Equals(0))
                        throw new Exception("This is not created yet, bad bad programmer...");
                    LiquidCore.Data.MenuData.AddAuthorizedRole(base.Details, RolId);
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public void DeleteAuthorizedViewRole(Int32 RolId)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::DeleteAuthorizedViewRole]";
                try
                {
                    if (base.Details.Id.Equals(0))
                        throw new Exception("This is not created yet, bad bad programmer...");
                    LiquidCore.Data.MenuData.DeleteAuthorizedRole(base.Details, RolId);
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public int[] GetAuthorizedEditRoles()
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::GetAuthorizedEditRoles]";
                try
                {
                    if (base.Details.Id.Equals(0))
                        throw new Exception("This is not created yet, bad bad programmer...");
                    return base.Details.AuthorizedEditRoles;
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                    return new int[0];
                }
            }
            public void AddAuthorizedEditRole(Int32 RolId)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::AddAuthorizedEditRole]";
                try
                {
                    if (base.Details.Id.Equals(0))
                        throw new Exception("This is not created yet, bad bad programmer...");
                    LiquidCore.Data.MenuData.AddAuthorizedEditRole(base.Details, RolId);
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public void DeleteAuthorizedEditRole(Int32 RolId)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::DeleteAuthorizedEditRole]";
                try
                {
                    if (base.Details.Id.Equals(0))
                        throw new Exception("This is not created yet, bad bad programmer...");
                    LiquidCore.Data.MenuData.DeleteAuthorizedEditRole(base.Details, RolId);
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public void ChangeOrderUp()
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::ChangeOrderUp]";
                try
                {
                    if (base.Details.Id.Equals(0))
                        throw new Exception("This is not created yet, bad bad programmer...");
                    if (this.Order > 1)
                        this.Order = this.Order - 3;
                    this.Save();
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public void ChangeOrderDown()
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::ChangeOrderDown]";
                try
                {
                    if (base.Details.Id.Equals(0))
                        throw new Exception("This is not created yet, bad bad programmer...");
                    this.Order = this.Order + 3;
                    this.Save();
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public void CreateSetting(String Name, String Value)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::CreateSetting]";
                try
                {
                    if (base.Details.Id.Equals(0))
                        throw new Exception("This is not created yet, bad bad programmer...");
                    Setting s = new Setting();
                    s.Status = 1;
                    s.Language = 1;
                    s.SitId = base.Details.SitId;
                    s.PagId = base.Details.Id;
                    s.ModId = 0;
                    s.Pointer = base.Details.Id;
                    s.Title = Name;
                    s.Alias = "User-Setting";
                    s.Description = Name;
                    s.Value = Value;
                    s.Save();
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public void CreateSetting(String Name)
            {
                CreateSetting(Name, String.Empty);
            }
            public void SaveSetting(String Name, String Value)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::SaveSetting]";
                try
                {
                    Boolean Exist = false;
                    foreach (Setting s in this.Settings)
                        if (s.Title.ToLower().Equals(Name.ToLower()))
                        {
                            s.Value = Value;
                            s.Save();
                            Exist = true;
                        }
                    if (!Exist)
                        CreateSetting(Name, Value);
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public String GetSetting(String Name)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::ChangeSetting]";
                try
                {
                    foreach (Setting s in this.Settings)
                        if (s.Title.ToLower().Equals(Name.ToLower()))
                            return s.Value;
                    return String.Empty;
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                    return String.Empty;
                }
            }
        }
    }
    public class List : ListDefinition
    {
        string CLASSNAME = "[Namespace::LiquidCore][Class::List]";
        private Settings _settings;
        private Int32 _sitid;
        private Int32 _pagid;
        private Int32 _modid;
        public Int32 SitId
        {
            get
            {
                return this._sitid; 
            }
        }
        public Int32 PagId
        {
            get
            {
                return this._pagid;
            }
        }
        public Int32 ModId
        {
            get
            {
                return this._modid;
            }
        }
        public Int32 Order
        {
            get
            {
                return new Module(this._modid).Order;
            }
        }
        public Int32 Language
        {
            get
            {
                return new Module(this._modid).Language;
            }
        }
        public Int32 Status
        {
            get
            {
                return new Module(this._modid).Status;
            }
            set
            {
                Module m = new Module(this._modid);
                m.Status = value;
                m.Save();
            }
        }
        public Boolean Hidden
        {
            get
            {
                return new Module(this._modid).Hidden;
            }
            set
            {
                Module m = new Module(this._modid);
                m.Hidden = value;
                m.Save();
            }
        }
        public Boolean UseSSL
        {
            get
            {
                return new Module(this._modid).SSL;
            }
        }
        public Boolean AllPages
        {
            get
            {
                return new Module(this._modid).AllPages;
            }
        }
        public String ContentPane
        {
            get
            {
                return new Module(this._modid).ContentPane;
            }
            set
            {
                Module m = new Module(this._modid);
                m.ContentPane = value;
                m.Save();
            }
        }
        public Settings Settings
        {
            get 
			{
				if (_settings == null)
				{
					_settings = new Settings(this.SitId, this.PagId, this.ModId, 0);
				}

				return _settings;
			}
        }
        public List() : base() { }
        public List(String alias, Prefix prefix, Param[] parameters) : base(alias, prefix, parameters) { }
		public List(String alias, Prefix prefix, Param[] parameters, bool PreventSortOnSave) : base(alias, prefix, parameters, PreventSortOnSave) { }
        public List(Int32 SitId, Int32 PagId, Int32 ModId) : base(SitId, PagId, ModId) { Init(SitId, PagId, ModId); }
        public List(Int32 SitId, Int32 PagId, Int32 ModId, Int32 MaxRowCount, String StartDate, String EndDate) : base(SitId, PagId, ModId, MaxRowCount, StartDate, EndDate) { Init(SitId, PagId, ModId); }
        public List(Int32 SitId, Int32 PagId, Int32 MaxRowCount, String StartDate, String EndDate) : base(SitId, PagId, MaxRowCount, StartDate, EndDate) { Init(SitId, PagId, 0); }
        public List(Int32 SitId, Int32 MaxRowCount, String StartDate, String EndDate) : base(SitId, MaxRowCount, StartDate, EndDate) { Init(SitId, 0, 0); } 
        public List(String Alias) : base(Alias) { }
		public List(String Alias, bool PreventSortOnSave) : base(Alias, PreventSortOnSave) { }
        public static Int32 Create(Int32 SitId, Int32 PagId, Int32 MdeId, String ContentPane, Int32 Language, Int32 Status, Boolean UseSSL, Boolean AllPages)
        {
            return Create(SitId, PagId, MdeId, ContentPane, Language, Status, UseSSL, AllPages, true);
        }
        public static Int32 Create(Int32 SitId, Int32 PagId, Int32 MdeId, String ContentPane, Int32 Language, Int32 Status, Boolean UseSSL, Boolean AllPages, Boolean Hidden)
        {
            string FUNCTIONNAME = "[Namespace::LiquidCore][Class::List][Function::Create]";
            Int32 ModId = 0;
            try
            {
                using (ModDef mde = new ModDef(MdeId))
                {
                    // Kolla om ModDef existerar...
                    if (mde.Id.Equals(0))
                        throw new Exception("ModuleDefinition with Id: " + MdeId.ToString() + " does not exist in database.");

                    // Skapa modulen...
                    using (Module m = new Module())
                    {
                        m.Status = Status;
                        m.Language = Language;
                        m.SitId = SitId;
                        m.PagId = PagId;
                        m.MdeId = MdeId;
                        m.ContentPane = ContentPane;
                        m.Src = mde.Src;
                        m.Title = mde.Title + "_Instance";
                        m.Alias = mde.Alias + "_Instance";
                        m.Description = mde.Description + "_Instance";
                        m.SSL = UseSSL;
                        m.AllPages = AllPages;
                        m.Hidden = Hidden;
                        m.Save();
                        ModId = m.Id;
                    }
                }
                return ModId; ;
            }
            catch (Exception ex)
            {
                Error.Report(ex, FUNCTIONNAME, "");
                return ModId;
            }
        }
        public void ChangeOrderUp() 
        {
            new Module(this._modid).ChangeOrderUp();  
        }
        public void ChangeOrderDown()
        {
            new Module(this._modid).ChangeOrderDown();  
        }
        private void Init(Int32 SitId, Int32 PagId, Int32 ModId)
        {
            _sitid = SitId;
            _pagid = PagId;
            _modid = ModId;
        }
        public void CreateSetting(String Name, String Value)
        {
            string FUNCTIONNAME = CLASSNAME + "[Function::CreateSetting]";
            try
            {
                if (_modid.Equals(0))
                    throw new Exception("This is not created yet, bad bad programmer...");
                Setting s = new Setting();
                s.Status = 1;
                s.Language = 1;
                s.SitId = _sitid;
                s.PagId = _pagid;
                s.ModId = _modid;
                s.Pointer = 0;
                s.Title = Name;
                s.Alias = "List-Setting";
                s.Description = Name;
                s.Value = Value;
                s.Save();
            }
            catch (Exception ex)
            {
                Error.Report(ex, FUNCTIONNAME, "");
            }
        }
        public void CreateSetting(String Name)
        {
            CreateSetting(Name, String.Empty);
        }
        public void SaveSetting(String Name, String Value)
        {
            string FUNCTIONNAME = CLASSNAME + "[Function::SaveSetting]";
            try
            {
                Boolean Exist = false;
                foreach (Setting s in this.Settings)
                    if (s.Title.ToLower().Equals(Name.ToLower()))
                    {
                        s.Value = Value;
                        s.Save();
                        Exist = true;
                    }
                if (!Exist)
                    CreateSetting(Name, Value);
            }
            catch (Exception ex)
            {
                Error.Report(ex, FUNCTIONNAME, "");
            }
        }

        public String GetSetting(String Name)
        {
            string FUNCTIONNAME = CLASSNAME + "[Function::ChangeSetting]";
            try
            {
                foreach (Setting s in this.Settings)
                    if (s.Title.ToLower().Equals(Name.ToLower()))
                        return s.Value;
                return String.Empty;
            }
            catch (Exception ex)
            {
                Error.Report(ex, FUNCTIONNAME, "");
                return String.Empty;
            }
        }
        public class Item : ListDefinition.ItemDefinition
        {
            static string CLASSNAME = "[Namespace::LiquidCore][Class::List.Item]";
            private Settings _settings;
            public int Id
            {
                get { return base.Details.Id; }
                set { base.Details.Id = value; }
            }
            public int SitId
            {
                get { return base.Details.SitId; }
                set { base.Details.SitId = value; }
            }
            public int PagId
            {
                get { return base.Details.PagId; }
                set { base.Details.PagId = value; }
            }
            public int ModId
            {
                get { return base.Details.ModId; }
                set { base.Details.ModId = value; }
            }
            public int Status
            {
                get { return base.Details.Status; }
                set { base.Details.Status = value; }
            }
            public int Language
            {
                get { return base.Details.Language; }
                set { base.Details.Language = value; }
            }
            public int Type
            {
                get { return base.Details.Type; }
                set { base.Details.Type = value; }
            }
            public int ParentId
            {
                get { return base.Details.ParentId; }
                set { base.Details.ParentId = value; }
            }
            public int Order
            {
                get { return base.Details.Order; }
                set { base.Details.Order = value; }
            }
            public string Title
            {
                get { return base.Details.Title; }
                set { base.Details.Title = value; }
            }
            public string Alias
            {
                get { return base.Details.Alias; }
                set { base.Details.Alias = value; }
            }
            public string Description
            {
                get { return base.Details.Description; }
                set { base.Details.Description = value; }
            }
            public string Value1
            {
                get { return base.Details.Value1; }
                set { base.Details.Value1 = value; }
            }
            public string Value2
            {
                get { return base.Details.Value2; }
                set { base.Details.Value2 = value; }
            }
            public string Value3
            {
                get { return base.Details.Value3; }
                set { base.Details.Value3 = value; }
            }
            public string Value4
            {
                get { return base.Details.Value4; }
                set { base.Details.Value4 = value; }
            }
            public string Value5
            {
                get { return base.Details.Value5; }
                set { base.Details.Value5 = value; }
            }
            public string Value6
            {
                get { return base.Details.Value6; }
                set { base.Details.Value6 = value; }
            }
            public string Value7
            {
                get { return base.Details.Value7; }
                set { base.Details.Value7 = value; }
            }
            public string Value8
            {
                get { return base.Details.Value8; }
                set { base.Details.Value8 = value; }
            }
            public string Value9
            {
                get { return base.Details.Value9; }
                set { base.Details.Value9 = value; }
            }
            public string Value10
            {
                get { return base.Details.Value10; }
                set { base.Details.Value10 = value; }
            }
            public string Value11
            {
                get { return base.Details.Value11; }
                set { base.Details.Value11 = value; }
            }
            public string Value12
            {
                get { return base.Details.Value12; }
                set { base.Details.Value12 = value; }
            }
            public string Value13
            {
                get { return base.Details.Value13; }
                set { base.Details.Value13 = value; }
            }
            public string Value14
            {
                get { return base.Details.Value14; }
                set { base.Details.Value14 = value; }
            }
            public string Value15
            {
                get { return base.Details.Value15; }
                set { base.Details.Value15 = value; }
            }
            public string Value16
            {
                get { return base.Details.Value16; }
                set { base.Details.Value16 = value; }
            }
            public string Value17
            {
                get { return base.Details.Value17; }
                set { base.Details.Value17 = value; }
            }
            public string Value18
            {
                get { return base.Details.Value18; }
                set { base.Details.Value18 = value; }
            }
            public string Value19
            {
                get { return base.Details.Value19; }
                set { base.Details.Value19 = value; }
            }
            public string Value20
            {
                get { return base.Details.Value20; }
                set { base.Details.Value20 = value; }
            }
            public string Value21
            {
                get { return base.Details.Value21; }
                set { base.Details.Value21 = value; }
            }
            public string Value22
            {
                get { return base.Details.Value22; }
                set { base.Details.Value22 = value; }
            }
            public string Value23
            {
                get { return base.Details.Value23; }
                set { base.Details.Value23 = value; }
            }
            public string Value24
            {
                get { return base.Details.Value24; }
                set { base.Details.Value24 = value; }
            }
            public string Value25
            {
                get { return base.Details.Value25; }
                set { base.Details.Value25 = value; }
            }
            public string Value26
            {
                get { return base.Details.Value26; }
                set { base.Details.Value26 = value; }
            }
            public string Value27
            {
                get { return base.Details.Value27; }
                set { base.Details.Value27 = value; }
            }
            public string Value28
            {
                get { return base.Details.Value28; }
                set { base.Details.Value28 = value; }
            }
            public string Value29
            {
                get { return base.Details.Value29; }
                set { base.Details.Value29 = value; }
            }
            public string Value30
            {
                get { return base.Details.Value30; }
                set { base.Details.Value30 = value; }
            }
            public string Value31
            {
                get { return base.Details.Value31; }
                set { base.Details.Value31 = value; }
            }
            public string Value32
            {
                get { return base.Details.Value32; }
                set { base.Details.Value32 = value; }
            }
            public string Value33
            {
                get { return base.Details.Value33; }
                set { base.Details.Value33 = value; }
            }
            public string Value34
            {
                get { return base.Details.Value34; }
                set { base.Details.Value34 = value; }
            }
            public string Value35
            {
                get { return base.Details.Value35; }
                set { base.Details.Value35 = value; }
            }
            public string Value36
            {
                get { return base.Details.Value36; }
                set { base.Details.Value36 = value; }
            }
            public string Value37
            {
                get { return base.Details.Value37; }
                set { base.Details.Value37 = value; }
            }
            public string Value38
            {
                get { return base.Details.Value38; }
                set { base.Details.Value38 = value; }
            }
            public string Value39
            {
                get { return base.Details.Value39; }
                set { base.Details.Value39 = value; }
            }
            public string Value40
            {
                get { return base.Details.Value40; }
                set { base.Details.Value40 = value; }
            }
            public string Value41
            {
                get { return base.Details.Value41; }
                set { base.Details.Value41 = value; }
            }
            public string Value42
            {
                get { return base.Details.Value42; }
                set { base.Details.Value42 = value; }
            }
            public string Value43
            {
                get { return base.Details.Value43; }
                set { base.Details.Value43 = value; }
            }
            public string Value44
            {
                get { return base.Details.Value44; }
                set { base.Details.Value44 = value; }
            }
            public string Value45
            {
                get { return base.Details.Value45; }
                set { base.Details.Value45 = value; }
            }
            public string Value46
            {
                get { return base.Details.Value46; }
                set { base.Details.Value46 = value; }
            }
            public string Value47
            {
                get { return base.Details.Value47; }
                set { base.Details.Value47 = value; }
            }
            public string Value48
            {
                get { return base.Details.Value48; }
                set { base.Details.Value48 = value; }
            }
            public string Value49
            {
                get { return base.Details.Value49; }
                set { base.Details.Value49 = value; }
            }
            public string Value50
            {
                get { return base.Details.Value50; }
                set { base.Details.Value50 = value; }
            }
            public DateTime CreatedDate
            {
                get { return base.Details.CreatedDate; }
                set { base.Details.CreatedDate = value; }
            }
            public string CreatedBy
            {
                get { return base.Details.CreatedBy; }
                set { base.Details.CreatedBy = value; }
            }
            public DateTime UpdatedDate
            {
                get { return base.Details.UpdatedDate; }
                set { base.Details.UpdatedDate = value; }
            }
            public string UpdatedBy
            {
                get { return base.Details.UpdatedBy; }
                set { base.Details.UpdatedBy = value; }
            }
            public bool Hidden
            {
                get { return base.Details.Hidden; }
                set { base.Details.Hidden = value; }
            }
            public bool Deleted
            {
                get { return base.Details.Deleted; }
                set { base.Details.Deleted = value; }
            }

            public bool HasChildren
            {
                get
                {
                    return LiquidCore.Data.ListData.FindChildren(base.Details);
                }
            }
            public bool HasParent
            {
                get
                {
                    return base.Details.ParentId.Equals(0) ? false : true;
                }
            }

            public Settings Settings
            {
                get 
				{
					if (_settings == null)
					{
						_settings = new Settings(this.Id);
					}

					return _settings;
				}
            }

            public Item() : base() { }
            public Item(Int32 Id) : base(Id) { }
			public Item(DataRow dr) : base(dr) { }
            public Item(Boolean PreventSortOnSave) : base(PreventSortOnSave) {  }
			public Item(Int32 Id, Boolean PreventSortOnSave) : base(Id, PreventSortOnSave) { }
			public Item(DataRow dr, Boolean PreventSortOnSave) : base(dr, PreventSortOnSave) { }

            public void ChangeOrderUp()
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::ChangeOrderUp]";
                try
                {
                    if (base.Details.Id.Equals(0))
                        throw new Exception("This is not created yet, bad bad programmer...");
                    if (this.Order > 1)
                        this.Order = this.Order - 3;
                    this.Save();
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public void ChangeOrderDown()
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::ChangeOrderDown]";
                try
                {
                    if (base.Details.Id.Equals(0))
                        throw new Exception("This is not created yet, bad bad programmer...");
                    this.Order = this.Order + 3;
                    this.Save();
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public void CreateSetting(String Name, String Value)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::CreateSetting]";
                try
                {
                    if (base.Details.Id.Equals(0))
                        throw new Exception("This is not created yet, bad bad programmer...");
                    Setting s = new Setting();
                    s.Status = 1;
                    s.Language = 1;
                    s.SitId = 0;
                    s.PagId = 0;
                    s.ModId = 0;
                    s.Pointer = base.Details.Id;
                    s.Title = Name;
                    s.Alias = "Item-Setting";
                    s.Description = Name;
                    s.Value = Value;
                    s.Save();
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public void CreateSetting(String Name)
            {
                CreateSetting(Name, String.Empty);
            }
            public void SaveSetting(String Name, String Value)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::SaveSetting]";
                try
                {
                    Boolean Exist = false;
                    foreach (Setting s in this.Settings)
                        if (s.Title.ToLower().Equals(Name.ToLower()))
                        {
                            s.Value = Value;
                            s.Save();
                            Exist = true;
                        }
                    if (!Exist)
                        CreateSetting(Name, Value);
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public String GetSetting(String Name)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::ChangeSetting]";
                try
                {
                    foreach (Setting s in this.Settings)
                        if (s.Title.ToLower().Equals(Name.ToLower()))
                            return s.Value;
                    return String.Empty;
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                    return String.Empty;
                }
            }
            public String[] GetSettings()
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::GetSettings]";
                ArrayList a = new ArrayList();
                try
                {
                    foreach (Setting s in this.Settings)
                        a.Add(s.Value);
                    return (string[])a.ToArray(typeof(string));
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                    return (string[])a.ToArray(typeof(string));
                }
            }

			public List<String> Invoices
			{
				get
				{
					if (this.Alias == "order")
					{
						List<String> list = new List<String>();

						foreach (String s in this.Value3.Split('|'))
						{
							if (s.Trim() == "")
							{
								continue;
							}

							list.Add(s);
						}

						return list;
					}

					return null;
				}
				set
				{
					if (this.Alias == "order")
					{
						String val = "";
						foreach (String s in value)
						{
							if (s.Trim() == "")
							{
								continue;
							}

							val += s + "|";
						}

						if (val.EndsWith("|"))
						{
							val = val.Substring(0, val.Length - 1);
						}

						this.Value3 = "|" + val + "|";
					}
				}
			}

			public List<String> OrderSums
			{
				get
				{
					if (this.Alias == "order")
					{
						List<String> list = new List<String>();

						foreach (String s in this.Value4.Split('|'))
						{
							if (s.Trim() == "")
							{
								continue;
							}

							list.Add(s);
						}

						return list;
					}

					return null;
				}
				set
				{
					if (this.Alias == "order")
					{
						String val = "";
						foreach (String s in value)
						{
							if (s.Trim() == "")
							{
								continue;
							}

							val += s + "|";
						}

						if (val.EndsWith("|"))
						{
							val = val.Substring(0, val.Length - 1);
						}

						this.Value4 = "|" + val + "|";
					}
				}
			}

			public List<String> InvoiceDates
			{
				get
				{
					if (this.Alias == "order")
					{
						List<String> list = new List<String>();

						foreach (String s in this.Value14.Split('|'))
						{
							if (s.Trim() == "")
							{
								continue;
							}

							list.Add(s);
						}

						return list;
					}

					return null;
				}
				set
				{
					if (this.Alias == "order")
					{
						String val = "";
						foreach (String s in value)
						{
							if (s.Trim() == "")
							{
								continue;
							}

							val += s + "|";
						}

						if (val.EndsWith("|"))
						{
							val = val.Substring(0, val.Length - 1);
						}

						this.Value14 = "|" + val + "|";
					}
				}
			}
        }
    }
    public class UserTypes : UserTypesDefinition
    {
        string CLASSNAME = "[Namespace::LiquidCore][Class::UserTypes]";
        public UserTypes() : base() { }
        public class UserType : UserTypesDefinition.UserTypeDefinition
        {
            static string CLASSNAME = "[Namespace::LiquidCore][Class::UserTypes.UserType]";
            public int Id
            {
                get { return base.Details.Id; }
                set { base.Details.Id = value; }
            }
            public int Status
            {
                get { return base.Details.Status; }
                set { base.Details.Status = value; }
            }
            public int Language
            {
                get { return base.Details.Language; }
                set { base.Details.Language = value; }
            }
            public int ParentId
            {
                get { return base.Details.ParentId; }
                set { base.Details.ParentId = value; }
            }
            public int Order
            {
                get { return base.Details.Order; }
                set { base.Details.Order = value; }
            }
            public string Title
            {
                get { return base.Details.Title; }
                set { base.Details.Title = value; }
            }
            public string Alias
            {
                get { return base.Details.Alias; }
                set { base.Details.Alias = value; }
            }
            public string Description
            {
                get { return base.Details.Description; }
                set { base.Details.Description = value; }
            }
            public DateTime CreatedDate
            {
                get { return base.Details.CreatedDate; }
                set { base.Details.CreatedDate = value; }
            }
            public string CreatedBy
            {
                get { return base.Details.CreatedBy; }
                set { base.Details.CreatedBy = value; }
            }
            public DateTime UpdatedDate
            {
                get { return base.Details.UpdatedDate; }
                set { base.Details.UpdatedDate = value; }
            }
            public string UpdatedBy
            {
                get { return base.Details.UpdatedBy; }
                set { base.Details.UpdatedBy = value; }
            }
            public bool Hidden
            {
                get { return base.Details.Hidden; }
                set { base.Details.Hidden = value; }
            }
            public bool Deleted
            {
                get { return base.Details.Deleted; }
                set { base.Details.Deleted = value; }
            }

            public bool HasChildren
            {
                get
                {
                    return LiquidCore.Data.UserTypeData.FindChildren(base.Details);
                }
            }
            public bool HasParent
            {
                get
                {
                    return base.Details.ParentId.Equals(0) ? false : true;
                }
            }

            public UserType() : base() { }
            public UserType(Int32 Id) : base(Id) { }

            public void ChangeOrderUp()
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::ChangeOrderUp]";
                try
                {
                    if (base.Details.Id.Equals(0))
                        throw new Exception("This is not created yet, bad bad programmer...");
                    if (this.Order > 1)
                        this.Order = this.Order - 3;
                    this.Save();
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public void ChangeOrderDown()
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::ChangeOrderDown]";
                try
                {
                    if (base.Details.Id.Equals(0))
                        throw new Exception("This is not created yet, bad bad programmer...");
                    this.Order = this.Order + 3;
                    this.Save();
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
        }
    }
    public class Roles : RolesDefinition
    {
        string CLASSNAME = "[Namespace::LiquidCore][Class::Roles]";
        public Roles() : base() { }
        public class Role : RolesDefinition.RoleDefinition
        {
            string CLASSNAME = "[Namespace::LiquidCore.Roles][Class::Role]";
            public int Id
            {
                get { return base.Details.Id; }
                set { base.Details.Id = value; }
            }
            public int Status
            {
                get { return base.Details.Status; }
                set { base.Details.Status = value; }
            }
            public int Language
            {
                get { return base.Details.Language; }
                set { base.Details.Language = value; }
            }
            public int ParentId
            {
                get { return base.Details.ParentId; }
                set { base.Details.ParentId = value; }
            }
            public int Order
            {
                get { return base.Details.Order; }
                set { base.Details.Order = value; }
            }
            public string Title
            {
                get { return base.Details.Title; }
                set { base.Details.Title = value; }
            }
            public string Alias
            {
                get { return base.Details.Alias; }
                set { base.Details.Alias = value; }
            }
            public string Description
            {
                get { return base.Details.Description; }
                set { base.Details.Description = value; }
            }
            public DateTime CreatedDate
            {
                get { return base.Details.CreatedDate; }
                set { base.Details.CreatedDate = value; }
            }
            public string CreatedBy
            {
                get { return base.Details.CreatedBy; }
                set { base.Details.CreatedBy = value; }
            }
            public DateTime UpdatedDate
            {
                get { return base.Details.UpdatedDate; }
                set { base.Details.UpdatedDate = value; }
            }
            public string UpdatedBy
            {
                get { return base.Details.UpdatedBy; }
                set { base.Details.UpdatedBy = value; }
            }
            public bool Hidden
            {
                get { return base.Details.Hidden; }
                set { base.Details.Hidden = value; }
            }
            public bool Deleted
            {
                get { return base.Details.Deleted; }
                set { base.Details.Deleted = value; }
            }

            public bool HasChildren
            {
                get
                {
                    return LiquidCore.Data.RoleData.FindChildren(base.Details);
                }
            }
            public bool HasParent
            {
                get
                {
                    return base.Details.ParentId.Equals(0) ? false : true;
                }
            }

            public Role() : base() { }
            public Role(Int32 Id) : base(Id) { }

            public void AddUser(Int32 UsrId)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::AddUser]";
                try
                {
                    if (base.Details.Id.Equals(0))
                        base.Save();
                    LiquidCore.Data.RoleData.AddToUser(base.Details, UsrId);
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                } 
            }
            public void DeleteUser(Int32 UsrId)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::AddUser]";
                try
                {
                    if (base.Details.Id.Equals(0))
                        base.Save();
                    LiquidCore.Data.RoleData.DeleteFromUser(base.Details, UsrId);
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public ArrayList GetUsers()
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::GetUsers]";
                try
                {
                    if (base.Details.Id.Equals(0))
                        base.Save();
                    return LiquidCore.Data.RoleData.GetUsers(base.Details);
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                    return new ArrayList();
                }
            }
            public void ChangeOrderUp()
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::ChangeOrderUp]";
                try
                {
                    if (base.Details.Id.Equals(0))
                        throw new Exception("This is not created yet, bad bad programmer...");
                    if (this.Order > 1)
                        this.Order = this.Order - 3;
                    this.Save();
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public void ChangeOrderDown()
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::ChangeOrderDown]";
                try
                {
                    if (base.Details.Id.Equals(0))
                        throw new Exception("This is not created yet, bad bad programmer...");
                    this.Order = this.Order + 3;
                    this.Save();
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
        }
    }
    public class Users : UsersDefinition
    {
        string CLASSNAME = "[Namespace::LiquidCore][Class::Users]";
        public Users() : base() { }
        public class User : UsersDefinition.UserDefinition
        {
            string CLASSNAME = "[Namespace::LiquidCore.Users][Class::User]";
            private Settings _settings;
            public int Id
            {
                get { return base.Details.Id; }
                set { base.Details.Id = value; }
            }
            public int Status
            {
                get { return base.Details.Status; }
                set { base.Details.Status = value; }
            }
            public int Language
            {
                get { return base.Details.Language; }
                set { base.Details.Language = value; }
            }
            public int Type
            {
                get { return base.Details.Type; }
                set { base.Details.Type = value; }
            }
            public int ParentId
            {
                get { return base.Details.ParentId; }
                set { base.Details.ParentId = value; }
            }
            public int Order
            {
                get { return base.Details.Order; }
                set { base.Details.Order = value; }
            }
            public string Title
            {
                get { return base.Details.Title; }
                set { base.Details.Title = value; }
            }
            public string Alias
            {
                get { return base.Details.Alias; }
                set { base.Details.Alias = value; }
            }
            public string Description
            {
                get { return base.Details.Description; }
                set { base.Details.Description = value; }
            }
            public string Mail
            {
                get { return base.Details.Mail; }
                set { base.Details.Mail = value; }
            }
            public string LoginName
            {
                get { return base.Details.LoginName; }
                set { base.Details.LoginName = value; }
            }
            public string Password
            {
                get { return base.Details.Password; }
                set { base.Details.Password = value; }
            }
            public string FirstName
            {
                get { return base.Details.FirstName; }
                set { base.Details.FirstName = value; }
            }
            public string MiddleName
            {
                get { return base.Details.MiddleName; }
                set { base.Details.MiddleName = value; }
            }
            public string LastName
            {
                get { return base.Details.LastName; }
                set { base.Details.LastName = value; }
            }
            public string Address
            {
                get { return base.Details.Address; }
                set { base.Details.Address = value; }
            }
            public string CO
            {
                get { return base.Details.CO; }
                set { base.Details.CO = value; }
            }
            public string PostalCode
            {
                get { return base.Details.PostalCode; }
                set { base.Details.PostalCode = value; }
            }
            public string City
            {
                get { return base.Details.City; }
                set { base.Details.City = value; }
            }
            public string Country
            {
                get { return base.Details.Country; }
                set { base.Details.Country = value; }
            }
            public string Phone
            {
                get { return base.Details.Phone; }
                set { base.Details.Phone = value; }
            }
            public string Mobile
            {
                get { return base.Details.Mobile; }
                set { base.Details.Mobile = value; }
            }
            public string Fax
            {
                get { return base.Details.Fax; }
                set { base.Details.Fax = value; }
            }
            public string Company
            {
                get { return base.Details.Company; }
                set { base.Details.Company = value; }
            }
            public int StartPage
            {
                get { return base.Details.StartPage; }
                set { base.Details.StartPage = value; }
            }
            public DateTime CreatedDate
            {
                get { return base.Details.CreatedDate; }
                set { base.Details.CreatedDate = value; }
            }
            public string CreatedBy
            {
                get { return base.Details.CreatedBy; }
                set { base.Details.CreatedBy = value; }
            }
            public DateTime UpdatedDate
            {
                get { return base.Details.UpdatedDate; }
                set { base.Details.UpdatedDate = value; }
            }
            public string UpdatedBy
            {
                get { return base.Details.UpdatedBy; }
                set { base.Details.UpdatedBy = value; }
            }
            public bool Hidden
            {
                get { return base.Details.Hidden; }
                set { base.Details.Hidden = value; }
            }
            public bool Deleted
            {
                get { return base.Details.Deleted; }
                set { base.Details.Deleted = value; }
            }

            public bool HasChildren
            {
                get
                {
                    return LiquidCore.Data.UserData.FindChildren(base.Details);
                }
            }
            public bool HasParent
            {
                get
                {
                    return base.Details.ParentId.Equals(0) ? false : true;
                }
            }
            public Settings Settings
            {
                get
				{
					if (_settings == null)
					{
						_settings = new Settings(this.Id);
					}
					return _settings;
				}
            }

            public User() : base() { }
            public User(Int32 Id) : base(Id) { }

            public bool IsAuthorizedForPageView(Int32 PagId)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::IsAuthorizedForPageView]";
                try
                {
                    using (Menu.Item mi = new Menu.Item(PagId))
                    {
                        foreach (Int32 i in mi.AuthorizedRoles)
                        {
                            foreach (object r in GetRoles().ToArray())
                            {
                                if (i.Equals(Convert.ToInt32(r)))
                                    return true;
                            }
                        }
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                    return false;
                }
            }
            public bool IsAuthorizedForPageEdit(Int32 PagId)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::IsAuthorizedForPageEdit]";
                try
                {
                    using (Menu.Item mi = new Menu.Item(PagId))
                    {
                        foreach (Int32 i in mi.AuthorizedEditRoles)
                        {
                            foreach (object r in GetRoles().ToArray())
                            {
                                if (i.Equals(Convert.ToInt32(r)))
                                    return true;
                            }
                        }
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                    return false;
                }
            }
            public void AddToRole(Int32 RolId)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::AddToRole]";
                try
                {
                    if (base.Details.Id.Equals(0))
                        base.Save();
                    LiquidCore.Data.UserData.AddToRole(base.Details, RolId);
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                } 
            }
            public void DeleteFromRole(Int32 RolId)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::DeleteFromRole]";
                try
                {
                    if (base.Details.Id.Equals(0))
                        base.Save();
                    LiquidCore.Data.UserData.DeleteFromRole(base.Details, RolId);
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public ArrayList GetRoles()
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::GetRoles]";
                try
                {
                    if (base.Details.Id.Equals(0))
                        base.Save();
                    return LiquidCore.Data.UserData.GetRoles(base.Details);
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                    return new ArrayList();
                }
            }
            public void ChangeOrderUp()
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::ChangeOrderUp]";
                try
                {
                    if (base.Details.Id.Equals(0))
                        throw new Exception("This is not created yet, bad bad programmer...");
                    if (this.Order > 1)
                        this.Order = this.Order - 3;
                    this.Save();
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public void ChangeOrderDown()
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::ChangeOrderDown]";
                try
                {
                    if (base.Details.Id.Equals(0))
                        throw new Exception("This is not created yet, bad bad programmer...");
                    this.Order = this.Order + 3;
                    this.Save();
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public void CreateSetting(String Name, String Value)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::CreateSetting]";
                try
                {
                    if (base.Details.Id.Equals(0))
                        throw new Exception("This is not created yet, bad bad programmer...");
                    Setting s = new Setting();
                    s.Status = 1;
                    s.Language = 1;
                    s.SitId = 0;
                    s.PagId = 0;
                    s.ModId = 0;
                    s.Pointer = base.Details.Id;
                    s.Title = Name;
                    s.Alias = "User-Setting";
                    s.Description = Name;
                    s.Value = Value;
                    s.Save();
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public void CreateSetting(String Name)
            {
                CreateSetting(Name, String.Empty);
            }
            public void SaveSetting(String Name, String Value)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::SaveSetting]";
                try
                {
                    Boolean Exist = false;
                    foreach (Setting s in this.Settings)
                        if (s.Title.ToLower().Equals(Name.ToLower()))
                        {
                            s.Value = Value;
                            s.Save();
                            Exist = true;
                        }
                    if (!Exist)
                        CreateSetting(Name, Value); 
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
			public void DeleteSetting(String Name)
			{
				string FUNCTIONNAME = CLASSNAME + "[Function::DeleteSetting]";
				try
				{
					Boolean Exist = false;
					foreach (Setting s in this.Settings)
						if (s.Title.ToLower().Equals(Name.ToLower()))
						{
							s.Delete();
							Exist = true;
						}
				}
				catch (Exception ex)
				{
					Error.Report(ex, FUNCTIONNAME, "");
				}
			}
            public String GetSetting(String Name)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::ChangeSetting]";
                try
                {
                    foreach (Setting s in this.Settings)
                        if (s.Title.ToLower().Equals(Name.ToLower()))
                            return s.Value;
                    return String.Empty;
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                    return String.Empty;
                }
            }
        }
    }
    public class Security
    {
        string CLASSNAME = "[Namespace::LiquidCore][Class::Security]";
        private static byte[] CRYPTO_KEY = { 0xf, 0x6f, 0x13, 0x2e, 0x35, 0xc2, 0xcd, 0xf9, 0x5, 0x46, 0x9c, 0xea, 0xa8, 0x4b, 0x73, 0xcc };
        private static byte[] CRYPTO_IV = { 0xf, 0x6f, 0x13, 0x2e, 0x35, 0xc2, 0xcd, 0xf9, 0x6f, 0x13, 0x2e, 0x35, 0xc2, 0xcd, 0xf9, 0xcd };

        public static string Encrypt(string PlainText)
        {
            try
            {
                if (PlainText == null)
                    return String.Empty;
				AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
                byte[] ByteArray = Encoding.UTF8.GetBytes(PlainText);
                ICryptoTransform enc = aes.CreateEncryptor(CRYPTO_KEY, CRYPTO_IV);
                byte[] ByteArr = enc.TransformFinalBlock(ByteArray, 0, ByteArray.GetLength(0));
                return Convert.ToBase64String(ByteArr);
            }
            catch (Exception ex)
            {
                return "Encrypt - " + ex.Message.ToString();
            }
        }
        public static string Decrypt(string Base64String)
        {
            try
            {
                if (Base64String == null)
                    return String.Empty;
				AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
				ICryptoTransform dec = aes.CreateDecryptor(CRYPTO_KEY, CRYPTO_IV);
                byte[] ByteArr = Convert.FromBase64String(Base64String);
                return Encoding.UTF8.GetString(dec.TransformFinalBlock(ByteArr, 0, ByteArr.GetLength(0)));
            }
            catch (Exception ex)
            {
                return "Decrypt - " + ex.Message.ToString();
            }
        }
        public static bool IsInRole(String Role)
        {
            Int32 RolId = 0;
            Int32.TryParse(Role, out RolId);
            if (RolId != 0)
                return HttpContext.Current.User.IsInRole(Role.ToString());
            else
            {
                String LoginName = String.Empty;
                String Password = String.Empty;
                if (HttpContext.Current != null) if (HttpContext.Current != null) LoginName = LiquidCore.Authentication.User.Identity.Name != null ? LiquidCore.Authentication.User.Identity.Name : "none";
                if (HttpContext.Current != null) if (HttpContext.Current != null) Password = LiquidCore.Authentication.User.Identity.Password != null ? LiquidCore.Authentication.User.Identity.Password : "none";
                String[] roles = LiquidCore.Data.RoleData.GetRolesArray(LoginName, Password, true);
                foreach (String role in roles)
                {
                    if (LiquidCore.Data.RoleData.CheckRoleAlias(Convert.ToInt32(role), Role))
                        return true;
                }
                return false;
            }
        }
        public static void LogIn(String LoginName, String Password)
        {
            LiquidCore.Data.UserData.SignIn(LoginName, Password);
        }
        public static void LogOut()
        {
            LiquidCore.Data.UserData.SignOut();
        }
    }
    public class Status : StatusDefinition
    {
        string CLASSNAME = "[Namespace::LiquidCore][Class::Status]";
        public Status() : base() { }
        public class Item : StatusDefinition.ItemDefinition
        {
            static string CLASSNAME = "[Namespace::LiquidCore][Class::Status.Item]";
            public int Id
            {
                get { return base.Details.Id; }
                set { base.Details.Id = value; }
            }
            public int Language
            {
                get { return base.Details.Language; }
                set { base.Details.Language = value; }
            }
            public int ParentId
            {
                get { return base.Details.ParentId; }
                set { base.Details.ParentId = value; }
            }
            public int Order
            {
                get { return base.Details.Order; }
                set { base.Details.Order = value; }
            }
            public int Revision
            {
                get { return base.Details.Revision; }
                set { base.Details.Revision = value; }
            }
            public string Title
            {
                get { return base.Details.Title; }
                set { base.Details.Title = value; }
            }
            public string Alias
            {
                get { return base.Details.Alias; }
                set { base.Details.Alias = value; }
            }
            public string Description
            {
                get { return base.Details.Description; }
                set { base.Details.Description = value; }
            }
            public DateTime CreatedDate
            {
                get { return base.Details.CreatedDate; }
                set { base.Details.CreatedDate = value; }
            }
            public string CreatedBy
            {
                get { return base.Details.CreatedBy; }
                set { base.Details.CreatedBy = value; }
            }
            public DateTime UpdatedDate
            {
                get { return base.Details.UpdatedDate; }
                set { base.Details.UpdatedDate = value; }
            }
            public string UpdatedBy
            {
                get { return base.Details.UpdatedBy; }
                set { base.Details.UpdatedBy = value; }
            }
            public bool Hidden
            {
                get { return base.Details.Hidden; }
                set { base.Details.Hidden = value; }
            }
            public bool Deleted
            {
                get { return base.Details.Deleted; }
                set { base.Details.Deleted = value; }
            }

            public bool HasChildren
            {
                get
                {
                    return LiquidCore.Data.StatusData.FindChildren(base.Details);
                }
            }
            public bool HasParent
            {
                get
                {
                    return base.Details.ParentId.Equals(0) ? false : true;
                }
            }

            public Item() : base() { }
            public Item(Int32 Id) : base(Id) { }

            public void ChangeOrderUp()
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::ChangeOrderUp]";
                try
                {
                    if (base.Details.Id.Equals(0))
                        throw new Exception("This is not created yet, bad bad programmer...");
                    if (this.Order > 1)
                        this.Order = this.Order - 3;
                    this.Save();
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public void ChangeOrderDown()
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::ChangeOrderDown]";
                try
                {
                    if (base.Details.Id.Equals(0))
                        throw new Exception("This is not created yet, bad bad programmer...");
                    this.Order = this.Order + 3;
                    this.Save();
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
        }
    }

    /// <summary>
    /// Helper Class for the itemregister in RX.
    /// </summary>
    public class ItemRegister
    {
        static string CLASSNAME = "[Namespace::LiquidCore][Class::ItemRegister]";
        public static bool IsBigMove(Int32 SourceId, Int32 DestId)
        {
            string FUNCTIONNAME = CLASSNAME + "[Function::IsBigMove]";
            bool ret = false;
            try
            {
                if (SourceId.Equals(0))
                    return false;

                if (DestId.Equals(0))
                    return false;

                using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, false))
                {
                    StringBuilder sSQL1 = new StringBuilder();
                    sSQL1.Append("SELECT obd_parentid FROM obd_objectdata ");
                    sSQL1.Append("WHERE obd_id = " + SourceId.ToString());

                    LiquidCore.Data.EventLog.LogEvent(sSQL1.ToString(), FUNCTIONNAME, String.Empty);
                    Int32 SourceParentId = Convert.ToInt32(oDo.GetDataTable(sSQL1.ToString()).Rows[0][0].ToString());
                    if (oDo.HasError)
                        throw new Exception(oDo.GetError);

                    StringBuilder sSQL2 = new StringBuilder();
                    sSQL2.Append("SELECT obd_parentid FROM obd_objectdata ");
                    sSQL2.Append("WHERE obd_id = " + DestId.ToString());

                    LiquidCore.Data.EventLog.LogEvent(sSQL2.ToString(), FUNCTIONNAME, String.Empty);
                    Int32 DestParentId = Convert.ToInt32(oDo.GetDataTable(sSQL2.ToString()).Rows[0][0].ToString());
                    if (oDo.HasError)
                        throw new Exception(oDo.GetError);

                    ret = SourceParentId != DestParentId;
                }
                return ret;
            }
            catch (Exception ex)
            {
                Error.Report(ex, FUNCTIONNAME, "");
                return ret;
            }
        }
        public static void ChangeOrderToAboveOrUnder(Int32 SourceId, Int32 DestId, String Position, Boolean IsBigMove)
        {
            string FUNCTIONNAME = CLASSNAME + "[Function::ChangeOrderToAboveOrUnder]";
            try
            {
                if (SourceId.Equals(0))
                    return;

                if (DestId.Equals(0))
                    return;

                using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, true))
                {
                    StringBuilder sSQL1 = new StringBuilder();
                    sSQL1.Append("SELECT obd_alias FROM obd_objectdata ");
                    sSQL1.Append("WHERE obd_id = " + SourceId.ToString());

                    LiquidCore.Data.EventLog.LogEvent(sSQL1.ToString(), FUNCTIONNAME, String.Empty);
                    String SourceAlias = oDo.GetDataTable(sSQL1.ToString()).Rows[0][0].ToString();
                    if (oDo.HasError)
                        throw new Exception(oDo.GetError);

                    Int32 NewOrder = 210000000;

                    if (IsBigMove)
                    {
                        StringBuilder sSQL2 = new StringBuilder();
                        sSQL2.Append("SELECT obd_order FROM obd_objectdata ");
                        sSQL2.Append("WHERE obd_id = " + DestId.ToString());

                        LiquidCore.Data.EventLog.LogEvent(sSQL2.ToString(), FUNCTIONNAME, String.Empty);
                        Int32 OldOrder = Convert.ToInt32(oDo.GetDataTable(sSQL2.ToString()).Rows[0][0].ToString());
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);

                        if (Position.Equals("Above"))
                        {
                            if (OldOrder > 1)
                                NewOrder = OldOrder - 1;
                        }
                        else if (Position.Equals("Below"))
                            NewOrder = OldOrder + 1;

                    }

                    StringBuilder sSQL3 = new StringBuilder();
                    sSQL3.Append("UPDATE obd_objectdata SET ");
                    sSQL3.Append("obd_parentid = " + DestId.ToString() + ", ");
                    sSQL3.Append("obd_order = " + NewOrder.ToString() + ", ");
                    sSQL3.Append("obd_alias = '" + SourceAlias.Substring(0, SourceAlias.IndexOf("_") + 1) + DestId.ToString() + "', ");
                    sSQL3.Append("obd_updateddate = '" + DateTime.Now.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "' ");
                    sSQL3.Append("WHERE obd_id = " + SourceId.ToString());

                    LiquidCore.Data.EventLog.LogEvent(sSQL3.ToString(), FUNCTIONNAME, String.Empty);
                    oDo.ExecuteNonQuery(sSQL3.ToString());
                    if (oDo.HasError)
                        throw new Exception(oDo.GetError);

                    LiquidCore.Data.EventLog.LogEvent("CALL sort_obd_objectdata();", FUNCTIONNAME, String.Empty);
                    oDo.ExecuteNonQuery("CALL sort_obd_objectdata();");
                    if (oDo.HasError)
                        throw new Exception(oDo.GetError);

                    LiquidCore.Data.ObjectData.ResetThis();
                }
            }
            catch (Exception ex)
            {
                Error.Report(ex, FUNCTIONNAME, "");
            }
        }
        public static void ChangeOrderToAbove(Int32 Id, Int32 AboveId)
        {
            string FUNCTIONNAME = CLASSNAME + "[Function::ChangeOrderToAbove]";
            try
            {
                if (Id.Equals(0))
                    return;
                if (AboveId.Equals(0))
                    return;
                using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, true))
                {
                    StringBuilder sSQL1 = new StringBuilder();
                    sSQL1.Append("SELECT obd_order FROM obd_objectdata ");
                    sSQL1.Append("WHERE obd_id = " + AboveId.ToString());

                    LiquidCore.Data.EventLog.LogEvent(sSQL1.ToString(), FUNCTIONNAME, String.Empty);
                    Int32 AboveOrder = Convert.ToInt32(oDo.GetDataTable(sSQL1.ToString()).Rows[0][0]);
                    if (oDo.HasError)
                        throw new Exception(oDo.GetError);

                    StringBuilder sSQL2 = new StringBuilder();
                    sSQL2.Append("UPDATE obd_objectdata SET ");
                    sSQL2.Append("obd_order = " + (AboveOrder - 1).ToString() + ", ");
                    sSQL2.Append("obd_updateddate = '" + DateTime.Now.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "' ");
                    sSQL2.Append("WHERE obd_id = " + Id.ToString());

                    LiquidCore.Data.EventLog.LogEvent(sSQL2.ToString(), FUNCTIONNAME, String.Empty);
                    oDo.ExecuteNonQuery(sSQL2.ToString());
                    if (oDo.HasError)
                        throw new Exception(oDo.GetError);

                    LiquidCore.Data.EventLog.LogEvent("CALL sort_obd_objectdata();", FUNCTIONNAME, String.Empty);
                    oDo.ExecuteNonQuery("CALL sort_obd_objectdata();");
                    if (oDo.HasError)
                        throw new Exception(oDo.GetError);

                    LiquidCore.Data.ObjectData.ResetThis();
                }
            }
            catch (Exception ex)
            {
                Error.Report(ex, FUNCTIONNAME, "");
            }
        }
        public static void ChangeOrderToUnder(Int32 Id, Int32 UnderId)
        {
            string FUNCTIONNAME = CLASSNAME + "[Function::ChangeOrderToUnder]";
            try
            {
                if (Id.Equals(0))
                    return;
                if (UnderId.Equals(0))
                    return;
                using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, true))
                {
                    StringBuilder sSQL1 = new StringBuilder();
                    sSQL1.Append("SELECT obd_order FROM obd_objectdata ");
                    sSQL1.Append("WHERE obd_id = " + UnderId.ToString());

                    LiquidCore.Data.EventLog.LogEvent(sSQL1.ToString(), FUNCTIONNAME, String.Empty);
                    Int32 UnderOrder = Convert.ToInt32(oDo.GetDataTable(sSQL1.ToString()).Rows[0][0]);
                    if (oDo.HasError)
                        throw new Exception(oDo.GetError);

                    StringBuilder sSQL2 = new StringBuilder();
                    sSQL2.Append("UPDATE obd_objectdata SET ");
                    sSQL2.Append("obd_order = " + (UnderOrder + 1).ToString() + ", ");
                    sSQL2.Append("obd_updateddate = '" + DateTime.Now.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "' ");
                    sSQL2.Append("WHERE obd_id = " + Id.ToString());

                    LiquidCore.Data.EventLog.LogEvent(sSQL2.ToString(), FUNCTIONNAME, String.Empty);
                    oDo.ExecuteNonQuery(sSQL2.ToString());
                    if (oDo.HasError)
                        throw new Exception(oDo.GetError);

                    LiquidCore.Data.EventLog.LogEvent("CALL sort_obd_objectdata();", FUNCTIONNAME, String.Empty);
                    oDo.ExecuteNonQuery("CALL sort_obd_objectdata();");
                    if (oDo.HasError)
                        throw new Exception(oDo.GetError);

                    LiquidCore.Data.ObjectData.ResetThis();
                }
            }
            catch (Exception ex)
            {
                Error.Report(ex, FUNCTIONNAME, "");
            }
        }
        public static void MoveNodeDown(Int32 Id)
        {
            string FUNCTIONNAME = CLASSNAME + "[Function::MoveNodeDown]";
            try
            {
                if (Id.Equals(0))
                    return;
                using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, true))
                {
                    StringBuilder sSQL1 = new StringBuilder();
                    sSQL1.Append("SELECT obd_order FROM obd_objectdata ");
                    sSQL1.Append("WHERE obd_id = " + Id.ToString());

                    LiquidCore.Data.EventLog.LogEvent(sSQL1.ToString(), FUNCTIONNAME, String.Empty);
                    Int32 Order = Convert.ToInt32(oDo.GetDataTable(sSQL1.ToString()).Rows[0][0]);
                    if (oDo.HasError)
                        throw new Exception(oDo.GetError);

                    StringBuilder sSQL2 = new StringBuilder();
                    sSQL2.Append("UPDATE obd_objectdata SET ");
                    sSQL2.Append("obd_order = " + (Order + 3).ToString() + ", ");
                    sSQL2.Append("obd_updateddate = '" + DateTime.Now.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "' ");
                    sSQL2.Append("WHERE obd_id = " + Id.ToString());

                    LiquidCore.Data.EventLog.LogEvent(sSQL2.ToString(), FUNCTIONNAME, String.Empty);
                    oDo.ExecuteNonQuery(sSQL2.ToString());
                    if (oDo.HasError)
                        throw new Exception(oDo.GetError);

                    LiquidCore.Data.EventLog.LogEvent("CALL sort_obd_objectdata();", FUNCTIONNAME, String.Empty);
                    oDo.ExecuteNonQuery("CALL sort_obd_objectdata();");
                    if (oDo.HasError)
                        throw new Exception(oDo.GetError);

                    LiquidCore.Data.ObjectData.ResetThis();
                }
            }
            catch (Exception ex)
            {
                Error.Report(ex, FUNCTIONNAME, "");
            }
        }
        public static void MoveNodeUp(Int32 Id)
        {
            string FUNCTIONNAME = CLASSNAME + "[Function::MoveNodeUp]";
            try
            {
                if (Id.Equals(0))
                    return;
                using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, true))
                {
                    StringBuilder sSQL1 = new StringBuilder();
                    sSQL1.Append("SELECT obd_order FROM obd_objectdata ");
                    sSQL1.Append("WHERE obd_id = " + Id.ToString());

                    LiquidCore.Data.EventLog.LogEvent(sSQL1.ToString(), FUNCTIONNAME, String.Empty);
                    Int32 Order = Convert.ToInt32(oDo.GetDataTable(sSQL1.ToString()).Rows[0][0]);
                    if (oDo.HasError)
                        throw new Exception(oDo.GetError);

                    StringBuilder sSQL2 = new StringBuilder();
                    sSQL2.Append("UPDATE obd_objectdata SET ");
                    sSQL2.Append("obd_order = " + (Order - 3).ToString() + ", ");
                    sSQL2.Append("obd_updateddate = '" + DateTime.Now.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "' ");
                    sSQL2.Append("WHERE obd_id = " + Id.ToString());

                    LiquidCore.Data.EventLog.LogEvent(sSQL2.ToString(), FUNCTIONNAME, String.Empty);
                    oDo.ExecuteNonQuery(sSQL2.ToString());
                    if (oDo.HasError)
                        throw new Exception(oDo.GetError);

                    LiquidCore.Data.EventLog.LogEvent("CALL sort_obd_objectdata();", FUNCTIONNAME, String.Empty);
                    oDo.ExecuteNonQuery("CALL sort_obd_objectdata();");
                    if (oDo.HasError)
                        throw new Exception(oDo.GetError);

                    LiquidCore.Data.ObjectData.ResetThis();
                }
            }
            catch (Exception ex)
            {
                Error.Report(ex, FUNCTIONNAME, "");
            }
        }
        public static bool HasChildren(Int32 ParentId)
        {
            string FUNCTIONNAME = CLASSNAME + "[Function::HasChildren]";
            try
            {
                if (ParentId.Equals(0))
                    return false;

                using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, false))
                {
                    StringBuilder sSQL1 = new StringBuilder();
                    sSQL1.Append("SELECT obd_alias FROM obd_objectdata ");
                    sSQL1.Append("WHERE obd_parentid = " + ParentId.ToString());

                    LiquidCore.Data.EventLog.LogEvent(sSQL1.ToString(), FUNCTIONNAME, String.Empty);
                    DataTable dt = oDo.GetDataTable(sSQL1.ToString());
                    if (oDo.HasError)
                        throw new Exception(oDo.GetError);

                    if (dt.Rows.Count.Equals(0))
                        return false;

                    foreach (DataRow dr in dt.Rows)
                        if (dr["obd_alias"].ToString().Equals("Node_" + ParentId.ToString()))
                            return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Error.Report(ex, FUNCTIONNAME, "");
                return false;
            }
        }
        public static bool NodeExists(Int32 Id)
        {
            string FUNCTIONNAME = CLASSNAME + "[Function::NodeExists]";
            try
            {
                if (Id.Equals(0))
                    return false;

                using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, false))
                {
                    StringBuilder sSQL1 = new StringBuilder();
                    sSQL1.Append("SELECT obd_id FROM obd_objectdata ");
                    sSQL1.Append("WHERE obd_id = " + Id.ToString());

                    LiquidCore.Data.EventLog.LogEvent(sSQL1.ToString(), FUNCTIONNAME, String.Empty);
                    Int32 ExistId = Convert.ToInt32(oDo.GetDataTable(sSQL1.ToString()).Rows[0][0]);
                    if (oDo.HasError)
                        throw new Exception(oDo.GetError);

                    if (ExistId.Equals(0))
                        return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Error.Report(ex, FUNCTIONNAME, "");
                return false;
            }
        }
        public static List<int> GetNodeIdsUnderParent(Int32 ParentId)
        {
            string FUNCTIONNAME = CLASSNAME + "[Function::GetNodeIdsUnderParent]";
            List<int> ids = new List<int>();
            try
            {
                if (ParentId.Equals(0))
                    return ids;

                using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, false))
                {
                    StringBuilder sSQL1 = new StringBuilder();
                    sSQL1.Append("SELECT obd_id FROM obd_objectdata ");
                    sSQL1.Append("WHERE obd_deleted = 0 AND obd_alias = 'Node_" + ParentId.ToString() + "' ORDER BY obd_order");

                    LiquidCore.Data.EventLog.LogEvent(sSQL1.ToString(), FUNCTIONNAME, String.Empty);
                    DataTable dt = oDo.GetDataTable(sSQL1.ToString());
                    if (oDo.HasError)
                        throw new Exception(oDo.GetError);

                    foreach (DataRow dr in dt.Rows)
                        ids.Add(Convert.ToInt32(dr["obd_id"].ToString())); 
                }
                return ids;
            }
            catch (Exception ex)
            {
                Error.Report(ex, FUNCTIONNAME, "");
                return ids;
            }
        }
        public static List<int> GetNodeIdsUnderParent(Int32 ParentId, String Type)
        {
            string FUNCTIONNAME = CLASSNAME + "[Function::GetNodeIdsUnderParent]";
            List<int> ids = new List<int>();
            try
            {
                if (ParentId.Equals(0))
                    return ids;

                using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, false))
                {
                    StringBuilder sSQL1 = new StringBuilder();
                    sSQL1.Append("SELECT obd_id, obd_varchar1 FROM obd_objectdata ");
                    sSQL1.Append("WHERE obd_deleted = 0 AND obd_alias = 'Node_" + ParentId.ToString() + "' ORDER BY obd_order");

                    LiquidCore.Data.EventLog.LogEvent(sSQL1.ToString(), FUNCTIONNAME, String.Empty);
                    DataTable dt = oDo.GetDataTable(sSQL1.ToString());
                    if (oDo.HasError)
                        throw new Exception(oDo.GetError);

                    foreach (DataRow dr in dt.Rows)
                        if (dr["obd_varchar1"].ToString().Equals(Type))
                            ids.Add(Convert.ToInt32(dr["obd_id"].ToString()));
                }
                return ids;
            }
            catch (Exception ex)
            {
                Error.Report(ex, FUNCTIONNAME, "");
                return ids;
            }
        }
        public static String GetNodType(Int32 Id)
        {
            string FUNCTIONNAME = CLASSNAME + "[Function::GetNodType]";
            String ret = String.Empty;
            try
            {
                if (Id.Equals(0))
                    return ret;

                using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, false))
                {
                    StringBuilder sSQL1 = new StringBuilder();
                    sSQL1.Append("SELECT obd_varchar1 FROM obd_objectdata ");
                    sSQL1.Append("WHERE obd_id = " + Id.ToString());

                    LiquidCore.Data.EventLog.LogEvent(sSQL1.ToString(), FUNCTIONNAME, String.Empty);
                    ret = oDo.GetDataTable(sSQL1.ToString()).Rows[0][0].ToString();
                    if (oDo.HasError)
                        throw new Exception(oDo.GetError);
                }
                return ret;
            }
            catch (Exception ex)
            {
                Error.Report(ex, FUNCTIONNAME, "");
                return ret;
            }
        }
        public static bool GetBoolData(Int32 dataNodeId)
        {
            string FUNCTIONNAME = CLASSNAME + "[Function::GetBoolData]";
            bool ret = false;
            try
            {
                if (dataNodeId.Equals(0))
                    return ret;

                using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, false))
                {
                    StringBuilder sSQL1 = new StringBuilder();
                    sSQL1.Append("SELECT obd_varchar2 FROM obd_objectdata ");
                    sSQL1.Append("WHERE obd_id = " + dataNodeId.ToString());

                    LiquidCore.Data.EventLog.LogEvent(sSQL1.ToString(), FUNCTIONNAME, String.Empty);
                    String val = oDo.GetDataTable(sSQL1.ToString()).Rows[0][0].ToString();
                    if (oDo.HasError)
                        throw new Exception(oDo.GetError);

                    Boolean.TryParse(val, out ret);
                }
                return ret;
            }
            catch (Exception ex)
            {
                Error.Report(ex, FUNCTIONNAME, "");
                return ret;
            }
        }
        public static bool GetBoolData(String name, Int32 nodeId)
        {
            string FUNCTIONNAME = CLASSNAME + "[Function::GetBoolData]";
            bool ret = false;
            try
            {
                if (nodeId.Equals(0))
                    return ret;

                using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, false))
                {
                    StringBuilder sSQL1 = new StringBuilder();
                    sSQL1.Append("SELECT obd_varchar1, obd_varchar2 FROM obd_objectdata ");
                    sSQL1.Append("WHERE obd_alias = 'DataNode_" + nodeId.ToString() + "'");

                    LiquidCore.Data.EventLog.LogEvent(sSQL1.ToString(), FUNCTIONNAME, String.Empty);
                    DataTable dt = oDo.GetDataTable(sSQL1.ToString());
                    if (oDo.HasError)
                        throw new Exception(oDo.GetError);

                    if (dt.Rows.Count.Equals(0))
                        return false;

                    foreach (DataRow dr in dt.Rows)
                        if (dr["obd_varchar1"].ToString().Equals(name))
                        {
                            Boolean.TryParse(dr["obd_varchar2"].ToString(), out ret);
                            break;
                        }

                }
                return ret;
            }
            catch (Exception ex)
            {
                Error.Report(ex, FUNCTIONNAME, "");
                return ret;
            }
        }
        public static Decimal GetDecimalData(Int32 dataNodeId)
        {
            string FUNCTIONNAME = CLASSNAME + "[Function::GetDecimalData]";
            Decimal ret = 0;
            try
            {
                if (dataNodeId.Equals(0))
                    return ret;

                using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, false))
                {
                    StringBuilder sSQL1 = new StringBuilder();
                    sSQL1.Append("SELECT obd_varchar2 FROM obd_objectdata ");
                    sSQL1.Append("WHERE obd_id = " + dataNodeId.ToString());

                    LiquidCore.Data.EventLog.LogEvent(sSQL1.ToString(), FUNCTIONNAME, String.Empty);
                    String val = oDo.GetDataTable(sSQL1.ToString()).Rows[0][0].ToString();
                    if (oDo.HasError)
                        throw new Exception(oDo.GetError);

                    Decimal.TryParse(val, out ret);
                }
                return ret;
            }
            catch (Exception ex)
            {
                Error.Report(ex, FUNCTIONNAME, "");
                return ret;
            }
        }
        public static Decimal GetDecimalData(String name, Int32 nodeId)
        {
            string FUNCTIONNAME = CLASSNAME + "[Function::GetDecimalData]";
            Decimal ret = 0;
            try
            {
                if (nodeId.Equals(0))
                    return ret;

                using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, false))
                {
                    StringBuilder sSQL1 = new StringBuilder();
                    sSQL1.Append("SELECT obd_varchar1, obd_varchar2 FROM obd_objectdata ");
                    sSQL1.Append("WHERE obd_alias = 'DataNode_" + nodeId.ToString() + "'");

                    LiquidCore.Data.EventLog.LogEvent(sSQL1.ToString(), FUNCTIONNAME, String.Empty);
                    DataTable dt = oDo.GetDataTable(sSQL1.ToString());
                    if (oDo.HasError)
                        throw new Exception(oDo.GetError);

                    if (dt.Rows.Count.Equals(0))
                        return 0;

                    foreach (DataRow dr in dt.Rows)
                        if (dr["obd_varchar1"].ToString().Equals(name))
                        {
                            Decimal.TryParse(dr["obd_varchar2"].ToString(), out ret);
                            break;
                        }

                }
                return ret;
            }
            catch (Exception ex)
            {
                Error.Report(ex, FUNCTIONNAME, "");
                return ret;
            }
        }
        public static String GetTextFieldData(int dataNodeId)
        {
            string FUNCTIONNAME = CLASSNAME + "[Function::GetTextFieldData]";
            String ret = String.Empty;
            try
            {
                if (dataNodeId.Equals(0))
                    return ret;

                using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, false))
                {
                    StringBuilder sSQL1 = new StringBuilder();
                    sSQL1.Append("SELECT obd_varchar2 FROM obd_objectdata ");
                    sSQL1.Append("WHERE obd_id = " + dataNodeId.ToString());

                    LiquidCore.Data.EventLog.LogEvent(sSQL1.ToString(), FUNCTIONNAME, String.Empty);
                    ret = oDo.GetDataTable(sSQL1.ToString()).Rows[0][0].ToString();
                    if (oDo.HasError)
                        throw new Exception(oDo.GetError);
                }
                return ret;
            }
            catch (Exception ex)
            {
                Error.Report(ex, FUNCTIONNAME, "");
                return ret;
            }
        }
        public static String GetTextFieldData(String name, Int32 nodeId)
        {
            string FUNCTIONNAME = CLASSNAME + "[Function::GetTextFieldData]";
            String ret = String.Empty;
            try
            {
                if (nodeId.Equals(0))
                    return ret;

                using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, false))
                {
                    StringBuilder sSQL1 = new StringBuilder();
                    sSQL1.Append("SELECT obd_varchar1, obd_varchar2 FROM obd_objectdata ");
                    sSQL1.Append("WHERE obd_alias = 'DataNode_" + nodeId.ToString() + "'");

                    LiquidCore.Data.EventLog.LogEvent(sSQL1.ToString(), FUNCTIONNAME, String.Empty);
                    DataTable dt = oDo.GetDataTable(sSQL1.ToString());
                    if (oDo.HasError)
                        throw new Exception(oDo.GetError);

                    if (dt.Rows.Count.Equals(0))
                        return ret;

                    foreach (DataRow dr in dt.Rows)
                        if (dr["obd_varchar1"].ToString().Equals(name))
                        {
                            ret = dr["obd_varchar2"].ToString();
                            break;
                        }

                }
                return ret;
            }
            catch (Exception ex)
            {
                Error.Report(ex, FUNCTIONNAME, "");
                return ret;
            }
        }
        public static String GetHtmlTextFieldData(int dataNodeId)
        {
            string FUNCTIONNAME = CLASSNAME + "[Function::GetTextFieldData]";
            String ret = String.Empty;
            try
            {
                if (dataNodeId.Equals(0))
                    return ret;

                using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, false))
                {
                    StringBuilder sSQL1 = new StringBuilder();
                    sSQL1.Append("SELECT obd_varchar2 FROM obd_objectdata ");
                    sSQL1.Append("WHERE obd_id = " + dataNodeId.ToString());

                    LiquidCore.Data.EventLog.LogEvent(sSQL1.ToString(), FUNCTIONNAME, String.Empty);
                    ret = oDo.GetDataTable(sSQL1.ToString()).Rows[0][0].ToString();
                    if (oDo.HasError)
                        throw new Exception(oDo.GetError);
                }
                return HttpContext.Current.Server.HtmlDecode(ret).Replace("`", "'");
            }
            catch (Exception ex)
            {
                Error.Report(ex, FUNCTIONNAME, "");
                return ret;
            }
        }
        public static String GetHtmlTextFieldData(String name, Int32 nodeId)
        {
            string FUNCTIONNAME = CLASSNAME + "[Function::GetTextFieldData]";
            String ret = String.Empty;
            try
            {
                if (nodeId.Equals(0))
                    return ret;

                using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, false))
                {
                    StringBuilder sSQL1 = new StringBuilder();
                    sSQL1.Append("SELECT obd_varchar1, obd_varchar2 FROM obd_objectdata ");
                    sSQL1.Append("WHERE obd_alias = 'DataNode_" + nodeId.ToString() + "'");

                    LiquidCore.Data.EventLog.LogEvent(sSQL1.ToString(), FUNCTIONNAME, String.Empty);
                    DataTable dt = oDo.GetDataTable(sSQL1.ToString());
                    if (oDo.HasError)
                        throw new Exception(oDo.GetError);

                    if (dt.Rows.Count.Equals(0))
                        return ret;

                    foreach (DataRow dr in dt.Rows)
                        if (dr["obd_varchar1"].ToString().Equals(name))
                        {
                            ret = dr["obd_varchar2"].ToString();
                            break;
                        }

                }
                return HttpContext.Current.Server.HtmlDecode(ret).Replace("`", "'");
            }
            catch (Exception ex)
            {
                Error.Report(ex, FUNCTIONNAME, "");
                return ret;
            }
        }
        public static int GetDataNodeId(String name, Int32 nodeId)
        {
            string FUNCTIONNAME = CLASSNAME + "[Function::GetDataNodeId]";
            Int32 ret = 0;
            try
            {
                if (nodeId.Equals(0))
                    return ret;

                using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, false))
                {
                    StringBuilder sSQL1 = new StringBuilder();
                    sSQL1.Append("SELECT obd_id, obd_varchar1 FROM obd_objectdata ");
                    sSQL1.Append("WHERE obd_alias = 'DataNode_" + nodeId);

                    LiquidCore.Data.EventLog.LogEvent(sSQL1.ToString(), FUNCTIONNAME, String.Empty);
                    DataTable dt = oDo.GetDataTable(sSQL1.ToString());
                    if (oDo.HasError)
                        throw new Exception(oDo.GetError);

                    if (dt.Rows.Count.Equals(0))
                        return 0;

                    foreach (DataRow dr in dt.Rows)
                        if (dr["obd_varchar1"].ToString().Equals(name))
                        {
                            Int32.TryParse(dr["obd_id"].ToString(), out ret);
                            break;
                        }

                }
                return ret;
            }
            catch (Exception ex)
            {
                Error.Report(ex, FUNCTIONNAME, "");
                return 0;
            }
        }
        public static Int32 GetParentNodeId(Int32 Id)
        {
            string FUNCTIONNAME = CLASSNAME + "[Function::GetParentNodeId]";
            Int32 ret = 0;
            try
            {
                if (Id.Equals(0))
                    return ret;

                using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, false))
                {
                    StringBuilder sSQL1 = new StringBuilder();
                    sSQL1.Append("SELECT obd_parentid FROM obd_objectdata ");
                    sSQL1.Append("WHERE obd_id = " + Id.ToString());

                    LiquidCore.Data.EventLog.LogEvent(sSQL1.ToString(), FUNCTIONNAME, String.Empty);
                    Int32.TryParse(oDo.GetDataTable(sSQL1.ToString()).Rows[0][0].ToString(), out ret); 
                    if (oDo.HasError)
                        throw new Exception(oDo.GetError);
                }
                return ret;
            }
            catch (Exception ex)
            {
                Error.Report(ex, FUNCTIONNAME, "");
                return ret;
            }
        }
        public static void SetBoolData(Int32 Id, bool data)
        {
            string FUNCTIONNAME = CLASSNAME + "[Function::SetBoolData]";
            try
            {
                if (Id.Equals(0))
                    return;

                using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, true))
                {
                    StringBuilder sSQL1 = new StringBuilder();
                    sSQL1.Append("UPDATE obd_objectdata SET ");
                    sSQL1.Append("obd_varchar2 = '" + data.ToString() + "', ");
                    sSQL1.Append("obd_updateddate = '" + DateTime.Now.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "' ");
                    sSQL1.Append("WHERE obd_id = " + Id.ToString());

                    LiquidCore.Data.EventLog.LogEvent(sSQL1.ToString(), FUNCTIONNAME, String.Empty);
                    oDo.ExecuteNonQuery(sSQL1.ToString());
                    if (oDo.HasError)
                        throw new Exception(oDo.GetError);
                }
            }
            catch (Exception ex)
            {
                Error.Report(ex, FUNCTIONNAME, "");
            }
        }
        public static void SetBoolData(int nodeId, String fieldName, bool data)
        {
            string FUNCTIONNAME = CLASSNAME + "[Function::SetBoolData]";
            try
            {
                if (nodeId.Equals(0))
                    return;

                using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, false))
                {
                    StringBuilder sSQL1 = new StringBuilder();
                    sSQL1.Append("UPDATE obd_objectdata SET ");
                    sSQL1.Append("obd_varchar2 = '" + data.ToString() + "', ");
                    sSQL1.Append("obd_updateddate = '" + DateTime.Now.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "' ");
                    sSQL1.Append("WHERE obd_alias = 'DataNode_" + nodeId + "' AND obd_varchar1 = '" + fieldName + "'");

                    LiquidCore.Data.EventLog.LogEvent(sSQL1.ToString(), FUNCTIONNAME, String.Empty);
                    oDo.ExecuteNonQuery(sSQL1.ToString());
                    if (oDo.HasError)
                        throw new Exception(oDo.GetError);

                }
            }
            catch (Exception ex)
            {
                Error.Report(ex, FUNCTIONNAME, "");
            }
        }
        public static void SetDecimalData(Int32 Id, Decimal data)
        {
            string FUNCTIONNAME = CLASSNAME + "[Function::SetDecimalData]";
            try
            {
                if (Id.Equals(0))
                    return;

                using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, true))
                {
                    StringBuilder sSQL1 = new StringBuilder();
                    sSQL1.Append("UPDATE obd_objectdata SET ");
                    sSQL1.Append("obd_varchar2 = '" + data.ToString() + "', ");
                    sSQL1.Append("obd_updateddate = '" + DateTime.Now.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "' ");
                    sSQL1.Append("WHERE obd_id = " + Id.ToString());

                    LiquidCore.Data.EventLog.LogEvent(sSQL1.ToString(), FUNCTIONNAME, String.Empty);
                    oDo.ExecuteNonQuery(sSQL1.ToString());
                    if (oDo.HasError)
                        throw new Exception(oDo.GetError);
                }
            }
            catch (Exception ex)
            {
                Error.Report(ex, FUNCTIONNAME, "");
            }
        }
        public static void SetDecimalData(int nodeId, String fieldName, Decimal data)
        {
            string FUNCTIONNAME = CLASSNAME + "[Function::SetDecimalData]";
            try
            {
                if (nodeId.Equals(0))
                    return;

                using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, false))
                {
                    StringBuilder sSQL1 = new StringBuilder();
                    sSQL1.Append("UPDATE obd_objectdata SET ");
                    sSQL1.Append("obd_varchar2 = '" + data.ToString() + "', ");
                    sSQL1.Append("obd_updateddate = '" + DateTime.Now.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "' ");
                    sSQL1.Append("WHERE obd_alias = 'DataNode_" + nodeId + "' AND obd_varchar1 = '" + fieldName + "'");

                    LiquidCore.Data.EventLog.LogEvent(sSQL1.ToString(), FUNCTIONNAME, String.Empty);
                    oDo.ExecuteNonQuery(sSQL1.ToString());
                    if (oDo.HasError)
                        throw new Exception(oDo.GetError);

                }
            }
            catch (Exception ex)
            {
                Error.Report(ex, FUNCTIONNAME, "");
            }
        }
        public static void SetImagePath(String path, int nodeId)
        {
            string FUNCTIONNAME = CLASSNAME + "[Function::SetImagePath]";
            try
            {
                if (nodeId.Equals(0))
                    return;

                using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, true))
                {
                    StringBuilder sSQL1 = new StringBuilder();
                    sSQL1.Append("UPDATE obd_objectdata SET ");
                    sSQL1.Append("obd_varchar2 = '" + path + "', ");
                    sSQL1.Append("obd_updateddate = '" + DateTime.Now.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "' ");
                    sSQL1.Append("WHERE obd_id = " + nodeId.ToString());

                    LiquidCore.Data.EventLog.LogEvent(sSQL1.ToString(), FUNCTIONNAME, String.Empty);
                    oDo.ExecuteNonQuery(sSQL1.ToString());
                    if (oDo.HasError)
                        throw new Exception(oDo.GetError);
                }
            }
            catch (Exception ex)
            {
                Error.Report(ex, FUNCTIONNAME, "");
            }
        }
        public static void SetImageLargePath(String path, int nodeId)
        {
            string FUNCTIONNAME = CLASSNAME + "[Function::SetImageLargePath]";
            try
            {
                if (nodeId.Equals(0))
                    return;

                using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, true))
                {
                    StringBuilder sSQL1 = new StringBuilder();
                    sSQL1.Append("UPDATE obd_objectdata SET ");
                    sSQL1.Append("obd_varchar3 = '" + path + "', ");
                    sSQL1.Append("obd_updateddate = '" + DateTime.Now.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "' ");
                    sSQL1.Append("WHERE obd_id = " + nodeId.ToString());

                    LiquidCore.Data.EventLog.LogEvent(sSQL1.ToString(), FUNCTIONNAME, String.Empty);
                    oDo.ExecuteNonQuery(sSQL1.ToString());
                    if (oDo.HasError)
                        throw new Exception(oDo.GetError);
                }
            }
            catch (Exception ex)
            {
                Error.Report(ex, FUNCTIONNAME, "");
            }
        }
        public static void SetImageThumbPath(String path, int nodeId)
        {
            string FUNCTIONNAME = CLASSNAME + "[Function::SetImageThumbPath]";
            try
            {
                if (nodeId.Equals(0))
                    return;

                using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, true))
                {
                    StringBuilder sSQL1 = new StringBuilder();
                    sSQL1.Append("UPDATE obd_objectdata SET ");
                    sSQL1.Append("obd_varchar4 = '" + path + "', ");
                    sSQL1.Append("obd_updateddate = '" + DateTime.Now.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "' ");
                    sSQL1.Append("WHERE obd_id = " + nodeId.ToString());

                    LiquidCore.Data.EventLog.LogEvent(sSQL1.ToString(), FUNCTIONNAME, String.Empty);
                    oDo.ExecuteNonQuery(sSQL1.ToString());
                    if (oDo.HasError)
                        throw new Exception(oDo.GetError);
                }
            }
            catch (Exception ex)
            {
                Error.Report(ex, FUNCTIONNAME, "");
            }
        }
        public static bool IsNode(int nodeId)
        {
            string FUNCTIONNAME = CLASSNAME + "[Function::IsNode]";
            try
            {
                if (nodeId.Equals(0))
                    return false;

                using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, false))
                {
                    StringBuilder sSQL1 = new StringBuilder();
                    sSQL1.Append("SELECT obd_alias, obd_parentid FROM obd_objectdata ");
                    sSQL1.Append("WHERE obd_id = " + nodeId.ToString());

                    LiquidCore.Data.EventLog.LogEvent(sSQL1.ToString(), FUNCTIONNAME, String.Empty);
                    DataTable dt = oDo.GetDataTable(sSQL1.ToString());
                    if (oDo.HasError)
                        throw new Exception(oDo.GetError);

                    if (dt.Rows.Count.Equals(0))
                        return false;

                    foreach (DataRow dr in dt.Rows)
                        if (dr["obd_alias"].ToString().Equals("Node_" + dr["obd_parentid"].ToString()))
                            return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Error.Report(ex, FUNCTIONNAME, "");
                return false;
            }
        }
        public static bool IsDataNode(int nodeId)
        {
            string FUNCTIONNAME = CLASSNAME + "[Function::IsDataNode]";
            try
            {
                if (nodeId.Equals(0))
                    return false;

                using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, false))
                {
                    StringBuilder sSQL1 = new StringBuilder();
                    sSQL1.Append("SELECT obd_alias, obd_parentid FROM obd_objectdata ");
                    sSQL1.Append("WHERE obd_id = " + nodeId.ToString());

                    LiquidCore.Data.EventLog.LogEvent(sSQL1.ToString(), FUNCTIONNAME, String.Empty);
                    DataTable dt = oDo.GetDataTable(sSQL1.ToString());
                    if (oDo.HasError)
                        throw new Exception(oDo.GetError);

                    if (dt.Rows.Count.Equals(0))
                        return false;

                    foreach (DataRow dr in dt.Rows)
                        if (dr["obd_alias"].ToString().Equals("DataNode_" + dr["obd_parentid"].ToString()))
                            return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Error.Report(ex, FUNCTIONNAME, "");
                return false;
            }
        }
        public static Int32 CreateNodeObject(Int32 ParentId, Int32 Order, String Alias, String Value1)
        {
            string FUNCTIONNAME = CLASSNAME + "[Function::CreateNodeObject]";
            Int32 Id = 0;
            try
            {
                StringBuilder sSQL = new StringBuilder();
                sSQL.Append("INSERT INTO obd_objectdata (sit_id, pag_id, mod_id, sta_id, lng_id, obd_parentid, obd_order, obd_type, obd_title, ");
                sSQL.Append("obd_alias, obd_description, obd_varchar1, obd_varchar2, obd_varchar3, obd_varchar4, obd_varchar5, ");
                sSQL.Append("obd_varchar6, obd_varchar7, obd_varchar8, obd_varchar9, obd_varchar10, obd_varchar11, ");
                sSQL.Append("obd_varchar12, obd_varchar13, obd_varchar14, obd_varchar15, obd_varchar16, obd_varchar17, ");
                sSQL.Append("obd_varchar18, obd_varchar19, obd_varchar20, obd_varchar21, obd_varchar22, obd_varchar23, ");
                sSQL.Append("obd_varchar24, obd_varchar25, obd_varchar26, obd_varchar27, obd_varchar28, obd_varchar29, ");
                sSQL.Append("obd_varchar30, obd_varchar31, obd_varchar32, obd_varchar33, obd_varchar34, obd_varchar35, ");
                sSQL.Append("obd_varchar36, obd_varchar37, obd_varchar38, obd_varchar39, obd_varchar40, obd_varchar41, ");
                sSQL.Append("obd_varchar42, obd_varchar43, obd_varchar44, obd_varchar45, obd_varchar46, obd_varchar47, ");
                sSQL.Append("obd_varchar48, obd_varchar49, obd_varchar50, ");
                sSQL.Append("obd_createddate, obd_createdby, ");
                sSQL.Append("obd_updateddate, obd_updatedby, obd_hidden, obd_deleted) VALUES ( ");
                sSQL.Append("0, ");
                sSQL.Append("0, ");
                sSQL.Append("0, ");
                sSQL.Append("0, ");
                sSQL.Append("0, ");
                sSQL.Append(ParentId.ToString() + ", ");
                sSQL.Append(Order.ToString() + ", ");
                sSQL.Append("0, ");
                sSQL.Append("'', ");
                sSQL.Append("'" + Alias + "', ");
                sSQL.Append("'', ");
                sSQL.Append("'" + Value1 + "', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'" + DateTime.Now.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "', ");
                sSQL.Append("'', ");
                sSQL.Append("'" + DateTime.Now.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "', ");
                sSQL.Append("'', ");
                sSQL.Append("0, ");
                sSQL.Append("0)");
                using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, true))
                {
                    LiquidCore.Data.EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                    oDo.ExecuteNonQuery(sSQL.ToString());
                    if (oDo.HasError)
                        throw new Exception(oDo.GetError);

                    LiquidCore.Data.EventLog.LogEvent("SELECT @@IDENTITY", FUNCTIONNAME, String.Empty);
                    Id = Convert.ToInt32(oDo.GetDataTable("SELECT @@IDENTITY").Rows[0][0]);
                    if (oDo.HasError)
                        throw new Exception(oDo.GetError);
                }
                return Id;
            }
            catch (Exception ex)
            {
                Error.Report(ex, FUNCTIONNAME, "");
                return Id;
            }
        }
        public static void CreateNodeObjectItem(Int32 ParentId, Int32 Order, String Alias, String Value1, String Value2)
        {
            string FUNCTIONNAME = CLASSNAME + "[Function::CreateNodeObjectItem]";
            try
            {
                if (ParentId.Equals(0))
                    return;

                StringBuilder sSQL = new StringBuilder();
                sSQL.Append("INSERT INTO obd_objectdata (sit_id, pag_id, mod_id, sta_id, lng_id, obd_parentid, obd_order, obd_type, obd_title, ");
                sSQL.Append("obd_alias, obd_description, obd_varchar1, obd_varchar2, obd_varchar3, obd_varchar4, obd_varchar5, ");
                sSQL.Append("obd_varchar6, obd_varchar7, obd_varchar8, obd_varchar9, obd_varchar10, obd_varchar11, ");
                sSQL.Append("obd_varchar12, obd_varchar13, obd_varchar14, obd_varchar15, obd_varchar16, obd_varchar17, ");
                sSQL.Append("obd_varchar18, obd_varchar19, obd_varchar20, obd_varchar21, obd_varchar22, obd_varchar23, ");
                sSQL.Append("obd_varchar24, obd_varchar25, obd_varchar26, obd_varchar27, obd_varchar28, obd_varchar29, ");
                sSQL.Append("obd_varchar30, obd_varchar31, obd_varchar32, obd_varchar33, obd_varchar34, obd_varchar35, ");
                sSQL.Append("obd_varchar36, obd_varchar37, obd_varchar38, obd_varchar39, obd_varchar40, obd_varchar41, ");
                sSQL.Append("obd_varchar42, obd_varchar43, obd_varchar44, obd_varchar45, obd_varchar46, obd_varchar47, ");
                sSQL.Append("obd_varchar48, obd_varchar49, obd_varchar50, ");
                sSQL.Append("obd_createddate, obd_createdby, ");
                sSQL.Append("obd_updateddate, obd_updatedby, obd_hidden, obd_deleted) VALUES ( ");
                sSQL.Append("0, ");
                sSQL.Append("0, ");
                sSQL.Append("0, ");
                sSQL.Append("0, ");
                sSQL.Append("0, ");
                sSQL.Append(ParentId.ToString() + ", ");
                sSQL.Append(Order.ToString() + ", ");
                sSQL.Append("0, ");
                sSQL.Append("'', ");
                sSQL.Append("'" + Alias + "', ");
                sSQL.Append("'', ");
                sSQL.Append("'" + Value1 + "', ");
                sSQL.Append("'" + Value2 + "', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'', ");
                sSQL.Append("'" + DateTime.Now.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "', ");
                sSQL.Append("'', ");
                sSQL.Append("'" + DateTime.Now.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "', ");
                sSQL.Append("'', ");
                sSQL.Append("0, ");
                sSQL.Append("0)");
                using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, true))
                {
                    LiquidCore.Data.EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                    oDo.ExecuteNonQuery(sSQL.ToString());
                    if (oDo.HasError)
                        throw new Exception(oDo.GetError);
                }
            }
            catch (Exception ex)
            {
                Error.Report(ex, FUNCTIONNAME, "");
            }
        }
        public static void CreateNodeObjectSortAndReset()
        {
            string FUNCTIONNAME = CLASSNAME + "[Function::CreateNodeObjectSortAndReset]";
            try
            {
                using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, false))
                {
                    LiquidCore.Data.EventLog.LogEvent("CALL sort_obd_objectdata();", FUNCTIONNAME, String.Empty);
                    oDo.ExecuteNonQuery("CALL sort_obd_objectdata();");
                    if (oDo.HasError)
                        throw new Exception(oDo.GetError);

                    LiquidCore.Data.ObjectData.ResetThis();
                }
            }
            catch (Exception ex)
            {
                Error.Report(ex, FUNCTIONNAME, "");
            }
        }
        public static void DeleteNode(Int32 Id)
        {
            string FUNCTIONNAME = CLASSNAME + "[Function::DeleteNode]";
            try
            {
                if (Id.Equals(0))
                    return;

                using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, true))
                {
                    StringBuilder sSQL1 = new StringBuilder();
                    sSQL1.Append("UPDATE obd_objectdata SET ");
                    sSQL1.Append("obd_deleted = 1, ");
                    sSQL1.Append("obd_updateddate = '" + DateTime.Now.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "' ");
                    sSQL1.Append("WHERE obd_id = " + Id.ToString());

                    LiquidCore.Data.EventLog.LogEvent(sSQL1.ToString(), FUNCTIONNAME, String.Empty);
                    oDo.ExecuteNonQuery(sSQL1.ToString());
                    if (oDo.HasError)
                        throw new Exception(oDo.GetError);
                }
            }
            catch (Exception ex)
            {
                Error.Report(ex, FUNCTIONNAME, "");
            }
        }

    }
}

// DefinitionLayer
namespace LiquidCore
{
    namespace LiquidCore.Definition
    {
        using CoreLib;
        using LiquidCore.Modeler;
        public class SitesDefinition : DomainObject, IDomainObject, IDisposable, ICollection<Site>
        {
            private bool _disposed = false;
            protected List<Site> _list = new List<Site>();

            private SitesModel _details = null;

            public Site this[Int32 index]
            {
                get
                {
                    return _list[index];
                }
            }

            public SitesDefinition()
                : base(DomainObjectState.Clean)
            {
                _details = new SitesModel(this);
            }

            public SitesDefinition(String Alias)
                : base(DomainObjectState.Clean)
            {
                this.DataFields["Alias"] = Alias;
                _details = new SitesModel(this);
            }

            ~SitesDefinition() { Dispose(false); }

            protected virtual void Dispose(bool disposing)
            {
                if (!_disposed)
                {
                    if (disposing)
                    {
                        _details = null;
                        DataFields = null;
                        foreach (Site l in _list)
                            l.Dispose();
                    }
                }
                _disposed = true;
            }

            public IEnumerator GetEnumerator()
            {
                return _list.GetEnumerator();
            }

            public new void Save()
            {
                foreach (Site l in _list)
                    if (l.ObjectState == DomainObjectState.Dirty)
                        l.Save();
            }
           
            #region IDomainObject Members

            void IDomainObject.CreateEntity()
            {
                // Not in use...
            }

            void IDomainObject.LoadEntity(ModelObject details)
            {
                LiquidCore.Data.SiteData.LoadAll(ref _list, this.DataFields);
            }

            void IDomainObject.UpdateEntity()
            {
                foreach (Site l in _list)
                    if (l.ObjectState == DomainObjectState.Dirty)
                        l.Save();
            }

            void IDomainObject.DeleteEntity()
            {
                foreach (Site l in _list)
                    l.Delete();
            }

            #endregion

            #region IDisposable Members

            void IDisposable.Dispose()
            {
                Dispose(true); 
                GC.SuppressFinalize(this);
            }

            #endregion

            #region ICollection<Site> Members

            public void Clear()
            {
                _list.Clear();
            }

            public bool Contains(Site item)
            {
                //return _list.Contains(item);
                foreach (Site m in _list)
                    if (m.Id.Equals(item.Id))
                        return true;
                return false;
            }

            public void CopyTo(Site[] array, int arrayIndex)
            {
                _list.CopyTo(array, arrayIndex);
            }

            public int Count
            {
                get { return _list.Count; }
            }

            public bool IsReadOnly
            {
                get { throw new Exception("The method or operation is not implemented."); }
            }

            public bool Remove(Site item)
            {
                return _list.Remove(item);
            }

            public void Add(Site item)
            {
                _list.Add(item);
            }

            #endregion

            #region IEnumerable<Site> Members

            IEnumerator<Site> IEnumerable<Site>.GetEnumerator()
            {
                return _list.GetEnumerator();
            }

            #endregion

            #region GenericComparer Members

            public enum SortOrderEnum
            {
                Ascending,
                Descending
            }

            public enum SortParamEnum
            {
                Id,
                Name,
                CreatedDate,
                UpdatedDate,
            }

            public class GenericComparer : IComparer<Site>
            {
                private String _Property = null;
                private SortOrderEnum _SortOrder = SortOrderEnum.Ascending;

                public String SortProperty
                {
                    get { return _Property; }
                    set { _Property = value; }
                }

                public SortOrderEnum SortOrder
                {
                    get { return _SortOrder; }
                    set { _SortOrder = value; }
                }

                public int Compare(Site x, Site y)
                {
                    try
                    {
                        Site ing1 = (Site)x;
                        Site ing2 = (Site)y;

                        if (this.SortOrder.Equals(SortOrderEnum.Ascending))
                            return ing1.CompareTo(ing2, this.SortProperty);
                        else
                            return ing2.CompareTo(ing1, this.SortProperty);
                    }
                    catch (Exception ex)
                    {
                        return 0;
                    }
                }
            }


            public void Sort(SortParamEnum SortBy, SortOrderEnum SortOrder)
            {
                GenericComparer comparer = new GenericComparer();
                comparer.SortProperty = SortBy.ToString();
                comparer.SortOrder = SortOrder;
                _list.Sort((IComparer<Site>)comparer);
            }

            #endregion GenericComparer Members
        }
        public class SiteDefinition : DomainObject, IDomainObject, IDisposable
        {
            private bool _disposed = false;
            private SiteModel _details = null;

            protected SiteModel Details
            {
                get { return _details; }
                set { _details = value; }
            }

            public SiteDefinition()
                : base(DomainObjectState.New)
            {
                _details = new SiteModel(this);
            }

            public SiteDefinition(Int32 Id)
                : base(DomainObjectState.Clean)
            {
                DataFields["Id"] = Id;
                _details = new SiteModel(this);
            }

            ~SiteDefinition() { Dispose(false); }

            public void Dispose() { Dispose(true); GC.SuppressFinalize(this); }

            protected virtual void Dispose(bool disposing)
            {
                if (!_disposed)
                {
                    if (disposing)
                    {
                        _details = null;
                        DataFields = null;
                        //base.Dispose(true);
                    }
                }
                _disposed = true;
            }

            #region IDomainObject Members

            void IDomainObject.CreateEntity()
            {
                LiquidCore.Data.SiteData.Create(Details);
            }

            void IDomainObject.LoadEntity(ModelObject Details)
            {
                LiquidCore.Data.SiteData.Load(Details, DataFields);
            }

            void IDomainObject.UpdateEntity()
            {
                LiquidCore.Data.SiteData.Update(Details);
            }

            void IDomainObject.DeleteEntity()
            {
                LiquidCore.Data.SiteData.Delete(Details);
            }

            #endregion

            #region GenericComparer Members
            public int CompareTo(object obj, string Property)
            {
                try
                {
                    Type type = this.GetType();
                    PropertyInfo propertie = type.GetProperty(Property);


                    Type type2 = obj.GetType();
                    PropertyInfo propertie2 = type2.GetProperty(Property);

                    object[] index = null;

                    object Obj1 = propertie.GetValue(this, index);
                    object Obj2 = propertie2.GetValue(obj, index);

                    IComparable Ic1 = (IComparable)Obj1;
                    IComparable Ic2 = (IComparable)Obj2;

                    int ret = Ic1.CompareTo(Ic2);

                    return ret;

                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
            #endregion GenericComparer Members

        }
        public class SettingsDefinition : DomainObject, IDomainObject, IDisposable, ICollection<Setting>
        {
            private bool _disposed = false;
            protected List<Setting> _list = new List<Setting>();

            private SettingsModel _details = null;

            public Setting this[Int32 index]
            {
                get
                {
                    return _list[index];
                }
            }

            public SettingsDefinition()
                : base(DomainObjectState.Clean)
            {
                _details = new SettingsModel(this);
            }

            public SettingsDefinition(Int32 SitId, Int32 PagId, Int32 ModId, Int32 ParentId)
                : base(DomainObjectState.Clean)
            {
                this.DataFields["SitId"] = SitId;
                this.DataFields["PagId"] = PagId;
                this.DataFields["ModId"] = ModId;
                this.DataFields["ParentId"] = ParentId;
                _details = new SettingsModel(this);
            }

            public SettingsDefinition(String Alias)
                : base(DomainObjectState.Clean)
            {
                this.DataFields["Alias"] = Alias;
                _details = new SettingsModel(this);
            }

            public SettingsDefinition(Int32 Pointer)
                : base(DomainObjectState.Clean)
            {
                this.DataFields["Pointer"] = Pointer;
                _details = new SettingsModel(this);
            }

            ~SettingsDefinition() { Dispose(false); }

            protected virtual void Dispose(bool disposing)
            {
                if (!_disposed)
                {
                    if (disposing)
                    {
                        _details = null;
                        DataFields = null;
                        foreach (Setting l in _list)
                            l.Dispose();
                    }
                }
                _disposed = true;
            }

            public IEnumerator GetEnumerator()
            {
                return _list.GetEnumerator();
            }

            public new void Save()
            {
                foreach (Setting l in _list)
                    if (l.ObjectState == DomainObjectState.Dirty)
                        l.Save();
            }

            #region IDomainObject Members

            void IDomainObject.CreateEntity()
            {
                // Not in use...
            }

            void IDomainObject.LoadEntity(ModelObject details)
            {
                LiquidCore.Data.SettingData.LoadAll(ref _list, this.DataFields);
            }

            void IDomainObject.UpdateEntity()
            {
                foreach (Setting l in _list)
                    if (l.ObjectState == DomainObjectState.Dirty)
                        l.Save();
            }

            void IDomainObject.DeleteEntity()
            {
                foreach (Setting l in _list)
                    l.Delete();
            }

            #endregion

            #region IDisposable Members

            void IDisposable.Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            #endregion

            #region ICollection<Setting> Members

            public void Clear()
            {
                _list.Clear();
            }

            public bool Contains(Setting item)
            {
                //return _list.Contains(item);
                foreach (Setting m in _list)
                    if (m.Id.Equals(item.Id))
                        return true;
                return false;
            }

            public void CopyTo(Setting[] array, int arrayIndex)
            {
                _list.CopyTo(array, arrayIndex);
            }

            public int Count
            {
                get { return _list.Count; }
            }

            public bool IsReadOnly
            {
                get { throw new Exception("The method or operation is not implemented."); }
            }

            public bool Remove(Setting item)
            {
                return _list.Remove(item);
            }

            public void Add(Setting item)
            {
                _list.Add(item);
            }

            #endregion

            #region IEnumerable<Setting> Members

            IEnumerator<Setting> IEnumerable<Setting>.GetEnumerator()
            {
                return _list.GetEnumerator();
            }

            #endregion

            #region GenericComparer Members

            public enum SortOrderEnum
            {
                Ascending,
                Descending
            }

            public enum SortParamEnum
            {
                Id,
                Title,
                Value,
                CreatedDate,
                UpdatedDate,
            }

            public class GenericComparer : IComparer<Setting>
            {
                private String _Property = null;
                private SortOrderEnum _SortOrder = SortOrderEnum.Ascending;

                public String SortProperty
                {
                    get { return _Property; }
                    set { _Property = value; }
                }

                public SortOrderEnum SortOrder
                {
                    get { return _SortOrder; }
                    set { _SortOrder = value; }
                }

                public int Compare(Setting x, Setting y)
                {
                    try
                    {
                        Setting ing1 = (Setting)x;
                        Setting ing2 = (Setting)y;

                        if (this.SortOrder.Equals(SortOrderEnum.Ascending))
                            return ing1.CompareTo(ing2, this.SortProperty);
                        else
                            return ing2.CompareTo(ing1, this.SortProperty);
                    }
                    catch (Exception ex)
                    {
                        return 0;
                    }
                }
            }


            public void Sort(SortParamEnum SortBy, SortOrderEnum SortOrder)
            {
                GenericComparer comparer = new GenericComparer();
                comparer.SortProperty = SortBy.ToString();
                comparer.SortOrder = SortOrder;
                _list.Sort((IComparer<Setting>)comparer);
            }

            #endregion GenericComparer Members
        }
        public class SettingDefinition : DomainObject, IDomainObject, IDisposable
        {
            private bool _disposed = false;
            private SettingModel _details = null;

            protected SettingModel Details
            {
                get { return _details; }
                set { _details = value; }
            }

            public SettingDefinition()
                : base(DomainObjectState.New)
            {
                _details = new SettingModel(this);
            }

            public SettingDefinition(Int32 Id)
                : base(DomainObjectState.Clean)
            {
                DataFields["Id"] = Id;
                _details = new SettingModel(this);
            }

			public SettingDefinition(DataRow dr)
				: base(DomainObjectState.Clean)
			{
				DataFields["dr"] = dr;
				_details = new SettingModel(this);
			}

            public SettingDefinition(String Alias)
                : base(DomainObjectState.Clean)
            {
                DataFields["Alias"] = Alias;
                _details = new SettingModel(this);
            }

            ~SettingDefinition() { Dispose(false); }

            public void Dispose() { Dispose(true); GC.SuppressFinalize(this); }

            protected virtual void Dispose(bool disposing)
            {
                if (!_disposed)
                {
                    if (disposing)
                    {
                        _details = null;
                        DataFields = null;
                        //base.Dispose(true);
                    }
                }
                _disposed = true;
            }

            #region IDomainObject Members

            void IDomainObject.CreateEntity()
            {
                LiquidCore.Data.SettingData.Create(Details);
            }

            void IDomainObject.LoadEntity(ModelObject Details)
            {
                LiquidCore.Data.SettingData.Load(Details, DataFields);
            }

            void IDomainObject.UpdateEntity()
            {
                LiquidCore.Data.SettingData.Update(Details);
            }

            void IDomainObject.DeleteEntity()
            {
                LiquidCore.Data.SettingData.Delete(Details);
            }

            #endregion

            #region GenericComparer Members
            public int CompareTo(object obj, string Property)
            {
                try
                {
                    Type type = this.GetType();
                    PropertyInfo propertie = type.GetProperty(Property);


                    Type type2 = obj.GetType();
                    PropertyInfo propertie2 = type2.GetProperty(Property);

                    object[] index = null;

                    object Obj1 = propertie.GetValue(this, index);
                    object Obj2 = propertie2.GetValue(obj, index);

                    IComparable Ic1 = (IComparable)Obj1;
                    IComparable Ic2 = (IComparable)Obj2;

                    int ret = Ic1.CompareTo(Ic2);

                    return ret;

                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
            #endregion GenericComparer Members

        }
        public class PagesDefinition : DomainObject, IDomainObject, IDisposable, ICollection<Page>
        {
            private bool _disposed = false;
            protected List<Page> _list = new List<Page>();

            private PagesModel _details = null;

            public Page this[Int32 index]
            {
                get
                {
                    return _list[index];
                }
            }

            public PagesDefinition()
                : base(DomainObjectState.Clean)
            {
                _details = new PagesModel(this);
            }

            public PagesDefinition(Int32 SitId, Int32 ParentId)
                : base(DomainObjectState.Clean)
            {
                this.DataFields["SitId"] = SitId;
                this.DataFields["ParentId"] = ParentId;
                _details = new PagesModel(this);
            }

            public PagesDefinition(String Alias)
                : base(DomainObjectState.Clean)
            {
                this.DataFields["Alias"] = Alias;
                _details = new PagesModel(this);
            }

            ~PagesDefinition() { Dispose(false); }

            protected virtual void Dispose(bool disposing)
            {
                if (!_disposed)
                {
                    if (disposing)
                    {
                        _details = null;
                        DataFields = null;
                        foreach (Page l in _list)
                            l.Dispose();
                    }
                }
                _disposed = true;
            }

            public IEnumerator GetEnumerator()
            {
                return _list.GetEnumerator();
            }

            public new void Save()
            {
                foreach (Page l in _list)
                    if (l.ObjectState == DomainObjectState.Dirty)
                        l.Save();
            }

            #region IDomainObject Members

            void IDomainObject.CreateEntity()
            {
                // Not in use...
            }

            void IDomainObject.LoadEntity(ModelObject details)
            {
                LiquidCore.Data.PageData.LoadAll(ref _list, this.DataFields);
            }

            void IDomainObject.UpdateEntity()
            {
                foreach (Page l in _list)
                    if (l.ObjectState == DomainObjectState.Dirty)
                        l.Save();
            }

            void IDomainObject.DeleteEntity()
            {
                foreach (Page l in _list)
                    l.Delete();
            }

            #endregion

            #region IDisposable Members

            void IDisposable.Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            #endregion

            #region ICollection<Page> Members

            public void Clear()
            {
                _list.Clear();
            }

            public bool Contains(Page item)
            {
                //return _list.Contains(item);
                foreach (Page m in _list)
                    if (m.Id.Equals(item.Id))
                        return true;
                return false;
            }

            public void CopyTo(Page[] array, int arrayIndex)
            {
                _list.CopyTo(array, arrayIndex);
            }

            public int Count
            {
                get { return _list.Count; }
            }

            public bool IsReadOnly
            {
                get { throw new Exception("The method or operation is not implemented."); }
            }

            public bool Remove(Page item)
            {
                return _list.Remove(item);
            }

            public void Add(Page item)
            {
                _list.Add(item);
            }

            #endregion

            #region IEnumerable<Page> Members

            IEnumerator<Page> IEnumerable<Page>.GetEnumerator()
            {
                return _list.GetEnumerator();
            }

            #endregion

            #region GenericComparer Members

            public enum SortOrderEnum
            {
                Ascending,
                Descending
            }

            public enum SortParamEnum
            {
                Id,
                Name,
                CreatedDate,
                UpdatedDate,
            }

            public class GenericComparer : IComparer<Page>
            {
                private String _Property = null;
                private SortOrderEnum _SortOrder = SortOrderEnum.Ascending;

                public String SortProperty
                {
                    get { return _Property; }
                    set { _Property = value; }
                }

                public SortOrderEnum SortOrder
                {
                    get { return _SortOrder; }
                    set { _SortOrder = value; }
                }

                public int Compare(Page x, Page y)
                {
                    try
                    {
                        Page ing1 = (Page)x;
                        Page ing2 = (Page)y;

                        if (this.SortOrder.Equals(SortOrderEnum.Ascending))
                            return ing1.CompareTo(ing2, this.SortProperty);
                        else
                            return ing2.CompareTo(ing1, this.SortProperty);
                    }
                    catch (Exception ex)
                    {
                        return 0;
                    }
                }
            }


            public void Sort(SortParamEnum SortBy, SortOrderEnum SortOrder)
            {
                GenericComparer comparer = new GenericComparer();
                comparer.SortProperty = SortBy.ToString();
                comparer.SortOrder = SortOrder;
                _list.Sort((IComparer<Page>)comparer);
            }

            #endregion GenericComparer Members
        }
        public class PageDefinition : DomainObject, IDomainObject, IDisposable
        {
            private bool _disposed = false;
            private PageModel _details = null;

            protected PageModel Details
            {
                get { return _details; }
                set { _details = value; }
            }

            public PageDefinition()
                : base(DomainObjectState.New)
            {
                _details = new PageModel(this);
            }

            public PageDefinition(Int32 Id)
                : base(DomainObjectState.Clean)
            {
                DataFields["Id"] = Id;
                _details = new PageModel(this);
            }

            ~PageDefinition() { Dispose(false); }

            public void Dispose() { Dispose(true); GC.SuppressFinalize(this); }

            protected virtual void Dispose(bool disposing)
            {
                if (!_disposed)
                {
                    if (disposing)
                    {
                        _details = null;
                        DataFields = null;
                        //base.Dispose(true);
                    }
                }
                _disposed = true;
            }

            #region IDomainObject Members

            void IDomainObject.CreateEntity()
            {
                LiquidCore.Data.PageData.Create(Details);
            }

            void IDomainObject.LoadEntity(ModelObject Details)
            {
                LiquidCore.Data.PageData.Load(Details, DataFields);
            }

            void IDomainObject.UpdateEntity()
            {
                LiquidCore.Data.PageData.Update(Details);
            }

            void IDomainObject.DeleteEntity()
            {
                LiquidCore.Data.PageData.Delete(Details, DataFields);
            }

            #endregion

            #region GenericComparer Members
            public int CompareTo(object obj, string Property)
            {
                try
                {
                    Type type = this.GetType();
                    PropertyInfo propertie = type.GetProperty(Property);


                    Type type2 = obj.GetType();
                    PropertyInfo propertie2 = type2.GetProperty(Property);

                    object[] index = null;

                    object Obj1 = propertie.GetValue(this, index);
                    object Obj2 = propertie2.GetValue(obj, index);

                    IComparable Ic1 = (IComparable)Obj1;
                    IComparable Ic2 = (IComparable)Obj2;

                    int ret = Ic1.CompareTo(Ic2);

                    return ret;

                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
            #endregion GenericComparer Members

        }
        public class ModulesDefinition : DomainObject, IDomainObject, IDisposable, ICollection<Module>
        {
            private bool _disposed = false;
            protected List<Module> _list = new List<Module>();

            private ModulesModel _details = null;

            public Module this[Int32 index]
            {
                get
                {
                    return _list[index];
                }
            }

            public ModulesDefinition()
                : base(DomainObjectState.Clean)
            {
                _details = new ModulesModel(this);
            }

            public ModulesDefinition(Int32 SitId, Int32 PagId, Int32 ParentId)
                : base(DomainObjectState.Clean)
            {
                this.DataFields["SitId"] = SitId;
                this.DataFields["PagId"] = PagId;
                this.DataFields["ParentId"] = ParentId;
                _details = new ModulesModel(this);
            }

            public ModulesDefinition(Int32 SitId, Int32 PagId, Int32 ParentId, Boolean IncludeAggregated)
                : base(DomainObjectState.Clean)
            {
                this.DataFields["SitId"] = SitId;
                this.DataFields["PagId"] = PagId;
                this.DataFields["ParentId"] = ParentId;
                this.DataFields["IncludeAggregated"] = IncludeAggregated ? 1 : 0;
                _details = new ModulesModel(this);
            }

            public ModulesDefinition(Int32 Status, Int32 SitId, Int32 PagId, Int32 ParentId, Boolean IncludeAggregated)
                : base(DomainObjectState.Clean)
            {
                this.DataFields["Status"] = Status;
                this.DataFields["SitId"] = SitId;
                this.DataFields["PagId"] = PagId;
                this.DataFields["ParentId"] = ParentId;
                this.DataFields["IncludeAggregated"] = IncludeAggregated ? 1 : 0;
                _details = new ModulesModel(this);
            }

            public ModulesDefinition(String Alias)
                : base(DomainObjectState.Clean)
            {
                this.DataFields["Alias"] = Alias;
                _details = new ModulesModel(this);
            }

            ~ModulesDefinition() { Dispose(false); }

            protected virtual void Dispose(bool disposing)
            {
                if (!_disposed)
                {
                    if (disposing)
                    {
                        _details = null;
                        DataFields = null;
                        foreach (Module l in _list)
                            l.Dispose();
                    }
                }
                _disposed = true;
            }

            public IEnumerator GetEnumerator()
            {
                return _list.GetEnumerator();
            }

            public new void Save()
            {
                foreach (Module l in _list)
                    if (l.ObjectState == DomainObjectState.Dirty)
                        l.Save();
            }

            #region IDomainObject Members

            void IDomainObject.CreateEntity()
            {
                // Not in use...
            }

            void IDomainObject.LoadEntity(ModelObject details)
            {
                LiquidCore.Data.ModuleData.LoadAll(ref _list, this.DataFields);
            }

            void IDomainObject.UpdateEntity()
            {
                foreach (Module l in _list)
                    if (l.ObjectState == DomainObjectState.Dirty)
                        l.Save();
            }

            void IDomainObject.DeleteEntity()
            {
                foreach (Module l in _list)
                    l.Delete();
            }

            #endregion

            #region IDisposable Members

            void IDisposable.Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            #endregion

            #region ICollection<Module> Members

            public void Clear()
            {
                _list.Clear();
            }

            public bool Contains(Module item)
            {
                //return _list.Contains(item);
                foreach (Module m in _list)
                    if (m.Id.Equals(item.Id))
                        return true;
                return false;
            }

            public void CopyTo(Module[] array, int arrayIndex)
            {
                _list.CopyTo(array, arrayIndex);
            }

            public int Count
            {
                get { return _list.Count; }
            }

            public bool IsReadOnly
            {
                get { throw new Exception("The method or operation is not implemented."); }
            }

            public bool Remove(Module item)
            {
                return _list.Remove(item);
            }

            public void Add(Module item)
            {
                _list.Add(item);
            }

            #endregion

            #region IEnumerable<Module> Members

            IEnumerator<Module> IEnumerable<Module>.GetEnumerator()
            {
                return _list.GetEnumerator();
            }

            #endregion

            #region GenericComparer Members

            public enum SortOrderEnum
            {
                Ascending,
                Descending
            }

            public enum SortParamEnum
            {
                Id,
                Name,
				MobileOrder,
                CreatedDate,
                UpdatedDate,
            }

            public class GenericComparer : IComparer<Module>
            {
                private String _Property = null;
                private SortOrderEnum _SortOrder = SortOrderEnum.Ascending;

                public String SortProperty
                {
                    get { return _Property; }
                    set { _Property = value; }
                }

                public SortOrderEnum SortOrder
                {
                    get { return _SortOrder; }
                    set { _SortOrder = value; }
                }

                public int Compare(Module x, Module y)
                {
                    try
                    {
                        Module ing1 = (Module)x;
                        Module ing2 = (Module)y;

                        if (this.SortOrder.Equals(SortOrderEnum.Ascending))
                            return ing1.CompareTo(ing2, this.SortProperty);
                        else
                            return ing2.CompareTo(ing1, this.SortProperty);
                    }
                    catch (Exception ex)
                    {
                        return 0;
                    }
                }
            }


            public void Sort(SortParamEnum SortBy, SortOrderEnum SortOrder)
            {
                GenericComparer comparer = new GenericComparer();
                comparer.SortProperty = SortBy.ToString();
                comparer.SortOrder = SortOrder;
                _list.Sort((IComparer<Module>)comparer);
            }

            #endregion GenericComparer Members
        }
        public class ModuleDefinition : DomainObject, IDomainObject, IDisposable
        {
            private bool _disposed = false;
            private ModuleModel _details = null;

            protected ModuleModel Details
            {
                get { return _details; }
                set { _details = value; }
            }

            public ModuleDefinition()
                : base(DomainObjectState.New)
            {
                _details = new ModuleModel(this);
            }

            public ModuleDefinition(Int32 Id)
                : base(DomainObjectState.Clean)
            {
                DataFields["Id"] = Id;
                _details = new ModuleModel(this);
            }

			public ModuleDefinition(DataRow dr)
				: base(DomainObjectState.Clean)
			{
				DataFields["dr"] = dr;
				_details = new ModuleModel(this);
			}

            ~ModuleDefinition() { Dispose(false); }

            public void Dispose() { Dispose(true); GC.SuppressFinalize(this); }

            protected virtual void Dispose(bool disposing)
            {
                if (!_disposed)
                {
                    if (disposing)
                    {
                        _details = null;
                        DataFields = null;
                        //base.Dispose(true);
                    }
                }
                _disposed = true;
            }

            #region IDomainObject Members

            void IDomainObject.CreateEntity()
            {
                LiquidCore.Data.ModuleData.Create(Details);
            }

            void IDomainObject.LoadEntity(ModelObject Details)
            {
                LiquidCore.Data.ModuleData.Load(Details, DataFields);
            }

            void IDomainObject.UpdateEntity()
            {
                LiquidCore.Data.ModuleData.Update(Details);
            }

            void IDomainObject.DeleteEntity()
            {
                LiquidCore.Data.ModuleData.Delete(Details, DataFields);
            }

            #endregion

            #region GenericComparer Members
            public int CompareTo(object obj, string Property)
            {
                try
                {
                    Type type = this.GetType();
                    PropertyInfo propertie = type.GetProperty(Property);


                    Type type2 = obj.GetType();
                    PropertyInfo propertie2 = type2.GetProperty(Property);

                    object[] index = null;

                    object Obj1 = propertie.GetValue(this, index);
                    object Obj2 = propertie2.GetValue(obj, index);

                    IComparable Ic1 = (IComparable)Obj1;
                    IComparable Ic2 = (IComparable)Obj2;

                    int ret = Ic1.CompareTo(Ic2);

                    return ret;

                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
            #endregion GenericComparer Members

        }
        public class ObjectsDefinition : DomainObject, IDomainObject, IDisposable, ICollection<Objects.Item>
        {
            private bool _disposed = false;
            protected List<Objects.Item> _list = new List<Objects.Item>();

            private ObjectsModel _details = null;

            public Objects.Item this[Int32 index]
            {
                get
                {
                    return _list[index];
                }
            }

            public ObjectsDefinition()
                : base(DomainObjectState.Clean)
            {
                _details = new ObjectsModel(this);
            }

            public ObjectsDefinition(Int32 SitId, Int32 PagId, Int32 ModId, Int32 ParentId)
                : base(DomainObjectState.Clean)
            {
                this.DataFields["SitId"] = SitId;
                this.DataFields["PagId"] = PagId;
                this.DataFields["ModId"] = ModId;
                this.DataFields["ParentId"] = ParentId;
                _details = new ObjectsModel(this);
            }

            public ObjectsDefinition(String Alias)
                : base(DomainObjectState.Clean)
            {
                this.DataFields["Alias"] = Alias;
                _details = new ObjectsModel(this);
            }

            public ObjectsDefinition(String Alias, Param[] SettingParameters)
                : base(DomainObjectState.Clean)
            {
                this.DataFields["Alias"] = Alias;
                this.DataFields["SettingParameters"] = SettingParameters;
                _details = new ObjectsModel(this);
            }

            public ObjectsDefinition(Int32 ParentId)
                : base(DomainObjectState.Clean)
            {
                this.DataFields["ParentId"] = ParentId;
                _details = new ObjectsModel(this);
            }

            ~ObjectsDefinition() { Dispose(false); }

            protected virtual void Dispose(bool disposing)
            {
                if (!_disposed)
                {
                    if (disposing)
                    {
                        _details = null;
                        DataFields = null;
                        foreach (Objects.Item l in _list)
                            l.Dispose();
                    }
                }
                _disposed = true;
            }

            public IEnumerator GetEnumerator()
            {
                return _list.GetEnumerator();
            }

            public new void Save()
            {
                foreach (Objects.Item l in _list)
                    if (l.ObjectState == DomainObjectState.Dirty)
                        l.Save();
            }
            
            public enum Operator
            {
                SmallerThan,
                SmallerThanEquals,
                Equals,
                GreaterThanEquals,
                GreaterThan,
				Contains
            };
            public static string GetOperator(Operator o)
            {
                switch (o)
                {
                    case Operator.SmallerThan:
                        return "<";
                    case Operator.SmallerThanEquals:
                        return "<=";
                    case Operator.Equals:
                        return "=";
                    case Operator.GreaterThanEquals:
                        return ">=";
                    case Operator.GreaterThan:
                        return ">";
					case Operator.Contains:
						return "LIKE";
                    default:
                        return String.Empty;
                }
            }
            public struct Param
            {
                public String name;
                public String value;
                public Operator operand;
            }

            #region IDomainObject Members

            void IDomainObject.CreateEntity()
            {
                // Not in use...
            }

            void IDomainObject.LoadEntity(ModelObject details)
            {
                LiquidCore.Data.ObjectData.LoadAll(ref _list, this.DataFields);
            }

            void IDomainObject.UpdateEntity()
            {
                foreach (Objects.Item l in _list)
                    if (l.ObjectState == DomainObjectState.Dirty)
                        l.Save();
            }

            void IDomainObject.DeleteEntity()
            {
                foreach (Objects.Item l in _list)
                    l.Delete();
            }

            #endregion

            #region IDisposable Members

            void IDisposable.Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            #endregion

            #region ICollection<Object> Members

            public void Clear()
            {
                _list.Clear();
            }

            public bool Contains(Objects.Item item)
            {
                //return _list.Contains(item);
                foreach (Objects.Item m in _list)
                    if (m.Id.Equals(item.Id))
                        return true;
                return false;
            }

            public void CopyTo(Objects.Item[] array, int arrayIndex)
            {
                _list.CopyTo(array, arrayIndex);
            }

            public int Count
            {
                get { return _list.Count; }
            }

            public bool IsReadOnly
            {
                get { throw new Exception("The method or operation is not implemented."); }
            }

            public bool Remove(Objects.Item item)
            {
                return _list.Remove(item);
            }

            public void Add(Objects.Item item)
            {
                _list.Add(item);
            }

            #endregion

            #region IEnumerable<Module> Members

            IEnumerator<Objects.Item> IEnumerable<Objects.Item>.GetEnumerator()
            {
                return _list.GetEnumerator();
            }

            #endregion

            #region GenericComparer Members

            public enum SortOrderEnum
            {
                Ascending,
                Descending
            }

            public enum SortParamEnum
            {
                Id,
                Name,
                CreatedDate,
                UpdatedDate,
            }

            public class GenericComparer : IComparer<Objects.Item>
            {
                private String _Property = null;
                private SortOrderEnum _SortOrder = SortOrderEnum.Ascending;

                public String SortProperty
                {
                    get { return _Property; }
                    set { _Property = value; }
                }

                public SortOrderEnum SortOrder
                {
                    get { return _SortOrder; }
                    set { _SortOrder = value; }
                }

                public int Compare(Objects.Item x, Objects.Item y)
                {
                    try
                    {
                        Objects.Item ing1 = (Objects.Item)x;
                        Objects.Item ing2 = (Objects.Item)y;

                        if (this.SortOrder.Equals(SortOrderEnum.Ascending))
                            return ing1.CompareTo(ing2, this.SortProperty);
                        else
                            return ing2.CompareTo(ing1, this.SortProperty);
                    }
                    catch (Exception ex)
                    {
                        return 0;
                    }
                }
            }


            public void Sort(SortParamEnum SortBy, SortOrderEnum SortOrder)
            {
                GenericComparer comparer = new GenericComparer();
                comparer.SortProperty = SortBy.ToString();
                comparer.SortOrder = SortOrder;
                _list.Sort((IComparer<Objects.Item>)comparer);
            }

            #endregion GenericComparer Members
        }
        public class ObjectDefinition : DomainObject, IDomainObject, IDisposable
        {
            private bool _disposed = false;
            private ObjectModel _details = null;

            protected ObjectModel Details
            {
                get { return _details; }
                set { _details = value; }
            }

            public ObjectDefinition()
                : base(DomainObjectState.New)
            {
                _details = new ObjectModel(this);
            }

            public ObjectDefinition(Int32 Id)
                : base(DomainObjectState.Clean)
            {
                DataFields["Id"] = Id;
                _details = new ObjectModel(this);
            }

            ~ObjectDefinition() { Dispose(false); }

            public void Dispose() { Dispose(true); GC.SuppressFinalize(this); }

            protected virtual void Dispose(bool disposing)
            {
                if (!_disposed)
                {
                    if (disposing)
                    {
                        _details = null;
                        DataFields = null;
                        //base.Dispose(true);
                    }
                }
                _disposed = true;
            }

            #region IDomainObject Members

            void IDomainObject.CreateEntity()
            {
                LiquidCore.Data.ObjectData.Create(Details);
            }

            void IDomainObject.LoadEntity(ModelObject Details)
            {
                LiquidCore.Data.ObjectData.Load(Details, DataFields);
            }

            void IDomainObject.UpdateEntity()
            {
                LiquidCore.Data.ObjectData.Update(Details);
            }

            void IDomainObject.DeleteEntity()
            {
                LiquidCore.Data.ObjectData.Delete(Details, DataFields);
            }

            #endregion

            #region GenericComparer Members
            public int CompareTo(object obj, string Property)
            {
                try
                {
                    Type type = this.GetType();
                    PropertyInfo propertie = type.GetProperty(Property);


                    Type type2 = obj.GetType();
                    PropertyInfo propertie2 = type2.GetProperty(Property);

                    object[] index = null;

                    object Obj1 = propertie.GetValue(this, index);
                    object Obj2 = propertie2.GetValue(obj, index);

                    IComparable Ic1 = (IComparable)Obj1;
                    IComparable Ic2 = (IComparable)Obj2;

                    int ret = Ic1.CompareTo(Ic2);

                    return ret;

                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
            #endregion GenericComparer Members

        }
        public class ModelsDefinition : DomainObject, IDomainObject, IDisposable, ICollection<Model>
        {
            private bool _disposed = false;
            protected List<Model> _list = new List<Model>();

            private ModelsModel _details = null;

            public Model this[Int32 index]
            {
                get
                {
                    return _list[index];
                }
            }

            public ModelsDefinition()
                : base(DomainObjectState.Clean)
            {
                _details = new ModelsModel(this);
            }

            public ModelsDefinition(Int32 SitId, Int32 ParentId)
                : base(DomainObjectState.Clean)
            {
                this.DataFields["SitId"] = SitId;
                this.DataFields["ParentId"] = ParentId;
                _details = new ModelsModel(this);
            }

            ~ModelsDefinition() { Dispose(false); }

            protected virtual void Dispose(bool disposing)
            {
                if (!_disposed)
                {
                    if (disposing)
                    {
                        _details = null;
                        DataFields = null;
                        foreach (Model l in _list)
                            l.Dispose();
                    }
                }
                _disposed = true;
            }

            public IEnumerator GetEnumerator()
            {
                return _list.GetEnumerator();
            }

            public new void Save()
            {
                foreach (Model l in _list)
                    if (l.ObjectState == DomainObjectState.Dirty)
                        l.Save();
            }

            #region IDomainObject Members

            void IDomainObject.CreateEntity()
            {
                // Not in use...
            }

            void IDomainObject.LoadEntity(ModelObject details)
            {
                LiquidCore.Data.ModelData.LoadAll(ref _list, this.DataFields);
            }

            void IDomainObject.UpdateEntity()
            {
                foreach (Model l in _list)
                    if (l.ObjectState == DomainObjectState.Dirty)
                        l.Save();
            }

            void IDomainObject.DeleteEntity()
            {
                foreach (Model l in _list)
                    l.Delete();
            }

            #endregion

            #region IDisposable Members

            void IDisposable.Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            #endregion

            #region ICollection<Model> Members

            public void Clear()
            {
                _list.Clear();
            }

            public bool Contains(Model item)
            {
                //return _list.Contains(item);
                foreach (Model m in _list)
                    if (m.Id.Equals(item.Id))
                        return true;
                return false;
            }

            public void CopyTo(Model[] array, int arrayIndex)
            {
                _list.CopyTo(array, arrayIndex);
            }

            public int Count
            {
                get { return _list.Count; }
            }

            public bool IsReadOnly
            {
                get { throw new Exception("The method or operation is not implemented."); }
            }

            public bool Remove(Model item)
            {
                return _list.Remove(item);
            }

            public void Add(Model item)
            {
                _list.Add(item);
            }

            #endregion

            #region IEnumerable<Model> Members

            IEnumerator<Model> IEnumerable<Model>.GetEnumerator()
            {
                return _list.GetEnumerator();
            }

            #endregion

            #region GenericComparer Members

            public enum SortOrderEnum
            {
                Ascending,
                Descending
            }

            public enum SortParamEnum
            {
                Id,
                Name,
                CreatedDate,
                UpdatedDate,
            }

            public class GenericComparer : IComparer<Model>
            {
                private String _Property = null;
                private SortOrderEnum _SortOrder = SortOrderEnum.Ascending;

                public String SortProperty
                {
                    get { return _Property; }
                    set { _Property = value; }
                }

                public SortOrderEnum SortOrder
                {
                    get { return _SortOrder; }
                    set { _SortOrder = value; }
                }

                public int Compare(Model x, Model y)
                {
                    try
                    {
                        Model ing1 = (Model)x;
                        Model ing2 = (Model)y;

                        if (this.SortOrder.Equals(SortOrderEnum.Ascending))
                            return ing1.CompareTo(ing2, this.SortProperty);
                        else
                            return ing2.CompareTo(ing1, this.SortProperty);
                    }
                    catch (Exception ex)
                    {
                        return 0;
                    }
                }
            }


            public void Sort(SortParamEnum SortBy, SortOrderEnum SortOrder)
            {
                GenericComparer comparer = new GenericComparer();
                comparer.SortProperty = SortBy.ToString();
                comparer.SortOrder = SortOrder;
                _list.Sort((IComparer<Model>)comparer);
            }

            #endregion GenericComparer Members
        }
        public class ModelDefinition : DomainObject, IDomainObject, IDisposable
        {
            private bool _disposed = false;
            private ModelModel _details = null;

            protected ModelModel Details
            {
                get { return _details; }
                set { _details = value; }
            }

            public ModelDefinition()
                : base(DomainObjectState.New)
            {
                _details = new ModelModel(this);
            }

            public ModelDefinition(Int32 Id)
                : base(DomainObjectState.Clean)
            {
                DataFields["Id"] = Id;
                _details = new ModelModel(this);
            }

            ~ModelDefinition() { Dispose(false); }

            public void Dispose() { Dispose(true); GC.SuppressFinalize(this); }

            protected virtual void Dispose(bool disposing)
            {
                if (!_disposed)
                {
                    if (disposing)
                    {
                        _details = null;
                        DataFields = null;
                        //base.Dispose(true);
                    }
                }
                _disposed = true;
            }

            #region IDomainObject Members

            void IDomainObject.CreateEntity()
            {
                LiquidCore.Data.ModelData.Create(Details);
            }

            void IDomainObject.LoadEntity(ModelObject Details)
            {
                LiquidCore.Data.ModelData.Load(Details, DataFields);
            }

            void IDomainObject.UpdateEntity()
            {
                LiquidCore.Data.ModelData.Update(Details);
            }

            void IDomainObject.DeleteEntity()
            {
                LiquidCore.Data.ModelData.Delete(Details);
            }

            #endregion

            #region GenericComparer Members
            public int CompareTo(object obj, string Property)
            {
                try
                {
                    Type type = this.GetType();
                    PropertyInfo propertie = type.GetProperty(Property);


                    Type type2 = obj.GetType();
                    PropertyInfo propertie2 = type2.GetProperty(Property);

                    object[] index = null;

                    object Obj1 = propertie.GetValue(this, index);
                    object Obj2 = propertie2.GetValue(obj, index);

                    IComparable Ic1 = (IComparable)Obj1;
                    IComparable Ic2 = (IComparable)Obj2;

                    int ret = Ic1.CompareTo(Ic2);

                    return ret;

                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
            #endregion GenericComparer Members

        }
        public class ModelItemsDefinition : DomainObject, IDomainObject, IDisposable, ICollection<ModelItem>
        {
            private bool _disposed = false;
            protected List<ModelItem> _list = new List<ModelItem>();

            private ModelItemsModel _details = null;

            public ModelItem this[Int32 index]
            {
                get
                {
                    return _list[index];
                }
            }

            public ModelItemsDefinition()
                : base(DomainObjectState.Clean)
            {
                _details = new ModelItemsModel(this);
            }

            public ModelItemsDefinition(Int32 SitId, Int32 MdlId, Int32 ParentId)
                : base(DomainObjectState.Clean)
            {
                this.DataFields["SitId"] = SitId;
                this.DataFields["MdlId"] = MdlId;
                this.DataFields["ParentId"] = ParentId;
                _details = new ModelItemsModel(this);
            }

            ~ModelItemsDefinition() { Dispose(false); }

            protected virtual void Dispose(bool disposing)
            {
                if (!_disposed)
                {
                    if (disposing)
                    {
                        _details = null;
                        DataFields = null;
                        foreach (ModelItem l in _list)
                            l.Dispose();
                    }
                }
                _disposed = true;
            }

            public IEnumerator GetEnumerator()
            {
                return _list.GetEnumerator();
            }

            public new void Save()
            {
                foreach (ModelItem l in _list)
                    if (l.ObjectState == DomainObjectState.Dirty)
                        l.Save();
            }

            #region IDomainObject Members

            void IDomainObject.CreateEntity()
            {
                // Not in use...
            }

            void IDomainObject.LoadEntity(ModelObject details)
            {
                LiquidCore.Data.ModelItemData.LoadAll(ref _list, this.DataFields);
            }

            void IDomainObject.UpdateEntity()
            {
                foreach (ModelItem l in _list)
                    if (l.ObjectState == DomainObjectState.Dirty)
                        l.Save();
            }

            void IDomainObject.DeleteEntity()
            {
                foreach (ModelItem l in _list)
                    l.Delete();
            }

            #endregion

            #region IDisposable Members

            void IDisposable.Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            #endregion

            #region ICollection<ModelItem> Members

            public void Clear()
            {
                _list.Clear();
            }

            public bool Contains(ModelItem item)
            {
                //return _list.Contains(item);
                foreach (ModelItem m in _list)
                    if (m.Id.Equals(item.Id))
                        return true;
                return false;
            }

            public void CopyTo(ModelItem[] array, int arrayIndex)
            {
                _list.CopyTo(array, arrayIndex);
            }

            public int Count
            {
                get { return _list.Count; }
            }

            public bool IsReadOnly
            {
                get { throw new Exception("The method or operation is not implemented."); }
            }

            public bool Remove(ModelItem item)
            {
                return _list.Remove(item);
            }

            public void Add(ModelItem item)
            {
                _list.Add(item);
            }

            #endregion

            #region IEnumerable<ModelItem> Members

            IEnumerator<ModelItem> IEnumerable<ModelItem>.GetEnumerator()
            {
                return _list.GetEnumerator();
            }

            #endregion

            #region GenericComparer Members

            public enum SortOrderEnum
            {
                Ascending,
                Descending
            }

            public enum SortParamEnum
            {
                Id,
                Name,
                CreatedDate,
                UpdatedDate,
            }

            public class GenericComparer : IComparer<ModelItem>
            {
                private String _Property = null;
                private SortOrderEnum _SortOrder = SortOrderEnum.Ascending;

                public String SortProperty
                {
                    get { return _Property; }
                    set { _Property = value; }
                }

                public SortOrderEnum SortOrder
                {
                    get { return _SortOrder; }
                    set { _SortOrder = value; }
                }

                public int Compare(ModelItem x, ModelItem y)
                {
                    try
                    {
                        ModelItem ing1 = (ModelItem)x;
                        ModelItem ing2 = (ModelItem)y;

                        if (this.SortOrder.Equals(SortOrderEnum.Ascending))
                            return ing1.CompareTo(ing2, this.SortProperty);
                        else
                            return ing2.CompareTo(ing1, this.SortProperty);
                    }
                    catch (Exception ex)
                    {
                        return 0;
                    }
                }
            }


            public void Sort(SortParamEnum SortBy, SortOrderEnum SortOrder)
            {
                GenericComparer comparer = new GenericComparer();
                comparer.SortProperty = SortBy.ToString();
                comparer.SortOrder = SortOrder;
                _list.Sort((IComparer<ModelItem>)comparer);
            }

            #endregion GenericComparer Members
        }
        public class ModelItemDefinition : DomainObject, IDomainObject, IDisposable
        {
            private bool _disposed = false;
            private ModelItemModel _details = null;

            protected ModelItemModel Details
            {
                get { return _details; }
                set { _details = value; }
            }

            public ModelItemDefinition()
                : base(DomainObjectState.New)
            {
                _details = new ModelItemModel(this);
            }

            public ModelItemDefinition(Int32 Id)
                : base(DomainObjectState.Clean)
            {
                DataFields["Id"] = Id;
                _details = new ModelItemModel(this);
            }

            ~ModelItemDefinition() { Dispose(false); }

            public void Dispose() { Dispose(true); GC.SuppressFinalize(this); }

            protected virtual void Dispose(bool disposing)
            {
                if (!_disposed)
                {
                    if (disposing)
                    {
                        _details = null;
                        DataFields = null;
                        //base.Dispose(true);
                    }
                }
                _disposed = true;
            }

            #region IDomainObject Members

            void IDomainObject.CreateEntity()
            {
                LiquidCore.Data.ModelItemData.Create(Details);
            }

            void IDomainObject.LoadEntity(ModelObject Details)
            {
                LiquidCore.Data.ModelItemData.Load(Details, DataFields);
            }

            void IDomainObject.UpdateEntity()
            {
                LiquidCore.Data.ModelItemData.Update(Details);
            }

            void IDomainObject.DeleteEntity()
            {
                LiquidCore.Data.ModelItemData.Delete(Details);
            }

            #endregion

            #region GenericComparer Members
            public int CompareTo(object obj, string Property)
            {
                try
                {
                    Type type = this.GetType();
                    PropertyInfo propertie = type.GetProperty(Property);


                    Type type2 = obj.GetType();
                    PropertyInfo propertie2 = type2.GetProperty(Property);

                    object[] index = null;

                    object Obj1 = propertie.GetValue(this, index);
                    object Obj2 = propertie2.GetValue(obj, index);

                    IComparable Ic1 = (IComparable)Obj1;
                    IComparable Ic2 = (IComparable)Obj2;

                    int ret = Ic1.CompareTo(Ic2);

                    return ret;

                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
            #endregion GenericComparer Members

        }
        public class ModDefsDefinition : DomainObject, IDomainObject, IDisposable, ICollection<ModDef>
        {
            private bool _disposed = false;
            protected List<ModDef> _list = new List<ModDef>();

            private ModDefsModel _details = null;

            public ModDef this[Int32 index]
            {
                get
                {
                    return _list[index];
                }
            }

            public ModDefsDefinition()
                : base(DomainObjectState.Clean)
            {
                _details = new ModDefsModel(this);
            }

            public ModDefsDefinition(Int32 SitId, Int32 ParentId)
                : base(DomainObjectState.Clean)
            {
                this.DataFields["SitId"] = SitId;
                this.DataFields["ParentId"] = ParentId;
                _details = new ModDefsModel(this);
            }

            ~ModDefsDefinition() { Dispose(false); }

            protected virtual void Dispose(bool disposing)
            {
                if (!_disposed)
                {
                    if (disposing)
                    {
                        _details = null;
                        DataFields = null;
                        foreach (ModDef l in _list)
                            l.Dispose();
                    }
                }
                _disposed = true;
            }

            public IEnumerator GetEnumerator()
            {
                return _list.GetEnumerator();
            }

            public new void Save()
            {
                foreach (ModDef l in _list)
                    if (l.ObjectState == DomainObjectState.Dirty)
                        l.Save();
            }

            #region IDomainObject Members

            void IDomainObject.CreateEntity()
            {
                // Not in use...
            }

            void IDomainObject.LoadEntity(ModelObject details)
            {
                LiquidCore.Data.ModDefData.LoadAll(ref _list, this.DataFields);
            }

            void IDomainObject.UpdateEntity()
            {
                foreach (ModDef l in _list)
                    if (l.ObjectState == DomainObjectState.Dirty)
                        l.Save();
            }

            void IDomainObject.DeleteEntity()
            {
                foreach (ModDef l in _list)
                    l.Delete();
            }

            #endregion

            #region IDisposable Members

            void IDisposable.Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            #endregion

            #region ICollection<ModDef> Members

            public void Clear()
            {
                _list.Clear();
            }

            public bool Contains(ModDef item)
            {
                //return _list.Contains(item);
                foreach (ModDef m in _list)
                    if (m.Id.Equals(item.Id))
                        return true;
                return false;
            }

            public void CopyTo(ModDef[] array, int arrayIndex)
            {
                _list.CopyTo(array, arrayIndex);
            }

            public int Count
            {
                get { return _list.Count; }
            }

            public bool IsReadOnly
            {
                get { throw new Exception("The method or operation is not implemented."); }
            }

            public bool Remove(ModDef item)
            {
                return _list.Remove(item);
            }

            public void Add(ModDef item)
            {
                _list.Add(item);
            }

            #endregion

            #region IEnumerable<ModelItem> Members

            IEnumerator<ModDef> IEnumerable<ModDef>.GetEnumerator()
            {
                return _list.GetEnumerator();
            }

            #endregion

            #region GenericComparer Members

            public enum SortOrderEnum
            {
                Ascending,
                Descending
            }

            public enum SortParamEnum
            {
                Id,
                Name,
                CreatedDate,
                UpdatedDate,
            }

            public class GenericComparer : IComparer<ModDef>
            {
                private String _Property = null;
                private SortOrderEnum _SortOrder = SortOrderEnum.Ascending;

                public String SortProperty
                {
                    get { return _Property; }
                    set { _Property = value; }
                }

                public SortOrderEnum SortOrder
                {
                    get { return _SortOrder; }
                    set { _SortOrder = value; }
                }

                public int Compare(ModDef x, ModDef y)
                {
                    try
                    {
                        ModDef ing1 = (ModDef)x;
                        ModDef ing2 = (ModDef)y;

                        if (this.SortOrder.Equals(SortOrderEnum.Ascending))
                            return ing1.CompareTo(ing2, this.SortProperty);
                        else
                            return ing2.CompareTo(ing1, this.SortProperty);
                    }
                    catch (Exception ex)
                    {
                        return 0;
                    }
                }
            }


            public void Sort(SortParamEnum SortBy, SortOrderEnum SortOrder)
            {
                GenericComparer comparer = new GenericComparer();
                comparer.SortProperty = SortBy.ToString();
                comparer.SortOrder = SortOrder;
                _list.Sort((IComparer<ModDef>)comparer);
            }

            #endregion GenericComparer Members
        }
        public class ModDefDefinition : DomainObject, IDomainObject, IDisposable
        {
            private bool _disposed = false;
            private ModDefModel _details = null;

            protected ModDefModel Details
            {
                get { return _details; }
                set { _details = value; }
            }

            public ModDefDefinition()
                : base(DomainObjectState.New)
            {
                _details = new ModDefModel(this);
            }

            public ModDefDefinition(Int32 Id)
                : base(DomainObjectState.Clean)
            {
                DataFields["Id"] = Id;
                _details = new ModDefModel(this);
            }

            ~ModDefDefinition() { Dispose(false); }

            public void Dispose() { Dispose(true); GC.SuppressFinalize(this); }

            protected virtual void Dispose(bool disposing)
            {
                if (!_disposed)
                {
                    if (disposing)
                    {
                        _details = null;
                        DataFields = null;
                        //base.Dispose(true);
                    }
                }
                _disposed = true;
            }

            #region IDomainObject Members

            void IDomainObject.CreateEntity()
            {
                LiquidCore.Data.ModDefData.Create(Details);
            }

            void IDomainObject.LoadEntity(ModelObject Details)
            {
                LiquidCore.Data.ModDefData.Load(Details, DataFields);
            }

            void IDomainObject.UpdateEntity()
            {
                LiquidCore.Data.ModDefData.Update(Details);
            }

            void IDomainObject.DeleteEntity()
            {
                LiquidCore.Data.ModDefData.Delete(Details);
            }

            #endregion

            #region GenericComparer Members
            public int CompareTo(object obj, string Property)
            {
                try
                {
                    Type type = this.GetType();
                    PropertyInfo propertie = type.GetProperty(Property);


                    Type type2 = obj.GetType();
                    PropertyInfo propertie2 = type2.GetProperty(Property);

                    object[] index = null;

                    object Obj1 = propertie.GetValue(this, index);
                    object Obj2 = propertie2.GetValue(obj, index);

                    IComparable Ic1 = (IComparable)Obj1;
                    IComparable Ic2 = (IComparable)Obj2;

                    int ret = Ic1.CompareTo(Ic2);

                    return ret;

                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
            #endregion GenericComparer Members

        }
        
        public class MenuDefinition : DomainObject, IDomainObject, IDisposable, ICollection<Menu.Item>
        {
            private bool _disposed = false;
            protected List<Menu.Item> _list = new List<Menu.Item>();

            private MenuModel _details = null;

            public Menu.Item this[Int32 index]
            {
                get
                {
                    return _list[index];
                }
            }

            public MenuDefinition()
                : base(DomainObjectState.Clean)
            {
                _details = new MenuModel(this);
            }

            public MenuDefinition(Int32 SitId, Int32 ParentId)
                : base(DomainObjectState.Clean)
            {
                this.DataFields["SitId"] = SitId;
                this.DataFields["ParentId"] = ParentId;
                _details = new MenuModel(this);
            }

			public MenuDefinition(Int32 SitId, Int32 ParentId, Int32 Status)
				: base(DomainObjectState.Clean)
			{
				this.DataFields["SitId"] = SitId;
				this.DataFields["ParentId"] = ParentId;
				this.DataFields["Status"] = Status;
				_details = new MenuModel(this);
			}

            public MenuDefinition(String Alias)
                : base(DomainObjectState.Clean)
            {
                this.DataFields["Alias"] = Alias;
                _details = new MenuModel(this);
            }

            ~MenuDefinition() { Dispose(false); }

            protected virtual void Dispose(bool disposing)
            {
                if (!_disposed)
                {
                    if (disposing)
                    {
                        _details = null;
                        DataFields = null;
                        foreach (Menu.Item l in _list)
                            l.Dispose();
                    }
                }
                _disposed = true;
            }

            public IEnumerator GetEnumerator()
            {
                return _list.GetEnumerator();
            }

            public new void Save()
            {
                foreach (Menu.Item l in _list)
                    if (l.ObjectState == DomainObjectState.Dirty)
                        l.Save();
            }

            #region IDomainObject Members

            void IDomainObject.CreateEntity()
            {
                // Not in use...
            }

            void IDomainObject.LoadEntity(ModelObject details)
            {
                LiquidCore.Data.MenuData.LoadAll(ref _list, this.DataFields);
            }

            void IDomainObject.UpdateEntity()
            {
                foreach (Menu.Item l in _list)
                    if (l.ObjectState == DomainObjectState.Dirty)
                        l.Save();
            }

            void IDomainObject.DeleteEntity()
            {
                foreach (Menu.Item l in _list)
                    l.Delete();
            }

            #endregion

            #region IDisposable Members

            void IDisposable.Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            #endregion

            #region ICollection<Menu.Item> Members

            public void Clear()
            {
                _list.Clear();
            }

            public bool Contains(Menu.Item item)
            {
                //return _list.Contains(item);
                foreach (Menu.Item m in _list)
                    if (m.Id.Equals(item.Id))
                        return true;
                return false;
            }

            public void CopyTo(Menu.Item[] array, int arrayIndex)
            {
                _list.CopyTo(array, arrayIndex);
            }

            public int Count
            {
                get { return _list.Count; }
            }

            public bool IsReadOnly
            {
                get { throw new Exception("The method or operation is not implemented."); }
            }

            public bool Remove(Menu.Item item)
            {
                return _list.Remove(item);
            }

            public void Add(Menu.Item item)
            {
                _list.Add(item);
            }

            #endregion

            #region IEnumerable<Menu.Item> Members

            IEnumerator<Menu.Item> IEnumerable<Menu.Item>.GetEnumerator()
            {
                return _list.GetEnumerator();
            }

            #endregion

            #region GenericComparer Members

            public enum SortOrderEnum
            {
                Ascending,
                Descending
            }

            public enum SortParamEnum
            {
                Id,
                Status,
                Language,
                Order,
                ModelId,
                Title,
                Alias,
                Description,
                Template,
                CreatedDate,
                UpdatedDate,
            }

            public class GenericComparer : IComparer<Menu.Item>
            {
                private String _Property = null;
                private SortOrderEnum _SortOrder = SortOrderEnum.Ascending;

                public String SortProperty
                {
                    get { return _Property; }
                    set { _Property = value; }
                }

                public SortOrderEnum SortOrder
                {
                    get { return _SortOrder; }
                    set { _SortOrder = value; }
                }

                public int Compare(Menu.Item x, Menu.Item y)
                {
                    try
                    {
                        Menu.Item ing1 = (Menu.Item)x;
                        Menu.Item ing2 = (Menu.Item)y;

                        if (this.SortOrder.Equals(SortOrderEnum.Ascending))
                            return ing1.CompareTo(ing2, this.SortProperty);
                        else
                            return ing2.CompareTo(ing1, this.SortProperty);
                    }
                    catch (Exception ex)
                    {
                        return 0;
                    }
                }
            }


            public void Sort(SortParamEnum SortBy, SortOrderEnum SortOrder)
            {
                GenericComparer comparer = new GenericComparer();
                comparer.SortProperty = SortBy.ToString();
                comparer.SortOrder = SortOrder;
                _list.Sort((IComparer<Menu.Item>)comparer);
            }

            #endregion GenericComparer Members

            public class ItemDefinition : DomainObject, IDomainObject, IDisposable
            {
                private bool _disposed = false;
                private MenuModel.ItemModel _details = null;

                protected MenuModel.ItemModel Details
                {
                    get { return _details; }
                    set { _details = value; }
                }

                public ItemDefinition()
                    : base(DomainObjectState.New)
                {
					this.DataFields["PreventSortOnSave"] = false;
                    _details = new MenuModel.ItemModel(this);
                }

				public ItemDefinition(bool PreventSortOnSave)
					: base(DomainObjectState.New)
				{
					this.DataFields["PreventSortOnSave"] = PreventSortOnSave;
					_details = new MenuModel.ItemModel(this);
				}

                public ItemDefinition(Int32 Id)
                    : base(DomainObjectState.Clean)
                {
                    this.DataFields["Id"] = Id;
					this.DataFields["PreventSortOnSave"] = false;
                    _details = new MenuModel.ItemModel(this);
                }

				public ItemDefinition(DataRow dr)
					: base(DomainObjectState.Clean)
				{
					this.DataFields["dr"] = dr;
					this.DataFields["PreventSortOnSave"] = false;
					_details = new MenuModel.ItemModel(this);
				}

                ~ItemDefinition() { Dispose(false); }

                public void Dispose() { Dispose(true); GC.SuppressFinalize(this); }

                protected virtual void Dispose(bool disposing)
                {
                    if (!_disposed)
                    {
                        if (disposing)
                        {
                            _details = null;
                            DataFields = null;
                            //base.Dispose(true);
                        }
                    }
                    _disposed = true;
                }

                #region IDomainObject Members

                void IDomainObject.CreateEntity()
                {
					LiquidCore.Data.MenuData.Create(Details, DataFields);
                }

                void IDomainObject.LoadEntity(ModelObject Details)
                {
                    LiquidCore.Data.MenuData.Load(Details, DataFields);
                }

                void IDomainObject.UpdateEntity()
                {
					LiquidCore.Data.MenuData.Update(Details, DataFields);
                }

                void IDomainObject.DeleteEntity()
                {
                    LiquidCore.Data.MenuData.Delete(Details, DataFields);
                }

                #endregion

                #region GenericComparer Members
                public int CompareTo(object obj, string Property)
                {
                    try
                    {
                        Type type = this.GetType();
                        PropertyInfo propertie = type.GetProperty(Property);


                        Type type2 = obj.GetType();
                        PropertyInfo propertie2 = type2.GetProperty(Property);

                        object[] index = null;

                        object Obj1 = propertie.GetValue(this, index);
                        object Obj2 = propertie2.GetValue(obj, index);

                        IComparable Ic1 = (IComparable)Obj1;
                        IComparable Ic2 = (IComparable)Obj2;

                        //int ret = Ic1.CompareTo(Ic2);
                        int ret = String.Compare(HttpUtility.HtmlDecode(Ic1.ToString()), HttpUtility.HtmlDecode(Ic2.ToString()), true, new System.Globalization.CultureInfo("sv-SE"));

                        return ret;

                    }
                    catch (Exception ex)
                    {
                        return 0;
                    }
                }
                #endregion GenericComparer Members

            }
        }
        public class ListDefinition : DomainObject, IDomainObject, IDisposable, ICollection<List.Item>
        {
            private bool _disposed = false;
            protected List<List.Item> _list = new List<List.Item>();

            private ListModel _details = null;

            public List.Item this[Int32 index]
            {
                get
                {
                    return _list[index];
                }
            }

            public ListDefinition()
                : base(DomainObjectState.Clean)
            {
                _details = new ListModel(this);
            }

            public ListDefinition(Int32 SitId, Int32 PagId, Int32 ModId)
                : base(DomainObjectState.Clean)
            {
                this.DataFields["SitId"] = SitId;
                this.DataFields["PagId"] = PagId;
                this.DataFields["ModId"] = ModId;
                _details = new ListModel(this);
            }

            public ListDefinition(Int32 SitId, Int32 PagId, Int32 ModId, Int32 MaxRowCount, String StartDate, String EndDate)
                : base(DomainObjectState.Clean)
            {
                this.DataFields["SitId"] = SitId;
                this.DataFields["PagId"] = PagId;
                this.DataFields["ModId"] = ModId;
                this.DataFields["MaxRowCount"] = MaxRowCount;
                this.DataFields["StartDate"] = StartDate;
                this.DataFields["EndDate"] = EndDate;
                _details = new ListModel(this);
            }

            public ListDefinition(Int32 SitId, Int32 PagId, Int32 MaxRowCount, String StartDate, String EndDate)
                : base(DomainObjectState.Clean)
            {
                this.DataFields["SitId"] = SitId;
                this.DataFields["PagId"] = PagId;
                this.DataFields["MaxRowCount"] = MaxRowCount;
                this.DataFields["StartDate"] = StartDate;
                this.DataFields["EndDate"] = EndDate;
                _details = new ListModel(this);
            }

            public ListDefinition(Int32 SitId, Int32 MaxRowCount, String StartDate, String EndDate)
                : base(DomainObjectState.Clean)
            {
                this.DataFields["SitId"] = SitId;
                this.DataFields["MaxRowCount"] = MaxRowCount;
                this.DataFields["StartDate"] = StartDate;
                this.DataFields["EndDate"] = EndDate;
                _details = new ListModel(this);
            }

            public ListDefinition(String Alias)
                : base(DomainObjectState.Clean)
            {
                this.DataFields["Alias"] = Alias;
                _details = new ListModel(this);
            }

			public ListDefinition(String Alias, bool PreventSortOnSave)
				: base(DomainObjectState.Clean)
			{
				this.DataFields["Alias"] = Alias;
				this.DataFields["PreventSortOnSave"] = PreventSortOnSave;
				_details = new ListModel(this);
			}

            public ListDefinition(String Alias, Prefix prefix, Param[] Parameters)
                : base(DomainObjectState.Clean)
            {
                this.DataFields["Prefix"] = prefix;
                this.DataFields["Alias"] = Alias;
                this.DataFields["Parameters"] = Parameters;
                _details = new ListModel(this);
            }

			public ListDefinition(String Alias, Prefix prefix, Param[] Parameters, bool PreventSortOnSave)
				: base(DomainObjectState.Clean)
			{
				this.DataFields["Prefix"] = prefix;
				this.DataFields["Alias"] = Alias;
				this.DataFields["Parameters"] = Parameters;
				this.DataFields["PreventSortOnSave"] = PreventSortOnSave;
				_details = new ListModel(this);
			}

            ~ListDefinition() { Dispose(false); }

            protected virtual void Dispose(bool disposing)
            {
                if (!_disposed)
                {
                    if (disposing)
                    {
                        _details = null;
                        DataFields = null;
                        foreach (List.Item l in _list)
                            l.Dispose();
                    }
                }
                _disposed = true;
            }

            public IEnumerator GetEnumerator()
            {
                return _list.GetEnumerator();
            }

            public new void Save()
            {
                foreach (List.Item l in _list)
                    if (l.ObjectState == DomainObjectState.Dirty)
                        l.Save();
            }

            public enum Prefix
            {
                AND,
                OR
            };
            public enum DataType
            {
                String,
                Integer,
                DateTime,
                Float,
                Double
            };
            public enum Operator
            {
                SmallerThan,
                SmallerThanEquals,
                Equals,
                GreaterThanEquals,
                GreaterThan,
				Contains
            };
            public static string GetOperator(Operator o)
            {
                switch (o)
                {
                    case Operator.SmallerThan:
                        return "<";
                    case Operator.SmallerThanEquals:
                        return "<=";
                    case Operator.Equals:
                        return "=";
                    case Operator.GreaterThanEquals:
                        return ">=";
                    case Operator.GreaterThan:
                        return ">";
                    default:
                        return String.Empty;
                }
            }
            public struct Param
            {
                public String name;
                public String value;
                public Operator operand;
                public DataType datatype;
            }


            #region IDomainObject Members

            void IDomainObject.CreateEntity()
            {
                // Not in use...
            }

            void IDomainObject.LoadEntity(ModelObject details)
            {
                LiquidCore.Data.ListData.LoadAll(ref _list, this.DataFields);
            }

            void IDomainObject.UpdateEntity()
            {
                foreach (List.Item l in _list)
                    if (l.ObjectState == DomainObjectState.Dirty)
                        l.Save();
            }

            void IDomainObject.DeleteEntity()
            {
                foreach (List.Item l in _list)
                    l.Delete();
            }

            #endregion

            #region IDisposable Members

            void IDisposable.Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            #endregion

            #region ICollection<List.Item> Members

            public void Clear()
            {
                _list.Clear();
            }

            public bool Contains(List.Item item)
            {
                //return _list.Contains(item);
                foreach (List.Item m in _list)
                    if (m.Id.Equals(item.Id))
                        return true;
                return false;
            }

            public void CopyTo(List.Item[] array, int arrayIndex)
            {
                _list.CopyTo(array, arrayIndex);
            }

            public int Count
            {
                get { return _list.Count; }
            }

            public bool IsReadOnly
            {
                get { throw new Exception("The method or operation is not implemented."); }
            }

            public bool Remove(List.Item item)
            {
                return _list.Remove(item);
            }

            public void Add(List.Item item)
            {
                _list.Add(item);
            }

            #endregion

            #region IEnumerable<List.Item> Members

            IEnumerator<List.Item> IEnumerable<List.Item>.GetEnumerator()
            {
                return _list.GetEnumerator();
            }

            #endregion

            #region GenericComparer Members

            public enum SortOrderEnum
            {
                Ascending,
                Descending
            }

            public enum SortParamEnum
            {
                Id,
                Order,
                Type,
                Title,
                Alias,
                Description,
                Value1,
                Value2,
                Value3,
                Value4,
                Value5,
                Value6,
                Value7,
                Value8,
                Value9,
                Value10,
                Value11,
                Value12,
                Value13,
                Value14,
                Value15,
                Value16,
                Value17,
                Value18,
                Value19,
                Value20,
                Value21,
                Value22,
                Value23,
                Value24,
                Value25,
                Value26,
                Value27,
                Value28,
                Value29,
                Value30,
                Value31,
                Value32,
                Value33,
                Value34,
                Value35,
                Value36,
                Value37,
                Value38,
                Value39,
                Value40,
                Value41,
                Value42,
                Value43,
                Value44,
                Value45,
                Value46,
                Value47,
                Value48,
                Value49,
                Value50,
                CreatedDate,
                UpdatedDate,
            }

            public class GenericComparer : IComparer<List.Item>
            {
                private String _Property = null;
                private SortOrderEnum _SortOrder = SortOrderEnum.Ascending;

                public String SortProperty
                {
                    get { return _Property; }
                    set { _Property = value; }
                }

                public SortOrderEnum SortOrder
                {
                    get { return _SortOrder; }
                    set { _SortOrder = value; }
                }

                public int Compare(List.Item x, List.Item y)
                {
                    try
                    {
                        List.Item ing1 = (List.Item)x;
                        List.Item ing2 = (List.Item)y;

                        if (this.SortOrder.Equals(SortOrderEnum.Ascending))
                            return ing1.CompareTo(ing2, this.SortProperty);
                        else
                            return ing2.CompareTo(ing1, this.SortProperty);
                    }
                    catch (Exception ex)
                    {
                        return 0;
                    }
                }
            }


            public void Sort(SortParamEnum SortBy, SortOrderEnum SortOrder)
            {
                GenericComparer comparer = new GenericComparer();
                comparer.SortProperty = SortBy.ToString();
                comparer.SortOrder = SortOrder;
                _list.Sort((IComparer<List.Item>)comparer);
            }

            #endregion GenericComparer Members

            public class ItemDefinition : DomainObject, IDomainObject, IDisposable
            {
                private bool _disposed = false;
                private ListModel.ItemModel _details = null;

                protected ListModel.ItemModel Details
                {
                    get { return _details; }
                    set { _details = value; }
                }

                public ItemDefinition()
                    : base(DomainObjectState.New)
                {
                    DataFields["PreventSortOnSave"] = false;
                    _details = new ListModel.ItemModel(this);
                }

                public ItemDefinition(Int32 Id)
                    : base(DomainObjectState.Clean)
                {
                    DataFields["Id"] = Id;
                    DataFields["PreventSortOnSave"] = false;
                    _details = new ListModel.ItemModel(this);
                }

				public ItemDefinition(DataRow dr)
					: base(DomainObjectState.Clean)
				{
					DataFields["dr"] = dr;
					DataFields["PreventSortOnSave"] = false;
					_details = new ListModel.ItemModel(this);
				}

				public ItemDefinition(Int32 Id, bool PreventSortOnSave)
					: base(DomainObjectState.Clean)
				{
					DataFields["Id"] = Id;
					DataFields["PreventSortOnSave"] = PreventSortOnSave;
					_details = new ListModel.ItemModel(this);
				}

				public ItemDefinition(DataRow dr, bool PreventSortOnSave)
					: base(DomainObjectState.Clean)
				{
					DataFields["dr"] = dr;
					DataFields["PreventSortOnSave"] = PreventSortOnSave;
					_details = new ListModel.ItemModel(this);
				}

                public ItemDefinition(Boolean PreventSortOnSave)
                    : base(DomainObjectState.New)
                {
                    DataFields["PreventSortOnSave"] = PreventSortOnSave;
                    _details = new ListModel.ItemModel(this);
                }

                ~ItemDefinition() { Dispose(false); }

                public void Dispose() { Dispose(true); GC.SuppressFinalize(this); }

                protected virtual void Dispose(bool disposing)
                {
                    if (!_disposed)
                    {
                        if (disposing)
                        {
                            _details = null;
                            DataFields = null;
                            //base.Dispose(true);
                        }
                    }
                    _disposed = true;
                }

                #region IDomainObject Members

                void IDomainObject.CreateEntity()
                {
                    LiquidCore.Data.ListData.Create(Details, DataFields);
                }

                void IDomainObject.LoadEntity(ModelObject Details)
                {
                    LiquidCore.Data.ListData.Load(Details, DataFields);
                }

                void IDomainObject.UpdateEntity()
                {
                    LiquidCore.Data.ListData.Update(Details, DataFields);
                }

                void IDomainObject.DeleteEntity()
                {
                    LiquidCore.Data.ListData.Delete(Details, DataFields);
                }

                #endregion

                #region GenericComparer Members
                public int CompareTo(object obj, string Property)
                {
                    int ret = 0;
                    try
                    {
                        Type type = this.GetType();
                        PropertyInfo propertie = type.GetProperty(Property);


                        Type type2 = obj.GetType();
                        PropertyInfo propertie2 = type2.GetProperty(Property);

                        object[] index = null;

                        object Obj1 = propertie.GetValue(this, index);
                        object Obj2 = propertie2.GetValue(obj, index);

                        IComparable Ic1 = (IComparable)Obj1;
                        IComparable Ic2 = (IComparable)Obj2;

                        if(propertie2.PropertyType.FullName .Equals("System.Int32"))
                            ret = Ic1.CompareTo(Ic2);
                        else
                            ret = String.Compare(HttpUtility.HtmlDecode(Ic1.ToString()), HttpUtility.HtmlDecode(Ic2.ToString()), true, new System.Globalization.CultureInfo("sv-SE")); 

                        return ret;

                    }
                    catch (Exception ex)
                    {
                        return 0;
                    }
                }
                #endregion GenericComparer Members

            }
        }
        public class UserTypesDefinition : DomainObject, IDomainObject, IDisposable, ICollection<UserTypes.UserType>
        {
            private bool _disposed = false;
            protected List<UserTypes.UserType> _list = new List<UserTypes.UserType>();

            private UserTypesModel _details = null;

            public UserTypes.UserType this[Int32 index]
            {
                get
                {
                    return _list[index];
                }
            }

            public UserTypesDefinition()
                : base(DomainObjectState.Clean)
            {
                _details = new UserTypesModel(this);
            }

            public UserTypesDefinition(String Alias)
                : base(DomainObjectState.Clean)
            {
                this.DataFields["Alias"] = Alias;
                _details = new UserTypesModel(this);
            }

            ~UserTypesDefinition() { Dispose(false); }

            protected virtual void Dispose(bool disposing)
            {
                if (!_disposed)
                {
                    if (disposing)
                    {
                        _details = null;
                        DataFields = null;
                        foreach (UserTypes.UserType l in _list)
                            l.Dispose();
                    }
                }
                _disposed = true;
            }

            public IEnumerator GetEnumerator()
            {
                return _list.GetEnumerator();
            }

            public new void Save()
            {
                foreach (UserTypes.UserType l in _list)
                    if (l.ObjectState == DomainObjectState.Dirty)
                        l.Save();
            }

            #region IDomainObject Members

            void IDomainObject.CreateEntity()
            {
                // Not in use...
            }

            void IDomainObject.LoadEntity(ModelObject details)
            {
                LiquidCore.Data.UserTypeData.LoadAll(ref _list, this.DataFields);
            }

            void IDomainObject.UpdateEntity()
            {
                foreach (UserTypes.UserType l in _list)
                    if (l.ObjectState == DomainObjectState.Dirty)
                        l.Save();
            }

            void IDomainObject.DeleteEntity()
            {
                foreach (UserTypes.UserType l in _list)
                    l.Delete();
            }

            #endregion

            #region IDisposable Members

            void IDisposable.Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            #endregion

            #region ICollection<UserTypes.UserType> Members

            public void Clear()
            {
                _list.Clear();
            }

            public bool Contains(UserTypes.UserType item)
            {
                //return _list.Contains(item);
                foreach (UserTypes.UserType m in _list)
                    if (m.Id.Equals(item.Id))
                        return true;
                return false;
            }

            public void CopyTo(UserTypes.UserType[] array, int arrayIndex)
            {
                _list.CopyTo(array, arrayIndex);
            }

            public int Count
            {
                get { return _list.Count; }
            }

            public bool IsReadOnly
            {
                get { throw new Exception("The method or operation is not implemented."); }
            }

            public bool Remove(UserTypes.UserType item)
            {
                return _list.Remove(item);
            }

            public void Add(UserTypes.UserType item)
            {
                _list.Add(item);
            }

            #endregion

            #region IEnumerable<UserTypes.UserType> Members

            IEnumerator<UserTypes.UserType> IEnumerable<UserTypes.UserType>.GetEnumerator()
            {
                return _list.GetEnumerator();
            }

            #endregion

            #region GenericComparer Members

            public enum SortOrderEnum
            {
                Ascending,
                Descending
            }

            public enum SortParamEnum
            {
                Id,
                Name,
                CreatedDate,
                UpdatedDate,
            }

            public class GenericComparer : IComparer<UserTypes.UserType>
            {
                private String _Property = null;
                private SortOrderEnum _SortOrder = SortOrderEnum.Ascending;

                public String SortProperty
                {
                    get { return _Property; }
                    set { _Property = value; }
                }

                public SortOrderEnum SortOrder
                {
                    get { return _SortOrder; }
                    set { _SortOrder = value; }
                }

                public int Compare(UserTypes.UserType x, UserTypes.UserType y)
                {
                    try
                    {
                        UserTypes.UserType ing1 = (UserTypes.UserType)x;
                        UserTypes.UserType ing2 = (UserTypes.UserType)y;

                        if (this.SortOrder.Equals(SortOrderEnum.Ascending))
                            return ing1.CompareTo(ing2, this.SortProperty);
                        else
                            return ing2.CompareTo(ing1, this.SortProperty);
                    }
                    catch (Exception ex)
                    {
                        return 0;
                    }
                }
            }


            public void Sort(SortParamEnum SortBy, SortOrderEnum SortOrder)
            {
                GenericComparer comparer = new GenericComparer();
                comparer.SortProperty = SortBy.ToString();
                comparer.SortOrder = SortOrder;
                _list.Sort((IComparer<UserTypes.UserType>)comparer);
            }

            #endregion GenericComparer Members

            public class UserTypeDefinition : DomainObject, IDomainObject, IDisposable
            {
                private bool _disposed = false;
                private UserTypesModel.UserTypeModel _details = null;

                protected UserTypesModel.UserTypeModel Details
                {
                    get { return _details; }
                    set { _details = value; }
                }

                public UserTypeDefinition()
                    : base(DomainObjectState.New)
                {
                    _details = new UserTypesModel.UserTypeModel(this);
                }

                public UserTypeDefinition(Int32 Id)
                    : base(DomainObjectState.Clean)
                {
                    DataFields["Id"] = Id;
                    _details = new UserTypesModel.UserTypeModel(this);
                }

                ~UserTypeDefinition() { Dispose(false); }

                public void Dispose() { Dispose(true); GC.SuppressFinalize(this); }

                protected virtual void Dispose(bool disposing)
                {
                    if (!_disposed)
                    {
                        if (disposing)
                        {
                            _details = null;
                            DataFields = null;
                            //base.Dispose(true);
                        }
                    }
                    _disposed = true;
                }

                #region IDomainObject Members

                void IDomainObject.CreateEntity()
                {
                    LiquidCore.Data.UserTypeData.Create(Details);
                }

                void IDomainObject.LoadEntity(ModelObject Details)
                {
                    LiquidCore.Data.UserTypeData.Load(Details, DataFields);
                }

                void IDomainObject.UpdateEntity()
                {
                    LiquidCore.Data.UserTypeData.Update(Details);
                }

                void IDomainObject.DeleteEntity()
                {
                    LiquidCore.Data.UserTypeData.Delete(Details);
                }

                #endregion

                #region GenericComparer Members
                public int CompareTo(object obj, string Property)
                {
                    try
                    {
                        Type type = this.GetType();
                        PropertyInfo propertie = type.GetProperty(Property);


                        Type type2 = obj.GetType();
                        PropertyInfo propertie2 = type2.GetProperty(Property);

                        object[] index = null;

                        object Obj1 = propertie.GetValue(this, index);
                        object Obj2 = propertie2.GetValue(obj, index);

                        IComparable Ic1 = (IComparable)Obj1;
                        IComparable Ic2 = (IComparable)Obj2;

                        int ret = Ic1.CompareTo(Ic2);

                        return ret;

                    }
                    catch (Exception ex)
                    {
                        return 0;
                    }
                }
                #endregion GenericComparer Members

            }
        }
        public class RolesDefinition : DomainObject, IDomainObject, IDisposable, ICollection<Roles.Role>
        {
            private bool _disposed = false;
            protected List<Roles.Role> _list = new List<Roles.Role>();

            private RolesModel _details = null;

            public Roles.Role this[Int32 index]
            {
                get
                {
                    return _list[index];
                }
            }

            public RolesDefinition()
                : base(DomainObjectState.Clean)
            {
                _details = new RolesModel(this);
            }

            public RolesDefinition(String Alias)
                : base(DomainObjectState.Clean)
            {
                this.DataFields["Alias"] = Alias;
                _details = new RolesModel(this);
            }

            ~RolesDefinition() { Dispose(false); }

            protected virtual void Dispose(bool disposing)
            {
                if (!_disposed)
                {
                    if (disposing)
                    {
                        _details = null;
                        DataFields = null;
                        foreach (Roles.Role l in _list)
                            l.Dispose();
                    }
                }
                _disposed = true;
            }

            public IEnumerator GetEnumerator()
            {
                return _list.GetEnumerator();
            }

            public new void Save()
            {
                foreach (Roles.Role l in _list)
                    if (l.ObjectState == DomainObjectState.Dirty)
                        l.Save();
            }

            #region IDomainObject Members

            void IDomainObject.CreateEntity()
            {
                // Not in use...
            }

            void IDomainObject.LoadEntity(ModelObject details)
            {
                LiquidCore.Data.RoleData.LoadAll(ref _list, this.DataFields);
            }

            void IDomainObject.UpdateEntity()
            {
                foreach (Roles.Role l in _list)
                    if (l.ObjectState == DomainObjectState.Dirty)
                        l.Save();
            }

            void IDomainObject.DeleteEntity()
            {
                foreach (Roles.Role l in _list)
                    l.Delete();
            }

            #endregion

            #region IDisposable Members

            void IDisposable.Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            #endregion

            #region ICollection<Roles.Role> Members

            public void Clear()
            {
                _list.Clear();
            }

            public bool Contains(Roles.Role item)
            {
                //return _list.Contains(item);
                foreach (Roles.Role m in _list)
                    if (m.Id.Equals(item.Id))
                        return true;
                return false;
            }

            public void CopyTo(Roles.Role[] array, int arrayIndex)
            {
                _list.CopyTo(array, arrayIndex);
            }

            public int Count
            {
                get { return _list.Count; }
            }

            public bool IsReadOnly
            {
                get { throw new Exception("The method or operation is not implemented."); }
            }

            public bool Remove(Roles.Role item)
            {
                return _list.Remove(item);
            }

            public void Add(Roles.Role item)
            {
                _list.Add(item);
            }

            #endregion

            #region IEnumerable<Roles.Role> Members

            IEnumerator<Roles.Role> IEnumerable<Roles.Role>.GetEnumerator()
            {
                return _list.GetEnumerator();
            }

            #endregion

            #region GenericComparer Members

            public enum SortOrderEnum
            {
                Ascending,
                Descending
            }

            public enum SortParamEnum
            {
                Id,
                Name,
                CreatedDate,
                UpdatedDate,
            }

            public class GenericComparer : IComparer<Roles.Role>
            {
                private String _Property = null;
                private SortOrderEnum _SortOrder = SortOrderEnum.Ascending;

                public String SortProperty
                {
                    get { return _Property; }
                    set { _Property = value; }
                }

                public SortOrderEnum SortOrder
                {
                    get { return _SortOrder; }
                    set { _SortOrder = value; }
                }

                public int Compare(Roles.Role x, Roles.Role y)
                {
                    try
                    {
                        Roles.Role ing1 = (Roles.Role)x;
                        Roles.Role ing2 = (Roles.Role)y;

                        if (this.SortOrder.Equals(SortOrderEnum.Ascending))
                            return ing1.CompareTo(ing2, this.SortProperty);
                        else
                            return ing2.CompareTo(ing1, this.SortProperty);
                    }
                    catch (Exception ex)
                    {
                        return 0;
                    }
                }
            }


            public void Sort(SortParamEnum SortBy, SortOrderEnum SortOrder)
            {
                GenericComparer comparer = new GenericComparer();
                comparer.SortProperty = SortBy.ToString();
                comparer.SortOrder = SortOrder;
                _list.Sort((IComparer<Roles.Role>)comparer);
            }

            #endregion GenericComparer Members

            public class RoleDefinition : DomainObject, IDomainObject, IDisposable
            {
                private bool _disposed = false;
                private RolesModel.RoleModel _details = null;

                protected RolesModel.RoleModel Details
                {
                    get { return _details; }
                    set { _details = value; }
                }

                public RoleDefinition()
                    : base(DomainObjectState.New)
                {
                    _details = new RolesModel.RoleModel(this);
                }

                public RoleDefinition(Int32 Id)
                    : base(DomainObjectState.Clean)
                {
                    DataFields["Id"] = Id;
                    _details = new RolesModel.RoleModel(this);
                }

                ~RoleDefinition() { Dispose(false); }

                public void Dispose() { Dispose(true); GC.SuppressFinalize(this); }

                protected virtual void Dispose(bool disposing)
                {
                    if (!_disposed)
                    {
                        if (disposing)
                        {
                            _details = null;
                            DataFields = null;
                            //base.Dispose(true);
                        }
                    }
                    _disposed = true;
                }

                #region IDomainObject Members

                void IDomainObject.CreateEntity()
                {
                    LiquidCore.Data.RoleData.Create(Details);
                }

                void IDomainObject.LoadEntity(ModelObject Details)
                {
                    LiquidCore.Data.RoleData.Load(Details, DataFields);
                }

                void IDomainObject.UpdateEntity()
                {
                    LiquidCore.Data.RoleData.Update(Details);
                }

                void IDomainObject.DeleteEntity()
                {
                    LiquidCore.Data.RoleData.Delete(Details);
                }

                #endregion

                #region GenericComparer Members
                public int CompareTo(object obj, string Property)
                {
                    try
                    {
                        Type type = this.GetType();
                        PropertyInfo propertie = type.GetProperty(Property);


                        Type type2 = obj.GetType();
                        PropertyInfo propertie2 = type2.GetProperty(Property);

                        object[] index = null;

                        object Obj1 = propertie.GetValue(this, index);
                        object Obj2 = propertie2.GetValue(obj, index);

                        IComparable Ic1 = (IComparable)Obj1;
                        IComparable Ic2 = (IComparable)Obj2;

                        int ret = Ic1.CompareTo(Ic2);

                        return ret;

                    }
                    catch (Exception ex)
                    {
                        return 0;
                    }
                }
                #endregion GenericComparer Members

            }
        }
        public class UsersDefinition : DomainObject, IDomainObject, IDisposable, ICollection<Users.User>
        {
            private bool _disposed = false;
            protected List<Users.User> _list = new List<Users.User>();

            private UsersModel _details = null;

            public Users.User this[Int32 index]
            {
                get
                {
                    return _list[index];
                }
            }

            public UsersDefinition()
                : base(DomainObjectState.Clean)
            {
                _details = new UsersModel(this);
            }

            public UsersDefinition(String Alias)
                : base(DomainObjectState.Clean)
            {
                this.DataFields["Alias"] = Alias;
                _details = new UsersModel(this);
            }

            ~UsersDefinition() { Dispose(false); }

            protected virtual void Dispose(bool disposing)
            {
                if (!_disposed)
                {
                    if (disposing)
                    {
                        _details = null;
                        DataFields = null;
                        foreach (Users.User l in _list)
                            l.Dispose();
                    }
                }
                _disposed = true;
            }

            public IEnumerator GetEnumerator()
            {
                return _list.GetEnumerator();
            }

            public new void Save()
            {
                foreach (Users.User l in _list)
                    if (l.ObjectState == DomainObjectState.Dirty)
                        l.Save();
            }

            #region IDomainObject Members

            void IDomainObject.CreateEntity()
            {
                // Not in use...
            }

            void IDomainObject.LoadEntity(ModelObject details)
            {
                LiquidCore.Data.UserData.LoadAll(ref _list, this.DataFields);
            }

            void IDomainObject.UpdateEntity()
            {
                foreach (Users.User l in _list)
                    if (l.ObjectState == DomainObjectState.Dirty)
                        l.Save();
            }

            void IDomainObject.DeleteEntity()
            {
                foreach (Users.User l in _list)
                    l.Delete();
            }

            #endregion

            #region IDisposable Members

            void IDisposable.Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            #endregion

            #region ICollection<Users.User> Members

            public void Clear()
            {
                _list.Clear();
            }

            public bool Contains(Users.User item)
            {
                //return _list.Contains(item);
                foreach (Users.User m in _list)
                    if (m.Id.Equals(item.Id))
                        return true;
                return false;
            }

            public void CopyTo(Users.User[] array, int arrayIndex)
            {
                _list.CopyTo(array, arrayIndex);
            }

            public int Count
            {
                get { return _list.Count; }
            }

            public bool IsReadOnly
            {
                get { throw new Exception("The method or operation is not implemented."); }
            }

            public bool Remove(Users.User item)
            {
                return _list.Remove(item);
            }

            public void Add(Users.User item)
            {
                _list.Add(item);
            }

            #endregion

            #region IEnumerable<Users.User> Members

            IEnumerator<Users.User> IEnumerable<Users.User>.GetEnumerator()
            {
                return _list.GetEnumerator();
            }

            #endregion

            #region GenericComparer Members

            public enum SortOrderEnum
            {
                Ascending,
                Descending
            }

            public enum SortParamEnum
            {
                Id,
                Status,
                LoginName,
                Password,
                FirstName,
                MiddleName,
                LastName,
                Address,
                CO,
                PostalCode,
                City,
                Country,
                Phone,
                Mobile,
                Fax,
                Company,
                StartPage,
                CreatedDate,
                UpdatedDate,
            }

            public class GenericComparer : IComparer<Users.User>
            {
                private String _Property = null;
                private SortOrderEnum _SortOrder = SortOrderEnum.Ascending;

                public String SortProperty
                {
                    get { return _Property; }
                    set { _Property = value; }
                }

                public SortOrderEnum SortOrder
                {
                    get { return _SortOrder; }
                    set { _SortOrder = value; }
                }

                public int Compare(Users.User x, Users.User y)
                {
                    try
                    {
                        Users.User ing1 = (Users.User)x;
                        Users.User ing2 = (Users.User)y;

                        if (this.SortOrder.Equals(SortOrderEnum.Ascending))
                            return ing1.CompareTo(ing2, this.SortProperty);
                        else
                            return ing2.CompareTo(ing1, this.SortProperty);
                    }
                    catch (Exception ex)
                    {
                        return 0;
                    }
                }
            }


            public void Sort(SortParamEnum SortBy, SortOrderEnum SortOrder)
            {
                GenericComparer comparer = new GenericComparer();
                comparer.SortProperty = SortBy.ToString();
                comparer.SortOrder = SortOrder;
                _list.Sort((IComparer<Users.User>)comparer);
            }

            #endregion GenericComparer Members

            public class UserDefinition : DomainObject, IDomainObject, IDisposable
            {
                private bool _disposed = false;
                private UsersModel.UserModel _details = null;

                protected UsersModel.UserModel Details
                {
                    get { return _details; }
                    set { _details = value; }
                }

                public UserDefinition()
                    : base(DomainObjectState.New)
                {
                    _details = new UsersModel.UserModel(this);
                }

                public UserDefinition(Int32 Id)
                    : base(DomainObjectState.Clean)
                {
                    DataFields["Id"] = Id;
                    _details = new UsersModel.UserModel(this);
                }

                ~UserDefinition() { Dispose(false); }

                public void Dispose() { Dispose(true); GC.SuppressFinalize(this); }

                protected virtual void Dispose(bool disposing)
                {
                    if (!_disposed)
                    {
                        if (disposing)
                        {
                            _details = null;
                            DataFields = null;
                            //base.Dispose(true);
                        }
                    }
                    _disposed = true;
                }

                #region IDomainObject Members

                void IDomainObject.CreateEntity()
                {
                    LiquidCore.Data.UserData.Create(Details);
                }

                void IDomainObject.LoadEntity(ModelObject Details)
                {
                    LiquidCore.Data.UserData.Load(Details, DataFields);
                }

                void IDomainObject.UpdateEntity()
                {
                    LiquidCore.Data.UserData.Update(Details);
                }

                void IDomainObject.DeleteEntity()
                {
                    LiquidCore.Data.UserData.Delete(Details);
                }

                #endregion

                #region GenericComparer Members
                public int CompareTo(object obj, string Property)
                {
                    try
                    {
                        Type type = this.GetType();
                        PropertyInfo propertie = type.GetProperty(Property);


                        Type type2 = obj.GetType();
                        PropertyInfo propertie2 = type2.GetProperty(Property);

                        object[] index = null;

                        object Obj1 = propertie.GetValue(this, index);
                        object Obj2 = propertie2.GetValue(obj, index);

                        IComparable Ic1 = (IComparable)Obj1;
                        IComparable Ic2 = (IComparable)Obj2;

                        int ret = Ic1.CompareTo(Ic2);

                        return ret;

                    }
                    catch (Exception ex)
                    {
                        return 0;
                    }
                }
                #endregion GenericComparer Members

            }
        }
        public class StatusDefinition : DomainObject, IDomainObject, IDisposable, ICollection<Status.Item>
        {
            private bool _disposed = false;
            protected List<Status.Item> _list = new List<Status.Item>();

            private StatusModel _details = null;

            public Status.Item this[Int32 index]
            {
                get
                {
                    return _list[index];
                }
            }

            public StatusDefinition()
                : base(DomainObjectState.Clean)
            {
                _details = new StatusModel(this);
            }

            public StatusDefinition(String Alias)
                : base(DomainObjectState.Clean)
            {
                this.DataFields["Alias"] = Alias;
                _details = new StatusModel(this);
            }

            ~StatusDefinition() { Dispose(false); }

            protected virtual void Dispose(bool disposing)
            {
                if (!_disposed)
                {
                    if (disposing)
                    {
                        _details = null;
                        DataFields = null;
                        foreach (Status.Item l in _list)
                            l.Dispose();
                    }
                }
                _disposed = true;
            }

            public IEnumerator GetEnumerator()
            {
                return _list.GetEnumerator();
            }

            public new void Save()
            {
                foreach (Status.Item l in _list)
                    if (l.ObjectState == DomainObjectState.Dirty)
                        l.Save();
            }

            #region IDomainObject Members

            void IDomainObject.CreateEntity()
            {
                // Not in use...
            }

            void IDomainObject.LoadEntity(ModelObject details)
            {
                LiquidCore.Data.StatusData.LoadAll(ref _list, this.DataFields);
            }

            void IDomainObject.UpdateEntity()
            {
                foreach (Status.Item l in _list)
                    if (l.ObjectState == DomainObjectState.Dirty)
                        l.Save();
            }

            void IDomainObject.DeleteEntity()
            {
                foreach (Status.Item l in _list)
                    l.Delete();
            }

            #endregion

            #region IDisposable Members

            void IDisposable.Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            #endregion

            #region ICollection<Status.Item> Members

            public void Clear()
            {
                _list.Clear();
            }

            public bool Contains(Status.Item item)
            {
                //return _list.Contains(item);
                foreach (Status.Item m in _list)
                    if (m.Id.Equals(item.Id))
                        return true;
                return false;
            }

            public void CopyTo(Status.Item[] array, int arrayIndex)
            {
                _list.CopyTo(array, arrayIndex);
            }

            public int Count
            {
                get { return _list.Count; }
            }

            public bool IsReadOnly
            {
                get { throw new Exception("The method or operation is not implemented."); }
            }

            public bool Remove(Status.Item item)
            {
                return _list.Remove(item);
            }

            public void Add(Status.Item item)
            {
                _list.Add(item);
            }

            #endregion

            #region IEnumerable<Status.Item> Members

            IEnumerator<Status.Item> IEnumerable<Status.Item>.GetEnumerator()
            {
                return _list.GetEnumerator();
            }

            #endregion

            #region GenericComparer Members

            public enum SortOrderEnum
            {
                Ascending,
                Descending
            }

            public enum SortParamEnum
            {
                Id,
                Name,
                CreatedDate,
                UpdatedDate,
            }

            public class GenericComparer : IComparer<Status.Item>
            {
                private String _Property = null;
                private SortOrderEnum _SortOrder = SortOrderEnum.Ascending;

                public String SortProperty
                {
                    get { return _Property; }
                    set { _Property = value; }
                }

                public SortOrderEnum SortOrder
                {
                    get { return _SortOrder; }
                    set { _SortOrder = value; }
                }

                public int Compare(Status.Item x, Status.Item y)
                {
                    try
                    {
                        Status.Item ing1 = (Status.Item)x;
                        Status.Item ing2 = (Status.Item)y;

                        if (this.SortOrder.Equals(SortOrderEnum.Ascending))
                            return ing1.CompareTo(ing2, this.SortProperty);
                        else
                            return ing2.CompareTo(ing1, this.SortProperty);
                    }
                    catch (Exception ex)
                    {
                        return 0;
                    }
                }
            }


            public void Sort(SortParamEnum SortBy, SortOrderEnum SortOrder)
            {
                GenericComparer comparer = new GenericComparer();
                comparer.SortProperty = SortBy.ToString();
                comparer.SortOrder = SortOrder;
                _list.Sort((IComparer<Status.Item>)comparer);
            }

            #endregion GenericComparer Members

            public class ItemDefinition : DomainObject, IDomainObject, IDisposable
            {
                private bool _disposed = false;
                private StatusModel.ItemModel _details = null;

                protected StatusModel.ItemModel Details
                {
                    get { return _details; }
                    set { _details = value; }
                }

                public ItemDefinition()
                    : base(DomainObjectState.New)
                {
                    _details = new StatusModel.ItemModel(this);
                }

                public ItemDefinition(Int32 Id)
                    : base(DomainObjectState.Clean)
                {
                    DataFields["Id"] = Id;
                    _details = new StatusModel.ItemModel(this);
                }

                ~ItemDefinition() { Dispose(false); }

                public void Dispose() { Dispose(true); GC.SuppressFinalize(this); }

                protected virtual void Dispose(bool disposing)
                {
                    if (!_disposed)
                    {
                        if (disposing)
                        {
                            _details = null;
                            DataFields = null;
                            //base.Dispose(true);
                        }
                    }
                    _disposed = true;
                }

                #region IDomainObject Members

                void IDomainObject.CreateEntity()
                {
                    LiquidCore.Data.StatusData.Create(Details);
                }

                void IDomainObject.LoadEntity(ModelObject Details)
                {
                    LiquidCore.Data.StatusData.Load(Details, DataFields);
                }

                void IDomainObject.UpdateEntity()
                {
                    LiquidCore.Data.StatusData.Update(Details);
                }

                void IDomainObject.DeleteEntity()
                {
                    LiquidCore.Data.StatusData.Delete(Details);
                }

                #endregion

                #region GenericComparer Members
                public int CompareTo(object obj, string Property)
                {
                    try
                    {
                        Type type = this.GetType();
                        PropertyInfo propertie = type.GetProperty(Property);


                        Type type2 = obj.GetType();
                        PropertyInfo propertie2 = type2.GetProperty(Property);

                        object[] index = null;

                        object Obj1 = propertie.GetValue(this, index);
                        object Obj2 = propertie2.GetValue(obj, index);

                        IComparable Ic1 = (IComparable)Obj1;
                        IComparable Ic2 = (IComparable)Obj2;

                        int ret = Ic1.CompareTo(Ic2);

                        return ret;

                    }
                    catch (Exception ex)
                    {
                        return 0;
                    }
                }
                #endregion GenericComparer Members

            }
        }
    }
}

// ModelerLayer
namespace LiquidCore
{
    namespace LiquidCore.Modeler
    {
        using CoreLib;
        using LiquidCore.Definition;
        public class SitesModel : ModelObject
        {
            private Wrap<List<Site>> _sites = new Wrap<List<Site>>();

            public SitesModel(DomainObject container)
                : base(container)
            {
                _sites.ValueUpdated += new Wrap<List<Site>>.ValueUpdateEventHandler(NotifyContainer);
            }
        }
        public class SiteModel : ModelObject
        {
            private Wrap<int> _id = new Wrap<int>();
            private Wrap<int> _sta_id = new Wrap<int>();
            private Wrap<int> _lng_id = new Wrap<int>();
            private Wrap<int> _parentid = new Wrap<int>();
            private Wrap<int> _order = new Wrap<int>();
            private Wrap<string> _title = new Wrap<string>();
            private Wrap<string> _alias = new Wrap<string>();
            private Wrap<string> _description = new Wrap<string>();
            private Wrap<int[]> _authorizedroles = new Wrap<int[]>();
            private Wrap<string> _theme = new Wrap<string>();
            private Wrap<string> _structure = new Wrap<string>();
            private Wrap<DateTime> _createddate = new Wrap<DateTime>();
            private Wrap<string> _createdby = new Wrap<string>();
            private Wrap<DateTime> _updateddate = new Wrap<DateTime>();
            private Wrap<string> _updatedby = new Wrap<string>();
            private Wrap<bool> _hidden = new Wrap<bool>();
            private Wrap<bool> _deleted = new Wrap<bool>();

            public int Id
            {
                get { return _id.Value; }
                set { _id.Value = value; }
            }
            public int Status
            {
                get { return _sta_id.Value; }
                set { _sta_id.Value = value; }
            }
            public int Language
            {
                get { return _lng_id.Value; }
                set { _lng_id.Value = value; }
            }
            public int ParentId
            {
                get { return _parentid.Value; }
                set { _parentid.Value = value; }
            }
            public int Order
            {
                get { return _order.Value; }
                set { _order.Value = value; }
            }
            public string Title
            {
                get { return _title.Value; }
                set { _title.Value = value; }
            }
            public string Alias
            {
                get { return _alias.Value; }
                set { _alias.Value = value; }
            }
            public string Description
            {
                get { return _description.Value; }
                set { _description.Value = value; }
            }
            public int[] AuthorizedRoles
            {
                get { return _authorizedroles.Value; }
                set { _authorizedroles.Value = value; }
            }
            public string Theme
            {
                get { return _theme.Value; }
                set { _theme.Value = value; }
            }
            public string Structure
            {
                get { return _structure.Value; }
                set { _structure.Value = value; }
            }
            public DateTime CreatedDate
            {
                get { return _createddate.Value; }
                set { _createddate.Value = value; }
            }
            public string CreatedBy
            {
                get { return _createdby.Value; }
                set { _createdby.Value = value; }
            }
            public DateTime UpdatedDate
            {
                get { return _updateddate.Value; }
                set { _updateddate.Value = value; }
            }
            public string UpdatedBy
            {
                get { return _updatedby.Value; }
                set { _updatedby.Value = value; }
            }
            public bool Hidden
            {
                get { return _hidden.Value; }
                set { _hidden.Value = value; }
            }
            public bool Deleted
            {
                get { return _deleted.Value; }
                set { _deleted.Value = value; }
            }

            public SiteModel(DomainObject container)
                : base(container)
            {
                _id.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                _sta_id.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                _lng_id.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                _parentid.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                _order.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                _title.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _alias.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _description.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _authorizedroles.ValueUpdated += new Wrap<int[]>.ValueUpdateEventHandler(NotifyContainer);
                _theme.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _structure.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _createddate.ValueUpdated += new Wrap<DateTime>.ValueUpdateEventHandler(NotifyContainer);
                _createdby.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _updateddate.ValueUpdated += new Wrap<DateTime>.ValueUpdateEventHandler(NotifyContainer);
                _updatedby.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _hidden.ValueUpdated += new Wrap<bool>.ValueUpdateEventHandler(NotifyContainer);
                _deleted.ValueUpdated += new Wrap<bool>.ValueUpdateEventHandler(NotifyContainer);
            }
        }
        public class SettingsModel : ModelObject
        {
            private Wrap<List<Setting>> _settings = new Wrap<List<Setting>>();

            public SettingsModel(DomainObject container)
                : base(container)
            {
                _settings.ValueUpdated += new Wrap<List<Setting>>.ValueUpdateEventHandler(NotifyContainer);
            }
        }
        public class SettingModel : ModelObject
        {
            private Wrap<int> _id = new Wrap<int>();
            private Wrap<int> _pointer = new Wrap<int>();
            private Wrap<int> _sta_id = new Wrap<int>();
            private Wrap<int> _lng_id = new Wrap<int>();
            private Wrap<int> _sit_id = new Wrap<int>();
            private Wrap<int> _pag_id = new Wrap<int>();
            private Wrap<int> _mod_id = new Wrap<int>();
            private Wrap<int> _parentid = new Wrap<int>();
            private Wrap<int> _order = new Wrap<int>();
            private Wrap<string> _title = new Wrap<string>();
            private Wrap<string> _alias = new Wrap<string>();
            private Wrap<string> _description = new Wrap<string>();
            private Wrap<string> _value = new Wrap<string>();
            private Wrap<DateTime> _createddate = new Wrap<DateTime>();
            private Wrap<string> _createdby = new Wrap<string>();
            private Wrap<DateTime> _updateddate = new Wrap<DateTime>();
            private Wrap<string> _updatedby = new Wrap<string>();
            private Wrap<bool> _hidden = new Wrap<bool>();
            private Wrap<bool> _deleted = new Wrap<bool>();

            public int Id
            {
                get { return _id.Value; }
                set { _id.Value = value; }
            }
            public int Pointer
            {
                get { return _pointer.Value; }
                set { _pointer.Value = value; }
            }
            public int Status
            {
                get { return _sta_id.Value; }
                set { _sta_id.Value = value; }
            }
            public int Language
            {
                get { return _lng_id.Value; }
                set { _lng_id.Value = value; }
            }
            public int SitId
            {
                get { return _sit_id.Value; }
                set { _sit_id.Value = value; }
            }
            public int PagId
            {
                get { return _pag_id.Value; }
                set { _pag_id.Value = value; }
            }
            public int ModId
            {
                get { return _mod_id.Value; }
                set { _mod_id.Value = value; }
            }
            public int ParentId
            {
                get { return _parentid.Value; }
                set { _parentid.Value = value; }
            }
            public int Order
            {
                get { return _order.Value; }
                set { _order.Value = value; }
            }
            public string Title
            {
                get { return _title.Value; }
                set { _title.Value = value; }
            }
            public string Alias
            {
                get { return _alias.Value; }
                set { _alias.Value = value; }
            }
            public string Description
            {
                get { return _description.Value; }
                set { _description.Value = value; }
            }
            public string Value
            {
                get { return _value.Value; }
                set { _value.Value = value; }
            }
            public DateTime CreatedDate
            {
                get { return _createddate.Value; }
                set { _createddate.Value = value; }
            }
            public string CreatedBy
            {
                get { return _createdby.Value; }
                set { _createdby.Value = value; }
            }
            public DateTime UpdatedDate
            {
                get { return _updateddate.Value; }
                set { _updateddate.Value = value; }
            }
            public string UpdatedBy
            {
                get { return _updatedby.Value; }
                set { _updatedby.Value = value; }
            }
            public bool Hidden
            {
                get { return _hidden.Value; }
                set { _hidden.Value = value; }
            }
            public bool Deleted
            {
                get { return _deleted.Value; }
                set { _deleted.Value = value; }
            }

            public SettingModel(DomainObject container)
                : base(container)
            {
                _id.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                _pointer.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                _sta_id.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                _lng_id.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                _sit_id.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                _pag_id.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                _mod_id.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                _parentid.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                _order.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                _title.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _alias.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _description.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _value.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _createddate.ValueUpdated += new Wrap<DateTime>.ValueUpdateEventHandler(NotifyContainer);
                _createdby.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _updateddate.ValueUpdated += new Wrap<DateTime>.ValueUpdateEventHandler(NotifyContainer);
                _updatedby.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _hidden.ValueUpdated += new Wrap<bool>.ValueUpdateEventHandler(NotifyContainer);
                _deleted.ValueUpdated += new Wrap<bool>.ValueUpdateEventHandler(NotifyContainer);
            }
        }
        public class PagesModel : ModelObject
        {
            private Wrap<List<Page>> _list = new Wrap<List<Page>>();

            public PagesModel(DomainObject container)
                : base(container)
            {
                _list.ValueUpdated += new Wrap<List<Page>>.ValueUpdateEventHandler(NotifyContainer);
            }
        }
        public class PageModel : ModelObject
        {
            private Wrap<int> _id = new Wrap<int>();
            private Wrap<int> _sit_id = new Wrap<int>();
            private Wrap<int> _sta_id = new Wrap<int>();
            private Wrap<int> _lng_id = new Wrap<int>();
            private Wrap<int> _modelid = new Wrap<int>();
            private Wrap<int> _parentid = new Wrap<int>();
            private Wrap<int> _order = new Wrap<int>();
            private Wrap<string> _title = new Wrap<string>();
            private Wrap<string> _alias = new Wrap<string>();
            private Wrap<string> _description = new Wrap<string>();
            private Wrap<int[]> _authorizedroles = new Wrap<int[]>();
            private Wrap<string> _template = new Wrap<string>();
            private Wrap<DateTime> _createddate = new Wrap<DateTime>();
            private Wrap<string> _createdby = new Wrap<string>();
            private Wrap<DateTime> _updateddate = new Wrap<DateTime>();
            private Wrap<string> _updatedby = new Wrap<string>();
            private Wrap<bool> _hidden = new Wrap<bool>();
            private Wrap<bool> _deleted = new Wrap<bool>();

            public int Id
            {
                get { return _id.Value; }
                set { _id.Value = value; }
            }
            public int SitId
            {
                get { return _sit_id.Value; }
                set { _sit_id.Value = value; }
            }
            public int Status
            {
                get { return _sta_id.Value; }
                set { _sta_id.Value = value; }
            }
            public int Language
            {
                get { return _lng_id.Value; }
                set { _lng_id.Value = value; }
            }
            public int ModelId
            {
                get { return _modelid.Value; }
                set { _modelid.Value = value; }
            }
            public int ParentId
            {
                get { return _parentid.Value; }
                set { _parentid.Value = value; }
            }
            public int Order
            {
                get { return _order.Value; }
                set { _order.Value = value; }
            }
            public string Title
            {
                get { return _title.Value; }
                set { _title.Value = value; }
            }
            public string Alias
            {
                get { return _alias.Value; }
                set { _alias.Value = value; }
            }
            public string Description
            {
                get { return _description.Value; }
                set { _description.Value = value; }
            }
            public int[] AuthorizedRoles
            {
                get { return _authorizedroles.Value; }
                set { _authorizedroles.Value = value; }
            }
            public string Template
            {
                get { return _template.Value; }
                set { _template.Value = value; }
            }
            public DateTime CreatedDate
            {
                get { return _createddate.Value; }
                set { _createddate.Value = value; }
            }
            public string CreatedBy
            {
                get { return _createdby.Value; }
                set { _createdby.Value = value; }
            }
            public DateTime UpdatedDate
            {
                get { return _updateddate.Value; }
                set { _updateddate.Value = value; }
            }
            public string UpdatedBy
            {
                get { return _updatedby.Value; }
                set { _updatedby.Value = value; }
            }
            public bool Hidden
            {
                get { return _hidden.Value; }
                set { _hidden.Value = value; }
            }
            public bool Deleted
            {
                get { return _deleted.Value; }
                set { _deleted.Value = value; }
            }

            public PageModel(DomainObject container)
                : base(container)
            {
                _id.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                _sit_id.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                _sta_id.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                _lng_id.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                _modelid.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                _parentid.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                _order.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                _title.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _alias.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _description.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _authorizedroles.ValueUpdated += new Wrap<int[]>.ValueUpdateEventHandler(NotifyContainer);
                _template.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _createddate.ValueUpdated += new Wrap<DateTime>.ValueUpdateEventHandler(NotifyContainer);
                _createdby.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _updateddate.ValueUpdated += new Wrap<DateTime>.ValueUpdateEventHandler(NotifyContainer);
                _updatedby.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _hidden.ValueUpdated += new Wrap<bool>.ValueUpdateEventHandler(NotifyContainer);
                _deleted.ValueUpdated += new Wrap<bool>.ValueUpdateEventHandler(NotifyContainer);
            }
        }
        public class ModulesModel : ModelObject
        {
            private Wrap<List<Module>> _list = new Wrap<List<Module>>();

            public ModulesModel(DomainObject container)
                : base(container)
            {
                _list.ValueUpdated += new Wrap<List<Module>>.ValueUpdateEventHandler(NotifyContainer);
            }
        }
        public class ModuleModel : ModelObject
        {
            private Wrap<int> _id = new Wrap<int>();
            private Wrap<int> _sit_id = new Wrap<int>();
            private Wrap<int> _pag_id = new Wrap<int>();
            private Wrap<int> _mde_id = new Wrap<int>();
            private Wrap<int> _sta_id = new Wrap<int>();
            private Wrap<int> _lng_id = new Wrap<int>();
            private Wrap<int> _parentid = new Wrap<int>();
            private Wrap<int> _order = new Wrap<int>();
            private Wrap<int> _mobileorder = new Wrap<int>();
			private Wrap<int> _revision = new Wrap<int>();
            private Wrap<string> _title = new Wrap<string>();
            private Wrap<string> _alias = new Wrap<string>();
            private Wrap<string> _description = new Wrap<string>();
            private Wrap<int[]> _authorizedroles = new Wrap<int[]>();
            private Wrap<string> _src = new Wrap<string>();
            private Wrap<string> _contentpane = new Wrap<string>();
            private Wrap<bool> _allpages = new Wrap<bool>();
            private Wrap<bool> _ssl = new Wrap<bool>();
            private Wrap<int> _cachetime = new Wrap<int>();
            private Wrap<DateTime> _createddate = new Wrap<DateTime>();
            private Wrap<string> _createdby = new Wrap<string>();
            private Wrap<DateTime> _updateddate = new Wrap<DateTime>();
            private Wrap<string> _updatedby = new Wrap<string>();
            private Wrap<bool> _hidden = new Wrap<bool>();
            private Wrap<bool> _mobilehidden = new Wrap<bool>();
            private Wrap<bool> _deleted = new Wrap<bool>();

            public int Id
            {
                get { return _id.Value; }
                set { _id.Value = value; }
            }
            public int SitId
            {
                get { return _sit_id.Value; }
                set { _sit_id.Value = value; }
            }
            public int PagId
            {
                get { return _pag_id.Value; }
                set { _pag_id.Value = value; }
            }
            public int MdeId
            {
                get { return _mde_id.Value; }
                set { _mde_id.Value = value; }
            }
            public int Status
            {
                get { return _sta_id.Value; }
                set { _sta_id.Value = value; }
            }
            public int Language
            {
                get { return _lng_id.Value; }
                set { _lng_id.Value = value; }
            }
            public int ParentId
            {
                get { return _parentid.Value; }
                set { _parentid.Value = value; }
            }
            public int Order
            {
                get { return _order.Value; }
                set { _order.Value = value; }
            }
            public int MobileOrder
            {
                get { return _mobileorder.Value; }
                set { _mobileorder.Value = value; }
            }
			public int Revision
			{
				get { return _revision.Value; }
				set { _revision.Value = value; }
			}
            public string Title
            {
                get { return _title.Value; }
                set { _title.Value = value; }
            }
            public string Alias
            {
                get { return _alias.Value; }
                set { _alias.Value = value; }
            }
            public string Description
            {
                get { return _description.Value; }
                set { _description.Value = value; }
            }
            public int[] AuthorizedRoles
            {
                get { return _authorizedroles.Value; }
                set { _authorizedroles.Value = value; }
            }
            public string Src
            {
                get { return _src.Value; }
                set { _src.Value = value; }
            }
            public string ContentPane
            {
                get { return _contentpane.Value; }
                set { _contentpane.Value = value; }
            }
            public bool AllPages
            {
                get { return _allpages.Value; }
                set { _allpages.Value = value; }
            }
            public bool SSL
            {
                get { return _ssl.Value; }
                set { _ssl.Value = value; }
            }
            public int CacheTime
            {
                get { return _cachetime.Value; }
                set { _cachetime.Value = value; }
            }
            public DateTime CreatedDate
            {
                get { return _createddate.Value; }
                set { _createddate.Value = value; }
            }
            public string CreatedBy
            {
                get { return _createdby.Value; }
                set { _createdby.Value = value; }
            }
            public DateTime UpdatedDate
            {
                get { return _updateddate.Value; }
                set { _updateddate.Value = value; }
            }
            public string UpdatedBy
            {
                get { return _updatedby.Value; }
                set { _updatedby.Value = value; }
            }
            public bool Hidden
            {
                get { return _hidden.Value; }
                set { _hidden.Value = value; }
            }
            public bool MobileHidden
            {
                get { return _mobilehidden.Value; }
                set { _mobilehidden.Value = value; }
            }
            public bool Deleted
            {
                get { return _deleted.Value; }
                set { _deleted.Value = value; }
            }

            public ModuleModel(DomainObject container)
                : base(container)
            {
                _id.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                _sit_id.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                _pag_id.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                _mde_id.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                _sta_id.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                _lng_id.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                _parentid.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                _order.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                _mobileorder.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
				_revision.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                _title.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _alias.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _description.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _authorizedroles.ValueUpdated += new Wrap<int[]>.ValueUpdateEventHandler(NotifyContainer);
                _src.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _contentpane.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _allpages.ValueUpdated += new Wrap<bool>.ValueUpdateEventHandler(NotifyContainer);
                _ssl.ValueUpdated += new Wrap<bool>.ValueUpdateEventHandler(NotifyContainer);
                _cachetime.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                _createddate.ValueUpdated += new Wrap<DateTime>.ValueUpdateEventHandler(NotifyContainer);
                _createdby.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _updateddate.ValueUpdated += new Wrap<DateTime>.ValueUpdateEventHandler(NotifyContainer);
                _updatedby.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _hidden.ValueUpdated += new Wrap<bool>.ValueUpdateEventHandler(NotifyContainer);
                _mobilehidden.ValueUpdated += new Wrap<bool>.ValueUpdateEventHandler(NotifyContainer);
                _deleted.ValueUpdated += new Wrap<bool>.ValueUpdateEventHandler(NotifyContainer);
            }
        }
        public class ObjectsModel : ModelObject
        {
            private Wrap<List<Objects.Item>> _list = new Wrap<List<Objects.Item>>();

            public ObjectsModel(DomainObject container)
                : base(container)
            {
                _list.ValueUpdated += new Wrap<List<Objects.Item>>.ValueUpdateEventHandler(NotifyContainer);
            }
        }
        public class ObjectModel : ModelObject
        {
            private Wrap<int> _id = new Wrap<int>();
            private Wrap<int> _sit_id = new Wrap<int>();
            private Wrap<int> _pag_id = new Wrap<int>();
            private Wrap<int> _mod_id = new Wrap<int>();
            private Wrap<int> _sta_id = new Wrap<int>();
            private Wrap<int> _lng_id = new Wrap<int>();
            private Wrap<int> _parentid = new Wrap<int>();
            private Wrap<int> _order = new Wrap<int>();
            private Wrap<int> _type = new Wrap<int>();
            private Wrap<string> _title = new Wrap<string>();
            private Wrap<string> _alias = new Wrap<string>();
            private Wrap<string> _description = new Wrap<string>();
            private Wrap<string> _value1 = new Wrap<string>();
            private Wrap<string> _value2 = new Wrap<string>();
            private Wrap<string> _value3 = new Wrap<string>();
            private Wrap<string> _value4 = new Wrap<string>();
            private Wrap<string> _value5 = new Wrap<string>();
            private Wrap<string> _value6 = new Wrap<string>();
            private Wrap<string> _value7 = new Wrap<string>();
            private Wrap<string> _value8 = new Wrap<string>();
            private Wrap<string> _value9 = new Wrap<string>();
            private Wrap<string> _value10 = new Wrap<string>();
            private Wrap<string> _value11 = new Wrap<string>();
            private Wrap<string> _value12 = new Wrap<string>();
            private Wrap<string> _value13 = new Wrap<string>();
            private Wrap<string> _value14 = new Wrap<string>();
            private Wrap<string> _value15 = new Wrap<string>();
            private Wrap<string> _value16 = new Wrap<string>();
            private Wrap<string> _value17 = new Wrap<string>();
            private Wrap<string> _value18 = new Wrap<string>();
            private Wrap<string> _value19 = new Wrap<string>();
            private Wrap<string> _value20 = new Wrap<string>();
            private Wrap<string> _value21 = new Wrap<string>();
            private Wrap<string> _value22 = new Wrap<string>();
            private Wrap<string> _value23 = new Wrap<string>();
            private Wrap<string> _value24 = new Wrap<string>();
            private Wrap<string> _value25 = new Wrap<string>();
            private Wrap<string> _value26 = new Wrap<string>();
            private Wrap<string> _value27 = new Wrap<string>();
            private Wrap<string> _value28 = new Wrap<string>();
            private Wrap<string> _value29 = new Wrap<string>();
            private Wrap<string> _value30 = new Wrap<string>();
            private Wrap<string> _value31 = new Wrap<string>();
            private Wrap<string> _value32 = new Wrap<string>();
            private Wrap<string> _value33 = new Wrap<string>();
            private Wrap<string> _value34 = new Wrap<string>();
            private Wrap<string> _value35 = new Wrap<string>();
            private Wrap<string> _value36 = new Wrap<string>();
            private Wrap<string> _value37 = new Wrap<string>();
            private Wrap<string> _value38 = new Wrap<string>();
            private Wrap<string> _value39 = new Wrap<string>();
            private Wrap<string> _value40 = new Wrap<string>();
            private Wrap<string> _value41 = new Wrap<string>();
            private Wrap<string> _value42 = new Wrap<string>();
            private Wrap<string> _value43 = new Wrap<string>();
            private Wrap<string> _value44 = new Wrap<string>();
            private Wrap<string> _value45 = new Wrap<string>();
            private Wrap<string> _value46 = new Wrap<string>();
            private Wrap<string> _value47 = new Wrap<string>();
            private Wrap<string> _value48 = new Wrap<string>();
            private Wrap<string> _value49 = new Wrap<string>();
            private Wrap<string> _value50 = new Wrap<string>();
            private Wrap<DateTime> _createddate = new Wrap<DateTime>();
            private Wrap<string> _createdby = new Wrap<string>();
            private Wrap<DateTime> _updateddate = new Wrap<DateTime>();
            private Wrap<string> _updatedby = new Wrap<string>();
            private Wrap<bool> _hidden = new Wrap<bool>();
            private Wrap<bool> _deleted = new Wrap<bool>();

            public int Id
            {
                get { return _id.Value; }
                set { _id.Value = value; }
            }
            public int SitId
            {
                get { return _sit_id.Value; }
                set { _sit_id.Value = value; }
            }
            public int PagId
            {
                get { return _pag_id.Value; }
                set { _pag_id.Value = value; }
            }
            public int ModId
            {
                get { return _mod_id.Value; }
                set { _mod_id.Value = value; }
            }
            public int Status
            {
                get { return _sta_id.Value; }
                set { _sta_id.Value = value; }
            }
            public int Language
            {
                get { return _lng_id.Value; }
                set { _lng_id.Value = value; }
            }
            public int ParentId
            {
                get { return _parentid.Value; }
                set { _parentid.Value = value; }
            }
            public int Order
            {
                get { return _order.Value; }
                set { _order.Value = value; }
            }
            public int Type
            {
                get { return _type.Value; }
                set { _type.Value = value; }
            }
            public string Title
            {
                get { return _title.Value; }
                set { _title.Value = value; }
            }
            public string Alias
            {
                get { return _alias.Value; }
                set { _alias.Value = value; }
            }
            public string Description
            {
                get { return _description.Value; }
                set { _description.Value = value; }
            }
            public string Value1
            {
                get { return _value1.Value; }
                set { _value1.Value = value; }
            }
            public string Value2
            {
                get { return _value2.Value; }
                set { _value2.Value = value; }
            }
            public string Value3
            {
                get { return _value3.Value; }
                set { _value3.Value = value; }
            }
            public string Value4
            {
                get { return _value4.Value; }
                set { _value4.Value = value; }
            }
            public string Value5
            {
                get { return _value5.Value; }
                set { _value5.Value = value; }
            }
            public string Value6
            {
                get { return _value6.Value; }
                set { _value6.Value = value; }
            }
            public string Value7
            {
                get { return _value7.Value; }
                set { _value7.Value = value; }
            }
            public string Value8
            {
                get { return _value8.Value; }
                set { _value8.Value = value; }
            }
            public string Value9
            {
                get { return _value9.Value; }
                set { _value9.Value = value; }
            }
            public string Value10
            {
                get { return _value10.Value; }
                set { _value10.Value = value; }
            }
            public string Value11
            {
                get { return _value11.Value; }
                set { _value11.Value = value; }
            }
            public string Value12
            {
                get { return _value12.Value; }
                set { _value12.Value = value; }
            }
            public string Value13
            {
                get { return _value13.Value; }
                set { _value13.Value = value; }
            }
            public string Value14
            {
                get { return _value14.Value; }
                set { _value14.Value = value; }
            }
            public string Value15
            {
                get { return _value15.Value; }
                set { _value15.Value = value; }
            }
            public string Value16
            {
                get { return _value16.Value; }
                set { _value16.Value = value; }
            }
            public string Value17
            {
                get { return _value17.Value; }
                set { _value17.Value = value; }
            }
            public string Value18
            {
                get { return _value18.Value; }
                set { _value18.Value = value; }
            }
            public string Value19
            {
                get { return _value19.Value; }
                set { _value19.Value = value; }
            }
            public string Value20
            {
                get { return _value20.Value; }
                set { _value20.Value = value; }
            }
            public string Value21
            {
                get { return _value21.Value; }
                set { _value21.Value = value; }
            }
            public string Value22
            {
                get { return _value22.Value; }
                set { _value22.Value = value; }
            }
            public string Value23
            {
                get { return _value23.Value; }
                set { _value23.Value = value; }
            }
            public string Value24
            {
                get { return _value24.Value; }
                set { _value24.Value = value; }
            }
            public string Value25
            {
                get { return _value25.Value; }
                set { _value25.Value = value; }
            }
            public string Value26
            {
                get { return _value26.Value; }
                set { _value26.Value = value; }
            }
            public string Value27
            {
                get { return _value27.Value; }
                set { _value27.Value = value; }
            }
            public string Value28
            {
                get { return _value28.Value; }
                set { _value28.Value = value; }
            }
            public string Value29
            {
                get { return _value29.Value; }
                set { _value29.Value = value; }
            }
            public string Value30
            {
                get { return _value30.Value; }
                set { _value30.Value = value; }
            }
            public string Value31
            {
                get { return _value31.Value; }
                set { _value31.Value = value; }
            }
            public string Value32
            {
                get { return _value32.Value; }
                set { _value32.Value = value; }
            }
            public string Value33
            {
                get { return _value33.Value; }
                set { _value33.Value = value; }
            }
            public string Value34
            {
                get { return _value34.Value; }
                set { _value34.Value = value; }
            }
            public string Value35
            {
                get { return _value35.Value; }
                set { _value35.Value = value; }
            }
            public string Value36
            {
                get { return _value36.Value; }
                set { _value36.Value = value; }
            }
            public string Value37
            {
                get { return _value37.Value; }
                set { _value37.Value = value; }
            }
            public string Value38
            {
                get { return _value38.Value; }
                set { _value38.Value = value; }
            }
            public string Value39
            {
                get { return _value39.Value; }
                set { _value39.Value = value; }
            }
            public string Value40
            {
                get { return _value40.Value; }
                set { _value40.Value = value; }
            }
            public string Value41
            {
                get { return _value41.Value; }
                set { _value41.Value = value; }
            }
            public string Value42
            {
                get { return _value42.Value; }
                set { _value42.Value = value; }
            }
            public string Value43
            {
                get { return _value43.Value; }
                set { _value43.Value = value; }
            }
            public string Value44
            {
                get { return _value44.Value; }
                set { _value44.Value = value; }
            }
            public string Value45
            {
                get { return _value45.Value; }
                set { _value45.Value = value; }
            }
            public string Value46
            {
                get { return _value46.Value; }
                set { _value46.Value = value; }
            }
            public string Value47
            {
                get { return _value47.Value; }
                set { _value47.Value = value; }
            }
            public string Value48
            {
                get { return _value48.Value; }
                set { _value48.Value = value; }
            }
            public string Value49
            {
                get { return _value49.Value; }
                set { _value49.Value = value; }
            }
            public string Value50
            {
                get { return _value50.Value; }
                set { _value50.Value = value; }
            }
            public DateTime CreatedDate
            {
                get { return _createddate.Value; }
                set { _createddate.Value = value; }
            }
            public string CreatedBy
            {
                get { return _createdby.Value; }
                set { _createdby.Value = value; }
            }
            public DateTime UpdatedDate
            {
                get { return _updateddate.Value; }
                set { _updateddate.Value = value; }
            }
            public string UpdatedBy
            {
                get { return _updatedby.Value; }
                set { _updatedby.Value = value; }
            }
            public bool Hidden
            {
                get { return _hidden.Value; }
                set { _hidden.Value = value; }
            }
            public bool Deleted
            {
                get { return _deleted.Value; }
                set { _deleted.Value = value; }
            }

            public ObjectModel(DomainObject container)
                : base(container)
            {
                _id.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                _sit_id.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                _pag_id.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                _mod_id.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                _sta_id.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                _lng_id.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                _parentid.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                _order.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                _type.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                _title.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _alias.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _description.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _value1.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _value2.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _value3.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _value4.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _value5.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _value6.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _value7.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _value8.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _value9.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _value10.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _value11.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _value12.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _value13.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _value14.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _value15.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _value16.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _value17.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _value18.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _value19.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _value20.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _value21.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _value22.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _value23.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _value24.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _value25.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _value26.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _value27.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _value28.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _value29.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _value30.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _value31.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _value32.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _value33.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _value34.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _value35.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _value36.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _value37.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _value38.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _value39.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _value40.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _value41.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _value42.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _value43.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _value44.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _value45.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _value46.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _value47.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _value48.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _value49.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _value50.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _createddate.ValueUpdated += new Wrap<DateTime>.ValueUpdateEventHandler(NotifyContainer);
                _createdby.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _updateddate.ValueUpdated += new Wrap<DateTime>.ValueUpdateEventHandler(NotifyContainer);
                _updatedby.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _hidden.ValueUpdated += new Wrap<bool>.ValueUpdateEventHandler(NotifyContainer);
                _deleted.ValueUpdated += new Wrap<bool>.ValueUpdateEventHandler(NotifyContainer);
            }
        }
        public class ModelsModel : ModelObject
        {
            private Wrap<List<Model>> _list = new Wrap<List<Model>>();

            public ModelsModel(DomainObject container)
                : base(container)
            {
                _list.ValueUpdated += new Wrap<List<Model>>.ValueUpdateEventHandler(NotifyContainer);
            }
        }
        public class ModelModel : ModelObject
        {
            private Wrap<int> _id = new Wrap<int>();
            private Wrap<int> _sit_id = new Wrap<int>();
            private Wrap<int> _sta_id = new Wrap<int>();
            private Wrap<int> _lng_id = new Wrap<int>();
            private Wrap<int> _parentid = new Wrap<int>();
            private Wrap<int> _order = new Wrap<int>();
            private Wrap<string> _title = new Wrap<string>();
            private Wrap<string> _alias = new Wrap<string>();
            private Wrap<string> _description = new Wrap<string>();
            private Wrap<DateTime> _createddate = new Wrap<DateTime>();
            private Wrap<string> _createdby = new Wrap<string>();
            private Wrap<DateTime> _updateddate = new Wrap<DateTime>();
            private Wrap<string> _updatedby = new Wrap<string>();
            private Wrap<bool> _hidden = new Wrap<bool>();
            private Wrap<bool> _deleted = new Wrap<bool>();

            public int Id
            {
                get { return _id.Value; }
                set { _id.Value = value; }
            }
            public int SitId
            {
                get { return _sit_id.Value; }
                set { _sit_id.Value = value; }
            }
            public int Status
            {
                get { return _sta_id.Value; }
                set { _sta_id.Value = value; }
            }
            public int Language
            {
                get { return _lng_id.Value; }
                set { _lng_id.Value = value; }
            }
            public int ParentId
            {
                get { return _parentid.Value; }
                set { _parentid.Value = value; }
            }
            public int Order
            {
                get { return _order.Value; }
                set { _order.Value = value; }
            }
            public string Title
            {
                get { return _title.Value; }
                set { _title.Value = value; }
            }
            public string Alias
            {
                get { return _alias.Value; }
                set { _alias.Value = value; }
            }
            public string Description
            {
                get { return _description.Value; }
                set { _description.Value = value; }
            }
            public DateTime CreatedDate
            {
                get { return _createddate.Value; }
                set { _createddate.Value = value; }
            }
            public string CreatedBy
            {
                get { return _createdby.Value; }
                set { _createdby.Value = value; }
            }
            public DateTime UpdatedDate
            {
                get { return _updateddate.Value; }
                set { _updateddate.Value = value; }
            }
            public string UpdatedBy
            {
                get { return _updatedby.Value; }
                set { _updatedby.Value = value; }
            }
            public bool Hidden
            {
                get { return _hidden.Value; }
                set { _hidden.Value = value; }
            }
            public bool Deleted
            {
                get { return _deleted.Value; }
                set { _deleted.Value = value; }
            }

            public ModelModel(DomainObject container)
                : base(container)
            {
                _id.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                _sit_id.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                _sta_id.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                _lng_id.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                _parentid.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                _order.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                _title.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _alias.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _description.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _createddate.ValueUpdated += new Wrap<DateTime>.ValueUpdateEventHandler(NotifyContainer);
                _createdby.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _updateddate.ValueUpdated += new Wrap<DateTime>.ValueUpdateEventHandler(NotifyContainer);
                _updatedby.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _hidden.ValueUpdated += new Wrap<bool>.ValueUpdateEventHandler(NotifyContainer);
                _deleted.ValueUpdated += new Wrap<bool>.ValueUpdateEventHandler(NotifyContainer);
            }
        }
        public class ModelItemsModel : ModelObject
        {
            private Wrap<List<ModelItem>> _list = new Wrap<List<ModelItem>>();

            public ModelItemsModel(DomainObject container)
                : base(container)
            {
                _list.ValueUpdated += new Wrap<List<ModelItem>>.ValueUpdateEventHandler(NotifyContainer);
            }
        }
        public class ModelItemModel : ModelObject
        {
            private Wrap<int> _id = new Wrap<int>();
            private Wrap<int> _sit_id = new Wrap<int>();
            private Wrap<int> _mdl_id = new Wrap<int>();
            private Wrap<int> _mde_id = new Wrap<int>();
            private Wrap<int> _sta_id = new Wrap<int>();
            private Wrap<int> _lng_id = new Wrap<int>();
            private Wrap<int> _parentid = new Wrap<int>();
            private Wrap<int> _order = new Wrap<int>();
            private Wrap<string> _contentpane = new Wrap<string>();
            private Wrap<DateTime> _createddate = new Wrap<DateTime>();
            private Wrap<string> _createdby = new Wrap<string>();
            private Wrap<DateTime> _updateddate = new Wrap<DateTime>();
            private Wrap<string> _updatedby = new Wrap<string>();
            private Wrap<bool> _hidden = new Wrap<bool>();
            private Wrap<bool> _deleted = new Wrap<bool>();

            public int Id
            {
                get { return _id.Value; }
                set { _id.Value = value; }
            }
            public int SitId
            {
                get { return _sit_id.Value; }
                set { _sit_id.Value = value; }
            }
            public int MdlId
            {
                get { return _mdl_id.Value; }
                set { _mdl_id.Value = value; }
            }
            public int MdeId
            {
                get { return _mde_id.Value; }
                set { _mde_id.Value = value; }
            }
            public int Status
            {
                get { return _sta_id.Value; }
                set { _sta_id.Value = value; }
            }
            public int Language
            {
                get { return _lng_id.Value; }
                set { _lng_id.Value = value; }
            }
            public int ParentId
            {
                get { return _parentid.Value; }
                set { _parentid.Value = value; }
            }
            public int Order
            {
                get { return _order.Value; }
                set { _order.Value = value; }
            }
            public string ContentPane
            {
                get { return _contentpane.Value; }
                set { _contentpane.Value = value; }
            }
            public DateTime CreatedDate
            {
                get { return _createddate.Value; }
                set { _createddate.Value = value; }
            }
            public string CreatedBy
            {
                get { return _createdby.Value; }
                set { _createdby.Value = value; }
            }
            public DateTime UpdatedDate
            {
                get { return _updateddate.Value; }
                set { _updateddate.Value = value; }
            }
            public string UpdatedBy
            {
                get { return _updatedby.Value; }
                set { _updatedby.Value = value; }
            }
            public bool Hidden
            {
                get { return _hidden.Value; }
                set { _hidden.Value = value; }
            }
            public bool Deleted
            {
                get { return _deleted.Value; }
                set { _deleted.Value = value; }
            }

            public ModelItemModel(DomainObject container)
                : base(container)
            {
                _id.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                _sit_id.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                _mdl_id.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                _mde_id.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                _sta_id.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                _lng_id.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                _parentid.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                _order.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                _contentpane.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _createddate.ValueUpdated += new Wrap<DateTime>.ValueUpdateEventHandler(NotifyContainer);
                _createdby.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _updateddate.ValueUpdated += new Wrap<DateTime>.ValueUpdateEventHandler(NotifyContainer);
                _updatedby.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _hidden.ValueUpdated += new Wrap<bool>.ValueUpdateEventHandler(NotifyContainer);
                _deleted.ValueUpdated += new Wrap<bool>.ValueUpdateEventHandler(NotifyContainer);
            }
        }
        public class ModDefsModel : ModelObject
        {
            private Wrap<List<ModDef>> _list = new Wrap<List<ModDef>>();

            public ModDefsModel(DomainObject container)
                : base(container)
            {
                _list.ValueUpdated += new Wrap<List<ModDef>>.ValueUpdateEventHandler(NotifyContainer);
            }
        }
        public class ModDefModel : ModelObject
        {
            private Wrap<int> _id = new Wrap<int>();
            private Wrap<int> _sit_id = new Wrap<int>();
            private Wrap<int> _sta_id = new Wrap<int>();
            private Wrap<int> _lng_id = new Wrap<int>();
            private Wrap<int> _parentid = new Wrap<int>();
            private Wrap<int> _order = new Wrap<int>();
            private Wrap<string> _title = new Wrap<string>();
            private Wrap<string> _alias = new Wrap<string>();
            private Wrap<string> _description = new Wrap<string>();
            private Wrap<string> _src = new Wrap<string>();
            private Wrap<int> _cachetime = new Wrap<int>();
            private Wrap<string> _iconfile = new Wrap<string>();
            private Wrap<DateTime> _createddate = new Wrap<DateTime>();
            private Wrap<string> _createdby = new Wrap<string>();
            private Wrap<DateTime> _updateddate = new Wrap<DateTime>();
            private Wrap<string> _updatedby = new Wrap<string>();
            private Wrap<bool> _hidden = new Wrap<bool>();
            private Wrap<bool> _deleted = new Wrap<bool>();

            public int Id
            {
                get { return _id.Value; }
                set { _id.Value = value; }
            }
            public int SitId
            {
                get { return _sit_id.Value; }
                set { _sit_id.Value = value; }
            }
            public int Status
            {
                get { return _sta_id.Value; }
                set { _sta_id.Value = value; }
            }
            public int Language
            {
                get { return _lng_id.Value; }
                set { _lng_id.Value = value; }
            }
            public int ParentId
            {
                get { return _parentid.Value; }
                set { _parentid.Value = value; }
            }
            public int Order
            {
                get { return _order.Value; }
                set { _order.Value = value; }
            }
            public string Title
            {
                get { return _title.Value; }
                set { _title.Value = value; }
            }
            public string Alias
            {
                get { return _alias.Value; }
                set { _alias.Value = value; }
            }
            public string Description
            {
                get { return _description.Value; }
                set { _description.Value = value; }
            }
            public string Src
            {
                get { return _src.Value; }
                set { _src.Value = value; }
            }
            public int CacheTime
            {
                get { return _cachetime.Value; }
                set { _cachetime.Value = value; }
            }
            public string Iconfile
            {
                get { return _iconfile.Value; }
                set { _iconfile.Value = value; }
            }
            public DateTime CreatedDate
            {
                get { return _createddate.Value; }
                set { _createddate.Value = value; }
            }
            public string CreatedBy
            {
                get { return _createdby.Value; }
                set { _createdby.Value = value; }
            }
            public DateTime UpdatedDate
            {
                get { return _updateddate.Value; }
                set { _updateddate.Value = value; }
            }
            public string UpdatedBy
            {
                get { return _updatedby.Value; }
                set { _updatedby.Value = value; }
            }
            public bool Hidden
            {
                get { return _hidden.Value; }
                set { _hidden.Value = value; }
            }
            public bool Deleted
            {
                get { return _deleted.Value; }
                set { _deleted.Value = value; }
            }

            public ModDefModel(DomainObject container)
                : base(container)
            {
                _id.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                _sit_id.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                _sta_id.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                _lng_id.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                _parentid.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                _order.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                _title.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _alias.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _description.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _src.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _cachetime.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                _iconfile.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _createddate.ValueUpdated += new Wrap<DateTime>.ValueUpdateEventHandler(NotifyContainer);
                _createdby.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _updateddate.ValueUpdated += new Wrap<DateTime>.ValueUpdateEventHandler(NotifyContainer);
                _updatedby.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                _hidden.ValueUpdated += new Wrap<bool>.ValueUpdateEventHandler(NotifyContainer);
                _deleted.ValueUpdated += new Wrap<bool>.ValueUpdateEventHandler(NotifyContainer);
            }
        }

        public class MenuModel : ModelObject
        {
            private Wrap<List<Menu>> _list = new Wrap<List<Menu>>();

            public MenuModel(DomainObject container)
                : base(container)
            {
                _list.ValueUpdated += new Wrap<List<Menu>>.ValueUpdateEventHandler(NotifyContainer);
            }
            public class ItemModel : ModelObject
            {
                private Wrap<int> _id = new Wrap<int>();
                private Wrap<int> _sit_id = new Wrap<int>();
                private Wrap<int> _sta_id = new Wrap<int>();
                private Wrap<int> _lng_id = new Wrap<int>();
                private Wrap<int> _modelid = new Wrap<int>();
                private Wrap<int> _parentid = new Wrap<int>();
                private Wrap<int> _order = new Wrap<int>();
                private Wrap<string> _title = new Wrap<string>();
                private Wrap<string> _alias = new Wrap<string>();
                private Wrap<string> _description = new Wrap<string>();
                private Wrap<int[]> _authorizedroles = new Wrap<int[]>();
                private Wrap<int[]> _authorizededitroles = new Wrap<int[]>();
                private Wrap<string> _template = new Wrap<string>();
                private Wrap<DateTime> _createddate = new Wrap<DateTime>();
                private Wrap<string> _createdby = new Wrap<string>();
                private Wrap<DateTime> _updateddate = new Wrap<DateTime>();
                private Wrap<string> _updatedby = new Wrap<string>();
                private Wrap<bool> _hidden = new Wrap<bool>();
                private Wrap<bool> _mobilehidden = new Wrap<bool>();
                private Wrap<bool> _deleted = new Wrap<bool>();

                public int Id
                {
                    get { return _id.Value; }
                    set { _id.Value = value; }
                }
                public int SitId
                {
                    get { return _sit_id.Value; }
                    set { _sit_id.Value = value; }
                }
                public int Status
                {
                    get { return _sta_id.Value; }
                    set { _sta_id.Value = value; }
                }
                public int Language
                {
                    get { return _lng_id.Value; }
                    set { _lng_id.Value = value; }
                }
                public int ModelId
                {
                    get { return _modelid.Value; }
                    set { _modelid.Value = value; }
                }
                public int ParentId
                {
                    get { return _parentid.Value; }
                    set { _parentid.Value = value; }
                }
                public int Order
                {
                    get { return _order.Value; }
                    set { _order.Value = value; }
                }
                public string Title
                {
                    get { return _title.Value; }
                    set { _title.Value = value; }
                }
                public string Alias
                {
                    get { return _alias.Value; }
                    set { _alias.Value = value; }
                }
                public string Description
                {
                    get { return _description.Value; }
                    set { _description.Value = value; }
                }
                public int[] AuthorizedRoles
                {
                    get { return _authorizedroles.Value; }
                    set { _authorizedroles.Value = value; }
                }
                public int[] AuthorizedEditRoles
                {
                    get { return _authorizededitroles.Value; }
                    set { _authorizededitroles.Value = value; }
                }
                public string Template
                {
                    get { return _template.Value; }
                    set { _template.Value = value; }
                }
                public DateTime CreatedDate
                {
                    get { return _createddate.Value; }
                    set { _createddate.Value = value; }
                }
                public string CreatedBy
                {
                    get { return _createdby.Value; }
                    set { _createdby.Value = value; }
                }
                public DateTime UpdatedDate
                {
                    get { return _updateddate.Value; }
                    set { _updateddate.Value = value; }
                }
                public string UpdatedBy
                {
                    get { return _updatedby.Value; }
                    set { _updatedby.Value = value; }
                }
                public bool Hidden
                {
                    get { return _hidden.Value; }
                    set { _hidden.Value = value; }
                }
                public bool MobileHidden
                {
                    get { return _mobilehidden.Value; }
                    set { _mobilehidden.Value = value; }
                }
                public bool Deleted
                {
                    get { return _deleted.Value; }
                    set { _deleted.Value = value; }
                }

                public ItemModel(DomainObject container)
                    : base(container)
                {
                    _id.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                    _sit_id.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                    _sta_id.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                    _lng_id.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                    _modelid.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                    _parentid.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                    _order.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                    _title.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _alias.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _description.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _authorizedroles.ValueUpdated += new Wrap<int[]>.ValueUpdateEventHandler(NotifyContainer);
                    _authorizededitroles.ValueUpdated += new Wrap<int[]>.ValueUpdateEventHandler(NotifyContainer);
                    _template.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _createddate.ValueUpdated += new Wrap<DateTime>.ValueUpdateEventHandler(NotifyContainer);
                    _createdby.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _updateddate.ValueUpdated += new Wrap<DateTime>.ValueUpdateEventHandler(NotifyContainer);
                    _updatedby.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _hidden.ValueUpdated += new Wrap<bool>.ValueUpdateEventHandler(NotifyContainer);
                    _mobilehidden.ValueUpdated += new Wrap<bool>.ValueUpdateEventHandler(NotifyContainer);
                    _deleted.ValueUpdated += new Wrap<bool>.ValueUpdateEventHandler(NotifyContainer);
                }
            }
        }
        public class ListModel : ModelObject
        {
            private Wrap<List<List>> _list = new Wrap<List<List>>();

            public ListModel(DomainObject container)
                : base(container)
            {
                _list.ValueUpdated += new Wrap<List<List>>.ValueUpdateEventHandler(NotifyContainer);
            }
            public class ItemModel : ModelObject
            {
                private Wrap<int> _id = new Wrap<int>();
                private Wrap<int> _sit_id = new Wrap<int>();
                private Wrap<int> _pag_id = new Wrap<int>();
                private Wrap<int> _mod_id = new Wrap<int>();
                private Wrap<int> _sta_id = new Wrap<int>();
                private Wrap<int> _lng_id = new Wrap<int>();
                private Wrap<int> _parentid = new Wrap<int>();
                private Wrap<int> _order = new Wrap<int>();
                private Wrap<int> _type = new Wrap<int>();
                private Wrap<string> _title = new Wrap<string>();
                private Wrap<string> _alias = new Wrap<string>();
                private Wrap<string> _description = new Wrap<string>();
                private Wrap<string> _value1 = new Wrap<string>();
                private Wrap<string> _value2 = new Wrap<string>();
                private Wrap<string> _value3 = new Wrap<string>();
                private Wrap<string> _value4 = new Wrap<string>();
                private Wrap<string> _value5 = new Wrap<string>();
                private Wrap<string> _value6 = new Wrap<string>();
                private Wrap<string> _value7 = new Wrap<string>();
                private Wrap<string> _value8 = new Wrap<string>();
                private Wrap<string> _value9 = new Wrap<string>();
                private Wrap<string> _value10 = new Wrap<string>();
                private Wrap<string> _value11 = new Wrap<string>();
                private Wrap<string> _value12 = new Wrap<string>();
                private Wrap<string> _value13 = new Wrap<string>();
                private Wrap<string> _value14 = new Wrap<string>();
                private Wrap<string> _value15 = new Wrap<string>();
                private Wrap<string> _value16 = new Wrap<string>();
                private Wrap<string> _value17 = new Wrap<string>();
                private Wrap<string> _value18 = new Wrap<string>();
                private Wrap<string> _value19 = new Wrap<string>();
                private Wrap<string> _value20 = new Wrap<string>();
                private Wrap<string> _value21 = new Wrap<string>();
                private Wrap<string> _value22 = new Wrap<string>();
                private Wrap<string> _value23 = new Wrap<string>();
                private Wrap<string> _value24 = new Wrap<string>();
                private Wrap<string> _value25 = new Wrap<string>();
                private Wrap<string> _value26 = new Wrap<string>();
                private Wrap<string> _value27 = new Wrap<string>();
                private Wrap<string> _value28 = new Wrap<string>();
                private Wrap<string> _value29 = new Wrap<string>();
                private Wrap<string> _value30 = new Wrap<string>();
                private Wrap<string> _value31 = new Wrap<string>();
                private Wrap<string> _value32 = new Wrap<string>();
                private Wrap<string> _value33 = new Wrap<string>();
                private Wrap<string> _value34 = new Wrap<string>();
                private Wrap<string> _value35 = new Wrap<string>();
                private Wrap<string> _value36 = new Wrap<string>();
                private Wrap<string> _value37 = new Wrap<string>();
                private Wrap<string> _value38 = new Wrap<string>();
                private Wrap<string> _value39 = new Wrap<string>();
                private Wrap<string> _value40 = new Wrap<string>();
                private Wrap<string> _value41 = new Wrap<string>();
                private Wrap<string> _value42 = new Wrap<string>();
                private Wrap<string> _value43 = new Wrap<string>();
                private Wrap<string> _value44 = new Wrap<string>();
                private Wrap<string> _value45 = new Wrap<string>();
                private Wrap<string> _value46 = new Wrap<string>();
                private Wrap<string> _value47 = new Wrap<string>();
                private Wrap<string> _value48 = new Wrap<string>();
                private Wrap<string> _value49 = new Wrap<string>();
                private Wrap<string> _value50 = new Wrap<string>();
                private Wrap<DateTime> _createddate = new Wrap<DateTime>();
                private Wrap<string> _createdby = new Wrap<string>();
                private Wrap<DateTime> _updateddate = new Wrap<DateTime>();
                private Wrap<string> _updatedby = new Wrap<string>();
                private Wrap<bool> _hidden = new Wrap<bool>();
                private Wrap<bool> _deleted = new Wrap<bool>();

                public int Id
                {
                    get { return _id.Value; }
                    set { _id.Value = value; }
                }
                public int SitId
                {
                    get { return _sit_id.Value; }
                    set { _sit_id.Value = value; }
                }
                public int PagId
                {
                    get { return _pag_id.Value; }
                    set { _pag_id.Value = value; }
                }
                public int ModId
                {
                    get { return _mod_id.Value; }
                    set { _mod_id.Value = value; }
                }
                public int Status
                {
                    get { return _sta_id.Value; }
                    set { _sta_id.Value = value; }
                }
                public int Language
                {
                    get { return _lng_id.Value; }
                    set { _lng_id.Value = value; }
                }
                public int ParentId
                {
                    get { return _parentid.Value; }
                    set { _parentid.Value = value; }
                }
                public int Order
                {
                    get { return _order.Value; }
                    set { _order.Value = value; }
                }
                public int Type
                {
                    get { return _type.Value; }
                    set { _type.Value = value; }
                }
                public string Title
                {
                    get { return _title.Value; }
                    set { _title.Value = value; }
                }
                public string Alias
                {
                    get { return _alias.Value; }
                    set { _alias.Value = value; }
                }
                public string Description
                {
                    get { return _description.Value; }
                    set { _description.Value = value; }
                }
                public string Value1
                {
                    get { return _value1.Value; }
                    set { _value1.Value = value; }
                }
                public string Value2
                {
                    get { return _value2.Value; }
                    set { _value2.Value = value; }
                }
                public string Value3
                {
                    get { return _value3.Value; }
                    set { _value3.Value = value; }
                }
                public string Value4
                {
                    get { return _value4.Value; }
                    set { _value4.Value = value; }
                }
                public string Value5
                {
                    get { return _value5.Value; }
                    set { _value5.Value = value; }
                }
                public string Value6
                {
                    get { return _value6.Value; }
                    set { _value6.Value = value; }
                }
                public string Value7
                {
                    get { return _value7.Value; }
                    set { _value7.Value = value; }
                }
                public string Value8
                {
                    get { return _value8.Value; }
                    set { _value8.Value = value; }
                }
                public string Value9
                {
                    get { return _value9.Value; }
                    set { _value9.Value = value; }
                }
                public string Value10
                {
                    get { return _value10.Value; }
                    set { _value10.Value = value; }
                }
                public string Value11
                {
                    get { return _value11.Value; }
                    set { _value11.Value = value; }
                }
                public string Value12
                {
                    get { return _value12.Value; }
                    set { _value12.Value = value; }
                }
                public string Value13
                {
                    get { return _value13.Value; }
                    set { _value13.Value = value; }
                }
                public string Value14
                {
                    get { return _value14.Value; }
                    set { _value14.Value = value; }
                }
                public string Value15
                {
                    get { return _value15.Value; }
                    set { _value15.Value = value; }
                }
                public string Value16
                {
                    get { return _value16.Value; }
                    set { _value16.Value = value; }
                }
                public string Value17
                {
                    get { return _value17.Value; }
                    set { _value17.Value = value; }
                }
                public string Value18
                {
                    get { return _value18.Value; }
                    set { _value18.Value = value; }
                }
                public string Value19
                {
                    get { return _value19.Value; }
                    set { _value19.Value = value; }
                }
                public string Value20
                {
                    get { return _value20.Value; }
                    set { _value20.Value = value; }
                }
                public string Value21
                {
                    get { return _value21.Value; }
                    set { _value21.Value = value; }
                }
                public string Value22
                {
                    get { return _value22.Value; }
                    set { _value22.Value = value; }
                }
                public string Value23
                {
                    get { return _value23.Value; }
                    set { _value23.Value = value; }
                }
                public string Value24
                {
                    get { return _value24.Value; }
                    set { _value24.Value = value; }
                }
                public string Value25
                {
                    get { return _value25.Value; }
                    set { _value25.Value = value; }
                }
                public string Value26
                {
                    get { return _value26.Value; }
                    set { _value26.Value = value; }
                }
                public string Value27
                {
                    get { return _value27.Value; }
                    set { _value27.Value = value; }
                }
                public string Value28
                {
                    get { return _value28.Value; }
                    set { _value28.Value = value; }
                }
                public string Value29
                {
                    get { return _value29.Value; }
                    set { _value29.Value = value; }
                }
                public string Value30
                {
                    get { return _value30.Value; }
                    set { _value30.Value = value; }
                }
                public string Value31
                {
                    get { return _value31.Value; }
                    set { _value31.Value = value; }
                }
                public string Value32
                {
                    get { return _value32.Value; }
                    set { _value32.Value = value; }
                }
                public string Value33
                {
                    get { return _value33.Value; }
                    set { _value33.Value = value; }
                }
                public string Value34
                {
                    get { return _value34.Value; }
                    set { _value34.Value = value; }
                }
                public string Value35
                {
                    get { return _value35.Value; }
                    set { _value35.Value = value; }
                }
                public string Value36
                {
                    get { return _value36.Value; }
                    set { _value36.Value = value; }
                }
                public string Value37
                {
                    get { return _value37.Value; }
                    set { _value37.Value = value; }
                }
                public string Value38
                {
                    get { return _value38.Value; }
                    set { _value38.Value = value; }
                }
                public string Value39
                {
                    get { return _value39.Value; }
                    set { _value39.Value = value; }
                }
                public string Value40
                {
                    get { return _value40.Value; }
                    set { _value40.Value = value; }
                }
                public string Value41
                {
                    get { return _value41.Value; }
                    set { _value41.Value = value; }
                }
                public string Value42
                {
                    get { return _value42.Value; }
                    set { _value42.Value = value; }
                }
                public string Value43
                {
                    get { return _value43.Value; }
                    set { _value43.Value = value; }
                }
                public string Value44
                {
                    get { return _value44.Value; }
                    set { _value44.Value = value; }
                }
                public string Value45
                {
                    get { return _value45.Value; }
                    set { _value45.Value = value; }
                }
                public string Value46
                {
                    get { return _value46.Value; }
                    set { _value46.Value = value; }
                }
                public string Value47
                {
                    get { return _value47.Value; }
                    set { _value47.Value = value; }
                }
                public string Value48
                {
                    get { return _value48.Value; }
                    set { _value48.Value = value; }
                }
                public string Value49
                {
                    get { return _value49.Value; }
                    set { _value49.Value = value; }
                }
                public string Value50
                {
                    get { return _value50.Value; }
                    set { _value50.Value = value; }
                }
                public DateTime CreatedDate
                {
                    get { return _createddate.Value; }
                    set { _createddate.Value = value; }
                }
                public string CreatedBy
                {
                    get { return _createdby.Value; }
                    set { _createdby.Value = value; }
                }
                public DateTime UpdatedDate
                {
                    get { return _updateddate.Value; }
                    set { _updateddate.Value = value; }
                }
                public string UpdatedBy
                {
                    get { return _updatedby.Value; }
                    set { _updatedby.Value = value; }
                }
                public bool Hidden
                {
                    get { return _hidden.Value; }
                    set { _hidden.Value = value; }
                }
                public bool Deleted
                {
                    get { return _deleted.Value; }
                    set { _deleted.Value = value; }
                }

                public ItemModel(DomainObject container)
                    : base(container)
                {
                    _id.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                    _sit_id.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                    _pag_id.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                    _mod_id.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                    _sta_id.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                    _lng_id.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                    _parentid.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                    _order.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                    _type.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                    _title.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _alias.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _description.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _value1.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _value2.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _value3.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _value4.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _value5.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _value6.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _value7.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _value8.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _value9.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _value10.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _value11.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _value12.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _value13.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _value14.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _value15.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _value16.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _value17.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _value18.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _value19.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _value20.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _value21.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _value22.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _value23.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _value24.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _value25.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _value26.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _value27.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _value28.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _value29.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _value30.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _value31.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _value32.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _value33.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _value34.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _value35.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _value36.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _value37.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _value38.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _value39.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _value40.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _value41.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _value42.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _value43.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _value44.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _value45.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _value46.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _value47.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _value48.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _value49.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _value50.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _createddate.ValueUpdated += new Wrap<DateTime>.ValueUpdateEventHandler(NotifyContainer);
                    _createdby.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _updateddate.ValueUpdated += new Wrap<DateTime>.ValueUpdateEventHandler(NotifyContainer);
                    _updatedby.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _hidden.ValueUpdated += new Wrap<bool>.ValueUpdateEventHandler(NotifyContainer);
                    _deleted.ValueUpdated += new Wrap<bool>.ValueUpdateEventHandler(NotifyContainer);
                }
            }
        }
        public class UserTypesModel : ModelObject
        {
            private Wrap<List<UserTypes.UserType>> _list = new Wrap<List<UserTypes.UserType>>();

            public UserTypesModel(DomainObject container)
                : base(container)
            {
                _list.ValueUpdated += new Wrap<List<UserTypes.UserType>>.ValueUpdateEventHandler(NotifyContainer);
            }
            public class UserTypeModel : ModelObject
            {
                private Wrap<int> _id = new Wrap<int>();
                private Wrap<int> _sta_id = new Wrap<int>();
                private Wrap<int> _lng_id = new Wrap<int>();
                private Wrap<int> _parentid = new Wrap<int>();
                private Wrap<int> _order = new Wrap<int>();
                private Wrap<string> _title = new Wrap<string>();
                private Wrap<string> _alias = new Wrap<string>();
                private Wrap<string> _description = new Wrap<string>();
                private Wrap<DateTime> _createddate = new Wrap<DateTime>();
                private Wrap<string> _createdby = new Wrap<string>();
                private Wrap<DateTime> _updateddate = new Wrap<DateTime>();
                private Wrap<string> _updatedby = new Wrap<string>();
                private Wrap<bool> _hidden = new Wrap<bool>();
                private Wrap<bool> _deleted = new Wrap<bool>();

                public int Id
                {
                    get { return _id.Value; }
                    set { _id.Value = value; }
                }
                public int Status
                {
                    get { return _sta_id.Value; }
                    set { _sta_id.Value = value; }
                }
                public int Language
                {
                    get { return _lng_id.Value; }
                    set { _lng_id.Value = value; }
                }
                public int ParentId
                {
                    get { return _parentid.Value; }
                    set { _parentid.Value = value; }
                }
                public int Order
                {
                    get { return _order.Value; }
                    set { _order.Value = value; }
                }
                public string Title
                {
                    get { return _title.Value; }
                    set { _title.Value = value; }
                }
                public string Alias
                {
                    get { return _alias.Value; }
                    set { _alias.Value = value; }
                }
                public string Description
                {
                    get { return _description.Value; }
                    set { _description.Value = value; }
                }
                public DateTime CreatedDate
                {
                    get { return _createddate.Value; }
                    set { _createddate.Value = value; }
                }
                public string CreatedBy
                {
                    get { return _createdby.Value; }
                    set { _createdby.Value = value; }
                }
                public DateTime UpdatedDate
                {
                    get { return _updateddate.Value; }
                    set { _updateddate.Value = value; }
                }
                public string UpdatedBy
                {
                    get { return _updatedby.Value; }
                    set { _updatedby.Value = value; }
                }
                public bool Hidden
                {
                    get { return _hidden.Value; }
                    set { _hidden.Value = value; }
                }
                public bool Deleted
                {
                    get { return _deleted.Value; }
                    set { _deleted.Value = value; }
                }

                public UserTypeModel(DomainObject container)
                    : base(container)
                {
                    _id.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                    _sta_id.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                    _lng_id.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                    _parentid.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                    _order.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                    _title.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _alias.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _description.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _createddate.ValueUpdated += new Wrap<DateTime>.ValueUpdateEventHandler(NotifyContainer);
                    _createdby.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _updateddate.ValueUpdated += new Wrap<DateTime>.ValueUpdateEventHandler(NotifyContainer);
                    _updatedby.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _hidden.ValueUpdated += new Wrap<bool>.ValueUpdateEventHandler(NotifyContainer);
                    _deleted.ValueUpdated += new Wrap<bool>.ValueUpdateEventHandler(NotifyContainer);
                }
            }
        }
        public class RolesModel : ModelObject
        {
            private Wrap<List<Roles.Role>> _list = new Wrap<List<Roles.Role>>();

            public RolesModel(DomainObject container)
                : base(container)
            {
                _list.ValueUpdated += new Wrap<List<Roles.Role>>.ValueUpdateEventHandler(NotifyContainer);
            }
            public class RoleModel : ModelObject
            {
                private Wrap<int> _id = new Wrap<int>();
                private Wrap<int> _sta_id = new Wrap<int>();
                private Wrap<int> _lng_id = new Wrap<int>();
                private Wrap<int> _parentid = new Wrap<int>();
                private Wrap<int> _order = new Wrap<int>();
                private Wrap<string> _title = new Wrap<string>();
                private Wrap<string> _alias = new Wrap<string>();
                private Wrap<string> _description = new Wrap<string>();
                private Wrap<DateTime> _createddate = new Wrap<DateTime>();
                private Wrap<string> _createdby = new Wrap<string>();
                private Wrap<DateTime> _updateddate = new Wrap<DateTime>();
                private Wrap<string> _updatedby = new Wrap<string>();
                private Wrap<bool> _hidden = new Wrap<bool>();
                private Wrap<bool> _deleted = new Wrap<bool>();

                public int Id
                {
                    get { return _id.Value; }
                    set { _id.Value = value; }
                }
                public int Status
                {
                    get { return _sta_id.Value; }
                    set { _sta_id.Value = value; }
                }
                public int Language
                {
                    get { return _lng_id.Value; }
                    set { _lng_id.Value = value; }
                }
                public int ParentId
                {
                    get { return _parentid.Value; }
                    set { _parentid.Value = value; }
                }
                public int Order
                {
                    get { return _order.Value; }
                    set { _order.Value = value; }
                }
                public string Title
                {
                    get { return _title.Value; }
                    set { _title.Value = value; }
                }
                public string Alias
                {
                    get { return _alias.Value; }
                    set { _alias.Value = value; }
                }
                public string Description
                {
                    get { return _description.Value; }
                    set { _description.Value = value; }
                }
                public DateTime CreatedDate
                {
                    get { return _createddate.Value; }
                    set { _createddate.Value = value; }
                }
                public string CreatedBy
                {
                    get { return _createdby.Value; }
                    set { _createdby.Value = value; }
                }
                public DateTime UpdatedDate
                {
                    get { return _updateddate.Value; }
                    set { _updateddate.Value = value; }
                }
                public string UpdatedBy
                {
                    get { return _updatedby.Value; }
                    set { _updatedby.Value = value; }
                }
                public bool Hidden
                {
                    get { return _hidden.Value; }
                    set { _hidden.Value = value; }
                }
                public bool Deleted
                {
                    get { return _deleted.Value; }
                    set { _deleted.Value = value; }
                }

                public RoleModel(DomainObject container)
                    : base(container)
                {
                    _id.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                    _sta_id.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                    _lng_id.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                    _parentid.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                    _order.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                    _title.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _alias.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _description.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _createddate.ValueUpdated += new Wrap<DateTime>.ValueUpdateEventHandler(NotifyContainer);
                    _createdby.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _updateddate.ValueUpdated += new Wrap<DateTime>.ValueUpdateEventHandler(NotifyContainer);
                    _updatedby.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _hidden.ValueUpdated += new Wrap<bool>.ValueUpdateEventHandler(NotifyContainer);
                    _deleted.ValueUpdated += new Wrap<bool>.ValueUpdateEventHandler(NotifyContainer);
                }
            }
        }
        public class UsersModel : ModelObject
        {
            private Wrap<List<Users.User>> _list = new Wrap<List<Users.User>>();

            public UsersModel(DomainObject container)
                : base(container)
            {
                _list.ValueUpdated += new Wrap<List<Users.User>>.ValueUpdateEventHandler(NotifyContainer);
            }
            public class UserModel : ModelObject
            {
                private Wrap<int> _id = new Wrap<int>();
                private Wrap<int> _sta_id = new Wrap<int>();
                private Wrap<int> _lng_id = new Wrap<int>();
                private Wrap<int> _parentid = new Wrap<int>();
                private Wrap<int> _order = new Wrap<int>();
                private Wrap<int> _type = new Wrap<int>();
                private Wrap<string> _title = new Wrap<string>();
                private Wrap<string> _alias = new Wrap<string>();
                private Wrap<string> _description = new Wrap<string>();
                private Wrap<string> _mail = new Wrap<string>();
                private Wrap<string> _loginname = new Wrap<string>();
                private Wrap<string> _password = new Wrap<string>();
                private Wrap<string> _firstname = new Wrap<string>();
                private Wrap<string> _middlename = new Wrap<string>();
                private Wrap<string> _lastname = new Wrap<string>();
                private Wrap<string> _address = new Wrap<string>();
                private Wrap<string> _co = new Wrap<string>();
                private Wrap<string> _postalcode = new Wrap<string>();
                private Wrap<string> _city = new Wrap<string>();
                private Wrap<string> _country = new Wrap<string>();
                private Wrap<string> _phone = new Wrap<string>();
                private Wrap<string> _mobile = new Wrap<string>();
                private Wrap<string> _fax = new Wrap<string>();
                private Wrap<string> _company = new Wrap<string>();
                private Wrap<int> _startpage = new Wrap<int>();
                private Wrap<DateTime> _createddate = new Wrap<DateTime>();
                private Wrap<string> _createdby = new Wrap<string>();
                private Wrap<DateTime> _updateddate = new Wrap<DateTime>();
                private Wrap<string> _updatedby = new Wrap<string>();
                private Wrap<bool> _hidden = new Wrap<bool>();
                private Wrap<bool> _deleted = new Wrap<bool>();

                public int Id
                {
                    get { return _id.Value; }
                    set { _id.Value = value; }
                }
                public int Status
                {
                    get { return _sta_id.Value; }
                    set { _sta_id.Value = value; }
                }
                public int Language
                {
                    get { return _lng_id.Value; }
                    set { _lng_id.Value = value; }
                }
                public int ParentId
                {
                    get { return _parentid.Value; }
                    set { _parentid.Value = value; }
                }
                public int Order
                {
                    get { return _order.Value; }
                    set { _order.Value = value; }
                }
                public int Type
                {
                    get { return _type.Value; }
                    set { _type.Value = value; }
                }
                public string Title
                {
                    get { return _title.Value; }
                    set { _title.Value = value; }
                }
                public string Alias
                {
                    get { return _alias.Value; }
                    set { _alias.Value = value; }
                }
                public string Description
                {
                    get { return _description.Value; }
                    set { _description.Value = value; }
                }
                public string Mail
                {
                    get { return _mail.Value; }
                    set { _mail.Value = value; }
                }
                public string LoginName
                {
                    get { return _loginname.Value; }
                    set { _loginname.Value = value; }
                }
                public string Password
                {
                    get { return _password.Value; }
                    set { _password.Value = value; }
                }
                public string FirstName
                {
                    get { return _firstname.Value; }
                    set { _firstname.Value = value; }
                }
                public string MiddleName
                {
                    get { return _middlename.Value; }
                    set { _middlename.Value = value; }
                }
                public string LastName
                {
                    get { return _lastname.Value; }
                    set { _lastname.Value = value; }
                }
                public string Address
                {
                    get { return _address.Value; }
                    set { _address.Value = value; }
                }
                public string CO
                {
                    get { return _co.Value; }
                    set { _co.Value = value; }
                }
                public string PostalCode
                {
                    get { return _postalcode.Value; }
                    set { _postalcode.Value = value; }
                }
                public string City
                {
                    get { return _city.Value; }
                    set { _city.Value = value; }
                }
                public string Country
                {
                    get { return _country.Value; }
                    set { _country.Value = value; }
                }
                public string Phone
                {
                    get { return _phone.Value; }
                    set { _phone.Value = value; }
                }
                public string Mobile
                {
                    get { return _mobile.Value; }
                    set { _mobile.Value = value; }
                }
                public string Fax
                {
                    get { return _fax.Value; }
                    set { _fax.Value = value; }
                }
                public string Company
                {
                    get { return _company.Value; }
                    set { _company.Value = value; }
                }
                public int StartPage
                {
                    get { return _startpage.Value; }
                    set { _startpage.Value = value; }
                }
                public DateTime CreatedDate
                {
                    get { return _createddate.Value; }
                    set { _createddate.Value = value; }
                }
                public string CreatedBy
                {
                    get { return _createdby.Value; }
                    set { _createdby.Value = value; }
                }
                public DateTime UpdatedDate
                {
                    get { return _updateddate.Value; }
                    set { _updateddate.Value = value; }
                }
                public string UpdatedBy
                {
                    get { return _updatedby.Value; }
                    set { _updatedby.Value = value; }
                }
                public bool Hidden
                {
                    get { return _hidden.Value; }
                    set { _hidden.Value = value; }
                }
                public bool Deleted
                {
                    get { return _deleted.Value; }
                    set { _deleted.Value = value; }
                }

                public UserModel(DomainObject container)
                    : base(container)
                {
                    _id.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                    _sta_id.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                    _lng_id.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                    _parentid.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                    _order.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                    _type.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                    _title.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _alias.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _description.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _mail.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _loginname.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _password.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);

                    _firstname.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _middlename.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _lastname.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _address.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _co.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _postalcode.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _city.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _country.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _phone.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _mobile.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _fax.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _company.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _startpage.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);

                    _createddate.ValueUpdated += new Wrap<DateTime>.ValueUpdateEventHandler(NotifyContainer);
                    _createdby.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _updateddate.ValueUpdated += new Wrap<DateTime>.ValueUpdateEventHandler(NotifyContainer);
                    _updatedby.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _hidden.ValueUpdated += new Wrap<bool>.ValueUpdateEventHandler(NotifyContainer);
                    _deleted.ValueUpdated += new Wrap<bool>.ValueUpdateEventHandler(NotifyContainer);
                }
            }
        }
        public class StatusModel : ModelObject
        {
            private Wrap<List<Status.Item>> _list = new Wrap<List<Status.Item>>();

            public StatusModel(DomainObject container)
                : base(container)
            {
                _list.ValueUpdated += new Wrap<List<Status.Item>>.ValueUpdateEventHandler(NotifyContainer);
            }
            public class ItemModel : ModelObject
            {
                private Wrap<int> _id = new Wrap<int>();
                private Wrap<int> _lng_id = new Wrap<int>();
                private Wrap<int> _parentid = new Wrap<int>();
                private Wrap<int> _order = new Wrap<int>();
                private Wrap<int> _revision = new Wrap<int>();
                private Wrap<string> _title = new Wrap<string>();
                private Wrap<string> _alias = new Wrap<string>();
                private Wrap<string> _description = new Wrap<string>();
                private Wrap<DateTime> _createddate = new Wrap<DateTime>();
                private Wrap<string> _createdby = new Wrap<string>();
                private Wrap<DateTime> _updateddate = new Wrap<DateTime>();
                private Wrap<string> _updatedby = new Wrap<string>();
                private Wrap<bool> _hidden = new Wrap<bool>();
                private Wrap<bool> _deleted = new Wrap<bool>();

                public int Id
                {
                    get { return _id.Value; }
                    set { _id.Value = value; }
                }
                public int Language
                {
                    get { return _lng_id.Value; }
                    set { _lng_id.Value = value; }
                }
                public int ParentId
                {
                    get { return _parentid.Value; }
                    set { _parentid.Value = value; }
                }
                public int Order
                {
                    get { return _order.Value; }
                    set { _order.Value = value; }
                }
                public int Revision
                {
                    get { return _revision.Value; }
                    set { _revision.Value = value; }
                }
                public string Title
                {
                    get { return _title.Value; }
                    set { _title.Value = value; }
                }
                public string Alias
                {
                    get { return _alias.Value; }
                    set { _alias.Value = value; }
                }
                public string Description
                {
                    get { return _description.Value; }
                    set { _description.Value = value; }
                }
                public DateTime CreatedDate
                {
                    get { return _createddate.Value; }
                    set { _createddate.Value = value; }
                }
                public string CreatedBy
                {
                    get { return _createdby.Value; }
                    set { _createdby.Value = value; }
                }
                public DateTime UpdatedDate
                {
                    get { return _updateddate.Value; }
                    set { _updateddate.Value = value; }
                }
                public string UpdatedBy
                {
                    get { return _updatedby.Value; }
                    set { _updatedby.Value = value; }
                }
                public bool Hidden
                {
                    get { return _hidden.Value; }
                    set { _hidden.Value = value; }
                }
                public bool Deleted
                {
                    get { return _deleted.Value; }
                    set { _deleted.Value = value; }
                }

                public ItemModel(DomainObject container)
                    : base(container)
                {
                    _id.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                    _lng_id.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                    _parentid.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                    _order.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                    _revision.ValueUpdated += new Wrap<int>.ValueUpdateEventHandler(NotifyContainer);
                    _title.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _alias.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _description.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _createddate.ValueUpdated += new Wrap<DateTime>.ValueUpdateEventHandler(NotifyContainer);
                    _createdby.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _updateddate.ValueUpdated += new Wrap<DateTime>.ValueUpdateEventHandler(NotifyContainer);
                    _updatedby.ValueUpdated += new Wrap<string>.ValueUpdateEventHandler(NotifyContainer);
                    _hidden.ValueUpdated += new Wrap<bool>.ValueUpdateEventHandler(NotifyContainer);
                    _deleted.ValueUpdated += new Wrap<bool>.ValueUpdateEventHandler(NotifyContainer);
                }
            }
        }
    }
}

// DataLayer
namespace LiquidCore
{
    // ToDo:
    // Testa att skapa revisions hantering för alla Update()
    // Detta ska kunna sättas på och av.
    // Databasen är uppdaterad nu saknas bara logiken i Update() metoderna.
    
    namespace LiquidCore.Data
    {
        using CoreLib;
        using LiquidCore.Modeler;
        using LiquidCore.Definition;
        using System.IO;
		using System.Web.Caching;

        public enum OrderMinMax
        {
            Min = 1,
            Max = 2100000000,
            Step = 2
        }
        public static class EventLog
        {
            static string CLASSNAME = "[Namespace::LiquidCore.Data][Class::EventLog]";

            static String Path = System.Web.HttpContext.Current.Server.MapPath(".");

            public static void LogEvent(String Event, String Function, String Variant)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::LogEvent]";
                StreamWriter SW = null;
                try
                {
                    bool EventLogOn = false;

                    if (System.Configuration.ConfigurationManager.AppSettings["Data.EventLogOn"] != null)
                        EventLogOn = Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["Data.EventLogOn"].ToString());
                    
                    if (!EventLogOn)
                        return;

                    if (File.Exists(Path + @"\eventlog_" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + ".txt"))
                        SW = File.AppendText(Path + @"\eventlog_" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + ".txt");
                    else
                        SW = File.CreateText(Path + @"\eventlog_" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + ".txt");

                    SW.WriteLine(DateTime.Now.ToLongTimeString() + " : " + Function + " : " + Event + " (" + Variant + ")");

                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, Function + " : " + Event + " (" + Variant + ")");
                }
                finally
                {
                    if(SW != null)
                        SW.Close();
                }
            }

        }
        public static class CacheData
        {
            static Int32 CacheTimeOut = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Data.CacheTimeOutInMinutes"].ToString());
            static bool ShowCacheHistory = Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["Data.ShowCacheHistory"].ToString());

            public static void Insert(String CacheItem, DataTable dt)
            {
                if (HttpContext.Current != null && CacheTimeOut > 0)
                {
                    if (HttpContext.Current.Cache[CacheItem] == null)
                    {
                        Reset(CacheItem);
						HttpContext.Current.Cache.Insert(CacheItem, dt, null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration);
                        dt = (DataTable)HttpContext.Current.Cache[CacheItem];
                        if (ShowCacheHistory)
                            System.Diagnostics.Debug.WriteLine("LiquidCore is inserting item: " + CacheItem + " into cache for " + CacheTimeOut.ToString() + " minutes.");
                    }
                }
            }
            public static void Insert(String CacheItem, Int32[] i)
            {
                if (HttpContext.Current != null && CacheTimeOut > 0)
                {
                    if (HttpContext.Current.Cache[CacheItem] == null)
                    {
                        Reset(CacheItem);
						HttpContext.Current.Cache.Insert(CacheItem, i, null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration);
                        i = (Int32[])HttpContext.Current.Cache[CacheItem];
                        if (ShowCacheHistory)
                            System.Diagnostics.Debug.WriteLine("LiquidCore is inserting item: " + CacheItem + " with data: " + i.Length.ToString() + " into cache for " + CacheTimeOut.ToString() + " minutes.");
                    }
                }
            }

            public static void Reset(String CachePreValue)
            {
                if (HttpContext.Current != null && CacheTimeOut > 0)
                {
                    String cacheItem = String.Empty;
                    IDictionaryEnumerator CacheEnum = HttpContext.Current.Cache.GetEnumerator();
                    while (CacheEnum.MoveNext())
                    {
                        cacheItem = CacheEnum.Key.ToString();
                        if (cacheItem.StartsWith(CachePreValue))
                        {
                            if (ShowCacheHistory)
                                System.Diagnostics.Debug.WriteLine("LiquidCore is removing: " + cacheItem + " from cache.");
                            HttpContext.Current.Cache.Remove(cacheItem);
                        }
                    }
                }
            }

            public static void Reset()
            {
                Reset("LiquidCore");
            }

        }

        public static class SiteData
        {
            static string CLASSNAME = "[Namespace::LiquidCore][Class::SiteData]";

            static Int32 DataBlockSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Data.DataBlockSize"].ToString());
            static Int32 UseRevision = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Data.UseRevision"].ToString());

            public static void Create(SiteModel Details)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::Create]";
                try
                {
                    ((SiteModel)Details).Order = (Int32)OrderMinMax.Max;
                    ((SiteModel)Details).UpdatedDate = DateTime.Now;
                    ((SiteModel)Details).UpdatedBy = Authentication.User.Identity.Name;
                    ((SiteModel)Details).CreatedDate = DateTime.Now;
                    ((SiteModel)Details).CreatedBy = Authentication.User.Identity.Name;

                    System.Data.DataSet ds = new System.Data.DataSet();
                    StringBuilder sSQL = new StringBuilder();
                    sSQL.Append("INSERT INTO sit_sites (sta_id, lng_id, sit_parentid, sit_order, sit_title, ");
                    sSQL.Append("sit_alias, sit_description, sit_theme, sit_structure, sit_createddate, sit_createdby, ");
                    sSQL.Append("sit_updateddate, sit_updatedby, sit_hidden, sit_deleted) VALUES ( ");
                    sSQL.Append(Details.Status.ToString() + ", ");
                    sSQL.Append(Details.Language.ToString() + ", ");
                    sSQL.Append(Details.ParentId.ToString() + ", ");
                    sSQL.Append(Details.Order.ToString() + ", ");
                    sSQL.Append("'" + Details.Title + "', ");
                    sSQL.Append("'" + Details.Alias + "', ");
                    sSQL.Append("'" + Details.Description + "', ");
                    sSQL.Append("'" + Details.Theme + "', ");
                    sSQL.Append("'" + Details.Structure + "', ");
                    sSQL.Append("'" + Details.CreatedDate.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "', ");
                    sSQL.Append("'" + Details.CreatedBy + "', ");
                    sSQL.Append("'" + Details.UpdatedDate.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "', ");
                    sSQL.Append("'" + Details.UpdatedBy + "', ");
                    sSQL.Append("" + (Details.Hidden ? "1" : "0") + ", ");
                    sSQL.Append("" + (Details.Deleted ? "1" : "0") + ")");
                    using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, true))
                    {
                        EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                        oDo.ExecuteNonQuery(sSQL.ToString());
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);

                        EventLog.LogEvent("SELECT @@IDENTITY", FUNCTIONNAME, String.Empty);
                        ((SiteModel)Details).Id = Convert.ToInt32(oDo.GetDataTable("SELECT @@IDENTITY").Rows[0][0]);
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);

                        //EventLog.LogEvent("CALL sort_sit_sites();", FUNCTIONNAME, String.Empty);
                        //oDo.ExecuteNonQuery("CALL sort_sit_sites();");
                        //if (oDo.HasError)
                        //    throw new Exception(oDo.GetError);

                    }
                    SortAll();
                    ResetThis();
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void Load(ModelObject Details, NameValueSet DataFields)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::Load]";
                try
                {
                    System.Data.DataTable dt = null;
                    String CacheItem = "LiquidCore.Sites.Site.Id(" + DataFields["Id"].ToString() + ")";
                    if (HttpContext.Current != null) if (HttpContext.Current != null) dt = (DataTable)HttpContext.Current.Cache[CacheItem];
                    if (dt == null)
                    {
                        StringBuilder sSQL = new StringBuilder();
                        sSQL.Append("SELECT * FROM sit_sites WHERE sit_id = " + DataFields["Id"].ToString() + " AND sit_deleted = 0");
                        using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true))
                        {
                            EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                            dt = oDo.GetDataTable(sSQL.ToString());
                            if (oDo.HasError)
                                throw new Exception(oDo.GetError);
                        }
                        CacheData.Insert(CacheItem, dt);
                    }
                    if (dt.Rows.Count > 0)
                    {
                        System.Data.DataRow dr = dt.Rows[0];
                        ((SiteModel)Details).Id = Convert.ToInt32(dr["sit_id"]);
                        ((SiteModel)Details).Status = Convert.ToInt32(dr["sta_id"]);
                        ((SiteModel)Details).Language = Convert.ToInt32(dr["lng_id"]);
                        ((SiteModel)Details).ParentId = Convert.ToInt32(dr["sit_parentid"]);
                        ((SiteModel)Details).Order = Convert.ToInt32(dr["sit_order"]);
                        ((SiteModel)Details).Title = Convert.ToString(dr["sit_title"]);
                        ((SiteModel)Details).Alias = Convert.ToString(dr["sit_alias"]);
                        ((SiteModel)Details).Description = Convert.ToString(dr["sit_description"]);
                        ((SiteModel)Details).AuthorizedRoles = GetAuthorizedRoles(Convert.ToInt32(DataFields["Id"]));
                        ((SiteModel)Details).Theme = Convert.ToString(dr["sit_theme"]);
                        ((SiteModel)Details).Structure = Convert.ToString(dr["sit_structure"]);
                        ((SiteModel)Details).CreatedDate = Convert.ToDateTime(dr["sit_createddate"]);
                        ((SiteModel)Details).CreatedBy = Convert.ToString(dr["sit_createdby"]);
                        ((SiteModel)Details).UpdatedDate = Convert.ToDateTime(dr["sit_updateddate"]);
                        ((SiteModel)Details).UpdatedBy = Convert.ToString(dr["sit_updatedby"]);
                        ((SiteModel)Details).Hidden = (dr["sit_hidden"].ToString().Equals("0") ? false : true);
                        ((SiteModel)Details).Deleted = (dr["sit_deleted"].ToString().Equals("0") ? false : true);
                    }
                    else
                        ((SiteModel)Details).Deleted = true;
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void LoadAll(ref List<Site> _sites)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::LoadAll]";
                try
                {
                    DataTable dt = null;
                    String CacheItem = "LiquidCore.Sites";
                    if (HttpContext.Current != null) if (HttpContext.Current != null) dt = (DataTable)HttpContext.Current.Cache[CacheItem];
                    if (dt == null)
                    {
                        StringBuilder sSQL = new StringBuilder();
                        sSQL.Append("SELECT sit_id FROM sit_sites WHERE sit_deleted = 0 ORDER BY sta_id, ");
                        sSQL.Append("lng_id, sit_parentid, sit_order");
                        using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true))
                        {
                            EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                            dt = oDo.GetDataTable(sSQL.ToString());
                            if (oDo.HasError)
                                throw new Exception(oDo.GetError);
                        }
                        CacheData.Insert(CacheItem, dt);
                    }
                    foreach (DataRow dr in dt.Rows)
                    {
                        Site s = new Site(Convert.ToInt32(dr["sit_id"].ToString()));
                        _sites.Add(s);
                    }
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void LoadAll(ref List<Site> _sites, NameValueSet DataFields)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::LoadAll]";
                try
                {
                    String PrimaryKey = String.Empty;
                    String CacheItem = String.Empty;
                    StringBuilder sSQL = new StringBuilder();
                    if (DataFields["Alias"] != null)
                    {
                        PrimaryKey = DataFields["Alias"].ToString();
                        CacheItem = "LiquidCore.Sites.PrimaryKey(Alias(" + PrimaryKey + "))";
                        sSQL.Append("SELECT sit_id FROM sit_sites WHERE sit_alias = '" + PrimaryKey + "' AND sit_deleted = 0 ORDER BY sta_id, lng_id, sit_parentid, sit_order");
                    }
                    else
                    {
                        LoadAll(ref _sites);
                        return;
                    }
                    DataTable dt = null;
                    if (HttpContext.Current != null) if (HttpContext.Current != null) dt = (DataTable)HttpContext.Current.Cache[CacheItem];
                    if (dt == null)
                    {
                        using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true))
                        {
                            EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                            dt = oDo.GetDataTable(sSQL.ToString());
                            if (oDo.HasError)
                                throw new Exception(oDo.GetError);
                        }
                        CacheData.Insert(CacheItem, dt);
                    }
                    foreach (DataRow dr in dt.Rows)
                    {
                        Site s = new Site(Convert.ToInt32(dr["sit_id"].ToString()));
                        _sites.Add(s);
                    }
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void Update(SiteModel Details)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::Update]";
                try
                {
                    ((SiteModel)Details).UpdatedDate = DateTime.Now;
                    ((SiteModel)Details).UpdatedBy = Authentication.User.Identity.Name;

                    System.Data.DataSet ds = new System.Data.DataSet();
                    StringBuilder sSQL = new StringBuilder();
                    sSQL.Append("UPDATE sit_sites SET ");
                    sSQL.Append("sta_id = " + Details.Status.ToString() + ", ");
                    sSQL.Append("lng_id = " + Details.Language.ToString() + ", ");
                    sSQL.Append("sit_parentid = " + Details.ParentId.ToString() + ", ");
                    sSQL.Append("sit_order = " + Details.Order.ToString() + ", ");
                    sSQL.Append("sit_title = '" + Details.Title + "', ");
                    sSQL.Append("sit_alias = '" + Details.Alias + "', ");
                    sSQL.Append("sit_description = '" + Details.Description + "', ");
                    sSQL.Append("sit_theme = '" + Details.Theme + "', ");
                    sSQL.Append("sit_structure = '" + Details.Structure + "', ");
                    sSQL.Append("sit_createddate = '" + Details.CreatedDate.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "', ");
                    sSQL.Append("sit_createdby = '" + Details.CreatedBy + "', ");
                    sSQL.Append("sit_updateddate = '" + Details.UpdatedDate.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "', ");
                    sSQL.Append("sit_updatedby = '" + Details.UpdatedBy + "', ");
                    sSQL.Append("sit_hidden = " + (!Details.Hidden ? "0" : "1") + ", ");
                    sSQL.Append("sit_deleted = " + (!Details.Deleted ? "0" : "1") + " ");
                    sSQL.Append("WHERE sit_id = " + Details.Id.ToString());
                    using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, true))
                    {
                        EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                        oDo.ExecuteNonQuery(sSQL.ToString());
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);

                        //EventLog.LogEvent("CALL sort_sit_sites();", FUNCTIONNAME, String.Empty);
                        //oDo.ExecuteNonQuery("CALL sort_sit_sites();");
                        //if (oDo.HasError)
                        //    throw new Exception(oDo.GetError);

                    }
                    SortAll();
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void Delete(SiteModel Details)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::Delete]";
                try
                {
                    RecursiveDelete(Details.Id);
                    //using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, false))
                    //{
                    //    //EventLog.LogEvent("CALL sort_sit_sites();", FUNCTIONNAME, String.Empty);
                    //    //oDo.ExecuteNonQuery("CALL sort_sit_sites();");
                    //    //if (oDo.HasError)
                    //    //    throw new Exception(oDo.GetError);

                    //    //EventLog.LogEvent("CALL sort_set_settings();", FUNCTIONNAME, String.Empty);
                    //    //oDo.ExecuteNonQuery("CALL sort_set_settings();");
                    //    //if (oDo.HasError)
                    //    //    throw new Exception(oDo.GetError);
                    //}
                    SortAll();
                    ResetThis();
                    Details = null;
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void RecursiveDelete(Int32 Id)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::RecursiveDelete]";
                try
                {
                    if (Id.Equals(0))
                        return;

                    using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, true))
                    {
                        String sSQL1 = "UPDATE asr_authorizedsitesroles SET asr_deleted = 1 WHERE sit_id = " + Id.ToString();
                        String sSQL2 = "UPDATE set_settings SET set_deleted = 1 WHERE sit_id = " + Id.ToString();
                        String sSQL3 = "UPDATE sit_sites SET sit_deleted = 1 WHERE sit_id = " + Id.ToString();

                        EventLog.LogEvent(sSQL1, FUNCTIONNAME, String.Empty);
                        System.Diagnostics.Debug.WriteLine(sSQL1);
                        oDo.ExecuteNonQuery(sSQL1);
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);

                        EventLog.LogEvent(sSQL2, FUNCTIONNAME, String.Empty);
                        System.Diagnostics.Debug.WriteLine(sSQL2);
                        oDo.ExecuteNonQuery(sSQL2);
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);

                        EventLog.LogEvent(sSQL3, FUNCTIONNAME, String.Empty);
                        System.Diagnostics.Debug.WriteLine(sSQL3);
                        oDo.ExecuteNonQuery(sSQL3);
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);
                    }
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void ResetThis()
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::ResetThis]";
                try
                {
                    CacheData.Reset("LiquidCore.Sites");
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            //[ObsoleteAttribute("SortAll has been deprecated. All sorting take place in stored procedures instead.")]
            public static void SortAll()
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::SortAll]";
                try
                {
                    Int32 Order = (Int32)OrderMinMax.Min;
                    StringBuilder sSQL = new StringBuilder();
                    sSQL.Append("SELECT sit_id FROM sit_sites WHERE sit_deleted = 0 ORDER BY sta_id, lng_id, sit_parentid, sit_order");
                    using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, false))
                    {
                        EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                        DataTable dt = oDo.GetDataTable(sSQL.ToString());
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);
                        StringBuilder sSQL2 = new StringBuilder();
                        foreach (DataRow dr in dt.Rows)
                        {
                            sSQL2.AppendLine("UPDATE sit_sites SET sit_order = " + Order.ToString() + " WHERE sit_id = " + dr["sit_id"].ToString() + ";");
                            Order = Order + (Int32)OrderMinMax.Step;
                            if (sSQL2.Length > DataBlockSize)
                            {
                                EventLog.LogEvent(sSQL2.ToString(), FUNCTIONNAME, String.Empty);
                                if (!(oDo.ExecuteNonQuery(sSQL2.ToString()) > 0))
                                    throw new Exception(oDo.GetError);
                                sSQL2 = new StringBuilder();
                            }
                        }
                        if (sSQL2.Length > 0)
                        {
                            EventLog.LogEvent(sSQL2.ToString(), FUNCTIONNAME, String.Empty);
                            if (!(oDo.ExecuteNonQuery(sSQL2.ToString()) > 0))
                                throw new Exception(oDo.GetError);
                        }
                        ResetThis();
                    }
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static Boolean FindChildren(SiteModel Details)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::FindChildren]";
                DataTable dt = null;
                try
                {
                    String CacheItem = "LiquidCore.Sites.Site.Children(" + Details.Id.ToString() + ")";
                    if (HttpContext.Current != null) if (HttpContext.Current != null) dt = (DataTable)HttpContext.Current.Cache[CacheItem];
                    if (dt == null)
                    {
                        StringBuilder sSQL = new StringBuilder();
                        sSQL.Append("SELECT sit_id FROM sit_sites WHERE sit_deleted = 0 AND sit_parentid = " + Details.Id.ToString());
                        using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true))
                        {
                            EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                            dt = oDo.GetDataTable(sSQL.ToString());
                        }
                        CacheData.Insert(CacheItem, dt);
                    }
                    if (dt.Rows.Count > 0)
                        return true;

                    return false;
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                    return false;
                }
            }
            public static void AddAuthorizedRole(SiteModel Details, Int32 RolId)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::AddAuthorizedRole]";
                try
                {
                    StringBuilder sSQL = new StringBuilder();
                    sSQL.Append("INSERT INTO asr_authorizedsitesroles (sta_id, lng_id, sit_id, rol_id, asr_createddate, asr_createdby, ");
                    sSQL.Append("asr_updateddate, asr_updatedby, asr_hidden, asr_deleted) VALUES ( ");
                    sSQL.Append(Details.Status.ToString() + ", ");
                    sSQL.Append(Details.Language.ToString() + ", ");
                    sSQL.Append(Details.Id.ToString() + ", ");
                    sSQL.Append(RolId.ToString() + ", ");
                    sSQL.Append("'" + DateTime.Now.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "', ");
                    sSQL.Append("'" + Authentication.User.Identity.Name + "', ");
                    sSQL.Append("'" + DateTime.Now.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "', ");
                    sSQL.Append("'" + Authentication.User.Identity.Name + "', ");
                    sSQL.Append("0, ");
                    sSQL.Append("0)");
                    using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, true))
                    {
                        EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                        if (oDo.ExecuteNonQuery(sSQL.ToString()).Equals(-1))
                            throw new Exception(oDo.GetError);
                    }
                    ResetThis();
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void DeleteAuthorizedRole(SiteModel Details, Int32 RolId)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::DeleteAuthorizedRole]";
                try
                {
                    System.Data.DataSet ds = new System.Data.DataSet();
                    StringBuilder sSQL = new StringBuilder();
                    sSQL.Append("UPDATE asr_authorizedsitesroles SET asr_deleted = 1 WHERE sit_id = " + Details.Id.ToString() + " AND rol_id = " + RolId.ToString());
                    using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, true))
                    {
                        EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                        if (oDo.ExecuteNonQuery(sSQL.ToString()) < 1)
                            throw new Exception(oDo.GetError);
                    }
                    ResetThis();
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            private static Int32[] GetAuthorizedRoles(Int32 Id)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::GetAuthorizedRoles]";
                Int32[] Roles;
                DataTable dt = null;
                try
                {
                    String CacheItem = "LiquidCore.Sites.Site.GetAuthorizedRoles(" + Id.ToString() + ")";
                    if (HttpContext.Current != null) if (HttpContext.Current != null) dt = (DataTable)HttpContext.Current.Cache[CacheItem];
                    if (dt == null)
                    {
                        StringBuilder sSQL = new StringBuilder();
                        sSQL.Append("SELECT rol_id FROM asr_authorizedsitesroles WHERE sit_id = " + Id.ToString() + " AND asr_deleted = 0");
                        using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true))
                        {
                            EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                            dt = oDo.GetDataTable(sSQL.ToString());
                        }
                        CacheData.Insert(CacheItem, dt);
                    }
                    Roles = new Int32[dt.Rows.Count];
                    for (Int32 i = 0; i < dt.Rows.Count; i++)
                        Roles[i] = Convert.ToInt32(dt.Rows[i]["rol_id"]);
                    return Roles;
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                    return new Int32[0];
                }
            }
        }
        public static class SettingData
        {
            static string CLASSNAME = "[Namespace::LiquidCore][Class::SettingData]";

            static Int32 DataBlockSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Data.DataBlockSize"].ToString());
            static Int32 UseRevision = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Data.UseRevision"].ToString());

            public static void Create(SettingModel Details)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::Create]";
                try
                {
                    ((SettingModel)Details).Order = (Int32)OrderMinMax.Max;
                    ((SettingModel)Details).UpdatedDate = DateTime.Now;
                    ((SettingModel)Details).UpdatedBy = Authentication.User.Identity.Name;
                    ((SettingModel)Details).CreatedDate = DateTime.Now;
                    ((SettingModel)Details).CreatedBy = Authentication.User.Identity.Name;

                    System.Data.DataSet ds = new System.Data.DataSet();
                    StringBuilder sSQL = new StringBuilder();
                    sSQL.Append("INSERT INTO set_settings (sta_id, lng_id, set_pointer, sit_id, pag_id, mod_id, set_parentid, set_order, set_title, ");
                    sSQL.Append("set_alias, set_description, set_value, set_createddate, set_createdby, ");
                    sSQL.Append("set_updateddate, set_updatedby, set_hidden, set_deleted) VALUES ( ");
                    sSQL.Append(Details.Status.ToString() + ", ");
                    sSQL.Append(Details.Language.ToString() + ", ");
                    sSQL.Append(Details.Pointer.ToString() + ", ");
                    sSQL.Append(Details.SitId.ToString() + ", ");
                    sSQL.Append(Details.PagId.ToString() + ", ");
                    sSQL.Append(Details.ModId.ToString() + ", ");
                    sSQL.Append(Details.ParentId.ToString() + ", ");
                    sSQL.Append(Details.Order.ToString() + ", ");
                    sSQL.Append("'" + Details.Title + "', ");
                    sSQL.Append("'" + Details.Alias + "', ");
                    sSQL.Append("'" + Details.Description + "', ");
                    sSQL.Append("'" + Details.Value + "', ");
                    sSQL.Append("'" + Details.CreatedDate.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "', ");
                    sSQL.Append("'" + Details.CreatedBy + "', ");
                    sSQL.Append("'" + Details.UpdatedDate.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "', ");
                    sSQL.Append("'" + Details.UpdatedBy + "', ");
                    sSQL.Append("" + (Details.Hidden ? "1" : "0") + ", ");
                    sSQL.Append("" + (Details.Deleted ? "1" : "0") + ")");
                    using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, true))
                    {
                        EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                        oDo.ExecuteNonQuery(sSQL.ToString());
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);

                        EventLog.LogEvent("SELECT @@IDENTITY", FUNCTIONNAME, String.Empty);
                        ((SettingModel)Details).Id = Convert.ToInt32(oDo.GetDataTable("SELECT @@IDENTITY").Rows[0][0]);
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);

                        //EventLog.LogEvent("CALL sort_set_settings();", FUNCTIONNAME, String.Empty);
                        //oDo.ExecuteNonQuery("CALL sort_set_settings();");
                        //if (oDo.HasError)
                        //    throw new Exception(oDo.GetError);

                    }
                    SortAll(Details.Pointer);

                    CacheData.Reset("LiquidCore.Settings.Setting.Id(" + Details.Id.ToString() + ")");
                    CacheData.Reset("LiquidCore.Settings.Setting.Alias(" + Details.Alias + ")");
                    CacheData.Reset("LiquidCore.Settings.PrimaryKeys(SitId(" + Details.SitId.ToString() + "), PagId(" + Details.PagId.ToString() + "), ModId(" + Details.ModId.ToString() + "), ParentId(" + Details.ParentId.ToString() + "))");
                    CacheData.Reset("LiquidCore.Settings.PrimaryKey(Alias(" + Details.Alias + "))");
                    CacheData.Reset("LiquidCore.Settings.PrimaryKey(Pointer(" + Details.Pointer.ToString() + "))");
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void Load(ModelObject Details, NameValueSet DataFields)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::Load]";
                try
                {
                    StringBuilder sSQL = new StringBuilder();
                    String CacheItem = String.Empty;
                    String Id = String.Empty;
                    String Alias = String.Empty;
					bool fromDr = false;
					if (DataFields["dr"] != null)
					{
						DataRow dr = (DataRow)DataFields["dr"];
						Id = dr["set_id"].ToString();
						CacheItem = "LiquidCore.Settings.Setting.Id(" + Id + ")";
						fromDr = true;
					}
					else if (DataFields["Id"] != null)
					{
						Id = DataFields["Id"].ToString();
						CacheItem = "LiquidCore.Settings.Setting.Id(" + Id + ")";
						sSQL.Append("SELECT * FROM set_settings WHERE set_id = " + Id + " AND set_deleted = 0");
					}
					else if (DataFields["Alias"] != null)
					{
						Alias = DataFields["Alias"].ToString();
						CacheItem = "LiquidCore.Settings.Setting.Alias(" + Alias + ")";
						sSQL.Append("SELECT * FROM set_settings WHERE set_alias = '" + Alias + "' AND set_deleted = 0");
					}
					else
						throw new Exception("Bad programmer !!!");

					System.Data.DataTable dt = null;
                    if (HttpContext.Current != null) if (HttpContext.Current != null) dt = (DataTable)HttpContext.Current.Cache[CacheItem];
                    if (dt == null)
                    {
						if (fromDr)
						{
							dt = CreateDt();
							dt.ImportRow((DataRow)DataFields["dr"]);
						}
						else
						{
							using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true))
							{
								EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
								dt = oDo.GetDataTable(sSQL.ToString());
								if (oDo.HasError)
									throw new Exception(oDo.GetError);
							}
						}
                        CacheData.Insert(CacheItem, dt);
                    }
                    if (dt.Rows.Count > 0)
                    {
                        System.Data.DataRow dr = dt.Rows[0];
                        ((SettingModel)Details).Id = Convert.ToInt32(dr["set_id"]);
                        ((SettingModel)Details).Status = Convert.ToInt32(dr["sta_id"]);
                        ((SettingModel)Details).Language = Convert.ToInt32(dr["lng_id"]);
                        ((SettingModel)Details).SitId = Convert.ToInt32(dr["sit_id"]);
                        ((SettingModel)Details).PagId = Convert.ToInt32(dr["pag_id"]);
                        ((SettingModel)Details).ModId = Convert.ToInt32(dr["mod_id"]);
                        ((SettingModel)Details).Pointer = Convert.ToInt32(dr["set_pointer"]);
                        ((SettingModel)Details).ParentId = Convert.ToInt32(dr["set_parentid"]);
                        ((SettingModel)Details).Order = Convert.ToInt32(dr["set_order"]);
                        ((SettingModel)Details).Title = Convert.ToString(dr["set_title"]);
                        ((SettingModel)Details).Alias = Convert.ToString(dr["set_alias"]);
                        ((SettingModel)Details).Description = Convert.ToString(dr["set_description"]);
                        ((SettingModel)Details).Value = Convert.ToString(dr["set_value"]);
                        ((SettingModel)Details).CreatedDate = Convert.ToDateTime(dr["set_createddate"]);
                        ((SettingModel)Details).CreatedBy = Convert.ToString(dr["set_createdby"]);
                        ((SettingModel)Details).UpdatedDate = Convert.ToDateTime(dr["set_updateddate"]);
                        ((SettingModel)Details).UpdatedBy = Convert.ToString(dr["set_updatedby"]);
                        ((SettingModel)Details).Hidden = (dr["set_hidden"].ToString().Equals("0") ? false : true);
                        ((SettingModel)Details).Deleted = (dr["set_deleted"].ToString().Equals("0") ? false : true);
                    }
                    else
                        ((SettingModel)Details).Deleted = true;
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
			private static DataTable CreateDt()
			{
				string FUNCTIONNAME = CLASSNAME + "[Function::CreateDt]";
				try
				{
					DataTable dt = new DataTable();
					dt.Columns.Add("set_id");
					dt.Columns.Add("sta_id");
					dt.Columns.Add("lng_id");
					dt.Columns.Add("sit_id");
					dt.Columns.Add("pag_id");
					dt.Columns.Add("mod_id");
					dt.Columns.Add("set_pointer");
					dt.Columns.Add("set_parentid");
					dt.Columns.Add("set_order");
					dt.Columns.Add("set_revision");
					dt.Columns.Add("set_title");
					dt.Columns.Add("set_alias");
					dt.Columns.Add("set_description");
					dt.Columns.Add("set_value");
					dt.Columns.Add("set_createddate");
					dt.Columns.Add("set_createdby");
					dt.Columns.Add("set_updateddate");
					dt.Columns.Add("set_updatedby");
					dt.Columns.Add("set_hidden");
					dt.Columns.Add("set_deleted");
					return dt;
				}
				catch (Exception ex)
				{
					Error.Report(ex, FUNCTIONNAME, "");
					return null;
				}
			}
            public static void LoadAll(ref List<Setting> _settings)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::LoadAll]";
                try
                {
                    DataTable dt = null;
                    String CacheItem = "LiquidCore.Settings";
                    if (HttpContext.Current != null) if (HttpContext.Current != null) dt = (DataTable)HttpContext.Current.Cache[CacheItem];
                    if (dt == null)
                    {
                        StringBuilder sSQL = new StringBuilder();
                        sSQL.Append("SELECT * FROM set_settings WHERE set_deleted = 0 ORDER BY sta_id, ");
                        sSQL.Append("lng_id, set_parentid, set_order");
                        using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true))
                        {
                            EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                            dt = oDo.GetDataTable(sSQL.ToString());
                            if (oDo.HasError)
                                throw new Exception(oDo.GetError);
                        }
                        CacheData.Insert(CacheItem, dt);
                    }
                    foreach (DataRow dr in dt.Rows)
                    {
                        Setting s = new Setting(dr);
                        _settings.Add(s);
                    }
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void LoadAll(ref List<Setting> _settings, NameValueSet DataFields)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::LoadAll]";
                try
                {
                    String PrimaryKey = String.Empty;
                    String CacheItem = String.Empty;
                    StringBuilder sSQL = new StringBuilder();
                    if (DataFields["SitId"] != null && DataFields["PagId"] != null && DataFields["ModId"] != null && DataFields["ParentId"] != null)
                    {
                        String SitId = DataFields["SitId"].ToString();
                        String PagId = DataFields["PagId"].ToString();
                        String ModId = DataFields["ModId"].ToString();
                        String ParentId = DataFields["ParentId"].ToString();
                        CacheItem = "LiquidCore.Settings.PrimaryKeys(SitId(" + SitId + "), PagId(" + PagId + "), ModId(" + ModId + "), ParentId(" + ParentId + "))";
                        sSQL.Append("SELECT * FROM set_settings WHERE sit_id = " + SitId + " AND pag_id = " + PagId + " AND mod_id = " + ModId + " AND set_parentid = " + ParentId + " AND set_deleted = 0 ORDER BY sta_id, lng_id, set_parentid, set_order");
                    }
                    else if (DataFields["Alias"] != null)
                    {
                        PrimaryKey = DataFields["Alias"].ToString();
                        CacheItem = "LiquidCore.Settings.PrimaryKey(Alias(" + PrimaryKey + "))";
                        sSQL.Append("SELECT * FROM set_settings WHERE set_alias = '" + PrimaryKey + "' AND set_deleted = 0 ORDER BY sta_id, lng_id, set_parentid, set_order");
                    }
                    else if (DataFields["Pointer"] != null)
                    {
                        PrimaryKey = DataFields["Pointer"].ToString();
                        CacheItem = "LiquidCore.Settings.PrimaryKey(Pointer(" + PrimaryKey + "))";
                        sSQL.Append("SELECT * FROM set_settings WHERE set_pointer = " + PrimaryKey + " AND set_deleted = 0 ORDER BY sta_id, lng_id, set_parentid, set_order");
                    }
                    else
                    {
                        LoadAll(ref _settings);
                        return;
                    }
                    DataTable dt = null;
                    if (HttpContext.Current != null) if (HttpContext.Current != null) dt = (DataTable)HttpContext.Current.Cache[CacheItem];
                    if (dt == null)
                    {
                        using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true))
                        {
                            EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                            dt = oDo.GetDataTable(sSQL.ToString());
                            if (oDo.HasError)
                                throw new Exception(oDo.GetError);
                        }
                        CacheData.Insert(CacheItem, dt);
                    }
                    foreach (DataRow dr in dt.Rows)
                    {
                        Setting s = new Setting(dr);
                        _settings.Add(s);
                    }
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void Update(SettingModel Details)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::Update]";
                try
                {
                    ((SettingModel)Details).UpdatedDate = DateTime.Now;
                    ((SettingModel)Details).UpdatedBy = Authentication.User.Identity.Name;

                    System.Data.DataSet ds = new System.Data.DataSet();
                    StringBuilder sSQL = new StringBuilder();
                    sSQL.Append("UPDATE set_settings SET ");
                    sSQL.Append("sta_id = " + Details.Status.ToString() + ", ");
                    sSQL.Append("lng_id = " + Details.Language.ToString() + ", ");
                    sSQL.Append("set_pointer = " + Details.Pointer.ToString() + ", ");
                    sSQL.Append("sit_id = " + Details.SitId.ToString() + ", ");
                    sSQL.Append("pag_id = " + Details.PagId.ToString() + ", ");
                    sSQL.Append("mod_id = " + Details.ModId.ToString() + ", ");
                    sSQL.Append("set_parentid = " + Details.ParentId.ToString() + ", ");
                    sSQL.Append("set_order = " + Details.Order.ToString() + ", ");
                    sSQL.Append("set_title = '" + Details.Title + "', ");
                    sSQL.Append("set_alias = '" + Details.Alias + "', ");
                    sSQL.Append("set_description = '" + Details.Description + "', ");
                    sSQL.Append("set_value = '" + Details.Value + "', ");
                    sSQL.Append("set_createddate = '" + Details.CreatedDate.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "', ");
                    sSQL.Append("set_createdby = '" + Details.CreatedBy + "', ");
                    sSQL.Append("set_updateddate = '" + Details.UpdatedDate.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "', ");
                    sSQL.Append("set_updatedby = '" + Details.UpdatedBy + "', ");
                    sSQL.Append("set_hidden = " + (!Details.Hidden ? "0" : "1") + ", ");
                    sSQL.Append("set_deleted = " + (!Details.Deleted ? "0" : "1") + " ");
                    sSQL.Append("WHERE set_id = " + Details.Id.ToString());
                    using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, true))
                    {
                        EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                        oDo.ExecuteNonQuery(sSQL.ToString());
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);

                        //EventLog.LogEvent("CALL sort_set_settings();", FUNCTIONNAME, String.Empty);
                        //oDo.ExecuteNonQuery("CALL sort_set_settings();");
                        //if (oDo.HasError)
                        //    throw new Exception(oDo.GetError);

                    }
                    SortAll(Details.Pointer);
                    CacheData.Reset("LiquidCore.Settings.Setting.Id(" + Details.Id.ToString() + ")");
                    CacheData.Reset("LiquidCore.Settings.Setting.Alias(" + Details.Alias + ")");
                    CacheData.Reset("LiquidCore.Settings.PrimaryKeys(SitId(" + Details.SitId.ToString() + "), PagId(" + Details.PagId.ToString() + "), ModId(" + Details.ModId.ToString() + "), ParentId(" + Details.ParentId.ToString() + "))");
                    CacheData.Reset("LiquidCore.Settings.PrimaryKey(Alias(" + Details.Alias + "))");
                    CacheData.Reset("LiquidCore.Settings.PrimaryKey(Pointer(" + Details.Pointer.ToString() + "))");
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void Delete(SettingModel Details)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::Delete]";
                try
                {
                    RecursiveDelete(Details.Id);
                    //using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, false))
                    //{
                    //    EventLog.LogEvent("CALL sort_set_settings();", FUNCTIONNAME, String.Empty);
                    //    oDo.ExecuteNonQuery("CALL sort_set_settings();");
                    //    if (oDo.HasError)
                    //        throw new Exception(oDo.GetError);
                    //}
                    SortAll(Details.Pointer);
                    CacheData.Reset("LiquidCore.Settings.Setting.Id(" + Details.Id.ToString() + ")");
                    CacheData.Reset("LiquidCore.Settings.Setting.Alias(" + Details.Alias + ")");
                    CacheData.Reset("LiquidCore.Settings.PrimaryKeys(SitId(" + Details.SitId.ToString() + "), PagId(" + Details.PagId.ToString() + "), ModId(" + Details.ModId.ToString() + "), ParentId(" + Details.ParentId.ToString() + "))");
                    CacheData.Reset("LiquidCore.Settings.PrimaryKey(Alias(" + Details.Alias + "))");
                    CacheData.Reset("LiquidCore.Settings.PrimaryKey(Pointer(" + Details.Pointer.ToString() + "))");
                    Details = null;
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void RecursiveDelete(Int32 Id)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::RecursiveDelete]";
                try
                {
                    if (Id.Equals(0))
                        return;

                    using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, true))
                    {
                        String sSQL1 = "UPDATE set_settings SET set_deleted = 1 WHERE set_id = " + Id.ToString();

                        EventLog.LogEvent(sSQL1, FUNCTIONNAME, String.Empty);
                        System.Diagnostics.Debug.WriteLine(sSQL1);
                        oDo.ExecuteNonQuery(sSQL1);
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);
                    }
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void ResetThis()
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::ResetThis]";
                try
                {
                    //CacheData.Reset("LiquidCore.Settings.Setting.Id(" + Details.Id.ToString() + ")");
                    //CacheData.Reset("LiquidCore.Settings.Setting.Alias(" + Alias + ")");
                    //CacheData.Reset("LiquidCore.Settings.PrimaryKeys(SitId(" + Details.SitId.ToString() + "), PagId(" + Details.PagId.ToString() + "), ModId(" + Details.ModId.ToString() + "), ParentId(" + Details.ParentId.ToString() + "))");
                    //CacheData.Reset("LiquidCore.Settings.PrimaryKey(Alias(" + Details.Alias + "))");
                    //CacheData.Reset("LiquidCore.Settings.PrimaryKey(Pointer(" + Details.Pointer.ToString() + "))");
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            //[ObsoleteAttribute("SortAll has been deprecated. All sorting take place in stored procedures instead.")]
            public static void SortAll(Int32 Pointer)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::SortAll]";
                try
                {
                    Int32 Order = (Int32)OrderMinMax.Min;
                    StringBuilder sSQL = new StringBuilder();
                    sSQL.Append("SELECT set_id FROM set_settings WHERE set_pointer = " + Pointer.ToString() + " AND set_deleted = 0 ORDER BY sta_id, lng_id, set_parentid, set_order");
                    using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, false))
                    {
                        EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                        DataTable dt = oDo.GetDataTable(sSQL.ToString());
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);
                        StringBuilder sSQL2 = new StringBuilder();
                        foreach (DataRow dr in dt.Rows)
                        {
                            sSQL2.AppendLine("UPDATE set_settings SET set_order = " + Order.ToString() + " WHERE set_id = " + dr["set_id"].ToString() + ";");
                            Order = Order + (Int32)OrderMinMax.Step;
                            if (sSQL2.Length > DataBlockSize)
                            {
                                EventLog.LogEvent(sSQL2.ToString(), FUNCTIONNAME, String.Empty);
                                if (!(oDo.ExecuteNonQuery(sSQL2.ToString()) > 0))
                                    throw new Exception(oDo.GetError);
                                sSQL2 = new StringBuilder();
                            }
                        }
                        if (sSQL2.Length > 0)
                        {
                            EventLog.LogEvent(sSQL2.ToString(), FUNCTIONNAME, String.Empty);
                            if (!(oDo.ExecuteNonQuery(sSQL2.ToString()) > 0))
                                throw new Exception(oDo.GetError);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static Boolean FindChildren(SettingModel Details)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::FindChildren]";
                DataTable dt = null;
                try
                {
                    String CacheItem = "LiquidCore.Settings.Setting.Children(" + Details.Id.ToString() + ")";
                    if (HttpContext.Current != null) if (HttpContext.Current != null) dt = (DataTable)HttpContext.Current.Cache[CacheItem];
                    if (dt == null)
                    {
                        StringBuilder sSQL = new StringBuilder();
                        sSQL.Append("SELECT set_id FROM set_settings WHERE set_deleted = 0 AND set_parentid = " + Details.Id.ToString());
                        using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true))
                        {
                            EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                            dt = oDo.GetDataTable(sSQL.ToString());
                        }
                        CacheData.Insert(CacheItem, dt);
                    }
                    if (dt.Rows.Count > 0)
                        return true;

                    return false;
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                    return false;
                }
            }
        }
        public static class PageData
        {
            static string CLASSNAME = "[Namespace::LiquidCore][Class::PageData]";
            
            static Int32 DataBlockSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Data.DataBlockSize"].ToString());
            static Int32 UseRevision = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Data.UseRevision"].ToString());

            public static void Create(PageModel Details)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::Create]";
                try
                {
                    ((PageModel)Details).Order = (Int32)OrderMinMax.Max; 
                    ((PageModel)Details).UpdatedDate = DateTime.Now;
                    ((PageModel)Details).UpdatedBy = Authentication.User.Identity.Name;
                    ((PageModel)Details).CreatedDate = DateTime.Now;
                    ((PageModel)Details).CreatedBy = Authentication.User.Identity.Name;  

                    System.Data.DataSet ds = new System.Data.DataSet();
                    StringBuilder sSQL = new StringBuilder();
                    sSQL.Append("INSERT INTO pag_pages (sit_id, sta_id, lng_id, mdl_id, pag_parentid, pag_order, pag_title, ");
                    sSQL.Append("pag_alias, pag_description, pag_template, pag_createddate, pag_createdby, ");
                    sSQL.Append("pag_updateddate, pag_updatedby, pag_hidden, pag_deleted) VALUES ( ");
                    sSQL.Append(Details.SitId.ToString() + ", ");
                    sSQL.Append(Details.Status.ToString() + ", ");
                    sSQL.Append(Details.Language.ToString() + ", ");
                    sSQL.Append(Details.ModelId.ToString() + ", ");
                    sSQL.Append(Details.ParentId.ToString() + ", ");
                    sSQL.Append(Details.Order.ToString() + ", ");
                    sSQL.Append("'" + Details.Title + "', ");
                    sSQL.Append("'" + Details.Alias + "', ");
                    sSQL.Append("'" + Details.Description + "', ");
                    sSQL.Append("'" + Details.Template + "', ");
                    sSQL.Append("'" + Details.CreatedDate.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "', ");
                    sSQL.Append("'" + Details.CreatedBy + "', ");
                    sSQL.Append("'" + Details.UpdatedDate.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "', ");
                    sSQL.Append("'" + Details.UpdatedBy + "', ");
                    sSQL.Append("" + (Details.Hidden ? "1" : "0") + ", ");
                    sSQL.Append("" + (Details.Deleted ? "1" : "0") + ")");
                    using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, true))
                    {
                        EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                        oDo.ExecuteNonQuery(sSQL.ToString());
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);

                        EventLog.LogEvent("SELECT @@IDENTITY", FUNCTIONNAME, String.Empty);
                        ((PageModel)Details).Id = Convert.ToInt32(oDo.GetDataTable("SELECT @@IDENTITY").Rows[0][0]);
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);

                        //EventLog.LogEvent("CALL sort_pag_pages();", FUNCTIONNAME, String.Empty);
                        //oDo.ExecuteNonQuery("CALL sort_pag_pages();");
                        //if (oDo.HasError)
                        //    throw new Exception(oDo.GetError);
                    }
                    SortAll(Details.Status, Details.Language, Details.ParentId);
                    CacheData.Reset("LiquidCore.Pages.PrimaryKeys(SitId(" + Details.SitId.ToString() + "), ParentId(" + Details.ParentId.ToString() + "))");
                    CacheData.Reset("LiquidCore.Pages.PrimaryKey(Alias(" + Details.Alias + "))");

                    sSQL = new StringBuilder();
                    sSQL.Append("SELECT pag_id FROM pag_pages WHERE sit_id = " + Details.SitId.ToString() + " AND pag_parentid = " + Details.ParentId.ToString() + " AND pag_deleted = 0");
                    using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, false))
                    {
                        DataTable dt = oDo.GetDataTable(sSQL.ToString());
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);
                        foreach (DataRow dr in dt.Rows)
                            CacheData.Reset("LiquidCore.Pages.Page.Id(" + dr["pag_id"].ToString() + ")");
                    }
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void Load(ModelObject Details, NameValueSet DataFields)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::Load]";
                try
                {
                    System.Data.DataTable dt = null;
                    String CacheItem = "LiquidCore.Pages.Page.Id(" + DataFields["Id"].ToString() + ")";
                    if (HttpContext.Current != null) if (HttpContext.Current != null) dt = (DataTable)HttpContext.Current.Cache[CacheItem];
                    if (dt == null)
                    {
                        StringBuilder sSQL = new StringBuilder();
                        sSQL.Append("SELECT * FROM pag_pages WHERE pag_id = " + DataFields["Id"].ToString() + " AND pag_deleted = 0");
                        using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true))
                        {
                            EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                            dt = oDo.GetDataTable(sSQL.ToString());
                            if (oDo.HasError)
                                throw new Exception(oDo.GetError);
                        }
                        CacheData.Insert(CacheItem, dt);  
                    }
                    if (dt.Rows.Count > 0)
                    {
                        System.Data.DataRow dr = dt.Rows[0];
                        ((PageModel)Details).Id = Convert.ToInt32(dr["pag_id"]);
                        ((PageModel)Details).SitId = Convert.ToInt32(dr["sit_id"]);
                        ((PageModel)Details).Status = Convert.ToInt32(dr["sta_id"]);
                        ((PageModel)Details).Language = Convert.ToInt32(dr["lng_id"]);
                        ((PageModel)Details).ModelId = Convert.ToInt32(dr["mdl_id"]);
                        ((PageModel)Details).ParentId = Convert.ToInt32(dr["pag_parentid"]);
                        ((PageModel)Details).Order = Convert.ToInt32(dr["pag_order"]);
                        ((PageModel)Details).Title = Convert.ToString(dr["pag_title"]);
                        ((PageModel)Details).Alias = Convert.ToString(dr["pag_alias"]);
                        ((PageModel)Details).Description = Convert.ToString(dr["pag_description"]);
                        ((PageModel)Details).AuthorizedRoles = GetAuthorizedRoles(Convert.ToInt32(DataFields["Id"]));
                        ((PageModel)Details).Template = Convert.ToString(dr["pag_template"]);
                        ((PageModel)Details).CreatedDate = Convert.ToDateTime(dr["pag_createddate"]);
                        ((PageModel)Details).CreatedBy = Convert.ToString(dr["pag_createdby"]);
                        ((PageModel)Details).UpdatedDate = Convert.ToDateTime(dr["pag_updateddate"]);
                        ((PageModel)Details).UpdatedBy = Convert.ToString(dr["pag_updatedby"]);
                        ((PageModel)Details).Hidden = (dr["pag_hidden"].ToString().Equals("0") ? false : true);
                        ((PageModel)Details).Deleted = (dr["pag_deleted"].ToString().Equals("0") ? false : true);
                    }
                    else
                        ((PageModel)Details).Deleted = true;
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void LoadAll(ref List<Page> _pages)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::LoadAll]";
                try
                {
                    DataTable dt = null;
                    String CacheItem = "LiquidCore.Pages";
                    if (HttpContext.Current != null) if (HttpContext.Current != null) dt = (DataTable)HttpContext.Current.Cache[CacheItem];
                    if (dt == null)
                    {
                        StringBuilder sSQL = new StringBuilder();
                        sSQL.Append("SELECT pag_id FROM pag_pages WHERE pag_deleted = 0 ORDER BY sit_id, sta_id, ");
                        sSQL.Append("lng_id, pag_parentid, pag_order");
                        using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true))
                        {
                            EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                            dt = oDo.GetDataTable(sSQL.ToString());
                            if (oDo.HasError)
                                throw new Exception(oDo.GetError);
                        }
                        CacheData.Insert(CacheItem, dt);  
                    }
                    foreach (DataRow dr in dt.Rows)
                    {
                        Page p = new Page(Convert.ToInt32(dr["pag_id"].ToString()));
                        _pages.Add(p);
                    }          
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void LoadAll(ref List<Page> _pages, NameValueSet DataFields)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::LoadAll]";
                try
                {
                    String PrimaryKey = String.Empty;
                    String CacheItem = String.Empty;
                    StringBuilder sSQL = new StringBuilder();
                    if (DataFields["SitId"] != null && DataFields["ParentId"] != null)
                    {
                        String SitId = DataFields["SitId"].ToString();
                        String ParentId = DataFields["ParentId"].ToString();
                        CacheItem = "LiquidCore.Pages.PrimaryKeys(SitId(" + SitId + "), ParentId(" + ParentId + "))";
                        sSQL.Append("SELECT * FROM pag_pages WHERE sit_id = " + SitId + " AND pag_parentid = " + ParentId + " AND pag_deleted = 0 ORDER BY sit_id, sta_id, lng_id, pag_parentid, pag_order");
                    }
                    else if (DataFields["Alias"] != null)
                    {
                        PrimaryKey = DataFields["Alias"].ToString();
                        CacheItem = "LiquidCore.Pages.PrimaryKey(Alias(" + PrimaryKey + "))";
                        sSQL.Append("SELECT * FROM pag_pages WHERE pag_alias = '" + PrimaryKey + "' AND pag_deleted = 0 ORDER BY sit_id, sta_id, lng_id, pag_parentid, pag_order");
                    }
                    else
                    {
                        LoadAll(ref _pages);
                        return;
                    }
                    DataTable dt = null;
                    if (HttpContext.Current != null) if (HttpContext.Current != null) dt = (DataTable)HttpContext.Current.Cache[CacheItem];
                    if (dt == null)
                    {
                        using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true))
                        {
                            EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                            dt = oDo.GetDataTable(sSQL.ToString());
                            if (oDo.HasError)
                                throw new Exception(oDo.GetError);
                        }
                        CacheData.Insert(CacheItem, dt);
                    }
                    foreach (DataRow dr in dt.Rows)
                    {
                        Page p = new Page(Convert.ToInt32(dr["pag_id"].ToString()));
                        _pages.Add(p);
                    }        
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void Update(PageModel Details)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::Update]";
                try
                {
                    ((PageModel)Details).UpdatedDate = DateTime.Now;
                    ((PageModel)Details).UpdatedBy = Authentication.User.Identity.Name;

                    System.Data.DataSet ds = new System.Data.DataSet();
                    StringBuilder sSQL = new StringBuilder();
                    sSQL.Append("UPDATE pag_pages SET ");
                    sSQL.Append("sit_id = " + Details.SitId.ToString() + ", ");
                    sSQL.Append("sta_id = " + Details.Status.ToString() + ", ");
                    sSQL.Append("lng_id = " + Details.Language.ToString() + ", ");
                    sSQL.Append("mdl_id = " + Details.ModelId.ToString() + ", ");
                    sSQL.Append("pag_parentid = " + Details.ParentId.ToString() + ", ");
                    sSQL.Append("pag_order = " + Details.Order.ToString() + ", ");
                    sSQL.Append("pag_title = '" + Details.Title + "', ");
                    sSQL.Append("pag_alias = '" + Details.Alias + "', ");
                    sSQL.Append("pag_description = '" + Details.Description + "', ");
                    sSQL.Append("pag_template = '" + Details.Template + "', ");
                    sSQL.Append("pag_createddate = '" + Details.CreatedDate.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "', ");
                    sSQL.Append("pag_createdby = '" + Details.CreatedBy + "', ");
                    sSQL.Append("pag_updateddate = '" + Details.UpdatedDate.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "', ");
                    sSQL.Append("pag_updatedby = '" + Details.UpdatedBy + "', ");
                    sSQL.Append("pag_hidden = " + (!Details.Hidden ? "0" : "1") + ", ");
                    sSQL.Append("pag_deleted = " + (!Details.Deleted ? "0" : "1") + " ");
                    sSQL.Append("WHERE pag_id = " + Details.Id.ToString());
                    using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, true))
                    {
                        EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                        oDo.ExecuteNonQuery(sSQL.ToString());
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);

                        //EventLog.LogEvent("CALL sort_pag_pages();", FUNCTIONNAME, String.Empty);
                        //oDo.ExecuteNonQuery("CALL sort_pag_pages();");
                        //if (oDo.HasError)
                        //    throw new Exception(oDo.GetError);

                    }
                    SortAll(Details.Status, Details.Language, Details.ParentId);
                    CacheData.Reset("LiquidCore.Pages.PrimaryKeys(SitId(" + Details.SitId.ToString() + "), ParentId(" + Details.ParentId.ToString() + "))");
                    CacheData.Reset("LiquidCore.Pages.PrimaryKey(Alias(" + Details.Alias + "))");

                    sSQL = new StringBuilder();
                    sSQL.Append("SELECT pag_id FROM pag_pages WHERE sit_id = " + Details.SitId.ToString() + " AND pag_parentid = " + Details.ParentId.ToString() + " AND pag_deleted = 0");
                    using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, false))
                    {
                        DataTable dt = oDo.GetDataTable(sSQL.ToString());
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);
                        foreach (DataRow dr in dt.Rows)
                            CacheData.Reset("LiquidCore.Pages.Page.Id(" + dr["pag_id"].ToString() + ")");
                    }
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void Delete(PageModel Details, NameValueSet DataFields)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::Delete]";
                try
                {
                    RecursiveDelete(Details.Id, DataFields["Recursive"]);
                    //using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, false))
                    //{
                    //    EventLog.LogEvent("CALL sort_pag_pages();", FUNCTIONNAME, String.Empty);
                    //    oDo.ExecuteNonQuery("CALL sort_pag_pages();");
                    //    if (oDo.HasError)
                    //        throw new Exception(oDo.GetError);

                    //    EventLog.LogEvent("CALL sort_mod_modules();", FUNCTIONNAME, String.Empty);
                    //    oDo.ExecuteNonQuery("CALL sort_mod_modules();");
                    //    if (oDo.HasError)
                    //        throw new Exception(oDo.GetError);

                    //    EventLog.LogEvent("CALL sort_obd_objectdata();", FUNCTIONNAME, String.Empty);
                    //    oDo.ExecuteNonQuery("CALL sort_obd_objectdata();");
                    //    if (oDo.HasError)
                    //        throw new Exception(oDo.GetError);

                    //    EventLog.LogEvent("CALL sort_set_settings();", FUNCTIONNAME, String.Empty);
                    //    oDo.ExecuteNonQuery("CALL sort_set_settings();");
                    //    if (oDo.HasError)
                    //        throw new Exception(oDo.GetError);
                    //}
                    SortAll(Details.Status, Details.Language, Details.ParentId);
                    CacheData.Reset("LiquidCore.Pages.PrimaryKeys(SitId(" + Details.SitId.ToString() + "), ParentId(" + Details.ParentId.ToString() + "))");
                    CacheData.Reset("LiquidCore.Pages.PrimaryKey(Alias(" + Details.Alias + "))");

                    StringBuilder sSQL = new StringBuilder();
                    sSQL.Append("SELECT pag_id FROM pag_pages WHERE sit_id = " + Details.SitId.ToString() + " AND pag_parentid = " + Details.ParentId.ToString() + " AND pag_deleted = 0");
                    using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, false))
                    {
                        DataTable dt = oDo.GetDataTable(sSQL.ToString());
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);
                        foreach (DataRow dr in dt.Rows)
                            CacheData.Reset("LiquidCore.Pages.Page.Id(" + dr["pag_id"].ToString() + ")");
                    }
                    Details = null;
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void RecursiveDelete(Int32 Id, System.Object recursive)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::RecursiveDelete]";
                try
                {
                    if (Id.Equals(0))
                        return;

                    DataTable dt1 = new DataTable();
                    DataTable dt2 = new DataTable();
                    using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, true))
                    {
                        String sSQL1 = "SELECT pag_id FROM pag_pages WHERE pag_parentid = " + Id.ToString();
                        String sSQL2 = "SELECT mod_id FROM mod_modules WHERE pag_id = " + Id.ToString();
                        String sSQL3 = "UPDATE apr_authorizedpagesroles SET apr_deleted = 1 WHERE pag_id = " + Id.ToString();
                        String sSQL4 = "UPDATE set_settings SET set_deleted = 1 WHERE pag_id = " + Id.ToString();
                        String sSQL5 = "UPDATE pag_pages SET pag_deleted = 1 WHERE pag_id = " + Id.ToString();

                        EventLog.LogEvent(sSQL1, FUNCTIONNAME, String.Empty);
                        System.Diagnostics.Debug.WriteLine(sSQL1);
                        dt1 = oDo.GetDataTable(sSQL1);
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);

                        EventLog.LogEvent(sSQL2, FUNCTIONNAME, String.Empty);
                        System.Diagnostics.Debug.WriteLine(sSQL2);
                        dt2 = oDo.GetDataTable(sSQL2);
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);

                        EventLog.LogEvent(sSQL3, FUNCTIONNAME, String.Empty);
                        System.Diagnostics.Debug.WriteLine(sSQL3);
                        oDo.ExecuteNonQuery(sSQL3);
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);

                        EventLog.LogEvent(sSQL4, FUNCTIONNAME, String.Empty);
                        System.Diagnostics.Debug.WriteLine(sSQL4);
                        oDo.ExecuteNonQuery(sSQL4);
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);

                        EventLog.LogEvent(sSQL5, FUNCTIONNAME, String.Empty);
                        System.Diagnostics.Debug.WriteLine(sSQL5);
                        oDo.ExecuteNonQuery(sSQL5);
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);
                    }
					if (recursive.Equals(true))
					{
						foreach (DataRow dr in dt1.Rows)
						{
							Int32 ParentId = 0;
							Int32.TryParse(dr["pag_id"].ToString(), out ParentId);
							RecursiveDelete(ParentId, true);
						}
					}
                    foreach (DataRow dr in dt2.Rows)
                    {
                        Int32 ParentId = 0;
                        Int32.TryParse(dr["mod_id"].ToString(), out ParentId);
                        LiquidCore.Data.ModuleData.RecursiveDelete(ParentId);
                    }
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void ResetThis()
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::ResetThis]";
                try
                {
                    
                        
                            
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            //[ObsoleteAttribute("SortAll has been deprecated. All sorting take place in stored procedures instead.")]
            public static void SortAll(Int32 Status, Int32 Language, Int32 ParentId)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::SortAll]";
                try
                {
                    Int32 Order = (Int32)OrderMinMax.Min;
                    StringBuilder sSQL = new StringBuilder();
                    sSQL.Append("SELECT pag_id FROM pag_pages WHERE sta_id = " + Status.ToString() + " AND lng_id = " + Language.ToString() + " AND pag_parentid = " + ParentId.ToString() + " AND pag_deleted = 0 ORDER BY sit_id, sta_id, lng_id, pag_parentid, pag_order");
                    using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, false))
                    {
                        EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                        DataTable dt = oDo.GetDataTable(sSQL.ToString());
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);
                        StringBuilder sSQL2 = new StringBuilder();
                        foreach (DataRow dr in dt.Rows)
                        {
                            sSQL2.AppendLine("UPDATE pag_pages SET pag_order = " + Order.ToString() + " WHERE pag_id = " + dr["pag_id"].ToString() + ";");
                            Order = Order + (Int32)OrderMinMax.Step;
                            if (sSQL2.Length > DataBlockSize)
                            {
                                EventLog.LogEvent(sSQL2.ToString(), FUNCTIONNAME, String.Empty);
                                if (!(oDo.ExecuteNonQuery(sSQL2.ToString()) > 0))
                                    throw new Exception(oDo.GetError);
                                sSQL2 = new StringBuilder();
                            }
                        }
                        if (sSQL2.Length > 0)
                        {
                            EventLog.LogEvent(sSQL2.ToString(), FUNCTIONNAME, String.Empty);
                            if (!(oDo.ExecuteNonQuery(sSQL2.ToString()) > 0))
                                throw new Exception(oDo.GetError);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static Boolean FindChildren(PageModel Details)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::FindChildren]";
                DataTable dt = null;
                try
                {
                    String CacheItem = "LiquidCore.Pages.Page.Children(" + Details.Id.ToString() + ")";
                    if (HttpContext.Current != null) if (HttpContext.Current != null) dt = (DataTable)HttpContext.Current.Cache[CacheItem];
                    if (dt == null)
                    {
                        StringBuilder sSQL = new StringBuilder();
                        sSQL.Append("SELECT pag_id FROM pag_pages WHERE pag_deleted = 0 AND pag_parentid = " + Details.Id.ToString());
                        using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true))
                        {
                            EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                            dt = oDo.GetDataTable(sSQL.ToString());
                        }
                        CacheData.Insert(CacheItem, dt);
                    }
                    if (dt.Rows.Count > 0)
                        return true;

                    return false;
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                    return false;
                }
            }
            public static void AddAuthorizedRole(PageModel Details, Int32 RolId)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::AddAuthorizedRole]";
                try
                {
                    StringBuilder sSQL = new StringBuilder();
                    sSQL.Append("INSERT INTO apr_authorizedpagesroles (sta_id, lng_id, pag_id, rol_id, apr_createddate, apr_createdby, ");
                    sSQL.Append("apr_updateddate, apr_updatedby, apr_hidden, apr_deleted) VALUES ( ");
                    sSQL.Append(Details.Status.ToString() + ", ");
                    sSQL.Append(Details.Language.ToString() + ", ");
                    sSQL.Append(Details.Id.ToString() + ", ");
                    sSQL.Append(RolId.ToString() + ", ");
                    sSQL.Append("'" + DateTime.Now.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "', ");
                    sSQL.Append("'" + Authentication.User.Identity.Name + "', ");
                    sSQL.Append("'" + DateTime.Now.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "', ");
                    sSQL.Append("'" + Authentication.User.Identity.Name + "', ");
                    sSQL.Append("0, ");
                    sSQL.Append("0)");
                    using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, true))
                    {
                        EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                        if (oDo.ExecuteNonQuery(sSQL.ToString()).Equals(-1))
                            throw new Exception(oDo.GetError);
                    }
                    CacheData.Reset("LiquidCore.Pages.Page.Id(" + Details.Id.ToString() + ")");
                    CacheData.Reset("LiquidCore.Pages.PrimaryKeys(SitId(" + Details.SitId.ToString() + "), ParentId(" + Details.ParentId.ToString() + "))");
                    CacheData.Reset("LiquidCore.Pages.PrimaryKey(Alias(" + Details.Alias + "))");
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void DeleteAuthorizedRole(PageModel Details, Int32 RolId)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::DeleteAuthorizedRole]";
                try
                {
                    System.Data.DataSet ds = new System.Data.DataSet();
                    StringBuilder sSQL = new StringBuilder();
                    sSQL.Append("UPDATE apr_authorizedpagesroles SET apr_deleted = 1 WHERE pag_id = " + Details.Id.ToString() + " AND rol_id = " + RolId.ToString());
                    using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, true))
                    {
                        EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                        if (oDo.ExecuteNonQuery(sSQL.ToString()) < 1)
                            throw new Exception(oDo.GetError);
                    }
                    CacheData.Reset("LiquidCore.Pages.Page.Id(" + Details.Id.ToString() + ")");
                    CacheData.Reset("LiquidCore.Pages.PrimaryKeys(SitId(" + Details.SitId.ToString() + "), ParentId(" + Details.ParentId.ToString() + "))");
                    CacheData.Reset("LiquidCore.Pages.PrimaryKey(Alias(" + Details.Alias + "))");
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            private static Int32[] GetAuthorizedRoles(Int32 Id)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::GetAuthorizedRoles]";
                Int32[] Roles;
                DataTable dt = null;
                try
                {
                    String CacheItem = "LiquidCore.Pages.Page.GetAuthorizedRoles(" + Id.ToString() + ")";
                    if (HttpContext.Current != null) if (HttpContext.Current != null) dt = (DataTable)HttpContext.Current.Cache[CacheItem];
                    if (dt == null)
                    {
                        StringBuilder sSQL = new StringBuilder();
                        sSQL.Append("SELECT rol_id FROM apr_authorizedpagesroles WHERE pag_id = " + Id.ToString() + " AND apr_deleted = 0");
                        using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true))
                        {
                            EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                            dt = oDo.GetDataTable(sSQL.ToString());
                        }
                        CacheData.Insert(CacheItem, dt);
                    }
                    Roles = new Int32[dt.Rows.Count];
                    for (Int32 i = 0; i < dt.Rows.Count; i++)
                        Roles[i] = Convert.ToInt32(dt.Rows[i]["rol_id"]);
                    return Roles;
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                    return new Int32[0];
                }
            }
        }
        public static class ModuleData
        {
            static string CLASSNAME = "[Namespace::LiquidCore][Class::ModuleData]";

            static Int32 DataBlockSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Data.DataBlockSize"].ToString());
            static Int32 UseRevision = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Data.UseRevision"].ToString());

            public static void Create(ModuleModel Details)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::Create]";
                try
                {
                    ((ModuleModel)Details).Order = (Int32)OrderMinMax.Max;
                    ((ModuleModel)Details).UpdatedDate = DateTime.Now;
                    ((ModuleModel)Details).UpdatedBy = Authentication.User.Identity.Name;
                    ((ModuleModel)Details).CreatedDate = DateTime.Now;
                    ((ModuleModel)Details).CreatedBy = Authentication.User.Identity.Name;

                    System.Data.DataSet ds = new System.Data.DataSet();
                    StringBuilder sSQL1 = new StringBuilder();
                    sSQL1.Append("INSERT INTO mod_modules (sit_id, pag_id, mde_id, sta_id, lng_id, mod_parentid, mod_order, mod_mobileorder, mod_revision, mod_title, ");
                    sSQL1.Append("mod_alias, mod_description, mod_src, mod_contentpane, mod_allpages, mod_ssl, mod_cachetime, mod_createddate, mod_createdby, ");
                    sSQL1.Append("mod_updateddate, mod_updatedby, mod_hidden, mod_mobilehidden, mod_deleted) VALUES ( ");
                    sSQL1.Append(Details.SitId.ToString() + ", ");
                    sSQL1.Append(Details.PagId.ToString() + ", ");
                    sSQL1.Append(Details.MdeId.ToString() + ", ");
                    sSQL1.Append(Details.Status.ToString() + ", ");
                    sSQL1.Append(Details.Language.ToString() + ", ");
                    sSQL1.Append(Details.ParentId.ToString() + ", ");
                    sSQL1.Append(Details.Order.ToString() + ", ");
                    sSQL1.Append(Details.MobileOrder.ToString() + ", ");
					sSQL1.Append(Details.Revision.ToString() + ", ");
                    sSQL1.Append("'" + Details.Title + "', ");
                    sSQL1.Append("'" + Details.Alias + "', ");
                    sSQL1.Append("'" + Details.Description + "', ");
                    sSQL1.Append("'" + Details.Src + "', ");
                    sSQL1.Append("'" + Details.ContentPane + "', ");
                    sSQL1.Append("" + (Details.AllPages ? "1" : "0") + ", ");
                    sSQL1.Append("" + (Details.SSL ? "1" : "0") + ", ");
                    sSQL1.Append(Details.CacheTime.ToString() + ", ");
                    sSQL1.Append("'" + Details.CreatedDate.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "', ");
                    sSQL1.Append("'" + Details.CreatedBy + "', ");
                    sSQL1.Append("'" + Details.UpdatedDate.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "', ");
                    sSQL1.Append("'" + Details.UpdatedBy + "', ");
                    sSQL1.Append("" + (Details.Hidden ? "1" : "0") + ", ");
                    sSQL1.Append("" + (Details.MobileHidden ? "1" : "0") + ", ");
                    sSQL1.Append("" + (Details.Deleted ? "1" : "0") + ")");
                    using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, true))
                    {
                        EventLog.LogEvent(sSQL1.ToString(), FUNCTIONNAME, String.Empty);
                        oDo.ExecuteNonQuery(sSQL1.ToString());
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);

                        EventLog.LogEvent("SELECT @@IDENTITY", FUNCTIONNAME, String.Empty);
                        ((ModuleModel)Details).Id = Convert.ToInt32(oDo.GetDataTable("SELECT @@IDENTITY").Rows[0][0]);
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);

                        StringBuilder sSQL2 = new StringBuilder();
                        sSQL2.Append("INSERT INTO agg_aggregation (sit_id, pag_id, mod_id, sta_id, agg_createddate, agg_createdby, ");
                        sSQL2.Append("agg_updateddate, agg_updatedby, agg_hidden, agg_deleted) VALUES ( ");
                        sSQL2.Append(Details.SitId.ToString() + ", ");
                        sSQL2.Append(Details.PagId.ToString() + ", ");
                        sSQL2.Append(Details.Id.ToString() + ", ");
                        sSQL2.Append(Details.Status.ToString() + ", ");
                        sSQL2.Append("'" + Details.CreatedDate.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "', ");
                        sSQL2.Append("'" + Details.CreatedBy + "', ");
                        sSQL2.Append("'" + Details.UpdatedDate.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "', ");
                        sSQL2.Append("'" + Details.UpdatedBy + "', ");
                        sSQL2.Append("" + (Details.Hidden ? "1" : "0") + ", ");
                        sSQL2.Append("" + (Details.Deleted ? "1" : "0") + ")");

                        EventLog.LogEvent(sSQL2.ToString(), FUNCTIONNAME, String.Empty);
                        oDo.ExecuteNonQuery(sSQL2.ToString());
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);

                        //EventLog.LogEvent("CALL sort_mod_modules();", FUNCTIONNAME, String.Empty);
                        //oDo.ExecuteNonQuery("CALL sort_mod_modules();");
                        //if (oDo.HasError)
                        //    throw new Exception(oDo.GetError);
                    }

                    SortAll(Details.SitId, Details.PagId, Details.ParentId);

                    CacheData.Reset("LiquidCore.Pages.Page.Id(" + Details.PagId.ToString() + ")");
                    CacheData.Reset("LiquidCore.Settings.PrimaryKeys(SitId(" + Details.SitId.ToString() + "), PagId(" + Details.PagId.ToString() + ")");
                    CacheData.Reset("LiquidCore.Modules.PrimaryKeys(SitId(" + Details.SitId.ToString() + "), PagId(" + Details.PagId.ToString() + ")");
                    CacheData.Reset("LiquidCore.Modules.PrimaryKey(Alias(" + Details.Alias + "))");
                    CacheData.Reset("LiquidCore.Modules.PrimaryKeys(Status(" + Details.Status.ToString() + "), SitId(" + Details.SitId.ToString() + "), PagId(" + Details.PagId.ToString() + "))");

                    StringBuilder sSQL = new StringBuilder();
                    sSQL.Append("SELECT mod_id FROM mod_modules WHERE sit_id = " + Details.SitId.ToString() + " AND pag_id = " + Details.PagId.ToString() + " AND mod_parentid = " + Details.ParentId.ToString() + " AND mod_deleted = 0");
                    using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, false))
                    {
                        DataTable dt = oDo.GetDataTable(sSQL.ToString());
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);
                        foreach (DataRow dr in dt.Rows)
                            CacheData.Reset("LiquidCore.Modules.Module.Id(" + dr["mod_id"].ToString() + ")");
                    }
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void Load(ModelObject Details, NameValueSet DataFields)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::Load]";
                try
                {

					String CacheItem = String.Empty;
					bool fromDr = false;
					if (DataFields["dr"] != null)
					{
						DataRow dr = (DataRow)DataFields["dr"];
						CacheItem = "LiquidCore.Modules.Module.Id(" + dr["mod_id"].ToString() + ")";
						fromDr = true;
					}
					else if (DataFields["Id"] != null)
					{
						CacheItem = "LiquidCore.Modules.Module.Id(" + DataFields["Id"].ToString() + ")";
					}

					System.Data.DataTable dt = null;
                    if (HttpContext.Current != null) if (HttpContext.Current != null) dt = (DataTable)HttpContext.Current.Cache[CacheItem];
                    if (dt == null)
                    {
						if (fromDr)
						{
							dt = CreateDt();
							dt.ImportRow((DataRow)DataFields["dr"]);
						}
						else
						{
							StringBuilder sSQL = new StringBuilder();
							sSQL.Append("SELECT * FROM mod_modules WHERE mod_id = " + DataFields["Id"].ToString() + " AND mod_deleted = 0");
							using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true))
							{
								EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
								dt = oDo.GetDataTable(sSQL.ToString());
								if (oDo.HasError)
									throw new Exception(oDo.GetError);
							}
						}
                        CacheData.Insert(CacheItem, dt);
                    }
                    if (dt.Rows.Count > 0)
                    {
                        System.Data.DataRow dr = dt.Rows[0];
                        ((ModuleModel)Details).Id = Convert.ToInt32(dr["mod_id"]);
                        ((ModuleModel)Details).SitId = Convert.ToInt32(dr["sit_id"]);
                        ((ModuleModel)Details).PagId = Convert.ToInt32(dr["pag_id"]);
                        ((ModuleModel)Details).MdeId = Convert.ToInt32(dr["mde_id"]);
                        ((ModuleModel)Details).Status = Convert.ToInt32(dr["sta_id"]);
                        ((ModuleModel)Details).Language = Convert.ToInt32(dr["lng_id"]);
                        ((ModuleModel)Details).ParentId = Convert.ToInt32(dr["mod_parentid"]);
                        ((ModuleModel)Details).Order = Convert.ToInt32(dr["mod_order"]);
                        ((ModuleModel)Details).MobileOrder = Convert.ToInt32(dr["mod_mobileorder"]);
						((ModuleModel)Details).Revision = Convert.ToInt32(dr["mod_revision"]);
                        ((ModuleModel)Details).Title = Convert.ToString(dr["mod_title"]);
                        ((ModuleModel)Details).Alias = Convert.ToString(dr["mod_alias"]);
                        ((ModuleModel)Details).Description = Convert.ToString(dr["mod_description"]);
                        ((ModuleModel)Details).AuthorizedRoles = GetAuthorizedRoles(Convert.ToInt32(DataFields["Id"]));
                        ((ModuleModel)Details).Src = Convert.ToString(dr["mod_src"]);
                        ((ModuleModel)Details).ContentPane = Convert.ToString(dr["mod_contentpane"]);
                        ((ModuleModel)Details).AllPages = (dr["mod_allpages"].ToString().Equals("0") ? false : true);
                        ((ModuleModel)Details).SSL = (dr["mod_ssl"].ToString().Equals("0") ? false : true);
                        ((ModuleModel)Details).CacheTime = Convert.ToInt32(dr["mod_cachetime"]);
                        ((ModuleModel)Details).CreatedDate = Convert.ToDateTime(dr["mod_createddate"]);
                        ((ModuleModel)Details).CreatedBy = Convert.ToString(dr["mod_createdby"]);
                        ((ModuleModel)Details).UpdatedDate = Convert.ToDateTime(dr["mod_updateddate"]);
                        ((ModuleModel)Details).UpdatedBy = Convert.ToString(dr["mod_updatedby"]);
                        ((ModuleModel)Details).Hidden = (dr["mod_hidden"].ToString().Equals("0") ? false : true);
                        ((ModuleModel)Details).MobileHidden = (dr["mod_mobilehidden"].ToString().Equals("0") ? false : true);
                        ((ModuleModel)Details).Deleted = (dr["mod_deleted"].ToString().Equals("0") ? false : true);
                    }
                    else
                        ((ModuleModel)Details).Deleted = true;
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
			private static DataTable CreateDt()
			{
				string FUNCTIONNAME = CLASSNAME + "[Function::CreateDt]";
				try
				{
					DataTable dt = new DataTable();
					dt.Columns.Add("mod_id");
					dt.Columns.Add("sit_id");
					dt.Columns.Add("pag_id");
					dt.Columns.Add("mde_id");
					dt.Columns.Add("sta_id");
					dt.Columns.Add("lng_id");
					dt.Columns.Add("mod_parentid");
					dt.Columns.Add("mod_order");
					dt.Columns.Add("mod_mobileorder");
					dt.Columns.Add("mod_revision");
					dt.Columns.Add("mod_title");
					dt.Columns.Add("mod_alias");
					dt.Columns.Add("mod_description");
					dt.Columns.Add("mod_src");
					dt.Columns.Add("mod_contentpane");
					dt.Columns.Add("mod_allpages");
					dt.Columns.Add("mod_ssl");
					dt.Columns.Add("mod_cachetime");
					dt.Columns.Add("mod_createddate");
					dt.Columns.Add("mod_createdby");
					dt.Columns.Add("mod_updateddate");
					dt.Columns.Add("mod_updatedby");
					dt.Columns.Add("mod_hidden");
					dt.Columns.Add("mod_mobilehidden");
					dt.Columns.Add("mod_deleted");
					return dt;
				}
				catch (Exception ex)
				{
					Error.Report(ex, FUNCTIONNAME, "");
					return null;
				}
			}
            public static void LoadAll(ref List<Module> _modules)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::LoadAll]";
                try
                {
                    DataTable dt = null;
                    String CacheItem = "LiquidCore.Modules";
                    if (HttpContext.Current != null) if (HttpContext.Current != null) dt = (DataTable)HttpContext.Current.Cache[CacheItem];
                    if (dt == null)
                    {
                        StringBuilder sSQL = new StringBuilder();
                        sSQL.Append("SELECT * FROM mod_modules WHERE mod_deleted = 0 ORDER BY sit_id, pag_id, sta_id, ");
                        sSQL.Append("lng_id, mod_parentid, mod_order");
                        using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true))
                        {
                            EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                            dt = oDo.GetDataTable(sSQL.ToString());
                            if (oDo.HasError)
                                throw new Exception(oDo.GetError);
                        }
                        CacheData.Insert(CacheItem, dt);
                    }
                    foreach (DataRow dr in dt.Rows)
                    {
                        Module m = new Module(dr);
                        _modules.Add(m);
                    }
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void LoadAll(ref List<Module> _modules, NameValueSet DataFields)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::LoadAll]";
                try
                {
                    String PrimaryKey = String.Empty;
                    String CacheItem = String.Empty;
                    StringBuilder sSQL = new StringBuilder();
                    if (DataFields["SitId"] != null && DataFields["GetAggregatableModules"] != null)
                    {
                        String SitId = DataFields["SitId"].ToString();
                        Int32 GetAggregatableModules = Convert.ToInt32(DataFields["GetAggregatableModules"].ToString());
                        CacheItem = "LiquidCore.Modules.GetAggregatableModules(SitId(" + SitId + "))";
                        sSQL.Append("SELECT m.* FROM mod_modules m WHERE m.sit_id = " + SitId + " AND m.mod_id IN ( SELECT mod_id FROM agg_aggregation WHERE agg_deleted = 0 ) AND m.mod_deleted = 0 ORDER BY m.sit_id, m.pag_id, m.sta_id, m.lng_id, m.mod_parentid, m.mod_order");
                    }
                    else if (DataFields["Status"] != null && DataFields["SitId"] != null && DataFields["PagId"] != null && DataFields["ParentId"] != null && DataFields["IncludeAggregated"] != null)
                    {
                        String Status = DataFields["Status"].ToString();
                        String SitId = DataFields["SitId"].ToString();
                        String PagId = DataFields["PagId"].ToString();
                        String ParentId = DataFields["ParentId"].ToString();
                        Int32 IncludeAggregated = Convert.ToInt32(DataFields["IncludeAggregated"].ToString());
                        if (IncludeAggregated.Equals(0))
                        {
                            CacheItem = "LiquidCore.Modules.PrimaryKeys(Status(" + Status + "), SitId(" + SitId + "), PagId(" + PagId + "), ParentId(" + ParentId + "), IncludeAggregated(" + (IncludeAggregated.Equals(1) ? "true" : "false").ToString() + "))";
                            sSQL.Append("SELECT * FROM mod_modules WHERE sta_id = " + Status + " AND sit_id = " + SitId + " AND pag_id = " + PagId + " AND mod_parentid = " + ParentId + " AND mod_deleted = 0 OR sit_id = " + SitId + " AND sta_id = " + Status + " AND mod_allpages = 1 AND mod_parentid = " + ParentId + " AND mod_deleted = 0 ORDER BY sit_id, pag_id, sta_id, lng_id, mod_parentid, mod_order");
                        }
                        else
                        {
                            CacheItem = "LiquidCore.Modules.PrimaryKeys(Status(" + Status + "), SitId(" + SitId + "), PagId(" + PagId + "), ParentId(" + ParentId + "), IncludeAggregated(" + (IncludeAggregated.Equals(1) ? "true" : "false").ToString() + "))";
                            sSQL.Append("SELECT m.* FROM mod_modules m WHERE m.sta_id = " + Status + " AND m.sit_id = " + SitId + " AND m.pag_id = " + PagId + " AND m.mod_parentid = " + ParentId + " AND m.mod_deleted = 0 OR m.sit_id = " + SitId + " AND m.mod_allpages = 1 AND m.mod_parentid = " + ParentId + " AND m.mod_deleted = 0 OR m.sit_id = " + SitId + " AND m.sta_id = " + Status + " AND m.mod_id IN ( SELECT mod_id FROM agg_aggregation WHERE agg_deleted = 0 AND pag_id = " + PagId + " ) AND m.mod_parentid = " + ParentId + " AND m.mod_deleted = 0 ORDER BY m.sit_id, m.pag_id, m.sta_id, m.lng_id, m.mod_parentid, m.mod_order");
                        }
                    }
                    else if (DataFields["SitId"] != null && DataFields["PagId"] != null && DataFields["ParentId"] != null && DataFields["IncludeAggregated"] != null)
                    {
                        String SitId = DataFields["SitId"].ToString();
                        String PagId = DataFields["PagId"].ToString();
                        String ParentId = DataFields["ParentId"].ToString();
                        Int32 IncludeAggregated = Convert.ToInt32(DataFields["IncludeAggregated"].ToString());
                        if (IncludeAggregated.Equals(0))
                        {
                            CacheItem = "LiquidCore.Modules.PrimaryKeys(SitId(" + SitId + "), PagId(" + PagId + "), ParentId(" + ParentId + "), IncludeAggregated(" + (IncludeAggregated.Equals(1) ? "true" : "false").ToString() + "))";
                            sSQL.Append("SELECT * FROM mod_modules WHERE sit_id = " + SitId + " AND pag_id = " + PagId + " AND mod_parentid = " + ParentId + " AND mod_deleted = 0 OR sit_id = " + SitId + " AND mod_allpages = 1 AND mod_parentid = " + ParentId + " AND mod_deleted = 0 ORDER BY sit_id, pag_id, sta_id, lng_id, mod_parentid, mod_order");
                        }
                        else
                        {
                            CacheItem = "LiquidCore.Modules.PrimaryKeys(SitId(" + SitId + "), PagId(" + PagId + "), ParentId(" + ParentId + "), IncludeAggregated(" + (IncludeAggregated.Equals(1) ? "true" : "false").ToString() + "))";
                            sSQL.Append("SELECT m.* FROM mod_modules m WHERE m.sit_id = " + SitId + " AND m.pag_id = " + PagId + " AND m.mod_parentid = " + ParentId + " AND m.mod_deleted = 0 OR m.sit_id = " + SitId + " AND m.mod_allpages = 1 AND m.mod_parentid = " + ParentId + " AND m.mod_deleted = 0 OR m.sit_id = " + SitId + " AND m.mod_id IN ( SELECT mod_id FROM agg_aggregation WHERE agg_deleted = 0 AND pag_id = " + PagId + " ) AND m.mod_parentid = " + ParentId + " AND m.mod_deleted = 0 ORDER BY m.sit_id, m.pag_id, m.sta_id, m.lng_id, m.mod_parentid, m.mod_order");
                        }
                    }
                    else if (DataFields["SitId"] != null && DataFields["PagId"] != null && DataFields["ParentId"] != null)
                    {
                        String SitId = DataFields["SitId"].ToString();
                        String PagId = DataFields["PagId"].ToString();
                        String ParentId = DataFields["ParentId"].ToString();
                        CacheItem = "LiquidCore.Modules.PrimaryKeys(SitId(" + SitId + "), PagId(" + PagId + "), ParentId(" + ParentId + "))";
                        sSQL.Append("SELECT * FROM mod_modules WHERE sit_id = " + SitId + " AND pag_id = " + PagId + " AND mod_parentid = " + ParentId + " AND mod_deleted = 0 OR sit_id = " + SitId + " AND mod_allpages = 1 AND mod_parentid = " + ParentId + " AND mod_deleted = 0 ORDER BY sit_id, pag_id, sta_id, lng_id, mod_parentid, mod_order");
                    }
                    else if (DataFields["Alias"] != null)
                    {
                        PrimaryKey = DataFields["Alias"].ToString();
                        CacheItem = "LiquidCore.Modules.PrimaryKey(Alias(" + PrimaryKey + "))";
                        sSQL.Append("SELECT * FROM mod_modules WHERE mod_alias = '" + PrimaryKey + "' AND mod_deleted = 0 ORDER BY sit_id, pag_id, sta_id, lng_id, mod_parentid, mod_order");
                    }
                    else
                    {
                        LoadAll(ref _modules);
                        return;
                    }
                    DataTable dt = null;
                    if (HttpContext.Current != null) if (HttpContext.Current != null) dt = (DataTable)HttpContext.Current.Cache[CacheItem];
                    if (dt == null)
                    {
                        using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true))
                        {
                            EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                            dt = oDo.GetDataTable(sSQL.ToString());
                            if (oDo.HasError)
                                throw new Exception(oDo.GetError);
                        }
                        CacheData.Insert(CacheItem, dt);
                    }
                    foreach (DataRow dr in dt.Rows)
                    {
                        Module m = new Module(dr);
                        _modules.Add(m);
                    }
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void Update(ModuleModel Details)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::Update]";
                try
                {
                    ((ModuleModel)Details).UpdatedDate = DateTime.Now;
                    ((ModuleModel)Details).UpdatedBy = Authentication.User.Identity.Name;

                    System.Data.DataSet ds = new System.Data.DataSet();
                    StringBuilder sSQL1 = new StringBuilder();
                    sSQL1.Append("UPDATE mod_modules SET ");
                    sSQL1.Append("sit_id = " + Details.SitId.ToString() + ", ");
                    sSQL1.Append("pag_id = " + Details.PagId.ToString() + ", ");
                    sSQL1.Append("mde_id = " + Details.MdeId.ToString() + ", ");
                    sSQL1.Append("sta_id = " + Details.Status.ToString() + ", ");
                    sSQL1.Append("lng_id = " + Details.Language.ToString() + ", ");
                    sSQL1.Append("mod_parentid = " + Details.ParentId.ToString() + ", ");
                    sSQL1.Append("mod_order = " + Details.Order.ToString() + ", ");
                    sSQL1.Append("mod_mobileorder = " + Details.MobileOrder.ToString() + ", ");
					sSQL1.Append("mod_revision = " + Details.Revision.ToString() + ", ");
                    sSQL1.Append("mod_title = '" + Details.Title + "', ");
                    sSQL1.Append("mod_alias = '" + Details.Alias + "', ");
                    sSQL1.Append("mod_description = '" + Details.Description + "', ");
                    sSQL1.Append("mod_src = '" + Details.Src + "', ");
                    sSQL1.Append("mod_contentpane = '" + Details.ContentPane + "', ");
                    sSQL1.Append("mod_allpages = " + (!Details.AllPages ? "0" : "1") + ", ");
                    sSQL1.Append("mod_ssl = " + (!Details.SSL ? "0" : "1") + ", ");
                    sSQL1.Append("mod_cachetime = " + Details.CacheTime.ToString() + ", ");
                    sSQL1.Append("mod_createddate = '" + Details.CreatedDate.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "', ");
                    sSQL1.Append("mod_createdby = '" + Details.CreatedBy + "', ");
                    sSQL1.Append("mod_updateddate = '" + Details.UpdatedDate.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "', ");
                    sSQL1.Append("mod_updatedby = '" + Details.UpdatedBy + "', ");
                    sSQL1.Append("mod_hidden = " + (!Details.Hidden ? "0" : "1") + ", ");
                    sSQL1.Append("mod_mobilehidden = " + (!Details.MobileHidden ? "0" : "1") + ", ");
                    sSQL1.Append("mod_deleted = " + (!Details.Deleted ? "0" : "1") + " ");
                    sSQL1.Append("WHERE mod_id = " + Details.Id.ToString());
                    StringBuilder sSQL2 = new StringBuilder();
                    sSQL2.Append("UPDATE agg_aggregation SET ");
                    sSQL2.Append("sit_id = " + Details.SitId.ToString() + ", ");
                    sSQL2.Append("pag_id = " + Details.PagId.ToString() + ", ");
                    sSQL2.Append("mod_id = " + Details.Id.ToString() + ", ");
                    sSQL2.Append("sta_id = " + Details.Status.ToString() + ", ");
                    sSQL2.Append("agg_createddate = '" + Details.CreatedDate.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "', ");
                    sSQL2.Append("agg_createdby = '" + Details.CreatedBy + "', ");
                    sSQL2.Append("agg_updateddate = '" + Details.UpdatedDate.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "', ");
                    sSQL2.Append("agg_updatedby = '" + Details.UpdatedBy + "', ");
                    sSQL2.Append("agg_hidden = " + (!Details.Hidden ? "0" : "1") + ", ");
                    sSQL2.Append("agg_deleted = " + (!Details.Deleted ? "0" : "1") + " ");
                    sSQL2.Append("WHERE mod_id = " + Details.Id.ToString());
                    using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, true))
                    {
                        EventLog.LogEvent(sSQL1.ToString(), FUNCTIONNAME, String.Empty);
                        oDo.ExecuteNonQuery(sSQL1.ToString());
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);

                        EventLog.LogEvent(sSQL2.ToString(), FUNCTIONNAME, String.Empty);
                        oDo.ExecuteNonQuery(sSQL2.ToString());
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);
                    }

                    SortAll(Details.SitId, Details.PagId, Details.ParentId);

                    CacheData.Reset("LiquidCore.Pages.Page.Id(" + Details.PagId.ToString() + ")");
                    CacheData.Reset("LiquidCore.Settings.PrimaryKeys(SitId(" + Details.SitId.ToString() + "), PagId(" + Details.PagId.ToString() + ")");
                    CacheData.Reset("LiquidCore.Modules.PrimaryKeys(SitId(" + Details.SitId.ToString() + "), PagId(" + Details.PagId.ToString() + ")");
                    CacheData.Reset("LiquidCore.Modules.PrimaryKey(Alias(" + Details.Alias + "))");
                    CacheData.Reset("LiquidCore.Modules.PrimaryKeys(Status(" + Details.Status.ToString() + "), SitId(" + Details.SitId.ToString() + "), PagId(" + Details.PagId.ToString() + "))");
                    
                    StringBuilder sSQL = new StringBuilder();
                    sSQL.Append("SELECT mod_id FROM mod_modules WHERE sit_id = " + Details.SitId.ToString() + " AND pag_id = " + Details.PagId.ToString() + " AND mod_parentid = " + Details.ParentId.ToString() + " AND mod_deleted = 0");
                    using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, false))
                    {
                        DataTable dt = oDo.GetDataTable(sSQL.ToString());
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);
                        foreach (DataRow dr in dt.Rows)
                            CacheData.Reset("LiquidCore.Modules.Module.Id(" + dr["mod_id"].ToString() + ")");
                    }
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void Delete(ModuleModel Details, NameValueSet DataFields)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::Delete]";
                try
                {
                    RecursiveDelete(Details.Id);   
                    //using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, false))
                    //{
                    //    EventLog.LogEvent("CALL sort_set_settings();", FUNCTIONNAME, String.Empty);
                    //    oDo.ExecuteNonQuery("CALL sort_set_settings();");
                    //    if (oDo.HasError)
                    //        throw new Exception(oDo.GetError);

                    //    EventLog.LogEvent("CALL sort_mod_modules();", FUNCTIONNAME, String.Empty);
                    //    oDo.ExecuteNonQuery("CALL sort_mod_modules();");
                    //    if (oDo.HasError)
                    //        throw new Exception(oDo.GetError);

                    //    EventLog.LogEvent("CALL sort_obd_objectdata();", FUNCTIONNAME, String.Empty);
                    //    oDo.ExecuteNonQuery("CALL sort_obd_objectdata();");
                    //    if (oDo.HasError)
                    //        throw new Exception(oDo.GetError);
                    //}
                    SortAll(Details.SitId, Details.PagId, Details.ParentId);

                    CacheData.Reset("LiquidCore.Pages.Page.Id(" + Details.PagId.ToString() + ")");
                    CacheData.Reset("LiquidCore.Settings.PrimaryKeys(SitId(" + Details.SitId.ToString() + "), PagId(" + Details.PagId.ToString() + ")");
                    CacheData.Reset("LiquidCore.Modules.PrimaryKeys(SitId(" + Details.SitId.ToString() + "), PagId(" + Details.PagId.ToString() + ")");
                    CacheData.Reset("LiquidCore.Modules.PrimaryKey(Alias(" + Details.Alias + "))");
                    CacheData.Reset("LiquidCore.Modules.PrimaryKeys(Status(" + Details.Status.ToString() + "), SitId(" + Details.SitId.ToString() + "), PagId(" + Details.PagId.ToString() + "))");

                    StringBuilder sSQL = new StringBuilder();
                    sSQL.Append("SELECT mod_id FROM mod_modules WHERE sit_id = " + Details.SitId.ToString() + " AND pag_id = " + Details.PagId.ToString() + " AND mod_parentid = " + Details.ParentId.ToString() + " AND mod_deleted = 0");
                    using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, false))
                    {
                        DataTable dt = oDo.GetDataTable(sSQL.ToString());
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);
                        foreach (DataRow dr in dt.Rows)
                            CacheData.Reset("LiquidCore.Modules.Module.Id(" + dr["mod_id"].ToString() + ")");
                    }
                    Details = null; 
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void RecursiveDelete(Int32 Id)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::RecursiveDelete]";
                try
                {
                    if (Id.Equals(0))
                        return;

                    DataTable dt = new DataTable();
                    using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, true))
                    {
                        String sSQL1 = "SELECT obd_id FROM obd_objectdata WHERE mod_id = " + Id.ToString();
                        String sSQL2 = "UPDATE amr_authorizedmodulesroles SET amr_deleted = 1 WHERE mod_id = " + Id.ToString();
                        String sSQL3 = "UPDATE agg_aggregation SET agg_deleted = 1 WHERE mod_id = " + Id.ToString();
                        String sSQL4 = "UPDATE set_settings SET set_deleted = 1 WHERE mod_id = " + Id.ToString();
                        String sSQL5 = "UPDATE mod_modules SET mod_deleted = 1 WHERE mod_id = " + Id.ToString();

                        EventLog.LogEvent(sSQL1, FUNCTIONNAME, String.Empty);
                        System.Diagnostics.Debug.WriteLine(sSQL1);
                        dt = oDo.GetDataTable(sSQL1);
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);

                        EventLog.LogEvent(sSQL2, FUNCTIONNAME, String.Empty);
                        System.Diagnostics.Debug.WriteLine(sSQL2);
                        oDo.ExecuteNonQuery(sSQL2);
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);

                        EventLog.LogEvent(sSQL3, FUNCTIONNAME, String.Empty);
                        System.Diagnostics.Debug.WriteLine(sSQL3);
                        oDo.ExecuteNonQuery(sSQL3);
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);

                        EventLog.LogEvent(sSQL4, FUNCTIONNAME, String.Empty);
                        System.Diagnostics.Debug.WriteLine(sSQL4);
                        oDo.ExecuteNonQuery(sSQL4);
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);

                        EventLog.LogEvent(sSQL5, FUNCTIONNAME, String.Empty);
                        System.Diagnostics.Debug.WriteLine(sSQL5);
                        oDo.ExecuteNonQuery(sSQL5);
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);
                    }
                    foreach (DataRow dr in dt.Rows)
                    {
                        Int32 ParentId = 0;
                        Int32.TryParse(dr["obd_id"].ToString(), out ParentId);
                        LiquidCore.Data.ObjectData.RecursiveDelete(ParentId, false);
                    }
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void AddAggregation(Int32 SitId, Int32 PagId, Int32 ModId)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::Delete]";
                try
                {
                    StringBuilder sSQL = new StringBuilder();
                    sSQL.Append("INSERT INTO agg_aggregation (sit_id, pag_id, mod_id, sta_id, agg_createddate, agg_createdby, ");
                    sSQL.Append("agg_updateddate, agg_updatedby, agg_hidden, agg_deleted) VALUES ( ");
                    sSQL.Append(SitId.ToString() + ", ");
                    sSQL.Append(PagId.ToString() + ", ");
                    sSQL.Append(ModId.ToString() + ", ");
                    sSQL.Append("1, ");
                    sSQL.Append("'" + DateTime.Now.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "', ");
                    sSQL.Append("'" + Authentication.User.Identity.Name + "', ");
                    sSQL.Append("'" + DateTime.Now.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "', ");
                    sSQL.Append("'" + Authentication.User.Identity.Name + "', ");
                    sSQL.Append("0, ");
                    sSQL.Append("0)");
                    using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, true))
                    {
                        EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                        if (oDo.ExecuteNonQuery(sSQL.ToString()).Equals(-1))
                            throw new Exception(oDo.GetError);
                    }
                    CacheData.Reset("LiquidCore.Modules.GetAggregatableModules(SitId(" + SitId.ToString() + "))");
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void DeleteAggregation(Int32 SitId, Int32 PagId, Int32 ModId)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::DeleteAggregation]";
                try
                {
                    System.Data.DataSet ds = new System.Data.DataSet();
                    StringBuilder sSQL = new StringBuilder();
                    sSQL.Append("UPDATE agg_aggregation SET agg_deleted = 1 WHERE sit_id = " + SitId.ToString() + " AND pag_id = " + PagId.ToString() + " AND mod_id = " + ModId.ToString());
                    using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, true))
                    {
                        EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                        if (oDo.ExecuteNonQuery(sSQL.ToString()).Equals(-1))
                            throw new Exception(oDo.GetError);
                    }
                    CacheData.Reset("LiquidCore.Modules.GetAggregatableModules(SitId(" + SitId.ToString() + "))");
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void ResetThis()
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::ResetThis]";
                try
                {
                    //String CacheItem = "LiquidCore.Modules.Module.Id(" + DataFields["Id"].ToString() + ")";

                    //CacheItem = "LiquidCore.Modules.PrimaryKeys(Status(" + Status + "), SitId(" + SitId + "), PagId(" + PagId + "), ParentId(" + ParentId + "), IncludeAggregated(" + (IncludeAggregated.Equals(1) ? "true" : "false").ToString() + "))";
                    //CacheItem = "LiquidCore.Modules.PrimaryKeys(SitId(" + SitId + "), PagId(" + PagId + "), ParentId(" + ParentId + "))";
                    //CacheItem = "LiquidCore.Modules.PrimaryKey(Alias(" + PrimaryKey + "))";
                    //CacheItem = "LiquidCore.Modules.GetAggregatableModules(SitId(" + SitId + "))";
                    //CacheData.Reset("LiquidCore.Modules");
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            //[ObsoleteAttribute("SortAll has been deprecated. All sorting take place in stored procedures instead.")]
            public static void SortAll(Int32 SitId, Int32 PagId, Int32 ParentId)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::SortAll]";
                try
                {
                    Int32 Order = (Int32)OrderMinMax.Min;
                    StringBuilder sSQL = new StringBuilder();
                    sSQL.Append("SELECT mod_id FROM mod_modules WHERE sit_id = " + SitId.ToString() + " AND pag_id = " + PagId.ToString() + " AND mod_parentid = " + ParentId.ToString() + " AND mod_deleted = 0 ORDER BY sit_id, pag_id, mod_parentid, " + (LiquidCore.Data.ModuleData.IsMobileView() ? "mod_mobileorder" : "mod_contentpane, mod_order"));
                    using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, false))
                    {
                        EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                        DataTable dt = oDo.GetDataTable(sSQL.ToString());
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);
                        StringBuilder sSQL2 = new StringBuilder();
                        foreach (DataRow dr in dt.Rows)
                        {
                            sSQL2.AppendLine("UPDATE mod_modules SET " + (LiquidCore.Data.ModuleData.IsMobileView() ? "mod_mobileorder" : "mod_order") + " = " + Order.ToString() + " WHERE mod_id = " + dr["mod_id"].ToString() + ";");
                            Order = Order + (Int32)OrderMinMax.Step;
                            if (sSQL2.Length > DataBlockSize)
                            {
                                EventLog.LogEvent(sSQL2.ToString(), FUNCTIONNAME, String.Empty);
                                if (!(oDo.ExecuteNonQuery(sSQL2.ToString()) > 0))
                                    throw new Exception(oDo.GetError);
                                sSQL2 = new StringBuilder();
                            }
                        }
                        if (sSQL2.Length > 0)
                        {
                            EventLog.LogEvent(sSQL2.ToString(), FUNCTIONNAME, String.Empty);
                            if (!(oDo.ExecuteNonQuery(sSQL2.ToString()) > 0))
                                throw new Exception(oDo.GetError);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static Boolean FindChildren(ModuleModel Details)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::FindChildren]";
                DataTable dt = null;
                try
                {
                    String CacheItem = "LiquidCore.Modules.Module.Children(" + Details.Id.ToString() + ")";
                    if (HttpContext.Current != null) if (HttpContext.Current != null) dt = (DataTable)HttpContext.Current.Cache[CacheItem];
                    if (dt == null)
                    {
                        StringBuilder sSQL = new StringBuilder();
                        sSQL.Append("SELECT mod_id FROM mod_modules WHERE mod_deleted = 0 AND mod_parentid = " + Details.Id.ToString());
                        using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true))
                        {
                            EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                            dt = oDo.GetDataTable(sSQL.ToString());
                        }
                        CacheData.Insert(CacheItem, dt);
                    }
                    if (dt.Rows.Count > 0)
                        return true;

                    return false;
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                    return false;
                }
            }
            public static void AddAuthorizedRole(ModuleModel Details, Int32 RolId)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::AddAuthorizedRole]";
                try
                {
                    StringBuilder sSQL = new StringBuilder();
                    sSQL.Append("INSERT INTO amr_authorizedmodulesroles (sta_id, lng_id, mod_id, rol_id, amr_createddate, amr_createdby, ");
                    sSQL.Append("amr_updateddate, amr_updatedby, amr_hidden, amr_deleted) VALUES ( ");
                    sSQL.Append(Details.Status.ToString() + ", ");
                    sSQL.Append(Details.Language.ToString() + ", ");
                    sSQL.Append(Details.Id.ToString() + ", ");
                    sSQL.Append(RolId.ToString() + ", ");
                    sSQL.Append("'" + DateTime.Now.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "', ");
                    sSQL.Append("'" + Authentication.User.Identity.Name + "', ");
                    sSQL.Append("'" + DateTime.Now.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "', ");
                    sSQL.Append("'" + Authentication.User.Identity.Name + "', ");
                    sSQL.Append("0, ");
                    sSQL.Append("0)");
                    using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, true))
                    {
                        EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                        if (oDo.ExecuteNonQuery(sSQL.ToString()).Equals(-1))
                            throw new Exception(oDo.GetError);
                    }
                    ResetThis();
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void DeleteAuthorizedRole(ModuleModel Details, Int32 RolId)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::DeleteAuthorizedRole]";
                try
                {
                    System.Data.DataSet ds = new System.Data.DataSet();
                    StringBuilder sSQL = new StringBuilder();
                    sSQL.Append("UPDATE amr_authorizedmodulesroles SET amr_deleted = 1 WHERE mod_id = " + Details.Id.ToString() + " AND rol_id = " + RolId.ToString());
                    using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, true))
                    {
                        EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                        if (oDo.ExecuteNonQuery(sSQL.ToString()) < 1)
                            throw new Exception(oDo.GetError);
                    }
                    ResetThis();
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static Int32[] GetAuthorizedRoles(Int32 Id)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::GetAuthorizedRoles]";
                Int32[] Roles;
                DataTable dt = null;
                try
                {
                    String CacheItem = "LiquidCore.Modules.Module.GetAuthorizedRoles(" + Id.ToString() + ")";
                    if (HttpContext.Current != null) if (HttpContext.Current != null) dt = (DataTable)HttpContext.Current.Cache[CacheItem];
                    if (dt == null)
                    {
                        StringBuilder sSQL = new StringBuilder();
                        sSQL.Append("SELECT rol_id FROM amr_authorizedmodulesroles WHERE mod_id = " + Id.ToString() + " AND amr_deleted = 0");
                        using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true))
                        {
                            EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                            dt = oDo.GetDataTable(sSQL.ToString());
                        }
                        CacheData.Insert(CacheItem, dt);
                    }
                    Roles = new Int32[dt.Rows.Count];
                    for (Int32 i = 0; i < dt.Rows.Count; i++)
                        Roles[i] = Convert.ToInt32(dt.Rows[i]["rol_id"]);
                    return Roles;
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                    return new Int32[0];
                }
            }
            public static Boolean IsMobileView()
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::IsMobileView]";
                try
                {
                    if (HttpContext.Current != null) 
                        if (HttpContext.Current != null)
                        {
                            if (HttpContext.Current.Session["USE_DESKTOP"] != null && HttpContext.Current.Session["USE_DESKTOP"].ToString() == "true")
                            {
                                return false;
                            }
                            else if (HttpContext.Current.Session["ADMIN_MOBILE_VIEW"] != null && HttpContext.Current.Session["ADMIN_MOBILE_VIEW"].ToString() == "true")
                            {
                                return true;
                            }
                            return false; 
                        }
                    return false;
                    
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                    return false;
                }
            }
        }
        public static class ObjectData
        {
            static string CLASSNAME = "[Namespace::LiquidCore][Class::ObjectData]";

            static Int32 DataBlockSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Data.DataBlockSize"].ToString());
            static Int32 UseRevision = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Data.UseRevision"].ToString());

            public static void Create(ObjectModel Details)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::Create]";
                try
                {
                    ((ObjectModel)Details).Order = (Int32)OrderMinMax.Max;
                    ((ObjectModel)Details).UpdatedDate = DateTime.Now;
                    ((ObjectModel)Details).UpdatedBy = Authentication.User.Identity.Name;
                    ((ObjectModel)Details).CreatedDate = DateTime.Now;
                    ((ObjectModel)Details).CreatedBy = Authentication.User.Identity.Name;

                    System.Data.DataSet ds = new System.Data.DataSet();
                    StringBuilder sSQL = new StringBuilder();
                    sSQL.Append("INSERT INTO obd_objectdata (sit_id, pag_id, mod_id, sta_id, lng_id, obd_parentid, obd_order, obd_type, obd_title, ");
                    sSQL.Append("obd_alias, obd_description, obd_varchar1, obd_varchar2, obd_varchar3, obd_varchar4, obd_varchar5, ");
                    sSQL.Append("obd_varchar6, obd_varchar7, obd_varchar8, obd_varchar9, obd_varchar10, obd_varchar11, ");
                    sSQL.Append("obd_varchar12, obd_varchar13, obd_varchar14, obd_varchar15, obd_varchar16, obd_varchar17, ");
                    sSQL.Append("obd_varchar18, obd_varchar19, obd_varchar20, obd_varchar21, obd_varchar22, obd_varchar23, ");
                    sSQL.Append("obd_varchar24, obd_varchar25, obd_varchar26, obd_varchar27, obd_varchar28, obd_varchar29, ");
                    sSQL.Append("obd_varchar30, obd_varchar31, obd_varchar32, obd_varchar33, obd_varchar34, obd_varchar35, ");
                    sSQL.Append("obd_varchar36, obd_varchar37, obd_varchar38, obd_varchar39, obd_varchar40, obd_varchar41, ");
                    sSQL.Append("obd_varchar42, obd_varchar43, obd_varchar44, obd_varchar45, obd_varchar46, obd_varchar47, ");
                    sSQL.Append("obd_varchar48, obd_varchar49, obd_varchar50, ");
                    sSQL.Append("obd_createddate, obd_createdby, ");
                    sSQL.Append("obd_updateddate, obd_updatedby, obd_hidden, obd_deleted) VALUES ( ");
                    sSQL.Append(Details.SitId.ToString() + ", ");
                    sSQL.Append(Details.PagId.ToString() + ", ");
                    sSQL.Append(Details.ModId.ToString() + ", ");
                    sSQL.Append(Details.Status.ToString() + ", ");
                    sSQL.Append(Details.Language.ToString() + ", ");
                    sSQL.Append(Details.ParentId.ToString() + ", ");
                    sSQL.Append(Details.Order.ToString() + ", ");
                    sSQL.Append(Details.Type.ToString() + ", ");
                    sSQL.Append("'" + Details.Title + "', ");
                    sSQL.Append("'" + Details.Alias + "', ");
                    sSQL.Append("'" + Details.Description + "', ");
                    sSQL.Append("'" + Details.Value1 + "', ");
                    sSQL.Append("'" + Details.Value2 + "', ");
                    sSQL.Append("'" + Details.Value3 + "', ");
                    sSQL.Append("'" + Details.Value4 + "', ");
                    sSQL.Append("'" + Details.Value5 + "', ");
                    sSQL.Append("'" + Details.Value6 + "', ");
                    sSQL.Append("'" + Details.Value7 + "', ");
                    sSQL.Append("'" + Details.Value8 + "', ");
                    sSQL.Append("'" + Details.Value9 + "', ");
                    sSQL.Append("'" + Details.Value10 + "', ");
                    sSQL.Append("'" + Details.Value11 + "', ");
                    sSQL.Append("'" + Details.Value12 + "', ");
                    sSQL.Append("'" + Details.Value13 + "', ");
                    sSQL.Append("'" + Details.Value14 + "', ");
                    sSQL.Append("'" + Details.Value15 + "', ");
                    sSQL.Append("'" + Details.Value16 + "', ");
                    sSQL.Append("'" + Details.Value17 + "', ");
                    sSQL.Append("'" + Details.Value18 + "', ");
                    sSQL.Append("'" + Details.Value19 + "', ");
                    sSQL.Append("'" + Details.Value20 + "', ");
                    sSQL.Append("'" + Details.Value21 + "', ");
                    sSQL.Append("'" + Details.Value22 + "', ");
                    sSQL.Append("'" + Details.Value23 + "', ");
                    sSQL.Append("'" + Details.Value24 + "', ");
                    sSQL.Append("'" + Details.Value25 + "', ");
                    sSQL.Append("'" + Details.Value26 + "', ");
                    sSQL.Append("'" + Details.Value27 + "', ");
                    sSQL.Append("'" + Details.Value28 + "', ");
                    sSQL.Append("'" + Details.Value29 + "', ");
                    sSQL.Append("'" + Details.Value30 + "', ");
                    sSQL.Append("'" + Details.Value31 + "', ");
                    sSQL.Append("'" + Details.Value32 + "', ");
                    sSQL.Append("'" + Details.Value33 + "', ");
                    sSQL.Append("'" + Details.Value34 + "', ");
                    sSQL.Append("'" + Details.Value35 + "', ");
                    sSQL.Append("'" + Details.Value36 + "', ");
                    sSQL.Append("'" + Details.Value37 + "', ");
                    sSQL.Append("'" + Details.Value38 + "', ");
                    sSQL.Append("'" + Details.Value39 + "', ");
                    sSQL.Append("'" + Details.Value40 + "', ");
                    sSQL.Append("'" + Details.Value41 + "', ");
                    sSQL.Append("'" + Details.Value42 + "', ");
                    sSQL.Append("'" + Details.Value43 + "', ");
                    sSQL.Append("'" + Details.Value44 + "', ");
                    sSQL.Append("'" + Details.Value45 + "', ");
                    sSQL.Append("'" + Details.Value46 + "', ");
                    sSQL.Append("'" + Details.Value47 + "', ");
                    sSQL.Append("'" + Details.Value48 + "', ");
                    sSQL.Append("'" + Details.Value49 + "', ");
                    sSQL.Append("'" + Details.Value50 + "', ");
                    sSQL.Append("'" + Details.CreatedDate.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "', ");
                    sSQL.Append("'" + Details.CreatedBy + "', ");
                    sSQL.Append("'" + Details.UpdatedDate.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "', ");
                    sSQL.Append("'" + Details.UpdatedBy + "', ");
                    sSQL.Append("" + (Details.Hidden ? "1" : "0") + ", ");
                    sSQL.Append("" + (Details.Deleted ? "1" : "0") + ")");
                    using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, true))
                    {
                        EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                        oDo.ExecuteNonQuery(sSQL.ToString());
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);

                        EventLog.LogEvent("SELECT @@IDENTITY", FUNCTIONNAME, String.Empty);
                        ((ObjectModel)Details).Id = Convert.ToInt32(oDo.GetDataTable("SELECT @@IDENTITY").Rows[0][0]);
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);

                        //EventLog.LogEvent("CALL sort_obd_objectdata();", FUNCTIONNAME, String.Empty);
                        //oDo.ExecuteNonQuery("CALL sort_obd_objectdata();");
                        //if (oDo.HasError)
                        //    throw new Exception(oDo.GetError);

                    }
                    SortAll(Details.SitId, Details.PagId, Details.ModId, Details.Status, Details.Language, Details.ParentId);
                    CacheData.Reset("LiquidCore.Objects.PrimaryKey(AliasAndSettingParameters");
                    CacheData.Reset("LiquidCore.Objects.PrimaryKeys(SitId(" + Details.SitId.ToString() + "), PagId(" + Details.PagId.ToString() + ")");
                    CacheData.Reset("LiquidCore.Objects.PrimaryKey(Alias(" + Details.Alias + "))");
                    CacheData.Reset("LiquidCore.Objects.PrimaryKey(ParentId(" + Details.ParentId.ToString() + "))");

                    sSQL = new StringBuilder();
                    sSQL.Append("SELECT obd_id FROM obd_objectdata WHERE sit_id = " + Details.SitId.ToString() + " AND pag_id = " + Details.PagId.ToString() + " AND mod_id = " + Details.ModId.ToString() + " AND obd_parentid = " + Details.ParentId.ToString() + " AND obd_deleted = 0");
                    using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, false))
                    {
                        DataTable dt = oDo.GetDataTable(sSQL.ToString());
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);
                        foreach (DataRow dr in dt.Rows)
                            CacheData.Reset("LiquidCore.Objects.Object.Id(" + dr["obd_id"].ToString() + ")");
                    }
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void Load(ModelObject Details, NameValueSet DataFields)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::Load]";
                try
                {
                    System.Data.DataTable dt = null;
                    String CacheItem = "LiquidCore.Objects.Object.Id(" + DataFields["Id"].ToString() + ")";
                    if (HttpContext.Current != null) if (HttpContext.Current != null) dt = (DataTable)HttpContext.Current.Cache[CacheItem];
                    if (dt == null)
                    {
                        StringBuilder sSQL = new StringBuilder();
                        sSQL.Append("SELECT * FROM obd_objectdata WHERE obd_id = " + DataFields["Id"].ToString() + " AND obd_deleted = 0");
                        using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true))
                        {
                            EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                            dt = oDo.GetDataTable(sSQL.ToString());
                            if (oDo.HasError)
                                throw new Exception(oDo.GetError);
                        }
                        CacheData.Insert(CacheItem, dt);
                    }
                    if (dt.Rows.Count > 0)
                    {
                        System.Data.DataRow dr = dt.Rows[0];
                        ((ObjectModel)Details).Id = Convert.ToInt32(dr["obd_id"]);
                        ((ObjectModel)Details).SitId = Convert.ToInt32(dr["sit_id"]);
                        ((ObjectModel)Details).PagId = Convert.ToInt32(dr["pag_id"]);
                        ((ObjectModel)Details).ModId = Convert.ToInt32(dr["mod_id"]);
                        ((ObjectModel)Details).Status = Convert.ToInt32(dr["sta_id"]);
                        ((ObjectModel)Details).Language = Convert.ToInt32(dr["lng_id"]);
                        ((ObjectModel)Details).ParentId = Convert.ToInt32(dr["obd_parentid"]);
                        ((ObjectModel)Details).Order = Convert.ToInt32(dr["obd_order"]);
                        ((ObjectModel)Details).Type = Convert.ToInt32(dr["obd_type"]);
                        ((ObjectModel)Details).Title = Convert.ToString(dr["obd_title"]);
                        ((ObjectModel)Details).Alias = Convert.ToString(dr["obd_alias"]);
                        ((ObjectModel)Details).Description = Convert.ToString(dr["obd_description"]);
                        ((ObjectModel)Details).Value1 = Convert.ToString(dr["obd_varchar1"]);
                        ((ObjectModel)Details).Value2 = Convert.ToString(dr["obd_varchar2"]);
                        ((ObjectModel)Details).Value3 = Convert.ToString(dr["obd_varchar3"]);
                        ((ObjectModel)Details).Value4 = Convert.ToString(dr["obd_varchar4"]);
                        ((ObjectModel)Details).Value5 = Convert.ToString(dr["obd_varchar5"]);
                        ((ObjectModel)Details).Value6 = Convert.ToString(dr["obd_varchar6"]);
                        ((ObjectModel)Details).Value7 = Convert.ToString(dr["obd_varchar7"]);
                        ((ObjectModel)Details).Value8 = Convert.ToString(dr["obd_varchar8"]);
                        ((ObjectModel)Details).Value9 = Convert.ToString(dr["obd_varchar9"]);
                        ((ObjectModel)Details).Value10 = Convert.ToString(dr["obd_varchar10"]);
                        ((ObjectModel)Details).Value11 = Convert.ToString(dr["obd_varchar11"]);
                        ((ObjectModel)Details).Value12 = Convert.ToString(dr["obd_varchar12"]);
                        ((ObjectModel)Details).Value13 = Convert.ToString(dr["obd_varchar13"]);
                        ((ObjectModel)Details).Value14 = Convert.ToString(dr["obd_varchar14"]);
                        ((ObjectModel)Details).Value15 = Convert.ToString(dr["obd_varchar15"]);
                        ((ObjectModel)Details).Value16 = Convert.ToString(dr["obd_varchar16"]);
                        ((ObjectModel)Details).Value17 = Convert.ToString(dr["obd_varchar17"]);
                        ((ObjectModel)Details).Value18 = Convert.ToString(dr["obd_varchar18"]);
                        ((ObjectModel)Details).Value19 = Convert.ToString(dr["obd_varchar19"]);
                        ((ObjectModel)Details).Value20 = Convert.ToString(dr["obd_varchar20"]);
                        ((ObjectModel)Details).Value21 = Convert.ToString(dr["obd_varchar21"]);
                        ((ObjectModel)Details).Value22 = Convert.ToString(dr["obd_varchar22"]);
                        ((ObjectModel)Details).Value23 = Convert.ToString(dr["obd_varchar23"]);
                        ((ObjectModel)Details).Value24 = Convert.ToString(dr["obd_varchar24"]);
                        ((ObjectModel)Details).Value25 = Convert.ToString(dr["obd_varchar25"]);
                        ((ObjectModel)Details).Value26 = Convert.ToString(dr["obd_varchar26"]);
                        ((ObjectModel)Details).Value27 = Convert.ToString(dr["obd_varchar27"]);
                        ((ObjectModel)Details).Value28 = Convert.ToString(dr["obd_varchar28"]);
                        ((ObjectModel)Details).Value29 = Convert.ToString(dr["obd_varchar29"]);
                        ((ObjectModel)Details).Value30 = Convert.ToString(dr["obd_varchar30"]);
                        ((ObjectModel)Details).Value31 = Convert.ToString(dr["obd_varchar31"]);
                        ((ObjectModel)Details).Value32 = Convert.ToString(dr["obd_varchar32"]);
                        ((ObjectModel)Details).Value33 = Convert.ToString(dr["obd_varchar33"]);
                        ((ObjectModel)Details).Value34 = Convert.ToString(dr["obd_varchar34"]);
                        ((ObjectModel)Details).Value35 = Convert.ToString(dr["obd_varchar35"]);
                        ((ObjectModel)Details).Value36 = Convert.ToString(dr["obd_varchar36"]);
                        ((ObjectModel)Details).Value37 = Convert.ToString(dr["obd_varchar37"]);
                        ((ObjectModel)Details).Value38 = Convert.ToString(dr["obd_varchar38"]);
                        ((ObjectModel)Details).Value39 = Convert.ToString(dr["obd_varchar39"]);
                        ((ObjectModel)Details).Value40 = Convert.ToString(dr["obd_varchar40"]);
                        ((ObjectModel)Details).Value41 = Convert.ToString(dr["obd_varchar41"]);
                        ((ObjectModel)Details).Value42 = Convert.ToString(dr["obd_varchar42"]);
                        ((ObjectModel)Details).Value43 = Convert.ToString(dr["obd_varchar43"]);
                        ((ObjectModel)Details).Value44 = Convert.ToString(dr["obd_varchar44"]);
                        ((ObjectModel)Details).Value45 = Convert.ToString(dr["obd_varchar45"]);
                        ((ObjectModel)Details).Value46 = Convert.ToString(dr["obd_varchar46"]);
                        ((ObjectModel)Details).Value47 = Convert.ToString(dr["obd_varchar47"]);
                        ((ObjectModel)Details).Value48 = Convert.ToString(dr["obd_varchar48"]);
                        ((ObjectModel)Details).Value49 = Convert.ToString(dr["obd_varchar49"]);
                        ((ObjectModel)Details).Value50 = Convert.ToString(dr["obd_varchar50"]);
                        ((ObjectModel)Details).CreatedDate = Convert.ToDateTime(dr["obd_createddate"]);
                        ((ObjectModel)Details).CreatedBy = Convert.ToString(dr["obd_createdby"]);
                        ((ObjectModel)Details).UpdatedDate = Convert.ToDateTime(dr["obd_updateddate"]);
                        ((ObjectModel)Details).UpdatedBy = Convert.ToString(dr["obd_updatedby"]);
                        ((ObjectModel)Details).Hidden = (dr["obd_hidden"].ToString().Equals("0") ? false : true);
                        ((ObjectModel)Details).Deleted = (dr["obd_deleted"].ToString().Equals("0") ? false : true);
                    }
                    else
                        ((ObjectModel)Details).Deleted = true;
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void LoadAll(ref List<Objects.Item> _objects)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::LoadAll]";
                try
                {
                    DataTable dt = null;
                    String CacheItem = "LiquidCore.Objects";
                    if (HttpContext.Current != null) if (HttpContext.Current != null) dt = (DataTable)HttpContext.Current.Cache[CacheItem];
                    if (dt == null)
                    {
                        StringBuilder sSQL = new StringBuilder();
                        sSQL.Append("SELECT obd_id FROM obd_objectdata WHERE obd_deleted = 0 ORDER BY sit_id, pag_id, mod_id, sta_id, ");
                        sSQL.Append("lng_id, obd_parentid, obd_order");
                        using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true))
                        {
                            EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                            dt = oDo.GetDataTable(sSQL.ToString());
                            if (oDo.HasError)
                                throw new Exception(oDo.GetError);
                        }
                        CacheData.Insert(CacheItem, dt);
                    }
                    foreach (DataRow dr in dt.Rows)
                    {
                        Objects.Item o = new Objects.Item(Convert.ToInt32(dr["obd_id"].ToString()));
                        _objects.Add(o);
                    }
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void LoadAll(ref List<Objects.Item> _objects, NameValueSet DataFields)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::LoadAll]";
                try
                {
                    String PrimaryKey = String.Empty;
                    String CacheItem = String.Empty;
                    StringBuilder sSQL = new StringBuilder();

                    if (DataFields["SettingParameters"] != null && DataFields["Alias"] != null)
                    {
                        StringBuilder sWhere = new StringBuilder();
                        PrimaryKey = DataFields["Alias"].ToString();
                        LiquidCore.Definition.ObjectsDefinition.Param[] SettingParameters = (LiquidCore.Definition.ObjectsDefinition.Param[])DataFields["SettingParameters"];
                        foreach (LiquidCore.Definition.ObjectsDefinition.Param p in SettingParameters)
                        {
                            PrimaryKey += "_" + p.name + LiquidCore.Definition.ObjectsDefinition.GetOperator(p.operand) + p.value;
							sWhere.Append(" AND obd_id IN (SELECT set_pointer FROM set_settings WHERE set_title = '" + p.name + "' AND set_value " + LiquidCore.Definition.ObjectsDefinition.GetOperator(p.operand) + " '" + p.value + "' AND set_hidden = 0 AND set_deleted = 0)");
                        }
                        CacheItem = "LiquidCore.Objects.PrimaryKey(AliasAndSettingParameters(" + PrimaryKey + "))";
                        sSQL.Append("SELECT obd_id FROM obd_objectdata WHERE obd_alias = '" + DataFields["Alias"].ToString() + "' AND obd_deleted = 0 " + sWhere.ToString() + " ORDER BY sit_id, pag_id, mod_id, sta_id, lng_id, obd_parentid, obd_order");
                    }
                    else if (DataFields["SitId"] != null && DataFields["PagId"] != null && DataFields["ParentId"] != null && DataFields["ModId"] != null)
                    {
                        String SitId = DataFields["SitId"].ToString();
                        String PagId = DataFields["PagId"].ToString();
                        String ModId = DataFields["ModId"].ToString();
                        String ParentId = DataFields["ParentId"].ToString();
                        CacheItem = "LiquidCore.Objects.PrimaryKeys(SitId(" + SitId + "), PagId(" + PagId + "), ModId(" + ModId + "), ParentId(" + ParentId + "))";
                        sSQL.Append("SELECT obd_id FROM obd_objectdata WHERE sit_id = " + SitId + " AND pag_id = " + PagId + " AND mod_id = " + ModId + " AND obd_parentid = " + ParentId + " AND obd_deleted = 0 ORDER BY sit_id, pag_id, mod_id, sta_id, lng_id, obd_parentid, obd_order");
                    }
                    else if (DataFields["Alias"] != null)
                    {
                        PrimaryKey = DataFields["Alias"].ToString();
                        CacheItem = "LiquidCore.Objects.PrimaryKey(Alias(" + PrimaryKey + "))";
                        sSQL.Append("SELECT obd_id FROM obd_objectdata WHERE obd_alias = '" + PrimaryKey + "' AND obd_deleted = 0 ORDER BY sit_id, pag_id, mod_id, sta_id, lng_id, obd_parentid, obd_order");
                    }
                    else if (DataFields["ParentId"] != null)
                    {
                        PrimaryKey = DataFields["ParentId"].ToString();
                        CacheItem = "LiquidCore.Objects.PrimaryKey(ParentId(" + PrimaryKey + "))";
                        sSQL.Append("SELECT obd_id FROM obd_objectdata WHERE obd_parentid = " + PrimaryKey + " AND obd_deleted = 0 ORDER BY sit_id, pag_id, mod_id, sta_id, lng_id, obd_parentid, obd_order");
                    }
                    else
                    {
                        LoadAll(ref _objects);
                        return;
                    }
                    DataTable dt = null;
                    if (HttpContext.Current != null) if (HttpContext.Current != null) dt = (DataTable)HttpContext.Current.Cache[CacheItem];
                    if (dt == null)
                    {
                        using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true))
                        {
                            EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                            dt = oDo.GetDataTable(sSQL.ToString());
                            if (oDo.HasError)
                                throw new Exception(oDo.GetError);
                        }
                        CacheData.Insert(CacheItem, dt);
                    }
                    foreach (DataRow dr in dt.Rows)
                    {
                        Objects.Item o = new Objects.Item(Convert.ToInt32(dr["obd_id"].ToString()));
                        _objects.Add(o);
                    }
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void Update(ObjectModel Details)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::Update]";
                try
                {
                    ((ObjectModel)Details).UpdatedDate = DateTime.Now;
                    ((ObjectModel)Details).UpdatedBy = Authentication.User.Identity.Name;

                    System.Data.DataSet ds = new System.Data.DataSet();
                    StringBuilder sSQL = new StringBuilder();
                    sSQL.Append("UPDATE obd_objectdata SET ");
                    sSQL.Append("sit_id = " + Details.SitId.ToString() + ", ");
                    sSQL.Append("pag_id = " + Details.PagId.ToString() + ", ");
                    sSQL.Append("mod_id = " + Details.ModId.ToString() + ", ");
                    sSQL.Append("sta_id = " + Details.Status.ToString() + ", ");
                    sSQL.Append("lng_id = " + Details.Language.ToString() + ", ");
                    sSQL.Append("obd_parentid = " + Details.ParentId.ToString() + ", ");
                    sSQL.Append("obd_order = " + Details.Order.ToString() + ", ");
                    sSQL.Append("obd_type = " + Details.Type.ToString() + ", ");
                    sSQL.Append("obd_title = '" + Details.Title + "', ");
                    sSQL.Append("obd_alias = '" + Details.Alias + "', ");
                    sSQL.Append("obd_description = '" + Details.Description + "', ");
                    sSQL.Append("obd_varchar1 = '" + Details.Value1 + "', ");
                    sSQL.Append("obd_varchar2 = '" + Details.Value2 + "', ");
                    sSQL.Append("obd_varchar3 = '" + Details.Value3 + "', ");
                    sSQL.Append("obd_varchar4 = '" + Details.Value4 + "', ");
                    sSQL.Append("obd_varchar5 = '" + Details.Value5 + "', ");
                    sSQL.Append("obd_varchar6 = '" + Details.Value6 + "', ");
                    sSQL.Append("obd_varchar7 = '" + Details.Value7 + "', ");
                    sSQL.Append("obd_varchar8 = '" + Details.Value8 + "', ");
                    sSQL.Append("obd_varchar9 = '" + Details.Value9 + "', ");
                    sSQL.Append("obd_varchar10 = '" + Details.Value10 + "', ");
                    sSQL.Append("obd_varchar11 = '" + Details.Value11 + "', ");
                    sSQL.Append("obd_varchar12 = '" + Details.Value12 + "', ");
                    sSQL.Append("obd_varchar13 = '" + Details.Value13 + "', ");
                    sSQL.Append("obd_varchar14 = '" + Details.Value14 + "', ");
                    sSQL.Append("obd_varchar15 = '" + Details.Value15 + "', ");
                    sSQL.Append("obd_varchar16 = '" + Details.Value16 + "', ");
                    sSQL.Append("obd_varchar17 = '" + Details.Value17 + "', ");
                    sSQL.Append("obd_varchar18 = '" + Details.Value18 + "', ");
                    sSQL.Append("obd_varchar19 = '" + Details.Value19 + "', ");
                    sSQL.Append("obd_varchar20 = '" + Details.Value20 + "', ");
                    sSQL.Append("obd_varchar21 = '" + Details.Value21 + "', ");
                    sSQL.Append("obd_varchar22 = '" + Details.Value22 + "', ");
                    sSQL.Append("obd_varchar23 = '" + Details.Value23 + "', ");
                    sSQL.Append("obd_varchar24 = '" + Details.Value24 + "', ");
                    sSQL.Append("obd_varchar25 = '" + Details.Value25 + "', ");
                    sSQL.Append("obd_varchar26 = '" + Details.Value26 + "', ");
                    sSQL.Append("obd_varchar27 = '" + Details.Value27 + "', ");
                    sSQL.Append("obd_varchar28 = '" + Details.Value28 + "', ");
                    sSQL.Append("obd_varchar29 = '" + Details.Value29 + "', ");
                    sSQL.Append("obd_varchar30 = '" + Details.Value30 + "', ");
                    sSQL.Append("obd_varchar31 = '" + Details.Value31 + "', ");
                    sSQL.Append("obd_varchar32 = '" + Details.Value32 + "', ");
                    sSQL.Append("obd_varchar33 = '" + Details.Value33 + "', ");
                    sSQL.Append("obd_varchar34 = '" + Details.Value34 + "', ");
                    sSQL.Append("obd_varchar35 = '" + Details.Value35 + "', ");
                    sSQL.Append("obd_varchar36 = '" + Details.Value36 + "', ");
                    sSQL.Append("obd_varchar37 = '" + Details.Value37 + "', ");
                    sSQL.Append("obd_varchar38 = '" + Details.Value38 + "', ");
                    sSQL.Append("obd_varchar39 = '" + Details.Value39 + "', ");
                    sSQL.Append("obd_varchar40 = '" + Details.Value40 + "', ");
                    sSQL.Append("obd_varchar41 = '" + Details.Value41 + "', ");
                    sSQL.Append("obd_varchar42 = '" + Details.Value42 + "', ");
                    sSQL.Append("obd_varchar43 = '" + Details.Value43 + "', ");
                    sSQL.Append("obd_varchar44 = '" + Details.Value44 + "', ");
                    sSQL.Append("obd_varchar45 = '" + Details.Value45 + "', ");
                    sSQL.Append("obd_varchar46 = '" + Details.Value46 + "', ");
                    sSQL.Append("obd_varchar47 = '" + Details.Value47 + "', ");
                    sSQL.Append("obd_varchar48 = '" + Details.Value48 + "', ");
                    sSQL.Append("obd_varchar49 = '" + Details.Value49 + "', ");
                    sSQL.Append("obd_varchar50 = '" + Details.Value50 + "', ");
                    sSQL.Append("obd_createddate = '" + Details.CreatedDate.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "', ");
                    sSQL.Append("obd_createdby = '" + Details.CreatedBy + "', ");
                    sSQL.Append("obd_updateddate = '" + Details.UpdatedDate.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "', ");
                    sSQL.Append("obd_updatedby = '" + Details.UpdatedBy + "', ");
                    sSQL.Append("obd_hidden = " + (!Details.Hidden ? "0" : "1") + ", ");
                    sSQL.Append("obd_deleted = " + (!Details.Deleted ? "0" : "1") + " ");
                    sSQL.Append("WHERE obd_id = " + Details.Id.ToString());
                    using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, true))
                    {
                        EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                        oDo.ExecuteNonQuery(sSQL.ToString());
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);

                        //EventLog.LogEvent("CALL sort_obd_objectdata();", FUNCTIONNAME, String.Empty);
                        //oDo.ExecuteNonQuery("CALL sort_obd_objectdata();");
                        //if (oDo.HasError)
                        //    throw new Exception(oDo.GetError);

                    }
                    SortAll(Details.SitId, Details.PagId, Details.ModId, Details.Status, Details.Language, Details.ParentId);
                    CacheData.Reset("LiquidCore.Objects.PrimaryKey(AliasAndSettingParameters");
                    CacheData.Reset("LiquidCore.Objects.PrimaryKeys(SitId(" + Details.SitId.ToString() + "), PagId(" + Details.PagId.ToString() + ")");
                    CacheData.Reset("LiquidCore.Objects.PrimaryKey(Alias(" + Details.Alias + "))");
                    CacheData.Reset("LiquidCore.Objects.PrimaryKey(ParentId(" + Details.ParentId.ToString() + "))");

                    sSQL = new StringBuilder();
                    sSQL.Append("SELECT obd_id FROM obd_objectdata WHERE sit_id = " + Details.SitId.ToString() + " AND pag_id = " + Details.PagId.ToString() + " AND mod_id = " + Details.ModId.ToString() + " AND obd_parentid = " + Details.ParentId.ToString() + " AND obd_deleted = 0");
                    using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, false))
                    {
                        DataTable dt = oDo.GetDataTable(sSQL.ToString());
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);
                        foreach (DataRow dr in dt.Rows)
                            CacheData.Reset("LiquidCore.Objects.Object.Id(" + dr["obd_id"].ToString() + ")");
                    }
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
			public static void Delete(ObjectModel Details, NameValueSet DataFields)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::Delete]";
                try
                {
                    RecursiveDelete(Details.Id, DataFields["Recursive"]);
                    //using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, false))
                    //{
                    //    EventLog.LogEvent("CALL sort_obd_objectdata();", FUNCTIONNAME, String.Empty);
                    //    oDo.ExecuteNonQuery("CALL sort_obd_objectdata();");
                    //    if (oDo.HasError)
                    //        throw new Exception(oDo.GetError);

                    //    EventLog.LogEvent("CALL sort_set_settings();", FUNCTIONNAME, String.Empty);
                    //    oDo.ExecuteNonQuery("CALL sort_set_settings();");
                    //    if (oDo.HasError)
                    //        throw new Exception(oDo.GetError);
                    //}
                    SortAll(Details.SitId, Details.PagId, Details.ModId, Details.Status, Details.Language, Details.ParentId);
                    CacheData.Reset("LiquidCore.Objects.PrimaryKey(AliasAndSettingParameters");
                    CacheData.Reset("LiquidCore.Objects.PrimaryKeys(SitId(" + Details.SitId.ToString() + "), PagId(" + Details.PagId.ToString() + ")");
                    CacheData.Reset("LiquidCore.Objects.PrimaryKey(Alias(" + Details.Alias + "))");
                    CacheData.Reset("LiquidCore.Objects.PrimaryKey(ParentId(" + Details.ParentId.ToString() + "))");

                    StringBuilder sSQL = new StringBuilder();
                    sSQL.Append("SELECT obd_id FROM obd_objectdata WHERE sit_id = " + Details.SitId.ToString() + " AND pag_id = " + Details.PagId.ToString() + " AND mod_id = " + Details.ModId.ToString() + " AND obd_parentid = " + Details.ParentId.ToString() + " AND obd_deleted = 0");
                    using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, false))
                    {
                        DataTable dt = oDo.GetDataTable(sSQL.ToString());
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);
                        foreach (DataRow dr in dt.Rows)
                            CacheData.Reset("LiquidCore.Objects.Object.Id(" + dr["obd_id"].ToString() + ")");
                    }
                    Details = null;
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
			public static void RecursiveDelete(Int32 Id, System.Object recursive)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::RecursiveDelete]";
                try
                {
                    if (Id.Equals(0))
                        return;

                    DataTable dt = new DataTable();
                    using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, true))
                    {
                        String sSQL1 = "SELECT obd_id FROM obd_objectdata WHERE obd_parentid = " + Id.ToString();
                        String sSQL2 = "UPDATE set_settings SET set_deleted = 1 WHERE set_pointer = " + Id.ToString();
                        String sSQL3 = "UPDATE obd_objectdata SET obd_deleted = 1 WHERE obd_id = " + Id.ToString();

                        EventLog.LogEvent(sSQL1, FUNCTIONNAME, String.Empty);
                        System.Diagnostics.Debug.WriteLine(sSQL1);
                        dt = oDo.GetDataTable(sSQL1);
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);

                        EventLog.LogEvent(sSQL2, FUNCTIONNAME, String.Empty);
                        System.Diagnostics.Debug.WriteLine(sSQL2);
                        oDo.ExecuteNonQuery(sSQL2);
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);

                        EventLog.LogEvent(sSQL3, FUNCTIONNAME, String.Empty);
                        System.Diagnostics.Debug.WriteLine(sSQL3);
                        oDo.ExecuteNonQuery(sSQL3);
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);
                    }
					if (recursive.Equals(true))
					{
						foreach (DataRow dr in dt.Rows)
						{
							Int32 ParentId = 0;
							Int32.TryParse(dr["obd_id"].ToString(), out ParentId);
							RecursiveDelete(ParentId, true);
						}
					}
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void ResetThis()
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::ResetThis]";
                try
                {
                    
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            //[ObsoleteAttribute("SortAll has been deprecated. All sorting take place in stored procedures instead.")]
            public static void SortAll(Int32 SitId, Int32 PagId, Int32 ModId, Int32 Status, Int32 Language, Int32 ParentId)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::SortAll]";
                try
                {
                    //using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, false))
                    //{
                    //    int ret = oDo.ExecuteNonQuery("CALL SetOrder();");
                    //    if (oDo.HasError)
                    //        throw new Exception(oDo.GetError);
                    //}
                    //ResetThis();

                    Int32 Order = (Int32)OrderMinMax.Min;
                    StringBuilder sSQL = new StringBuilder();
                    sSQL.Append("SELECT obd_id FROM obd_objectdata WHERE sit_id = " + SitId.ToString() + " AND pag_id = " + PagId.ToString() + " AND mod_id = " + ModId.ToString() + " AND sta_id = " + Status.ToString() + " AND lng_id = " + Language.ToString() + " AND obd_parentid = " + ParentId.ToString() + " AND obd_deleted = 0 ORDER BY sit_id, pag_id, mod_id, sta_id, lng_id, obd_parentid, obd_order");
                    using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, false))
                    {
                        EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                        DataTable dt = oDo.GetDataTable(sSQL.ToString());
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);
                        StringBuilder sSQL2 = new StringBuilder();
                        foreach (DataRow dr in dt.Rows)
                        {
                            sSQL2.AppendLine("UPDATE obd_objectdata SET obd_order = " + Order.ToString() + " WHERE obd_id = " + dr["obd_id"].ToString() + ";");
                            Order = Order + (Int32)OrderMinMax.Step;
                            if (sSQL2.Length > DataBlockSize)
                            {
                                EventLog.LogEvent(sSQL2.ToString(), FUNCTIONNAME, String.Empty);
                                if (!(oDo.ExecuteNonQuery(sSQL2.ToString()) > 0))
                                    throw new Exception(oDo.GetError);
                                sSQL2 = new StringBuilder();
                            }
                        }
                        if (sSQL2.Length > 0)
                        {
                            EventLog.LogEvent(sSQL2.ToString(), FUNCTIONNAME, String.Empty);
                            if (!(oDo.ExecuteNonQuery(sSQL2.ToString()) > 0))
                                throw new Exception(oDo.GetError);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static Boolean FindChildren(ObjectModel Details)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::FindChildren]";
                DataTable dt = null;
                try
                {
                    String CacheItem = "LiquidCore.Objects.Object.Children(" + Details.Id.ToString() + ")";
                    if (HttpContext.Current != null) if (HttpContext.Current != null) dt = (DataTable)HttpContext.Current.Cache[CacheItem];
                    if (dt == null)
                    {
                        StringBuilder sSQL = new StringBuilder();
                        sSQL.Append("SELECT obd_id FROM obd_objectdata WHERE obd_deleted = 0 AND obd_parentid = " + Details.Id.ToString());
                        using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true))
                        {
                            EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                            dt = oDo.GetDataTable(sSQL.ToString());
                        }
                        CacheData.Insert(CacheItem, dt);
                    }
                    if (dt.Rows.Count > 0)
                        return true;

                    return false;
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                    return false;
                }
            }
        }
        public static class ModelData
        {
            static string CLASSNAME = "[Namespace::LiquidCore][Class::ModelData]";

            static Int32 DataBlockSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Data.DataBlockSize"].ToString());
            static Int32 UseRevision = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Data.UseRevision"].ToString());

            public static void Create(ModelModel Details)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::Create]";
                try
                {
                    ((ModelModel)Details).Order = (Int32)OrderMinMax.Max;
                    ((ModelModel)Details).UpdatedDate = DateTime.Now;
                    ((ModelModel)Details).UpdatedBy = Authentication.User.Identity.Name;
                    ((ModelModel)Details).CreatedDate = DateTime.Now;
                    ((ModelModel)Details).CreatedBy = Authentication.User.Identity.Name;

                    System.Data.DataSet ds = new System.Data.DataSet();
                    StringBuilder sSQL = new StringBuilder();
                    sSQL.Append("INSERT INTO mdl_model (sit_id, sta_id, lng_id, mdl_parentid, mdl_order, mdl_title, ");
                    sSQL.Append("mdl_alias, mdl_description, mdl_createddate, mdl_createdby, ");
                    sSQL.Append("mdl_updateddate, mdl_updatedby, mdl_hidden, mdl_deleted) VALUES ( ");
                    sSQL.Append(Details.SitId.ToString() + ", ");
                    sSQL.Append(Details.Status.ToString() + ", ");
                    sSQL.Append(Details.Language.ToString() + ", ");
                    sSQL.Append(Details.ParentId.ToString() + ", ");
                    sSQL.Append(Details.Order.ToString() + ", ");
                    sSQL.Append("'" + Details.Title + "', ");
                    sSQL.Append("'" + Details.Alias + "', ");
                    sSQL.Append("'" + Details.Description + "', ");
                    sSQL.Append("'" + Details.CreatedDate.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "', ");
                    sSQL.Append("'" + Details.CreatedBy + "', ");
                    sSQL.Append("'" + Details.UpdatedDate.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "', ");
                    sSQL.Append("'" + Details.UpdatedBy + "', ");
                    sSQL.Append("" + (Details.Hidden ? "1" : "0") + ", ");
                    sSQL.Append("" + (Details.Deleted ? "1" : "0") + ")");
                    using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, true))
                    {
                        EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                        oDo.ExecuteNonQuery(sSQL.ToString());
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);

                        EventLog.LogEvent("SELECT @@IDENTITY", FUNCTIONNAME, String.Empty);
                        ((ModelModel)Details).Id = Convert.ToInt32(oDo.GetDataTable("SELECT @@IDENTITY").Rows[0][0]);
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);

                        //EventLog.LogEvent("CALL sort_mdl_model();", FUNCTIONNAME, String.Empty);
                        //oDo.ExecuteNonQuery("CALL sort_mdl_model();");
                        //if (oDo.HasError)
                        //    throw new Exception(oDo.GetError);

                        
                    }
                    SortAll();
                    ResetThis();
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void Load(ModelObject Details, NameValueSet DataFields)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::Load]";
                try
                {
                    System.Data.DataTable dt = null;
                    String CacheItem = "LiquidCore.Models.Model.Id(" + DataFields["Id"].ToString() + ")";
                    if (HttpContext.Current != null) if (HttpContext.Current != null) dt = (DataTable)HttpContext.Current.Cache[CacheItem];
                    if (dt == null)
                    {
                        StringBuilder sSQL = new StringBuilder();
                        sSQL.Append("SELECT * FROM mdl_model WHERE mdl_id = " + DataFields["Id"].ToString() + " AND mdl_deleted = 0");
                        using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true))
                        {
                            EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                            dt = oDo.GetDataTable(sSQL.ToString());
                            if (oDo.HasError)
                                throw new Exception(oDo.GetError);
                        }
                        CacheData.Insert(CacheItem, dt);
                    }
                    if (dt.Rows.Count > 0)
                    {
                        System.Data.DataRow dr = dt.Rows[0];
                        ((ModelModel)Details).Id = Convert.ToInt32(dr["mdl_id"]);
                        ((ModelModel)Details).SitId = Convert.ToInt32(dr["sit_id"]);
                        ((ModelModel)Details).Status = Convert.ToInt32(dr["sta_id"]);
                        ((ModelModel)Details).Language = Convert.ToInt32(dr["lng_id"]);
                        ((ModelModel)Details).ParentId = Convert.ToInt32(dr["mdl_parentid"]);
                        ((ModelModel)Details).Order = Convert.ToInt32(dr["mdl_order"]);
                        ((ModelModel)Details).Title = Convert.ToString(dr["mdl_title"]);
                        ((ModelModel)Details).Alias = Convert.ToString(dr["mdl_alias"]);
                        ((ModelModel)Details).Description = Convert.ToString(dr["mdl_description"]);
                        ((ModelModel)Details).CreatedDate = Convert.ToDateTime(dr["mdl_createddate"]);
                        ((ModelModel)Details).CreatedBy = Convert.ToString(dr["mdl_createdby"]);
                        ((ModelModel)Details).UpdatedDate = Convert.ToDateTime(dr["mdl_updateddate"]);
                        ((ModelModel)Details).UpdatedBy = Convert.ToString(dr["mdl_updatedby"]);
                        ((ModelModel)Details).Hidden = (dr["mdl_hidden"].ToString().Equals("0") ? false : true);
                        ((ModelModel)Details).Deleted = (dr["mdl_deleted"].ToString().Equals("0") ? false : true);
                    }
                    else
                        ((ModelModel)Details).Deleted = true;
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void LoadAll(ref List<Model> _models)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::LoadAll]";
                try
                {
                    DataTable dt = null;
                    String CacheItem = "LiquidCore.Models";
                    if (HttpContext.Current != null) if (HttpContext.Current != null) dt = (DataTable)HttpContext.Current.Cache[CacheItem];
                    if (dt == null)
                    {
                        StringBuilder sSQL = new StringBuilder();
                        sSQL.Append("SELECT mdl_id FROM mdl_model WHERE mdl_deleted = 0 ORDER BY sit_id, sta_id, ");
                        sSQL.Append("lng_id, mdl_parentid, mdl_order");
                        using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true))
                        {
                            EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                            dt = oDo.GetDataTable(sSQL.ToString());
                            if (oDo.HasError)
                                throw new Exception(oDo.GetError);
                        }
                        CacheData.Insert(CacheItem, dt);
                    }
                    foreach (DataRow dr in dt.Rows)
                    {
                        Model m = new Model(Convert.ToInt32(dr["mdl_id"].ToString()));
                        _models.Add(m);
                    }
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void LoadAll(ref List<Model> _models, NameValueSet DataFields)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::LoadAll]";
                try
                {
                    String PrimaryKey = String.Empty;
                    String CacheItem = String.Empty;
                    StringBuilder sSQL = new StringBuilder();
                    if (DataFields["SitId"] != null && DataFields["ParentId"] != null)
                    {
                        String SitId = DataFields["SitId"].ToString();
                        String ParentId = DataFields["ParentId"].ToString();
                        CacheItem = "LiquidCore.Models.PrimaryKeys(SitId(" + SitId + "), ParentId(" + ParentId + "))";
                        sSQL.Append("SELECT mdl_id FROM mdl_model WHERE sit_id = " + SitId + " AND mdl_parentid = " + ParentId + " AND mdl_deleted = 0 ORDER BY sit_id, sta_id, lng_id, mdl_parentid, mdl_order");
                    }
                    else
                    {
                        LoadAll(ref _models);
                        return;
                    }
                    DataTable dt = null;
                    if (HttpContext.Current != null) if (HttpContext.Current != null) dt = (DataTable)HttpContext.Current.Cache[CacheItem];
                    if (dt == null)
                    {
                        using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true))
                        {
                            EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                            dt = oDo.GetDataTable(sSQL.ToString());
                            if (oDo.HasError)
                                throw new Exception(oDo.GetError);
                        }
                        CacheData.Insert(CacheItem, dt);
                    }
                    foreach (DataRow dr in dt.Rows)
                    {
                        Model m = new Model(Convert.ToInt32(dr["mdl_id"].ToString()));
                        _models.Add(m);
                    }
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void Update(ModelModel Details)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::Update]";
                try
                {
                    ((ModelModel)Details).UpdatedDate = DateTime.Now;
                    ((ModelModel)Details).UpdatedBy = Authentication.User.Identity.Name;

                    System.Data.DataSet ds = new System.Data.DataSet();
                    StringBuilder sSQL = new StringBuilder();
                    sSQL.Append("UPDATE mdl_model SET ");
                    sSQL.Append("sit_id = " + Details.SitId.ToString() + ", ");
                    sSQL.Append("sta_id = " + Details.Status.ToString() + ", ");
                    sSQL.Append("lng_id = " + Details.Language.ToString() + ", ");
                    sSQL.Append("mdl_parentid = " + Details.ParentId.ToString() + ", ");
                    sSQL.Append("mdl_order = " + Details.Order.ToString() + ", ");
                    sSQL.Append("mdl_title = '" + Details.Title + "', ");
                    sSQL.Append("mdl_alias = '" + Details.Alias + "', ");
                    sSQL.Append("mdl_description = '" + Details.Description + "', ");
                    sSQL.Append("mdl_createddate = '" + Details.CreatedDate.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "', ");
                    sSQL.Append("mdl_createdby = '" + Details.CreatedBy + "', ");
                    sSQL.Append("mdl_updateddate = '" + Details.UpdatedDate.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "', ");
                    sSQL.Append("mdl_updatedby = '" + Details.UpdatedBy + "', ");
                    sSQL.Append("mdl_hidden = " + (!Details.Hidden ? "0" : "1") + ", ");
                    sSQL.Append("mdl_deleted = " + (!Details.Deleted ? "0" : "1") + " ");
                    sSQL.Append("WHERE mdl_id = " + Details.Id.ToString());
                    using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, true))
                    {
                        EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                        oDo.ExecuteNonQuery(sSQL.ToString());
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);

                        //EventLog.LogEvent("CALL sort_mdl_model();", FUNCTIONNAME, String.Empty);
                        //oDo.ExecuteNonQuery("CALL sort_mdl_model();");
                        //if (oDo.HasError)
                        //    throw new Exception(oDo.GetError);

                    }
                    SortAll();

                    ResetThis();
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void Delete(ModelModel Details)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::Delete]";
                try
                {
                    RecursiveDelete(Details.Id);
                    //using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, true))
                    //{
                    //    EventLog.LogEvent("CALL sort_mdl_model();", FUNCTIONNAME, String.Empty);
                    //    System.Diagnostics.Debug.WriteLine("oDo.ExecuteNonQuery(sort_mdl_model);");
                    //    oDo.ExecuteNonQuery("CALL sort_mdl_model();");
                    //    if (oDo.HasError)
                    //        throw new Exception(oDo.GetError);

                    //    EventLog.LogEvent("CALL sort_mdi_modelitems();", FUNCTIONNAME, String.Empty);
                    //    System.Diagnostics.Debug.WriteLine("oDo.ExecuteNonQuery(sort_mdi_modelitems);");
                    //    oDo.ExecuteNonQuery("CALL sort_mdi_modelitems();");
                    //    if (oDo.HasError)
                    //        throw new Exception(oDo.GetError);
                    //}
                    SortAll();
                    LiquidCore.Data.ModelData.ResetThis();
                    LiquidCore.Data.ModelItemData.ResetThis();
                    Details = null;
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void RecursiveDelete(Int32 Id)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::RecursiveDelete]";
                try
                {
                    if (Id.Equals(0))
                        return;

                    using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, true))
                    {
                        String sSQL1 = "UPDATE mdl_model SET mdl_deleted = 1 WHERE mdl_id = " + Id.ToString();

                        EventLog.LogEvent(sSQL1, FUNCTIONNAME, String.Empty);
                        System.Diagnostics.Debug.WriteLine(sSQL1);
                        oDo.ExecuteNonQuery(sSQL1);
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);
                    }
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void ResetThis()
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::ResetThis]";
                try
                {
                    CacheData.Reset("LiquidCore.Models");
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            //[ObsoleteAttribute("SortAll has been deprecated. All sorting take place in stored procedures instead.")]
            public static void SortAll()
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::SortAll]";
                try
                {
                    Int32 Order = (Int32)OrderMinMax.Min;
                    StringBuilder sSQL = new StringBuilder();
                    sSQL.Append("SELECT mdl_id FROM mdl_model WHERE mdl_deleted = 0 ORDER BY sit_id, sta_id, lng_id, mdl_parentid, mdl_order");
                    using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, false))
                    {
                        EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                        DataTable dt = oDo.GetDataTable(sSQL.ToString());
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);
                        StringBuilder sSQL2 = new StringBuilder();
                        foreach (DataRow dr in dt.Rows)
                        {
                            sSQL2.AppendLine("UPDATE mdl_model SET mdl_order = " + Order.ToString() + " WHERE mdl_id = " + dr["mdl_id"].ToString() + ";");
                            Order = Order + (Int32)OrderMinMax.Step;
                            if (sSQL2.Length > DataBlockSize)
                            {
                                EventLog.LogEvent(sSQL2.ToString(), FUNCTIONNAME, String.Empty);
                                if (!(oDo.ExecuteNonQuery(sSQL2.ToString()) > 0))
                                    throw new Exception(oDo.GetError);
                                sSQL2 = new StringBuilder();
                            }
                        }
                        if (sSQL2.Length > 0)
                        {
                            EventLog.LogEvent(sSQL2.ToString(), FUNCTIONNAME, String.Empty);
                            if (!(oDo.ExecuteNonQuery(sSQL2.ToString()) > 0))
                                throw new Exception(oDo.GetError);
                        }
                        ResetThis();
                    }
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static Boolean FindChildren(ModelModel Details)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::FindChildren]";
                DataTable dt = null;
                try
                {
                    String CacheItem = "LiquidCore.Models.Model.Children(" + Details.Id.ToString() + ")";
                    if (HttpContext.Current != null) if (HttpContext.Current != null) dt = (DataTable)HttpContext.Current.Cache[CacheItem];
                    if (dt == null)
                    {
                        StringBuilder sSQL = new StringBuilder();
                        sSQL.Append("SELECT mdl_id FROM mdl_model WHERE mdl_deleted = 0 AND mdl_parentid = " + Details.Id.ToString());
                        using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true))
                        {
                            EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                            dt = oDo.GetDataTable(sSQL.ToString());
                        }
                        CacheData.Insert(CacheItem, dt);
                    }
                    if (dt.Rows.Count > 0)
                        return true;

                    return false;
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                    return false;
                }
            }
        }
        public static class ModelItemData
        {
            static string CLASSNAME = "[Namespace::LiquidCore][Class::ModelItemData]";

            static Int32 DataBlockSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Data.DataBlockSize"].ToString());
            static Int32 UseRevision = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Data.UseRevision"].ToString());

            public static void Create(ModelItemModel Details)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::Create]";
                try
                {
                    ((ModelItemModel)Details).Order = (Int32)OrderMinMax.Max;
                    ((ModelItemModel)Details).UpdatedDate = DateTime.Now;
                    ((ModelItemModel)Details).UpdatedBy = Authentication.User.Identity.Name;
                    ((ModelItemModel)Details).CreatedDate = DateTime.Now;
                    ((ModelItemModel)Details).CreatedBy = Authentication.User.Identity.Name;

                    System.Data.DataSet ds = new System.Data.DataSet();
                    StringBuilder sSQL = new StringBuilder();
                    sSQL.Append("INSERT INTO mdi_modelitems (sit_id, mdl_id, mde_id, sta_id, lng_id, mdi_parentid, mdi_order, mdi_contentpane, ");
                    sSQL.Append("mdi_createddate, mdi_createdby, ");
                    sSQL.Append("mdi_updateddate, mdi_updatedby, mdi_hidden, mdi_deleted) VALUES ( ");
                    sSQL.Append(Details.SitId.ToString() + ", ");
                    sSQL.Append(Details.MdlId.ToString() + ", ");
                    sSQL.Append(Details.MdeId.ToString() + ", ");
                    sSQL.Append(Details.Status.ToString() + ", ");
                    sSQL.Append(Details.Language.ToString() + ", ");
                    sSQL.Append(Details.ParentId.ToString() + ", ");
                    sSQL.Append(Details.Order.ToString() + ", ");
                    sSQL.Append("'" + Details.ContentPane + "', ");
                    sSQL.Append("'" + Details.CreatedDate.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "', ");
                    sSQL.Append("'" + Details.CreatedBy + "', ");
                    sSQL.Append("'" + Details.UpdatedDate.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "', ");
                    sSQL.Append("'" + Details.UpdatedBy + "', ");
                    sSQL.Append("" + (Details.Hidden ? "1" : "0") + ", ");
                    sSQL.Append("" + (Details.Deleted ? "1" : "0") + ")");
                    using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, true))
                    {
                        EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                        oDo.ExecuteNonQuery(sSQL.ToString());
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);

                        EventLog.LogEvent("SELECT @@IDENTITY", FUNCTIONNAME, String.Empty);
                        ((ModelItemModel)Details).Id = Convert.ToInt32(oDo.GetDataTable("SELECT @@IDENTITY").Rows[0][0]);
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);

                        //EventLog.LogEvent("CALL sort_mdi_modelitems();", FUNCTIONNAME, String.Empty);
                        //oDo.ExecuteNonQuery("CALL sort_mdi_modelitems();");
                        //if (oDo.HasError)
                        //    throw new Exception(oDo.GetError);

                    }
                    SortAll();
                    ResetThis();
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void Load(ModelObject Details, NameValueSet DataFields)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::Load]";
                try
                {
                    System.Data.DataTable dt = null;
                    String CacheItem = "LiquidCore.ModelItems.ModelItem.Id(" + DataFields["Id"].ToString() + ")";
                    if (HttpContext.Current != null) if (HttpContext.Current != null) dt = (DataTable)HttpContext.Current.Cache[CacheItem];
                    if (dt == null)
                    {
                        StringBuilder sSQL = new StringBuilder();
                        sSQL.Append("SELECT * FROM mdi_modelitems WHERE mdi_id = " + DataFields["Id"].ToString() + " AND mdi_deleted = 0");
                        using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true))
                        {
                            EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                            dt = oDo.GetDataTable(sSQL.ToString());
                            if (oDo.HasError)
                                throw new Exception(oDo.GetError);
                        }
                        CacheData.Insert(CacheItem, dt);
                    }
                    if (dt.Rows.Count > 0)
                    {
                        System.Data.DataRow dr = dt.Rows[0];
                        ((ModelItemModel)Details).Id = Convert.ToInt32(dr["mdi_id"]);
                        ((ModelItemModel)Details).SitId = Convert.ToInt32(dr["sit_id"]);
                        ((ModelItemModel)Details).MdlId = Convert.ToInt32(dr["mdl_id"]);
                        ((ModelItemModel)Details).MdeId = Convert.ToInt32(dr["mde_id"]);
                        ((ModelItemModel)Details).Status = Convert.ToInt32(dr["sta_id"]);
                        ((ModelItemModel)Details).Language = Convert.ToInt32(dr["lng_id"]);
                        ((ModelItemModel)Details).ParentId = Convert.ToInt32(dr["mdi_parentid"]);
                        ((ModelItemModel)Details).Order = Convert.ToInt32(dr["mdi_order"]);
                        ((ModelItemModel)Details).ContentPane = Convert.ToString(dr["mdi_contentpane"]);
                        ((ModelItemModel)Details).CreatedDate = Convert.ToDateTime(dr["mdi_createddate"]);
                        ((ModelItemModel)Details).CreatedBy = Convert.ToString(dr["mdi_createdby"]);
                        ((ModelItemModel)Details).UpdatedDate = Convert.ToDateTime(dr["mdi_updateddate"]);
                        ((ModelItemModel)Details).UpdatedBy = Convert.ToString(dr["mdi_updatedby"]);
                        ((ModelItemModel)Details).Hidden = (dr["mdi_hidden"].ToString().Equals("0") ? false : true);
                        ((ModelItemModel)Details).Deleted = (dr["mdi_deleted"].ToString().Equals("0") ? false : true);
                    }
                    else
                        ((ModelItemModel)Details).Deleted = true;
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void LoadAll(ref List<ModelItem> _modelitems)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::LoadAll]";
                try
                {
                    DataTable dt = null;
                    String CacheItem = "LiquidCore.ModelItems";
                    if (HttpContext.Current != null) if (HttpContext.Current != null) dt = (DataTable)HttpContext.Current.Cache[CacheItem];
                    if (dt == null)
                    {
                        StringBuilder sSQL = new StringBuilder();
                        sSQL.Append("SELECT mdi_id FROM mdi_modelitems WHERE mdi_deleted = 0 ORDER BY sit_id, mdl_id, sta_id, ");
                        sSQL.Append("lng_id, mdi_parentid, mdi_order");
                        using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true))
                        {
                            EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                            dt = oDo.GetDataTable(sSQL.ToString());
                            if (oDo.HasError)
                                throw new Exception(oDo.GetError);
                        }
                        CacheData.Insert(CacheItem, dt);
                    }
                    foreach (DataRow dr in dt.Rows)
                    {
                        ModelItem m = new ModelItem(Convert.ToInt32(dr["mdi_id"].ToString()));
                        _modelitems.Add(m);
                    }
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void LoadAll(ref List<ModelItem> _modelitems, NameValueSet DataFields)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::LoadAll]";
                try
                {
                    String PrimaryKey = String.Empty;
                    String CacheItem = String.Empty;
                    StringBuilder sSQL = new StringBuilder();
                    if (DataFields["SitId"] != null && DataFields["MdlId"] != null && DataFields["ParentId"] != null)
                    {
                        String SitId = DataFields["SitId"].ToString();
                        String MdlId = DataFields["MdlId"].ToString();
                        String ParentId = DataFields["ParentId"].ToString();
                        CacheItem = "LiquidCore.ModelItems.PrimaryKeys(SitId(" + SitId + "), MdlId(" + MdlId + "), ParentId(" + ParentId + "))";
                        sSQL.Append("SELECT mdi_id FROM mdi_modelitems WHERE sit_id = " + SitId + " AND mdl_id = " + MdlId + " AND mdi_parentid = " + ParentId + " AND mdi_deleted = 0 ORDER BY sit_id, mdl_id, sta_id, lng_id, mdi_parentid, mdi_order");
                    }
                    else
                    {
                        LoadAll(ref _modelitems);
                        return;
                    }
                    DataTable dt = null;
                    if (HttpContext.Current != null) if (HttpContext.Current != null) dt = (DataTable)HttpContext.Current.Cache[CacheItem];
                    if (dt == null)
                    {
                        using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true))
                        {
                            EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                            dt = oDo.GetDataTable(sSQL.ToString());
                            if (oDo.HasError)
                                throw new Exception(oDo.GetError);
                        }
                        CacheData.Insert(CacheItem, dt);
                    }
                    foreach (DataRow dr in dt.Rows)
                    {
                        ModelItem m = new ModelItem(Convert.ToInt32(dr["mdi_id"].ToString()));
                        _modelitems.Add(m);
                    }
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void Update(ModelItemModel Details)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::Update]";
                try
                {
                    ((ModelItemModel)Details).UpdatedDate = DateTime.Now;
                    ((ModelItemModel)Details).UpdatedBy = Authentication.User.Identity.Name;

                    System.Data.DataSet ds = new System.Data.DataSet();
                    StringBuilder sSQL = new StringBuilder();
                    sSQL.Append("UPDATE mdi_modelitems SET ");
                    sSQL.Append("sit_id = " + Details.SitId.ToString() + ", ");
                    sSQL.Append("mdl_id = " + Details.MdlId.ToString() + ", ");
                    sSQL.Append("mde_id = " + Details.MdeId.ToString() + ", ");
                    sSQL.Append("sta_id = " + Details.Status.ToString() + ", ");
                    sSQL.Append("lng_id = " + Details.Language.ToString() + ", ");
                    sSQL.Append("mdi_parentid = " + Details.ParentId.ToString() + ", ");
                    sSQL.Append("mdi_order = " + Details.Order.ToString() + ", ");
                    sSQL.Append("mdi_contentpane = '" + Details.ContentPane + "', ");
                    sSQL.Append("mdi_createddate = '" + Details.CreatedDate.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "', ");
                    sSQL.Append("mdi_createdby = '" + Details.CreatedBy + "', ");
                    sSQL.Append("mdi_updateddate = '" + Details.UpdatedDate.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "', ");
                    sSQL.Append("mdi_updatedby = '" + Details.UpdatedBy + "', ");
                    sSQL.Append("mdi_hidden = " + (!Details.Hidden ? "0" : "1") + ", ");
                    sSQL.Append("mdi_deleted = " + (!Details.Deleted ? "0" : "1") + " ");
                    sSQL.Append("WHERE mdi_id = " + Details.Id.ToString());
                    using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, true))
                    {
                        EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                        oDo.ExecuteNonQuery(sSQL.ToString());
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);

                        //EventLog.LogEvent("CALL sort_mdi_modelitems();", FUNCTIONNAME, String.Empty);
                        //oDo.ExecuteNonQuery("CALL sort_mdi_modelitems();");
                        //if (oDo.HasError)
                        //    throw new Exception(oDo.GetError);

                    }
                    SortAll();

                    ResetThis();
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void Delete(ModelItemModel Details)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::Delete]";
                try
                {
                    RecursiveDelete(Details.Id);
                    //using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, true))
                    //{
                    //    EventLog.LogEvent("CALL sort_mdi_modelitems();", FUNCTIONNAME, String.Empty);
                    //    System.Diagnostics.Debug.WriteLine("oDo.ExecuteNonQuery(sort_mdi_modelitems);");
                    //    oDo.ExecuteNonQuery("CALL sort_mdi_modelitems();");
                    //    if (oDo.HasError)
                    //        throw new Exception(oDo.GetError);
                    //}
                    SortAll();
                    ResetThis();
                    Details = null;
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void RecursiveDelete(Int32 Id)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::RecursiveDelete]";
                try
                {
                    if (Id.Equals(0))
                        return;

                    using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, true))
                    {
                        String sSQL1 = "UPDATE mdi_modelitems SET mdi_deleted = 1 WHERE mdi_id = " + Id.ToString();

                        EventLog.LogEvent(sSQL1, FUNCTIONNAME, String.Empty);
                        System.Diagnostics.Debug.WriteLine(sSQL1);
                        oDo.ExecuteNonQuery(sSQL1);
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);
                    }
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void ResetThis()
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::ResetThis]";
                try
                {
                    CacheData.Reset("LiquidCore.ModelItems");
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            //[ObsoleteAttribute("SortAll has been deprecated. All sorting take place in stored procedures instead.")]
            public static void SortAll()
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::SortAll]";
                try
                {
                    Int32 Order = (Int32)OrderMinMax.Min;
                    StringBuilder sSQL = new StringBuilder();
                    sSQL.Append("SELECT mdi_id FROM mdi_modelitems WHERE mdi_deleted = 0 ORDER BY sit_id, mdl_id, sta_id, lng_id, mdi_parentid, mdi_order");
                    using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, false))
                    {
                        EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                        DataTable dt = oDo.GetDataTable(sSQL.ToString());
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);
                        StringBuilder sSQL2 = new StringBuilder();
                        foreach (DataRow dr in dt.Rows)
                        {
                            sSQL2.AppendLine("UPDATE mdi_modelitems SET mdi_order = " + Order.ToString() + " WHERE mdi_id = " + dr["mdi_id"].ToString() + ";");
                            Order = Order + (Int32)OrderMinMax.Step;
                            if (sSQL2.Length > DataBlockSize)
                            {
                                EventLog.LogEvent(sSQL2.ToString(), FUNCTIONNAME, String.Empty);
                                if (!(oDo.ExecuteNonQuery(sSQL2.ToString()) > 0))
                                    throw new Exception(oDo.GetError);
                                sSQL2 = new StringBuilder();
                            }
                        }
                        if (sSQL2.Length > 0)
                        {
                            EventLog.LogEvent(sSQL2.ToString(), FUNCTIONNAME, String.Empty);
                            if (!(oDo.ExecuteNonQuery(sSQL2.ToString()) > 0))
                                throw new Exception(oDo.GetError);
                        }
                        ResetThis();
                    }
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static Boolean FindChildren(ModelItemModel Details)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::FindChildren]";
                DataTable dt = null;
                try
                {
                    String CacheItem = "LiquidCore.ModelItems.ModelItem.Children(" + Details.Id.ToString() + ")";
                    if (HttpContext.Current != null) if (HttpContext.Current != null) dt = (DataTable)HttpContext.Current.Cache[CacheItem];
                    if (dt == null)
                    {
                        StringBuilder sSQL = new StringBuilder();
                        sSQL.Append("SELECT mdi_id FROM mdi_modelitems WHERE mdi_deleted = 0 AND mdi_parentid = " + Details.Id.ToString());
                        using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true))
                        {
                            EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                            dt = oDo.GetDataTable(sSQL.ToString());
                        }
                        CacheData.Insert(CacheItem, dt);
                    }
                    if (dt.Rows.Count > 0)
                        return true;

                    return false;
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                    return false;
                }
            }
        }
        public static class ModDefData
        {
            static string CLASSNAME = "[Namespace::LiquidCore][Class::ModDefData]";

            static Int32 DataBlockSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Data.DataBlockSize"].ToString());
            static Int32 UseRevision = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Data.UseRevision"].ToString());

            public static void Create(ModDefModel Details)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::Create]";
                try
                {
                    ((ModDefModel)Details).Order = (Int32)OrderMinMax.Max;
                    ((ModDefModel)Details).UpdatedDate = DateTime.Now;
                    ((ModDefModel)Details).UpdatedBy = Authentication.User.Identity.Name;
                    ((ModDefModel)Details).CreatedDate = DateTime.Now;
                    ((ModDefModel)Details).CreatedBy = Authentication.User.Identity.Name;

                    System.Data.DataSet ds = new System.Data.DataSet();
                    StringBuilder sSQL = new StringBuilder();
                    sSQL.Append("INSERT INTO mde_moduledefinitions (sit_id, sta_id, lng_id, mde_parentid, mde_order, mde_src, ");
                    sSQL.Append("mde_cachetime, mde_iconfile, mde_title, mde_alias, mde_description, mde_createddate, mde_createdby, ");
                    sSQL.Append("mde_updateddate, mde_updatedby, mde_hidden, mde_deleted) VALUES ( ");
                    sSQL.Append(Details.SitId.ToString() + ", ");
                    sSQL.Append(Details.Status.ToString() + ", ");
                    sSQL.Append(Details.Language.ToString() + ", ");
                    sSQL.Append(Details.ParentId.ToString() + ", ");
                    sSQL.Append(Details.Order.ToString() + ", ");
                    sSQL.Append("'" + Details.Src + "', ");
                    sSQL.Append(Details.CacheTime + ", ");
                    sSQL.Append("'" + Details.Iconfile + "', ");
                    sSQL.Append("'" + Details.Title + "', ");
                    sSQL.Append("'" + Details.Alias + "', ");
                    sSQL.Append("'" + Details.Description + "', ");
                    sSQL.Append("'" + Details.CreatedDate.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "', ");
                    sSQL.Append("'" + Details.CreatedBy + "', ");
                    sSQL.Append("'" + Details.UpdatedDate.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "', ");
                    sSQL.Append("'" + Details.UpdatedBy + "', ");
                    sSQL.Append("" + (Details.Hidden ? "1" : "0") + ", ");
                    sSQL.Append("" + (Details.Deleted ? "1" : "0") + ")");
                    using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, true))
                    {
                        EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                        oDo.ExecuteNonQuery(sSQL.ToString());
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);

                        EventLog.LogEvent("SELECT @@IDENTITY", FUNCTIONNAME, String.Empty);
                        ((ModDefModel)Details).Id = Convert.ToInt32(oDo.GetDataTable("SELECT @@IDENTITY").Rows[0][0]);
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);

                        //EventLog.LogEvent("CALL sort_mde_modeldefinitions();", FUNCTIONNAME, String.Empty);
                        //oDo.ExecuteNonQuery("CALL sort_mde_modeldefinitions();");
                        //if (oDo.HasError)
                        //    throw new Exception(oDo.GetError);

                    }
                    SortAll();
                    ResetThis();
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void Load(ModelObject Details, NameValueSet DataFields)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::Load]";
                try
                {
                    System.Data.DataTable dt = null;
                    String CacheItem = "LiquidCore.ModDefs.ModDef.Id(" + DataFields["Id"].ToString() + ")";
                    if (HttpContext.Current != null) if (HttpContext.Current != null) dt = (DataTable)HttpContext.Current.Cache[CacheItem];
                    if (dt == null)
                    {
                        StringBuilder sSQL = new StringBuilder();
                        sSQL.Append("SELECT * FROM mde_moduledefinitions WHERE mde_id = " + DataFields["Id"].ToString() + " AND mde_deleted = 0");
                        using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true))
                        {
                            EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                            dt = oDo.GetDataTable(sSQL.ToString());
                            if (oDo.HasError)
                                throw new Exception(oDo.GetError);
                        }
                        CacheData.Insert(CacheItem, dt);
                    }
                    if (dt.Rows.Count > 0)
                    {
                        System.Data.DataRow dr = dt.Rows[0];
                        ((ModDefModel)Details).Id = Convert.ToInt32(dr["mde_id"]);
                        ((ModDefModel)Details).SitId = Convert.ToInt32(dr["sit_id"]);
                        ((ModDefModel)Details).Status = Convert.ToInt32(dr["sta_id"]);
                        ((ModDefModel)Details).Language = Convert.ToInt32(dr["lng_id"]);
                        ((ModDefModel)Details).ParentId = Convert.ToInt32(dr["mde_parentid"]);
                        ((ModDefModel)Details).Order = Convert.ToInt32(dr["mde_order"]);
                        ((ModDefModel)Details).Title = Convert.ToString(dr["mde_title"]);
                        ((ModDefModel)Details).Alias = Convert.ToString(dr["mde_alias"]);
                        ((ModDefModel)Details).Description = Convert.ToString(dr["mde_description"]);
                        ((ModDefModel)Details).Src = Convert.ToString(dr["mde_src"]);
                        ((ModDefModel)Details).CacheTime = Convert.ToInt32(dr["mde_cachetime"]);
                        ((ModDefModel)Details).Iconfile = Convert.ToString(dr["mde_iconfile"]);
                        ((ModDefModel)Details).CreatedDate = Convert.ToDateTime(dr["mde_createddate"]);
                        ((ModDefModel)Details).CreatedBy = Convert.ToString(dr["mde_createdby"]);
                        ((ModDefModel)Details).UpdatedDate = Convert.ToDateTime(dr["mde_updateddate"]);
                        ((ModDefModel)Details).UpdatedBy = Convert.ToString(dr["mde_updatedby"]);
                        ((ModDefModel)Details).Hidden = (dr["mde_hidden"].ToString().Equals("0") ? false : true);
                        ((ModDefModel)Details).Deleted = (dr["mde_deleted"].ToString().Equals("0") ? false : true);
                    }
                    else
                        ((ModDefModel)Details).Deleted = true;
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void LoadAll(ref List<ModDef> _moddefs)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::LoadAll]";
                try
                {
                    DataTable dt = null;
                    String CacheItem = "LiquidCore.ModDefs";
                    if (HttpContext.Current != null) if (HttpContext.Current != null) dt = (DataTable)HttpContext.Current.Cache[CacheItem];
                    if (dt == null)
                    {
                        StringBuilder sSQL = new StringBuilder();
                        sSQL.Append("SELECT mde_id FROM mde_moduledefinitions WHERE mde_deleted = 0 ORDER BY sit_id, sta_id, ");
                        sSQL.Append("lng_id, mde_parentid, mde_order");
                        using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true))
                        {
                            EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                            dt = oDo.GetDataTable(sSQL.ToString());
                            if (oDo.HasError)
                                throw new Exception(oDo.GetError);
                        }
                        CacheData.Insert(CacheItem, dt);
                    }
                    foreach (DataRow dr in dt.Rows)
                    {
                        ModDef m = new ModDef(Convert.ToInt32(dr["mde_id"].ToString()));
                        _moddefs.Add(m);
                    }
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void LoadAll(ref List<ModDef> _moddefs, NameValueSet DataFields)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::LoadAll]";
                try
                {
                    String PrimaryKey = String.Empty;
                    String CacheItem = String.Empty;
                    StringBuilder sSQL = new StringBuilder();
                    if (DataFields["SitId"] != null && DataFields["ParentId"] != null)
                    {
                        String SitId = DataFields["SitId"].ToString();
                        String ParentId = DataFields["ParentId"].ToString();
                        CacheItem = "LiquidCore.ModDefs.PrimaryKeys(SitId(" + SitId + "), ParentId(" + ParentId + "))";
                        sSQL.Append("SELECT mde_id FROM mde_moduledefinitions WHERE sit_id = " + SitId + " AND mde_parentid = " + ParentId + " AND mde_deleted = 0 ORDER BY sit_id, sta_id, lng_id, mde_parentid, mde_order");
                    }
                    else
                    {
                        LoadAll(ref _moddefs);
                        return;
                    }
                    DataTable dt = null;
                    if (HttpContext.Current != null) if (HttpContext.Current != null) dt = (DataTable)HttpContext.Current.Cache[CacheItem];
                    if (dt == null)
                    {
                        using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true))
                        {
                            EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                            dt = oDo.GetDataTable(sSQL.ToString());
                            if (oDo.HasError)
                                throw new Exception(oDo.GetError);
                        }
                        CacheData.Insert(CacheItem, dt);
                    }
                    foreach (DataRow dr in dt.Rows)
                    {
                        ModDef m = new ModDef(Convert.ToInt32(dr["mde_id"].ToString()));
                        _moddefs.Add(m);
                    }
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void Update(ModDefModel Details)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::Update]";
                try
                {
                    ((ModDefModel)Details).UpdatedDate = DateTime.Now;
                    ((ModDefModel)Details).UpdatedBy = Authentication.User.Identity.Name;

                    System.Data.DataSet ds = new System.Data.DataSet();
                    StringBuilder sSQL = new StringBuilder();
                    sSQL.Append("UPDATE mde_moduledefinitions SET ");
                    sSQL.Append("sit_id = " + Details.SitId.ToString() + ", ");
                    sSQL.Append("sta_id = " + Details.Status.ToString() + ", ");
                    sSQL.Append("lng_id = " + Details.Language.ToString() + ", ");
                    sSQL.Append("mde_parentid = " + Details.ParentId.ToString() + ", ");
                    sSQL.Append("mde_order = " + Details.Order.ToString() + ", ");
                    sSQL.Append("mde_title = '" + Details.Title + "', ");
                    sSQL.Append("mde_alias = '" + Details.Alias + "', ");
                    sSQL.Append("mde_description = '" + Details.Description + "', ");
                    sSQL.Append("mde_src = '" + Details.Src + "', ");
                    sSQL.Append("mde_cachetime = " + Details.CacheTime.ToString() + ", ");
                    sSQL.Append("mde_iconfile = '" + Details.Iconfile + "', ");
                    sSQL.Append("mde_createddate = '" + Details.CreatedDate.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "', ");
                    sSQL.Append("mde_createdby = '" + Details.CreatedBy + "', ");
                    sSQL.Append("mde_updateddate = '" + Details.UpdatedDate.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "', ");
                    sSQL.Append("mde_updatedby = '" + Details.UpdatedBy + "', ");
                    sSQL.Append("mde_hidden = " + (!Details.Hidden ? "0" : "1") + ", ");
                    sSQL.Append("mde_deleted = " + (!Details.Deleted ? "0" : "1") + " ");
                    sSQL.Append("WHERE mde_id = " + Details.Id.ToString());
                    using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, true))
                    {
                        EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                        oDo.ExecuteNonQuery(sSQL.ToString());
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);

                        //EventLog.LogEvent("CALL sort_mde_modeldefinitions();", FUNCTIONNAME, String.Empty);
                        //oDo.ExecuteNonQuery("CALL sort_mde_modeldefinitions();");
                        //if (oDo.HasError)
                        //    throw new Exception(oDo.GetError);

                    }
                    SortAll();

                    ResetThis();
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void Delete(ModDefModel Details)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::Delete]";
                try
                {
                    RecursiveDelete(Details.Id);
                    //using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, false))
                    //{
                    //    EventLog.LogEvent("CALL sort_mde_modeldefinitions();", FUNCTIONNAME, String.Empty);
                    //    oDo.ExecuteNonQuery("CALL sort_mde_modeldefinitions();");
                    //    if (oDo.HasError)
                    //        throw new Exception(oDo.GetError);
                    //}
                    SortAll();
                    ResetThis();
                    Details = null;
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void RecursiveDelete(Int32 Id)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::RecursiveDelete]";
                try
                {
                    if (Id.Equals(0))
                        return;

                    using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, true))
                    {
                        String sSQL1 = "UPDATE mde_moduledefinitions SET mde_deleted = 1 WHERE mde_id = " + Id.ToString();

                        EventLog.LogEvent(sSQL1, FUNCTIONNAME, String.Empty);
                        System.Diagnostics.Debug.WriteLine(sSQL1);
                        oDo.ExecuteNonQuery(sSQL1);
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);
                    }
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void ResetThis()
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::ResetThis]";
                try
                {
                    CacheData.Reset("LiquidCore.ModDefs");
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            //[ObsoleteAttribute("SortAll has been deprecated. All sorting take place in stored procedures instead.")]
            public static void SortAll()
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::SortAll]";
                try
                {
                    Int32 Order = (Int32)OrderMinMax.Min;
                    StringBuilder sSQL = new StringBuilder();
                    sSQL.Append("SELECT mde_id FROM mde_moduledefinitions WHERE mde_deleted = 0 ORDER BY sit_id, sta_id, lng_id, mde_parentid, mde_order");
                    using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, false))
                    {
                        EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                        DataTable dt = oDo.GetDataTable(sSQL.ToString());
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);
                        StringBuilder sSQL2 = new StringBuilder();
                        foreach (DataRow dr in dt.Rows)
                        {
                            sSQL2.AppendLine("UPDATE mde_moduledefinitions SET mde_order = " + Order.ToString() + " WHERE mde_id = " + dr["mde_id"].ToString() + ";");
                            Order = Order + (Int32)OrderMinMax.Step;
                            if (sSQL2.Length > DataBlockSize)
                            {
                                EventLog.LogEvent(sSQL2.ToString(), FUNCTIONNAME, String.Empty);
                                if (!(oDo.ExecuteNonQuery(sSQL2.ToString()) > 0))
                                    throw new Exception(oDo.GetError);
                                sSQL2 = new StringBuilder();
                            }
                        }
                        if (sSQL2.Length > 0)
                        {
                            EventLog.LogEvent(sSQL2.ToString(), FUNCTIONNAME, String.Empty);
                            if (!(oDo.ExecuteNonQuery(sSQL2.ToString()) > 0))
                                throw new Exception(oDo.GetError);
                        }
                        ResetThis();
                    }
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static Boolean FindChildren(ModDefModel Details)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::FindChildren]";
                DataTable dt = null;
                try
                {
                    String CacheItem = "LiquidCore.ModDefs.ModDef.Children(" + Details.Id.ToString() + ")";
                    if (HttpContext.Current != null) if (HttpContext.Current != null) dt = (DataTable)HttpContext.Current.Cache[CacheItem];
                    if (dt == null)
                    {
                        StringBuilder sSQL = new StringBuilder();
                        sSQL.Append("SELECT mde_id FROM mde_moduledefinitions WHERE mde_deleted = 0 AND mde_parentid = " + Details.Id.ToString());
                        using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true))
                        {
                            EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                            dt = oDo.GetDataTable(sSQL.ToString());
                        }
                        CacheData.Insert(CacheItem, dt);
                    }
                    if (dt.Rows.Count > 0)
                        return true;

                    return false;
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                    return false;
                }
            }
        }
        public static class MenuData
        {
            static string CLASSNAME = "[Namespace::LiquidCore][Class::MenuData]";

            static Int32 DataBlockSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Data.DataBlockSize"].ToString());
            static Int32 UseRevision = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Data.UseRevision"].ToString());

            public static void Create(MenuModel.ItemModel Details, NameValueSet DataFields)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::Create]";
                try
                {
                    ((MenuModel.ItemModel)Details).Order = (Int32)OrderMinMax.Max;
                    ((MenuModel.ItemModel)Details).UpdatedDate = DateTime.Now;
                    ((MenuModel.ItemModel)Details).UpdatedBy = Authentication.User.Identity.Name;
                    ((MenuModel.ItemModel)Details).CreatedDate = DateTime.Now;
                    ((MenuModel.ItemModel)Details).CreatedBy = Authentication.User.Identity.Name;

                    System.Data.DataSet ds = new System.Data.DataSet();
                    StringBuilder sSQL = new StringBuilder();
                    sSQL.Append("INSERT INTO pag_pages (sit_id, sta_id, lng_id, mdl_id, pag_parentid, pag_order, pag_title, ");
                    sSQL.Append("pag_alias, pag_description, pag_template, pag_createddate, pag_createdby, ");
                    sSQL.Append("pag_updateddate, pag_updatedby, pag_hidden, pag_mobilehidden, pag_deleted) VALUES ( ");
                    sSQL.Append(Details.SitId.ToString() + ", ");
                    sSQL.Append(Details.Status.ToString() + ", ");
                    sSQL.Append(Details.Language.ToString() + ", ");
                    sSQL.Append(Details.ModelId.ToString() + ", ");
                    sSQL.Append(Details.ParentId.ToString() + ", ");
                    sSQL.Append(Details.Order.ToString() + ", ");
                    sSQL.Append("'" + Details.Title + "', ");
                    sSQL.Append("'" + Details.Alias + "', ");
                    sSQL.Append("'" + Details.Description + "', ");
                    sSQL.Append("'" + Details.Template + "', ");
                    sSQL.Append("'" + Details.CreatedDate.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "', ");
                    sSQL.Append("'" + Details.CreatedBy + "', ");
                    sSQL.Append("'" + Details.UpdatedDate.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "', ");
                    sSQL.Append("'" + Details.UpdatedBy + "', ");
                    sSQL.Append("" + (Details.Hidden ? "1" : "0") + ", ");
                    sSQL.Append("" + (Details.MobileHidden ? "1" : "0") + ", ");
                    sSQL.Append("" + (Details.Deleted ? "1" : "0") + ")");
                    using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, true))
                    {
                        EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty); 
                        oDo.ExecuteNonQuery(sSQL.ToString());
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);

                        EventLog.LogEvent("SELECT @@IDENTITY", FUNCTIONNAME, String.Empty); 
                        ((MenuModel.ItemModel)Details).Id = Convert.ToInt32(oDo.GetDataTable("SELECT @@IDENTITY").Rows[0][0]);
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);

                        //EventLog.LogEvent("CALL sort_pag_pages();", FUNCTIONNAME, String.Empty); 
                        //oDo.ExecuteNonQuery("CALL sort_pag_pages();");
                        //if (oDo.HasError)
                        //    throw new Exception(oDo.GetError);
                    }
					if (DataFields["PreventSortOnSave"].Equals(false))
					{
						SortAll(Details.Status, Details.Language, Details.ParentId);
					}
					CacheData.Reset("LiquidCore.Pages.PrimaryKeys(SitId(" + Details.SitId.ToString() + "), ParentId(" + Details.ParentId.ToString() + "), Status(" + Details.Status.ToString() + "))");
                    CacheData.Reset("LiquidCore.Pages.PrimaryKeys(SitId(" + Details.SitId.ToString() + "), ParentId(" + Details.ParentId.ToString() + "))");
                    CacheData.Reset("LiquidCore.Pages.PrimaryKey(Alias(" + Details.Alias + "))");

                    sSQL = new StringBuilder();
                    sSQL.Append("SELECT pag_id FROM pag_pages WHERE sit_id = " + Details.SitId.ToString() + " AND pag_parentid = " + Details.ParentId.ToString() + " AND pag_deleted = 0");
                    using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, false))
                    {
                        DataTable dt = oDo.GetDataTable(sSQL.ToString());
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);
                        foreach (DataRow dr in dt.Rows)
                            CacheData.Reset("LiquidCore.Pages.Page.Id(" + dr["pag_id"].ToString() + ")");
                    }
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void Load(ModelObject Details, NameValueSet DataFields)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::Load]";
                try
                {
					String CacheItem = String.Empty;
					bool fromDr = false;
					if (DataFields["dr"] != null)
					{
						DataRow dr = (DataRow)DataFields["dr"];
						CacheItem = "LiquidCore.Pages.Page.Id(" + dr["pag_id"].ToString() + ")";
						fromDr = true;
					}
					else if (DataFields["Id"] != null)
					{
						CacheItem = "LiquidCore.Pages.Page.Id(" + DataFields["Id"].ToString() + ")";
					}

					System.Data.DataTable dt = null;
					if (HttpContext.Current != null) if (HttpContext.Current != null) dt = (DataTable)HttpContext.Current.Cache[CacheItem];
					if (dt == null)
					{
						if (fromDr)
						{
							dt = CreateDt();
							dt.ImportRow((DataRow)DataFields["dr"]);
						}
						else
						{
							StringBuilder sSQL = new StringBuilder();
							sSQL.Append("SELECT * FROM pag_pages WHERE pag_id = " + DataFields["Id"].ToString() + " AND pag_deleted = 0");
							using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true))
							{
								EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
								dt = oDo.GetDataTable(sSQL.ToString());
								if (oDo.HasError)
									throw new Exception(oDo.GetError);
							}
						}
						CacheData.Insert(CacheItem, dt);
					}

                    if (dt.Rows.Count > 0)
                    {
                        System.Data.DataRow dr = dt.Rows[0];
                        ((MenuModel.ItemModel)Details).Id = Convert.ToInt32(dr["pag_id"]);
                        ((MenuModel.ItemModel)Details).SitId = Convert.ToInt32(dr["sit_id"]);
                        ((MenuModel.ItemModel)Details).Status = Convert.ToInt32(dr["sta_id"]);
                        ((MenuModel.ItemModel)Details).Language = Convert.ToInt32(dr["lng_id"]);
                        ((MenuModel.ItemModel)Details).ModelId = Convert.ToInt32(dr["mdl_id"]);
                        ((MenuModel.ItemModel)Details).ParentId = Convert.ToInt32(dr["pag_parentid"]);
                        ((MenuModel.ItemModel)Details).Order = Convert.ToInt32(dr["pag_order"]);
                        ((MenuModel.ItemModel)Details).Title = Convert.ToString(dr["pag_title"]);
                        ((MenuModel.ItemModel)Details).Alias = Convert.ToString(dr["pag_alias"]);
                        ((MenuModel.ItemModel)Details).Description = Convert.ToString(dr["pag_description"]);
                        ((MenuModel.ItemModel)Details).AuthorizedRoles = GetAuthorizedRoles(Convert.ToInt32(DataFields["Id"]));
                        ((MenuModel.ItemModel)Details).AuthorizedEditRoles = GetAuthorizedEditRoles(Convert.ToInt32(DataFields["Id"]));
                        ((MenuModel.ItemModel)Details).Template = Convert.ToString(dr["pag_template"]);
                        ((MenuModel.ItemModel)Details).CreatedDate = Convert.ToDateTime(dr["pag_createddate"]);
                        ((MenuModel.ItemModel)Details).CreatedBy = Convert.ToString(dr["pag_createdby"]);
                        ((MenuModel.ItemModel)Details).UpdatedDate = Convert.ToDateTime(dr["pag_updateddate"]);
                        ((MenuModel.ItemModel)Details).UpdatedBy = Convert.ToString(dr["pag_updatedby"]);
                        ((MenuModel.ItemModel)Details).Hidden = (dr["pag_hidden"].ToString().Equals("0") ? false : true);
                        ((MenuModel.ItemModel)Details).MobileHidden = (dr["pag_mobilehidden"].ToString().Equals("0") ? false : true);
                        ((MenuModel.ItemModel)Details).Deleted = (dr["pag_deleted"].ToString().Equals("0") ? false : true);
                    }
                    else
                        ((MenuModel.ItemModel)Details).Deleted = true;
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
			private static DataTable CreateDt()
			{
				string FUNCTIONNAME = CLASSNAME + "[Function::CreateDt]";
				try
				{
					DataTable dt = new DataTable();
					dt.Columns.Add("pag_id");
					dt.Columns.Add("sit_id");
					dt.Columns.Add("sta_id");
					dt.Columns.Add("lng_id");
					dt.Columns.Add("mdl_id");
					dt.Columns.Add("pag_parentid");
					dt.Columns.Add("pag_order");
					dt.Columns.Add("pag_title");
					dt.Columns.Add("pag_alias");
					dt.Columns.Add("pag_description");
					dt.Columns.Add("pag_template");
					dt.Columns.Add("pag_createddate");
					dt.Columns.Add("pag_createdby");
					dt.Columns.Add("pag_updateddate");
					dt.Columns.Add("pag_updatedby");
					dt.Columns.Add("pag_hidden");
					dt.Columns.Add("pag_mobilehidden");
					dt.Columns.Add("pag_deleted");
					return dt;
				}
				catch (Exception ex)
				{
					Error.Report(ex, FUNCTIONNAME, "");
					return null;
				}
			}
            public static void LoadAll(ref List<Menu.Item> _pages)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::LoadAll]";
                try
                {
                    DataTable dt = null;
                    String CacheItem = "LiquidCore.Pages";
                    if (HttpContext.Current != null) if (HttpContext.Current != null) dt = (DataTable)HttpContext.Current.Cache[CacheItem];
                    if (dt == null)
                    {
                        StringBuilder sSQL = new StringBuilder();
                        sSQL.Append("SELECT * FROM pag_pages WHERE pag_deleted = 0 ORDER BY sit_id, sta_id, ");
                        sSQL.Append("lng_id, pag_parentid, pag_order");
                        using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true))
                        {
                            EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                            dt = oDo.GetDataTable(sSQL.ToString());
                            if (oDo.HasError)
                                throw new Exception(oDo.GetError);
                        }
                        CacheData.Insert(CacheItem, dt);
                    }
                    foreach (DataRow dr in dt.Rows)
                    {
                        Menu.Item p = new Menu.Item(dr);
                        _pages.Add(p);
                    }
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void LoadAll(ref List<Menu.Item> _pages, NameValueSet DataFields)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::LoadAll]";
                try
                {
                    String PrimaryKey = String.Empty;
                    String CacheItem = String.Empty;
                    StringBuilder sSQL = new StringBuilder();
					if (DataFields["SitId"] != null && DataFields["ParentId"] != null && DataFields["Status"] != null)
					{
						String SitId = DataFields["SitId"].ToString();
						String ParentId = DataFields["ParentId"].ToString();
						String Status = DataFields["Status"].ToString();
						CacheItem = "LiquidCore.Pages.PrimaryKeys(SitId(" + SitId + "), ParentId(" + ParentId + "), Status(" + Status + "))";
						sSQL.Append("SELECT * FROM pag_pages WHERE sit_id = " + SitId + " AND pag_parentid = " + ParentId + " AND sta_id = " + Status + " AND pag_deleted = 0 ORDER BY sit_id, sta_id, lng_id, pag_parentid, pag_order");
					}
					else if (DataFields["SitId"] != null && DataFields["ParentId"] != null)
                    {
                        String SitId = DataFields["SitId"].ToString();
                        String ParentId = DataFields["ParentId"].ToString();
                        CacheItem = "LiquidCore.Pages.PrimaryKeys(SitId(" + SitId + "), ParentId(" + ParentId + "))";
                        sSQL.Append("SELECT * FROM pag_pages WHERE sit_id = " + SitId + " AND pag_parentid = " + ParentId + " AND pag_deleted = 0 ORDER BY sit_id, sta_id, lng_id, pag_parentid, pag_order");
                    }
                    else if (DataFields["Alias"] != null)
                    {
                        PrimaryKey = DataFields["Alias"].ToString();
                        CacheItem = "LiquidCore.Pages.PrimaryKey(Alias(" + PrimaryKey + "))";
                        sSQL.Append("SELECT * FROM pag_pages WHERE pag_alias = '" + PrimaryKey + "' AND pag_deleted = 0 ORDER BY sit_id, sta_id, lng_id, pag_parentid, pag_order");
                    }
                    else
                    {
                        LoadAll(ref _pages);
                        return;
                    }
                    DataTable dt = null;
                    if (HttpContext.Current != null) if (HttpContext.Current != null) dt = (DataTable)HttpContext.Current.Cache[CacheItem];
                    if (dt == null)
                    {
                        using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true))
                        {
                            EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                            dt = oDo.GetDataTable(sSQL.ToString());
                            if (oDo.HasError)
                                throw new Exception(oDo.GetError);
                        }
                        CacheData.Insert(CacheItem, dt);
                    }
                    foreach (DataRow dr in dt.Rows)
                    {
                        Menu.Item p = new Menu.Item(dr);
                        _pages.Add(p);
                    }
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
			public static void Update(MenuModel.ItemModel Details, NameValueSet DataFields)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::Update]";
                try
                {
                    ((MenuModel.ItemModel)Details).UpdatedDate = DateTime.Now;
                    ((MenuModel.ItemModel)Details).UpdatedBy = Authentication.User.Identity.Name;

                    System.Data.DataSet ds = new System.Data.DataSet();
                    StringBuilder sSQL = new StringBuilder();
                    sSQL.Append("UPDATE pag_pages SET ");
                    sSQL.Append("sit_id = " + Details.SitId.ToString() + ", ");
                    sSQL.Append("sta_id = " + Details.Status.ToString() + ", ");
                    sSQL.Append("lng_id = " + Details.Language.ToString() + ", ");
                    sSQL.Append("mdl_id = " + Details.ModelId.ToString() + ", ");
                    sSQL.Append("pag_parentid = " + Details.ParentId.ToString() + ", ");
                    sSQL.Append("pag_order = " + Details.Order.ToString() + ", ");
                    sSQL.Append("pag_title = '" + Details.Title + "', ");
                    sSQL.Append("pag_alias = '" + Details.Alias + "', ");
                    sSQL.Append("pag_description = '" + Details.Description + "', ");
                    sSQL.Append("pag_template = '" + Details.Template + "', ");
                    sSQL.Append("pag_createddate = '" + Details.CreatedDate.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "', ");
                    sSQL.Append("pag_createdby = '" + Details.CreatedBy + "', ");
                    sSQL.Append("pag_updateddate = '" + Details.UpdatedDate.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "', ");
                    sSQL.Append("pag_updatedby = '" + Details.UpdatedBy + "', ");
                    sSQL.Append("pag_hidden = " + (!Details.Hidden ? "0" : "1") + ", ");
                    sSQL.Append("pag_mobilehidden = " + (!Details.MobileHidden ? "0" : "1") + ", ");
                    sSQL.Append("pag_deleted = " + (!Details.Deleted ? "0" : "1") + " ");
                    sSQL.Append("WHERE pag_id = " + Details.Id.ToString());
                    using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, true))
                    {
                        EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                        oDo.ExecuteNonQuery(sSQL.ToString());
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);

                        //EventLog.LogEvent("CALL sort_pag_pages();", FUNCTIONNAME, String.Empty);
                        //oDo.ExecuteNonQuery("CALL sort_pag_pages();");
                        //if (oDo.HasError)
                        //    throw new Exception(oDo.GetError);

                    }
					if (DataFields["PreventSortOnSave"].Equals(false))
					{
						SortAll(Details.Status, Details.Language, Details.ParentId);
					}
                    CacheData.Reset("LiquidCore.Pages.PrimaryKeys(SitId(" + Details.SitId.ToString() + "), ParentId(" + Details.ParentId.ToString() + "))");
                    CacheData.Reset("LiquidCore.Pages.PrimaryKey(Alias(" + Details.Alias + "))");

                    sSQL = new StringBuilder();
                    sSQL.Append("SELECT pag_id FROM pag_pages WHERE sit_id = " + Details.SitId.ToString() + " AND pag_parentid = " + Details.ParentId.ToString() + " AND pag_deleted = 0");
                    using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, false))
                    {
                        DataTable dt = oDo.GetDataTable(sSQL.ToString());
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);
                        foreach (DataRow dr in dt.Rows)
                            CacheData.Reset("LiquidCore.Pages.Page.Id(" + dr["pag_id"].ToString() + ")");
                    }
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void Delete(MenuModel.ItemModel Details, NameValueSet DataFields)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::Delete]";
                try
                {
                    RecursiveDelete(Details.Id, DataFields["Recursive"]);
                    //using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, false))
                    //{
                    //    EventLog.LogEvent("CALL sort_pag_pages();", FUNCTIONNAME, String.Empty);
                    //    oDo.ExecuteNonQuery("CALL sort_pag_pages();");
                    //    if (oDo.HasError)
                    //        throw new Exception(oDo.GetError);

                    //    EventLog.LogEvent("CALL sort_mod_modules();", FUNCTIONNAME, String.Empty);
                    //    oDo.ExecuteNonQuery("CALL sort_mod_modules();");
                    //    if (oDo.HasError)
                    //        throw new Exception(oDo.GetError);

                    //    EventLog.LogEvent("CALL sort_obd_objectdata();", FUNCTIONNAME, String.Empty);
                    //    oDo.ExecuteNonQuery("CALL sort_obd_objectdata();");
                    //    if (oDo.HasError)
                    //        throw new Exception(oDo.GetError);

                    //    EventLog.LogEvent("CALL sort_set_settings();", FUNCTIONNAME, String.Empty);
                    //    oDo.ExecuteNonQuery("CALL sort_set_settings();");
                    //    if (oDo.HasError)
                    //        throw new Exception(oDo.GetError);
                    //}
                    SortAll(Details.Status, Details.Language, Details.ParentId);
                    CacheData.Reset("LiquidCore.Pages.PrimaryKeys(SitId(" + Details.SitId.ToString() + "), ParentId(" + Details.ParentId.ToString() + "))");
                    CacheData.Reset("LiquidCore.Pages.PrimaryKey(Alias(" + Details.Alias + "))");

                    StringBuilder sSQL = new StringBuilder();
                    sSQL.Append("SELECT pag_id FROM pag_pages WHERE sit_id = " + Details.SitId.ToString() + " AND pag_parentid = " + Details.ParentId.ToString() + " AND pag_deleted = 0");
                    using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, false))
                    {
                        DataTable dt = oDo.GetDataTable(sSQL.ToString());
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);
                        foreach (DataRow dr in dt.Rows)
                            CacheData.Reset("LiquidCore.Pages.Page.Id(" + dr["pag_id"].ToString() + ")");
                    }
                    Details = null;
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
			public static void RecursiveDelete(Int32 Id, System.Object recursive)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::RecursiveDelete]";
                try
                {
                    if (Id.Equals(0))
                        return;

                    DataTable dt = new DataTable();
                    using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, true))
                    {
                        String sSQL1 = "SELECT pag_id FROM pag_pages WHERE pag_parentid = " + Id.ToString();
                        String sSQL2 = "UPDATE apr_authorizedpagesroles SET apr_deleted = 1 WHERE pag_id = " + Id.ToString();
                        String sSQL3 = "UPDATE set_settings SET set_deleted = 1 WHERE pag_id = " + Id.ToString();
                        String sSQL4 = "UPDATE pag_pages SET pag_deleted = 1 WHERE pag_id = " + Id.ToString();

                        EventLog.LogEvent(sSQL1, FUNCTIONNAME, String.Empty);
                        System.Diagnostics.Debug.WriteLine(sSQL1);
                        dt = oDo.GetDataTable(sSQL1);
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);

                        EventLog.LogEvent(sSQL2, FUNCTIONNAME, String.Empty);
                        System.Diagnostics.Debug.WriteLine(sSQL2);
                        oDo.ExecuteNonQuery(sSQL2);
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);

                        EventLog.LogEvent(sSQL3, FUNCTIONNAME, String.Empty);
                        System.Diagnostics.Debug.WriteLine(sSQL3);
                        oDo.ExecuteNonQuery(sSQL3);
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);

                        EventLog.LogEvent(sSQL4, FUNCTIONNAME, String.Empty);
                        System.Diagnostics.Debug.WriteLine(sSQL4);
                        oDo.ExecuteNonQuery(sSQL4);
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);
                    }
					if (recursive.Equals(true))
					{
						foreach (DataRow dr in dt.Rows)
						{
							Int32 ParentId = 0;
							Int32.TryParse(dr["pag_id"].ToString(), out ParentId);
							RecursiveDelete(ParentId, true);
						}
					}
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void ResetThis()
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::ResetThis]";
                try
                {
                    //EventLog.LogEvent("LiquidCore.Pages", FUNCTIONNAME, String.Empty);
                    //CacheData.Reset("LiquidCore.Pages");
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }

            //[ObsoleteAttribute("SortAll has been deprecated. All sorting take place in stored procedures instead.")]
            public static void SortAll(Int32 Status, Int32 Language, Int32 ParentId)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::SortAll]";
                try
                {
                    Int32 Order = (Int32)OrderMinMax.Min;
                    StringBuilder sSQL = new StringBuilder();
                    sSQL.Append("SELECT pag_id FROM pag_pages WHERE sta_id = " + Status.ToString() + " AND lng_id = " + Language.ToString() + " AND pag_parentid = " + ParentId.ToString() + " AND pag_deleted = 0 ORDER BY sit_id, sta_id, lng_id, pag_parentid, pag_order");
                    using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, false))
                    {
                        EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                        DataTable dt = oDo.GetDataTable(sSQL.ToString());
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);
                        StringBuilder sSQL2 = new StringBuilder();
                        foreach (DataRow dr in dt.Rows)
                        {
                            sSQL2.AppendLine("UPDATE pag_pages SET pag_order = " + Order.ToString() + " WHERE pag_id = " + dr["pag_id"].ToString() + ";");
                            Order = Order + (Int32)OrderMinMax.Step;
                            if (sSQL2.Length > DataBlockSize)
                            {
                                EventLog.LogEvent(sSQL2.ToString(), FUNCTIONNAME, String.Empty);
                                if (!(oDo.ExecuteNonQuery(sSQL2.ToString()) > 0))
                                    throw new Exception(oDo.GetError);
                                sSQL2 = new StringBuilder();
                            }
                        }
                        if (sSQL2.Length > 0)
                        {
                            EventLog.LogEvent(sSQL2.ToString(), FUNCTIONNAME, String.Empty);
                            if (!(oDo.ExecuteNonQuery(sSQL2.ToString()) > 0))
                                throw new Exception(oDo.GetError);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static Boolean FindChildren(MenuModel.ItemModel Details)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::FindChildren]";
                DataTable dt = null;
                try
                {
                    String CacheItem = "LiquidCore.Pages.Page.Children(" + Details.Id.ToString() + ")";
                    if (HttpContext.Current != null) if (HttpContext.Current != null) dt = (DataTable)HttpContext.Current.Cache[CacheItem];
                    if (dt == null)
                    {
                        StringBuilder sSQL = new StringBuilder();
                        sSQL.Append("SELECT pag_id FROM pag_pages WHERE pag_deleted = 0 AND pag_parentid = " + Details.Id.ToString());
                        using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true))
                        {
                            EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                            dt = oDo.GetDataTable(sSQL.ToString());
                        }
                        CacheData.Insert(CacheItem, dt);
                    }
                    if (dt.Rows.Count > 0)
                        return true;

                    return false;
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                    return false;
                }
            }
            public static void AddAuthorizedRole(MenuModel.ItemModel Details, Int32 RolId)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::AddAuthorizedRole]";
                try
                {
                    StringBuilder sSQL = new StringBuilder();
                    sSQL.Append("INSERT INTO apr_authorizedpagesroles (sta_id, lng_id, pag_id, rol_id, apr_createddate, apr_createdby, ");
                    sSQL.Append("apr_updateddate, apr_updatedby, apr_hidden, apr_deleted) VALUES ( ");
                    sSQL.Append("1, ");
                    sSQL.Append(Details.Language.ToString() + ", ");
                    sSQL.Append(Details.Id.ToString() + ", ");
                    sSQL.Append(RolId.ToString() + ", ");
                    sSQL.Append("'" + DateTime.Now.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "', ");
                    sSQL.Append("'" + Authentication.User.Identity.Name + "', ");
                    sSQL.Append("'" + DateTime.Now.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "', ");
                    sSQL.Append("'" + Authentication.User.Identity.Name + "', ");
                    sSQL.Append("0, ");
                    sSQL.Append("0)");
                    using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, true))
                    {
                        EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                        if (oDo.ExecuteNonQuery(sSQL.ToString()).Equals(-1))
                            throw new Exception(oDo.GetError);
                    }
                    CacheData.Reset("LiquidCore.Pages.Page.Id(" + Details.Id.ToString() + ")");
                    CacheData.Reset("LiquidCore.Pages.PrimaryKeys(SitId(" + Details.SitId.ToString() + "), ParentId(" + Details.ParentId.ToString() + "))");
                    CacheData.Reset("LiquidCore.Pages.PrimaryKey(Alias(" + Details.Alias + "))");
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void DeleteAuthorizedRole(MenuModel.ItemModel Details, Int32 RolId)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::DeleteAuthorizedRole]";
                try
                {
                    StringBuilder sSQL = new StringBuilder();
                    sSQL.Append("UPDATE apr_authorizedpagesroles SET apr_deleted = 1 WHERE pag_id = " + Details.Id.ToString() + " AND sta_id = 1 AND rol_id = " + RolId.ToString());
                    using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, true))
                    {
                        EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                        oDo.ExecuteNonQuery(sSQL.ToString());
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);
                    }
                    CacheData.Reset("LiquidCore.Pages.Page.Id(" + Details.Id.ToString() + ")");
                    CacheData.Reset("LiquidCore.Pages.PrimaryKeys(SitId(" + Details.SitId.ToString() + "), ParentId(" + Details.ParentId.ToString() + "))");
                    CacheData.Reset("LiquidCore.Pages.PrimaryKey(Alias(" + Details.Alias + "))");
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static Int32[] GetAuthorizedRoles(Int32 Id)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::GetAuthorizedRoles]";
                Int32[] Roles;
                DataTable dt = null;
                try
                {
                    String CacheItem = "LiquidCore.Pages.Page.GetAuthorizedRoles(" + Id.ToString() + ")";
                    if (HttpContext.Current != null) if (HttpContext.Current != null) dt = (DataTable)HttpContext.Current.Cache[CacheItem];
                    if (dt == null)
                    {
                        StringBuilder sSQL = new StringBuilder();
                        sSQL.Append("SELECT rol_id FROM apr_authorizedpagesroles WHERE pag_id = " + Id.ToString() + " AND sta_id = 1 AND apr_deleted = 0");
                        using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true))
                        {
                            EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                            dt = oDo.GetDataTable(sSQL.ToString());
                        }
                        CacheData.Insert(CacheItem, dt);
                    }
                    Roles = new Int32[dt.Rows.Count];
                    for (Int32 i = 0; i < dt.Rows.Count; i++)
                        Roles[i] = Convert.ToInt32(dt.Rows[i]["rol_id"]);
                    return Roles;
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                    return new Int32[0];
                }
            }
            public static void AddAuthorizedEditRole(MenuModel.ItemModel Details, Int32 RolId)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::AddAuthorizedEditRole]";
                try
                {
                    StringBuilder sSQL = new StringBuilder();
                    sSQL.Append("INSERT INTO apr_authorizedpagesroles (sta_id, lng_id, pag_id, rol_id, apr_createddate, apr_createdby, ");
                    sSQL.Append("apr_updateddate, apr_updatedby, apr_hidden, apr_deleted) VALUES ( ");
                    sSQL.Append("2, ");
                    sSQL.Append(Details.Language.ToString() + ", ");
                    sSQL.Append(Details.Id.ToString() + ", ");
                    sSQL.Append(RolId.ToString() + ", ");
                    sSQL.Append("'" + DateTime.Now.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "', ");
                    sSQL.Append("'" + Authentication.User.Identity.Name + "', ");
                    sSQL.Append("'" + DateTime.Now.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "', ");
                    sSQL.Append("'" + Authentication.User.Identity.Name + "', ");
                    sSQL.Append("0, ");
                    sSQL.Append("0)");
                    using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, true))
                    {
                        EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                        if (oDo.ExecuteNonQuery(sSQL.ToString()).Equals(-1))
                            throw new Exception(oDo.GetError);
                    }
                    CacheData.Reset("LiquidCore.Pages.Page.Id(" + Details.Id.ToString() + ")");
                    CacheData.Reset("LiquidCore.Pages.PrimaryKeys(SitId(" + Details.SitId.ToString() + "), ParentId(" + Details.ParentId.ToString() + "))");
                    CacheData.Reset("LiquidCore.Pages.PrimaryKey(Alias(" + Details.Alias + "))");
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void DeleteAuthorizedEditRole(MenuModel.ItemModel Details, Int32 RolId)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::DeleteAuthorizedEditRole]";
                try
                {
                    StringBuilder sSQL = new StringBuilder();
                    sSQL.Append("UPDATE apr_authorizedpagesroles SET apr_deleted = 1 WHERE pag_id = " + Details.Id.ToString() + " AND sta_id = 2 AND rol_id = " + RolId.ToString());
                    using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, true))
                    {
                        EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                        oDo.ExecuteNonQuery(sSQL.ToString());
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);
                    }
                    CacheData.Reset("LiquidCore.Pages.Page.Id(" + Details.Id.ToString() + ")");
                    CacheData.Reset("LiquidCore.Pages.PrimaryKeys(SitId(" + Details.SitId.ToString() + "), ParentId(" + Details.ParentId.ToString() + "))");
                    CacheData.Reset("LiquidCore.Pages.PrimaryKey(Alias(" + Details.Alias + "))");
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
			public static Int32[] GetAuthorizedEditRoles(Int32 Id)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::GetAuthorizedEditRoles]";
                Int32[] Roles;
                DataTable dt = null;
                try
                {
                    String CacheItem = "LiquidCore.Pages.Page.GetAuthorizedEditRoles(" + Id.ToString() + ")";
                    if (HttpContext.Current != null) if (HttpContext.Current != null) dt = (DataTable)HttpContext.Current.Cache[CacheItem];
                    if (dt == null)
                    {
                        StringBuilder sSQL = new StringBuilder();
                        sSQL.Append("SELECT rol_id FROM apr_authorizedpagesroles WHERE pag_id = " + Id.ToString() + " AND sta_id = 2 AND apr_deleted = 0");
                        using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true))
                        {
                            EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                            dt = oDo.GetDataTable(sSQL.ToString());
                        }
                        CacheData.Insert(CacheItem, dt);
                    }
                    Roles = new Int32[dt.Rows.Count];
                    for (Int32 i = 0; i < dt.Rows.Count; i++)
                        Roles[i] = Convert.ToInt32(dt.Rows[i]["rol_id"]);
                    return Roles;
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                    return new Int32[0];
                }
            }
            public static Int32[] GetParents(MenuModel.ItemModel Details)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::GetParents]";
                Int32[] Parents = null;
                try
                {
                    String CacheItem = "LiquidCore.Pages.Page.GetParentsForId(" + Details.Id.ToString() + ")";
                    if (HttpContext.Current != null) if (HttpContext.Current != null) Parents = (Int32[])HttpContext.Current.Cache[CacheItem];
                    if (Parents == null)
                    {
                        if (Details.ParentId > 0)
                        {
                            Parents = new Int32[1];
                            Parents[0] = Details.ParentId;
                            Parents = Generic.GrowArray(GetNextParent(Parents, Details.ParentId), 0);
                            CacheData.Insert(CacheItem, Parents);
                        }
                    }
                    return Parents;
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                    return new Int32[0];
                }
            }
            private static Int32[] GetNextParent(Int32[] Parents, Int32 Id)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::GetNextParent]";
                try
                {
                    Menu.Item m = new Menu.Item(Id);
                    if (m.ParentId > 0)
                    {
                        Parents = Generic.GrowArray(Parents, Parents.Length + 1);
                        Parents[Parents.Length - 1] = m.ParentId;
                        Parents = GetNextParent(Parents, m.ParentId);
                    }
                    return Parents;
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                    return Parents;
                }
            }
        }
        public static class ListData
        {
            static string CLASSNAME = "[Namespace::LiquidCore][Class::ListData]";

            static Int32 DataBlockSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Data.DataBlockSize"].ToString());
            static Int32 UseRevision = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Data.UseRevision"].ToString());

            public static void Create(ListModel.ItemModel Details, NameValueSet DataFields)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::Create]";
                try
                {
                    ((ListModel.ItemModel)Details).Order = (Int32)OrderMinMax.Max;
                    ((ListModel.ItemModel)Details).UpdatedDate = DateTime.Now;
                    ((ListModel.ItemModel)Details).UpdatedBy = Authentication.User.Identity.Name;
                    ((ListModel.ItemModel)Details).CreatedDate = DateTime.Now;
                    ((ListModel.ItemModel)Details).CreatedBy = Authentication.User.Identity.Name;

                    System.Data.DataSet ds = new System.Data.DataSet();
                    StringBuilder sSQL1 = new StringBuilder();
                    sSQL1.Append("INSERT INTO obd_objectdata (sit_id, pag_id, mod_id, sta_id, lng_id, obd_parentid, obd_order, obd_type, obd_title, ");
                    sSQL1.Append("obd_alias, obd_description, obd_varchar1, obd_varchar2, obd_varchar3, obd_varchar4, obd_varchar5, ");
                    sSQL1.Append("obd_varchar6, obd_varchar7, obd_varchar8, obd_varchar9, obd_varchar10, obd_varchar11, ");
                    sSQL1.Append("obd_varchar12, obd_varchar13, obd_varchar14, obd_varchar15, obd_varchar16, obd_varchar17, ");
                    sSQL1.Append("obd_varchar18, obd_varchar19, obd_varchar20, obd_varchar21, obd_varchar22, obd_varchar23, ");
                    sSQL1.Append("obd_varchar24, obd_varchar25, obd_varchar26, obd_varchar27, obd_varchar28, obd_varchar29, ");
                    sSQL1.Append("obd_varchar30, obd_varchar31, obd_varchar32, obd_varchar33, obd_varchar34, obd_varchar35, ");
                    sSQL1.Append("obd_varchar36, obd_varchar37, obd_varchar38, obd_varchar39, obd_varchar40, obd_varchar41, ");
                    sSQL1.Append("obd_varchar42, obd_varchar43, obd_varchar44, obd_varchar45, obd_varchar46, obd_varchar47, ");
                    sSQL1.Append("obd_varchar48, obd_varchar49, obd_varchar50, ");
                    sSQL1.Append("obd_createddate, obd_createdby, ");
                    sSQL1.Append("obd_updateddate, obd_updatedby, obd_hidden, obd_deleted) VALUES ( ");
                    sSQL1.Append(Details.SitId.ToString() + ", ");
                    sSQL1.Append(Details.PagId.ToString() + ", ");
                    sSQL1.Append(Details.ModId.ToString() + ", ");
                    sSQL1.Append(Details.Status.ToString() + ", ");
                    sSQL1.Append(Details.Language.ToString() + ", ");
                    sSQL1.Append(Details.ParentId.ToString() + ", ");
                    sSQL1.Append(Details.Order.ToString() + ", ");
                    sSQL1.Append(Details.Type.ToString() + ", ");
                    sSQL1.Append("'" + Details.Title + "', ");
                    sSQL1.Append("'" + Details.Alias + "', ");
                    sSQL1.Append("'" + Details.Description + "', ");
                    sSQL1.Append("'" + Details.Value1 + "', ");
                    sSQL1.Append("'" + Details.Value2 + "', ");
                    sSQL1.Append("'" + Details.Value3 + "', ");
                    sSQL1.Append("'" + Details.Value4 + "', ");
                    sSQL1.Append("'" + Details.Value5 + "', ");
                    sSQL1.Append("'" + Details.Value6 + "', ");
                    sSQL1.Append("'" + Details.Value7 + "', ");
                    sSQL1.Append("'" + Details.Value8 + "', ");
                    sSQL1.Append("'" + Details.Value9 + "', ");
                    sSQL1.Append("'" + Details.Value10 + "', ");
                    sSQL1.Append("'" + Details.Value11 + "', ");
                    sSQL1.Append("'" + Details.Value12 + "', ");
                    sSQL1.Append("'" + Details.Value13 + "', ");
                    sSQL1.Append("'" + Details.Value14 + "', ");
                    sSQL1.Append("'" + Details.Value15 + "', ");
                    sSQL1.Append("'" + Details.Value16 + "', ");
                    sSQL1.Append("'" + Details.Value17 + "', ");
                    sSQL1.Append("'" + Details.Value18 + "', ");
                    sSQL1.Append("'" + Details.Value19 + "', ");
                    sSQL1.Append("'" + Details.Value20 + "', ");
                    sSQL1.Append("'" + Details.Value21 + "', ");
                    sSQL1.Append("'" + Details.Value22 + "', ");
                    sSQL1.Append("'" + Details.Value23 + "', ");
                    sSQL1.Append("'" + Details.Value24 + "', ");
                    sSQL1.Append("'" + Details.Value25 + "', ");
                    sSQL1.Append("'" + Details.Value26 + "', ");
                    sSQL1.Append("'" + Details.Value27 + "', ");
                    sSQL1.Append("'" + Details.Value28 + "', ");
                    sSQL1.Append("'" + Details.Value29 + "', ");
                    sSQL1.Append("'" + Details.Value30 + "', ");
                    sSQL1.Append("'" + Details.Value31 + "', ");
                    sSQL1.Append("'" + Details.Value32 + "', ");
                    sSQL1.Append("'" + Details.Value33 + "', ");
                    sSQL1.Append("'" + Details.Value34 + "', ");
                    sSQL1.Append("'" + Details.Value35 + "', ");
                    sSQL1.Append("'" + Details.Value36 + "', ");
                    sSQL1.Append("'" + Details.Value37 + "', ");
                    sSQL1.Append("'" + Details.Value38 + "', ");
                    sSQL1.Append("'" + Details.Value39 + "', ");
                    sSQL1.Append("'" + Details.Value40 + "', ");
                    sSQL1.Append("'" + Details.Value41 + "', ");
                    sSQL1.Append("'" + Details.Value42 + "', ");
                    sSQL1.Append("'" + Details.Value43 + "', ");
                    sSQL1.Append("'" + Details.Value44 + "', ");
                    sSQL1.Append("'" + Details.Value45 + "', ");
                    sSQL1.Append("'" + Details.Value46 + "', ");
                    sSQL1.Append("'" + Details.Value47 + "', ");
                    sSQL1.Append("'" + Details.Value48 + "', ");
                    sSQL1.Append("'" + Details.Value49 + "', ");
                    sSQL1.Append("'" + Details.Value50 + "', ");
                    sSQL1.Append("'" + Details.CreatedDate.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "', ");
                    sSQL1.Append("'" + Details.CreatedBy + "', ");
                    sSQL1.Append("'" + Details.UpdatedDate.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "', ");
                    sSQL1.Append("'" + Details.UpdatedBy + "', ");
                    sSQL1.Append("" + (Details.Hidden ? "1" : "0") + ", ");
                    sSQL1.Append("" + (Details.Deleted ? "1" : "0") + ")");
                    using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, true))
                    {
                        EventLog.LogEvent(sSQL1.ToString(), FUNCTIONNAME, String.Empty);
                        oDo.ExecuteNonQuery(sSQL1.ToString());
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);

                        EventLog.LogEvent("SELECT @@IDENTITY", FUNCTIONNAME, String.Empty);
                        ((ListModel.ItemModel)Details).Id = Convert.ToInt32(oDo.GetDataTable("SELECT @@IDENTITY").Rows[0][0]);
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);

                        //EventLog.LogEvent("CALL sort_obd_objectdata();", FUNCTIONNAME, String.Empty);
                        //oDo.ExecuteNonQuery("CALL sort_obd_objectdata();");
                        //if (oDo.HasError)
                        //    throw new Exception(oDo.GetError);
                    }

                    if (DataFields["PreventSortOnSave"].Equals(false))
                    {
                        SortAll(Details.SitId, Details.PagId, Details.ModId, Details.Status, Details.Language, Details.ParentId, Details.Alias);
                    }

					if (Details.Alias != "")
					{
						CacheData.Reset("LiquidCore.List.PrimaryKey(AliasAndParameters(" + Details.Alias);
					}

                    CacheData.Reset("LiquidCore.List.PrimaryKeys(SitId(" + Details.SitId.ToString() + "), PagId(" + Details.PagId.ToString() + "), ModId(" + Details.ModId.ToString() + "), MaxRowCount");
                    CacheData.Reset("LiquidCore.List.PrimaryKeys(SitId(" + Details.SitId.ToString() + "), PagId(" + Details.PagId.ToString() + "), MaxRowCount");
                    CacheData.Reset("LiquidCore.List.PrimaryKeys(SitId(" + Details.SitId.ToString() + "), MaxRowCount");
                    CacheData.Reset("LiquidCore.List.PrimaryKeys(SitId(" + Details.SitId.ToString() + "), PagId(" + Details.PagId.ToString() + "), ModId(" + Details.ModId.ToString() + "))");
                    CacheData.Reset("LiquidCore.List.PrimaryKey(Alias(" + Details.Alias + "))");

					if (Details.SitId != 0 || Details.PagId != 0 || Details.ModId != 0 || Details.ParentId != 0)
					{
						StringBuilder sSQL = new StringBuilder();
						sSQL.Append("SELECT obd_id FROM obd_objectdata WHERE sit_id = " + Details.SitId.ToString() + " AND pag_id = " + Details.PagId.ToString() + " AND mod_id = " + Details.ModId.ToString() + " AND obd_parentid = " + Details.ParentId.ToString() + " AND obd_deleted = 0");
						using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, false))
						{
							DataTable dt = oDo.GetDataTable(sSQL.ToString());
							if (oDo.HasError)
								throw new Exception(oDo.GetError);
							foreach (DataRow dr in dt.Rows)
								CacheData.Reset("LiquidCore.List.Item.Id(" + dr["obd_id"].ToString() + ")");
						}
					}
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void Load(ModelObject Details, NameValueSet DataFields)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::Load]";
                try
                {
					String CacheItem = String.Empty;
					bool fromDr = false;
					if (DataFields["dr"] != null)
					{
						DataRow dr = (DataRow)DataFields["dr"];
						CacheItem = "LiquidCore.List.Item.Id(" + dr["obd_id"].ToString() + ")";
						fromDr = true;
					}
					else if (DataFields["Id"] != null)
					{
						CacheItem = "LiquidCore.List.Item.Id(" + DataFields["Id"].ToString() + ")";
					}

					System.Data.DataTable dt = null;
					if (HttpContext.Current != null) if (HttpContext.Current != null) dt = (DataTable)HttpContext.Current.Cache[CacheItem];
					if (dt == null)
					{
						if (fromDr)
						{
							dt = CreateDt();
							dt.ImportRow((DataRow)DataFields["dr"]);
						}
						else
						{
							StringBuilder sSQL = new StringBuilder();
							sSQL.Append("SELECT * FROM obd_objectdata WHERE obd_id = " + DataFields["Id"].ToString() + " AND obd_deleted = 0");
							using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true))
							{
								EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
								dt = oDo.GetDataTable(sSQL.ToString());
								if (oDo.HasError)
									throw new Exception(oDo.GetError);
							}
						}
						CacheData.Insert(CacheItem, dt);
					}

                    if (dt.Rows.Count > 0)
                    {
                        System.Data.DataRow dr = dt.Rows[0];
                        ((ListModel.ItemModel)Details).Id = Convert.ToInt32(dr["obd_id"]);
                        ((ListModel.ItemModel)Details).SitId = Convert.ToInt32(dr["sit_id"]);
                        ((ListModel.ItemModel)Details).PagId = Convert.ToInt32(dr["pag_id"]);
                        ((ListModel.ItemModel)Details).ModId = Convert.ToInt32(dr["mod_id"]);
                        ((ListModel.ItemModel)Details).Status = Convert.ToInt32(dr["sta_id"]);
                        ((ListModel.ItemModel)Details).Language = Convert.ToInt32(dr["lng_id"]);
                        ((ListModel.ItemModel)Details).ParentId = Convert.ToInt32(dr["obd_parentid"]);
                        ((ListModel.ItemModel)Details).Order = Convert.ToInt32(dr["obd_order"]);
                        ((ListModel.ItemModel)Details).Type = Convert.ToInt32(dr["obd_type"]);
                        ((ListModel.ItemModel)Details).Title = Convert.ToString(dr["obd_title"]);
                        ((ListModel.ItemModel)Details).Alias = Convert.ToString(dr["obd_alias"]);
                        ((ListModel.ItemModel)Details).Description = Convert.ToString(dr["obd_description"]);
                        ((ListModel.ItemModel)Details).Value1 = Convert.ToString(dr["obd_varchar1"]);
                        ((ListModel.ItemModel)Details).Value2 = Convert.ToString(dr["obd_varchar2"]);
                        ((ListModel.ItemModel)Details).Value3 = Convert.ToString(dr["obd_varchar3"]);
                        ((ListModel.ItemModel)Details).Value4 = Convert.ToString(dr["obd_varchar4"]);
                        ((ListModel.ItemModel)Details).Value5 = Convert.ToString(dr["obd_varchar5"]);
                        ((ListModel.ItemModel)Details).Value6 = Convert.ToString(dr["obd_varchar6"]);
                        ((ListModel.ItemModel)Details).Value7 = Convert.ToString(dr["obd_varchar7"]);
                        ((ListModel.ItemModel)Details).Value8 = Convert.ToString(dr["obd_varchar8"]);
                        ((ListModel.ItemModel)Details).Value9 = Convert.ToString(dr["obd_varchar9"]);
                        ((ListModel.ItemModel)Details).Value10 = Convert.ToString(dr["obd_varchar10"]);
                        ((ListModel.ItemModel)Details).Value11 = Convert.ToString(dr["obd_varchar11"]);
                        ((ListModel.ItemModel)Details).Value12 = Convert.ToString(dr["obd_varchar12"]);
                        ((ListModel.ItemModel)Details).Value13 = Convert.ToString(dr["obd_varchar13"]);
                        ((ListModel.ItemModel)Details).Value14 = Convert.ToString(dr["obd_varchar14"]);
                        ((ListModel.ItemModel)Details).Value15 = Convert.ToString(dr["obd_varchar15"]);
                        ((ListModel.ItemModel)Details).Value16 = Convert.ToString(dr["obd_varchar16"]);
                        ((ListModel.ItemModel)Details).Value17 = Convert.ToString(dr["obd_varchar17"]);
                        ((ListModel.ItemModel)Details).Value18 = Convert.ToString(dr["obd_varchar18"]);
                        ((ListModel.ItemModel)Details).Value19 = Convert.ToString(dr["obd_varchar19"]);
                        ((ListModel.ItemModel)Details).Value20 = Convert.ToString(dr["obd_varchar20"]);
                        ((ListModel.ItemModel)Details).Value21 = Convert.ToString(dr["obd_varchar21"]);
                        ((ListModel.ItemModel)Details).Value22 = Convert.ToString(dr["obd_varchar22"]);
                        ((ListModel.ItemModel)Details).Value23 = Convert.ToString(dr["obd_varchar23"]);
                        ((ListModel.ItemModel)Details).Value24 = Convert.ToString(dr["obd_varchar24"]);
                        ((ListModel.ItemModel)Details).Value25 = Convert.ToString(dr["obd_varchar25"]);
                        ((ListModel.ItemModel)Details).Value26 = Convert.ToString(dr["obd_varchar26"]);
                        ((ListModel.ItemModel)Details).Value27 = Convert.ToString(dr["obd_varchar27"]);
                        ((ListModel.ItemModel)Details).Value28 = Convert.ToString(dr["obd_varchar28"]);
                        ((ListModel.ItemModel)Details).Value29 = Convert.ToString(dr["obd_varchar29"]);
                        ((ListModel.ItemModel)Details).Value30 = Convert.ToString(dr["obd_varchar30"]);
                        ((ListModel.ItemModel)Details).Value31 = Convert.ToString(dr["obd_varchar31"]);
                        ((ListModel.ItemModel)Details).Value32 = Convert.ToString(dr["obd_varchar32"]);
                        ((ListModel.ItemModel)Details).Value33 = Convert.ToString(dr["obd_varchar33"]);
                        ((ListModel.ItemModel)Details).Value34 = Convert.ToString(dr["obd_varchar34"]);
                        ((ListModel.ItemModel)Details).Value35 = Convert.ToString(dr["obd_varchar35"]);
                        ((ListModel.ItemModel)Details).Value36 = Convert.ToString(dr["obd_varchar36"]);
                        ((ListModel.ItemModel)Details).Value37 = Convert.ToString(dr["obd_varchar37"]);
                        ((ListModel.ItemModel)Details).Value38 = Convert.ToString(dr["obd_varchar38"]);
                        ((ListModel.ItemModel)Details).Value39 = Convert.ToString(dr["obd_varchar39"]);
                        ((ListModel.ItemModel)Details).Value40 = Convert.ToString(dr["obd_varchar40"]);
                        ((ListModel.ItemModel)Details).Value41 = Convert.ToString(dr["obd_varchar41"]);
                        ((ListModel.ItemModel)Details).Value42 = Convert.ToString(dr["obd_varchar42"]);
                        ((ListModel.ItemModel)Details).Value43 = Convert.ToString(dr["obd_varchar43"]);
                        ((ListModel.ItemModel)Details).Value44 = Convert.ToString(dr["obd_varchar44"]);
                        ((ListModel.ItemModel)Details).Value45 = Convert.ToString(dr["obd_varchar45"]);
                        ((ListModel.ItemModel)Details).Value46 = Convert.ToString(dr["obd_varchar46"]);
                        ((ListModel.ItemModel)Details).Value47 = Convert.ToString(dr["obd_varchar47"]);
                        ((ListModel.ItemModel)Details).Value48 = Convert.ToString(dr["obd_varchar48"]);
                        ((ListModel.ItemModel)Details).Value49 = Convert.ToString(dr["obd_varchar49"]);
                        ((ListModel.ItemModel)Details).Value50 = Convert.ToString(dr["obd_varchar50"]);
                        ((ListModel.ItemModel)Details).CreatedDate = Convert.ToDateTime(dr["obd_createddate"]);
                        ((ListModel.ItemModel)Details).CreatedBy = Convert.ToString(dr["obd_createdby"]);
                        ((ListModel.ItemModel)Details).UpdatedDate = Convert.ToDateTime(dr["obd_updateddate"]);
                        ((ListModel.ItemModel)Details).UpdatedBy = Convert.ToString(dr["obd_updatedby"]);
                        ((ListModel.ItemModel)Details).Hidden = (dr["obd_hidden"].ToString().Equals("0") ? false : true);
                        ((ListModel.ItemModel)Details).Deleted = (dr["obd_deleted"].ToString().Equals("0") ? false : true);
                    }
                    else
                        ((ListModel.ItemModel)Details).Deleted = true;
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
			private static DataTable CreateDt()
			{
				string FUNCTIONNAME = CLASSNAME + "[Function::CreateDt]";
				try
				{
					DataTable dt = new DataTable();
					dt.Columns.Add("obd_id");
					dt.Columns.Add("sit_id");
					dt.Columns.Add("pag_id");
					dt.Columns.Add("mod_id");
					dt.Columns.Add("sta_id");
					dt.Columns.Add("lng_id");
					dt.Columns.Add("obd_parentid");
					dt.Columns.Add("obd_order");
					dt.Columns.Add("obd_type");
					dt.Columns.Add("obd_title");
					dt.Columns.Add("obd_alias");
					dt.Columns.Add("obd_description");
					dt.Columns.Add("obd_varchar1");
					dt.Columns.Add("obd_varchar2");
					dt.Columns.Add("obd_varchar3");
					dt.Columns.Add("obd_varchar4");
					dt.Columns.Add("obd_varchar5");
					dt.Columns.Add("obd_varchar6");
					dt.Columns.Add("obd_varchar7");
					dt.Columns.Add("obd_varchar8");
					dt.Columns.Add("obd_varchar9");
					dt.Columns.Add("obd_varchar10");
					dt.Columns.Add("obd_varchar11");
					dt.Columns.Add("obd_varchar12");
					dt.Columns.Add("obd_varchar13");
					dt.Columns.Add("obd_varchar14");
					dt.Columns.Add("obd_varchar15");
					dt.Columns.Add("obd_varchar16");
					dt.Columns.Add("obd_varchar17");
					dt.Columns.Add("obd_varchar18");
					dt.Columns.Add("obd_varchar19");
					dt.Columns.Add("obd_varchar20");
					dt.Columns.Add("obd_varchar21");
					dt.Columns.Add("obd_varchar22");
					dt.Columns.Add("obd_varchar23");
					dt.Columns.Add("obd_varchar24");
					dt.Columns.Add("obd_varchar25");
					dt.Columns.Add("obd_varchar26");
					dt.Columns.Add("obd_varchar27");
					dt.Columns.Add("obd_varchar28");
					dt.Columns.Add("obd_varchar29");
					dt.Columns.Add("obd_varchar30");
					dt.Columns.Add("obd_varchar31");
					dt.Columns.Add("obd_varchar32");
					dt.Columns.Add("obd_varchar33");
					dt.Columns.Add("obd_varchar34");
					dt.Columns.Add("obd_varchar35");
					dt.Columns.Add("obd_varchar36");
					dt.Columns.Add("obd_varchar37");
					dt.Columns.Add("obd_varchar38");
					dt.Columns.Add("obd_varchar39");
					dt.Columns.Add("obd_varchar40");
					dt.Columns.Add("obd_varchar41");
					dt.Columns.Add("obd_varchar42");
					dt.Columns.Add("obd_varchar43");
					dt.Columns.Add("obd_varchar44");
					dt.Columns.Add("obd_varchar45");
					dt.Columns.Add("obd_varchar46");
					dt.Columns.Add("obd_varchar47");
					dt.Columns.Add("obd_varchar48");
					dt.Columns.Add("obd_varchar49");
					dt.Columns.Add("obd_varchar50");
					dt.Columns.Add("obd_createddate");
					dt.Columns.Add("obd_createdby");
					dt.Columns.Add("obd_updateddate");
					dt.Columns.Add("obd_updatedby");
					dt.Columns.Add("obd_hidden");
					dt.Columns.Add("obd_deleted");
					return dt;
				}
				catch (Exception ex)
				{
					Error.Report(ex, FUNCTIONNAME, "");
					return null;
				}
			}
            public static void LoadAll(ref List<List.Item> _objects)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::LoadAll]";
                try
                {
                    DataTable dt = null;
                    String CacheItem = "LiquidCore.List";
                    if (HttpContext.Current != null) if (HttpContext.Current != null) dt = (DataTable)HttpContext.Current.Cache[CacheItem];
                    if (dt == null)
                    {
                        StringBuilder sSQL = new StringBuilder();
                        sSQL.Append("SELECT * FROM obd_objectdata WHERE obd_deleted = 0 ORDER BY sit_id, pag_id, mod_id, sta_id, ");
						sSQL.Append("lng_id, obd_parentid, obd_order, obd_id");
                        using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true))
                        {
                            EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                            dt = oDo.GetDataTable(sSQL.ToString());
                            if (oDo.HasError)
                                throw new Exception(oDo.GetError);
                        }
                        CacheData.Insert(CacheItem, dt);
                    }
                    foreach (DataRow dr in dt.Rows)
                    {
                        List.Item o = new List.Item(dr);
                        _objects.Add(o);
                    }
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void LoadAll(ref List<List.Item> _objects, NameValueSet DataFields)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::LoadAll]";
                try
                {
                    String PrimaryKey = String.Empty;
                    String CacheItem = String.Empty;
                    StringBuilder sSQL = new StringBuilder();
                    if (DataFields["Parameters"] != null && DataFields["Alias"] != null)
                    {
                        Int32 _Counter = 0;
                        StringBuilder sWhere = new StringBuilder();
                        PrimaryKey = DataFields["Alias"].ToString();
                        LiquidCore.Definition.ListDefinition.Param[] Parameters = (LiquidCore.Definition.ListDefinition.Param[])DataFields["Parameters"];
                        foreach (LiquidCore.Definition.ListDefinition.Param p in Parameters)
                        {
                            PrimaryKey += "_" + p.name + LiquidCore.Definition.ListDefinition.GetOperator(p.operand) + p.value;
							if (p.operand == ListDefinition.Operator.Contains)
							{
								sWhere.Append(" " + Convert.ToString(_Counter > 0 ? DataFields["Prefix"].ToString() : "") + " " + p.name + " LIKE '%" + p.value + "%' ");
							}
							else
							{
								sWhere.Append(" " + Convert.ToString(_Counter > 0 ? DataFields["Prefix"].ToString() : "") + " " + p.name + " " + LiquidCore.Definition.ListDefinition.GetOperator(p.operand) + " " + Convert.ToString(p.datatype.Equals(LiquidCore.Definition.ListDefinition.DataType.DateTime) || p.datatype.Equals(LiquidCore.Definition.ListDefinition.DataType.String) ? "'" : "") + p.value + Convert.ToString(p.datatype.Equals(LiquidCore.Definition.ListDefinition.DataType.DateTime) || p.datatype.Equals(LiquidCore.Definition.ListDefinition.DataType.String) ? "'" : "") + " ");
							}
                            _Counter++;
                        }
                        CacheItem = "LiquidCore.List.PrimaryKey(AliasAndParameters(" + PrimaryKey + "))";
						sSQL.Append("SELECT * FROM obd_objectdata WHERE obd_alias = '" + DataFields["Alias"].ToString() + "' AND obd_deleted = 0 AND (" + sWhere.ToString() + ") ORDER BY sit_id, pag_id, mod_id, sta_id, lng_id, obd_parentid, obd_order, obd_id");
                    }
                    else if (DataFields["SitId"] != null && DataFields["PagId"] != null && DataFields["ModId"] != null && DataFields["MaxRowCount"] != null && DataFields["StartDate"] != null && DataFields["EndDate"] != null)
                    {
                        String SitId = DataFields["SitId"].ToString();
                        String PagId = DataFields["PagId"].ToString();
                        String ModId = DataFields["ModId"].ToString();
                        String MaxRowCount = DataFields["MaxRowCount"].ToString();
                        String StartDate = DataFields["StartDate"].ToString();
                        String EndDate = DataFields["EndDate"].ToString();
                        CacheItem = "LiquidCore.List.PrimaryKeys(SitId(" + SitId + "), PagId(" + PagId + "), ModId(" + ModId + "), MaxRowCount(" + MaxRowCount + "), StartDate(" + StartDate + "), EndDate(" + EndDate + "))";
						sSQL.Append("SELECT * FROM obd_objectdata WHERE sit_id = " + SitId + " AND pag_id = " + PagId + " AND mod_id = " + ModId + " AND obd_updateddate <= '" + EndDate + "' AND obd_updateddate >= '" + StartDate + "' AND obd_deleted = 0 ORDER BY sit_id, pag_id, mod_id, sta_id, lng_id, obd_parentid, obd_order, obd_id LIMIT " + MaxRowCount);
                    }
                    else if (DataFields["SitId"] != null && DataFields["PagId"] != null && DataFields["MaxRowCount"] != null && DataFields["StartDate"] != null && DataFields["EndDate"] != null)
                    {
                        String SitId = DataFields["SitId"].ToString();
                        String PagId = DataFields["PagId"].ToString();
                        String MaxRowCount = DataFields["MaxRowCount"].ToString();
                        String StartDate = DataFields["StartDate"].ToString();
                        String EndDate = DataFields["EndDate"].ToString();
                        CacheItem = "LiquidCore.List.PrimaryKeys(SitId(" + SitId + "), PagId(" + PagId + "), MaxRowCount(" + MaxRowCount + "), StartDate(" + StartDate + "), EndDate(" + EndDate + "))";
						sSQL.Append("SELECT * FROM obd_objectdata WHERE sit_id = " + SitId + " AND pag_id = " + PagId + " AND obd_updateddate <= '" + EndDate + "' AND obd_updateddate >= '" + StartDate + "' AND obd_deleted = 0 ORDER BY sit_id, pag_id, mod_id, sta_id, lng_id, obd_parentid, obd_order, obd_id LIMIT " + MaxRowCount);
                    }
                    else if (DataFields["SitId"] != null && DataFields["MaxRowCount"] != null && DataFields["StartDate"] != null && DataFields["EndDate"] != null)
                    {
                        String SitId = DataFields["SitId"].ToString();
                        String MaxRowCount = DataFields["MaxRowCount"].ToString();
                        String StartDate = DataFields["StartDate"].ToString();
                        String EndDate = DataFields["EndDate"].ToString();
                        CacheItem = "LiquidCore.List.PrimaryKeys(SitId(" + SitId + "), MaxRowCount(" + MaxRowCount + "), StartDate(" + StartDate + "), EndDate(" + EndDate + "))";
						sSQL.Append("SELECT * FROM obd_objectdata WHERE sit_id = " + SitId + " AND obd_updateddate <= '" + EndDate + "' AND obd_updateddate >= '" + StartDate + "' AND obd_deleted = 0 ORDER BY sit_id, pag_id, mod_id, sta_id, lng_id, obd_parentid, obd_order, obd_id LIMIT " + MaxRowCount);
                    }
                    else if (DataFields["SitId"] != null && DataFields["PagId"] != null && DataFields["ModId"] != null)
                    {
                        String SitId = DataFields["SitId"].ToString();
                        String PagId = DataFields["PagId"].ToString();
                        String ModId = DataFields["ModId"].ToString();
                        CacheItem = "LiquidCore.List.PrimaryKeys(SitId(" + SitId + "), PagId(" + PagId + "), ModId(" + ModId + "))";
                        sSQL.Append("SELECT * FROM obd_objectdata WHERE sit_id = " + SitId + " AND pag_id = " + PagId + " AND mod_id = " + ModId + " AND obd_deleted = 0 ORDER BY sit_id, pag_id, mod_id, sta_id, lng_id, obd_parentid, obd_order, obd_id");
                    }
                    else if (DataFields["Alias"] != null)
                    {
                        PrimaryKey = DataFields["Alias"].ToString();
                        CacheItem = "LiquidCore.List.PrimaryKey(Alias(" + PrimaryKey + "))";
						sSQL.Append("SELECT * FROM obd_objectdata WHERE obd_alias = '" + PrimaryKey + "' AND obd_deleted = 0 ORDER BY sit_id, pag_id, mod_id, sta_id, lng_id, obd_parentid, obd_order, obd_id");
                    }
                    else
                    {
                        LoadAll(ref _objects);
                        return;
                    }
                    DataTable dt = null;
                    if (HttpContext.Current != null) if (HttpContext.Current != null) dt = (DataTable)HttpContext.Current.Cache[CacheItem];
                    if (dt == null)
                    {
                        using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true))
                        {
                            EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                            dt = oDo.GetDataTable(sSQL.ToString());
                            if (oDo.HasError)
                                throw new Exception(oDo.GetError);
                        }
                        CacheData.Insert(CacheItem, dt);
                    }
                    foreach (DataRow dr in dt.Rows)
                    {
                        List.Item o;
						if (DataFields["PreventSortOnSave"] != null)
						{
							o = new List.Item(dr, (bool)DataFields["PreventSortOnSave"]);
						}
						else
						{
							o = new List.Item(dr);
						}

                        _objects.Add(o);
                    }
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void Update(ListModel.ItemModel Details, NameValueSet DataFields)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::Update]";
                try
                {
                    ((ListModel.ItemModel)Details).UpdatedDate = DateTime.Now;
                    ((ListModel.ItemModel)Details).UpdatedBy = Authentication.User.Identity.Name;

                    System.Data.DataSet ds = new System.Data.DataSet();
                    StringBuilder sSQL = new StringBuilder();
                    sSQL.Append("UPDATE obd_objectdata SET ");
                    sSQL.Append("sit_id = " + Details.SitId.ToString() + ", ");
                    sSQL.Append("pag_id = " + Details.PagId.ToString() + ", ");
                    sSQL.Append("mod_id = " + Details.ModId.ToString() + ", ");
                    sSQL.Append("sta_id = " + Details.Status.ToString() + ", ");
                    sSQL.Append("lng_id = " + Details.Language.ToString() + ", ");
                    sSQL.Append("obd_parentid = " + Details.ParentId.ToString() + ", ");
                    sSQL.Append("obd_order = " + Details.Order.ToString() + ", ");
                    sSQL.Append("obd_type = " + Details.Type.ToString() + ", ");
                    sSQL.Append("obd_title = '" + Details.Title + "', ");
                    sSQL.Append("obd_alias = '" + Details.Alias + "', ");
                    sSQL.Append("obd_description = '" + Details.Description + "', ");
                    sSQL.Append("obd_varchar1 = '" + Details.Value1 + "', ");
                    sSQL.Append("obd_varchar2 = '" + Details.Value2 + "', ");
                    sSQL.Append("obd_varchar3 = '" + Details.Value3 + "', ");
                    sSQL.Append("obd_varchar4 = '" + Details.Value4 + "', ");
                    sSQL.Append("obd_varchar5 = '" + Details.Value5 + "', ");
                    sSQL.Append("obd_varchar6 = '" + Details.Value6 + "', ");
                    sSQL.Append("obd_varchar7 = '" + Details.Value7 + "', ");
                    sSQL.Append("obd_varchar8 = '" + Details.Value8 + "', ");
                    sSQL.Append("obd_varchar9 = '" + Details.Value9 + "', ");
                    sSQL.Append("obd_varchar10 = '" + Details.Value10 + "', ");
                    sSQL.Append("obd_varchar11 = '" + Details.Value11 + "', ");
                    sSQL.Append("obd_varchar12 = '" + Details.Value12 + "', ");
                    sSQL.Append("obd_varchar13 = '" + Details.Value13 + "', ");
                    sSQL.Append("obd_varchar14 = '" + Details.Value14 + "', ");
                    sSQL.Append("obd_varchar15 = '" + Details.Value15 + "', ");
                    sSQL.Append("obd_varchar16 = '" + Details.Value16 + "', ");
                    sSQL.Append("obd_varchar17 = '" + Details.Value17 + "', ");
                    sSQL.Append("obd_varchar18 = '" + Details.Value18 + "', ");
                    sSQL.Append("obd_varchar19 = '" + Details.Value19 + "', ");
                    sSQL.Append("obd_varchar20 = '" + Details.Value20 + "', ");
                    sSQL.Append("obd_varchar21 = '" + Details.Value21 + "', ");
                    sSQL.Append("obd_varchar22 = '" + Details.Value22 + "', ");
                    sSQL.Append("obd_varchar23 = '" + Details.Value23 + "', ");
                    sSQL.Append("obd_varchar24 = '" + Details.Value24 + "', ");
                    sSQL.Append("obd_varchar25 = '" + Details.Value25 + "', ");
                    sSQL.Append("obd_varchar26 = '" + Details.Value26 + "', ");
                    sSQL.Append("obd_varchar27 = '" + Details.Value27 + "', ");
                    sSQL.Append("obd_varchar28 = '" + Details.Value28 + "', ");
                    sSQL.Append("obd_varchar29 = '" + Details.Value29 + "', ");
                    sSQL.Append("obd_varchar30 = '" + Details.Value30 + "', ");
                    sSQL.Append("obd_varchar31 = '" + Details.Value31 + "', ");
                    sSQL.Append("obd_varchar32 = '" + Details.Value32 + "', ");
                    sSQL.Append("obd_varchar33 = '" + Details.Value33 + "', ");
                    sSQL.Append("obd_varchar34 = '" + Details.Value34 + "', ");
                    sSQL.Append("obd_varchar35 = '" + Details.Value35 + "', ");
                    sSQL.Append("obd_varchar36 = '" + Details.Value36 + "', ");
                    sSQL.Append("obd_varchar37 = '" + Details.Value37 + "', ");
                    sSQL.Append("obd_varchar38 = '" + Details.Value38 + "', ");
                    sSQL.Append("obd_varchar39 = '" + Details.Value39 + "', ");
                    sSQL.Append("obd_varchar40 = '" + Details.Value40 + "', ");
                    sSQL.Append("obd_varchar41 = '" + Details.Value41 + "', ");
                    sSQL.Append("obd_varchar42 = '" + Details.Value42 + "', ");
                    sSQL.Append("obd_varchar43 = '" + Details.Value43 + "', ");
                    sSQL.Append("obd_varchar44 = '" + Details.Value44 + "', ");
                    sSQL.Append("obd_varchar45 = '" + Details.Value45 + "', ");
                    sSQL.Append("obd_varchar46 = '" + Details.Value46 + "', ");
                    sSQL.Append("obd_varchar47 = '" + Details.Value47 + "', ");
                    sSQL.Append("obd_varchar48 = '" + Details.Value48 + "', ");
                    sSQL.Append("obd_varchar49 = '" + Details.Value49 + "', ");
                    sSQL.Append("obd_varchar50 = '" + Details.Value50 + "', ");
                    sSQL.Append("obd_createddate = '" + Details.CreatedDate.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "', ");
                    sSQL.Append("obd_createdby = '" + Details.CreatedBy + "', ");
                    sSQL.Append("obd_updateddate = '" + Details.UpdatedDate.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "', ");
                    sSQL.Append("obd_updatedby = '" + Details.UpdatedBy + "', ");
                    sSQL.Append("obd_hidden = " + (!Details.Hidden ? "0" : "1") + ", ");
                    sSQL.Append("obd_deleted = " + (!Details.Deleted ? "0" : "1") + " ");
                    sSQL.Append("WHERE obd_id = " + Details.Id.ToString());
                    using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, true))
                    {
                        EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                        oDo.ExecuteNonQuery(sSQL.ToString());
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);

                        //EventLog.LogEvent("CALL sort_obd_objectdata();", FUNCTIONNAME, String.Empty);
                        //oDo.ExecuteNonQuery("CALL sort_obd_objectdata();");
                        //if (oDo.HasError)
                        //    throw new Exception(oDo.GetError);

                    }

                    if (DataFields["PreventSortOnSave"].Equals(false))
                    {
                        SortAll(Details.SitId, Details.PagId, Details.ModId, Details.Status, Details.Language, Details.ParentId, Details.Alias);
                    }

					if (Details.Alias != "")
					{
						CacheData.Reset("LiquidCore.List.PrimaryKey(AliasAndParameters(" + Details.Alias);
					}

                    CacheData.Reset("LiquidCore.List.PrimaryKeys(SitId(" + Details.SitId.ToString() + "), PagId(" + Details.PagId.ToString() + "), ModId(" + Details.ModId.ToString() + "), MaxRowCount");
                    CacheData.Reset("LiquidCore.List.PrimaryKeys(SitId(" + Details.SitId.ToString() + "), PagId(" + Details.PagId.ToString() + "), MaxRowCount");
                    CacheData.Reset("LiquidCore.List.PrimaryKeys(SitId(" + Details.SitId.ToString() + "), MaxRowCount");
                    CacheData.Reset("LiquidCore.List.PrimaryKeys(SitId(" + Details.SitId.ToString() + "), PagId(" + Details.PagId.ToString() + "), ModId(" + Details.ModId.ToString() + "))");
                    CacheData.Reset("LiquidCore.List.PrimaryKey(Alias(" + Details.Alias + "))");
					CacheData.Reset("LiquidCore.List.Item.Id(" + Details.Id.ToString() + ")");

					if (Details.SitId != 0 || Details.PagId != 0 || Details.ModId != 0 || Details.ParentId != 0)
					{
						sSQL = new StringBuilder();
						sSQL.Append("SELECT obd_id FROM obd_objectdata WHERE sit_id = " + Details.SitId.ToString() + " AND pag_id = " + Details.PagId.ToString() + " AND mod_id = " + Details.ModId.ToString() + " AND obd_parentid = " + Details.ParentId.ToString() + " AND obd_deleted = 0");
						using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, false))
						{
							DataTable dt = oDo.GetDataTable(sSQL.ToString());
							if (oDo.HasError)
								throw new Exception(oDo.GetError);
							foreach (DataRow dr in dt.Rows)
								CacheData.Reset("LiquidCore.List.Item.Id(" + dr["obd_id"].ToString() + ")");
						}
					}
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void Delete(ListModel.ItemModel Details, NameValueSet DataFields)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::Delete]";
                try
                {
                    RecursiveDelete(Details.Id, DataFields["Recursive"]);
                    //using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, false))
                    //{
                    //    EventLog.LogEvent("CALL sort_obd_objectdata();", FUNCTIONNAME, String.Empty);
                    //    oDo.ExecuteNonQuery("CALL sort_obd_objectdata();");
                    //    if (oDo.HasError)
                    //        throw new Exception(oDo.GetError);

                    //    EventLog.LogEvent("CALL sort_set_settings();", FUNCTIONNAME, String.Empty);
                    //    oDo.ExecuteNonQuery("CALL sort_set_settings();");
                    //    if (oDo.HasError)
                    //        throw new Exception(oDo.GetError);
                    //}

                    if (DataFields["PreventSortOnSave"].Equals(false))
                    {
                        SortAll(Details.SitId, Details.PagId, Details.ModId, Details.Status, Details.Language, Details.ParentId, Details.Alias);
                    }

					if (Details.Alias != "")
					{
						CacheData.Reset("LiquidCore.List.PrimaryKey(AliasAndParameters(" + Details.Alias);
					}

                    CacheData.Reset("LiquidCore.List.PrimaryKeys(SitId(" + Details.SitId.ToString() + "), PagId(" + Details.PagId.ToString() + "), ModId(" + Details.ModId.ToString() + "), MaxRowCount");
                    CacheData.Reset("LiquidCore.List.PrimaryKeys(SitId(" + Details.SitId.ToString() + "), PagId(" + Details.PagId.ToString() + "), MaxRowCount");
                    CacheData.Reset("LiquidCore.List.PrimaryKeys(SitId(" + Details.SitId.ToString() + "), MaxRowCount");
                    CacheData.Reset("LiquidCore.List.PrimaryKeys(SitId(" + Details.SitId.ToString() + "), PagId(" + Details.PagId.ToString() + "), ModId(" + Details.ModId.ToString() + "))");
                    CacheData.Reset("LiquidCore.List.PrimaryKey(Alias(" + Details.Alias + "))");

					if (Details.SitId != 0 || Details.PagId != 0 || Details.ModId != 0 || Details.ParentId != 0)
					{
						StringBuilder sSQL = new StringBuilder();
						sSQL.Append("SELECT obd_id FROM obd_objectdata WHERE sit_id = " + Details.SitId.ToString() + " AND pag_id = " + Details.PagId.ToString() + " AND mod_id = " + Details.ModId.ToString() + " AND obd_parentid = " + Details.ParentId.ToString() + " AND obd_deleted = 0");
						using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, false))
						{
							DataTable dt = oDo.GetDataTable(sSQL.ToString());
							if (oDo.HasError)
								throw new Exception(oDo.GetError);
							foreach (DataRow dr in dt.Rows)
								CacheData.Reset("LiquidCore.List.Item.Id(" + dr["obd_id"].ToString() + ")");
						}
					}

                    Details = null;
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void RecursiveDelete(Int32 Id, System.Object recursive)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::RecursiveDelete]";
                try
                {
                    if (Id.Equals(0))
                        return;

                    DataTable dt = new DataTable();
                    using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, true))
                    {
                        String sSQL1 = "SELECT obd_id FROM obd_objectdata WHERE obd_parentid = " + Id.ToString();
                        //String sSQL2 = "UPDATE set_settings SET set_deleted = 1 WHERE set_pointer = " + Id.ToString();
                        String sSQL3 = "UPDATE obd_objectdata SET obd_deleted = 1 WHERE obd_id = " + Id.ToString();

                        EventLog.LogEvent(sSQL1, FUNCTIONNAME, String.Empty);
                        System.Diagnostics.Debug.WriteLine(sSQL1);
                        dt = oDo.GetDataTable(sSQL1);
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);

                        //EventLog.LogEvent(sSQL2, FUNCTIONNAME, String.Empty);
                        //System.Diagnostics.Debug.WriteLine(sSQL2);
                        //oDo.ExecuteNonQuery(sSQL2);
                        //if (oDo.HasError)
                        //    throw new Exception(oDo.GetError);

                        EventLog.LogEvent(sSQL3, FUNCTIONNAME, String.Empty);
                        System.Diagnostics.Debug.WriteLine(sSQL3);
                        oDo.ExecuteNonQuery(sSQL3);
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);
                    }
					if (recursive.Equals(true))
					{
						foreach (DataRow dr in dt.Rows)
						{
							Int32 ParentId = 0;
							Int32.TryParse(dr["obd_id"].ToString(), out ParentId);
							RecursiveDelete(ParentId, true);
						}
					}
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void ResetThis()
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::ResetThis]";
                try
                {
                //    CacheData.Reset("LiquidCore.List.Item.Id(" + Details.Id.ToString() + ")");
				//    CacheData.Reset("LiquidCore.List.PrimaryKey(AliasAndParameters(" + Details.Alias);
                //    CacheData.Reset("LiquidCore.List.PrimaryKeys(SitId(" + Details.SitId.ToString() + "), PagId(" + Details.PagId.ToString() + "), ModId(" + Details.ModId.ToString() + "), MaxRowCount");
                //    CacheData.Reset("LiquidCore.List.PrimaryKeys(SitId(" + Details.SitId.ToString() + "), PagId(" + Details.PagId.ToString() + "), MaxRowCount");
                //    CacheData.Reset("LiquidCore.List.PrimaryKeys(SitId(" + Details.SitId.ToString() + "), MaxRowCount");
                //    CacheData.Reset("LiquidCore.List.PrimaryKeys(SitId(" + Details.SitId.ToString() + "), PagId(" + Details.PagId.ToString() + "), ModId(" + Details.ModId.ToString() + "))");
                //    CacheData.Reset("LiquidCore.List.PrimaryKey(Alias(" + Details.Alias + "))");
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            //[ObsoleteAttribute("SortAll has been deprecated. All sorting take place in stored procedures instead.")]
            public static void SortAll(Int32 SitId, Int32 PagId, Int32 ModId, Int32 Status, Int32 Language, Int32 ParentId, String Alias)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::SortAll]";
                try
                {
                    Int32 Order = (Int32)OrderMinMax.Min;
                    StringBuilder sSQL = new StringBuilder();
                    sSQL.Append("SELECT obd_id FROM obd_objectdata WHERE sit_id = " + SitId.ToString() + " AND pag_id = " + PagId.ToString() + " AND mod_id = " + ModId.ToString() + " AND sta_id = " + Status.ToString() + " AND obd_parentid = " + ParentId.ToString() + " AND obd_alias = '" + Alias.ToString() + "' AND obd_deleted = 0 ORDER BY sit_id, pag_id, mod_id, sta_id, lng_id, obd_parentid, obd_alias, obd_order");
                    using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, false))
                    {
                        EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                        DataTable dt = oDo.GetDataTable(sSQL.ToString());
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);
                        StringBuilder sSQL2 = new StringBuilder();
                        foreach (DataRow dr in dt.Rows)
                        {
                            sSQL2.AppendLine("UPDATE obd_objectdata SET obd_order = " + Order.ToString() + " WHERE obd_id = " + dr["obd_id"].ToString() + ";");
                            Order = Order + (Int32)OrderMinMax.Step;
                            if (sSQL2.Length > DataBlockSize)
                            {
                                EventLog.LogEvent(sSQL2.ToString(), FUNCTIONNAME, String.Empty);
                                if (!(oDo.ExecuteNonQuery(sSQL2.ToString()) > 0))
                                    throw new Exception(oDo.GetError);
                                sSQL2 = new StringBuilder();
                            }
                        }
                        if (sSQL2.Length > 0)
                        {
                            EventLog.LogEvent(sSQL2.ToString(), FUNCTIONNAME, String.Empty);
                            if (!(oDo.ExecuteNonQuery(sSQL2.ToString()) > 0))
                                throw new Exception(oDo.GetError);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static Boolean FindChildren(ListModel.ItemModel Details)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::FindChildren]";
                DataTable dt = null;
                try
                {
                    String CacheItem = "LiquidCore.List.Item.Children(" + Details.Id.ToString() + ")";
                    if (HttpContext.Current != null) if (HttpContext.Current != null) dt = (DataTable)HttpContext.Current.Cache[CacheItem];
                    if (dt == null)
                    {
                        StringBuilder sSQL = new StringBuilder();
                        sSQL.Append("SELECT obd_id FROM obd_objectdata WHERE obd_deleted = 0 AND obd_parentid = " + Details.Id.ToString());
                        using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true))
                        {
                            EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                            dt = oDo.GetDataTable(sSQL.ToString());
                        }
                        CacheData.Insert(CacheItem, dt);
                    }
                    if (dt.Rows.Count > 0)
                        return true;

                    return false;
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                    return false;
                }
            }
        }
        public static class UserTypeData
        {
            static string CLASSNAME = "[Namespace::LiquidCore][Class::UserTypeData]";

            static Int32 DataBlockSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Data.DataBlockSize"].ToString());
            static Int32 UseRevision = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Data.UseRevision"].ToString());

            public static void Create(UserTypesModel.UserTypeModel Details)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::Create]";
                try
                {
                    ((UserTypesModel.UserTypeModel)Details).Order = (Int32)OrderMinMax.Max;
                    ((UserTypesModel.UserTypeModel)Details).UpdatedDate = DateTime.Now;
                    ((UserTypesModel.UserTypeModel)Details).UpdatedBy = Authentication.User.Identity.Name;
                    ((UserTypesModel.UserTypeModel)Details).CreatedDate = DateTime.Now;
                    ((UserTypesModel.UserTypeModel)Details).CreatedBy = Authentication.User.Identity.Name;

                    System.Data.DataSet ds = new System.Data.DataSet();
                    StringBuilder sSQL1 = new StringBuilder();
                    sSQL1.Append("INSERT INTO ust_usertypes (sta_id, lng_id, ust_parentid, ust_order, ust_title, ");
                    sSQL1.Append("ust_alias, ust_description, ust_createddate, ust_createdby, ");
                    sSQL1.Append("ust_updateddate, ust_updatedby, ust_hidden, ust_deleted) VALUES ( ");
                    sSQL1.Append(Details.Status.ToString() + ", ");
                    sSQL1.Append(Details.Language.ToString() + ", ");
                    sSQL1.Append(Details.ParentId.ToString() + ", ");
                    sSQL1.Append(Details.Order.ToString() + ", ");
                    sSQL1.Append("'" + Details.Title + "', ");
                    sSQL1.Append("'" + Details.Alias + "', ");
                    sSQL1.Append("'" + Details.Description + "', ");
                    sSQL1.Append("'" + Details.CreatedDate.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "', ");
                    sSQL1.Append("'" + Details.CreatedBy + "', ");
                    sSQL1.Append("'" + Details.UpdatedDate.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "', ");
                    sSQL1.Append("'" + Details.UpdatedBy + "', ");
                    sSQL1.Append("" + (Details.Hidden ? "1" : "0") + ", ");
                    sSQL1.Append("" + (Details.Deleted ? "1" : "0") + ")");
                    using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, true))
                    {
                        EventLog.LogEvent(sSQL1.ToString(), FUNCTIONNAME, String.Empty);
                        oDo.ExecuteNonQuery(sSQL1.ToString());
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);

                        EventLog.LogEvent("SELECT @@IDENTITY", FUNCTIONNAME, String.Empty);
                        ((UserTypesModel.UserTypeModel)Details).Id = Convert.ToInt32(oDo.GetDataTable("SELECT @@IDENTITY").Rows[0][0]);
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);

                        //EventLog.LogEvent("CALL sort_ust_usertypes();", FUNCTIONNAME, String.Empty);
                        //oDo.ExecuteNonQuery("CALL sort_ust_usertypes();");
                        //if (oDo.HasError)
                        //    throw new Exception(oDo.GetError);

                    }
                    SortAll();
                    ResetThis();

                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void Load(ModelObject Details, NameValueSet DataFields)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::Load]";
                try
                {
                    System.Data.DataTable dt = null;
                    String CacheItem = "LiquidCore.UserTypes.UserType.Id(" + DataFields["Id"].ToString() + ")";
                    if (HttpContext.Current != null) if (HttpContext.Current != null) dt = (DataTable)HttpContext.Current.Cache[CacheItem];
                    if (dt == null)
                    {
                        StringBuilder sSQL = new StringBuilder();
                        sSQL.Append("SELECT * FROM ust_usertypes WHERE ust_id = " + DataFields["Id"].ToString() + " AND ust_deleted = 0");
                        using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true))
                        {
                            EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                            dt = oDo.GetDataTable(sSQL.ToString());
                            if (oDo.HasError)
                                throw new Exception(oDo.GetError);
                        }
                        CacheData.Insert(CacheItem, dt);
                    }
                    if (dt.Rows.Count > 0)
                    {
                        System.Data.DataRow dr = dt.Rows[0];
                        ((UserTypesModel.UserTypeModel)Details).Id = Convert.ToInt32(dr["ust_id"]);
                        ((UserTypesModel.UserTypeModel)Details).Status = Convert.ToInt32(dr["sta_id"]);
                        ((UserTypesModel.UserTypeModel)Details).Language = Convert.ToInt32(dr["lng_id"]);
                        ((UserTypesModel.UserTypeModel)Details).ParentId = Convert.ToInt32(dr["ust_parentid"]);
                        ((UserTypesModel.UserTypeModel)Details).Order = Convert.ToInt32(dr["ust_order"]);
                        ((UserTypesModel.UserTypeModel)Details).Title = Convert.ToString(dr["ust_title"]);
                        ((UserTypesModel.UserTypeModel)Details).Alias = Convert.ToString(dr["ust_alias"]);
                        ((UserTypesModel.UserTypeModel)Details).Description = Convert.ToString(dr["ust_description"]);
                        ((UserTypesModel.UserTypeModel)Details).CreatedDate = Convert.ToDateTime(dr["ust_createddate"]);
                        ((UserTypesModel.UserTypeModel)Details).CreatedBy = Convert.ToString(dr["ust_createdby"]);
                        ((UserTypesModel.UserTypeModel)Details).UpdatedDate = Convert.ToDateTime(dr["ust_updateddate"]);
                        ((UserTypesModel.UserTypeModel)Details).UpdatedBy = Convert.ToString(dr["ust_updatedby"]);
                        ((UserTypesModel.UserTypeModel)Details).Hidden = (dr["ust_hidden"].ToString().Equals("0") ? false : true);
                        ((UserTypesModel.UserTypeModel)Details).Deleted = (dr["ust_deleted"].ToString().Equals("0") ? false : true);
                    }
                    else
                        ((UserTypesModel.UserTypeModel)Details).Deleted = true;
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void LoadAll(ref List<UserTypes.UserType> _objects)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::LoadAll]";
                try
                {
                    DataTable dt = null;
                    String CacheItem = "LiquidCore.UserTypes";
                    if (HttpContext.Current != null) if (HttpContext.Current != null) dt = (DataTable)HttpContext.Current.Cache[CacheItem];
                    if (dt == null)
                    {
                        StringBuilder sSQL = new StringBuilder();
                        sSQL.Append("SELECT ust_id FROM ust_usertypes WHERE ust_deleted = 0 ORDER BY sta_id, ");
                        sSQL.Append("lng_id, ust_parentid, ust_order");
                        using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true))
                        {
                            EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                            dt = oDo.GetDataTable(sSQL.ToString());
                            if (oDo.HasError)
                                throw new Exception(oDo.GetError);
                        }
                        CacheData.Insert(CacheItem, dt);
                    }
                    foreach (DataRow dr in dt.Rows)
                    {
                        UserTypes.UserType o = new UserTypes.UserType(Convert.ToInt32(dr["ust_id"].ToString()));
                        _objects.Add(o);
                    }
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void LoadAll(ref List<UserTypes.UserType> _objects, NameValueSet DataFields)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::LoadAll]";
                try
                {
                    String PrimaryKey = String.Empty;
                    String CacheItem = String.Empty;
                    StringBuilder sSQL = new StringBuilder();
                    if (DataFields["Alias"] != null)
                    {
                        PrimaryKey = DataFields["Alias"].ToString();
                        CacheItem = "LiquidCore.UserTypes.PrimaryKey(Alias(" + PrimaryKey + "))";
                        sSQL.Append("SELECT ust_id FROM ust_usertypes WHERE ust_alias = '" + PrimaryKey + "' AND ust_deleted = 0 ORDER BY sta_id, lng_id, ust_parentid, ust_order");
                    }
                    else
                    {
                        LoadAll(ref _objects);
                        return;
                    }
                    DataTable dt = null;
                    if (HttpContext.Current != null) if (HttpContext.Current != null) dt = (DataTable)HttpContext.Current.Cache[CacheItem];
                    if (dt == null)
                    {
                        using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true))
                        {
                            EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                            dt = oDo.GetDataTable(sSQL.ToString());
                            if (oDo.HasError)
                                throw new Exception(oDo.GetError);
                        }
                        CacheData.Insert(CacheItem, dt);
                    }
                    foreach (DataRow dr in dt.Rows)
                    {
                        UserTypes.UserType o = new UserTypes.UserType(Convert.ToInt32(dr["ust_id"].ToString()));
                        _objects.Add(o);
                    }
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void Update(UserTypesModel.UserTypeModel Details)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::Update]";
                try
                {
                    ((UserTypesModel.UserTypeModel)Details).UpdatedDate = DateTime.Now;
                    ((UserTypesModel.UserTypeModel)Details).UpdatedBy = Authentication.User.Identity.Name;

                    System.Data.DataSet ds = new System.Data.DataSet();
                    StringBuilder sSQL = new StringBuilder();
                    sSQL.Append("UPDATE ust_usertypes SET ");
                    sSQL.Append("sta_id = " + Details.Status.ToString() + ", ");
                    sSQL.Append("lng_id = " + Details.Language.ToString() + ", ");
                    sSQL.Append("ust_parentid = " + Details.ParentId.ToString() + ", ");
                    sSQL.Append("ust_order = " + Details.Order.ToString() + ", ");
                    sSQL.Append("ust_title = '" + Details.Title + "', ");
                    sSQL.Append("ust_alias = '" + Details.Alias + "', ");
                    sSQL.Append("ust_description = '" + Details.Description + "', ");
                    sSQL.Append("ust_createddate = '" + Details.CreatedDate.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "', ");
                    sSQL.Append("ust_createdby = '" + Details.CreatedBy + "', ");
                    sSQL.Append("ust_updateddate = '" + Details.UpdatedDate.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "', ");
                    sSQL.Append("ust_updatedby = '" + Details.UpdatedBy + "', ");
                    sSQL.Append("ust_hidden = " + (!Details.Hidden ? "0" : "1") + ", ");
                    sSQL.Append("ust_deleted = " + (!Details.Deleted ? "0" : "1") + " ");
                    sSQL.Append("WHERE ust_id = " + Details.Id.ToString());
                    using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, true))
                    {
                        EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                        oDo.ExecuteNonQuery(sSQL.ToString());
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);

                        //EventLog.LogEvent("CALL sort_ust_usertypes();", FUNCTIONNAME, String.Empty);
                        //oDo.ExecuteNonQuery("CALL sort_ust_usertypes();");
                        //if (oDo.HasError)
                        //    throw new Exception(oDo.GetError);

                    }
                    SortAll();
                    ResetThis();
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void Delete(UserTypesModel.UserTypeModel Details)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::Delete]";
                try
                {
                    RecursiveDelete(Details.Id);
                    //using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, false))
                    //{
                    //    EventLog.LogEvent("CALL sort_ust_usertypes();", FUNCTIONNAME, String.Empty);
                    //    oDo.ExecuteNonQuery("CALL sort_ust_usertypes();");
                    //    if (oDo.HasError)
                    //        throw new Exception(oDo.GetError);
                    //}
                    SortAll();
                    ResetThis();
                    Details = null;
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void RecursiveDelete(Int32 Id)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::RecursiveDelete]";
                try
                {
                    if (Id.Equals(0))
                        return;

                    using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, true))
                    {
                        String sSQL1 = "UPDATE ust_usertypes SET ust_deleted = 1 WHERE ust_id = " + Id.ToString();

                        EventLog.LogEvent(sSQL1, FUNCTIONNAME, String.Empty);
                        System.Diagnostics.Debug.WriteLine(sSQL1);
                        oDo.ExecuteNonQuery(sSQL1);
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);
                    }
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void ResetThis()
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::ResetThis]";
                try
                {
                    CacheData.Reset("LiquidCore.UserTypes");
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            //[ObsoleteAttribute("SortAll has been deprecated. All sorting take place in stored procedures instead.")]
            public static void SortAll()
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::SortAll]";
                try
                {
                    Int32 Order = (Int32)OrderMinMax.Min;
                    StringBuilder sSQL = new StringBuilder();
                    sSQL.Append("SELECT ust_id FROM ust_usertypes WHERE ust_deleted = 0 ORDER BY sta_id, lng_id, ust_parentid, ust_order");
                    using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, false))
                    {
                        EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                        DataTable dt = oDo.GetDataTable(sSQL.ToString());
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);
                        StringBuilder sSQL2 = new StringBuilder();
                        foreach (DataRow dr in dt.Rows)
                        {
                            sSQL2.AppendLine("UPDATE ust_usertypes SET ust_order = " + Order.ToString() + " WHERE ust_id = " + dr["ust_id"].ToString() + ";");
                            Order = Order + (Int32)OrderMinMax.Step;
                            if (sSQL2.Length > DataBlockSize)
                            {
                                EventLog.LogEvent(sSQL2.ToString(), FUNCTIONNAME, String.Empty);
                                if (!(oDo.ExecuteNonQuery(sSQL2.ToString()) > 0))
                                    throw new Exception(oDo.GetError);
                                sSQL2 = new StringBuilder();
                            }
                        }
                        if (sSQL2.Length > 0)
                        {
                            EventLog.LogEvent(sSQL2.ToString(), FUNCTIONNAME, String.Empty);
                            if (!(oDo.ExecuteNonQuery(sSQL2.ToString()) > 0))
                                throw new Exception(oDo.GetError);
                        }
                        ResetThis();
                    }
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static Boolean FindChildren(UserTypesModel.UserTypeModel Details)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::FindChildren]";
                DataTable dt = null;
                try
                {
                    String CacheItem = "LiquidCore.UserTypes.UserType.Children(" + Details.Id.ToString() + ")";
                    if (HttpContext.Current != null) if (HttpContext.Current != null) dt = (DataTable)HttpContext.Current.Cache[CacheItem];
                    if (dt == null)
                    {
                        StringBuilder sSQL = new StringBuilder();
                        sSQL.Append("SELECT ust_id FROM ust_usertypes WHERE ust_deleted = 0 AND ust_parentid = " + Details.Id.ToString());
                        using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true))
                        {
                            EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                            dt = oDo.GetDataTable(sSQL.ToString());
                        }
                        CacheData.Insert(CacheItem, dt);
                    }
                    if (dt.Rows.Count > 0)
                        return true;

                    return false;
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                    return false;
                }
            }
        }
        public static class RoleData
        {
            static string CLASSNAME = "[Namespace::LiquidCore][Class::RoleData]";

            static Int32 DataBlockSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Data.DataBlockSize"].ToString());
            static Int32 UseRevision = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Data.UseRevision"].ToString());

            public static void Create(RolesModel.RoleModel Details)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::Create]";
                try
                {
                    ((RolesModel.RoleModel)Details).Order = (Int32)OrderMinMax.Max;
                    ((RolesModel.RoleModel)Details).UpdatedDate = DateTime.Now;
                    ((RolesModel.RoleModel)Details).UpdatedBy = Authentication.User.Identity.Name;
                    ((RolesModel.RoleModel)Details).CreatedDate = DateTime.Now;
                    ((RolesModel.RoleModel)Details).CreatedBy = Authentication.User.Identity.Name;

                    System.Data.DataSet ds = new System.Data.DataSet();
                    StringBuilder sSQL1 = new StringBuilder();
                    sSQL1.Append("INSERT INTO rol_roles (sta_id, lng_id, rol_parentid, rol_order, rol_title, ");
                    sSQL1.Append("rol_alias, rol_description, rol_createddate, rol_createdby, ");
                    sSQL1.Append("rol_updateddate, rol_updatedby, rol_hidden, rol_deleted) VALUES ( ");
                    sSQL1.Append(Details.Status.ToString() + ", ");
                    sSQL1.Append(Details.Language.ToString() + ", ");
                    sSQL1.Append(Details.ParentId.ToString() + ", ");
                    sSQL1.Append(Details.Order.ToString() + ", ");
                    sSQL1.Append("'" + Details.Title + "', ");
                    sSQL1.Append("'" + Details.Alias + "', ");
                    sSQL1.Append("'" + Details.Description + "', ");
                    sSQL1.Append("'" + Details.CreatedDate.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "', ");
                    sSQL1.Append("'" + Details.CreatedBy + "', ");
                    sSQL1.Append("'" + Details.UpdatedDate.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "', ");
                    sSQL1.Append("'" + Details.UpdatedBy + "', ");
                    sSQL1.Append("" + (Details.Hidden ? "1" : "0") + ", ");
                    sSQL1.Append("" + (Details.Deleted ? "1" : "0") + ")");
                    using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, true))
                    {
                        EventLog.LogEvent(sSQL1.ToString(), FUNCTIONNAME, String.Empty);
                        oDo.ExecuteNonQuery(sSQL1.ToString());
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);

                        EventLog.LogEvent("SELECT @@IDENTITY", FUNCTIONNAME, String.Empty);
                        ((RolesModel.RoleModel)Details).Id = Convert.ToInt32(oDo.GetDataTable("SELECT @@IDENTITY").Rows[0][0]);
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);

                        //EventLog.LogEvent("CALL sort_rol_roles();", FUNCTIONNAME, String.Empty);
                        //oDo.ExecuteNonQuery("CALL sort_rol_roles();");
                        //if (oDo.HasError)
                        //    throw new Exception(oDo.GetError);

                    }
                    SortAll();
                    ResetThis();
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void Load(ModelObject Details, NameValueSet DataFields)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::Load]";
                try
                {
                    System.Data.DataTable dt = null;
                    String CacheItem = "LiquidCore.Roles.Role.Id(" + DataFields["Id"].ToString() + ")";
                    if (HttpContext.Current != null) if (HttpContext.Current != null) dt = (DataTable)HttpContext.Current.Cache[CacheItem];
                    if (dt == null)
                    {
                        StringBuilder sSQL = new StringBuilder();
                        sSQL.Append("SELECT * FROM rol_roles WHERE rol_id = " + DataFields["Id"].ToString() + " AND rol_deleted = 0");
                        using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true))
                        {
                            EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                            dt = oDo.GetDataTable(sSQL.ToString());
                            if (oDo.HasError)
                                throw new Exception(oDo.GetError);
                        }
                        CacheData.Insert(CacheItem, dt);
                    }
                    if (dt.Rows.Count > 0)
                    {
                        System.Data.DataRow dr = dt.Rows[0];
                        ((RolesModel.RoleModel)Details).Id = Convert.ToInt32(dr["rol_id"]);
                        ((RolesModel.RoleModel)Details).Status = Convert.ToInt32(dr["sta_id"]);
                        ((RolesModel.RoleModel)Details).Language = Convert.ToInt32(dr["lng_id"]);
                        ((RolesModel.RoleModel)Details).ParentId = Convert.ToInt32(dr["rol_parentid"]);
                        ((RolesModel.RoleModel)Details).Order = Convert.ToInt32(dr["rol_order"]);
                        ((RolesModel.RoleModel)Details).Title = Convert.ToString(dr["rol_title"]);
                        ((RolesModel.RoleModel)Details).Alias = Convert.ToString(dr["rol_alias"]);
                        ((RolesModel.RoleModel)Details).Description = Convert.ToString(dr["rol_description"]);
                        ((RolesModel.RoleModel)Details).CreatedDate = Convert.ToDateTime(dr["rol_createddate"]);
                        ((RolesModel.RoleModel)Details).CreatedBy = Convert.ToString(dr["rol_createdby"]);
                        ((RolesModel.RoleModel)Details).UpdatedDate = Convert.ToDateTime(dr["rol_updateddate"]);
                        ((RolesModel.RoleModel)Details).UpdatedBy = Convert.ToString(dr["rol_updatedby"]);
                        ((RolesModel.RoleModel)Details).Hidden = (dr["rol_hidden"].ToString().Equals("0") ? false : true);
                        ((RolesModel.RoleModel)Details).Deleted = (dr["rol_deleted"].ToString().Equals("0") ? false : true);
                    }
                    else
                        ((RolesModel.RoleModel)Details).Deleted = true;
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void LoadAll(ref List<Roles.Role> _objects)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::LoadAll]";
                try
                {
                    DataTable dt = null;
                    String CacheItem = "LiquidCore.Roles";
                    if (HttpContext.Current != null) if (HttpContext.Current != null) dt = (DataTable)HttpContext.Current.Cache[CacheItem];
                    if (dt == null)
                    {
                        StringBuilder sSQL = new StringBuilder();
                        sSQL.Append("SELECT rol_id FROM rol_roles WHERE rol_deleted = 0 ORDER BY rol_id, ");
                        sSQL.Append("lng_id, rol_parentid, rol_order");
                        using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true))
                        {
                            EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                            dt = oDo.GetDataTable(sSQL.ToString());
                            if (oDo.HasError)
                                throw new Exception(oDo.GetError);
                        }
                        CacheData.Insert(CacheItem, dt);
                    }
                    foreach (DataRow dr in dt.Rows)
                    {
                        Roles.Role o = new Roles.Role(Convert.ToInt32(dr["rol_id"].ToString()));
                        _objects.Add(o);
                    }
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void LoadAll(ref List<Roles.Role> _objects, NameValueSet DataFields)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::LoadAll]";
                try
                {
                    String PrimaryKey = String.Empty;
                    String CacheItem = String.Empty;
                    StringBuilder sSQL = new StringBuilder();
                    if (DataFields["Alias"] != null)
                    {
                        PrimaryKey = DataFields["Alias"].ToString();
                        CacheItem = "LiquidCore.Roles.PrimaryKey(Alias(" + PrimaryKey + "))";
                        sSQL.Append("SELECT rol_id FROM rol_roles WHERE rol_alias = '" + PrimaryKey + "' AND rol_deleted = 0 ORDER BY sta_id, lng_id, rol_parentid, rol_order");
                    }
                    else
                    {
                        LoadAll(ref _objects);
                        return;
                    }
                    DataTable dt = null;
                    if (HttpContext.Current != null) if (HttpContext.Current != null) dt = (DataTable)HttpContext.Current.Cache[CacheItem];
                    if (dt == null)
                    {
                        using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true))
                        {
                            EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                            dt = oDo.GetDataTable(sSQL.ToString());
                            if (oDo.HasError)
                                throw new Exception(oDo.GetError);
                        }
                        CacheData.Insert(CacheItem, dt);
                    }
                    foreach (DataRow dr in dt.Rows)
                    {
                        Roles.Role o = new Roles.Role(Convert.ToInt32(dr["rol_id"].ToString()));
                        _objects.Add(o);
                    }
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void Update(RolesModel.RoleModel Details)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::Update]";
                try
                {
                    ((RolesModel.RoleModel)Details).UpdatedDate = DateTime.Now;
                    ((RolesModel.RoleModel)Details).UpdatedBy = Authentication.User.Identity.Name;

                    System.Data.DataSet ds = new System.Data.DataSet();
                    StringBuilder sSQL = new StringBuilder();
                    sSQL.Append("UPDATE rol_roles SET ");
                    sSQL.Append("sta_id = " + Details.Status.ToString() + ", ");
                    sSQL.Append("lng_id = " + Details.Language.ToString() + ", ");
                    sSQL.Append("rol_parentid = " + Details.ParentId.ToString() + ", ");
                    sSQL.Append("rol_order = " + Details.Order.ToString() + ", ");
                    sSQL.Append("rol_title = '" + Details.Title + "', ");
                    sSQL.Append("rol_alias = '" + Details.Alias + "', ");
                    sSQL.Append("rol_description = '" + Details.Description + "', ");
                    sSQL.Append("rol_createddate = '" + Details.CreatedDate.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "', ");
                    sSQL.Append("rol_createdby = '" + Details.CreatedBy + "', ");
                    sSQL.Append("rol_updateddate = '" + Details.UpdatedDate.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "', ");
                    sSQL.Append("rol_updatedby = '" + Details.UpdatedBy + "', ");
                    sSQL.Append("rol_hidden = " + (!Details.Hidden ? "0" : "1") + ", ");
                    sSQL.Append("rol_deleted = " + (!Details.Deleted ? "0" : "1") + " ");
                    sSQL.Append("WHERE rol_id = " + Details.Id.ToString());
                    using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, true))
                    {
                        EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                        oDo.ExecuteNonQuery(sSQL.ToString());
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);

                        //EventLog.LogEvent("CALL sort_rol_roles();", FUNCTIONNAME, String.Empty);
                        //oDo.ExecuteNonQuery("CALL sort_rol_roles();");
                        //if (oDo.HasError)
                        //    throw new Exception(oDo.GetError);

                    }
                    SortAll();
                    ResetThis();
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void Delete(RolesModel.RoleModel Details)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::Delete]";
                try
                {
                    RecursiveDelete(Details.Id);
                    //using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, false))
                    //{
                    //    EventLog.LogEvent("CALL sort_rol_roles();", FUNCTIONNAME, String.Empty);
                    //    oDo.ExecuteNonQuery("CALL sort_rol_roles();");
                    //    if (oDo.HasError)
                    //        throw new Exception(oDo.GetError);
                    //}
                    SortAll();
                    ResetThis();
                    Details = null;
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void RecursiveDelete(Int32 Id)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::RecursiveDelete]";
                try
                {
                    if (Id.Equals(0))
                        return;

                    using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, true))
                    {
                        String sSQL1 = "UPDATE asr_authorizedsitesroles SET asr_deleted = 1 WHERE rol_id = " + Id.ToString();
                        String sSQL2 = "UPDATE adr_authorizeddocumentsroles SET adr_deleted = 1 WHERE rol_id = " + Id.ToString();
                        String sSQL3 = "UPDATE amr_authorizedmodulesroles SET amr_deleted = 1 WHERE rol_id = " + Id.ToString();
                        String sSQL4 = "UPDATE apr_authorizedpagesroles SET apr_deleted = 1 WHERE rol_id = " + Id.ToString();
                        String sSQL5 = "UPDATE atr_authorizedtasksroles SET atr_deleted = 1 WHERE rol_id = " + Id.ToString();
                        String sSQL6 = "UPDATE uro_usersroles SET uro_deleted = 1 WHERE rol_id = " + Id.ToString();
                        String sSQL7 = "UPDATE rol_roles SET rol_deleted = 1 WHERE rol_id = " + Id.ToString();

                        EventLog.LogEvent(sSQL1, FUNCTIONNAME, String.Empty);
                        System.Diagnostics.Debug.WriteLine(sSQL1);
                        oDo.ExecuteNonQuery(sSQL1);
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);

                        EventLog.LogEvent(sSQL2, FUNCTIONNAME, String.Empty);
                        System.Diagnostics.Debug.WriteLine(sSQL2);
                        oDo.ExecuteNonQuery(sSQL2);
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);

                        EventLog.LogEvent(sSQL3, FUNCTIONNAME, String.Empty);
                        System.Diagnostics.Debug.WriteLine(sSQL3);
                        oDo.ExecuteNonQuery(sSQL3);
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);

                        EventLog.LogEvent(sSQL4, FUNCTIONNAME, String.Empty);
                        System.Diagnostics.Debug.WriteLine(sSQL4);
                        oDo.ExecuteNonQuery(sSQL4);
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);

                        EventLog.LogEvent(sSQL5, FUNCTIONNAME, String.Empty);
                        System.Diagnostics.Debug.WriteLine(sSQL5);
                        oDo.ExecuteNonQuery(sSQL5);
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);

                        EventLog.LogEvent(sSQL6, FUNCTIONNAME, String.Empty);
                        System.Diagnostics.Debug.WriteLine(sSQL6);
                        oDo.ExecuteNonQuery(sSQL6);
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);

                        EventLog.LogEvent(sSQL7, FUNCTIONNAME, String.Empty);
                        System.Diagnostics.Debug.WriteLine(sSQL7);
                        oDo.ExecuteNonQuery(sSQL7);
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);
                    }
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void ResetThis()
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::ResetThis]";
                try
                {
                    CacheData.Reset("LiquidCore.Roles");
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            //[ObsoleteAttribute("SortAll has been deprecated. All sorting take place in stored procedures instead.")]
            public static void SortAll()
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::SortAll]";
                try
                {
                    Int32 Order = (Int32)OrderMinMax.Min;
                    StringBuilder sSQL = new StringBuilder();
                    sSQL.Append("SELECT rol_id FROM rol_roles WHERE rol_deleted = 0 ORDER BY sta_id, lng_id, rol_parentid, rol_order");
                    using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, false))
                    {
                        EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                        DataTable dt = oDo.GetDataTable(sSQL.ToString());
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);
                        StringBuilder sSQL2 = new StringBuilder();
                        foreach (DataRow dr in dt.Rows)
                        {
                            sSQL2.AppendLine("UPDATE rol_roles SET rol_order = " + Order.ToString() + " WHERE rol_id = " + dr["rol_id"].ToString() + ";");
                            Order = Order + (Int32)OrderMinMax.Step;
                            if (sSQL2.Length > DataBlockSize)
                            {
                                EventLog.LogEvent(sSQL2.ToString(), FUNCTIONNAME, String.Empty);
                                if (!(oDo.ExecuteNonQuery(sSQL2.ToString()) > 0))
                                    throw new Exception(oDo.GetError);
                                sSQL2 = new StringBuilder();
                            }
                        }
                        if (sSQL2.Length > 0)
                        {
                            EventLog.LogEvent(sSQL2.ToString(), FUNCTIONNAME, String.Empty);
                            if (!(oDo.ExecuteNonQuery(sSQL2.ToString()) > 0))
                                throw new Exception(oDo.GetError);
                        }
                        ResetThis();
                    }
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static Boolean FindChildren(RolesModel.RoleModel Details)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::FindChildren]";
                DataTable dt = null;
                try
                {
                    String CacheItem = "LiquidCore.Roles.Role.Children(" + Details.Id.ToString() + ")";
                    if (HttpContext.Current != null) if (HttpContext.Current != null) dt = (DataTable)HttpContext.Current.Cache[CacheItem];
                    if (dt == null)
                    {
                        StringBuilder sSQL = new StringBuilder();
                        sSQL.Append("SELECT rol_id FROM rol_roles WHERE rol_deleted = 0 AND rol_parentid = " + Details.Id.ToString());
                        using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true))
                        {
                            EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                            dt = oDo.GetDataTable(sSQL.ToString());
                        }
                        CacheData.Insert(CacheItem, dt);
                    }
                    if (dt.Rows.Count > 0)
                        return true;

                    return false;
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                    return false;
                }
            }
            public static void AddToUser(RolesModel.RoleModel Details, Int32 UsrId)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::AddToUser]";
                try
                {
                    StringBuilder sSQL = new StringBuilder();
                    sSQL.Append("INSERT INTO uro_usersroles (sta_id, lng_id, usr_id, rol_id, uro_createddate, uro_createdby, ");
                    sSQL.Append("uro_updateddate, uro_updatedby, uro_hidden, uro_deleted) VALUES ( ");
                    sSQL.Append(Details.Status.ToString() + ", ");
                    sSQL.Append(Details.Language.ToString() + ", ");
                    sSQL.Append(UsrId.ToString() + ", ");
                    sSQL.Append(Details.Id.ToString() + ", ");
                    sSQL.Append("'" + DateTime.Now.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "', ");
                    sSQL.Append("'" + Authentication.User.Identity.Name + "', ");
                    sSQL.Append("'" + DateTime.Now.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "', ");
                    sSQL.Append("'" + Authentication.User.Identity.Name + "', ");
                    sSQL.Append("0, ");
                    sSQL.Append("0)");
                    using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, true))
                    {
                        EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                        if (oDo.ExecuteNonQuery(sSQL.ToString()).Equals(-1))
                            throw new Exception(oDo.GetError);
                    }
                    ResetThis();
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void DeleteFromUser(RolesModel.RoleModel Details, Int32 UsrId)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::DeleteFromUser]";
                try
                {
                    System.Data.DataSet ds = new System.Data.DataSet();
                    StringBuilder sSQL = new StringBuilder();
                    sSQL.Append("UPDATE uro_usersroles SET uro_deleted = 1 WHERE usr_id = " + UsrId.ToString() + " AND rol_id = " + Details.Id.ToString());
                    using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, true))
                    {
                        EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                        oDo.ExecuteNonQuery(sSQL.ToString());
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);
                    }
                    ResetThis();
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static ArrayList GetUsers(RolesModel.RoleModel Details)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::GetUsers]";
                ArrayList Users = new ArrayList();
                DataTable dt = null;
                try
                {
                    String CacheItem = "LiquidCore.Roles" + FUNCTIONNAME + "::UsersInRole" + Details.Id.ToString();
                    if (HttpContext.Current != null) if (HttpContext.Current != null) dt = (DataTable)HttpContext.Current.Cache[CacheItem];
                    if (dt == null)
                    {
                        StringBuilder sSQL = new StringBuilder();
                        sSQL.Append("SELECT usr_id FROM uro_usersroles WHERE rol_id = " + Details.Id.ToString() + " AND uro_deleted = 0");
                        using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, true))
                        {
                            EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                            dt = oDo.GetDataTable(sSQL.ToString());
                        }
                        CacheData.Insert(CacheItem, dt);
                    }
                    foreach (DataRow dr in dt.Rows)
                        Users.Add(dr["usr_id"].ToString());
                    return Users;  
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                    return Users;
                }
            }
            public static Boolean CheckRoleAlias(Int32 RolId, String Alias)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::CheckRoleAlias]";
                DataTable dt = null;
                try
                {
                    String CacheItem = "LiquidCore.Roles" + FUNCTIONNAME + "::RolId" + RolId.ToString();
                    if (HttpContext.Current != null) if (HttpContext.Current != null) dt = (DataTable)HttpContext.Current.Cache[CacheItem];
                    if (dt == null)
                    {
                        StringBuilder sSQL = new StringBuilder();
                        sSQL.Append("SELECT rol_title FROM rol_roles WHERE rol_id = " + RolId.ToString() + " AND rol_deleted = 0");
                        using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, true))
                        {
                            EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                            dt = oDo.GetDataTable(sSQL.ToString());
                        }
                        CacheData.Insert(CacheItem, dt);
                    }
                    foreach (DataRow dr in dt.Rows)
                        if (dr["rol_title"].ToString().ToLower().Equals(Alias.ToLower()))
                            return true;
                    return false;
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                    return false;
                }
            }
            public static string[] GetRolesArray(string LoginName, string Password, bool UseRoleCache)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::GetRolesArray]";
                try
                {
                    ArrayList userRoles = new ArrayList();
                    if (UseRoleCache)
                    {
                        string CacheKey = "LiquidCore.Roles" + FUNCTIONNAME + "GetRolesArray-" + LoginName + "-" + Password;
                        userRoles = (ArrayList)HttpContext.Current.Cache[CacheKey];
                        if (userRoles == null)
                            GetRolesArray_Loader(CacheKey, LoginName, Password);
                        userRoles = (ArrayList)HttpContext.Current.Cache[CacheKey];
                        return (String[])userRoles.ToArray(typeof(String));
                    }
                    else
                    {
                        StringBuilder sSQL = new StringBuilder();
                        sSQL.Append("SELECT DISTINCT rol_roles.rol_id FROM ");
                        sSQL.Append("rol_roles, uro_usersroles, usr_users WHERE ");
                        sSQL.Append("rol_roles.rol_id = uro_usersroles.rol_id AND ");
                        sSQL.Append("uro_usersroles.usr_id = usr_users.usr_id AND ");
                        sSQL.Append("uro_usersroles.uro_deleted = 0 AND ");
                        sSQL.Append("rol_roles.rol_deleted = 0 AND ");
                        sSQL.Append("usr_users.usr_deleted = 0 AND ");
                        sSQL.Append("usr_users.usr_loginname = '" + LoginName + "' AND usr_users.usr_password = '" + Security.Encrypt(Password) + "'");
                        using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, true))
                        {
                            EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                            IDataReader oReader = oDo.ExecuteReader(sSQL.ToString());
                            while (oReader.Read())
                            {
                                userRoles.Add(oReader["rol_id"].ToString());
                            }
                            oReader.Close();
                        }
                        return (String[])userRoles.ToArray(typeof(String));
                    }

                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, String.Empty);
                    return null;
                }
            }
            public static void GetRolesArray_Loader(String CacheKey, String LoginName, String Password)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::GetRolesArray_Loader]";
                try
                {
                    Double CacheTimeOut = 1;
                    ArrayList userRoles = new ArrayList();
                    StringBuilder sSQL = new StringBuilder();
                    sSQL.Append("SELECT DISTINCT rol_roles.rol_id FROM ");
                    sSQL.Append("rol_roles, uro_usersroles, usr_users WHERE ");
                    sSQL.Append("rol_roles.rol_id = uro_usersroles.rol_id AND ");
                    sSQL.Append("uro_usersroles.usr_id = usr_users.usr_id AND ");
                    sSQL.Append("uro_usersroles.uro_deleted = 0 AND ");
                    sSQL.Append("rol_roles.rol_deleted = 0 AND ");
                    sSQL.Append("usr_users.usr_deleted = 0 AND ");
                    sSQL.Append("usr_users.usr_loginname = '" + LoginName + "' AND usr_users.usr_password = '" + Security.Encrypt(Password) + "'");
                    using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, true))
                    {
                        EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                        IDataReader oReader = oDo.ExecuteReader(sSQL.ToString());
                        while (oReader.Read())
                        {
                            userRoles.Add(oReader["rol_id"].ToString());
                        }
                        oReader.Close();
                    }
                    try
                    {
                        CacheTimeOut = Convert.ToDouble(Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Data.CacheTimeOutInMinutes"].ToString()));
                    }
                    catch (Exception)
                    { }
                    HttpContext.Current.Cache.Insert(CacheKey, userRoles, null, DateTime.MaxValue, TimeSpan.FromMinutes(CacheTimeOut));
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, String.Empty);
                }
            }
        }
        public static class UserData
        {
            static string CLASSNAME = "[Namespace::LiquidCore][Class::UserData]";

            static Int32 DataBlockSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Data.DataBlockSize"].ToString());
            static Int32 UseRevision = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Data.UseRevision"].ToString());

            public static void Create(UsersModel.UserModel Details)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::Create]";
                try
                {
                    ((UsersModel.UserModel)Details).Order = GetHighestOrder() + ((int)OrderMinMax.Step);
                    ((UsersModel.UserModel)Details).UpdatedDate = DateTime.Now;
                    ((UsersModel.UserModel)Details).UpdatedBy = Authentication.User.Identity.Name;
                    ((UsersModel.UserModel)Details).CreatedDate = DateTime.Now;
                    ((UsersModel.UserModel)Details).CreatedBy = Authentication.User.Identity.Name;

                    System.Data.DataSet ds = new System.Data.DataSet();
                    StringBuilder sSQL1 = new StringBuilder();
                    sSQL1.Append("INSERT INTO usr_users (sta_id, lng_id, ust_id, usr_parentid, usr_order, usr_title, ");
                    sSQL1.Append("usr_alias, usr_description, usr_mail, usr_loginname, usr_password, ");
                    sSQL1.Append("usr_firstname, usr_middlename, usr_lastname, usr_address, usr_co, usr_postalcode, ");
                    sSQL1.Append("usr_city, usr_country, usr_phone, usr_mobile, usr_fax, usr_company, usr_startpage, ");
                    sSQL1.Append("usr_createddate, usr_createdby, ");
                    sSQL1.Append("usr_updateddate, usr_updatedby, usr_hidden, usr_deleted) VALUES ( ");
                    sSQL1.Append(Details.Status.ToString() + ", ");
                    sSQL1.Append(Details.Language.ToString() + ", ");
                    sSQL1.Append(Details.Type.ToString() + ", ");
                    sSQL1.Append(Details.ParentId.ToString() + ", ");
                    sSQL1.Append(Details.Order.ToString() + ", ");
                    sSQL1.Append("'" + Details.Title + "', ");
                    sSQL1.Append("'" + Details.Alias + "', ");
                    sSQL1.Append("'" + Details.Description + "', ");
                    sSQL1.Append("'" + Details.Mail + "', ");
                    sSQL1.Append("'" + Details.LoginName + "', ");
                    sSQL1.Append("'" + Security.Encrypt(Details.Password) + "', ");
                    sSQL1.Append("'" + Details.FirstName + "', ");
                    sSQL1.Append("'" + Details.MiddleName + "', ");
                    sSQL1.Append("'" + Details.LastName + "', ");
                    sSQL1.Append("'" + Details.Address + "', ");
                    sSQL1.Append("'" + Details.CO + "', ");
                    sSQL1.Append("'" + Details.PostalCode + "', ");
                    sSQL1.Append("'" + Details.City + "', ");
                    sSQL1.Append("'" + Details.Country + "', ");
                    sSQL1.Append("'" + Details.Phone + "', ");
                    sSQL1.Append("'" + Details.Mobile + "', ");
                    sSQL1.Append("'" + Details.Fax + "', ");
                    sSQL1.Append("'" + Details.Company + "', ");
                    sSQL1.Append(Details.StartPage.ToString() + ", ");
                    sSQL1.Append("'" + Details.CreatedDate.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "', ");
                    sSQL1.Append("'" + Details.CreatedBy + "', ");
                    sSQL1.Append("'" + Details.UpdatedDate.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "', ");
                    sSQL1.Append("'" + Details.UpdatedBy + "', ");
                    sSQL1.Append("" + (Details.Hidden ? "1" : "0") + ", ");
                    sSQL1.Append("" + (Details.Deleted ? "1" : "0") + ")");
                    using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, true))
                    {
                        EventLog.LogEvent(sSQL1.ToString(), FUNCTIONNAME, String.Empty);
                        oDo.ExecuteNonQuery(sSQL1.ToString());
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);

                        EventLog.LogEvent("SELECT @@IDENTITY", FUNCTIONNAME, String.Empty);
                        ((UsersModel.UserModel)Details).Id = Convert.ToInt32(oDo.GetDataTable("SELECT @@IDENTITY").Rows[0][0]);
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);

                        //EventLog.LogEvent("CALL sort_usr_users();", FUNCTIONNAME, String.Empty);
                        //oDo.ExecuteNonQuery("CALL sort_usr_users();");
                        //if (oDo.HasError)
                        //    throw new Exception(oDo.GetError);

                    }
                    //SortAll();
                    ResetThis();
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void Load(ModelObject Details, NameValueSet DataFields)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::Load]";
                try
                {
                    System.Data.DataTable dt = null;
                    String CacheItem = "LiquidCore.Users.User.Id(" + DataFields["Id"].ToString() + ")";
                    if (HttpContext.Current != null) if (HttpContext.Current != null) dt = (DataTable)HttpContext.Current.Cache[CacheItem];
                    if (dt == null)
                    {
                        StringBuilder sSQL = new StringBuilder();
                        sSQL.Append("SELECT * FROM usr_users WHERE usr_id = " + DataFields["Id"].ToString() + " AND usr_deleted = 0");
                        using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true))
                        {
                            EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                            dt = oDo.GetDataTable(sSQL.ToString());
                            if (oDo.HasError)
                                throw new Exception(oDo.GetError);
                        }
                        CacheData.Insert(CacheItem, dt);
                    }
                    if (dt.Rows.Count > 0)
                    {
                        System.Data.DataRow dr = dt.Rows[0];
                        ((UsersModel.UserModel)Details).Id = Convert.ToInt32(dr["usr_id"]);
                        ((UsersModel.UserModel)Details).Status = Convert.ToInt32(dr["sta_id"]);
                        ((UsersModel.UserModel)Details).Language = Convert.ToInt32(dr["lng_id"]);
                        ((UsersModel.UserModel)Details).Type = Convert.ToInt32(dr["ust_id"]);
                        ((UsersModel.UserModel)Details).ParentId = Convert.ToInt32(dr["usr_parentid"]);
                        ((UsersModel.UserModel)Details).Order = Convert.ToInt32(dr["usr_order"]);
                        ((UsersModel.UserModel)Details).Title = Convert.ToString(dr["usr_title"]);
                        ((UsersModel.UserModel)Details).Alias = Convert.ToString(dr["usr_alias"]);
                        ((UsersModel.UserModel)Details).Description = Convert.ToString(dr["usr_description"]);
                        ((UsersModel.UserModel)Details).Mail = Convert.ToString(dr["usr_mail"]);
                        ((UsersModel.UserModel)Details).LoginName = Convert.ToString(dr["usr_loginname"]);
                        ((UsersModel.UserModel)Details).Password = Security.Decrypt(Convert.ToString(dr["usr_password"]));
                        ((UsersModel.UserModel)Details).FirstName = Convert.ToString(dr["usr_firstname"]);
                        ((UsersModel.UserModel)Details).MiddleName = Convert.ToString(dr["usr_middlename"]);
                        ((UsersModel.UserModel)Details).LastName = Convert.ToString(dr["usr_lastname"]);
                        ((UsersModel.UserModel)Details).Address = Convert.ToString(dr["usr_address"]);
                        ((UsersModel.UserModel)Details).CO = Convert.ToString(dr["usr_co"]);
                        ((UsersModel.UserModel)Details).PostalCode = Convert.ToString(dr["usr_postalcode"]);
                        ((UsersModel.UserModel)Details).City = Convert.ToString(dr["usr_city"]);
                        ((UsersModel.UserModel)Details).Country = Convert.ToString(dr["usr_country"]);
                        ((UsersModel.UserModel)Details).Phone = Convert.ToString(dr["usr_phone"]);
                        ((UsersModel.UserModel)Details).Mobile = Convert.ToString(dr["usr_mobile"]);
                        ((UsersModel.UserModel)Details).Fax = Convert.ToString(dr["usr_fax"]);
                        ((UsersModel.UserModel)Details).Company = Convert.ToString(dr["usr_company"]);
                        ((UsersModel.UserModel)Details).StartPage = Convert.ToInt32(dr["usr_startpage"]);
                        ((UsersModel.UserModel)Details).CreatedDate = Convert.ToDateTime(dr["usr_createddate"]);
                        ((UsersModel.UserModel)Details).CreatedBy = Convert.ToString(dr["usr_createdby"]);
                        ((UsersModel.UserModel)Details).UpdatedDate = Convert.ToDateTime(dr["usr_updateddate"]);
                        ((UsersModel.UserModel)Details).UpdatedBy = Convert.ToString(dr["usr_updatedby"]);
                        ((UsersModel.UserModel)Details).Hidden = (dr["usr_hidden"].ToString().Equals("0") ? false : true);
                        ((UsersModel.UserModel)Details).Deleted = (dr["usr_deleted"].ToString().Equals("0") ? false : true);
                    }
                    else
                        ((UsersModel.UserModel)Details).Deleted = true;
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void LoadAll(ref List<Users.User> _objects)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::LoadAll]";
                try
                {
                    DataTable dt = null;
                    String CacheItem = "LiquidCore.Users";
                    if (HttpContext.Current != null) if (HttpContext.Current != null) dt = (DataTable)HttpContext.Current.Cache[CacheItem];
                    if (dt == null)
                    {
                        StringBuilder sSQL = new StringBuilder();
                        sSQL.Append("SELECT usr_id FROM usr_users WHERE usr_deleted = 0 ORDER BY sta_id, lng_id, usr_parentid, usr_order");
                        using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true))
                        {
                            EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                            dt = oDo.GetDataTable(sSQL.ToString());
                            if (oDo.HasError)
                                throw new Exception(oDo.GetError);
                        }
                        CacheData.Insert(CacheItem, dt);
                    }
                    foreach (DataRow dr in dt.Rows)
                    {
                        Users.User o = new Users.User(Convert.ToInt32(dr["usr_id"].ToString()));
                        _objects.Add(o);
                    }
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void LoadAll(ref List<Users.User> _objects, NameValueSet DataFields)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::LoadAll]";
                try
                {
                    String PrimaryKey = String.Empty;
                    String CacheItem = String.Empty;
                    StringBuilder sSQL = new StringBuilder();
                    if (DataFields["Alias"] != null)
                    {
                        PrimaryKey = DataFields["Alias"].ToString();
                        CacheItem = "LiquidCore.Users.PrimaryKey(Alias(" + PrimaryKey + "))";
                        sSQL.Append("SELECT usr_id FROM usr_users WHERE usr_alias = '" + PrimaryKey + "' AND usr_deleted = 0 ORDER BY sta_id, lng_id, usr_parentid, usr_order");
                    }
                    else
                    {
                        LoadAll(ref _objects);
                        return;
                    }
                    DataTable dt = null;
                    if (HttpContext.Current != null) if (HttpContext.Current != null) dt = (DataTable)HttpContext.Current.Cache[CacheItem];
                    if (dt == null)
                    {
                        using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true))
                        {
                            EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                            dt = oDo.GetDataTable(sSQL.ToString());
                            if (oDo.HasError)
                                throw new Exception(oDo.GetError);
                        }
                        CacheData.Insert(CacheItem, dt);
                    }
                    foreach (DataRow dr in dt.Rows)
                    {
                        Users.User o = new Users.User(Convert.ToInt32(dr["usr_id"].ToString()));
                        _objects.Add(o);
                    }
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void Update(UsersModel.UserModel Details)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::Update]";
                try
                {
                    ((UsersModel.UserModel)Details).UpdatedDate = DateTime.Now;
                    ((UsersModel.UserModel)Details).UpdatedBy = Authentication.User.Identity.Name;

                    System.Data.DataSet ds = new System.Data.DataSet();
                    StringBuilder sSQL = new StringBuilder();
                    sSQL.Append("UPDATE usr_users SET ");
                    sSQL.Append("sta_id = " + Details.Status.ToString() + ", ");
                    sSQL.Append("lng_id = " + Details.Language.ToString() + ", ");
                    sSQL.Append("ust_id = " + Details.Type.ToString() + ", ");
                    sSQL.Append("usr_parentid = " + Details.ParentId.ToString() + ", ");
                    sSQL.Append("usr_order = " + Details.Order.ToString() + ", ");
                    sSQL.Append("usr_title = '" + Details.Title + "', ");
                    sSQL.Append("usr_alias = '" + Details.Alias + "', ");
                    sSQL.Append("usr_description = '" + Details.Description + "', ");
                    sSQL.Append("usr_mail = '" + Details.Mail + "', ");
                    sSQL.Append("usr_loginname = '" + Details.LoginName + "', ");
                    sSQL.Append("usr_password = '" + Security.Encrypt(Details.Password) + "', ");
                    sSQL.Append("usr_firstname = '" + Details.FirstName + "', ");
                    sSQL.Append("usr_middlename = '" + Details.MiddleName + "', ");
                    sSQL.Append("usr_lastname = '" + Details.LastName + "', ");
                    sSQL.Append("usr_address = '" + Details.Address + "', ");
                    sSQL.Append("usr_co = '" + Details.CO + "', ");
                    sSQL.Append("usr_postalcode = '" + Details.PostalCode + "', ");
                    sSQL.Append("usr_city = '" + Details.City + "', ");
                    sSQL.Append("usr_country = '" + Details.Country + "', ");
                    sSQL.Append("usr_phone = '" + Details.Phone + "', ");
                    sSQL.Append("usr_mobile = '" + Details.Mobile + "', ");
                    sSQL.Append("usr_fax = '" + Details.Fax + "', ");
                    sSQL.Append("usr_company = '" + Details.Company + "', ");
                    sSQL.Append("usr_startpage = " + Details.StartPage.ToString() + ", ");
                    sSQL.Append("usr_createddate = '" + Details.CreatedDate.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "', ");
                    sSQL.Append("usr_createdby = '" + Details.CreatedBy + "', ");
                    sSQL.Append("usr_updateddate = '" + Details.UpdatedDate.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "', ");
                    sSQL.Append("usr_updatedby = '" + Details.UpdatedBy + "', ");
                    sSQL.Append("usr_hidden = " + (!Details.Hidden ? "0" : "1") + ", ");
                    sSQL.Append("usr_deleted = " + (!Details.Deleted ? "0" : "1") + " ");
                    sSQL.Append("WHERE usr_id = " + Details.Id.ToString());
                    using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, true))
                    {
                        EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                        oDo.ExecuteNonQuery(sSQL.ToString());
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);

                        //EventLog.LogEvent("CALL sort_usr_users();", FUNCTIONNAME, String.Empty);
                        //oDo.ExecuteNonQuery("CALL sort_usr_users();");
                        //if (oDo.HasError)
                        //    throw new Exception(oDo.GetError);

                    }
                    SortAll();
                    ResetThis();
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void Delete(UsersModel.UserModel Details)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::Delete]";
                try
                {
                    RecursiveDelete(Details.Id);
                    //using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, false))
                    //{
                    //    EventLog.LogEvent("CALL sort_usr_users();", FUNCTIONNAME, String.Empty);
                    //    oDo.ExecuteNonQuery("CALL sort_usr_users();");
                    //    if (oDo.HasError)
                    //        throw new Exception(oDo.GetError);
                    //}
                    SortAll();
                    ResetThis();
                    Details = null;
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void RecursiveDelete(Int32 Id)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::RecursiveDelete]";
                try
                {
                    if (Id.Equals(0))
                        return;

                    using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, true))
                    {
                        String sSQL1 = "UPDATE uro_usersroles SET uro_deleted = 1 WHERE usr_id = " + Id.ToString();
                        String sSQL2 = "UPDATE usr_users SET usr_deleted = 1 WHERE usr_id = " + Id.ToString();

                        EventLog.LogEvent(sSQL1, FUNCTIONNAME, String.Empty);
                        System.Diagnostics.Debug.WriteLine(sSQL1);
                        oDo.ExecuteNonQuery(sSQL1);
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);

                        EventLog.LogEvent(sSQL2, FUNCTIONNAME, String.Empty);
                        System.Diagnostics.Debug.WriteLine(sSQL2);
                        oDo.ExecuteNonQuery(sSQL2);
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);
                    }
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void ResetThis()
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::ResetThis]";
                try
                {
                    CacheData.Reset("LiquidCore.Users");
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            //[ObsoleteAttribute("SortAll has been deprecated. All sorting take place in stored procedures instead.")]
            public static void SortAll()
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::SortAll]";
                try
                {
                    Int32 Order = (Int32)OrderMinMax.Min;
                    StringBuilder sSQL = new StringBuilder();
                    sSQL.Append("SELECT usr_id FROM usr_users WHERE usr_deleted = 0 ORDER BY sta_id, lng_id, usr_parentid, usr_order");
                    using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, false))
                    {
                        EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                        DataTable dt = oDo.GetDataTable(sSQL.ToString());
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);
                        StringBuilder sSQL2 = new StringBuilder();
                        foreach (DataRow dr in dt.Rows)
                        {
                            sSQL2.AppendLine("UPDATE usr_users SET usr_order = " + Order.ToString() + " WHERE usr_id = " + dr["usr_id"].ToString() + ";");
                            Order = Order + (Int32)OrderMinMax.Step;
                            if (sSQL2.Length > DataBlockSize)
                            {
                                EventLog.LogEvent(sSQL2.ToString(), FUNCTIONNAME, String.Empty);
                                if (!(oDo.ExecuteNonQuery(sSQL2.ToString()) > 0))
                                    throw new Exception(oDo.GetError);
                                sSQL2 = new StringBuilder();
                            }
                        }
                        if (sSQL2.Length > 0)
                        {
                            EventLog.LogEvent(sSQL2.ToString(), FUNCTIONNAME, String.Empty);
                            if (!(oDo.ExecuteNonQuery(sSQL2.ToString()) > 0))
                                throw new Exception(oDo.GetError);
                        }
                        ResetThis();
                    }
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
			public static int GetHighestOrder()
			{
				string FUNCTIONNAME = CLASSNAME + "[Function::GetHighestOrder]";
				try
				{
					Int32 Order = (Int32)OrderMinMax.Min;
					StringBuilder sSQL = new StringBuilder();
					sSQL.Append("SELECT usr_order FROM usr_users WHERE usr_deleted = 0 ORDER BY usr_order DESC LIMIT 1");
					using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, false))
					{
						EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
						DataTable dt = oDo.GetDataTable(sSQL.ToString());
						if (oDo.HasError)
							throw new Exception(oDo.GetError);
						if (dt.Rows.Count > 0)
						{
							return Convert.ToInt32(dt.Rows[0]["usr_order"].ToString());
						}
						else
						{
							return (int)OrderMinMax.Min;
						}
					}
				}
				catch (Exception ex)
				{
					Error.Report(ex, FUNCTIONNAME, "");
					return (int)OrderMinMax.Max;
				}
			}
            public static Boolean FindChildren(UsersModel.UserModel Details)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::FindChildren]";
                DataTable dt = null;
                try
                {
                    String CacheItem = "LiquidCore.Users.User.Children(" + Details.Id.ToString() + ")";
                    if (HttpContext.Current != null) if (HttpContext.Current != null) dt = (DataTable)HttpContext.Current.Cache[CacheItem];
                    if (dt == null)
                    {
                        StringBuilder sSQL = new StringBuilder();
                        sSQL.Append("SELECT usr_id FROM usr_users WHERE usr_deleted = 0 AND usr_parentid = " + Details.Id.ToString());
                        using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true))
                        {
                            EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                            dt = oDo.GetDataTable(sSQL.ToString());
                        }
                        CacheData.Insert(CacheItem, dt);
                    }
                    if (dt.Rows.Count > 0)
                        return true;

                    return false;
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                    return false;
                }
            }
            public static void AddToRole(UsersModel.UserModel Details, Int32 RolId)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::AddToRole]";
                try
                {
                    StringBuilder sSQL = new StringBuilder();
                    sSQL.Append("INSERT INTO uro_usersroles (sta_id, lng_id, usr_id, rol_id, uro_createddate, uro_createdby, ");
                    sSQL.Append("uro_updateddate, uro_updatedby, uro_hidden, uro_deleted) VALUES ( ");
                    sSQL.Append(Details.Status.ToString() + ", ");
                    sSQL.Append(Details.Language.ToString() + ", ");
                    sSQL.Append(Details.Id.ToString() + ", ");
                    sSQL.Append(RolId.ToString() + ", ");
                    sSQL.Append("'" + DateTime.Now.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "', ");
                    sSQL.Append("'" + Authentication.User.Identity.Name + "', ");
                    sSQL.Append("'" + DateTime.Now.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "', ");
                    sSQL.Append("'" + Authentication.User.Identity.Name + "', ");
                    sSQL.Append("0, ");
                    sSQL.Append("0)");
                    using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, true))
                    {
                        EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                        if (oDo.ExecuteNonQuery(sSQL.ToString()).Equals(-1))
                            throw new Exception(oDo.GetError);
                    }
                    ResetThis();
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void DeleteFromRole(UsersModel.UserModel Details, Int32 RolId)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::DeleteFromRole]";
                try
                {
                    System.Data.DataSet ds = new System.Data.DataSet();
                    StringBuilder sSQL = new StringBuilder();
                    sSQL.Append("UPDATE uro_usersroles SET uro_deleted = 1 WHERE usr_id = " + Details.Id.ToString() + " AND rol_id = " + RolId.ToString());
                    using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, true))
                    {
                        EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                        oDo.ExecuteNonQuery(sSQL.ToString());
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);
                    }
                    ResetThis();
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static ArrayList GetRoles(UsersModel.UserModel Details)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::GetRoles]";
                ArrayList Roles = new ArrayList();
                DataTable dt = null;
                try
                {
                    String CacheItem = "LiquidCore.Users" + FUNCTIONNAME + "::RolesForUser" + Details.Id.ToString();
                    if (HttpContext.Current != null) if (HttpContext.Current != null) dt = (DataTable)HttpContext.Current.Cache[CacheItem];
                    if (dt == null)
                    {
                        StringBuilder sSQL = new StringBuilder();
                        sSQL.Append("SELECT rol_id FROM uro_usersroles WHERE usr_id = " + Details.Id.ToString() + " AND uro_deleted = 0");
                        using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, true))
                        {
                            EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                            dt = oDo.GetDataTable(sSQL.ToString());
                        }
                        CacheData.Insert(CacheItem, dt);
                    }
                    foreach (DataRow dr in dt.Rows)
                        Roles.Add(dr["rol_id"].ToString());
                    return Roles;
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                    return Roles;
                }
            }
            public static void SignIn(String Name, String Password)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::SignIn]";
                try
                {
                    StringBuilder sSQL = new StringBuilder();
                    sSQL.Append("SELECT usr_users.usr_id, ");
                    sSQL.Append("rol_roles.rol_title FROM usr_users ");
                    sSQL.Append("JOIN uro_usersroles ON uro_usersroles.usr_id = usr_users.usr_id ");
                    sSQL.Append("JOIN rol_roles ON rol_roles.rol_id = uro_usersroles.rol_id ");
                    sSQL.Append("WHERE ");
                    sSQL.Append("usr_users.usr_loginname = '" + Name + "' AND ");
                    sSQL.Append("usr_users.usr_password = '" + Security.Encrypt(Password) + "' AND ");
                    sSQL.Append("usr_users.usr_deleted = 0 AND uro_usersroles.uro_deleted = 0 AND ");
                    sSQL.Append("rol_roles.rol_deleted = 0");
                    using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, true))
                    {
                        EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                        DataTable dt = oDo.GetDataTable(sSQL.ToString());
                        if (dt.Rows.Count > 0)
                        {
                            // Data Values...
							LiquidCore.Authentication.User.Identity.AuthenticatedTimestamp = DateTime.Now;
							LiquidCore.Authentication.User.Identity.Id = Convert.ToInt32(dt.Rows[0]["usr_id"].ToString());
                            LiquidCore.Authentication.User.Identity.Name = Name;
                            LiquidCore.Authentication.User.Identity.Password = Password;
                            LiquidCore.Authentication.User.Identity.Role = dt.Rows[0]["rol_title"].ToString();
                            LiquidCore.Authentication.User.Identity.Authenticated = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, String.Empty);
                }
            }
            public static void SignOut()
            {
                LiquidCore.Authentication.User.Identity.Name = String.Empty;
                LiquidCore.Authentication.User.Identity.Password = String.Empty;
                LiquidCore.Authentication.User.Identity.Role = String.Empty;
                LiquidCore.Authentication.User.Identity.Authenticated = false;
				LiquidCore.Authentication.User.Identity.AuthenticatedTimestamp = DateTime.MinValue;
            }
        }
        public static class StatusData
        {
            static string CLASSNAME = "[Namespace::LiquidCore][Class::StatusData]";

            static Int32 DataBlockSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Data.DataBlockSize"].ToString());
            static Int32 UseRevision = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Data.UseRevision"].ToString());

            public static void Create(StatusModel.ItemModel Details)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::Create]";
                try
                {
                    ((StatusModel.ItemModel)Details).Order = (Int32)OrderMinMax.Max;
                    ((StatusModel.ItemModel)Details).UpdatedDate = DateTime.Now;
                    ((StatusModel.ItemModel)Details).UpdatedBy = Authentication.User.Identity.Name;
                    ((StatusModel.ItemModel)Details).CreatedDate = DateTime.Now;
                    ((StatusModel.ItemModel)Details).CreatedBy = Authentication.User.Identity.Name;

                    System.Data.DataSet ds = new System.Data.DataSet();
                    StringBuilder sSQL1 = new StringBuilder();
                    sSQL1.Append("INSERT INTO sta_status (lng_id, sta_parentid, sta_order, sta_revision, sta_title, ");
                    sSQL1.Append("sta_alias, sta_description, sta_createddate, sta_createdby, ");
                    sSQL1.Append("sta_updateddate, sta_updatedby, sta_hidden, sta_deleted) VALUES ( ");
                    sSQL1.Append(Details.Language.ToString() + ", ");
                    sSQL1.Append(Details.ParentId.ToString() + ", ");
                    sSQL1.Append(Details.Order.ToString() + ", ");
                    sSQL1.Append(Details.Revision.ToString() + ", ");
                    sSQL1.Append("'" + Details.Title + "', ");
                    sSQL1.Append("'" + Details.Alias + "', ");
                    sSQL1.Append("'" + Details.Description + "', ");
                    sSQL1.Append("'" + Details.CreatedDate.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "', ");
                    sSQL1.Append("'" + Details.CreatedBy + "', ");
                    sSQL1.Append("'" + Details.UpdatedDate.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "', ");
                    sSQL1.Append("'" + Details.UpdatedBy + "', ");
                    sSQL1.Append("" + (Details.Hidden ? "1" : "0") + ", ");
                    sSQL1.Append("" + (Details.Deleted ? "1" : "0") + ")");
                    using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, true))
                    {
                        EventLog.LogEvent(sSQL1.ToString(), FUNCTIONNAME, String.Empty);
                        oDo.ExecuteNonQuery(sSQL1.ToString());
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);

                        EventLog.LogEvent("SELECT @@IDENTITY", FUNCTIONNAME, String.Empty);
                        ((StatusModel.ItemModel)Details).Id = Convert.ToInt32(oDo.GetDataTable("SELECT @@IDENTITY").Rows[0][0]);
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);

                        //EventLog.LogEvent("CALL sort_sta_status();", FUNCTIONNAME, String.Empty);
                        //oDo.ExecuteNonQuery("CALL sort_sta_status();");
                        //if (oDo.HasError)
                        //    throw new Exception(oDo.GetError);

                    }
                    SortAll();
                    ResetThis();
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void Load(ModelObject Details, NameValueSet DataFields)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::Load]";
                try
                {
                    System.Data.DataTable dt = null;
                    String CacheItem = "LiquidCore.Status.Item.Id(" + DataFields["Id"].ToString() + ")";
                    if (HttpContext.Current != null) if (HttpContext.Current != null) dt = (DataTable)HttpContext.Current.Cache[CacheItem];
                    if (dt == null)
                    {
                        StringBuilder sSQL = new StringBuilder();
                        sSQL.Append("SELECT * FROM sta_status WHERE sta_id = " + DataFields["Id"].ToString() + " AND sta_deleted = 0");
                        using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true))
                        {
                            EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                            dt = oDo.GetDataTable(sSQL.ToString());
                            if (oDo.HasError)
                                throw new Exception(oDo.GetError);
                        }
                        CacheData.Insert(CacheItem, dt);
                    }
                    if (dt.Rows.Count > 0)
                    {
                        System.Data.DataRow dr = dt.Rows[0];
                        ((StatusModel.ItemModel)Details).Id = Convert.ToInt32(dr["sta_id"]);
                        ((StatusModel.ItemModel)Details).Language = Convert.ToInt32(dr["lng_id"]);
                        ((StatusModel.ItemModel)Details).ParentId = Convert.ToInt32(dr["sta_parentid"]);
                        ((StatusModel.ItemModel)Details).Order = Convert.ToInt32(dr["sta_order"]);
                        ((StatusModel.ItemModel)Details).Revision = Convert.ToInt32(dr["sta_revision"]);
                        ((StatusModel.ItemModel)Details).Title = Convert.ToString(dr["sta_title"]);
                        ((StatusModel.ItemModel)Details).Alias = Convert.ToString(dr["sta_alias"]);
                        ((StatusModel.ItemModel)Details).Description = Convert.ToString(dr["sta_description"]);
                        ((StatusModel.ItemModel)Details).CreatedDate = Convert.ToDateTime(dr["sta_createddate"]);
                        ((StatusModel.ItemModel)Details).CreatedBy = Convert.ToString(dr["sta_createdby"]);
                        ((StatusModel.ItemModel)Details).UpdatedDate = Convert.ToDateTime(dr["sta_updateddate"]);
                        ((StatusModel.ItemModel)Details).UpdatedBy = Convert.ToString(dr["sta_updatedby"]);
                        ((StatusModel.ItemModel)Details).Hidden = (dr["sta_hidden"].ToString().Equals("0") ? false : true);
                        ((StatusModel.ItemModel)Details).Deleted = (dr["sta_deleted"].ToString().Equals("0") ? false : true);
                    }
                    else
                        ((StatusModel.ItemModel)Details).Deleted = true;
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void LoadAll(ref List<Status.Item> _objects)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::LoadAll]";
                try
                {
                    DataTable dt = null;
                    String CacheItem = "LiquidCore.Status";
                    if (HttpContext.Current != null) if (HttpContext.Current != null) dt = (DataTable)HttpContext.Current.Cache[CacheItem];
                    if (dt == null)
                    {
                        StringBuilder sSQL = new StringBuilder();
                        sSQL.Append("SELECT sta_id FROM sta_status WHERE sta_deleted = 0 ORDER BY ");
                        sSQL.Append("lng_id, sta_parentid, sta_order");
                        using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true))
                        {
                            EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                            dt = oDo.GetDataTable(sSQL.ToString());
                            if (oDo.HasError)
                                throw new Exception(oDo.GetError);
                        }
                        CacheData.Insert(CacheItem, dt);
                    }
                    foreach (DataRow dr in dt.Rows)
                    {
                        Status.Item o = new Status.Item(Convert.ToInt32(dr["sta_id"].ToString()));
                        _objects.Add(o);
                    }
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void LoadAll(ref List<Status.Item> _objects, NameValueSet DataFields)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::LoadAll]";
                try
                {
                    String PrimaryKey = String.Empty;
                    String CacheItem = String.Empty;
                    StringBuilder sSQL = new StringBuilder();
                    if (DataFields["Alias"] != null)
                    {
                        PrimaryKey = DataFields["Alias"].ToString();
                        CacheItem = "LiquidCore.Status.PrimaryKey(Alias(" + PrimaryKey + "))";
                        sSQL.Append("SELECT sta_id FROM sta_status WHERE sta_alias = '" + PrimaryKey + "' AND sta_deleted = 0 ORDER BY lng_id, sta_parentid, sta_order");
                    }
                    else
                    {
                        LoadAll(ref _objects);
                        return;
                    }
                    DataTable dt = null;
                    if (HttpContext.Current != null) if (HttpContext.Current != null) dt = (DataTable)HttpContext.Current.Cache[CacheItem];
                    if (dt == null)
                    {
                        using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true))
                        {
                            EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                            dt = oDo.GetDataTable(sSQL.ToString());
                            if (oDo.HasError)
                                throw new Exception(oDo.GetError);
                        }
                        CacheData.Insert(CacheItem, dt);
                    }
                    foreach (DataRow dr in dt.Rows)
                    {
                        Status.Item o = new Status.Item(Convert.ToInt32(dr["ust_id"].ToString()));
                        _objects.Add(o);
                    }
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void Update(StatusModel.ItemModel Details)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::Update]";
                try
                {
                    ((StatusModel.ItemModel)Details).UpdatedDate = DateTime.Now;
                    ((StatusModel.ItemModel)Details).UpdatedBy = Authentication.User.Identity.Name;

                    System.Data.DataSet ds = new System.Data.DataSet();
                    StringBuilder sSQL = new StringBuilder();
                    sSQL.Append("UPDATE sta_status SET ");
                    sSQL.Append("lng_id = " + Details.Language.ToString() + ", ");
                    sSQL.Append("sta_parentid = " + Details.ParentId.ToString() + ", ");
                    sSQL.Append("sta_order = " + Details.Order.ToString() + ", ");
                    sSQL.Append("sta_revision = " + Details.Revision.ToString() + ", ");
                    sSQL.Append("sta_title = '" + Details.Title + "', ");
                    sSQL.Append("sta_alias = '" + Details.Alias + "', ");
                    sSQL.Append("sta_description = '" + Details.Description + "', ");
                    sSQL.Append("sta_createddate = '" + Details.CreatedDate.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "', ");
                    sSQL.Append("sta_createdby = '" + Details.CreatedBy + "', ");
                    sSQL.Append("sta_updateddate = '" + Details.UpdatedDate.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + "', ");
                    sSQL.Append("sta_updatedby = '" + Details.UpdatedBy + "', ");
                    sSQL.Append("sta_hidden = " + (!Details.Hidden ? "0" : "1") + ", ");
                    sSQL.Append("sta_deleted = " + (!Details.Deleted ? "0" : "1") + " ");
                    sSQL.Append("WHERE sta_id = " + Details.Id.ToString());
                    using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, true))
                    {
                        EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                        oDo.ExecuteNonQuery(sSQL.ToString());
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);

                        //EventLog.LogEvent("CALL sort_sta_status();", FUNCTIONNAME, String.Empty);
                        //oDo.ExecuteNonQuery("CALL sort_sta_status();");
                        //if (oDo.HasError)
                        //    throw new Exception(oDo.GetError);

                    }

                    SortAll();
                    ResetThis();
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void Delete(StatusModel.ItemModel Details)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::Delete]";
                try
                {
                    RecursiveDelete(Details.Id);
                    //using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, false))
                    //{
                    //    EventLog.LogEvent("CALL sort_sta_status();", FUNCTIONNAME, String.Empty);
                    //    oDo.ExecuteNonQuery("CALL sort_sta_status();");
                    //    if (oDo.HasError)
                    //        throw new Exception(oDo.GetError);
                    //}
                    SortAll();
                    ResetThis();
                    Details = null;
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void RecursiveDelete(Int32 Id)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::RecursiveDelete]";
                try
                {
                    if (Id.Equals(0))
                        return;

                    using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, true))
                    {
                        String sSQL1 = "UPDATE sta_status SET sta_deleted = 1 WHERE sta_id = " + Id.ToString();

                        EventLog.LogEvent(sSQL1, FUNCTIONNAME, String.Empty);
                        System.Diagnostics.Debug.WriteLine(sSQL1);
                        oDo.ExecuteNonQuery(sSQL1);
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);
                    }
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static void ResetThis()
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::ResetThis]";
                try
                {
                    CacheData.Reset("LiquidCore.Status");
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            //[ObsoleteAttribute("SortAll has been deprecated. All sorting take place in stored procedures instead.")]
            public static void SortAll()
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::SortAll]";
                try
                {
                    Int32 Order = (Int32)OrderMinMax.Min;
                    StringBuilder sSQL = new StringBuilder();
                    sSQL.Append("SELECT sta_id FROM sta_status WHERE sta_deleted = 0 ORDER BY lng_id, sta_parentid, sta_order");
                    using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, false))
                    {
                        EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                        DataTable dt = oDo.GetDataTable(sSQL.ToString());
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);
                        StringBuilder sSQL2 = new StringBuilder();
                        foreach (DataRow dr in dt.Rows)
                        {
                            sSQL2.AppendLine("UPDATE sta_status SET sta_order = " + Order.ToString() + " WHERE sta_id = " + dr["sta_id"].ToString() + ";");
                            Order = Order + (Int32)OrderMinMax.Step;
                            if (sSQL2.Length > DataBlockSize)
                            {
                                EventLog.LogEvent(sSQL2.ToString(), FUNCTIONNAME, String.Empty);
                                if (!(oDo.ExecuteNonQuery(sSQL2.ToString()) > 0))
                                    throw new Exception(oDo.GetError);
                                sSQL2 = new StringBuilder();
                            }
                        }
                        if (sSQL2.Length > 0)
                        {
                            EventLog.LogEvent(sSQL2.ToString(), FUNCTIONNAME, String.Empty);
                            if (!(oDo.ExecuteNonQuery(sSQL2.ToString()) > 0))
                                throw new Exception(oDo.GetError);
                        }
                        ResetThis();
                    }
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                }
            }
            public static Boolean FindChildren(StatusModel.ItemModel Details)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::FindChildren]";
                DataTable dt = null;
                try
                {
                    String CacheItem = "LiquidCore.Status.Status.Children(" + Details.Id.ToString() + ")";
                    if (HttpContext.Current != null) if (HttpContext.Current != null) dt = (DataTable)HttpContext.Current.Cache[CacheItem];
                    if (dt == null)
                    {
                        StringBuilder sSQL = new StringBuilder();
                        sSQL.Append("SELECT sta_id FROM sta_status WHERE sta_deleted = 0 AND sta_parentid = " + Details.Id.ToString());
                        using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true))
                        {
                            EventLog.LogEvent(sSQL.ToString(), FUNCTIONNAME, String.Empty);
                            dt = oDo.GetDataTable(sSQL.ToString());
                        }
                        CacheData.Insert(CacheItem, dt);
                    }
                    if (dt.Rows.Count > 0)
                        return true;

                    return false;
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, "");
                    return false;
                }
            }
        }
    }
}

// Error
namespace LiquidCore
{
    using System.IO;
    public class Error
    {
        static bool ShowErrorHistory = Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["Error.ShowErrorHistory"].ToString());
        static bool ErrorRssOn = Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["Error.RssOn"].ToString());
        static bool ErrorMailOn = Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["Error.MailOn"].ToString());
        static String DataSource = System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString();
        static String ConnectionString = System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString();
        static String CacheTimeOutInMinutes = System.Configuration.ConfigurationManager.AppSettings["Data.CacheTimeOutInMinutes"].ToString();
        static String CultureDisplayName = System.Configuration.ConfigurationManager.AppSettings["Data.CultureDisplayName"].ToString();
        static String ShowCacheHistory = System.Configuration.ConfigurationManager.AppSettings["Data.ShowCacheHistory"].ToString();
        static String DataBlockSize = System.Configuration.ConfigurationManager.AppSettings["Data.DataBlockSize"].ToString();
        static String UseRevision = System.Configuration.ConfigurationManager.AppSettings["Data.UseRevision"].ToString();
        static String Path = System.Web.HttpContext.Current.Server.MapPath(".");

        public static void Report(Exception ex, String Function, String Variant)
        {
            if (ShowErrorHistory)
                System.Diagnostics.Debug.WriteLine(ex.ToString() + Function + Variant);
            if (ErrorRssOn)
                CreateRSSLogRow(ex, Function, Variant);
            if (ErrorMailOn)
                EventMail(ex, Function, Variant);
        }
        
        private static void EventMail(Exception ex, String function, String variant)
        {
            try
            {
                if (HttpContext.Current != null)
                {
                    variant +=
                        "<br/><br/>Current.User.Identity.Name: " + LiquidCore.Authentication.User.Identity.Name != null ? LiquidCore.Authentication.User.Identity.Name : "none" +
                        "<br/><br/>Current.User.Identity.IsAuthenticated: " + LiquidCore.Authentication.User.Identity.Authenticated.ToString() +
                        "<br/><br/>Current.User.Identity.AuthenticationType: RXServer.Authentication";
                }
                Generic.SendMail(ConfigurationManager.AppSettings["ErrorMailServer"].ToString(),
                    ConfigurationManager.AppSettings["ErrorMailPort"].ToString(),
                    ConfigurationManager.AppSettings["ErrorMailSender"].ToString(),
                    ConfigurationManager.AppSettings["ErrorMailAddress"].ToString(),
                    "An Exception was thrown in the application RXServer...",
                    "In function/routine: " + function + "<br/><br/>" + ex.Message +
                    "<br/><br/>Version: " + System.Reflection.Assembly.GetExecutingAssembly().CodeBase + "<br/>Caller: " + System.Reflection.Assembly.GetCallingAssembly().CodeBase + "<br/>Stack trace: " + ex.StackTrace +
                    "<br/><br/>Variant Data: " + variant +
                    "<br/><br/>RXServer.Data.CacheTimeOut: " + CacheTimeOutInMinutes +
                    "<br/><br/>RXServer.Data.ConnectionString: " + ConnectionString +
                    "<br/><br/>RXServer.Data.DataSource: " + DataSource +
                    "<br/><br/>RXServer.Data.DataBlockSize: " + DataBlockSize +
                    "<br/><br/>RXServer.Data.UseRevision: " + UseRevision +
                    "<br/><br/>RXServer.Data.Path: " + Path +
                    "<br/><br/>RXServer.Data.CultureInfo: " + CultureDisplayName,
                    null);
            }
            catch (Exception)
            {
            }
        }

        private static void CreateRSSLogRow(Exception ex, String Function, String Variant)
        {
            StreamWriter sw = File.AppendText(Path + @"\errorlog1.tmp");
            sw.WriteLine("<Table1>");
            sw.WriteLine("<Function>" + Function + "</Function>");
            sw.WriteLine("<Source>" + ex.Source + "</Source>");
            sw.WriteLine("<Message>" + ex.Message + "</Message>");
            sw.WriteLine("<StackTrace>" + ex.StackTrace + "</StackTrace>");
            sw.WriteLine("<TargetSite>" + ex.TargetSite.ToString() + "</TargetSite>");
            sw.WriteLine("<Version>" + System.Reflection.Assembly.GetExecutingAssembly().CodeBase + "</Version>");
            sw.WriteLine("<Caller>" + System.Reflection.Assembly.GetCallingAssembly().CodeBase + "</Caller>");
            sw.WriteLine("<Variant>" + Variant + "</Variant>");
            sw.WriteLine("<DataSource>" + DataSource + "</DataSource>");
            sw.WriteLine("<ConnectionString>" + ConnectionString + "</ConnectionString>");
            sw.WriteLine("<CacheTimeOutInMinutes>" + CacheTimeOutInMinutes + "</CacheTimeOutInMinutes>");
            sw.WriteLine("<CultureDisplayName>" + CultureDisplayName + "</CultureDisplayName>");
            sw.WriteLine("<ShowCacheHistory>" + ShowCacheHistory + "</ShowCacheHistory>");
            sw.WriteLine("<DataBlockSize>" + DataBlockSize + "</DataBlockSize>");
            sw.WriteLine("<UseRevision>" + UseRevision + "</UseRevision>");
            sw.WriteLine("<IsAuthenticated>" + LiquidCore.Authentication.User.Identity.Authenticated.ToString() + "</IsAuthenticated>");
            sw.WriteLine("<AuthenticationType>RXServer.Authentication</AuthenticationType>");
            sw.WriteLine("<Date>" + DateTime.Now.ToLongTimeString() + "</Date>");
            sw.WriteLine("</Table1>");
            sw.Close();
        }

        public static void CreateRSSLogFile()
        {
            StreamWriter sw = File.AppendText(Path + @"\errorlog2.tmp");
            sw.WriteLine("<DataSet1>");
            sw.WriteLine(File.ReadAllText(Path + @"\errorlog1.tmp"));
            sw.WriteLine("</DataSet1>");
            sw.Close();

            DataSet ds = new DataSet();
            ds.ReadXml(Path + @"\errorlog2.tmp");

            RssBuilder.RssConfigurator configurator = new RssBuilder.RssConfigurator();
            configurator.Title = "LiquidCore Error Log";
            configurator.Link = "http://www.liquidcore.se";
            configurator.ImageUrl = "http://www.liquidcore.se/image.gif";

            configurator.ExpressionDate = "[Date]";
            configurator.ExpressionDescription = "StackTrace is: <b>[StackTrace]</b>";
            configurator.ExpressionLink = "";
            configurator.ExpressionTitle = "[Message]";
            System.Xml.XmlDocument xmlDoc = RssBuilder.RssBuilder.BuildXML(ds, configurator);

            sw = File.AppendText(Path + @"\errorlog" + "_" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + " " + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + ".xml");
            sw.WriteLine(xmlDoc.OuterXml);
            sw.Close();
            File.Delete(Path + @"\errorlog1.tmp");
            File.Delete(Path + @"\errorlog2.tmp");
        }
    }
}

// Generic
namespace LiquidCore
{
    using System.Net.Mail;
    using System.IO;
    public class Generic
    {
        string CLASSNAME = "[Namespace::LiquidCore][Class::Generic]";

        public static Int32[] GrowArray(Int32[] pOldArray, Int32 pNewSize)
        {
            Int32 counter = 0;
            Int32[] newArray = null;

            if (pOldArray != null)
            {
                if (pNewSize <= pOldArray.Length)
                {
                    newArray = pOldArray;
                }
                else
                {
                    newArray = new Int32[pNewSize];
                    foreach (Int32 item in pOldArray)
                    {
                        newArray[counter] = item;
                        counter++;
                    }
                }
            }
            return newArray;
        }

        public static void SendMail(string Server, string Port, string From, string To, string Subject, string Body, ArrayList Attachments)
        {
            string FUNCTIONNAME = "[Class::Generic::Function::SendMail]";
            try
            {
                MailMessage mailMsg = new MailMessage();

                mailMsg.From = new MailAddress(From);

				foreach (var address in To.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
				{
					mailMsg.To.Add(address);
				}

                mailMsg.Subject = Subject;
                mailMsg.BodyEncoding = System.Text.Encoding.GetEncoding("utf-8");

                System.Net.Mail.AlternateView plainView = System.Net.Mail.AlternateView.CreateAlternateViewFromString
                (System.Text.RegularExpressions.Regex.Replace(Body, @"<(.|\n)*?>", string.Empty), null, "text/plain");
                System.Net.Mail.AlternateView htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(Body, null, "text/html");

                mailMsg.AlternateViews.Add(plainView);
                mailMsg.AlternateViews.Add(htmlView);

                if (Attachments != null)
                {
                    foreach (Attachment a in Attachments)
                    {
                        mailMsg.Attachments.Add(a);
                    }
                }

                SmtpClient client = new SmtpClient(Server, Convert.ToInt32(Port));
                //client.EnableSsl = true;
                client.Send(mailMsg);
            }
            catch (Exception ex)
            {
                Error.Report(ex, FUNCTIONNAME, String.Empty);
            }
        }

        public static void SendMail(string Server, string Port, string From, string To, string Subject, string Body, ArrayList Attachments, MailAddressCollection Cc)
        {
            string FUNCTIONNAME = "[Class::Generic::Function::SendMail]";
            try
            {
                MailMessage mailMsg = new MailMessage();

                mailMsg.From = new MailAddress(From);

				foreach (var address in To.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
				{
					mailMsg.To.Add(address);
				}

                foreach (MailAddress m in Cc)
                    mailMsg.CC.Add(m);
                mailMsg.Subject = Subject;
                mailMsg.BodyEncoding = System.Text.Encoding.GetEncoding("utf-8");

                System.Net.Mail.AlternateView plainView = System.Net.Mail.AlternateView.CreateAlternateViewFromString
                (System.Text.RegularExpressions.Regex.Replace(Body, @"<(.|\n)*?>", string.Empty), null, "text/plain");
                System.Net.Mail.AlternateView htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(Body, null, "text/html");

                mailMsg.AlternateViews.Add(plainView);
                mailMsg.AlternateViews.Add(htmlView);

                if (Attachments != null)
                {
                    foreach (Attachment a in Attachments)
                    {
                        mailMsg.Attachments.Add(a);
                    }
                }

                SmtpClient client = new SmtpClient(Server, Convert.ToInt32(Port));
                //client.EnableSsl = true;
                client.Send(mailMsg);
            }
            catch (Exception ex)
            {
                Error.Report(ex, FUNCTIONNAME, String.Empty);
            }
        }

        public static void SendMail(string Server, string Port, string From, string To, string Subject, string Body, ArrayList Attachments, string Username, string Password)
        {
            string FUNCTIONNAME = "[Class::Generic::Function::SendMail]";
            try
            {
                MailMessage mailMsg = new MailMessage();

                mailMsg.From = new MailAddress(From);

				foreach (var address in To.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
				{
					mailMsg.To.Add(address);
				}

                mailMsg.Subject = Subject;
                mailMsg.BodyEncoding = System.Text.Encoding.GetEncoding("utf-8");

                System.Net.Mail.AlternateView plainView = System.Net.Mail.AlternateView.CreateAlternateViewFromString
                (System.Text.RegularExpressions.Regex.Replace(Body, @"<(.|\n)*?>", string.Empty), null, "text/plain");
                System.Net.Mail.AlternateView htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(Body, null, "text/html");

                mailMsg.AlternateViews.Add(plainView);
                mailMsg.AlternateViews.Add(htmlView);

                if (Attachments != null)
                {
                    foreach (Attachment a in Attachments)
                    {
                        mailMsg.Attachments.Add(a);
                    }
                }

                SmtpClient client = new SmtpClient(Server, Convert.ToInt32(Port));
                client.Credentials = new System.Net.NetworkCredential(Username, Password);  
                client.Send(mailMsg);
            }
            catch (Exception ex)
            {
                Error.Report(ex, FUNCTIONNAME, String.Empty);
            }
        }

        public static void SendMail(string Server, string Port, string From, string To, string Subject, string Body, ArrayList Attachments, MailAddressCollection Cc, string Username, string Password)
        {
            string FUNCTIONNAME = "[Class::Generic::Function::SendMail]";
            try
            {
                MailMessage mailMsg = new MailMessage();

                mailMsg.From = new MailAddress(From);

				foreach (var address in To.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
				{
					mailMsg.To.Add(address);
				}

                foreach (MailAddress m in Cc)
                    mailMsg.CC.Add(m);
                mailMsg.Subject = Subject;
                mailMsg.BodyEncoding = System.Text.Encoding.GetEncoding("utf-8");

                System.Net.Mail.AlternateView plainView = System.Net.Mail.AlternateView.CreateAlternateViewFromString
                (System.Text.RegularExpressions.Regex.Replace(Body, @"<(.|\n)*?>", string.Empty), null, "text/plain");
                System.Net.Mail.AlternateView htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(Body, null, "text/html");

                mailMsg.AlternateViews.Add(plainView);
                mailMsg.AlternateViews.Add(htmlView);

                if (Attachments != null)
                {
                    foreach (Attachment a in Attachments)
                    {
                        mailMsg.Attachments.Add(a);
                    }
                }

                SmtpClient client = new SmtpClient(Server, Convert.ToInt32(Port));
                client.Credentials = new System.Net.NetworkCredential(Username, Password);  
                client.Send(mailMsg);
            }
            catch (Exception ex)
            {
                Error.Report(ex, FUNCTIONNAME, String.Empty);
            }
        }

    }
    /// <summary>
    /// Klipp in detta i din web.config fil för att få tillgång till functionerna.
    ///     <system.web>
    ///         <httpModules>
    ///             <add name="FriendlyUrl" type="LiquidCore.FriendlyUrl, LiquidCore"/>
    ///         </httpModules>
    ///     </system.web>
    /// </summary>
    public class FriendlyUrl : IHttpModule
    {
        string CLASSNAME = "[Namespace::LiquidCore][Class::FriendlyUrl]";
        public void Init(HttpApplication c)
        {
            c.BeginRequest += new EventHandler(rx_BeginRequest);
            c.EndRequest += new EventHandler(rx_EndRequest);
        }
        void rx_BeginRequest(object sender, EventArgs e)
        {
            string FUNCTIONNAME = CLASSNAME + "[Function::rx_BeginRequest]";
            try
            {
                String currentURL = HttpContext.Current.Request.Path.ToLower();
                String physicalPath = HttpContext.Current.Server.MapPath(currentURL.Substring(currentURL.LastIndexOf("/") + 1));
                //if (physicalPath.EndsWith("\\default.aspx"))
                //    physicalPath = physicalPath.Substring(0, physicalPath.LastIndexOf("\\") + 1);
                
                //if (physicalPath.EndsWith(".pop"))
                //{

                if (!System.IO.File.Exists(physicalPath) &&
                    !physicalPath.Contains(".asmx") &&
                    !physicalPath.EndsWith(".axd") &&
                    !physicalPath.Contains("PagId=") &&
                    !physicalPath.Contains(".") &&
                    !physicalPath.Contains("efault.aspx"))
                {
                //if (!physicalPath.EndsWith(".axd") && 
                //    !physicalPath.Contains("PagId=") &&
                //    !physicalPath.Contains("efault.aspx"))
                //{
                    String processPath = currentURL.Substring(HttpContext.Current.Request.ApplicationPath.Length).TrimStart('/').ToLower();
                    processPath.Replace(@"\", "/");
                    processPath = processPath.EndsWith("/") ? processPath.Substring(0, processPath.Length - 1) : processPath;

                    //processPath = processPath.Substring(0, processPath.Length - ".pop".Length);

                    if (processPath.EndsWith(".aspx"))
                        processPath = processPath.Substring(0, processPath.Length - ".aspx".Length);

                    String[] processParts = processPath.Split('/');  

                    String queryString = HttpContext.Current.Request.ServerVariables["QUERY_STRING"];
                    String defaultPage = "~/Default.aspx?PagId=";

                    //HttpContext.Current.RewritePath("~/Default.aspx");

                    String Id = GetPageIdFromTag(processParts);

                    Id = Id.Equals("0") ? "1" : Id;

                    processPath = defaultPage + Id + (queryString.Length.Equals(0) ? String.Empty : ("&" + queryString));

                    HttpContext.Current.RewritePath(processPath, false);
                    //HttpContext.Current.Response.Redirect(processPath, false);    
                }
            }
            catch (Exception ex)
            {
                Error.Report(ex, FUNCTIONNAME, String.Empty);
            }
        }
        void rx_EndRequest(object sender, EventArgs e)
        {
            
        }
        private String GetPageIdFromTag(String[] Tags)
        {
            Int32 Id = 0;
            if (Tags.Length.Equals(0))
                return Id.ToString();
            else if (Tags.Length.Equals(1))
            {
                foreach (Menu.Item i in new Menu(1, 0))
                {
                    if (i.Alias.ToLower().Equals(Tags[0].ToLower()))
                        return i.Id.ToString();
                }
                return Id.ToString();
            }
            else
            {
                foreach (Menu.Item i in new Menu(1, 0))
                {
                    if (i.Alias.ToLower().Equals(Tags[0].ToLower()))
                    {
                        Id = i.Id;
                        break;
                    }
                }
                for (Int32 i = 1; i < (Tags.GetUpperBound(0) + 1); i++)
                {
                    foreach (Menu.Item j in new Menu(1, Id))
                    {
                        if (j.Alias.ToLower().Equals(Tags[i].ToLower()))
                        {
                            Id = j.Id;
                            break;
                        }
                    }
                }
                return Id.ToString();
            }
        }
        private String GetParentIdToStringFromTag(Int32 SitId, Int32 ParentId, String Tag)
        {
            using (Menu m = new Menu(SitId, ParentId))
            {
                foreach (Menu.Item i in m)
                {
                    if (i.Alias.ToLower().Equals(Tag.ToLower()))
                        return i.Id.ToString();
                }
            }
            return "0";
        }
        private String GetRealValue(String[] processParts)
        {
            //string FUNCTIONNAME = CLASSNAME + "[Function::GetRealValue]";
            //// denna används bara av RXServer.Web.Menus.MenuItem i syfte av RXServer.Web.UrlRewrite
            //String PagId = "1";
            //if (p.IndexOf("/") > 0)
            //    p = p.Substring(p.IndexOf("/") + 1);
            //try
            //{
            //    using (RXServer.Web.Menus.MenuItem m = new RXServer.Web.Menus.MenuItem(p, ConfigurationManager.AppSettings["Data.DataSource"], ConfigurationManager.AppSettings["Data.ConnectionString"]))
            //    {
            //        if (!m.Id.Equals(0))
            //            PagId = m.Id.ToString();
            //    }
            //    return PagId;
            //}
            //catch (Exception ex)
            //{
            //    Error.Report(ex, FUNCTIONNAME, String.Empty);
            //    return PagId;
            //}
            return "";
        }
        public void Dispose() { }
    }
}

// Authentication
namespace LiquidCore
{
    namespace LiquidCore.Authentication
    { 
        public class User
        {
            //public static bool Authenticated = false;
            public static class Identity
            {
				public static int Id
				{
					get
					{
						int Id = 0;
						if (!IsStillAuthenticated())
						{
							return Id;
						}

						if (HttpContext.Current.Session != null)
							if (HttpContext.Current.Session["LiquidCore.Authentication.User.Identity.Id"] != null)
								Int32.TryParse(HttpContext.Current.Session["LiquidCore.Authentication.User.Identity.Id"].ToString(), out Id);
						return Id;
					}
					set
					{
						HttpContext.Current.Session["LiquidCore.Authentication.User.Identity.Id"] = value.ToString();
					}
				}
                public static String Name
                {
                    get
                    {
                        String Name = String.Empty;
						if (!IsStillAuthenticated())
						{
							return Name;
						}

                        if (HttpContext.Current.Session != null)
                            if (HttpContext.Current.Session["LiquidCore.Authentication.User.Identity.Name"] != null)
                                Name = HttpContext.Current.Session["LiquidCore.Authentication.User.Identity.Name"].ToString();
                        return Name;
                    }
                    set
                    {
                        HttpContext.Current.Session["LiquidCore.Authentication.User.Identity.Name"] = value;
                    }
                }
                public static String Password
                {
                    get
                    {
                        String Password = String.Empty;
						if (!IsStillAuthenticated())
						{
							return Password;
						}

                        if (HttpContext.Current.Session != null)
                            if (HttpContext.Current.Session["LiquidCore.Authentication.User.Identity.Password"] != null)
                                Password = HttpContext.Current.Session["LiquidCore.Authentication.User.Identity.Password"].ToString();
                        return Password;
                    }
                    set
                    {
                        HttpContext.Current.Session["LiquidCore.Authentication.User.Identity.Password"] = value;
                    }
                }
                public static String Role
                {
                    get
                    {
                        String Role = String.Empty;
						if (!IsStillAuthenticated())
						{
							return Role;
						}

                        if (HttpContext.Current.Session != null)
                            if (HttpContext.Current.Session["LiquidCore.Authentication.User.Identity.Role"] != null)
                                Role = HttpContext.Current.Session["LiquidCore.Authentication.User.Identity.Role"].ToString();
                        return Role;
                    }
                    set
                    {
                        HttpContext.Current.Session["LiquidCore.Authentication.User.Identity.Role"] = value;
                    }
                }
                public static Boolean Authenticated
                {
                    get
                    {
                        Int32 Authenticated = 0;
						if (!IsStillAuthenticated())
						{
							return Authenticated.Equals(0) ? false : true;
						}

                        if (HttpContext.Current.Session != null)
                            if (HttpContext.Current.Session["LiquidCore.Authentication.User.Identity.Authenticated"] != null)
                                Int32.TryParse(HttpContext.Current.Session["LiquidCore.Authentication.User.Identity.Authenticated"].ToString(), out Authenticated);
                        return Authenticated.Equals(0) ? false : true;
                    }
                    set
                    {
                        HttpContext.Current.Session["LiquidCore.Authentication.User.Identity.Authenticated"] = value.Equals(true) ? "1" : "0";
                    }
                }
				public static DateTime AuthenticatedTimestamp
				{
					get
					{
						try
						{
							DateTime timeStamp = DateTime.MinValue;
							if (HttpContext.Current.Session != null)
								if (HttpContext.Current.Session["LiquidCore.Authentication.User.Identity.AuthenticatedTimestamp"] != null)
									timeStamp = (DateTime)HttpContext.Current.Session["LiquidCore.Authentication.User.Identity.AuthenticatedTimestamp"];
							return timeStamp;
						}
						catch
						{
							return DateTime.MinValue;
						}
					}
					set
					{
						HttpContext.Current.Session["LiquidCore.Authentication.User.Identity.AuthenticatedTimestamp"] = value;
					}
				}
				public static bool IsStillAuthenticated()
				{
					int sessionTimeout;
					if (!Int32.TryParse(ConfigurationManager.AppSettings["System.LoginSession"], out sessionTimeout))
					{
						sessionTimeout = 120; //Default to 120 m.
					}

					return AuthenticatedTimestamp > DateTime.Now.AddMinutes(-1 * sessionTimeout);
				}
            }
        }
    }
}

// Config
namespace LiquidCore
{
    public class ConfigSettings
    {
        private ConfigSettings() { }

        public static string ReadSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        public static void WriteSetting(string key, string value)
        {
            // load config document for current assembly
            XmlDocument doc = loadConfigDocument();

            // retrieve appSettings node
            XmlNode node = doc.SelectSingleNode("//appSettings");

            if (node == null)
                throw new InvalidOperationException("appSettings section not found in config file.");

            try
            {
                // select the 'add' element that contains the key
                XmlElement elem = (XmlElement)node.SelectSingleNode(string.Format("//add[@key='{0}']", key));

                if (elem != null)
                {
                    // add value for key
                    elem.SetAttribute("value", value);
                }
                else
                {
                    // key was not found so create the 'add' element 
                    // and set it's key/value attributes 
                    elem = doc.CreateElement("add");
                    elem.SetAttribute("key", key);
                    elem.SetAttribute("value", value);
                    node.AppendChild(elem);
                }
                doc.Save(getConfigFilePath());
            }
            catch
            {
                throw;
            }
        }

        public static void RemoveSetting(string key)
        {
            // load config document for current assembly
            XmlDocument doc = loadConfigDocument();

            // retrieve appSettings node
            XmlNode node = doc.SelectSingleNode("//appSettings");

            try
            {
                if (node == null)
                    throw new InvalidOperationException("appSettings section not found in config file.");
                else
                {
                    // remove 'add' element with coresponding key
                    node.RemoveChild(node.SelectSingleNode(string.Format("//add[@key='{0}']", key)));
                    doc.Save(getConfigFilePath());
                }
            }
            catch (NullReferenceException e)
            {
                throw new Exception(string.Format("The key {0} does not exist.", key), e);
            }
        }

        private static XmlDocument loadConfigDocument()
        {
            XmlDocument doc = null;
            try
            {
                doc = new XmlDocument();
                doc.Load(getConfigFilePath());
                return doc;
            }
            catch (System.IO.FileNotFoundException e)
            {
                throw new Exception("No configuration file found.", e);
            }
        }

        private static string getConfigFilePath()
        {
            return HttpContext.Current.Server.MapPath("web.config");   
            //return Assembly.GetExecutingAssembly().Location + ".config";
        }
    }
}



