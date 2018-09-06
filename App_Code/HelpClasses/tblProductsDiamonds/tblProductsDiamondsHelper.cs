using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DBComponent;

namespace tblproductsdiamonds
{
    /// <summary>
    /// Summary description for tblProductsDiamondsHelper
    /// </summary>
    public class tblProductsDiamondsHelper
    {
        DataBase db = null;
        SqlParameter[] param;
        //DataSet ds;

        public tblProductsDiamondsHelper()
        {
        }

        public void InsertProductsDiamonds(tblproductsdiamonds.tblProductsDiamonds otblProductsDiamonds)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ToString()); ;
            try
            {
                con.Open();
                SqlCommand cmd1 = new SqlCommand("InsertProductsDiamonds", con);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add(new SqlParameter("@ProductDiamondID", otblProductsDiamonds.ProductDiamondID));
                cmd1.Parameters.Add(new SqlParameter("@DiamondID", otblProductsDiamonds.DiamondID));
                cmd1.Parameters.Add(new SqlParameter("@ProductID", otblProductsDiamonds.ProductID));
                cmd1.Parameters.Add(new SqlParameter("@StoneSettingID", otblProductsDiamonds.StoneSettingID));
                cmd1.Parameters.Add(new SqlParameter("@StoneSettingVendorID", otblProductsDiamonds.StoneSettingVendorID));
                cmd1.Parameters.Add(new SqlParameter("@NoOfDiamondsForStandardSize", otblProductsDiamonds.NoOfDiamondsForStandardSize));
                cmd1.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
                throw new Exception("Problem In Saving Record.");
            }
            finally
            {
                if (con.State.ToString() == "Open")
                    con.Close();
                ResetAll();
            }
        }

        #region Private Methods

        private void ResetAll()
        {
            db = null;
            param = null;
        }

        #endregion

    }

}