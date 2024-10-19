using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using LiquidCore;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Net.Mail;
using System.IO;
using System.Xml;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.Drawing;
using System.Drawing.Imaging;

// RXServer.Web
namespace RXServer
{
    namespace Web
    {
		#region public class AliasFilter
		/// <summary>
		/// void Application_Start(object sender, EventArgs e) 
		/// {
		///     RXServer.Web.AliasFilter.Build();
		/// }
		/// RXServer.Web.AliasFilter.Source <-- hashtable
		/// </summary>
		public static class AliasFilter
		{
			static string CLASSNAME = "[Namespace::RXServer::Web][Class::AliasFilter]";
			public static Hashtable Source = new Hashtable();
			private static string Loop(String pre, Int32 sId, Int32 pId, TextWriter file)
			{
				String topSiteFilter = "";
				foreach (LiquidCore.Page p in new LiquidCore.Pages(sId, pId))
				{
					if (!Source.ContainsKey(p.Alias))
					{
						topSiteFilter += Loop(pre + "/" + p.Alias, sId, p.Id, file);
						Source.Add(pre + "/" + p.Alias.ToLower(), p.Id);
						file.WriteLine(pre + "/" + p.Alias + "\t" + p.Id.ToString());
						topSiteFilter += pre + "/" + p.Alias + "/|";
					}
				}
				return topSiteFilter;
			}
			public static void Build()
			{
				string FUNCTIONNAME = CLASSNAME + "[Function::Build]";
				try
				{
					String topSiteFilter = "";
					Source = new Hashtable();

					String siteMapFile = @"~/mapfile.txt";
					String mapFile = HttpContext.Current.Server.MapPath(siteMapFile);

					FileStream stream = new FileStream(mapFile, FileMode.Create);
					TextWriter file = new StreamWriter(stream);

					foreach (LiquidCore.Site s in new LiquidCore.Sites())
					{
						foreach (LiquidCore.Page p in new LiquidCore.Pages(s.Id, 0))
						{
							if (!Source.ContainsKey(p.Alias))
							{
								topSiteFilter += Loop(p.Alias, s.Id, p.Id, file);
								Source.Add(p.Alias.ToLower(), p.Id);
								file.WriteLine(p.Alias + "\t" + p.Id.ToString());
								topSiteFilter += p.Alias + "/|";
							}
						}
					}

					file.Close();

					topSiteFilter = topSiteFilter.Substring(0, topSiteFilter.Length - 1); //Remove trailing slash

					siteMapFile = @"~/.htaccess";
					mapFile = HttpContext.Current.Server.MapPath(siteMapFile);
                    if(File.Exists(mapFile))
                    {
                        File.Delete(mapFile);
                    }

					stream = new FileStream(mapFile, FileMode.Create);
					file = new StreamWriter(stream);




					file.WriteLine("RewriteEngine on");
					file.WriteLine("RewriteMap mapfile txt:mapfile.txt");
					file.WriteLine("RewriteRule (rxlogin|rxlogin/)$ /rxlogin/default.aspx [R=301,L]");
					file.WriteLine("RewriteRule ^[^.]+$ /Default.aspx?pagId=${mapfile:$0} [QSA]");
					file.WriteLine("RewriteRule (" + topSiteFilter + ")([A-Za-z0-9_\\-]+\\/)(.+\\..+) $2$3");

					file.Close();
				}
				catch (Exception ex)
				{
					Error.Report(ex, FUNCTIONNAME, String.Empty);
				}
			}
		}
		#endregion public class AliasFilter

        #region public class CultureInfo
        public static class CultureInfo
        {
            static string CLASSNAME = "[Namespace::RXServer::Web][Class::CultureInfo]";
            //public static void SetCultureInfo()
            //{
            //    string FUNCTIONNAME = CLASSNAME + "[Function::SetCultureInfo]";
            //    try
            //    {
            //        using (LiquidCore.Site s = new LiquidCore.Site(CurrentValues.SitId, RXServer.Data.DataSource, RXServer.Data.ConnectionString))
            //        {
            //            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(s.Language.ToString());
            //            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(s.Language.ToString());
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        Error.Report(ex, FUNCTIONNAME, String.Empty);
            //    }
            //}
        }
        #endregion public class CultureInfo

        #region public class CurrentValues
        public static class CurrentValues
        {
            public static Int32 SitId
            {
                get
                {
                    Int32 x = 0;
                    if (HttpContext.Current.Session != null)
                        if (HttpContext.Current.Session["RXServer.Web.CurrentValues.SitId"] != null)
                            Int32.TryParse(HttpContext.Current.Session["RXServer.Web.CurrentValues.SitId"].ToString(), out x);
                    return x;
                }
                set
                {
                    HttpContext.Current.Session["RXServer.Web.CurrentValues.SitId"] = value;
                }
            }
            public static Int32 PagId
            {
                get
                {
                    Int32 x = 0;
                    if (HttpContext.Current.Session != null)
                        if (HttpContext.Current.Session["RXServer.Web.CurrentValues.PagId"] != null)
                            Int32.TryParse(HttpContext.Current.Session["RXServer.Web.CurrentValues.PagId"].ToString(), out x);
                    return x;
                }
                set
                {
                    HttpContext.Current.Session["RXServer.Web.CurrentValues.PagId"] = value;
                }
            }
            public static String Username
            {
                get
                {
                    String x = "";
                    if (HttpContext.Current.Session != null)
                        if (HttpContext.Current.Session["RXServer.Web.CurrentValues.Username"] != null)
                            x = HttpContext.Current.Session["RXServer.Web.CurrentValues.Username"].ToString();
                    return x;
                }
                set
                {
                    HttpContext.Current.Session["RXServer.Web.CurrentValues.Username"] = value;
                }
            }
            public static String Password
            {
                get
                {
                    String x = "";
                    if (HttpContext.Current.Session != null)
                        if (HttpContext.Current.Session["RXServer.Web.CurrentValues.Password"] != null)
                            x = HttpContext.Current.Session["RXServer.Web.CurrentValues.Password"].ToString();
                    return x;
                }
                set
                {
                    HttpContext.Current.Session["RXServer.Web.CurrentValues.Password"] = value;
                }
            }


        }
        #endregion public class CurrentValues

        #region public class SelectedPages
        public static class SelectedPages
        {
            static string CLASSNAME = "[Namespace::RXServer::Web][Class::SelectedPages]";
            public static Int32 Level1
            {
                get
                {
                    Int32 x = 0;
                    String y = "RXServer.Web.SelectedPages.Level1_On_SitId_" + RXServer.Web.CurrentValues.SitId.ToString();
                    if (HttpContext.Current.Session != null)
                        if (HttpContext.Current.Session[y] != null)
                            Int32.TryParse(HttpContext.Current.Session[y].ToString(), out x);
                    return x;
                }
                set
                {
                    HttpContext.Current.Session["RXServer.Web.SelectedPages.Level1_On_SitId_" + RXServer.Web.CurrentValues.SitId.ToString()] = value;
                }
            }
            public static Int32 Level2
            {
                get
                {
                    Int32 x = 0;
                    String y = "RXServer.Web.SelectedPages.Level2_On_SitId_" + RXServer.Web.CurrentValues.SitId.ToString();
                    if (HttpContext.Current.Session != null)
                        if (HttpContext.Current.Session[y] != null)
                            Int32.TryParse(HttpContext.Current.Session[y].ToString(), out x);
                    return x;
                }
                set
                {
                    HttpContext.Current.Session["RXServer.Web.SelectedPages.Level2_On_SitId_" + RXServer.Web.CurrentValues.SitId.ToString()] = value;
                }
            }
            public static Int32 Level3
            {
                get
                {
                    Int32 x = 0;
                    String y = "RXServer.Web.SelectedPages.Level3_On_SitId_" + RXServer.Web.CurrentValues.SitId.ToString();
                    if (HttpContext.Current.Session != null)
                        if (HttpContext.Current.Session[y] != null)
                            Int32.TryParse(HttpContext.Current.Session[y].ToString(), out x);
                    return x;
                }
                set
                {
                    HttpContext.Current.Session["RXServer.Web.SelectedPages.Level3_On_SitId_" + RXServer.Web.CurrentValues.SitId.ToString()] = value;
                }
            }
            public static Int32 Level4
            {
                get
                {
                    Int32 x = 0;
                    String y = "RXServer.Web.SelectedPages.Level4_On_SitId_" + RXServer.Web.CurrentValues.SitId.ToString();
                    if (HttpContext.Current.Session != null)
                        if (HttpContext.Current.Session[y] != null)
                            Int32.TryParse(HttpContext.Current.Session[y].ToString(), out x);
                    return x;
                }
                set
                {
                    HttpContext.Current.Session["RXServer.Web.SelectedPages.Level4_On_SitId_" + RXServer.Web.CurrentValues.SitId.ToString()] = value;
                }
            }
            public static Int32 Level5
            {
                get
                {
                    Int32 x = 0;
                    String y = "RXServer.Web.SelectedPages.Level5_On_SitId_" + RXServer.Web.CurrentValues.SitId.ToString();
                    if (HttpContext.Current.Session != null)
                        if (HttpContext.Current.Session[y] != null)
                            Int32.TryParse(HttpContext.Current.Session[y].ToString(), out x);
                    return x;
                }
                set
                {
                    HttpContext.Current.Session["RXServer.Web.SelectedPages.Level5_On_SitId_" + RXServer.Web.CurrentValues.SitId.ToString()] = value;
                }
            }
            public static Int32 Level6
            {
                get
                {
                    Int32 x = 0;
                    String y = "RXServer.Web.SelectedPages.Level6_On_SitId_" + RXServer.Web.CurrentValues.SitId.ToString();
                    if (HttpContext.Current.Session != null)
                        if (HttpContext.Current.Session[y] != null)
                            Int32.TryParse(HttpContext.Current.Session[y].ToString(), out x);
                    return x;
                }
                set
                {
                    HttpContext.Current.Session["RXServer.Web.SelectedPages.Level6_On_SitId_" + RXServer.Web.CurrentValues.SitId.ToString()] = value;
                }
            }
            public static Int32 Level7
            {
                get
                {
                    Int32 x = 0;
                    String y = "RXServer.Web.SelectedPages.Level7_On_SitId_" + RXServer.Web.CurrentValues.SitId.ToString();
                    if (HttpContext.Current.Session != null)
                        if (HttpContext.Current.Session[y] != null)
                            Int32.TryParse(HttpContext.Current.Session[y].ToString(), out x);
                    return x;
                }
                set
                {
                    HttpContext.Current.Session["RXServer.Web.SelectedPages.Level7_On_SitId_" + RXServer.Web.CurrentValues.SitId.ToString()] = value;
                }
            }
            public static Int32 Level8
            {
                get
                {
                    Int32 x = 0;
                    String y = "RXServer.Web.SelectedPages.Level8_On_SitId_" + RXServer.Web.CurrentValues.SitId.ToString();
                    if (HttpContext.Current.Session != null)
                        if (HttpContext.Current.Session[y] != null)
                            Int32.TryParse(HttpContext.Current.Session[y].ToString(), out x);
                    return x;
                }
                set
                {
                    HttpContext.Current.Session["RXServer.Web.SelectedPages.Level8_On_SitId_" + RXServer.Web.CurrentValues.SitId.ToString()] = value;
                }
            }
            public static Int32 Level9
            {
                get
                {
                    Int32 x = 0;
                    String y = "RXServer.Web.SelectedPages.Level9_On_SitId_" + RXServer.Web.CurrentValues.SitId.ToString();
                    if (HttpContext.Current.Session != null)
                        if (HttpContext.Current.Session[y] != null)
                            Int32.TryParse(HttpContext.Current.Session[y].ToString(), out x);
                    return x;
                }
                set
                {
                    HttpContext.Current.Session["RXServer.Web.SelectedPages.Level9_On_SitId_" + RXServer.Web.CurrentValues.SitId.ToString()] = value;
                }
            }
            public static String Level1Name
            {
                get
                {
                    String x = String.Empty;
                    String y = "RXServer.Web.SelectedPages.Level1Name_On_SitId_" + RXServer.Web.CurrentValues.SitId.ToString();
                    if (HttpContext.Current.Session != null)
                        if (HttpContext.Current.Session[y] != null)
                            x = HttpContext.Current.Session[y].ToString();
                    return x;
                }
                set
                {
                    HttpContext.Current.Session["RXServer.Web.SelectedPages.Level1Name_On_SitId_" + RXServer.Web.CurrentValues.SitId.ToString()] = value;
                }
            }
            public static String Level2Name
            {
                get
                {
                    String x = String.Empty;
                    String y = "RXServer.Web.SelectedPages.Level2Name_On_SitId_" + RXServer.Web.CurrentValues.SitId.ToString();
                    if (HttpContext.Current.Session != null)
                        if (HttpContext.Current.Session[y] != null)
                            x = HttpContext.Current.Session[y].ToString();
                    return x;
                }
                set
                {
                    HttpContext.Current.Session["RXServer.Web.SelectedPages.Level2Name_On_SitId_" + RXServer.Web.CurrentValues.SitId.ToString()] = value;
                }
            }
            public static String Level3Name
            {
                get
                {
                    String x = String.Empty;
                    String y = "RXServer.Web.SelectedPages.Level3Name_On_SitId_" + RXServer.Web.CurrentValues.SitId.ToString();
                    if (HttpContext.Current.Session != null)
                        if (HttpContext.Current.Session[y] != null)
                            x = HttpContext.Current.Session[y].ToString();
                    return x;
                }
                set
                {
                    HttpContext.Current.Session["RXServer.Web.SelectedPages.Level3Name_On_SitId_" + RXServer.Web.CurrentValues.SitId.ToString()] = value;
                }
            }
            public static String Level4Name
            {
                get
                {
                    String x = String.Empty;
                    String y = "RXServer.Web.SelectedPages.Level4Name_On_SitId_" + RXServer.Web.CurrentValues.SitId.ToString();
                    if (HttpContext.Current.Session != null)
                        if (HttpContext.Current.Session[y] != null)
                            x = HttpContext.Current.Session[y].ToString();
                    return x;
                }
                set
                {
                    HttpContext.Current.Session["RXServer.Web.SelectedPages.Level4Name_On_SitId_" + RXServer.Web.CurrentValues.SitId.ToString()] = value;
                }
            }
            public static String Level5Name
            {
                get
                {
                    String x = String.Empty;
                    String y = "RXServer.Web.SelectedPages.Level5Name_On_SitId_" + RXServer.Web.CurrentValues.SitId.ToString();
                    if (HttpContext.Current.Session != null)
                        if (HttpContext.Current.Session[y] != null)
                            x = HttpContext.Current.Session[y].ToString();
                    return x;
                }
                set
                {
                    HttpContext.Current.Session["RXServer.Web.SelectedPages.Level5Name_On_SitId_" + RXServer.Web.CurrentValues.SitId.ToString()] = value;
                }
            }
            public static String Level6Name
            {
                get
                {
                    String x = String.Empty;
                    String y = "RXServer.Web.SelectedPages.Level6Name_On_SitId_" + RXServer.Web.CurrentValues.SitId.ToString();
                    if (HttpContext.Current.Session != null)
                        if (HttpContext.Current.Session[y] != null)
                            x = HttpContext.Current.Session[y].ToString();
                    return x;
                }
                set
                {
                    HttpContext.Current.Session["RXServer.Web.SelectedPages.Level6Name_On_SitId_" + RXServer.Web.CurrentValues.SitId.ToString()] = value;
                }
            }
            public static String Level7Name
            {
                get
                {
                    String x = String.Empty;
                    String y = "RXServer.Web.SelectedPages.Level7Name_On_SitId_" + RXServer.Web.CurrentValues.SitId.ToString();
                    if (HttpContext.Current.Session != null)
                        if (HttpContext.Current.Session[y] != null)
                            x = HttpContext.Current.Session[y].ToString();
                    return x;
                }
                set
                {
                    HttpContext.Current.Session["RXServer.Web.SelectedPages.Level7Name_On_SitId_" + RXServer.Web.CurrentValues.SitId.ToString()] = value;
                }
            }
            public static String Level8Name
            {
                get
                {
                    String x = String.Empty;
                    String y = "RXServer.Web.SelectedPages.Level8Name_On_SitId_" + RXServer.Web.CurrentValues.SitId.ToString();
                    if (HttpContext.Current.Session != null)
                        if (HttpContext.Current.Session[y] != null)
                            x = HttpContext.Current.Session[y].ToString();
                    return x;
                }
                set
                {
                    HttpContext.Current.Session["RXServer.Web.SelectedPages.Level8Name_On_SitId_" + RXServer.Web.CurrentValues.SitId.ToString()] = value;
                }
            }
            public static String Level9Name
            {
                get
                {
                    String x = String.Empty;
                    String y = "RXServer.Web.SelectedPages.Level9Name_On_SitId_" + RXServer.Web.CurrentValues.SitId.ToString();
                    if (HttpContext.Current.Session != null)
                        if (HttpContext.Current.Session[y] != null)
                            x = HttpContext.Current.Session[y].ToString();
                    return x;
                }
                set
                {
                    HttpContext.Current.Session["RXServer.Web.SelectedPages.Level9Name_On_SitId_" + RXServer.Web.CurrentValues.SitId.ToString()] = value;
                }
            }

