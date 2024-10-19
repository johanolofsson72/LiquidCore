using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using iConsulting.iCXmlDbClient; 
using System.Security.Cryptography;
using System.Threading;
using System.Globalization;

namespace iConsulting.iCDataHandler
{
	public class iCDataObject : clsConnection, IDisposable  
	{
        private Boolean         m_bIsConnected;
		private IDbConnection	m_oConnection;
		private IDbTransaction	m_tTransaction;
        private Boolean         m_bEncrypted;
        private CultureInfo     m_CultureInfo;
        private Boolean         m_useTransaction = false;

        static String CLASSNAME = "[Namespace::iConsulting][Service::iCDataHandler][Class::iCDataObject]";

		#region Constructors

        public iCDataObject(String sDataSource, String sConnectionString, Boolean Encrypted)
			: base(sDataSource, sConnectionString) 
		{
			this.m_bEncrypted = Encrypted;
			this.Initialize(sDataSource, sConnectionString);
            this.m_bIsConnected = false;
            this.m_CultureInfo = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("sv-SE");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("sv-SE");
		}

        public iCDataObject(String sDataSource, String sConnectionString, Boolean Encrypted, Boolean AutoConnect)
			: base(sDataSource, sConnectionString)
		{
			this.m_bEncrypted = Encrypted;
			this.Initialize(sDataSource, sConnectionString);
			this.m_bIsConnected = false;
			if (AutoConnect)
			{
				this.Connect(false);
            }
            this.m_CultureInfo = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("sv-SE");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("sv-SE");
		}

        public iCDataObject(String sDataSource, String sConnectionString, Boolean Encrypted, Boolean AutoConnect, Boolean UseTransaction)
			: base(sDataSource, sConnectionString)
		{
			this.m_bEncrypted = Encrypted;
			this.Initialize(sDataSource, sConnectionString);
			this.m_bIsConnected = false;
			if (AutoConnect)
			{
				this.Connect(UseTransaction);
            }
            this.m_CultureInfo = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("sv-SE");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("sv-SE");
		}


        public iCDataObject(String sDataSource, String sConnectionString, Boolean Encrypted, String CultureInfoString)
            : base(sDataSource, sConnectionString)
        {
            this.m_bEncrypted = Encrypted;
            this.Initialize(sDataSource, sConnectionString);
            this.m_bIsConnected = false;
            this.m_CultureInfo = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = new CultureInfo(CultureInfoString);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(CultureInfoString);  
        }

        public iCDataObject(String sDataSource, String sConnectionString, Boolean Encrypted, Boolean AutoConnect, String CultureInfoString)
            : base(sDataSource, sConnectionString)
        {
            this.m_bEncrypted = Encrypted;
            this.Initialize(sDataSource, sConnectionString);
            this.m_bIsConnected = false;
            if (AutoConnect)
            {
                this.Connect(false);
            }
            this.m_CultureInfo = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = new CultureInfo(CultureInfoString);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(CultureInfoString);
        }

        public iCDataObject(String sDataSource, String sConnectionString, Boolean Encrypted, Boolean AutoConnect, Boolean UseTransaction, String CultureInfoString)
            : base(sDataSource, sConnectionString)
        {
            this.m_bEncrypted = Encrypted;
            this.Initialize(sDataSource, sConnectionString);
            this.m_bIsConnected = false;
            if (AutoConnect)
            {
                this.Connect(UseTransaction);
            }
            this.m_CultureInfo = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = new CultureInfo(CultureInfoString);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(CultureInfoString);
        }

		~iCDataObject()
		{
			xFinalize();
		}

		public void Dispose()
		{
			xFinalize();
			System.GC.SuppressFinalize(this);
		}

		private void xFinalize()
		{
            if (this.m_oConnection != null)
			//if (!(bool)(m_oConnection.State == ConnectionState.Closed))
			{
				Close();
				this.m_oConnection = null;
            }
            Thread.CurrentThread.CurrentCulture = this.m_CultureInfo;
            Thread.CurrentThread.CurrentUICulture = this.m_CultureInfo;
		}


