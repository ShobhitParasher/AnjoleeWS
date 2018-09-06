using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace tblimagestypes
{
    /// <summary>
    /// Summary description for tblImagesTypes
    /// </summary>
    public class tblImagesTypes
    {
        // Default Constructor
        public tblImagesTypes()
        {
        }

        #region Private Data Member Declaration

        private string _ImageTypeID;
        private string _ImageType;
        private string _Description;

        #endregion

        #region Public Properties Implementation

        public string ImageTypeID
        {
            get { return _ImageTypeID; }
            set { _ImageTypeID = value; }
        }

        public string ImageType
        {
            get { return _ImageType; }
            set { _ImageType = value; }
        }

        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        #endregion
    }
}