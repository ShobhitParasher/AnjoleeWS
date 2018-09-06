using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
namespace tblStore_Checkout
{

    /// <summary>
    /// Summary description for tblStore_Checkout
    /// </summary>
    public class tblStore_Checkout
    {
        public tblStore_Checkout()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        private string _Prefix;
        private bool _chkSSLCheckout;
        private bool _chkSSLAdmin;
        private string _WebsiteURL;
        private string _AdminURL;
        private string _CheckputSSLURL;
        private string _AdminSSLURL;
        public string Prefix
        {
            get { return _Prefix; }
            set { _Prefix = value; }
        }
        public bool chkSSLCheckout
        {
            get { return _chkSSLCheckout; }
            set { _chkSSLCheckout = value; }
        }
        public bool chkSSLAdmin
        {
            get { return _chkSSLAdmin; }
            set { _chkSSLAdmin = value; }
        }
        public string WebsiteURL
        {
            get { return _WebsiteURL; }
            set { _WebsiteURL = value; }
        }
        public string AdminURL
        {
            get { return _AdminURL; }
            set { _AdminURL = value; }
        }
        public string CheckputSSLURL
        {
            get { return _CheckputSSLURL; }
            set { _CheckputSSLURL = value; }
        }
        public string AdminSSLURL
        {
            get { return _AdminSSLURL; }
            set { _AdminSSLURL = value; }
        }
    }
}
