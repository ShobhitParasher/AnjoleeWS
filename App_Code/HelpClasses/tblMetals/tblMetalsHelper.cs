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

namespace tblmetals
{
    /// <summary>
    /// Summary description for tblMetalsHelper
    /// </summary>
    public class tblMetalsHelper
    {
        DataBase db = null;
        SqlParameter[] param;
        //DataSet ds;

        # region Public Methods

        public tblMetalsHelper()
        {
        }

        public void InsertMetal(tblmetals.tblMetals otblMetals)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ToString()); ;
            try
            {
                con.Open();
                SqlCommand cmd1 = new SqlCommand("InsertMetal", con);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add(new SqlParameter("@MetalID", otblMetals.MetalID));                
                cmd1.Parameters.Add(new SqlParameter("@MetalVendorID", otblMetals.MetalVendorID));
                cmd1.Parameters.Add(new SqlParameter("@MetalName", otblMetals.MetalName));
                //cmd1.Parameters.Add(new SqlParameter("@Description", otblMetals.Description));
                cmd1.Parameters.Add(new SqlParameter("@MetalLoss", otblMetals.MetalLoss));                
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

        public void UpdateMetal(tblmetals.tblMetals otblMetals)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ToString()); ;
            try
            {
                con.Open();
                SqlCommand cmd1 = new SqlCommand("UpdateMetal", con);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add(new SqlParameter("@MetalID", otblMetals.MetalID));                
                cmd1.Parameters.Add(new SqlParameter("@MetalVendorID", otblMetals.MetalVendorID));
                cmd1.Parameters.Add(new SqlParameter("@MetalName", otblMetals.MetalName));
                //cmd1.Parameters.Add(new SqlParameter("@Description", otblMetals.Description));
                cmd1.Parameters.Add(new SqlParameter("@MetalLoss", otblMetals.MetalLoss));     
                cmd1.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
                throw new Exception("Problem In Updating Record.");
            }
            finally
            {
                if (con.State.ToString() == "Open")
                    con.Close();
                ResetAll();
            }
        }

        #endregion

        #region Private Methods

        private void ResetAll()
        {
            db = null;
            param = null;
        }

        #endregion


    }
}