using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace tblimages
{
    /// <summary>
    /// Summary description for tblImages
    /// </summary>
    public class tblImages
    {
        // Default Constructor
        public tblImages()
        {
        }

        #region Private Data Declaration

        private string _ImageID;
        private string _ImageName;
        private string _ImageType;


        #endregion

        #region  Public Properties Implementation

        public string ImageID
        {
            get { return _ImageID; }
            set { _ImageID = value; }
        }

        public string ImageName
        {
            get { return _ImageName; }
            set { _ImageName = value; }
        }

        public string ImageType
        {
            get { return _ImageType; }
            set { _ImageType = value; }
        }

        #endregion

    }
}