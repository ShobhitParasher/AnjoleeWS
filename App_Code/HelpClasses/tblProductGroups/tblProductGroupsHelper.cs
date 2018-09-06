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

namespace tblProductGroups
{
    /// <summary>
    /// Summary description for tblCompaniesHelper
    /// </summary>
    public class tblProductGroupsHelper
    {
        DBComponent.DataBase db = null;
        SqlParameter[] param;
        DataSet ds;

        // Default Constructor
        public tblProductGroupsHelper() { }



        public DataSet GetProductExcProductGroup()
        {


            string strSql = "SELECT ProductID, ProductName FROM Products WHERE (ProductID NOT IN (SELECT a.ProductID FROM ProductsProductsGroups AS a INNER JOIN Products AS b ON a.ProductID = b.ProductID))";
            SqlConnection con = DBComponent.CommonFunctions.GetConnection();
            con.Open();

            SqlDataAdapter ad = new SqlDataAdapter(strSql, con);
            DataSet ds = new DataSet();
            ad.Fill(ds);

            con.Close();
            return ds;

        }

        public DataSet GetAllProductGroup()
        {
            //if (object.Equals(db, null))
            //{
            //    db = new DBComponent.DataBase();
            //}
            //if (object.Equals(ds, null))
            //{
            //    ds = new DataSet();
            //}
            //db.RunProcedure("P_GetAllAdminUsers", null, out ds);
            //ResetAll();
            //return ds;


            string strSql = "SELECT     a.ProductID, b.ProductName FROM         ProductsProductsGroups AS a INNER JOIN Products AS b ON a.ProductID = b.ProductID GROUP BY a.ProductID, b.ProductName";
            SqlConnection con = DBComponent.CommonFunctions.GetConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand(strSql, con);
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            ad.Fill(ds);

            con.Close();
            return ds;

        }

