using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace tblcompanies
{
    /// <summary>
    /// Summary description for tblCompanies
    /// </summary>
    public class tblCompanies
    {
        // Default Constructor
        public tblCompanies() { }

        # region Private Data Member Declaration

        private string _CompanyID;
        private string _CompanyName = null;
        private string _JBTAccoNo = null;
        private string _SiteStreet = null;
        private string _SiteCity = null;
        private string _SiteState = null;
        private string _SiteCountry = null;
        private string _SiteZipCode = null;
        private string _ShippingStreet = null;
        private string _ShippingCity = null;
        private string _ShippingState = null;
        private string _ShippingCountry = null;
        private string _ShippingZipCode = null;
        private string _Telephone1 = null;
        private string _Telephone2 = null;
        private string _Telephone3 = null;
        private string _Telephone4 = null;
        private string _Telephone5 = null;
        private string _PaymentTerms = null;
        private string _ReturnPolicyID;
        private string _AnnualSalesRangeFrom = null;
        private string _AnnualSalesRangeTo = null;
        private Boolean _IsActive = false;
        private string _TypeID;
        private string _RefCompany;
        
        #endregion

        # region Public Properties Implementation

        public string CompanyID
        {
            get { return _CompanyID; }
            set { _CompanyID = value; }
        }

        public string CompanyName
        {
            get { return _CompanyName; }
            set { _CompanyName = value; }
        }

        public string JBTAccoNo
        {
            get { return _JBTAccoNo; }
            set { _JBTAccoNo = value; }
        }

        public string SiteStreet
        {
            get { return _SiteStreet; }
            set { _SiteStreet = value; }
        }

        public string SiteCity
        {
            get { return _SiteCity; }
            set { _SiteCity = value; }
        }

        public string SiteState
        {
            get { return _SiteState; }
            set { _SiteState = value; }
        }

        public string SiteCountry
        {
            get { return _SiteCountry; }
            set { _SiteCountry = value; }
        }

        public string SiteZipCode
        {
            get { return _SiteZipCode; }
            set { _SiteZipCode = value; }
        }

        public string ShippingStreet
        {
            get { return _ShippingStreet; }
            set { _ShippingStreet = value; }
        }

        public string ShippingCity
        {
            get { return _ShippingCity; }
            set { _ShippingCity = value; }
        }

        public string ShippingState
        {
            get { return _ShippingState; }
            set { _ShippingState = value; }
        }

        public string ShippingCountry
        {
            get { return _ShippingCountry; }
            set { _ShippingCountry = value; }
        }

        public string ShippingZipCode
        {
            get { return _ShippingZipCode; }
            set { _ShippingZipCode = value; }
        }

        public string Telephone1
        {
            get { return _Telephone1; }
            set { _Telephone1 = value; }
        }

        public string Telephone2
        {
            get { return _Telephone2; }
            set { _Telephone2 = value; }
        }

        public string Telephone3
        {
            get { return _Telephone3; }
            set { _Telephone3 = value; }
        }

        public string Telephone4
        {
            get { return _Telephone4; }
            set { _Telephone4 = value; }
        }

        public string Telephone5
        {
            get { return _Telephone5; }
            set { _Telephone5 = value; }
        }

        public string PaymentTerms
        {
            get { return _PaymentTerms; }
            set { _PaymentTerms = value; }
        }

        public string ReturnPolicyID
        {
            get { return _ReturnPolicyID; }
            set { _ReturnPolicyID = value; }
        }

        public string AnnualSalesRangeFrom
        {
            get { return _AnnualSalesRangeFrom; }
            set { _AnnualSalesRangeFrom = value; }
        }

        public string AnnualSalesRangeTo
        {
            get { return _AnnualSalesRangeTo; }
            set { _AnnualSalesRangeTo = value; }
        }

        public Boolean IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; }
        }

        public string TypeID
        {
            get { return _TypeID; }
            set { _TypeID = value; }
        }

        public string RefCompany
        {
            get { return _RefCompany; }
            set { _RefCompany = value; }
        }

        #endregion

        public tblCompanies(string userID)
        {
            tblCompaniesHelper otblcompanieshelper = new tblCompaniesHelper();
            DataSet ds = otblcompanieshelper.GetCompanies(userID);
            if (ds.Tables.Count != 0)
            {
                this.CompanyID = ds.Tables[0].Rows[0]["CompanyID"].ToString();
                this.CompanyName = ds.Tables[0].Rows[0]["CompanyName"].ToString();
                this.JBTAccoNo = ds.Tables[0].Rows[0]["JBTAccoNo"].ToString();
                this.SiteStreet = ds.Tables[0].Rows[0]["SiteStreet"].ToString();
                this.SiteCity = ds.Tables[0].Rows[0]["SiteCity"].ToString();
                this.SiteState = ds.Tables[0].Rows[0]["SiteState"].ToString();
                this.SiteCountry =  ds.Tables[0].Rows[0]["SiteCountry"].ToString();
                this.SiteZipCode =  ds.Tables[0].Rows[0]["SiteZipCode"].ToString();
                this.ShippingStreet=  ds.Tables[0].Rows[0]["ShippingStreet"].ToString();
                this.ShippingCity = ds.Tables[0].Rows[0]["ShippingCity"].ToString();
                this.ShippingState =  ds.Tables[0].Rows[0]["ShippingState"].ToString();
                this.ShippingCountry =  ds.Tables[0].Rows[0]["ShippingCountry"].ToString();
                this.ShippingZipCode =  ds.Tables[0].Rows[0]["ShippingZipCode"].ToString();
                this.Telephone1 = ds.Tables[0].Rows[0]["Telephone1"].ToString();
                this.Telephone2 = ds.Tables[0].Rows[0]["Telephone2"].ToString();
                this.Telephone3 = ds.Tables[0].Rows[0]["Telephone3"].ToString();
                this.Telephone4 = ds.Tables[0].Rows[0]["Telephone4"].ToString();
                this.Telephone5=ds.Tables[0].Rows[0]["Telephone5"].ToString();
                this.PaymentTerms =  ds.Tables[0].Rows[0]["PaymentTerms"].ToString();
                this.ReturnPolicyID =   ds.Tables[0].Rows[0]["ReturnPolicyID"].ToString();
                this.AnnualSalesRangeFrom = ds.Tables[0].Rows[0]["AnnualSalesRangeFrom"].ToString();
                this.AnnualSalesRangeTo = ds.Tables[0].Rows[0]["AnnualSalesRangeTo"].ToString();
                this.IsActive = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsActive"].ToString());
                this.TypeID = ds.Tables[0].Rows[0]["TypeID"].ToString();
                this.RefCompany = ds.Tables[0].Rows[0]["RefCompany"].ToString();
            }
       }
    }
}