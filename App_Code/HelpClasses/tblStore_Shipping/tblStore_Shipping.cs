using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
namespace tblStore_Shipping
{
    /// <summary>
    /// Summary description for tblStore_Shipping
    /// </summary>
    public class tblStore_Shipping
    {
        public tblStore_Shipping()
        {
            
            // TODO: Add constructor logic here
            //
        }
        #region Private variables Declaration
        private string _description = "";
        private decimal _price;
        private string _method = "";
        private string _AccessLicenseNumber = "";
        private string _UserId = "";
        private string _Password = "";
        #endregion

        #region Private function Declaration
        public string description
        {
            get { return _description; }
            set { _description = value; }
        }
        public decimal price
        {
            get { return _price; }
            set { _price = value; }
        }
        public string method
        {
            get { return _method; }
            set { _method = value; }
        }
        public string AccessLicenseNumber
        {
            get { return _AccessLicenseNumber; }
            set { _AccessLicenseNumber = value; }
        }
        public string UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }
        public string Password
        {
            get { return _Password; }
            set { _Password = value; }

        }
        #endregion
    }
}