            public static void SetSelected()
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::SetSelected]";
                Int32 CurrentSitId = 0;
                Int32 CurrentPagId = 0;
                try
                {
                    Level1 = 0;
                    Level2 = 0;
                    Level3 = 0;
                    Level4 = 0;
                    Level5 = 0;
                    Level6 = 0;
                    Level7 = 0;
                    Level8 = 0;
                    Level9 = 0;
                    Level1Name = String.Empty;
                    Level2Name = String.Empty;
                    Level3Name = String.Empty;
                    Level4Name = String.Empty;
                    Level5Name = String.Empty;
                    Level6Name = String.Empty;
                    Level7Name = String.Empty;
                    Level8Name = String.Empty;
                    Level9Name = String.Empty;
                    CurrentSitId = CurrentValues.SitId;
                    CurrentPagId = CurrentValues.PagId;
                    if (!CurrentPagId.Equals(0))
                    {
                        using (RXServer.Modules.Menu.Item m = ((RXServer.Lib.RXBasePage)HttpContext.Current.CurrentHandler).CurrentPage)
                        {
                            if (!m.ParentId.Equals(0))
                            {
                                GetParent(CurrentSitId, m.ParentId);
                                switch (m.Parents.Length)
                                {
                                    case 1:
                                        Level2 = CurrentPagId;
                                        Level2Name = m.Title;
                                        break;
                                    case 2:
                                        Level3 = CurrentPagId;
                                        Level3Name = m.Title;
                                        break;
                                    case 3:
                                        Level4 = CurrentPagId;
                                        Level4Name = m.Title;
                                        break;
                                    case 4:
                                        Level5 = CurrentPagId;
                                        Level5Name = m.Title;
                                        break;
                                    case 5:
                                        Level6 = CurrentPagId;
                                        Level6Name = m.Title;
                                        break;
                                    case 6:
                                        Level7 = CurrentPagId;
                                        Level7Name = m.Title;
                                        break;
                                    case 7:
                                        Level8 = CurrentPagId;
                                        Level8Name = m.Title;
                                        break;
                                    case 8:
                                        Level9 = CurrentPagId;
                                        Level9Name = m.Title;
                                        break;
                                }
                            }
                            else
                            {
                                Level1 = CurrentPagId;
                                Level1Name = m.Title;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, String.Empty);
                }
            }
            private static void GetParent(Int32 SitId, Int32 PagId)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::GetParent]";
                try
                {
                    using (RXServer.Modules.Menu.Item m = new RXServer.Modules.Menu.Item(PagId))
                    {
                        if (!m.ParentId.Equals(0))
                        {
                            GetParent(SitId, m.ParentId);
                            switch (m.Parents.Length)
                            {
                                case 1:
                                    Level2 = PagId;
                                    Level2Name = m.Title;
                                    break;
                                case 2:
                                    Level3 = PagId;
                                    Level3Name = m.Title;
                                    break;
                                case 3:
                                    Level4 = PagId;
                                    Level4Name = m.Title;
                                    break;
                                case 4:
                                    Level5 = PagId;
                                    Level5Name = m.Title;
                                    break;
                                case 5:
                                    Level6 = PagId;
                                    Level6Name = m.Title;
                                    break;
                                case 6:
                                    Level7 = PagId;
                                    Level7Name = m.Title;
                                    break;
                                case 7:
                                    Level8 = PagId;
                                    Level8Name = m.Title;
                                    break;
                                case 8:
                                    Level9 = PagId;
                                    Level9Name = m.Title;
                                    break;
                            }
                        }
                        else
                        {
                            Level1 = PagId;
                            Level1Name = m.Title;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, String.Empty);
                }
            }
            public static String GetSelectedSiteMapPath(String Devider)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::GetSelectedSiteMapPath]";
                StringBuilder ret = new StringBuilder();
                try
                {
                    if (Level9Name.Equals(String.Empty))
                    {
                        if (Level8Name.Equals(String.Empty))
                        {
                            if (Level7Name.Equals(String.Empty))
                            {
                                if (Level6Name.Equals(String.Empty))
                                {
                                    if (Level5Name.Equals(String.Empty))
                                    {
                                        if (Level4Name.Equals(String.Empty))
                                        {
                                            if (Level3Name.Equals(String.Empty))
                                            {
                                                if (Level2Name.Equals(String.Empty))
                                                {
                                                    ret.Append(Level1Name);
                                                }
                                                else
                                                {
                                                    ret.Append(Level1Name);
                                                    ret.Append(Devider);
                                                    ret.Append(Level2Name);
                                                }
                                            }
                                            else
                                            {
                                                ret.Append(Level1Name);
                                                ret.Append(Devider);
                                                ret.Append(Level2Name);
                                                ret.Append(Devider);
                                                ret.Append(Level3Name);
                                            }
                                        }
                                        else
                                        {
                                            ret.Append(Level1Name);
                                            ret.Append(Devider);
                                            ret.Append(Level2Name);
                                            ret.Append(Devider);
                                            ret.Append(Level3Name);
                                            ret.Append(Devider);
                                            ret.Append(Level4Name);
                                        }
                                    }
                                    else
                                    {
                                        ret.Append(Level1Name);
                                        ret.Append(Devider);
                                        ret.Append(Level2Name);
                                        ret.Append(Devider);
                                        ret.Append(Level3Name);
                                        ret.Append(Devider);
                                        ret.Append(Level4Name);
                                        ret.Append(Devider);
                                        ret.Append(Level5Name);
                                    }
                                }
                                else
                                {
                                    ret.Append(Level1Name);
                                    ret.Append(Devider);
                                    ret.Append(Level2Name);
                                    ret.Append(Devider);
                                    ret.Append(Level3Name);
                                    ret.Append(Devider);
                                    ret.Append(Level4Name);
                                    ret.Append(Devider);
                                    ret.Append(Level5Name);
                                    ret.Append(Devider);
                                    ret.Append(Level6Name);
                                }
                            }
                            else
                            {
                                ret.Append(Level1Name);
                                ret.Append(Devider);
                                ret.Append(Level2Name);
                                ret.Append(Devider);
                                ret.Append(Level3Name);
                                ret.Append(Devider);
                                ret.Append(Level4Name);
                                ret.Append(Devider);
                                ret.Append(Level5Name);
                                ret.Append(Devider);
                                ret.Append(Level6Name);
                                ret.Append(Devider);
                                ret.Append(Level7Name);
                            }
                        }
                        else
                        {
                            ret.Append(Level1Name);
                            ret.Append(Devider);
                            ret.Append(Level2Name);
                            ret.Append(Devider);
                            ret.Append(Level3Name);
                            ret.Append(Devider);
                            ret.Append(Level4Name);
                            ret.Append(Devider);
                            ret.Append(Level5Name);
                            ret.Append(Devider);
                            ret.Append(Level6Name);
                            ret.Append(Devider);
                            ret.Append(Level7Name);
                            ret.Append(Devider);
                            ret.Append(Level8Name);
                        }
                    }
                    else
                    {
                        ret.Append(Level1Name);
                        ret.Append(Devider);
                        ret.Append(Level2Name);
                        ret.Append(Devider);
                        ret.Append(Level3Name);
                        ret.Append(Devider);
                        ret.Append(Level4Name);
                        ret.Append(Devider);
                        ret.Append(Level5Name);
                        ret.Append(Devider);
                        ret.Append(Level6Name);
                        ret.Append(Devider);
                        ret.Append(Level7Name);
                        ret.Append(Devider);
                        ret.Append(Level8Name);
                        ret.Append(Devider);
                        ret.Append(Level9Name);
                    }
                    return ret.ToString();
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, String.Empty);
                    return ret.ToString();
                }
            }
            public static Boolean IsSelected(Int32 PagId)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::IsSelected]";
                try
                {
                    if (PagId.Equals(SelectedPages.Level1) ||
                        PagId.Equals(SelectedPages.Level2) ||
                        PagId.Equals(SelectedPages.Level3) ||
                        PagId.Equals(SelectedPages.Level4) ||
                        PagId.Equals(SelectedPages.Level5) ||
                        PagId.Equals(SelectedPages.Level6) ||
                        PagId.Equals(SelectedPages.Level7) ||
                        PagId.Equals(SelectedPages.Level8) ||
                        PagId.Equals(SelectedPages.Level9))
                    {
                        return true;
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, String.Empty);
                    return false;
                }
            }
            public static String GetDynamicSiteMapPath(Int32 PagId, String Devider)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::GetDynamicSiteMapPath]";
                StringBuilder ret = new StringBuilder();
                try
                {
                    if (!PagId.Equals(0))
                    {
                        using (RXServer.Modules.Menu.Item m = new RXServer.Modules.Menu.Item(PagId))
                        {
                            if (!m.ParentId.Equals(0))
                            {
                                GetDynamicSiteMapPathParent(m.ParentId, ref ret, Devider);
                                ret.Append(Devider + m.Alias);
                            }
                            else
                                ret.Append(m.Alias);
                        }
                    }
                    return ret.ToString();
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, String.Empty);
                    return ret.ToString();
                }
            }
            private static void GetDynamicSiteMapPathParent(Int32 PagId, ref StringBuilder ret, String Devider)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::GetDynamicSiteMapPathParent]";
                try
                {
                    if (!PagId.Equals(0))
                    {
                        using (RXServer.Modules.Menu.Item m = new RXServer.Modules.Menu.Item(PagId))
                        {
                            if (!m.ParentId.Equals(0))
                            {
                                GetDynamicSiteMapPathParent(m.ParentId, ref ret, Devider);
                                ret.Append(Devider + m.Alias);
                            }
                            else
                                ret.Append(m.Alias);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, String.Empty);
                }
            }
        }
        #endregion public class SelectedPages

        #region public class ControlValues
        //public static class ControlValues
        //{
        //    static string CLASSNAME = "[Namespace::RXServer::Web][Class::ControlValues]";
        //    public static string PageControlDate
        //    {
        //        get
        //        {
        //            string FUNCTIONNAME = CLASSNAME + "[Function::PageControlDate::Get]";
        //            try
        //            {
        //                String ret = String.Empty;
        //                using (RXServer.Web.Menus.MenuItem mi = new RXServer.Web.Menus.MenuItem(CurrentValues.SitId, CurrentValues.PagId))
        //                {
        //                    ret = mi.Settings.ControlDate;
        //                }
        //                return ret;
        //            }
        //            catch (Exception ex)
        //            {
        //                Error.Report(ex, FUNCTIONNAME, String.Empty);
        //                return String.Empty;
        //            }
        //        }
        //        set
        //        {
        //            string FUNCTIONNAME = CLASSNAME + "[Function::PageControlDate:Set]";
        //            try
        //            {
        //                using (RXServer.Web.Menus.MenuItem mi = new RXServer.Web.Menus.MenuItem(CurrentValues.SitId, CurrentValues.PagId))
        //                {
        //                    mi.Settings.ControlDate = value;
        //                    mi.Settings.Save();
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                Error.Report(ex, FUNCTIONNAME, String.Empty);
        //            }
        //        }
        //    }
        //}
        #endregion public class ControlValues

        #region public class RequestValues
        public static class RequestValues
        {
            public static Int32 SitId
            {
                get
                {
                    Int32 x = 0;
                    if (HttpContext.Current.Request["SitId"] != null)
                        Int32.TryParse(HttpContext.Current.Request["SitId"], out x);
                    return x;
                }
            }
            public static Int32 PagId
            {
                get
                {
                    Int32 x = 0;
                    if (HttpContext.Current.Request["PagId"] != null)
                        Int32.TryParse(HttpContext.Current.Request["PagId"], out x);
                    return x;
                }
            }
            public static Int32 ModId
            {
                get
                {
                    Int32 x = 0;
                    if (HttpContext.Current.Request["ModId"] != null)
                        Int32.TryParse(HttpContext.Current.Request["ModId"], out x);
                    return x;
                }
            }
            public static Int32 MenId
            {
                get
                {
                    Int32 x = 0;
                    if (HttpContext.Current.Request["MenId"] != null)
                        Int32.TryParse(HttpContext.Current.Request["MenId"], out x);
                    return x;
                }
            }
            public static Int32 ItmId
            {
                get
                {
                    Int32 x = 0;
                    if (HttpContext.Current.Request["ItmId"] != null)
                        Int32.TryParse(HttpContext.Current.Request["ItmId"], out x);
                    return x;
                }
            }
            public static Int32 ForumId
            {
                get
                {
                    Int32 x = 0;
                    if (HttpContext.Current.Request["ForumId"] != null)
                        Int32.TryParse(HttpContext.Current.Request["ForumId"], out x);
                    return x;
                }
            }
            public static Int32 ThreadId
            {
                get
                {
                    Int32 x = 0;
                    if (HttpContext.Current.Request["ThreadId"] != null)
                        Int32.TryParse(HttpContext.Current.Request["ThreadId"], out x);
                    return x;
                }
            }
            public static Int32 Sort
            {
                get
                {
                    Int32 x = 0;
                    if (HttpContext.Current.Request["Sort"] != null)
                        Int32.TryParse(HttpContext.Current.Request["Sort"], out x);
                    return x;
                }
            }
            public static String Url
            {
                get
                {
                    String x = String.Empty;
                    if (HttpContext.Current.Request["Url"] != null)
                        x = HttpContext.Current.Request["Url"].ToString();
                    return x;
                }
            }
            public static String ConPa
            {
                get
                {
                    String x = String.Empty;
                    if (HttpContext.Current.Request["ConPa"] != null)
                        x = HttpContext.Current.Request["ConPa"].ToString();
                    return x;
                }
            }
            public static String Email
            {
                get
                {
                    String x = String.Empty;
                    if (HttpContext.Current.Request["Email"] != null)
                        x = HttpContext.Current.Request["Email"].ToString();
                    return x;
                }
            }
            public static String Code
            {
                get
                {
                    String x = String.Empty;
                    if (HttpContext.Current.Request["Code"] != null)
                        x = HttpContext.Current.Request["Code"].ToString();
                    return x;
                }
            }
            public static String RolId
            {
                get
                {
                    String x = String.Empty;
                    if (HttpContext.Current.Request["RolId"] != null)
                        x = HttpContext.Current.Request["RolId"].ToString();
                    return x;
                }
            }
            public static String Page
            {
                get
                {
                    String x = String.Empty;
                    if (HttpContext.Current.Request["Page"] != null)
                        x = HttpContext.Current.Request["Page"].ToString();
                    return x;
                }
            }
            public static String DelId
            {
                get
                {
                    String x = String.Empty;
                    if (HttpContext.Current.Request["DelId"] != null)
                        x = HttpContext.Current.Request["DelId"].ToString();
                    return x;
                }
            }
            public static String EditId
            {
                get
                {
                    String x = String.Empty;
                    if (HttpContext.Current.Request["EditId"] != null)
                        x = HttpContext.Current.Request["EditId"].ToString();
                    return x;
                }
            }
            public static String ViewId
            {
                get
                {
                    String x = String.Empty;
                    if (HttpContext.Current.Request["ViewId"] != null)
                        x = HttpContext.Current.Request["ViewId"].ToString();
                    return x;
                }
            }
            public static String SubPage
            {
                get
                {
                    String x = String.Empty;
                    if (HttpContext.Current.Request["SubPage"] != null)
                        x = HttpContext.Current.Request["SubPage"].ToString();
                    return x;
                }
            }
            public static String StaId
            {
                get
                {
                    String x = String.Empty;
                    if (HttpContext.Current.Request["StaId"] != null)
                        x = HttpContext.Current.Request["StaId"].ToString();
                    return x;
                }
            }
            public static Int32 ParentId
            {
                get
                {
					Int32 x = 0;
					if (HttpContext.Current.Request["ParentId"] != null)
						Int32.TryParse(HttpContext.Current.Request["ParentId"], out x);
					return x;
                }
            }
            public static String Level1
            {
                get
                {
                    String x = String.Empty;
                    if (HttpContext.Current.Request["Level1"] != null)
                        x = HttpContext.Current.Request["Level1"].ToString();
                    return x;
                }
            }
            public static String Level2
            {
                get
                {
                    String x = String.Empty;
                    if (HttpContext.Current.Request["Level2"] != null)
                        x = HttpContext.Current.Request["Level2"].ToString();
                    return x;
                }
            }
            public static String Level3
            {
                get
                {
                    String x = String.Empty;
                    if (HttpContext.Current.Request["Level3"] != null)
                        x = HttpContext.Current.Request["Level3"].ToString();
                    return x;
                }
            }
            public static String Level4
            {
                get
                {
                    String x = String.Empty;
                    if (HttpContext.Current.Request["Level4"] != null)
                        x = HttpContext.Current.Request["Level4"].ToString();
                    return x;
                }
            }
            public static String Mode
            {
                get
                {
                    String x = String.Empty;
                    if (HttpContext.Current.Request["Mode"] != null)
                        x = HttpContext.Current.Request["Mode"].ToString();
                    return x;
                }
            }
            public static Int32 Logout
            {
                get
                {
                    Int32 x = 0;
                    if (HttpContext.Current.Request["Logout"] != null)
                        Int32.TryParse(HttpContext.Current.Request["Logout"], out x);
                    return x;
                }
            }
            public static Int32 Index
            {
                get
                {
                    Int32 x = 0;
                    if (HttpContext.Current.Request["Index"] != null)
                        Int32.TryParse(HttpContext.Current.Request["Index"], out x);
                    return x;
                }
            }
            public static String Orderby
            {
                get
                {
                    String x = String.Empty;
                    if (HttpContext.Current.Request["Orderby"] != null)
                        x = HttpContext.Current.Request["Orderby"].ToString();
                    return x;
                }
            }
            public static Int32 Event
            {
                get
                {
                    Int32 x = 0;
                    if (HttpContext.Current.Request["Event"] != null)
                        Int32.TryParse(HttpContext.Current.Request["Event"], out x);
                    return x;
                }
            }
            public static Int32 News
            {
                get
                {
                    Int32 x = 0;
                    if (HttpContext.Current.Request["News"] != null)
                        Int32.TryParse(HttpContext.Current.Request["News"], out x);
                    return x;
                }
            }
            public static Int32 ObdId
            {
                get
                {
                    Int32 x = 0;
                    if (HttpContext.Current.Request["ObdId"] != null)
                        Int32.TryParse(HttpContext.Current.Request["ObdId"], out x);
                    return x;
                }
            }
            public static String Var
            {
                get
                {
                    String x = String.Empty;
                    if (HttpContext.Current.Request["Var"] != null)
                        x = HttpContext.Current.Request["Var"].ToString();
                    return x;
                }
            }
            public static String Tag
            {
                get
                {
                    String x = String.Empty;
                    if (HttpContext.Current.Request["Tag"] != null)
                        x = HttpContext.Current.Request["Tag"].ToString();
                    return x;
                }
            }
            public static Int32 Id
            {
                get
                {
                    Int32 x = 0;
                    if (HttpContext.Current.Request["Id"] != null)
                        Int32.TryParse(HttpContext.Current.Request["Id"], out x);
                    return x;
                }
            }
            public static String v1
            {
                get
                {
                    String x = String.Empty;
                    if (HttpContext.Current.Request["v1"] != null)
                        x = HttpContext.Current.Request["v1"].ToString();
                    return x;
                }
            }
            public static String v2
            {
                get
                {
                    String x = String.Empty;
                    if (HttpContext.Current.Request["v2"] != null)
                        x = HttpContext.Current.Request["v2"].ToString();
                    return x;
                }
            }
            public static String v3
            {
                get
                {
                    String x = String.Empty;
                    if (HttpContext.Current.Request["v3"] != null)
                        x = HttpContext.Current.Request["v3"].ToString();
                    return x;
                }
            }
			public static String Path
			{
				get
				{
					String x = String.Empty;
					if (HttpContext.Current.Request["Path"] != null)
						x = HttpContext.Current.Request["Path"].ToString();
					return x;
				}
			}
			public static String ArtId
			{
				get
				{
					String x = String.Empty;
					if (HttpContext.Current.Request["ArtId"] != null)
						x = HttpContext.Current.Request["ArtId"].ToString();
					return x;
				}
			}
			public static String FileName
			{
				get
				{
					String x = String.Empty;
					if (HttpContext.Current.Request["FileName"] != null)
						x = HttpContext.Current.Request["FileName"].ToString();
					return x;
				}
			}
        }
        #endregion public class RequestValues

        #region public class Redirect
		public static class Redirect
		{
			public enum Method
			{
				RedirectFalse = 1,
				RedirectTrue = 2,
				TransferFalse = 3,
				TransferTrue = 4
			}
			public static void To(String Url)
			{
                try
                {
					((RXServer.Lib.RXDefaultPage)HttpContext.Current.CurrentHandler).IsTerminating = true;
                    HttpContext.Current.Response.Redirect(Url, false);
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                    return;
                }
                catch (System.Threading.ThreadAbortException ex) { }
                catch (Exception ex)
                {
                    RXServer.Lib.Error.Report(ex, "To", String.Empty);
                }
			}
			//public static void To(String Url, Method m)
			//{
			//    try
			//    {
			//        switch (m)
			//        {
			//            case Method.RedirectFalse:
			//                HttpContext.Current.Response.Redirect(Url, false);
			//                HttpContext.Current.ApplicationInstance.CompleteRequest();
			//                break;
			//            case Method.RedirectTrue:
			//                HttpContext.Current.Response.Redirect(Url, true);
			//                break;
			//            case Method.TransferFalse:
			//                HttpContext.Current.Server.Transfer(Url, false);
			//                break;
			//            case Method.TransferTrue:
			//                HttpContext.Current.Server.Transfer(Url, true);
			//                break;
			//            default:
			//                HttpContext.Current.Response.Redirect(Url, true);
			//                break;
			//        }
			//    }
			//    catch (System.Threading.ThreadAbortException ex) { }
			//    catch (Exception ex)
			//    {
			//        RXServer.Lib.Error.Report(ex, "To", String.Empty);
			//    }
			//}
		}
        #endregion public class Redirect

        #region public class SiteMap
        //public static class SiteMap
        //{
        //    static string CLASSNAME = "[Namespace::RXServer::Web][Class::SiteMap]";
        //    private static StringBuilder Html = new StringBuilder();
        //    public static StringBuilder GenerateHtml(Int32 SitId)
        //    {
        //        string FUNCTIONNAME = CLASSNAME + "[Function::GenerateHtml]";
        //        try
        //        {
        //            return _GenerateHtml(SitId, 0);
        //        }
        //        catch (Exception ex)
        //        {
        //            Error.Report(ex, FUNCTIONNAME, String.Empty);
        //            return new StringBuilder();
        //        }
        //    }
        //    public static StringBuilder GenerateHtml(Int32 SitId, Int32 PagId)
        //    {
        //        string FUNCTIONNAME = CLASSNAME + "[Function::GenerateHtml]";
        //        try
        //        {
        //            return _GenerateHtml(SitId, PagId);
        //        }
        //        catch (Exception ex)
        //        {
        //            Error.Report(ex, FUNCTIONNAME, String.Empty);
        //            return new StringBuilder();
        //        }
        //    }
        //    private static StringBuilder _GenerateHtml(Int32 SitId, Int32 PagId)
        //    {
        //        string FUNCTIONNAME = CLASSNAME + "[Function::_GenerateHtml]";
        //        try
        //        {
        //            Html = new StringBuilder();
        //            using (RXServer.PageCollection pc = new RXServer.PageCollection(SitId, PagId, false, RXServer.Data.DataSource, RXServer.Data.ConnectionString))
        //            {
        //                if (!PagId.Equals(0))
        //                    using (RXServer.Web.Menus.MenuItem mi = new RXServer.Web.Menus.MenuItem(SitId, PagId))
        //                    {
        //                        Html.Append("<div class=\"sitemapitem0\"><a href=\"Default.aspx?PagId=" + PagId.ToString() + "\">" + HttpContext.Current.Server.UrlDecode(mi.Name) + "</a></div>");
        //                    }
        //                Write(pc);
        //            }
        //            return Html;
        //        }
        //        catch (Exception ex)
        //        {
        //            Error.Report(ex, FUNCTIONNAME, String.Empty);
        //            return new StringBuilder();
        //        }
        //    }
        //    private static void Write(RXServer.PageCollection pc)
        //    {
        //        string FUNCTIONNAME = CLASSNAME + "[Function::Write]";
        //        try
        //        {
        //            foreach (RXServer.Page p in pc.Items)
        //            {
        //                if (!p.Hidden)
        //                {
        //                    System.Text.StringBuilder s = new System.Text.StringBuilder();
        //                    Int32 index = 0;
        //                    if (p.Parents != null)
        //                    {
        //                        index = p.Parents.Length;
        //                        //for (Int32 i = 0; i < index; i++)
        //                        //    s.Append("&nbsp;&nbsp;&nbsp;&nbsp;");
        //                    }
        //                    s.Append("<div class=\"sitemapitem" + index.ToString() + "\"><a href=\"Default.aspx?PagId=" + p.Id.ToString() + "\">" + HttpContext.Current.Server.UrlDecode(p.Name) + "</a></div>");
        //                    Html.Append(s.ToString());

        //                    if (p.Pages.Count() > 0)
        //                        Write(p.Pages);
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            Error.Report(ex, FUNCTIONNAME, String.Empty);
        //        }
        //    }
        //}
        #endregion public class SiteMap

		#region public class Cookie
		public static class Cookie
		{
			static string CLASSNAME = "[Namespace::RXServer::Web][Class::Cookie]";
			public static void WriteObjectToCookie(System.Object o, String tag)
			{
				string FUNCTIONNAME = CLASSNAME + "[Function::WriteObjectToCookie]";
				try
				{
					MemoryStream memStream = new MemoryStream();
					BinaryFormatter binFormatter = new BinaryFormatter();
					binFormatter.Serialize(memStream, o);
					System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
					HttpCookie c = new HttpCookie(tag);
					c.Value = Convert.ToBase64String(memStream.ToArray());
					c.Expires = DateTime.Now.AddYears(30);
					HttpContext.Current.Response.Cookies.Add(c);
					memStream.Close();
				}
				catch (Exception ex)
				{
					Error.Report(ex, FUNCTIONNAME, String.Empty);
				}
			}
			public static System.Object ReadObjectFromCookie(String tag)
			{
				string FUNCTIONNAME = CLASSNAME + "[Function::ReadObjectFromCookie]";
				try
				{
					if (HttpContext.Current.Request.Cookies[tag] == null)
						return null;
					System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
					HttpCookie c = HttpContext.Current.Request.Cookies[tag];
					MemoryStream memStream = new MemoryStream(Convert.FromBase64String(c.Value.ToString()));
					BinaryFormatter binFormatter = new BinaryFormatter();
					System.Object o = binFormatter.Deserialize(memStream);
					memStream.Close();
					return o;
				}
				catch (Exception ex)
				{
					Error.Report(ex, FUNCTIONNAME, String.Empty);
					return null;
				}
			}
		}
		#endregion public class Cookie
    }
}

