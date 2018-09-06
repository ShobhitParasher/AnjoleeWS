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
namespace tblStore_Email
{
    /// <summary>
    /// Summary description for tblStore_EmailHelper
    /// </summary>
    public class tblStore_EmailHelper
    {
        public tblStore_EmailHelper()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        private DataBase db;
        SqlParameter[] param;
        DataSet ds;
        public int InsertStoreEmail(tblStore_Email otblStore_Email)
        {
            int status = -1;
            try
            {
                if (object.Equals(db, null))
                {
                    db = new DataBase();
                }
                param = new SqlParameter[14];
                param[0] = db.MakeInParameter("@emailSystemServer", SqlDbType.VarChar, 256, otblStore_Email.emailSystemServer);
                param[1] = db.MakeInParameter("@ccStaff", SqlDbType.Bit, 1, otblStore_Email.ccStaff);
                param[2] = db.MakeInParameter("@staffEmail1", SqlDbType.VarChar, 120, otblStore_Email.staffEmail1);
                param[3] = db.MakeInParameter("@staffEmail2", SqlDbType.VarChar, 120, otblStore_Email.staffEmail2);
                param[4] = db.MakeInParameter("@staffEmail3", SqlDbType.VarChar, 120, otblStore_Email.staffEmail3);
                param[5] = db.MakeInParameter("@emailFromAddress", SqlDbType.VarChar, 120, otblStore_Email.emailFromAddress);
                param[6] = db.MakeInParameter("@confirmSubject", SqlDbType.VarChar, 330, otblStore_Email.confirmSubject);
                param[7] = db.MakeInParameter("@confirmEmail", SqlDbType.NText, -1, otblStore_Email.confirmEmail);
                param[8] = db.MakeInParameter("@confirmEmailPartial", SqlDbType.NText, -1, otblStore_Email.confirmEmailPartial);
                param[9] = db.MakeInParameter("@requestConfirmTrackingNo", SqlDbType.Bit, 1, otblStore_Email.requestConfirmTrackingNo);
                param[10] = db.MakeInParameter("@emailCustomerReceipt", SqlDbType.Bit, 1, otblStore_Email.emailCustomerReceipt);
                param[11] = db.MakeInParameter("@receiptSubject", SqlDbType.VarChar, 330, otblStore_Email.receiptSubject);
                param[12] = db.MakeInParameter("@receiptEmail", SqlDbType.NText, -1, otblStore_Email.receiptEmail);
                param[13] = db.MakeOutParameter("@Status", SqlDbType.Int, 4);
                db.RunProcedure("P_InsertStoreEmailInfo_Anjolee", param);
                status = (int)param[13].Value;
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
            return status;
        }

        
        public DataSet GetStoreEmail()
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
            db.RunProcedure("P_GetStoreEmail_Anjolee", null, out ds);
            ResetAll();
            return ds;
        }

        
        private void ResetAll()
        {
            param = null;
            db = null;
        }
    }
}
