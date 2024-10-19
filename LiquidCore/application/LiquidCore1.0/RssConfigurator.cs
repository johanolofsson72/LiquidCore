///
///    Thanks to felipesabino
///    http://www.codeproject.com/KB/applications/SimpleRSSBuilder.aspx
///
using System;
using System.Text;

namespace RssBuilder
{
    /// <summary>
    /// Summary description for RssConfigurator
    /// </summary>
    public class RssConfigurator
    {
        public RssConfigurator()
        {
        }

        private string _title;

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        private string _imageUrl;

        public string ImageUrl
        {
            get { return _imageUrl; }
            set { _imageUrl = value; }
        }

        private string _link;

        public string Link
        {
            get { return _link; }
            set { _link = value; }
        }
        private string _expressionDate;

        public string ExpressionDate
        {
            get { return _expressionDate; }
            set { _expressionDate = value; }
        }
        private string _expressionLink;

        public string ExpressionLink
        {
            get { return _expressionLink; }
            set { _expressionLink = value; }
        }
        private string _expressionDescription;

        public string ExpressionDescription
        {
            get { return _expressionDescription; }
            set { _expressionDescription = value; }
        }
        private string _expressionTitle;

        public string ExpressionTitle
        {
            get { return _expressionTitle; }
            set { _expressionTitle = value; }
        }

    }
}