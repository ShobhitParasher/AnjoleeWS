using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for tblUsers
/// </summary>

namespace tblUsers
{
    public class tblUsers
    {
        public tblUsers()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        private string _UserID;
        private string _Name;
        private string _Password;
        private DateTime _FirstLogin;
        private DateTime _LastLogin;
        private DateTime _Creation;
        private string _AccessLevelID;

        private string _DomainName;

        private bool _MasterMerchant;
        private bool _Merchant;
        private bool _EndCustomer;
        private bool _DomainOwner;
        private bool _Approve;
        private string _PaymentTerms;
        private float _PremiumCharges;
        private string _MasterMerchantID;
        private string _MerchantID;



        public string DomainName
        {
            get { return _DomainName; }
            set { _DomainName = value; }
        }

        public string UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }
        public DateTime FirstLogin
        {
            get { return _FirstLogin; }
            set { _FirstLogin = value; }
        }
        public DateTime LastLogin
        {
            get { return _LastLogin; }
            set { _LastLogin = value; }
        }
        public DateTime Creation
        {
            get { return _Creation; }
            set { _Creation = value; }
        }
        public string AccessLevelID
        {
            get { return _AccessLevelID; }
            set { _AccessLevelID = value; }
        }

        public bool MasterMerchant
        {
            get { return _MasterMerchant; }
            set { _MasterMerchant = value; }
        }
        public bool Merchant
        {
            get { return _Merchant; }
            set { _Merchant = value; }
        }
        public bool EndCustomer
        {
            get { return _EndCustomer; }
            set { _EndCustomer = value; }
        }
        public bool DomainOwner
        {
            get { return _DomainOwner; }
            set { _DomainOwner = value; }
        }

        public bool Approve
        {
            get { return _Approve; }
            set { _Approve = value; }
        }

        public string PaymentTerms
        {
            get { return _PaymentTerms; }
            set { _PaymentTerms = value; }
        }
        public float PremiumCharges
        {
            get { return _PremiumCharges; }
            set { _PremiumCharges = value; }
        }
        public string MasterMerchantID
        {
            get { return _MasterMerchantID; }
            set { _MasterMerchantID = value; }
        }
        public string MerchantID
        {
            get { return _MerchantID; }
            set { _MerchantID = value; }
        }
    }
}