        public void DeleteProductGroup(Guid ID)
        {

            string strSql = "delete from ProductsProductsGroups where ProductID ='" + ID.ToString() + "'";
            SqlConnection con = DBComponent.CommonFunctions.GetConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand(strSql, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void InsertNewUserAccess(string userName, string ModuleSectionId)
        {

            string strSql = "Insert into ProductsProductsGroups (ProductID, ProductsProductsGroupID) values('" + userName.ToString() + "','" + ModuleSectionId.ToString() + "')";
            SqlConnection con = DBComponent.CommonFunctions.GetConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand(strSql, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public static SqlConnection GetConnection()
        {
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Constr"].ToString());
            return con;
        }

        public DataSet ExecuteQueryReturnDataSet(string query)
        {
            SqlConnection con = GetConnection();
            SqlDataAdapter ad = new SqlDataAdapter(query, con);
            con.Open();
            ds = new DataSet();
            ad.Fill(ds);

            return ds;
        }

        public void ExecuteQueryReturnNothing(string query)
        {
            SqlConnection con = GetConnection();
            SqlCommand cmd = new SqlCommand(query, con);
            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                string test = ex.ToString();
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }

        public string ExecuteQueryReturnSingleString(string query)
        {
            string result = string.Empty;
            SqlDataReader dr = null;
            SqlConnection con = GetConnection();
            SqlCommand cmd = new SqlCommand(query, con);
            try
            {
                cmd.Connection.Open();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.Read())
                {
                    result = dr[0].ToString();
                }
            }
            catch (Exception ex)
            {
                string test = ex.ToString();
            }
            finally
            {
                dr.Close();
            }
            return result;
        }


        public DataSet GetProduct(string PID)
        {
            if (object.Equals(db, null))
            {
                db = new DataBase();
            }
            if (object.Equals(ds, null))
            {
                //DataSet ds;
                ds = new DataSet();
            }
            param = new SqlParameter[1];
            if (PID == "")
            {
                param[0] = db.MakeInParameter("@ProductsGroupID", SqlDbType.UniqueIdentifier, 64, null);
            }
            else
            {
                Guid g = new Guid(PID);

                param[0] = db.MakeInParameter("@ProductsGroupID", SqlDbType.UniqueIdentifier, 64, g);
            }
            db.RunProcedure("EditProductGroups", param, out ds);
            ResetAll();
            return ds;
        }

        /* The following method will insert a new record in the table [Companies] */
        public string SPProduct(tblProductGroups objProductGroup, String Action)
        {
            string str;
            //int status = -1; // denote failure
            try
            {
                if (object.Equals(db, null))
                {
                    db = new DBComponent.DataBase();
                    param = new SqlParameter[19];
                    if (objProductGroup.ProductsGroupID.Equals(System.Guid.Empty))
                    {
                        param[0] = db.MakeInParameter("@ProductsGroupID", SqlDbType.UniqueIdentifier, 64, null);
                    }
                    else
                    {
                        param[0] = db.MakeInParameter("@ProductsGroupID", SqlDbType.UniqueIdentifier, 64, objProductGroup.ProductsGroupID);
                    }
                    param[1] = db.MakeInParameter("@Description", SqlDbType.NVarChar, 4000, objProductGroup.Description);
                    param[2] = db.MakeInParameter("@VisualOrderIndex", SqlDbType.Int, 0, objProductGroup.VisualOrderIndex);
                    if (objProductGroup.ParentProductGroupID.Equals(System.Guid.Empty))
                    {
                        param[3] = db.MakeInParameter("@ParentProductGroupID", SqlDbType.UniqueIdentifier, 64, null);
                    }
                    else
                    {
                        param[3] = db.MakeInParameter("@ParentProductGroupID", SqlDbType.UniqueIdentifier, 64, objProductGroup.ParentProductGroupID);
                    }
                    param[4] = db.MakeInParameter("@MinimumPrice", SqlDbType.Float, 0, objProductGroup.MinimumPrice);
                    param[5] = db.MakeInParameter("@MaximumPrice", SqlDbType.Float, 0, objProductGroup.MaximumPrice);
                    param[6] = db.MakeParameter("@Action", SqlDbType.NVarChar, 10, ParameterDirection.InputOutput, Action);
                    if (objProductGroup.ImageID.Equals(System.Guid.Empty))
                    {
                        param[7] = db.MakeInParameter("@ImageID", SqlDbType.UniqueIdentifier, 64, null);
                    }
                    else
                    {
                        param[7] = db.MakeInParameter("@ImageID", SqlDbType.UniqueIdentifier, 64, objProductGroup.ImageID);
                    }
                    if (objProductGroup.thumbImageID.Equals(System.Guid.Empty))
                    {
                        param[8] = db.MakeInParameter("@thumbImageID", SqlDbType.UniqueIdentifier, 64, null);
                    }
                    else
                    {
                        param[8] = db.MakeInParameter("@thumbImageID", SqlDbType.UniqueIdentifier, 64, objProductGroup.thumbImageID);
                    }
                    param[9] = db.MakeInParameter("@ProductsGroupName", SqlDbType.NVarChar, 50, objProductGroup.ProductsGroupName);
                    param[10] = db.MakeInParameter("@DefaultSize", SqlDbType.NVarChar, 10, objProductGroup.DefaultSize);
                    //param[10] = db.MakeInParameter("@GroupDimension", SqlDbType.NVarChar, 1, objProductGroup.GroupDimension);
                    param[11] = db.MakeInParameter("@AudioLink", SqlDbType.NVarChar, 1000, objProductGroup.AudioLink.Trim());
                    param[12] = db.MakeInParameter("@Size", SqlDbType.Bit, 1, objProductGroup.Size);
                    param[13] = db.MakeInParameter("@Length", SqlDbType.Bit, 1, objProductGroup.Length);
                    param[14] = db.MakeInParameter("@Enable", SqlDbType.Bit, 1, objProductGroup.Enable);
                    param[15] = db.MakeInParameter("@ThumbnailDescription", SqlDbType.NVarChar, 4000, objProductGroup.ThumbnailDescription);
                    param[16] = db.MakeInParameter("@Description_Anjolee", SqlDbType.VarChar, 4000, objProductGroup.Description_Anjolee);
                    param[17] = db.MakeInParameter("@ThumbDescription_Anjolee", SqlDbType.VarChar, 4000, objProductGroup.ThumbDescription_Anjolee);
                    param[18] = db.MakeInParameter("@ThumbAnjoleeTitle", SqlDbType.VarChar, 50, objProductGroup.ThumbAnjoleeTitle);

                    db.RunProcedure("AddProductGroups", param);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                str = param[6].Value.ToString();
                ResetAll();
            }
            return str;
        }

        /* The following method will insert a new record in the table [Companies] */
        public DataSet StoredProcedureProduct(tblProductGroups objProductGroup, String Action)
        {
            DataSet DataSetResult = new DataSet();

            //int status = -1; // denote failure
            try
            {
                if (object.Equals(db, null))
                {
                    db = new DBComponent.DataBase();
                    param = new SqlParameter[12];

                    //obj = new object();
                    //obj.value = "Select";


                    if (objProductGroup.ProductsGroupID.Equals(System.Guid.Empty))
                    {
                        param[0] = db.MakeInParameter("@ProductsGroupID", SqlDbType.UniqueIdentifier, 64, null);
                    }
                    else
                    {
                        param[0] = db.MakeInParameter("@ProductsGroupID", SqlDbType.UniqueIdentifier, 64, objProductGroup.ProductsGroupID);
                    }
                    param[1] = db.MakeInParameter("@Description", SqlDbType.NVarChar, 30, objProductGroup.Description);
                    param[2] = db.MakeInParameter("@VisualOrderIndex", SqlDbType.Int, 0, objProductGroup.VisualOrderIndex);
                    if (objProductGroup.ParentProductGroupID.Equals(System.Guid.Empty))
                    {
                        param[3] = db.MakeInParameter("@ParentProductGroupID", SqlDbType.UniqueIdentifier, 64, null);
                    }
                    else
                    {
                        param[3] = db.MakeInParameter("@ParentProductGroupID", SqlDbType.UniqueIdentifier, 64, objProductGroup.ParentProductGroupID);
                    }
                    param[4] = db.MakeInParameter("@MinimumPrice", SqlDbType.Float, 0, objProductGroup.MinimumPrice);
                    param[5] = db.MakeInParameter("@MaximumPrice", SqlDbType.Float, 0, objProductGroup.MaximumPrice);
                    param[6] = db.MakeParameter("@Action", SqlDbType.NVarChar, 10, ParameterDirection.InputOutput, Action);
                    if (objProductGroup.ImageID.Equals(System.Guid.Empty))
                    {
                        param[7] = db.MakeInParameter("@ImageID", SqlDbType.UniqueIdentifier, 64, null);

                    }
                    else
                    {
                        param[7] = db.MakeInParameter("@ImageID", SqlDbType.UniqueIdentifier, 64, objProductGroup.ImageID);

                    }
                    param[8] = db.MakeInParameter("@ProductsGroupName", SqlDbType.NVarChar, 50, objProductGroup.ProductsGroupName);
                    param[9] = db.MakeInParameter("@Size", SqlDbType.Bit, 1, objProductGroup.Size);
                    param[10] = db.MakeInParameter("@Length", SqlDbType.Bit, 1, objProductGroup.Length);
                    param[11] = db.MakeInParameter("@Enable", SqlDbType.Bit, 1, objProductGroup.Enable);


                    db.RunProcedure("AddProductGroups", param, out DataSetResult);
                    // status = 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                ResetAll();
            }


            return DataSetResult;
        }

        public DataSet GetProductsGroupName(string ProductsGroupName)
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
            param[0] = db.MakeInParameter("@ProductsGroupName", SqlDbType.VarChar, 50, ProductsGroupName);
            db.RunProcedure("GetProductsGroups", param, out ds);
            ResetAll();
            return ds;
        }
        protected void ResetAll()
        {
            param = null;
            db = null;
        }
    }
}