// RXServer.Admin
namespace RXServer
{
    namespace Admin
    {

    }
}

// RXServer.Modules
namespace RXServer
{
    namespace Modules
    {
        namespace Base
        {
            public class List : LiquidCore.List
            {
                public Boolean Exist
                {
                    get
                    {
                        return this._list.Count > 0;
                    }
                }
                public String State
                {
                    get
                    {
                        return this.GetSetting("State");
                    }
                    set
                    {
                        this.SaveSetting("State", value);
                    }
                }
                public List(Int32 SitId, Int32 PagId, Int32 ModId) : base(SitId, PagId, ModId) { }
                public List(String Alias) : base(Alias) { }
                public List(Int32 SitId, Int32 PagId, Int32 ModId, List.SortParamEnum SortParamEnum, List.SortOrderEnum SortOrderEnum, Int32 StartIndex, Int32 Limit) : base(SitId, PagId, ModId) 
                {
                    SortOrderClean(SortParamEnum, SortOrderEnum, StartIndex, Limit);
                }
                public List(String Alias, List.SortParamEnum SortParamEnum, List.SortOrderEnum SortOrderEnum, Int32 StartIndex, Int32 Limit) : base(Alias)
                {
                    SortOrderClean(SortParamEnum, SortOrderEnum, StartIndex, Limit);
                }
                private void SortOrderClean(List.SortParamEnum SortParamEnum, List.SortOrderEnum SortOrderEnum, Int32 StartIndex, Int32 Limit)
                {
                    base.Sort(SortParamEnum, SortOrderEnum);

                    Int32 _counter = 0;

                    for (Int32 i = 0; i <= base._list.Count; i++)
                    {
                        if (i < StartIndex)
                            _counter++;
                        else if (i > (StartIndex + Limit))
                            _counter++;
                    }

                    Int32[] _index = new Int32[_counter];

                    _counter = 0;

                    for (Int32 i = 0; i <= base._list.Count; i++)
                    {
                        if (i < StartIndex)
                        {
                            _index[_counter] = i;
                            _counter++;
                        }
                        else if (i > (StartIndex + Limit))
                        {
                            _index[_counter] = (i - 1);
                            _counter++;
                        }
                    }

                    for (int i = _index.GetUpperBound(0); i >= _index.GetLowerBound(0); i--)
                        base._list.RemoveAt(_index[i]);
                }
            }
			public abstract class Generic : LiquidCore.List
			{
				public enum ScaleMethod
				{
					ScaleFixedImage,
					ScaleFixedWidthImage
				}
				public Boolean Exist
				{
					get
					{
						return this._list.Count > 0;
					}
				}
				public String State
				{
					get
					{
						return this.GetSetting("State");
					}
					set
					{
						this.SaveSetting("State", value);
					}
				}
				public Generic(Int32 SitId, Int32 PagId, Int32 ModId) : base(SitId, PagId, ModId) { }
				public Generic(Int32 SitId, Int32 PagId, Int32 ModId, List.SortParamEnum SortParamEnum, List.SortOrderEnum SortOrderEnum, Int32 StartIndex, Int32 Limit) : base(SitId, PagId, ModId)
				{
					SortOrderClean(SortParamEnum, SortOrderEnum, StartIndex, Limit);
				}
				public Generic(String Alias, List.SortParamEnum SortParamEnum, List.SortOrderEnum SortOrderEnum, Int32 StartIndex, Int32 Limit) : base(Alias)
				{
					SortOrderClean(SortParamEnum, SortOrderEnum, StartIndex, Limit);
				}
				private void SortOrderClean(List.SortParamEnum SortParamEnum, List.SortOrderEnum SortOrderEnum, Int32 StartIndex, Int32 Limit)
				{
					base.Sort(SortParamEnum, SortOrderEnum);

					Int32 _counter = 0;

					for (Int32 i = 0; i <= base._list.Count; i++)
					{
						if (i < StartIndex)
							_counter++;
						else if (i > (StartIndex + Limit))
							_counter++;
					}

					Int32[] _index = new Int32[_counter];

					_counter = 0;

					for (Int32 i = 0; i <= base._list.Count; i++)
					{
						if (i < StartIndex)
						{
							_index[_counter] = i;
							_counter++;
						}
						else if (i > (StartIndex + Limit))
						{
							_index[_counter] = (i - 1);
							_counter++;
						}
					}

					for (int i = _index.GetUpperBound(0); i >= _index.GetLowerBound(0); i--)
						base._list.RemoveAt(_index[i]);
				}
				public static new Int32 Create(Int32 SitId, Int32 PagId, Int32 MdeId, String ContentPane, Int32 Language, Int32 Status, Boolean UseSSL, Boolean AllPages)
				{
					Int32 ModId = List.Create(SitId, PagId, MdeId, ContentPane, Language, Status, UseSSL, AllPages);
					if (!ModId.Equals(0))
					{
						// Skapa ett item, som i de fall objectet 
						// ska användas som enskilt då ser ut som ett object och inte en object lista...
						using (Item li = new Item())
						{
							li.Status = Status;
							li.Language = Language;
							li.SitId = SitId;
							li.PagId = PagId;
							li.ModId = ModId;
							li.Save();
						}
					}
					return ModId;
				}
				public static void Delete(Int32 Id)
				{
					LiquidCore.Module m = new LiquidCore.Module(Id);
					m.Delete();
				}
				private System.Drawing.Image ByteArrayToImage(byte[] byteArrayIn)
				{
					MemoryStream ms = new MemoryStream(byteArrayIn);
					System.Drawing.Image returnImage = System.Drawing.Image.FromStream(ms);
					return returnImage;
				}
				public static String SaveImage(Stream stream, int width, int height, ScaleMethod scaleMethod, String activeDir, String fileName)
				{
					Bitmap image = new Bitmap(stream);
					return SaveImage(image, width, height, scaleMethod, activeDir, fileName);
				}
				public static String SaveImage(byte[] imageData, int width, int height, ScaleMethod scaleMethod, String activeDir, String fileName)
				{
					MemoryStream ms = new MemoryStream(imageData);
					Bitmap image = new Bitmap(ms);
					return SaveImage(image, width, height, scaleMethod, activeDir, fileName);
				}
				public static String SaveImage(Bitmap image, int width, int height, ScaleMethod scaleMethod, String activeDir, String fileName)
				{
					bool hasAlpha = false;
					for (int x = 0; x < image.Width; x++)
					{
						for (int y = 0; y < image.Height; y++)
						{
							if (image.GetPixel(x, y).A != 255)
							{
								hasAlpha = true;
								break;
							}
						}
					}

					System.Drawing.Image imageToSave;
					if (image.Width != width)
					{
						if (scaleMethod == ScaleMethod.ScaleFixedImage)
						{
							imageToSave = RXMali.ScaleFixedImage(image, width, height);
						}
						else
						{
							imageToSave = RXMali.ScaleFixedWidthImage(image, width);
						}
					}
					else
					{
						imageToSave = image;
					}

					System.Drawing.Image imageToSaveMobile;
					if (image.Width != 560)
					{
						if (scaleMethod == ScaleMethod.ScaleFixedImage)
						{
							double ratio = 560 / (double)width;
							imageToSaveMobile = RXMali.ScaleFixedImage(image, 560, (int)(height * ratio));
						}
						else
						{
							imageToSaveMobile = RXMali.ScaleFixedWidthImage(image, 560);
						}
					}
					else
					{
						imageToSaveMobile = image;
					}

					if (!activeDir.EndsWith("\\"))
					{
						activeDir += "\\";
					}

					String ext;
					if (hasAlpha)
					{
						imageToSave.Save(activeDir + fileName + ".png", System.Drawing.Imaging.ImageFormat.Png);
						imageToSave.Dispose();
						imageToSaveMobile.Save(activeDir + "m_" + fileName + ".png", System.Drawing.Imaging.ImageFormat.Png);
						imageToSaveMobile.Dispose();
						ext = ".png";
					}
					else
					{
						ImageCodecInfo jgpEncoder = GetEncoder(ImageFormat.Jpeg);
						System.Drawing.Imaging.Encoder encoder = System.Drawing.Imaging.Encoder.Quality;
						EncoderParameters encoderParameters = new EncoderParameters(1);
						EncoderParameter encoderParameter = new EncoderParameter(encoder, 90L);
						encoderParameters.Param[0] = encoderParameter;
						imageToSave.Save(activeDir + fileName + ".jpg", jgpEncoder, encoderParameters);
						imageToSave.Dispose();
						imageToSaveMobile.Save(activeDir + "m_" + fileName + ".jpg", jgpEncoder, encoderParameters);
						imageToSaveMobile.Dispose();
						ext = ".jpg";
					}

					return ext;
				}
				private static ImageCodecInfo GetEncoder(ImageFormat format)
				{
					ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
					foreach (ImageCodecInfo codec in codecs)
					{
						if (codec.FormatID == format.Guid)
						{
							return codec;
						}
					}
					return null;
				}
				public abstract String GeoCode
				{
					get;
					set;
				}
				public abstract String TimeStamp
				{
					get;
					set;
				}
			}
        }
        public class Site : LiquidCore.Site
        {
            public Site(Int32 SitId) : base(SitId) { }
        }
        public class ModuleDefinitions : LiquidCore.ModDefs
        {
            public ModuleDefinitions(Int32 SitId) : base(SitId, 0) { }
            public ModuleDefinitions(Int32 Status, Int32 SitId) : base(SitId, 0) 
            {
                Int32 _counter = 0;
                foreach (LiquidCore.ModDef i in base._list)
                    if (!i.Status.Equals(Status))
                        _counter++;

                Int32[] _index = new Int32[_counter];
                _counter = 0;

                for (Int32 x = 0; x < base._list.Count; x++)
                {
                    LiquidCore.ModDef i = (LiquidCore.ModDef)base._list[x];
                    if (!i.Status.Equals(Status))
                    {
                        _index[_counter] = x;
                        _counter++;
                    }
                }

                for (int i = _index.GetUpperBound(0); i >= _index.GetLowerBound(0); i--)
                    base._list.RemoveAt(_index[i]);
            }
        }
        public class Modules : LiquidCore.Modules
        {
            public Modules() : base() { }
            public Modules(Int32 Status) : base() 
            {
                Int32 _counter = 0;
                foreach (LiquidCore.Module i in base._list)
                    if (!i.Status.Equals(Status))
                        _counter++;

                Int32[] _index = new Int32[_counter];
                _counter = 0;

                for (Int32 x = 0; x < base._list.Count; x++)
                {
                    LiquidCore.Module i = (LiquidCore.Module)base._list[x];
                    if (!i.Status.Equals(Status))
                    {
                        _index[_counter] = x;
                        _counter++;
                    }
                }

                for (int i = _index.GetUpperBound(0); i >= _index.GetLowerBound(0); i--)
                    base._list.RemoveAt(_index[i]);
            }
            public Modules(Int32 SitId, Int32 MdeId) : base()
            {
                Int32 _counter = 0;
                foreach (LiquidCore.Module i in base._list)
                    if (!i.MdeId.Equals(MdeId))
                        _counter++;

                Int32[] _index = new Int32[_counter];
                _counter = 0;

                for (Int32 x = 0; x < base._list.Count; x++)
                {
                    LiquidCore.Module i = (LiquidCore.Module)base._list[x];
                    if (!i.MdeId.Equals(MdeId))
                    {
                        _index[_counter] = x;
                        _counter++;
                    }
                }

                for (int i = _index.GetUpperBound(0); i >= _index.GetLowerBound(0); i--)
                    base._list.RemoveAt(_index[i]);
            }
            public Modules(Int32 Status, Int32 SitId, Int32 MdeId) : this(Status)
            {
                Int32 _counter = 0;
                foreach (LiquidCore.Module i in base._list)
                    if (!i.MdeId.Equals(MdeId))
                        _counter++;

                Int32[] _index = new Int32[_counter];
                _counter = 0;

                for (Int32 x = 0; x < base._list.Count; x++)
                {
                    LiquidCore.Module i = (LiquidCore.Module)base._list[x];
                    if (!i.MdeId.Equals(MdeId))
                    {
                        _index[_counter] = x;
                        _counter++;
                    }
                }

                for (int i = _index.GetUpperBound(0); i >= _index.GetLowerBound(0); i--)
                    base._list.RemoveAt(_index[i]);
            }
            public Modules(Int32 SitId, Int32 PagId, Int32 ParentId, Boolean IncludeAggregated) : base(SitId, PagId, ParentId, IncludeAggregated) { }
            public Modules(Int32 Status, Int32 SitId, Int32 PagId, Int32 ParentId, Boolean IncludeAggregated) : base(Status, SitId, PagId, ParentId, IncludeAggregated) { }
            public static RXServer.Modules.Modules GetModulesByPagId(Int32 SitId, Int32 PagId)
            {
                return new Modules(SitId, PagId, 0, true);
            }
            public static RXServer.Modules.Modules GetModulesByPagId(Int32 Status, Int32 SitId, Int32 PagId)
            {
                return new Modules(Status, SitId, PagId, 0, false);
            }
            public static RXServer.Modules.Modules GetModulesByMdeId(Int32 SitId, Int32 MdeId)
            {
                return new Modules(SitId, MdeId);
            }
            public static RXServer.Modules.Modules GetModulesByMdeId(Int32 Status, Int32 SitId, Int32 MdeId)
            {
                return new Modules(Status, SitId, MdeId);
            }
            public static Int32 GetTotalModulesOnPage(Int32 PagId)
            {
                RXServer.Modules.Menu.Item i = new RXServer.Modules.Menu.Item(PagId);
                return i.Modules.Count;
            }
            public static Int32 GetTotalModulesOnPage(Int32 Status, Int32 PagId)
            {
                Int32 _counter = 0;
                RXServer.Modules.Menu.Item i = new RXServer.Modules.Menu.Item(PagId);
                foreach (LiquidCore.Module m in i.Modules)
                    if (m.Status.Equals(Status))
                        _counter++;
                return _counter;
            }
            public static Int32 GetTotalModulesBasedOnModDef(Int32 SitId, Int32 MdeId)
            {
                RXServer.Modules.Modules m = new RXServer.Modules.Modules(SitId, MdeId);
                return m.Count;
            }
            public static Int32 GetTotalModulesBasedOnModDef(Int32 SitId, Int32 Status, Int32 MdeId)
            {
                RXServer.Modules.Modules m = new RXServer.Modules.Modules(Status, SitId, MdeId);
                return m.Count;
            }
        }
        public class Menu : LiquidCore.Menu
        {
            public Menu(Int32 SitId, Int32 PagParentId) : base(SitId, PagParentId) 
            {
 
            }
            public Menu(Int32 SitId, Int32 PagParentId, Int32 Status) : base(SitId, PagParentId) 
            {
                Int32 _counter = 0;
                foreach (LiquidCore.Menu.Item i in base._list)
                    if (!i.Status.Equals(Status))
                        _counter++;

                Int32[] _index = new Int32[_counter];
                _counter = 0;

                for (Int32 x = 0; x < base._list.Count; x++)
                {
                    LiquidCore.Menu.Item i = (LiquidCore.Menu.Item)base._list[x];
                    if (!i.Status.Equals(Status))
                    {
                        _index[_counter] = x;
                        _counter++;
                    }
                }

                for (int i = _index.GetUpperBound(0); i >= _index.GetLowerBound(0); i--)
                    base._list.RemoveAt(_index[i]);

            }
            public Menu(Int32 SitId, Int32 PagParentId, RXServer.Modules.Menu.SortParamEnum SortParamEnum, RXServer.Modules.Menu.SortOrderEnum SortOrderEnum, Int32 StartIndex, Int32 Limit) : this(SitId, PagParentId)
            {
                SortOrderClean(SortParamEnum, SortOrderEnum, StartIndex, Limit);
            }
            public Menu(Int32 SitId, Int32 PagParentId, Int32 Status, RXServer.Modules.Menu.SortParamEnum SortParamEnum, RXServer.Modules.Menu.SortOrderEnum SortOrderEnum, Int32 StartIndex, Int32 Limit) : this(SitId, PagParentId, Status)
            {
                SortOrderClean(SortParamEnum, SortOrderEnum, StartIndex, Limit);
            }
            public static Int32 GetTotalChildsOnPage(Int32 SitId, Int32 PagId)
            {
                RXServer.Modules.Menu i = new RXServer.Modules.Menu(SitId, PagId);
                return i.Count;
            }
            public static void DeletePage(Int32 SitId,Int32 PagId)
            {
                //Recursively delete childs
                Pages childs = new Pages(SitId, PagId);
                foreach (LiquidCore.Page child in childs)
                {
                    DeletePage(SitId, child.Id);
                }

                RXServer.Modules.Modules m = RXServer.Modules.Modules.GetModulesByPagId(SitId, PagId);

                if (m.Count > 0)
                {
                    foreach (LiquidCore.Module mod in m)
                    {
                        if (!mod.AllPages)
                        {
                            mod.Delete();
                        }
                    }
                }        
                RXServer.Modules.Menu.Item mItem = new RXServer.Modules.Menu.Item(PagId);
                
                //Delete settings
                foreach (Setting s in mItem.Settings)
                {
                    s.Delete();
                }

                mItem.Delete();
            }
            private void SortOrderClean(RXServer.Modules.Menu.SortParamEnum SortParamEnum, RXServer.Modules.Menu.SortOrderEnum SortOrderEnum, Int32 StartIndex, Int32 Limit)
            {
                base.Sort(SortParamEnum, SortOrderEnum);

                Int32 _counter = 0;

                for (Int32 i = 0; i <= base._list.Count; i++)
                {
                    if (i < StartIndex)
                        _counter++;
                    else if (i > (StartIndex + Limit))
                        _counter++;
                }

                Int32[] _index = new Int32[_counter];

                _counter = 0;

                for (Int32 i = 0; i <= base._list.Count; i++)
                {
                    if (i < StartIndex)
                    {
                        _index[_counter] = i;
                        _counter++;
                    }
                    else if (i > (StartIndex + Limit))
                    {
                        _index[_counter] = (i - 1);
                        _counter++;
                    }
                }

                for (int i = _index.GetUpperBound(0); i >= _index.GetLowerBound(0); i--)
                    base._list.RemoveAt(_index[i]);
            }
            public new class Item : LiquidCore.Menu.Item
            {
                private Int32[] m_pag_parents;
                public string MetaTitle
                {
                    get
                    {
                        return this.GetSetting("MetaTitle");
                    }
                    set
                    {
                        this.SaveSetting("MetaTitle", value);
                    }
                }
                public string MetaDescription
                {
                    get
                    {
                        return this.GetSetting("MetaDescription");
                    }
                    set
                    {
                        this.SaveSetting("MetaDescription", value);
                    }
                }
                public string MetaKeywords
                {
                    get
                    {
                        return this.GetSetting("MetaKeywords");
                    }
                    set
                    {
                        this.SaveSetting("MetaKeywords", value);
                    }
                }
                public string MetaAuthor
                {
                    get
                    {
                        return this.GetSetting("MetaAuthor");
                    }
                    set
                    {
                        this.SaveSetting("MetaAuthor", value);
                    }
                }
                public string MetaCopyright
                {
                    get
                    {
                        return this.GetSetting("MetaCopyright");
                    }
                    set
                    {
                        this.SaveSetting("MetaCopyright", value);
                    }
                }
                public string MetaRobots
                {
                    get
                    {
                        return this.GetSetting("MetaRobots");
                    }
                    set
                    {
                        this.SaveSetting("MetaRobots", value);
                    }
                }
                public string MetaCustom
                {
                    get
                    {
                        return this.GetSetting("MetaCustom");
                    }
                    set
                    {
                        this.SaveSetting("MetaCustom", value);
                    }
                }
                public string MetaFriendlyUrl
                {
                    get
                    {
                        return this.GetSetting("MetaFriendlyUrl");
                    }
                    set
                    {
                        this.SaveSetting("MetaFriendlyUrl", value);
                    }
                }
				public string Terms
				{
					get
					{
						return this.GetSetting("Terms");
					}
					set
					{
						this.SaveSetting("Terms", value);
					}
				}
				public bool ShowCart
				{
					get
					{
						return this.GetSetting("ShowCart").Equals("True");
					}
					set
					{
						this.SaveSetting("ShowCart", value.ToString());
					}
				}
                public Item() : base() { }
                public Item(Int32 Id) : base(Id) { }
                public void Save()
                {
                    base.Save(); 
                    if (!this.ParentId.Equals(0))
                    {
                        RXServer.Modules.Menu.Item i = new Item(this.ParentId);
                        this.MetaTitle = i.MetaTitle;
                        this.MetaDescription = i.MetaDescription;
                        this.MetaKeywords = i.MetaKeywords;
                        this.MetaAuthor = i.MetaAuthor;
                        this.MetaCopyright = i.MetaCopyright;
                        this.MetaRobots = i.MetaRobots;
                        this.MetaCustom = i.MetaCustom; 
                    }
                }
                public Int32[] Parents
                {
                    get
                    {
                        GetParents();
                        return this.m_pag_parents;
                    }
                }
                private void GetParents()
                {
                    Int32[] Parents = null;
                    try
                    {
                        if (base.ParentId > 0)
                        {
                            Parents = new Int32[1];
                            Parents[0] = base.ParentId;
                            Parents = RXServer.Lib.Generic.GrowArray(GetNextParent(Parents, base.ParentId), 0);
                        }
                        this.m_pag_parents = Parents;
                    }
                    catch (Exception ex)
                    {
                    }
                }
                private Int32[] GetNextParent(Int32[] Parents, Int32 PagId)
                {
                    try
                    {
                        Item p = new Item(PagId);
                        if (p.ParentId > 0)
                        {
                            Parents = RXServer.Lib.Generic.GrowArray(Parents, Parents.Length + 1);
                            Parents[Parents.Length - 1] = p.ParentId;
                            Parents = GetNextParent(Parents, p.ParentId);
                        }
                        return Parents;
                    }
                    catch (Exception ex)
                    {
                        return Parents;
                    }
                }
            }
        }

