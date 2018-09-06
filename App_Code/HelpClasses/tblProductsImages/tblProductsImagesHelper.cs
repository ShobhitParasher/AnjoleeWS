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

namespace tblproductsimages
{
    /// <summary>
    /// Summary description for tblProductsImagesHelper
    /// </summary>
    public class tblProductsImagesHelper
    {
        DataBase db = null;
        SqlParameter[] param;
        //DataSet ds;

        // Default Constructor
        public tblProductsImagesHelper()
        {
        }

        # region Public Methods
       
        public void InsertProductImages(tblProductsImages otblProductsImages)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ToString()); ;
            try
            {
                con.Open();
                SqlCommand cmd1 = new SqlCommand("InsertProductImage", con);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add(new SqlParameter("@ImageID", otblProductsImages.ImageID));
                cmd1.Parameters.Add(new SqlParameter("@ProductID", otblProductsImages.ProductID));
                cmd1.Parameters.Add(new SqlParameter("@ImageType", otblProductsImages.ImageType));
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

        public void ProductImages(tblProductsImages otblProductsImages)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ToString()); ;
            try
            {
                con.Open();
                SqlCommand cmd1 = new SqlCommand("ProductImage", con);
                cmd1.CommandType = CommandType.StoredProcedure;                
                cmd1.Parameters.Add(new SqlParameter("@ImageID", otblProductsImages.ImageID));                
                cmd1.Parameters.Add(new SqlParameter("@ProductID", otblProductsImages.ProductID));
                cmd1.Parameters.Add(new SqlParameter("@ImageType", otblProductsImages.ImageType));
                cmd1.Parameters.Add(new SqlParameter("@Default", otblProductsImages.Default));

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