		#endregion

		#region Public Functions

		public bool NewTransaction()
		{
			string FUNCTIONNAME = CLASSNAME + "[Function::NewTransaction]";
			try
			{
				this.m_tTransaction = this.m_oConnection.BeginTransaction(IsolationLevel.ReadCommitted);
				return true;
			}
			catch (Exception ex)
			{
				AddErrorData(ref this.m_sError, ex, FUNCTIONNAME);
				return false;
			}
		}

		public bool EndTransaction()
		{
			string FUNCTIONNAME = CLASSNAME + "[Function::EndTransaction]";
			try
			{
                if (this.m_tTransaction != null)
                {
                    if (!this.m_HasError)
                    {
                        this.m_tTransaction.Commit();
                    }
                    else
                    {
                        this.m_tTransaction.Rollback();
                    }
                }
				return true;
			}
			catch (Exception ex)
			{
				AddErrorData(ref this.m_sError, ex, FUNCTIONNAME);
				return false;
			}
		}

		public void Connect(Boolean UseTransaction)
		{
			string FUNCTIONNAME = CLASSNAME + "[Function::Connect]";
			try
			{
                this.m_useTransaction = true;
				this.m_oConnection = base.GetConnection();
				this.m_bIsConnected = base.IsConnected;
				if (UseTransaction)
					this.NewTransaction();
			}
			catch (Exception ex)
			{
				AddErrorData(ref this.m_sError, ex, FUNCTIONNAME);
			}
		}

		public void Connect()
		{
			string FUNCTIONNAME = CLASSNAME + "[Function::Connect]";
			try
			{

				this.m_oConnection = base.GetConnection();
				this.m_bIsConnected = base.IsConnected;
			}
			catch (Exception ex)
			{
				AddErrorData(ref this.m_sError, ex, FUNCTIONNAME);
			}
		}

		public void Close()
		{
			string FUNCTIONNAME = CLASSNAME + "[Function::Close]";
			try
			{
                if (this.m_useTransaction)
                    EndTransaction();
				base.CloseConnection(ref this.m_oConnection);
			}
			catch (Exception ex)
			{
				AddErrorData(ref this.m_sError, ex, FUNCTIONNAME);
			}
		}

        public DataSet GetDataSet(String sSQL)
		{
			string FUNCTIONNAME = CLASSNAME + "[Function::GetDataSet]";
			try
			{
				DataSet ds = new DataSet();
				if (IsConnected)
				{
					if (m_oConnection is SqlConnection)
                    {
                        SqlCommand oCommand = new SqlCommand(sSQL, (SqlConnection)this.m_oConnection);
                        oCommand.Transaction = (SqlTransaction)m_tTransaction;
                        SqlDataAdapter oAdapter = new SqlDataAdapter(oCommand);
						oAdapter.Fill(ds, "GetDataSet");
					}
					else if (m_oConnection is MySqlConnection)
                    {
                        MySqlCommand oCommand = new MySqlCommand(sSQL, (MySqlConnection)this.m_oConnection);
                        MySqlDataAdapter oAdapter = new MySqlDataAdapter(oCommand);
						oAdapter.Fill(ds, "GetDataSet");
                    }
                    else if (m_oConnection is XmlDbConnection)
                    {
                        XmlDbCommand oCommand = new XmlDbCommand(sSQL, (XmlDbConnection)this.m_oConnection);
                        XmlDbDataAdapter oAdapter = new XmlDbDataAdapter(oCommand);
                        oAdapter.Fill(ds);
                    }
					if (base.HasError)
						throw new Exception();
				}
				return ds;
			}
			catch (Exception ex)
			{
				AddErrorData(ref this.m_sError, ex, FUNCTIONNAME);
				return new DataSet(); 
			}
		}

