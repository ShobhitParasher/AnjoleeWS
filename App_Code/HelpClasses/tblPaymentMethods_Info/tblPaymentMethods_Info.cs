using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
namespace tblPaymentMethods_Info
{
    /// <summary>
    /// Summary description for tblPaymentMethods_Info
    /// </summary>
    public class tblPaymentMethods_Info
    {
        public tblPaymentMethods_Info()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #region Private Declarations
        private string _userName;
        private string _userPassword;
        private string _userKey;
        private string _emailID;
        private string _vendorName;
        private string _partnerName;
        private string _comment;
        private int _LiveMode;
        #endregion

        #region Public Get Set Properties
        public string userName
        {
            get { return _userName; }
            set { _userName = value; }
        }
        public string userPassword
        {
            get { return _userPassword; }
            set { _userPassword = value; }
        }
        public string userKey
        {
            get { return _userKey; }
            set { _userKey = value; }
        }
        public string emailID
        {
            get { return _emailID; }
            set { _emailID = value; }
        }
        public string vendorName
        {
            get { return _vendorName; }
            set { _vendorName = value; }
        }
        public string partnerName
        {
            get { return _partnerName; }
            set { _partnerName = value; }
        }
        public string comment
        {
            get { return _comment; }
            set { _comment = value; }
        }
        public int LiveMode
        {
            get { return _LiveMode; }
            set { _LiveMode = value; }
        }
        #endregion
    }
}