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
namespace tblStore_Checkout
{
    /// <summary>
    /// Summary description for tblStore_CheckoutHelper
    /// </summary>
    public class tblStore_CheckoutHelper
    {
        public tblStore_CheckoutHelper()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        private DataBase db;
        SqlParameter[] param;
        DataSet ds;
        public DataSet GetStoreCheckout()
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
            db.RunProcedure("P_GetStoreCheckout", null, out ds);
            ResetAll();
            return ds;
        }
        public int InsertStoresetup(tblStore_Checkout otblStore_Checkout, string Mode)
        {
            int status = -1;
            try
            {
                if (object.Equals(db, null))
                {
                    db = new DataBase();
                }
                param = new SqlParameter[9];
                param[0] = db.MakeInParameter("@orderPrefix", SqlDbType.NVarChar, 20, otblStore_Checkout.Prefix);
                param[1] = db.MakeInParameter("@enableSSL", SqlDbType.Bit, 1, otblStore_Checkout.chkSSLCheckout);
                param[2] = db.MakeInParameter("@enableAdminSSL", SqlDbType.Bit, 1, otblStore_Checkout.chkSSLAdmin);
                param[3] = db.MakeInParameter("@websiteURL", SqlDbType.NVarChar, 510, otblStore_Checkout.WebsiteURL);
                param[4] = db.MakeInParameter("@adminURL", SqlDbType.NVarChar, 510, otblStore_Checkout.AdminURL);
                param[5] = db.MakeInParameter("@sslDomain", SqlDbType.NVarChar, 510, otblStore_Checkout.CheckputSSLURL);
                param[6] = db.MakeInParameter("@adminSSLDomain", SqlDbType.NVarChar, 510, otblStore_Checkout.AdminSSLURL);
                param[7] = db.MakeInParameter("@Mode", SqlDbType.VarChar, 10, Mode);
                param[8] = db.MakeOutParameter("@Status", SqlDbType.Int, 4);
                db.RunProcedure("P_InsertStoreCheckout", param);
                status = (int)param[8].Value;
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
        private void ResetAll()
        {
            param = null;
            db = null;
        }
    }
}