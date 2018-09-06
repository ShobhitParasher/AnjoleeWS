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

namespace tblProductsStone
{
    /// <summary>
    /// Summary description for tblProductsHelper
    /// </summary>
    public class tblProductsStoneHelper
    {
        DataBase db = null;
        SqlParameter[] param;
        //DataSet ds;

        public tblProductsStoneHelper()
        {  
        }

        # region Public Methods


        // fill method Stone Shape
        public DataSet GetProductShape(DataSet ds)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ToString());
            con.Open();
            try
            {
                SqlCommand cmd1 = new SqlCommand("InsertProductStoneShape", con);
                cmd1.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter ad = new SqlDataAdapter(cmd1);
                ad.Fill(ds);
            }
            catch { }
            finally
            {
                con.Close();
            }
            return ds;
        }

        //Added in 26 nov
        public DataSet GetProductSetting(DataSet ds1)
        {
            SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ToString());
            con1.Open();
            try
            {
                SqlCommand cmd2 = new SqlCommand("InsertProductStonesetting", con1);
                cmd2.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter ad1 = new SqlDataAdapter(cmd2);
                ad1.Fill(ds1);
            }
            catch { }
            finally
            {
                con1.Close();
            }
            return ds1;
        }

        //////////////////////////////////////////////////

        //Added in 26 nov
        public DataSet GetVendorName(DataSet ds2)
        {
            SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ToString());
            con1.Open();
            try
            {
                SqlCommand cmd2 = new SqlCommand("InsertVendorName", con1);
                cmd2.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter ad1 = new SqlDataAdapter(cmd2);
                ad1.Fill(ds2);
            }
            catch { }
            finally
            {
                con1.Close();
            }
            return ds2;
        }

        //////////////////////////////////////////////////


        // fill method
        public DataSet GetProductID(tblProductsStone otblProducts, int Repeat, DataSet ds)
        {

            
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ToString());

            con.Open();
            try
            {
                SqlCommand cmd1 = new SqlCommand("InsertProductStone", con);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add(new SqlParameter("@ProductID", otblProducts.ProductID));
                cmd1.Parameters.Add(new SqlParameter("@ProductSizeID", otblProducts.ProductSizeID));
                cmd1.Parameters.Add(new SqlParameter("@StoneShapeID", otblProducts.StoneShapeID));
                cmd1.Parameters.Add(new SqlParameter("@StoneSettingID", otblProducts.StoneSettingID)); 
                cmd1.Parameters.Add(new SqlParameter("@StoneConfigurationID", otblProducts.StoneConfigurationID));
                cmd1.Parameters.Add(new SqlParameter("@StoneType", otblProducts.StoneType));
                cmd1.Parameters.Add(new SqlParameter("@StoneSize", otblProducts.StoneSize));
                cmd1.Parameters.Add(new SqlParameter("@StoneQTy", otblProducts.StoneQTy));
                cmd1.Parameters.Add(new SqlParameter("@VendorID", otblProducts.VendorID));
                cmd1.Parameters.Add(new SqlParameter("@Repeat", Repeat));
                SqlDataAdapter ad = new SqlDataAdapter(cmd1);
                
                ad.Fill(ds);
            }
            catch { }
            finally
            {
                con.Close();
            }
            return ds;
        }


        public int InsertRecord(tblProductsStone otblProducts, int Repeat)
        {
            int status = -1;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ToString());
            SqlTransaction trans1;

            con.Open();
            trans1 = con.BeginTransaction();
            try
            {


                SqlCommand cmd1 = new SqlCommand("InsertProductStone", con, trans1);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add(new SqlParameter("@ProductID", otblProducts.ProductID));
                cmd1.Parameters.Add(new SqlParameter("@ProductSizeID", otblProducts.ProductSizeID));
                cmd1.Parameters.Add(new SqlParameter("@StoneShapeID", otblProducts.StoneShapeID));
                cmd1.Parameters.Add(new SqlParameter("@StoneSettingID", otblProducts.StoneSettingID)); 
                cmd1.Parameters.Add(new SqlParameter("@StoneConfigurationID", otblProducts.StoneConfigurationID));
                cmd1.Parameters.Add(new SqlParameter("@StoneType", otblProducts.StoneType));
                cmd1.Parameters.Add(new SqlParameter("@StoneSize", otblProducts.StoneSize));
                cmd1.Parameters.Add(new SqlParameter("@StoneQTy", otblProducts.StoneQTy));
                cmd1.Parameters.Add(new SqlParameter("@Repeat", Repeat));
                
                cmd1.ExecuteNonQuery();
              
                trans1.Commit();
                status = 0;
            }
            catch (Exception ex)
            {
                trans1.Rollback();
                string strMsg = ex.Message;
            }
            finally
            {
                if (con.State.ToString() == "Open")
                    con.Close();
                ResetAll();
            }
            return status;
        }

        public int UpdateRecord(tblProductsStone otblProducts)
        {
            int status = -1;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ToString());
            SqlTransaction trans1;

            con.Open();
            trans1 = con.BeginTransaction();
            try
            {

                int Repeat = -1;
                SqlCommand cmd1 = new SqlCommand("InsertProductStone", con, trans1);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add(new SqlParameter("@ProductID", otblProducts.ProductID));
                cmd1.Parameters.Add(new SqlParameter("@ProductSizeID", otblProducts.ProductSizeID));
                cmd1.Parameters.Add(new SqlParameter("@StoneShapeID", otblProducts.StoneShapeID));
                cmd1.Parameters.Add(new SqlParameter("@StoneSettingID", otblProducts.StoneSettingID));                
                cmd1.Parameters.Add(new SqlParameter("@StoneConfigurationID", otblProducts.StoneConfigurationID));
                cmd1.Parameters.Add(new SqlParameter("@StoneType", otblProducts.StoneType));
                cmd1.Parameters.Add(new SqlParameter("@StoneSize", otblProducts.StoneSize));
                cmd1.Parameters.Add(new SqlParameter("@StoneQTy", otblProducts.StoneQTy));
                cmd1.Parameters.Add(new SqlParameter("@CaratWeight", otblProducts.CaratWeight));
                cmd1.Parameters.Add(new SqlParameter("@VendorID", otblProducts.VendorID));
                cmd1.Parameters.Add(new SqlParameter("@Repeat", Repeat));
                
                cmd1.ExecuteNonQuery();
                               
                trans1.Commit();
                status = 0;
            }
            catch (Exception ex)
            {
                trans1.Rollback();
                string strMsg = ex.Message;
            }
            finally
            {
                if (con.State.ToString() == "Open")
                    con.Close();
                ResetAll();
            }
            return status;
        }


        public int RemoveRecord(tblProductsStone otblProducts)
        {
            int status = -1;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ToString());
            SqlTransaction trans1;

            con.Open();
            trans1 = con.BeginTransaction();
            try
            {

                int Repeat = -2;
                SqlCommand cmd1 = new SqlCommand("InsertProductStone", con, trans1);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add(new SqlParameter("@ProductID", otblProducts.ProductID));
                cmd1.Parameters.Add(new SqlParameter("@ProductSizeID", otblProducts.ProductSizeID));
                cmd1.Parameters.Add(new SqlParameter("@StoneShapeID", otblProducts.StoneShapeID));
                cmd1.Parameters.Add(new SqlParameter("@StoneConfigurationID", otblProducts.StoneConfigurationID));
                cmd1.Parameters.Add(new SqlParameter("@StoneType", otblProducts.StoneType));
                cmd1.Parameters.Add(new SqlParameter("@StoneSize", otblProducts.StoneSize));
                cmd1.Parameters.Add(new SqlParameter("@StoneQTy", otblProducts.StoneQTy));
                cmd1.Parameters.Add(new SqlParameter("@Repeat", Repeat));
                
                cmd1.ExecuteNonQuery();

                trans1.Commit();
                status = 0;
            }
            catch (Exception ex)
            {
                trans1.Rollback();
                string strMsg = ex.Message;
            }
            finally
            {
                if (con.State.ToString() == "Open")
                    con.Close();
                ResetAll();
            }
            return status;
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
