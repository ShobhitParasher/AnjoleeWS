using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
namespace tblStore_Prefs
{
    /// <summary>
    /// Summary description for tblStorePrefs
    /// </summary>
    public class tblStorePrefs
    {
        public tblStorePrefs()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        private int _ID;
        private string _Storelocale;
        private string _StoreName;
        private string _StorePageTitle;
        private string _Street;
        private string _suburb;
        private string _postCode;
        private string _state;
        private string _country;
        private string _phone;
        private string _fax;
        private string _email;
        private string _website;
        private string _storeClosed;
        private string _storeClosedMessage;
        private int _CityID;
        private string _cityName;

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public string Storelocale
        {
            get { return _Storelocale; }
            set { _Storelocale = value; }
        }
        public string StoreName
        {
            get { return _StoreName; }
            set { _StoreName = value; }
        }
        public string StorePageTitle
        {
            get { return _StorePageTitle; }
            set { _StorePageTitle = value; }
        }
        public string Street
        {
            get { return _Street; }
            set { _Street = value; }
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
        //tblCountries
        private int _countryID;
        private string _countryName;
        private int _countryVisible;
        private int _countryDefault;

        public int countryID
        {
            get { return _countryID; }
            set { _countryID = value; }
        }
        public string countryName
        {
            get { return _countryName; }
            set { _countryName = value; }
        }
        public int countryVisible
        {
            get { return _countryVisible; }
            set { _countryVisible = value; }
        }
        public int countryDefault
        {
            get { return _countryDefault; }
            set { _countryDefault = value; }
        }
        //tblCountry_States
        private int _stateID;
        private string _stateName;
        public int stateID
        {
            get { return _stateID; }
            set { _stateID = value; }
        }
        public string stateName
        {
            get { return _stateName; }
            set { _stateName = value; }
        }
        public string storeClosed
        {
            get { return _storeClosed; }
            set { _storeClosed = value; }
        }
        public string storeClosedMessage
        {
            get { return _storeClosedMessage; }
            set { _storeClosedMessage = value; }
        }
        public int CityID
        {
            get { return _CityID; }
            set { _CityID = value; }
        }
        public string cityName
        {
            get { return _cityName; }
            set { _cityName = value; }
        }
    }
}