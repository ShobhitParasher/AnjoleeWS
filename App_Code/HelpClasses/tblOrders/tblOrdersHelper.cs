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
using DBComponent;

namespace tblOrders
{
    /// <summary>
    /// Summary description for tblOrdersHelper
    /// </summary>
    public class tblOrdersHelper
    {
        private DataBase db;
        SqlParameter[] param;
        DataSet ds;
        DBComponent.CommonFunctions objComFun = new DBComponent.CommonFunctions();
        public tblOrdersHelper()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public string InsertOrder(tblOrders otblOrders, string mode)
        {
            string OrderNo = string.Empty;
            try
            {
                if (object.Equals(db, null))
                {
                    db = new DataBase();
                }
                param = new SqlParameter[63];
                param[0] = db.MakeInParameter("@orderID", SqlDbType.Int, 4,

otblOrders.orderID);
                param[1] = db.MakeInParameter("@orderNo", SqlDbType.NVarChar, 50,

otblOrders.orderNo);
                param[2] = db.MakeInParameter("@orderAck", SqlDbType.Bit, 1,

otblOrders.orderAck);
                param[3] = db.MakeInParameter("@orderCancelled", SqlDbType.Bit, 1,

otblOrders.orderCancelled);
                param[4] = db.MakeInParameter("@customerID", otblOrders.customerID);
                param[5] = db.MakeInParameter("@membershipID", SqlDbType.Int, 4,

otblOrders.membershipID);
                param[6] = db.MakeInParameter("@membershipType", SqlDbType.NVarChar, 50,

otblOrders.membershipType);
                param[7] = db.MakeInParameter("@sessionID", SqlDbType.NVarChar, 50,

otblOrders.sessionID);
                param[8] = db.MakeInParameter("@orderDate", SqlDbType.DateTime, 8,

otblOrders.orderDate);
                param[9] = db.MakeInParameter("@orderTotal", SqlDbType.Decimal, 8,

otblOrders.orderTotal);
                param[10] = db.MakeInParameter("@taxAmountInTotal", SqlDbType.Decimal, 8,

otblOrders.taxAmountInTotal);
                param[11] = db.MakeInParameter("@taxAmountAdded", SqlDbType.Decimal, 8,

otblOrders.taxAmountAdded);
                param[12] = db.MakeInParameter("@taxDescription", SqlDbType.NVarChar, 30,

otblOrders.taxDescription);
                param[13] = db.MakeInParameter("@shippingAmount", SqlDbType.Decimal, 8,

otblOrders.shippingAmount);
                param[14] = db.MakeInParameter("@shippingMethod", SqlDbType.Int, 4,

otblOrders.shippingMethod);
                param[15] = db.MakeInParameter("@shippingDesc", SqlDbType.NText, 0,

otblOrders.shippingDesc);
                param[16] = db.MakeInParameter("@feeAmount", SqlDbType.Decimal, 8,

otblOrders.feeAmount);
                param[17] = db.MakeInParameter("@paymentAmountRequired", SqlDbType.Decimal,

8, otblOrders.paymentAmountRequired);
                param[18] = db.MakeInParameter("@paymentMethod", SqlDbType.NVarChar, 50,

otblOrders.paymentMethod);
                param[19] = db.MakeInParameter("@paymentMethodID", SqlDbType.Int, 4,

otblOrders.paymentMethodID);
                param[20] = db.MakeInParameter("@paymentMethodRDesc", SqlDbType.NText, 0,

otblOrders.paymentMethodRDesc);
                param[21] = db.MakeInParameter("@paymentMethodIsCC", SqlDbType.Bit, 1,

otblOrders.paymentMethodIsCC);
                param[22] = db.MakeInParameter("@paymentMethodIsSC", SqlDbType.Bit, 1,

otblOrders.paymentMethodIsSC);
                param[23] = db.MakeInParameter("@cardNumber", SqlDbType.NVarChar, 100,

otblOrders.cardNumber);
                param[24] = db.MakeInParameter("@cardExpiryMonth", SqlDbType.NVarChar, 10,

otblOrders.cardExpiryMonth);
                param[25] = db.MakeInParameter("@cardExpiryYear", SqlDbType.NVarChar, 10,

otblOrders.cardExpiryYear);
                param[26] = db.MakeInParameter("@cardName", SqlDbType.NVarChar, 100,

otblOrders.cardName);
                param[27] = db.MakeInParameter("@cardType", SqlDbType.NVarChar, 20,

otblOrders.cardType);
                param[28] = db.MakeInParameter("@cardCCV", SqlDbType.NVarChar, 4,

otblOrders.cardCCV);
                param[29] = db.MakeInParameter("@cardStoreInfo", SqlDbType.NVarChar, 20,

otblOrders.cardStoreInfo);
                param[30] = db.MakeInParameter("@shipping_Company", SqlDbType.NVarChar, 100,

otblOrders.shipping_Company);
                param[31] = db.MakeInParameter("@shipping_FirstName", SqlDbType.NVarChar,

50, otblOrders.shipping_FirstName);
                param[32] = db.MakeInParameter("@shipping_Surname", SqlDbType.NVarChar, 50,

otblOrders.shipping_Surname);
                param[33] = db.MakeInParameter("@shipping_Street", SqlDbType.NVarChar, 100,

otblOrders.shipping_Street);
                param[34] = db.MakeInParameter("@shipping_Suburb", SqlDbType.NVarChar, 50,

otblOrders.shipping_Suburb);
                param[35] = db.MakeInParameter("@shipping_State", SqlDbType.NVarChar, 50,

otblOrders.shipping_State);
                param[36] = db.MakeInParameter("@shipping_PostCode", SqlDbType.NVarChar, 15,

otblOrders.shipping_PostCode);
                param[37] = db.MakeInParameter("@shipping_Country", SqlDbType.NVarChar, 50,

otblOrders.shipping_Country);
                param[38] = db.MakeInParameter("@shipping_Phone", SqlDbType.NVarChar, 50,

otblOrders.shipping_Phone);
                param[39] = db.MakeInParameter("@specialInstructions", SqlDbType.NText, 0,

otblOrders.specialInstructions);
                param[40] = db.MakeInParameter("@paymentProcessed", SqlDbType.Bit, 1,

otblOrders.paymentProcessed);
                param[41] = db.MakeInParameter("@paymentProcessedDate", SqlDbType.DateTime,

8, otblOrders.paymentProcessedDate);
                param[42] = db.MakeInParameter("@paymentSuccessful", SqlDbType.Bit, 1,

otblOrders.paymentSuccessful);
                param[43] = db.MakeInParameter("@ipAddress", SqlDbType.NVarChar, 24,

otblOrders.ipAddress);
                param[44] = db.MakeInParameter("@referrer", SqlDbType.NVarChar, 255,

otblOrders.referrer);
                param[45] = db.MakeInParameter("@archived", SqlDbType.Bit, 1,

otblOrders.archived);
                param[46] = db.MakeInParameter("@messageToCustomer", SqlDbType.NText, 0,

otblOrders.messageToCustomer);
                param[47] = db.MakeInParameter("@CouponCode", SqlDbType.NVarChar, 255,

otblOrders.CouponCode);
                param[48] = db.MakeInParameter("@CouponAmount", SqlDbType.Decimal, 8,

otblOrders.CouponAmount);
                param[49] = db.MakeInParameter("@Mode", SqlDbType.VarChar, 10, mode);
                param[50] = db.MakeInParameter("@shipping_EmailID", SqlDbType.NVarChar, 50,

otblOrders.Shipping_Email);

                param[51] = db.MakeInParameter("@shipping_AptUnitNo", SqlDbType.NVarChar,

50, otblOrders.shipping_AptUnitNo);

                param[52] = db.MakeOutParameter("@Status", SqlDbType.VarChar, 30);

                param[53] = db.MakeInParameter("@Billing_Address1", SqlDbType.NVarChar, 100,

otblOrders.Billing_Address1);
                param[54] = db.MakeInParameter("@Billing_Address2", SqlDbType.NVarChar, 100,

otblOrders.Billing_Address2);
                param[55] = db.MakeInParameter("@Billing_AptUnitNo", SqlDbType.NVarChar, 50,

otblOrders.Billing_AptUnitNo);
                param[56] = db.MakeInParameter("@Billing_City", SqlDbType.NVarChar, 100,

otblOrders.Billing_City);
                param[57] = db.MakeInParameter("@Billing_ZipCode", SqlDbType.NVarChar, 20,

otblOrders.Billing_ZipCode);
                param[58] = db.MakeInParameter("@csPhNo", SqlDbType.NVarChar, 20, otblOrders.CustomeServiceNumber);
                param[59] = db.MakeInParameter("@TransferDiscount", SqlDbType.NVarChar, 20, otblOrders.TransferAmount);
                param[60] = db.MakeInParameter("@CouponVoucherAmount", SqlDbType.Decimal, 8, otblOrders.CouponVoucherAmount);
                param[61] = db.MakeInParameter("@Applied", SqlDbType.Bit, 1, otblOrders.CouponApplied);
                param[62] = db.MakeInParameter("@Applied_date", SqlDbType.DateTime, 8, otblOrders.CouponAppliedDate);
              

                //db.RunProcedure("P_InsertOrder_Iphone", param);
                db.RunProcedure("P_InsertOrder_new", param); // Added implement coupon amount

                OrderNo = param[52].Value.ToString();
            }
            catch (Exception ex)
            {
                string test = ex.ToString();
                string test1 = ex.Message;

            }
            finally
            {
                ResetAll();

            }
            return OrderNo;
        }

