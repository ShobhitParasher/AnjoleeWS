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
using System.Net;
using System.Text;
using System.IO;



namespace DBComponent
{
    public class CommonFunctions
    {
        private DataBase db;
        SqlParameter[] param;
        DataSet ds;
        public CommonFunctions()
        {          
        }

        public static SqlConnection GetConnection()
        {
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConStr"].ToString());
            return con;
        }
       
        public DataTable ExecuteQueryReturnDataTable(string query)
        {
            SqlConnection con = GetConnection();
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            System.Data.DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        
        public DataSet GetBreadcrubsURL(string ProductID)
        {
            SqlConnection con = GetConnection();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "USP_GetBreadcrubsURLMOWdetails";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@ProductID", ProductID));
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);

            return ds;
        }
        
        public DataSet GetSpecialAnnouncement(string ID)
        {
            SqlConnection con = GetConnection();
            DataSet ds = new DataSet();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "usp_PromtionalMSG";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CategoryID", ID));
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
            }
            return ds;
        }
        public DataSet GetCategoryMetaTag(string ProductGroupID)
        {
            SqlConnection con = GetConnection();
            System.Data.DataTable dt = new DataTable();

            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SP_GetMetaTagdata";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@ProductGroupId", ProductGroupID));
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;


        }
        public DataSet HomepageNotification()
        {
            SqlConnection con = GetConnection();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "usp_HomepageNotification";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);

            return ds;
        }
        public DataSet GetProductSerachByStyleNo(string style)
        {
            try
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
                param[0] = db.MakeInParameter("@Style", SqlDbType.NVarChar, 500, style);
                db.RunProcedure("SP_GetSerachByStyleNumber", param, out ds);
                ResetAll();

            }
            catch (Exception ex)
            {
                string _strError = ex.ToString();
            }
            return ds;
        }

        public DataTable GetCategoryDetail(string ID)
        {
            SqlConnection con = GetConnection();
            System.Data.DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SP_GetCategoryDetail";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id", ID));
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
            }
            return dt;
        }
        
        public DataTable GetBrandingImages(string GroupId)
        {
            SqlConnection con = GetConnection();
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "usp_GetThumbnailBannerImage";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ProductsProductsGroupID", GroupId));
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
            }
            return dt;
        }
        public DataSet GetAllJewelleryCollection(string GroupidId)
        {

            SqlConnection con = GetConnection();
            DataSet dsJewelleryCollection = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "GetJCollections";
            cmd.Parameters.Add(new SqlParameter("@ProductId", GroupidId));
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dsJewelleryCollection);
            return dsJewelleryCollection;

        }
        public DataSet ProductNarrowSearch(string price, string caratweight, string stoneshape, string stonesetting, string JewelleryCollection, string GroupId, string NewArival, string SpecialOffer, string Sortingby, string PageIndex)
        {
            DataSet ds = new DataSet();


            SqlConnection con = GetConnection();
            DataSet dsNarrowSearch = new DataSet();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "P_GetProductThumbnailSearch";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@PRICE", price));
                cmd.Parameters.Add(new SqlParameter("@CARATWEIGHT", caratweight));
                cmd.Parameters.Add(new SqlParameter("@SHAPE", stoneshape));
                cmd.Parameters.Add(new SqlParameter("@STONESETTINGS", stonesetting));
                cmd.Parameters.Add(new SqlParameter("@JewelleryCollection", JewelleryCollection));
                cmd.Parameters.Add(new SqlParameter("@id", GroupId));
                cmd.Parameters.Add(new SqlParameter("@NewArival", NewArival));
                cmd.Parameters.Add(new SqlParameter("@Show_All", SpecialOffer));
                cmd.Parameters.Add(new SqlParameter("@SortingBy", Sortingby));
                cmd.Parameters.Add(new SqlParameter("@startIndex", PageIndex));



                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dsNarrowSearch);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
            }

            return dsNarrowSearch;
        }

        public DataSet ProductThumbnailProductSearch(string EternityCollection,string price, string caratweight, string stoneshape, string stonesetting, string JewelleryCollection, string GroupId, string NewArival, string SpecialOffer, string Sortingby, string PageIndex)
        {
            DataSet ds = new DataSet();


            SqlConnection con = GetConnection();
            DataSet dsNarrowSearch = new DataSet();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "P_GetProductThumbnailSearch_NEW";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@EternityType", EternityCollection));
                cmd.Parameters.Add(new SqlParameter("@PRICE", price));
                cmd.Parameters.Add(new SqlParameter("@CARATWEIGHT", caratweight));
                cmd.Parameters.Add(new SqlParameter("@SHAPE", stoneshape));
                cmd.Parameters.Add(new SqlParameter("@STONESETTINGS", stonesetting));
                cmd.Parameters.Add(new SqlParameter("@JewelleryCollection", JewelleryCollection));
                cmd.Parameters.Add(new SqlParameter("@id", GroupId));
                cmd.Parameters.Add(new SqlParameter("@NewArival", NewArival));
                cmd.Parameters.Add(new SqlParameter("@Show_All", SpecialOffer));
                cmd.Parameters.Add(new SqlParameter("@SortingBy", Sortingby));
                cmd.Parameters.Add(new SqlParameter("@startIndex", PageIndex));



                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dsNarrowSearch);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
            }

            return dsNarrowSearch;
        }

        public DataSet GetClassicBandcollection()
        {
            if (object.Equals(db, null))
            {
                db = new DataBase();
            }
            if (object.Equals(ds, null))
            {
                ds = new DataSet();
            }
            param = new SqlParameter[0];
            db.RunProcedure("usp_ClassicBands", param, out ds);
            ResetAll();
            return ds;
        }

        public DataSet GetDesignerbandcollection()
        {
            if (object.Equals(db, null))
            {
                db = new DataBase();
            }
            if (object.Equals(ds, null))
            {
                ds = new DataSet();
            }
            param = new SqlParameter[0];
            db.RunProcedure("usp_Wedding_Band", param, out ds);
            ResetAll();
            return ds;
        }
        public DataSet GetModernbandcollection()
        {
            if (object.Equals(db, null))
            {
                db = new DataBase();
            }
            if (object.Equals(ds, null))
            {
                ds = new DataSet();
            }
            param = new SqlParameter[0];
            db.RunProcedure("usp_Wedding_BandModern", param, out ds);
            ResetAll();
            return ds;
        }

        public DataSet GetLoosediamondData(string pageIndex, string shape, string cut, string color, string clarity, string polish, string symmetry, string fluorescence, string sort_col, string sort_type, string carat_weight, string price)
        {
            SqlConnection con = GetConnection();
            System.Data.DataSet ds = new DataSet();
            try
            {
                int LastIndex = 50;
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "usp_loosediamonds";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Shape", shape);
                cmd.Parameters.AddWithValue("@Carat", carat_weight);
                cmd.Parameters.AddWithValue("@Price", price);
                cmd.Parameters.AddWithValue("@Cut", cut);
                cmd.Parameters.AddWithValue("@Color", color);
                cmd.Parameters.AddWithValue("@Clarity", clarity);
                cmd.Parameters.AddWithValue("@Polish", polish);
                cmd.Parameters.AddWithValue("@Symmetry", symmetry);
                cmd.Parameters.AddWithValue("@Fluorescence", fluorescence);
                cmd.Parameters.AddWithValue("@PageIndex", pageIndex);
                cmd.Parameters.AddWithValue("@PageSize", LastIndex);
                cmd.Parameters.AddWithValue("@sort_col", sort_col);
                cmd.Parameters.AddWithValue("@sort_type", sort_type);
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                da.Dispose();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
            }
            return ds;
        }

        public DataSet GetLandingPage(string ID)
        {
            SqlConnection con = GetConnection();
            DataSet ds = new DataSet();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "usp_GetBreadcrubsURLMOWdetails_Landing";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Search_Text", ID));
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
            }
            return ds;
        }


        public DataSet LandingData(string GroupId, string NewArival, string SpecialOffer, string Sortingby,string PageIndex)
        {
            DataSet ds = new DataSet();

            SqlConnection con = GetConnection();
            DataSet dsNarrowSearch = new DataSet();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                //cmd.CommandText = "[usp_Landing_Product_Search]";
                cmd.CommandText = "[usp_Landingpage_Detail_VER_1]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ProductsGroupID", GroupId));
                cmd.Parameters.Add(new SqlParameter("@NewArival", NewArival));
                cmd.Parameters.Add(new SqlParameter("@Discount_Flag", SpecialOffer));
                cmd.Parameters.Add(new SqlParameter("@Sort_Order", Sortingby));
                cmd.Parameters.Add(new SqlParameter("@pageIndex", PageIndex));
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dsNarrowSearch);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
            }

            return dsNarrowSearch;
        }
    
        public DataTable GetDiamondStudsEarringProducts(string ProductID, string Metaltype, string MetalValue, string Color, string Clarity)
        {


            SqlConnection con = GetConnection();
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "[usp_Calculate_Minimum_Price_Stud_Earings_New]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ProductID", ProductID));
                cmd.Parameters.Add(new SqlParameter("@MetalType", Metaltype));
                cmd.Parameters.Add(new SqlParameter("@MetalValue", MetalValue));
                cmd.Parameters.Add(new SqlParameter("@Color", Color));
                cmd.Parameters.Add(new SqlParameter("@Clarity", Clarity));                
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
            }

            return dt;
        }

        public DataSet GetLoosediamondSelectedData(string ID)
        {
            SqlConnection con = GetConnection();
            System.Data.DataSet ds = new DataSet();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SP_GetLooseDiamonds";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@DiamondID", ID));
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
            }
            return ds;
        }



        public DataSet GetProductsConfigurations(string ProductID)
        {
            SqlConnection con = GetConnection();
            DataSet ds = new DataSet();

            try
            {

            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "usp_Products_Configurations";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@ProductID", ProductID));
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);


            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
            }
            return ds;
        }


        public string GetProductCertiPrice()
        {
            string CertiPrice = string.Empty;
            SqlConnection con = GetConnection();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SP_GetProductCertiPrice";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                CertiPrice = (cmd.ExecuteScalar()).ToString();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
            }
            return CertiPrice;
        }

        public DataTable GetStaticPagesNew(int PageID)
        {
            SqlConnection con = GetConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "P_GetStaticPagesContentsForAnjolee";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@PageID", PageID));
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            System.Data.DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        
        public DataTable GetRootCategory()
        {
            SqlConnection con = GetConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "P_GetRootCategory";
            cmd.CommandType = CommandType.StoredProcedure;            
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            System.Data.DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;            
        }
        public DataTable GetSubCategory(string itemId)
        {
            SqlConnection con = GetConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "P_GetSubCategory";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@itemId", itemId));
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            System.Data.DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        public int GetUser_Count(string str_AdminUser, string str_VsOptionLink)
        {
            int Usercount;
            SqlConnection con = GetConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "P_Count_Users";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@AdminUser", str_AdminUser));
            cmd.Parameters.Add(new SqlParameter("@VsOptionLink", str_VsOptionLink));
            cmd.Connection = con;

            Usercount = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();
            return Usercount;
        }

       
        public DataTable URLReWriteTitleList(string parentGroupID)
        {
            SqlConnection con = GetConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "P_URLReWriteTitleList";
            cmd.CommandType = CommandType.StoredProcedure;
            if (parentGroupID.Trim() != "")
                cmd.Parameters.Add(new SqlParameter("@parentGroupID", parentGroupID));
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            System.Data.DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            return dt;
        }

      
        public string GetImageNameOnProductID(string productID)
        {
            using (SqlConnection con = GetConnection())
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("P_GetImageNameOnProductID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@productID", productID));
                SqlDataReader dr = cmd.ExecuteReader();
                string imgName = string.Empty;
                while (dr.Read())
                {
                    imgName = dr[0].ToString();
                }
                return imgName;
            }
       
        }

        public DataTable GetColorClarityForProdDetail()
        {
            using (SqlConnection con = GetConnection())
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("P_GetColorClarity", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                System.Data.DataTable dt = new DataTable();
                da.Fill(dt);
                da.Dispose();
                return dt;
            }

        }


        public DataTable GetDefaultSettingForGroups(string productsGroupID)
        {
            SqlConnection con = GetConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "P_GetDefaultSettingForGroups";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@productsGroupID", productsGroupID));
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            System.Data.DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            return dt;
        }

        public DataSet GetStoreEmailDetail()
        {
            SqlConnection con = GetConnection();
            SqlCommand cmd = new SqlCommand();
           
            cmd.CommandText = "P_GetStoreEmailDetail_Anjolee";
            cmd.CommandType = CommandType.StoredProcedure;            
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            System.Data.DataSet ds = new DataSet();
            da.Fill(ds);
            da.Dispose();
            return ds;
        }

      

        public DataTable GetSubSubCategory(string ProductGroupID2)
        {
            SqlConnection con = GetConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "P_GetSubSubCategory";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@ProductGroupID2", ProductGroupID2));
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            System.Data.DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public DataTable GetSweepStakeData()
        {
            SqlConnection con = GetConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "P_GetSweepStakeData";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            System.Data.DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        
        public DataTable FillDivState()
        {
            SqlConnection con = GetConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "P_FillDivState";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            System.Data.DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public string GetMiniShoppingCartImage(string MiniProductID)
        {
            SqlConnection con = GetConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "P_GetMiniShoppingCartImage";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@MiniProductID", MiniProductID));
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            System.Data.DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["ImageName"].ToString();
            }
            else
            {
                return "";
            }
        }

        public static string eur_returnHtml(string rfh_fieldData)
        {
            if (!String.IsNullOrEmpty(rfh_fieldData))
                return rfh_fieldData;
            else
                return "&nbsp;";

           
        }


        public DataTable GetBestSellerData()
        {
            SqlConnection con = GetConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "GetBestSeller";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            System.Data.DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public DataTable GetBestSellerDataForCom(string groupID, string gGroupID, string productID)
        {
            SqlConnection con = GetConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "GetBestSellerForCom";
            cmd.CommandType = CommandType.StoredProcedure;

            if (groupID != "")
                cmd.Parameters.AddWithValue("@groupId", groupID);
            else if (gGroupID != "")
                cmd.Parameters.AddWithValue("@gGroupID", gGroupID);
            else if (productID != "")
                cmd.Parameters.AddWithValue("@productID", productID);

            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            System.Data.DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }


        public DataTable GetProductsForMail(int OrderID)
        {
            SqlConnection con = GetConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "P_GetProductsForMail";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@OrderID", OrderID));
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            System.Data.DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
       

        public DataTable ExecuteQueryReturnDataTableFixedCharges(string ProdID)
        {
            SqlConnection con = GetConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "P_GetFixedCharges";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@ProductID", ProdID));
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            System.Data.DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public DataTable GetStatesThrgProc(int countryID)
        {
            SqlConnection con = GetConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "P_GetState";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@countryID", countryID));
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            System.Data.DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        
        public DataSet ExecuteQueryReturnDataSet(string query)
        {
            SqlConnection con = GetConnection();
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            System.Data.DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        public static  SqlDataReader ExecuteQueryReturnDataReader(string query)
        {
            SqlConnection con = GetConnection();
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return dr;
        }

        public static SqlDataAdapter ExecuteQueryReturnDataAdapter(string query)
        {
            SqlConnection con = GetConnection();
            SqlCommand cmd = new SqlCommand(query, con);            
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            return sda;
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
            catch (Exception Ex)
            {
                string test = Ex.ToString();
                ErrorLog(Ex.Source, Ex.Message, Ex.TargetSite.ToString(), Ex.StackTrace, "ProductsOrdered5655");
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
                cmd.Connection.Close();
            }
            return result;
        }

        public static int GetOrderID(string orderNo)
        {
            DBComponent.CommonFunctions obj = new DBComponent.CommonFunctions();
            string SQL = "SELECT orderID FROM tblOrders WHERE orderNo = '" + orderNo + "'";
            int orderID = int.Parse(obj.ExecuteQueryReturnSingleString(SQL));
            return orderID;
        }



        public int GetOrderPrefixLenghth()
        {
            DBComponent.CommonFunctions obj = new DBComponent.CommonFunctions();
            return int.Parse(obj.ExecuteQueryReturnSingleString("SELECT LEN(orderPrefix) AS Ln FROM tblStore_Checkout;"));
        }
        public string ExecuteScalerReturnSingleString(string query)
        {
            string result = string.Empty;
            SqlConnection con = GetConnection();
            SqlCommand cmd = new SqlCommand(query, con);
            try
            {
                cmd.Connection.Open();
                result = (string)cmd.ExecuteScalar();
                
            }
            catch (Exception ex)
            {
                string test = ex.ToString();
            }
            finally
            {
               
                cmd.Connection.Close();
            }

            return result;
        }
        public static string GetStateCode(int CounteryID, string state)
        {
            DBComponent.CommonFunctions obj = new DBComponent.CommonFunctions();
            string SQL = "SELECT isNULL(Abrivationstate, '') AS Abrivationstate FROM tblCountry_States WHERE stateName = '" + state + "' AND countryID=" + CounteryID;
            string sCode = obj.ExecuteQueryReturnSingleString(SQL);
            return sCode;
        }
        public static string getCountryCode(string Country)
        {
            DBComponent.CommonFunctions obj = new DBComponent.CommonFunctions();
            string SQL = "SELECT countryUPSCode FROM tblCountry_Codes WHERE countryUPSName ='" + Country + "';";
            return obj.ExecuteQueryReturnSingleString(SQL);
        }

        public string GetPageTitle()
        {
            string SQL = "SELECT storePageTitle FROM tblStore_Prefs_Anjolee;";
            string sTemp = ExecuteQueryReturnSingleString(SQL);
            return sTemp;
        }

        public void ListingSession(System.Web.UI.Page CurrentPage)
        {
            try
            {
                if (CurrentPage.Session["PgNoBack"] != null)
                {
                    CurrentPage.Session.Remove("PgNoBack");
                    CurrentPage.Session.Remove("sortingOrder");
                    CurrentPage.Session.Remove("noSort");
                    CurrentPage.Session.Remove("showAl");
                    CurrentPage.Session.Remove("PagedPageno");
                    CurrentPage.Session.Remove("PagedNoofPages");
                    CurrentPage.Session.Remove("PagedNoofPages");
                    CurrentPage.Session.Remove("flag");
                }
            }
            catch (Exception ex)
            {
                string test = ex.ToString();
            }
        }

        public void PopulateVenders(System.Web.UI.WebControls.DropDownList ddlMetalVendors)
        {
            string sqlText = "Select distinct VendorID, VendorName from tblVendor Order By VendorName";
            ddlMetalVendors.DataSource = CommonFunctions.ExecuteQueryReturnDataReader(sqlText);
            ddlMetalVendors.DataBind();
            ddlMetalVendors.Items.Insert(0, new ListItem("--Select Vendor--", "-1"));
        }

        public void PopulateVendor(System.Web.UI.WebControls.DropDownList ddlVender)
        {
            string sqlText = "Select distinct VendorID, VendorName from tblVendor Order By VendorName";
            ddlVender.DataSource = CommonFunctions.ExecuteQueryReturnDataReader(sqlText);
            ddlVender.DataBind();
            ddlVender.Items.Insert(0, new ListItem("--Select Vendor--", "-1"));
        }

        public void PopulateFormulaType(System.Web.UI.WebControls.DropDownList ddlPriceCalcFormula)
        {
            string sqlText = "Select FormulaID, Formula_Name from tbFormulaFixedCharge order by Formula_Name";
            ddlPriceCalcFormula.DataSource = CommonFunctions.ExecuteQueryReturnDataReader(sqlText);
            ddlPriceCalcFormula.DataBind();
            ddlPriceCalcFormula.Items.Insert(0, new ListItem("--Select Formula--", "-1"));
        }

        public void PopulateProductsGroups(System.Web.UI.WebControls.DropDownList ddlProductsGroups)
        {
            ddlProductsGroups.DataSource = CommonFunctions.ExecuteQueryReturnDataReader("SELECT DISTINCT ProductsGroupID, ProductsGroupName FROM ProductsGroups WHERE ParentProductGroupID IS NULL ORDER BY ProductsGroupName");
            ddlProductsGroups.DataBind();
            ddlProductsGroups.Items.Insert(0, new ListItem("--Select Group--", ""));
        }

        public void PopulateProductsStyleNumber(System.Web.UI.WebControls.DropDownList ddlProductsStyleNumber)
        {
            ddlProductsStyleNumber.DataSource = CommonFunctions.ExecuteQueryReturnDataReader("SELECT ProductID,ProductStyleNumber FROM Products WHERE ProductStyleNumber <> '' order by ProductStyleNumber ");
            ddlProductsStyleNumber.DataBind();
            ddlProductsStyleNumber.Items.Insert(0, new ListItem("--Select Style No--", ""));
        }

        public void PopulateCoupon(System.Web.UI.WebControls.DropDownList ddlCoupon)
        {
            ddlCoupon.Items.Clear();
            ddlCoupon.DataSource = CommonFunctions.ExecuteQueryReturnDataReader("SELECT Vouchercode FROM tblVouchers Order by Vouchercode");                        
            ddlCoupon.DataBind();
            ddlCoupon.Items.Insert(0, new ListItem("-- Select Coupon --", ""));
        }

        public void FillMasterMerchant(System.Web.UI.WebControls.DropDownList ddMasterMerchant)
        {
            ddMasterMerchant.Items.Clear();
            ddMasterMerchant.DataSource = CommonFunctions.ExecuteQueryReturnDataReader("SELECT UserID,(SELECT CompanyName from contacts where userid=a.userid) as CompanyName FROM Users a Where mastermerchant=1 order by CompanyName");
            ddMasterMerchant.DataBind();
            ddMasterMerchant.Items.Insert(0, new ListItem("Select Master Merchant", ""));
        }

        public void PopulateProductsSubGroups(System.Web.UI.WebControls.DropDownList ddlProductsGroups, System.Web.UI.WebControls.DropDownList ddlProductSubGroups)
        {
            if (ddlProductsGroups.SelectedIndex > 0)
            {
                ddlProductSubGroups.Items.Clear();
                ddlProductSubGroups.DataSource = CommonFunctions.ExecuteQueryReturnDataReader("Select distinct ProductsGroupID, ProductsGroupName from ProductsGroups  where ParentProductGroupID='" + ddlProductsGroups.SelectedItem.Value + "'");
                ddlProductSubGroups.DataBind();
                if (ddlProductSubGroups.Items.Count == 0)
                    ddlProductSubGroups.Items.Insert(0, new ListItem("-- No Sub Group --", "-1"));
            }
        }

        public void PopulateMetals(System.Web.UI.WebControls.DropDownList ddlMetal)
        {          
            ddlMetal.DataSource = CommonFunctions.ExecuteQueryReturnDataReader("Select distinct MetalID, MetalName from Metals");
            ddlMetal.DataBind();
            ddlMetal.Items.Insert(0, new ListItem("--Select Metal--", "-1"));
        }

        public void PopulateMetalVenders(System.Web.UI.WebControls.DropDownList ddlMetal, System.Web.UI.WebControls.DropDownList ddlMetalVender)
        {
            if (ddlMetal.SelectedIndex > 0)
            {             
                string sqlText = "Select CompanyName, CompanyID from Companies where CompanyID in (Select MetalVendorID from Metals where MetalID='" + ddlMetal.SelectedItem.Value + "')";
                ddlMetalVender.DataSource = CommonFunctions.ExecuteQueryReturnDataReader(sqlText);
                ddlMetalVender.DataBind();
                ddlMetalVender.Items.Insert(0, new ListItem("--Select Vender--", "-1"));
            }
            else
            {
                ddlMetalVender.Items.Clear();
                ddlMetalVender.Items.Insert(0, new ListItem("--Select Vender--", "-1"));
            }
        }

        public void PopulatePriceCalcFormula(System.Web.UI.WebControls.DropDownList ddlPriceCalcFormula)
        {           
            string sqlText = "Select FormulaID, FormulaName from PriceCalculationFormula";
            ddlPriceCalcFormula.DataSource = CommonFunctions.ExecuteQueryReturnDataReader(sqlText);
            ddlPriceCalcFormula.DataBind();
            ddlPriceCalcFormula.Items.Insert(0, new ListItem("--Select Formula--", "-1"));
        }

        public void PopulateColorStoneImages(System.Web.UI.WebControls.DropDownList ddlImages)
        {
            ddlImages.Items.Clear();
            
            string sqlText = "select ImageID,ImageName,substring(ImageName,len(left(ImageName,3)),len(ImageName))as FinalName from images where ImageType='T' and  ImageName <> '' Order By ImageName";
            ddlImages.DataSource = CommonFunctions.ExecuteQueryReturnDataReader(sqlText);
            ddlImages.DataBind();
            ddlImages.Items.Insert(0, new ListItem("--Select Image--", "-1"));
        }

        public void PopulateProductImages(System.Web.UI.WebControls.DropDownList ddlProductImages)
        {
            ddlProductImages.Items.Clear();
            
            string sqlText = "select ImageID,ImageName,substring(ImageName,len(left(ImageName,3)),len(ImageName))as FinalName from images where ImageType='T' and ImageName <> '' Order By ImageName";
            ddlProductImages.DataSource = CommonFunctions.ExecuteQueryReturnDataReader(sqlText);
            ddlProductImages.DataBind();
            ddlProductImages.Items.Insert(0, new ListItem("--Select Image--", "-1"));
        }
        public void PopulateDimaondImages(System.Web.UI.WebControls.DropDownList ddlProductImages)
        {
            ddlProductImages.Items.Clear();
            string sqlText = "SELECT ImageID, SUBSTRING(ImageName, LEN(LEFT(ImageName, 3)), LEN(ImageName)) AS ImageName FROM Images WHERE (ImageType = N'D') AND (ImageName <> '') ORDER BY ImageName";
            ddlProductImages.DataSource = CommonFunctions.ExecuteQueryReturnDataReader(sqlText);
            ddlProductImages.DataBind();
            ddlProductImages.Items.Insert(0, new ListItem("--Select Image--", "-1"));
        }
        public void PopulateProductLargeImages(System.Web.UI.WebControls.DropDownList ddlProductLargeImages)
        {
            ddlProductLargeImages.Items.Clear();
            
            string sqlText = "select ImageID,ImageName, substring(imagename,len(left(imagename,4)),len(imagename))as FinalLargeName from images where ImageType='LW' and ImageName <> '' Order By ImageName";
            ddlProductLargeImages.DataSource = CommonFunctions.ExecuteQueryReturnDataReader(sqlText);
            
            ddlProductLargeImages.DataBind();
            ddlProductLargeImages.Items.Insert(0, new ListItem("--Select Image--", "-1"));
        }
        public void PopulateMediumWhiteGoldImages(System.Web.UI.WebControls.DropDownList ddlMediumWhiteGold)
        {
            ddlMediumWhiteGold.Items.Clear();
            string sqlText = "select ImageID,ImageName, substring(imagename,len(left(imagename,4)),len(imagename))as MediumWhiteGold from images where ImageType='MW' and ImageName <> '' Order By ImageName";
            ddlMediumWhiteGold.DataSource = CommonFunctions.ExecuteQueryReturnDataReader(sqlText);
            ddlMediumWhiteGold.DataBind();
            ddlMediumWhiteGold.Items.Insert(0, new ListItem("--Select Image--", "-1"));
        }
        public void PopulateMediumYellowGoldImages(System.Web.UI.WebControls.DropDownList ddlMediumYellowGold)
        {
            ddlMediumYellowGold.Items.Clear();
            string sqlText = "select ImageID,ImageName, substring(imagename,len(left(imagename,4)),len(imagename))as MediumYellowGold from images where ImageType='MY' and ImageName <> '' Order By ImageName";
            ddlMediumYellowGold.DataSource = CommonFunctions.ExecuteQueryReturnDataReader(sqlText);
            ddlMediumYellowGold.DataBind();
            ddlMediumYellowGold.Items.Insert(0, new ListItem("--Select Image--", "-1"));
        }

        public void PopulateLargeYellowGoldImages(System.Web.UI.WebControls.DropDownList ddlLargeYellowImage)
        {
            ddlLargeYellowImage.Items.Clear();
            string sqlText = "select ImageID,ImageName, substring(imagename,len(left(imagename,4)),len(imagename))as LargeYellowGold from images where ImageType='LY' and ImageName <> '' Order By ImageName";
            ddlLargeYellowImage.DataSource = CommonFunctions.ExecuteQueryReturnDataReader(sqlText);
            ddlLargeYellowImage.DataBind();
            ddlLargeYellowImage.Items.Insert(0, new ListItem("--Select Image--", "-1"));
        }

        public void PopulateCaratWeightImages(System.Web.UI.WebControls.DropDownList ddlCaratWtImage)
        {
            ddlCaratWtImage.Items.Clear();
           
            string sqlText = "select ImageID,ImageName, substring(imagename,len(left(imagename,3)),len(imagename))as CaratWeightImage from images where ImageType='C' and ImageName <> '' Order By ImageName";
            ddlCaratWtImage.DataSource = CommonFunctions.ExecuteQueryReturnDataReader(sqlText);
           
            ddlCaratWtImage.DataBind();
            ddlCaratWtImage.Items.Insert(0, new ListItem("--Select Image--", "-1"));
        }

        public void PopulateMiniThumbnailImages(System.Web.UI.WebControls.DropDownList ddlMiniThumnailImage)
        {
            ddlMiniThumnailImage.Items.Clear();
            string sqlText = "select ImageID,ImageName, substring(imagename,len(left(imagename,4)),len(imagename))as FinalName from images where ImageType='TM' and ImageName <> '' Order By ImageName";
            ddlMiniThumnailImage.DataSource = CommonFunctions.ExecuteQueryReturnDataReader(sqlText);
            ddlMiniThumnailImage.DataBind();
            ddlMiniThumnailImage.Items.Insert(0, new ListItem("--Select Image--", "-1"));
        }

        public void PopulateProductLogoImages(System.Web.UI.WebControls.DropDownList ddProductLogo)
        {
            ddProductLogo.Items.Clear();            
            string sqlText = "select ImageID,ImageName, substring(imagename,len(left(imagename,4)),len(imagename))as ProductLogoImage from images where ImageType='PL' and ImageName <> '' Order By ImageName";
            ddProductLogo.DataSource = CommonFunctions.ExecuteQueryReturnDataReader(sqlText);            
            ddProductLogo.DataBind();
            ddProductLogo.Items.Insert(0, new ListItem("--Select Image--", "-1"));
        }

        public void PopulateBigProductLogoImages(System.Web.UI.WebControls.DropDownList ddBigProductLogo)
        {
            ddBigProductLogo.Items.Clear();
            string sqlText = "select ImageID,ImageName, substring(imagename,len(left(imagename,4)),len(imagename))as BigProductLogoImage from images where ImageType='BL' and ImageName <> '' Order By ImageName";
            ddBigProductLogo.DataSource = CommonFunctions.ExecuteQueryReturnDataReader(sqlText);
            ddBigProductLogo.DataBind();
            ddBigProductLogo.Items.Insert(0, new ListItem("--Select Image--", "-1"));
        }

        public void PopulateProductAdvMaterial(System.Web.UI.WebControls.DropDownList ddlAdvMaterial)
        {
            ddlAdvMaterial.Items.Clear();
            string sqlText = "SELECT AdvertisementID, AdvName  FROM tblAdvMaterial order by AdvName";
            ddlAdvMaterial.DataSource = CommonFunctions.ExecuteQueryReturnDataReader(sqlText);
            ddlAdvMaterial.DataBind();
            ddlAdvMaterial.Items.Insert(0, new ListItem("--Select Adv Material--", "-1"));
        }


        public void SetDropDownList(System.Web.UI.WebControls.DropDownList DropDownList, string ValueToMatch)
        {
          

            if (ValueToMatch == "")
            {
                DropDownList.SelectedIndex = -1;
            }
            else if (ValueToMatch == "0")
            {
                DropDownList.SelectedIndex = -1;
            }
            else
            {
                for (int i = 0; i < DropDownList.Items.Count; i++)
                {
                    DropDownList.SelectedIndex = i;
                    if (DropDownList.SelectedItem.Value == ValueToMatch)
                        break;
                }
            }
        }

        public void PopulateProducts(System.Web.UI.WebControls.DropDownList ddlProducts)
        {
            ddlProducts.Items.Clear();
            ddlProducts.DataSource = CommonFunctions.ExecuteQueryReturnDataReader("Select ProductID, ProductName from [Products]");
            ddlProducts.DataBind();
            ddlProducts.Items.Insert(0, new ListItem("-- Select Product --", "-1"));
        }

        public void PopulateDiamonds(System.Web.UI.WebControls.DropDownList ddlDiamonds)
        {
            ddlDiamonds.Items.Clear();
            ddlDiamonds.DataSource = CommonFunctions.ExecuteQueryReturnDataReader("Select DiamondID, DiamondName from [Diamonds]");
            ddlDiamonds.DataBind();
            ddlDiamonds.Items.Insert(0, new ListItem("-- Select Diamond --", "-1"));
        }

        public void PopulateColorStones(System.Web.UI.WebControls.DropDownList ddlColorStones)
        {
            ddlColorStones.Items.Clear();
            ddlColorStones.DataSource = CommonFunctions.ExecuteQueryReturnDataReader("Select ColorStoneID, ColorStoneName from [ColorStones]");
            ddlColorStones.DataBind();
            ddlColorStones.Items.Insert(0, new ListItem("-- Select Color Stone --", "-1"));
        }

        public void PopulateCuts(System.Web.UI.WebControls.DropDownList ddlCuts)
        {
            ddlCuts.Items.Clear();
            ddlCuts.DataSource = CommonFunctions.ExecuteQueryReturnDataReader("Select distinct CutID, Cut from [Cuts] order by Cut");
            ddlCuts.DataBind();
            ddlCuts.Items.Insert(0, new ListItem("-- Select Cut --", "-1"));
        }

        public void PopulateClarities(System.Web.UI.WebControls.DropDownList ddlClarities)
        {
            ddlClarities.Items.Clear();
            ddlClarities.DataSource = CommonFunctions.ExecuteQueryReturnDataReader("Select  ClarityID, Clarity from [Clarities] order by Clarity");
            ddlClarities.DataBind();
            ddlClarities.Items.Insert(0, new ListItem("-- Select Clarity --", "-1"));
        }

        public void PopulateColors(System.Web.UI.WebControls.DropDownList ddlColors)
        {
            ddlColors.Items.Clear();
            ddlColors.DataSource = CommonFunctions.ExecuteQueryReturnDataReader("Select  ColorID, Color from [Colors] Order by Color");
            ddlColors.DataBind();
            ddlColors.Items.Insert(0, new ListItem("-- Select Color --", "-1"));
        }
        public void PopulateColorStoneColor(System.Web.UI.WebControls.DropDownList ddlColors)
        {
            ddlColors.Items.Clear();
            ddlColors.DataSource = CommonFunctions.ExecuteQueryReturnDataReader("Select distinct ColorStoneColorID, ColorStoneColor from tblColorStone_Color");
            ddlColors.DataBind();
            ddlColors.Items.Insert(0, new ListItem("-- Select Color --", "-1"));
        }

        public void PopulateShapes(System.Web.UI.WebControls.DropDownList ddlShapes)
        {
            ddlShapes.Items.Clear();
            ddlShapes.DataSource = CommonFunctions.ExecuteQueryReturnDataReader("Select  ShapeID, Shape from [Shapes] order by Shape");
            ddlShapes.DataBind();
            ddlShapes.Items.Insert(0, new ListItem("-- Select Shape --", "-1"));
        }
        public void PopulateColorStoneShapes(System.Web.UI.WebControls.DropDownList ddlShapes)
        {
            ddlShapes.Items.Clear();
            ddlShapes.DataSource = CommonFunctions.ExecuteQueryReturnDataReader("Select distinct ColorStoneShapeID, ColorStoneShape from tblColorStone_Shape");
            ddlShapes.DataBind();
            ddlShapes.Items.Insert(0, new ListItem("-- Select Shape --", "-1"));
        }
        public void PopulateColorstoneType(System.Web.UI.WebControls.DropDownList ddlColosStoneType)
        {
            ddlColosStoneType.Items.Clear();
            ddlColosStoneType.DataSource = CommonFunctions.ExecuteQueryReturnDataReader("Select distinct ColorStoneTypeID, ColorStoneType from tblColorStone_Type");
            ddlColosStoneType.DataBind();
            ddlColosStoneType.Items.Insert(0, new ListItem("-- Select Type --", "-1"));
        }

        public void PopulateStoneSettings(System.Web.UI.WebControls.DropDownList ddlStoneSettings)
        {
            ddlStoneSettings.Items.Clear();
            ddlStoneSettings.DataSource = CommonFunctions.ExecuteQueryReturnDataReader("Select StoneSettingID, StoneSettingName from [StoneSettings]");
            ddlStoneSettings.DataBind();
            ddlStoneSettings.Items.Insert(0, new ListItem("-- Select Stone Setting --", "-1"));
        }

        public void PopulateAboutUs(System.Web.UI.WebControls.DropDownList ddlIndexHearAbtUs)
        {
            ddlIndexHearAbtUs.Items.Clear();
            ddlIndexHearAbtUs.DataSource = CommonFunctions.ExecuteQueryReturnDataReader("Select ID,Description from tbl_ManageAboutus order by VisualOrderIndex");
            ddlIndexHearAbtUs.DataBind();
            ddlIndexHearAbtUs.Items.Insert(0, new ListItem("- Select-", "-1"));
        }

        public void PopulateStoneSettingVenders(System.Web.UI.WebControls.DropDownList ddlStoneSettings, System.Web.UI.WebControls.DropDownList ddlStoneSettingVenders)
        {
            ddlStoneSettingVenders.Items.Clear();
            if (ddlStoneSettings.SelectedIndex > 0)
            {
                string sqlText = "Select CompanyID, CompanyName from Companies where CompanyID in (Select distinct VendorID from StoneSettingsVendors where StoneSettingID='" + ddlStoneSettings.SelectedItem.Value + "')";
                ddlStoneSettingVenders.DataSource = CommonFunctions.ExecuteQueryReturnDataReader(sqlText);
                ddlStoneSettingVenders.DataBind();
                ddlStoneSettingVenders.Items.Insert(0, new ListItem("--Select Vender--", "-1"));
            }
            else
            {
                ddlStoneSettingVenders.Items.Clear();
                ddlStoneSettingVenders.Items.Insert(0, new ListItem("--Select Vender--", "-1"));
            }
        }

        public void Fill_DDL_State(System.Web.UI.WebControls.DropDownList oDropDown, int countryID)
        {
            DataTable dtStates = null;
            try
            {
                dtStates = GetStatesThrgProc(countryID);
                oDropDown.DataSource = dtStates;
                oDropDown.DataTextField = "stateName";
                oDropDown.DataValueField = "stateID";
                oDropDown.DataBind();
                oDropDown.Focus();
            }
            catch (Exception ex) { string test = ex.ToString(); }
            finally
            {
                dtStates.Dispose();
            }
            oDropDown.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-Select State-", ""));
        }

        public static void Fill_DDL_Country(System.Web.UI.WebControls.DropDownList oDropDown)
        {
            DBComponent.CommonFunctions obj = new DBComponent.CommonFunctions();
            string sSqlQuery = "SELECT * FROM Countries where countryid in (263,440) ORDER BY countryName desc";

            string ID = "";
            try
            {
                string SQL = "SELECT countryID FROM Countries WHERE countryVisible = 1 AND countryDefault = 1";
                ID = obj.ExecuteQueryReturnSingleString(SQL);
                oDropDown.DataSource = ExecuteQueryReturnDataReader(sSqlQuery);
                oDropDown.DataTextField = "countryName";
                oDropDown.DataValueField = "countryID";
                oDropDown.DataBind();
            }
            catch { }
           
        }



        public static void Fill_DDL_Country_MasterMerchant(System.Web.UI.WebControls.DropDownList oDropDown)
        {
            DBComponent.CommonFunctions obj = new DBComponent.CommonFunctions();
            string sSqlQuery = "SELECT * FROM Countries ORDER BY countryName";
            string ID = "";
            try
            {
                string SQL = "SELECT countryID FROM Countries WHERE countryVisible = 1 AND countryDefault = 1";
                ID = obj.ExecuteQueryReturnSingleString(SQL);
                oDropDown.DataSource = ExecuteQueryReturnDataReader(sSqlQuery);
                oDropDown.DataTextField = "countryName";
                oDropDown.DataValueField = "countryID";
                oDropDown.DataBind();
            }
            catch { }
           
            oDropDown.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-Select Country-", ""));
            
        }


        public void Fill_DDL_City(System.Web.UI.WebControls.DropDownList oDropDown)
        {
            SqlDataReader dr = null;
            string sSqlQuery = "SELECT * FROM tblCity ORDER BY sCityName";
            try
            {
                dr = ExecuteQueryReturnDataReader(sSqlQuery);
                oDropDown.DataSource = dr;
                oDropDown.DataTextField = "sCityCode";
                oDropDown.DataValueField = "sCityName";
                oDropDown.DataBind();
            }
            catch (Exception ex)
            {
                string str = ex.ToString();
            }
            finally
            {
                dr.Close();
            }
            oDropDown.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-Select City-", ""));
        }

        public static void IncrementSoldCountAndValue(System.Web.UI.WebControls.GridView gvProduct)
        {
            DBComponent.CommonFunctions obj = new DBComponent.CommonFunctions();
            
            string productID = string.Empty;
            string qty = string.Empty;
            string price = string.Empty;
            decimal dPrice = 0;
            decimal dSoldValue = 0;
            string sSoldValue = string.Empty;
            string query = string.Empty;
            bool isstock_AutoReduce = false;
            foreach (System.Web.UI.WebControls.GridViewRow dgi in gvProduct.Rows)
            {
                qty = dgi.Cells[1].Text.Trim();
                int iQuantity = int.Parse(qty.Trim());
                price = dgi.Cells[2].Text.Trim().Substring(1);
                dPrice = decimal.Parse(price.Trim());
                dSoldValue = iQuantity * dPrice;
                sSoldValue = dSoldValue.ToString("N");
                Label lblpid = (Label)dgi.Cells[4].FindControl("lblProductId");
                string productId = lblpid.Text.ToString();

                if (productID != string.Empty)
                {
                    if (Convert.ToInt32(productID) > 0)
                    {
                        query = "SELECT stock_AutoReduce FROM tblProducts WHERE productID = " + productID;
                        isstock_AutoReduce = bool.Parse(obj.ExecuteQueryReturnSingleString(query));
                    }
                }
                query = "UPDATE tblProducts SET numberSoldCount = numberSoldCount +" + qty;
                query += ",soldValue = soldValue +" + sSoldValue;
                if (isstock_AutoReduce)
                    query += ", stock_Level = stock_Level -" + qty;

                query += " WHERE productID =" + productID;
                obj.ExecuteQueryReturnNothing(query);
            }
        }
        public string GetProName(string PID)
        {
            string ProName;
            SqlConnection con = GetConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "P_GetProName";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@PID", PID));           
            cmd.Connection = con;

            ProName = (cmd.ExecuteScalar()).ToString();
            con.Close();
            return ProName;
        }
       
        public string GetProID(string PName)
        {
            string UserID = "";
            try
            {
                if (object.Equals(db, null))
                {
                    db = new DBComponent.DataBase();
                }
                if (object.Equals(ds, null))
                {
                    ds = new DataSet();
                }
                param = new SqlParameter[2];
                param[0] = db.MakeInParameter("@PName", SqlDbType.VarChar, 50, PName);
                param[1] = db.MakeOutParameter("@UserID", SqlDbType.UniqueIdentifier, 16);
                db.RunProcedure("sp_GetProID", param);
                UserID = param[1].Value.ToString().Trim();

                return UserID;
            }
            catch (Exception ex)
            {
                string test = ex.ToString();
                
            }
            finally
            {
                ResetAll();
            }
            return UserID;
        }
        private void ResetAll()
        {
            param = null;
            db = null;
        }
     
        public string GetProductCategoryNames(string PID, out string CatName, out string SubCatName)
        {
            string ProName = String.Empty;
            CatName = String.Empty;
            SubCatName = String.Empty;
            SqlDataReader dr = null;
            SqlConnection con = GetConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "P_GetProNameNEW";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@PID", PID));
            cmd.Connection = con;
            try
            {
                cmd.Connection.Open();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.Read())
                {
                    ProName = dr[0].ToString();
                    CatName = dr[1].ToString().Trim();
                    SubCatName = dr[2].ToString().Trim();
                }
            }
            catch (Exception ex)
            {
                string test = ex.ToString();
            }
            finally
            {
                dr.Close();
                cmd.Connection.Close();
            }

            return ProName;
        }
        public string ExecuteQueryReturnBothStrings(string query, out string addlString)
        {
            string result = string.Empty;
            addlString = string.Empty;
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
                    addlString = dr[1].ToString();
                }
            }
            catch (Exception ex)
            {
                string test = ex.ToString();
            }
            finally
            {
                dr.Close();
                cmd.Connection.Close();
            }
            return result;
        }



        public DataSet GetSubCategories(string itemId)
        {
            SqlConnection con = GetConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "P_GetSubCategories";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@itemId", itemId));
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            System.Data.DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }
       

        public DataTable GetSubCategories_New(string itemId)
        {
            SqlConnection con = GetConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "P_GetSubCategories_New";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@itemId", itemId));
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            System.Data.DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }        

        public DataSet SearchProductsbyStyle(string style)
        {
            try
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
                param[0] = db.MakeInParameter("@Style", SqlDbType.NVarChar, 500, style);
                db.RunProcedure("P_GetProductListingBySearchNEW", param, out ds);
                ResetAll();

            }
            catch (Exception ex)
            {
                string _strError = ex.ToString();
            }
            return ds;
        }

              
         public void ErrorLog(string ErrorSource, string ErrorMessage, string TargetSite, string StackTrace, string  ErrorURL)
        {
            SqlConnection con = GetConnection();
          
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SP_ErrorLog";  
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;


            cmd.Parameters.Add(new SqlParameter("@Error_Source", ErrorSource));
            cmd.Parameters.Add(new SqlParameter("@Error_Message", ErrorMessage));
            cmd.Parameters.Add(new SqlParameter("@Operation_Type", TargetSite));
            cmd.Parameters.Add(new SqlParameter("@Application_PageName", StackTrace));
            cmd.Parameters.Add(new SqlParameter("@Stored_Procedure_Name", ErrorURL));
            
            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }


        public DataSet GetRapNetDataShapes(string ProductID, string Weight)
        {
            SqlConnection con = GetConnection();
            System.Data.DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SP_GetRapNetdata_ByWeight_NEW09OCT2012";  
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ProductID", ProductID));
                cmd.Parameters.Add(new SqlParameter("@weight", Weight));
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                da.Dispose();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
            }
            return ds;
        }



        public DataTable GetdiamondstudearringsSPForSpecialPremium(string Clarity, string StoneShapeid, string caratweight, string color, string vendorID)
        {
            SqlConnection con = GetConnection();
            System.Data.DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SP_GetdiamondstudearringsSPForSpecialPremium";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Clarity", Clarity));
                cmd.Parameters.Add(new SqlParameter("@StoneShapeid", StoneShapeid));
                cmd.Parameters.Add(new SqlParameter("@caratweight", caratweight));
                cmd.Parameters.Add(new SqlParameter("@color", color));
                cmd.Parameters.Add(new SqlParameter("@vendorID", vendorID));
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
            }
            return dt;
        }

        public string GetdiamondstudearringsStone_Price(string Clarity, string StoneShapeid, string caratweight, string color, string vendorID)
        {
            string DiamondPrices = string.Empty;
            SqlConnection con = GetConnection();

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SP_GetdiamondstudearringsStone_Price";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Clarity", Clarity));
                cmd.Parameters.Add(new SqlParameter("@StoneShapeid", StoneShapeid));
                cmd.Parameters.Add(new SqlParameter("@caratweight", caratweight));
                cmd.Parameters.Add(new SqlParameter("@color", color));
                cmd.Parameters.Add(new SqlParameter("@vendorID", vendorID));
                cmd.Connection = con;
                DiamondPrices = (cmd.ExecuteScalar()).ToString();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
            }
            return DiamondPrices;
        }

       


        public DataSet GetRapNetDataShapes(string ProductID, string Weight,string Color,string Clarity,string Cut,string Polish,string Symmetry,string Fluorescence)
        {
            SqlConnection con = GetConnection();
            System.Data.DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SP_GetLabCertifieddataSearch";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ProductID", ProductID));
                cmd.Parameters.Add(new SqlParameter("@weight", Weight));
                cmd.Parameters.Add(new SqlParameter("@Color", Color));
                cmd.Parameters.Add(new SqlParameter("@Clarity", Clarity));
                cmd.Parameters.Add(new SqlParameter("@Cut", Cut));
                cmd.Parameters.Add(new SqlParameter("@Polish", Polish));
                cmd.Parameters.Add(new SqlParameter("@Symmetry", Symmetry));
                cmd.Parameters.Add(new SqlParameter("@Fluorescence", Fluorescence));
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                da.Dispose();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
            }
            return ds;
        }
  public DataSet GetRapNetDataShapes_New(string ProductID, string Weight, string Color, string Clarity, string Cut, string Polish, string Symmetry, string Fluorescence, string MinPrice, string MaxPrice)
        {
            SqlConnection con = GetConnection();
            System.Data.DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand();
                
                cmd.CommandText = "SP_GetLabCertifiedDiamondSearch"; 
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ProductID", ProductID));
                cmd.Parameters.Add(new SqlParameter("@weight", Weight));
                cmd.Parameters.Add(new SqlParameter("@Color", Color));
                cmd.Parameters.Add(new SqlParameter("@Clarity", Clarity));
                cmd.Parameters.Add(new SqlParameter("@Cut", Cut));
                cmd.Parameters.Add(new SqlParameter("@Polish", Polish));
                cmd.Parameters.Add(new SqlParameter("@Symmetry", Symmetry));
                cmd.Parameters.Add(new SqlParameter("@Fluorescence", Fluorescence));
                cmd.Parameters.Add(new SqlParameter("@Min_Value", MinPrice));
                cmd.Parameters.Add(new SqlParameter("@Max_Value", MaxPrice));
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                da.Dispose();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
            }
            return ds;
        }
        public DataSet GetProductDetailLengthSizeCal(string ID)
        {
            SqlConnection con = GetConnection();
            System.Data.DataSet ds = new DataSet();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SP_GetProductDetailLengthSizeCal";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id", ID));
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
            }
            return ds;
        }

        public static DataTable GetOrderData(int ID)
        {
            SqlConnection con = GetConnection();
            System.Data.DataSet ds = new DataSet();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "getorderdata";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Orderid", ID));
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
            }
            return ds.Tables[0];
        }

        public DataTable GetProductDetailBindCarat1(string ID)
        {
            SqlConnection con = GetConnection();
            System.Data.DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SP_GetProductDetailBindCarat1";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id", ID));
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
            }
            return dt;
        }

        public DataTable GetProductDetailBindCarat2(string ID)
        {
            SqlConnection con = GetConnection();
            System.Data.DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SP_GetProductDetailBindCarat2";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id", ID));
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
            }
            return dt;
        }

        public DataTable GetProductDetailBindCarat3(string ID)
        {
            SqlConnection con = GetConnection();
            System.Data.DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SP_GetProductDetailBindCarat3";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id", ID));
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
            }
            return dt;
        }
        public DataTable GetProductDetailBindSize(string ID)
        {
            SqlConnection con = GetConnection();
            System.Data.DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SP_GetProductDetailBindSize";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id", ID));
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
            }
            return dt;
        }


        public string GetProductStoneCount(string ID, int stoneType)
        {
            string StoneCount = string.Empty;
            SqlConnection con = GetConnection();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SP_GetProductStoneCount";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id", ID));
                cmd.Parameters.Add(new SqlParameter("@stoneType", stoneType));
                cmd.Connection = con;
                StoneCount = (cmd.ExecuteScalar()).ToString();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
            }
            return StoneCount;
        }

      
        public DataSet CheckEternity(string ProductId)
        {
            SqlConnection con = GetConnection();
            DataSet dsenternity = new DataSet();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "sp_checketernity";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@productID", ProductId));
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dsenternity);

            }
            catch (Exception ex)
            {
                ex.ToString();

            }

            return dsenternity;
        }

        public int CheckEternityStatus(string ProductId)
        {
            SqlConnection con = GetConnection();
            System.Data.DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "sp_checketernity";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@productID", ProductId));



                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            if (Convert.ToBoolean(dt.Rows[0][0].ToString()) == true)
            {
                return 1;
            }
            else
            {
                return 0;
            }


        }


        public DataSet GetRingSizeData(int Condition, string ProductId, string ProductSizeId)
        {
            SqlConnection con = GetConnection();
            DataSet dsEnternityRingSize = new DataSet();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "usp_Get_RingSize_CWCalc_NewLatest";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CONDIATION", Condition));
                cmd.Parameters.Add(new SqlParameter("@productID", ProductId));
                cmd.Parameters.Add(new SqlParameter("@ProductSizeID", ProductSizeId));


                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dsEnternityRingSize);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
            }
            return dsEnternityRingSize;
        }

        public DataSet GetRingSizes(int Condition, string ProductId, string ProductSizeId, string RingSize)
        {

            SqlConnection con = GetConnection();
            DataSet dsEnternityRingSizeNew = new DataSet();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "usp_Get_RingSize_CWCalc_NewLatest";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CONDIATION", Condition));
                cmd.Parameters.Add(new SqlParameter("@productID", ProductId));
                cmd.Parameters.Add(new SqlParameter("@ProductSizeID", ProductSizeId));
                cmd.Parameters.Add(new SqlParameter("@RingSize", Convert.ToDecimal(RingSize)));


                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dsEnternityRingSizeNew);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
            }
            return dsEnternityRingSizeNew;
        }


        public string GetdiamondstudearringsColorstoneprice(string StoneShapeid, string caratweight, string vendorID, string ColorStoneType)
        {
            string Colorstoneprice = string.Empty; ;
            SqlConnection con = GetConnection();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SP_GetdiamondstudearringsColorstoneprice";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@StoneShapeid", StoneShapeid));
                cmd.Parameters.Add(new SqlParameter("@caratweight", caratweight));
                cmd.Parameters.Add(new SqlParameter("@vendorID", vendorID));
                cmd.Parameters.Add(new SqlParameter("@ColorStoneType", ColorStoneType));
                cmd.Connection = con;
                Colorstoneprice = (cmd.ExecuteScalar()).ToString();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
            }
            return Colorstoneprice;
        }

        

        public DataSet Get_SingleRowEternityProducts()
        {
            if (object.Equals(db, null))
            {
                db = new DataBase();
            }
            if (object.Equals(ds, null))
            {
                ds = new DataSet();
            }
            param = new SqlParameter[0];
            db.RunProcedure("sp_EternitySingleRowProductsIphone", param, out ds);
            ResetAll();
            return ds;
        }
        public DataSet Get_DoubleRowEternityProducts()
        {
            if (object.Equals(db, null))
            {
                db = new DataBase();
            }
            if (object.Equals(ds, null))
            {
                ds = new DataSet();
            }
            param = new SqlParameter[0];
            db.RunProcedure("sp_EternityDoubleRowProductsIphone", param, out ds);
            ResetAll();
            return ds;
        }
        public DataSet Get_RapeNetEternityProducts()
        {
            if (object.Equals(db, null))
            {
                db = new DataBase();
            }
            if (object.Equals(ds, null))
            {
                ds = new DataSet();
            }
            param = new SqlParameter[0];
            db.RunProcedure("sp_EternityRapeNetProductsIphone", param, out ds);
            ResetAll();
            return ds;
        }
        public DataSet Get_DiamondGemEternityProducts()
        {
            if (object.Equals(db, null))
            {
                db = new DataBase();
            }
            if (object.Equals(ds, null))
            {
                ds = new DataSet();
            }
            param = new SqlParameter[0];
            db.RunProcedure("sp_EternityDiamondGemProductsIphone", param, out ds);
            ResetAll();
            return ds;
        }

       

        public DataSet GetOrderDetails(string OrderId, string EmailAddress)
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
            param[0] = db.MakeInParameter("@OrderId", SqlDbType.VarChar, 50, OrderId);
            param[1] = db.MakeInParameter("@Email", SqlDbType.VarChar, 50, EmailAddress);
            db.RunProcedure("sp_GetOrderDetails", param, out ds);

            ResetAll();
            return ds;


        }
        public DataSet GetStatus(string orderNo)
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
            param[0] = db.MakeInParameter("@OrderNo", SqlDbType.VarChar, 50, orderNo);
            db.RunProcedure("sp_OrderStatus", param, out ds);

            ResetAll();

            return ds;
        }

        public DataSet GetOrderStatuses()
        {
            if (object.Equals(db, null))
            {
                db = new DataBase();
            }
            if (object.Equals(ds, null))
            {
                ds = new DataSet();
            }
            param = new SqlParameter[0];
            db.RunProcedure("sp_GetOrderStatus", param, out ds);
            ResetAll();
            return ds;

        }
        public int CheckIGIPayment(string OrderId)
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
            param[0] = db.MakeInParameter("@OrderNumber", SqlDbType.VarChar, 50, OrderId);
            db.RunProcedure("sp_CheckIGIPayment", param, out ds);

            ResetAll();
            if (ds.Tables[0].Rows.Count > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public DataTable  SetTimeZone(string DayName, DateTime OrderDate)
        {
            string shipdate = string.Empty;
            string Tshipdate = string.Empty;
            string deliverDate = string.Empty;
            string ShippingStatus, DeliveryStatus;
            string sDate = DayName;
            if (sDate == "Wednesday" || sDate == "Thursday" || sDate == "Friday" || sDate == "Saturday" || sDate == "Tuesday")
            {

                Tshipdate = Convert.ToString(OrderDate.AddDays(6).ToString("dddd, MMMM d"));
            }
            else if ( sDate == "Monday")
            {
                Tshipdate = Convert.ToString(OrderDate.AddDays(4).ToString("dddd, MMMM d"));
            }
            else if (sDate == "Sunday")
            {
                Tshipdate = Convert.ToString(OrderDate.AddDays(5).ToString("dddd, MMMM d"));
            }
            DateTime dt = Convert.ToDateTime(Tshipdate);

            shipdate = dt.ToString("dddd, MMMM d");
            DateTime now = DateTime.Now;
            int Time1 = Convert.ToInt32(OrderDate.AddDays(4).Hour);
            DateTime date = Convert.ToDateTime(DateTime.Now.ToShortTimeString());
            DateTime Date1 = Convert.ToDateTime(Tshipdate);
            int now1 = DateTime.Now.Hour;
            if (((date == Date1) && (DateTime.Now.Hour >= 19)))
            {
                

                string Curntday = DateTime.Now.DayOfWeek.ToString();
                if (Tshipdate.Contains("Saturday"))
                {

                    Tshipdate = Convert.ToDateTime(Tshipdate).AddDays(2).ToString("dddd, MMMM d");
                }
                else if (Tshipdate.Contains("Sunday"))
                {
                    Tshipdate = Convert.ToDateTime(Tshipdate).AddDays(1).ToString("dddd, MMMM d");
                }

                else if (Tshipdate.Contains("Friday"))
                {
                    deliverDate = Convert.ToDateTime(Tshipdate).AddDays(3).ToString("dddd, MMMM d");
                }

                else
                {
                    deliverDate = Convert.ToDateTime(Tshipdate).AddDays(1).ToString("dddd, MMMM d");
                }


                ShippingStatus = Tshipdate;
            DeliveryStatus = deliverDate;

            }
            else if ((date > Date1))
            {
                if (DateTime.Now.Hour >= 19)
                {
                    string Curntday = DateTime.Now.DayOfWeek.ToString();
                    if (Curntday == "Saturday" )
                    {

                        Tshipdate = Convert.ToDateTime(DateTime.Now).AddDays(2).ToString("dddd, MMMM d");
                        deliverDate = Convert.ToDateTime(Tshipdate).AddDays(1).ToString("dddd, MMMM d");
                    }
                    else if (Curntday == "Sunday")
                    {
                        Tshipdate = Convert.ToDateTime(DateTime.Now).AddDays(1).ToString("dddd, MMMM d");
                        deliverDate = Convert.ToDateTime(Tshipdate).AddDays(1).ToString("dddd, MMMM d");
                    }

                    else if (Curntday.Contains("Friday"))
                    {
                        Tshipdate = Convert.ToDateTime(DateTime.Now).ToString("dddd, MMMM d"); ;
                        deliverDate = Convert.ToDateTime(Tshipdate).AddDays(3).ToString("dddd, MMMM d");
                    }
                    else 
                    {
                        Tshipdate = Convert.ToDateTime(DateTime.Now).AddDays(1).ToString("dddd, MMMM d");
                        deliverDate = Convert.ToDateTime(Tshipdate).AddDays(1).ToString("dddd, MMMM d");
                    }

                   
                    

                    ShippingStatus = Tshipdate;
                    DeliveryStatus = deliverDate; ;
                }
                else
                {
                    string Curntday = DateTime.Now.DayOfWeek.ToString();
                    if (Curntday == "Saturday")
                    {

                        Tshipdate = Convert.ToDateTime(DateTime.Now).AddDays(2).ToString("dddd, MMMM d");
                        deliverDate = Convert.ToDateTime(Tshipdate).AddDays(1).ToString("dddd, MMMM d");
                    }
                    else if (Curntday == "Sunday")
                    {
                        Tshipdate = Convert.ToDateTime(DateTime.Now).AddDays(1).ToString("dddd, MMMM d");
                        deliverDate = Convert.ToDateTime(Tshipdate).AddDays(1).ToString("dddd, MMMM d");
                    }
                    else if (Curntday.Contains("Friday"))
                    {
                        Tshipdate = Convert.ToDateTime(DateTime.Now).ToString("dddd, MMMM d"); ;
                        deliverDate = Convert.ToDateTime(Tshipdate).AddDays(3).ToString("dddd, MMMM d");
                    }
                    else
                    {
                        Tshipdate = Convert.ToDateTime(DateTime.Now).AddDays(0).ToString("dddd, MMMM d");
                        deliverDate = Convert.ToDateTime(Tshipdate).AddDays(1).ToString("dddd, MMMM d");
                    }


                    ShippingStatus = Tshipdate;
                    DeliveryStatus = deliverDate;
                }

            }
            else
            {
                string day = Convert.ToDateTime(Tshipdate).DayOfWeek.ToString();
                if (day.Contains("Friday"))
                {
                    deliverDate = Convert.ToDateTime(Tshipdate).AddDays(3).ToString("dddd, MMMM d");

                }
                else
                {
                    deliverDate = Convert.ToDateTime(Tshipdate).AddDays(1).ToString("dddd, MMMM d");

                }
                ShippingStatus = Tshipdate;
                DeliveryStatus = deliverDate;

            
            }
           

            

            DataTable dtSetTimeZone = new DataTable();
            dtSetTimeZone.Columns.Add("ShippingStatus");
            dtSetTimeZone.Columns.Add("DeliveryStatus");
            dtSetTimeZone.TableName = "ShippingDeliveryStatus";
            DataRow _datarows = dtSetTimeZone.NewRow();

            _datarows["ShippingStatus"] = ShippingStatus;
            _datarows["DeliveryStatus"] = DeliveryStatus;


            dtSetTimeZone.Rows.Add(_datarows);

            return dtSetTimeZone;

           
        }

        public DataTable LabelColorChange(int Status)
        {
            string CurrentStatus1 = string.Empty;
            string CurrentStatus2 = string.Empty;
            string CurrentStatus3 = string.Empty;
            string CurrentStatus4 = string.Empty;
            string CurrentStatus5 = string.Empty;
            string CStatus = string.Empty;

            switch (Status)
            {
                case 1:
                    


                    CurrentStatus1 = "Status1" ;
                    CStatus = CurrentStatus1;
                    
                    break;
                case 2:
                   
                    CurrentStatus2 = "Status2";
                    CStatus = CurrentStatus2;
                    break;
                case 3:
                    
                    CurrentStatus3 = "Status3";
                    CStatus = CurrentStatus3;
                    break;
                case 4:
                    
                    CurrentStatus4 = "Status4";
                    CStatus = CurrentStatus4;
                    break;
                case 5:
                   
                    CurrentStatus5 = "Status5";
                    CStatus = CurrentStatus5;
                    break;


            }

            DataTable dtSetTimeZone = new DataTable();
            dtSetTimeZone.Columns.Add("ShippingStatus");
           
            DataRow _datarows = dtSetTimeZone.NewRow();

            _datarows["ShippingStatus"] = CStatus;
           
            dtSetTimeZone.Rows.Add(_datarows);

            return dtSetTimeZone;
        }
        
        public DataSet  GetRelatedProductDetails(string ID)
        {
            SqlConnection con = GetConnection();
            DataSet dsRelProducts = new DataSet();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SP_GetProductRelatedProduct";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id", ID));
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dsRelProducts);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
            }
            return dsRelProducts;
        }


        public DataSet GetRelatedProductPopupDetails(string PName)
        {
            SqlConnection con = GetConnection();
            DataSet dsProductPopup = new DataSet();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SP_GetProductRelatedProductPopupDetailsIPhone";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@PName", PName));
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dsProductPopup);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
            }
            return dsProductPopup;
        }

        public DataTable GetProductDetailImageFor1(string ID)
        {
            SqlConnection con = GetConnection();
            System.Data.DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SP_GetProductDetailImageFor1";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id", ID));
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
            }
            return dt;
        }


        public DataTable GetProductDetailImageFor2(string ID)
        {
            SqlConnection con = GetConnection();
            System.Data.DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SP_GetProductDetailImageFor2";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id", ID));
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
            }
            return dt;
        }
        

        public DataSet ProdcutListingNarrowSearch(string price, string caratweight, string stoneshape, string stonesetting, string JewelleryCollection, string GroupId)
        {
            DataSet ds = new DataSet();
            string startIndex = "1";
            string lastIndex = "500";

            SqlConnection con = GetConnection();
            DataSet dsNarrowSearch = new DataSet();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "P_GetProductListingByNarrowSearch_JCIphone";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@PRICE", price));
                cmd.Parameters.Add(new SqlParameter("@CARATWEIGHT", caratweight));
                cmd.Parameters.Add(new SqlParameter("@SHAPE", stoneshape));
                cmd.Parameters.Add(new SqlParameter("@STONESETTINGS", stonesetting));
                cmd.Parameters.Add(new SqlParameter("@JewelleryCollection", JewelleryCollection));
                cmd.Parameters.Add(new SqlParameter("@id", GroupId));
                cmd.Parameters.Add(new SqlParameter("@startIndex", startIndex));
                cmd.Parameters.Add(new SqlParameter("@lastIndex", lastIndex));
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dsNarrowSearch);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
            }

            return dsNarrowSearch;
        }

        

      

        public DataSet  GetProductEternitySearch(string EternityRing, string price, string caratweight, string stoneshape, string stonesetting)
        {
            DataSet dsEternity = new DataSet();
            try
            {
                SqlConnection con = GetConnection();
               
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_GetProductListingByNarrowSearch_JC_NEW1_Iphone";
                cmd.Parameters.Add(new SqlParameter("@EternityType", EternityRing));
                cmd.Parameters.Add(new SqlParameter("@PRICE", price));
                cmd.Parameters.Add(new SqlParameter("@CARATWEIGHT", caratweight));
                cmd.Parameters.Add(new SqlParameter("@SHAPE", stoneshape));
                cmd.Parameters.Add(new SqlParameter("@STONESETTINGS", stonesetting));
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dsEternity);
               
             
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            return dsEternity;
        }


       


        public string CheckStudsEarringsTypes(string productID, string ProductSizeId)
        {
            using (SqlConnection con = GetConnection())
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("CheckStudsEarringsTypes", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ProductID", productID));
                cmd.Parameters.Add(new SqlParameter("@ProductSizeID", ProductSizeId));
                SqlDataReader dr = cmd.ExecuteReader();
                string imgName = string.Empty;
                while (dr.Read())
                {
                    imgName = dr[0].ToString();
                }
                return imgName;
            }
        }

        public string GetStudsEarringsCartWeightTYPE1(string productID, string ProductSizeId)
        {
            using (SqlConnection con = GetConnection())
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("GetStudsEarringsCartWeightTYPE1", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ProductID", productID));
                cmd.Parameters.Add(new SqlParameter("@ProductSizeID", ProductSizeId));
                SqlDataReader dr = cmd.ExecuteReader();
                string imgName = string.Empty;
                while (dr.Read())
                {
                    imgName = dr[0].ToString();
                }
                return imgName;
            }
        }

        public string GetStudsEarringsCartWeightTYPE2(string productID, string ProductSizeId)
        {
            using (SqlConnection con = GetConnection())
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("GetStudsEarringsCartWeightTYPE2", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ProductID", productID));
                cmd.Parameters.Add(new SqlParameter("@ProductSizeID", ProductSizeId));
                SqlDataReader dr = cmd.ExecuteReader();
                string imgName = string.Empty;
                while (dr.Read())
                {
                    imgName = dr[0].ToString();
                }
                return imgName;
            }
        }

        public DataSet GetRapNetDataShapesNew(string ProductID, string Weight)
        {
            SqlConnection con = GetConnection();
            System.Data.DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SP_GetRapNetdata_ByWeight_NEW30JULY2013";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ProductID", ProductID));
                cmd.Parameters.Add(new SqlParameter("@weight", Weight));
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                da.Dispose();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
            }
            return ds;
        }


        public DataSet GetRapNetDataShapes_Rapnetstuds(string ProductID, string Weight, string Color, string Clarity, string Cut, string Polish, string Symmetry, string Fluorescence)
        {
            SqlConnection con = GetConnection();
            System.Data.DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand();
                
                cmd.CommandText = "SP_GetLabCertifiedDiamondSearch_studs";  
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ProductID", ProductID));
                cmd.Parameters.Add(new SqlParameter("@weight", Weight));
                cmd.Parameters.Add(new SqlParameter("@Color", Color));
                cmd.Parameters.Add(new SqlParameter("@Clarity", Clarity));
                cmd.Parameters.Add(new SqlParameter("@Cut", Cut));
                cmd.Parameters.Add(new SqlParameter("@Polish", Polish));
                cmd.Parameters.Add(new SqlParameter("@Symmetry", Symmetry));
                cmd.Parameters.Add(new SqlParameter("@Fluorescence", Fluorescence));
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                da.Dispose();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
            }
            return ds;
        }


        public DataSet  GetProductDetailShowRanging(string ID)
        {
            SqlConnection con = GetConnection();
            DataSet ds = new DataSet();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SP_GetProductDetailShowRanging";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id", ID));
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
            }
            return ds;
        }

        public DataSet GetEternityRingSize(int Condition, string ProductId, string ProductSizeId)
        {
            SqlConnection con = GetConnection();
            DataSet dsEnternityRingSize = new DataSet();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "usp_Get_RingSize_CWCalc_HTML";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CONDIATION", Condition));
                cmd.Parameters.Add(new SqlParameter("@productID", ProductId));
                cmd.Parameters.Add(new SqlParameter("@ProductSizeID", ProductSizeId));


                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dsEnternityRingSize);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
            }
            return dsEnternityRingSize;
        }


        public string GetCheckOutBtngo1(string vcode)
        {
            string amount = string.Empty;
            SqlConnection con = GetConnection();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SP_GetCheckOutBtngo1";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@vcode", vcode));
                cmd.Connection = con;
                amount = (cmd.ExecuteScalar()).ToString();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
            }
            return amount;
        }

        public DataTable GetCheckOutBtngoNew(string vcode)
        {
            SqlConnection con = GetConnection();
            System.Data.DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SP_GetCheckOutBtngoNew";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@vcode", vcode));
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);


            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
            }
            return dt;
        }

        public DataSet ProdcutListingNarrowSearch_newarrival(string price, string caratweight, string stoneshape, string stonesetting, string JewelleryCollection, string GroupId, string NewArival, string SpecialOffer)
        {
            DataSet ds = new DataSet();
            string startIndex = "1";
            string lastIndex = "500";

            SqlConnection con = GetConnection();
            DataSet dsNarrowSearch = new DataSet();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "P_GetProductListingByNarrowSearch_JCIphoneNEW";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@PRICE", price));
                cmd.Parameters.Add(new SqlParameter("@CARATWEIGHT", caratweight));
                cmd.Parameters.Add(new SqlParameter("@SHAPE", stoneshape));
                cmd.Parameters.Add(new SqlParameter("@STONESETTINGS", stonesetting));
                cmd.Parameters.Add(new SqlParameter("@JewelleryCollection", JewelleryCollection));
                cmd.Parameters.Add(new SqlParameter("@id", GroupId));

                cmd.Parameters.Add(new SqlParameter("@startIndex", startIndex));
                cmd.Parameters.Add(new SqlParameter("@lastIndex", lastIndex));
                cmd.Parameters.Add(new SqlParameter("@NewArival", NewArival));
                cmd.Parameters.Add(new SqlParameter("@Show_All", SpecialOffer));

                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dsNarrowSearch);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
            }

            return dsNarrowSearch;
        }

        public DataSet Getnewarrivalcheck(string ID)
        {
            SqlConnection con = GetConnection();
            DataSet ds = new DataSet();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "newarrivalcheck_iphone";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@p_productsproductsgroupid", ID));
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
            }
            return ds;
        }

        public DataSet Getproductsearch(string ID)
        {
            SqlConnection con = GetConnection();
            DataSet ds = new DataSet();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "usp_SearchProduct_Listing";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ProductsProductsGroupID", ID));
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
            }
            return ds;
        }

        public DataSet GetProductListingSerachByStyleNo(string style)
        {
            try
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
                param[0] = db.MakeInParameter("@Style", SqlDbType.NVarChar, 500, style);
                db.RunProcedure("SP_GetProductListingSerachByStyleNo", param, out ds);
                ResetAll();

            }
            catch (Exception ex)
            {
                string _strError = ex.ToString();
            }
            return ds;
        }


        public DataTable GetMargedata_newarrival(string ID)
        {
            SqlConnection con = GetConnection();
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "usp_Mearge_Data_New";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ProductID", ID));
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
            }
            return dt;
        }


        public DataSet GetCouponDiscountStatus(string ProductStyleNumber, string CouponNumber, decimal gtotal)
        {
            SqlConnection con = GetConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "usp_Verify_Coupon_Discount";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@Prod_Style_Number", ProductStyleNumber));
            cmd.Parameters.Add(new SqlParameter("@Voucher_Code", CouponNumber));
            cmd.Parameters.Add(new SqlParameter("@Grand_Total", gtotal));
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            System.Data.DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        public DataSet GetProductEternitySearch_new(string EternityRing, string price, string caratweight, string stoneshape, string stonesetting, string newarrival, string SpecialOffer)
        {
            DataSet dsEternity = new DataSet();
            try
            {
                SqlConnection con = GetConnection();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_GetProductListingByNarrowSearch_JC_NEW1_eternityipone";
                cmd.Parameters.Add(new SqlParameter("@EternityType", EternityRing));
                cmd.Parameters.Add(new SqlParameter("@PRICE", price));
                cmd.Parameters.Add(new SqlParameter("@CARATWEIGHT", caratweight));
                cmd.Parameters.Add(new SqlParameter("@SHAPE", stoneshape));
                cmd.Parameters.Add(new SqlParameter("@STONESETTINGS", stonesetting));
                cmd.Parameters.Add(new SqlParameter("@NewArival", newarrival));
                cmd.Parameters.Add(new SqlParameter("@Show_All", SpecialOffer));
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dsEternity);


            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            return dsEternity;
        }
        public DataSet Get_SingleRowEternityProducts_new()
        {
            if (object.Equals(db, null))
            {
                db = new DataBase();
            }
            if (object.Equals(ds, null))
            {
                ds = new DataSet();
            }
            param = new SqlParameter[0];
            db.RunProcedure("sp_EternitySingleRowProductsIphone_newarrival", param, out ds);
            ResetAll();
            return ds;
        }
        public DataSet Get_DoubleRowEternityProducts_new()
        {
            if (object.Equals(db, null))
            {
                db = new DataBase();
            }
            if (object.Equals(ds, null))
            {
                ds = new DataSet();
            }
            param = new SqlParameter[0];
            db.RunProcedure("sp_EternityDoubleRowProductsIphone_newarrival", param, out ds);
            ResetAll();
            return ds;
        }
        public DataSet Get_RapeNetEternityProducts_new()
        {
            if (object.Equals(db, null))
            {
                db = new DataBase();
            }
            if (object.Equals(ds, null))
            {
                ds = new DataSet();
            }
            param = new SqlParameter[0];
            db.RunProcedure("sp_EternityrapenetProductsIphone_newarrival", param, out ds);
            ResetAll();
            return ds;
        }
        public DataSet Get_DiamondGemEternityProducts_new()
        {
            if (object.Equals(db, null))
            {
                db = new DataBase();
            }
            if (object.Equals(ds, null))
            {
                ds = new DataSet();
            }
            param = new SqlParameter[0];
            db.RunProcedure("sp_EternityDiamondGemProductsIphone_newarrival", param, out ds);
            ResetAll();
            return ds;
        }

        public DataSet GetNewArrivalcheckforEternity()
        {
            if (object.Equals(db, null))
            {
                db = new DataBase();
            }
            if (object.Equals(ds, null))
            {
                ds = new DataSet();
            }
            param = new SqlParameter[0];
            db.RunProcedure("newarrivalcheck_iphone_Eernity", param, out ds);
            ResetAll();
            return ds;
        }

        public DataSet GetproductsearchDetailsIphone(string ID, string priority)
        {
            SqlConnection con = GetConnection();
            DataSet ds = new DataSet();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "usp_SearchProduct_Listing_Iphone";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ProductsProductsGroupID", ID));
                cmd.Parameters.Add(new SqlParameter("@SortingNo", priority));
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
            }
            return ds;
        }

       
        


        public DataSet ProductNarrowSearch(string price, string caratweight, string stoneshape, string stonesetting, string JewelleryCollection, string GroupId, string NewArival, string SpecialOffer)
        {
            DataSet ds = new DataSet();
            string startIndex = "1";
            string lastIndex = "500";

            SqlConnection con = GetConnection();
            DataSet dsNarrowSearch = new DataSet();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "P_GetProductListingAnjoleeIphone";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@PRICE", price));
                cmd.Parameters.Add(new SqlParameter("@CARATWEIGHT", caratweight));
                cmd.Parameters.Add(new SqlParameter("@SHAPE", stoneshape));
                cmd.Parameters.Add(new SqlParameter("@STONESETTINGS", stonesetting));
                cmd.Parameters.Add(new SqlParameter("@JewelleryCollection", JewelleryCollection));
                cmd.Parameters.Add(new SqlParameter("@id", GroupId));

                cmd.Parameters.Add(new SqlParameter("@startIndex", startIndex));
                cmd.Parameters.Add(new SqlParameter("@lastIndex", lastIndex));
                cmd.Parameters.Add(new SqlParameter("@NewArival", NewArival));
                cmd.Parameters.Add(new SqlParameter("@Show_All", SpecialOffer));

                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dsNarrowSearch);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
            }

            return dsNarrowSearch;
        }

       

        public DataSet GetEnternityProductSearch(string EternityRing, string price, string caratweight, string stoneshape, string stonesetting, string newarrival, string SpecialOffer)
        {
            DataSet dsEternity = new DataSet();
            try
            {
                SqlConnection con = GetConnection();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_GetHD_EternityIPhone";
                cmd.Parameters.Add(new SqlParameter("@EternityType", EternityRing));
                cmd.Parameters.Add(new SqlParameter("@PRICE", price));
                cmd.Parameters.Add(new SqlParameter("@CARATWEIGHT", caratweight));
                cmd.Parameters.Add(new SqlParameter("@SHAPE", stoneshape));
                cmd.Parameters.Add(new SqlParameter("@STONESETTINGS", stonesetting));
                cmd.Parameters.Add(new SqlParameter("@NewArival", newarrival));
                cmd.Parameters.Add(new SqlParameter("@Show_All", SpecialOffer));
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dsEternity);


            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            return dsEternity;
        }

        public DataSet GetProductImages(string ProductID)
        {
            SqlConnection con = GetConnection();
            System.Data.DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SP_GetProductImageURLs";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ProductID", ProductID));
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                da.Dispose();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
            }
            return ds;
        }


        public DataSet GetSingleRowEternityProducts()
        {
            if (object.Equals(db, null))
            {
                db = new DataBase();
            }
            if (object.Equals(ds, null))
            {
                ds = new DataSet();
            }
            param = new SqlParameter[0];
            db.RunProcedure("sp_EternitySingleRowProductsdata", param, out ds);
            ResetAll();
            return ds;
        }
        public DataSet GetDoubleRowEternityProducts()
        {
            if (object.Equals(db, null))
            {
                db = new DataBase();
            }
            if (object.Equals(ds, null))
            {
                ds = new DataSet();
            }
            param = new SqlParameter[0];
            db.RunProcedure("sp_EternityDoubleRowProductsdata", param, out ds);
            ResetAll();
            return ds;
        }
        public DataSet GetRapeNetEternityProducts()
        {
            if (object.Equals(db, null))
            {
                db = new DataBase();
            }
            if (object.Equals(ds, null))
            {
                ds = new DataSet();
            }
            param = new SqlParameter[0];
            db.RunProcedure("sp_EternityrapenetProductsdata", param, out ds);
            ResetAll();
            return ds;
        }
        public DataSet GetDiamondGemEternityProducts()
        {
            if (object.Equals(db, null))
            {
                db = new DataBase();
            }
            if (object.Equals(ds, null))
            {
                ds = new DataSet();
            }
            param = new SqlParameter[0];
            db.RunProcedure("sp_EternityDiamondGemProductsdata", param, out ds);
            ResetAll();
            return ds;
        }
        public DataSet GetProductMergedata(string  ProductID)
        {
            SqlConnection con = GetConnection();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();
           
            cmd.CommandText = "usp_ProductData_Android";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@ProductID", ProductID));
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);

            return ds;
        }

        public DataSet GetEternityProducts()
        {
            if (object.Equals(db, null))
            {
                db = new DataBase();
            }
            if (object.Equals(ds, null))
            {
                ds = new DataSet();
            }
            param = new SqlParameter[0];
            db.RunProcedure("sp_EternityProductAnjoleeIphone", param, out ds);
            ResetAll();
            return ds;
        }

       
        public void InsertDeviceID(string DeviceID)
        {
            SqlConnection con = GetConnection();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SP_InertDeviceID";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.Add(new SqlParameter("@DeviceId", DeviceID.Trim()));
          

            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }

        public void UpdateDeviceIDStatus(string DeviceID)
        {
            SqlConnection con = GetConnection();
            int ViewStatus = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SP_UpdateDeviceViewStatus";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.Add(new SqlParameter("@DeviceId", DeviceID));
            cmd.Parameters.Add(new SqlParameter("@ViewStatus", ViewStatus));

            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }
        public DataSet Pushnotification(string DeviceID)  
        {
            SqlConnection con = GetConnection();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();
           
            cmd.CommandText = "usp_Push_Notification";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@DeviceID", DeviceID));
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);

            return ds;
        }
        public DataSet PushnotificationDetail(string id)  
        {
            SqlConnection con = GetConnection();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();
            
            cmd.CommandText = "usp_Push_Notifications";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@id", id));
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);

            return ds;
        }

        public DataSet ProductPageURL(string id) 
        {
            SqlConnection con = GetConnection();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();          
            cmd.CommandText = "P_GetProductURLMOW";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@PID", id));
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);

            return ds;
        }

        public DataSet ProductListingPageURL(string ProductgroupID) 
        {
            SqlConnection con = GetConnection();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "P_GetProductListingURLMOW";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@PROD_GROUPID", ProductgroupID));
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);

            return ds;
        }


        public DataSet ProductPageFullURL(string Productpageurl) 
        {
            SqlConnection con = GetConnection();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "P_GetProductFullURL";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@Input_String", Productpageurl));
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);

            return ds;
        }


        public DataSet ProductCategoryName(string SubCategoryId)
        {
            SqlConnection con = GetConnection();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SP_GetProductCategoryName";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@SubCategoryId", SubCategoryId));
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);

            return ds;
        }

      

        public DataSet GetProductMetaTag(string ID)
        {
            SqlConnection con = GetConnection();
            System.Data.DataTable dt = new DataTable();
              con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SP_GetProductMetaTag";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id", ID));
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                DataSet ds = new DataSet();
                ds.Tables.Add(dt);         
                return ds;
        }

        public DataSet GetproductsearchHTML5(string ID)
        {
            SqlConnection con = GetConnection();
            DataSet ds = new DataSet();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "usp_SearchProduct_Listing_NEW08Sep2015";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ProductsProductsGroupID", ID));
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
            }
            return ds;
        }

        public string Addtocart(string cartid, string SessionData, string CertificateData)
        {
            try
            {
                int cartSessionIdentity = 0;
                if (object.Equals(db, null))
                {
                    db = new DataBase();
                }
                param = new SqlParameter[4];
                param[0] = db.MakeInParameter("@cartID", cartid);
                param[1] = db.MakeOutParameter("@csid", SqlDbType.Int, 4);
                param[2] = db.MakeInParameter("@ProductSessiondata", SessionData);
                param[3] = db.MakeInParameter("@CertificateData", CertificateData);
                db.RunProcedure("InsertProductIntoCartHTML5", param); 
                cartSessionIdentity = (int)param[1].Value;


                return cartSessionIdentity.ToString();
            }
            catch (Exception ex)
            {
                
                return "1";
            }
        }

        public DataSet GetCartSessionProduct(string ID)
        {
            SqlConnection con = GetConnection();
            System.Data.DataTable dt = new DataTable();
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SP_GetCartSessionProduct";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@CSPID", ID));
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            return ds;
        }

        public string ClearCartSessionProduct(string SessionID)
        {
            try
            {
                ExecuteQueryReturnNothing("DELETE FROM tbl_CartSession_Product WHERE sessionID='" + SessionID + "'");
               return "true";
            }
            catch (Exception ex)
            {             
                return "true";
            }
        }


        public string  UpdateCartSessionProduct(string CSPID, string PID, string ProductData)
        {
            SqlConnection con = GetConnection();           
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SP_UpdateCartSessionProduct";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.Add(new SqlParameter("@CSPID", CSPID));
            cmd.Parameters.Add(new SqlParameter("@ID", PID));
            cmd.Parameters.Add(new SqlParameter("@PRODUCTDATA", ProductData));

            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                return "True";
            }
            catch (Exception ex)
            {
                ex.ToString();
                return "False";
            }
            finally
            {
                con.Close();
                con.Dispose();
            }

             
        }

        public DataSet GetBreadcrubsURLMOW(string ProductID) 
        {
            SqlConnection con = GetConnection();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "P_GetBreadcrubsURLMOW";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@PROD_GROUPID", ProductID));
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);

            return ds;
        }

        public string GetApplyCoupnCode(string vcode,string StyleNumbers)
        {
            string amount = string.Empty;
            SqlConnection con = GetConnection();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "usp_GetApplyCouponCode";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@vcode", vcode));
                cmd.Parameters.Add(new SqlParameter("@ProductStyleNumber", StyleNumbers));
                cmd.Connection = con;
                amount = (cmd.ExecuteScalar()).ToString();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
            }
            return amount;
        }

        public DataSet GetRapNetRingsDataShapes(string ProductID, string Weight, string Color, string Clarity, string Cut, string Polish, string Symmetry, string Fluorescence, string PageIndex)
        {
            SqlConnection con = GetConnection();
            System.Data.DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SP_GetLabCertifieddataSearchRings"; 
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ProductID", ProductID));
                cmd.Parameters.Add(new SqlParameter("@weight", Weight));
                cmd.Parameters.Add(new SqlParameter("@Color", Color));
                cmd.Parameters.Add(new SqlParameter("@Clarity", Clarity));
                cmd.Parameters.Add(new SqlParameter("@Cut", Cut));
                cmd.Parameters.Add(new SqlParameter("@Polish", Polish));
                cmd.Parameters.Add(new SqlParameter("@Symmetry", Symmetry));
                cmd.Parameters.Add(new SqlParameter("@Fluorescence", Fluorescence));
                cmd.Parameters.Add(new SqlParameter("@Page_No", PageIndex));
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                da.Dispose();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
            }
            return ds;
        }


        public DataSet GetRapNetDataShapesEarringstuds(string ProductID, string Weight, string Color, string Clarity, string Cut, string Polish, string Symmetry, string Fluorescence, string PageIndex)
        {
            SqlConnection con = GetConnection();
            System.Data.DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SP_GetLabCertifiedDiamondSearch_Earringstuds"; 
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ProductID", ProductID));
                cmd.Parameters.Add(new SqlParameter("@weight", Weight));
                cmd.Parameters.Add(new SqlParameter("@Color", Color));
                cmd.Parameters.Add(new SqlParameter("@Clarity", Clarity));
                cmd.Parameters.Add(new SqlParameter("@Cut", Cut));
                cmd.Parameters.Add(new SqlParameter("@Polish", Polish));
                cmd.Parameters.Add(new SqlParameter("@Symmetry", Symmetry));
                cmd.Parameters.Add(new SqlParameter("@Fluorescence", Fluorescence));
                cmd.Parameters.Add(new SqlParameter("@Page_No", PageIndex));
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                da.Dispose();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
            }
            return ds;
        }

        public DataSet GetBangleDiamondStatus(string ProductID)
        {

            SqlConnection con = GetConnection();
            System.Data.DataSet ds = new DataSet();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SP_GetBangleDiamondStatus";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@PID", ProductID));
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
            }
            return ds;
        }

        public DataSet GetProductSearchNewHTML5(string ID)
        {
            SqlConnection con = GetConnection();
            DataSet ds = new DataSet();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "usp_SearchProduct_Listing_html5";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ProductsProductsGroupID", ID));
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
            }
            return ds;
        }


        public DataSet GetMOWProductMergedata(string ProductID)
        {
            SqlConnection con = GetConnection();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "usp_ProductData_Html";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@ProductID", ProductID));
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);

            return ds;
        }

        public DataSet GetReturnProductDetails(string OrderNumber, string EmailAddress)
        {
            SqlConnection con = GetConnection();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "usp_ReturnRequest_ByOrderID";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@ORDERID", OrderNumber));
            cmd.Parameters.Add(new SqlParameter("@EMAIL_ID", EmailAddress));
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);

            return ds;
        }


        public DataSet GetReturnReason()
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
            db.RunProcedure("GetReturnReason", null, out ds);
            ResetAll();
            return ds;
        }


        public string InsertReturnOrders(string OrderId, string ProductstyleNumber, string Amount, string ReturnReasonID, string CustomerFeedback, string ReturnStatus)
        {
            if (ReturnReasonID == "0")
            {

                ReturnReasonID = null;

            }

            SqlConnection con = GetConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SP_InsertReturnOrders";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.Add(new SqlParameter("@OrderID", Convert.ToInt32(OrderId)));
            cmd.Parameters.Add(new SqlParameter("@ProductStyleNumber", ProductstyleNumber));
            cmd.Parameters.Add(new SqlParameter("@Amount", Amount));
            cmd.Parameters.Add(new SqlParameter("@ReturnReasonID", ReturnReasonID));
            cmd.Parameters.Add(new SqlParameter("@CustomerFeedback", CustomerFeedback));
            cmd.Parameters.Add(new SqlParameter("@ReturnID", ReturnStatus));
            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                return "true";
            }
            catch (Exception ex)
            {
                ex.ToString();

                
                return "false";
            }
            
        }

        public int InsertNewsletter(string EmailID,string IpAddress,string DateSigned )
        {
            int status = -1;
            try
            {
                if (object.Equals(db, null))
                {
                    db = new DataBase();
                }
                param = new SqlParameter[4];
                param[0] = db.MakeInParameter("@email_address", SqlDbType.VarChar, 200, EmailID);
                param[1] = db.MakeInParameter("@ip_address", SqlDbType.VarChar, 50, IpAddress);
                param[2] = db.MakeInParameter("@date_signed", SqlDbType.VarChar, 50, DateSigned);
                param[3] = db.MakeOutParameter("@Status", SqlDbType.Int, 4);
                db.RunProcedure("P_InsertNewsletter", param);
                status = (int)param[3].Value;
            }
            catch (Exception ex)
            {
               string str = ex.Message;
            }
            
            return status;
        }


        public DataSet GetReturnOrderdetails(string OrderId, string EmailAddress)
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
            param[0] = db.MakeInParameter("@ORDERID", SqlDbType.VarChar, 50, OrderId);
            param[1] = db.MakeInParameter("@EMAIL_ID", SqlDbType.VarChar, 50, EmailAddress);
            db.RunProcedure("usp_ReturnRequestOrder", param, out ds);

            ResetAll();
            return ds;


        }

       
       

       
        public DataSet GetWeddingAutoBuild(string productId, string Mtype, string ClassicWeight, string ProductName)
        {

            SqlConnection con = GetConnection();
            System.Data.DataSet ds = new DataSet();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "usp_AutoBuildWedding";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ProductID", productId));
                cmd.Parameters.Add(new SqlParameter("@Flag", Mtype));
                cmd.Parameters.Add(new SqlParameter("@Width", ClassicWeight));
                cmd.Parameters.Add(new SqlParameter("@Prod_Name", ProductName));   
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
            }
            return ds;
        }
        public DataSet GetWeddingBandCalculation(string ProductID, string MetalType, string RingSizes, string CaratWeight)
        {

            SqlConnection con = GetConnection();
            System.Data.DataSet ds = new DataSet();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "usp_WeddingProduct";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ProductID", ProductID));
                cmd.Parameters.Add(new SqlParameter("@MetalType", MetalType));
                cmd.Parameters.Add(new SqlParameter("@RingSizes", RingSizes));
                cmd.Parameters.Add(new SqlParameter("@Width", CaratWeight));
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
            }
            return ds;
        }

        public DataSet GetWBandCalculation(string ProductID, string MetalType, string RingSizes, string BandWidth, string flag)
        {

            SqlConnection con = GetConnection();
            System.Data.DataSet ds = new DataSet();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "usp_WeddingClassicBand";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ProductID", ProductID));
                cmd.Parameters.Add(new SqlParameter("@Metal_Type", MetalType));
                cmd.Parameters.Add(new SqlParameter("@Width", BandWidth));
                cmd.Parameters.Add(new SqlParameter("@Ring_Sizes", RingSizes));
                cmd.Parameters.Add(new SqlParameter("@Selection", flag));
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
            }
            return ds;
        }


        public DataSet GetProductDetailBindProducts1(string ProductID, string BandWidth)
        {

            SqlConnection con = GetConnection();
            System.Data.DataSet ds = new DataSet();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SP_GetProductDetailBindProducts1_anjolee";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id", ProductID));
                cmd.Parameters.Add(new SqlParameter("@Width", BandWidth));
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
            }
            return ds;
        }
        public DataSet GetVendorFeedCalculation(string ProductID, string MetalType, string BandWidth, string flag)
        {

            SqlConnection con = GetConnection();
            System.Data.DataSet ds = new DataSet();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "usp_VendorFeedDetail";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ProductID", ProductID));
                cmd.Parameters.Add(new SqlParameter("@Metal_Type", MetalType));
                cmd.Parameters.Add(new SqlParameter("@TotalCaratWeight", BandWidth));
                cmd.Parameters.Add(new SqlParameter("@Selection", flag));
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
            }
            return ds;
        }
        public DataSet GetvendorAutoBuild(string productId)
        {

            SqlConnection con = GetConnection();
            System.Data.DataSet ds = new DataSet();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "usp_AutoBuildVendorMOW";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ProductID", productId));
               
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
            }
            return ds;
        }
        public string GetVendorproductstatus(string ProductID)
        {
            string Status = string.Empty;
            SqlConnection con = GetConnection();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "usp_Vendor_Feed";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ProductID", ProductID));
                cmd.Connection = con;
                Status = (cmd.ExecuteScalar()).ToString();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
            }
            return Status;
        }
       

       






      
        public DataSet LooseDiamondThumbnailSearch(string gpuid, string pageIndex, string specialOffer, string Newarrival, string Settingtype, string Stoneshape, string Collection, string carat_weight, string sort_type, string sort_order)
        {
             SqlConnection con = GetConnection();
            System.Data.DataSet ds = new DataSet();
            try
            {
            int pageSize = 0;
            string query = "P_GetProductListingByNarrowSearchAnjoleeLoseDiamond_MOW";
            SqlCommand cmd = new SqlCommand(query);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PRICE", "0,50000");
            cmd.Parameters.AddWithValue("@CARATWEIGHT", carat_weight);
            cmd.Parameters.AddWithValue("@SHAPE", Stoneshape);
            cmd.Parameters.AddWithValue("@STONESETTINGS", Settingtype);
            cmd.Parameters.AddWithValue("@JewelleryCollection", Collection);
            cmd.Parameters.AddWithValue("@id", gpuid);
            cmd.Parameters.AddWithValue("@startIndex", pageIndex);
            cmd.Parameters.AddWithValue("@lastIndex", pageSize);
            cmd.Parameters.AddWithValue("@NewArival", Newarrival);
            cmd.Parameters.AddWithValue("@Show_All", specialOffer);
            cmd.Parameters.AddWithValue("@SortType", sort_type);
            cmd.Parameters.AddWithValue("@SortOrder", sort_order);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);

            da.Dispose();

            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
            }
            return ds;
        }

        public DataSet GetLooseDiamondCaratWeight(string ID, string CaratWeight)
        {
            SqlConnection con = GetConnection();
            System.Data.DataSet ds = new DataSet();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SP_GetProductLooseDiamond";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id", ID));
                cmd.Parameters.Add(new SqlParameter("@Input_Value", CaratWeight));
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
            }
            return ds;
        }

        public DataSet LabCertifiedDiamondSearchEngineOLD(string ProductID, string Weight, string Color, string Clarity, string Cut, string Polish, string Symmetry, string Fluorescence, string MinPrice, string MaxPrice, string SortbyColumns, string Orderby)
        {
            SqlConnection con = GetConnection();
            System.Data.DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SP_GetLabCertifiedDiamondSearch_HTML5"; 
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ProductID", ProductID));
                cmd.Parameters.Add(new SqlParameter("@weight", Weight));
                cmd.Parameters.Add(new SqlParameter("@Color", Color));
                cmd.Parameters.Add(new SqlParameter("@Clarity", Clarity));
                cmd.Parameters.Add(new SqlParameter("@Cut", Cut));
                cmd.Parameters.Add(new SqlParameter("@Polish", Polish));
                cmd.Parameters.Add(new SqlParameter("@Symmetry", Symmetry));
                cmd.Parameters.Add(new SqlParameter("@Fluorescence", Fluorescence));
                cmd.Parameters.Add(new SqlParameter("@Min_Value", MinPrice));
                cmd.Parameters.Add(new SqlParameter("@Max_Value", MaxPrice));
                cmd.Parameters.Add(new SqlParameter("@Sort_Field", SortbyColumns));
                cmd.Parameters.Add(new SqlParameter("@Sort_Order", Orderby));
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                da.Dispose();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
            }
            return ds;
        }

        public DataSet GetRapNetDataShapes_RapnetstudsHTML5(string ProductID, string Weight, string Color, string Clarity, string Cut, string Polish, string Symmetry, string Fluorescence, string SortbyColumns, string OrderBy)
        {
            SqlConnection con = GetConnection();
            System.Data.DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand();              
                cmd.CommandText = "SP_GetLabCertifiedDiamondSearch_studs_HTML5";  
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ProductID", ProductID));
                cmd.Parameters.Add(new SqlParameter("@weight", Weight));
                cmd.Parameters.Add(new SqlParameter("@Color", Color));
                cmd.Parameters.Add(new SqlParameter("@Clarity", Clarity));
                cmd.Parameters.Add(new SqlParameter("@Cut", Cut));
                cmd.Parameters.Add(new SqlParameter("@Polish", Polish));
                cmd.Parameters.Add(new SqlParameter("@Symmetry", Symmetry));
                cmd.Parameters.Add(new SqlParameter("@Fluorescence", Fluorescence));
                cmd.Parameters.Add(new SqlParameter("@Sort_Field", SortbyColumns));
                cmd.Parameters.Add(new SqlParameter("@Sort_Order", OrderBy));
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                da.Dispose();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
            }
            return ds;
        }



        public void GetPayaplInsertProd(string ShoppingCartReturnURL, string PaymentAmount, string shoppingCartItems, string PayPalCoupon, string PayPalCouponAmount,string PayPalCouponDiscountRate, string PaypalProductTitle, string PaypalProductCustom, string TokenID, string PayerId, string SilverPendantStatus, string SilverPendantStylenoStatus, string TotalAmount)
        {

            SqlConnection con = GetConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SP_InsertPaypalProds";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.Add(new SqlParameter("@ShoppingCartReturnURL", ShoppingCartReturnURL));
            cmd.Parameters.Add(new SqlParameter("@PaymentAmount", PaymentAmount));
            cmd.Parameters.Add(new SqlParameter("@shoppingCartItems", shoppingCartItems));
            cmd.Parameters.Add(new SqlParameter("@PayPalCoupon", PayPalCoupon));
            cmd.Parameters.Add(new SqlParameter("@PayPalCouponAmount", PayPalCouponAmount));
            cmd.Parameters.Add(new SqlParameter("@PayPalCouponDiscountRate", PayPalCouponDiscountRate));
            cmd.Parameters.Add(new SqlParameter("@PaypalProductTitle", PaypalProductTitle));
            cmd.Parameters.Add(new SqlParameter("@PaypalProductCustom", PaypalProductCustom));
            cmd.Parameters.Add(new SqlParameter("@TokenID", TokenID));
            cmd.Parameters.Add(new SqlParameter("@PayerId", PayerId));
            cmd.Parameters.Add(new SqlParameter("@SilverPendantStatus", SilverPendantStatus));
            cmd.Parameters.Add(new SqlParameter("@SilverPendantStylenoStatus", SilverPendantStylenoStatus));
            cmd.Parameters.Add(new SqlParameter("@TotalAmount", TotalAmount));
           

            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ex.ToString();
                ErrorLog(ex.Source, ex.Message, ex.TargetSite.ToString(), ex.StackTrace, "GetPayaplInsertProd");
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }
        
        public DataSet GetPayPalProdsDetails(string TokenID)
        {
            SqlConnection con = GetConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SP_GetPaypalProdsDetails";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@TokenID", TokenID));
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            System.Data.DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }


        public void UpdateSilverPendantStatus(string TokenID, string SilverPendantData)
        {
            SqlConnection con = GetConnection();           
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SP_UpdateSilverPendantData";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.Add(new SqlParameter("@TokenID", TokenID));
            cmd.Parameters.Add(new SqlParameter("@SilverPendantData", SilverPendantData));

            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }


        public void UpdateShoppingCartItemsPart1data(string TokenID, string ShoppingCartItems)
        {
            SqlConnection con = GetConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SP_UpdateShoppingCartItemsdata";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.Add(new SqlParameter("@TokenID", TokenID));
            cmd.Parameters.Add(new SqlParameter("@ShoppingCartItems", ShoppingCartItems));

            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ex.ToString();
                ErrorLog(ex.Source, ex.Message, ex.TargetSite.ToString(), ex.StackTrace, "UpdateShoppingCartItemsPart1data");
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }

        public void UpdateShoppingCartItemsPart2data(string TokenID, string ShoppingCartItems)
        {
            SqlConnection con = GetConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SP_UpdateShoppingCartItemsPart2data";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.Add(new SqlParameter("@TokenID", TokenID));
            cmd.Parameters.Add(new SqlParameter("@ShoppingCartItems", ShoppingCartItems));

            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }


        public void GetDeletePaypalProd(string TokenID)
        {
            SqlConnection con = GetConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "usp_Delete_tbl_PaypalProduct";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.Add(new SqlParameter("@TokenID", TokenID));
            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }

        public void UpdatePayPalOrderId(string TokenID, string OrderId)
        {
            SqlConnection con = GetConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SP_UpdatePaypalOrderId";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.Add(new SqlParameter("@TokenID", TokenID));
            cmd.Parameters.Add(new SqlParameter("@Orderid", OrderId));

            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }

        public DataSet GetPayPalOrderID(string TokenID)
        {
            SqlConnection con = GetConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SP_GetPaypalOrderID";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@TokenID", TokenID));
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            System.Data.DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        public DataSet GetRapnetStatus(string PID)
        {
            SqlConnection con = GetConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "GetRapnetStatus";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@PID", PID));
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            System.Data.DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }


        public DataSet GetProductCaratPremium(string ProductSizeId)
        {
            SqlConnection con = GetConnection();
            System.Data.DataSet ds = new DataSet();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SP_ProductCaratPremium";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ProductSizeId", ProductSizeId));
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
            }
            return ds;
        }

        public DataSet GetStaticPages(string PageID)
        {
            SqlConnection con = GetConnection();
            System.Data.DataSet ds = new DataSet();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "P_GetStaticPagesContentsForAnjolee";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@PageID", PageID));
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
            }
            return ds;
        }

        public DataTable GetOrderExportReturnColomn(int orderID, string ProductID, string StoneSize, int Stone_Shape_No, string CaretWeight)
        {
            SqlConnection con = GetConnection();
            SqlCommand cmd = new SqlCommand();
           
            cmd.CommandText = "usp_ExportOrder_ReturnColomn_new";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@orderID", orderID));
            cmd.Parameters.Add(new SqlParameter("@ProductID", ProductID));
            cmd.Parameters.Add(new SqlParameter("@StoneSize", StoneSize));
            cmd.Parameters.Add(new SqlParameter("@Stone_Shape_No", Stone_Shape_No));
            cmd.Parameters.Add(new SqlParameter("@Carat_Weight", CaretWeight));
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            System.Data.DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public DataSet GetOrderExportColomnQuickBase(string From, string To)
        {
            SqlConnection con = GetConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "usp_ExportOrder_QuickBase";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@From", From));
            cmd.Parameters.Add(new SqlParameter("@To", To));
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            System.Data.DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        public DataSet GetWebsiteName(string OrderNo)
        {
            SqlConnection con = GetConnection();
            System.Data.DataSet ds = new DataSet();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "usp_Get_WebsiteName_new";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ORDERID", OrderNo));
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
            }
            return ds;
        }

        public DataTable GetLooseExportReturnColumn(string OrderID, string productID)
        {
            SqlConnection con = GetConnection();
            System.Data.DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "GetLooseExportReturnColumns";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Orderid", OrderID));
                cmd.Parameters.Add(new SqlParameter("@ProductID", productID));
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
            }
            return dt;
        }
        public void SendTOQuickBase(string OrderID)
        {

            try
            {

                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                DataTable dt1 = new DataTable();

                string CaretWeight = string.Empty;
                string LooseDiamondID = string.Empty;

                ds = GetOrderExportColomnQuickBase(OrderID, OrderID);

               
                DataView Dv = new DataView(ds.Tables[1]);
                DataTable FinalTable = Dv.ToTable();
                if (ds.Tables[0].Rows.Count > 0)
                {

                    for (int i = 0; i < Convert.ToInt32(ds.Tables[0].Rows[0][1].ToString()); i++)
                    {
                        int num = i + 1;
                        DataColumn DC1 = new DataColumn();
                        DC1.ColumnName = "Stone " + (num).ToString() + " Type";
                        DataColumn DC2 = new DataColumn();
                        DC2.ColumnName = "Stone " + (num).ToString() + " Shape";
                        DataColumn DC3 = new DataColumn();
                        DC3.ColumnName = "Stone " + (num).ToString() + " MM";
                        DataColumn DC4 = new DataColumn();
                        DC4.ColumnName = "Stone " + (num).ToString() + " Carat";
                        DataColumn DC5 = new DataColumn();
                        DC5.ColumnName = "Stone " + (num).ToString() + " Qty";
                        DataColumn DC6 = new DataColumn();
                        DC6.ColumnName = "Stone " + (num).ToString() + " Setting";
                        DataColumn DC7 = new DataColumn();
                        DC7.ColumnName = "Stone " + (num).ToString() + " Cost";
                        FinalTable.Columns.Add(DC1);
                        FinalTable.Columns.Add(DC2);
                        FinalTable.Columns.Add(DC3);
                        FinalTable.Columns.Add(DC4);
                        FinalTable.Columns.Add(DC5);
                        FinalTable.Columns.Add(DC6);
                        FinalTable.Columns.Add(DC7);


                    }
                    int k = 0;
                    foreach (DataRow dr in FinalTable.Rows)
                    {


                        CaretWeight = dr["Model Carat Weight"].ToString();

                        LooseDiamondID = dr["DiamondID"].ToString();

                        if (CaretWeight != "" || CaretWeight != "0")
                        {
                            if (dr["StoneMM"].ToString() != "")
                            {
                                string[] StoneMM = dr["StoneMM"].ToString().Split(',');
                                if (StoneMM.Length > 1)
                                {
                                  
                                    for (int j = 0; j < StoneMM.Length; j++)
                                    {
                                        string[] MM = StoneMM[j].ToString().Split('X');
                                        if (MM.Length > 2)
                                        {

                                            MM[1] = MM[1].ToString() + "X" + MM[2].ToString();

                                        }

                                        dt1 = GetOrderExportReturnColomn(Convert.ToInt32(dr.ItemArray[0].ToString()), dr.ItemArray[1].ToString(), MM[1].ToString(), j + 1, CaretWeight);

                                        if (dt1.Rows.Count > 1 && j + 1 == 2)
                                        {

                                            k = 1;
                                            dr["Stone " + (j + 1).ToString() + " Type"] = dt1.Rows[k][0].ToString();
                                            dr["Stone " + (j + 1).ToString() + " Shape"] = dt1.Rows[k][1].ToString();
                                            dr["Stone " + (j + 1).ToString() + " MM"] = dt1.Rows[k][2].ToString();
                                            dr["Stone " + (j + 1).ToString() + " Carat"] = dt1.Rows[k][3];
                                            dr["Stone " + (j + 1).ToString() + " Qty"] = MM[0].ToString();
                                            dr["Stone " + (j + 1).ToString() + " Setting"] = dt1.Rows[k][5].ToString();
                                            dr["Stone " + (j + 1).ToString() + " Cost"] = dt1.Rows[k][6].ToString();
                                            k++;
                                        }
                                        else
                                        {

                                            if (k == 1)
                                            {
                                                dr["Stone " + (j + 1).ToString() + " Type"] = dt1.Rows[k][0].ToString();
                                                dr["Stone " + (j + 1).ToString() + " Shape"] = dt1.Rows[k][1].ToString();
                                                dr["Stone " + (j + 1).ToString() + " MM"] = dt1.Rows[k][2].ToString();
                                                dr["Stone " + (j + 1).ToString() + " Carat"] = dt1.Rows[k][3];
                                                dr["Stone " + (j + 1).ToString() + " Qty"] = MM[0].ToString();
                                                dr["Stone " + (j + 1).ToString() + " Setting"] = dt1.Rows[k][5].ToString();
                                                dr["Stone " + (j + 1).ToString() + " Cost"] = dt1.Rows[k][6].ToString();
                                            }
                                            else
                                            {

                                                dr["Stone " + (j + 1).ToString() + " Type"] = dt1.Rows[0][0].ToString();
                                                dr["Stone " + (j + 1).ToString() + " Shape"] = dt1.Rows[0][1].ToString();
                                                dr["Stone " + (j + 1).ToString() + " MM"] = dt1.Rows[0][2].ToString();
                                                dr["Stone " + (j + 1).ToString() + " Carat"] = dt1.Rows[0][3];
                                                dr["Stone " + (j + 1).ToString() + " Qty"] = dt1.Rows[0][4].ToString();
                                                dr["Stone " + (j + 1).ToString() + " Setting"] = dt1.Rows[0][5].ToString();
                                                dr["Stone " + (j + 1).ToString() + " Cost"] = dt1.Rows[0][6].ToString();
                                            }

                                        }


                                    }
                                   
                                    FinalTable.AcceptChanges();

                                }
                                else
                                {

                                    string[] MM = StoneMM[0].ToString().Split('X');
                                    if (MM.Length > 2)
                                    {

                                        MM[1] = MM[1].ToString() + "X" + MM[2].ToString();

                                        dt1 = GetOrderExportReturnColomn(Convert.ToInt32(dr.ItemArray[0].ToString()), dr.ItemArray[1].ToString(), MM[1].ToString(), 1, CaretWeight);

                                    }
                                    else
                                    {

                                        dt1 = GetOrderExportReturnColomn(Convert.ToInt32(dr.ItemArray[0].ToString()), dr.ItemArray[1].ToString(), MM[1].ToString(), 1, CaretWeight);

                                    }

                                    
                                    DataRow dr1 = dt.NewRow();
                                    dr["Stone " + (1).ToString() + " Type"] = dt1.Rows[0][0].ToString();
                                    dr["Stone " + (1).ToString() + " Shape"] = dt1.Rows[0][1].ToString();
                                    dr["Stone " + (1).ToString() + " MM"] = dt1.Rows[0][2].ToString();
                                    dr["Stone " + (1).ToString() + " Carat"] = dt1.Rows[0][3].ToString();
                                   
                                    dr["Stone " + (1).ToString() + " Qty"] = dt1.Rows[0][4].ToString();
                                    dr["Stone " + (1).ToString() + " Setting"] = dt1.Rows[0][5].ToString();
                                    dr["Stone " + (1).ToString() + " Cost"] = dt1.Rows[0][6].ToString();
                              
                                    FinalTable.AcceptChanges();
                                }
                            }
                        }
                        else if (LooseDiamondID != "")
                        {
                            dt1 = GetLooseExportReturnColumn(dr.ItemArray[0].ToString(), dr.ItemArray[1].ToString());

                            DataRow dr1 = dt.NewRow();
                            dr["Stone " + (1).ToString() + " Type"] = dt1.Rows[0][0].ToString();
                            dr["Stone " + (1).ToString() + " Shape"] = dt1.Rows[0][1].ToString();
                            dr["Stone " + (1).ToString() + " MM"] = dt1.Rows[0][2].ToString();
                            dr["Stone " + (1).ToString() + " Carat"] = dt1.Rows[0][4].ToString();
                            dr["Stone " + (1).ToString() + " Qty"] = dt1.Rows[0][3].ToString();
                            dr["Stone " + (1).ToString() + " Setting"] = "";
                            dr["Stone " + (1).ToString() + " Cost"] = dt1.Rows[0][6].ToString();
                          
                            FinalTable.AcceptChanges();
                        }
                    }

                }
                string GetWebName = string.Empty;
                string OrderPlatform = string.Empty;
                DataSet DsWebName = GetWebsiteName(FinalTable.Rows[0]["Order number"].ToString());
                if (DsWebName.Tables[0].Rows.Count > 0)
                {
                    GetWebName = DsWebName.Tables[0].Rows[0][0].ToString();
                    OrderPlatform = DsWebName.Tables[0].Rows[0][1].ToString();
                }


                for (int QB = 0; QB < Convert.ToInt32(FinalTable.Rows.Count); QB++)
                {

                    WebRequest request;
                    if (FinalTable.Columns.Contains("Stone 18 Type"))
                    {

                        request = WebRequest.Create("https://nirgolan.quickbase.com/db/bmimrj5vm?a=API_AddRecord" + "&_fid_6=" + FinalTable.Rows[QB]["Order Date"].ToString() + "&_fid_7=" + FinalTable.Rows[QB]["Order number"].ToString() + "&_fid_8=" + FinalTable.Rows[QB]["Amount"].ToString() + "&_fid_9=" + FinalTable.Rows[QB]["Transaction"].ToString() + "&_fid_10=" + FinalTable.Rows[QB]["Payment Method"].ToString() + "&_fid_11=" + FinalTable.Rows[QB]["Amount Before Tax"].ToString() + "&_fid_12=" + FinalTable.Rows[QB]["Tax"].ToString() + "&_fid_13=" + FinalTable.Rows[QB]["Shipping City"].ToString() + "&_fid_14=" + FinalTable.Rows[QB]["Shipping Zip"].ToString() + "&_fid_15=" + FinalTable.Rows[QB]["ShippingService"].ToString() + "&_fid_16=" + FinalTable.Rows[QB]["Customer'sAccounts"].ToString() + "&_fid_17=" + FinalTable.Rows[QB]["Style No"].ToString() + "&_fid_18=" + FinalTable.Rows[QB]["Diamond Quality"].ToString() + "&_fid_19=" + FinalTable.Rows[QB]["Order Quantity"].ToString() + "&_fid_20=" + FinalTable.Rows[QB]["Gram Weight"].ToString() + "&_fid_21=" + FinalTable.Rows[QB]["Metal Karat"].ToString() + "&_fid_22=" + FinalTable.Rows[QB]["Metal Color"].ToString() + "&_fid_23=" + FinalTable.Rows[QB]["Labor Rate"].ToString() + "&_fid_24=" + FinalTable.Rows[QB]["Coupon"].ToString() + "&_fid_25=" + FinalTable.Rows[QB]["IGI"].ToString() + "&_fid_26=" + FinalTable.Rows[QB]["SilverReplicaText"].ToString() + "&_fid_27=" + FinalTable.Rows[QB]["Silver Pendant"].ToString() + "&_fid_28=" + FinalTable.Rows[QB]["Charm/Engraving"].ToString() + "&_fid_29=" + FinalTable.Rows[QB]["EngravingType"].ToString() + "&_fid_30=" + FinalTable.Rows[QB]["EngravingText"].ToString() + "&_fid_31=" + FinalTable.Rows[QB]["Rapnet Diamond"].ToString() + "&_fid_32=" + FinalTable.Rows[QB]["Anjolee Diamond"].ToString() + "&_fid_33=" + FinalTable.Rows[QB]["Customer Name"].ToString() + "&_fid_34=" + FinalTable.Rows[QB]["Customer email"].ToString() + "&_fid_35=" + FinalTable.Rows[QB]["Billing Phone Number"].ToString() + "&_fid_36=" + FinalTable.Rows[QB]["Bill/Ship Match"].ToString() + "&_fid_37=" + FinalTable.Rows[QB]["Payment Option"].ToString() + "&_fid_38=" + FinalTable.Rows[QB]["Shipping Name"].ToString() + "&_fid_39=" + FinalTable.Rows[QB]["Shipping Address1"].ToString() + "&_fid_40=" + FinalTable.Rows[QB]["Shipping Address2"].ToString() + "&_fid_41=" + FinalTable.Rows[QB]["Shipping Apt/Unit No"].ToString() + "&_fid_42=" + FinalTable.Rows[QB]["Shipping State"].ToString() + "&_fid_43=" + FinalTable.Rows[QB]["Shipping Country"].ToString() + "&_fid_44=" + FinalTable.Rows[QB]["Shipping Phone"].ToString() + "&_fid_45=" + FinalTable.Rows[QB]["Item Length/Ring Size"].ToString() + "&_fid_46=" + FinalTable.Rows[QB]["BandMetalType"].ToString() + "&_fid_47=" + FinalTable.Rows[QB]["VendorChildSku"].ToString() + "&_fid_48=" + FinalTable.Rows[QB]["Model Carat Weight"].ToString() + "&_fid_49=" + FinalTable.Rows[QB]["Effective Carat Weight"].ToString() + "&_fid_50=" + FinalTable.Rows[QB]["Vendor Metal Type"].ToString() + "&_fid_51=" + FinalTable.Rows[QB]["Stone Information Quality"].ToString() + "&_fid_52=" + FinalTable.Rows[QB]["Vendor Feed Price"].ToString() + "&_fid_53=" + FinalTable.Rows[QB]["Cut"].ToString() + "&_fid_54=" + FinalTable.Rows[QB]["Color"].ToString() + "&_fid_55=" + FinalTable.Rows[QB]["Clarity"].ToString() + "&_fid_56=" + FinalTable.Rows[QB]["Depth"].ToString() + "&_fid_57=" + FinalTable.Rows[QB]["Table"].ToString() + "&_fid_58=" + FinalTable.Rows[QB]["Girdle"].ToString() + "&_fid_59=" + FinalTable.Rows[QB]["Symmetry"].ToString() + "&_fid_60=" + FinalTable.Rows[QB]["Polish"].ToString() + "&_fid_61=" + FinalTable.Rows[QB]["CuletSize"].ToString() + "&_fid_62=" + FinalTable.Rows[QB]["FluorescenceIntensity"].ToString() + "&_fid_63=" + FinalTable.Rows[QB]["Lab"].ToString() + "&_fid_64=" + FinalTable.Rows[QB]["DiamondID"].ToString() + "&_fid_65=" + FinalTable.Rows[QB]["Stone 1 Type"].ToString() + "&_fid_66=" + FinalTable.Rows[QB]["Stone 1 Shape"].ToString() + "&_fid_67=" + FinalTable.Rows[QB]["Stone 1 MM"].ToString() + "&_fid_68=" + FinalTable.Rows[QB]["Stone 1 Carat"].ToString() + "&_fid_69=" + FinalTable.Rows[QB]["Stone 1 Qty"].ToString() + "&_fid_70=" + FinalTable.Rows[QB]["Stone 2 Setting"].ToString() + "&_fid_71=" + FinalTable.Rows[QB]["Stone 2 Type"].ToString() + "&_fid_72=" + FinalTable.Rows[QB]["Stone 2 Shape"].ToString() + "&_fid_73=" + FinalTable.Rows[QB]["Stone 2 MM"].ToString() + "&_fid_74=" + FinalTable.Rows[QB]["Stone 2 Carat"].ToString() + "&_fid_75=" + FinalTable.Rows[QB]["Stone 2 Qty"].ToString() + "&_fid_76=" + FinalTable.Rows[QB]["Stone 2 Setting"].ToString() + "&_fid_78=" + FinalTable.Rows[QB]["Stone 3 Type"].ToString() + "&_fid_79=" + FinalTable.Rows[QB]["Stone 3 Shape"].ToString() + "&_fid_80=" + FinalTable.Rows[QB]["Stone 3 MM"].ToString() + "&_fid_81=" + FinalTable.Rows[QB]["Stone 3 Carat"].ToString() + "&_fid_82=" + FinalTable.Rows[QB]["Stone 3 Qty"].ToString() + "&_fid_83=" + FinalTable.Rows[QB]["Stone 3 Setting"].ToString() + "&_fid_84=" + FinalTable.Rows[QB]["Stone 4 Type"].ToString() + "&_fid_85=" + FinalTable.Rows[QB]["Stone 4 Shape"].ToString() + "&_fid_86=" + FinalTable.Rows[QB]["Stone 4 MM"].ToString() + "&_fid_87=" + FinalTable.Rows[QB]["Stone 4 Carat"].ToString() + "&_fid_88=" + FinalTable.Rows[QB]["Stone 4 Qty"].ToString() + "&_fid_89=" + FinalTable.Rows[QB]["Stone 4 Setting"].ToString() + "&_fid_90=" + FinalTable.Rows[QB]["Stone 5 Type"].ToString() + "&_fid_91=" + FinalTable.Rows[QB]["Stone 5 Shape"].ToString() + "&_fid_92=" + FinalTable.Rows[QB]["Stone 5 MM"].ToString() + "&_fid_93=" + FinalTable.Rows[QB]["Stone 5 Carat"].ToString() + "&_fid_94=" + FinalTable.Rows[QB]["Stone 5 Qty"].ToString() + "&_fid_95=" + FinalTable.Rows[QB]["Stone 5 Setting"].ToString() + "&_fid_96=" + FinalTable.Rows[QB]["Stone 6 Type"].ToString() + "&_fid_97=" + FinalTable.Rows[QB]["Stone 6 Shape"].ToString() + "&_fid_98=" + FinalTable.Rows[QB]["Stone 6 MM"].ToString() + "&_fid_99=" + FinalTable.Rows[QB]["Stone 6 Carat"].ToString() + "&_fid_100=" + FinalTable.Rows[QB]["Stone 6 Qty"].ToString() + "&_fid_101=" + FinalTable.Rows[QB]["Stone 6 Setting"].ToString() + "&_fid_102=" + FinalTable.Rows[QB]["Stone 7 Type"].ToString() + "&_fid_103=" + FinalTable.Rows[QB]["Stone 7 Shape"].ToString() + "&_fid_104=" + FinalTable.Rows[QB]["Stone 7 MM"].ToString() + "&_fid_105=" + FinalTable.Rows[QB]["Stone 7 Carat"].ToString() + "&_fid_106=" + FinalTable.Rows[QB]["Stone 7 Qty"].ToString() + "&_fid_107=" + FinalTable.Rows[QB]["Stone 7 Setting"].ToString() + "&_fid_108=" + FinalTable.Rows[QB]["Stone 8 Type"].ToString() + "&_fid_109=" + FinalTable.Rows[QB]["Stone 8 Shape"].ToString() + "&_fid_110=" + FinalTable.Rows[QB]["Stone 8 MM"].ToString() + "&_fid_111=" + FinalTable.Rows[QB]["Stone 8 Carat"].ToString() + "&_fid_112=" + FinalTable.Rows[QB]["Stone 8 Qty"].ToString() + "&_fid_113=" + FinalTable.Rows[QB]["Stone 8 Setting"].ToString() + "&_fid_114=" + FinalTable.Rows[QB]["Stone 9 Type"].ToString() + "&_fid_115=" + FinalTable.Rows[QB]["Stone 9 Shape"].ToString() + "&_fid_116=" + FinalTable.Rows[QB]["Stone 9 MM"].ToString() + "&_fid_117=" + FinalTable.Rows[QB]["Stone 9 Carat"].ToString() + "&_fid_118=" + FinalTable.Rows[QB]["Stone 9 Qty"].ToString() + "&_fid_119=" + FinalTable.Rows[QB]["Stone 9 Setting"].ToString() + "&_fid_120=" + FinalTable.Rows[QB]["Stone 10 Type"].ToString() + "&_fid_121=" + FinalTable.Rows[QB]["Stone 10 Shape"].ToString() + "&_fid_122=" + FinalTable.Rows[QB]["Stone 10 MM"].ToString() + "&_fid_123=" + FinalTable.Rows[QB]["Stone 10 Carat"].ToString() + "&_fid_124=" + FinalTable.Rows[QB]["Stone 10 Qty"].ToString() + "&_fid_125=" + FinalTable.Rows[QB]["Stone 10 Setting"].ToString() + "&_fid_126=" + FinalTable.Rows[QB]["Stone 11 Type"].ToString() + "&_fid_127=" + FinalTable.Rows[QB]["Stone 11 Shape"].ToString() + "&_fid_128=" + FinalTable.Rows[QB]["Stone 11 MM"].ToString() + "&_fid_129=" + FinalTable.Rows[QB]["Stone 11 Carat"].ToString() + "&_fid_130=" + FinalTable.Rows[QB]["Stone 11 Qty"].ToString() + "&_fid_131=" + FinalTable.Rows[QB]["Stone 11 Setting"].ToString() + "&_fid_132=" + FinalTable.Rows[QB]["Stone 12 Type"].ToString() + "&_fid_133=" + FinalTable.Rows[QB]["Stone 12 Shape"].ToString() + "&_fid_134=" + FinalTable.Rows[QB]["Stone 12 MM"].ToString() + "&_fid_135=" + FinalTable.Rows[QB]["Stone 12 Carat"].ToString() + "&_fid_136=" + FinalTable.Rows[QB]["Stone 12 Qty"].ToString() + "&_fid_137=" + FinalTable.Rows[QB]["Stone 12 Setting"].ToString() + "&_fid_138=" + FinalTable.Rows[QB]["Stone 13 Type"].ToString() + "&_fid_139=" + FinalTable.Rows[QB]["Stone 13 Shape"].ToString() + "&_fid_140=" + FinalTable.Rows[QB]["Stone 13 MM"].ToString() + "&_fid_141=" + FinalTable.Rows[QB]["Stone 13 Carat"].ToString() + "&_fid_142=" + FinalTable.Rows[QB]["Stone 13 Qty"].ToString() + "&_fid_143=" + FinalTable.Rows[QB]["Stone 13 Setting"].ToString() + "&_fid_144=" + FinalTable.Rows[QB]["Stone 14 Type"].ToString() + "&_fid_145=" + FinalTable.Rows[QB]["Stone 14 Shape"].ToString() + "&_fid_146=" + FinalTable.Rows[QB]["Stone 14 MM"].ToString() + "&_fid_147=" + FinalTable.Rows[QB]["Stone 14 Carat"].ToString() + "&_fid_148=" + FinalTable.Rows[QB]["Stone 14 Qty"].ToString() + "&_fid_149=" + FinalTable.Rows[QB]["Stone 14 Setting"].ToString() + "&_fid_150=" + FinalTable.Rows[QB]["Stone 15 Type"].ToString() + "&_fid_151=" + FinalTable.Rows[QB]["Stone 15 Shape"].ToString() + "&_fid_152=" + FinalTable.Rows[QB]["Stone 15 MM"].ToString() + "&_fid_153=" + FinalTable.Rows[QB]["Stone 15 Carat"].ToString() + "&_fid_154=" + FinalTable.Rows[QB]["Stone 15 Qty"].ToString() + "&_fid_155=" + FinalTable.Rows[QB]["Stone 15 Setting"].ToString() + "&_fid_156=" + FinalTable.Rows[QB]["Stone 16 Type"].ToString() + "&_fid_157=" + FinalTable.Rows[QB]["Stone 16 Shape"].ToString() + "&_fid_158=" + FinalTable.Rows[QB]["Stone 16 MM"].ToString() + "&_fid_159=" + FinalTable.Rows[QB]["Stone 16 Carat"].ToString() + "&_fid_160=" + FinalTable.Rows[QB]["Stone 16 Qty"].ToString() + "&_fid_161=" + FinalTable.Rows[QB]["Stone 16 Setting"].ToString() + "&_fid_162=" + FinalTable.Rows[QB]["Stone 17 Type"].ToString() + "&_fid_163=" + FinalTable.Rows[QB]["Stone 17 Shape"].ToString() + "&_fid_164=" + FinalTable.Rows[QB]["Stone 17 MM"].ToString() + "&_fid_165=" + FinalTable.Rows[QB]["Stone 17 Carat"].ToString() + "&_fid_166=" + FinalTable.Rows[QB]["Stone 17 Qty"].ToString() + "&_fid_167=" + FinalTable.Rows[QB]["Stone 17 Setting"].ToString() + "&_fid_168=" + FinalTable.Rows[QB]["Stone 18 Type"].ToString() + "&_fid_169=" + FinalTable.Rows[QB]["Stone 18 Shape"].ToString() + "&_fid_170=" + FinalTable.Rows[QB]["Stone 18 MM"].ToString() + "&_fid_171=" + FinalTable.Rows[QB]["Stone 18 Carat"].ToString() + "&_fid_172=" + FinalTable.Rows[QB]["Stone 18 Qty"].ToString() + "&_fid_173=" + FinalTable.Rows[QB]["Stone 18 Setting"].ToString() + "&_fid_245=" + FinalTable.Rows[QB]["Stone 1 Cost"].ToString() + "&_fid_246=" + FinalTable.Rows[QB]["Stone 2 Cost"].ToString() + "&_fid_247=" + FinalTable.Rows[QB]["Stone 3 Cost"].ToString() + "&_fid_248=" + FinalTable.Rows[QB]["Stone 4 Cost"].ToString() + "&_fid_249=" + FinalTable.Rows[QB]["Stone 5 Cost"].ToString() + "&_fid_250=" + FinalTable.Rows[QB]["Stone 6 Cost"].ToString() + "&_fid_251=" + FinalTable.Rows[QB]["Stone 7 Cost"].ToString() + "&_fid_252=" + FinalTable.Rows[QB]["Stone 8 Cost"].ToString() + "&_fid_253=" + FinalTable.Rows[QB]["Stone 9 Cost"].ToString() + "&_fid_254=" + FinalTable.Rows[QB]["Stone 10 Cost"].ToString() + "&_fid_255=" + FinalTable.Rows[QB]["Stone 11 Cost"].ToString() + "&_fid_256=" + FinalTable.Rows[QB]["Stone 12 Cost"].ToString() + "&_fid_257=" + FinalTable.Rows[QB]["Stone 13 Cost"].ToString() + "&_fid_258=" + FinalTable.Rows[QB]["Stone 14 Cost"].ToString() + "&_fid_259=" + FinalTable.Rows[QB]["Stone 15 Cost"].ToString() + "&_fid_260=" + FinalTable.Rows[QB]["Stone 16 Cost"].ToString() + "&_fid_261=" + FinalTable.Rows[QB]["Stone 17 Cost"].ToString() + "&_fid_262=" + FinalTable.Rows[QB]["Stone 18 Cost"].ToString() + "&_fid_77=" + GetWebName + "&_fid_345=" + FinalTable.Rows[QB]["CenterDiamondCarat"].ToString() + "&_fid_346=" + FinalTable.Rows[QB]["CenterDiamondCut"].ToString() + "&_fid_347=" + FinalTable.Rows[QB]["CenterDiamondColor"].ToString() + "&_fid_348=" + FinalTable.Rows[QB]["CenterDiamondClarity"].ToString() + "&_fid_349=" + FinalTable.Rows[QB]["Certificate"].ToString() + "&_fid_369=" + FinalTable.Rows[QB]["SilverPendantType"].ToString() + "&_fid_364=" + OrderPlatform + "&_fid_374=" + FinalTable.Rows[QB]["Semi Mount"].ToString() + "&_fid_381=" + "&_fid_381=" + FinalTable.Rows[QB]["Center_Stone_Shape"].ToString() + "&_fid_384=" + FinalTable.Rows[QB]["CouponCode"].ToString() + "&_fid_385=" + FinalTable.Rows[QB]["DiscountAmount"].ToString() + "&_fid_386=" + FinalTable.Rows[QB]["Sub_Total"].ToString() + "&usertoken=bzuxs9_cw8e_du2n5mbq9t6s7dqgxexu68h53s&apptoken=duby72vbbnfpx4dzsrwtjcbyqwep");

                    }

                    else if (FinalTable.Columns.Contains("Stone 17 Type"))
                    {

                        request = WebRequest.Create("https://nirgolan.quickbase.com/db/bmimrj5vm?a=API_AddRecord" + "&_fid_6=" + FinalTable.Rows[QB]["Order Date"].ToString() + "&_fid_7=" + FinalTable.Rows[QB]["Order number"].ToString() + "&_fid_8=" + FinalTable.Rows[QB]["Amount"].ToString() + "&_fid_9=" + FinalTable.Rows[QB]["Transaction"].ToString() + "&_fid_10=" + FinalTable.Rows[QB]["Payment Method"].ToString() + "&_fid_11=" + FinalTable.Rows[QB]["Amount Before Tax"].ToString() + "&_fid_12=" + FinalTable.Rows[QB]["Tax"].ToString() + "&_fid_13=" + FinalTable.Rows[QB]["Shipping City"].ToString() + "&_fid_14=" + FinalTable.Rows[QB]["Shipping Zip"].ToString() + "&_fid_15=" + FinalTable.Rows[QB]["ShippingService"].ToString() + "&_fid_16=" + FinalTable.Rows[QB]["Customer'sAccounts"].ToString() + "&_fid_17=" + FinalTable.Rows[QB]["Style No"].ToString() + "&_fid_18=" + FinalTable.Rows[QB]["Diamond Quality"].ToString() + "&_fid_19=" + FinalTable.Rows[QB]["Order Quantity"].ToString() + "&_fid_20=" + FinalTable.Rows[QB]["Gram Weight"].ToString() + "&_fid_21=" + FinalTable.Rows[QB]["Metal Karat"].ToString() + "&_fid_22=" + FinalTable.Rows[QB]["Metal Color"].ToString() + "&_fid_23=" + FinalTable.Rows[QB]["Labor Rate"].ToString() + "&_fid_24=" + FinalTable.Rows[QB]["Coupon"].ToString() + "&_fid_25=" + FinalTable.Rows[QB]["IGI"].ToString() + "&_fid_26=" + FinalTable.Rows[QB]["SilverReplicaText"].ToString() + "&_fid_27=" + FinalTable.Rows[QB]["Silver Pendant"].ToString() + "&_fid_28=" + FinalTable.Rows[QB]["Charm/Engraving"].ToString() + "&_fid_29=" + FinalTable.Rows[QB]["EngravingType"].ToString() + "&_fid_30=" + FinalTable.Rows[QB]["EngravingText"].ToString() + "&_fid_31=" + FinalTable.Rows[QB]["Rapnet Diamond"].ToString() + "&_fid_32=" + FinalTable.Rows[QB]["Anjolee Diamond"].ToString() + "&_fid_33=" + FinalTable.Rows[QB]["Customer Name"].ToString() + "&_fid_34=" + FinalTable.Rows[QB]["Customer email"].ToString() + "&_fid_35=" + FinalTable.Rows[QB]["Billing Phone Number"].ToString() + "&_fid_36=" + FinalTable.Rows[QB]["Bill/Ship Match"].ToString() + "&_fid_37=" + FinalTable.Rows[QB]["Payment Option"].ToString() + "&_fid_38=" + FinalTable.Rows[QB]["Shipping Name"].ToString() + "&_fid_39=" + FinalTable.Rows[QB]["Shipping Address1"].ToString() + "&_fid_40=" + FinalTable.Rows[QB]["Shipping Address2"].ToString() + "&_fid_41=" + FinalTable.Rows[QB]["Shipping Apt/Unit No"].ToString() + "&_fid_42=" + FinalTable.Rows[QB]["Shipping State"].ToString() + "&_fid_43=" + FinalTable.Rows[QB]["Shipping Country"].ToString() + "&_fid_44=" + FinalTable.Rows[QB]["Shipping Phone"].ToString() + "&_fid_45=" + FinalTable.Rows[QB]["Item Length/Ring Size"].ToString() + "&_fid_46=" + FinalTable.Rows[QB]["BandMetalType"].ToString() + "&_fid_47=" + FinalTable.Rows[QB]["VendorChildSku"].ToString() + "&_fid_48=" + FinalTable.Rows[QB]["Model Carat Weight"].ToString() + "&_fid_49=" + FinalTable.Rows[QB]["Effective Carat Weight"].ToString() + "&_fid_50=" + FinalTable.Rows[QB]["Vendor Metal Type"].ToString() + "&_fid_51=" + FinalTable.Rows[QB]["Stone Information Quality"].ToString() + "&_fid_52=" + FinalTable.Rows[QB]["Vendor Feed Price"].ToString() + "&_fid_53=" + FinalTable.Rows[QB]["Cut"].ToString() + "&_fid_54=" + FinalTable.Rows[QB]["Color"].ToString() + "&_fid_55=" + FinalTable.Rows[QB]["Clarity"].ToString() + "&_fid_56=" + FinalTable.Rows[QB]["Depth"].ToString() + "&_fid_57=" + FinalTable.Rows[QB]["Table"].ToString() + "&_fid_58=" + FinalTable.Rows[QB]["Girdle"].ToString() + "&_fid_59=" + FinalTable.Rows[QB]["Symmetry"].ToString() + "&_fid_60=" + FinalTable.Rows[QB]["Polish"].ToString() + "&_fid_61=" + FinalTable.Rows[QB]["CuletSize"].ToString() + "&_fid_62=" + FinalTable.Rows[QB]["FluorescenceIntensity"].ToString() + "&_fid_63=" + FinalTable.Rows[QB]["Lab"].ToString() + "&_fid_64=" + FinalTable.Rows[QB]["DiamondID"].ToString() + "&_fid_65=" + FinalTable.Rows[QB]["Stone 1 Type"].ToString() + "&_fid_66=" + FinalTable.Rows[QB]["Stone 1 Shape"].ToString() + "&_fid_67=" + FinalTable.Rows[QB]["Stone 1 MM"].ToString() + "&_fid_68=" + FinalTable.Rows[QB]["Stone 1 Carat"].ToString() + "&_fid_69=" + FinalTable.Rows[QB]["Stone 1 Qty"].ToString() + "&_fid_70=" + FinalTable.Rows[QB]["Stone 2 Setting"].ToString() + "&_fid_71=" + FinalTable.Rows[QB]["Stone 2 Type"].ToString() + "&_fid_72=" + FinalTable.Rows[QB]["Stone 2 Shape"].ToString() + "&_fid_73=" + FinalTable.Rows[QB]["Stone 2 MM"].ToString() + "&_fid_74=" + FinalTable.Rows[QB]["Stone 2 Carat"].ToString() + "&_fid_75=" + FinalTable.Rows[QB]["Stone 2 Qty"].ToString() + "&_fid_76=" + FinalTable.Rows[QB]["Stone 2 Setting"].ToString() + "&_fid_78=" + FinalTable.Rows[QB]["Stone 3 Type"].ToString() + "&_fid_79=" + FinalTable.Rows[QB]["Stone 3 Shape"].ToString() + "&_fid_80=" + FinalTable.Rows[QB]["Stone 3 MM"].ToString() + "&_fid_81=" + FinalTable.Rows[QB]["Stone 3 Carat"].ToString() + "&_fid_82=" + FinalTable.Rows[QB]["Stone 3 Qty"].ToString() + "&_fid_83=" + FinalTable.Rows[QB]["Stone 3 Setting"].ToString() + "&_fid_84=" + FinalTable.Rows[QB]["Stone 4 Type"].ToString() + "&_fid_85=" + FinalTable.Rows[QB]["Stone 4 Shape"].ToString() + "&_fid_86=" + FinalTable.Rows[QB]["Stone 4 MM"].ToString() + "&_fid_87=" + FinalTable.Rows[QB]["Stone 4 Carat"].ToString() + "&_fid_88=" + FinalTable.Rows[QB]["Stone 4 Qty"].ToString() + "&_fid_89=" + FinalTable.Rows[QB]["Stone 4 Setting"].ToString() + "&_fid_90=" + FinalTable.Rows[QB]["Stone 5 Type"].ToString() + "&_fid_91=" + FinalTable.Rows[QB]["Stone 5 Shape"].ToString() + "&_fid_92=" + FinalTable.Rows[QB]["Stone 5 MM"].ToString() + "&_fid_93=" + FinalTable.Rows[QB]["Stone 5 Carat"].ToString() + "&_fid_94=" + FinalTable.Rows[QB]["Stone 5 Qty"].ToString() + "&_fid_95=" + FinalTable.Rows[QB]["Stone 5 Setting"].ToString() + "&_fid_96=" + FinalTable.Rows[QB]["Stone 6 Type"].ToString() + "&_fid_97=" + FinalTable.Rows[QB]["Stone 6 Shape"].ToString() + "&_fid_98=" + FinalTable.Rows[QB]["Stone 6 MM"].ToString() + "&_fid_99=" + FinalTable.Rows[QB]["Stone 6 Carat"].ToString() + "&_fid_100=" + FinalTable.Rows[QB]["Stone 6 Qty"].ToString() + "&_fid_101=" + FinalTable.Rows[QB]["Stone 6 Setting"].ToString() + "&_fid_102=" + FinalTable.Rows[QB]["Stone 7 Type"].ToString() + "&_fid_103=" + FinalTable.Rows[QB]["Stone 7 Shape"].ToString() + "&_fid_104=" + FinalTable.Rows[QB]["Stone 7 MM"].ToString() + "&_fid_105=" + FinalTable.Rows[QB]["Stone 7 Carat"].ToString() + "&_fid_106=" + FinalTable.Rows[QB]["Stone 7 Qty"].ToString() + "&_fid_107=" + FinalTable.Rows[QB]["Stone 7 Setting"].ToString() + "&_fid_108=" + FinalTable.Rows[QB]["Stone 8 Type"].ToString() + "&_fid_109=" + FinalTable.Rows[QB]["Stone 8 Shape"].ToString() + "&_fid_110=" + FinalTable.Rows[QB]["Stone 8 MM"].ToString() + "&_fid_111=" + FinalTable.Rows[QB]["Stone 8 Carat"].ToString() + "&_fid_112=" + FinalTable.Rows[QB]["Stone 8 Qty"].ToString() + "&_fid_113=" + FinalTable.Rows[QB]["Stone 8 Setting"].ToString() + "&_fid_114=" + FinalTable.Rows[QB]["Stone 9 Type"].ToString() + "&_fid_115=" + FinalTable.Rows[QB]["Stone 9 Shape"].ToString() + "&_fid_116=" + FinalTable.Rows[QB]["Stone 9 MM"].ToString() + "&_fid_117=" + FinalTable.Rows[QB]["Stone 9 Carat"].ToString() + "&_fid_118=" + FinalTable.Rows[QB]["Stone 9 Qty"].ToString() + "&_fid_119=" + FinalTable.Rows[QB]["Stone 9 Setting"].ToString() + "&_fid_120=" + FinalTable.Rows[QB]["Stone 10 Type"].ToString() + "&_fid_121=" + FinalTable.Rows[QB]["Stone 10 Shape"].ToString() + "&_fid_122=" + FinalTable.Rows[QB]["Stone 10 MM"].ToString() + "&_fid_123=" + FinalTable.Rows[QB]["Stone 10 Carat"].ToString() + "&_fid_124=" + FinalTable.Rows[QB]["Stone 10 Qty"].ToString() + "&_fid_125=" + FinalTable.Rows[QB]["Stone 10 Setting"].ToString() + "&_fid_126=" + FinalTable.Rows[QB]["Stone 11 Type"].ToString() + "&_fid_127=" + FinalTable.Rows[QB]["Stone 11 Shape"].ToString() + "&_fid_128=" + FinalTable.Rows[QB]["Stone 11 MM"].ToString() + "&_fid_129=" + FinalTable.Rows[QB]["Stone 11 Carat"].ToString() + "&_fid_130=" + FinalTable.Rows[QB]["Stone 11 Qty"].ToString() + "&_fid_131=" + FinalTable.Rows[QB]["Stone 11 Setting"].ToString() + "&_fid_132=" + FinalTable.Rows[QB]["Stone 12 Type"].ToString() + "&_fid_133=" + FinalTable.Rows[QB]["Stone 12 Shape"].ToString() + "&_fid_134=" + FinalTable.Rows[QB]["Stone 12 MM"].ToString() + "&_fid_135=" + FinalTable.Rows[QB]["Stone 12 Carat"].ToString() + "&_fid_136=" + FinalTable.Rows[QB]["Stone 12 Qty"].ToString() + "&_fid_137=" + FinalTable.Rows[QB]["Stone 12 Setting"].ToString() + "&_fid_138=" + FinalTable.Rows[QB]["Stone 13 Type"].ToString() + "&_fid_139=" + FinalTable.Rows[QB]["Stone 13 Shape"].ToString() + "&_fid_140=" + FinalTable.Rows[QB]["Stone 13 MM"].ToString() + "&_fid_141=" + FinalTable.Rows[QB]["Stone 13 Carat"].ToString() + "&_fid_142=" + FinalTable.Rows[QB]["Stone 13 Qty"].ToString() + "&_fid_143=" + FinalTable.Rows[QB]["Stone 13 Setting"].ToString() + "&_fid_144=" + FinalTable.Rows[QB]["Stone 14 Type"].ToString() + "&_fid_145=" + FinalTable.Rows[QB]["Stone 14 Shape"].ToString() + "&_fid_146=" + FinalTable.Rows[QB]["Stone 14 MM"].ToString() + "&_fid_147=" + FinalTable.Rows[QB]["Stone 14 Carat"].ToString() + "&_fid_148=" + FinalTable.Rows[QB]["Stone 14 Qty"].ToString() + "&_fid_149=" + FinalTable.Rows[QB]["Stone 14 Setting"].ToString() + "&_fid_150=" + FinalTable.Rows[QB]["Stone 15 Type"].ToString() + "&_fid_151=" + FinalTable.Rows[QB]["Stone 15 Shape"].ToString() + "&_fid_152=" + FinalTable.Rows[QB]["Stone 15 MM"].ToString() + "&_fid_153=" + FinalTable.Rows[QB]["Stone 15 Carat"].ToString() + "&_fid_154=" + FinalTable.Rows[QB]["Stone 15 Qty"].ToString() + "&_fid_155=" + FinalTable.Rows[QB]["Stone 15 Setting"].ToString() + "&_fid_156=" + FinalTable.Rows[QB]["Stone 16 Type"].ToString() + "&_fid_157=" + FinalTable.Rows[QB]["Stone 16 Shape"].ToString() + "&_fid_158=" + FinalTable.Rows[QB]["Stone 16 MM"].ToString() + "&_fid_159=" + FinalTable.Rows[QB]["Stone 16 Carat"].ToString() + "&_fid_160=" + FinalTable.Rows[QB]["Stone 16 Qty"].ToString() + "&_fid_161=" + FinalTable.Rows[QB]["Stone 16 Setting"].ToString() + "&_fid_162=" + FinalTable.Rows[QB]["Stone 17 Type"].ToString() + "&_fid_163=" + FinalTable.Rows[QB]["Stone 17 Shape"].ToString() + "&_fid_164=" + FinalTable.Rows[QB]["Stone 17 MM"].ToString() + "&_fid_165=" + FinalTable.Rows[QB]["Stone 17 Carat"].ToString() + "&_fid_166=" + FinalTable.Rows[QB]["Stone 17 Qty"].ToString() + "&_fid_167=" + FinalTable.Rows[QB]["Stone 17 Setting"].ToString() + "&_fid_261=" + FinalTable.Rows[QB]["Stone 17 Cost"].ToString() + "&_fid_245=" + FinalTable.Rows[QB]["Stone 1 Cost"].ToString() + "&_fid_246=" + FinalTable.Rows[QB]["Stone 2 Cost"].ToString() + "&_fid_247=" + FinalTable.Rows[QB]["Stone 3 Cost"].ToString() + "&_fid_248=" + FinalTable.Rows[QB]["Stone 4 Cost"].ToString() + "&_fid_249=" + FinalTable.Rows[QB]["Stone 5 Cost"].ToString() + "&_fid_250=" + FinalTable.Rows[QB]["Stone 6 Cost"].ToString() + "&_fid_251=" + FinalTable.Rows[QB]["Stone 7 Cost"].ToString() + "&_fid_252=" + FinalTable.Rows[QB]["Stone 8 Cost"].ToString() + "&_fid_253=" + FinalTable.Rows[QB]["Stone 9 Cost"].ToString() + "&_fid_254=" + FinalTable.Rows[QB]["Stone 10 Cost"].ToString() + "&_fid_255=" + FinalTable.Rows[QB]["Stone 11 Cost"].ToString() + "&_fid_256=" + FinalTable.Rows[QB]["Stone 12 Cost"].ToString() + "&_fid_257=" + FinalTable.Rows[QB]["Stone 13 Cost"].ToString() + "&_fid_258=" + FinalTable.Rows[QB]["Stone 14 Cost"].ToString() + "&_fid_259=" + FinalTable.Rows[QB]["Stone 15 Cost"].ToString() + "&_fid_260=" + FinalTable.Rows[QB]["Stone 16 Cost"].ToString() + "&_fid_261=" + FinalTable.Rows[QB]["Stone 17 Cost"].ToString() + "&_fid_77=" + GetWebName + "&_fid_345=" + FinalTable.Rows[QB]["CenterDiamondCarat"].ToString() + "&_fid_346=" + FinalTable.Rows[QB]["CenterDiamondCut"].ToString() + "&_fid_347=" + FinalTable.Rows[QB]["CenterDiamondColor"].ToString() + "&_fid_348=" + FinalTable.Rows[QB]["CenterDiamondClarity"].ToString() + "&_fid_349=" + FinalTable.Rows[QB]["Certificate"].ToString() + "&_fid_369=" + FinalTable.Rows[QB]["SilverPendantType"].ToString() + "&_fid_364=" + OrderPlatform + "&_fid_374=" + FinalTable.Rows[QB]["Semi Mount"].ToString() + "&_fid_381=" + "&_fid_381=" + FinalTable.Rows[QB]["Center_Stone_Shape"].ToString() + "&_fid_384=" + FinalTable.Rows[QB]["CouponCode"].ToString() + "&_fid_385=" + FinalTable.Rows[QB]["DiscountAmount"].ToString() + "&_fid_386=" + FinalTable.Rows[QB]["Sub_Total"].ToString() + "&usertoken=bzuxs9_cw8e_du2n5mbq9t6s7dqgxexu68h53s&apptoken=duby72vbbnfpx4dzsrwtjcbyqwep");

                    }


                    else if (FinalTable.Columns.Contains("Stone 16 Type"))
                    {

                        request = WebRequest.Create("https://nirgolan.quickbase.com/db/bmimrj5vm?a=API_AddRecord" + "&_fid_6=" + FinalTable.Rows[QB]["Order Date"].ToString() + "&_fid_7=" + FinalTable.Rows[QB]["Order number"].ToString() + "&_fid_8=" + FinalTable.Rows[QB]["Amount"].ToString() + "&_fid_9=" + FinalTable.Rows[QB]["Transaction"].ToString() + "&_fid_10=" + FinalTable.Rows[QB]["Payment Method"].ToString() + "&_fid_11=" + FinalTable.Rows[QB]["Amount Before Tax"].ToString() + "&_fid_12=" + FinalTable.Rows[QB]["Tax"].ToString() + "&_fid_13=" + FinalTable.Rows[QB]["Shipping City"].ToString() + "&_fid_14=" + FinalTable.Rows[QB]["Shipping Zip"].ToString() + "&_fid_15=" + FinalTable.Rows[QB]["ShippingService"].ToString() + "&_fid_16=" + FinalTable.Rows[QB]["Customer'sAccounts"].ToString() + "&_fid_17=" + FinalTable.Rows[QB]["Style No"].ToString() + "&_fid_18=" + FinalTable.Rows[QB]["Diamond Quality"].ToString() + "&_fid_19=" + FinalTable.Rows[QB]["Order Quantity"].ToString() + "&_fid_20=" + FinalTable.Rows[QB]["Gram Weight"].ToString() + "&_fid_21=" + FinalTable.Rows[QB]["Metal Karat"].ToString() + "&_fid_22=" + FinalTable.Rows[QB]["Metal Color"].ToString() + "&_fid_23=" + FinalTable.Rows[QB]["Labor Rate"].ToString() + "&_fid_24=" + FinalTable.Rows[QB]["Coupon"].ToString() + "&_fid_25=" + FinalTable.Rows[QB]["IGI"].ToString() + "&_fid_26=" + FinalTable.Rows[QB]["SilverReplicaText"].ToString() + "&_fid_27=" + FinalTable.Rows[QB]["Silver Pendant"].ToString() + "&_fid_28=" + FinalTable.Rows[QB]["Charm/Engraving"].ToString() + "&_fid_29=" + FinalTable.Rows[QB]["EngravingType"].ToString() + "&_fid_30=" + FinalTable.Rows[QB]["EngravingText"].ToString() + "&_fid_31=" + FinalTable.Rows[QB]["Rapnet Diamond"].ToString() + "&_fid_32=" + FinalTable.Rows[QB]["Anjolee Diamond"].ToString() + "&_fid_33=" + FinalTable.Rows[QB]["Customer Name"].ToString() + "&_fid_34=" + FinalTable.Rows[QB]["Customer email"].ToString() + "&_fid_35=" + FinalTable.Rows[QB]["Billing Phone Number"].ToString() + "&_fid_36=" + FinalTable.Rows[QB]["Bill/Ship Match"].ToString() + "&_fid_37=" + FinalTable.Rows[QB]["Payment Option"].ToString() + "&_fid_38=" + FinalTable.Rows[QB]["Shipping Name"].ToString() + "&_fid_39=" + FinalTable.Rows[QB]["Shipping Address1"].ToString() + "&_fid_40=" + FinalTable.Rows[QB]["Shipping Address2"].ToString() + "&_fid_41=" + FinalTable.Rows[QB]["Shipping Apt/Unit No"].ToString() + "&_fid_42=" + FinalTable.Rows[QB]["Shipping State"].ToString() + "&_fid_43=" + FinalTable.Rows[QB]["Shipping Country"].ToString() + "&_fid_44=" + FinalTable.Rows[QB]["Shipping Phone"].ToString() + "&_fid_45=" + FinalTable.Rows[QB]["Item Length/Ring Size"].ToString() + "&_fid_46=" + FinalTable.Rows[QB]["BandMetalType"].ToString() + "&_fid_47=" + FinalTable.Rows[QB]["VendorChildSku"].ToString() + "&_fid_48=" + FinalTable.Rows[QB]["Model Carat Weight"].ToString() + "&_fid_49=" + FinalTable.Rows[QB]["Effective Carat Weight"].ToString() + "&_fid_50=" + FinalTable.Rows[QB]["Vendor Metal Type"].ToString() + "&_fid_51=" + FinalTable.Rows[QB]["Stone Information Quality"].ToString() + "&_fid_52=" + FinalTable.Rows[QB]["Vendor Feed Price"].ToString() + "&_fid_53=" + FinalTable.Rows[QB]["Cut"].ToString() + "&_fid_54=" + FinalTable.Rows[QB]["Color"].ToString() + "&_fid_55=" + FinalTable.Rows[QB]["Clarity"].ToString() + "&_fid_56=" + FinalTable.Rows[QB]["Depth"].ToString() + "&_fid_57=" + FinalTable.Rows[QB]["Table"].ToString() + "&_fid_58=" + FinalTable.Rows[QB]["Girdle"].ToString() + "&_fid_59=" + FinalTable.Rows[QB]["Symmetry"].ToString() + "&_fid_60=" + FinalTable.Rows[QB]["Polish"].ToString() + "&_fid_61=" + FinalTable.Rows[QB]["CuletSize"].ToString() + "&_fid_62=" + FinalTable.Rows[QB]["FluorescenceIntensity"].ToString() + "&_fid_63=" + FinalTable.Rows[QB]["Lab"].ToString() + "&_fid_64=" + FinalTable.Rows[QB]["DiamondID"].ToString() + "&_fid_65=" + FinalTable.Rows[QB]["Stone 1 Type"].ToString() + "&_fid_66=" + FinalTable.Rows[QB]["Stone 1 Shape"].ToString() + "&_fid_67=" + FinalTable.Rows[QB]["Stone 1 MM"].ToString() + "&_fid_68=" + FinalTable.Rows[QB]["Stone 1 Carat"].ToString() + "&_fid_69=" + FinalTable.Rows[QB]["Stone 1 Qty"].ToString() + "&_fid_70=" + FinalTable.Rows[QB]["Stone 2 Setting"].ToString() + "&_fid_71=" + FinalTable.Rows[QB]["Stone 2 Type"].ToString() + "&_fid_72=" + FinalTable.Rows[QB]["Stone 2 Shape"].ToString() + "&_fid_73=" + FinalTable.Rows[QB]["Stone 2 MM"].ToString() + "&_fid_74=" + FinalTable.Rows[QB]["Stone 2 Carat"].ToString() + "&_fid_75=" + FinalTable.Rows[QB]["Stone 2 Qty"].ToString() + "&_fid_76=" + FinalTable.Rows[QB]["Stone 2 Setting"].ToString() + "&_fid_78=" + FinalTable.Rows[QB]["Stone 3 Type"].ToString() + "&_fid_79=" + FinalTable.Rows[QB]["Stone 3 Shape"].ToString() + "&_fid_80=" + FinalTable.Rows[QB]["Stone 3 MM"].ToString() + "&_fid_81=" + FinalTable.Rows[QB]["Stone 3 Carat"].ToString() + "&_fid_82=" + FinalTable.Rows[QB]["Stone 3 Qty"].ToString() + "&_fid_83=" + FinalTable.Rows[QB]["Stone 3 Setting"].ToString() + "&_fid_84=" + FinalTable.Rows[QB]["Stone 4 Type"].ToString() + "&_fid_85=" + FinalTable.Rows[QB]["Stone 4 Shape"].ToString() + "&_fid_86=" + FinalTable.Rows[QB]["Stone 4 MM"].ToString() + "&_fid_87=" + FinalTable.Rows[QB]["Stone 4 Carat"].ToString() + "&_fid_88=" + FinalTable.Rows[QB]["Stone 4 Qty"].ToString() + "&_fid_89=" + FinalTable.Rows[QB]["Stone 4 Setting"].ToString() + "&_fid_90=" + FinalTable.Rows[QB]["Stone 5 Type"].ToString() + "&_fid_91=" + FinalTable.Rows[QB]["Stone 5 Shape"].ToString() + "&_fid_92=" + FinalTable.Rows[QB]["Stone 5 MM"].ToString() + "&_fid_93=" + FinalTable.Rows[QB]["Stone 5 Carat"].ToString() + "&_fid_94=" + FinalTable.Rows[QB]["Stone 5 Qty"].ToString() + "&_fid_95=" + FinalTable.Rows[QB]["Stone 5 Setting"].ToString() + "&_fid_96=" + FinalTable.Rows[QB]["Stone 6 Type"].ToString() + "&_fid_97=" + FinalTable.Rows[QB]["Stone 6 Shape"].ToString() + "&_fid_98=" + FinalTable.Rows[QB]["Stone 6 MM"].ToString() + "&_fid_99=" + FinalTable.Rows[QB]["Stone 6 Carat"].ToString() + "&_fid_100=" + FinalTable.Rows[QB]["Stone 6 Qty"].ToString() + "&_fid_101=" + FinalTable.Rows[QB]["Stone 6 Setting"].ToString() + "&_fid_102=" + FinalTable.Rows[QB]["Stone 7 Type"].ToString() + "&_fid_103=" + FinalTable.Rows[QB]["Stone 7 Shape"].ToString() + "&_fid_104=" + FinalTable.Rows[QB]["Stone 7 MM"].ToString() + "&_fid_105=" + FinalTable.Rows[QB]["Stone 7 Carat"].ToString() + "&_fid_106=" + FinalTable.Rows[QB]["Stone 7 Qty"].ToString() + "&_fid_107=" + FinalTable.Rows[QB]["Stone 7 Setting"].ToString() + "&_fid_108=" + FinalTable.Rows[QB]["Stone 8 Type"].ToString() + "&_fid_109=" + FinalTable.Rows[QB]["Stone 8 Shape"].ToString() + "&_fid_110=" + FinalTable.Rows[QB]["Stone 8 MM"].ToString() + "&_fid_111=" + FinalTable.Rows[QB]["Stone 8 Carat"].ToString() + "&_fid_112=" + FinalTable.Rows[QB]["Stone 8 Qty"].ToString() + "&_fid_113=" + FinalTable.Rows[QB]["Stone 8 Setting"].ToString() + "&_fid_114=" + FinalTable.Rows[QB]["Stone 9 Type"].ToString() + "&_fid_115=" + FinalTable.Rows[QB]["Stone 9 Shape"].ToString() + "&_fid_116=" + FinalTable.Rows[QB]["Stone 9 MM"].ToString() + "&_fid_117=" + FinalTable.Rows[QB]["Stone 9 Carat"].ToString() + "&_fid_118=" + FinalTable.Rows[QB]["Stone 9 Qty"].ToString() + "&_fid_119=" + FinalTable.Rows[QB]["Stone 9 Setting"].ToString() + "&_fid_120=" + FinalTable.Rows[QB]["Stone 10 Type"].ToString() + "&_fid_121=" + FinalTable.Rows[QB]["Stone 10 Shape"].ToString() + "&_fid_122=" + FinalTable.Rows[QB]["Stone 10 MM"].ToString() + "&_fid_123=" + FinalTable.Rows[QB]["Stone 10 Carat"].ToString() + "&_fid_124=" + FinalTable.Rows[QB]["Stone 10 Qty"].ToString() + "&_fid_125=" + FinalTable.Rows[QB]["Stone 10 Setting"].ToString() + "&_fid_126=" + FinalTable.Rows[QB]["Stone 11 Type"].ToString() + "&_fid_127=" + FinalTable.Rows[QB]["Stone 11 Shape"].ToString() + "&_fid_128=" + FinalTable.Rows[QB]["Stone 11 MM"].ToString() + "&_fid_129=" + FinalTable.Rows[QB]["Stone 11 Carat"].ToString() + "&_fid_130=" + FinalTable.Rows[QB]["Stone 11 Qty"].ToString() + "&_fid_131=" + FinalTable.Rows[QB]["Stone 11 Setting"].ToString() + "&_fid_132=" + FinalTable.Rows[QB]["Stone 12 Type"].ToString() + "&_fid_133=" + FinalTable.Rows[QB]["Stone 12 Shape"].ToString() + "&_fid_134=" + FinalTable.Rows[QB]["Stone 12 MM"].ToString() + "&_fid_135=" + FinalTable.Rows[QB]["Stone 12 Carat"].ToString() + "&_fid_136=" + FinalTable.Rows[QB]["Stone 12 Qty"].ToString() + "&_fid_137=" + FinalTable.Rows[QB]["Stone 12 Setting"].ToString() + "&_fid_138=" + FinalTable.Rows[QB]["Stone 13 Type"].ToString() + "&_fid_139=" + FinalTable.Rows[QB]["Stone 13 Shape"].ToString() + "&_fid_140=" + FinalTable.Rows[QB]["Stone 13 MM"].ToString() + "&_fid_141=" + FinalTable.Rows[QB]["Stone 13 Carat"].ToString() + "&_fid_142=" + FinalTable.Rows[QB]["Stone 13 Qty"].ToString() + "&_fid_143=" + FinalTable.Rows[QB]["Stone 13 Setting"].ToString() + "&_fid_144=" + FinalTable.Rows[QB]["Stone 14 Type"].ToString() + "&_fid_145=" + FinalTable.Rows[QB]["Stone 14 Shape"].ToString() + "&_fid_146=" + FinalTable.Rows[QB]["Stone 14 MM"].ToString() + "&_fid_147=" + FinalTable.Rows[QB]["Stone 14 Carat"].ToString() + "&_fid_148=" + FinalTable.Rows[QB]["Stone 14 Qty"].ToString() + "&_fid_149=" + FinalTable.Rows[QB]["Stone 14 Setting"].ToString() + "&_fid_150=" + FinalTable.Rows[QB]["Stone 15 Type"].ToString() + "&_fid_151=" + FinalTable.Rows[QB]["Stone 15 Shape"].ToString() + "&_fid_152=" + FinalTable.Rows[QB]["Stone 15 MM"].ToString() + "&_fid_153=" + FinalTable.Rows[QB]["Stone 15 Carat"].ToString() + "&_fid_154=" + FinalTable.Rows[QB]["Stone 15 Qty"].ToString() + "&_fid_155=" + FinalTable.Rows[QB]["Stone 15 Setting"].ToString() + "&_fid_156=" + FinalTable.Rows[QB]["Stone 16 Type"].ToString() + "&_fid_157=" + FinalTable.Rows[QB]["Stone 16 Shape"].ToString() + "&_fid_158=" + FinalTable.Rows[QB]["Stone 16 MM"].ToString() + "&_fid_159=" + FinalTable.Rows[QB]["Stone 16 Carat"].ToString() + "&_fid_160=" + FinalTable.Rows[QB]["Stone 16 Qty"].ToString() + "&_fid_161=" + FinalTable.Rows[QB]["Stone 16 Setting"].ToString() + "&_fid_245=" + FinalTable.Rows[QB]["Stone 1 Cost"].ToString() + "&_fid_246=" + FinalTable.Rows[QB]["Stone 2 Cost"].ToString() + "&_fid_247=" + FinalTable.Rows[QB]["Stone 3 Cost"].ToString() + "&_fid_248=" + FinalTable.Rows[QB]["Stone 4 Cost"].ToString() + "&_fid_249=" + FinalTable.Rows[QB]["Stone 5 Cost"].ToString() + "&_fid_250=" + FinalTable.Rows[QB]["Stone 6 Cost"].ToString() + "&_fid_251=" + FinalTable.Rows[QB]["Stone 7 Cost"].ToString() + "&_fid_252=" + FinalTable.Rows[QB]["Stone 8 Cost"].ToString() + "&_fid_253=" + FinalTable.Rows[QB]["Stone 9 Cost"].ToString() + "&_fid_254=" + FinalTable.Rows[QB]["Stone 10 Cost"].ToString() + "&_fid_255=" + FinalTable.Rows[QB]["Stone 11 Cost"].ToString() + "&_fid_256=" + FinalTable.Rows[QB]["Stone 12 Cost"].ToString() + "&_fid_257=" + FinalTable.Rows[QB]["Stone 13 Cost"].ToString() + "&_fid_258=" + FinalTable.Rows[QB]["Stone 14 Cost"].ToString() + "&_fid_259=" + FinalTable.Rows[QB]["Stone 15 Cost"].ToString() + "&_fid_260=" + FinalTable.Rows[QB]["Stone 16 Cost"].ToString() + "&_fid_77=" + GetWebName + "&_fid_345=" + FinalTable.Rows[QB]["CenterDiamondCarat"].ToString() + "&_fid_346=" + FinalTable.Rows[QB]["CenterDiamondCut"].ToString() + "&_fid_347=" + FinalTable.Rows[QB]["CenterDiamondColor"].ToString() + "&_fid_348=" + FinalTable.Rows[QB]["CenterDiamondClarity"].ToString() + "&_fid_349=" + FinalTable.Rows[QB]["Certificate"].ToString() + "&_fid_369=" + FinalTable.Rows[QB]["SilverPendantType"].ToString() + "&_fid_364=" + OrderPlatform + "&_fid_374=" + FinalTable.Rows[QB]["Semi Mount"].ToString() + "&_fid_381=" + "&_fid_381=" + FinalTable.Rows[QB]["Center_Stone_Shape"].ToString() + "&_fid_384=" + FinalTable.Rows[QB]["CouponCode"].ToString() + "&_fid_385=" + FinalTable.Rows[QB]["DiscountAmount"].ToString() + "&_fid_386=" + FinalTable.Rows[QB]["Sub_Total"].ToString() + "&usertoken=bzuxs9_cw8e_du2n5mbq9t6s7dqgxexu68h53s&apptoken=duby72vbbnfpx4dzsrwtjcbyqwep");

                    }


                    else if (FinalTable.Columns.Contains("Stone 15 Type"))
                    {

                        request = WebRequest.Create("https://nirgolan.quickbase.com/db/bmimrj5vm?a=API_AddRecord" + "&_fid_6=" + FinalTable.Rows[QB]["Order Date"].ToString() + "&_fid_7=" + FinalTable.Rows[QB]["Order number"].ToString() + "&_fid_8=" + FinalTable.Rows[QB]["Amount"].ToString() + "&_fid_9=" + FinalTable.Rows[QB]["Transaction"].ToString() + "&_fid_10=" + FinalTable.Rows[QB]["Payment Method"].ToString() + "&_fid_11=" + FinalTable.Rows[QB]["Amount Before Tax"].ToString() + "&_fid_12=" + FinalTable.Rows[QB]["Tax"].ToString() + "&_fid_13=" + FinalTable.Rows[QB]["Shipping City"].ToString() + "&_fid_14=" + FinalTable.Rows[QB]["Shipping Zip"].ToString() + "&_fid_15=" + FinalTable.Rows[QB]["ShippingService"].ToString() + "&_fid_16=" + FinalTable.Rows[QB]["Customer'sAccounts"].ToString() + "&_fid_17=" + FinalTable.Rows[QB]["Style No"].ToString() + "&_fid_18=" + FinalTable.Rows[QB]["Diamond Quality"].ToString() + "&_fid_19=" + FinalTable.Rows[QB]["Order Quantity"].ToString() + "&_fid_20=" + FinalTable.Rows[QB]["Gram Weight"].ToString() + "&_fid_21=" + FinalTable.Rows[QB]["Metal Karat"].ToString() + "&_fid_22=" + FinalTable.Rows[QB]["Metal Color"].ToString() + "&_fid_23=" + FinalTable.Rows[QB]["Labor Rate"].ToString() + "&_fid_24=" + FinalTable.Rows[QB]["Coupon"].ToString() + "&_fid_25=" + FinalTable.Rows[QB]["IGI"].ToString() + "&_fid_26=" + FinalTable.Rows[QB]["SilverReplicaText"].ToString() + "&_fid_27=" + FinalTable.Rows[QB]["Silver Pendant"].ToString() + "&_fid_28=" + FinalTable.Rows[QB]["Charm/Engraving"].ToString() + "&_fid_29=" + FinalTable.Rows[QB]["EngravingType"].ToString() + "&_fid_30=" + FinalTable.Rows[QB]["EngravingText"].ToString() + "&_fid_31=" + FinalTable.Rows[QB]["Rapnet Diamond"].ToString() + "&_fid_32=" + FinalTable.Rows[QB]["Anjolee Diamond"].ToString() + "&_fid_33=" + FinalTable.Rows[QB]["Customer Name"].ToString() + "&_fid_34=" + FinalTable.Rows[QB]["Customer email"].ToString() + "&_fid_35=" + FinalTable.Rows[QB]["Billing Phone Number"].ToString() + "&_fid_36=" + FinalTable.Rows[QB]["Bill/Ship Match"].ToString() + "&_fid_37=" + FinalTable.Rows[QB]["Payment Option"].ToString() + "&_fid_38=" + FinalTable.Rows[QB]["Shipping Name"].ToString() + "&_fid_39=" + FinalTable.Rows[QB]["Shipping Address1"].ToString() + "&_fid_40=" + FinalTable.Rows[QB]["Shipping Address2"].ToString() + "&_fid_41=" + FinalTable.Rows[QB]["Shipping Apt/Unit No"].ToString() + "&_fid_42=" + FinalTable.Rows[QB]["Shipping State"].ToString() + "&_fid_43=" + FinalTable.Rows[QB]["Shipping Country"].ToString() + "&_fid_44=" + FinalTable.Rows[QB]["Shipping Phone"].ToString() + "&_fid_45=" + FinalTable.Rows[QB]["Item Length/Ring Size"].ToString() + "&_fid_46=" + FinalTable.Rows[QB]["BandMetalType"].ToString() + "&_fid_47=" + FinalTable.Rows[QB]["VendorChildSku"].ToString() + "&_fid_48=" + FinalTable.Rows[QB]["Model Carat Weight"].ToString() + "&_fid_49=" + FinalTable.Rows[QB]["Effective Carat Weight"].ToString() + "&_fid_50=" + FinalTable.Rows[QB]["Vendor Metal Type"].ToString() + "&_fid_51=" + FinalTable.Rows[QB]["Stone Information Quality"].ToString() + "&_fid_52=" + FinalTable.Rows[QB]["Vendor Feed Price"].ToString() + "&_fid_53=" + FinalTable.Rows[QB]["Cut"].ToString() + "&_fid_54=" + FinalTable.Rows[QB]["Color"].ToString() + "&_fid_55=" + FinalTable.Rows[QB]["Clarity"].ToString() + "&_fid_56=" + FinalTable.Rows[QB]["Depth"].ToString() + "&_fid_57=" + FinalTable.Rows[QB]["Table"].ToString() + "&_fid_58=" + FinalTable.Rows[QB]["Girdle"].ToString() + "&_fid_59=" + FinalTable.Rows[QB]["Symmetry"].ToString() + "&_fid_60=" + FinalTable.Rows[QB]["Polish"].ToString() + "&_fid_61=" + FinalTable.Rows[QB]["CuletSize"].ToString() + "&_fid_62=" + FinalTable.Rows[QB]["FluorescenceIntensity"].ToString() + "&_fid_63=" + FinalTable.Rows[QB]["Lab"].ToString() + "&_fid_64=" + FinalTable.Rows[QB]["DiamondID"].ToString() + "&_fid_65=" + FinalTable.Rows[QB]["Stone 1 Type"].ToString() + "&_fid_66=" + FinalTable.Rows[QB]["Stone 1 Shape"].ToString() + "&_fid_67=" + FinalTable.Rows[QB]["Stone 1 MM"].ToString() + "&_fid_68=" + FinalTable.Rows[QB]["Stone 1 Carat"].ToString() + "&_fid_69=" + FinalTable.Rows[QB]["Stone 1 Qty"].ToString() + "&_fid_70=" + FinalTable.Rows[QB]["Stone 2 Setting"].ToString() + "&_fid_71=" + FinalTable.Rows[QB]["Stone 2 Type"].ToString() + "&_fid_72=" + FinalTable.Rows[QB]["Stone 2 Shape"].ToString() + "&_fid_73=" + FinalTable.Rows[QB]["Stone 2 MM"].ToString() + "&_fid_74=" + FinalTable.Rows[QB]["Stone 2 Carat"].ToString() + "&_fid_75=" + FinalTable.Rows[QB]["Stone 2 Qty"].ToString() + "&_fid_76=" + FinalTable.Rows[QB]["Stone 2 Setting"].ToString() + "&_fid_78=" + FinalTable.Rows[QB]["Stone 3 Type"].ToString() + "&_fid_79=" + FinalTable.Rows[QB]["Stone 3 Shape"].ToString() + "&_fid_80=" + FinalTable.Rows[QB]["Stone 3 MM"].ToString() + "&_fid_81=" + FinalTable.Rows[QB]["Stone 3 Carat"].ToString() + "&_fid_82=" + FinalTable.Rows[QB]["Stone 3 Qty"].ToString() + "&_fid_83=" + FinalTable.Rows[QB]["Stone 3 Setting"].ToString() + "&_fid_84=" + FinalTable.Rows[QB]["Stone 4 Type"].ToString() + "&_fid_85=" + FinalTable.Rows[QB]["Stone 4 Shape"].ToString() + "&_fid_86=" + FinalTable.Rows[QB]["Stone 4 MM"].ToString() + "&_fid_87=" + FinalTable.Rows[QB]["Stone 4 Carat"].ToString() + "&_fid_88=" + FinalTable.Rows[QB]["Stone 4 Qty"].ToString() + "&_fid_89=" + FinalTable.Rows[QB]["Stone 4 Setting"].ToString() + "&_fid_90=" + FinalTable.Rows[QB]["Stone 5 Type"].ToString() + "&_fid_91=" + FinalTable.Rows[QB]["Stone 5 Shape"].ToString() + "&_fid_92=" + FinalTable.Rows[QB]["Stone 5 MM"].ToString() + "&_fid_93=" + FinalTable.Rows[QB]["Stone 5 Carat"].ToString() + "&_fid_94=" + FinalTable.Rows[QB]["Stone 5 Qty"].ToString() + "&_fid_95=" + FinalTable.Rows[QB]["Stone 5 Setting"].ToString() + "&_fid_96=" + FinalTable.Rows[QB]["Stone 6 Type"].ToString() + "&_fid_97=" + FinalTable.Rows[QB]["Stone 6 Shape"].ToString() + "&_fid_98=" + FinalTable.Rows[QB]["Stone 6 MM"].ToString() + "&_fid_99=" + FinalTable.Rows[QB]["Stone 6 Carat"].ToString() + "&_fid_100=" + FinalTable.Rows[QB]["Stone 6 Qty"].ToString() + "&_fid_101=" + FinalTable.Rows[QB]["Stone 6 Setting"].ToString() + "&_fid_102=" + FinalTable.Rows[QB]["Stone 7 Type"].ToString() + "&_fid_103=" + FinalTable.Rows[QB]["Stone 7 Shape"].ToString() + "&_fid_104=" + FinalTable.Rows[QB]["Stone 7 MM"].ToString() + "&_fid_105=" + FinalTable.Rows[QB]["Stone 7 Carat"].ToString() + "&_fid_106=" + FinalTable.Rows[QB]["Stone 7 Qty"].ToString() + "&_fid_107=" + FinalTable.Rows[QB]["Stone 7 Setting"].ToString() + "&_fid_108=" + FinalTable.Rows[QB]["Stone 8 Type"].ToString() + "&_fid_109=" + FinalTable.Rows[QB]["Stone 8 Shape"].ToString() + "&_fid_110=" + FinalTable.Rows[QB]["Stone 8 MM"].ToString() + "&_fid_111=" + FinalTable.Rows[QB]["Stone 8 Carat"].ToString() + "&_fid_112=" + FinalTable.Rows[QB]["Stone 8 Qty"].ToString() + "&_fid_113=" + FinalTable.Rows[QB]["Stone 8 Setting"].ToString() + "&_fid_114=" + FinalTable.Rows[QB]["Stone 9 Type"].ToString() + "&_fid_115=" + FinalTable.Rows[QB]["Stone 9 Shape"].ToString() + "&_fid_116=" + FinalTable.Rows[QB]["Stone 9 MM"].ToString() + "&_fid_117=" + FinalTable.Rows[QB]["Stone 9 Carat"].ToString() + "&_fid_118=" + FinalTable.Rows[QB]["Stone 9 Qty"].ToString() + "&_fid_119=" + FinalTable.Rows[QB]["Stone 9 Setting"].ToString() + "&_fid_120=" + FinalTable.Rows[QB]["Stone 10 Type"].ToString() + "&_fid_121=" + FinalTable.Rows[QB]["Stone 10 Shape"].ToString() + "&_fid_122=" + FinalTable.Rows[QB]["Stone 10 MM"].ToString() + "&_fid_123=" + FinalTable.Rows[QB]["Stone 10 Carat"].ToString() + "&_fid_124=" + FinalTable.Rows[QB]["Stone 10 Qty"].ToString() + "&_fid_125=" + FinalTable.Rows[QB]["Stone 10 Setting"].ToString() + "&_fid_126=" + FinalTable.Rows[QB]["Stone 11 Type"].ToString() + "&_fid_127=" + FinalTable.Rows[QB]["Stone 11 Shape"].ToString() + "&_fid_128=" + FinalTable.Rows[QB]["Stone 11 MM"].ToString() + "&_fid_129=" + FinalTable.Rows[QB]["Stone 11 Carat"].ToString() + "&_fid_130=" + FinalTable.Rows[QB]["Stone 11 Qty"].ToString() + "&_fid_131=" + FinalTable.Rows[QB]["Stone 11 Setting"].ToString() + "&_fid_132=" + FinalTable.Rows[QB]["Stone 12 Type"].ToString() + "&_fid_133=" + FinalTable.Rows[QB]["Stone 12 Shape"].ToString() + "&_fid_134=" + FinalTable.Rows[QB]["Stone 12 MM"].ToString() + "&_fid_135=" + FinalTable.Rows[QB]["Stone 12 Carat"].ToString() + "&_fid_136=" + FinalTable.Rows[QB]["Stone 12 Qty"].ToString() + "&_fid_137=" + FinalTable.Rows[QB]["Stone 12 Setting"].ToString() + "&_fid_138=" + FinalTable.Rows[QB]["Stone 13 Type"].ToString() + "&_fid_139=" + FinalTable.Rows[QB]["Stone 13 Shape"].ToString() + "&_fid_140=" + FinalTable.Rows[QB]["Stone 13 MM"].ToString() + "&_fid_141=" + FinalTable.Rows[QB]["Stone 13 Carat"].ToString() + "&_fid_142=" + FinalTable.Rows[QB]["Stone 13 Qty"].ToString() + "&_fid_143=" + FinalTable.Rows[QB]["Stone 13 Setting"].ToString() + "&_fid_144=" + FinalTable.Rows[QB]["Stone 14 Type"].ToString() + "&_fid_145=" + FinalTable.Rows[QB]["Stone 14 Shape"].ToString() + "&_fid_146=" + FinalTable.Rows[QB]["Stone 14 MM"].ToString() + "&_fid_147=" + FinalTable.Rows[QB]["Stone 14 Carat"].ToString() + "&_fid_148=" + FinalTable.Rows[QB]["Stone 14 Qty"].ToString() + "&_fid_149=" + FinalTable.Rows[QB]["Stone 14 Setting"].ToString() + "&_fid_150=" + FinalTable.Rows[QB]["Stone 15 Type"].ToString() + "&_fid_151=" + FinalTable.Rows[QB]["Stone 16 Shape"].ToString() + "&_fid_152=" + FinalTable.Rows[QB]["Stone 15 MM"].ToString() + "&_fid_153=" + FinalTable.Rows[QB]["Stone 15 Carat"].ToString() + "&_fid_154=" + FinalTable.Rows[QB]["Stone 15 Qty"].ToString() + "&_fid_155=" + FinalTable.Rows[QB]["Stone 15 Setting"].ToString() + "&_fid_245=" + FinalTable.Rows[QB]["Stone 1 Cost"].ToString() + "&_fid_246=" + FinalTable.Rows[QB]["Stone 2 Cost"].ToString() + "&_fid_247=" + FinalTable.Rows[QB]["Stone 3 Cost"].ToString() + "&_fid_248=" + FinalTable.Rows[QB]["Stone 4 Cost"].ToString() + "&_fid_249=" + FinalTable.Rows[QB]["Stone 5 Cost"].ToString() + "&_fid_250=" + FinalTable.Rows[QB]["Stone 6 Cost"].ToString() + "&_fid_251=" + FinalTable.Rows[QB]["Stone 7 Cost"].ToString() + "&_fid_252=" + FinalTable.Rows[QB]["Stone 8 Cost"].ToString() + "&_fid_253=" + FinalTable.Rows[QB]["Stone 9 Cost"].ToString() + "&_fid_254=" + FinalTable.Rows[QB]["Stone 10 Cost"].ToString() + "&_fid_255=" + FinalTable.Rows[QB]["Stone 11 Cost"].ToString() + "&_fid_256=" + FinalTable.Rows[QB]["Stone 12 Cost"].ToString() + "&_fid_257=" + FinalTable.Rows[QB]["Stone 13 Cost"].ToString() + "&_fid_258=" + FinalTable.Rows[QB]["Stone 14 Cost"].ToString() + "&_fid_259=" + FinalTable.Rows[QB]["Stone 15 Cost"].ToString() + "&_fid_77=" + GetWebName + "&_fid_345=" + FinalTable.Rows[QB]["CenterDiamondCarat"].ToString() + "&_fid_346=" + FinalTable.Rows[QB]["CenterDiamondCut"].ToString() + "&_fid_347=" + FinalTable.Rows[QB]["CenterDiamondColor"].ToString() + "&_fid_348=" + FinalTable.Rows[QB]["CenterDiamondClarity"].ToString() + "&_fid_349=" + FinalTable.Rows[QB]["Certificate"].ToString() + "&_fid_369=" + FinalTable.Rows[QB]["SilverPendantType"].ToString() + "&_fid_364=" + OrderPlatform + "&_fid_374=" + FinalTable.Rows[QB]["Semi Mount"].ToString() + "&_fid_381=" + "&_fid_381=" + FinalTable.Rows[QB]["Center_Stone_Shape"].ToString() + "&_fid_384=" + FinalTable.Rows[QB]["CouponCode"].ToString() + "&_fid_385=" + FinalTable.Rows[QB]["DiscountAmount"].ToString() + "&_fid_386=" + FinalTable.Rows[QB]["Sub_Total"].ToString() + "&usertoken=bzuxs9_cw8e_du2n5mbq9t6s7dqgxexu68h53s&apptoken=duby72vbbnfpx4dzsrwtjcbyqwep");

                    }

                    else if (FinalTable.Columns.Contains("Stone 14 Type"))
                    {

                        request = WebRequest.Create("https://nirgolan.quickbase.com/db/bmimrj5vm?a=API_AddRecord" + "&_fid_6=" + FinalTable.Rows[QB]["Order Date"].ToString() + "&_fid_7=" + FinalTable.Rows[QB]["Order number"].ToString() + "&_fid_8=" + FinalTable.Rows[QB]["Amount"].ToString() + "&_fid_9=" + FinalTable.Rows[QB]["Transaction"].ToString() + "&_fid_10=" + FinalTable.Rows[QB]["Payment Method"].ToString() + "&_fid_11=" + FinalTable.Rows[QB]["Amount Before Tax"].ToString() + "&_fid_12=" + FinalTable.Rows[QB]["Tax"].ToString() + "&_fid_13=" + FinalTable.Rows[QB]["Shipping City"].ToString() + "&_fid_14=" + FinalTable.Rows[QB]["Shipping Zip"].ToString() + "&_fid_15=" + FinalTable.Rows[QB]["ShippingService"].ToString() + "&_fid_16=" + FinalTable.Rows[QB]["Customer'sAccounts"].ToString() + "&_fid_17=" + FinalTable.Rows[QB]["Style No"].ToString() + "&_fid_18=" + FinalTable.Rows[QB]["Diamond Quality"].ToString() + "&_fid_19=" + FinalTable.Rows[QB]["Order Quantity"].ToString() + "&_fid_20=" + FinalTable.Rows[QB]["Gram Weight"].ToString() + "&_fid_21=" + FinalTable.Rows[QB]["Metal Karat"].ToString() + "&_fid_22=" + FinalTable.Rows[QB]["Metal Color"].ToString() + "&_fid_23=" + FinalTable.Rows[QB]["Labor Rate"].ToString() + "&_fid_24=" + FinalTable.Rows[QB]["Coupon"].ToString() + "&_fid_25=" + FinalTable.Rows[QB]["IGI"].ToString() + "&_fid_26=" + FinalTable.Rows[QB]["SilverReplicaText"].ToString() + "&_fid_27=" + FinalTable.Rows[QB]["Silver Pendant"].ToString() + "&_fid_28=" + FinalTable.Rows[QB]["Charm/Engraving"].ToString() + "&_fid_29=" + FinalTable.Rows[QB]["EngravingType"].ToString() + "&_fid_30=" + FinalTable.Rows[QB]["EngravingText"].ToString() + "&_fid_31=" + FinalTable.Rows[QB]["Rapnet Diamond"].ToString() + "&_fid_32=" + FinalTable.Rows[QB]["Anjolee Diamond"].ToString() + "&_fid_33=" + FinalTable.Rows[QB]["Customer Name"].ToString() + "&_fid_34=" + FinalTable.Rows[QB]["Customer email"].ToString() + "&_fid_35=" + FinalTable.Rows[QB]["Billing Phone Number"].ToString() + "&_fid_36=" + FinalTable.Rows[QB]["Bill/Ship Match"].ToString() + "&_fid_37=" + FinalTable.Rows[QB]["Payment Option"].ToString() + "&_fid_38=" + FinalTable.Rows[QB]["Shipping Name"].ToString() + "&_fid_39=" + FinalTable.Rows[QB]["Shipping Address1"].ToString() + "&_fid_40=" + FinalTable.Rows[QB]["Shipping Address2"].ToString() + "&_fid_41=" + FinalTable.Rows[QB]["Shipping Apt/Unit No"].ToString() + "&_fid_42=" + FinalTable.Rows[QB]["Shipping State"].ToString() + "&_fid_43=" + FinalTable.Rows[QB]["Shipping Country"].ToString() + "&_fid_44=" + FinalTable.Rows[QB]["Shipping Phone"].ToString() + "&_fid_45=" + FinalTable.Rows[QB]["Item Length/Ring Size"].ToString() + "&_fid_46=" + FinalTable.Rows[QB]["BandMetalType"].ToString() + "&_fid_47=" + FinalTable.Rows[QB]["VendorChildSku"].ToString() + "&_fid_48=" + FinalTable.Rows[QB]["Model Carat Weight"].ToString() + "&_fid_49=" + FinalTable.Rows[QB]["Effective Carat Weight"].ToString() + "&_fid_50=" + FinalTable.Rows[QB]["Vendor Metal Type"].ToString() + "&_fid_51=" + FinalTable.Rows[QB]["Stone Information Quality"].ToString() + "&_fid_52=" + FinalTable.Rows[QB]["Vendor Feed Price"].ToString() + "&_fid_53=" + FinalTable.Rows[QB]["Cut"].ToString() + "&_fid_54=" + FinalTable.Rows[QB]["Color"].ToString() + "&_fid_55=" + FinalTable.Rows[QB]["Clarity"].ToString() + "&_fid_56=" + FinalTable.Rows[QB]["Depth"].ToString() + "&_fid_57=" + FinalTable.Rows[QB]["Table"].ToString() + "&_fid_58=" + FinalTable.Rows[QB]["Girdle"].ToString() + "&_fid_59=" + FinalTable.Rows[QB]["Symmetry"].ToString() + "&_fid_60=" + FinalTable.Rows[QB]["Polish"].ToString() + "&_fid_61=" + FinalTable.Rows[QB]["CuletSize"].ToString() + "&_fid_62=" + FinalTable.Rows[QB]["FluorescenceIntensity"].ToString() + "&_fid_63=" + FinalTable.Rows[QB]["Lab"].ToString() + "&_fid_64=" + FinalTable.Rows[QB]["DiamondID"].ToString() + "&_fid_65=" + FinalTable.Rows[QB]["Stone 1 Type"].ToString() + "&_fid_66=" + FinalTable.Rows[QB]["Stone 1 Shape"].ToString() + "&_fid_67=" + FinalTable.Rows[QB]["Stone 1 MM"].ToString() + "&_fid_68=" + FinalTable.Rows[QB]["Stone 1 Carat"].ToString() + "&_fid_69=" + FinalTable.Rows[QB]["Stone 1 Qty"].ToString() + "&_fid_70=" + FinalTable.Rows[QB]["Stone 2 Setting"].ToString() + "&_fid_71=" + FinalTable.Rows[QB]["Stone 2 Type"].ToString() + "&_fid_72=" + FinalTable.Rows[QB]["Stone 2 Shape"].ToString() + "&_fid_73=" + FinalTable.Rows[QB]["Stone 2 MM"].ToString() + "&_fid_74=" + FinalTable.Rows[QB]["Stone 2 Carat"].ToString() + "&_fid_75=" + FinalTable.Rows[QB]["Stone 2 Qty"].ToString() + "&_fid_76=" + FinalTable.Rows[QB]["Stone 2 Setting"].ToString() + "&_fid_78=" + FinalTable.Rows[QB]["Stone 3 Type"].ToString() + "&_fid_79=" + FinalTable.Rows[QB]["Stone 3 Shape"].ToString() + "&_fid_80=" + FinalTable.Rows[QB]["Stone 3 MM"].ToString() + "&_fid_81=" + FinalTable.Rows[QB]["Stone 3 Carat"].ToString() + "&_fid_82=" + FinalTable.Rows[QB]["Stone 3 Qty"].ToString() + "&_fid_83=" + FinalTable.Rows[QB]["Stone 3 Setting"].ToString() + "&_fid_84=" + FinalTable.Rows[QB]["Stone 4 Type"].ToString() + "&_fid_85=" + FinalTable.Rows[QB]["Stone 4 Shape"].ToString() + "&_fid_86=" + FinalTable.Rows[QB]["Stone 4 MM"].ToString() + "&_fid_87=" + FinalTable.Rows[QB]["Stone 4 Carat"].ToString() + "&_fid_88=" + FinalTable.Rows[QB]["Stone 4 Qty"].ToString() + "&_fid_89=" + FinalTable.Rows[QB]["Stone 4 Setting"].ToString() + "&_fid_90=" + FinalTable.Rows[QB]["Stone 5 Type"].ToString() + "&_fid_91=" + FinalTable.Rows[QB]["Stone 5 Shape"].ToString() + "&_fid_92=" + FinalTable.Rows[QB]["Stone 5 MM"].ToString() + "&_fid_93=" + FinalTable.Rows[QB]["Stone 5 Carat"].ToString() + "&_fid_94=" + FinalTable.Rows[QB]["Stone 5 Qty"].ToString() + "&_fid_95=" + FinalTable.Rows[QB]["Stone 5 Setting"].ToString() + "&_fid_96=" + FinalTable.Rows[QB]["Stone 6 Type"].ToString() + "&_fid_97=" + FinalTable.Rows[QB]["Stone 6 Shape"].ToString() + "&_fid_98=" + FinalTable.Rows[QB]["Stone 6 MM"].ToString() + "&_fid_99=" + FinalTable.Rows[QB]["Stone 6 Carat"].ToString() + "&_fid_100=" + FinalTable.Rows[QB]["Stone 6 Qty"].ToString() + "&_fid_101=" + FinalTable.Rows[QB]["Stone 6 Setting"].ToString() + "&_fid_102=" + FinalTable.Rows[QB]["Stone 7 Type"].ToString() + "&_fid_103=" + FinalTable.Rows[QB]["Stone 7 Shape"].ToString() + "&_fid_104=" + FinalTable.Rows[QB]["Stone 7 MM"].ToString() + "&_fid_105=" + FinalTable.Rows[QB]["Stone 7 Carat"].ToString() + "&_fid_106=" + FinalTable.Rows[QB]["Stone 7 Qty"].ToString() + "&_fid_107=" + FinalTable.Rows[QB]["Stone 7 Setting"].ToString() + "&_fid_108=" + FinalTable.Rows[QB]["Stone 8 Type"].ToString() + "&_fid_109=" + FinalTable.Rows[QB]["Stone 8 Shape"].ToString() + "&_fid_110=" + FinalTable.Rows[QB]["Stone 8 MM"].ToString() + "&_fid_111=" + FinalTable.Rows[QB]["Stone 8 Carat"].ToString() + "&_fid_112=" + FinalTable.Rows[QB]["Stone 8 Qty"].ToString() + "&_fid_113=" + FinalTable.Rows[QB]["Stone 8 Setting"].ToString() + "&_fid_114=" + FinalTable.Rows[QB]["Stone 9 Type"].ToString() + "&_fid_115=" + FinalTable.Rows[QB]["Stone 9 Shape"].ToString() + "&_fid_116=" + FinalTable.Rows[QB]["Stone 9 MM"].ToString() + "&_fid_117=" + FinalTable.Rows[QB]["Stone 9 Carat"].ToString() + "&_fid_118=" + FinalTable.Rows[QB]["Stone 9 Qty"].ToString() + "&_fid_119=" + FinalTable.Rows[QB]["Stone 9 Setting"].ToString() + "&_fid_120=" + FinalTable.Rows[QB]["Stone 10 Type"].ToString() + "&_fid_121=" + FinalTable.Rows[QB]["Stone 10 Shape"].ToString() + "&_fid_122=" + FinalTable.Rows[QB]["Stone 10 MM"].ToString() + "&_fid_123=" + FinalTable.Rows[QB]["Stone 10 Carat"].ToString() + "&_fid_124=" + FinalTable.Rows[QB]["Stone 10 Qty"].ToString() + "&_fid_125=" + FinalTable.Rows[QB]["Stone 10 Setting"].ToString() + "&_fid_126=" + FinalTable.Rows[QB]["Stone 11 Type"].ToString() + "&_fid_127=" + FinalTable.Rows[QB]["Stone 11 Shape"].ToString() + "&_fid_128=" + FinalTable.Rows[QB]["Stone 11 MM"].ToString() + "&_fid_129=" + FinalTable.Rows[QB]["Stone 11 Carat"].ToString() + "&_fid_130=" + FinalTable.Rows[QB]["Stone 11 Qty"].ToString() + "&_fid_131=" + FinalTable.Rows[QB]["Stone 11 Setting"].ToString() + "&_fid_132=" + FinalTable.Rows[QB]["Stone 12 Type"].ToString() + "&_fid_133=" + FinalTable.Rows[QB]["Stone 12 Shape"].ToString() + "&_fid_134=" + FinalTable.Rows[QB]["Stone 12 MM"].ToString() + "&_fid_135=" + FinalTable.Rows[QB]["Stone 12 Carat"].ToString() + "&_fid_136=" + FinalTable.Rows[QB]["Stone 12 Qty"].ToString() + "&_fid_137=" + FinalTable.Rows[QB]["Stone 12 Setting"].ToString() + "&_fid_138=" + FinalTable.Rows[QB]["Stone 13 Type"].ToString() + "&_fid_139=" + FinalTable.Rows[QB]["Stone 13 Shape"].ToString() + "&_fid_140=" + FinalTable.Rows[QB]["Stone 13 MM"].ToString() + "&_fid_141=" + FinalTable.Rows[QB]["Stone 13 Carat"].ToString() + "&_fid_142=" + FinalTable.Rows[QB]["Stone 13 Qty"].ToString() + "&_fid_143=" + FinalTable.Rows[QB]["Stone 13 Setting"].ToString() + "&_fid_144=" + FinalTable.Rows[QB]["Stone 14 Type"].ToString() + "&_fid_145=" + FinalTable.Rows[QB]["Stone 14 Shape"].ToString() + "&_fid_146=" + FinalTable.Rows[QB]["Stone 14 MM"].ToString() + "&_fid_147=" + FinalTable.Rows[QB]["Stone 14 Carat"].ToString() + "&_fid_148=" + FinalTable.Rows[QB]["Stone 14 Qty"].ToString() + "&_fid_149=" + FinalTable.Rows[QB]["Stone 14 Setting"].ToString() + "&_fid_245=" + FinalTable.Rows[QB]["Stone 1 Cost"].ToString() + "&_fid_246=" + FinalTable.Rows[QB]["Stone 2 Cost"].ToString() + "&_fid_247=" + FinalTable.Rows[QB]["Stone 3 Cost"].ToString() + "&_fid_248=" + FinalTable.Rows[QB]["Stone 4 Cost"].ToString() + "&_fid_249=" + FinalTable.Rows[QB]["Stone 5 Cost"].ToString() + "&_fid_250=" + FinalTable.Rows[QB]["Stone 6 Cost"].ToString() + "&_fid_251=" + FinalTable.Rows[QB]["Stone 7 Cost"].ToString() + "&_fid_252=" + FinalTable.Rows[QB]["Stone 8 Cost"].ToString() + "&_fid_253=" + FinalTable.Rows[QB]["Stone 9 Cost"].ToString() + "&_fid_254=" + FinalTable.Rows[QB]["Stone 10 Cost"].ToString() + "&_fid_255=" + FinalTable.Rows[QB]["Stone 11 Cost"].ToString() + "&_fid_256=" + FinalTable.Rows[QB]["Stone 12 Cost"].ToString() + "&_fid_257=" + FinalTable.Rows[QB]["Stone 13 Cost"].ToString() + "&_fid_258=" + FinalTable.Rows[QB]["Stone 14 Cost"].ToString() + "&_fid_77=" + GetWebName + "&_fid_345=" + FinalTable.Rows[QB]["CenterDiamondCarat"].ToString() + "&_fid_346=" + FinalTable.Rows[QB]["CenterDiamondCut"].ToString() + "&_fid_347=" + FinalTable.Rows[QB]["CenterDiamondColor"].ToString() + "&_fid_348=" + FinalTable.Rows[QB]["CenterDiamondClarity"].ToString() + "&_fid_349=" + FinalTable.Rows[QB]["Certificate"].ToString() + "&_fid_369=" + FinalTable.Rows[QB]["SilverPendantType"].ToString() + "&_fid_364=" + OrderPlatform + "&_fid_374=" + FinalTable.Rows[QB]["Semi Mount"].ToString() + "&_fid_381=" + "&_fid_381=" + FinalTable.Rows[QB]["Center_Stone_Shape"].ToString() + "&_fid_384=" + FinalTable.Rows[QB]["CouponCode"].ToString() + "&_fid_385=" + FinalTable.Rows[QB]["DiscountAmount"].ToString() + "&_fid_386=" + FinalTable.Rows[QB]["Sub_Total"].ToString() + "&usertoken=bzuxs9_cw8e_du2n5mbq9t6s7dqgxexu68h53s&apptoken=duby72vbbnfpx4dzsrwtjcbyqwep");

                    }


                    else if (FinalTable.Columns.Contains("Stone 13 Type"))
                    {

                        request = WebRequest.Create("https://nirgolan.quickbase.com/db/bmimrj5vm?a=API_AddRecord" + "&_fid_6=" + FinalTable.Rows[QB]["Order Date"].ToString() + "&_fid_7=" + FinalTable.Rows[QB]["Order number"].ToString() + "&_fid_8=" + FinalTable.Rows[QB]["Amount"].ToString() + "&_fid_9=" + FinalTable.Rows[QB]["Transaction"].ToString() + "&_fid_10=" + FinalTable.Rows[QB]["Payment Method"].ToString() + "&_fid_11=" + FinalTable.Rows[QB]["Amount Before Tax"].ToString() + "&_fid_12=" + FinalTable.Rows[QB]["Tax"].ToString() + "&_fid_13=" + FinalTable.Rows[QB]["Shipping City"].ToString() + "&_fid_14=" + FinalTable.Rows[QB]["Shipping Zip"].ToString() + "&_fid_15=" + FinalTable.Rows[QB]["ShippingService"].ToString() + "&_fid_16=" + FinalTable.Rows[QB]["Customer'sAccounts"].ToString() + "&_fid_17=" + FinalTable.Rows[QB]["Style No"].ToString() + "&_fid_18=" + FinalTable.Rows[QB]["Diamond Quality"].ToString() + "&_fid_19=" + FinalTable.Rows[QB]["Order Quantity"].ToString() + "&_fid_20=" + FinalTable.Rows[QB]["Gram Weight"].ToString() + "&_fid_21=" + FinalTable.Rows[QB]["Metal Karat"].ToString() + "&_fid_22=" + FinalTable.Rows[QB]["Metal Color"].ToString() + "&_fid_23=" + FinalTable.Rows[QB]["Labor Rate"].ToString() + "&_fid_24=" + FinalTable.Rows[QB]["Coupon"].ToString() + "&_fid_25=" + FinalTable.Rows[QB]["IGI"].ToString() + "&_fid_26=" + FinalTable.Rows[QB]["SilverReplicaText"].ToString() + "&_fid_27=" + FinalTable.Rows[QB]["Silver Pendant"].ToString() + "&_fid_28=" + FinalTable.Rows[QB]["Charm/Engraving"].ToString() + "&_fid_29=" + FinalTable.Rows[QB]["EngravingType"].ToString() + "&_fid_30=" + FinalTable.Rows[QB]["EngravingText"].ToString() + "&_fid_31=" + FinalTable.Rows[QB]["Rapnet Diamond"].ToString() + "&_fid_32=" + FinalTable.Rows[QB]["Anjolee Diamond"].ToString() + "&_fid_33=" + FinalTable.Rows[QB]["Customer Name"].ToString() + "&_fid_34=" + FinalTable.Rows[QB]["Customer email"].ToString() + "&_fid_35=" + FinalTable.Rows[QB]["Billing Phone Number"].ToString() + "&_fid_36=" + FinalTable.Rows[QB]["Bill/Ship Match"].ToString() + "&_fid_37=" + FinalTable.Rows[QB]["Payment Option"].ToString() + "&_fid_38=" + FinalTable.Rows[QB]["Shipping Name"].ToString() + "&_fid_39=" + FinalTable.Rows[QB]["Shipping Address1"].ToString() + "&_fid_40=" + FinalTable.Rows[QB]["Shipping Address2"].ToString() + "&_fid_41=" + FinalTable.Rows[QB]["Shipping Apt/Unit No"].ToString() + "&_fid_42=" + FinalTable.Rows[QB]["Shipping State"].ToString() + "&_fid_43=" + FinalTable.Rows[QB]["Shipping Country"].ToString() + "&_fid_44=" + FinalTable.Rows[QB]["Shipping Phone"].ToString() + "&_fid_45=" + FinalTable.Rows[QB]["Item Length/Ring Size"].ToString() + "&_fid_46=" + FinalTable.Rows[QB]["BandMetalType"].ToString() + "&_fid_47=" + FinalTable.Rows[QB]["VendorChildSku"].ToString() + "&_fid_48=" + FinalTable.Rows[QB]["Model Carat Weight"].ToString() + "&_fid_49=" + FinalTable.Rows[QB]["Effective Carat Weight"].ToString() + "&_fid_50=" + FinalTable.Rows[QB]["Vendor Metal Type"].ToString() + "&_fid_51=" + FinalTable.Rows[QB]["Stone Information Quality"].ToString() + "&_fid_52=" + FinalTable.Rows[QB]["Vendor Feed Price"].ToString() + "&_fid_53=" + FinalTable.Rows[QB]["Cut"].ToString() + "&_fid_54=" + FinalTable.Rows[QB]["Color"].ToString() + "&_fid_55=" + FinalTable.Rows[QB]["Clarity"].ToString() + "&_fid_56=" + FinalTable.Rows[QB]["Depth"].ToString() + "&_fid_57=" + FinalTable.Rows[QB]["Table"].ToString() + "&_fid_58=" + FinalTable.Rows[QB]["Girdle"].ToString() + "&_fid_59=" + FinalTable.Rows[QB]["Symmetry"].ToString() + "&_fid_60=" + FinalTable.Rows[QB]["Polish"].ToString() + "&_fid_61=" + FinalTable.Rows[QB]["CuletSize"].ToString() + "&_fid_62=" + FinalTable.Rows[QB]["FluorescenceIntensity"].ToString() + "&_fid_63=" + FinalTable.Rows[QB]["Lab"].ToString() + "&_fid_64=" + FinalTable.Rows[QB]["DiamondID"].ToString() + "&_fid_65=" + FinalTable.Rows[QB]["Stone 1 Type"].ToString() + "&_fid_66=" + FinalTable.Rows[QB]["Stone 1 Shape"].ToString() + "&_fid_67=" + FinalTable.Rows[QB]["Stone 1 MM"].ToString() + "&_fid_68=" + FinalTable.Rows[QB]["Stone 1 Carat"].ToString() + "&_fid_69=" + FinalTable.Rows[QB]["Stone 1 Qty"].ToString() + "&_fid_70=" + FinalTable.Rows[QB]["Stone 2 Setting"].ToString() + "&_fid_71=" + FinalTable.Rows[QB]["Stone 2 Type"].ToString() + "&_fid_72=" + FinalTable.Rows[QB]["Stone 2 Shape"].ToString() + "&_fid_73=" + FinalTable.Rows[QB]["Stone 2 MM"].ToString() + "&_fid_74=" + FinalTable.Rows[QB]["Stone 2 Carat"].ToString() + "&_fid_75=" + FinalTable.Rows[QB]["Stone 2 Qty"].ToString() + "&_fid_76=" + FinalTable.Rows[QB]["Stone 2 Setting"].ToString() + "&_fid_78=" + FinalTable.Rows[QB]["Stone 3 Type"].ToString() + "&_fid_79=" + FinalTable.Rows[QB]["Stone 3 Shape"].ToString() + "&_fid_80=" + FinalTable.Rows[QB]["Stone 3 MM"].ToString() + "&_fid_81=" + FinalTable.Rows[QB]["Stone 3 Carat"].ToString() + "&_fid_82=" + FinalTable.Rows[QB]["Stone 3 Qty"].ToString() + "&_fid_83=" + FinalTable.Rows[QB]["Stone 3 Setting"].ToString() + "&_fid_84=" + FinalTable.Rows[QB]["Stone 4 Type"].ToString() + "&_fid_85=" + FinalTable.Rows[QB]["Stone 4 Shape"].ToString() + "&_fid_86=" + FinalTable.Rows[QB]["Stone 4 MM"].ToString() + "&_fid_87=" + FinalTable.Rows[QB]["Stone 4 Carat"].ToString() + "&_fid_88=" + FinalTable.Rows[QB]["Stone 4 Qty"].ToString() + "&_fid_89=" + FinalTable.Rows[QB]["Stone 4 Setting"].ToString() + "&_fid_90=" + FinalTable.Rows[QB]["Stone 5 Type"].ToString() + "&_fid_91=" + FinalTable.Rows[QB]["Stone 5 Shape"].ToString() + "&_fid_92=" + FinalTable.Rows[QB]["Stone 5 MM"].ToString() + "&_fid_93=" + FinalTable.Rows[QB]["Stone 5 Carat"].ToString() + "&_fid_94=" + FinalTable.Rows[QB]["Stone 5 Qty"].ToString() + "&_fid_95=" + FinalTable.Rows[QB]["Stone 5 Setting"].ToString() + "&_fid_96=" + FinalTable.Rows[QB]["Stone 6 Type"].ToString() + "&_fid_97=" + FinalTable.Rows[QB]["Stone 6 Shape"].ToString() + "&_fid_98=" + FinalTable.Rows[QB]["Stone 6 MM"].ToString() + "&_fid_99=" + FinalTable.Rows[QB]["Stone 6 Carat"].ToString() + "&_fid_100=" + FinalTable.Rows[QB]["Stone 6 Qty"].ToString() + "&_fid_101=" + FinalTable.Rows[QB]["Stone 6 Setting"].ToString() + "&_fid_102=" + FinalTable.Rows[QB]["Stone 7 Type"].ToString() + "&_fid_103=" + FinalTable.Rows[QB]["Stone 7 Shape"].ToString() + "&_fid_104=" + FinalTable.Rows[QB]["Stone 7 MM"].ToString() + "&_fid_105=" + FinalTable.Rows[QB]["Stone 7 Carat"].ToString() + "&_fid_106=" + FinalTable.Rows[QB]["Stone 7 Qty"].ToString() + "&_fid_107=" + FinalTable.Rows[QB]["Stone 7 Setting"].ToString() + "&_fid_108=" + FinalTable.Rows[QB]["Stone 8 Type"].ToString() + "&_fid_109=" + FinalTable.Rows[QB]["Stone 8 Shape"].ToString() + "&_fid_110=" + FinalTable.Rows[QB]["Stone 8 MM"].ToString() + "&_fid_111=" + FinalTable.Rows[QB]["Stone 8 Carat"].ToString() + "&_fid_112=" + FinalTable.Rows[QB]["Stone 8 Qty"].ToString() + "&_fid_113=" + FinalTable.Rows[QB]["Stone 8 Setting"].ToString() + "&_fid_114=" + FinalTable.Rows[QB]["Stone 9 Type"].ToString() + "&_fid_115=" + FinalTable.Rows[QB]["Stone 9 Shape"].ToString() + "&_fid_116=" + FinalTable.Rows[QB]["Stone 9 MM"].ToString() + "&_fid_117=" + FinalTable.Rows[QB]["Stone 9 Carat"].ToString() + "&_fid_118=" + FinalTable.Rows[QB]["Stone 9 Qty"].ToString() + "&_fid_119=" + FinalTable.Rows[QB]["Stone 9 Setting"].ToString() + "&_fid_120=" + FinalTable.Rows[QB]["Stone 10 Type"].ToString() + "&_fid_121=" + FinalTable.Rows[QB]["Stone 10 Shape"].ToString() + "&_fid_122=" + FinalTable.Rows[QB]["Stone 10 MM"].ToString() + "&_fid_123=" + FinalTable.Rows[QB]["Stone 10 Carat"].ToString() + "&_fid_124=" + FinalTable.Rows[QB]["Stone 10 Qty"].ToString() + "&_fid_125=" + FinalTable.Rows[QB]["Stone 10 Setting"].ToString() + "&_fid_126=" + FinalTable.Rows[QB]["Stone 11 Type"].ToString() + "&_fid_127=" + FinalTable.Rows[QB]["Stone 11 Shape"].ToString() + "&_fid_128=" + FinalTable.Rows[QB]["Stone 11 MM"].ToString() + "&_fid_129=" + FinalTable.Rows[QB]["Stone 11 Carat"].ToString() + "&_fid_130=" + FinalTable.Rows[QB]["Stone 11 Qty"].ToString() + "&_fid_131=" + FinalTable.Rows[QB]["Stone 11 Setting"].ToString() + "&_fid_132=" + FinalTable.Rows[QB]["Stone 12 Type"].ToString() + "&_fid_133=" + FinalTable.Rows[QB]["Stone 12 Shape"].ToString() + "&_fid_134=" + FinalTable.Rows[QB]["Stone 12 MM"].ToString() + "&_fid_135=" + FinalTable.Rows[QB]["Stone 12 Carat"].ToString() + "&_fid_136=" + FinalTable.Rows[QB]["Stone 12 Qty"].ToString() + "&_fid_137=" + FinalTable.Rows[QB]["Stone 12 Setting"].ToString() + "&_fid_138=" + FinalTable.Rows[QB]["Stone 13 Type"].ToString() + "&_fid_139=" + FinalTable.Rows[QB]["Stone 13 Shape"].ToString() + "&_fid_140=" + FinalTable.Rows[QB]["Stone 13 MM"].ToString() + "&_fid_141=" + FinalTable.Rows[QB]["Stone 13 Carat"].ToString() + "&_fid_142=" + FinalTable.Rows[QB]["Stone 13 Qty"].ToString() + "&_fid_143=" + FinalTable.Rows[QB]["Stone 13 Setting"].ToString() + "&_fid_245=" + FinalTable.Rows[QB]["Stone 1 Cost"].ToString() + "&_fid_246=" + FinalTable.Rows[QB]["Stone 2 Cost"].ToString() + "&_fid_247=" + FinalTable.Rows[QB]["Stone 3 Cost"].ToString() + "&_fid_248=" + FinalTable.Rows[QB]["Stone 4 Cost"].ToString() + "&_fid_249=" + FinalTable.Rows[QB]["Stone 5 Cost"].ToString() + "&_fid_250=" + FinalTable.Rows[QB]["Stone 6 Cost"].ToString() + "&_fid_251=" + FinalTable.Rows[QB]["Stone 7 Cost"].ToString() + "&_fid_252=" + FinalTable.Rows[QB]["Stone 8 Cost"].ToString() + "&_fid_253=" + FinalTable.Rows[QB]["Stone 9 Cost"].ToString() + "&_fid_254=" + FinalTable.Rows[QB]["Stone 10 Cost"].ToString() + "&_fid_255=" + FinalTable.Rows[QB]["Stone 11 Cost"].ToString() + "&_fid_256=" + FinalTable.Rows[QB]["Stone 12 Cost"].ToString() + "&_fid_257=" + FinalTable.Rows[QB]["Stone 13 Cost"].ToString() + "&_fid_77=" + GetWebName + "&_fid_345=" + FinalTable.Rows[QB]["CenterDiamondCarat"].ToString() + "&_fid_346=" + FinalTable.Rows[QB]["CenterDiamondCut"].ToString() + "&_fid_347=" + FinalTable.Rows[QB]["CenterDiamondColor"].ToString() + "&_fid_348=" + FinalTable.Rows[QB]["CenterDiamondClarity"].ToString() + "&_fid_349=" + FinalTable.Rows[QB]["Certificate"].ToString() + "&_fid_369=" + FinalTable.Rows[QB]["SilverPendantType"].ToString() + "&_fid_364=" + OrderPlatform + "&_fid_374=" + FinalTable.Rows[QB]["Semi Mount"].ToString() + "&_fid_381=" + "&_fid_381=" + FinalTable.Rows[QB]["Center_Stone_Shape"].ToString() + "&_fid_384=" + FinalTable.Rows[QB]["CouponCode"].ToString() + "&_fid_385=" + FinalTable.Rows[QB]["DiscountAmount"].ToString() + "&_fid_386=" + FinalTable.Rows[QB]["Sub_Total"].ToString() + "&usertoken=bzuxs9_cw8e_du2n5mbq9t6s7dqgxexu68h53s&apptoken=duby72vbbnfpx4dzsrwtjcbyqwep");

                    }


                    else if (FinalTable.Columns.Contains("Stone 12 Type"))
                    {

                        request = WebRequest.Create("https://nirgolan.quickbase.com/db/bmimrj5vm?a=API_AddRecord" + "&_fid_6=" + FinalTable.Rows[QB]["Order Date"].ToString() + "&_fid_7=" + FinalTable.Rows[QB]["Order number"].ToString() + "&_fid_8=" + FinalTable.Rows[QB]["Amount"].ToString() + "&_fid_9=" + FinalTable.Rows[QB]["Transaction"].ToString() + "&_fid_10=" + FinalTable.Rows[QB]["Payment Method"].ToString() + "&_fid_11=" + FinalTable.Rows[QB]["Amount Before Tax"].ToString() + "&_fid_12=" + FinalTable.Rows[QB]["Tax"].ToString() + "&_fid_13=" + FinalTable.Rows[QB]["Shipping City"].ToString() + "&_fid_14=" + FinalTable.Rows[QB]["Shipping Zip"].ToString() + "&_fid_15=" + FinalTable.Rows[QB]["ShippingService"].ToString() + "&_fid_16=" + FinalTable.Rows[QB]["Customer'sAccounts"].ToString() + "&_fid_17=" + FinalTable.Rows[QB]["Style No"].ToString() + "&_fid_18=" + FinalTable.Rows[QB]["Diamond Quality"].ToString() + "&_fid_19=" + FinalTable.Rows[QB]["Order Quantity"].ToString() + "&_fid_20=" + FinalTable.Rows[QB]["Gram Weight"].ToString() + "&_fid_21=" + FinalTable.Rows[QB]["Metal Karat"].ToString() + "&_fid_22=" + FinalTable.Rows[QB]["Metal Color"].ToString() + "&_fid_23=" + FinalTable.Rows[QB]["Labor Rate"].ToString() + "&_fid_24=" + FinalTable.Rows[QB]["Coupon"].ToString() + "&_fid_25=" + FinalTable.Rows[QB]["IGI"].ToString() + "&_fid_26=" + FinalTable.Rows[QB]["SilverReplicaText"].ToString() + "&_fid_27=" + FinalTable.Rows[QB]["Silver Pendant"].ToString() + "&_fid_28=" + FinalTable.Rows[QB]["Charm/Engraving"].ToString() + "&_fid_29=" + FinalTable.Rows[QB]["EngravingType"].ToString() + "&_fid_30=" + FinalTable.Rows[QB]["EngravingText"].ToString() + "&_fid_31=" + FinalTable.Rows[QB]["Rapnet Diamond"].ToString() + "&_fid_32=" + FinalTable.Rows[QB]["Anjolee Diamond"].ToString() + "&_fid_33=" + FinalTable.Rows[QB]["Customer Name"].ToString() + "&_fid_34=" + FinalTable.Rows[QB]["Customer email"].ToString() + "&_fid_35=" + FinalTable.Rows[QB]["Billing Phone Number"].ToString() + "&_fid_36=" + FinalTable.Rows[QB]["Bill/Ship Match"].ToString() + "&_fid_37=" + FinalTable.Rows[QB]["Payment Option"].ToString() + "&_fid_38=" + FinalTable.Rows[QB]["Shipping Name"].ToString() + "&_fid_39=" + FinalTable.Rows[QB]["Shipping Address1"].ToString() + "&_fid_40=" + FinalTable.Rows[QB]["Shipping Address2"].ToString() + "&_fid_41=" + FinalTable.Rows[QB]["Shipping Apt/Unit No"].ToString() + "&_fid_42=" + FinalTable.Rows[QB]["Shipping State"].ToString() + "&_fid_43=" + FinalTable.Rows[QB]["Shipping Country"].ToString() + "&_fid_44=" + FinalTable.Rows[QB]["Shipping Phone"].ToString() + "&_fid_45=" + FinalTable.Rows[QB]["Item Length/Ring Size"].ToString() + "&_fid_46=" + FinalTable.Rows[QB]["BandMetalType"].ToString() + "&_fid_47=" + FinalTable.Rows[QB]["VendorChildSku"].ToString() + "&_fid_48=" + FinalTable.Rows[QB]["Model Carat Weight"].ToString() + "&_fid_49=" + FinalTable.Rows[QB]["Effective Carat Weight"].ToString() + "&_fid_50=" + FinalTable.Rows[QB]["Vendor Metal Type"].ToString() + "&_fid_51=" + FinalTable.Rows[QB]["Stone Information Quality"].ToString() + "&_fid_52=" + FinalTable.Rows[QB]["Vendor Feed Price"].ToString() + "&_fid_53=" + FinalTable.Rows[QB]["Cut"].ToString() + "&_fid_54=" + FinalTable.Rows[QB]["Color"].ToString() + "&_fid_55=" + FinalTable.Rows[QB]["Clarity"].ToString() + "&_fid_56=" + FinalTable.Rows[QB]["Depth"].ToString() + "&_fid_57=" + FinalTable.Rows[QB]["Table"].ToString() + "&_fid_58=" + FinalTable.Rows[QB]["Girdle"].ToString() + "&_fid_59=" + FinalTable.Rows[QB]["Symmetry"].ToString() + "&_fid_60=" + FinalTable.Rows[QB]["Polish"].ToString() + "&_fid_61=" + FinalTable.Rows[QB]["CuletSize"].ToString() + "&_fid_62=" + FinalTable.Rows[QB]["FluorescenceIntensity"].ToString() + "&_fid_63=" + FinalTable.Rows[QB]["Lab"].ToString() + "&_fid_64=" + FinalTable.Rows[QB]["DiamondID"].ToString() + "&_fid_65=" + FinalTable.Rows[QB]["Stone 1 Type"].ToString() + "&_fid_66=" + FinalTable.Rows[QB]["Stone 1 Shape"].ToString() + "&_fid_67=" + FinalTable.Rows[QB]["Stone 1 MM"].ToString() + "&_fid_68=" + FinalTable.Rows[QB]["Stone 1 Carat"].ToString() + "&_fid_69=" + FinalTable.Rows[QB]["Stone 1 Qty"].ToString() + "&_fid_70=" + FinalTable.Rows[QB]["Stone 2 Setting"].ToString() + "&_fid_71=" + FinalTable.Rows[QB]["Stone 2 Type"].ToString() + "&_fid_72=" + FinalTable.Rows[QB]["Stone 2 Shape"].ToString() + "&_fid_73=" + FinalTable.Rows[QB]["Stone 2 MM"].ToString() + "&_fid_74=" + FinalTable.Rows[QB]["Stone 2 Carat"].ToString() + "&_fid_75=" + FinalTable.Rows[QB]["Stone 2 Qty"].ToString() + "&_fid_76=" + FinalTable.Rows[QB]["Stone 2 Setting"].ToString() + "&_fid_78=" + FinalTable.Rows[QB]["Stone 3 Type"].ToString() + "&_fid_79=" + FinalTable.Rows[QB]["Stone 3 Shape"].ToString() + "&_fid_80=" + FinalTable.Rows[QB]["Stone 3 MM"].ToString() + "&_fid_81=" + FinalTable.Rows[QB]["Stone 3 Carat"].ToString() + "&_fid_82=" + FinalTable.Rows[QB]["Stone 3 Qty"].ToString() + "&_fid_83=" + FinalTable.Rows[QB]["Stone 3 Setting"].ToString() + "&_fid_84=" + FinalTable.Rows[QB]["Stone 4 Type"].ToString() + "&_fid_85=" + FinalTable.Rows[QB]["Stone 4 Shape"].ToString() + "&_fid_86=" + FinalTable.Rows[QB]["Stone 4 MM"].ToString() + "&_fid_87=" + FinalTable.Rows[QB]["Stone 4 Carat"].ToString() + "&_fid_88=" + FinalTable.Rows[QB]["Stone 4 Qty"].ToString() + "&_fid_89=" + FinalTable.Rows[QB]["Stone 4 Setting"].ToString() + "&_fid_90=" + FinalTable.Rows[QB]["Stone 5 Type"].ToString() + "&_fid_91=" + FinalTable.Rows[QB]["Stone 5 Shape"].ToString() + "&_fid_92=" + FinalTable.Rows[QB]["Stone 5 MM"].ToString() + "&_fid_93=" + FinalTable.Rows[QB]["Stone 5 Carat"].ToString() + "&_fid_94=" + FinalTable.Rows[QB]["Stone 5 Qty"].ToString() + "&_fid_95=" + FinalTable.Rows[QB]["Stone 5 Setting"].ToString() + "&_fid_96=" + FinalTable.Rows[QB]["Stone 6 Type"].ToString() + "&_fid_97=" + FinalTable.Rows[QB]["Stone 6 Shape"].ToString() + "&_fid_98=" + FinalTable.Rows[QB]["Stone 6 MM"].ToString() + "&_fid_99=" + FinalTable.Rows[QB]["Stone 6 Carat"].ToString() + "&_fid_100=" + FinalTable.Rows[QB]["Stone 6 Qty"].ToString() + "&_fid_101=" + FinalTable.Rows[QB]["Stone 6 Setting"].ToString() + "&_fid_102=" + FinalTable.Rows[QB]["Stone 7 Type"].ToString() + "&_fid_103=" + FinalTable.Rows[QB]["Stone 7 Shape"].ToString() + "&_fid_104=" + FinalTable.Rows[QB]["Stone 7 MM"].ToString() + "&_fid_105=" + FinalTable.Rows[QB]["Stone 7 Carat"].ToString() + "&_fid_106=" + FinalTable.Rows[QB]["Stone 7 Qty"].ToString() + "&_fid_107=" + FinalTable.Rows[QB]["Stone 7 Setting"].ToString() + "&_fid_108=" + FinalTable.Rows[QB]["Stone 8 Type"].ToString() + "&_fid_109=" + FinalTable.Rows[QB]["Stone 8 Shape"].ToString() + "&_fid_110=" + FinalTable.Rows[QB]["Stone 8 MM"].ToString() + "&_fid_111=" + FinalTable.Rows[QB]["Stone 8 Carat"].ToString() + "&_fid_112=" + FinalTable.Rows[QB]["Stone 8 Qty"].ToString() + "&_fid_113=" + FinalTable.Rows[QB]["Stone 8 Setting"].ToString() + "&_fid_114=" + FinalTable.Rows[QB]["Stone 9 Type"].ToString() + "&_fid_115=" + FinalTable.Rows[QB]["Stone 9 Shape"].ToString() + "&_fid_116=" + FinalTable.Rows[QB]["Stone 9 MM"].ToString() + "&_fid_117=" + FinalTable.Rows[QB]["Stone 9 Carat"].ToString() + "&_fid_118=" + FinalTable.Rows[QB]["Stone 9 Qty"].ToString() + "&_fid_119=" + FinalTable.Rows[QB]["Stone 9 Setting"].ToString() + "&_fid_120=" + FinalTable.Rows[QB]["Stone 10 Type"].ToString() + "&_fid_121=" + FinalTable.Rows[QB]["Stone 10 Shape"].ToString() + "&_fid_122=" + FinalTable.Rows[QB]["Stone 10 MM"].ToString() + "&_fid_123=" + FinalTable.Rows[QB]["Stone 10 Carat"].ToString() + "&_fid_124=" + FinalTable.Rows[QB]["Stone 10 Qty"].ToString() + "&_fid_125=" + FinalTable.Rows[QB]["Stone 10 Setting"].ToString() + "&_fid_126=" + FinalTable.Rows[QB]["Stone 11 Type"].ToString() + "&_fid_127=" + FinalTable.Rows[QB]["Stone 11 Shape"].ToString() + "&_fid_128=" + FinalTable.Rows[QB]["Stone 11 MM"].ToString() + "&_fid_129=" + FinalTable.Rows[QB]["Stone 11 Carat"].ToString() + "&_fid_130=" + FinalTable.Rows[QB]["Stone 11 Qty"].ToString() + "&_fid_131=" + FinalTable.Rows[QB]["Stone 11 Setting"].ToString() + "&_fid_132=" + FinalTable.Rows[QB]["Stone 12 Type"].ToString() + "&_fid_133=" + FinalTable.Rows[QB]["Stone 12 Shape"].ToString() + "&_fid_134=" + FinalTable.Rows[QB]["Stone 12 MM"].ToString() + "&_fid_135=" + FinalTable.Rows[QB]["Stone 12 Carat"].ToString() + "&_fid_136=" + FinalTable.Rows[QB]["Stone 12 Qty"].ToString() + "&_fid_137=" + FinalTable.Rows[QB]["Stone 12 Setting"].ToString() + "&_fid_245=" + FinalTable.Rows[QB]["Stone 1 Cost"].ToString() + "&_fid_246=" + FinalTable.Rows[QB]["Stone 2 Cost"].ToString() + "&_fid_247=" + FinalTable.Rows[QB]["Stone 3 Cost"].ToString() + "&_fid_248=" + FinalTable.Rows[QB]["Stone 4 Cost"].ToString() + "&_fid_249=" + FinalTable.Rows[QB]["Stone 5 Cost"].ToString() + "&_fid_250=" + FinalTable.Rows[QB]["Stone 6 Cost"].ToString() + "&_fid_251=" + FinalTable.Rows[QB]["Stone 7 Cost"].ToString() + "&_fid_252=" + FinalTable.Rows[QB]["Stone 8 Cost"].ToString() + "&_fid_253=" + FinalTable.Rows[QB]["Stone 9 Cost"].ToString() + "&_fid_254=" + FinalTable.Rows[QB]["Stone 10 Cost"].ToString() + "&_fid_255=" + FinalTable.Rows[QB]["Stone 11 Cost"].ToString() + "&_fid_256=" + FinalTable.Rows[QB]["Stone 12 Cost"].ToString() + "&_fid_77=" + GetWebName + "&_fid_345=" + FinalTable.Rows[QB]["CenterDiamondCarat"].ToString() + "&_fid_346=" + FinalTable.Rows[QB]["CenterDiamondCut"].ToString() + "&_fid_347=" + FinalTable.Rows[QB]["CenterDiamondColor"].ToString() + "&_fid_348=" + FinalTable.Rows[QB]["CenterDiamondClarity"].ToString() + "&_fid_349=" + FinalTable.Rows[QB]["Certificate"].ToString() + "&_fid_369=" + FinalTable.Rows[QB]["SilverPendantType"].ToString() + "&_fid_364=" + OrderPlatform + "&_fid_374=" + FinalTable.Rows[QB]["Semi Mount"].ToString() + "&_fid_381=" + "&_fid_381=" + FinalTable.Rows[QB]["Center_Stone_Shape"].ToString() + "&_fid_384=" + FinalTable.Rows[QB]["CouponCode"].ToString() + "&_fid_385=" + FinalTable.Rows[QB]["DiscountAmount"].ToString() + "&_fid_386=" + FinalTable.Rows[QB]["Sub_Total"].ToString() + "&usertoken=bzuxs9_cw8e_du2n5mbq9t6s7dqgxexu68h53s&apptoken=duby72vbbnfpx4dzsrwtjcbyqwep");

                    }

                    else if (FinalTable.Columns.Contains("Stone 11 Type"))
                    {

                        request = WebRequest.Create("https://nirgolan.quickbase.com/db/bmimrj5vm?a=API_AddRecord" + "&_fid_6=" + FinalTable.Rows[QB]["Order Date"].ToString() + "&_fid_7=" + FinalTable.Rows[QB]["Order number"].ToString() + "&_fid_8=" + FinalTable.Rows[QB]["Amount"].ToString() + "&_fid_9=" + FinalTable.Rows[QB]["Transaction"].ToString() + "&_fid_10=" + FinalTable.Rows[QB]["Payment Method"].ToString() + "&_fid_11=" + FinalTable.Rows[QB]["Amount Before Tax"].ToString() + "&_fid_12=" + FinalTable.Rows[QB]["Tax"].ToString() + "&_fid_13=" + FinalTable.Rows[QB]["Shipping City"].ToString() + "&_fid_14=" + FinalTable.Rows[QB]["Shipping Zip"].ToString() + "&_fid_15=" + FinalTable.Rows[QB]["ShippingService"].ToString() + "&_fid_16=" + FinalTable.Rows[QB]["Customer'sAccounts"].ToString() + "&_fid_17=" + FinalTable.Rows[QB]["Style No"].ToString() + "&_fid_18=" + FinalTable.Rows[QB]["Diamond Quality"].ToString() + "&_fid_19=" + FinalTable.Rows[QB]["Order Quantity"].ToString() + "&_fid_20=" + FinalTable.Rows[QB]["Gram Weight"].ToString() + "&_fid_21=" + FinalTable.Rows[QB]["Metal Karat"].ToString() + "&_fid_22=" + FinalTable.Rows[QB]["Metal Color"].ToString() + "&_fid_23=" + FinalTable.Rows[QB]["Labor Rate"].ToString() + "&_fid_24=" + FinalTable.Rows[QB]["Coupon"].ToString() + "&_fid_25=" + FinalTable.Rows[QB]["IGI"].ToString() + "&_fid_26=" + FinalTable.Rows[QB]["SilverReplicaText"].ToString() + "&_fid_27=" + FinalTable.Rows[QB]["Silver Pendant"].ToString() + "&_fid_28=" + FinalTable.Rows[QB]["Charm/Engraving"].ToString() + "&_fid_29=" + FinalTable.Rows[QB]["EngravingType"].ToString() + "&_fid_30=" + FinalTable.Rows[QB]["EngravingText"].ToString() + "&_fid_31=" + FinalTable.Rows[QB]["Rapnet Diamond"].ToString() + "&_fid_32=" + FinalTable.Rows[QB]["Anjolee Diamond"].ToString() + "&_fid_33=" + FinalTable.Rows[QB]["Customer Name"].ToString() + "&_fid_34=" + FinalTable.Rows[QB]["Customer email"].ToString() + "&_fid_35=" + FinalTable.Rows[QB]["Billing Phone Number"].ToString() + "&_fid_36=" + FinalTable.Rows[QB]["Bill/Ship Match"].ToString() + "&_fid_37=" + FinalTable.Rows[QB]["Payment Option"].ToString() + "&_fid_38=" + FinalTable.Rows[QB]["Shipping Name"].ToString() + "&_fid_39=" + FinalTable.Rows[QB]["Shipping Address1"].ToString() + "&_fid_40=" + FinalTable.Rows[QB]["Shipping Address2"].ToString() + "&_fid_41=" + FinalTable.Rows[QB]["Shipping Apt/Unit No"].ToString() + "&_fid_42=" + FinalTable.Rows[QB]["Shipping State"].ToString() + "&_fid_43=" + FinalTable.Rows[QB]["Shipping Country"].ToString() + "&_fid_44=" + FinalTable.Rows[QB]["Shipping Phone"].ToString() + "&_fid_45=" + FinalTable.Rows[QB]["Item Length/Ring Size"].ToString() + "&_fid_46=" + FinalTable.Rows[QB]["BandMetalType"].ToString() + "&_fid_47=" + FinalTable.Rows[QB]["VendorChildSku"].ToString() + "&_fid_48=" + FinalTable.Rows[QB]["Model Carat Weight"].ToString() + "&_fid_49=" + FinalTable.Rows[QB]["Effective Carat Weight"].ToString() + "&_fid_50=" + FinalTable.Rows[QB]["Vendor Metal Type"].ToString() + "&_fid_51=" + FinalTable.Rows[QB]["Stone Information Quality"].ToString() + "&_fid_52=" + FinalTable.Rows[QB]["Vendor Feed Price"].ToString() + "&_fid_53=" + FinalTable.Rows[QB]["Cut"].ToString() + "&_fid_54=" + FinalTable.Rows[QB]["Color"].ToString() + "&_fid_55=" + FinalTable.Rows[QB]["Clarity"].ToString() + "&_fid_56=" + FinalTable.Rows[QB]["Depth"].ToString() + "&_fid_57=" + FinalTable.Rows[QB]["Table"].ToString() + "&_fid_58=" + FinalTable.Rows[QB]["Girdle"].ToString() + "&_fid_59=" + FinalTable.Rows[QB]["Symmetry"].ToString() + "&_fid_60=" + FinalTable.Rows[QB]["Polish"].ToString() + "&_fid_61=" + FinalTable.Rows[QB]["CuletSize"].ToString() + "&_fid_62=" + FinalTable.Rows[QB]["FluorescenceIntensity"].ToString() + "&_fid_63=" + FinalTable.Rows[QB]["Lab"].ToString() + "&_fid_64=" + FinalTable.Rows[QB]["DiamondID"].ToString() + "&_fid_65=" + FinalTable.Rows[QB]["Stone 1 Type"].ToString() + "&_fid_66=" + FinalTable.Rows[QB]["Stone 1 Shape"].ToString() + "&_fid_67=" + FinalTable.Rows[QB]["Stone 1 MM"].ToString() + "&_fid_68=" + FinalTable.Rows[QB]["Stone 1 Carat"].ToString() + "&_fid_69=" + FinalTable.Rows[QB]["Stone 1 Qty"].ToString() + "&_fid_70=" + FinalTable.Rows[QB]["Stone 2 Setting"].ToString() + "&_fid_71=" + FinalTable.Rows[QB]["Stone 2 Type"].ToString() + "&_fid_72=" + FinalTable.Rows[QB]["Stone 2 Shape"].ToString() + "&_fid_73=" + FinalTable.Rows[QB]["Stone 2 MM"].ToString() + "&_fid_74=" + FinalTable.Rows[QB]["Stone 2 Carat"].ToString() + "&_fid_75=" + FinalTable.Rows[QB]["Stone 2 Qty"].ToString() + "&_fid_76=" + FinalTable.Rows[QB]["Stone 2 Setting"].ToString() + "&_fid_78=" + FinalTable.Rows[QB]["Stone 3 Type"].ToString() + "&_fid_79=" + FinalTable.Rows[QB]["Stone 3 Shape"].ToString() + "&_fid_80=" + FinalTable.Rows[QB]["Stone 3 MM"].ToString() + "&_fid_81=" + FinalTable.Rows[QB]["Stone 3 Carat"].ToString() + "&_fid_82=" + FinalTable.Rows[QB]["Stone 3 Qty"].ToString() + "&_fid_83=" + FinalTable.Rows[QB]["Stone 3 Setting"].ToString() + "&_fid_84=" + FinalTable.Rows[QB]["Stone 4 Type"].ToString() + "&_fid_85=" + FinalTable.Rows[QB]["Stone 4 Shape"].ToString() + "&_fid_86=" + FinalTable.Rows[QB]["Stone 4 MM"].ToString() + "&_fid_87=" + FinalTable.Rows[QB]["Stone 4 Carat"].ToString() + "&_fid_88=" + FinalTable.Rows[QB]["Stone 4 Qty"].ToString() + "&_fid_89=" + FinalTable.Rows[QB]["Stone 4 Setting"].ToString() + "&_fid_90=" + FinalTable.Rows[QB]["Stone 5 Type"].ToString() + "&_fid_91=" + FinalTable.Rows[QB]["Stone 5 Shape"].ToString() + "&_fid_92=" + FinalTable.Rows[QB]["Stone 5 MM"].ToString() + "&_fid_93=" + FinalTable.Rows[QB]["Stone 5 Carat"].ToString() + "&_fid_94=" + FinalTable.Rows[QB]["Stone 5 Qty"].ToString() + "&_fid_95=" + FinalTable.Rows[QB]["Stone 5 Setting"].ToString() + "&_fid_96=" + FinalTable.Rows[QB]["Stone 6 Type"].ToString() + "&_fid_97=" + FinalTable.Rows[QB]["Stone 6 Shape"].ToString() + "&_fid_98=" + FinalTable.Rows[QB]["Stone 6 MM"].ToString() + "&_fid_99=" + FinalTable.Rows[QB]["Stone 6 Carat"].ToString() + "&_fid_100=" + FinalTable.Rows[QB]["Stone 6 Qty"].ToString() + "&_fid_101=" + FinalTable.Rows[QB]["Stone 6 Setting"].ToString() + "&_fid_102=" + FinalTable.Rows[QB]["Stone 7 Type"].ToString() + "&_fid_103=" + FinalTable.Rows[QB]["Stone 7 Shape"].ToString() + "&_fid_104=" + FinalTable.Rows[QB]["Stone 7 MM"].ToString() + "&_fid_105=" + FinalTable.Rows[QB]["Stone 7 Carat"].ToString() + "&_fid_106=" + FinalTable.Rows[QB]["Stone 7 Qty"].ToString() + "&_fid_107=" + FinalTable.Rows[QB]["Stone 7 Setting"].ToString() + "&_fid_108=" + FinalTable.Rows[QB]["Stone 8 Type"].ToString() + "&_fid_109=" + FinalTable.Rows[QB]["Stone 8 Shape"].ToString() + "&_fid_110=" + FinalTable.Rows[QB]["Stone 8 MM"].ToString() + "&_fid_111=" + FinalTable.Rows[QB]["Stone 8 Carat"].ToString() + "&_fid_112=" + FinalTable.Rows[QB]["Stone 8 Qty"].ToString() + "&_fid_113=" + FinalTable.Rows[QB]["Stone 8 Setting"].ToString() + "&_fid_114=" + FinalTable.Rows[QB]["Stone 9 Type"].ToString() + "&_fid_115=" + FinalTable.Rows[QB]["Stone 9 Shape"].ToString() + "&_fid_116=" + FinalTable.Rows[QB]["Stone 9 MM"].ToString() + "&_fid_117=" + FinalTable.Rows[QB]["Stone 9 Carat"].ToString() + "&_fid_118=" + FinalTable.Rows[QB]["Stone 9 Qty"].ToString() + "&_fid_119=" + FinalTable.Rows[QB]["Stone 9 Setting"].ToString() + "&_fid_120=" + FinalTable.Rows[QB]["Stone 10 Type"].ToString() + "&_fid_121=" + FinalTable.Rows[QB]["Stone 10 Shape"].ToString() + "&_fid_122=" + FinalTable.Rows[QB]["Stone 10 MM"].ToString() + "&_fid_123=" + FinalTable.Rows[QB]["Stone 10 Carat"].ToString() + "&_fid_124=" + FinalTable.Rows[QB]["Stone 10 Qty"].ToString() + "&_fid_125=" + FinalTable.Rows[QB]["Stone 10 Setting"].ToString() + "&_fid_126=" + FinalTable.Rows[QB]["Stone 11 Type"].ToString() + "&_fid_127=" + FinalTable.Rows[QB]["Stone 11 Shape"].ToString() + "&_fid_128=" + FinalTable.Rows[QB]["Stone 11 MM"].ToString() + "&_fid_129=" + FinalTable.Rows[QB]["Stone 11 Carat"].ToString() + "&_fid_130=" + FinalTable.Rows[QB]["Stone 11 Qty"].ToString() + "&_fid_131=" + FinalTable.Rows[QB]["Stone 11 Setting"].ToString() + "&_fid_245=" + FinalTable.Rows[QB]["Stone 1 Cost"].ToString() + "&_fid_246=" + FinalTable.Rows[QB]["Stone 2 Cost"].ToString() + "&_fid_247=" + FinalTable.Rows[QB]["Stone 3 Cost"].ToString() + "&_fid_248=" + FinalTable.Rows[QB]["Stone 4 Cost"].ToString() + "&_fid_249=" + FinalTable.Rows[QB]["Stone 5 Cost"].ToString() + "&_fid_250=" + FinalTable.Rows[QB]["Stone 6 Cost"].ToString() + "&_fid_251=" + FinalTable.Rows[QB]["Stone 7 Cost"].ToString() + "&_fid_252=" + FinalTable.Rows[QB]["Stone 8 Cost"].ToString() + "&_fid_253=" + FinalTable.Rows[QB]["Stone 9 Cost"].ToString() + "&_fid_254=" + FinalTable.Rows[QB]["Stone 10 Cost"].ToString() + "&_fid_255=" + FinalTable.Rows[QB]["Stone 11 Cost"].ToString() + "&_fid_77=" + GetWebName + "&_fid_345=" + FinalTable.Rows[QB]["CenterDiamondCarat"].ToString() + "&_fid_346=" + FinalTable.Rows[QB]["CenterDiamondCut"].ToString() + "&_fid_347=" + FinalTable.Rows[QB]["CenterDiamondColor"].ToString() + "&_fid_348=" + FinalTable.Rows[QB]["CenterDiamondClarity"].ToString() + "&_fid_349=" + FinalTable.Rows[QB]["Certificate"].ToString() + "&_fid_369=" + FinalTable.Rows[QB]["SilverPendantType"].ToString() + "&_fid_364=" + OrderPlatform + "&_fid_374=" + FinalTable.Rows[QB]["Semi Mount"].ToString() + "&_fid_381=" + "&_fid_381=" + FinalTable.Rows[QB]["Center_Stone_Shape"].ToString() + "&_fid_384=" + FinalTable.Rows[QB]["CouponCode"].ToString() + "&_fid_385=" + FinalTable.Rows[QB]["DiscountAmount"].ToString() + "&_fid_386=" + FinalTable.Rows[QB]["Sub_Total"].ToString() + "&usertoken=bzuxs9_cw8e_du2n5mbq9t6s7dqgxexu68h53s&apptoken=duby72vbbnfpx4dzsrwtjcbyqwep");

                    }


                    else if (FinalTable.Columns.Contains("Stone 10 Type"))
                    {

                        request = WebRequest.Create("https://nirgolan.quickbase.com/db/bmimrj5vm?a=API_AddRecord" + "&_fid_6=" + FinalTable.Rows[QB]["Order Date"].ToString() + "&_fid_7=" + FinalTable.Rows[QB]["Order number"].ToString() + "&_fid_8=" + FinalTable.Rows[QB]["Amount"].ToString() + "&_fid_9=" + FinalTable.Rows[QB]["Transaction"].ToString() + "&_fid_10=" + FinalTable.Rows[QB]["Payment Method"].ToString() + "&_fid_11=" + FinalTable.Rows[QB]["Amount Before Tax"].ToString() + "&_fid_12=" + FinalTable.Rows[QB]["Tax"].ToString() + "&_fid_13=" + FinalTable.Rows[QB]["Shipping City"].ToString() + "&_fid_14=" + FinalTable.Rows[QB]["Shipping Zip"].ToString() + "&_fid_15=" + FinalTable.Rows[QB]["ShippingService"].ToString() + "&_fid_16=" + FinalTable.Rows[QB]["Customer'sAccounts"].ToString() + "&_fid_17=" + FinalTable.Rows[QB]["Style No"].ToString() + "&_fid_18=" + FinalTable.Rows[QB]["Diamond Quality"].ToString() + "&_fid_19=" + FinalTable.Rows[QB]["Order Quantity"].ToString() + "&_fid_20=" + FinalTable.Rows[QB]["Gram Weight"].ToString() + "&_fid_21=" + FinalTable.Rows[QB]["Metal Karat"].ToString() + "&_fid_22=" + FinalTable.Rows[QB]["Metal Color"].ToString() + "&_fid_23=" + FinalTable.Rows[QB]["Labor Rate"].ToString() + "&_fid_24=" + FinalTable.Rows[QB]["Coupon"].ToString() + "&_fid_25=" + FinalTable.Rows[QB]["IGI"].ToString() + "&_fid_26=" + FinalTable.Rows[QB]["SilverReplicaText"].ToString() + "&_fid_27=" + FinalTable.Rows[QB]["Silver Pendant"].ToString() + "&_fid_28=" + FinalTable.Rows[QB]["Charm/Engraving"].ToString() + "&_fid_29=" + FinalTable.Rows[QB]["EngravingType"].ToString() + "&_fid_30=" + FinalTable.Rows[QB]["EngravingText"].ToString() + "&_fid_31=" + FinalTable.Rows[QB]["Rapnet Diamond"].ToString() + "&_fid_32=" + FinalTable.Rows[QB]["Anjolee Diamond"].ToString() + "&_fid_33=" + FinalTable.Rows[QB]["Customer Name"].ToString() + "&_fid_34=" + FinalTable.Rows[QB]["Customer email"].ToString() + "&_fid_35=" + FinalTable.Rows[QB]["Billing Phone Number"].ToString() + "&_fid_36=" + FinalTable.Rows[QB]["Bill/Ship Match"].ToString() + "&_fid_37=" + FinalTable.Rows[QB]["Payment Option"].ToString() + "&_fid_38=" + FinalTable.Rows[QB]["Shipping Name"].ToString() + "&_fid_39=" + FinalTable.Rows[QB]["Shipping Address1"].ToString() + "&_fid_40=" + FinalTable.Rows[QB]["Shipping Address2"].ToString() + "&_fid_41=" + FinalTable.Rows[QB]["Shipping Apt/Unit No"].ToString() + "&_fid_42=" + FinalTable.Rows[QB]["Shipping State"].ToString() + "&_fid_43=" + FinalTable.Rows[QB]["Shipping Country"].ToString() + "&_fid_44=" + FinalTable.Rows[QB]["Shipping Phone"].ToString() + "&_fid_45=" + FinalTable.Rows[QB]["Item Length/Ring Size"].ToString() + "&_fid_46=" + FinalTable.Rows[QB]["BandMetalType"].ToString() + "&_fid_47=" + FinalTable.Rows[QB]["VendorChildSku"].ToString() + "&_fid_48=" + FinalTable.Rows[QB]["Model Carat Weight"].ToString() + "&_fid_49=" + FinalTable.Rows[QB]["Effective Carat Weight"].ToString() + "&_fid_50=" + FinalTable.Rows[QB]["Vendor Metal Type"].ToString() + "&_fid_51=" + FinalTable.Rows[QB]["Stone Information Quality"].ToString() + "&_fid_52=" + FinalTable.Rows[QB]["Vendor Feed Price"].ToString() + "&_fid_53=" + FinalTable.Rows[QB]["Cut"].ToString() + "&_fid_54=" + FinalTable.Rows[QB]["Color"].ToString() + "&_fid_55=" + FinalTable.Rows[QB]["Clarity"].ToString() + "&_fid_56=" + FinalTable.Rows[QB]["Depth"].ToString() + "&_fid_57=" + FinalTable.Rows[QB]["Table"].ToString() + "&_fid_58=" + FinalTable.Rows[QB]["Girdle"].ToString() + "&_fid_59=" + FinalTable.Rows[QB]["Symmetry"].ToString() + "&_fid_60=" + FinalTable.Rows[QB]["Polish"].ToString() + "&_fid_61=" + FinalTable.Rows[QB]["CuletSize"].ToString() + "&_fid_62=" + FinalTable.Rows[QB]["FluorescenceIntensity"].ToString() + "&_fid_63=" + FinalTable.Rows[QB]["Lab"].ToString() + "&_fid_64=" + FinalTable.Rows[QB]["DiamondID"].ToString() + "&_fid_65=" + FinalTable.Rows[QB]["Stone 1 Type"].ToString() + "&_fid_66=" + FinalTable.Rows[QB]["Stone 1 Shape"].ToString() + "&_fid_67=" + FinalTable.Rows[QB]["Stone 1 MM"].ToString() + "&_fid_68=" + FinalTable.Rows[QB]["Stone 1 Carat"].ToString() + "&_fid_69=" + FinalTable.Rows[QB]["Stone 1 Qty"].ToString() + "&_fid_70=" + FinalTable.Rows[QB]["Stone 2 Setting"].ToString() + "&_fid_71=" + FinalTable.Rows[QB]["Stone 2 Type"].ToString() + "&_fid_72=" + FinalTable.Rows[QB]["Stone 2 Shape"].ToString() + "&_fid_73=" + FinalTable.Rows[QB]["Stone 2 MM"].ToString() + "&_fid_74=" + FinalTable.Rows[QB]["Stone 2 Carat"].ToString() + "&_fid_75=" + FinalTable.Rows[QB]["Stone 2 Qty"].ToString() + "&_fid_76=" + FinalTable.Rows[QB]["Stone 2 Setting"].ToString() + "&_fid_78=" + FinalTable.Rows[QB]["Stone 3 Type"].ToString() + "&_fid_79=" + FinalTable.Rows[QB]["Stone 3 Shape"].ToString() + "&_fid_80=" + FinalTable.Rows[QB]["Stone 3 MM"].ToString() + "&_fid_81=" + FinalTable.Rows[QB]["Stone 3 Carat"].ToString() + "&_fid_82=" + FinalTable.Rows[QB]["Stone 3 Qty"].ToString() + "&_fid_83=" + FinalTable.Rows[QB]["Stone 3 Setting"].ToString() + "&_fid_84=" + FinalTable.Rows[QB]["Stone 4 Type"].ToString() + "&_fid_85=" + FinalTable.Rows[QB]["Stone 4 Shape"].ToString() + "&_fid_86=" + FinalTable.Rows[QB]["Stone 4 MM"].ToString() + "&_fid_87=" + FinalTable.Rows[QB]["Stone 4 Carat"].ToString() + "&_fid_88=" + FinalTable.Rows[QB]["Stone 4 Qty"].ToString() + "&_fid_89=" + FinalTable.Rows[QB]["Stone 4 Setting"].ToString() + "&_fid_90=" + FinalTable.Rows[QB]["Stone 5 Type"].ToString() + "&_fid_91=" + FinalTable.Rows[QB]["Stone 5 Shape"].ToString() + "&_fid_92=" + FinalTable.Rows[QB]["Stone 5 MM"].ToString() + "&_fid_93=" + FinalTable.Rows[QB]["Stone 5 Carat"].ToString() + "&_fid_94=" + FinalTable.Rows[QB]["Stone 5 Qty"].ToString() + "&_fid_95=" + FinalTable.Rows[QB]["Stone 5 Setting"].ToString() + "&_fid_96=" + FinalTable.Rows[QB]["Stone 6 Type"].ToString() + "&_fid_97=" + FinalTable.Rows[QB]["Stone 6 Shape"].ToString() + "&_fid_98=" + FinalTable.Rows[QB]["Stone 6 MM"].ToString() + "&_fid_99=" + FinalTable.Rows[QB]["Stone 6 Carat"].ToString() + "&_fid_100=" + FinalTable.Rows[QB]["Stone 6 Qty"].ToString() + "&_fid_101=" + FinalTable.Rows[QB]["Stone 6 Setting"].ToString() + "&_fid_102=" + FinalTable.Rows[QB]["Stone 7 Type"].ToString() + "&_fid_103=" + FinalTable.Rows[QB]["Stone 7 Shape"].ToString() + "&_fid_104=" + FinalTable.Rows[QB]["Stone 7 MM"].ToString() + "&_fid_105=" + FinalTable.Rows[QB]["Stone 7 Carat"].ToString() + "&_fid_106=" + FinalTable.Rows[QB]["Stone 7 Qty"].ToString() + "&_fid_107=" + FinalTable.Rows[QB]["Stone 7 Setting"].ToString() + "&_fid_108=" + FinalTable.Rows[QB]["Stone 8 Type"].ToString() + "&_fid_109=" + FinalTable.Rows[QB]["Stone 8 Shape"].ToString() + "&_fid_110=" + FinalTable.Rows[QB]["Stone 8 MM"].ToString() + "&_fid_111=" + FinalTable.Rows[QB]["Stone 8 Carat"].ToString() + "&_fid_112=" + FinalTable.Rows[QB]["Stone 8 Qty"].ToString() + "&_fid_113=" + FinalTable.Rows[QB]["Stone 8 Setting"].ToString() + "&_fid_114=" + FinalTable.Rows[QB]["Stone 9 Type"].ToString() + "&_fid_115=" + FinalTable.Rows[QB]["Stone 9 Shape"].ToString() + "&_fid_116=" + FinalTable.Rows[QB]["Stone 9 MM"].ToString() + "&_fid_117=" + FinalTable.Rows[QB]["Stone 9 Carat"].ToString() + "&_fid_118=" + FinalTable.Rows[QB]["Stone 9 Qty"].ToString() + "&_fid_119=" + FinalTable.Rows[QB]["Stone 9 Setting"].ToString() + "&_fid_120=" + FinalTable.Rows[QB]["Stone 10 Type"].ToString() + "&_fid_121=" + FinalTable.Rows[QB]["Stone 10 Shape"].ToString() + "&_fid_122=" + FinalTable.Rows[QB]["Stone 10 MM"].ToString() + "&_fid_123=" + FinalTable.Rows[QB]["Stone 10 Carat"].ToString() + "&_fid_124=" + FinalTable.Rows[QB]["Stone 10 Qty"].ToString() + "&_fid_125=" + FinalTable.Rows[QB]["Stone 10 Setting"].ToString() + "&_fid_245=" + FinalTable.Rows[QB]["Stone 1 Cost"].ToString() + "&_fid_246=" + FinalTable.Rows[QB]["Stone 2 Cost"].ToString() + "&_fid_247=" + FinalTable.Rows[QB]["Stone 3 Cost"].ToString() + "&_fid_248=" + FinalTable.Rows[QB]["Stone 4 Cost"].ToString() + "&_fid_249=" + FinalTable.Rows[QB]["Stone 5 Cost"].ToString() + "&_fid_250=" + FinalTable.Rows[QB]["Stone 6 Cost"].ToString() + "&_fid_251=" + FinalTable.Rows[QB]["Stone 7 Cost"].ToString() + "&_fid_252=" + FinalTable.Rows[QB]["Stone 8 Cost"].ToString() + "&_fid_253=" + FinalTable.Rows[QB]["Stone 9 Cost"].ToString() + "&_fid_254=" + FinalTable.Rows[QB]["Stone 10 Cost"].ToString() + "&_fid_77=" + GetWebName + "&_fid_345=" + FinalTable.Rows[QB]["CenterDiamondCarat"].ToString() + "&_fid_346=" + FinalTable.Rows[QB]["CenterDiamondCut"].ToString() + "&_fid_347=" + FinalTable.Rows[QB]["CenterDiamondColor"].ToString() + "&_fid_348=" + FinalTable.Rows[QB]["CenterDiamondClarity"].ToString() + "&_fid_349=" + FinalTable.Rows[QB]["Certificate"].ToString() + "&_fid_369=" + FinalTable.Rows[QB]["SilverPendantType"].ToString() + "&_fid_364=" + OrderPlatform + "&_fid_374=" + FinalTable.Rows[QB]["Semi Mount"].ToString() + "&_fid_381=" + "&_fid_381=" + FinalTable.Rows[QB]["Center_Stone_Shape"].ToString() + "&_fid_384=" + FinalTable.Rows[QB]["CouponCode"].ToString() + "&_fid_385=" + FinalTable.Rows[QB]["DiscountAmount"].ToString() + "&_fid_386=" + FinalTable.Rows[QB]["Sub_Total"].ToString() + "&usertoken=bzuxs9_cw8e_du2n5mbq9t6s7dqgxexu68h53s&apptoken=duby72vbbnfpx4dzsrwtjcbyqwep");

                    }

                    else if (FinalTable.Columns.Contains("Stone 9 Type"))
                    {

                        request = WebRequest.Create("https://nirgolan.quickbase.com/db/bmimrj5vm?a=API_AddRecord" + "&_fid_6=" + FinalTable.Rows[QB]["Order Date"].ToString() + "&_fid_7=" + FinalTable.Rows[QB]["Order number"].ToString() + "&_fid_8=" + FinalTable.Rows[QB]["Amount"].ToString() + "&_fid_9=" + FinalTable.Rows[QB]["Transaction"].ToString() + "&_fid_10=" + FinalTable.Rows[QB]["Payment Method"].ToString() + "&_fid_11=" + FinalTable.Rows[QB]["Amount Before Tax"].ToString() + "&_fid_12=" + FinalTable.Rows[QB]["Tax"].ToString() + "&_fid_13=" + FinalTable.Rows[QB]["Shipping City"].ToString() + "&_fid_14=" + FinalTable.Rows[QB]["Shipping Zip"].ToString() + "&_fid_15=" + FinalTable.Rows[QB]["ShippingService"].ToString() + "&_fid_16=" + FinalTable.Rows[QB]["Customer'sAccounts"].ToString() + "&_fid_17=" + FinalTable.Rows[QB]["Style No"].ToString() + "&_fid_18=" + FinalTable.Rows[QB]["Diamond Quality"].ToString() + "&_fid_19=" + FinalTable.Rows[QB]["Order Quantity"].ToString() + "&_fid_20=" + FinalTable.Rows[QB]["Gram Weight"].ToString() + "&_fid_21=" + FinalTable.Rows[QB]["Metal Karat"].ToString() + "&_fid_22=" + FinalTable.Rows[QB]["Metal Color"].ToString() + "&_fid_23=" + FinalTable.Rows[QB]["Labor Rate"].ToString() + "&_fid_24=" + FinalTable.Rows[QB]["Coupon"].ToString() + "&_fid_25=" + FinalTable.Rows[QB]["IGI"].ToString() + "&_fid_26=" + FinalTable.Rows[QB]["SilverReplicaText"].ToString() + "&_fid_27=" + FinalTable.Rows[QB]["Silver Pendant"].ToString() + "&_fid_28=" + FinalTable.Rows[QB]["Charm/Engraving"].ToString() + "&_fid_29=" + FinalTable.Rows[QB]["EngravingType"].ToString() + "&_fid_30=" + FinalTable.Rows[QB]["EngravingText"].ToString() + "&_fid_31=" + FinalTable.Rows[QB]["Rapnet Diamond"].ToString() + "&_fid_32=" + FinalTable.Rows[QB]["Anjolee Diamond"].ToString() + "&_fid_33=" + FinalTable.Rows[QB]["Customer Name"].ToString() + "&_fid_34=" + FinalTable.Rows[QB]["Customer email"].ToString() + "&_fid_35=" + FinalTable.Rows[QB]["Billing Phone Number"].ToString() + "&_fid_36=" + FinalTable.Rows[QB]["Bill/Ship Match"].ToString() + "&_fid_37=" + FinalTable.Rows[QB]["Payment Option"].ToString() + "&_fid_38=" + FinalTable.Rows[QB]["Shipping Name"].ToString() + "&_fid_39=" + FinalTable.Rows[QB]["Shipping Address1"].ToString() + "&_fid_40=" + FinalTable.Rows[QB]["Shipping Address2"].ToString() + "&_fid_41=" + FinalTable.Rows[QB]["Shipping Apt/Unit No"].ToString() + "&_fid_42=" + FinalTable.Rows[QB]["Shipping State"].ToString() + "&_fid_43=" + FinalTable.Rows[QB]["Shipping Country"].ToString() + "&_fid_44=" + FinalTable.Rows[QB]["Shipping Phone"].ToString() + "&_fid_45=" + FinalTable.Rows[QB]["Item Length/Ring Size"].ToString() + "&_fid_46=" + FinalTable.Rows[QB]["BandMetalType"].ToString() + "&_fid_47=" + FinalTable.Rows[QB]["VendorChildSku"].ToString() + "&_fid_48=" + FinalTable.Rows[QB]["Model Carat Weight"].ToString() + "&_fid_49=" + FinalTable.Rows[QB]["Effective Carat Weight"].ToString() + "&_fid_50=" + FinalTable.Rows[QB]["Vendor Metal Type"].ToString() + "&_fid_51=" + FinalTable.Rows[QB]["Stone Information Quality"].ToString() + "&_fid_52=" + FinalTable.Rows[QB]["Vendor Feed Price"].ToString() + "&_fid_53=" + FinalTable.Rows[QB]["Cut"].ToString() + "&_fid_54=" + FinalTable.Rows[QB]["Color"].ToString() + "&_fid_55=" + FinalTable.Rows[QB]["Clarity"].ToString() + "&_fid_56=" + FinalTable.Rows[QB]["Depth"].ToString() + "&_fid_57=" + FinalTable.Rows[QB]["Table"].ToString() + "&_fid_58=" + FinalTable.Rows[QB]["Girdle"].ToString() + "&_fid_59=" + FinalTable.Rows[QB]["Symmetry"].ToString() + "&_fid_60=" + FinalTable.Rows[QB]["Polish"].ToString() + "&_fid_61=" + FinalTable.Rows[QB]["CuletSize"].ToString() + "&_fid_62=" + FinalTable.Rows[QB]["FluorescenceIntensity"].ToString() + "&_fid_63=" + FinalTable.Rows[QB]["Lab"].ToString() + "&_fid_64=" + FinalTable.Rows[QB]["DiamondID"].ToString() + "&_fid_65=" + FinalTable.Rows[QB]["Stone 1 Type"].ToString() + "&_fid_66=" + FinalTable.Rows[QB]["Stone 1 Shape"].ToString() + "&_fid_67=" + FinalTable.Rows[QB]["Stone 1 MM"].ToString() + "&_fid_68=" + FinalTable.Rows[QB]["Stone 1 Carat"].ToString() + "&_fid_69=" + FinalTable.Rows[QB]["Stone 1 Qty"].ToString() + "&_fid_70=" + FinalTable.Rows[QB]["Stone 2 Setting"].ToString() + "&_fid_71=" + FinalTable.Rows[QB]["Stone 2 Type"].ToString() + "&_fid_72=" + FinalTable.Rows[QB]["Stone 2 Shape"].ToString() + "&_fid_73=" + FinalTable.Rows[QB]["Stone 2 MM"].ToString() + "&_fid_74=" + FinalTable.Rows[QB]["Stone 2 Carat"].ToString() + "&_fid_75=" + FinalTable.Rows[QB]["Stone 2 Qty"].ToString() + "&_fid_76=" + FinalTable.Rows[QB]["Stone 2 Setting"].ToString() + "&_fid_78=" + FinalTable.Rows[QB]["Stone 3 Type"].ToString() + "&_fid_79=" + FinalTable.Rows[QB]["Stone 3 Shape"].ToString() + "&_fid_80=" + FinalTable.Rows[QB]["Stone 3 MM"].ToString() + "&_fid_81=" + FinalTable.Rows[QB]["Stone 3 Carat"].ToString() + "&_fid_82=" + FinalTable.Rows[QB]["Stone 3 Qty"].ToString() + "&_fid_83=" + FinalTable.Rows[QB]["Stone 3 Setting"].ToString() + "&_fid_84=" + FinalTable.Rows[QB]["Stone 4 Type"].ToString() + "&_fid_85=" + FinalTable.Rows[QB]["Stone 4 Shape"].ToString() + "&_fid_86=" + FinalTable.Rows[QB]["Stone 4 MM"].ToString() + "&_fid_87=" + FinalTable.Rows[QB]["Stone 4 Carat"].ToString() + "&_fid_88=" + FinalTable.Rows[QB]["Stone 4 Qty"].ToString() + "&_fid_89=" + FinalTable.Rows[QB]["Stone 4 Setting"].ToString() + "&_fid_90=" + FinalTable.Rows[QB]["Stone 5 Type"].ToString() + "&_fid_91=" + FinalTable.Rows[QB]["Stone 5 Shape"].ToString() + "&_fid_92=" + FinalTable.Rows[QB]["Stone 5 MM"].ToString() + "&_fid_93=" + FinalTable.Rows[QB]["Stone 5 Carat"].ToString() + "&_fid_94=" + FinalTable.Rows[QB]["Stone 5 Qty"].ToString() + "&_fid_95=" + FinalTable.Rows[QB]["Stone 5 Setting"].ToString() + "&_fid_96=" + FinalTable.Rows[QB]["Stone 6 Type"].ToString() + "&_fid_97=" + FinalTable.Rows[QB]["Stone 6 Shape"].ToString() + "&_fid_98=" + FinalTable.Rows[QB]["Stone 6 MM"].ToString() + "&_fid_99=" + FinalTable.Rows[QB]["Stone 6 Carat"].ToString() + "&_fid_100=" + FinalTable.Rows[QB]["Stone 6 Qty"].ToString() + "&_fid_101=" + FinalTable.Rows[QB]["Stone 6 Setting"].ToString() + "&_fid_102=" + FinalTable.Rows[QB]["Stone 7 Type"].ToString() + "&_fid_103=" + FinalTable.Rows[QB]["Stone 7 Shape"].ToString() + "&_fid_104=" + FinalTable.Rows[QB]["Stone 7 MM"].ToString() + "&_fid_105=" + FinalTable.Rows[QB]["Stone 7 Carat"].ToString() + "&_fid_106=" + FinalTable.Rows[QB]["Stone 7 Qty"].ToString() + "&_fid_107=" + FinalTable.Rows[QB]["Stone 7 Setting"].ToString() + "&_fid_108=" + FinalTable.Rows[QB]["Stone 8 Type"].ToString() + "&_fid_109=" + FinalTable.Rows[QB]["Stone 8 Shape"].ToString() + "&_fid_110=" + FinalTable.Rows[QB]["Stone 8 MM"].ToString() + "&_fid_111=" + FinalTable.Rows[QB]["Stone 8 Carat"].ToString() + "&_fid_112=" + FinalTable.Rows[QB]["Stone 8 Qty"].ToString() + "&_fid_113=" + FinalTable.Rows[QB]["Stone 8 Setting"].ToString() + "&_fid_114=" + FinalTable.Rows[QB]["Stone 9 Type"].ToString() + "&_fid_115=" + FinalTable.Rows[QB]["Stone 9 Shape"].ToString() + "&_fid_116=" + FinalTable.Rows[QB]["Stone 9 MM"].ToString() + "&_fid_117=" + FinalTable.Rows[QB]["Stone 9 Carat"].ToString() + "&_fid_118=" + FinalTable.Rows[QB]["Stone 9 Qty"].ToString() + "&_fid_119=" + FinalTable.Rows[QB]["Stone 9 Setting"].ToString() + "&_fid_245=" + FinalTable.Rows[QB]["Stone 1 Cost"].ToString() + "&_fid_246=" + FinalTable.Rows[QB]["Stone 2 Cost"].ToString() + "&_fid_247=" + FinalTable.Rows[QB]["Stone 3 Cost"].ToString() + "&_fid_248=" + FinalTable.Rows[QB]["Stone 4 Cost"].ToString() + "&_fid_249=" + FinalTable.Rows[QB]["Stone 5 Cost"].ToString() + "&_fid_250=" + FinalTable.Rows[QB]["Stone 6 Cost"].ToString() + "&_fid_251=" + FinalTable.Rows[QB]["Stone 7 Cost"].ToString() + "&_fid_252=" + FinalTable.Rows[QB]["Stone 8 Cost"].ToString() + "&_fid_253=" + FinalTable.Rows[QB]["Stone 9 Cost"].ToString() + "&_fid_77=" + GetWebName + "&_fid_345=" + FinalTable.Rows[QB]["CenterDiamondCarat"].ToString() + "&_fid_346=" + FinalTable.Rows[QB]["CenterDiamondCut"].ToString() + "&_fid_347=" + FinalTable.Rows[QB]["CenterDiamondColor"].ToString() + "&_fid_348=" + FinalTable.Rows[QB]["CenterDiamondClarity"].ToString() + "&_fid_349=" + FinalTable.Rows[QB]["Certificate"].ToString() + "&_fid_369=" + FinalTable.Rows[QB]["SilverPendantType"].ToString() + "&_fid_364=" + OrderPlatform + "&_fid_374=" + FinalTable.Rows[QB]["Semi Mount"].ToString() + "&_fid_381=" + "&_fid_381=" + FinalTable.Rows[QB]["Center_Stone_Shape"].ToString() + "&_fid_384=" + FinalTable.Rows[QB]["CouponCode"].ToString() + "&_fid_385=" + FinalTable.Rows[QB]["DiscountAmount"].ToString() + "&_fid_386=" + FinalTable.Rows[QB]["Sub_Total"].ToString() + "&usertoken=bzuxs9_cw8e_du2n5mbq9t6s7dqgxexu68h53s&apptoken=duby72vbbnfpx4dzsrwtjcbyqwep");

                    }

                    else if (FinalTable.Columns.Contains("Stone 8 Type"))
                    {

                        request = WebRequest.Create("https://nirgolan.quickbase.com/db/bmimrj5vm?a=API_AddRecord" + "&_fid_6=" + FinalTable.Rows[QB]["Order Date"].ToString() + "&_fid_7=" + FinalTable.Rows[QB]["Order number"].ToString() + "&_fid_8=" + FinalTable.Rows[QB]["Amount"].ToString() + "&_fid_9=" + FinalTable.Rows[QB]["Transaction"].ToString() + "&_fid_10=" + FinalTable.Rows[QB]["Payment Method"].ToString() + "&_fid_11=" + FinalTable.Rows[QB]["Amount Before Tax"].ToString() + "&_fid_12=" + FinalTable.Rows[QB]["Tax"].ToString() + "&_fid_13=" + FinalTable.Rows[QB]["Shipping City"].ToString() + "&_fid_14=" + FinalTable.Rows[QB]["Shipping Zip"].ToString() + "&_fid_15=" + FinalTable.Rows[QB]["ShippingService"].ToString() + "&_fid_16=" + FinalTable.Rows[QB]["Customer'sAccounts"].ToString() + "&_fid_17=" + FinalTable.Rows[QB]["Style No"].ToString() + "&_fid_18=" + FinalTable.Rows[QB]["Diamond Quality"].ToString() + "&_fid_19=" + FinalTable.Rows[QB]["Order Quantity"].ToString() + "&_fid_20=" + FinalTable.Rows[QB]["Gram Weight"].ToString() + "&_fid_21=" + FinalTable.Rows[QB]["Metal Karat"].ToString() + "&_fid_22=" + FinalTable.Rows[QB]["Metal Color"].ToString() + "&_fid_23=" + FinalTable.Rows[QB]["Labor Rate"].ToString() + "&_fid_24=" + FinalTable.Rows[QB]["Coupon"].ToString() + "&_fid_25=" + FinalTable.Rows[QB]["IGI"].ToString() + "&_fid_26=" + FinalTable.Rows[QB]["SilverReplicaText"].ToString() + "&_fid_27=" + FinalTable.Rows[QB]["Silver Pendant"].ToString() + "&_fid_28=" + FinalTable.Rows[QB]["Charm/Engraving"].ToString() + "&_fid_29=" + FinalTable.Rows[QB]["EngravingType"].ToString() + "&_fid_30=" + FinalTable.Rows[QB]["EngravingText"].ToString() + "&_fid_31=" + FinalTable.Rows[QB]["Rapnet Diamond"].ToString() + "&_fid_32=" + FinalTable.Rows[QB]["Anjolee Diamond"].ToString() + "&_fid_33=" + FinalTable.Rows[QB]["Customer Name"].ToString() + "&_fid_34=" + FinalTable.Rows[QB]["Customer email"].ToString() + "&_fid_35=" + FinalTable.Rows[QB]["Billing Phone Number"].ToString() + "&_fid_36=" + FinalTable.Rows[QB]["Bill/Ship Match"].ToString() + "&_fid_37=" + FinalTable.Rows[QB]["Payment Option"].ToString() + "&_fid_38=" + FinalTable.Rows[QB]["Shipping Name"].ToString() + "&_fid_39=" + FinalTable.Rows[QB]["Shipping Address1"].ToString() + "&_fid_40=" + FinalTable.Rows[QB]["Shipping Address2"].ToString() + "&_fid_41=" + FinalTable.Rows[QB]["Shipping Apt/Unit No"].ToString() + "&_fid_42=" + FinalTable.Rows[QB]["Shipping State"].ToString() + "&_fid_43=" + FinalTable.Rows[QB]["Shipping Country"].ToString() + "&_fid_44=" + FinalTable.Rows[QB]["Shipping Phone"].ToString() + "&_fid_45=" + FinalTable.Rows[QB]["Item Length/Ring Size"].ToString() + "&_fid_46=" + FinalTable.Rows[QB]["BandMetalType"].ToString() + "&_fid_47=" + FinalTable.Rows[QB]["VendorChildSku"].ToString() + "&_fid_48=" + FinalTable.Rows[QB]["Model Carat Weight"].ToString() + "&_fid_49=" + FinalTable.Rows[QB]["Effective Carat Weight"].ToString() + "&_fid_50=" + FinalTable.Rows[QB]["Vendor Metal Type"].ToString() + "&_fid_51=" + FinalTable.Rows[QB]["Stone Information Quality"].ToString() + "&_fid_52=" + FinalTable.Rows[QB]["Vendor Feed Price"].ToString() + "&_fid_53=" + FinalTable.Rows[QB]["Cut"].ToString() + "&_fid_54=" + FinalTable.Rows[QB]["Color"].ToString() + "&_fid_55=" + FinalTable.Rows[QB]["Clarity"].ToString() + "&_fid_56=" + FinalTable.Rows[QB]["Depth"].ToString() + "&_fid_57=" + FinalTable.Rows[QB]["Table"].ToString() + "&_fid_58=" + FinalTable.Rows[QB]["Girdle"].ToString() + "&_fid_59=" + FinalTable.Rows[QB]["Symmetry"].ToString() + "&_fid_60=" + FinalTable.Rows[QB]["Polish"].ToString() + "&_fid_61=" + FinalTable.Rows[QB]["CuletSize"].ToString() + "&_fid_62=" + FinalTable.Rows[QB]["FluorescenceIntensity"].ToString() + "&_fid_63=" + FinalTable.Rows[QB]["Lab"].ToString() + "&_fid_64=" + FinalTable.Rows[QB]["DiamondID"].ToString() + "&_fid_65=" + FinalTable.Rows[QB]["Stone 1 Type"].ToString() + "&_fid_66=" + FinalTable.Rows[QB]["Stone 1 Shape"].ToString() + "&_fid_67=" + FinalTable.Rows[QB]["Stone 1 MM"].ToString() + "&_fid_68=" + FinalTable.Rows[QB]["Stone 1 Carat"].ToString() + "&_fid_69=" + FinalTable.Rows[QB]["Stone 1 Qty"].ToString() + "&_fid_70=" + FinalTable.Rows[QB]["Stone 2 Setting"].ToString() + "&_fid_71=" + FinalTable.Rows[QB]["Stone 2 Type"].ToString() + "&_fid_72=" + FinalTable.Rows[QB]["Stone 2 Shape"].ToString() + "&_fid_73=" + FinalTable.Rows[QB]["Stone 2 MM"].ToString() + "&_fid_74=" + FinalTable.Rows[QB]["Stone 2 Carat"].ToString() + "&_fid_75=" + FinalTable.Rows[QB]["Stone 2 Qty"].ToString() + "&_fid_76=" + FinalTable.Rows[QB]["Stone 2 Setting"].ToString() + "&_fid_78=" + FinalTable.Rows[QB]["Stone 3 Type"].ToString() + "&_fid_79=" + FinalTable.Rows[QB]["Stone 3 Shape"].ToString() + "&_fid_80=" + FinalTable.Rows[QB]["Stone 3 MM"].ToString() + "&_fid_81=" + FinalTable.Rows[QB]["Stone 3 Carat"].ToString() + "&_fid_82=" + FinalTable.Rows[QB]["Stone 3 Qty"].ToString() + "&_fid_83=" + FinalTable.Rows[QB]["Stone 3 Setting"].ToString() + "&_fid_84=" + FinalTable.Rows[QB]["Stone 4 Type"].ToString() + "&_fid_85=" + FinalTable.Rows[QB]["Stone 4 Shape"].ToString() + "&_fid_86=" + FinalTable.Rows[QB]["Stone 4 MM"].ToString() + "&_fid_87=" + FinalTable.Rows[QB]["Stone 4 Carat"].ToString() + "&_fid_88=" + FinalTable.Rows[QB]["Stone 4 Qty"].ToString() + "&_fid_89=" + FinalTable.Rows[QB]["Stone 4 Setting"].ToString() + "&_fid_90=" + FinalTable.Rows[QB]["Stone 5 Type"].ToString() + "&_fid_91=" + FinalTable.Rows[QB]["Stone 5 Shape"].ToString() + "&_fid_92=" + FinalTable.Rows[QB]["Stone 5 MM"].ToString() + "&_fid_93=" + FinalTable.Rows[QB]["Stone 5 Carat"].ToString() + "&_fid_94=" + FinalTable.Rows[QB]["Stone 5 Qty"].ToString() + "&_fid_95=" + FinalTable.Rows[QB]["Stone 5 Setting"].ToString() + "&_fid_96=" + FinalTable.Rows[QB]["Stone 6 Type"].ToString() + "&_fid_97=" + FinalTable.Rows[QB]["Stone 6 Shape"].ToString() + "&_fid_98=" + FinalTable.Rows[QB]["Stone 6 MM"].ToString() + "&_fid_99=" + FinalTable.Rows[QB]["Stone 6 Carat"].ToString() + "&_fid_100=" + FinalTable.Rows[QB]["Stone 6 Qty"].ToString() + "&_fid_101=" + FinalTable.Rows[QB]["Stone 6 Setting"].ToString() + "&_fid_102=" + FinalTable.Rows[QB]["Stone 7 Type"].ToString() + "&_fid_103=" + FinalTable.Rows[QB]["Stone 7 Shape"].ToString() + "&_fid_104=" + FinalTable.Rows[QB]["Stone 7 MM"].ToString() + "&_fid_105=" + FinalTable.Rows[QB]["Stone 7 Carat"].ToString() + "&_fid_106=" + FinalTable.Rows[QB]["Stone 7 Qty"].ToString() + "&_fid_107=" + FinalTable.Rows[QB]["Stone 7 Setting"].ToString() + "&_fid_108=" + FinalTable.Rows[QB]["Stone 8 Type"].ToString() + "&_fid_109=" + FinalTable.Rows[QB]["Stone 8 Shape"].ToString() + "&_fid_110=" + FinalTable.Rows[QB]["Stone 8 MM"].ToString() + "&_fid_111=" + FinalTable.Rows[QB]["Stone 8 Carat"].ToString() + "&_fid_112=" + FinalTable.Rows[QB]["Stone 8 Qty"].ToString() + "&_fid_113=" + FinalTable.Rows[QB]["Stone 8 Setting"].ToString() + "&_fid_245=" + FinalTable.Rows[QB]["Stone 1 Cost"].ToString() + "&_fid_246=" + FinalTable.Rows[QB]["Stone 2 Cost"].ToString() + "&_fid_247=" + FinalTable.Rows[QB]["Stone 3 Cost"].ToString() + "&_fid_248=" + FinalTable.Rows[QB]["Stone 4 Cost"].ToString() + "&_fid_249=" + FinalTable.Rows[QB]["Stone 5 Cost"].ToString() + "&_fid_250=" + FinalTable.Rows[QB]["Stone 6 Cost"].ToString() + "&_fid_251=" + FinalTable.Rows[QB]["Stone 7 Cost"].ToString() + "&_fid_252=" + FinalTable.Rows[QB]["Stone 8 Cost"].ToString() + "&_fid_77=" + GetWebName + "&_fid_345=" + FinalTable.Rows[QB]["CenterDiamondCarat"].ToString() + "&_fid_346=" + FinalTable.Rows[QB]["CenterDiamondCut"].ToString() + "&_fid_347=" + FinalTable.Rows[QB]["CenterDiamondColor"].ToString() + "&_fid_348=" + FinalTable.Rows[QB]["CenterDiamondClarity"].ToString() + "&_fid_349=" + FinalTable.Rows[QB]["Certificate"].ToString() + "&_fid_369=" + FinalTable.Rows[QB]["SilverPendantType"].ToString() + "&_fid_364=" + OrderPlatform + "&_fid_374=" + FinalTable.Rows[QB]["Semi Mount"].ToString() + "&_fid_381=" + "&_fid_381=" + FinalTable.Rows[QB]["Center_Stone_Shape"].ToString() + "&_fid_384=" + FinalTable.Rows[QB]["CouponCode"].ToString() + "&_fid_385=" + FinalTable.Rows[QB]["DiscountAmount"].ToString() + "&_fid_386=" + FinalTable.Rows[QB]["Sub_Total"].ToString() + "&usertoken=bzuxs9_cw8e_du2n5mbq9t6s7dqgxexu68h53s&apptoken=duby72vbbnfpx4dzsrwtjcbyqwep");

                    }


                    else if (FinalTable.Columns.Contains("Stone 7 Type"))
                    {

                        request = WebRequest.Create("https://nirgolan.quickbase.com/db/bmimrj5vm?a=API_AddRecord" + "&_fid_6=" + FinalTable.Rows[QB]["Order Date"].ToString() + "&_fid_7=" + FinalTable.Rows[QB]["Order number"].ToString() + "&_fid_8=" + FinalTable.Rows[QB]["Amount"].ToString() + "&_fid_9=" + FinalTable.Rows[QB]["Transaction"].ToString() + "&_fid_10=" + FinalTable.Rows[QB]["Payment Method"].ToString() + "&_fid_11=" + FinalTable.Rows[QB]["Amount Before Tax"].ToString() + "&_fid_12=" + FinalTable.Rows[QB]["Tax"].ToString() + "&_fid_13=" + FinalTable.Rows[QB]["Shipping City"].ToString() + "&_fid_14=" + FinalTable.Rows[QB]["Shipping Zip"].ToString() + "&_fid_15=" + FinalTable.Rows[QB]["ShippingService"].ToString() + "&_fid_16=" + FinalTable.Rows[QB]["Customer'sAccounts"].ToString() + "&_fid_17=" + FinalTable.Rows[QB]["Style No"].ToString() + "&_fid_18=" + FinalTable.Rows[QB]["Diamond Quality"].ToString() + "&_fid_19=" + FinalTable.Rows[QB]["Order Quantity"].ToString() + "&_fid_20=" + FinalTable.Rows[QB]["Gram Weight"].ToString() + "&_fid_21=" + FinalTable.Rows[QB]["Metal Karat"].ToString() + "&_fid_22=" + FinalTable.Rows[QB]["Metal Color"].ToString() + "&_fid_23=" + FinalTable.Rows[QB]["Labor Rate"].ToString() + "&_fid_24=" + FinalTable.Rows[QB]["Coupon"].ToString() + "&_fid_25=" + FinalTable.Rows[QB]["IGI"].ToString() + "&_fid_26=" + FinalTable.Rows[QB]["SilverReplicaText"].ToString() + "&_fid_27=" + FinalTable.Rows[QB]["Silver Pendant"].ToString() + "&_fid_28=" + FinalTable.Rows[QB]["Charm/Engraving"].ToString() + "&_fid_29=" + FinalTable.Rows[QB]["EngravingType"].ToString() + "&_fid_30=" + FinalTable.Rows[QB]["EngravingText"].ToString() + "&_fid_31=" + FinalTable.Rows[QB]["Rapnet Diamond"].ToString() + "&_fid_32=" + FinalTable.Rows[QB]["Anjolee Diamond"].ToString() + "&_fid_33=" + FinalTable.Rows[QB]["Customer Name"].ToString() + "&_fid_34=" + FinalTable.Rows[QB]["Customer email"].ToString() + "&_fid_35=" + FinalTable.Rows[QB]["Billing Phone Number"].ToString() + "&_fid_36=" + FinalTable.Rows[QB]["Bill/Ship Match"].ToString() + "&_fid_37=" + FinalTable.Rows[QB]["Payment Option"].ToString() + "&_fid_38=" + FinalTable.Rows[QB]["Shipping Name"].ToString() + "&_fid_39=" + FinalTable.Rows[QB]["Shipping Address1"].ToString() + "&_fid_40=" + FinalTable.Rows[QB]["Shipping Address2"].ToString() + "&_fid_41=" + FinalTable.Rows[QB]["Shipping Apt/Unit No"].ToString() + "&_fid_42=" + FinalTable.Rows[QB]["Shipping State"].ToString() + "&_fid_43=" + FinalTable.Rows[QB]["Shipping Country"].ToString() + "&_fid_44=" + FinalTable.Rows[QB]["Shipping Phone"].ToString() + "&_fid_45=" + FinalTable.Rows[QB]["Item Length/Ring Size"].ToString() + "&_fid_46=" + FinalTable.Rows[QB]["BandMetalType"].ToString() + "&_fid_47=" + FinalTable.Rows[QB]["VendorChildSku"].ToString() + "&_fid_48=" + FinalTable.Rows[QB]["Model Carat Weight"].ToString() + "&_fid_49=" + FinalTable.Rows[QB]["Effective Carat Weight"].ToString() + "&_fid_50=" + FinalTable.Rows[QB]["Vendor Metal Type"].ToString() + "&_fid_51=" + FinalTable.Rows[QB]["Stone Information Quality"].ToString() + "&_fid_52=" + FinalTable.Rows[QB]["Vendor Feed Price"].ToString() + "&_fid_53=" + FinalTable.Rows[QB]["Cut"].ToString() + "&_fid_54=" + FinalTable.Rows[QB]["Color"].ToString() + "&_fid_55=" + FinalTable.Rows[QB]["Clarity"].ToString() + "&_fid_56=" + FinalTable.Rows[QB]["Depth"].ToString() + "&_fid_57=" + FinalTable.Rows[QB]["Table"].ToString() + "&_fid_58=" + FinalTable.Rows[QB]["Girdle"].ToString() + "&_fid_59=" + FinalTable.Rows[QB]["Symmetry"].ToString() + "&_fid_60=" + FinalTable.Rows[QB]["Polish"].ToString() + "&_fid_61=" + FinalTable.Rows[QB]["CuletSize"].ToString() + "&_fid_62=" + FinalTable.Rows[QB]["FluorescenceIntensity"].ToString() + "&_fid_63=" + FinalTable.Rows[QB]["Lab"].ToString() + "&_fid_64=" + FinalTable.Rows[QB]["DiamondID"].ToString() + "&_fid_65=" + FinalTable.Rows[QB]["Stone 1 Type"].ToString() + "&_fid_66=" + FinalTable.Rows[QB]["Stone 1 Shape"].ToString() + "&_fid_67=" + FinalTable.Rows[QB]["Stone 1 MM"].ToString() + "&_fid_68=" + FinalTable.Rows[QB]["Stone 1 Carat"].ToString() + "&_fid_69=" + FinalTable.Rows[QB]["Stone 1 Qty"].ToString() + "&_fid_70=" + FinalTable.Rows[QB]["Stone 2 Setting"].ToString() + "&_fid_71=" + FinalTable.Rows[QB]["Stone 2 Type"].ToString() + "&_fid_72=" + FinalTable.Rows[QB]["Stone 2 Shape"].ToString() + "&_fid_73=" + FinalTable.Rows[QB]["Stone 2 MM"].ToString() + "&_fid_74=" + FinalTable.Rows[QB]["Stone 2 Carat"].ToString() + "&_fid_75=" + FinalTable.Rows[QB]["Stone 2 Qty"].ToString() + "&_fid_76=" + FinalTable.Rows[QB]["Stone 2 Setting"].ToString() + "&_fid_78=" + FinalTable.Rows[QB]["Stone 3 Type"].ToString() + "&_fid_79=" + FinalTable.Rows[QB]["Stone 3 Shape"].ToString() + "&_fid_80=" + FinalTable.Rows[QB]["Stone 3 MM"].ToString() + "&_fid_81=" + FinalTable.Rows[QB]["Stone 3 Carat"].ToString() + "&_fid_82=" + FinalTable.Rows[QB]["Stone 3 Qty"].ToString() + "&_fid_83=" + FinalTable.Rows[QB]["Stone 3 Setting"].ToString() + "&_fid_84=" + FinalTable.Rows[QB]["Stone 4 Type"].ToString() + "&_fid_85=" + FinalTable.Rows[QB]["Stone 4 Shape"].ToString() + "&_fid_86=" + FinalTable.Rows[QB]["Stone 4 MM"].ToString() + "&_fid_87=" + FinalTable.Rows[QB]["Stone 4 Carat"].ToString() + "&_fid_88=" + FinalTable.Rows[QB]["Stone 4 Qty"].ToString() + "&_fid_89=" + FinalTable.Rows[QB]["Stone 4 Setting"].ToString() + "&_fid_90=" + FinalTable.Rows[QB]["Stone 5 Type"].ToString() + "&_fid_91=" + FinalTable.Rows[QB]["Stone 5 Shape"].ToString() + "&_fid_92=" + FinalTable.Rows[QB]["Stone 5 MM"].ToString() + "&_fid_93=" + FinalTable.Rows[QB]["Stone 5 Carat"].ToString() + "&_fid_94=" + FinalTable.Rows[QB]["Stone 5 Qty"].ToString() + "&_fid_95=" + FinalTable.Rows[QB]["Stone 5 Setting"].ToString() + "&_fid_96=" + FinalTable.Rows[QB]["Stone 6 Type"].ToString() + "&_fid_97=" + FinalTable.Rows[QB]["Stone 6 Shape"].ToString() + "&_fid_98=" + FinalTable.Rows[QB]["Stone 6 MM"].ToString() + "&_fid_99=" + FinalTable.Rows[QB]["Stone 6 Carat"].ToString() + "&_fid_100=" + FinalTable.Rows[QB]["Stone 6 Qty"].ToString() + "&_fid_101=" + FinalTable.Rows[QB]["Stone 6 Setting"].ToString() + "&_fid_102=" + FinalTable.Rows[QB]["Stone 7 Type"].ToString() + "&_fid_103=" + FinalTable.Rows[QB]["Stone 7 Shape"].ToString() + "&_fid_104=" + FinalTable.Rows[QB]["Stone 7 MM"].ToString() + "&_fid_105=" + FinalTable.Rows[QB]["Stone 7 Carat"].ToString() + "&_fid_106=" + FinalTable.Rows[QB]["Stone 7 Qty"].ToString() + "&_fid_107=" + FinalTable.Rows[QB]["Stone 7 Setting"].ToString() + "&_fid_245=" + FinalTable.Rows[QB]["Stone 1 Cost"].ToString() + "&_fid_246=" + FinalTable.Rows[QB]["Stone 2 Cost"].ToString() + "&_fid_247=" + FinalTable.Rows[QB]["Stone 3 Cost"].ToString() + "&_fid_248=" + FinalTable.Rows[QB]["Stone 4 Cost"].ToString() + "&_fid_249=" + FinalTable.Rows[QB]["Stone 5 Cost"].ToString() + "&_fid_250=" + FinalTable.Rows[QB]["Stone 6 Cost"].ToString() + "&_fid_251=" + FinalTable.Rows[QB]["Stone 7 Cost"].ToString() + "&_fid_77=" + GetWebName + "&_fid_345=" + FinalTable.Rows[QB]["CenterDiamondCarat"].ToString() + "&_fid_346=" + FinalTable.Rows[QB]["CenterDiamondCut"].ToString() + "&_fid_347=" + FinalTable.Rows[QB]["CenterDiamondColor"].ToString() + "&_fid_348=" + FinalTable.Rows[QB]["CenterDiamondClarity"].ToString() + "&_fid_349=" + FinalTable.Rows[QB]["Certificate"].ToString() + "&_fid_369=" + FinalTable.Rows[QB]["SilverPendantType"].ToString() + "&_fid_364=" + OrderPlatform + "&_fid_374=" + FinalTable.Rows[QB]["Semi Mount"].ToString() + "&_fid_381=" + "&_fid_381=" + FinalTable.Rows[QB]["Center_Stone_Shape"].ToString() + "&_fid_384=" + FinalTable.Rows[QB]["CouponCode"].ToString() + "&_fid_385=" + FinalTable.Rows[QB]["DiscountAmount"].ToString() + "&_fid_386=" + FinalTable.Rows[QB]["Sub_Total"].ToString() + "&usertoken=bzuxs9_cw8e_du2n5mbq9t6s7dqgxexu68h53s&apptoken=duby72vbbnfpx4dzsrwtjcbyqwep");

                    }

                    else if (FinalTable.Columns.Contains("Stone 6 Type"))
                    {

                        request = WebRequest.Create("https://nirgolan.quickbase.com/db/bmimrj5vm?a=API_AddRecord" + "&_fid_6=" + FinalTable.Rows[QB]["Order Date"].ToString() + "&_fid_7=" + FinalTable.Rows[QB]["Order number"].ToString() + "&_fid_8=" + FinalTable.Rows[QB]["Amount"].ToString() + "&_fid_9=" + FinalTable.Rows[QB]["Transaction"].ToString() + "&_fid_10=" + FinalTable.Rows[QB]["Payment Method"].ToString() + "&_fid_11=" + FinalTable.Rows[QB]["Amount Before Tax"].ToString() + "&_fid_12=" + FinalTable.Rows[QB]["Tax"].ToString() + "&_fid_13=" + FinalTable.Rows[QB]["Shipping City"].ToString() + "&_fid_14=" + FinalTable.Rows[QB]["Shipping Zip"].ToString() + "&_fid_15=" + FinalTable.Rows[QB]["ShippingService"].ToString() + "&_fid_16=" + FinalTable.Rows[QB]["Customer'sAccounts"].ToString() + "&_fid_17=" + FinalTable.Rows[QB]["Style No"].ToString() + "&_fid_18=" + FinalTable.Rows[QB]["Diamond Quality"].ToString() + "&_fid_19=" + FinalTable.Rows[QB]["Order Quantity"].ToString() + "&_fid_20=" + FinalTable.Rows[QB]["Gram Weight"].ToString() + "&_fid_21=" + FinalTable.Rows[QB]["Metal Karat"].ToString() + "&_fid_22=" + FinalTable.Rows[QB]["Metal Color"].ToString() + "&_fid_23=" + FinalTable.Rows[QB]["Labor Rate"].ToString() + "&_fid_24=" + FinalTable.Rows[QB]["Coupon"].ToString() + "&_fid_25=" + FinalTable.Rows[QB]["IGI"].ToString() + "&_fid_26=" + FinalTable.Rows[QB]["SilverReplicaText"].ToString() + "&_fid_27=" + FinalTable.Rows[QB]["Silver Pendant"].ToString() + "&_fid_28=" + FinalTable.Rows[QB]["Charm/Engraving"].ToString() + "&_fid_29=" + FinalTable.Rows[QB]["EngravingType"].ToString() + "&_fid_30=" + FinalTable.Rows[QB]["EngravingText"].ToString() + "&_fid_31=" + FinalTable.Rows[QB]["Rapnet Diamond"].ToString() + "&_fid_32=" + FinalTable.Rows[QB]["Anjolee Diamond"].ToString() + "&_fid_33=" + FinalTable.Rows[QB]["Customer Name"].ToString() + "&_fid_34=" + FinalTable.Rows[QB]["Customer email"].ToString() + "&_fid_35=" + FinalTable.Rows[QB]["Billing Phone Number"].ToString() + "&_fid_36=" + FinalTable.Rows[QB]["Bill/Ship Match"].ToString() + "&_fid_37=" + FinalTable.Rows[QB]["Payment Option"].ToString() + "&_fid_38=" + FinalTable.Rows[QB]["Shipping Name"].ToString() + "&_fid_39=" + FinalTable.Rows[QB]["Shipping Address1"].ToString() + "&_fid_40=" + FinalTable.Rows[QB]["Shipping Address2"].ToString() + "&_fid_41=" + FinalTable.Rows[QB]["Shipping Apt/Unit No"].ToString() + "&_fid_42=" + FinalTable.Rows[QB]["Shipping State"].ToString() + "&_fid_43=" + FinalTable.Rows[QB]["Shipping Country"].ToString() + "&_fid_44=" + FinalTable.Rows[QB]["Shipping Phone"].ToString() + "&_fid_45=" + FinalTable.Rows[QB]["Item Length/Ring Size"].ToString() + "&_fid_46=" + FinalTable.Rows[QB]["BandMetalType"].ToString() + "&_fid_47=" + FinalTable.Rows[QB]["VendorChildSku"].ToString() + "&_fid_48=" + FinalTable.Rows[QB]["Model Carat Weight"].ToString() + "&_fid_49=" + FinalTable.Rows[QB]["Effective Carat Weight"].ToString() + "&_fid_50=" + FinalTable.Rows[QB]["Vendor Metal Type"].ToString() + "&_fid_51=" + FinalTable.Rows[QB]["Stone Information Quality"].ToString() + "&_fid_52=" + FinalTable.Rows[QB]["Vendor Feed Price"].ToString() + "&_fid_53=" + FinalTable.Rows[QB]["Cut"].ToString() + "&_fid_54=" + FinalTable.Rows[QB]["Color"].ToString() + "&_fid_55=" + FinalTable.Rows[QB]["Clarity"].ToString() + "&_fid_56=" + FinalTable.Rows[QB]["Depth"].ToString() + "&_fid_57=" + FinalTable.Rows[QB]["Table"].ToString() + "&_fid_58=" + FinalTable.Rows[QB]["Girdle"].ToString() + "&_fid_59=" + FinalTable.Rows[QB]["Symmetry"].ToString() + "&_fid_60=" + FinalTable.Rows[QB]["Polish"].ToString() + "&_fid_61=" + FinalTable.Rows[QB]["CuletSize"].ToString() + "&_fid_62=" + FinalTable.Rows[QB]["FluorescenceIntensity"].ToString() + "&_fid_63=" + FinalTable.Rows[QB]["Lab"].ToString() + "&_fid_64=" + FinalTable.Rows[QB]["DiamondID"].ToString() + "&_fid_65=" + FinalTable.Rows[QB]["Stone 1 Type"].ToString() + "&_fid_66=" + FinalTable.Rows[QB]["Stone 1 Shape"].ToString() + "&_fid_67=" + FinalTable.Rows[QB]["Stone 1 MM"].ToString() + "&_fid_68=" + FinalTable.Rows[QB]["Stone 1 Carat"].ToString() + "&_fid_69=" + FinalTable.Rows[QB]["Stone 1 Qty"].ToString() + "&_fid_70=" + FinalTable.Rows[QB]["Stone 2 Setting"].ToString() + "&_fid_71=" + FinalTable.Rows[QB]["Stone 2 Type"].ToString() + "&_fid_72=" + FinalTable.Rows[QB]["Stone 2 Shape"].ToString() + "&_fid_73=" + FinalTable.Rows[QB]["Stone 2 MM"].ToString() + "&_fid_74=" + FinalTable.Rows[QB]["Stone 2 Carat"].ToString() + "&_fid_75=" + FinalTable.Rows[QB]["Stone 2 Qty"].ToString() + "&_fid_76=" + FinalTable.Rows[QB]["Stone 2 Setting"].ToString() + "&_fid_78=" + FinalTable.Rows[QB]["Stone 3 Type"].ToString() + "&_fid_79=" + FinalTable.Rows[QB]["Stone 3 Shape"].ToString() + "&_fid_80=" + FinalTable.Rows[QB]["Stone 3 MM"].ToString() + "&_fid_81=" + FinalTable.Rows[QB]["Stone 3 Carat"].ToString() + "&_fid_82=" + FinalTable.Rows[QB]["Stone 3 Qty"].ToString() + "&_fid_83=" + FinalTable.Rows[QB]["Stone 3 Setting"].ToString() + "&_fid_84=" + FinalTable.Rows[QB]["Stone 4 Type"].ToString() + "&_fid_85=" + FinalTable.Rows[QB]["Stone 4 Shape"].ToString() + "&_fid_86=" + FinalTable.Rows[QB]["Stone 4 MM"].ToString() + "&_fid_87=" + FinalTable.Rows[QB]["Stone 4 Carat"].ToString() + "&_fid_88=" + FinalTable.Rows[QB]["Stone 4 Qty"].ToString() + "&_fid_89=" + FinalTable.Rows[QB]["Stone 4 Setting"].ToString() + "&_fid_90=" + FinalTable.Rows[QB]["Stone 5 Type"].ToString() + "&_fid_91=" + FinalTable.Rows[QB]["Stone 5 Shape"].ToString() + "&_fid_92=" + FinalTable.Rows[QB]["Stone 5 MM"].ToString() + "&_fid_93=" + FinalTable.Rows[QB]["Stone 5 Carat"].ToString() + "&_fid_94=" + FinalTable.Rows[QB]["Stone 5 Qty"].ToString() + "&_fid_95=" + FinalTable.Rows[QB]["Stone 5 Setting"].ToString() + "&_fid_96=" + FinalTable.Rows[QB]["Stone 6 Type"].ToString() + "&_fid_97=" + FinalTable.Rows[QB]["Stone 6 Shape"].ToString() + "&_fid_98=" + FinalTable.Rows[QB]["Stone 6 MM"].ToString() + "&_fid_99=" + FinalTable.Rows[QB]["Stone 6 Carat"].ToString() + "&_fid_100=" + FinalTable.Rows[QB]["Stone 6 Qty"].ToString() + "&_fid_101=" + FinalTable.Rows[QB]["Stone 6 Setting"].ToString() + "&_fid_245=" + FinalTable.Rows[QB]["Stone 1 Cost"].ToString() + "&_fid_246=" + FinalTable.Rows[QB]["Stone 2 Cost"].ToString() + "&_fid_247=" + FinalTable.Rows[QB]["Stone 3 Cost"].ToString() + "&_fid_248=" + FinalTable.Rows[QB]["Stone 4 Cost"].ToString() + "&_fid_249=" + FinalTable.Rows[QB]["Stone 5 Cost"].ToString() + "&_fid_250=" + FinalTable.Rows[QB]["Stone 6 Cost"].ToString() + "&_fid_77=" + GetWebName + "&_fid_345=" + FinalTable.Rows[QB]["CenterDiamondCarat"].ToString() + "&_fid_346=" + FinalTable.Rows[QB]["CenterDiamondCut"].ToString() + "&_fid_347=" + FinalTable.Rows[QB]["CenterDiamondColor"].ToString() + "&_fid_348=" + FinalTable.Rows[QB]["CenterDiamondClarity"].ToString() + "&_fid_349=" + FinalTable.Rows[QB]["Certificate"].ToString() + "&_fid_369=" + FinalTable.Rows[QB]["SilverPendantType"].ToString() + "&_fid_364=" + OrderPlatform + "&_fid_374=" + FinalTable.Rows[QB]["Semi Mount"].ToString() + "&_fid_381=" + "&_fid_381=" + FinalTable.Rows[QB]["Center_Stone_Shape"].ToString() + "&_fid_384=" + FinalTable.Rows[QB]["CouponCode"].ToString() + "&_fid_385=" + FinalTable.Rows[QB]["DiscountAmount"].ToString() + "&_fid_386=" + FinalTable.Rows[QB]["Sub_Total"].ToString() + "&usertoken=bzuxs9_cw8e_du2n5mbq9t6s7dqgxexu68h53s&apptoken=duby72vbbnfpx4dzsrwtjcbyqwep");

                    }

                    else if (FinalTable.Columns.Contains("Stone 5 Type"))
                    {

                        request = WebRequest.Create("https://nirgolan.quickbase.com/db/bmimrj5vm?a=API_AddRecord" + "&_fid_6=" + FinalTable.Rows[QB]["Order Date"].ToString() + "&_fid_7=" + FinalTable.Rows[QB]["Order number"].ToString() + "&_fid_8=" + FinalTable.Rows[QB]["Amount"].ToString() + "&_fid_9=" + FinalTable.Rows[QB]["Transaction"].ToString() + "&_fid_10=" + FinalTable.Rows[QB]["Payment Method"].ToString() + "&_fid_11=" + FinalTable.Rows[QB]["Amount Before Tax"].ToString() + "&_fid_12=" + FinalTable.Rows[QB]["Tax"].ToString() + "&_fid_13=" + FinalTable.Rows[QB]["Shipping City"].ToString() + "&_fid_14=" + FinalTable.Rows[QB]["Shipping Zip"].ToString() + "&_fid_15=" + FinalTable.Rows[QB]["ShippingService"].ToString() + "&_fid_16=" + FinalTable.Rows[QB]["Customer'sAccounts"].ToString() + "&_fid_17=" + FinalTable.Rows[QB]["Style No"].ToString() + "&_fid_18=" + FinalTable.Rows[QB]["Diamond Quality"].ToString() + "&_fid_19=" + FinalTable.Rows[QB]["Order Quantity"].ToString() + "&_fid_20=" + FinalTable.Rows[QB]["Gram Weight"].ToString() + "&_fid_21=" + FinalTable.Rows[QB]["Metal Karat"].ToString() + "&_fid_22=" + FinalTable.Rows[QB]["Metal Color"].ToString() + "&_fid_23=" + FinalTable.Rows[QB]["Labor Rate"].ToString() + "&_fid_24=" + FinalTable.Rows[QB]["Coupon"].ToString() + "&_fid_25=" + FinalTable.Rows[QB]["IGI"].ToString() + "&_fid_26=" + FinalTable.Rows[QB]["SilverReplicaText"].ToString() + "&_fid_27=" + FinalTable.Rows[QB]["Silver Pendant"].ToString() + "&_fid_28=" + FinalTable.Rows[QB]["Charm/Engraving"].ToString() + "&_fid_29=" + FinalTable.Rows[QB]["EngravingType"].ToString() + "&_fid_30=" + FinalTable.Rows[QB]["EngravingText"].ToString() + "&_fid_31=" + FinalTable.Rows[QB]["Rapnet Diamond"].ToString() + "&_fid_32=" + FinalTable.Rows[QB]["Anjolee Diamond"].ToString() + "&_fid_33=" + FinalTable.Rows[QB]["Customer Name"].ToString() + "&_fid_34=" + FinalTable.Rows[QB]["Customer email"].ToString() + "&_fid_35=" + FinalTable.Rows[QB]["Billing Phone Number"].ToString() + "&_fid_36=" + FinalTable.Rows[QB]["Bill/Ship Match"].ToString() + "&_fid_37=" + FinalTable.Rows[QB]["Payment Option"].ToString() + "&_fid_38=" + FinalTable.Rows[QB]["Shipping Name"].ToString() + "&_fid_39=" + FinalTable.Rows[QB]["Shipping Address1"].ToString() + "&_fid_40=" + FinalTable.Rows[QB]["Shipping Address2"].ToString() + "&_fid_41=" + FinalTable.Rows[QB]["Shipping Apt/Unit No"].ToString() + "&_fid_42=" + FinalTable.Rows[QB]["Shipping State"].ToString() + "&_fid_43=" + FinalTable.Rows[QB]["Shipping Country"].ToString() + "&_fid_44=" + FinalTable.Rows[QB]["Shipping Phone"].ToString() + "&_fid_45=" + FinalTable.Rows[QB]["Item Length/Ring Size"].ToString() + "&_fid_46=" + FinalTable.Rows[QB]["BandMetalType"].ToString() + "&_fid_47=" + FinalTable.Rows[QB]["VendorChildSku"].ToString() + "&_fid_48=" + FinalTable.Rows[QB]["Model Carat Weight"].ToString() + "&_fid_49=" + FinalTable.Rows[QB]["Effective Carat Weight"].ToString() + "&_fid_50=" + FinalTable.Rows[QB]["Vendor Metal Type"].ToString() + "&_fid_51=" + FinalTable.Rows[QB]["Stone Information Quality"].ToString() + "&_fid_52=" + FinalTable.Rows[QB]["Vendor Feed Price"].ToString() + "&_fid_53=" + FinalTable.Rows[QB]["Cut"].ToString() + "&_fid_54=" + FinalTable.Rows[QB]["Color"].ToString() + "&_fid_55=" + FinalTable.Rows[QB]["Clarity"].ToString() + "&_fid_56=" + FinalTable.Rows[QB]["Depth"].ToString() + "&_fid_57=" + FinalTable.Rows[QB]["Table"].ToString() + "&_fid_58=" + FinalTable.Rows[QB]["Girdle"].ToString() + "&_fid_59=" + FinalTable.Rows[QB]["Symmetry"].ToString() + "&_fid_60=" + FinalTable.Rows[QB]["Polish"].ToString() + "&_fid_61=" + FinalTable.Rows[QB]["CuletSize"].ToString() + "&_fid_62=" + FinalTable.Rows[QB]["FluorescenceIntensity"].ToString() + "&_fid_63=" + FinalTable.Rows[QB]["Lab"].ToString() + "&_fid_64=" + FinalTable.Rows[QB]["DiamondID"].ToString() + "&_fid_65=" + FinalTable.Rows[QB]["Stone 1 Type"].ToString() + "&_fid_66=" + FinalTable.Rows[QB]["Stone 1 Shape"].ToString() + "&_fid_67=" + FinalTable.Rows[QB]["Stone 1 MM"].ToString() + "&_fid_68=" + FinalTable.Rows[QB]["Stone 1 Carat"].ToString() + "&_fid_69=" + FinalTable.Rows[QB]["Stone 1 Qty"].ToString() + "&_fid_70=" + FinalTable.Rows[QB]["Stone 2 Setting"].ToString() + "&_fid_71=" + FinalTable.Rows[QB]["Stone 2 Type"].ToString() + "&_fid_72=" + FinalTable.Rows[QB]["Stone 2 Shape"].ToString() + "&_fid_73=" + FinalTable.Rows[QB]["Stone 2 MM"].ToString() + "&_fid_74=" + FinalTable.Rows[QB]["Stone 2 Carat"].ToString() + "&_fid_75=" + FinalTable.Rows[QB]["Stone 2 Qty"].ToString() + "&_fid_76=" + FinalTable.Rows[QB]["Stone 2 Setting"].ToString() + "&_fid_78=" + FinalTable.Rows[QB]["Stone 3 Type"].ToString() + "&_fid_79=" + FinalTable.Rows[QB]["Stone 3 Shape"].ToString() + "&_fid_80=" + FinalTable.Rows[QB]["Stone 3 MM"].ToString() + "&_fid_81=" + FinalTable.Rows[QB]["Stone 3 Carat"].ToString() + "&_fid_82=" + FinalTable.Rows[QB]["Stone 3 Qty"].ToString() + "&_fid_83=" + FinalTable.Rows[QB]["Stone 3 Setting"].ToString() + "&_fid_84=" + FinalTable.Rows[QB]["Stone 4 Type"].ToString() + "&_fid_85=" + FinalTable.Rows[QB]["Stone 4 Shape"].ToString() + "&_fid_86=" + FinalTable.Rows[QB]["Stone 4 MM"].ToString() + "&_fid_87=" + FinalTable.Rows[QB]["Stone 4 Carat"].ToString() + "&_fid_88=" + FinalTable.Rows[QB]["Stone 4 Qty"].ToString() + "&_fid_89=" + FinalTable.Rows[QB]["Stone 4 Setting"].ToString() + "&_fid_90=" + FinalTable.Rows[QB]["Stone 5 Type"].ToString() + "&_fid_91=" + FinalTable.Rows[QB]["Stone 5 Shape"].ToString() + "&_fid_92=" + FinalTable.Rows[QB]["Stone 5 MM"].ToString() + "&_fid_93=" + FinalTable.Rows[QB]["Stone 5 Carat"].ToString() + "&_fid_94=" + FinalTable.Rows[QB]["Stone 5 Qty"].ToString() + "&_fid_95=" + FinalTable.Rows[QB]["Stone 5 Setting"].ToString() + "&_fid_245=" + FinalTable.Rows[QB]["Stone 1 Cost"].ToString() + "&_fid_246=" + FinalTable.Rows[QB]["Stone 2 Cost"].ToString() + "&_fid_247=" + FinalTable.Rows[QB]["Stone 3 Cost"].ToString() + "&_fid_248=" + FinalTable.Rows[QB]["Stone 4 Cost"].ToString() + "&_fid_249=" + FinalTable.Rows[QB]["Stone 5 Cost"].ToString() + "&_fid_77=" + GetWebName + "&_fid_345=" + FinalTable.Rows[QB]["CenterDiamondCarat"].ToString() + "&_fid_346=" + FinalTable.Rows[QB]["CenterDiamondCut"].ToString() + "&_fid_347=" + FinalTable.Rows[QB]["CenterDiamondColor"].ToString() + "&_fid_348=" + FinalTable.Rows[QB]["CenterDiamondClarity"].ToString() + "&_fid_349=" + FinalTable.Rows[QB]["Certificate"].ToString() + "&_fid_369=" + FinalTable.Rows[QB]["SilverPendantType"].ToString() + "&_fid_364=" + OrderPlatform + "&_fid_374=" + FinalTable.Rows[QB]["Semi Mount"].ToString() + "&_fid_381=" + "&_fid_381=" + FinalTable.Rows[QB]["Center_Stone_Shape"].ToString() + "&_fid_384=" + FinalTable.Rows[QB]["CouponCode"].ToString() + "&_fid_385=" + FinalTable.Rows[QB]["DiscountAmount"].ToString() + "&_fid_386=" + FinalTable.Rows[QB]["Sub_Total"].ToString() + "&usertoken=bzuxs9_cw8e_du2n5mbq9t6s7dqgxexu68h53s&apptoken=duby72vbbnfpx4dzsrwtjcbyqwep");



                    }

                    else if (FinalTable.Columns.Contains("Stone 4 Type"))
                    {

                        request = WebRequest.Create("https://nirgolan.quickbase.com/db/bmimrj5vm?a=API_AddRecord" + "&_fid_6=" + FinalTable.Rows[QB]["Order Date"].ToString() + "&_fid_7=" + FinalTable.Rows[QB]["Order number"].ToString() + "&_fid_8=" + FinalTable.Rows[QB]["Amount"].ToString() + "&_fid_9=" + FinalTable.Rows[QB]["Transaction"].ToString() + "&_fid_10=" + FinalTable.Rows[QB]["Payment Method"].ToString() + "&_fid_11=" + FinalTable.Rows[QB]["Amount Before Tax"].ToString() + "&_fid_12=" + FinalTable.Rows[QB]["Tax"].ToString() + "&_fid_13=" + FinalTable.Rows[QB]["Shipping City"].ToString() + "&_fid_14=" + FinalTable.Rows[QB]["Shipping Zip"].ToString() + "&_fid_15=" + FinalTable.Rows[QB]["ShippingService"].ToString() + "&_fid_16=" + FinalTable.Rows[QB]["Customer'sAccounts"].ToString() + "&_fid_17=" + FinalTable.Rows[QB]["Style No"].ToString() + "&_fid_18=" + FinalTable.Rows[QB]["Diamond Quality"].ToString() + "&_fid_19=" + FinalTable.Rows[QB]["Order Quantity"].ToString() + "&_fid_20=" + FinalTable.Rows[QB]["Gram Weight"].ToString() + "&_fid_21=" + FinalTable.Rows[QB]["Metal Karat"].ToString() + "&_fid_22=" + FinalTable.Rows[QB]["Metal Color"].ToString() + "&_fid_23=" + FinalTable.Rows[QB]["Labor Rate"].ToString() + "&_fid_24=" + FinalTable.Rows[QB]["Coupon"].ToString() + "&_fid_25=" + FinalTable.Rows[QB]["IGI"].ToString() + "&_fid_26=" + FinalTable.Rows[QB]["SilverReplicaText"].ToString() + "&_fid_27=" + FinalTable.Rows[QB]["Silver Pendant"].ToString() + "&_fid_28=" + FinalTable.Rows[QB]["Charm/Engraving"].ToString() + "&_fid_29=" + FinalTable.Rows[QB]["EngravingType"].ToString() + "&_fid_30=" + FinalTable.Rows[QB]["EngravingText"].ToString() + "&_fid_31=" + FinalTable.Rows[QB]["Rapnet Diamond"].ToString() + "&_fid_32=" + FinalTable.Rows[QB]["Anjolee Diamond"].ToString() + "&_fid_33=" + FinalTable.Rows[QB]["Customer Name"].ToString() + "&_fid_34=" + FinalTable.Rows[QB]["Customer email"].ToString() + "&_fid_35=" + FinalTable.Rows[QB]["Billing Phone Number"].ToString() + "&_fid_36=" + FinalTable.Rows[QB]["Bill/Ship Match"].ToString() + "&_fid_37=" + FinalTable.Rows[QB]["Payment Option"].ToString() + "&_fid_38=" + FinalTable.Rows[QB]["Shipping Name"].ToString() + "&_fid_39=" + FinalTable.Rows[QB]["Shipping Address1"].ToString() + "&_fid_40=" + FinalTable.Rows[QB]["Shipping Address2"].ToString() + "&_fid_41=" + FinalTable.Rows[QB]["Shipping Apt/Unit No"].ToString() + "&_fid_42=" + FinalTable.Rows[QB]["Shipping State"].ToString() + "&_fid_43=" + FinalTable.Rows[QB]["Shipping Country"].ToString() + "&_fid_44=" + FinalTable.Rows[QB]["Shipping Phone"].ToString() + "&_fid_45=" + FinalTable.Rows[QB]["Item Length/Ring Size"].ToString() + "&_fid_46=" + FinalTable.Rows[QB]["BandMetalType"].ToString() + "&_fid_47=" + FinalTable.Rows[QB]["VendorChildSku"].ToString() + "&_fid_48=" + FinalTable.Rows[QB]["Model Carat Weight"].ToString() + "&_fid_49=" + FinalTable.Rows[QB]["Effective Carat Weight"].ToString() + "&_fid_50=" + FinalTable.Rows[QB]["Vendor Metal Type"].ToString() + "&_fid_51=" + FinalTable.Rows[QB]["Stone Information Quality"].ToString() + "&_fid_52=" + FinalTable.Rows[QB]["Vendor Feed Price"].ToString() + "&_fid_53=" + FinalTable.Rows[QB]["Cut"].ToString() + "&_fid_54=" + FinalTable.Rows[QB]["Color"].ToString() + "&_fid_55=" + FinalTable.Rows[QB]["Clarity"].ToString() + "&_fid_56=" + FinalTable.Rows[QB]["Depth"].ToString() + "&_fid_57=" + FinalTable.Rows[QB]["Table"].ToString() + "&_fid_58=" + FinalTable.Rows[QB]["Girdle"].ToString() + "&_fid_59=" + FinalTable.Rows[QB]["Symmetry"].ToString() + "&_fid_60=" + FinalTable.Rows[QB]["Polish"].ToString() + "&_fid_61=" + FinalTable.Rows[QB]["CuletSize"].ToString() + "&_fid_62=" + FinalTable.Rows[QB]["FluorescenceIntensity"].ToString() + "&_fid_63=" + FinalTable.Rows[QB]["Lab"].ToString() + "&_fid_64=" + FinalTable.Rows[QB]["DiamondID"].ToString() + "&_fid_65=" + FinalTable.Rows[QB]["Stone 1 Type"].ToString() + "&_fid_66=" + FinalTable.Rows[QB]["Stone 1 Shape"].ToString() + "&_fid_67=" + FinalTable.Rows[QB]["Stone 1 MM"].ToString() + "&_fid_68=" + FinalTable.Rows[QB]["Stone 1 Carat"].ToString() + "&_fid_69=" + FinalTable.Rows[QB]["Stone 1 Qty"].ToString() + "&_fid_70=" + FinalTable.Rows[QB]["Stone 2 Setting"].ToString() + "&_fid_71=" + FinalTable.Rows[QB]["Stone 2 Type"].ToString() + "&_fid_72=" + FinalTable.Rows[QB]["Stone 2 Shape"].ToString() + "&_fid_73=" + FinalTable.Rows[QB]["Stone 2 MM"].ToString() + "&_fid_74=" + FinalTable.Rows[QB]["Stone 2 Carat"].ToString() + "&_fid_75=" + FinalTable.Rows[QB]["Stone 2 Qty"].ToString() + "&_fid_76=" + FinalTable.Rows[QB]["Stone 2 Setting"].ToString() + "&_fid_78=" + FinalTable.Rows[QB]["Stone 3 Type"].ToString() + "&_fid_79=" + FinalTable.Rows[QB]["Stone 3 Shape"].ToString() + "&_fid_80=" + FinalTable.Rows[QB]["Stone 3 MM"].ToString() + "&_fid_81=" + FinalTable.Rows[QB]["Stone 3 Carat"].ToString() + "&_fid_82=" + FinalTable.Rows[QB]["Stone 3 Qty"].ToString() + "&_fid_83=" + FinalTable.Rows[QB]["Stone 3 Setting"].ToString() + "&_fid_84=" + FinalTable.Rows[QB]["Stone 4 Type"].ToString() + "&_fid_85=" + FinalTable.Rows[QB]["Stone 4 Shape"].ToString() + "&_fid_86=" + FinalTable.Rows[QB]["Stone 4 MM"].ToString() + "&_fid_87=" + FinalTable.Rows[QB]["Stone 4 Carat"].ToString() + "&_fid_88=" + FinalTable.Rows[QB]["Stone 4 Qty"].ToString() + "&_fid_89=" + FinalTable.Rows[QB]["Stone 4 Setting"].ToString() + "&_fid_245=" + FinalTable.Rows[QB]["Stone 1 Cost"].ToString() + "&_fid_246=" + FinalTable.Rows[QB]["Stone 2 Cost"].ToString() + "&_fid_247=" + FinalTable.Rows[QB]["Stone 3 Cost"].ToString() + "&_fid_248=" + FinalTable.Rows[QB]["Stone 4 Cost"].ToString() + "&_fid_77=" + GetWebName + "&_fid_345=" + FinalTable.Rows[QB]["CenterDiamondCarat"].ToString() + "&_fid_346=" + FinalTable.Rows[QB]["CenterDiamondCut"].ToString() + "&_fid_347=" + FinalTable.Rows[QB]["CenterDiamondColor"].ToString() + "&_fid_348=" + FinalTable.Rows[QB]["CenterDiamondClarity"].ToString() + "&_fid_349=" + FinalTable.Rows[QB]["Certificate"].ToString() + "&_fid_369=" + FinalTable.Rows[QB]["SilverPendantType"].ToString() + "&_fid_364=" + OrderPlatform + "&_fid_374=" + FinalTable.Rows[QB]["Semi Mount"].ToString() + "&_fid_381=" + "&_fid_381=" + FinalTable.Rows[QB]["Center_Stone_Shape"].ToString() + "&_fid_384=" + FinalTable.Rows[QB]["CouponCode"].ToString() + "&_fid_385=" + FinalTable.Rows[QB]["DiscountAmount"].ToString() + "&_fid_386=" + FinalTable.Rows[QB]["Sub_Total"].ToString() + "&usertoken=bzuxs9_cw8e_du2n5mbq9t6s7dqgxexu68h53s&apptoken=duby72vbbnfpx4dzsrwtjcbyqwep");



                    }

                    else if (FinalTable.Columns.Contains("Stone 3 Type"))
                    {

                        request = WebRequest.Create("https://nirgolan.quickbase.com/db/bmimrj5vm?a=API_AddRecord" + "&_fid_6=" + FinalTable.Rows[QB]["Order Date"].ToString() + "&_fid_7=" + FinalTable.Rows[QB]["Order number"].ToString() + "&_fid_8=" + FinalTable.Rows[QB]["Amount"].ToString() + "&_fid_9=" + FinalTable.Rows[QB]["Transaction"].ToString() + "&_fid_10=" + FinalTable.Rows[QB]["Payment Method"].ToString() + "&_fid_11=" + FinalTable.Rows[QB]["Amount Before Tax"].ToString() + "&_fid_12=" + FinalTable.Rows[QB]["Tax"].ToString() + "&_fid_13=" + FinalTable.Rows[QB]["Shipping City"].ToString() + "&_fid_14=" + FinalTable.Rows[QB]["Shipping Zip"].ToString() + "&_fid_15=" + FinalTable.Rows[QB]["ShippingService"].ToString() + "&_fid_16=" + FinalTable.Rows[QB]["Customer'sAccounts"].ToString() + "&_fid_17=" + FinalTable.Rows[QB]["Style No"].ToString() + "&_fid_18=" + FinalTable.Rows[QB]["Diamond Quality"].ToString() + "&_fid_19=" + FinalTable.Rows[QB]["Order Quantity"].ToString() + "&_fid_20=" + FinalTable.Rows[QB]["Gram Weight"].ToString() + "&_fid_21=" + FinalTable.Rows[QB]["Metal Karat"].ToString() + "&_fid_22=" + FinalTable.Rows[QB]["Metal Color"].ToString() + "&_fid_23=" + FinalTable.Rows[QB]["Labor Rate"].ToString() + "&_fid_24=" + FinalTable.Rows[QB]["Coupon"].ToString() + "&_fid_25=" + FinalTable.Rows[QB]["IGI"].ToString() + "&_fid_26=" + FinalTable.Rows[QB]["SilverReplicaText"].ToString() + "&_fid_27=" + FinalTable.Rows[QB]["Silver Pendant"].ToString() + "&_fid_28=" + FinalTable.Rows[QB]["Charm/Engraving"].ToString() + "&_fid_29=" + FinalTable.Rows[QB]["EngravingType"].ToString() + "&_fid_30=" + FinalTable.Rows[QB]["EngravingText"].ToString() + "&_fid_31=" + FinalTable.Rows[QB]["Rapnet Diamond"].ToString() + "&_fid_32=" + FinalTable.Rows[QB]["Anjolee Diamond"].ToString() + "&_fid_33=" + FinalTable.Rows[QB]["Customer Name"].ToString() + "&_fid_34=" + FinalTable.Rows[QB]["Customer email"].ToString() + "&_fid_35=" + FinalTable.Rows[QB]["Billing Phone Number"].ToString() + "&_fid_36=" + FinalTable.Rows[QB]["Bill/Ship Match"].ToString() + "&_fid_37=" + FinalTable.Rows[QB]["Payment Option"].ToString() + "&_fid_38=" + FinalTable.Rows[QB]["Shipping Name"].ToString() + "&_fid_39=" + FinalTable.Rows[QB]["Shipping Address1"].ToString() + "&_fid_40=" + FinalTable.Rows[QB]["Shipping Address2"].ToString() + "&_fid_41=" + FinalTable.Rows[QB]["Shipping Apt/Unit No"].ToString() + "&_fid_42=" + FinalTable.Rows[QB]["Shipping State"].ToString() + "&_fid_43=" + FinalTable.Rows[QB]["Shipping Country"].ToString() + "&_fid_44=" + FinalTable.Rows[QB]["Shipping Phone"].ToString() + "&_fid_45=" + FinalTable.Rows[QB]["Item Length/Ring Size"].ToString() + "&_fid_46=" + FinalTable.Rows[QB]["BandMetalType"].ToString() + "&_fid_47=" + FinalTable.Rows[QB]["VendorChildSku"].ToString() + "&_fid_48=" + FinalTable.Rows[QB]["Model Carat Weight"].ToString() + "&_fid_49=" + FinalTable.Rows[QB]["Effective Carat Weight"].ToString() + "&_fid_50=" + FinalTable.Rows[QB]["Vendor Metal Type"].ToString() + "&_fid_51=" + FinalTable.Rows[QB]["Stone Information Quality"].ToString() + "&_fid_52=" + FinalTable.Rows[QB]["Vendor Feed Price"].ToString() + "&_fid_53=" + FinalTable.Rows[QB]["Cut"].ToString() + "&_fid_54=" + FinalTable.Rows[QB]["Color"].ToString() + "&_fid_55=" + FinalTable.Rows[QB]["Clarity"].ToString() + "&_fid_56=" + FinalTable.Rows[QB]["Depth"].ToString() + "&_fid_57=" + FinalTable.Rows[QB]["Table"].ToString() + "&_fid_58=" + FinalTable.Rows[QB]["Girdle"].ToString() + "&_fid_59=" + FinalTable.Rows[QB]["Symmetry"].ToString() + "&_fid_60=" + FinalTable.Rows[QB]["Polish"].ToString() + "&_fid_61=" + FinalTable.Rows[QB]["CuletSize"].ToString() + "&_fid_62=" + FinalTable.Rows[QB]["FluorescenceIntensity"].ToString() + "&_fid_63=" + FinalTable.Rows[QB]["Lab"].ToString() + "&_fid_64=" + FinalTable.Rows[QB]["DiamondID"].ToString() + "&_fid_65=" + FinalTable.Rows[QB]["Stone 1 Type"].ToString() + "&_fid_66=" + FinalTable.Rows[QB]["Stone 1 Shape"].ToString() + "&_fid_67=" + FinalTable.Rows[QB]["Stone 1 MM"].ToString() + "&_fid_68=" + FinalTable.Rows[QB]["Stone 1 Carat"].ToString() + "&_fid_69=" + FinalTable.Rows[QB]["Stone 1 Qty"].ToString() + "&_fid_70=" + FinalTable.Rows[QB]["Stone 2 Setting"].ToString() + "&_fid_71=" + FinalTable.Rows[QB]["Stone 2 Type"].ToString() + "&_fid_72=" + FinalTable.Rows[QB]["Stone 2 Shape"].ToString() + "&_fid_73=" + FinalTable.Rows[QB]["Stone 2 MM"].ToString() + "&_fid_74=" + FinalTable.Rows[QB]["Stone 2 Carat"].ToString() + "&_fid_75=" + FinalTable.Rows[QB]["Stone 2 Qty"].ToString() + "&_fid_76=" + FinalTable.Rows[QB]["Stone 2 Setting"].ToString() + "&_fid_78=" + FinalTable.Rows[QB]["Stone 3 Type"].ToString() + "&_fid_79=" + FinalTable.Rows[QB]["Stone 3 Shape"].ToString() + "&_fid_80=" + FinalTable.Rows[QB]["Stone 3 MM"].ToString() + "&_fid_81=" + FinalTable.Rows[QB]["Stone 3 Carat"].ToString() + "&_fid_82=" + FinalTable.Rows[QB]["Stone 3 Qty"].ToString() + "&_fid_83=" + FinalTable.Rows[QB]["Stone 3 Setting"].ToString() + "&_fid_245=" + FinalTable.Rows[QB]["Stone 1 Cost"].ToString() + "&_fid_246=" + FinalTable.Rows[QB]["Stone 2 Cost"].ToString() + "&_fid_247=" + FinalTable.Rows[QB]["Stone 3 Cost"].ToString() + "&_fid_77=" + GetWebName + "&_fid_345=" + FinalTable.Rows[QB]["CenterDiamondCarat"].ToString() + "&_fid_346=" + FinalTable.Rows[QB]["CenterDiamondCut"].ToString() + "&_fid_347=" + FinalTable.Rows[QB]["CenterDiamondColor"].ToString() + "&_fid_348=" + FinalTable.Rows[QB]["CenterDiamondClarity"].ToString() + "&_fid_349=" + FinalTable.Rows[QB]["Certificate"].ToString() + "&_fid_369=" + FinalTable.Rows[QB]["SilverPendantType"].ToString() + "&_fid_364=" + OrderPlatform + "&_fid_374=" + FinalTable.Rows[QB]["Semi Mount"].ToString() + "&_fid_381=" + "&_fid_381=" + FinalTable.Rows[QB]["Center_Stone_Shape"].ToString() + "&_fid_384=" + FinalTable.Rows[QB]["CouponCode"].ToString() + "&_fid_385=" + FinalTable.Rows[QB]["DiscountAmount"].ToString() + "&_fid_386=" + FinalTable.Rows[QB]["Sub_Total"].ToString() + "&usertoken=bzuxs9_cw8e_du2n5mbq9t6s7dqgxexu68h53s&apptoken=duby72vbbnfpx4dzsrwtjcbyqwep");



                    }
                    else if (FinalTable.Columns.Contains("Stone 2 Type"))
                    {

                        request = WebRequest.Create("https://nirgolan.quickbase.com/db/bmimrj5vm?a=API_AddRecord" + "&_fid_6=" + FinalTable.Rows[QB]["Order Date"].ToString() + "&_fid_7=" + FinalTable.Rows[QB]["Order number"].ToString() + "&_fid_8=" + FinalTable.Rows[QB]["Amount"].ToString() + "&_fid_9=" + FinalTable.Rows[QB]["Transaction"].ToString() + "&_fid_10=" + FinalTable.Rows[QB]["Payment Method"].ToString() + "&_fid_11=" + FinalTable.Rows[QB]["Amount Before Tax"].ToString() + "&_fid_12=" + FinalTable.Rows[QB]["Tax"].ToString() + "&_fid_13=" + FinalTable.Rows[QB]["Shipping City"].ToString() + "&_fid_14=" + FinalTable.Rows[QB]["Shipping Zip"].ToString() + "&_fid_15=" + FinalTable.Rows[QB]["ShippingService"].ToString() + "&_fid_16=" + FinalTable.Rows[QB]["Customer'sAccounts"].ToString() + "&_fid_17=" + FinalTable.Rows[QB]["Style No"].ToString() + "&_fid_18=" + FinalTable.Rows[QB]["Diamond Quality"].ToString() + "&_fid_19=" + FinalTable.Rows[QB]["Order Quantity"].ToString() + "&_fid_20=" + FinalTable.Rows[QB]["Gram Weight"].ToString() + "&_fid_21=" + FinalTable.Rows[QB]["Metal Karat"].ToString() + "&_fid_22=" + FinalTable.Rows[QB]["Metal Color"].ToString() + "&_fid_23=" + FinalTable.Rows[QB]["Labor Rate"].ToString() + "&_fid_24=" + FinalTable.Rows[QB]["Coupon"].ToString() + "&_fid_25=" + FinalTable.Rows[QB]["IGI"].ToString() + "&_fid_26=" + FinalTable.Rows[QB]["SilverReplicaText"].ToString() + "&_fid_27=" + FinalTable.Rows[QB]["Silver Pendant"].ToString() + "&_fid_28=" + FinalTable.Rows[QB]["Charm/Engraving"].ToString() + "&_fid_29=" + FinalTable.Rows[QB]["EngravingType"].ToString() + "&_fid_30=" + FinalTable.Rows[QB]["EngravingText"].ToString() + "&_fid_31=" + FinalTable.Rows[QB]["Rapnet Diamond"].ToString() + "&_fid_32=" + FinalTable.Rows[QB]["Anjolee Diamond"].ToString() + "&_fid_33=" + FinalTable.Rows[QB]["Customer Name"].ToString() + "&_fid_34=" + FinalTable.Rows[QB]["Customer email"].ToString() + "&_fid_35=" + FinalTable.Rows[QB]["Billing Phone Number"].ToString() + "&_fid_36=" + FinalTable.Rows[QB]["Bill/Ship Match"].ToString() + "&_fid_37=" + FinalTable.Rows[QB]["Payment Option"].ToString() + "&_fid_38=" + FinalTable.Rows[QB]["Shipping Name"].ToString() + "&_fid_39=" + FinalTable.Rows[QB]["Shipping Address1"].ToString() + "&_fid_40=" + FinalTable.Rows[QB]["Shipping Address2"].ToString() + "&_fid_41=" + FinalTable.Rows[QB]["Shipping Apt/Unit No"].ToString() + "&_fid_42=" + FinalTable.Rows[QB]["Shipping State"].ToString() + "&_fid_43=" + FinalTable.Rows[QB]["Shipping Country"].ToString() + "&_fid_44=" + FinalTable.Rows[QB]["Shipping Phone"].ToString() + "&_fid_45=" + FinalTable.Rows[QB]["Item Length/Ring Size"].ToString() + "&_fid_46=" + FinalTable.Rows[QB]["BandMetalType"].ToString() + "&_fid_47=" + FinalTable.Rows[QB]["VendorChildSku"].ToString() + "&_fid_48=" + FinalTable.Rows[QB]["Model Carat Weight"].ToString() + "&_fid_49=" + FinalTable.Rows[QB]["Effective Carat Weight"].ToString() + "&_fid_50=" + FinalTable.Rows[QB]["Vendor Metal Type"].ToString() + "&_fid_51=" + FinalTable.Rows[QB]["Stone Information Quality"].ToString() + "&_fid_52=" + FinalTable.Rows[QB]["Vendor Feed Price"].ToString() + "&_fid_53=" + FinalTable.Rows[QB]["Cut"].ToString() + "&_fid_54=" + FinalTable.Rows[QB]["Color"].ToString() + "&_fid_55=" + FinalTable.Rows[QB]["Clarity"].ToString() + "&_fid_56=" + FinalTable.Rows[QB]["Depth"].ToString() + "&_fid_57=" + FinalTable.Rows[QB]["Table"].ToString() + "&_fid_58=" + FinalTable.Rows[QB]["Girdle"].ToString() + "&_fid_59=" + FinalTable.Rows[QB]["Symmetry"].ToString() + "&_fid_60=" + FinalTable.Rows[QB]["Polish"].ToString() + "&_fid_61=" + FinalTable.Rows[QB]["CuletSize"].ToString() + "&_fid_62=" + FinalTable.Rows[QB]["FluorescenceIntensity"].ToString() + "&_fid_63=" + FinalTable.Rows[QB]["Lab"].ToString() + "&_fid_64=" + FinalTable.Rows[QB]["DiamondID"].ToString() + "&_fid_65=" + FinalTable.Rows[QB]["Stone 1 Type"].ToString() + "&_fid_66=" + FinalTable.Rows[QB]["Stone 1 Shape"].ToString() + "&_fid_67=" + FinalTable.Rows[QB]["Stone 1 MM"].ToString() + "&_fid_68=" + FinalTable.Rows[QB]["Stone 1 Carat"].ToString() + "&_fid_69=" + FinalTable.Rows[QB]["Stone 1 Qty"].ToString() + "&_fid_70=" + FinalTable.Rows[QB]["Stone 1 Setting"].ToString() + "&_fid_71=" + FinalTable.Rows[QB]["Stone 2 Type"].ToString() + "&_fid_72=" + FinalTable.Rows[QB]["Stone 2 Shape"].ToString() + "&_fid_73=" + FinalTable.Rows[QB]["Stone 2 MM"].ToString() + "&_fid_74=" + FinalTable.Rows[QB]["Stone 2 Carat"].ToString() + "&_fid_75=" + FinalTable.Rows[QB]["Stone 2 Qty"].ToString() + "&_fid_76=" + FinalTable.Rows[QB]["Stone 2 Setting"].ToString() + "&_fid_245=" + FinalTable.Rows[QB]["Stone 1 Cost"].ToString() + "&_fid_246=" + FinalTable.Rows[QB]["Stone 2 Cost"].ToString() + "&_fid_77=" + GetWebName + "&_fid_345=" + FinalTable.Rows[QB]["CenterDiamondCarat"].ToString() + "&_fid_346=" + FinalTable.Rows[QB]["CenterDiamondCut"].ToString() + "&_fid_347=" + FinalTable.Rows[QB]["CenterDiamondColor"].ToString() + "&_fid_348=" + FinalTable.Rows[QB]["CenterDiamondClarity"].ToString() + "&_fid_349=" + FinalTable.Rows[QB]["Certificate"].ToString() + "&_fid_369=" + FinalTable.Rows[QB]["SilverPendantType"].ToString() + "&_fid_364=" + OrderPlatform + "&_fid_374=" + FinalTable.Rows[QB]["Semi Mount"].ToString() + "&_fid_381=" + "&_fid_381=" + FinalTable.Rows[QB]["Center_Stone_Shape"].ToString() + "&_fid_384=" + FinalTable.Rows[QB]["CouponCode"].ToString() + "&_fid_385=" + FinalTable.Rows[QB]["DiscountAmount"].ToString() + "&_fid_386=" + FinalTable.Rows[QB]["Sub_Total"].ToString() + "&usertoken=bzuxs9_cw8e_du2n5mbq9t6s7dqgxexu68h53s&apptoken=duby72vbbnfpx4dzsrwtjcbyqwep");



                    }
                    else if (FinalTable.Columns.Contains("Stone 1 Type"))
                    {

                        request = WebRequest.Create("https://nirgolan.quickbase.com/db/bmimrj5vm?a=API_AddRecord" + "&_fid_6=" + FinalTable.Rows[QB]["Order Date"].ToString() + "&_fid_7=" + FinalTable.Rows[QB]["Order number"].ToString() + "&_fid_8=" + FinalTable.Rows[QB]["Amount"].ToString() + "&_fid_9=" + FinalTable.Rows[QB]["Transaction"].ToString() + "&_fid_10=" + FinalTable.Rows[QB]["Payment Method"].ToString() + "&_fid_11=" + FinalTable.Rows[QB]["Amount Before Tax"].ToString() + "&_fid_12=" + FinalTable.Rows[QB]["Tax"].ToString() + "&_fid_13=" + FinalTable.Rows[QB]["Shipping City"].ToString() + "&_fid_14=" + FinalTable.Rows[QB]["Shipping Zip"].ToString() + "&_fid_15=" + FinalTable.Rows[QB]["ShippingService"].ToString() + "&_fid_16=" + FinalTable.Rows[QB]["Customer'sAccounts"].ToString() + "&_fid_17=" + FinalTable.Rows[QB]["Style No"].ToString() + "&_fid_18=" + FinalTable.Rows[QB]["Diamond Quality"].ToString() + "&_fid_19=" + FinalTable.Rows[QB]["Order Quantity"].ToString() + "&_fid_20=" + FinalTable.Rows[QB]["Gram Weight"].ToString() + "&_fid_21=" + FinalTable.Rows[QB]["Metal Karat"].ToString() + "&_fid_22=" + FinalTable.Rows[QB]["Metal Color"].ToString() + "&_fid_23=" + FinalTable.Rows[QB]["Labor Rate"].ToString() + "&_fid_24=" + FinalTable.Rows[QB]["Coupon"].ToString() + "&_fid_25=" + FinalTable.Rows[QB]["IGI"].ToString() + "&_fid_26=" + FinalTable.Rows[QB]["SilverReplicaText"].ToString() + "&_fid_27=" + FinalTable.Rows[QB]["Silver Pendant"].ToString() + "&_fid_28=" + FinalTable.Rows[QB]["Charm/Engraving"].ToString() + "&_fid_29=" + FinalTable.Rows[QB]["EngravingType"].ToString() + "&_fid_30=" + FinalTable.Rows[QB]["EngravingText"].ToString() + "&_fid_31=" + FinalTable.Rows[QB]["Rapnet Diamond"].ToString() + "&_fid_32=" + FinalTable.Rows[QB]["Anjolee Diamond"].ToString() + "&_fid_33=" + FinalTable.Rows[QB]["Customer Name"].ToString() + "&_fid_34=" + FinalTable.Rows[QB]["Customer email"].ToString() + "&_fid_35=" + FinalTable.Rows[QB]["Billing Phone Number"].ToString() + "&_fid_36=" + FinalTable.Rows[QB]["Bill/Ship Match"].ToString() + "&_fid_37=" + FinalTable.Rows[QB]["Payment Option"].ToString() + "&_fid_38=" + FinalTable.Rows[QB]["Shipping Name"].ToString() + "&_fid_39=" + FinalTable.Rows[QB]["Shipping Address1"].ToString() + "&_fid_40=" + FinalTable.Rows[QB]["Shipping Address2"].ToString() + "&_fid_41=" + FinalTable.Rows[QB]["Shipping Apt/Unit No"].ToString() + "&_fid_42=" + FinalTable.Rows[QB]["Shipping State"].ToString() + "&_fid_43=" + FinalTable.Rows[QB]["Shipping Country"].ToString() + "&_fid_44=" + FinalTable.Rows[QB]["Shipping Phone"].ToString() + "&_fid_45=" + FinalTable.Rows[QB]["Item Length/Ring Size"].ToString() + "&_fid_46=" + FinalTable.Rows[QB]["BandMetalType"].ToString() + "&_fid_47=" + FinalTable.Rows[QB]["VendorChildSku"].ToString() + "&_fid_48=" + FinalTable.Rows[QB]["Model Carat Weight"].ToString() + "&_fid_49=" + FinalTable.Rows[QB]["Effective Carat Weight"].ToString() + "&_fid_50=" + FinalTable.Rows[QB]["Vendor Metal Type"].ToString() + "&_fid_51=" + FinalTable.Rows[QB]["Stone Information Quality"].ToString() + "&_fid_52=" + FinalTable.Rows[QB]["Vendor Feed Price"].ToString() + "&_fid_53=" + FinalTable.Rows[QB]["Cut"].ToString() + "&_fid_54=" + FinalTable.Rows[QB]["Color"].ToString() + "&_fid_55=" + FinalTable.Rows[QB]["Clarity"].ToString() + "&_fid_56=" + FinalTable.Rows[QB]["Depth"].ToString() + "&_fid_57=" + FinalTable.Rows[QB]["Table"].ToString() + "&_fid_58=" + FinalTable.Rows[QB]["Girdle"].ToString() + "&_fid_59=" + FinalTable.Rows[QB]["Symmetry"].ToString() + "&_fid_60=" + FinalTable.Rows[QB]["Polish"].ToString() + "&_fid_61=" + FinalTable.Rows[QB]["CuletSize"].ToString() + "&_fid_62=" + FinalTable.Rows[QB]["FluorescenceIntensity"].ToString() + "&_fid_63=" + FinalTable.Rows[QB]["Lab"].ToString() + "&_fid_64=" + FinalTable.Rows[QB]["DiamondID"].ToString() + "&_fid_65=" + FinalTable.Rows[QB]["Stone 1 Type"].ToString() + "&_fid_66=" + FinalTable.Rows[QB]["Stone 1 Shape"].ToString() + "&_fid_67=" + FinalTable.Rows[QB]["Stone 1 MM"].ToString() + "&_fid_68=" + FinalTable.Rows[QB]["Stone 1 Carat"].ToString() + "&_fid_69=" + FinalTable.Rows[QB]["Stone 1 Qty"].ToString() + "&_fid_70=" + FinalTable.Rows[QB]["Stone 1 Setting"].ToString() + "&_fid_245=" + FinalTable.Rows[QB]["Stone 1 Cost"].ToString() + "&_fid_77=" + GetWebName + "&_fid_345=" + FinalTable.Rows[QB]["CenterDiamondCarat"].ToString() + "&_fid_346=" + FinalTable.Rows[QB]["CenterDiamondCut"].ToString() + "&_fid_347=" + FinalTable.Rows[QB]["CenterDiamondColor"].ToString() + "&_fid_348=" + FinalTable.Rows[QB]["CenterDiamondClarity"].ToString() + "&_fid_349=" + FinalTable.Rows[QB]["Certificate"].ToString() + "&_fid_369=" + FinalTable.Rows[QB]["SilverPendantType"].ToString() + "&_fid_364=" + OrderPlatform + "&_fid_374=" + FinalTable.Rows[QB]["Semi Mount"].ToString() + "&_fid_381=" + "&_fid_381=" + FinalTable.Rows[QB]["Center_Stone_Shape"].ToString() + "&_fid_384=" + FinalTable.Rows[QB]["CouponCode"].ToString() + "&_fid_385=" + FinalTable.Rows[QB]["DiscountAmount"].ToString() + "&_fid_386=" + FinalTable.Rows[QB]["Sub_Total"].ToString() + "&usertoken=bzuxs9_cw8e_du2n5mbq9t6s7dqgxexu68h53s&apptoken=duby72vbbnfpx4dzsrwtjcbyqwep");


                    }


                    else
                    {
                        request = WebRequest.Create("https://nirgolan.quickbase.com/db/bmimrj5vm?a=API_AddRecord" + "&_fid_6=" + FinalTable.Rows[QB]["Order Date"].ToString() + "&_fid_7=" + FinalTable.Rows[QB]["Order number"].ToString() + "&_fid_8=" + FinalTable.Rows[QB]["Amount"].ToString() + "&_fid_9=" + FinalTable.Rows[QB]["Transaction"].ToString() + "&_fid_10=" + FinalTable.Rows[QB]["Payment Method"].ToString() + "&_fid_11=" + FinalTable.Rows[QB]["Amount Before Tax"].ToString() + "&_fid_12=" + FinalTable.Rows[QB]["Tax"].ToString() + "&_fid_13=" + FinalTable.Rows[QB]["Shipping City"].ToString() + "&_fid_14=" + FinalTable.Rows[QB]["Shipping Zip"].ToString() + "&_fid_15=" + FinalTable.Rows[QB]["ShippingService"].ToString() + "&_fid_16=" + FinalTable.Rows[QB]["Customer'sAccounts"].ToString() + "&_fid_17=" + FinalTable.Rows[QB]["Style No"].ToString() + "&_fid_18=" + FinalTable.Rows[QB]["Diamond Quality"].ToString() + "&_fid_19=" + FinalTable.Rows[QB]["Order Quantity"].ToString() + "&_fid_20=" + FinalTable.Rows[QB]["Gram Weight"].ToString() + "&_fid_21=" + FinalTable.Rows[QB]["Metal Karat"].ToString() + "&_fid_22=" + FinalTable.Rows[QB]["Metal Color"].ToString() + "&_fid_23=" + FinalTable.Rows[QB]["Labor Rate"].ToString() + "&_fid_24=" + FinalTable.Rows[QB]["Coupon"].ToString() + "&_fid_25=" + FinalTable.Rows[QB]["IGI"].ToString() + "&_fid_26=" + FinalTable.Rows[QB]["SilverReplicaText"].ToString() + "&_fid_27=" + FinalTable.Rows[QB]["Silver Pendant"].ToString() + "&_fid_28=" + FinalTable.Rows[QB]["Charm/Engraving"].ToString() + "&_fid_29=" + FinalTable.Rows[QB]["EngravingType"].ToString() + "&_fid_30=" + FinalTable.Rows[QB]["EngravingText"].ToString() + "&_fid_31=" + FinalTable.Rows[QB]["Rapnet Diamond"].ToString() + "&_fid_32=" + FinalTable.Rows[QB]["Anjolee Diamond"].ToString() + "&_fid_33=" + FinalTable.Rows[QB]["Customer Name"].ToString() + "&_fid_34=" + FinalTable.Rows[QB]["Customer email"].ToString() + "&_fid_35=" + FinalTable.Rows[QB]["Billing Phone Number"].ToString() + "&_fid_36=" + FinalTable.Rows[QB]["Bill/Ship Match"].ToString() + "&_fid_37=" + FinalTable.Rows[QB]["Payment Option"].ToString() + "&_fid_38=" + FinalTable.Rows[QB]["Shipping Name"].ToString() + "&_fid_39=" + FinalTable.Rows[QB]["Shipping Address1"].ToString() + "&_fid_40=" + FinalTable.Rows[QB]["Shipping Address2"].ToString() + "&_fid_41=" + FinalTable.Rows[QB]["Shipping Apt/Unit No"].ToString() + "&_fid_42=" + FinalTable.Rows[QB]["Shipping State"].ToString() + "&_fid_43=" + FinalTable.Rows[QB]["Shipping Country"].ToString() + "&_fid_44=" + FinalTable.Rows[QB]["Shipping Phone"].ToString() + "&_fid_45=" + FinalTable.Rows[QB]["Item Length/Ring Size"].ToString() + "&_fid_46=" + FinalTable.Rows[QB]["BandMetalType"].ToString() + "&_fid_47=" + FinalTable.Rows[QB]["VendorChildSku"].ToString() + "&_fid_48=" + FinalTable.Rows[QB]["Model Carat Weight"].ToString() + "&_fid_49=" + FinalTable.Rows[QB]["Effective Carat Weight"].ToString() + "&_fid_50=" + FinalTable.Rows[QB]["Vendor Metal Type"].ToString() + "&_fid_51=" + FinalTable.Rows[QB]["Stone Information Quality"].ToString() + "&_fid_52=" + FinalTable.Rows[QB]["Vendor Feed Price"].ToString() + "&_fid_53=" + FinalTable.Rows[QB]["Cut"].ToString() + "&_fid_54=" + FinalTable.Rows[QB]["Color"].ToString() + "&_fid_55=" + FinalTable.Rows[QB]["Clarity"].ToString() + "&_fid_56=" + FinalTable.Rows[QB]["Depth"].ToString() + "&_fid_57=" + FinalTable.Rows[QB]["Table"].ToString() + "&_fid_58=" + FinalTable.Rows[QB]["Girdle"].ToString() + "&_fid_59=" + FinalTable.Rows[QB]["Symmetry"].ToString() + "&_fid_60=" + FinalTable.Rows[QB]["Polish"].ToString() + "&_fid_61=" + FinalTable.Rows[QB]["CuletSize"].ToString() + "&_fid_62=" + FinalTable.Rows[QB]["FluorescenceIntensity"].ToString() + "&_fid_63=" + FinalTable.Rows[QB]["Lab"].ToString() + "&_fid_64=" + FinalTable.Rows[QB]["DiamondID"].ToString() + "&_fid_77=" + GetWebName + "&_fid_345=" + FinalTable.Rows[QB]["CenterDiamondCarat"].ToString() + "&_fid_346=" + FinalTable.Rows[QB]["CenterDiamondCut"].ToString() + "&_fid_347=" + FinalTable.Rows[QB]["CenterDiamondColor"].ToString() + "&_fid_348=" + FinalTable.Rows[QB]["CenterDiamondClarity"].ToString() + "&_fid_349=" + FinalTable.Rows[QB]["Certificate"].ToString() + "&_fid_369=" + FinalTable.Rows[QB]["SilverPendantType"].ToString() + "&_fid_364=" + OrderPlatform + "&_fid_374=" + FinalTable.Rows[QB]["Semi Mount"].ToString() + "&_fid_381=" + "&_fid_381=" + FinalTable.Rows[QB]["Center_Stone_Shape"].ToString() + "&_fid_384=" + FinalTable.Rows[QB]["CouponCode"].ToString() + "&_fid_385=" + FinalTable.Rows[QB]["DiscountAmount"].ToString() + "&_fid_386=" + FinalTable.Rows[QB]["Sub_Total"].ToString() + "&usertoken=bzuxs9_cw8e_du2n5mbq9t6s7dqgxexu68h53s&apptoken=duby72vbbnfpx4dzsrwtjcbyqwep");

                    }

                   
                    request.Method = "POST";
                   
                    string postData = "This is a test that posts this string to a Web server.";
                    byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                   
                    request.ContentType = "application/x-www-form-urlencoded";
                   
                    request.ContentLength = byteArray.Length;
                   
                    Stream dataStream = request.GetRequestStream();
                   
                    dataStream.Write(byteArray, 0, byteArray.Length);
                  
                    dataStream.Close();
                   
                    WebResponse response = request.GetResponse();
                
                    Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                  
                    dataStream = response.GetResponseStream();
                  
                    StreamReader reader = new StreamReader(dataStream);
                
               
                    string responseFromServer = reader.ReadToEnd();
                 
                    Console.WriteLine(responseFromServer);
                 
                    reader.Close();
                    dataStream.Close();
                    response.Close();


                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
                ErrorLog(ex.Source, ex.Message, ex.TargetSite.ToString(), ex.StackTrace, "SendtoQuickBaseWebservice");

            }
        }


        public DataSet GetSubcatImages(string ParentProductGroupID)
        {
            SqlConnection con = GetConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "usp_AppImages";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@ParentProductGroupID", ParentProductGroupID));
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            System.Data.DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }


        public DataSet GetSubcatIphoneImages(string ParentProductGroupID)
        {
            SqlConnection con = GetConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "usp_IphoneAppImages";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@ParentProductGroupID", ParentProductGroupID));
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            System.Data.DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }


        public DataSet GetSubcatIpadImages(string ParentProductGroupID)
        {
            SqlConnection con = GetConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "usp_IpadAppImages";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@ParentProductGroupID", ParentProductGroupID));
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            System.Data.DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        public DataSet ProductThumbnailSearch(string EternityType, string price, string caratweight, string stoneshape, string stonesetting, string JewelleryCollection, string GroupId, string NewArival, string SpecialOffer, string Sortingby, string SearchType, string PageIndex)
        {
            DataSet ds = new DataSet();
            string startIndex = "1";
            string lastIndex = "500";

            SqlConnection con = GetConnection();
            DataSet dsNarrowSearch = new DataSet();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "usp_GetThumbnailProductsSearch";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@EternityType", EternityType));
                cmd.Parameters.Add(new SqlParameter("@PRICE", price));
                cmd.Parameters.Add(new SqlParameter("@CARATWEIGHT", caratweight));
                cmd.Parameters.Add(new SqlParameter("@SHAPE", stoneshape));
                cmd.Parameters.Add(new SqlParameter("@STONESETTINGS", stonesetting));
                cmd.Parameters.Add(new SqlParameter("@JewelleryCollection", JewelleryCollection));
                cmd.Parameters.Add(new SqlParameter("@id", GroupId));
                cmd.Parameters.Add(new SqlParameter("@startIndex", startIndex));
                cmd.Parameters.Add(new SqlParameter("@lastIndex", lastIndex));
                cmd.Parameters.Add(new SqlParameter("@NewArival", NewArival));
                cmd.Parameters.Add(new SqlParameter("@Show_All", SpecialOffer));
                cmd.Parameters.Add(new SqlParameter("@SortingBy", Sortingby));
                cmd.Parameters.Add(new SqlParameter("@Flag", SearchType));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex)); 
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dsNarrowSearch);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
            }

            return dsNarrowSearch;
        }
        
       
        public DataSet GetJewelleryCollection(string GroupidId, string StatusType)
        {
            SqlConnection con = GetConnection();
            DataSet dsJCollection = new DataSet();
            try
            {
               
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GetJCollections_NEW";
                cmd.Parameters.Add(new SqlParameter("@ProductId", GroupidId));
                cmd.Parameters.Add(new SqlParameter("@Choice", StatusType));
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dsJCollection);
               
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
            }

            return dsJCollection;
        }
        public DataSet Getnewarrival(string ID)
        {
            SqlConnection con = GetConnection();
            DataSet ds = new DataSet();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "usp_NewArrivalCheck";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@p_productsproductsgroupid", ID));
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
            }
            return ds;
        }

        public DataSet GetWeddingbandData(string Flag)
        {
            SqlConnection con = GetConnection();
            DataSet ds = new DataSet();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "usp_Wedding_Bands";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Flag_Value", Flag));
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
            }
            return ds;
        }

        public int InsertNewsletterApps(string EmailID, string IpAddress, string DateSigned)
        {
            int status = -1;
            try
            {
                if (object.Equals(db, null))
                {
                    db = new DataBase();
                }
                param = new SqlParameter[4];
                param[0] = db.MakeInParameter("@email_address", SqlDbType.VarChar, 200, EmailID);
                param[1] = db.MakeInParameter("@ip_address", SqlDbType.VarChar, 50, IpAddress);
                param[2] = db.MakeInParameter("@date_signed", SqlDbType.VarChar, 50, DateSigned);
                param[3] = db.MakeOutParameter("@Status", SqlDbType.Int, 4);
                db.RunProcedure("P_InsertNewsletter_Iphone", param);
                status = (int)param[3].Value;
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }

            return status;
        }

        public DataSet GetLoosediamondDataApps(string pageIndex, string shape, string cut, string color, string clarity, string polish, string symmetry, string fluorescence, string sort_col, string sort_type, string carat_weight, string price)
        {
            SqlConnection con = GetConnection();
            System.Data.DataSet ds = new DataSet();
            try
            {
             
                int LastIndex = 20;
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "usp_Rapnetloosediamonds_Apps";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Shape", shape);
                cmd.Parameters.AddWithValue("@Carat", carat_weight);
                cmd.Parameters.AddWithValue("@Price", price);
                cmd.Parameters.AddWithValue("@Cut", cut);
                cmd.Parameters.AddWithValue("@Color", color);
                cmd.Parameters.AddWithValue("@Clarity", clarity);
                cmd.Parameters.AddWithValue("@Polish", polish);
                cmd.Parameters.AddWithValue("@Symmetry", symmetry);
                cmd.Parameters.AddWithValue("@Fluorescence", fluorescence);
                cmd.Parameters.AddWithValue("@PageIndex", pageIndex);
                cmd.Parameters.AddWithValue("@PageSize", LastIndex);
                cmd.Parameters.AddWithValue("@sort_col", sort_col);
                cmd.Parameters.AddWithValue("@sort_type", sort_type);
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                da.Dispose();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
            }
            return ds;
        }


        public DataSet GetvendorAutoBuildApps(string productId)
        {

            SqlConnection con = GetConnection();
            System.Data.DataSet ds = new DataSet();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "usp_AutoBuildVendorApp";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ProductID", productId));
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
            }
            return ds;
        }

        public DataSet GetVendorFeedCalculationApps(string ProductID, string MetalType, string BandWidth, string flag)
        {

            SqlConnection con = GetConnection();
            System.Data.DataSet ds = new DataSet();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "usp_VendorFeedDetailApps";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ProductID", ProductID));
                cmd.Parameters.Add(new SqlParameter("@Metal_Type", MetalType));
                cmd.Parameters.Add(new SqlParameter("@TotalCaratWeight", BandWidth));
                cmd.Parameters.Add(new SqlParameter("@Selection", flag));
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
            }
            return ds;
        }

        public DataSet GetProductMergedataApps(string ProductID)
        {
            SqlConnection con = GetConnection();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "usp_ProductDataApps";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@ProductID", ProductID));
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);

            return ds;
        }


        #region  //--- Android Push Notification Message----//
        public void InsertAndroidDeviceID(string DeviceID)
        {
            SqlConnection con = GetConnection();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SP_InertAndroidDeviceID";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.Add(new SqlParameter("@DeviceId", DeviceID.Trim()));


            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }

        public void UpdateAndroidDeviceIDStatus(string DeviceID)
        {
            SqlConnection con = GetConnection();
            int ViewStatus = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SP_UpdateAndroidDeviceViewStatus";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.Add(new SqlParameter("@DeviceId", DeviceID));
            cmd.Parameters.Add(new SqlParameter("@ViewStatus", ViewStatus));

            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }
        public DataSet AndroidPushnotification(string DeviceID)  
        {
            SqlConnection con = GetConnection();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();
           
            cmd.CommandText = "usp_AndroidPush_Notification";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@DeviceID", DeviceID));
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);

            return ds;
        }
        public DataSet AndroidPushnotificationDetail(string id)  
        {
            SqlConnection con = GetConnection();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();
            
            cmd.CommandText = "usp_AndroidPush_Notifications";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@id", id));
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);

            return ds;
        }


        # endregion //---- Android Push Notification Message -----//

       

      

        public DataSet LandingPageName(string SubCategoryId) 
        {
            SqlConnection con = GetConnection();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "productsgroupsId";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@Title", SubCategoryId));
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);

            return ds;
        }

       


     
        public DataSet GetProductListURL(string ProductURL)
        {
            SqlConnection con = GetConnection();
            DataSet ds = new DataSet();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SP_GetProductGroupidFromURL";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ProductURLtext", ProductURL));
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
            }
            return ds;
        }

        public DataSet LabCertifiedDiamondSearchEngine(string ProductID, string Weight, string Color, string Clarity, string Cut, string Polish, string Symmetry, string Fluorescence, string MinPrice, string MaxPrice, string SortbyColumns, string Orderby,string PageIndex)
        {
            SqlConnection con = GetConnection();
            System.Data.DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SP_GetLabCertifiedDiamondSearch_SearchData";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ProductID", ProductID));
                cmd.Parameters.Add(new SqlParameter("@weight", Weight));
                cmd.Parameters.Add(new SqlParameter("@Color", Color));
                cmd.Parameters.Add(new SqlParameter("@Clarity", Clarity));
                cmd.Parameters.Add(new SqlParameter("@Cut", Cut));
                cmd.Parameters.Add(new SqlParameter("@Polish", Polish));
                cmd.Parameters.Add(new SqlParameter("@Symmetry", Symmetry));
                cmd.Parameters.Add(new SqlParameter("@Fluorescence", Fluorescence));
                cmd.Parameters.Add(new SqlParameter("@Min_Value", MinPrice));
                cmd.Parameters.Add(new SqlParameter("@Max_Value", MaxPrice));
                cmd.Parameters.Add(new SqlParameter("@Sort_Field", SortbyColumns));
                cmd.Parameters.Add(new SqlParameter("@Sort_Order", Orderby));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                da.Dispose();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
            }
            return ds;
        }



        public DataTable  GetLandingPageMetaTag(string ProductGroupID)
        {
            SqlConnection con = GetConnection();
            System.Data.DataTable dt = new DataTable();

            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SP_LandingPageMetaData";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@id", ProductGroupID));
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            return dt;

        }


        public DataTable GetThumbnailProductDetails(string ProductName)
        {
            SqlConnection con = GetConnection();
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "usp_P_GetProductThumbnailSearch_Welcome";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ProductName", ProductName));
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
            }
            return dt;
        }


        public DataTable GetProductDetailStoneType1(string ID, string sizeid)
        {
            SqlConnection con = GetConnection();
            System.Data.DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SP_GetProductDetailStoneType1";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id", ID));
                cmd.Parameters.Add(new SqlParameter("@sizeid", sizeid));
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
            }
            return dt;
        }

        public DataTable GetProductDetailStoneType2(string ID, string sizeid)
        {
            SqlConnection con = GetConnection();
            System.Data.DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SP_GetProductDetailStoneType2";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id", ID));
                cmd.Parameters.Add(new SqlParameter("@sizeid", sizeid));
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
            }
            return dt;
        }


        public DataTable GetProductDetailDiamond(string ID, string sizeid, string stoneid)
        {
            SqlConnection con = GetConnection();
            System.Data.DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SP_GetProductDetailDiamond";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id", ID));
                cmd.Parameters.Add(new SqlParameter("@sizeid", sizeid));
                cmd.Parameters.Add(new SqlParameter("@stoneid", stoneid));
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
            }
            return dt;
        }

        public DataTable GetProductDetailColorStone(string ID, string sizeid, string stoneid)
        {
            SqlConnection con = GetConnection();
            System.Data.DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SP_GetProductDetailColorStone";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id", ID));
                cmd.Parameters.Add(new SqlParameter("@sizeid", sizeid));
                cmd.Parameters.Add(new SqlParameter("@stoneid", stoneid));
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
            }
            return dt;
        }


        public DataTable GetProductDetailDiamondNew(string ID, string sizeid, string stoneid, string SelLength)
        {
            SqlConnection con = GetConnection();
            System.Data.DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SP_GetProductDetailDiamond_NEW";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id", ID));
                cmd.Parameters.Add(new SqlParameter("@sizeid", sizeid));
                cmd.Parameters.Add(new SqlParameter("@stoneid", stoneid));
                cmd.Parameters.Add(new SqlParameter("@SelectedLength", SelLength));
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
            }
            return dt;
        }


        public DataTable GetProductDetailColorStoneNew(string ID, string sizeid, string stoneid, string SelLength)
        {
            SqlConnection con = GetConnection();
            System.Data.DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SP_GetProductDetailColorStone_New";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id", ID));
                cmd.Parameters.Add(new SqlParameter("@sizeid", sizeid));
                cmd.Parameters.Add(new SqlParameter("@stoneid", stoneid));
                cmd.Parameters.Add(new SqlParameter("@SelectedLength", SelLength));
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
            }
            return dt;
        }

        public DataTable GetEternityStoneCarat_Sizes(int Condition, string ProductId, string ProductSizeId, string RingSize)
        {
            SqlConnection con = GetConnection();
            System.Data.DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "usp_Get_RingSize_CWCalc";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CONDIATION", Condition));
                cmd.Parameters.Add(new SqlParameter("@productID", ProductId));
                cmd.Parameters.Add(new SqlParameter("@ProductSizeID", ProductSizeId));
                cmd.Parameters.Add(new SqlParameter("@RingSize", Convert.ToDecimal(RingSize)));


                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                con.Close();
            }
            return dt;
        }

        public DataSet GetRapNetdataShapeWise(string Productid, string Weight, string ProductSizeID, int pageIndex, int pageSize)
        {
            string constring = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constring))
            {

                using (SqlCommand cmd = new SqlCommand("usp_tbl_RepNet_DataField"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@weight", Weight);
                    cmd.Parameters.AddWithValue("@ProductID", Productid);
                    cmd.Parameters.AddWithValue("@ProductSizeID", ProductSizeID);
                    cmd.Parameters.AddWithValue("@PageIndex", pageIndex);
                    cmd.Parameters.AddWithValue("@PageSize", pageSize);
                    cmd.Parameters.Add("@PageCount", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataSet ds = new DataSet())
                        {
                            sda.Fill(ds, "Customers");
                            DataTable dt = new DataTable("PageCount");
                            dt.Columns.Add("PageCount");
                            dt.Rows.Add();
                            dt.Rows[0][0] = cmd.Parameters["@PageCount"].Value;
                            ds.Tables.Add(dt);
                            return ds;
                        }
                    }
                }
            }
        }


        public DataSet GetThumbnaildata(string gpuid,string Flag)
        {
            SqlConnection con = GetConnection();
            string query = "usp_MetaData_Detail";
            SqlCommand cmd = new SqlCommand(query);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ProductsGroupID", gpuid);
            cmd.Parameters.AddWithValue("@Flag", Flag);
            cmd.Connection = con;
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                cmd.Connection = con;
                sda.SelectCommand = cmd;
                using (DataSet dsTH = new DataSet())
                {
                    sda.Fill(dsTH);
                    return dsTH;
                }
            }
        }
    }
   
}