        public class Aggregation : LiquidCore.Aggregation
        {
            public static LiquidCore.Modules GetModules()
            {
                return new LiquidCore.Modules();
            }
            public static LiquidCore.Modules GetModules(Int32 SitId, Int32 PagId)
            {
                return new LiquidCore.Modules(SitId, PagId, 0, false);
            }
        }

        public class StandardModule : RXServer.Modules.Base.Generic
        {
			public enum WidthTypes
			{
				none, small, medium, large, xlarge
			}

			public enum FloatTypes
			{
				left, right
			};

            public WidthTypes Width
            {
				get
				{
					try
					{
						return (WidthTypes)Enum.Parse(typeof(WidthTypes), this[0].Value1, true);
					}
					catch
					{
						return WidthTypes.none;
					}
				}
				set
				{
					this[0].Value1 = value.ToString("g");
				}
            }
            public FloatTypes Float
            {
                get
                {
					try
					{
						return (FloatTypes)Enum.Parse(typeof(FloatTypes), this[0].Value2, true);
					}
					catch
					{
						return FloatTypes.left;
					}
                }
                set
                {
                    this[0].Value2 = value.ToString("g");
                }
            }
			public override String GeoCode
			{
				get
				{
					return HttpContext.Current.Server.HtmlDecode(this[0].Value3).Replace("`", "'");
				}
				set
				{
					this[0].Value3 = HttpContext.Current.Server.HtmlEncode(value).Replace("'", "`");
				}
			}
			public override String TimeStamp
			{
				get
				{
					return HttpContext.Current.Server.HtmlDecode(this[0].Value4).Replace("`", "'");
				}
				set
				{
					this[0].Value4 = HttpContext.Current.Server.HtmlEncode(value).Replace("'", "`");
				}
			}

            public StandardModule(Int32 SitId, Int32 PagId, Int32 ModId) : base(SitId, PagId, ModId) { }
        }

		public class ContactModule : StandardModule
		{
			public String Title
			{
				get
				{
					return HttpContext.Current.Server.HtmlDecode(this[0].Value5).Replace("`", "'");
				}
				set
				{
					this[0].Value5 = HttpContext.Current.Server.HtmlEncode(value).Replace("'", "`");
				}
			}

			public String Text
			{
				get
				{
					return HttpContext.Current.Server.HtmlDecode(this[0].Value6).Replace("`", "'");
				}
				set
				{
					this[0].Value6 = HttpContext.Current.Server.HtmlEncode(value).Replace("'", "`");
				}
			}

			public String PolicyText
			{
				get
				{
					return HttpContext.Current.Server.HtmlDecode(this[0].Value7).Replace("`", "'");
				}
				set
				{
					this[0].Value7 = HttpContext.Current.Server.HtmlEncode(value).Replace("'", "`");
				}
			}

			public String ConfirmText
			{
				get
				{
					return HttpContext.Current.Server.HtmlDecode(this[0].Value8).Replace("`", "'");
				}
				set
				{
					this[0].Value8 = HttpContext.Current.Server.HtmlEncode(value).Replace("'", "`");
				}
			}

			public String EmailSubject
			{
				get
				{
					return HttpContext.Current.Server.HtmlDecode(this[0].Value9).Replace("`", "'");
				}
				set
				{
					this[0].Value9 = HttpContext.Current.Server.HtmlEncode(value).Replace("'", "`");
				}
			}

			public String EmailFromAddress
			{
				get
				{
					return HttpContext.Current.Server.HtmlDecode(this[0].Value10).Replace("`", "'");
				}
				set
				{
					this[0].Value10 = HttpContext.Current.Server.HtmlEncode(value).Replace("'", "`");
				}
			}

			public ContactModule(Int32 SitId, Int32 PagId, Int32 ModId) : base(SitId, PagId, ModId) { }
		}

		public class DividerModule : StandardModule
		{
			public int Height
			{
				get
				{
					try
					{
						return Convert.ToInt32(this[0].Value5);
					}
					catch
					{
						return 0;
					}
				}
				set
				{
					this[0].Value5 = value.ToString();
				}
			}

			public DividerModule(Int32 SitId, Int32 PagId, Int32 ModId) : base(SitId, PagId, ModId) { }
		}

		public class GiftcardModule : StandardModule
		{
			public bool Greeting
			{
				get
				{
					try
					{
						return Convert.ToBoolean(this[0].Value5);
					}
					catch
					{
						return false;
					}
				}
				set
				{
					this[0].Value5 = value.ToString();
				}
			}

			public String Pdf
			{
				get
				{
					return HttpContext.Current.Server.HtmlDecode(this[0].Value6).Replace("`", "'");
				}
				set
				{
					this[0].Value6 = HttpContext.Current.Server.HtmlEncode(value).Replace("'", "`");
				}
			}

			public GiftcardModule(Int32 SitId, Int32 PagId, Int32 ModId) : base(SitId, PagId, ModId) { }
		}

		public class PackageModule : StandardModule
		{
			public String Title
			{
				get
				{
					return HttpContext.Current.Server.HtmlDecode(this[0].Value5).Replace("`", "'");
				}
				set
				{
					this[0].Value5 = HttpContext.Current.Server.HtmlEncode(value).Replace("'", "`");
				}
			}

			public String Text
			{
				get
				{
					return HttpContext.Current.Server.HtmlDecode(this[0].Value6).Replace("`", "'");
				}
				set
				{
					this[0].Value6 = HttpContext.Current.Server.HtmlEncode(value).Replace("'", "`");
				}
			}

			public PackageModule(Int32 SitId, Int32 PagId, Int32 ModId) : base(SitId, PagId, ModId) { }
		}

		public class SubMenuBoxModule : StandardModule
		{
			public String Title
			{
				get
				{
					return HttpContext.Current.Server.HtmlDecode(this[0].Value5).Replace("`", "'");
				}
				set
				{
					this[0].Value5 = HttpContext.Current.Server.HtmlEncode(value).Replace("'", "`");
				}
			}

			public SubMenuBoxModule(Int32 SitId, Int32 PagId, Int32 ModId) : base(SitId, PagId, ModId) { }
		}

		public class EventListModule : StandardModule
		{
			public String Title
			{
				get
				{
					return HttpContext.Current.Server.HtmlDecode(this[0].Value5).Replace("`", "'");
				}
				set
				{
					this[0].Value5 = HttpContext.Current.Server.HtmlEncode(value).Replace("'", "`");
				}
			}

			public EventListModule(Int32 SitId, Int32 PagId, Int32 ModId) : base(SitId, PagId, ModId) { }
		}

		public class GoogleMapsModule : StandardModule
		{
			public String Lat
			{
				get
				{
					return HttpContext.Current.Server.HtmlDecode(this[0].Value5).Replace("`", "'");
				}
				set
				{
					this[0].Value5 = HttpContext.Current.Server.HtmlEncode(value).Replace("'", "`");
				}
			}

			public String Long
			{
				get
				{
					return HttpContext.Current.Server.HtmlDecode(this[0].Value6).Replace("`", "'");
				}
				set
				{
					this[0].Value6 = HttpContext.Current.Server.HtmlEncode(value).Replace("'", "`");
				}
			}

			public String MouseOverText
			{
				get
				{
					return HttpContext.Current.Server.HtmlDecode(this[0].Value7).Replace("`", "'");
				}
				set
				{
					this[0].Value7 = HttpContext.Current.Server.HtmlEncode(value).Replace("'", "`");
				}
			}

			public String InfoWindowText
			{
				get
				{
					return HttpContext.Current.Server.HtmlDecode(this[0].Value8).Replace("`", "'");
				}
				set
				{
					this[0].Value8 = HttpContext.Current.Server.HtmlEncode(value).Replace("'", "`");
				}
			}

			public GoogleMapsModule(Int32 SitId, Int32 PagId, Int32 ModId) : base(SitId, PagId, ModId) { }
		}

		public class EventModule : StandardModule
		{
			public String Title
			{
				get
				{
					return HttpContext.Current.Server.HtmlDecode(this[0].Value5).Replace("`", "'");
				}
				set
				{
					this[0].Value5 = HttpContext.Current.Server.HtmlEncode(value).Replace("'", "`");
				}
			}

			public String Text
			{
				get
				{
					return HttpContext.Current.Server.HtmlDecode(this[0].Value6).Replace("`", "'");
				}
				set
				{
					this[0].Value6 = HttpContext.Current.Server.HtmlEncode(value).Replace("'", "`");
				}
			}

			public EventModule(Int32 SitId, Int32 PagId, Int32 ModId) : base(SitId, PagId, ModId) { }
		}

		public class BlogModule : StandardModule
		{
			public String SelectedBlog
			{
				get
				{
					return HttpContext.Current.Server.HtmlDecode(this[0].Value5).Replace("`", "'");
				}
				set
				{
					this[0].Value5 = HttpContext.Current.Server.HtmlEncode(value).Replace("'", "`");
				}
			}

			public BlogModule(Int32 SitId, Int32 PagId, Int32 ModId) : base(SitId, PagId, ModId) { }
		}

        public class MediaModule : StandardModule
        {
			public enum LinkTypes
			{
				eventLink, internalLink
			}

			public String Media
			{
				get
				{
					return HttpContext.Current.Server.HtmlDecode(this[0].Value5).Replace("`", "'");
				}
				set
				{
					this[0].Value5 = HttpContext.Current.Server.HtmlEncode(value).Replace("'", "`");
				}
			}
			public String MediaType
			{
				get
				{
					return HttpContext.Current.Server.HtmlDecode(this[0].Value6).Replace("`", "'");
				}
				set
				{
					this[0].Value6 = HttpContext.Current.Server.HtmlEncode(value).Replace("'", "`");
				}
			}
			public bool MediaVisible
			{
				get
				{
					try
					{
						return Convert.ToBoolean(this[0].Value7);
					}
					catch
					{
						return true;
					}

				}
				set
				{
					this[0].Value7 = value.ToString();
				}
			}
			public String MediaToolTip
			{
				get
				{
					return HttpContext.Current.Server.HtmlDecode(this[0].Value8).Replace("`", "'");
				}
				set
				{
					this[0].Value8 = HttpContext.Current.Server.HtmlEncode(value).Replace("'", "`");
				}
			}
			public int ReadMoreLinkId
			{
				get
				{
					try
					{
						return Convert.ToInt32(this[0].Value9);
					}
					catch
					{
						return 0;
					}
				}
				set
				{
					this[0].Value9 = value.ToString();
				}
			}
			public bool MediaMobileSizeWarning
			{
				get
				{
					try
					{
						return Convert.ToBoolean(this[0].Value10);
					}
					catch
					{
						return true;
					}
				}
				set
				{
					this[0].Value10 = value.ToString();
				}
			}
			public LinkTypes ReadMoreLinkType
			{
				get
				{
					try
					{
						return (LinkTypes)Enum.Parse(typeof(LinkTypes), this[0].Value11, true);
					}
					catch
					{
						return LinkTypes.internalLink;
					}
				}
				set
				{
					this[0].Value11 = value.ToString("g");
				}
			}

            public MediaModule(Int32 SitId, Int32 PagId, Int32 ModId) : base(SitId, PagId, ModId) { }
        }

		public class ShopModule : StandardModule
		{
			public String Header
			{
				get
				{
					return HttpContext.Current.Server.HtmlDecode(this[0].Value5).Replace("`", "'");
				}
				set
				{
					this[0].Value5 = HttpContext.Current.Server.HtmlEncode(value).Replace("'", "`");
				}
			}
			public String Text
			{
				get
				{
					return HttpContext.Current.Server.HtmlDecode(this[0].Value6).Replace("`", "'");
				}
				set
				{
					this[0].Value6 = HttpContext.Current.Server.HtmlEncode(value).Replace("'", "`");
				}
			}

			public ShopModule(Int32 SitId, Int32 PagId, Int32 ModId) : base(SitId, PagId, ModId) { }
		}

		public class FeedModule : StandardModule
		{
			public String Title
			{
				get
				{
					return HttpContext.Current.Server.HtmlDecode(this[0].Value5).Replace("`", "'");
				}
				set
				{
					this[0].Value5 = HttpContext.Current.Server.HtmlEncode(value).Replace("'", "`");
				}
			}
			public bool Show
			{
				get
				{
					try
					{
						return Convert.ToBoolean(this[0].Value6);
					}
					catch
					{
						return false;
					}
				}
				set
				{
					this[0].Value6 = value.ToString(); ;
				}
			}
			public int Amount
			{
				get
				{
					try
					{
						return Convert.ToInt32(this[0].Value7);
					}
					catch
					{
						return -1;
					}

				}
				set
				{
					this[0].Value7 = value.ToString();
				}
			}
			public String Header
			{
				get
				{
					return HttpContext.Current.Server.HtmlDecode(this[0].Value8).Replace("`", "'");
				}
				set
				{
					this[0].Value8 = HttpContext.Current.Server.HtmlEncode(value).Replace("'", "`");
				}
			}
			public LiquidCore.List FeedUrls
			{
				get
				{
					return new List("Multi_RSS_Feeder_" + base.ModId.ToString());
				}
			}

			public void AddFeed(String url)
			{
				List.Item l = new List.Item();
				l.Alias = "Multi_RSS_Feeder_" + base.ModId.ToString();
				l.Value2 = url;
				l.Save();
			}
			public void DeleteFeed(Int32 Id)
			{
				List.Item l = new List.Item(Id);
				l.Delete();
			}

			public FeedModule(Int32 SitId, Int32 PagId, Int32 ModId) : base(SitId, PagId, ModId) { }
		}

		public class TeaserModule : StandardModule
		{
			public String Header
			{
				get
				{
					return HttpContext.Current.Server.HtmlDecode(this[0].Value5).Replace("`", "'");
				}
				set
				{
					this[0].Value5 = HttpContext.Current.Server.HtmlEncode(value).Replace("'", "`");
				}
			}
			public String Text
			{
				get
				{
					return HttpContext.Current.Server.HtmlDecode(this[0].Value6).Replace("`", "'");
				}
				set
				{
					this[0].Value6 = HttpContext.Current.Server.HtmlEncode(value).Replace("'", "`");
				}
			}
			public int ReadMoreLinkId
			{
				get
				{
					try
					{
						return Convert.ToInt32(this[0].Value8);
					}
					catch
					{
						return 0;
					}
				}
				set
				{
					this[0].Value8 = value.ToString();
				}
			}
			public String ReadMoreLink
			{
				get
				{
					return HttpContext.Current.Server.HtmlDecode(this[0].Value9).Replace("`", "'");
				}
				set
				{
					this[0].Value9 = HttpContext.Current.Server.HtmlEncode(value).Replace("'", "`");
				}
			}
			public String MediaToolTip
			{
				get
				{
					return HttpContext.Current.Server.HtmlDecode(this[0].Value10).Replace("`", "'");
				}
				set
				{
					this[0].Value10 = HttpContext.Current.Server.HtmlEncode(value).Replace("'", "`");
				}
			}
			public bool MediaVisible
			{
				get
				{
					try
					{
						return Convert.ToBoolean(this[0].Value11);
					}
					catch
					{
						return true;
					}
				}
				set
				{
					this[0].Value11 = value.ToString();
				}
			}
			public String Media
			{
				get
				{
					return HttpContext.Current.Server.HtmlDecode(this[0].Value13).Replace("`", "'");
				}
				set
				{
					this[0].Value13 = HttpContext.Current.Server.HtmlEncode(value).Replace("'", "`");
				}
			}
			public String MediaType
			{
				get
				{
					return HttpContext.Current.Server.HtmlDecode(this[0].Value14).Replace("`", "'");
				}
				set
				{
					this[0].Value14 = HttpContext.Current.Server.HtmlEncode(value).Replace("'", "`");
				}
			}
			public bool MediaMobileSizeWarning
			{
				get
				{
					try
					{
						return Convert.ToBoolean(this[0].Value15);
					}
					catch
					{
						return true;
					}
				}
				set
				{
					this[0].Value15 = value.ToString();
				}
			}

			public TeaserModule(Int32 SitId, Int32 PagId, Int32 ModId) : base(SitId, PagId, ModId) { }
		}

		public class TextModule : StandardModule
		{
			public String Header
			{
				get
				{
					return HttpContext.Current.Server.HtmlDecode(this[0].Value5).Replace("`", "'");
				}
				set
				{
					this[0].Value5 = HttpContext.Current.Server.HtmlEncode(value).Replace("'", "`");
				}
			}
			public String Introduction
			{
				get
				{
					return HttpContext.Current.Server.HtmlDecode(this[0].Value6).Replace("`", "'");
				}
				set
				{
					this[0].Value6 = HttpContext.Current.Server.HtmlEncode(value).Replace("'", "`");
				}
			}
			public String Text
			{
				get
				{
					return HttpContext.Current.Server.HtmlDecode(this[0].Value7).Replace("`", "'");
				}
				set
				{
					this[0].Value7 = HttpContext.Current.Server.HtmlEncode(value).Replace("'", "`");
				}
			}
			public bool ShareThis
			{
				get
				{
					try
					{
						return Convert.ToBoolean(this[0].Value8);
					}
					catch
					{
						return true;
					}
				}
				set
				{
					this[0].Value8 = value.ToString();
				}
			}

			public TextModule(Int32 SitId, Int32 PagId, Int32 ModId) : base(SitId, PagId, ModId) { }
		}

		public class YoutubeModule : StandardModule
		{
			public String VideoId
			{
				get
				{
					return HttpContext.Current.Server.HtmlDecode(this[0].Value5).Replace("`", "'");
				}
				set
				{
					this[0].Value5 = HttpContext.Current.Server.HtmlEncode(value).Replace("'", "`");
				}
			}
			public String Text
			{
				get
				{
					return HttpContext.Current.Server.HtmlDecode(this[0].Value6).Replace("`", "'");
				}
				set
				{
					this[0].Value6 = HttpContext.Current.Server.HtmlEncode(value).Replace("'", "`");
				}
			}

			public YoutubeModule(Int32 SitId, Int32 PagId, Int32 ModId) : base(SitId, PagId, ModId) { }
		}

