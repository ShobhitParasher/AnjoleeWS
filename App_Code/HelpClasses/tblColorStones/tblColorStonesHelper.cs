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

namespace tblcolorstones
{
    /// <summary>
    /// Summary description for tblColorStonesHelper
    /// </summary>
    public class tblColorStonesHelper
    {
        DataBase db = null;
        SqlParameter[] param;
        //DataSet ds;

        public tblColorStonesHelper()
        {       
        }
        # region Public Methods

        public void InsertColorStone(tblcolorstones.tblColorStones otblColorStones)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ToString()); ;
            try
            {
                con.Open();
                SqlCommand cmd1 = new SqlCommand("InsertColorStone", con);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add(new SqlParameter("@ColorStoneID", otblColorStones.ColorStoneID));
                cmd1.Parameters.Add(new SqlParameter("@Weight", otblColorStones.Weight));
                cmd1.Parameters.Add(new SqlParameter("@Color", otblColorStones.Color));
                //cmd1.Parameters.Add(new SqlParameter("@Clarity", otblColorStones.Clarity));
                //cmd1.Parameters.Add(new SqlParameter("@Cut", otblColorStones.Cut));
                cmd1.Parameters.Add(new SqlParameter("@Shape", otblColorStones.Shape));
                //cmd1.Parameters.Add(new SqlParameter("@Type", otblColorStones.Type));
                cmd1.Parameters.Add(new SqlParameter("@Size", otblColorStones.Size));
                cmd1.Parameters.Add(new SqlParameter("@VendorID", otblColorStones.VendorID));
                //cmd1.Parameters.Add(new SqlParameter("@ImageID", otblColorStones.ImageID));
                cmd1.Parameters.Add(new SqlParameter("@Price", otblColorStones.Price));
                cmd1.Parameters.Add(new SqlParameter("@Weight1", otblColorStones.Weight1));
                cmd1.Parameters.Add(new SqlParameter("@ColorStoneType", otblColorStones.ColorStoneType));

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

        public void UpdateColorStone(tblcolorstones.tblColorStones otblColorStones)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ToString()); ;
            try
            {
                con.Open();
                SqlCommand cmd1 = new SqlCommand("UpdateColorStone", con);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add(new SqlParameter("@ColorStoneID", otblColorStones.ColorStoneID));                
                cmd1.Parameters.Add(new SqlParameter("@Weight", otblColorStones.Weight));
                cmd1.Parameters.Add(new SqlParameter("@Color", otblColorStones.Color));
                //cmd1.Parameters.Add(new SqlParameter("@Clarity", otblColorStones.Clarity));
                //cmd1.Parameters.Add(new SqlParameter("@Cut", otblColorStones.Cut));
                cmd1.Parameters.Add(new SqlParameter("@Shape", otblColorStones.Shape));
                //cmd1.Parameters.Add(new SqlParameter("@Type", otblColorStones.Type));
                cmd1.Parameters.Add(new SqlParameter("@Size", otblColorStones.Size));
                cmd1.Parameters.Add(new SqlParameter("@VendorID", otblColorStones.VendorID));
                //cmd1.Parameters.Add(new SqlParameter("@ImageID", otblColorStones.ImageID));
                cmd1.Parameters.Add(new SqlParameter("@Price", otblColorStones.Price));
                cmd1.Parameters.Add(new SqlParameter("@Weight1", otblColorStones.Weight1));
                cmd1.Parameters.Add(new SqlParameter("@ColorStoneType", otblColorStones.ColorStoneType));
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