 public string InsertOrder_Ipad(tblOrders otblOrders, string mode)
        {
            string OrderNo = string.Empty;
            try
            {
                if (object.Equals(db, null))
                {
                    db = new DataBase();
                }
                param = new SqlParameter[63];
                param[0] = db.MakeInParameter("@orderID", SqlDbType.Int, 4,

otblOrders.orderID);
                param[1] = db.MakeInParameter("@orderNo", SqlDbType.NVarChar, 50,

otblOrders.orderNo);
                param[2] = db.MakeInParameter("@orderAck", SqlDbType.Bit, 1,

otblOrders.orderAck);
                param[3] = db.MakeInParameter("@orderCancelled", SqlDbType.Bit, 1,

otblOrders.orderCancelled);
                param[4] = db.MakeInParameter("@customerID", otblOrders.customerID);
                param[5] = db.MakeInParameter("@membershipID", SqlDbType.Int, 4,

otblOrders.membershipID);
                param[6] = db.MakeInParameter("@membershipType", SqlDbType.NVarChar, 50,

otblOrders.membershipType);
                param[7] = db.MakeInParameter("@sessionID", SqlDbType.NVarChar, 50,

otblOrders.sessionID);
                param[8] = db.MakeInParameter("@orderDate", SqlDbType.DateTime, 8,

otblOrders.orderDate);
                param[9] = db.MakeInParameter("@orderTotal", SqlDbType.Decimal, 8,

otblOrders.orderTotal);
                param[10] = db.MakeInParameter("@taxAmountInTotal", SqlDbType.Decimal, 8,

otblOrders.taxAmountInTotal);
                param[11] = db.MakeInParameter("@taxAmountAdded", SqlDbType.Decimal, 8,

otblOrders.taxAmountAdded);
                param[12] = db.MakeInParameter("@taxDescription", SqlDbType.NVarChar, 30,

otblOrders.taxDescription);
                param[13] = db.MakeInParameter("@shippingAmount", SqlDbType.Decimal, 8,

otblOrders.shippingAmount);
                param[14] = db.MakeInParameter("@shippingMethod", SqlDbType.Int, 4,

otblOrders.shippingMethod);
                param[15] = db.MakeInParameter("@shippingDesc", SqlDbType.NText, 0,

otblOrders.shippingDesc);
                param[16] = db.MakeInParameter("@feeAmount", SqlDbType.Decimal, 8,

otblOrders.feeAmount);
                param[17] = db.MakeInParameter("@paymentAmountRequired", SqlDbType.Decimal,

8, otblOrders.paymentAmountRequired);
                param[18] = db.MakeInParameter("@paymentMethod", SqlDbType.NVarChar, 50,

otblOrders.paymentMethod);
                param[19] = db.MakeInParameter("@paymentMethodID", SqlDbType.Int, 4,

otblOrders.paymentMethodID);
                param[20] = db.MakeInParameter("@paymentMethodRDesc", SqlDbType.NText, 0,

otblOrders.paymentMethodRDesc);
                param[21] = db.MakeInParameter("@paymentMethodIsCC", SqlDbType.Bit, 1,

otblOrders.paymentMethodIsCC);
                param[22] = db.MakeInParameter("@paymentMethodIsSC", SqlDbType.Bit, 1,

otblOrders.paymentMethodIsSC);
                param[23] = db.MakeInParameter("@cardNumber", SqlDbType.NVarChar, 100,

otblOrders.cardNumber);
                param[24] = db.MakeInParameter("@cardExpiryMonth", SqlDbType.NVarChar, 10,

otblOrders.cardExpiryMonth);
                param[25] = db.MakeInParameter("@cardExpiryYear", SqlDbType.NVarChar, 10,

otblOrders.cardExpiryYear);
                param[26] = db.MakeInParameter("@cardName", SqlDbType.NVarChar, 100,

otblOrders.cardName);
                param[27] = db.MakeInParameter("@cardType", SqlDbType.NVarChar, 20,

otblOrders.cardType);
                param[28] = db.MakeInParameter("@cardCCV", SqlDbType.NVarChar, 4,

otblOrders.cardCCV);
                param[29] = db.MakeInParameter("@cardStoreInfo", SqlDbType.NVarChar, 20,

otblOrders.cardStoreInfo);
                param[30] = db.MakeInParameter("@shipping_Company", SqlDbType.NVarChar, 100,

otblOrders.shipping_Company);
                param[31] = db.MakeInParameter("@shipping_FirstName", SqlDbType.NVarChar,

50, otblOrders.shipping_FirstName);
                param[32] = db.MakeInParameter("@shipping_Surname", SqlDbType.NVarChar, 50,

otblOrders.shipping_Surname);
                param[33] = db.MakeInParameter("@shipping_Street", SqlDbType.NVarChar, 100,

otblOrders.shipping_Street);
                param[34] = db.MakeInParameter("@shipping_Suburb", SqlDbType.NVarChar, 50,

otblOrders.shipping_Suburb);
                param[35] = db.MakeInParameter("@shipping_State", SqlDbType.NVarChar, 50,

otblOrders.shipping_State);
                param[36] = db.MakeInParameter("@shipping_PostCode", SqlDbType.NVarChar, 15,

otblOrders.shipping_PostCode);
                param[37] = db.MakeInParameter("@shipping_Country", SqlDbType.NVarChar, 50,

otblOrders.shipping_Country);
                param[38] = db.MakeInParameter("@shipping_Phone", SqlDbType.NVarChar, 50,

otblOrders.shipping_Phone);
                param[39] = db.MakeInParameter("@specialInstructions", SqlDbType.NText, 0,

otblOrders.specialInstructions);
                param[40] = db.MakeInParameter("@paymentProcessed", SqlDbType.Bit, 1,

otblOrders.paymentProcessed);
                param[41] = db.MakeInParameter("@paymentProcessedDate", SqlDbType.DateTime,

8, otblOrders.paymentProcessedDate);
                param[42] = db.MakeInParameter("@paymentSuccessful", SqlDbType.Bit, 1,

otblOrders.paymentSuccessful);
                param[43] = db.MakeInParameter("@ipAddress", SqlDbType.NVarChar, 24,

otblOrders.ipAddress);
                param[44] = db.MakeInParameter("@referrer", SqlDbType.NVarChar, 255,

otblOrders.referrer);
                param[45] = db.MakeInParameter("@archived", SqlDbType.Bit, 1,

otblOrders.archived);
                param[46] = db.MakeInParameter("@messageToCustomer", SqlDbType.NText, 0,

otblOrders.messageToCustomer);
                param[47] = db.MakeInParameter("@CouponCode", SqlDbType.NVarChar, 255,

otblOrders.CouponCode);
                param[48] = db.MakeInParameter("@CouponAmount", SqlDbType.Decimal, 8,

otblOrders.CouponAmount);
                param[49] = db.MakeInParameter("@Mode", SqlDbType.VarChar, 10, mode);
                param[50] = db.MakeInParameter("@shipping_EmailID", SqlDbType.NVarChar, 50,

otblOrders.Shipping_Email);

                param[51] = db.MakeInParameter("@shipping_AptUnitNo", SqlDbType.NVarChar,

50, otblOrders.shipping_AptUnitNo);

                param[52] = db.MakeOutParameter("@Status", SqlDbType.VarChar, 30);

                param[53] = db.MakeInParameter("@Billing_Address1", SqlDbType.NVarChar, 100,

otblOrders.Billing_Address1);
                param[54] = db.MakeInParameter("@Billing_Address2", SqlDbType.NVarChar, 100,

otblOrders.Billing_Address2);
                param[55] = db.MakeInParameter("@Billing_AptUnitNo", SqlDbType.NVarChar, 50,

otblOrders.Billing_AptUnitNo);
                param[56] = db.MakeInParameter("@Billing_City", SqlDbType.NVarChar, 100,

otblOrders.Billing_City);
                param[57] = db.MakeInParameter("@Billing_ZipCode", SqlDbType.NVarChar, 20,

otblOrders.Billing_ZipCode);
                param[58] = db.MakeInParameter("@csPhNo", SqlDbType.NVarChar, 20, otblOrders.CustomeServiceNumber);
                param[59] = db.MakeInParameter("@TransferDiscount", SqlDbType.NVarChar, 20, otblOrders.TransferAmount);
                param[60] = db.MakeInParameter("@CouponVoucherAmount", SqlDbType.Decimal, 8, otblOrders.CouponVoucherAmount);
                param[61] = db.MakeInParameter("@Applied", SqlDbType.Bit, 1, otblOrders.CouponApplied);
                param[62] = db.MakeInParameter("@Applied_date", SqlDbType.DateTime, 8, otblOrders.CouponAppliedDate);
              


                //db.RunProcedure("P_InsertOrder_Ipad", param);
                db.RunProcedure("P_InsertOrder_IpadNEW", param); // added coupon task
                OrderNo = param[52].Value.ToString();
            }
            catch (Exception ex)
            {
                string test = ex.ToString();
                string test1 = ex.Message;
                objComFun.ErrorLog(ex.Source, ex.Message, ex.TargetSite.ToString(), ex.StackTrace, "InsertOrder_Ipad");
            }
            finally
            {
                ResetAll();

            }
            return OrderNo;
        }


