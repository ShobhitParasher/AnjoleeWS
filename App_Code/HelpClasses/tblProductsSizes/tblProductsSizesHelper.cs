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

namespace tblProductsSizes
{
    /// <summary>
    /// Summary description for tblProductsHelper
    /// </summary>
    public class tblProductsSizesHelper
    {
        DataBase db = null;
        SqlParameter[] param;
        DataSet ds;
        //DataTable dt;

        public tblProductsSizesHelper()
        {  
        }

        # region Public Methods


        public DataSet GetProductID(tblProductsSizes otblProducts, int Repeat, DataSet ds)
        {

            
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ToString());

            con.Open();
            try
            {
                SqlCommand cmd1 = new SqlCommand("InsertProductSizes", con);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add(new SqlParameter("@ProductID", otblProducts.ProductID));
                cmd1.Parameters.Add(new SqlParameter("@ProductSizeID", otblProducts.ProductSizeID));
                cmd1.Parameters.Add(new SqlParameter("@Sizes", otblProducts.Sizes));
                cmd1.Parameters.Add(new SqlParameter("@14kDefaultWeight", otblProducts.K14DefaultWeight));
                cmd1.Parameters.Add(new SqlParameter("@GoldLabor", otblProducts.GoldLabor));

                cmd1.Parameters.Add(new SqlParameter("@PlatinumLabor", otblProducts.PlatinumLabor));
                cmd1.Parameters.Add(new SqlParameter("@TotalTypeOfStones", otblProducts.TotalTypeOfStones));
                cmd1.Parameters.Add(new SqlParameter("@DefaultLength", otblProducts.DefaultLength));
                cmd1.Parameters.Add(new SqlParameter("@TotalNoOfStones", otblProducts.TotalNoOfStones));

                cmd1.Parameters.Add(new SqlParameter("@StandardSize", otblProducts.StandardSize));
                cmd1.Parameters.Add(new SqlParameter("@Height", otblProducts.Height));
                cmd1.Parameters.Add(new SqlParameter("@Width", otblProducts.Width));                
                
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


        public int InsertRecord(tblProductsSizes otblProducts, int Repeat)
        {
            int status = -1;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ToString());
            SqlTransaction trans1;

            con.Open();
            trans1 = con.BeginTransaction();
            try
            {


                SqlCommand cmd1 = new SqlCommand("InsertProductSizes", con, trans1);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add(new SqlParameter("@ProductID", otblProducts.ProductID));
                cmd1.Parameters.Add(new SqlParameter("@ProductSizeID", otblProducts.ProductSizeID));
                cmd1.Parameters.Add(new SqlParameter("@Sizes", otblProducts.Sizes));
                cmd1.Parameters.Add(new SqlParameter("@14kDefaultWeight", otblProducts.K14DefaultWeight));
                cmd1.Parameters.Add(new SqlParameter("@GoldLabor", otblProducts.GoldLabor));

                cmd1.Parameters.Add(new SqlParameter("@PlatinumLabor", otblProducts.PlatinumLabor));
                cmd1.Parameters.Add(new SqlParameter("@TotalTypeOfStones", otblProducts.TotalTypeOfStones));
                cmd1.Parameters.Add(new SqlParameter("@DefaultLength", otblProducts.DefaultLength));
                cmd1.Parameters.Add(new SqlParameter("@TotalNoOfStones", otblProducts.TotalNoOfStones));

                cmd1.Parameters.Add(new SqlParameter("@StandardSize", otblProducts.StandardSize));
                //cmd1.Parameters.Add(new SqlParameter("@ProductsImage", otblProducts.ProductsImage));

                cmd1.Parameters.Add(new SqlParameter("@Repeat", Repeat));
                
                cmd1.ExecuteNonQuery();

                //// second insert table
                //SqlCommand cmd2 = new SqlCommand("InsertProductsProductsGroups", con, trans1);
                //cmd2.CommandType = CommandType.StoredProcedure;
                //cmd2.Parameters.Add(new SqlParameter("@ProductsProductsGroupID", otblProductsProductsGroups.ProductsProductsGroupsID));
                //cmd2.Parameters.Add(new SqlParameter("@ProductID", otblProductsProductsGroups.ProductID));
                //cmd2.ExecuteNonQuery();



                //// Third insert table
                //SqlCommand cmd3 = new SqlCommand("InsertProductImage", con, trans1);
                //    cmd3.CommandType = CommandType.StoredProcedure;
                //    cmd3.Parameters.Add(new SqlParameter("@ImageID", otblProductsImages.ImageID));
                //    if (otblProductsImages.ImageType.ToString() == "-1" || otblProductsImages.ImageType.ToString() == "")
                //    {
                //        cmd3.Parameters.Add(new SqlParameter("@ImageType", otblProductsImages.ImageType));
                //    }
                //    else
                //    {
                //        cmd3.Parameters.Add(new SqlParameter("@ImageType", otblProductsImages.ImageType));
                //    }


                //    cmd3.Parameters.Add(new SqlParameter("@ProductID", otblProductsImages.ProductID));
                //    cmd3.ExecuteNonQuery();


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

        public int UpdateRecord(tblProductsSizes otblProducts)
        {
            int status = -1;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ToString());
            SqlTransaction trans1;

            con.Open();
            trans1 = con.BeginTransaction();
            try
            {

                int Repeat = -1;
                SqlCommand cmd1 = new SqlCommand("InsertProductSizes", con, trans1);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add(new SqlParameter("@ProductID", otblProducts.ProductID));
                cmd1.Parameters.Add(new SqlParameter("@ProductSizeID", otblProducts.ProductSizeID));
                cmd1.Parameters.Add(new SqlParameter("@Sizes", otblProducts.Sizes));
                cmd1.Parameters.Add(new SqlParameter("@14kDefaultWeight", otblProducts.K14DefaultWeight));
                cmd1.Parameters.Add(new SqlParameter("@GoldLabor", otblProducts.GoldLabor));
                cmd1.Parameters.Add(new SqlParameter("@PlatinumLabor", otblProducts.PlatinumLabor));
                cmd1.Parameters.Add(new SqlParameter("@TotalTypeOfStones", otblProducts.TotalTypeOfStones));
                cmd1.Parameters.Add(new SqlParameter("@DefaultLength", otblProducts.DefaultLength));
                cmd1.Parameters.Add(new SqlParameter("@TotalNoOfStones", otblProducts.TotalNoOfStones));                
                cmd1.Parameters.Add(new SqlParameter("@StandardSize", otblProducts.StandardSize));                
                cmd1.Parameters.Add(new SqlParameter("@Height", otblProducts.Height));
                cmd1.Parameters.Add(new SqlParameter("@Width", otblProducts.Width));
                cmd1.Parameters.Add(new SqlParameter("@IsSelectImage", otblProducts.SelectImage));
                
                cmd1.Parameters.Add(new SqlParameter("@Repeat", Repeat));
                
                cmd1.ExecuteNonQuery();                


                //// second insert table
                //SqlCommand cmd2 = new SqlCommand("InsertProductStone", con, trans1);
                //cmd2.CommandType = CommandType.StoredProcedure;
                //cmd2.Parameters.Add(new SqlParameter("@ProductsProductsGroupID", otblProductsProductsGroups.ProductsProductsGroupsID));
                //cmd2.Parameters.Add(new SqlParameter("@ProductID", otblProductsProductsGroups.ProductID));
                //otblProducts.TotalTypeOfStones
                //cmd2.ExecuteNonQuery();



                //// Third insert table
                //SqlCommand cmd3 = new SqlCommand("InsertProductImage", con, trans1);
                //    cmd3.CommandType = CommandType.StoredProcedure;
                //    cmd3.Parameters.Add(new SqlParameter("@ImageID", otblProductsImages.ImageID));
                //    if (otblProductsImages.ImageType.ToString() == "-1" || otblProductsImages.ImageType.ToString() == "")
                //    {
                //        cmd3.Parameters.Add(new SqlParameter("@ImageType", otblProductsImages.ImageType));
                //    }
                //    else
                //    {
                //        cmd3.Parameters.Add(new SqlParameter("@ImageType", otblProductsImages.ImageType));
                //    }


                //    cmd3.Parameters.Add(new SqlParameter("@ProductID", otblProductsImages.ProductID));
                //    cmd3.ExecuteNonQuery();
                

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


        public int RemoveRecord(tblProductsSizes otblProducts)
        {
            int status = -1;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ToString());
            SqlTransaction trans1;

            con.Open();
            trans1 = con.BeginTransaction();
            try
            {

                int Repeat = -2;
                SqlCommand cmd1 = new SqlCommand("InsertProductSizes", con, trans1);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add(new SqlParameter("@ProductID", otblProducts.ProductID));
                cmd1.Parameters.Add(new SqlParameter("@ProductSizeID", otblProducts.ProductSizeID));
                cmd1.Parameters.Add(new SqlParameter("@Sizes", otblProducts.Sizes));
                cmd1.Parameters.Add(new SqlParameter("@14kDefaultWeight", otblProducts.K14DefaultWeight));
                cmd1.Parameters.Add(new SqlParameter("@GoldLabor", otblProducts.GoldLabor));

                cmd1.Parameters.Add(new SqlParameter("@PlatinumLabor", otblProducts.PlatinumLabor));
                cmd1.Parameters.Add(new SqlParameter("@TotalTypeOfStones", otblProducts.TotalTypeOfStones));
                cmd1.Parameters.Add(new SqlParameter("@DefaultLength", otblProducts.DefaultLength));
                cmd1.Parameters.Add(new SqlParameter("@TotalNoOfStones", otblProducts.TotalNoOfStones));

                cmd1.Parameters.Add(new SqlParameter("@StandardSize", otblProducts.StandardSize));
                cmd1.Parameters.Add(new SqlParameter("@Repeat", Repeat));

                cmd1.ExecuteNonQuery();


                // Inserting into the Second Table 
                    tblProductsStone.tblProductsStone otblProductsStone;
                    otblProductsStone = new tblProductsStone.tblProductsStone();
                    tblProductsStone.tblProductsStoneHelper otblProductsStoneHelper;
                    otblProductsStoneHelper = new tblProductsStone.tblProductsStoneHelper();


                otblProductsStone.ProductSizeID = otblProducts.ProductSizeID;

             
                    otblProductsStone.ProductID =otblProducts.ProductID;

                otblProductsStoneHelper.RemoveRecord(otblProductsStone);


                

                //// second insert table
                //SqlCommand cmd2 = new SqlCommand("InsertProductsProductsGroups", con, trans1);
                //cmd2.CommandType = CommandType.StoredProcedure;
                //cmd2.Parameters.Add(new SqlParameter("@ProductsProductsGroupID", otblProductsProductsGroups.ProductsProductsGroupsID));
                //cmd2.Parameters.Add(new SqlParameter("@ProductID", otblProductsProductsGroups.ProductID));
                //cmd2.ExecuteNonQuery();



                //// Third insert table
                //SqlCommand cmd3 = new SqlCommand("InsertProductImage", con, trans1);
                //    cmd3.CommandType = CommandType.StoredProcedure;
                //    cmd3.Parameters.Add(new SqlParameter("@ImageID", otblProductsImages.ImageID));
                //    if (otblProductsImages.ImageType.ToString() == "-1" || otblProductsImages.ImageType.ToString() == "")
                //    {
                //        cmd3.Parameters.Add(new SqlParameter("@ImageType", otblProductsImages.ImageType));
                //    }
                //    else
                //    {
                //        cmd3.Parameters.Add(new SqlParameter("@ImageType", otblProductsImages.ImageType));
                //    }


                //    cmd3.Parameters.Add(new SqlParameter("@ProductID", otblProductsImages.ProductID));
                //    cmd3.ExecuteNonQuery();


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


        //public int InsertRecord(tblproducts.tblProducts otblProducts, tblproductsproductsgroups.tblProductsProductsGroups otblProductsProductsGroups, tblproductsimages.tblProductsImages otblProductsImages, int ans)
        //{
        //    int status = -1;
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ToString());
        //    SqlTransaction trans1;

        //    con.Open();
        //    trans1 = con.BeginTransaction();
        //    try
        //    {
        //        //
        //        SqlCommand cmd1 = new SqlCommand("InsertProductSizes", con, trans1);
        //        cmd1.CommandType = CommandType.StoredProcedure;
        //        cmd1.Parameters.Add(new SqlParameter("@ProductID", otblProducts.ProductID));
        //        cmd1.Parameters.Add(new SqlParameter("@ProductName", otblProducts.ProductName));
        //        cmd1.Parameters.Add(new SqlParameter("@Description", otblProducts.Description));

        //        if (otblProducts.MetalID != null)
        //        {
        //            if (otblProducts.MetalID.ToString() == "-1" || otblProducts.MetalID.ToString() == "")
        //            {
        //            }
        //            else
        //            {
        //                cmd1.Parameters.Add(new SqlParameter("@MetalID", otblProducts.MetalID));
        //            }
        //        }
        //        else
        //        {
        //            //cmd1.Parameters.Add(new SqlParameter("@MetalID", SqlDbType.UniqueIdentifier, 64, null));
        //            cmd1.Parameters.Add(new SqlParameter("@MetalID", DBNull.Value));
        //        }

        //        if (otblProducts.MetalVendorID != null)
        //        {
        //            if (otblProducts.MetalVendorID.ToString() == "-1" || otblProducts.MetalVendorID.ToString() == "")
        //            {
        //                cmd1.Parameters.Add(new SqlParameter("@MetalVendorID", SqlDbType.UniqueIdentifier, 64, null));
        //            }
        //            else
        //            {
        //                cmd1.Parameters.Add(new SqlParameter("@MetalVendorID", otblProducts.MetalVendorID));
        //            }
        //        }
        //        else
        //        {
        //            //cmd1.Parameters.Add(new SqlParameter("@MetalVendorID", SqlDbType.UniqueIdentifier, 64, null));
        //            cmd1.Parameters.Add(new SqlParameter("@MetalVendorID", DBNull.Value));
        //        }

        //        cmd1.Parameters.Add(new SqlParameter("@MetalWeightInGramsForStandardSize", otblProducts.MetalWeightInGramsForStandardSize));
        //        cmd1.Parameters.Add(new SqlParameter("@LaborRateForOneGramOfMetal", otblProducts.LaborRateForOneGramOfMetal));

        //        if (otblProducts.PriceCalculationFormulaID != null)
        //        {
        //            if (otblProducts.PriceCalculationFormulaID.ToString() == "-1" || otblProducts.PriceCalculationFormulaID.ToString() == "")
        //            {
        //                cmd1.Parameters.Add(new SqlParameter("@PriceCalculationFormulaID", SqlDbType.UniqueIdentifier, 64, null));
        //            }
        //            else
        //            {
        //                cmd1.Parameters.Add(new SqlParameter("@PriceCalculationFormulaID", otblProducts.PriceCalculationFormulaID));
        //            }
        //        }
        //        else
        //        {
        //            //cmd1.Parameters.Add(new SqlParameter("@PriceCalculationFormulaID", SqlDbType.UniqueIdentifier, 64, null));
        //            cmd1.Parameters.Add(new SqlParameter("@PriceCalculationFormulaID", DBNull.Value));
        //        }

        //        cmd1.Parameters.Add(new SqlParameter("@FixedPrice", otblProducts.FixedPrice));
        //        cmd1.Parameters.Add(new SqlParameter("@StandardSize", otblProducts.StandardSize));
        //        cmd1.Parameters.Add(new SqlParameter("@MinimumSize", otblProducts.MinimumSize));
        //        cmd1.Parameters.Add(new SqlParameter("@MaximumSize", otblProducts.MaximumSize));
        //        cmd1.Parameters.Add(new SqlParameter("@VisualOrderIndex", otblProducts.VisualOrderIndex));
        //        cmd1.Parameters.Add(new SqlParameter("@AvailableSize", otblProducts.AvailableSize));
        //        cmd1.Parameters.Add(new SqlParameter("@3DAnimationLink", otblProducts.D3AnimationLink));
        //        cmd1.Parameters.Add(new SqlParameter("@VoiceoverLink", otblProducts.VoiceoverLink));

        //        cmd1.ExecuteNonQuery();

        //        SqlCommand cmd2 = new SqlCommand("InsertProductsProductsGroups", con, trans1);
        //        cmd2.CommandType = CommandType.StoredProcedure;
        //        cmd2.Parameters.Add(new SqlParameter("@ProductsProductsGroupID", otblProductsProductsGroups.ProductsProductsGroupsID));
        //        cmd2.Parameters.Add(new SqlParameter("@ProductID", otblProductsProductsGroups.ProductID));
        //        cmd2.ExecuteNonQuery();

        //        if (ans > 0)
        //        {
        //            SqlCommand cmd3 = new SqlCommand("InsertProductImage", con, trans1);
        //            cmd3.CommandType = CommandType.StoredProcedure;
        //            cmd3.Parameters.Add(new SqlParameter("@ImageID", otblProductsImages.ImageID));
        //            if (otblProductsImages.ImageType.ToString() == "-1" || otblProductsImages.ImageType.ToString() == "")
        //            {
        //                cmd3.Parameters.Add(new SqlParameter("@ImageType", otblProductsImages.ImageType));
        //            }
        //            else
        //            {
        //                cmd3.Parameters.Add(new SqlParameter("@ImageType", otblProductsImages.ImageType));
        //            }


        //            cmd3.Parameters.Add(new SqlParameter("@ProductID", otblProductsImages.ProductID));
        //            cmd3.ExecuteNonQuery();
        //        }

        //        trans1.Commit();
        //        status = 0;
        //    }
        //    catch (Exception ex)
        //    {
        //        trans1.Rollback();
        //        string strMsg = ex.Message;
        //    }
        //    finally
        //    {
        //        if (con.State.ToString() == "Open")
        //            con.Close();
        //        ResetAll();
        //    }
        //    return status;
        //}

        public DataSet ReturnProductSizes(string PID, string PSizeID)
        {
            
            if (object.Equals(db, null))
            {
                db = new DataBase();
            }
             param = new SqlParameter[2];
            param[0] = db.MakeInParameter("@ProductsID", PID);
            param[1] = db.MakeInParameter("@ProductsSizeID",  PSizeID);

            db.RunProcedure("P_GetProductSizes", param,out ds);
            return ds;
 
        }
        public DataTable ReturnProductStoneDetails(string PID, string PSizeID)
        {
            DataTable dt = new DataTable();
            if (object.Equals(db, null))
            {
                db = new DataBase();
            }
            param = new SqlParameter[2];
            param[0] = db.MakeInParameter("@ProductsID", PID);
            param[1] = db.MakeInParameter("@ProductsSizeID", PSizeID);

            db.RunProcedure("P_GetProductstoneDetails", param, out ds);
            dt = ds.Tables[0];
            return dt;

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

      
        #endregion


      
        public DataTable ReturnProductStoneDetailsNew(string PID, string PSizeID, string SemiMount)
        {
            DataTable dt = new DataTable();
            if (object.Equals(db, null))
            {
                db = new DataBase();
            }
            param = new SqlParameter[3];
            param[0] = db.MakeInParameter("@ProductsID", PID);
            param[1] = db.MakeInParameter("@ProductsSizeID", PSizeID);

            if (SemiMount != null && SemiMount == "true")
            {
                param[2] = db.MakeInParameter("@hasSemiMount", 1);
            }
            else
            {
                param[2] = db.MakeInParameter("@hasSemiMount", 0);
            }

            db.RunProcedure("P_GetProductstoneDetails", param, out ds);
            dt = ds.Tables[0];
            return dt;

        }
        public DataTable ReturnProductStoneDetailsNew1(string PID, string PSizeID, string SemiMount, decimal RingSize)
        {
            DataTable dt = new DataTable();
            if (object.Equals(db, null))
            {
                db = new DataBase();
            }
            param = new SqlParameter[4];
            param[0] = db.MakeInParameter("@ProductsID", PID);
            param[1] = db.MakeInParameter("@ProductsSizeID", PSizeID);

            if (SemiMount != null && SemiMount == "true")
            {
                param[2] = db.MakeInParameter("@hasSemiMount", 1);
            }
            else
            {
                param[2] = db.MakeInParameter("@hasSemiMount", 0);
            }
            param[3] = db.MakeInParameter("@RingSize", RingSize);
            db.RunProcedure("P_GetProductstoneDetails_NEW", param, out ds);
            dt = ds.Tables[0];
            return dt;

        }

       
        public DataSet ReturnProductSizes(string PID, string PSizeID, string SemiMount)
        {

            if (object.Equals(db, null))
            {
                db = new DataBase();
            }
            param = new SqlParameter[3];
            param[0] = db.MakeInParameter("@ProductsID", PID);
            param[1] = db.MakeInParameter("@ProductsSizeID", PSizeID);
            if (SemiMount != null && SemiMount == "true")
            {
                param[2] = db.MakeInParameter("@hasSemiMount", 1);
            }
            else
            {
                param[2] = db.MakeInParameter("@hasSemiMount", 0);
            }
            db.RunProcedure("P_GetProductSizes", param, out ds);
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
