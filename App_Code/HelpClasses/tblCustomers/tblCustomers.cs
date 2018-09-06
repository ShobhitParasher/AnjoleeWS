using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace tblCustomers
{
    /// <summary>
    /// Summary description for tblCustomers
    /// </summary>
    public class tblCustomers
    {
        public tblCustomers()
        {
        }
        #region Private Declration
        private string _customerID;
        private string _firstName = null;
        private string _surName = null;
        private string _company = null;
        private string _street = null;
        private string _suburb = null;
        private string _postCode = null;
        private string _state = null;
        private string _country = null;
        private string _phone = null;
        private string _fax = null;
        private string _mobilePhone = null;
        private string _email = null;
        private string _website = null;
        private string _customerPassword = null;
        private int _newsletter;
        private int _membershipType;
        private string _membershipNo = null;
        #endregion

        #region Public Properties
        public string customerID
        {
            get { return _customerID; }
            set { _customerID = value; }
        }
        public string firstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }
        public string surName
        {
            get { return _surName; }
            set { _surName = value; }
        }
        public string company
        {
            get { return _company; }
            set { _company = value; }
        }
        public string street
        {
            get { return _street; }
            set { _street = value; }
        }
        public string suburb
        {
            get { return _suburb; }
            set { _suburb = value; }
        }
        public string postCode
        {
            get { return _postCode; }
            set { _postCode = value; }
        }
        public string state
        {
            get { return _state; }
            set { _state = value; }
        }
        public string country
        {
            get { return _country; }
            set { _country = value; }
        }
        public string phone
        {
            get { return _phone; }
            set { _phone = value; }
        }
        public string fax
        {
            get { return _fax; }
            set { _fax = value; }
        }
        public string mobilePhone
        {
            get { return _mobilePhone; }
            set { _mobilePhone = value; }
        }
        public string email
        {
            get { return _email; }
            set { _email = value; }
        }
        public string website
        {
            get { return _website; }
            set { _website = value; }
        }
        public string customerPassword
        {
            get { return _customerPassword; }
            set { _customerPassword = value; }
        }
        public int newsletter
        {
            get { return _newsletter; }
            set { _newsletter = value; }
        }
        public int membershipType
        {
            get { return _membershipType; }
            set { _membershipType = value; }
        }
        public string membershipNo
        {
            get { return _membershipNo; }
            set { _membershipNo = value; }
        }

        #endregion

        public tblCustomers(string emailID, string password)
        {
            tblCustomersHelper otblCustomersHelper = new tblCustomersHelper();
            if (otblCustomersHelper.VerifyCustomer(emailID.Trim(), password.Trim()) == 1)
            {
                DataSet ds = otblCustomersHelper.GetCustomer(emailID.Trim(), password.Trim());
                this.customerID = ds.Tables[0].Rows[0]["customerID"].ToString().Trim();
                this.firstName = ds.Tables[0].Rows[0]["firstName"].ToString().Trim();
                this.surName = ds.Tables[0].Rows[0]["surname"].ToString().Trim();
                this.company = ds.Tables[0].Rows[0]["company"].ToString().Trim();
                this.street = ds.Tables[0].Rows[0]["street"].ToString().Trim();
                this.suburb = ds.Tables[0].Rows[0]["suburb"].ToString().Trim();
                this.postCode = ds.Tables[0].Rows[0]["postCode"].ToString().Trim();
                this.state = ds.Tables[0].Rows[0]["state"].ToString().Trim();
                this.country = ds.Tables[0].Rows[0]["country"].ToString().Trim();
                this.phone = ds.Tables[0].Rows[0]["phone"].ToString().Trim();
                this.fax = ds.Tables[0].Rows[0]["fax"].ToString().Trim();
                this.mobilePhone = ds.Tables[0].Rows[0]["mobilePhone"].ToString().Trim();
                this.email = ds.Tables[0].Rows[0]["email"].ToString().Trim();
                //if (ds.Tables[0].Rows[0]["website"].ToString() != null)
                //{
                    this.website = ds.Tables[0].Rows[0]["website"].ToString().Trim();
                //}
                //else
                //{
                //    this.website = string.Empty;
                //}
                this.customerPassword = ds.Tables[0].Rows[0]["customerPassword"].ToString().Trim();
                string test = ds.Tables[0].Rows[0]["newsletter"].ToString().Trim();
                if (((bool)ds.Tables[0].Rows[0]["newsletter"]) == true)
                {
                    this.newsletter = 1;
                }
                else
                {
                    this.newsletter = 0;
                }
                if (ds.Tables[0].Rows[0]["membershipType"].ToString().Trim() != "")
                {
                    this.membershipType = int.Parse(ds.Tables[0].Rows[0]["membershipType"].ToString().Trim());
                }
                this.membershipNo = ds.Tables[0].Rows[0]["membershipNo"].ToString().Trim();
            }
        }

        public tblCustomers(int customerID)
        {
            tblCustomersHelper otblCustomersHelper = new tblCustomersHelper();
            DataSet ds = otblCustomersHelper.GetCustomer(customerID);
            if (ds.Tables[0].Rows.Count != 0)
            {
                this.customerID = ds.Tables[0].Rows[0]["customerID"].ToString().Trim();
                this.firstName = ds.Tables[0].Rows[0]["firstName"].ToString().Trim();
                this.surName = ds.Tables[0].Rows[0]["surname"].ToString().Trim();
                this.company = ds.Tables[0].Rows[0]["company"].ToString().Trim();
                this.street = ds.Tables[0].Rows[0]["street"].ToString().Trim();
                this.suburb = ds.Tables[0].Rows[0]["suburb"].ToString().Trim();
                this.postCode = ds.Tables[0].Rows[0]["postCode"].ToString().Trim();
                this.state = ds.Tables[0].Rows[0]["state"].ToString().Trim();
                this.country = ds.Tables[0].Rows[0]["country"].ToString().Trim();
                this.phone = ds.Tables[0].Rows[0]["phone"].ToString().Trim();
                this.fax = ds.Tables[0].Rows[0]["fax"].ToString().Trim();
                this.mobilePhone = ds.Tables[0].Rows[0]["mobilePhone"].ToString().Trim();
                this.email = ds.Tables[0].Rows[0]["email"].ToString().Trim();
                this.website = ds.Tables[0].Rows[0]["website"].ToString().Trim();
                this.customerPassword = ds.Tables[0].Rows[0]["customerPassword"].ToString().Trim();
                string test = ds.Tables[0].Rows[0]["newsletter"].ToString().Trim();
                if (((bool)ds.Tables[0].Rows[0]["newsletter"]) == true)
                {
                    this.newsletter = 1;
                }
                else
                {
                    this.newsletter = 0;
                }
                this.membershipType = int.Parse(ds.Tables[0].Rows[0]["membershipType"].ToString().Trim());
                this.membershipNo = ds.Tables[0].Rows[0]["membershipNo"].ToString().Trim();
            }
        }

    }
}
