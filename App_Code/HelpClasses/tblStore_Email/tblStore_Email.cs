using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
namespace tblStore_Email
{
    /// <summary>
    /// Summary description for tblStore_Email
    /// </summary>
    public class tblStore_Email
    {
        public tblStore_Email()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #region Private variables Declaration
        private string _emailSystemServer;
        private int _ccStaff;
        private string _staffEmail1;
        private string _staffEmail2;
        private string _staffEmail3;
        private string _emailFromAddress;
        private string _confirmSubject;
        private string _confirmEmail;
        private string _confirmEmailPartial;
        private int _requestConfirmTrackingN;
        private int _emailCustomerReceipt;
        private string _receiptSubject;
        private string _receiptEmail;
        #endregion

        #region Public Get Set Properties
        public string emailSystemServer
        {
            get { return _emailSystemServer; }
            set { _emailSystemServer = value; }
        }
        public int ccStaff
        {
            get { return _ccStaff; }
            set { _ccStaff = value; }
        }
        public string staffEmail1
        {
            get { return _staffEmail1; }
            set { _staffEmail1 = value; }
        }
        public string staffEmail2
        {
            get { return _staffEmail2; }
            set { _staffEmail2 = value; }
        }
        public string staffEmail3
        {
            get { return _staffEmail3; }
            set { _staffEmail3 = value; }
        }
        public string emailFromAddress
        {
            get { return _emailFromAddress; }
            set { _emailFromAddress = value; }
        }
        public string confirmSubject
        {
            get { return _confirmSubject; }
            set { _confirmSubject = value; }
        }
        public string confirmEmail
        {
            get { return _confirmEmail; }
            set { _confirmEmail = value; }
        }
        public string confirmEmailPartial
        {
            get { return _confirmEmailPartial; }
            set { _confirmEmailPartial = value; }
        }
        public int requestConfirmTrackingNo
        {
            get { return _requestConfirmTrackingN; }
            set { _requestConfirmTrackingN = value; }
        }
        public string receiptSubject
        {
            get { return _receiptSubject; }
            set { _receiptSubject = value; }
        }
        public string receiptEmail
        {
            get { return _receiptEmail; }
            set { _receiptEmail = value; }
        }
        public int emailCustomerReceipt
        {
            get { return _emailCustomerReceipt; }
            set { _emailCustomerReceipt = value; }
        }
        #endregion
    }
}
