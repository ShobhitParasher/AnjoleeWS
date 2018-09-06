using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
namespace tblOrders
{
    /// <summary>
    /// Summary description for tblOrders
    /// </summary>
    public class tblOrders
    {
        public tblOrders()
        {
        }
        public tblOrders(int orderID)
        {
            tblOrdersHelper otblOrdersHelper = new tblOrdersHelper();
            DataSet ds = otblOrdersHelper.GetOrderByOrderID(orderID);
            if (ds.Tables[0].Rows.Count != 0)
            {
                this.CustomeServiceNumber = ds.Tables[0].Rows[0]["CustomerServiceNumber"].ToString(); 
                this.orderID = (int)ds.Tables[0].Rows[0]["orderID"];
                this.orderNo = ds.Tables[0].Rows[0]["orderNo"].ToString();
                this.orderAck = (bool)ds.Tables[0].Rows[0]["orderAck"] ? 1 : 0;
                this.orderCancelled = (bool)ds.Tables[0].Rows[0]["orderCancelled"] ? 1 : 0;
                this.customerID = ds.Tables[0].Rows[0]["customerID"].ToString(); 
                this.membershipID = (int)ds.Tables[0].Rows[0]["membershipID"];
                this.membershipType = ds.Tables[0].Rows[0]["membershipType"].ToString();
                this.sessionID = ds.Tables[0].Rows[0]["sessionID"].ToString();
                this.orderDate = (DateTime)ds.Tables[0].Rows[0]["orderDate"];
                this.orderTotal = (decimal)ds.Tables[0].Rows[0]["orderTotal"];
                this.taxAmountInTotal = (decimal)ds.Tables[0].Rows[0]["taxAmountInTotal"];
                this.taxAmountAdded = (decimal)ds.Tables[0].Rows[0]["taxAmountAdded"];
                this.taxDescription = ds.Tables[0].Rows[0]["taxDescription"].ToString();
                this.shippingAmount = (decimal)ds.Tables[0].Rows[0]["shippingAmount"];
                this.shippingMethod = (int)ds.Tables[0].Rows[0]["shippingMethod"];
                this.shippingDesc = ds.Tables[0].Rows[0]["shippingDesc"].ToString();
                this.feeAmount = (decimal)ds.Tables[0].Rows[0]["feeAmount"];
                this.paymentAmountRequired = (decimal)ds.Tables[0].Rows[0]["paymentAmountRequired"];
                this.paymentMethod = ds.Tables[0].Rows[0]["paymentMethod"].ToString();
                this.paymentMethodID = (int)ds.Tables[0].Rows[0]["paymentMethodID"];
                this.paymentMethodRDesc = ds.Tables[0].Rows[0]["paymentMethodRDesc"].ToString();
                this.paymentMethodIsCC = (bool)ds.Tables[0].Rows[0]["paymentMethodIsCC"] ? 1 : 0; ;
                this.paymentMethodIsSC = (bool)ds.Tables[0].Rows[0]["paymentMethodIsSC"] ? 1 : 0; ;
                this.cardNumber = ds.Tables[0].Rows[0]["cardNumber"].ToString();
                this.cardExpiryMonth = ds.Tables[0].Rows[0]["cardExpiryMonth"].ToString();
                this.cardExpiryYear = ds.Tables[0].Rows[0]["cardExpiryYear"].ToString();
                this.cardName = ds.Tables[0].Rows[0]["cardName"].ToString();
                this.cardType = ds.Tables[0].Rows[0]["cardType"].ToString();
                this.cardCCV = ds.Tables[0].Rows[0]["cardCCV"].ToString();
                this.cardStoreInfo = ds.Tables[0].Rows[0]["cardStoreInfo"].ToString();
                this.shipping_Company = ds.Tables[0].Rows[0]["shipping_Company"].ToString();
                this.shipping_FirstName = ds.Tables[0].Rows[0]["shipping_FirstName"].ToString();
                this.shipping_Surname = ds.Tables[0].Rows[0]["shipping_Surname"].ToString();
                this.shipping_Street = ds.Tables[0].Rows[0]["shipping_Street"].ToString();
                this.shipping_AptUnitNo = ds.Tables[0].Rows[0]["shipping_AptUnitNo"].ToString();
                this.shipping_Suburb = ds.Tables[0].Rows[0]["shipping_Suburb"].ToString();
                this.shipping_State = ds.Tables[0].Rows[0]["shipping_State"].ToString();
                this.shipping_PostCode = ds.Tables[0].Rows[0]["shipping_PostCode"].ToString();
                this.shipping_Country = ds.Tables[0].Rows[0]["shipping_Country"].ToString();
                this.shipping_Phone = ds.Tables[0].Rows[0]["shipping_Phone"].ToString();
                this.specialInstructions = ds.Tables[0].Rows[0]["specialInstructions"].ToString();
                this.paymentProcessed = (bool)ds.Tables[0].Rows[0]["paymentProcessed"] ? 1 : 0;
                this.paymentProcessedDate = (DateTime)ds.Tables[0].Rows[0]["paymentProcessedDate"];
                this.paymentSuccessful = (bool)ds.Tables[0].Rows[0]["paymentSuccessful"] ? 1 : 0;
                this.ipAddress = ds.Tables[0].Rows[0]["ipAddress"].ToString();
                this.referrer = ds.Tables[0].Rows[0]["referrer"].ToString();
                this.archived = (bool)ds.Tables[0].Rows[0]["archived"] ? 1 : 0;
                this.messageToCustomer = ds.Tables[0].Rows[0]["messageToCustomer"].ToString();
                this.CouponCode = ds.Tables[0].Rows[0]["CouponCode"].ToString();
                this.Shipping_Email = ds.Tables[0].Rows[0]["shipping_Email"].ToString();
                this.Billing_AptUnitNo = ds.Tables[0].Rows[0]["shipping_AptUnitNo"].ToString();

                if (ds.Tables[0].Rows[0]["CouponAmount"].ToString() != "")
                    this.CouponAmount = Convert.ToDecimal((ds.Tables[0].Rows[0]["CouponAmount"]));
                else
                    this.CouponAmount = 0;

                if (ds.Tables[0].Rows[0]["VoucherAmount"].ToString() != "")
                    this.CouponVoucherAmount = Convert.ToDecimal((ds.Tables[0].Rows[0]["VoucherAmount"]));
                else
                    this.CouponVoucherAmount = 0;
            }
        }
        #region Private variables Declaration
        private int _orderID = -1;
        private string _orderNo = string.Empty;
        private int _orderAck = -1;
        private int _orderCancelled = -1;
        private string _customerID = "";
        private int _membershipID = -1;
        private string _membershipType = string.Empty;
        private string _sessionID = string.Empty;
        private DateTime _orderDate;
        private decimal _orderTotal = -1;
        private decimal _taxAmountInTotal = -1;
        private decimal _taxAmountAdded = -1;
        private string _taxDescription = string.Empty;
        private decimal _shippingAmount = -1;
        private int _shippingMethod = -1;
        private string _shippingDesc = string.Empty;
        private decimal _feeAmount = -1;
        private decimal _paymentAmountRequired = -1;
        private string _paymentMethod = string.Empty;
        private int _paymentMethodID = -1;
        private string _paymentMethodRDesc = string.Empty;
        private int _paymentMethodIsCC = -1;
        private int _paymentMethodIsSC = -1;
        private string _cardNumber = string.Empty;
        private string _cardExpiryMonth = string.Empty;
        private string _cardExpiryYear = string.Empty;
        private string _cardName = string.Empty;
        private string _cardType = string.Empty;
        private string _cardCCV = string.Empty;
        private string _cardStoreInfo = string.Empty;
        private string _shipping_Company = string.Empty;
        private string _shipping_FirstName = string.Empty;
        private string _shipping_Surname = string.Empty;
        private string _shipping_Street = string.Empty;
        private string _shipping_Suburb = string.Empty;
        private string _shipping_State = string.Empty;
        private string _shipping_PostCode = string.Empty;
        private string _shipping_Country = string.Empty;
        private string _shipping_Phone = string.Empty;
        private string _shipping_AptUnitNo = string.Empty;

