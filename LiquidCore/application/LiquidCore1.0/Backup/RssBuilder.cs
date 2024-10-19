///
///    Thanks to felipesabino
///    http://www.codeproject.com/KB/applications/SimpleRSSBuilder.aspx
///
using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace RssBuilder
{
    /// <summary>
    /// Static Class to Create a Xml Document for RSS Feeds
    /// </summary>
    public static class RssBuilder
    {
        public static XmlDocument BuildXML(DataSet ds, RssConfigurator configurator)
        {
            return BuildXML(ds.Tables[0], configurator); 
        }
        /// <summary>
        /// Creates a XML Document that represents a Rss 2.0 feed for a Given Data Table and RSS Configurator
        /// </summary>
        /// <param name="table">Data Table</param>
        /// <param name="configurator">RSS Configurator</param>
        public static XmlDocument BuildXML(DataTable table, RssConfigurator configurator)
        {

            #region Configure Description

            StringBuilder sbDescription = new StringBuilder();
            if (configurator.ExpressionDescription != null && configurator.ExpressionDescription != string.Empty)
            {
                sbDescription.Append(configurator.ExpressionDescription);
            }
            else
            {
                foreach (DataColumn dc in table.Columns)
                {
                    sbDescription.AppendFormat("{0}: [{0}]", dc.ColumnName);
                    sbDescription.AppendLine();
                }
            }

            string description = sbDescription.ToString();

            #endregion

            #region RSS Start Writer
            MemoryStream stm = new MemoryStream();
            XmlTextWriter writer = new XmlTextWriter(stm, System.Text.Encoding.UTF8);

            writer.Formatting = Formatting.Indented;
            writer.Indentation = 3;
            writer.IndentChar = ' ';


            writer.WriteStartDocument();

            //writer.WriteComment("This Is A List of My Books");

            #region RSS Beginning Tag

            writer.WriteStartElement("rss");
            writer.WriteAttributeString("version", "2.0");
            writer.WriteStartElement("channel");

            #region RSS Image

            if (configurator.ImageUrl != string.Empty && configurator.ImageUrl != null)
            {
                writer.WriteStartElement("image");

                writer.WriteElementString("url", configurator.ImageUrl);
                writer.WriteElementString("title", configurator.Title);
                writer.WriteElementString("link", configurator.Link);

                writer.WriteEndElement();
            }

            #endregion

            #region RSS Details

            writer.WriteElementString("title", configurator.Title);
            writer.WriteElementString("link", configurator.Link);

            #endregion

            #region Itens

            foreach (DataRow row in table.Rows)
            {
                writer.WriteStartElement("item");

                #region Item Title

                if (configurator.ExpressionTitle != null && configurator.ExpressionTitle != string.Empty)
                    writer.WriteElementString("title", ReplaceDataRowString(configurator.ExpressionTitle, row));
                else
                    writer.WriteElementString("title", description.Substring(0, 20) + "...");

                #endregion

                #region Item Link

                writer.WriteElementString("link", ReplaceDataRowString(configurator.ExpressionLink, row));

                #endregion

                #region Item Date
                string date = ReplaceDataRowString(configurator.ExpressionDate, row);

                try
                {
                    date = Convert.ToDateTime(date).ToString("r");
                    writer.WriteElementString("pubDate", ReplaceDataRowString(configurator.ExpressionDate, row));
                }
                catch
                {
                }

                #endregion

                #region Item Description

                writer.WriteStartElement("description");
                writer.WriteCData(ReplaceDataRowString(description, row));
                writer.WriteEndElement();

                #endregion

                writer.WriteEndElement();
            }


            #endregion

            writer.WriteEndElement();
            writer.WriteEndElement();

            #endregion

            writer.WriteEndDocument();

            #endregion

            #region End Writer and Return XML Document

            writer.Flush();

            stm.Seek(0, SeekOrigin.Begin);

            XmlTextReader xr = new XmlTextReader(stm);
            XmlDocument xmldoc = new XmlDocument();


            xmldoc.Load(xr);

            return xmldoc;

            #endregion

        }


        /// <summary>
        /// This method Replaces the String values by the DataRow values
        /// </summary>
        /// <param name="value">string expression</param>
        /// <param name="row">DataRow</param>
        /// <returns></returns>
        private static string ReplaceDataRowString(string value, DataRow row)
        {
            string valueReturn = value;

            foreach (DataColumn dc in row.Table.Columns)
            {
                //this line replaces all [COlumn Name] Values for the value in the Data Row.
                valueReturn = valueReturn.Replace(string.Format("[{0}]", dc.ColumnName.ToString()), row[dc.ColumnName].ToString());
            }

            return valueReturn;
        }

    }
}