 public string InsertOrderApps(tblOrders otblOrders, string mode)
 {
     string OrderNo = string.Empty;
     try
     {
         if (object.Equals(db, null))
         {
             db = new DataBase();
         }
         param = new SqlParameter[64];
         param[0] = db.MakeInParameter("@orderID", SqlDbType.Int, 4,

otblOrders.orderID);
         param[1] = db.MakeInParameter("@orderNo", SqlDbType.NVarChar, 50,

otblOrders.orderNo);
         param[2] = db.MakeInParameter("@orderAck", SqlDbType.Bit, 1,

otblOrders.orderAck);
         param[3] = db.MakeInParameter("@orderCancelled", SqlDbType.Bit, 1,

otblOrders.orderCancelled);
         param[4] = db.MakeInParameter("@customerID", otblOrders.customerID);
         param[5] = db.MakeInParameter("@membershipID", SqlDbType.Int, 4,

otblOrders.membershipID);
         param[6] = db.MakeInParameter("@membershipType", SqlDbType.NVarChar, 50,

otblOrders.membershipType);
         param[7] = db.MakeInParameter("@sessionID", SqlDbType.NVarChar, 50,

otblOrders.sessionID);
         param[8] = db.MakeInParameter("@orderDate", SqlDbType.DateTime, 8,

otblOrders.orderDate);
         param[9] = db.MakeInParameter("@orderTotal", SqlDbType.Decimal, 8,

otblOrders.orderTotal);
         param[10] = db.MakeInParameter("@taxAmountInTotal", SqlDbType.Decimal, 8,

otblOrders.taxAmountInTotal);
         param[11] = db.MakeInParameter("@taxAmountAdded", SqlDbType.Decimal, 8,

otblOrders.taxAmountAdded);
         param[12] = db.MakeInParameter("@taxDescription", SqlDbType.NVarChar, 30,

otblOrders.taxDescription);
         param[13] = db.MakeInParameter("@shippingAmount", SqlDbType.Decimal, 8,

otblOrders.shippingAmount);
         param[14] = db.MakeInParameter("@shippingMethod", SqlDbType.Int, 4,

otblOrders.shippingMethod);
         param[15] = db.MakeInParameter("@shippingDesc", SqlDbType.NText, 0,

otblOrders.shippingDesc);
         param[16] = db.MakeInParameter("@feeAmount", SqlDbType.Decimal, 8,

otblOrders.feeAmount);
         param[17] = db.MakeInParameter("@paymentAmountRequired", SqlDbType.Decimal,

8, otblOrders.paymentAmountRequired);
         param[18] = db.MakeInParameter("@paymentMethod", SqlDbType.NVarChar, 50,

otblOrders.paymentMethod);
         param[19] = db.MakeInParameter("@paymentMethodID", SqlDbType.Int, 4,

otblOrders.paymentMethodID);
         param[20] = db.MakeInParameter("@paymentMethodRDesc", SqlDbType.NText, 0,

otblOrders.paymentMethodRDesc);
         param[21] = db.MakeInParameter("@paymentMethodIsCC", SqlDbType.Bit, 1,

otblOrders.paymentMethodIsCC);
         param[22] = db.MakeInParameter("@paymentMethodIsSC", SqlDbType.Bit, 1,

otblOrders.paymentMethodIsSC);
         param[23] = db.MakeInParameter("@cardNumber", SqlDbType.NVarChar, 100,

otblOrders.cardNumber);
         param[24] = db.MakeInParameter("@cardExpiryMonth", SqlDbType.NVarChar, 10,

otblOrders.cardExpiryMonth);
         param[25] = db.MakeInParameter("@cardExpiryYear", SqlDbType.NVarChar, 10,

otblOrders.cardExpiryYear);
         param[26] = db.MakeInParameter("@cardName", SqlDbType.NVarChar, 100,

otblOrders.cardName);
         param[27] = db.MakeInParameter("@cardType", SqlDbType.NVarChar, 20,

otblOrders.cardType);
         param[28] = db.MakeInParameter("@cardCCV", SqlDbType.NVarChar, 4,

otblOrders.cardCCV);
         param[29] = db.MakeInParameter("@cardStoreInfo", SqlDbType.NVarChar, 20,

otblOrders.cardStoreInfo);
         param[30] = db.MakeInParameter("@shipping_Company", SqlDbType.NVarChar, 100,

otblOrders.shipping_Company);
         param[31] = db.MakeInParameter("@shipping_FirstName", SqlDbType.NVarChar,

50, otblOrders.shipping_FirstName);
         param[32] = db.MakeInParameter("@shipping_Surname", SqlDbType.NVarChar, 50,

otblOrders.shipping_Surname);
         param[33] = db.MakeInParameter("@shipping_Street", SqlDbType.NVarChar, 100,

otblOrders.shipping_Street);
         param[34] = db.MakeInParameter("@shipping_Suburb", SqlDbType.NVarChar, 50,

otblOrders.shipping_Suburb);
         param[35] = db.MakeInParameter("@shipping_State", SqlDbType.NVarChar, 50,

otblOrders.shipping_State);
         param[36] = db.MakeInParameter("@shipping_PostCode", SqlDbType.NVarChar, 15,

otblOrders.shipping_PostCode);
         param[37] = db.MakeInParameter("@shipping_Country", SqlDbType.NVarChar, 50,

otblOrders.shipping_Country);
         param[38] = db.MakeInParameter("@shipping_Phone", SqlDbType.NVarChar, 50,

otblOrders.shipping_Phone);
         param[39] = db.MakeInParameter("@specialInstructions", SqlDbType.NText, 0,

otblOrders.specialInstructions);
         param[40] = db.MakeInParameter("@paymentProcessed", SqlDbType.Bit, 1,

otblOrders.paymentProcessed);
         param[41] = db.MakeInParameter("@paymentProcessedDate", SqlDbType.DateTime,

8, otblOrders.paymentProcessedDate);
         param[42] = db.MakeInParameter("@paymentSuccessful", SqlDbType.Bit, 1,

otblOrders.paymentSuccessful);
         param[43] = db.MakeInParameter("@ipAddress", SqlDbType.NVarChar, 24,

otblOrders.ipAddress);
         param[44] = db.MakeInParameter("@referrer", SqlDbType.NVarChar, 255,

otblOrders.referrer);
         param[45] = db.MakeInParameter("@archived", SqlDbType.Bit, 1,

otblOrders.archived);
         param[46] = db.MakeInParameter("@messageToCustomer", SqlDbType.NText, 0,

otblOrders.messageToCustomer);
         param[47] = db.MakeInParameter("@CouponCode", SqlDbType.NVarChar, 255,

otblOrders.CouponCode);
         param[48] = db.MakeInParameter("@CouponAmount", SqlDbType.Decimal, 8,

otblOrders.CouponAmount);
         param[49] = db.MakeInParameter("@Mode", SqlDbType.VarChar, 10, mode);
         param[50] = db.MakeInParameter("@shipping_EmailID", SqlDbType.NVarChar, 50,

otblOrders.Shipping_Email);

         param[51] = db.MakeInParameter("@shipping_AptUnitNo", SqlDbType.NVarChar,

50, otblOrders.shipping_AptUnitNo);

         param[52] = db.MakeOutParameter("@Status", SqlDbType.VarChar, 30);

         param[53] = db.MakeInParameter("@Billing_Address1", SqlDbType.NVarChar, 100,

otblOrders.Billing_Address1);
         param[54] = db.MakeInParameter("@Billing_Address2", SqlDbType.NVarChar, 100,

otblOrders.Billing_Address2);
         param[55] = db.MakeInParameter("@Billing_AptUnitNo", SqlDbType.NVarChar, 50,

otblOrders.Billing_AptUnitNo);
         param[56] = db.MakeInParameter("@Billing_City", SqlDbType.NVarChar, 100,

otblOrders.Billing_City);
         param[57] = db.MakeInParameter("@Billing_ZipCode", SqlDbType.NVarChar, 20,

otblOrders.Billing_ZipCode);
         param[58] = db.MakeInParameter("@csPhNo", SqlDbType.NVarChar, 20, otblOrders.CustomeServiceNumber);
         param[59] = db.MakeInParameter("@TransferDiscount", SqlDbType.NVarChar, 20, otblOrders.TransferAmount);
         param[60] = db.MakeInParameter("@CouponVoucherAmount", SqlDbType.Decimal, 8, otblOrders.CouponVoucherAmount);
         param[61] = db.MakeInParameter("@Applied", SqlDbType.Bit, 1, otblOrders.CouponApplied);
         param[62] = db.MakeInParameter("@Applied_date", SqlDbType.DateTime, 8, otblOrders.CouponAppliedDate);
         param[63] = db.MakeInParameter("@PaypalTranstionID", SqlDbType.NVarChar, 100, otblOrders.PaypalTranstionID);


         //db.RunProcedure("P_InsertOrder_Iphone", param);
         db.RunProcedure("P_InsertOrder_Apps", param); // Added implement coupon amount

         OrderNo = param[52].Value.ToString();
     }
     catch (Exception ex)
     {
         string test = ex.ToString();
         string test1 = ex.Message;

     }
     finally
     {
         ResetAll();

     }
     return OrderNo;
 }
        public DataSet SearchProductreport(string searchon, int seacrchin, int sortby, string sortdirection)
        {
            if (object.Equals(db, null))
            {
                db = new DataBase();
            }
            if (object.Equals(ds, null))
            {
                ds = new DataSet();
            }
            param = new SqlParameter[4];
            param[0] = db.MakeInParameter("@searchOn", SqlDbType.VarChar, 50, searchon);
            param[1] = db.MakeInParameter("@searchIn", SqlDbType.Int, 4, seacrchin);
            param[2] = db.MakeInParameter("@sortBy", SqlDbType.Int, 4, sortby);
            param[3] = db.MakeInParameter("@sortDirection", SqlDbType.VarChar, 50, sortdirection);
            db.RunProcedure("P_Productsreport", param, out ds);
            ResetAll();
            return ds;
            // int i =(int) ds.Tables[0].Rows.Count;
        }