		public class BookPackageModule : StandardModule
		{
			public String HotelzoneCode
			{
				get
				{
					return HttpContext.Current.Server.HtmlDecode(this[0].Value7).Replace("`", "'");
				}
				set
				{
					this[0].Value7 = HttpContext.Current.Server.HtmlEncode(value).Replace("'", "`");
				}
			}

			public BookPackageModule(Int32 SitId, Int32 PagId, Int32 ModId) : base(SitId, PagId, ModId) { }
		}

		public class Footer : StandardModule
		{
			public String CopyrightText
			{
				get
				{
					return HttpContext.Current.Server.HtmlDecode(this[0].Value5).Replace("`", "'");
				}
				set
				{
					this[0].Value5 = HttpContext.Current.Server.HtmlEncode(value).Replace("'", "`");
				}
			}
			public String GeneralInformationText
			{
				get
				{
					return HttpContext.Current.Server.HtmlDecode(this[0].Value6).Replace("`", "'");
				}
				set
				{
					this[0].Value6 = HttpContext.Current.Server.HtmlEncode(value).Replace("'", "`");
				}
			}
			public String NewsletterWindowHeader
			{
				get
				{
					return HttpContext.Current.Server.HtmlDecode(this[0].Value7).Replace("`", "'");
				}
				set
				{
					this[0].Value7 = HttpContext.Current.Server.HtmlEncode(value).Replace("'", "`");
				}
			}
			public String NewsletterWindowText
			{
				get
				{
					return HttpContext.Current.Server.HtmlDecode(this[0].Value8).Replace("`", "'");
				}
				set
				{
					this[0].Value8 = HttpContext.Current.Server.HtmlEncode(value).Replace("'", "`");
				}
			}
			public String NewsletterTerms
			{
				get
				{
					return HttpContext.Current.Server.HtmlDecode(this[0].Value9).Replace("`", "'");
				}
				set
				{
					this[0].Value9 = HttpContext.Current.Server.HtmlEncode(value).Replace("'", "`");
				}
			}
			public String NewsletterThanksHeader
			{
				get
				{
					return HttpContext.Current.Server.HtmlDecode(this[0].Value10).Replace("`", "'");
				}
				set
				{
					this[0].Value10 = HttpContext.Current.Server.HtmlEncode(value).Replace("'", "`");
				}
			}
			public String NewsletterThanksText
			{
				get
				{
					return HttpContext.Current.Server.HtmlDecode(this[0].Value11).Replace("`", "'");
				}
				set
				{
					this[0].Value11 = HttpContext.Current.Server.HtmlEncode(value).Replace("'", "`");
				}
			}

			public Footer(Int32 SitId, Int32 PagId, Int32 ModId) : base(SitId, PagId, ModId) { }
		}

		public class SearchModule : StandardModule
		{
			public String Header
			{
				get
				{
					return HttpContext.Current.Server.HtmlDecode(this[0].Value5).Replace("`", "'");
				}
				set
				{
					this[0].Value5 = HttpContext.Current.Server.HtmlEncode(value).Replace("'", "`");
				}
			}

			public SearchModule(Int32 SitId, Int32 PagId, Int32 ModId) : base(SitId, PagId, ModId) { }
		}
    }
}

// RXServer.Lib
namespace RXServer
{
    namespace Lib
    {
        public class Generic : LiquidCore.Generic
        {
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
            public static void Log(String t)
            {
                try
                {
                    StreamWriter SW = File.AppendText(HttpContext.Current.Server.MapPath(@"/Rss\\") + "log.txt");
                    SW.WriteLine(DateTime.Now + " - " + t);
                    SW.Close();

                }
                catch (InvalidOperationException e)
                { 
                
                }
            }
            public static void ResetCache(String CachePreValue)
            {
                String cacheItem = String.Empty;
                IDictionaryEnumerator CacheEnum = HttpContext.Current.Cache.GetEnumerator();
                while (CacheEnum.MoveNext())
                {
                    cacheItem = CacheEnum.Key.ToString();
                    if (cacheItem.StartsWith(CachePreValue))
                        HttpContext.Current.Cache.Remove(cacheItem);
                }
            }
        }

        public class Replace
        {
            static string CLASSNAME = "[Namespace::RXServer::Web::Parse][Class::Replace]";
        }

        public class Rss
        {
            string CLASSNAME = "[Namespace::RXServer.Lib][Class::Rss]";
            private static String rssfile = String.Empty;
            private string _title = "RXServer Error Handler";
            private string _link = "http://www.noisycricket.se/rss/";
            private string _description = "Error information in rss format.";
            private string _copyright = "Copyright 2008 Johan Olofsson";
            private string _generator = "RXServer RssCreator 1.0";
            public Rss()
            {
                rssfile = ConfigurationManager.AppSettings["SystemRssFilePath"].ToString();
            }
            public Rss(String FilePath)
            {
                rssfile = FilePath;
                StreamWriter sw = File.CreateText(rssfile);
                sw.Write(String.Empty);
                sw.Close();
            }
            public Rss(String FilePath, String Title, String Link, String Description, String Copyright, String Generator)
            {
                _title = Title;
                _link = Link;
                _description = Description;
                _copyright = Copyright;
                _generator = Generator;
                rssfile = FilePath;
                StreamWriter sw = File.CreateText(rssfile);
                sw.Write(String.Empty);
                sw.Close();
            }

            private Stream CreateNew()
            {
                XmlTextWriter writer = new XmlTextWriter(rssfile, System.Text.Encoding.UTF8);
                this.OpenRss(writer);
                this.CloseRss(writer);
                writer.Flush();
                writer.Close();
                return writer.BaseStream;
            }

            public void AddItem(String Title, String Link, String Description, String PubDate)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::AddItem]";
                if (File.Exists(rssfile))
                {
                    StreamReader sr = File.OpenText(rssfile);
                    long i = sr.BaseStream.Length;
                    String orgdata = sr.ReadToEnd();
                    sr.Close();
                    if (i > 1)
                    {
                        Int32 Index = orgdata.IndexOf("</channel>");
                        if (Index > 1)
                        {
                            orgdata = orgdata.Substring(0, Index);
                            using (StreamWriter sw = File.CreateText(rssfile))
                            {
                                sw.Write(orgdata);
                                sw.Write("<item>");
                                sw.Write("<title>" + HttpContext.Current.Server.HtmlEncode(Title) + "</title>");
                                sw.Write("<description><![CDATA[ " + HttpContext.Current.Server.HtmlEncode(Description) + " ]]></description>");
                                sw.Write("<link>" + Link + "</link>");
                                sw.Write("<pubDate>" + Convert.ToDateTime(PubDate).ToString("r") + "</pubDate>");
                                sw.Write("</item>");
                                sw.Write("</channel>");
                                sw.Write("</rss>");
                                sw.Close();
                            }
                            return;
                        }
                    }
                }

                XmlTextWriter writer = new XmlTextWriter(rssfile, System.Text.Encoding.UTF8);
                this.OpenRss(writer);
                this.sAddItem(writer, Title, Link, Description, PubDate, true);
                this.CloseRss(writer);
                writer.Flush();
                writer.Close();
            }

            public void AddItem(String Title, String Link, String Description)
            {
                this.AddItem(Title, Link, Description, String.Empty);
            }

