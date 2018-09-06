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
/// Summary description for tblContacts
/// </summary>
namespace tblContacts
{
    public class tblContacts
    {
        public tblContacts()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region Private Variable Declaration
        private string _ContactID;
        private string _UserID;
        private string _Cell;
        private string _Fax;
        private string _Telephone1;
        private string _Telephone2;
        private string _Telephone3;
        private string _FirstName;
        private string _MiddleName;
        private string _LastName;
        private string _Email;
        private DateTime _DOB;
        private string _Street;
        private string _City;
        private string _State;
        private string _Country;
        private string _ZipCode;
        private string _Position;
        private string _CompanyID;
        private string _Address2;

        private bool _Newsletters;
        private string _Gender;
        private string _HearAbtUs;
        private string _AptUnitNo;
        private string _Email2;
        private string _Email3;
        private string _JBTAccountNo;
        private string _CompanyName;
        




        #endregion


        #region public variable Declaration

        public string JBTAccountNo
        {
            get { return _JBTAccountNo; }
            set { _JBTAccountNo = value; }
        }

        public string Email2
        {
            get { return _Email2; }
            set { _Email2 = value; }
        }
        public string Email3
        {
            get { return _Email3; }
            set { _Email3 = value; }
        }

        public bool Newsletters
        {
            get { return _Newsletters; }
            set { _Newsletters = value; }
        }

        public string HearAbtUs
        {
            get { return _HearAbtUs; }
            set { _HearAbtUs = value; }
        }

        public string AptUnitNo
        {
            get { return _AptUnitNo; }
            set { _AptUnitNo = value; }
        }
        
        public string Gender
        {
            get { return _Gender; }
            set { _Gender = value; }
        }


        public string ContactID
        {
            get { return _ContactID; }
            set { _ContactID = value; }
        }
        public string UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }
        public string Cell
        {
            get { return _Cell; }
            set { _Cell = value; }
        }
        public string Fax
        {
            get { return _Fax; }
            set { _Fax = value; }
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
        public string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }
        public string MiddleName
        {
            get { return _MiddleName; }
            set { _MiddleName = value; }
        }
        public string LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }

        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }
        public DateTime DOB
        {
            get { return _DOB; }
            set { _DOB = value; }
        }
        public string Street
        {
            get { return _Street; }
            set { _Street = value; }
        }
        public string City
        {
            get { return _City; }
            set { _City = value; }
        }
        public string State
        {
            get { return _State; }
            set { _State = value; }
        }
        public string Country
        {
            get { return _Country; }
            set { _Country = value; }
        }
        public string ZipCode
        {
            get { return _ZipCode; }
            set { _ZipCode = value; }
        }

        public string Position
        {
            get { return _Position; }
            set { _Position = value; }
        }

        public string CompanyID
        {
            get { return _CompanyID; }
            set { _CompanyID = value; }
        }

        public string Address2
        {
            get { return _Address2; }
            set { _Address2 = value; }
        }
        public string CompanyName
        {
            get { return _CompanyName; }
            set { _CompanyName = value; }
        }
        
        



        #endregion

        public tblContacts(string UID)
        {
            tblContactsHelper otblContactsHelper = new tblContactsHelper();
            DataSet ds = otblContactsHelper.GetContacts(UID);
            if (ds.Tables[0].Rows.Count != 0)
            {
                this.ContactID = ds.Tables[0].Rows[0]["ContactID"].ToString().Trim();
                this.Cell = ds.Tables[0].Rows[0]["Cell"].ToString().Trim();
                this.Telephone1 = ds.Tables[0].Rows[0]["Telephone1"].ToString();
                this.Telephone2 = ds.Tables[0].Rows[0]["Telephone2"].ToString();
                this.Telephone3 = ds.Tables[0].Rows[0]["Telephone3"].ToString();
                this.Fax = ds.Tables[0].Rows[0]["Fax"].ToString().Trim();
                this.FirstName = ds.Tables[0].Rows[0]["FirstName"].ToString().Trim();
                this.MiddleName = ds.Tables[0].Rows[0]["MiddleName"].ToString().Trim();
                this.LastName = ds.Tables[0].Rows[0]["LastName"].ToString().Trim();
                this.Email = ds.Tables[0].Rows[0]["Email"].ToString().Trim();
                //this.DOB = Convert.ToDateTime(ds.Tables[0].Rows[0]["DOB"].ToString().Trim());
                this.Street = ds.Tables[0].Rows[0]["Street"].ToString().Trim();
                this.City = ds.Tables[0].Rows[0]["City"].ToString().Trim();
                this.State = ds.Tables[0].Rows[0]["State"].ToString().Trim();
                this.Country = ds.Tables[0].Rows[0]["Country"].ToString().Trim();
                this.Position = ds.Tables[0].Rows[0]["Position"].ToString().Trim();
                this.CompanyID = ds.Tables[0].Rows[0]["CompanyID"].ToString().Trim();
                this.ZipCode = ds.Tables[0].Rows[0]["ZipCode"].ToString().Trim();
                this.Address2 = ds.Tables[0].Rows[0]["Address2"].ToString().Trim();
                this.HearAbtUs = ds.Tables[0].Rows[0]["HearAbtUs"].ToString().Trim();
                this.AptUnitNo = ds.Tables[0].Rows[0]["AptUnitNo"].ToString().Trim();
            }
        }

        public tblContacts(string UID, string NA)
        {
            tblContactsHelper otblContactsHelper = new tblContactsHelper();
            DataSet ds = otblContactsHelper.GetContacts1(UID);
            if (ds.Tables[0].Rows.Count != 0)
            {
                this.ContactID = ds.Tables[0].Rows[0]["ContactID"].ToString().Trim();
                this.Cell = ds.Tables[0].Rows[0]["Cell"].ToString().Trim();
                this.Telephone1 = ds.Tables[0].Rows[0]["Telephone1"].ToString();
                this.Telephone2 = ds.Tables[0].Rows[0]["Telephone2"].ToString();
                this.Telephone3 = ds.Tables[0].Rows[0]["Telephone3"].ToString();
                this.Fax = ds.Tables[0].Rows[0]["Fax"].ToString().Trim();
                this.FirstName = ds.Tables[0].Rows[0]["FirstName"].ToString().Trim();
                this.MiddleName = ds.Tables[0].Rows[0]["MiddleName"].ToString().Trim();
                this.LastName = ds.Tables[0].Rows[0]["LastName"].ToString().Trim();
                this.Email = ds.Tables[0].Rows[0]["Email"].ToString().Trim();
                //this.DOB = Convert.ToDateTime(ds.Tables[0].Rows[0]["DOB"].ToString().Trim());
                this.AptUnitNo = ds.Tables[0].Rows[0]["AptUnitNo"].ToString().Trim();
                this.Address2 = ds.Tables[0].Rows[0]["Address2"].ToString().Trim();
                this.Street = ds.Tables[0].Rows[0]["Street"].ToString().Trim();
                this.City = ds.Tables[0].Rows[0]["City"].ToString().Trim();
                this.State = ds.Tables[0].Rows[0]["State"].ToString().Trim();
                this.Country = ds.Tables[0].Rows[0]["Country"].ToString().Trim();
                this.Position = ds.Tables[0].Rows[0]["Position"].ToString().Trim();
                this.CompanyID = ds.Tables[0].Rows[0]["CompanyID"].ToString().Trim();
                this.ZipCode = ds.Tables[0].Rows[0]["ZipCode"].ToString().Trim();
                this.AptUnitNo = ds.Tables[0].Rows[0]["AptUnitNo"].ToString().Trim();
            }
        }


    }

}
