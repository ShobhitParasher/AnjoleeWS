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

namespace tblproductscolorstones
{
/// <summary>
/// Summary description for tblProductsColorStonesHelper
/// </summary>
    public class tblProductsColorStonesHelper
    {
        DataBase db = null;
        SqlParameter[] param;
        //DataSet ds;

        public tblProductsColorStonesHelper()
        {
        }

        # region Public Methods

        public void InsertProductsColorStones(tblproductscolorstones.tblProductsColorStones otblProductsColorStones)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ToString()); ;
            try
            {
                con.Open();
                SqlCommand cmd1 = new SqlCommand("InsertProductsColorStones", con);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add(new SqlParameter("@ColorStoneWeight", otblProductsColorStones.ColorStoneWeight));
                cmd1.Parameters.Add(new SqlParameter("@ColorStoneColor", otblProductsColorStones.ColorStoneColor));
                cmd1.Parameters.Add(new SqlParameter("@ColorStoneClarity", otblProductsColorStones.ColorStoneClarity));
                cmd1.Parameters.Add(new SqlParameter("@ColorStoneShape", otblProductsColorStones.ColorStoneShape));
                cmd1.Parameters.Add(new SqlParameter("@ColorStoneCut", otblProductsColorStones.ColorStoneCut));
                cmd1.Parameters.Add(new SqlParameter("@ColorStoneVenderID", otblProductsColorStones.ColorStoneVenderID));
                cmd1.Parameters.Add(new SqlParameter("@ColorStoneType", otblProductsColorStones.ColorStoneType));
                cmd1.Parameters.Add(new SqlParameter("@ColorStoneSize", otblProductsColorStones.ColorStoneSize));                                
                cmd1.Parameters.Add(new SqlParameter("@ProductID", otblProductsColorStones.ProductID));
                cmd1.Parameters.Add(new SqlParameter("@StoneSettingID", otblProductsColorStones.StoneSettingID));
                cmd1.Parameters.Add(new SqlParameter("@StoneSettingVenderID", otblProductsColorStones.StoneSettingVenderID));
                cmd1.Parameters.Add(new SqlParameter("@NoOfColorStonesForStandardSize", otblProductsColorStones.NoOfColorStonesForStandardSize));
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
