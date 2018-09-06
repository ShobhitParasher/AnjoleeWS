using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace tblproductsimages
{
    /// <summary>
    /// Summary description for tblProductsImages
    /// </summary>
    public class tblProductsImages
    {
        // Default Constructor
        public tblProductsImages()
        {
        }

        #region Private Data Member Declaration

        private string _ImageID;
        private string _ProductID;
        private string _ImageType;
        private int _Default=0;


        #endregion

        #region Public Properties Implmentation


        public string ImageID
        {
            get { return _ImageID; }
            set { _ImageID = value; }
        }
        public string ProductID
        {
            get { return _ProductID; }
            set { _ProductID = value; }
        }
        public string ImageType
        {
            get { return _ImageType; }
            set { _ImageType = value; }
        }

        public int Default
        {
            get { return _Default; }
            set { _Default = value; }
        }
        #endregion
    }

}