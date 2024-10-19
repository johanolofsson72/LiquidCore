using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using iConsulting.iCXmlDbClient;

namespace iConsulting.iCDataHandler
{
	public class clsConnection
	{	
		protected string	m_sError;
		protected bool      m_HasError;
		public string	    m_sDataSource;
		public string	    m_sConnectionString;
		bool			    m_bIsConnected;
		IDbConnection	    m_oConnection;

		static string	CLASSNAME = "[Namespace::iConsulting][Service::iCDataHandler][Class::clsConnection]";

		#region Properties
		
		public string GetError
		{
			get
			{
				return this.m_sError;
			}
		}

		public bool HasError
		{
			get
			{
				return this.m_HasError; 
			}
		}

		protected string DataSource
		{
			set 
			{
				this.m_sDataSource = value;
			}
			get 
			{
				return this.m_sDataSource;
			}
		}

		protected string ConnectionString
		{
			set
			{
				this.m_sConnectionString = value;
			}
			get
			{
				return this.m_sConnectionString;
			}
		}

		protected bool IsConnected
		{
			get
			{
				return this.m_bIsConnected; 
			}
		}

		protected bool Connectable
		{
			get
			{
				if ((ValidateDataSource()) && (ValidateConnectionString()))
					return true;
				return false;
			}
		}
		#endregion

		#region Constructors

		protected clsConnection()
		{
			this.m_bIsConnected = false;
		}

		protected clsConnection(string sConnectionString)
		{
			this.m_sConnectionString = sConnectionString;
			this.m_bIsConnected = false;
		}

		protected clsConnection(string sDataSource, string sConnectionString)
		{
			this.m_sDataSource = sDataSource;
			this.m_sConnectionString = sConnectionString;
			this.m_bIsConnected = false;
		}
		
		#endregion

		#region Private Functions

		private void AddErrorData(ref string sError, Exception ex, string sFunction)
		{
			try 
			{
				sError += "#" + ex.GetType().ToString() + "occured in " + sFunction + "\r\nSource: " + ex.Source + "\r\nMessage: " + ex.Message + "\r\nVersion: " + System.Reflection.Assembly.GetExecutingAssembly().CodeBase + "\r\nCaller: " + System.Reflection.Assembly.GetCallingAssembly().CodeBase + "\r\nStack trace: " + ex.StackTrace;
				this.m_HasError = true;
				ErrorHandler(ex, sFunction);
			}
			catch (Exception)
			{
				throw;
			}
		}

		private void ErrorHandler(Exception ex, string sFunction)
		{
			try
			{
				System.Diagnostics.EventLog.WriteEntry(
					"iCDataHandler",
					ex.GetType().ToString() + "occured in " + sFunction + "\r\nSource: " + ex.Source + "\r\nMessage: " + ex.Message + "\r\nVersion: " + System.Reflection.Assembly.GetExecutingAssembly().CodeBase + "\r\nCaller: " + System.Reflection.Assembly.GetCallingAssembly().CodeBase + "\r\nStack trace: " + ex.StackTrace,
					System.Diagnostics.EventLogEntryType.Error
				);
			}
			catch (Exception)
			{
				
			}
		}

		private bool ValidateConnectionString()
		{
			string FUNCTIONNAME = CLASSNAME + "[Function::ValidateConnectionString]";
			try 
			{	        
				if(this.m_sConnectionString.Length > 0)
					return true;
				return false;
			}
			catch (Exception ex)
			{
				AddErrorData(ref this.m_sError, ex, FUNCTIONNAME);
				return false;
			}
		}

		private bool ValidateDataSource()
		{
			string FUNCTIONNAME = CLASSNAME + "[Function::ValidateDataSource]";
			try 
			{	    
				switch(this.m_sDataSource.ToLower())
				{
					case "mssqlserver":
						break;
					case "mysql":
                        break;
                    case "xml":
                        break;
					default: 
						throw new Exception(); 
				}
				return true;

			}
			catch (Exception ex)
			{
				AddErrorData(ref this.m_sError, ex, FUNCTIONNAME);
				return false;
			}
		}

		#endregion

		#region Public Functions
		
		public virtual void Initialize(string sDataSource, string sConnectionString)
		{
			this.m_sDataSource = sDataSource;
			this.m_sConnectionString = sConnectionString;
			this.m_bIsConnected = false;
		}		
	
		public IDbConnection GetConnection()
		{
			string FUNCTIONNAME = CLASSNAME + "[Function::GetConnection]";
			try
			{
				if (Connectable)
				{ 
					switch(DataSource.ToLower())
					{
						case "mssqlserver":
							this.m_oConnection = new SqlConnection(this.ConnectionString);
							this.m_oConnection.Open();
							this.m_bIsConnected = true;
							return this.m_oConnection;
						case "mysql":
							this.m_oConnection = new MySqlConnection(this.ConnectionString);
							this.m_oConnection.Open();
							this.m_bIsConnected = true;
							return this.m_oConnection;
                        case "xml":
                            this.m_oConnection = new XmlDbConnection(this.ConnectionString);
                            this.m_oConnection.Open();
                            this.m_bIsConnected = true;
                            return this.m_oConnection; 
						default:
							return this.m_oConnection;
					}
				}
				else
				{
					this.m_bIsConnected = false;
					throw new Exception();
				}			
			}
			catch (Exception ex)
			{
				AddErrorData(ref this.m_sError, ex, FUNCTIONNAME);
                this.m_bIsConnected = false;
				return this.m_oConnection;
			}
		}

		public void CloseConnection(ref IDbConnection oConnection)
		{
			string FUNCTIONNAME = CLASSNAME + "[Function::CloseConnection]";
			try
			{
				oConnection.Close();
				oConnection = null;
			}
			catch (Exception ex)
			{
				oConnection = null;
				AddErrorData(ref this.m_sError, ex, FUNCTIONNAME);
			}
			
		}

		#endregion
	}
}