            private void AddItemToRss()
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::AddItemToRss]";
            }

            private XmlTextWriter OpenRss(XmlTextWriter writer)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::OpenRss]";
                writer.WriteStartDocument();
                writer.WriteComment("Generated at " + DateTime.Now.ToString("r"));
                writer.WriteStartElement("rss");
                writer.WriteAttributeString("version", "2.0");
                writer.WriteAttributeString("xmlns:mscom", _link);
                writer.WriteStartElement("channel");
                writer.WriteElementString("title", _title);
                writer.WriteElementString("link", _link);
                writer.WriteElementString("description", _description);
                writer.WriteElementString("copyright", _copyright);
                writer.WriteElementString("generator", _generator);

                return writer;
            }

            private XmlTextWriter sAddItem(XmlTextWriter writer, string sItemTitle, string sItemLink, string sItemDescription, string sPubDate)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::sAddItem]";
                writer.WriteStartElement("item");
                writer.WriteElementString("title", sItemTitle);
                writer.WriteElementString("link", sItemLink);

                writer.WriteElementString("description", sItemDescription);

                writer.WriteElementString("pubDate", Convert.ToDateTime(sPubDate).ToString("r"));
                writer.WriteEndElement();

                return writer;
            }

            private XmlTextWriter sAddItem(XmlTextWriter writer, string sItemTitle, string sItemLink, string sItemDescription, string sPubDate, bool bDescAsCDATA)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::sAddItem]";
                writer.WriteStartElement("item");
                writer.WriteElementString("title", sItemTitle);
                writer.WriteElementString("link", sItemLink);

                if (bDescAsCDATA == true)
                {
                    // Now we can write the description as CDATA to support html content.
                    // We find this used quite often in aggregators

                    writer.WriteStartElement("description");
                    writer.WriteCData(sItemDescription);
                    writer.WriteEndElement();
                }
                else
                {
                    writer.WriteElementString("description", sItemDescription);
                }

                writer.WriteElementString("pubDate", Convert.ToDateTime(sPubDate).ToString("r"));
                writer.WriteEndElement();

                return writer;
            }

            private XmlTextWriter CloseRss(XmlTextWriter writer)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::CloseRss]";
                writer.WriteEndElement();
                writer.WriteEndElement();
                writer.WriteEndDocument();

                return writer;
            }
        }
        public class Error
        {
            public static void Report(Exception ex, String function, String variant)
            {
                try
                {
                    if (Convert.ToBoolean(ConfigurationManager.AppSettings["Error.MailOn"]))
                        EventMail(ex, function, variant);
                }
                catch (Exception)
                {
                }
                try
                {
                    if (Convert.ToBoolean(ConfigurationManager.AppSettings["Error.RssOn"]))
                        EventRss(ex, function, variant);
                }
                catch (Exception)
                {
                }
                try
                {
                    EventLog(ex, function, variant);
                    System.Diagnostics.Debug.WriteLine(ex.Message.ToString());
                    System.Diagnostics.Debug.WriteLine(function);
                    System.Diagnostics.Debug.WriteLine(variant);
                }
                catch (Exception)
                {
                }
            }

            private static void EventLog(Exception ex, String function, String variant)
            {
                try
                {
                    System.Diagnostics.EventLog.WriteEntry(
                        "RXServer4.0",
                        ex.GetType().ToString() + "occured in " + function + "\r\nSource: " + ex.Source + "\r\nMessage: " + ex.Message + "\r\nVersion: " + System.Reflection.Assembly.GetExecutingAssembly().CodeBase + "\r\nCaller: " + System.Reflection.Assembly.GetCallingAssembly().CodeBase + "\r\nStack trace: " + ex.StackTrace +
                        "\r\nVariant Data: " + variant,
                        System.Diagnostics.EventLogEntryType.Error
                    );
                }
                catch (Exception)
                {

                }
            }

            private static void EventRss(Exception ex, String function, String variant)
            {
                try
                {
                    Rss oRss = new Rss();
                    oRss.AddItem(ex.GetType().ToString() + "occured in " + function,
                        "Source: " + ex.Source + "\r\nMessage: " + ex.Message + "\r\nVersion: " + System.Reflection.Assembly.GetExecutingAssembly().CodeBase + "\r\nCaller: " + System.Reflection.Assembly.GetCallingAssembly().CodeBase + "\r\nStack trace: " + ex.StackTrace +
                        "<br/><br/>Variant Data: " + variant,
                        "");
                }
                catch (Exception)
                {
                }
            }

            private static void EventMail(Exception ex, String function, String variant)
            {
                try
                {
                    RXServer.Lib.Generic.SendMail(ConfigurationManager.AppSettings["Error.MailServer"].ToString(),
                        ConfigurationManager.AppSettings["Error.MailPort"].ToString(),
                        ConfigurationManager.AppSettings["Error.MailSender"].ToString(),
                        ConfigurationManager.AppSettings["Error.MailAddress"].ToString(),
                        "An Exception was thrown in the application RXServer4...",
                        "In function/routine: " + function + "<br/><br/>" + ex.Message +
                        "<br/><br/>Version: " + System.Reflection.Assembly.GetExecutingAssembly().CodeBase + "<br/>Caller: " + System.Reflection.Assembly.GetCallingAssembly().CodeBase + "<br/>Stack trace: " + ex.StackTrace +
                        "<br/><br/>Variant Data: " + variant,
                        null);
                }
                catch (Exception)
                {
                }
            }
        }
        public class Settings : LiquidCore.Settings
        {
            static string CLASSNAME = "[Namespace::RXServer.Lib][Class::Settings]";
            static string UID = "RXServer.Lib.Settings.UserDefined";
            public Settings() : base(UID) { }
            public System.Object GetValue(String Title)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::GetValue]";
                try
                {
                    foreach (LiquidCore.Setting s in this)
                    {
                        if (s.Title.Equals(Title))
                            return s.Value;
                    }
                    return "not defined";
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, String.Empty);
                    return string.Empty;
                }
            }
            public void Add(String Title, String Value)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::Add]";
                try
                {
                    LiquidCore.Setting x = null;
                    foreach (LiquidCore.Setting s in this)
                        if (s.Title.Equals(Title))
                            x = s;
                    if (x == null)
                        x = new Setting();
                    x.SitId = 1;
                    x.PagId = 1;
                    x.ModId = 1;
                    x.ParentId = 0;
                    x.Status = 1;
                    x.Language = 1;
                    x.Alias = UID;
                    x.Title = Title;
                    x.Value = Value;
                    x.Save();
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, String.Empty);
                }
            }
            public void Remove(String Title)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::Remove]";
                try
                {
                    foreach (LiquidCore.Setting s in this)
                        if (s.Title.Equals(Title))
                            s.Delete();
                }
                catch (Exception ex)
                {
                    Error.Report(ex, FUNCTIONNAME, String.Empty);
                }
            }
        }
        public partial class Model
        {
            static string CLASSNAME = "[Namespace::RXServer.Lib][Class::Model]";

            public Model()
            {
                //
                // TODO: Add constructor logic here
                //
            }

            public static void CheckPagesForModelUpdate(Int32 SitId, Int32 PagParentId, Int32 MdlId)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::CheckPagesForModelUpdate]";
                try
                {
                    using (LiquidCore.Pages lp = new LiquidCore.Pages(SitId, PagParentId))
                    {
                        foreach (LiquidCore.Page p in lp)
                        {
                            CheckPagesForModelUpdate(SitId, p.Id, MdlId);

                            if (p.ModelId.Equals(MdlId))
                            {
                                // Leta upp alla moduler som är skapaded av "system" och raderar dem
                                // tillsammans med dess object om det finns några...
                                using (LiquidCore.Modules lm = new LiquidCore.Modules(SitId, p.Id, 0, false))
                                {
                                    foreach (LiquidCore.Module m in lm)
                                    {
                                        if (m.Alias.Equals("CREATED_BY_MODEL_SYSTEM"))
                                        {
                                            using (LiquidCore.Objects lo = new LiquidCore.Objects(SitId, p.Id, m.Id, 0))
                                            {
                                                foreach (LiquidCore.Object o in lo)
                                                    o.Delete();
                                            }
                                            m.Delete();
                                        }
                                    }
                                }

                                // Lägger in nya moduler enligt Model...
                                using (LiquidCore.ModelItems mi = new LiquidCore.ModelItems(SitId, MdlId, 0))
                                {
                                    foreach (LiquidCore.ModelItem i in mi)
                                    {
                                        using (LiquidCore.Module m = new LiquidCore.Module())
                                        {
                                            String Src = String.Empty;
                                            String Title = String.Empty;
                                            using (LiquidCore.ModDef md = new LiquidCore.ModDef(i.MdeId))
                                            {
                                                if (md.Id.Equals(0))
                                                    throw new Exception("bad programmer...");
                                                Src = md.Src;
                                                Title = md.Title;
                                            }
                                            m.Language = 1;
                                            m.Status = 1;
                                            m.SitId = SitId;
                                            m.PagId = p.Id;
                                            m.MdeId = i.MdeId;
                                            m.Title = Title;
                                            m.Alias = "CREATED_BY_MODEL_SYSTEM";
                                            m.ContentPane = i.ContentPane;
                                            m.Src = Src;
                                            m.Save();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    LiquidCore.Error.Report(ex, FUNCTIONNAME, "");
                }
            }

            public static void CheckPageForModelUpdate(Int32 SitId, Int32 PagId, Int32 MdlId)
            {
                string FUNCTIONNAME = CLASSNAME + "[Function::CheckPageForModelUpdate]";
                try
                {
                    using (LiquidCore.Page p = new LiquidCore.Page(PagId))
                    {
                        if (p.ModelId.Equals(MdlId))
                        {
                            // Leta upp alla moduler som är skapaded av "system" och raderar dem
                            // tillsammans med dess object om det finns några...
                            using (LiquidCore.Modules lm = new LiquidCore.Modules(SitId, p.Id, 0, false))
                            {
                                foreach (LiquidCore.Module m in lm)
                                {
                                    if (m.Alias.Equals("CREATED_BY_MODEL_SYSTEM"))
                                    {
                                        using (LiquidCore.Objects lo = new LiquidCore.Objects(SitId, p.Id, m.Id, 0))
                                        {
                                            foreach (LiquidCore.Object o in lo)
                                                o.Delete();
                                        }
                                        m.Delete();
                                    }
                                }
                            }
                            // Lägger in nya moduler enligt Model...
                            using (LiquidCore.ModelItems mi = new LiquidCore.ModelItems(SitId, MdlId, 0))
                            {
                                foreach (LiquidCore.ModelItem i in mi)
                                {
                                    using (LiquidCore.Module m = new LiquidCore.Module())
                                    {
                                        String Src = String.Empty;
                                        String Title = String.Empty;
                                        using (LiquidCore.ModDef md = new LiquidCore.ModDef(i.MdeId))
                                        {
                                            if (md.Id.Equals(0))
                                                throw new Exception("bad programmer...");
                                            Src = md.Src;
                                            Title = md.Title;
                                        }
                                        m.Language = 1;
                                        m.Status = 1;
                                        m.SitId = SitId;
                                        m.PagId = p.Id;
                                        m.MdeId = i.MdeId;
                                        m.Title = Title;
                                        m.Alias = "CREATED_BY_MODEL_SYSTEM";
                                        m.ContentPane = i.ContentPane;
                                        m.Src = Src;
                                        m.Save();
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    LiquidCore.Error.Report(ex, FUNCTIONNAME, "");
                }
            }

            public class ModuleDefinitions : ModDefs
            {
                public ModuleDefinitions(Int32 SitId)
                    : base(SitId, 0)
                { }
                public class ModuleDefinition : ModDef
                {
                    public ModuleDefinition(Int32 MdeId)
                        : base(MdeId)
                    { }
                    public ModuleDefinition()
                        : base()
                    { }
                }
            }

        }

        /// <summary>
        /// Can be called or parsed like the following:
        /// ((RXServer.Lib.RXBasePage)this.Page).CurrentPage
        /// ((RXServer.Lib.RXBasePage)this.Page).CurrentTopMenu
        /// ((RXServer.Lib.RXBasePage)HttpContext.Current.CurrentHandler).CurrentPage
        /// </summary>
		public class RXBasePage : RXDefaultPage
        {
			string CLASSNAME = "[Namespace::RXServer][Class::RXBasePage]";
			private RXServer.Modules.Site _currentsite;
			private RXServer.Modules.Menu _currenttopmenu;
			private RXServer.Modules.Menu.Item _currentpage;
			private Int32 _sitid = 1;
			private Int32 _pagid = 1;
			private string runtimeMasterPageFile = "";

			public RXServer.Modules.Site CurrentSite
			{
				get
				{
					return this._currentsite;
				}
				set
				{
					this._currentsite = value;
				}
			}
			public RXServer.Modules.Menu.Item CurrentPage
			{
				get
				{
					return this._currentpage;
				}
				set
				{
					this._currentpage = value;
				}
			}
			public RXServer.Modules.Menu CurrentTopMenu
			{
				get
				{
					return this._currenttopmenu;
				}
				set
				{
					this._currenttopmenu = value;
				}
			}
			public Int32 SitId
			{
				get
				{
					return this._sitid;
				}
				set
				{
					this._sitid = value;
				}
			}
			public Int32 PagId
			{
				get
				{
					return this._pagid;
				}
				set
				{
					this._pagid = value;
				}
			}
			public string RuntimeMasterPageFile
			{
				get
				{
					return runtimeMasterPageFile;
				}
				set
				{
					runtimeMasterPageFile = value;
				}
			}

			protected override void OnPreInit(EventArgs e)
			{
				if (runtimeMasterPageFile != null)
				{
					this.MasterPageFile = runtimeMasterPageFile;
				}

				base.OnPreInit(e);
			}
			public virtual void Page_PreInit(object sender, EventArgs e)
			{
				string FUNCTIONNAME = CLASSNAME + "[Function::Page_PreInit]";
				try
				{
					this.SitId = 1;

					// Läser av eventuell Request PagId...
					this.PagId = RXServer.Web.RequestValues.PagId;

					// Kolla om den är satt till noll, i så fall ändra till 1...
					if (this.PagId.Equals(0))
						this.PagId = 1;

					// Sätter nuvarande SitId och PagId, som sen kan läsas av...
					RXServer.Web.CurrentValues.SitId = this.SitId;
					RXServer.Web.CurrentValues.PagId = this.PagId;

					// Hämtar den rätta sidan och läser av MasterPage (Template.master) ...
					this.CurrentSite = new RXServer.Modules.Site(RXServer.Web.CurrentValues.SitId);

					// Hämtar topmenu...
					this.CurrentTopMenu = new RXServer.Modules.Menu(1, 0, 1);

					// Hämtar nuvarande Page/MenuItem...
					this.CurrentPage = new RXServer.Modules.Menu.Item(RXServer.Web.CurrentValues.PagId);

					if (this.CurrentPage.Id == 0)
					{
						if (!ConfigurationManager.AppSettings["FriendlyUrl.Helicon"].Equals("true") && !ConfigurationManager.AppSettings["FriendlyUrl.Rewrite"].Equals("true"))
						{
							RXServer.Web.Redirect.To("~/Default.aspx");
						}
						else
						{
							RXServer.Web.Redirect.To(RXServer.Lib.Common.Dynamic.GetFriendlyUrl(1));
						}
						return;
					}

					if (this.CurrentPage.Id > 1 && !RXServer.Auth.HasCurrentRoleViewRights(this.CurrentPage.Id))
					{
						if (!ConfigurationManager.AppSettings["FriendlyUrl.Helicon"].Equals("true") && !ConfigurationManager.AppSettings["FriendlyUrl.Rewrite"].Equals("true"))
						{
							RXServer.Web.Redirect.To("~/Default.aspx");
						}
						else
						{
							RXServer.Web.Redirect.To(RXServer.Lib.Common.Dynamic.GetFriendlyUrl(1));
						}
						return;
					}

					int template;
					bool number = Int32.TryParse(this.CurrentPage.Template, out template);

					if (IsMobile())
					{
						runtimeMasterPageFile = "~/Structure/" + this.CurrentSite.Structure + "/MobileTemplate.master";
					}
					else if (number)
					{
						runtimeMasterPageFile = "~/Structure/" + this.CurrentSite.Structure + "/DynamicTemplate.master";
					}
					else
					{
						runtimeMasterPageFile = "~/Structure/" + this.CurrentSite.Structure + "/" + this.CurrentPage.Template;
					}

					if (runtimeMasterPageFile != null)
						this.MasterPageFile = runtimeMasterPageFile;

					if (IsMobile())
					{
						this.Theme = "RXSKMobile";
					}
					else
					{
					this.Theme = this.CurrentSite.Theme;
					}

					if (!this.CurrentPage.MetaTitle.Equals(String.Empty))
						this.Title = Server.HtmlDecode(this.CurrentPage.MetaTitle);
					else
						this.Title = System.Configuration.ConfigurationManager.AppSettings["META.Title"].ToString();

					// Sätter alla nuvarande levels, där Level1 är root...
					RXServer.Web.SelectedPages.SetSelected();
				}
				catch (Exception ex)
				{
					System.Diagnostics.Debug.WriteLine(ex.ToString());
				}
			}

			public virtual void Page_Init(object sender, EventArgs e)
			{
				string FUNCTIONNAME = CLASSNAME + "[Function::Page_Init]";
				try
				{
					Boolean ModSSL = false;

					// används för SLL...
					String INSSL = "0";
					if (Session["INSSL"] != null)
						INSSL = Session["INSSL"].ToString();

					int template;
					int pagId = RXServer.Web.RequestValues.PagId;
					if (pagId == 0)
					{
						pagId = 1;
					}
					RXServer.Modules.Menu.Item page = new RXServer.Modules.Menu.Item(pagId);
					bool number = Int32.TryParse(page.Template, out template);
					if (number && !IsMobile())
					{
						BuildTemplate(template);
					}

					if (IsMobile())
					{
						ModSSL = PlaceModulesMobile();
					}
					else
					{
						ModSSL = PlaceModules();
					}

					// Här kontrolleras SLL och QueryStrings
					// om urlen behöver skrivas om så sker det.
					// Tänk på att filenerna som ska användas är följande:
					// Default.aspx och SLL.aspx och båda två ska ligga
					// under root.
					if (RXServer.Web.CurrentValues.PagId > 0)
					{
						if (ModSSL && INSSL.Equals("0"))
						{
							Session["INSSL"] = "1";
							Response.Redirect("https://" + Request.Url.Host + Request.Url.AbsolutePath.Replace("/Default.aspx", "/(S(" + HttpContext.Current.Session.SessionID + "))/SSL.aspx") + Request.Url.Query, false);
						}
						else if (INSSL.Equals("1") && Request.Url.AbsolutePath.EndsWith("Default.aspx"))
						{
							Session["INSSL"] = "1";
							Response.Redirect("https://" + Request.Url.Host + Request.Url.AbsolutePath.Replace("/Default.aspx", "/(S(" + HttpContext.Current.Session.SessionID + "))/SSL.aspx") + Request.Url.Query, false);
						}
						else if (!ModSSL && INSSL.Equals("1"))
						{
							Session["INSSL"] = "0";
							Response.Redirect("http://" + Request.Url.Host + Request.Url.AbsolutePath.Replace("/SSL.aspx", "/(S(" + HttpContext.Current.Session.SessionID + "))/Default.aspx") + Request.Url.Query, false);
						}

						// Sätter Meta Data
						HtmlHead PageHead = (HtmlHead)Common.FindControlRecursive(this.Master, "PageHead");

						if (!this.CurrentPage.MetaDescription.Equals(String.Empty))
							PageHead.Controls.AddAt(3, new LiteralControl("<meta name=\"description\" content=\"" + Server.HtmlDecode(this.CurrentPage.MetaDescription) + "\" />"));
						else
							PageHead.Controls.AddAt(3, new LiteralControl("<meta name=\"description\" content=\"" + System.Configuration.ConfigurationManager.AppSettings["META.Description"].ToString() + "\" />"));

						if (!this.CurrentPage.MetaKeywords.Equals(String.Empty))
							PageHead.Controls.AddAt(4, new LiteralControl("<meta name=\"keywords\" content=\"" + Server.HtmlDecode(this.CurrentPage.MetaKeywords) + "\" />"));
						else
							PageHead.Controls.AddAt(4, new LiteralControl("<meta name=\"keywords\" content=\"" + System.Configuration.ConfigurationManager.AppSettings["META.KeyWords"].ToString() + "\" />"));

						if (!this.CurrentPage.MetaCopyright.Equals(String.Empty))
							PageHead.Controls.AddAt(5, new LiteralControl("<meta name=\"copyright\" content=\"" + Server.HtmlDecode(this.CurrentPage.MetaCopyright) + "\" />"));
						else
							PageHead.Controls.AddAt(5, new LiteralControl("<meta name=\"copyright\" content=\"" + System.Configuration.ConfigurationManager.AppSettings["META.Copyright"].ToString() + "\" />"));

						if (!this.CurrentPage.MetaAuthor.Equals(String.Empty))
							PageHead.Controls.AddAt(6, new LiteralControl("<meta name=\"author\" content=\"" + Server.HtmlDecode(this.CurrentPage.MetaAuthor) + "\" />"));
						else
							PageHead.Controls.AddAt(6, new LiteralControl("<meta name=\"author\" content=\"" + System.Configuration.ConfigurationManager.AppSettings["META.Author"].ToString() + "\" />"));

						if (!this.CurrentPage.MetaRobots.Equals(String.Empty))
							PageHead.Controls.AddAt(7, new LiteralControl("<meta name=\"robots\" content=\"" + Server.HtmlDecode(this.CurrentPage.MetaRobots) + "\" />"));
						else
							PageHead.Controls.AddAt(7, new LiteralControl("<meta name=\"robots\" content=\"" + System.Configuration.ConfigurationManager.AppSettings["META.Robots"].ToString() + "\" />"));

						// Sätter javascript...
						//HtmlHead PageFoot = (HtmlHead)Common.FindControlRecursive(this.Master, "PageFoot");
						string _urlprefix = RXServer.Lib.Common.Dynamic.CreateUrlPrefix();
						PageHead.Controls.AddAt(8, new LiteralControl("<script type='text/javascript' charset='ISO-8859-1' src='" + _urlprefix + "JS/jquery-1.7.2.min.js'></script>"));
						PageHead.Controls.AddAt(9, new LiteralControl("<script type='text/javascript' charset='ISO-8859-1' src='" + _urlprefix + "JS/cufon-yui.mini.js'></script>"));
						PageHead.Controls.AddAt(10, new LiteralControl("<script type='text/javascript' charset='ISO-8859-1' src='" + _urlprefix + "JS/jquery.ext.mini.js'></script>"));
						PageHead.Controls.AddAt(11, new LiteralControl("<script type='text/javascript' charset='ISO-8859-1' src='" + _urlprefix + "JS/jquery.bubblepopup.v2.3.1.min.js'></script>"));
						PageHead.Controls.AddAt(12, new LiteralControl("<script type='text/javascript' src='http://maps.google.com/maps/api/js?sensor=false'></script>"));
						//PageHead.Controls.AddAt(5, new LiteralControl("<script type='text/javascript' charset='ISO-8859-1'>Cufon.replace('h2', { fontFamily: 'HelveticaNeue' });</script>"));
						//PageHead.Controls.AddAt(7, new LiteralControl("<script type='text/javascript' charset='ISO-8859-1' src='" + _urlprefix + "JS/HelveticaNeue_400-HelveticaNeue_400.font.js'></script>"));

					}
				}
				catch (Exception ex)
				{
					LiquidCore.Error.Report(ex, FUNCTIONNAME, String.Empty);
				}
			}

			private bool PlaceModules()
			{
				string FUNCTIONNAME = CLASSNAME + "[Function::PlaceModules]";
				try
				{
					Boolean AdminMode = RXServer.Auth.HasCurrentRoleEditRights(RXServer.Web.RequestValues.PagId);
					Boolean ModSSL = false;
					foreach (LiquidCore.Module m in this.CurrentPage.Modules)
					{
						Control c = Common.FindControlRecursive(this, m.ContentPane);
						if (c != null)
						{
							if (m.SSL)
								ModSSL = true;
							if (m.CacheTime > 0 && !AdminMode)
							{
								RXBaseCachedModule mg = new RXBaseCachedModule();
								mg.SitId = m.SitId;
								mg.PagId = m.PagId;
								mg.ModId = m.Id;
								mg.Src = m.Src;
								mg.CacheTime = m.CacheTime;
								mg.Hidden = m.Hidden;
								mg.Mobile = false;
								foreach (int ar in m.AuthorizedRoles) mg.AuthorizedEditRoles += ar.ToString();
								LiquidCore.ModDef def = new ModDef(m.MdeId);
								RenderContentControl(c, mg, AdminMode, mg.SitId, mg.PagId, mg.ModId, m.MdeId, m.Hidden);
								System.Diagnostics.Debug.WriteLine("Render (cached) module with SitId=" + m.SitId.ToString() + ", PagId=" + m.PagId.ToString() + ", ModId=" + m.Id.ToString());
							}
							else
							{
								if (!m.Src.Equals(String.Empty))
								{
									RXBaseModule mg = (RXBaseModule)this.Page.LoadControl(m.Src);
									mg.SitId = m.SitId;
									mg.PagId = m.PagId;
									mg.ModId = m.Id;
									mg.Src = m.Src;
									mg.CacheTime = m.CacheTime;
									mg.Hidden = m.Hidden;
									foreach (int ar in m.AuthorizedRoles) mg.AuthorizedEditRoles += ar.ToString();
									LiquidCore.ModDef def = new ModDef(m.MdeId);
									RenderContentControl(c, mg, AdminMode, mg.SitId, mg.PagId, mg.ModId, m.MdeId, m.Hidden);
									System.Diagnostics.Debug.WriteLine("Render module with SitId=" + m.SitId.ToString() + ", PagId=" + m.PagId.ToString() + ", ModId=" + m.Id.ToString());
								}
							}
						}
					}
					return ModSSL;
				}
				catch (Exception ex)
				{
					LiquidCore.Error.Report(ex, FUNCTIONNAME, String.Empty);
					return false;
				}
			}

			private bool PlaceModulesMobile()
			{
				string FUNCTIONNAME = CLASSNAME + "[Function::PlaceModulesMobile]";
				try
				{
					Boolean AdminMode = RXServer.Auth.HasCurrentRoleEditRights(RXServer.Web.RequestValues.PagId);
					Boolean ModSSL = false;
					LiquidCore.Modules modules = this.CurrentPage.Modules;
					modules.Sort(LiquidCore.LiquidCore.Definition.ModulesDefinition.SortParamEnum.MobileOrder, LiquidCore.LiquidCore.Definition.ModulesDefinition.SortOrderEnum.Ascending);
					foreach (LiquidCore.Module m in modules)
					{
						Control c = Common.FindControlRecursive(this, m.ContentPane);
						if (m.SSL)
							ModSSL = true;
						if (m.CacheTime > 0 && !AdminMode)
						{
							RXBaseCachedModule mg = new RXBaseCachedModule();
							mg.SitId = m.SitId;
							mg.PagId = m.PagId;
							mg.ModId = m.Id;
							mg.Src = m.Src;
							mg.CacheTime = m.CacheTime;
							mg.Hidden = m.MobileHidden;
							mg.Mobile = true;
							foreach (int ar in m.AuthorizedRoles) mg.AuthorizedEditRoles += ar.ToString();
							int contentPane;
							LiquidCore.ModDef def = new ModDef(m.MdeId);
							if (Int32.TryParse(m.ContentPane, out contentPane) && def.Status == 9)
							{
								RenderContentControlMobile(mg, mg.SitId, mg.PagId, mg.ModId, m.MdeId, AdminMode, m.MobileHidden);
							}
							else if (c != null)
							{
								RenderContentControl(c, mg, AdminMode, mg.SitId, mg.PagId, mg.ModId, m.MdeId, m.MobileHidden);
							}
							System.Diagnostics.Debug.WriteLine("Render (cached) module with SitId=" + m.SitId.ToString() + ", PagId=" + m.PagId.ToString() + ", ModId=" + m.Id.ToString());
						}
						else
						{
							if (!m.Src.Equals(String.Empty))
							{
								RXBaseModule mg = (RXBaseModule)this.Page.LoadControl(m.Src);
								mg.SitId = m.SitId;
								mg.PagId = m.PagId;
								mg.ModId = m.Id;
								mg.Src = m.Src;
								mg.CacheTime = m.CacheTime;
								mg.Hidden = m.MobileHidden;
								foreach (int ar in m.AuthorizedRoles) mg.AuthorizedEditRoles += ar.ToString();
								int contentPane;
								LiquidCore.ModDef def = new ModDef(m.MdeId);
								if (Int32.TryParse(m.ContentPane, out contentPane) && def.Status == 9)
								{
									RenderContentControlMobile(mg, mg.SitId, mg.PagId, mg.ModId, m.MdeId, AdminMode, m.MobileHidden);
								}
								else if (c != null)
								{
									RenderContentControl(c, mg, AdminMode, mg.SitId, mg.PagId, mg.ModId, m.MdeId, m.MobileHidden);
								}
								System.Diagnostics.Debug.WriteLine("Render module with SitId=" + m.SitId.ToString() + ", PagId=" + m.PagId.ToString() + ", ModId=" + m.Id.ToString());
							}
						}
					}
					return ModSSL;
				}
				catch (Exception ex)
				{
					LiquidCore.Error.Report(ex, FUNCTIONNAME, String.Empty);
					return false;
				}
			}

			public bool IsMobile()
			{
				string FUNCTIONNAME = CLASSNAME + "[Function::IsMobile]";
				try
				{
					String userAgent = Request.UserAgent.ToLower();
					if (ConfigurationManager.AppSettings["Mobile.AllowMobileView"] == "false")
					{
						return false;
					}
					else if (Session["USE_DESKTOP"] != null && Session["USE_DESKTOP"].ToString() == "true")
					{
						return false;
					}
					else if (Session["ADMIN_MOBILE_VIEW"] != null && Session["ADMIN_MOBILE_VIEW"].ToString() == "true")
					{
						return true;
					}
					else if (userAgent.Contains("android") && userAgent.Contains("mobile"))
					{
						return true;
					}
					else if (userAgent.Contains("iphone"))
					{
						return true;
					}
					else if (userAgent.Contains("windows phone"))
					{
						return true;
					}
					return false;
				}
				catch (Exception ex)
				{
					LiquidCore.Error.Report(ex, FUNCTIONNAME, String.Empty);
					return false;
				}
			}

			private void BuildTemplate(int templateId)
			{
				RXServer.Modules.Base.List.Item template = new LiquidCore.List.Item(templateId);
				String columns = template.Value2;
				int index = 1;
				foreach(String column in columns.Split('|'))
				{
					switch (column)
					{
						case "S":
							addColumn("small", index);
							break;
						case "M":
							addColumn("medium", index);
							break;
						case "L":
							addColumn("large", index);
							break;
						case "XL":
							addColumn("xlarge", index);
							break;
						case "ME":
							addMenu(index);
							break;
					}

					index++;
				}
			}

			private void addColumn(String type, int index)
			{
				HtmlGenericControl div = new HtmlGenericControl("DIV");
				div.Attributes.Add("class", type + "_col");

				RXEditModule editModule = (RXEditModule)this.Page.LoadControl("Modules/Modules/EditModules/EditModules.ascx");
				editModule.SitId = RXServer.Web.RequestValues.SitId;
				editModule.PagId = RXServer.Web.RequestValues.PagId;
				editModule.ModId = index;
				editModule.Src = "Modules/Modules/EditModules/EditModules.ascx";
				editModule.Hidden = false;
				editModule.Size = type;

				ContentPlaceHolder placeHolder = new ContentPlaceHolder();
				placeHolder.ID = index.ToString();

				div.Controls.Add(editModule);
				div.Controls.Add(placeHolder);
				Common.FindControlRecursive(this, "TemplatePlaceHolder").Controls.Add(div);
			}

			private void addMenu(int index)
			{
				HtmlGenericControl div = new HtmlGenericControl("DIV");
				div.Attributes.Add("class", "small_col");

				ContentPlaceHolder placeHolder = new ContentPlaceHolder();
				placeHolder.ID = "ContentPane3";

				div.Controls.Add(placeHolder);
				Common.FindControlRecursive(this, "TemplatePlaceHolder").Controls.Add(div);
			}

			public virtual void RenderContentControl(Control c, Control Mod, Boolean InsertAdminBar, Int32 SitId, Int32 PagId, Int32 ModId, Int32 MdeId, Boolean Hidden)
			{
				if (ModuleWithAdminbar(MdeId))
				{
					RXServer.Modules.StandardModule module = new RXServer.Modules.StandardModule(SitId, PagId, ModId);
					c.Controls.Add(new LiteralControl("<div style='float:" + module.Float + ";'>"));

					if (InsertAdminBar)
					{
						RXBaseAdminBarModule adm = new RXBaseAdminBarModule();
						// Här ska in check för mdeid och olika moduler...
						if (MdeId.Equals(71)) // TextBox
						{
							adm = (RXBaseAdminBarModule)this.Page.LoadControl("~/Modules/_System/AdminBars/AdminBar1.ascx");
							adm.AdminUrl = "/Modules/Boxes/TextBox/TextBox_Admin.aspx";
							adm.ModDefId = 71;
						}
						else if (MdeId.Equals(89)) // MediaBox
						{
							adm = (RXBaseAdminBarModule)this.Page.LoadControl("~/Modules/_System/AdminBars/AdminBar1.ascx");
							adm.AdminUrl = "/Modules/Boxes/MediaBox/MediaBox_Admin.aspx";
							adm.ModDefId = 89;
						}
						else if (MdeId.Equals(91)) // ImageGallery
						{
							adm = (RXBaseAdminBarModule)this.Page.LoadControl("~/Modules/_System/AdminBars/AdminBar1.ascx");
							adm.AdminUrl = "/Modules/Boxes/GalleryBox/GalleryBox_Admin.aspx";
							adm.ModDefId = 91;
						}
						else if (MdeId.Equals(96)) // TeaserBox
						{
							adm = (RXBaseAdminBarModule)this.Page.LoadControl("~/Modules/_System/AdminBars/AdminBar1.ascx");
							adm.AdminUrl = "/Modules/Boxes/TeaserBox/TeaserBox_Admin.aspx";
							adm.ModDefId = 96;
						}
						else if (MdeId.Equals(99)) // ContactUs
						{
							adm = (RXBaseAdminBarModule)this.Page.LoadControl("~/Modules/_System/AdminBars/AdminBar1.ascx");
							adm.AdminUrl = "/Modules/Boxes/ContactBox/ContactBox_Admin.aspx";
							adm.ModDefId = 99;
						}
						else if (MdeId.Equals(100)) // ShopBox
						{
							adm = (RXBaseAdminBarModule)this.Page.LoadControl("~/Modules/_System/AdminBars/AdminBar1.ascx");
							adm.AdminUrl = "/Modules/Boxes/ShopBox/ShopBox_Admin.aspx";
							adm.ModDefId = 100;
                        }
                        else if (MdeId.Equals(101)) // FeedBox
                        {
                            adm = (RXBaseAdminBarModule)this.Page.LoadControl("~/Modules/_System/AdminBars/AdminBar1.ascx");
                            adm.AdminUrl = "/Modules/Boxes/FeedBox/FeedBox_Admin.aspx";
							adm.ModDefId = 101;
                        }
						else if (MdeId.Equals(102)) // ForumBox
						{
							adm = (RXBaseAdminBarModule)this.Page.LoadControl("~/Modules/_System/AdminBars/AdminBar2.ascx");
							adm.ModDefId = 102;
						}
						else if (MdeId.Equals(103)) // BlogBox
						{
							adm = (RXBaseAdminBarModule)this.Page.LoadControl("~/Modules/_System/AdminBars/AdminBar1.ascx");
							adm.AdminUrl = "/Modules/Boxes/Blog/Blog_Admin.aspx";
							adm.ModDefId = 103;
						}
						else if (MdeId.Equals(104)) // ArticleBox
						{
							adm = (RXBaseAdminBarModule)this.Page.LoadControl("~/Modules/_System/AdminBars/AdminBar1.ascx");
							adm.AdminUrl = "/Modules/Boxes/ArticleBox/ArticleBox_Admin.aspx";
							adm.ModDefId = 104;
						}
						else if (MdeId.Equals(105)) // YoutubeBox
						{
							adm = (RXBaseAdminBarModule)this.Page.LoadControl("~/Modules/_System/AdminBars/AdminBar1.ascx");
							adm.AdminUrl = "/Modules/Boxes/YoutubeBox/YoutubeBox_Admin.aspx";
							adm.ModDefId = 105;
						}
						else if (MdeId.Equals(106)) // BookPackageBox
						{
							adm = (RXBaseAdminBarModule)this.Page.LoadControl("~/Modules/_System/AdminBars/AdminBar1.ascx");
							adm.AdminUrl = "/Modules/Boxes/BookPackageBox/BookPackageBox_Admin.aspx";
							adm.ModDefId = 106;
						}
						else if (MdeId.Equals(107)) // DividerBox
						{
							adm = (RXBaseAdminBarModule)this.Page.LoadControl("~/Modules/_System/AdminBars/AdminBar1.ascx");
							adm.AdminUrl = "/Modules/Boxes/DividerBox/DividerBox_Admin.aspx";
							adm.ModDefId = 107;
						}
						else if (MdeId.Equals(108)) // EventBox
						{
							adm = (RXBaseAdminBarModule)this.Page.LoadControl("~/Modules/_System/AdminBars/AdminBar1.ascx");
							adm.AdminUrl = "/Modules/Boxes/EventBox/EventBox_Admin.aspx";
							adm.ModDefId = 108;
						}
						else if (MdeId.Equals(109)) // EventListBox
						{
							adm = (RXBaseAdminBarModule)this.Page.LoadControl("~/Modules/_System/AdminBars/AdminBar1.ascx");
							adm.AdminUrl = "/Modules/Boxes/EventListBox/EventListBox_Admin.aspx";
							adm.ModDefId = 109;
						}
						else if (MdeId.Equals(110)) // EventTeaserBox
						{
							adm = (RXBaseAdminBarModule)this.Page.LoadControl("~/Modules/_System/AdminBars/AdminBar1.ascx");
							adm.AdminUrl = "/Modules/Boxes/EventTeaserBox/EventTeaserBox_Admin.aspx";
							adm.ModDefId = 110;
						}
						else if (MdeId.Equals(111)) // GiftcardBox
						{
							adm = (RXBaseAdminBarModule)this.Page.LoadControl("~/Modules/_System/AdminBars/AdminBar1.ascx");
							adm.AdminUrl = "/Modules/Boxes/GiftcardBox/GiftcardBox_Admin.aspx";
							adm.ModDefId = 111;
						}
						else if (MdeId.Equals(112)) // GoogleMapsBox
						{
							adm = (RXBaseAdminBarModule)this.Page.LoadControl("~/Modules/_System/AdminBars/AdminBar1.ascx");
							adm.AdminUrl = "/Modules/Boxes/GoogleMapsBox/GoogleMapsBox_Admin.aspx";
							adm.ModDefId = 112;
						}
						else if (MdeId.Equals(113)) // PackageBox
						{
							adm = (RXBaseAdminBarModule)this.Page.LoadControl("~/Modules/_System/AdminBars/AdminBar1.ascx");
							adm.AdminUrl = "/Modules/Boxes/PackageBox/PackageBox_Admin.aspx";
							adm.ModDefId = 113;
						}
						else if (MdeId.Equals(114)) // SubmenuBox
						{
							adm = (RXBaseAdminBarModule)this.Page.LoadControl("~/Modules/_System/AdminBars/AdminBar1.ascx");
							adm.AdminUrl = "/Modules/Boxes/SubmenuBox/SubmenuBox_Admin.aspx";
							adm.ModDefId = 114;
						}
						adm.SitId = SitId;
						adm.PagId = PagId;
						adm.ModId = ModId;
						adm.MasterPageFile = runtimeMasterPageFile;
						adm.Hidden = Hidden;
						c.Controls.Add(new LiteralControl("<div class='adminhidable' style='clear:both;'>"));
						c.Controls.Add(adm);
						c.Controls.Add(new LiteralControl("</div>"));
					}

					if (Hidden)
					{
						c.Controls.Add(new LiteralControl("<div class='adminhidable' style='clear:both;'>"));
					}
					else
					{
						c.Controls.Add(new LiteralControl("<div style='clear:both;'>"));
					}

					c.Controls.Add(Mod);
					c.Controls.Add(new LiteralControl("</div>"));
					c.Controls.Add(new LiteralControl("</div>"));
				}
				else
				{
					c.Controls.Add(Mod);
				}
			}
			public virtual void RenderContentControlMobile(Control Mod, Int32 SitId, Int32 PagId, Int32 ModId, Int32 MdeId, Boolean InsertAdminBar, Boolean Hidden)
			{
				Control c = Common.FindControlRecursive(this, "MobileModulesContentPane");
				c.Controls.Add(Mod);
				if (ModuleWithAdminbar(MdeId))
				{
					c.Controls.Add(new LiteralControl("<div style='float: left;'>"));
					if (InsertAdminBar)
					{
						RXBaseAdminBarModule adm = new RXBaseAdminBarModule();
						adm = (RXBaseAdminBarModule)this.Page.LoadControl("~/Modules/_System/AdminBars/AdminBarMobile.ascx");
						adm.ModDefId = MdeId;
						adm.SitId = SitId;
						adm.PagId = PagId;
						adm.ModId = ModId;
						adm.MasterPageFile = runtimeMasterPageFile;
						adm.Hidden = Hidden;
						c.Controls.Add(new LiteralControl("<div class='adminhidable' style='clear:both;'>"));
						c.Controls.Add(adm);
						c.Controls.Add(new LiteralControl("</div>"));
					}

					if (Hidden)
					{
						c.Controls.Add(new LiteralControl("<div class='adminhidable' style='clear:both;'>"));
					}
					else
					{
						c.Controls.Add(new LiteralControl("<div style='clear:both;'>"));
					}

					c.Controls.Add(Mod);
					c.Controls.Add(new LiteralControl("</div>"));
					c.Controls.Add(new LiteralControl("</div>"));
				}
				else
				{
					c.Controls.Add(Mod);
				}
			}

			private bool ModuleWithAdminbar(int MdeId)
			{
				if (MdeId == 32)
				{
					return true;
				}
				else if (MdeId == 66)
				{
					return true;
				}
				else if (MdeId == 71)
				{
					return true;
				}
				else if (MdeId == 89)
				{
					return true;
				}
				else if (MdeId == 90)
				{
					return true;
				}
				else if (MdeId == 91)
				{
					return true;
				}
				else if (MdeId == 94)
				{
					return true;
				}
				else if (MdeId == 95)
				{
					return true;
				}
				else if (MdeId == 96)
				{
					return true;
				}
				else if (MdeId == 97)
				{
					return true;
				}
				else if (MdeId == 99)
				{
					return true;
				}
				else if (MdeId == 100)
				{
					return true;
                }
                else if (MdeId == 101)
                {
                    return true;
                }
				else if (MdeId == 102)
				{
					return true;
				}
				else if (MdeId == 103)
				{
					return true;
				}
				else if (MdeId == 104)
				{
					return true;
				}
				else if (MdeId == 105)
				{
					return true;
				}
				else if (MdeId == 106)
				{
					return true;
				}
				else if (MdeId == 107)
				{
					return true;
				}
				else if (MdeId == 108)
				{
					return true;
				}
				else if (MdeId == 109)
				{
					return true;
				}
				else if (MdeId == 110)
				{
					return true;
				}
				else if (MdeId == 111)
				{
					return true;
				}
				else if (MdeId == 112)
				{
					return true;
				}
				else if (MdeId == 113)
				{
					return true;
				}
				else if (MdeId == 114)
				{
					return true;
				}

				return false;
			}

			public virtual void RenderContentSpacer(Control c, Boolean Thin, Boolean High)
			{
				if (Thin)
				{
					if (High)
					{
						c.Controls.Add(new LiteralControl("<table align='left' style='display: inline; '><tr>"));
						c.Controls.Add(new LiteralControl("<td class='module_start_spacer_big'><img src='../images/pixel_trans.gif' /></td>"));
						c.Controls.Add(new LiteralControl("</tr></table>"));
					}
					else
					{
						c.Controls.Add(new LiteralControl("<table align='left' style='display: inline; '><tr>"));
						c.Controls.Add(new LiteralControl("<td class='module_start_spacer_mini'><img src='../images/pixel_trans.gif' /></td>"));
						c.Controls.Add(new LiteralControl("</tr></table>"));
					}
				}
				else
				{
					c.Controls.Add(new LiteralControl("<table align='left' style='display: inline; '><tr>"));
					c.Controls.Add(new LiteralControl("<td class='module_spacer'><img src='../images/pixel_trans.gif' /></td>"));
					c.Controls.Add(new LiteralControl("</tr></table>"));
				}
			}
        }

		public class RXDefaultPage : System.Web.UI.Page
		{
			public bool IsTerminating = false;

			protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
			{
				if (IsTerminating == false)
					base.RaisePostBackEvent(sourceControl, eventArgument);
			}

			protected override void Render(HtmlTextWriter writer)
			{
				if (IsTerminating == false)
					base.Render(writer);
			}
		}

        public class RXBaseModuleSettings
        {
            string CLASSNAME = "[Namespace::RXServer.RXBaseModule][Class::RXBaseModuleSettings]";
            public Int32 Id;
            public Int32 SitId;
            public Int32 PagId;
            public Int32 ModId;
            public String Src;
            public Int32 CacheTime;
            public String AuthorizedEditRoles;
            public Boolean Hidden;
			public Boolean Mobile;
        }
        public class RXBaseAdminBarModule : System.Web.UI.UserControl
        {
            string CLASSNAME = "[Namespace::RXServer][Class::RXBaseAdminBarModule]";
            private Int32 _SitId = 0;
            private Int32 _PagId = 0;
            private Int32 _ModId = 0;
			public Int32 _ModDefId = 0;
            private String _AdminUrl = String.Empty;
            private String _MasterPageFile = String.Empty;
            private Boolean _Hidden = false;
			private Boolean _ExtraSocial = false;
            public Int32 SitId
            {
                get { return _SitId; }
                set { _SitId = value; }
            }
            public Int32 PagId
            {
                get { return _PagId; }
                set { _PagId = value; }
            }
            public Int32 ModId
            {
                get { return _ModId; }
                set { _ModId = value; }
            }
			public Int32 ModDefId
			{
				get { return _ModDefId; }
				set { _ModDefId = value; }
			}
            public String AdminUrl
            {
                get { return _AdminUrl; }
                set { _AdminUrl = value; }
            }
            public String MasterPageFile
            {
                get { return _MasterPageFile; }
                set { _MasterPageFile = value; }
            }
            public Boolean Hidden
            {
                get
                {
                    return _Hidden;
                }
                set
                {
                    _Hidden = value;
                }
            }
			public Boolean ExtraSocial
			{
				get
				{
					return _ExtraSocial;
				}
				set
				{
					_ExtraSocial = value;
				}
			}
        }

		public class RXBaseModule : System.Web.UI.UserControl
		{
			string CLASSNAME = "[Namespace::RXServer][Class::RXBaseModule]";
			private RXBaseModuleSettings _ModuleSettings = new RXBaseModuleSettings();
			public Int32 SitId
			{
				get
				{
					return _ModuleSettings.SitId;
				}
				set
				{
					_ModuleSettings.SitId = value;
				}
			}
			public Int32 PagId
			{
				get
				{
					return _ModuleSettings.PagId;
				}
				set
				{
					_ModuleSettings.PagId = value;
				}
			}
			public Int32 ModId
			{
				get
				{
					return _ModuleSettings.ModId;
				}
				set
				{
					_ModuleSettings.ModId = value;
				}
			}
			public String Src
			{
				get
				{
					return _ModuleSettings.Src;
				}
				set
				{
					_ModuleSettings.Src = value;
				}
			}
			public Int32 CacheTime
			{
				get
				{
					return _ModuleSettings.CacheTime;
				}
				set
				{
					_ModuleSettings.CacheTime = value;
				}
			}
			public String AuthorizedEditRoles
			{
				get
				{
					return _ModuleSettings.AuthorizedEditRoles;
				}
				set
				{
					_ModuleSettings.AuthorizedEditRoles = value;
				}
			}
			public Boolean Hidden
			{
				get
				{
					return _ModuleSettings.Hidden;
				}
				set
				{
					_ModuleSettings.Hidden = value;
				}
			}
			public RXBaseModuleSettings ModuleSettings
			{
				get
				{
					return _ModuleSettings;
				}
				set
				{
					_ModuleSettings = value;
				}
			}
			protected bool IsMobile()
			{
				String function_name = "IsMobile";
				try
				{
					String userAgent = Request.UserAgent.ToLower();

					if (ConfigurationManager.AppSettings["Mobile.AllowMobileView"] == "false")
					{
						return false;
					}
					else if (Session["USE_DESKTOP"] != null && Session["USE_DESKTOP"].ToString() == "true")
					{
						return false;
					}
					else if (Session["ADMIN_MOBILE_VIEW"] != null && Session["ADMIN_MOBILE_VIEW"].ToString() == "true")
					{
						return true;
					}
					else if (userAgent.Contains("android") && userAgent.Contains("mobile"))
					{
						return true;
					}
					else if (userAgent.Contains("iphone"))
					{
						return true;
					}
					else if (userAgent.Contains("windows phone"))
					{
						return true;
					}

					return false;
				}
				catch (Exception ex)
				{
					LiquidCore.Error.Report(ex, function_name, String.Empty);
					return false;
				}
			}
		}

		public class RXEditModule : RXBaseModule
		{
			string CLASSNAME = "[Namespace::RXServer][Class::RXEditModule]";
			private RXBaseModuleSettings _ModuleSettings = new RXBaseModuleSettings();
			private String _size;
			public String Size
			{
				get
				{
					return _size;
				}
				set
				{
					_size = value;
				}
			}
		}

		public class RXBaseCachedModule : Control
		{
			string CLASSNAME = "[Namespace::RXServer][Class::RXBaseCachedModule]";
			private RXBaseModuleSettings _ModuleSettings = new RXBaseModuleSettings();
			public Int32 SitId
			{
				get
				{
					return _ModuleSettings.SitId;
				}
				set
				{
					_ModuleSettings.SitId = value;
				}
			}
			public Int32 PagId
			{
				get
				{
					return _ModuleSettings.PagId;
				}
				set
				{
					_ModuleSettings.PagId = value;
				}
			}
			public Int32 ModId
			{
				get
				{
					return _ModuleSettings.ModId;
				}
				set
				{
					_ModuleSettings.ModId = value;
				}
			}
			public Int32 CacheTime
			{
				get
				{
					return _ModuleSettings.CacheTime;
				}
				set
				{
					_ModuleSettings.CacheTime = value;
				}
			}
			public String Src
			{
				get
				{
					return _ModuleSettings.Src;
				}
				set
				{
					_ModuleSettings.Src = value;
				}
			}
			public String AuthorizedEditRoles
			{
				get
				{
					return _ModuleSettings.AuthorizedEditRoles;
				}
				set
				{
					_ModuleSettings.AuthorizedEditRoles = value;
				}
			}
			public String CacheKey
			{
				get
				{
					return "Key:" + this.GetType().ToString() + this.ModId + AuthorizedEditRoles + this.Mobile;
				}
			}
			public Boolean Hidden
			{
				get
				{
					return _ModuleSettings.Hidden;
				}
				set
				{
					_ModuleSettings.Hidden = value;
				}
			}
			public Boolean Mobile
			{
				get
				{
					return _ModuleSettings.Mobile;
				}
				set
				{
					_ModuleSettings.Mobile = value;
				}
			}
			public RXBaseModuleSettings ModuleSettings
			{
				get
				{
					return _ModuleSettings;
				}
				set
				{
					_ModuleSettings = value;
				}
			}
			String _cachedOutput = null;
			protected override void CreateChildControls()
			{
				// If _moduleConfiguration.CacheTime > 0 Then
				if (_ModuleSettings.CacheTime > 0)
				{
					_cachedOutput = (String)(Context.Cache[CacheKey]);
				}
				if (_cachedOutput == null)
				{
					base.CreateChildControls();
					if (_ModuleSettings.Src.Equals(String.Empty))
						return;
					RXBaseModule mg = (RXBaseModule)this.Page.LoadControl(_ModuleSettings.Src);
					mg.SitId = this.SitId;
					mg.PagId = this.PagId;
					mg.ModId = this.ModId;
					mg.Src = this.Src;
					mg.CacheTime = this.CacheTime;
					mg.AuthorizedEditRoles = this.AuthorizedEditRoles;
					mg.Hidden = this.Hidden;
					this.Controls.Add(mg);
				}
			}
			protected override void Render(HtmlTextWriter writer)
			{
				// If _moduleConfiguration.CacheTime = 0 Then
				if (ModuleSettings.CacheTime.Equals(0))
				{
					base.Render(writer);
					return;
				}
				if (_cachedOutput == null)
				{
					StringWriter tempWriter = new StringWriter();
					base.Render(new HtmlTextWriter(tempWriter));
					_cachedOutput = tempWriter.ToString();
					Context.Cache.Insert(CacheKey, _cachedOutput, null, DateTime.Now.AddSeconds(ModuleSettings.CacheTime), TimeSpan.Zero);
				}
				writer.Write(_cachedOutput);
			}
		}
        public class Common
        {
            public static Control FindControlRecursive(Control Root, String Id)
            {
                string FUNCTIONNAME = "[Namespace::RXServer][Class::Common][Function::FindControlRecursive]";
                try
                {
                    if (Root.ID == Id)
                        return Root;
                    foreach (Control Ctl in Root.Controls)
                    {
                        Control FoundCtl = FindControlRecursive(Ctl, Id);
                        if (FoundCtl != null)
                            return FoundCtl;
                    }
                    return null;
                }
                catch (Exception ex)
                {
                    LiquidCore.Error.Report(ex, FUNCTIONNAME, String.Empty);
                    return null;
                }
            }

            public static Control FindRXContentHolder(Control Root, String Tag)
            {
                string FUNCTIONNAME = "[Namespace::RXServer][Class::Common][Function::FindControlRecursive]";
                try
                {
                    if (Root.GetType() == typeof(RXServer.RXContentHolder))
                        return Root;
                    foreach (Control Ctl in Root.Controls)
                    {

                        Control FoundCtl = FindRXContentHolder(Ctl, Tag);
                        if (FoundCtl != null)
                            if (FoundCtl.GetType() == typeof(RXServer.RXContentHolder))
                            {
                                RXServer.RXContentHolder ch = (RXServer.RXContentHolder)FoundCtl;
                                if (ch.PaneName == Tag)
                                    return FoundCtl;
                            }
                    }
                    return null;
                }
                catch (Exception ex)
                {
                    LiquidCore.Error.Report(ex, FUNCTIONNAME, String.Empty);
                    return null;
                }
            }

            public class Dynamic
            {
                public static String CreateUrlPrefix()
                {
                    string FUNCTIONNAME = "[Namespace::RXServer.Lib][Class::Dynamic][Function::CreateUrlPrefix]";
                    String root = string.Empty;
                    try
                    {
                        bool isRootWeb = System.Web.HttpContext.Current.Request.ApplicationPath.Equals("/");

                        if (isRootWeb)
                        {
                            for (Int32 x = 1; x < HttpContext.Current.Request.RawUrl.Split('/').GetUpperBound(0); x++)
                                root += "../";
                            return root;
                        }
                        else
                        {
                            for (Int32 x = 2; x < HttpContext.Current.Request.RawUrl.Split('/').GetUpperBound(0); x++)
                                root += "../";
                            return root;
                        }
                    }
                    catch (Exception ex)
                    {
                        LiquidCore.Error.Report(ex, FUNCTIONNAME, String.Empty);
                        return root;
                    }
                }
                public static Int32 GetCurrentLevel()
                {
                    return HttpContext.Current.Request.RawUrl.Split('/').GetUpperBound(0);
                }

                public static String GetFriendlyUrl(Int32 PagId)
                {
                    return Cambia.Web.CoreLib.PathHelper.ApplicationToVirtual(RXServer.Web.SelectedPages.GetDynamicSiteMapPath(PagId, "/"));
                }

                public static String GetRootPrefix(Int32 Index)
                {
                    string FUNCTIONNAME = "[Namespace::RXServer.Lib][Class::Dynamic][Function::GetRootPrefix]";
                    String root = string.Empty;
                    try
                    {
                        for (Int32 x = Index; x < HttpContext.Current.Request.RawUrl.Split('/').GetUpperBound(0); x++)
                            root += "../";
                        return root;
                    }
                    catch (Exception ex)
                    {
                        LiquidCore.Error.Report(ex, FUNCTIONNAME, String.Empty);
                        return root;
                    }
                }
                public static String GetRootPrefix()
                {
                    return GetRootPrefix(2);
                }

                public static String GetModulePrefix(Int32 PagId)
                {
                    string FUNCTIONNAME = "[Namespace::RXServer.Lib][Class::Dynamic][Function::GetModulePrefix]";
                    String root = string.Empty;
                    try
                    {
                        RXServer.Modules.Menu.Item p = new RXServer.Modules.Menu.Item(PagId);
                        for (Int32 x = 1; x < p.Level; x++)
                            root += "../";
                        return root;
                    }
                    catch (Exception ex)
                    {
                        LiquidCore.Error.Report(ex, FUNCTIONNAME, String.Empty);
                        return root;
                    }
                }

				public static int GetWidth(RXServer.Modules.StandardModule.WidthTypes width, RXServer.Modules.StandardModule.WidthTypes defaultValue)
				{
					String appSetting = "";
					bool getDefaultValue = false;
					switch (width)
					{
						case RXServer.Modules.StandardModule.WidthTypes.small:
							appSetting = "Width.Small";
							break;
						case RXServer.Modules.StandardModule.WidthTypes.medium:
							appSetting = "Width.Medium";
							break;
						case RXServer.Modules.StandardModule.WidthTypes.large:
							appSetting = "Width.Large";
							break;
						case RXServer.Modules.StandardModule.WidthTypes.xlarge:
							appSetting = "Width.Xlarge";
							break;
						default:
							getDefaultValue = true;
							break;
					}

					if (getDefaultValue)
					{
						switch (defaultValue)
						{
							case RXServer.Modules.StandardModule.WidthTypes.small:
								appSetting = "Width.Small";
								break;
							case RXServer.Modules.StandardModule.WidthTypes.medium:
								appSetting = "Width.Medium";
								break;
							case RXServer.Modules.StandardModule.WidthTypes.large:
								appSetting = "Width.Large";
								break;
							case RXServer.Modules.StandardModule.WidthTypes.xlarge:
								appSetting = "Width.Xlarge";
								break;
							default:
								appSetting = "Width.Small";
								break;
						}
					}

					return Convert.ToInt32(ConfigurationManager.AppSettings[appSetting]);
				}

				public static int GetWidth(RXServer.Modules.StandardModule.WidthTypes width)
				{
					String appSetting = "";
					switch (width)
					{
						case RXServer.Modules.StandardModule.WidthTypes.small:
							appSetting = "Width.Small";
							break;
						case RXServer.Modules.StandardModule.WidthTypes.medium:
							appSetting = "Width.Medium";
							break;
						case RXServer.Modules.StandardModule.WidthTypes.large:
							appSetting = "Width.Large";
							break;
						case RXServer.Modules.StandardModule.WidthTypes.xlarge:
							appSetting = "Width.Xlarge";
							break;
						default:
							appSetting = "Width.Small";
							break;
					}

					return Convert.ToInt32(ConfigurationManager.AppSettings[appSetting]);
				}
            }
        }
    }
    namespace Data
    {
        public class Direct
        {
            public static DataSet GetDataSet(String sSql)
            {
                string CLASSNAME = "[Namespace::RXServer::Data::Direct][Class::GetDataSet]";
                DataSet ret = new DataSet();
                try
                {
                    using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, false))
                    {
                        ret = oDo.GetDataSet(sSql);
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);
                    }
                    return ret;
                }
                catch (Exception ex)
                {
                    LiquidCore.Error.Report(ex, CLASSNAME, String.Empty);
                    return ret;
                }
            }
            public static DataTable GetDataTable(String sSql)
            {
                string CLASSNAME = "[Namespace::RXServer::Data::Direct][Class::GetDataTable]";
                DataTable ret = new DataTable();
                try
                {
                    using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, false))
                    {
                        ret = oDo.GetDataTable(sSql);
                        if (oDo.HasError)
                            throw new Exception(oDo.GetError);
                    }
                    return ret;
                }
                catch (Exception ex)
                {
                    LiquidCore.Error.Report(ex, CLASSNAME, String.Empty);
                    return ret;
                }
            }
            public static void ExecuteNonQuery(String sSql, Boolean UseTransaction)
            {
                string CLASSNAME = "[Namespace::RXServer::Data::Direct][Class::ExecuteNonQuery]";
                try
                {
                    using (iConsulting.iCDataHandler.iCDataObject oDo = new iConsulting.iCDataHandler.iCDataObject(System.Configuration.ConfigurationManager.AppSettings["Data.DataSource"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Data.ConnectionString"].ToString(), false, true, UseTransaction))
                    {
                        if (!oDo.ExecuteNonQuery(sSql.ToString()).Equals(1))
                            throw new Exception(oDo.GetError);
                    }
                }
                catch (Exception ex)
                {
                    LiquidCore.Error.Report(ex, CLASSNAME, String.Empty);
                }
            }
        }
    }
}

// RXServer.Auth
namespace RXServer
{
    public class Auth : LiquidCore.Security
    {
        public class Users : LiquidCore.Users
        {
            public Users(Int32 RolId)
                : base()
            {
                if (RolId != 0)
                {
                    Int32 _counter = 0;
                    foreach (LiquidCore.Users.User i in base._list)
                    {
                        if (i.GetRoles().Count < 1)
                            _counter++;
                        else
                            if (!i.GetRoles()[0].Equals(RolId.ToString()))
                                _counter++;
                    }

                    Int32[] _index = new Int32[_counter];
                    _counter = 0;

                    for (Int32 x = 0; x < base._list.Count; x++)
                    {
                        LiquidCore.Users.User i = (LiquidCore.Users.User)base._list[x];
                        if (i.GetRoles().Count < 1)
                        {
                            _index[_counter] = x;
                            _counter++;
                        }
                        else
                            if (!i.GetRoles()[0].Equals(RolId.ToString()))
                            {
                                _index[_counter] = x;
                                _counter++;
                            }
                    }

                    for (int i = _index.GetUpperBound(0); i >= _index.GetLowerBound(0); i--)
                        base._list.RemoveAt(_index[i]);
                }

            }
            public Users(RXServer.Auth.Users.SortParamEnum SortParamEnum, RXServer.Auth.Users.SortOrderEnum SortOrderEnum, Int32 StartIndex, Int32 Limit)
                : base()
            {
                SortOrderClean(SortParamEnum, SortOrderEnum, StartIndex, Limit);
            }
            public Users(Int32 RolId, RXServer.Auth.Users.SortParamEnum SortParamEnum, RXServer.Auth.Users.SortOrderEnum SortOrderEnum, Int32 StartIndex, Int32 Limit)
                : this(RolId)
            {
                SortOrderClean(SortParamEnum, SortOrderEnum, StartIndex, Limit);
            }
            private void SortOrderClean(RXServer.Auth.Users.SortParamEnum SortParamEnum, RXServer.Auth.Users.SortOrderEnum SortOrderEnum, Int32 StartIndex, Int32 Limit)
            {
                base.Sort(SortParamEnum, SortOrderEnum);

                Int32 _counter = 0;

                for (Int32 i = 0; i <= base._list.Count; i++)
                {
                    if (i < StartIndex)
                        _counter++;
                    else if (i > (StartIndex + Limit))
                        _counter++;
                }

                Int32[] _index = new Int32[_counter];

                _counter = 0;

                for (Int32 i = 0; i <= base._list.Count; i++)
                {
                    if (i < StartIndex)
                    {
                        _index[_counter] = i;
                        _counter++;
                    }
                    else if (i > (StartIndex + Limit))
                    {
                        _index[_counter] = (i - 1);
                        _counter++;
                    }
                }

                for (int i = _index.GetUpperBound(0); i >= _index.GetLowerBound(0); i--)
                    base._list.RemoveAt(_index[i]);
            }
            public Users() : base() { }
            public static Int32 GetUserId(String UserName)
            {
                foreach (LiquidCore.Users.User u in new LiquidCore.Users())
                    if (u.LoginName.ToLower().Equals(UserName.ToLower()))
                        return u.Id;
                return 0;
            }
            public static Int32 GetTotalRoleUsers(Int32 RoleId)
            {
                RXServer.Auth.Users u = new RXServer.Auth.Users(RoleId);
                return u.Count;
            }
            public static Int32 GetUserIdByEmail(String Mail)
            {
                foreach (LiquidCore.Users.User u in new LiquidCore.Users())
                    if (u.Mail.ToLower().Equals(Mail.ToLower()))
                        return u.Id;
                return 0;
            }
            public static bool UserNameExist(String UserName)
            {
                foreach (LiquidCore.Users.User u in new LiquidCore.Users())
                    if (u.LoginName.ToLower().Equals(UserName.ToLower()))
                        return true;
                return false;
            }
            public static bool UserEmailExist(String Mail)
            {
                foreach (LiquidCore.Users.User u in new LiquidCore.Users())
                    if (u.Mail.ToLower().Equals(Mail.ToLower()))
                        return true;
                return false;
            }
            public static Int32 GetUserStartPage(string UserName)
            {
                Int32 uPagId = 0;
                RXServer.Auth.Users.User u = new RXServer.Auth.Users.User(RXServer.Auth.Users.GetUserId(UserName));
                uPagId = u.StartPage;
                return uPagId;
            }
            public static void ActivateUserAccount(Int32 UserId)
            {
                LiquidCore.Users.User u = new LiquidCore.Users.User(UserId);
                u.Status = 1;
                u.Save();
            }
            public static void DeactivateUserAccount(Int32 UserId)
            {
                LiquidCore.Users.User u = new LiquidCore.Users.User(UserId);
                u.Status = 2;
                u.Save();
            }
            public static Int32 CreateUser(String UserName, String Mail, String Alias, String Password)
            {
                string function_name = "CreateUser";
                try
                {
                    return CreateUser(UserName, Mail, Alias, Password, 1);
                }
                catch (Exception ex)
                {
                    RXServer.Lib.Error.Report(ex, function_name, "");
                    return 0;
                }

            }
            public static Int32 CreateUser(String UserName, String Mail, String Alias, String Password, Int32 RolId)
            {
                string function_name = "CreateUser";
                try
                {
                    using (LiquidCore.Users.User u = new LiquidCore.Users.User())
                    {

                        u.Status = 1;
                        u.Language = 1;
                        u.Type = 1;
                        u.ParentId = 0;
                        u.Title = String.Empty;
                        u.Alias = Alias;
                        u.LoginName = UserName;
                        u.Mail = Mail;
                        u.Password = Password;
                        u.Save();

                        u.AddToRole(RolId);

                        return u.Id;
                    }
                }
                catch (Exception ex)
                {
                    RXServer.Lib.Error.Report(ex, function_name, "");
                    return 0;
                }

            }
            public static void DeleteUser(Int32 UsrId)
            {
                string function_name = "DeleteUser";
                try
                {
                    using (LiquidCore.Users.User u = new LiquidCore.Users.User(UsrId))
                    {
                        Int32 RolId = 0;
                        Int32.TryParse(u.GetRoles()[0].ToString(), out RolId);
                        u.DeleteFromRole(RolId);
                        u.Delete();
                    }
                }
                catch (Exception ex)
                {
                    RXServer.Lib.Error.Report(ex, function_name, "");
                }
            }

            public static void UpdateUser(Int32 Id, String UserName, String Description, String Mail, String Password)
            {
                using (LiquidCore.Users.User u = new LiquidCore.Users.User(Id))
                {
                    u.Title = UserName;
                    u.Description = Description;
                    u.Mail = Mail;
                    u.LoginName = UserName;
                    u.Password = Password;
                    u.Save();
                }
            }
            public static LiquidCore.Settings GetSettings(Int32 Id)
            {
                return new LiquidCore.Users.User(Id).Settings;
            }
            public bool IsUserInRole(Int32 UsrId, Int32 RolId)
            {
                using (LiquidCore.Users.User u = new LiquidCore.Users.User(UsrId))
                {
                    foreach (Int32 r in u.GetRoles())
                        if (r.Equals(RolId))
                            return true;
                }
                return false;
            }
            public class User : LiquidCore.Users.User
            {
                public string UserName
                {
                    get
                    {
                        return this.LoginName;
                    }
                    set
                    {
                        this.LoginName = value;
                    }
                }
                public string Address2
                {
                    get
                    {
                        return this.GetSetting("Address2");
                    }
                    set
                    {
                        this.SaveSetting("Address2", value);
                    }
                }
                public string CO2
                {
                    get
                    {
                        return this.GetSetting("CO2");
                    }
                    set
                    {
                        this.SaveSetting("CO2", value);
                    }
                }
                public string PostalCode2
                {
                    get
                    {
                        return this.GetSetting("PostalCode2");
                    }
                    set
                    {
                        this.SaveSetting("PostalCode2", value);
                    }
                }
                public string City2
                {
                    get
                    {
                        return this.GetSetting("City2");
                    }
                    set
                    {
                        this.SaveSetting("City2", value);
                    }
                }
                public string Country2
                {
                    get
                    {
                        return this.GetSetting("Country2");
                    }
                    set
                    {
                        this.SaveSetting("Country2", value);
                    }
                }
                public string Company2
                {
                    get
                    {
                        return this.GetSetting("Company2");
                    }
                    set
                    {
                        this.SaveSetting("Company2", value);
                    }
                }
                public string Phone2
                {
                    get
                    {
                        return this.GetSetting("Phone2");
                    }
                    set
                    {
                        this.SaveSetting("Phone2", value);
                    }
                }
                public string LogTime
                {
                    get
                    {
                        return this.GetSetting("LogTime");
                    }
                    set
                    {
                        this.SaveSetting("LogTime", value);
                    }
                }
                public string LogTime2
                {
                    get
                    {
                        return this.GetSetting("LogTime2");
                    }
                    set
                    {
                        this.SaveSetting("LogTime2", value);
                    }
                }
                public string ImageUrl
                {
                    get
                    {
                        return this.GetSetting("ImageUrl");
                    }
                    set
                    {
                        this.SaveSetting("ImageUrl", value);
                    }
                }
                public string ImageToolTip
                {
                    get
                    {
                        return this.GetSetting("ImageToolTip");
                    }
                    set
                    {
                        this.SaveSetting("ImageToolTip", value);
                    }
                }
                public string Signature
                {
                    get
                    {
                        return this.GetSetting("Signature");
                    }
                    set
                    {
                        this.SaveSetting("Signature", value);
                    }
                }
                public string Age
                {
                    get
                    {
                        return this.GetSetting("Age");
                    }
                    set
                    {
                        this.SaveSetting("Age", value);
                    }
                }
                public string Sex
                {
                    get
                    {
                        return this.GetSetting("Sex");
                    }
                    set
                    {
                        this.SaveSetting("Sex", value);
                    }
                }
                public string WebSite
                {
                    get
                    {
                        return this.GetSetting("WebSite");
                    }
                    set
                    {
                        this.SaveSetting("WebSite", value);
                    }
                }
                public string Mail2
                {
                    get
                    {
                        return this.GetSetting("Mail2");
                    }
                    set
                    {
                        this.SaveSetting("Mail2", value);
                    }
                }
                public string SecurityQuestion
                {
                    get
                    {
                        return this.GetSetting("SecurityQuestion");
                    }
                    set
                    {
                        this.SaveSetting("SecurityQuestion", value);
                    }
                }
                public string SecurityAnswer
                {
                    get
                    {
                        return this.GetSetting("SecurityAnswer");
                    }
                    set
                    {
                        this.SaveSetting("SecurityAnswer", value);
                    }
                }
                public string Activated
                {
                    get
                    {
                        return this.GetSetting("Activated");
                    }
                    set
                    {
                        this.SaveSetting("Activated", value);
                    }
                }
                public string ActivatedDate
                {
                    get
                    {
                        return this.GetSetting("ActivatedDate");
                    }
                    set
                    {
                        this.SaveSetting("ActivatedDate", value);
                    }
                }

                public User(Int32 Id) : base(Id) { }
            }
        }
        public class Roles : LiquidCore.Roles
        {
            public Roles() : base() { }
            public static Int32 GetRoleId(String RoleName)
            {
                foreach (LiquidCore.Roles.Role r in new LiquidCore.Roles())
                    if (r.Title.ToLower().Equals(RoleName.ToLower()))
                        return r.Id;
                return 0;
            }
            public static Int32 CreateRole(String RoleName)
            {
                using (LiquidCore.Roles.Role r = new LiquidCore.Roles.Role())
                {
                    r.Status = 1;
                    r.Language = 1;
                    r.ParentId = 0;
                    r.Title = RoleName;
                    r.Alias = RoleName;
                    r.Save();

                    return r.Id;
                }
            }
            public static void UpdateRole(Int32 Id, String RoleName)
            {
                using (LiquidCore.Roles.Role r = new LiquidCore.Roles.Role(Id))
                {
                    r.Title = RoleName;
                    r.Alias = RoleName;
                    r.Save();
                }
            }
            public static void DeleteRole(Int32 Id)
            {
                using (LiquidCore.Roles.Role r = new LiquidCore.Roles.Role(Id))
                {
                    r.Delete();
                }
            }

            public static bool RoleNameExist(String RoleName)
            {
                foreach (LiquidCore.Roles.Role r in new LiquidCore.Roles())
                    if (r.Title.ToLower().Equals(RoleName.ToLower()))
                        return true;
                return false;
            }
            public class Role : LiquidCore.Roles.Role
            {
                public Role() : base() { }
                public Role(Int32 Id) : base(Id) { }
            }
        }
        public class AuthorizedUser : LiquidCore.LiquidCore.Authentication.User { }

		public static bool HasCurrentRoleEditRights(int PagId)
		{
			string FUNCTIONNAME = "[Namespace::RXServer.Auth][Class::Dynamic][Function::HasCurrentRoleEditRights]";
			try
			{
				if (!RXServer.Auth.AuthorizedUser.Identity.Authenticated)
				{
					return false;
				}
				else if (RXServer.Auth.IsInRole("Admin"))
				{
					return true;
				}
				else
				{
					RXServer.Modules.Menu.Item mItem = new RXServer.Modules.Menu.Item(PagId);
					foreach (int role in mItem.AuthorizedEditRoles)
					{
						if (role == 0)
						{
							return true;
						}
						else if (role == RXServer.Auth.Roles.GetRoleId(RXServer.Auth.AuthorizedUser.Identity.Role))
						{
							return true;
						}
					}

					if (mItem.ParentId == 0)
					{
						return false;
					}
					else
					{
						return HasCurrentRoleEditRights(mItem.ParentId);
					}
				}
			}
			catch (Exception ex)
			{
				LiquidCore.Error.Report(ex, FUNCTIONNAME, String.Empty);
				return false;
			}
		}
		public static bool HasCurrentRoleViewRights(int PagId)
		{
			string FUNCTIONNAME = "[Namespace::RXServer.Auth][Class::Dynamic][Function::HasCurrentRoleViewRights]";
			try
			{
				if (RXServer.Auth.IsInRole("Admin"))
				{
					return true;
				}
				else
				{
					RXServer.Lib.RXBasePage page = (RXServer.Lib.RXBasePage)HttpContext.Current.Handler;
					RXServer.Modules.Menu.Item mItem = new RXServer.Modules.Menu.Item(PagId);
					if ((mItem.Hidden && !page.IsMobile()) || (mItem.MobileHidden && page.IsMobile()))
					{
						return false;
					}
					else if (mItem.AuthorizedRoles.Length > 0)
					{
						foreach (int role in mItem.AuthorizedRoles)
						{
							if (role == 0)
							{
								return true;
							}
							else if (role == RXServer.Auth.Roles.GetRoleId(RXServer.Auth.AuthorizedUser.Identity.Role))
							{
								return true;
							}
						}

						return false;
					}
					else
					{
						if (mItem.ParentId == 0)
						{
							return true;
						}
						else
						{
							return HasCurrentRoleViewRights(mItem.ParentId);
						}
					}
				}
			}
			catch (Exception ex)
			{
				LiquidCore.Error.Report(ex, FUNCTIONNAME, String.Empty);
				return false;
			}
		}
    }
}