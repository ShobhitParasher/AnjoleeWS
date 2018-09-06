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

namespace tblproducts
{
    /// <summary>
    /// Summary description for tblProductsHelper
    /// </summary>
    public class tblProductsHelper
    {
        DataBase db = null;
        SqlParameter[] param;
        DataSet ds;
        DataTable dt;

        public tblProductsHelper()
        {  
        }

        # region Public Methods
       
        public int OLD_InsertProduct(tblProducts otblProducts)
        {
            int status = -1;// -1 denote Insertion Failure
            try
            {
                if (object.Equals(db, null))
                    db = new DataBase();
                param = new SqlParameter[9];
                param[0] = db.MakeInParameter("@Description", SqlDbType.NVarChar, 100, otblProducts.Description);
                param[1] = db.MakeInParameter("@MetalVenderID", SqlDbType.NVarChar,0, otblProducts.MetalVendorID);
                param[2] = db.MakeInParameter("@MetalWeightInGramsForStandardSize", SqlDbType.Float,0, otblProducts.MetalWeightInGramsForStandardSize);
                param[3] = db.MakeInParameter("@LaborRateForOneGramOfMetal", SqlDbType.Float, 0, otblProducts.LaborRateForOneGramOfMetal);                
                param[4] = db.MakeInParameter("@PriceCalculationFormulaID", SqlDbType.NVarChar, 0, otblProducts.PriceCalculationFormulaID);                
                param[5] = db.MakeInParameter("@FixedPrice", SqlDbType.Float, 0, otblProducts.FixedPrice);
                //param[6] = db.MakeInParameter("@VisualOrderIndex", SqlDbType.Int, 0, otblProducts.VisualOrderIndex);
                param[6] = db.MakeInParameter("@StandardSize", SqlDbType.Float, 0, otblProducts.StandardSize);
                param[7] = db.MakeInParameter("@MinimumSize", SqlDbType.Float, 0, otblProducts.MinimumSize);
                param[8] = db.MakeInParameter("@MaximumSize", SqlDbType.Float, 0, otblProducts.MaximumSize);
                db.RunProcedure("InsertProduct", param);
                status = 0;
            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            finally
            {
                ResetAll();
            }
            return status;
        }

        public int InsertRecord(tblproducts.tblProducts otblProducts, tblproductsproductsgroups.tblProductsProductsGroups otblProductsProductsGroups, tblproductsimages.tblProductsImages otblProductsImages, int ans)
        {
            int status = -1;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ToString());
            SqlTransaction trans1;

            con.Open();
            trans1 = con.BeginTransaction();
            try
            {
                //
                SqlCommand cmd1 = new SqlCommand("InsertProduct", con, trans1);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add(new SqlParameter("@ProductID", otblProducts.ProductID));
                cmd1.Parameters.Add(new SqlParameter("@ProductName", otblProducts.ProductName));
                cmd1.Parameters.Add(new SqlParameter("@Description", otblProducts.Description));
                cmd1.Parameters.Add(new SqlParameter("@AffTitle", otblProducts.AffTitle));
                cmd1.Parameters.Add(new SqlParameter("@AffDescription", otblProducts.AffDescription));
                if (otblProducts.MetalID != null)
                {
                    if (otblProducts.MetalID.ToString() == "-1" || otblProducts.MetalID.ToString() == "")
                    {
                    }
                    else
                    {
                        cmd1.Parameters.Add(new SqlParameter("@MetalID", otblProducts.MetalID));
                    }
                }
                else
                {
                    //cmd1.Parameters.Add(new SqlParameter("@MetalID", SqlDbType.UniqueIdentifier, 64, null));
                    cmd1.Parameters.Add(new SqlParameter("@MetalID", DBNull.Value));
                }

                if (otblProducts.MetalVendorID != null)
                {
                    if (otblProducts.MetalVendorID.ToString() == "-1" || otblProducts.MetalVendorID.ToString() == "")
                    {
                        cmd1.Parameters.Add(new SqlParameter("@MetalVendorID", SqlDbType.UniqueIdentifier, 64, null));
                    }
                    else
                    {
                        cmd1.Parameters.Add(new SqlParameter("@MetalVendorID", otblProducts.MetalVendorID));
                    }
                }
                else
                {
                    //cmd1.Parameters.Add(new SqlParameter("@MetalVendorID", SqlDbType.UniqueIdentifier, 64, null));
                    cmd1.Parameters.Add(new SqlParameter("@MetalVendorID", DBNull.Value));
                }

                cmd1.Parameters.Add(new SqlParameter("@MetalWeightInGramsForStandardSize", otblProducts.MetalWeightInGramsForStandardSize));
                cmd1.Parameters.Add(new SqlParameter("@LaborRateForOneGramOfMetal", otblProducts.LaborRateForOneGramOfMetal));

                if (otblProducts.PriceCalculationFormulaID != null)
                {
                    if (otblProducts.PriceCalculationFormulaID.ToString() == "-1" || otblProducts.PriceCalculationFormulaID.ToString() == "")
                    {
                        cmd1.Parameters.Add(new SqlParameter("@PriceCalculationFormulaID", SqlDbType.UniqueIdentifier, 64, null));
                    }
                    else
                    {
                        cmd1.Parameters.Add(new SqlParameter("@PriceCalculationFormulaID", otblProducts.PriceCalculationFormulaID));
                    }
                }
                else
                {
                    //cmd1.Parameters.Add(new SqlParameter("@PriceCalculationFormulaID", SqlDbType.UniqueIdentifier, 64, null));
                    cmd1.Parameters.Add(new SqlParameter("@PriceCalculationFormulaID", DBNull.Value));
                }

                cmd1.Parameters.Add(new SqlParameter("@FixedPrice", otblProducts.FixedPrice));
                cmd1.Parameters.Add(new SqlParameter("@StandardSize", otblProducts.StandardSize));
                cmd1.Parameters.Add(new SqlParameter("@MinimumSize", otblProducts.MinimumSize));
                cmd1.Parameters.Add(new SqlParameter("@MaximumSize", otblProducts.MaximumSize));
                cmd1.Parameters.Add(new SqlParameter("@VisualOrderIndex", otblProducts.VisualOrderIndex));
                cmd1.Parameters.Add(new SqlParameter("@AvailableSize", otblProducts.AvailableSize));
                cmd1.Parameters.Add(new SqlParameter("@3DAnimationLink", otblProducts.D3AnimationLink));
                cmd1.Parameters.Add(new SqlParameter("@VoiceoverLink", otblProducts.VoiceoverLink));
                cmd1.Parameters.Add(new SqlParameter("@AvailableInTwoTones", otblProducts.AvailableInTwoTones));
                cmd1.Parameters.Add(new SqlParameter("@ProductStyleNumber", otblProducts.ProductStyleNumber));
                cmd1.Parameters.Add(new SqlParameter("@MarkupProduct", otblProducts.MarkupProduct));
                cmd1.Parameters.Add(new SqlParameter("@FakeDiscount", otblProducts.FakeDiscount));
                //cmd1.Parameters.Add(new SqlParameter("@Weight", otblProducts.Weight));
                cmd1.Parameters.Add(new SqlParameter("@Formula_ID", otblProducts.Formula_ID));
                cmd1.Parameters.Add(new SqlParameter("@ShowOnline", otblProducts.ShowOnline));
                cmd1.Parameters.Add(new SqlParameter("@BestSeller", otblProducts.BestSeller));
                cmd1.Parameters.Add(new SqlParameter("@MetaKeyword", otblProducts.MetaKeyword));
                cmd1.Parameters.Add(new SqlParameter("@AltTag", otblProducts.AltTag));
                cmd1.Parameters.Add(new SqlParameter("@MetaTitle", otblProducts.MetaTitle));
                cmd1.Parameters.Add(new SqlParameter("@MetaDescription", otblProducts.MetaDescription));

                cmd1.ExecuteNonQuery();

                SqlCommand cmd2 = new SqlCommand("InsertProductsProductsGroups", con, trans1);
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.Parameters.Add(new SqlParameter("@ProductsProductsGroupID", otblProductsProductsGroups.ProductsProductsGroupsID));
                cmd2.Parameters.Add(new SqlParameter("@ProductID", otblProductsProductsGroups.ProductID));
                cmd2.ExecuteNonQuery();

                if (ans > 0)
                {
                    SqlCommand cmd3 = new SqlCommand("InsertProductImage", con, trans1);
                    cmd3.CommandType = CommandType.StoredProcedure;
                    cmd3.Parameters.Add(new SqlParameter("@ImageID", otblProductsImages.ImageID));
                    if (otblProductsImages.ImageType.ToString() == "-1" || otblProductsImages.ImageType.ToString() == "")
                    {
                        cmd3.Parameters.Add(new SqlParameter("@ImageType", otblProductsImages.ImageType));
                    }
                    else
                    {
                        cmd3.Parameters.Add(new SqlParameter("@ImageType", otblProductsImages.ImageType));
                    }


                    cmd3.Parameters.Add(new SqlParameter("@ProductID", otblProductsImages.ProductID));
                    cmd3.ExecuteNonQuery();
                }

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

        public int InsertRecord_New(tblproducts.tblProducts otblProducts, tblproductsproductsgroups.tblProductsProductsGroups otblProductsProductsGroups, tblproductsimages.tblProductsImages otblProductsImages, int ans)
        {
            int status = -1;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ToString());
            SqlTransaction trans1;

            con.Open();
            trans1 = con.BeginTransaction();
            try
            {


                SqlCommand cmd1 = new SqlCommand("InsertProduct_New", con, trans1);
                cmd1.CommandType = CommandType.StoredProcedure;

                Guid GU;
                GU = new Guid(otblProducts.ProductID);

                cmd1.Parameters.Add("@ProductID", otblProducts.ProductID);
                // cmd1.Parameters["@ProductID"].Value = GU;

                cmd1.Parameters.Add("@ProductName", SqlDbType.NVarChar, 100, otblProducts.ProductName);
                cmd1.Parameters.Add("@Description", SqlDbType.NVarChar, 100, otblProducts.Description);
               
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

        public void UpdateRecord(tblproducts.tblProducts otblProducts)
        {           
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ToString()); ;            
            try
            {
                con.Open();
                SqlCommand cmd1 = new SqlCommand("UpdateProduct", con);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add(new SqlParameter("@ProductID", otblProducts.ProductID));
                cmd1.Parameters.Add(new SqlParameter("@ProductName", otblProducts.ProductName));
                cmd1.Parameters.Add(new SqlParameter("@Description", otblProducts.Description));
                cmd1.Parameters.Add(new SqlParameter("@AffTitle", otblProducts.AffTitle));
                cmd1.Parameters.Add(new SqlParameter("@AffDescription", otblProducts.AffDescription)); 


                if (otblProducts.MetalID != null)
                {
                    if (otblProducts.MetalID.ToString() == "-1" || otblProducts.MetalID.ToString() == "")
                    {
                    }
                    else
                    {
                        cmd1.Parameters.Add(new SqlParameter("@MetalID", otblProducts.MetalID));
                    }
                }
                else
                {
                    cmd1.Parameters.Add(new SqlParameter("@MetalID", SqlDbType.UniqueIdentifier, 64, null));
                }

                if (otblProducts.MetalVendorID != null)
                {
                    if (otblProducts.MetalVendorID.ToString() == "-1" || otblProducts.MetalVendorID.ToString() == "")
                    {
                        cmd1.Parameters.Add(new SqlParameter("@MetalVendorID", SqlDbType.UniqueIdentifier, 64, null));
                    }
                    else
                    {
                        cmd1.Parameters.Add(new SqlParameter("@MetalVendorID", otblProducts.MetalVendorID));
                    }
                }
                else
                {
                    cmd1.Parameters.Add(new SqlParameter("@MetalVendorID", SqlDbType.UniqueIdentifier, 64, null));
                }

                if (otblProducts.PriceCalculationFormulaID != null)
                {
                    if (otblProducts.PriceCalculationFormulaID.ToString() == "-1" || otblProducts.PriceCalculationFormulaID.ToString() == "")
                    {
                        cmd1.Parameters.Add(new SqlParameter("@PriceCalculationFormulaID", SqlDbType.UniqueIdentifier, 64, null));
                    }
                    else
                    {
                        cmd1.Parameters.Add(new SqlParameter("@PriceCalculationFormulaID", otblProducts.PriceCalculationFormulaID));
                    }
                }
                else
                {
                    cmd1.Parameters.Add(new SqlParameter("@PriceCalculationFormulaID", SqlDbType.UniqueIdentifier, 64, null));
                }

                

                cmd1.Parameters.Add(new SqlParameter("@MetalWeightInGramsForStandardSize", otblProducts.MetalWeightInGramsForStandardSize));
                cmd1.Parameters.Add(new SqlParameter("@LaborRateForOneGramOfMetal", otblProducts.LaborRateForOneGramOfMetal));
                cmd1.Parameters.Add(new SqlParameter("@StandardSize", otblProducts.StandardSize));
                cmd1.Parameters.Add(new SqlParameter("@MinimumSize", otblProducts.MinimumSize));
                cmd1.Parameters.Add(new SqlParameter("@MaximumSize", otblProducts.MaximumSize));



                cmd1.Parameters.Add(new SqlParameter("@VisualOrderIndex", otblProducts.VisualOrderIndex));


                cmd1.Parameters.Add(new SqlParameter("@AvailableSize", Convert.ToDecimal(otblProducts.AvailableSize)));

                cmd1.Parameters.Add(new SqlParameter("@3DAnimationLink", otblProducts.D3AnimationLink));
                cmd1.Parameters.Add(new SqlParameter("@VoiceoverLink", otblProducts.VoiceoverLink));
                cmd1.Parameters.Add(new SqlParameter("@AvailableInTwoTones", otblProducts.AvailableInTwoTones));
                cmd1.Parameters.Add(new SqlParameter("@ProductStyleNumber", otblProducts.ProductStyleNumber));
                cmd1.Parameters.Add(new SqlParameter("@MarkupProduct", otblProducts.MarkupProduct));
                cmd1.Parameters.Add(new SqlParameter("@FakeDiscount", otblProducts.FakeDiscount));
                //cmd1.Parameters.Add(new SqlParameter("@Weight", otblProducts.Weight));
                cmd1.Parameters.Add(new SqlParameter("@Formula_ID", otblProducts.Formula_ID));
                cmd1.Parameters.Add(new SqlParameter("@ShowOnline", otblProducts.ShowOnline));
                cmd1.Parameters.Add(new SqlParameter("@BestSeller", otblProducts.BestSeller));
                cmd1.Parameters.Add(new SqlParameter("@LimitedTimeOffer", otblProducts.LimitedTimeOffer));
                cmd1.Parameters.Add(new SqlParameter("@MetaKeyword", otblProducts.MetaKeyword));
                cmd1.Parameters.Add(new SqlParameter("@AltTag", otblProducts.AltTag));
                cmd1.Parameters.Add(new SqlParameter("@MetaTitle", otblProducts.MetaTitle));
                cmd1.Parameters.Add(new SqlParameter("@MetaDescription", otblProducts.MetaDescription));


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

        public void UpdatePCFormula(tblproducts.tblProducts otblProducts)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ToString()); ;
            try
            {
                con.Open();
                SqlCommand cmd1 = new SqlCommand("UpdatePCFormula", con);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add(new SqlParameter("@ProductID", otblProducts.ProductID));
                cmd1.Parameters.Add(new SqlParameter("@FixedPrice", otblProducts.FixedPrice));
                cmd1.Parameters.Add(new SqlParameter("@PriceCalculationFormulaID", otblProducts.PriceCalculationFormulaID));
                cmd1.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
                throw new Exception("Problem in updating Price Calculation Formula.");
            }
            finally
            {
                if (con.State.ToString() == "Open")
                    con.Close();
                ResetAll();
            }      
        }

        public DataSet GetProducts(string ProductsGroupID, string subProductsGroupID, string productName, int filter)
        {
            if (object.Equals(db, null))
            {
                db = new DataBase();
            }
            if (object.Equals(ds, null))
            {
                ds = new DataSet();
            }
            param = new SqlParameter[4];
            if (ProductsGroupID != "")
            {
                Guid U0 = new Guid(ProductsGroupID);
                param[0] = db.MakeInParameter("@ProductsGroupID", SqlDbType.UniqueIdentifier, 64, U0);
            }
            else
                param[0] = db.MakeInParameter("@ProductsGroupID", SqlDbType.UniqueIdentifier, 64, null);
            if (subProductsGroupID != "-1")
            {
                Guid U1 = new Guid(subProductsGroupID);
                param[1] = db.MakeInParameter("@subProductsGroupID", SqlDbType.UniqueIdentifier, 64, U1);
            }
            else
                param[1] = db.MakeInParameter("@subProductsGroupID", SqlDbType.UniqueIdentifier, 64, null);
            param[2] = db.MakeInParameter("@productName", SqlDbType.VarChar, 150, productName);
            param[3] = db.MakeInParameter("@filter", SqlDbType.Int, 0, filter);
            db.RunProcedure("SearchProduct", param, out ds);
            ResetAll();
            return ds;
        }
       /*public DataSet GetProducts(string ProductName, string ProductsGroupName)
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
            param[0] = db.MakeInParameter("@ProductName", SqlDbType.VarChar, 100, ProductName);
            param[1] = db.MakeInParameter("@ProductsGroupName", SqlDbType.VarChar, 50, ProductsGroupName);

            db.RunProcedure("GetProducts", param, out ds);
            ResetAll();
            return ds;
        }*/

        public DataSet GetProducts(string ProductName, string ProductsStyleNumber)
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
            param[0] = db.MakeInParameter("@ProductName", SqlDbType.VarChar, 100, ProductName);
            param[1] = db.MakeInParameter("@ProductsStyleNumber", SqlDbType.VarChar, 50, ProductsStyleNumber);

            db.RunProcedure("GetProducts", param, out ds);
            ResetAll();
            return ds;
        }
        public DataSet GetProductListingPaging(int pno, int psize, string categoryID)
        {
            if (object.Equals(db, null))
            {
                db = new DBComponent.DataBase();
            }
            if (object.Equals(ds, null))
            {
                ds = new DataSet();
            }
            Guid U0 = new Guid(categoryID);
            param = new SqlParameter[3];
            param[0] = db.MakeInParameter("@pgno", SqlDbType.Int, 4, pno);
            param[1] = db.MakeInParameter("@pgsize", SqlDbType.Int, 4, psize);
            param[2] = db.MakeInParameter("@ProductsGroupID", U0);
            db.RunProcedure("P_GetProductListPaging", param, out ds);
            ResetAll();
            return ds;

        }
        public DataTable GetProductListing(System.Guid id, int startIndex, int lastIndex)
        {
            try
            {
                if (object.Equals(db, null))
                {
                    db = new DBComponent.DataBase();
                }
                if (object.Equals(dt, null))
                {
                    dt = new DataTable();
                }

                param = new SqlParameter[] { this.db.MakeInParameter("@id", SqlDbType.UniqueIdentifier, 20, id), db.MakeInParameter("@startIndex", SqlDbType.Int, 4, startIndex),db.MakeInParameter("@lastIndex", SqlDbType.Int, 4, lastIndex) };

                //db.RunProcedure("P_GetProductListing", param, out dt);
                /*----- For SEO Change-----*/
                ////////////////db.RunProcedure("P_GetProductListingTest", param, out dt);
                /////////db.RunProcedure("P_GetProductListingNEW", param, out dt);
                db.RunProcedure("P_GetProductListingNEWPaging", param, out dt);
                /*----- For SEO Change-----*/
                ResetAll();
                
            }
            catch (Exception ex)
            {
                string _strError = ex.ToString();
            }
            return dt;
        }
        public DataTable GetProductListingBySearch(string style)
        {
            try
            {
                if (object.Equals(db, null))
                {
                    db = new DBComponent.DataBase();
                }
                if (object.Equals(dt, null))
                {
                    dt = new DataTable();
                }

                param = new SqlParameter[1];
                param[0] = db.MakeInParameter("@Style", SqlDbType.NVarChar, 10, style);
                /*----- For SEO Change-----*/
               /////////////////// db.RunProcedure("P_GetProductListingBySearchTest", param, out dt);
                db.RunProcedure("P_GetProductListingBySearchNEW", param, out dt);
                /*----- For SEO Change-----*/
                ResetAll();

            }
            catch (Exception ex)
            {
                string _strError = ex.ToString();
            }
            return dt;
        }

        //changed for optimization on 15sep09
        public DataSet GetProductLengthSize(string ID)
        {
            if (object.Equals(db, null))
            {
                db = new DBComponent.DataBase();
            }
            if (object.Equals(ds, null))
            {
                ds = new DataSet();
            }
            param = new SqlParameter[1];
            param[0] = db.MakeInParameter("@ID", SqlDbType.VarChar, 50, ID);
            db.RunProcedure("P_GetProductLengthSize", param, out ds);
            ResetAll();
            return ds;

        }
        public DataSet GetProductLengthSize1(string ID)
        {
            if (object.Equals(db, null))
            {
                db = new DBComponent.DataBase();
            }
            if (object.Equals(ds, null))
            {
                ds = new DataSet();
            }
            param = new SqlParameter[1];
            param[0] = db.MakeInParameter("@ID", SqlDbType.VarChar, 50, ID);
            db.RunProcedure("P_GetProductLengthSize1", param, out ds);
            ResetAll();
            return ds;

        }
        public string GetProductsProductsGroupID(string PgId)
        {
            string Pid;
            SqlConnection con = DBComponent.CommonFunctions.GetConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "P_GetProductsProductsGroupID";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@PgId", PgId));
            cmd.Connection = con;
            Pid = (cmd.ExecuteScalar()).ToString();
            con.Close();
            return Pid;
        }
        //changed for optimization on 15sep09
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
