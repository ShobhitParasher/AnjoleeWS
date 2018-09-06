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

namespace tblStore_Shipping
{
    /// <summary>
    /// Summary description for tblStore_ShippingHelper
    /// </summary>
    public class tblStore_ShippingHelper
    {
        public tblStore_ShippingHelper()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        private DataBase db;
        SqlParameter[] param;
        DataSet ds;
        public DataSet GetShiipingDetails()
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
            db.RunProcedure("P_GetShiipingDetails", null, out ds);
            ResetAll();
            return ds;
        }
        public int UpdateShipping(tblStore_Shipping otblStore_Shipping)
        {
            int status = -1;
            try
            {
                if (object.Equals(db, null))
                {
                    db = new DataBase();
                }
                param = new SqlParameter[4];
                /*
                param[0] = db.MakeInParameter("@method", SqlDbType.NVarChar, 100, otblStore_Shipping.method);
                param[1] = db.MakeInParameter("@description", SqlDbType.NVarChar, 50, otblStore_Shipping.description);
                param[2] = db.MakeInParameter("@price", SqlDbType.Decimal, 8, otblStore_Shipping.price);
                */
                param[0] = db.MakeInParameter("@AccessLicenseNumber", SqlDbType.VarChar, 50, otblStore_Shipping.AccessLicenseNumber);
                param[1] = db.MakeInParameter("@UserId", SqlDbType.VarChar, 50, otblStore_Shipping.UserId);
                param[2] = db.MakeInParameter("@Password", SqlDbType.VarChar, 50, otblStore_Shipping.Password);
                param[3] = db.MakeOutParameter("@Status", SqlDbType.Int, 4);
                db.RunProcedure("P_UpdateShipping", param);
                status = (int)param[3].Value;
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