        public DataSet GetDataSet(String sSQL, Int32 startRecord, Int32 maxRecords, String srcTable)
		{
			string FUNCTIONNAME = CLASSNAME + "[Function::GetDataSet]";
			try
			{
				DataSet ds = new DataSet();
				if (IsConnected)
				{
					if (m_oConnection is SqlConnection)
                    {
                        SqlCommand oCommand = new SqlCommand(sSQL, (SqlConnection)this.m_oConnection);
                        oCommand.Transaction = (SqlTransaction)m_tTransaction;
                        SqlDataAdapter oAdapter = new SqlDataAdapter(oCommand);
						oAdapter.Fill(ds, startRecord, maxRecords, srcTable);
					}
					else if (m_oConnection is MySqlConnection)
                    {
                        MySqlCommand oCommand = new MySqlCommand(sSQL, (MySqlConnection)this.m_oConnection);
                        MySqlDataAdapter oAdapter = new MySqlDataAdapter(oCommand);
						oAdapter.Fill(ds, startRecord, maxRecords, srcTable);
                    }
                    else if (m_oConnection is XmlDbConnection)
                    {
                        XmlDbCommand oCommand = new XmlDbCommand(sSQL, (XmlDbConnection)this.m_oConnection);
                        XmlDbDataAdapter oAdapter = new XmlDbDataAdapter(oCommand);
                        oAdapter.Fill(ds);
                    }
					if (base.HasError)
						throw new Exception();
				}
				return ds;
			}
			catch (Exception ex)
			{
				AddErrorData(ref this.m_sError, ex, FUNCTIONNAME);
				return new DataSet();
			}
		}

        public DataTable GetDataTable(String sSQL)
		{
			string FUNCTIONNAME = CLASSNAME + "[Function::GetDataTable]";
			try
			{
				DataTable dt = new DataTable();
				if (IsConnected)
				{
					if (m_oConnection is SqlConnection)
                    {
                        SqlCommand oCommand = new SqlCommand(sSQL, (SqlConnection)this.m_oConnection);
                        oCommand.Transaction = (SqlTransaction)m_tTransaction;
                        SqlDataAdapter oAdapter = new SqlDataAdapter(oCommand);
						oAdapter.Fill(dt);
					}
					else if (m_oConnection is MySqlConnection)
                    {
                        MySqlCommand oCommand = new MySqlCommand(sSQL, (MySqlConnection)this.m_oConnection);
                        MySqlDataAdapter oAdapter = new MySqlDataAdapter(oCommand);
						oAdapter.Fill(dt);
                    }
                    else if (m_oConnection is XmlDbConnection)
                    {
                        XmlDbCommand oCommand = new XmlDbCommand(sSQL, (XmlDbConnection)this.m_oConnection);
                        XmlDbDataAdapter oAdapter = new XmlDbDataAdapter(oCommand);
                        oAdapter.Fill(dt);
                    }
					if (base.HasError)
						throw new Exception();
				}
				return dt;
			}
			catch (Exception ex)
			{
				AddErrorData(ref this.m_sError, ex, FUNCTIONNAME);
				return new DataTable();
			}
		}

        public IDataReader ExecuteReader(String sSQL)
		{
			string FUNCTIONNAME = CLASSNAME + "[Function::ExecuteReader]";
			try
			{
				if (IsConnected)
				{
					if (m_oConnection is SqlConnection)
					{
                        SqlCommand oCommand = new SqlCommand(sSQL, (SqlConnection)this.m_oConnection);
                        oCommand.Transaction = (SqlTransaction)m_tTransaction;
						return oCommand.ExecuteReader();
					}
					else if (m_oConnection is MySqlConnection)
					{
						MySqlCommand oCommand = new MySqlCommand(sSQL, (MySqlConnection)this.m_oConnection);
						return oCommand.ExecuteReader();
                    }
                    else if (m_oConnection is XmlDbConnection)
                    {
                        XmlDbCommand oCommand = new XmlDbCommand(sSQL, (XmlDbConnection)this.m_oConnection);
                        return oCommand.ExecuteReader();
                    }
					else
					{
						return null;
					}
				}
				else
				{
					return null;
				}
			}
			catch (Exception ex)
			{
				AddErrorData(ref this.m_sError, ex, FUNCTIONNAME);
				return null;
			}
		}

