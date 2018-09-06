using System;

namespace tblPaymentMethods
{
	/// <summary>
	/// Summary description for tblPaymentMethods.
	/// </summary>
	public class tblPaymentMethods
	{
		public tblPaymentMethods()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		#region Private Declarations
		private int _paymentMethodID;
		private int _online;
		private	string _paymentMethodName;
		private string _shortDescription;
		private int _isDefault;
		private int _isCC;
		private string _acceptedCards;
		#endregion

		#region Public Get Set Properties
		public int paymentMethodID
		{
			get{return _paymentMethodID;}
			set{_paymentMethodID=value;}
		}
		public int online
		{
			get{return _online;}
			set{_online=value;}
		}
		public string paymentMethodName
		{
			get{return _paymentMethodName;}
			set{_paymentMethodName=value;}
		}
		public string shortDescription
		{
			get{return _shortDescription;}
			set{_shortDescription=value;}
		}
		public int isDefault
		{
			get{return _isDefault;}
			set{_isDefault=value;}
		}
		public int isCC
		{
			get{return _isCC;}
			set{_isCC=value;}
		}
		public string acceptedCards
		{
			get{return _acceptedCards;}
			set{_acceptedCards=value;}
		}
		#endregion
	}
}
