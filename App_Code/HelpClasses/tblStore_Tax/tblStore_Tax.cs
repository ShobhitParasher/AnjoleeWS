using System;

namespace tblStore_Tax
{
	/// <summary>
	/// Summary description for tblStore_Tax.
	/// </summary>
	public class tblStore_Tax
	{
		public tblStore_Tax()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		#region Private variables Declaration
		private int _taxID;
		private int _countryID;
		private int _stateID;
		private string _description;
		private decimal _rate;
        private string _zipcode;
		#endregion

		#region Public Get Set Properties
		public int taxID
		{
			get{return _taxID;}
			set{_taxID=value;}
		}
		public int countryID
		{
			get{return _countryID;}
			set{_countryID=value;}
		}
		public int stateID
		{
			get{return _stateID;}
			set{_stateID=value;}
		}
		public string description
		{
			get{return _description;}
			set{_description=value;}
		}
        public string zipcode
        {
            get { return _zipcode; }
            set { _zipcode = value; }
        }
		public decimal rate
		{
			get{return _rate;}
			set{_rate=value;}
		}
		#endregion
	}
}