        public int ExecuteNonQuery(String sSQL)
		{
			string FUNCTIONNAME = CLASSNAME + "[Function::ExecuteNonQuery]";
			try
			{
				if (IsConnected)
				{
					if (m_oConnection is SqlConnection)
					{
						SqlCommand oCommand = new SqlCommand(sSQL, (SqlConnection)this.m_oConnection);
                        oCommand.Transaction = (SqlTransaction)m_tTransaction;
						return oCommand.ExecuteNonQuery();
					}
					else if (m_oConnection is MySqlConnection)
					{
						MySqlCommand oCommand = new MySqlCommand(sSQL, (MySqlConnection)this.m_oConnection);
						return oCommand.ExecuteNonQuery();
                    }
                    else if (m_oConnection is XmlDbConnection)
                    {
                        XmlDbCommand oCommand = new XmlDbCommand(sSQL, (XmlDbConnection)this.m_oConnection);
                        return oCommand.ExecuteNonQuery();
                    }
					else
					{
						return 0;
					}
				}
				else
				{
					return 0;
				}
			}
			catch (Exception ex)
			{
				AddErrorData(ref this.m_sError, ex, FUNCTIONNAME);
				return 0;
			}
		}

        public object ExecuteScalar(String sSQL)
		{
			string FUNCTIONNAME = CLASSNAME + "[Function::ExecuteNonQuery]";
			try
			{
				if (IsConnected)
				{
					if (m_oConnection is SqlConnection)
					{
                        SqlCommand oCommand = new SqlCommand(sSQL, (SqlConnection)this.m_oConnection);
                        oCommand.Transaction = (SqlTransaction)m_tTransaction;
						return oCommand.ExecuteScalar();
					}
					else if (m_oConnection is MySqlConnection)
					{
						MySqlCommand oCommand = new MySqlCommand(sSQL, (MySqlConnection)this.m_oConnection);
						return oCommand.ExecuteScalar();
                    }
                    else if (m_oConnection is XmlDbConnection)
                    {
                        XmlDbCommand oCommand = new XmlDbCommand(sSQL, (XmlDbConnection)this.m_oConnection);
                        return oCommand.ExecuteScalar();
                    }
					else
					{
						return null;
					}
				}
				else
				{
					return null;
				}
			}
			catch (Exception ex)
			{
				AddErrorData(ref this.m_sError, ex, FUNCTIONNAME);
				return null;
			}
		}
	
		#endregion

		#region Private Functions

		public override void Initialize(string sDataSource, string sConnectionString)
		{
			if (this.m_bEncrypted)
			{
				clsCrypto oCrypt = new clsCrypto();
				this.m_sDataSource = oCrypt.Decrypt(sDataSource);
				this.m_sConnectionString = oCrypt.Decrypt(sConnectionString);
			}
			else
			{
				this.m_sDataSource = sDataSource;
				this.m_sConnectionString = sConnectionString;
			}
		}

		private void AddErrorData(ref string sError, Exception ex, string sFunction)
		{
			try
			{
				sError += "#" + ex.GetType().ToString() + "occured in " + sFunction + "\r\nSource: " + ex.Source + "\r\nMessage: " + ex.Message + "\r\nVersion: " + System.Reflection.Assembly.GetExecutingAssembly().CodeBase + "\r\nCaller: " + System.Reflection.Assembly.GetCallingAssembly().CodeBase + "\r\nStack trace: " + ex.StackTrace;
				ErrorHandler(ex, sFunction);
				this.m_HasError = true;
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

		#endregion

	}
}