        public DataSet SearchLowstockreport(string searchon, int seacrchin, int sortby, string sortdirection)
        {
            if (object.Equals(db, null))
            {
                db = new DataBase();
            }
            if (object.Equals(ds, null))
            {
                ds = new DataSet();
            }
            param = new SqlParameter[4];
            param[0] = db.MakeInParameter("@searchOn", SqlDbType.VarChar, 50, searchon);
            param[1] = db.MakeInParameter("@searchIn", SqlDbType.Int, 4, seacrchin);
            param[2] = db.MakeInParameter("@sortBy", SqlDbType.Int, 4, sortby);
            param[3] = db.MakeInParameter("@sortDirection", SqlDbType.VarChar, 50, sortdirection);
            db.RunProcedure("P_Lowstockreport", param, out ds);
            ResetAll();
            return ds;
        }

        public DataSet Searchreport(string sortby, string sortdirection, string TopRows)
        {
            if (object.Equals(db, null))
            {
                db = new DataBase();
            }
            if (object.Equals(ds, null))
            {
                ds = new DataSet();
            }
            param = new SqlParameter[3];
            param[0] = db.MakeInParameter("@sortBy", SqlDbType.VarChar, 50, sortby);
            param[1] = db.MakeInParameter("@TopRows", SqlDbType.VarChar, 3, TopRows);
            param[2] = db.MakeInParameter("@sortDirection", SqlDbType.VarChar, 50, sortdirection);
            db.RunProcedure("P_Searchreport", param, out ds);
            ResetAll();
            return ds;
        }
        /// <summary>
        /// GetAllOrders returns all orders that match the specified criteria
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="choice">
        ///	choice is 1 for search By OrderNo.
        ///	choice is 2 for search By OrderDate.
        ///	choice is 3 to list all orders.
        ///	</param>
        /// <returns></returns>
        public DataSet GetAllOrders(string orderNo, DateTime from, DateTime to, string CouponCode, int choice)
        {
            if (object.Equals(db, null))
            {
                db = new DataBase();
            }
            if (object.Equals(ds, null))
            {
                ds = new DataSet();
            }
            param = new SqlParameter[5];
            param[0] = db.MakeInParameter("@orderNo", SqlDbType.VarChar, 50, orderNo);
            param[1] = db.MakeInParameter("@From", SqlDbType.DateTime, 8, from);
            param[2] = db.MakeInParameter("@To", SqlDbType.DateTime, 8, to);
            param[3] = db.MakeInParameter("@CouponCode", SqlDbType.VarChar, 200, CouponCode);
            param[4] = db.MakeInParameter("@Choice", SqlDbType.Int, 8, choice);
            db.RunProcedure("P_GetAllOrders", param, out ds);
            ResetAll();
            return ds;
        }

        public DataSet GetAllOrders1(string orderNo, DateTime to, string CouponCode, int choice)
        {
            if (object.Equals(db, null))
            {
                db = new DataBase();
            }
            if (object.Equals(ds, null))
            {
                ds = new DataSet();
            }
            param = new SqlParameter[4];
            param[0] = db.MakeInParameter("@orderNo", SqlDbType.VarChar, 50, orderNo);
            //param[1] = db.MakeInParameter("@From", SqlDbType.DateTime, 8, from);
            param[1] = db.MakeInParameter("@To", SqlDbType.DateTime, 8, to);
            param[2] = db.MakeInParameter("@CouponCode", SqlDbType.VarChar, 200, CouponCode);
            param[3] = db.MakeInParameter("@Choice", SqlDbType.Int, 8, choice);
            db.RunProcedure("P_GetAllOrders", param, out ds);
            ResetAll();
            return ds;
        }

        public DataSet GetAllOrders_Merchant(string orderNo, DateTime from, DateTime to, string CouponCode, int choice)
        {
            if (object.Equals(db, null))
            {
                db = new DataBase();
            }
            if (object.Equals(ds, null))
            {
                ds = new DataSet();
            }
            param = new SqlParameter[5];
            param[0] = db.MakeInParameter("@orderNo", SqlDbType.VarChar, 50, orderNo);
            param[1] = db.MakeInParameter("@From", SqlDbType.DateTime, 8, from);
            param[2] = db.MakeInParameter("@To", SqlDbType.DateTime, 8, to);
            param[3] = db.MakeInParameter("@CouponCode", SqlDbType.VarChar, 200, CouponCode);
            param[4] = db.MakeInParameter("@Choice", SqlDbType.Int, 8, choice);
            db.RunProcedure("P_GetAllOrders_ORG", param, out ds);
            ResetAll();
            return ds;
        }


        public DataSet GetAllOrders_Merchant1(string orderNo, DateTime to, string CouponCode, int choice, string MasterMerchantID)
        {
            if (object.Equals(db, null))
            {
                db = new DataBase();
            }
            if (object.Equals(ds, null))
            {
                ds = new DataSet();
            }
            param = new SqlParameter[5];
            param[0] = db.MakeInParameter("@orderNo", SqlDbType.VarChar, 50, orderNo);
            //param[1] = db.MakeInParameter("@From", SqlDbType.DateTime, 8, from);
            param[1] = db.MakeInParameter("@To", SqlDbType.DateTime, 8, to);
            param[2] = db.MakeInParameter("@CouponCode", SqlDbType.VarChar, 200, CouponCode);
            param[3] = db.MakeInParameter("@Choice", SqlDbType.Int, 8, choice);
            param[4] = db.MakeInParameter("@MasterMerchantID", SqlDbType.VarChar, 50, MasterMerchantID);
            db.RunProcedure("P_GetAllOrders_ORG", param, out ds);
            ResetAll();
            return ds;
        }

        public DataSet GetAllOrdersEndCustomer(string orderNo, DateTime from, DateTime to, string Mastermerchantid, int choice)
        {
            if (object.Equals(db, null))
            {
                db = new DataBase();
            }
            if (object.Equals(ds, null))
            {
                ds = new DataSet();
            }
            param = new SqlParameter[5];
            param[0] = db.MakeInParameter("@orderNo", SqlDbType.VarChar, 50, orderNo);
            param[1] = db.MakeInParameter("@From", SqlDbType.DateTime, 8, from);
            param[2] = db.MakeInParameter("@To", SqlDbType.DateTime, 8, to);
            param[3] = db.MakeInParameter("@Mastermerchantid", SqlDbType.VarChar, 100, Mastermerchantid);
            param[4] = db.MakeInParameter("@Choice", SqlDbType.Int, 8, choice);
            db.RunProcedure("P_GetAllOrders_EndCustomerTest", param, out ds);
            ResetAll();
            return ds;
        }

        public DataSet GetAllOrdersEndCustomer1(string orderNo, DateTime to, string Mastermerchantid, int choice)
        {
            if (object.Equals(db, null))
            {
                db = new DataBase();
            }
            if (object.Equals(ds, null))
            {
                ds = new DataSet();
            }
            param = new SqlParameter[4];
            param[0] = db.MakeInParameter("@orderNo", SqlDbType.VarChar, 50, orderNo);
            //param[1] = db.MakeInParameter("@From", SqlDbType.DateTime, 8, from);
            param[1] = db.MakeInParameter("@To", SqlDbType.DateTime, 8, to);
            param[2] = db.MakeInParameter("@Mastermerchantid", SqlDbType.VarChar, 100, Mastermerchantid);
            param[3] = db.MakeInParameter("@Choice", SqlDbType.Int, 8, choice);
            db.RunProcedure("P_GetAllOrders_EndCustomerTest", param, out ds);
            ResetAll();
            return ds;
        }