        private string _specialInstructions = string.Empty;
        private int _paymentProcessed = -1;
        private DateTime _paymentProcessedDate;
        private int _paymentSuccessful = -1;
        private string _ipAddress = string.Empty;
        private string _referrer = string.Empty;
        private int _archived = -1;
        private string _messageToCustomer = string.Empty;
        private int _shipping_Country_ID = 0;
        private string _CouponCode = string.Empty;
        private string _Email;
        private decimal _CouponAmount = 0;
        private string _Shipping_Email = string.Empty;

        private string _Billing_Address1 = string.Empty;
        private string _Billing_Address2 = string.Empty;
        private string _Billing_ZipCode = string.Empty;
        private string _Billing_AptUnitNo = string.Empty;
        private string _Billing_City = string.Empty;
        private string _CustomeServiceNumber = string.Empty;

        //Added by Kapil Arora
        private decimal _TransferAmount = 0;
        private string _PaypalTranstionID = string.Empty;
        private decimal _CouponVoucherAmount = 0;
        private int _CouponApplied = 0;
        private DateTime _CouponAppliedDate;

        #endregion

        #region Public Get Set Properties
        public int orderID
        {
            get { return _orderID; }
            set { _orderID = value; }
        }
        public string orderNo
        {
            get { return _orderNo; }
            set { _orderNo = value; }
        }

        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        public int orderAck
        {
            get { return _orderAck; }
            set { _orderAck = value; }
        }
        public int orderCancelled
        {
            get { return _orderCancelled; }
            set { _orderCancelled = value; }
        }
        public string customerID
        {
            get { return _customerID; }
            set { _customerID = value; }
        }
        public int membershipID
        {
            get { return _membershipID; }
            set { _membershipID = value; }
        }
        public string membershipType
        {
            get { return _membershipType; }
            set { _membershipType = value; }
        }
        public string sessionID
        {
            get { return _sessionID; }
            set { _sessionID = value; }
        }
        public DateTime orderDate
        {
            get { return _orderDate; }
            set
            {
                if (_orderDate == null)
                {
                    _orderDate = new DateTime();
                    _orderDate = value;

                }
                else
                {
                    _orderDate = value;
                }
            }
        }
        public decimal orderTotal
        {
            get { return _orderTotal; }
            set { _orderTotal = value; }
        }
        public decimal taxAmountInTotal
        {
            get { return _taxAmountInTotal; }
            set { _taxAmountInTotal = value; }
        }
        public decimal taxAmountAdded
        {
            get { return _taxAmountAdded; }
            set { _taxAmountAdded = value; }
        }
        public string taxDescription
        {
            get { return _taxDescription; }
            set { _taxDescription = value; }
        }
        public decimal shippingAmount
        {
            get { return _shippingAmount; }
            set { _shippingAmount = value; }
        }
        public int shippingMethod
        {
            get { return _shippingMethod; }
            set { _shippingMethod = value; }
        }
        public string shippingDesc
        {
            get { return _shippingDesc; }
            set { _shippingDesc = value; }
        }
        public decimal feeAmount
        {
            get { return _feeAmount; }
            set { _feeAmount = value; }
        }
        public decimal paymentAmountRequired
        {
            get { return _paymentAmountRequired; }
            set { _paymentAmountRequired = value; }
        }
        public string paymentMethod
        {
            get { return _paymentMethod; }
            set { _paymentMethod = value; }
        }
        public int paymentMethodID
        {
            get { return _paymentMethodID; }
            set { _paymentMethodID = value; }
        }
        public string paymentMethodRDesc
        {
            get { return _paymentMethodRDesc; }
            set { _paymentMethodRDesc = value; }
        }
        public int paymentMethodIsCC
        {
            get { return _paymentMethodIsCC; }
            set { _paymentMethodIsCC = value; }
        }
        public int paymentMethodIsSC
        {
            get { return _paymentMethodIsSC; }
            set { _paymentMethodIsSC = value; }
        }
        public string cardNumber
        {
            get { return _cardNumber; }
            set { _cardNumber = value; }
        }
        public string cardExpiryMonth
        {
            get { return _cardExpiryMonth; }
            set { _cardExpiryMonth = value; }
        }
        public string cardExpiryYear
        {
            get { return _cardExpiryYear; }
            set { _cardExpiryYear = value; }
        }
        public string cardName
        {
            get { return _cardName; }
            set { _cardName = value; }
        }
        public string cardType
        {
            get { return _cardType; }
            set { _cardType = value; }
        }
        public string cardCCV
        {
            get { return _cardCCV; }
            set { _cardCCV = value; }
        }
        public string cardStoreInfo
        {
            get { return _cardStoreInfo; }
            set { _cardStoreInfo = value; }
        }
        public string shipping_Company
        {
            get { return _shipping_Company; }
            set { _shipping_Company = value; }
        }
        public string shipping_FirstName
        {
            get { return _shipping_FirstName; }
            set { _shipping_FirstName = value; }
        }
        public string shipping_Surname
        {
            get { return _shipping_Surname; }
            set { _shipping_Surname = value; }
        }
        public string shipping_Street
        {
            get { return _shipping_Street; }
            set { _shipping_Street = value; }
        }
        public string shipping_Suburb
        {
            get { return _shipping_Suburb; }
            set { _shipping_Suburb = value; }
        }
        public string shipping_State
        {
            get { return _shipping_State; }
            set { _shipping_State = value; }
        }
        public string shipping_PostCode
        {
            get { return _shipping_PostCode; }
            set { _shipping_PostCode = value; }
        }
        public string shipping_Country
        {
            get { return _shipping_Country; }
            set { _shipping_Country = value; }
        }
        public string shipping_Phone
        {
            get { return _shipping_Phone; }
            set { _shipping_Phone = value; }
        }

