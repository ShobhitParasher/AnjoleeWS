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

namespace tblimages
{
    /// <summary>
    /// Summary description for tblImagesHelper
    /// </summary>
    public class tblImagesHelper
    {
        DataBase db = null;
        SqlParameter[] param;
        DataSet ds;

        public tblImagesHelper()
        {

        }

        public void InsertImage(tblimages.tblImages otblImages)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ToString()); ;
            try
            {
                con.Open();
                SqlCommand cmd1 = new SqlCommand("InsertImage", con);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add(new SqlParameter("@ImageID", otblImages.ImageID));
                cmd1.Parameters.Add(new SqlParameter("@ImageName", otblImages.ImageName));
                cmd1.Parameters.Add(new SqlParameter("@ImageType", otblImages.ImageType));
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

        public string LastInsertImage(tblimages.tblImages otblImages)
        {

            string LastImageName = string.Empty;
            if (object.Equals(db, null))
            {
                db = new DBComponent.DataBase();
            }
            if (object.Equals(ds, null))
            {
                ds = new DataSet();
            }
            param = new SqlParameter[4];
            param[0] = db.MakeInParameter("@ImageID", otblImages.ImageID);
            param[1] = db.MakeInParameter("@ImageName", SqlDbType.NVarChar, 50, otblImages.ImageName);
            param[2] = db.MakeInParameter("@ImageType", SqlDbType.NVarChar, 2, otblImages.ImageType);
            param[3] = db.MakeOutParameter("@LastImageName", SqlDbType.NVarChar, 100);
            db.RunProcedure("InsertImage", param);
            LastImageName = param[3].Value.ToString().Trim();
            ResetAll();
            return LastImageName;           
           
        }

        public DataSet GetStaticImage()
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
            db.RunProcedure("GetStaticImage", null, out ds);
            ResetAll();
            return ds;
        }
        public DataSet GetStaticImage(string searchText)
        {
            if (object.Equals(db, null))
            {
                db = new DataBase();
            }
            if (object.Equals(ds, null))
            {
                ds = new DataSet();
            }
            param = new SqlParameter[1];
            param[0] = db.MakeInParameter("@ImageName", SqlDbType.VarChar, 100, searchText);
            db.RunProcedure("SearchStaticImage", param, out ds);
            ResetAll();
            return ds;
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