        public DataSet GetAllOrdersSubMasterMerchant(string orderNo, DateTime from, DateTime to, string Mastermerchantid, int choice)
        {
            if (object.Equals(db, null))
            {
                db = new DataBase();
            }
            if (object.Equals(ds, null))
            {
                ds = new DataSet();
            }
            param = new SqlParameter[5];
            param[0] = db.MakeInParameter("@orderNo", SqlDbType.VarChar, 50, orderNo);
            param[1] = db.MakeInParameter("@From", SqlDbType.DateTime, 8, from);
            param[2] = db.MakeInParameter("@To", SqlDbType.DateTime, 8, to);
            param[3] = db.MakeInParameter("@Mastermerchantid", SqlDbType.VarChar, 100, Mastermerchantid);
            param[4] = db.MakeInParameter("@Choice", SqlDbType.Int, 8, choice);            
            db.RunProcedure("P_GetAllOrders_SubMasterMerchantDOTCOM", param, out ds);
            ResetAll();
            return ds;
        }
        public DataSet GetAllOrdersSubMasterMerchant1(string orderNo, DateTime to, string Mastermerchantid, int choice)
        {
            if (object.Equals(db, null))
            {
                db = new DataBase();
            }
            if (object.Equals(ds, null))
            {
                ds = new DataSet();
            }
            param = new SqlParameter[4];
            param[0] = db.MakeInParameter("@orderNo", SqlDbType.VarChar, 50, orderNo);
            //param[1] = db.MakeInParameter("@From", SqlDbType.DateTime, 8, from);
            param[1] = db.MakeInParameter("@To", SqlDbType.DateTime, 8, to);
            param[2] = db.MakeInParameter("@Mastermerchantid", SqlDbType.VarChar, 100, Mastermerchantid);
            param[3] = db.MakeInParameter("@Choice", SqlDbType.Int, 8, choice);            
            db.RunProcedure("P_GetAllOrders_SubMasterMerchantDOTCOM", param, out ds);
            ResetAll();
            return ds;
        }


        public DataSet GetOrdersForUPSExcel(string orderNo, string from, string to, string CouponCode, int choice)
        {
            if (object.Equals(db, null))
            {
                db = new DataBase();
            }
            if (object.Equals(ds, null))
            {
                ds = new DataSet();
            }
            param = new SqlParameter[5];
            param[0] = db.MakeInParameter("@orderNo", SqlDbType.VarChar, 50, orderNo);
            param[1] = db.MakeInParameter("@From", SqlDbType.VarChar, 20, from);
            param[2] = db.MakeInParameter("@To", SqlDbType.VarChar, 20, to);
            param[3] = db.MakeInParameter("@CouponCode", SqlDbType.VarChar, 200, CouponCode);
            param[4] = db.MakeInParameter("@Choice", SqlDbType.Int, 8, choice);
            db.RunProcedure("[P_GetOrdersForSweepStakesExcel]", param, out ds);
            ResetAll();
            return ds;
        }

        public DataSet GetOrdersForUPSExcel_Merchant(string orderNo, string from, string to, string CouponCode, int choice)
        {
            if (object.Equals(db, null))
            {
                db = new DataBase();
            }
            if (object.Equals(ds, null))
            {
                ds = new DataSet();
            }
            param = new SqlParameter[5];
            param[0] = db.MakeInParameter("@orderNo", SqlDbType.VarChar, 50, orderNo);
            param[1] = db.MakeInParameter("@From", SqlDbType.VarChar, 20, from);
            param[2] = db.MakeInParameter("@To", SqlDbType.VarChar, 20, to);
            param[3] = db.MakeInParameter("@CouponCode", SqlDbType.VarChar, 200, CouponCode);
            param[4] = db.MakeInParameter("@Choice", SqlDbType.Int, 8, choice);
            db.RunProcedure("[P_GetOrdersForSweepStakesExcel_Merchant]", param, out ds);
            ResetAll();
            return ds;
        }



        public DataSet GetNewOrders(string orderNo, DateTime from, DateTime to, string CouponCode, int choice)
        {
            if (object.Equals(db, null))
            {
                db = new DataBase();
            }
            if (object.Equals(ds, null))
            {
                ds = new DataSet();
            }
            try
            {
                param = new SqlParameter[5];
                param[0] = db.MakeInParameter("@orderNo", SqlDbType.VarChar, 50, orderNo);
                param[1] = db.MakeInParameter("@From", SqlDbType.DateTime, 8, from);
                param[2] = db.MakeInParameter("@To", SqlDbType.DateTime, 8, to);
                param[3] = db.MakeInParameter("@CouponCode", SqlDbType.VarChar, 200, CouponCode);
                param[4] = db.MakeInParameter("@Choice", SqlDbType.Int, 8, choice);
                db.RunProcedure("P_GetNewOrders", param, out ds);
            }
            catch (Exception ex)
            {
                string test = ex.ToString();
                string test1 = ex.Message;
            }
            finally
            {
                ResetAll();
            }
            
            return ds;
        }


        public DataSet GetNewOrders1(string orderNo, DateTime to, string CouponCode, int choice)
        {
            if (object.Equals(db, null))
            {
                db = new DataBase();
            }
            if (object.Equals(ds, null))
            {
                ds = new DataSet();
            }
            try
            {
                param = new SqlParameter[4];
                param[0] = db.MakeInParameter("@orderNo", SqlDbType.VarChar, 50, orderNo);
                //param[1] = db.MakeInParameter("@From", SqlDbType.DateTime, 8, from);
                param[1] = db.MakeInParameter("@To", SqlDbType.DateTime, 8, to);
                param[2] = db.MakeInParameter("@CouponCode", SqlDbType.VarChar, 200, CouponCode);
                param[3] = db.MakeInParameter("@Choice", SqlDbType.Int, 8, choice);
                db.RunProcedure("P_GetNewOrders", param, out ds);
            }
            catch (Exception ex)
            {
                string test = ex.ToString();
                string test1 = ex.Message;
            }
            finally
            {
                ResetAll();
            }

            return ds;
        }

        public DataSet GetNewOrders_Merchant(string orderNo, DateTime from, DateTime to, string CouponCode, int choice)
        {
            if (object.Equals(db, null))
            {
                db = new DataBase();
            }
            if (object.Equals(ds, null))
            {
                ds = new DataSet();
            }
            try
            {
                param = new SqlParameter[5];
                param[0] = db.MakeInParameter("@orderNo", SqlDbType.VarChar, 50, orderNo);
                param[1] = db.MakeInParameter("@From", SqlDbType.DateTime, 8, from);
                param[2] = db.MakeInParameter("@To", SqlDbType.DateTime, 8, to);
                param[3] = db.MakeInParameter("@CouponCode", SqlDbType.VarChar, 200, CouponCode);
                param[4] = db.MakeInParameter("@Choice", SqlDbType.Int, 8, choice);
                db.RunProcedure("P_GetNewOrders_ORG", param, out ds);
            }
            catch (Exception ex)
            {
                string test = ex.ToString();
                string test1 = ex.Message;
            }
            finally
            {
                ResetAll();
            }

            return ds;
        }


        public DataSet GetNewOrders_Merchant1(string orderNo, DateTime to, string CouponCode, int choice, string MasterMerchantID)
        {
            if (object.Equals(db, null))
            {
                db = new DataBase();
            }
            if (object.Equals(ds, null))
            {
                ds = new DataSet();
            }
            try
            {
                param = new SqlParameter[5];
                param[0] = db.MakeInParameter("@orderNo", SqlDbType.VarChar, 50, orderNo);
                //param[1] = db.MakeInParameter("@From", SqlDbType.DateTime, 8, from);
                param[1] = db.MakeInParameter("@To", SqlDbType.DateTime, 8, to);
                param[2] = db.MakeInParameter("@CouponCode", SqlDbType.VarChar, 200, CouponCode);
                param[3] = db.MakeInParameter("@Choice", SqlDbType.Int, 8, choice);
                param[4] = db.MakeInParameter("@MasterMerchantID", SqlDbType.VarChar, 50, MasterMerchantID);
                db.RunProcedure("P_GetNewOrders_ORG", param, out ds);
            }
            catch (Exception ex)
            {
                string test = ex.ToString();
                string test1 = ex.Message;
            }
            finally
            {
                ResetAll();
            }

            return ds;
        }