        public string shipping_AptUnitNo
        {
            get { return _shipping_AptUnitNo; }
            set { _shipping_AptUnitNo = value; }
        }

        public string specialInstructions
        {
            get { return _specialInstructions; }
            set { _specialInstructions = value; }
        }
        public int paymentProcessed
        {
            get { return _paymentProcessed; }
            set { _paymentProcessed = value; }
        }
        public DateTime paymentProcessedDate
        {
            get { return _paymentProcessedDate; }
            set { _paymentProcessedDate = value; }
        }
        public int paymentSuccessful
        {
            get { return _paymentSuccessful; }
            set { _paymentSuccessful = value; }
        }
        public string ipAddress
        {
            get { return _ipAddress; }
            set { _ipAddress = value; }
        }
        public string referrer
        {
            get { return _referrer; }
            set { _referrer = value; }
        }
        public int archived
        {
            get { return _archived; }
            set { _archived = value; }
        }
        public string messageToCustomer
        {
            get { return _messageToCustomer; }
            set { _messageToCustomer = value; }
        }
        public int shipping_Country_ID
        {
            get { return _shipping_Country_ID; }
            set { _shipping_Country_ID = value; }
        }
        public string CouponCode
        {
            get { return _CouponCode; }
            set { _CouponCode = value; }
        }
        public decimal CouponAmount
        {
            get { return _CouponAmount; }
            set { _CouponAmount = value; }
        }
        public string Shipping_Email
        {
            get { return _Shipping_Email; }
            set { _Shipping_Email = value; }
        }

