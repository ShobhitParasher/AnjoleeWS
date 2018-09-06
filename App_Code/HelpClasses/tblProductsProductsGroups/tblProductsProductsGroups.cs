using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace tblproductsproductsgroups
{
    /// <summary>
    /// Summary description for tblProductsProductsGroups
    /// </summary>
    public class tblProductsProductsGroups
    {

        #region Private Data Member Declaration
        
        private string _ProductsProductsGroupsID;
        private string _ProductID;

        #endregion

        #region Public Properties

        // Default Constructor
        public tblProductsProductsGroups()
        {
        }

        public string ProductsProductsGroupsID
        {
            get { return _ProductsProductsGroupsID; }
            set { _ProductsProductsGroupsID = value; }
        }
        public string ProductID
        {
            get { return _ProductID;}
            set { _ProductID = value;}
        }

        #endregion
        
    }
}