        public DataSet GetNewOrdersEndCustomer(string orderNo, DateTime from, DateTime to, string Mastermerchantid, int choice)
        {
            if (object.Equals(db, null))
            {
                db = new DataBase();
            }
            if (object.Equals(ds, null))
            {
                ds = new DataSet();
            }
            try
            {
                param = new SqlParameter[5];
                param[0] = db.MakeInParameter("@orderNo", SqlDbType.VarChar, 50, orderNo);
                param[1] = db.MakeInParameter("@From", SqlDbType.DateTime, 8, from);
                param[2] = db.MakeInParameter("@To", SqlDbType.DateTime, 8, to);
                param[3] = db.MakeInParameter("@Mastermerchantid", SqlDbType.VarChar, 200, Mastermerchantid);
                param[4] = db.MakeInParameter("@Choice", SqlDbType.Int, 8, choice);
                db.RunProcedure("P_GetNewOrders_EndCustomerTest", param, out ds);
            }
            catch (Exception ex)
            {
                string test = ex.ToString();
                string test1 = ex.Message;
            }
            finally
            {
                ResetAll();
            }

            return ds;
        }
        public DataSet GetNewOrdersEndCustomer1(string orderNo, DateTime to, string Mastermerchantid, int choice)
        {
            if (object.Equals(db, null))
            {
                db = new DataBase();
            }
            if (object.Equals(ds, null))
            {
                ds = new DataSet();
            }
            try
            {
                param = new SqlParameter[4];
                param[0] = db.MakeInParameter("@orderNo", SqlDbType.VarChar, 50, orderNo);
                //param[1] = db.MakeInParameter("@From", SqlDbType.DateTime, 8, from);
                param[1] = db.MakeInParameter("@To", SqlDbType.DateTime, 8, to);
                param[2] = db.MakeInParameter("@Mastermerchantid", SqlDbType.VarChar, 100, Mastermerchantid);
                param[3] = db.MakeInParameter("@Choice", SqlDbType.Int, 8, choice);
                db.RunProcedure("P_GetNewOrders_EndCustomerTest", param, out ds);
            }
            catch (Exception ex)
            {
                string test = ex.ToString();
                string test1 = ex.Message;
            }
            finally
            {
                ResetAll();
            }

            return ds;
        }

        public DataSet GetNewOrdersSubMasterMerchant(string orderNo, DateTime from, DateTime to, string Mastermerchantid, int choice)
        {
            if (object.Equals(db, null))
            {
                db = new DataBase();
            }
            if (object.Equals(ds, null))
            {
                ds = new DataSet();
            }
            try
            {
                param = new SqlParameter[5];
                param[0] = db.MakeInParameter("@orderNo", SqlDbType.VarChar, 50, orderNo);
                param[1] = db.MakeInParameter("@From", SqlDbType.DateTime, 8, from);
                param[2] = db.MakeInParameter("@To", SqlDbType.DateTime, 8, to);
                param[3] = db.MakeInParameter("@Mastermerchantid", SqlDbType.VarChar, 100, Mastermerchantid);
                param[4] = db.MakeInParameter("@Choice", SqlDbType.Int, 8, choice);
                
                db.RunProcedure("[P_GetNewOrders_SubMasterMerchantDOTCOM]", param, out ds);
            }
            catch (Exception ex)
            {
                string test = ex.ToString();
                string test1 = ex.Message;
            }
            finally
            {
                ResetAll();
            }

            return ds;
        }
        public DataSet GetNewOrdersSubMasterMerchant1(string orderNo, DateTime to, string Mastermerchantid, int choice)
        {
            if (object.Equals(db, null))
            {
                db = new DataBase();
            }
            if (object.Equals(ds, null))
            {
                ds = new DataSet();
            }
            try
            {
                param = new SqlParameter[4];
                param[0] = db.MakeInParameter("@orderNo", SqlDbType.VarChar, 50, orderNo);
                //param[1] = db.MakeInParameter("@From", SqlDbType.DateTime, 8, from);
                param[1] = db.MakeInParameter("@To", SqlDbType.DateTime, 8, to);
                param[2] = db.MakeInParameter("@Mastermerchantid", SqlDbType.VarChar, 100, Mastermerchantid);
                param[3] = db.MakeInParameter("@Choice", SqlDbType.Int, 8, choice);
                
                db.RunProcedure("[P_GetNewOrders_SubMasterMerchantDOTCOM]", param, out ds);
            }
            catch (Exception ex)
            {
                string test = ex.ToString();
                string test1 = ex.Message;
            }
            finally
            {
                ResetAll();
            }

            return ds;
        }

        public DataSet GetShippedOrders(string orderNo, DateTime from, DateTime to, string CouponCode, int choice)
        {
            if (object.Equals(db, null))
            {
                db = new DataBase();
            }
            if (object.Equals(ds, null))
            {
                ds = new DataSet();
            }
            try
            {
                param = new SqlParameter[5];
                param[0] = db.MakeInParameter("@orderNo", SqlDbType.VarChar, 50, orderNo);
                param[1] = db.MakeInParameter("@From", SqlDbType.DateTime, 8, from);
                param[2] = db.MakeInParameter("@To", SqlDbType.DateTime, 8, to);
                param[3] = db.MakeInParameter("@CouponCode", SqlDbType.VarChar, 200, CouponCode);
                param[4] = db.MakeInParameter("@Choice", SqlDbType.Int, 8, choice);
                
                db.RunProcedure("P_GetOrdersToShip", param, out ds);
            }
            catch (Exception ex)
            {
                string test = ex.ToString();
                string test1 = ex.Message;
            }
            finally
            {
                ResetAll();
            }

            return ds;
        }

        public DataSet GetShippedOrders1(string orderNo, DateTime to, string CouponCode, int choice)
        {
            if (object.Equals(db, null))
            {
                db = new DataBase();
            }
            if (object.Equals(ds, null))
            {
                ds = new DataSet();
            }
            try
            {
                param = new SqlParameter[4];
                param[0] = db.MakeInParameter("@orderNo", SqlDbType.VarChar, 50, orderNo);
                //param[1] = db.MakeInParameter("@From", SqlDbType.DateTime, 8, from);
                param[1] = db.MakeInParameter("@To", SqlDbType.DateTime, 8, to);
                param[2] = db.MakeInParameter("@CouponCode", SqlDbType.VarChar, 200, CouponCode);
                param[3] = db.MakeInParameter("@Choice", SqlDbType.Int, 8, choice);

                db.RunProcedure("P_GetOrdersToShip", param, out ds);
            }
            catch (Exception ex)
            {
                string test = ex.ToString();
                string test1 = ex.Message;
            }
            finally
            {
                ResetAll();
            }

            return ds;
        }

        public DataSet GetShippedOrders_Merchant(string orderNo, DateTime from, DateTime to, string CouponCode, int choice)
        {
            if (object.Equals(db, null))
            {
                db = new DataBase();
            }
            if (object.Equals(ds, null))
            {
                ds = new DataSet();
            }
            try
            {
                param = new SqlParameter[5];
                param[0] = db.MakeInParameter("@orderNo", SqlDbType.VarChar, 50, orderNo);
                param[1] = db.MakeInParameter("@From", SqlDbType.DateTime, 8, from);
                param[2] = db.MakeInParameter("@To", SqlDbType.DateTime, 8, to);
                param[3] = db.MakeInParameter("@CouponCode", SqlDbType.VarChar, 200, CouponCode);
                param[4] = db.MakeInParameter("@Choice", SqlDbType.Int, 8, choice);

                db.RunProcedure("P_GetOrdersToShip_ORG", param, out ds);
            }
            catch (Exception ex)
            {
                string test = ex.ToString();
                string test1 = ex.Message;
            }
            finally
            {
                ResetAll();
            }

            return ds;
        }

        public DataSet GetShippedOrders_Merchant1(string orderNo, DateTime to, string CouponCode, int choice, string MasterMerchantID)
        {
            if (object.Equals(db, null))
            {
                db = new DataBase();
            }
            if (object.Equals(ds, null))
            {
                ds = new DataSet();
            }
            try
            {
                param = new SqlParameter[5];
                param[0] = db.MakeInParameter("@orderNo", SqlDbType.VarChar, 50, orderNo);
                //param[1] = db.MakeInParameter("@From", SqlDbType.DateTime, 8, from);
                param[1] = db.MakeInParameter("@To", SqlDbType.DateTime, 8, to);
                param[2] = db.MakeInParameter("@CouponCode", SqlDbType.VarChar, 200, CouponCode);
                param[3] = db.MakeInParameter("@Choice", SqlDbType.Int, 8, choice);
                param[4] = db.MakeInParameter("@MasterMerchantID", SqlDbType.VarChar, 50, MasterMerchantID);

                db.RunProcedure("P_GetOrdersToShip_ORG", param, out ds);
            }
            catch (Exception ex)
            {
                string test = ex.ToString();
                string test1 = ex.Message;
            }
            finally
            {
                ResetAll();
            }

            return ds;
        }

