using System;

namespace tbl_Newsletter
{
	/// <summary>
	/// Summary description for tbl_NewsletterOnly.
	/// </summary>
	public class tbl_Newsletter
	{
		public tbl_Newsletter()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		#region Private Variables
		private string _email_address = null;
		private string _ip_address = null;
		private string _date_signed = null;
        private int _Flag  = 0;
        #endregion
        #region Public Get Set Properties
        public string email_address
		{
			get{return _email_address;}
			set{_email_address=value;}
		}
		public string ip_address
		{
			get{return _ip_address;}
			set{_ip_address=value;}
		}
		public string date_signed
		{
			get{return _date_signed;}
			set{_date_signed=value;}
		}
        public int Flag
        {
            get { return _Flag; }
            set { _Flag = value; }
        }
        #endregion
    }
}