        //'''''''''''''''For Billing Detail
        public string Billing_Address1
        {
            get { return _Billing_Address1; }
            set { _Billing_Address1 = value; }
        }

        public string Billing_Address2
        {
            get { return _Billing_Address2; }
            set { _Billing_Address2 = value; }
        }
        
        public string Billing_ZipCode
        {
            get { return _Billing_ZipCode; }
            set { _Billing_ZipCode = value; }
        }
        public string Billing_AptUnitNo
        {
            get { return _Billing_AptUnitNo; }
            set { _Billing_AptUnitNo = value; }
        }
        public string Billing_City
        {
            get { return _Billing_City; }
            set { _Billing_City = value; }
        }
        public string CustomeServiceNumber
        {
            get { return _CustomeServiceNumber; }
            set { _CustomeServiceNumber = value; }
        }

        public string PaypalTranstionID
        {
            get { return _PaypalTranstionID; }
            set { _PaypalTranstionID = value; }
        }

        public decimal TransferAmount
        {
            get { return _TransferAmount; }
            set { _TransferAmount = value; }
        }

        // added Coupon amount 
        public decimal CouponVoucherAmount
        {
            get { return _CouponVoucherAmount; }
            set { _CouponVoucherAmount = value; }
        }

        public int CouponApplied
        {
            get { return _CouponApplied; }
            set { _CouponApplied = value; }
        }
        public DateTime CouponAppliedDate
        {
            get { return _CouponAppliedDate; }
            set
            {
                if (_CouponAppliedDate == null)
                {
                    _CouponAppliedDate = new DateTime();
                    _CouponAppliedDate = value;

                }
                else
                {
                    _CouponAppliedDate = value;
                }
            }
        }
        #endregion

    }
}
