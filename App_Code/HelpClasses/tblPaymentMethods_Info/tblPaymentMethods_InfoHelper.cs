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
namespace tblPaymentMethods_Info
{
    /// <summary>
    /// Summary description for tblPaymentMethods_InfoHelper
    /// </summary>
    public class tblPaymentMethods_InfoHelper
    {
        private DataBase db;
        SqlParameter[] param;
        DataSet ds;
        public tblPaymentMethods_InfoHelper()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public DataSet GetPaymentMethodInfo(int paymentMethodId)
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
            param[0] = db.MakeInParameter("@payId", SqlDbType.Int, 4, paymentMethodId);
            db.RunProcedure("P_GetPaymentMethodInfo", param, out ds);
            ResetAll();
            return ds;
        }

        public int insertpaymentMethod(tblPaymentMethods_Info otblPaymentMethod_Info, int payId, string mode)
        {
            int status = -1;
            try
            {
                if (object.Equals(db, null))
                {
                    db = new DataBase();
                }
                param = new SqlParameter[11];
                param[0] = db.MakeInParameter("@userID", SqlDbType.VarChar, 50, otblPaymentMethod_Info.userName);
                param[1] = db.MakeInParameter("@userPassword", SqlDbType.VarChar, 50, otblPaymentMethod_Info.userPassword);
                param[2] = db.MakeInParameter("@userKey", SqlDbType.VarChar, 50, otblPaymentMethod_Info.userKey);
                param[3] = db.MakeInParameter("@emailId", SqlDbType.VarChar, 50, otblPaymentMethod_Info.emailID);
                param[4] = db.MakeInParameter("@vendorName", SqlDbType.VarChar, 50, otblPaymentMethod_Info.vendorName);
                param[5] = db.MakeInParameter("@partnerName", SqlDbType.VarChar, 50, otblPaymentMethod_Info.partnerName);
                param[6] = db.MakeInParameter("@comment", SqlDbType.VarChar, 100, otblPaymentMethod_Info.comment);
                param[7] = db.MakeOutParameter("@Status", SqlDbType.Int, 4);
                param[8] = db.MakeInParameter("@pid", SqlDbType.Int, 4, payId);
                param[9] = db.MakeInParameter("@LiveMode", SqlDbType.Int, 4, otblPaymentMethod_Info.LiveMode);
                param[10] = db.MakeInParameter("@mode", SqlDbType.VarChar, 10, mode);
                db.RunProcedure("P_InsertPaymentMethod_Info", param);
                status = (int)param[7].Value;
            }
            catch (Exception ex)
            {
                string test = ex.ToString();
                string test1 = ex.Message;
            }
            return status;
        }
        private void ResetAll()
        {
            param = null;
            db = null;
        }
    }
}