        public DataSet GetShippedOrdersEndCustomer(string orderNo, DateTime from, DateTime to, string Mastermerchantid, int choice)
        {
            if (object.Equals(db, null))
            {
                db = new DataBase();
            }
            if (object.Equals(ds, null))
            {
                ds = new DataSet();
            }
            try
            {
                param = new SqlParameter[5];
                param[0] = db.MakeInParameter("@orderNo", SqlDbType.VarChar, 50, orderNo);
                param[1] = db.MakeInParameter("@From", SqlDbType.DateTime, 8, from);
                param[2] = db.MakeInParameter("@To", SqlDbType.DateTime, 8, to);
                param[3] = db.MakeInParameter("@Mastermerchantid", SqlDbType.VarChar, 100, Mastermerchantid);
                param[4] = db.MakeInParameter("@Choice", SqlDbType.Int, 8, choice);

                db.RunProcedure("P_GetOrdersToShip_EndCustomerTest", param, out ds);
            }
            catch (Exception ex)
            {
                string test = ex.ToString();
                string test1 = ex.Message;
            }
            finally
            {
                ResetAll();
            }

            return ds;
        }
        public DataSet GetShippedOrdersEndCustomer1(string orderNo, DateTime to, string Mastermerchantid, int choice)
        {
            if (object.Equals(db, null))
            {
                db = new DataBase();
            }
            if (object.Equals(ds, null))
            {
                ds = new DataSet();
            }
            try
            {
                param = new SqlParameter[4];
                param[0] = db.MakeInParameter("@orderNo", SqlDbType.VarChar, 50, orderNo);
                //param[1] = db.MakeInParameter("@From", SqlDbType.DateTime, 8, from);
                param[1] = db.MakeInParameter("@To", SqlDbType.DateTime, 8, to);
                param[2] = db.MakeInParameter("@Mastermerchantid", SqlDbType.VarChar, 100, Mastermerchantid);
                param[3] = db.MakeInParameter("@Choice", SqlDbType.Int, 8, choice);

                db.RunProcedure("P_GetOrdersToShip_EndCustomerTest", param, out ds);
            }
            catch (Exception ex)
            {
                string test = ex.ToString();
                string test1 = ex.Message;
            }
            finally
            {
                ResetAll();
            }

            return ds;
        }

        public DataSet GetShippedOrdersSubMasterMerchant(string orderNo, DateTime from, DateTime to, string Mastermerchantid, int choice)
        {
            if (object.Equals(db, null))
            {
                db = new DataBase();
            }
            if (object.Equals(ds, null))
            {
                ds = new DataSet();
            }
            try
            {
                param = new SqlParameter[5];
                param[0] = db.MakeInParameter("@orderNo", SqlDbType.VarChar, 50, orderNo);
                param[1] = db.MakeInParameter("@From", SqlDbType.DateTime, 8, from);
                param[2] = db.MakeInParameter("@To", SqlDbType.DateTime, 8, to);
                param[3] = db.MakeInParameter("@Mastermerchantid", SqlDbType.VarChar, 100, Mastermerchantid);
                param[4] = db.MakeInParameter("@Choice", SqlDbType.Int, 8, choice);                

                db.RunProcedure("P_GetOrdersToShip_SubMasterMerchantDOTCOM", param, out ds);
            }
            catch (Exception ex)
            {
                string test = ex.ToString();
                string test1 = ex.Message;
            }
            finally
            {
                ResetAll();
            }

            return ds;
        }
        public DataSet GetShippedOrdersSubMasterMerchant1(string orderNo, DateTime to, string Mastermerchantid, int choice)
        {
            if (object.Equals(db, null))
            {
                db = new DataBase();
            }
            if (object.Equals(ds, null))
            {
                ds = new DataSet();
            }
            try
            {
                param = new SqlParameter[4];
                param[0] = db.MakeInParameter("@orderNo", SqlDbType.VarChar, 50, orderNo);
                //param[1] = db.MakeInParameter("@From", SqlDbType.DateTime, 8, from);
                param[1] = db.MakeInParameter("@To", SqlDbType.DateTime, 8, to);
                param[2] = db.MakeInParameter("@Mastermerchantid", SqlDbType.VarChar, 100, Mastermerchantid);
                param[3] = db.MakeInParameter("@Choice", SqlDbType.Int, 8, choice);
                

                db.RunProcedure("P_GetOrdersToShip_SubMasterMerchantDOTCOM", param, out ds);
            }
            catch (Exception ex)
            {
                string test = ex.ToString();
                string test1 = ex.Message;
            }
            finally
            {
                ResetAll();
            }

            return ds;
        }


        public DataSet GetOrderByOrderID(int orderID)
        {
            if (object.Equals(db, null))
            {
                db = new DataBase();
            }
            if (object.Equals(ds, null))
            {
                ds = new DataSet();
            }
            param = new SqlParameter[1];
            param[0] = db.MakeInParameter("@orderID", SqlDbType.Int, 4, orderID);
            db.RunProcedure("P_GetOrderByOrderID", param, out ds);
            ResetAll();
            return ds;
        }

        public DataSet GetDeclinedOrders(string orderNo)
        {
            if (object.Equals(db, null))
            {
                db = new DataBase();
            }
            if (object.Equals(ds, null))
            {
                ds = new DataSet();
            }
            param = new SqlParameter[1];
            param[0] = db.MakeInParameter("@orderNo", SqlDbType.VarChar, 50, orderNo);
            db.RunProcedure("P_GetDeclinedOrders", param, out ds);
            ResetAll();
            return ds;
        }
        public DataSet GetOrderToProcess(string orderNo)
        {
            if (object.Equals(db, null))
            {
                db = new DataBase();
            }
            if (object.Equals(ds, null))
            {
                ds = new DataSet();
            }
            param = new SqlParameter[1];
            param[0] = db.MakeInParameter("@orderNo", SqlDbType.VarChar, 50, orderNo);
            db.RunProcedure("P_GetOrderToProcess", param, out ds);
            ResetAll();
            return ds;
        }
        public DataSet GetCancelledOrders(string orderNo, DateTime from, DateTime to, string CouponCode, int choice)
        {
            if (object.Equals(db, null))
            {
                db = new DataBase();
            }
            if (object.Equals(ds, null))
            {
                ds = new DataSet();
            }
            param = new SqlParameter[5];
            param[0] = db.MakeInParameter("@orderNo", SqlDbType.VarChar, 50, orderNo);
            param[1] = db.MakeInParameter("@From", SqlDbType.DateTime, 8, from);
            param[2] = db.MakeInParameter("@To", SqlDbType.DateTime, 8, to);
            param[3] = db.MakeInParameter("@CouponCode", SqlDbType.VarChar, 200, CouponCode);
            param[4] = db.MakeInParameter("@Choice", SqlDbType.Int, 8, choice);
            db.RunProcedure("P_GetCancelledOrders", param, out ds);
            ResetAll();
            return ds;
        }

        public DataSet GetCancelledOrders1(string orderNo, DateTime to, string CouponCode, int choice)
        {
            if (object.Equals(db, null))
            {
                db = new DataBase();
            }
            if (object.Equals(ds, null))
            {
                ds = new DataSet();
            }
            param = new SqlParameter[4];
            param[0] = db.MakeInParameter("@orderNo", SqlDbType.VarChar, 50, orderNo);
            //param[1] = db.MakeInParameter("@From", SqlDbType.DateTime, 8, from);
            param[1] = db.MakeInParameter("@To", SqlDbType.DateTime, 8, to);
            param[2] = db.MakeInParameter("@CouponCode", SqlDbType.VarChar, 200, CouponCode);
            param[3] = db.MakeInParameter("@Choice", SqlDbType.Int, 8, choice);
            db.RunProcedure("P_GetCancelledOrders", param, out ds);
            ResetAll();
            return ds;
        }
        public DataSet GetCancelledOrders_Merchant(string orderNo, DateTime from, DateTime to, string CouponCode, int choice)
        {
            if (object.Equals(db, null))
            {
                db = new DataBase();
            }
            if (object.Equals(ds, null))
            {
                ds = new DataSet();
            }
            param = new SqlParameter[5];
            param[0] = db.MakeInParameter("@orderNo", SqlDbType.VarChar, 50, orderNo);
            param[1] = db.MakeInParameter("@From", SqlDbType.DateTime, 8, from);
            param[2] = db.MakeInParameter("@To", SqlDbType.DateTime, 8, to);
            param[3] = db.MakeInParameter("@CouponCode", SqlDbType.VarChar, 200, CouponCode);
            param[4] = db.MakeInParameter("@Choice", SqlDbType.Int, 8, choice);
            db.RunProcedure("P_GetCancelledOrders_ORG", param, out ds);
            ResetAll();
            return ds;
        }

        public DataSet GetCancelledOrders_Merchant1(string orderNo, DateTime to, string CouponCode, int choice, string MasterMerchantID)
        {
            if (object.Equals(db, null))
            {
                db = new DataBase();
            }
            if (object.Equals(ds, null))
            {
                ds = new DataSet();
            }
            param = new SqlParameter[5];
            param[0] = db.MakeInParameter("@orderNo", SqlDbType.VarChar, 50, orderNo);
            //param[1] = db.MakeInParameter("@From", SqlDbType.DateTime, 8, from);
            param[1] = db.MakeInParameter("@To", SqlDbType.DateTime, 8, to);
            param[2] = db.MakeInParameter("@CouponCode", SqlDbType.VarChar, 200, CouponCode);
            param[3] = db.MakeInParameter("@Choice", SqlDbType.Int, 8, choice);
            param[4] = db.MakeInParameter("@MasterMerchantID", SqlDbType.VarChar, 50, MasterMerchantID);
            db.RunProcedure("P_GetCancelledOrders_ORG", param, out ds);
            ResetAll();
            return ds;
        }

