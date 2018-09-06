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

namespace tbldiamonds
{
    /// <summary>
    /// Summary description for tblDiamondsHelper
    /// </summary>
    public class tblDiamondsHelper
    {
        DataBase db = null;
        SqlParameter[] param;
        DataSet ds;


        public tblDiamondsHelper()
        {
        }


        # region Public Methods

        public void InsertDiamond(tbldiamonds.tblDiamonds otblDiamonds)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ToString()); ;
            try
            {
                con.Open();
                SqlCommand cmd1 = new SqlCommand("InsertDiamond", con);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add(new SqlParameter("@DiamondID", otblDiamonds.DiamondID));               
                cmd1.Parameters.Add(new SqlParameter("@Weight1", otblDiamonds.Weight1));
                cmd1.Parameters.Add(new SqlParameter("@Weight2", otblDiamonds.Weight2));
                cmd1.Parameters.Add(new SqlParameter("@Color", otblDiamonds.Color));
                cmd1.Parameters.Add(new SqlParameter("@Clarity", otblDiamonds.Clarity));
                cmd1.Parameters.Add(new SqlParameter("@Cut", otblDiamonds.Cut));
                cmd1.Parameters.Add(new SqlParameter("@Shape", otblDiamonds.Shape));
                if (otblDiamonds.VendorID == "-1")
                {
                    cmd1.Parameters.Add(new SqlParameter("@VendorID", SqlDbType.UniqueIdentifier, 64, null));
                }
                else
                {
                    cmd1.Parameters.Add(new SqlParameter("@VendorID", otblDiamonds.VendorID));
                }      
                //cmd1.Parameters.Add(new SqlParameter("@ImageID", otblDiamonds.ImageID));
                cmd1.Parameters.Add(new SqlParameter("@Price", otblDiamonds.Price));
                cmd1.Parameters.Add(new SqlParameter("@SpecialPremium", otblDiamonds.SpecialPremium));
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

        public void UpdateDiamond(tbldiamonds.tblDiamonds otblDiamonds)
        {            
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ToString()); ;
            try
            {
                con.Open();
                SqlCommand cmd1 = new SqlCommand("UpdateDiamond", con);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add(new SqlParameter("@DiamondID", otblDiamonds.DiamondID));                
                cmd1.Parameters.Add(new SqlParameter("@Weight1", otblDiamonds.Weight1));
                cmd1.Parameters.Add(new SqlParameter("@Weight2", otblDiamonds.Weight2));
                cmd1.Parameters.Add(new SqlParameter("@Color", otblDiamonds.Color));
                cmd1.Parameters.Add(new SqlParameter("@Clarity", otblDiamonds.Clarity));
                cmd1.Parameters.Add(new SqlParameter("@Cut", otblDiamonds.Cut));
                cmd1.Parameters.Add(new SqlParameter("@Shape", otblDiamonds.Shape));
                if (otblDiamonds.VendorID=="-1")
                {
                    cmd1.Parameters.Add(new SqlParameter("@VendorID",SqlDbType.UniqueIdentifier, 64, null));
                }
                else
                {
                    cmd1.Parameters.Add(new SqlParameter("@VendorID", otblDiamonds.VendorID));
                }                
                //cmd1.Parameters.Add(new SqlParameter("@ImageID", otblDiamonds.ImageID));
                cmd1.Parameters.Add(new SqlParameter("@Price", otblDiamonds.Price));
                cmd1.Parameters.Add(new SqlParameter("@SpecialPremium", otblDiamonds.SpecialPremium)); 
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

        public DataSet GetDiamonds()
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
            db.RunProcedure("GetDiamonds", null, out ds);
            ResetAll();
            return ds;
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
