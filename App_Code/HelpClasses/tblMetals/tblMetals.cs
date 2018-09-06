using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DBComponent;

namespace tblmetals
{
    /// <summary>
    /// Summary description for tblMetals
    /// </summary>
    public class tblMetals
    {
        #region Private Data Member Declarations

        private string _MetalID = null;
        private string _MetalName = null;
        private string _MetalVendorID = null;
        private string _Description = null;
        private float _MetalLoss;

        #endregion

        #region Public Properties Implmentations

        // Default Constructor
        public tblMetals()
        {
        }

        public string MetalID
        {
            get { return _MetalID;}
            set { _MetalID = value;}
        }

        public string MetalName
        {
            get { return _MetalName; }
            set { _MetalName = value; }
        }

        public string MetalVendorID
        {
            get { return _MetalVendorID; }
            set { _MetalVendorID = value; }
        }

        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        public float MetalLoss
        {
            get { return _MetalLoss; }
            set { _MetalLoss = value; }
        }

        #endregion

    }
}