        public DataSet GetCancelledOrdersEndCustomer(string orderNo, DateTime from, DateTime to, string Mastermerchantid, int choice)
        {
            if (object.Equals(db, null))
            {
                db = new DataBase();
            }
            if (object.Equals(ds, null))
            {
                ds = new DataSet();
            }
            param = new SqlParameter[5];
            param[0] = db.MakeInParameter("@orderNo", SqlDbType.VarChar, 50, orderNo);
            param[1] = db.MakeInParameter("@From", SqlDbType.DateTime, 8, from);
            param[2] = db.MakeInParameter("@To", SqlDbType.DateTime, 8, to);
            param[3] = db.MakeInParameter("@Mastermerchantid", SqlDbType.VarChar, 100, Mastermerchantid);
            param[4] = db.MakeInParameter("@Choice", SqlDbType.Int, 8, choice);
            db.RunProcedure("[P_GetCancelledOrders_EndCustomerTest]", param, out ds);
            ResetAll();
            return ds;
        }
        public DataSet GetCancelledOrdersEndCustomer1(string orderNo, DateTime to, string Mastermerchantid, int choice)
        {
            if (object.Equals(db, null))
            {
                db = new DataBase();
            }
            if (object.Equals(ds, null))
            {
                ds = new DataSet();
            }
            param = new SqlParameter[4];
            param[0] = db.MakeInParameter("@orderNo", SqlDbType.VarChar, 50, orderNo);
            // param[1] = db.MakeInParameter("@From", SqlDbType.DateTime, 8, from);
            param[1] = db.MakeInParameter("@To", SqlDbType.DateTime, 8, to);
            param[2] = db.MakeInParameter("@Mastermerchantid", SqlDbType.VarChar, 100, Mastermerchantid);
            param[3] = db.MakeInParameter("@Choice", SqlDbType.Int, 8, choice);
            db.RunProcedure("[P_GetCancelledOrders_EndCustomerTest]", param, out ds);
            ResetAll();
            return ds;
        }

        public DataSet GetCancelledOrdersSubMasterMerchant(string orderNo, DateTime from, DateTime to, string Mastermerchantid, int choice)
        {
            if (object.Equals(db, null))
            {
                db = new DataBase();
            }
            if (object.Equals(ds, null))
            {
                ds = new DataSet();
            }
            param = new SqlParameter[5];
            param[0] = db.MakeInParameter("@orderNo", SqlDbType.VarChar, 50, orderNo);
            param[1] = db.MakeInParameter("@From", SqlDbType.DateTime, 8, from);
            param[2] = db.MakeInParameter("@To", SqlDbType.DateTime, 8, to);
            param[3] = db.MakeInParameter("@Mastermerchantid", SqlDbType.VarChar, 100, Mastermerchantid);
            param[4] = db.MakeInParameter("@Choice", SqlDbType.Int, 8, choice);

            db.RunProcedure("[P_GetCancelledOrders_SubMasterMerchantDOTCOM]", param, out ds);
            ResetAll();
            return ds;
        }
        public DataSet GetCancelledOrdersSubMasterMerchant1(string orderNo, DateTime to, string Mastermerchantid, int choice)
        {
            if (object.Equals(db, null))
            {
                db = new DataBase();
            }
            if (object.Equals(ds, null))
            {
                ds = new DataSet();
            }
            param = new SqlParameter[4];
            param[0] = db.MakeInParameter("@orderNo", SqlDbType.VarChar, 50, orderNo);
            // param[1] = db.MakeInParameter("@From", SqlDbType.DateTime, 8, from);
            param[1] = db.MakeInParameter("@To", SqlDbType.DateTime, 8, to);
            param[2] = db.MakeInParameter("@Mastermerchantid", SqlDbType.VarChar, 100, Mastermerchantid);
            param[3] = db.MakeInParameter("@Choice", SqlDbType.Int, 8, choice);
            
            db.RunProcedure("[P_GetCancelledOrders_SubMasterMerchantDOTCOM]", param, out ds);
            ResetAll();
            return ds;
        }

        public DataSet GetShippedOrders(string orderNo)
        {
            if (object.Equals(db, null))
            {
                db = new DataBase();
            }
            if (object.Equals(ds, null))
            {
                ds = new DataSet();
            }
            param = new SqlParameter[1];
            param[0] = db.MakeInParameter("@orderNo", SqlDbType.VarChar, 50, orderNo);
            db.RunProcedure("P_GetShippedOrders", param, out ds);
            ResetAll();
            return ds;
        }

        public DataSet GetOrdersToShip(int filter)
        {
            if (object.Equals(db, null))
            {
                db = new DataBase();
            }
            if (object.Equals(ds, null))
            {
                ds = new DataSet();
            }
            param = new SqlParameter[1];
            param[0] = db.MakeInParameter("@filter", SqlDbType.Int, 4, filter);
            db.RunProcedure("P_GetOrdersToShip", param, out ds);
            ResetAll();
            return ds;
        }
        public DataSet GetJustShipedOrders(string orderID)
        {
            if (object.Equals(db, null))
            {
                db = new DataBase();
            }
            if (object.Equals(ds, null))
            {
                ds = new DataSet();
            }
            param = new SqlParameter[1];
            param[0] = db.MakeInParameter("@orderID", SqlDbType.VarChar, 100, orderID);
            db.RunProcedure("P_GetJustShipedOrders", param, out ds);
            ResetAll();
            return ds;
        }
        public void DeleteOrder(int orderID)
        {
            if (object.Equals(db, null))
            {
                db = new DataBase();
            }
            param = new SqlParameter[1];
            param[0] = db.MakeInParameter("@orderID", SqlDbType.Int, 4, orderID);
            db.RunProcedure("P_DeleteOrder", param);
            ResetAll();
        }

        public DataSet SearchCategoryreport()
        {
            if (object.Equals(db, null))
            {
                db = new DataBase();
            }
            if (object.Equals(ds, null))
            {
                ds = new DataSet();
            }
            param = new SqlParameter[0];
            db.RunProcedure("P_Categoriesreport", param, out ds);
            ResetAll();
            return ds;
        }

        public DataSet SearchCustomerreport(string searchon, string sortdirection)
        {
            if (object.Equals(db, null))
            {
                db = new DataBase();
            }
            if (object.Equals(ds, null))
            {
                ds = new DataSet();
            }
            param = new SqlParameter[2];
            param[0] = db.MakeInParameter("@searchOn", SqlDbType.VarChar, 50, searchon);
            param[1] = db.MakeInParameter("@sortDirection", SqlDbType.VarChar, 50, sortdirection);
            db.RunProcedure("P_Customersreport", param, out ds);

            ResetAll();
            return ds;
        }
        public string GetCheckOutBtngo2(string vcode,Decimal SubTotal)
        {
            if (object.Equals(db, null))
            {
                db = new DataBase();
            }
            if (object.Equals(ds, null))
            {
                ds = new DataSet();
            }
            string amount = string.Empty;
           
            try
            {
                param = new SqlParameter[2];
                param[0] = db.MakeInParameter("@vcode", SqlDbType.VarChar, 50, vcode);
                param[1] = db.MakeInParameter("@SubTotal", SqlDbType.Decimal, 8, SubTotal);
                db.RunProcedure("sp_getdiscount", param, out ds);
                ResetAll();
                amount = ds.Tables[0].Rows[0][1].ToString();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
               
            }
            return amount;
        }

        public void Insert_SilverPendant(int OrderId, int IsSilverPendant)
        {

            try
            {
                if (object.Equals(db, null))
                {
                    db = new DataBase();
                }
                param = new SqlParameter[2];
                param[0] = db.MakeInParameter("@orderID", SqlDbType.Int, 4, OrderId);
                param[1] = db.MakeInParameter("@IsSilverPendant", SqlDbType.Int, 4, IsSilverPendant);


                db.RunProcedure("sp_IsSilverPendant", param);

            }
            catch (Exception ex)
            {
                string test = ex.ToString();
                string test1 = ex.Message;

            }
            finally
            {
                ResetAll();

            }

        }
        public int Check_SilverPendant(int OrderId)
        {


            if (object.Equals(db, null))
            {
                db = new DataBase();
            }
            if (object.Equals(ds, null))
            {
                ds = new DataSet();
            }
            param = new SqlParameter[1];
            param[0] = db.MakeInParameter("@orderID", SqlDbType.Int, 4, OrderId);



            db.RunProcedure("sp_CheckSilverPendant", param, out ds);

            ResetAll();




            if (ds.Tables[0].Rows.Count > 0)
                return 1;
            else
                return 0;



        }
        public DataSet GetmailData(int orderid,string couponcode,int silver)
        {
            if (object.Equals(db, null))
            {
                db = new DataBase();
            }
            if (object.Equals(ds, null))
            {
                ds = new DataSet();
            }
            param = new SqlParameter[3];
            param[0] = db.MakeInParameter("@orderid", SqlDbType.Int, 4, orderid);
            param[1] = db.MakeInParameter("@VoucherCode", SqlDbType.VarChar, 50, couponcode);
            param[2] = db.MakeInParameter("@silver", SqlDbType.Int, 4, silver);



            db.RunProcedure("sp_getmaildata", param, out ds);

            ResetAll();


            return ds;

          
        }
        #region Private Methods
        private void ResetAll()
        {
            param = null;
            db = null;
        }
        #endregion
    }
}
