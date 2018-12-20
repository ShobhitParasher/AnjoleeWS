using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Net;
using System.IO;
using System.Web.Hosting;
using System.Web.Profile;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml;
using ShoppingCartGeneric;
using System.Collections.Generic;
using DBComponent;
using System.Net.Mail;
using System.Web.Script.Serialization;
using System.Text;

[WebService(Namespace = "http://anjolee.com/anjoleews")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService]
public class Service : System.Web.Services.WebService
{
    DBComponent.CommonFunctions objComFun = new DBComponent.CommonFunctions();
    DBComponent.EncryptDecrypt objEncry_Decrypt = new DBComponent.EncryptDecrypt();
    tblContacts.tblContacts objContacts = null;                                                                                         //'''''''''' Object For User Contacts property like Name etc 
    tblContacts.tblContactsHelper objContactsHelper = null;                                                                             //'''''''''' Object For User Contacts Methods like InsertContacts
    tblOrders.tblOrders objOrderNew = null;
    tblProductsSizes.tblProductsSizesHelper ojbProdsizeHelper = new tblProductsSizes.tblProductsSizesHelper();
    tblOrders.tblOrdersHelper objOrdersHelper = new tblOrders.tblOrdersHelper();                                                        //'''''''''' Object For Order Method like InsertOredrs etc   
    tblUsers.tblUsers objUsers = null;
    tblUsers.tblUsersHelper objUsersHelper = null;
    tblStore_Shipping.tblStore_ShippingHelper otblStore_ShippingHelper = null;
    tblStore_Prefs.tblStorePrefsHelper otblStorePrefsHelper = null;
    tblPaymentMethods_Info.tblPaymentMethods_InfoHelper objPayment_InfoHelper = new tblPaymentMethods_Info.tblPaymentMethods_InfoHelper();
    tblStore_Email.tblStore_EmailHelper objEmailHelper = null;
    string receiptEmail = string.Empty;
    string receiptSubject = string.Empty;
    string eur_msg = string.Empty;
    string eur_replace = string.Empty;
    string eur_ship = string.Empty;
    static string jsonString = string.Empty;
    private tbl_Newsletter.tbl_Newsletter objTblNewsLetter = null;
    private tbl_Newsletter.tbl_NewsletterHelper otbl_NewsletterOnlyHelper = null;
    public static decimal OrderLen;
    public static decimal StoneNo;
    public static decimal StoneNoForNew;
    public static decimal defaltlen;
    public static DataTable DtfixedCharge;
    public static string ProdCtSize;
    string _strError;
    string ProductCaratPremium = string.Empty;
    decimal TotPriceCaratPremium = 0;
    public string EffectiveWt = string.Empty;
    bool err = false;
    public string carWt = string.Empty;
    public Service()
    {
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }
    [System.Web.Script.Services.ScriptMethod]

    #region  //--------------- Common Methods----------------------//
    [WebMethod]
    public string GetDeliveryDate()
    {
        string shipdate = string.Empty;
        string Tshipdate = string.Empty;
        string deliverDate = string.Empty;
        string sDate = Convert.ToString(System.DateTime.Now.DayOfWeek);
        if (sDate == "Wednesday" || sDate == "Thursday" || sDate == "Friday" || sDate == "Saturday" || sDate == "Tuesday")
        {
            Tshipdate = Convert.ToString(System.DateTime.Now.AddDays(6).ToString("ddd,MMM dd yyyy"));
        }
        else if (sDate == "Monday")
        {
            Tshipdate = Convert.ToString(System.DateTime.Now.AddDays(4).ToString("ddd,MMM dd yyyy"));
        }
        else if (sDate == "Sunday")
        {
            Tshipdate = Convert.ToString(System.DateTime.Now.AddDays(5).ToString("ddd,MMM dd yyyy"));
        }
        DateTime dt = Convert.ToDateTime(Tshipdate);
        shipdate = dt.ToString("dddd, MMMM d");
        if (shipdate.Contains("Fri"))
        {

            deliverDate = Convert.ToDateTime(Tshipdate).AddDays(3).ToString("ddd, MMM dd");
        }
        else if (shipdate.Contains("Saturday"))
        {
            Tshipdate = Convert.ToString(System.DateTime.Now.AddDays(2).ToString("ddd,MMM dd yyyy"));
        }
        else
        {

            deliverDate = Convert.ToDateTime(Tshipdate).AddDays(1).ToString("ddd, MMM dd");
        }

        deliverDate = "Order Today, Estimated Delivery on " + deliverDate;
        return deliverDate;
    }

    [WebMethod]
    public string GetProductDeliveryDate()
    {
        string shipdate = string.Empty;
        string Tshipdate = string.Empty;
        string deliverDate = string.Empty;
        string sDate = Convert.ToString(System.DateTime.Now.DayOfWeek);
        if (sDate == "Wednesday" || sDate == "Thursday" || sDate == "Friday" || sDate == "Saturday" || sDate == "Tuesday")
        {
            Tshipdate = Convert.ToString(System.DateTime.Now.AddDays(6).ToString("ddd,MMM dd yyyy"));
        }
        else if (sDate == "Monday")
        {
            Tshipdate = Convert.ToString(System.DateTime.Now.AddDays(4).ToString("ddd,MMM dd yyyy"));
        }
        else if (sDate == "Sunday")
        {
            Tshipdate = Convert.ToString(System.DateTime.Now.AddDays(5).ToString("ddd,MMM dd yyyy"));
        }
        DateTime dt = Convert.ToDateTime(Tshipdate);
        shipdate = dt.ToString("dddd, MMMM d");
        if (shipdate.Contains("Fri"))
        {

            //deliverDate = Convert.ToDateTime(Tshipdate).AddDays(3).ToString("ddd, MMM dd");
            deliverDate = Convert.ToDateTime(Tshipdate).AddDays(3).ToString("dddd, MMMM d");
        }
        else if (shipdate.Contains("Saturday"))
        {
            Tshipdate = Convert.ToString(System.DateTime.Now.AddDays(2).ToString("ddd,MMM dd yyyy"));
        }
        else
        {

            //deliverDate = Convert.ToDateTime(Tshipdate).AddDays(1).ToString("ddd, MMM dd");
            deliverDate = Convert.ToDateTime(Tshipdate).AddDays(1).ToString("dddd, MMMM d");
        }

        deliverDate = "Order By: 4PM EST Today. Estimated Delivery on " + deliverDate;
        return deliverDate;
    }

    [WebMethod(Description = "Breadcrubs URL")]
    public void GetBreadcrubsURL(string ProductId)
    {
        DataSet dsBreadcrubs = new DataSet();
        dsBreadcrubs = objComFun.GetBreadcrubsURL(ProductId);

        if (dsBreadcrubs.Tables.Count > 0)
        {
            DataTable dt = dsBreadcrubs.Tables[0];
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Write(DataTableToJSONWithJavaScriptSerializer(dt));
        }
        else
        {
            Context.Response.Write("False");
        }      

    }

    [WebMethod(Description = " Special Announcement Message ")]
    public void GetSpecialAnnouncement(string ID)
    {
        DataSet dsSpecialAnnouncement = objComFun.GetSpecialAnnouncement(ID);

        if (dsSpecialAnnouncement.Tables.Count > 0)
        {
            DataTable dt = dsSpecialAnnouncement.Tables[0];
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Write(DataTableToJSONWithJavaScriptSerializer(dt));
           
        }
        else
        {
            Context.Response.Write("False");

        }

    }

    [WebMethod(Description = "Product Search by Style number or product Name")]
    public void GetProductSerachByStyleNo(string strSearch)
    {
        DataSet dstProductSeach = new DataSet();
        dstProductSeach = objComFun.GetProductSerachByStyleNo(strSearch);


        if (dstProductSeach.Tables[0].Rows.Count != 0)
        {
            DataTable dt = dstProductSeach.Tables[0];
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Write(DataTableToJSONWithJavaScriptSerializer(dt));
        }
        else
        {
            Context.Response.Write("False");
        }
    }
      
    [WebMethod(Description = "Home Page Video")]
    public void GetHomeVideo()
    {


        DataSet ds = new DataSet();
        ds = objComFun.HomepageNotification();
        if (ds.Tables.Count > 0)
        {
            DataTable dt = ds.Tables[0];
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Write(DataTableToJSONWithJavaScriptSerializer(dt));

        }
        else
        {
            Context.Response.Write("False");

        }

    }

    [WebMethod(Description = "Product Category Details")]
    public void GetCategoryDetails( string GroupId)
    {


        DataTable dt = new DataTable();
        dt = objComFun.GetCategoryDetail(GroupId);
        if (dt.Rows.Count > 0)
        {
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Write(DataTableToJSONWithJavaScriptSerializer(dt));

        }
        else
        {
            Context.Response.Write("False");

        }

    }

    [WebMethod(Description = " Get Product Id)")]
    public void GetProductID(string ProductName)
    {
        DataTable dtStatus = new DataTable();
        dtStatus.Columns.Add("ProductId");
        DataRow _dataRow = dtStatus.NewRow();
        try
        {
           
            string proID = objComFun.GetProID(ProductName);
            if (proID != string.Empty)
            {               
                _dataRow["ProductId"] = proID;
               
            }else
            {
                _dataRow["ProductId"] = "DataNotFound";               

            }
            dtStatus.Rows.Add(_dataRow);            
            

        }
        catch (Exception ex)
        {           
            _dataRow["ProductId"] = "DataNotFound";
            dtStatus.Rows.Add(_dataRow);
            
        }

       
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Write(DataTableToJSONWithJavaScriptSerializer(dtStatus));
       


    }

    #endregion //----Common Methods -----//

    #region  //--------------- Subscribe Newsletter ----------------------//
    [WebMethod(Description = "Subscribe Newsletter")]
    public void GetSubscribeNewsletter(string EmailAddress, string IpAddress)
    {
        DataTable dtNewsletter = new DataTable("table");
        dtNewsletter.Columns.Add("NewsletterStatus");
        string datesigned = DateTime.Now.ToShortDateString();
        int Status = objComFun.InsertNewsletter(EmailAddress, IpAddress, datesigned);
        DataRow dr = dtNewsletter.NewRow();
        dr["NewsletterStatus"] = Status;
        dtNewsletter.Rows.Add(dr);
        if (dtNewsletter.Rows.Count > 0)
        {
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Write(DataTableToJSONWithJavaScriptSerializer(dtNewsletter));
        }
        else
        {
            Context.Response.Write("False");
        }
    }



    #endregion //---------------Subscribe Newsletter----------------------//

    #region  //--------------- Thumbnail Page----------------------//
    [WebMethod(Description = "Branding Image on Top for Thumbnail and Landing page")]
    public void GetBrandingImages(string GroupId)
    {


        DataTable dt = new DataTable();
        dt = objComFun.GetBrandingImages(GroupId);
        if (dt.Rows.Count > 0)
        {
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Write(DataTableToJSONWithJavaScriptSerializer(dt));

        }
        else
        {
            Context.Response.Write("False");

        }

    }

    [WebMethod(Description = "Jewellery Collection List ")]
    public void GetAllJewelleryCollection(string GroupId)
    {
        try
        {
            DataSet dsewelleryCollection = new DataSet();

            dsewelleryCollection = objComFun.GetAllJewelleryCollection(GroupId);

            if (dsewelleryCollection.Tables.Count > 0)
            {
                DataTable dt = dsewelleryCollection.Tables[0];               
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.Write(DataTableToJSONWithJavaScriptSerializer(dt));
            }
            else
            {
                Context.Response.Write("False");
            }
        }
        catch (Exception ex)
        {

            Context.Response.Write("False");

        }
    }

    [WebMethod(Description = "Thumbnail page Search")]
    public void GetThumbnailProductSearch(string EternityCollection,string Price, string Caratweight, string StoneShapes, string Stonesettings, string JewelleryCollections, string GroupId, string NewArival, string SpecialOffer, string SortingBy, string PageIndex)
    {
        try
        {
            DataSet dsthumbnailSearch = new DataSet();
            dsthumbnailSearch = objComFun.ProductThumbnailProductSearch(EternityCollection,Price, Caratweight, StoneShapes, Stonesettings, JewelleryCollections, GroupId, NewArival, SpecialOffer, SortingBy, PageIndex);

            if (dsthumbnailSearch.Tables.Count > 0)
            {
                DataTable dt = dsthumbnailSearch.Tables[0];
                //return DataTableToJSONWithJavaScriptSerializer(dt);
                // JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.Write(DataTableToJSONWithJavaScriptSerializer(dt));
            }
            else
            {
                Context.Response.Write("False");
            }

        }
        catch (Exception ex)
        {

            Context.Response.Write(ex.Message);

        }

    }


    [WebMethod(Description = "Get Product details ")]
    public void GetThumbnailProductDetails(string ProductName)
    {
        try
        {
            DataTable  dtThumbnailProductDetails = new DataTable();

            dtThumbnailProductDetails = objComFun.GetThumbnailProductDetails(ProductName);

            if (dtThumbnailProductDetails.Rows.Count > 0)
            {
               
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.Write(DataTableToJSONWithJavaScriptSerializer(dtThumbnailProductDetails));
            }
            else
            {
                Context.Response.Write("False");
            }
        }
        catch (Exception ex)
        {

            Context.Response.Write("False");

        }
    }



    [WebMethod(Description = "Get Thumbnail and Landing page Meta data ")]
    public void GetThumbnailMetaData(string ProductGroupId,string Flag)
    {
        try
        {
            
            DataSet DSThumbnaildata = objComFun.GetThumbnaildata(ProductGroupId,Flag);

            if (DSThumbnaildata.Tables.Count > 0)
            {
                DataTable dt = DSThumbnaildata.Tables[0];               
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.Write(DataTableToJSONWithJavaScriptSerializer(dt));
            }
            else
            {
                Context.Response.Write("False");
            }
        }
        catch (Exception ex)
        {
            string str = ex.ToString();
            Context.Response.Write("False");
        }


    }

    #endregion  //--------------- Thumbnail Page----------------------//

    #region  //--------------- Wedding Band pages ----------------------//

    [WebMethod(Description = "Classic Band Collection")]
    public void GetClassicBandcollection()
    {
        DataSet dsClassicBand = new DataSet();
        dsClassicBand = objComFun.GetClassicBandcollection();
        if (dsClassicBand.Tables[0].Rows.Count != 0)
        {
            DataTable dt = dsClassicBand.Tables[0];
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Write(DataTableToJSONWithJavaScriptSerializer(dt));
        }
        else
        {
            Context.Response.Write("False");
        }

    }
    [WebMethod(Description = "Designer Band Collection")]
    public void GetDesignerbandcollection()
    {
        DataSet dsDesignerBand = new DataSet();
        dsDesignerBand = objComFun.GetDesignerbandcollection();
        if (dsDesignerBand.Tables[0].Rows.Count != 0)
        {
            DataTable dt = dsDesignerBand.Tables[0];
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Write(DataTableToJSONWithJavaScriptSerializer(dt));
        }
        else
        {
            Context.Response.Write("False");
        }

    }
    [WebMethod(Description = "Modern Band Collection")]
    public void GetModernbandcollection()
    {
        DataSet dsModernBand = new DataSet();
        dsModernBand = objComFun.GetModernbandcollection();
        if (dsModernBand.Tables[0].Rows.Count != 0)
        {
            DataTable dt = dsModernBand.Tables[0];
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Write(DataTableToJSONWithJavaScriptSerializer(dt));
        }
        else
        {
            Context.Response.Write("False");
        }

    }


    [WebMethod(Description = "Get Wedding Band Detail ")]
    public void GetWeddingBandConfigurations(string productId, string Mtype, string ClassicWeight, string ProductName)
    {
        try
        {
            DataSet dsBand = objComFun.GetWeddingAutoBuild(productId, Mtype, ClassicWeight, ProductName);
            if (dsBand.Tables[0].Rows.Count != 0)
            {
                DataTable dt = dsBand.Tables[0];
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.Write(DataTableToJSONWithJavaScriptSerializer(dt));
            }
            else
            {
                Context.Response.Write("False");
            }
        }
        catch (Exception ex)
        {
            Context.Response.Write("False");


        }

    }

    [WebMethod(Description = "Get Wedding Band Price")]
    public void GetWBandCalculation(string ProductID, string MetalType, string RingSizes, string BandWidth, string flag)
    {
        try
        {
            DataSet dsWBand = objComFun.GetWBandCalculation(ProductID, MetalType, RingSizes, BandWidth, flag);
            if (dsWBand.Tables[0].Rows.Count != 0)
            {
                DataTable dt = dsWBand.Tables[0];
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.Write(DataTableToJSONWithJavaScriptSerializer(dt));
            }
            else
            {
                Context.Response.Write("False");
            }
        }
        catch (Exception ex)
        {
            Context.Response.Write("False");


        }

    }
       

    [WebMethod(Description = "Get Vendor Feed ")]
    public void GetVendorfeed(string ProductID)
    {
        try
        {
            DataSet dsVendorfeed = objComFun.GetvendorAutoBuild(ProductID);
            if (dsVendorfeed.Tables[0].Rows.Count != 0)
            {
                DataTable dt = dsVendorfeed.Tables[0];
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.Write(DataTableToJSONWithJavaScriptSerializer(dt));
            }
            else
            {
                Context.Response.Write("False");
            }
        }
        catch (Exception ex)
        {
            Context.Response.Write("False");


        }

    }

    [WebMethod(Description = "Get VendorFeed Price")]
    public void GetVendorFeedCalculation(string ProductID, string MetalType, string BandWidth, string flag)
    {
        try
        {
            DataSet dsVendorfeed = objComFun.GetVendorFeedCalculation(ProductID, MetalType, BandWidth, flag);
            if (dsVendorfeed.Tables[0].Rows.Count != 0)
            {
                DataTable dt = dsVendorfeed.Tables[0];
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.Write(DataTableToJSONWithJavaScriptSerializer(dt));
            }
            else
            {
                Context.Response.Write("False");
            }
        }
        catch (Exception ex)
        {
            Context.Response.Write("False");


        }

    }





    #endregion //--------------- Wedding Band pages ----------------------//

    #region  //--------------- Landing page ----------------------//
    [WebMethod(Description = "Get Group Id for Landing page )")]
    public void GetLandingpage(string PageURL)
    {
        DataSet dsLanding = objComFun.GetLandingPage(PageURL);
        if (dsLanding.Tables[0].Rows.Count != 0)
        {
            DataTable dt = dsLanding.Tables[0];
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Write(DataTableToJSONWithJavaScriptSerializer(dt));
        }
        else
        {
            Context.Response.Write("False");
        }
    }

    [WebMethod(Description = "Landing Page Detail )")]
    public void LandingProductsdata(string GroupId, string NewArival, string SpecialOffer, string SortingBy,string PageIndex)
    {
        try
        {
            DataSet DsNarrowSearch = new DataSet();
            DsNarrowSearch = objComFun.LandingData(GroupId, NewArival, SpecialOffer, SortingBy, PageIndex);

            
            if (DsNarrowSearch.Tables.Count > 0)
            {
                DataTable dt = DsNarrowSearch.Tables[0];
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.Write(DataTableToJSONWithJavaScriptSerializer(dt));
            }
            else
            {
                Context.Response.Write("False");
            }

        }
        catch (Exception ex)
        {

            Context.Response.Write("False");

        }

    }

    #endregion //--------------- Landing page ----------------------//

    #region  //--------------- Loose Diamond page ----------------------//
    [WebMethod(Description = "Loose Diamond  Data.")]
    public void GetLooseDiamonddata(string pageIndex, string shape, string cut, string color, string clarity, string polish, string symmetry, string fluorescence, string sort_col, string sort_type, string carat_weight, string price)
    {
        try
        {
            DataSet ds = objComFun.GetLoosediamondData(pageIndex, shape, cut, color, clarity, polish, symmetry, fluorescence, sort_col, sort_type, carat_weight, price);
            if (ds.Tables[0].Rows.Count != 0)
            {
                DataTable dt = ds.Tables[0];
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.Write(DataTableToJSONWithJavaScriptSerializer(dt));
            }
            else
            {
                Context.Response.Write("False");
            }
        }
        catch (Exception ex)
        {
            Context.Response.Write(ex.Message);


        }
    }


    [WebMethod(Description = "Loose Diamond  Details.")]
    public void GetLoosediamondSelectedData(string DiamondID)
    {

        try
        {
            DataSet ds = objComFun.GetLoosediamondSelectedData(DiamondID);
            if (ds.Tables[0].Rows.Count != 0)
            {
                DataTable dt = ds.Tables[0];
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.Write(DataTableToJSONWithJavaScriptSerializer(dt));
            }
            else
            {
                Context.Response.Write("False");
            }

        }
        catch (Exception ex)
        {

            Context.Response.Write("False");

        }


    }

    [WebMethod(Description = "Thumbnail Loose Diamond Search Data.")]
    public void LooseDiamondThumbnailSearch(string GroupId, string pageIndex, string specialOffer, string Newarrival, string Settingtype, string Stoneshape, string Collection, string carat_weight, string sort_type, string sort_order)
    {

        try
        {
            DataSet ds = objComFun.LooseDiamondThumbnailSearch(GroupId, pageIndex, specialOffer, Newarrival, Settingtype, Stoneshape, Collection, carat_weight, sort_type, sort_order);
            if (ds.Tables[0].Rows.Count != 0)
            {
                DataTable dt = ds.Tables[0];
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.Write(DataTableToJSONWithJavaScriptSerializer(dt));
            }
            else
            {
                Context.Response.Write("False");
            }

        }
        catch (Exception ex)
        {

            Context.Response.Write("False");

        }


    }

    #endregion //--------------- Loose Diamond page ----------------------//

    #region  //--------------- Diamond Studs Earrings page ----------------------//
    [WebMethod(Description = "Diamond Studs Earrings Price Calculation.")]
    public void GetDiamondStudsEarringProducts(string ProductID, string Metaltype, string MetalValue, string Color, string Clarity)
    {

        try
        {
            DataTable dt = objComFun.GetDiamondStudsEarringProducts(ProductID, Metaltype, MetalValue, Color, Clarity);
            if (dt.Rows.Count > 0)
            {

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.Write(DataTableToJSONWithJavaScriptSerializer(dt));
            }
            else
            {
                Context.Response.Write("False");
            }

        }
        catch (Exception ex)
        {

            Context.Response.Write("False");

        }


    }


    #endregion //--------------- Diamond Studs Earrings page ----------------------//

    #region  //--------------- Loose Diamond page ----------------------//
    [WebMethod(Description = "Product Configurations details")]
    public string  GetProductsConfigurations(string ProductID)
    {

        try
        {
           DataSet ds = objComFun.GetProductsConfigurations(ProductID);
            if (ds.Tables[0].Rows.Count != 0)
            {
                jsonString = Serialize(ds);
                return jsonString;
            }else
            {

               
                return jsonString;

            }
            

        }
        catch (Exception ex)
        {

            return "False";

        }


    }

    [WebMethod(Description = "Approx Metal Weight")]
    public decimal GetApproxMetalWeight(string MT, string MQ, string Isenable, string pid, string Caretid, string Length)
    {
        string Metals = string.Empty;
        decimal PEW = 0;
        decimal EWtAT14k;
        decimal ordLength = Convert.ToDecimal(Length);
        OrderLen = ordLength;
        try
        {
            string str = "select [14kDefaultWeight],Platinumlabor,GoldLabor,Defaultlength from ProductSizes where productid='" + pid + "'and Productsizeid='" + Caretid + "'";
            DataTable dt = objComFun.ExecuteQueryReturnDataTable(str);
            defaltlen = Convert.ToDecimal(dt.Rows[0]["Defaultlength"].ToString());
            string strSql = "SELECT ltrim(rtrim(case when exists(select Bangle_Length  from products ds where ds.productId='" + pid + "' and ISNULL(ProductGroup_SubSubCategoryName, '') IN ('BanglesDiamondLength','BanglesMetalLength')) then (select Bangle_Length  from products ds where ds.productId='" + pid + "' and ISNULL(ProductGroup_SubSubCategoryName, '') IN ('BanglesDiamondLength','BanglesMetalLength')) else DefaultSize end)) DefaultSize  FROM ProductsGroups WHERE ProductsGroupID IN(SELECT ProductsProductsGroupID FROM ProductsProductsGroups WHERE ProductID='" + pid + "')";
            string strq = objComFun.ExecuteQueryReturnSingleString(strSql);
            if (strq.Trim() == "")
            {

                defaltlen = 1;
                OrderLen = 1;

            }
            if (Isenable.Trim() == "T")
            {
                OrderLen = defaltlen;
            }

            if (Isenable.Trim() == "ET")
            {
                OrderLen = defaltlen;
            }

            if (defaltlen != 0)
            {
                if (OrderLen != defaltlen)
                {
                    EWtAT14k = (Convert.ToDecimal(dt.Rows[0]["14kDefaultWeight"]) * OrderLen) / defaltlen;
                }
                else
                {
                    EWtAT14k = Convert.ToDecimal(dt.Rows[0]["14kDefaultWeight"]);
                }
            }
            else { EWtAT14k = Convert.ToDecimal(dt.Rows[0]["14kDefaultWeight"]); }

            if ((MT.Trim() == "Platinum") && (MQ.Trim() == "950"))
            {
                PEW = Math.Round((EWtAT14k * Convert.ToDecimal("1.642")), 2, MidpointRounding.AwayFromZero);
                Metals = "Platinum";
            }
            if ((MT.Trim() == "Two Tone" || MT.Trim() == "Yellow Gold" || MT.Trim() == "White Gold") && MQ.Trim() == "14k")
            {
                PEW = Math.Round(EWtAT14k, 2, MidpointRounding.AwayFromZero);
                Metals = "Gold";
            }
            if ((MT.Trim() == "Two Tone" || MT.Trim() == "Yellow Gold" || MT.Trim() == "White Gold") && MQ.Trim() == "18k")
            {
                PEW = Math.Round((EWtAT14k * Convert.ToDecimal("1.192")), 2, MidpointRounding.AwayFromZero);
                Metals = "Gold";
            }
            if ((MT.Trim() == "Two Tone" || MT.Trim() == "Yellow Gold" || MT.Trim() == "White Gold") && MQ.Trim() == "10k")
            {
                PEW = (Math.Round((EWtAT14k * Convert.ToDecimal("0.885")), 2, MidpointRounding.AwayFromZero));

            }

            PEW = Math.Round(PEW, 2, System.MidpointRounding.AwayFromZero);


        }
        catch (Exception ee)
        {
            string er = ee.ToString();
            err = true;
        }
        return PEW;
    }


    [WebMethod(Description = "Effective Carat Weight && No of Stones")]
    public void GetEffectiveCaratWeightNoofStones(string Pid, string carWt1, string MetalType1, string MetalQlty1, string strColor1, string strclarity1, string stoneType1, string length)
    {


        if (strColor1 == "HI" && strclarity1 == "SI-3")
        {
            strclarity1 = "SI-2";
        }


        DataTable dt = new DataTable();
        dt.Columns.Add("EffectiveCaratWeight");
        dt.Columns.Add("NoofStones");
        dt.Columns.Add("StoneCaratWeight");
        dt.Columns.Add("StonesSize");
        dt.TableName = "Test";
        DataRow dr = dt.NewRow();

        carWt = carWt1;
        string MetalType = MetalType1;
        string MetalQlty = MetalQlty1;
        string strColor = strColor1;
        string strclarity = strclarity1;
        string Radioval = "Diamond";
        string Isenable = string.Empty;
        string ISSize = string.Empty;
        OrderLen = Convert.ToDecimal(length);
        string stoneType = stoneType1;

        StoneNo = 0;
        StoneNoForNew = 0;       
        decimal MetalWeight = 0;      
        string query = "SELECT ISEnable,ISSize FROM ProductsGroups WHERE ProductsGroupID IN(SELECT ProductsProductsGroupID FROM ProductsProductsGroups WHERE ProductID='" + Pid + "')";
        DataSet ds = new DataSet();
        ds = objComFun.ExecuteQueryReturnDataSet(query);
        if (ds.Tables[0].Rows.Count != 0)
        {
            Isenable = ds.Tables[0].Rows[0]["ISEnable"].ToString();
            ISSize = ds.Tables[0].Rows[0]["ISSize"].ToString();
        }
        if ((Isenable == "True") && (ISSize == "False"))
        {
            Isenable = "T";
        }
        else if ((Isenable == "True") && (ISSize == "True"))
        {
            Isenable = "T";
        }
        else
        {
            Isenable = "F";
        }


        DtfixedCharge = objComFun.ExecuteQueryReturnDataTableFixedCharges(Pid);
        ProdCtSize = ProductSize(carWt, Pid);
        MetalWeight = Math.Round(getMetalWeight(MetalType, MetalQlty, Isenable, Pid, carWt1, length), 2, MidpointRounding.AwayFromZero);
        string[] myarray = new string[4];

        if (GetPriceCalculationOption(Radioval, stoneType, strColor, strclarity, carWt, Pid))
        {
            myarray = Stone_PriceForSpecialPremiums(Radioval, stoneType, strColor, strclarity, carWt, Pid, length);
        }
        else
        {
            myarray = Stone_Price(Radioval, stoneType, strColor, strclarity, carWt, Pid, length);
        }
        dr["EffectiveCaratWeight"] = myarray[0].ToString();
        dr["NoofStones"] = myarray[1].ToString();
        dr["StoneCaratWeight"] = myarray[2].ToString();
        dr["StonesSize"] = myarray[3].ToString();

     
        dt.Rows.Add(dr);

        if (dt.Rows.Count > 0)
        {

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Write(DataTableToJSONWithJavaScriptSerializer(dt));
        }
        else
        {
            Context.Response.Write("False");
        }
        

    }

    public string ProductSize(string carwt, string pid)
    {
        string CTSize = string.Empty;

        DataSet dtsize;
        try
        {
            dtsize = ojbProdsizeHelper.ReturnProductSizes(pid.ToString(), carwt.Trim());
            if (dtsize.Tables[0].Rows.Count != 0)
            {

                CTSize = dtsize.Tables[0].Rows[0]["Sizes"].ToString();

            }
        }
        catch (Exception ee)
        {
            string er = ee.ToString();
            err = true;
        }

        return CTSize;
    }

    [WebMethod(Description = "Product Stone Carat Weight and Stone Size")]
    public void GetStoneCaratWeightDetails(String ProductID, String CaratID)
    {
        String[] result = new String[8];
        Dictionary<string, string> dict = new Dictionary<string, string>();

        DataTable mydt = new DataTable();
        mydt.TableName = "CaratDetails";
        mydt.Columns.Add("Height");
        mydt.Columns.Add("Width");
        mydt.Columns.Add("CaratWeight");
        mydt.Columns.Add("StoneSize");
        mydt.Columns.Add("StoneCaratWeightCenterStone");
        mydt.Columns.Add("Shape");
        mydt.Columns.Add("StoneSettingName");
        mydt.Columns.Add("StoneQty");
        mydt.Columns.Add("CWTCAL");
        DataRow mydr = mydt.NewRow();




        try
        {

            String StrQry = "SELECT Height, Width,defaultLength FROM ProductSizes WHERE ProductID= '" + ProductID.Trim() + "' and  ProductSizeID='" + CaratID + "' ";
            DataTable dt = objComFun.ExecuteQueryReturnDataTable(StrQry);
            //decimal height = Convert.ToDecimal(dt.Rows[0]["Height"].ToString()) * Convert.ToDecimal("0.03937");
            //height = Decimal.Round(height, 2);

            //mydr["Height"] = height.ToString() + " Inches";
            //decimal Width = Convert.ToDecimal(dt.Rows[0]["Width"].ToString()) * Convert.ToDecimal("0.03937");
            //Width = Decimal.Round(Width, 2);

            //mydr["Width"] = Width.ToString() + " Inches";

            decimal height = Convert.ToDecimal(dt.Rows[0]["Height"].ToString());
            height = Decimal.Round(height, 2);

            mydr["Height"] = height.ToString() + " mm";
            decimal Width = Convert.ToDecimal(dt.Rows[0]["Width"].ToString());
            Width = Decimal.Round(Width, 2);

            mydr["Width"] = Width.ToString() + " mm";
            string Query = "";
            decimal caratweight = Convert.ToDecimal("0.00");

            string QueryCheckCenterDiamond = "SELECT CENTERDIAMOND FROM PRODUCTS WHERE ProductID='" + ProductID.Trim() + "'";
            bool checkCenterDiamond = Convert.ToBoolean(objComFun.ExecuteQueryReturnSingleString(QueryCheckCenterDiamond));
            if (checkCenterDiamond == true)
            {
                Query = "SELECT dbo.fn_caratweight_WebService('" + CaratID + "','" + ProductID.Trim() + "','1') as Sizes FROM ProductSizes WHERE ProductID='" + ProductID.Trim() + "' AND ProductSizeID='" + CaratID.Trim() + "' ";
                caratweight = Convert.ToDecimal(objComFun.ExecuteQueryReturnSingleString(Query));


                mydr["CaratWeight"] = caratweight.ToString();
            }
            else
            {
                mydr["CaratWeight"] = "Notfound";
            }




            string size = string.Empty;
            string SettingType = string.Empty;
            string shape = string.Empty;
            string StoneQty = string.Empty;
            string _strStoneCaratWeight = string.Empty;
            string _strStoneCaratWeightNew = string.Empty;
            string _strStoneCaratWeightCenterStone = string.Empty;
            string Caratval = string.Empty;
            string CWTRHS = string.Empty;
            string CWTLHS = string.Empty;
            string StrStoneType = "select stoneConfigurationID,StoneType from ProductsStoneConfiguration WHERE (ProductsStoneConfiguration.ProductID= '" + ProductID.Trim() + "')AND ProductsStoneConfiguration.ProductSizeID='" + CaratID.Trim() + "' ORDER BY CaratWeight; ";
            DataTable DtstoneConfig = objComFun.ExecuteQueryReturnDataTable(StrStoneType);
            DataTable dtColorStone;
            DataTable dtDiamond;
            int k;
            if (DtstoneConfig.Rows.Count != 0)
            {
                for (int i = 0; i < DtstoneConfig.Rows.Count; i++)
                {

                    if (DtstoneConfig.Rows[i]["StoneType"].ToString() == "1")
                    {


                        string strDiamond = "SELECT (convert(nvarchar(50),ProductsStoneConfiguration.StoneQTy,101)+'X'+ProductsStoneConfiguration.StoneSize+'mm') as StoneSize,";
                        strDiamond += " ProductsStoneConfiguration.StoneQTy, Shapes.Shape, StoneSettings.StoneSettingName,";
                        strDiamond += " ProductsStoneConfiguration.StoneSettingID,(convert(varchar(50),ProductsStoneConfiguration.StoneQTy,101)+'X'+convert(varchar(50),convert(decimal(18,3),ProductsStoneConfiguration.CaratWeight),101)+' ct.')as CaratWeight,(convert(varchar(50),ProductsStoneConfiguration.StoneQTy,101)+'X'+convert(varchar(50),convert(decimal(18,3),ProductsStoneConfiguration.CaratWeight),101))as CaratWeightNew,ProductsStoneConfiguration.CenterStone";
                        strDiamond += " FROM ProductsStoneConfiguration INNER JOIN Shapes ON ProductsStoneConfiguration.StoneShapeID = Shapes.ShapeID INNER JOIN StoneSettings";
                        strDiamond += " ON ProductsStoneConfiguration.StoneSettingID = StoneSettings.StoneSettingID WHERE (ProductsStoneConfiguration.ProductID= '" + ProductID.Trim() + "')AND ProductsStoneConfiguration.ProductSizeID='" + CaratID.Trim() + "' and ProductsStoneConfiguration.stoneConfigurationID='" + DtstoneConfig.Rows[i]["stoneConfigurationID"] + "' ";


                        dtDiamond = objComFun.ExecuteQueryReturnDataTable(strDiamond);

                        if (dtDiamond.Rows.Count != 0)
                        {
                            for (int j = 0; j < dtDiamond.Rows.Count; j++)
                            {
                                if (size.Trim() == string.Empty)
                                {

                                    size = Convert.ToString(dtDiamond.Rows[j]["StoneSize"].ToString());
                                }
                                else
                                {
                                    size = size + ", " + Convert.ToString(dtDiamond.Rows[j]["StoneSize"].ToString());
                                }

                                if (_strStoneCaratWeight.Trim() == string.Empty)
                                {
                                    _strStoneCaratWeight = Convert.ToString(dtDiamond.Rows[j]["CaratWeight"].ToString());
                                    _strStoneCaratWeightNew = Convert.ToString(dtDiamond.Rows[j]["CaratWeightNew"].ToString());

                                    // For Center Stone//
                                    if (dtDiamond.Rows[j]["CenterStone"].ToString().Trim() == "1")
                                    {
                                        _strStoneCaratWeightCenterStone = _strStoneCaratWeightNew + " (center stone)";

                                    }
                                    else
                                    {
                                        _strStoneCaratWeightCenterStone = _strStoneCaratWeightNew;
                                    }


                                }
                                else
                                {
                                    string strChkFr1 = dtDiamond.Rows[j]["CaratWeight"].ToString();

                                    _strStoneCaratWeight = _strStoneCaratWeight + ", " + Convert.ToString(dtDiamond.Rows[j]["CaratWeight"].ToString());
                                    _strStoneCaratWeightNew = _strStoneCaratWeightNew + ", " + Convert.ToString(dtDiamond.Rows[j]["CaratWeightNew"].ToString());
                                    // For Center Stone//
                                    if (dtDiamond.Rows[j]["CenterStone"].ToString().Trim() == "1")
                                    {
                                        _strStoneCaratWeightCenterStone = _strStoneCaratWeightCenterStone + ", " + Convert.ToString(dtDiamond.Rows[j]["CaratWeightNew"].ToString()) + "(center stone)";

                                    }
                                    else
                                    {
                                        _strStoneCaratWeightCenterStone = _strStoneCaratWeightCenterStone + ", " + Convert.ToString(dtDiamond.Rows[j]["CaratWeightNew"].ToString());
                                    }
                                    // For Center Stone//
                                }

                                if (shape.Trim() == string.Empty)
                                {
                                    shape = dtDiamond.Rows[j]["Shape"].ToString();
                                }
                                else
                                {
                                    if (shape.ToLower().IndexOf(dtDiamond.Rows[j]["Shape"].ToString().ToLower()) == -1)
                                    {
                                        shape = shape + ", " + dtDiamond.Rows[j]["Shape"].ToString();
                                    }

                                }

                                if (SettingType.Trim() == string.Empty)
                                {
                                    SettingType = dtDiamond.Rows[j]["StoneSettingName"].ToString();
                                }
                                else
                                {
                                    if (SettingType.ToLower().IndexOf(dtDiamond.Rows[j]["StoneSettingName"].ToString().ToLower()) == -1)
                                    {
                                        SettingType = SettingType + ", " + dtDiamond.Rows[j]["StoneSettingName"].ToString();
                                    }
                                }

                                if (StoneQty.Trim() == string.Empty)
                                {

                                    StoneQty = Convert.ToString(dtDiamond.Rows[j]["StoneQTy"].ToString());
                                }
                                else
                                {
                                    decimal x = Convert.ToDecimal(StoneQty) + Convert.ToDecimal(dtDiamond.Rows[j]["StoneQTy"].ToString());
                                    StoneQty = Convert.ToString(Decimal.Round(x, 1));

                                }

                            }
                        }
                    }
                    else
                    {
                        //colorstone
                        string strColorStone = "SELECT (convert(nvarchar(50),ProductsStoneConfiguration.StoneQTy,101)+'X'+ProductsStoneConfiguration.StoneSize+'mm') as StoneSize,(convert(varchar(50),ProductsStoneConfiguration.StoneQTy,101)+'X'+convert(varchar(50),convert(decimal(18,3),ProductsStoneConfiguration.CaratWeight),101)+' ct.')as CaratWeight,(convert(varchar(50),ProductsStoneConfiguration.StoneQTy,101)+'X'+convert(varchar(50),convert(decimal(18,3),ProductsStoneConfiguration.CaratWeight),101))as CaratWeightNew,ProductsStoneConfiguration.StoneQTy, StoneSettings.StoneSettingName,ProductsStoneConfiguration.StoneSettingID, ProductsStoneConfiguration.StoneShapeID, tblColorStone_Shape.ColorStoneShape,ProductsStoneConfiguration.CenterStone";
                        strColorStone += " FROM ProductsStoneConfiguration INNER JOIN StoneSettings ON ProductsStoneConfiguration.StoneSettingID = StoneSettings.StoneSettingID INNER JOIN tblColorStone_Shape ON ProductsStoneConfiguration.StoneShapeID = tblColorStone_Shape.ColorStoneShapeID";
                        strColorStone += " WHERE (ProductsStoneConfiguration.ProductID= '" + ProductID.Trim() + "')AND ProductsStoneConfiguration.ProductSizeID='" + CaratID.Trim() + "' and ProductsStoneConfiguration.stoneConfigurationID='" + DtstoneConfig.Rows[i]["stoneConfigurationID"] + "' ";
                        dtColorStone = objComFun.ExecuteQueryReturnDataTable(strColorStone);
                        if (dtColorStone.Rows.Count != 0)
                        {
                            for (int j = 0; j < dtColorStone.Rows.Count; j++)
                            {
                                if (size.Trim() == string.Empty)
                                {

                                    size = Convert.ToString(dtColorStone.Rows[j]["StoneSize"].ToString());
                                }
                                else
                                {
                                    size = size + ", " + Convert.ToString(dtColorStone.Rows[j]["StoneSize"].ToString());
                                }

                                if (_strStoneCaratWeight.Trim() == string.Empty)
                                {

                                    _strStoneCaratWeight = Convert.ToString(dtColorStone.Rows[j]["CaratWeight"].ToString());
                                    _strStoneCaratWeightNew = Convert.ToString(dtColorStone.Rows[j]["CaratWeightNew"].ToString());
                                    // For Center Stone//
                                    if (dtColorStone.Rows[j]["CenterStone"].ToString().Trim() == "1")
                                    {
                                        _strStoneCaratWeightCenterStone = _strStoneCaratWeightNew + " (center stone)";
                                    }
                                    else
                                    {
                                        _strStoneCaratWeightCenterStone = _strStoneCaratWeightNew;
                                    }
                                    // For Center Stone//
                                }
                                else
                                {
                                    _strStoneCaratWeight = _strStoneCaratWeight + ", " + Convert.ToString(dtColorStone.Rows[j]["CaratWeight"].ToString());
                                    _strStoneCaratWeightNew = _strStoneCaratWeightNew + ", " + Convert.ToString(dtColorStone.Rows[j]["CaratWeightNew"].ToString());
                                    // For Center Stone//
                                    if (dtColorStone.Rows[j]["CenterStone"].ToString().Trim() == "1")
                                    {
                                        _strStoneCaratWeightCenterStone = _strStoneCaratWeightCenterStone + ", " + Convert.ToString(dtColorStone.Rows[j]["CaratWeightNew"].ToString()) + "(center stone)";
                                    }
                                    else
                                    {
                                        _strStoneCaratWeightCenterStone = _strStoneCaratWeightCenterStone + ", " + Convert.ToString(dtColorStone.Rows[j]["CaratWeightNew"].ToString());
                                    }
                                    // For Center Stone//
                                }



                                if (shape.Trim() == string.Empty)
                                {
                                    shape = dtColorStone.Rows[j]["ColorStoneShape"].ToString();
                                }
                                else
                                {
                                    if (shape.ToLower().IndexOf(dtColorStone.Rows[j]["ColorStoneShape"].ToString().ToLower()) == -1)
                                    {
                                        shape = shape + ", " + dtColorStone.Rows[j]["ColorStoneShape"].ToString();
                                    }
                                }
                                if (SettingType.Trim() == string.Empty)
                                {
                                    SettingType = dtColorStone.Rows[j]["StoneSettingName"].ToString();
                                }
                                else
                                {
                                    if (SettingType.ToLower().IndexOf(dtColorStone.Rows[j]["StoneSettingName"].ToString().ToLower()) == -1)
                                    {
                                        SettingType = SettingType + ", " + dtColorStone.Rows[j]["StoneSettingName"].ToString();
                                    }
                                }
                                if (StoneQty.Trim() == string.Empty)
                                {

                                    StoneQty = Convert.ToString(dtColorStone.Rows[j]["StoneQTy"].ToString());
                                }
                                else
                                {
                                    decimal x = Convert.ToDecimal(StoneQty) + Convert.ToDecimal(dtColorStone.Rows[j]["StoneQTy"].ToString());
                                    StoneQty = Convert.ToString(Decimal.Round(x, 1));
                                }


                            }
                        }
                    }
                }
            }

            if (_strStoneCaratWeight.Trim() != "")
            {

                CWTRHS = "Carat Weight Calculation: " + _strStoneCaratWeightCenterStone + " = ";


                //************************************************
                if (_strStoneCaratWeightNew.Contains(","))
                {
                    string[] Caratvalsplit1 = _strStoneCaratWeightNew.Split(',');
                    Decimal Caratvaldec = 0;
                    for (int a = 0; a < Caratvalsplit1.Length; a++)
                    {
                        string[] Caratvalsplit2 = Caratvalsplit1[a].Split('X');
                        Caratvaldec = Caratvaldec + Decimal.Round(((Convert.ToDecimal(Caratvalsplit2[0].ToString())) * (Convert.ToDecimal(Caratvalsplit2[1].ToString()))), 2);
                        Caratval = Convert.ToString(Caratvaldec);
                        CWTLHS = Caratval + " total carat weight.";
                    }
                }
                else
                {
                    string[] Caratvalsplit2 = _strStoneCaratWeightNew.Split('X');
                    Caratval = Convert.ToString(Decimal.Round(((Convert.ToDecimal(Caratvalsplit2[0].ToString())) * (Convert.ToDecimal(Caratvalsplit2[1].ToString()))), 2));
                    CWTLHS = Caratval + " total carat weight.";
                }



            }
            mydr["StoneSize"] = size;
            mydr["StoneCaratWeightCenterStone"] = _strStoneCaratWeightCenterStone;
            mydr["Shape"] = shape;
            mydr["StoneSettingName"] = SettingType;
            mydr["StoneQty"] = StoneQty;
            mydr["CWTCAL"] = CWTRHS + CWTLHS;
            mydt.Rows.Add(mydr);
            if (mydt.Rows.Count > 0)
            {

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.Write(DataTableToJSONWithJavaScriptSerializer(mydt));

            }
            else
            {
                Context.Response.Write("False");

            }


        }
        catch (Exception ex)
        {
            Context.Response.Write("False");

        }


    }
    
    [WebMethod]
    public void GetProductStoneDetails(string ProductId, string CaratId)
    {
        DataTable dtStoneConfig = new DataTable();
        dtStoneConfig.TableName = "StoneConfiguration";
        dtStoneConfig.Columns.Add("StoneSize");
        dtStoneConfig.Columns.Add("StoneQTy");
        dtStoneConfig.Columns.Add("CaratWeight");
        dtStoneConfig.Columns.Add("Shape");
        dtStoneConfig.Columns.Add("StoneSettingName");
        dtStoneConfig.Columns.Add("CenterStone");


        String str1 = "select StoneType,StoneConfigurationID from ProductsStoneConfiguration where ProductID='" + ProductId + "' and ProductSizeID='" + CaratId + "'order by StoneSize desc";

        DataTable dt = objComFun.ExecuteQueryReturnDataTable(str1);
        dt.TableName = "MyTable";
        if (dt.Rows.Count != 0)
        {
            int i = 0;
            while (i < dt.Rows.Count)
            {
                String stonequery = "";
                String strtype = dt.Rows[i]["StoneType"].ToString();
                if (strtype == "1")
                {
                    stonequery = "select ProductsStoneConfiguration.StoneSize,ProductsStoneConfiguration.StoneQTy,RIGHT(convert(varchar(50),convert(decimal(18,3),ProductsStoneConfiguration.CaratWeight),101),CHARINDEX('.',convert(varchar(50), convert(decimal(18,3),ProductsStoneConfiguration.CaratWeight)))+CASE WHEN CONVERT(INT,LEFT(ProductsStoneConfiguration.CaratWeight,CHARINDEX('.',ProductsStoneConfiguration.CaratWeight)-1))>0 THEN 3 ELSE 2 END)as CaratWeight,Shapes.Shape, StoneSettings.StoneSettingName,";
                    stonequery += "ProductsStoneConfiguration.CenterStone FROM ProductsStoneConfiguration INNER JOIN Shapes ON ProductsStoneConfiguration.StoneShapeID = Shapes.ShapeID ";
                    stonequery += "INNER JOIN StoneSettings ON ProductsStoneConfiguration.StoneSettingID = StoneSettings.StoneSettingID WHERE (ProductsStoneConfiguration.ProductID= '" + ProductId + "')AND ProductsStoneConfiguration.ProductSizeID='" + CaratId + "'";
                    stonequery += "AND ProductsStoneConfiguration.stoneConfigurationID='" + dt.Rows[i]["StoneConfigurationID"].ToString() + "'";

                }
                else if (strtype == "2")
                {
                    stonequery = "SELECT ProductsStoneConfiguration.StoneSize,ProductsStoneConfiguration.StoneQTy,CASE WHEN CONVERT(INT,LEFT(ProductsStoneConfiguration.CaratWeight,CHARINDEX('.',ProductsStoneConfiguration.CaratWeight)-1))>0 THEN CONVERT(VARCHAR(50),ProductsStoneConfiguration.CaratWeight) ELSE CONVERT(VARCHAR(50),RIGHT(ProductsStoneConfiguration.CaratWeight,LEN(ProductsStoneConfiguration.CaratWeight)-(CHARINDEX('.',ProductsStoneConfiguration.CaratWeight)-1)))END CaratWeight,";
                    stonequery += "tblColorStone_Shape.ColorStoneShape as Shape,StoneSettings.StoneSettingName,ProductsStoneConfiguration.CenterStone ";
                    stonequery += "FROM ProductsStoneConfiguration INNER JOIN StoneSettings ON ProductsStoneConfiguration.StoneSettingID = StoneSettings.StoneSettingID INNER JOIN tblColorStone_Shape ON ProductsStoneConfiguration.StoneShapeID = tblColorStone_Shape.ColorStoneShapeID ";
                    stonequery += "WHERE (ProductsStoneConfiguration.ProductID= '" + ProductId + "')AND ProductsStoneConfiguration.ProductSizeID='" + CaratId + "' ";
                    stonequery += "and ProductsStoneConfiguration.stoneConfigurationID='" + dt.Rows[i]["StoneConfigurationID"] + "' ";

                }
                else
                {
                    stonequery = "select ProductsStoneConfiguration.StoneSize,ProductsStoneConfiguration.StoneQTy,RIGHT(convert(varchar(50),convert(decimal(18,3),ProductsStoneConfiguration.CaratWeight),101),CHARINDEX('.',convert(varchar(50),convert(decimal(18,3),ProductsStoneConfiguration.CaratWeight)))+CASE WHEN CONVERT(INT,LEFT(ProductsStoneConfiguration.CaratWeight,CHARINDEX('.',ProductsStoneConfiguration.CaratWeight)-1))>0 THEN 3 ELSE 2 END)as CaratWeight,";
                    stonequery += "ProductsStoneConfiguration.StoneShapeID as Shape, ProductsStoneConfiguration.StoneSettingID as StoneSettingName,";
                    stonequery += "ProductsStoneConfiguration.CenterStone from ProductsStoneConfiguration where ProductsStoneConfiguration.ProductID='" + ProductId + "'";
                    stonequery += "and ProductsStoneConfiguration.ProductSizeID='" + CaratId + "' and ProductsStoneConfiguration.StoneConfigurationID='" + dt.Rows[i]["StoneConfigurationID"] + "'";

                }
                DataTable Stonedt = objComFun.ExecuteQueryReturnDataTable(stonequery);
                DataRow dr = dtStoneConfig.NewRow();
                dr["StoneSize"] = Stonedt.Rows[0]["StoneSize"].ToString();
                dr["StoneQTy"] = Stonedt.Rows[0]["StoneQTy"].ToString();
                dr["CaratWeight"] = Stonedt.Rows[0]["CaratWeight"].ToString();
                dr["Shape"] = Stonedt.Rows[0]["Shape"].ToString();
                dr["StoneSettingName"] = Stonedt.Rows[0]["StoneSettingName"].ToString();
                dr["CenterStone"] = Stonedt.Rows[0]["CenterStone"].ToString();
                dtStoneConfig.Rows.Add(dr);
                i = i + 1;
            }
        }

        if (dtStoneConfig.Rows.Count > 0)
        {

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Write(DataTableToJSONWithJavaScriptSerializer(dtStoneConfig));

        }
        else
        {
            Context.Response.Write("False");

        }

    }
    [WebMethod(Description = "Get the RingSize data ")]
    public void GetEternityRingSize(string ProductId, string ProductSizeId, string RingSize, string Status)
    {

        int Condition = Convert.ToInt32(Status);
        DataSet dsEnternityRingSize = new DataSet();

        if (Condition == 1)
        {
            dsEnternityRingSize = objComFun.GetEternityRingSize(Condition, ProductId, ProductSizeId);

        }
        else if (Condition == 2)
        {
            dsEnternityRingSize = objComFun.GetRingSizes(Condition, ProductId, ProductSizeId, RingSize);

        }


        if (dsEnternityRingSize.Tables[0].Rows.Count > 0)
        {
            DataTable dt = dsEnternityRingSize.Tables[0];
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Write(DataTableToJSONWithJavaScriptSerializer(dt));
        }
        else
        {
            Context.Response.Write("False");
        }


    }

    #endregion //--------------- Product page ----------------------//

    #region  //--------------- Get Product Price  ----------------------//

    [WebMethod(Description = "Get Product Price Calculation")]
    public DataTable GetProduct_Price(string Pid, string carWt1, string MetalType1, string MetalQlty1, string strColor1, string strclarity1, string stoneType1, string length, string SemiMount, string CenterDiamond, string LabCertDiamondPrice, string SideStoneColor, string SideStoneClarity, string Rapnetstuds)
    {
        if (strColor1 == "HI" && strclarity1 == "SI-3")
        {

            strclarity1 = "SI-2";
        }

        if (SideStoneColor == "HI" && SideStoneClarity == "SI-3")
        {

            SideStoneClarity = "SI-2";
        }

        string RapnetstudsStatus = Rapnetstuds;

        decimal CenterDiamondPrice = 0;
        decimal anjoleediamondCenterDiamondPrice = 0;
        if (CenterDiamond.Trim() == "LabCertDiamond")
        {
            anjoleediamondCenterDiamondPrice = Decimal.Round(Convert.ToDecimal(LabCertDiamondPrice), 2, MidpointRounding.AwayFromZero);
        }
        DataTable dt = new DataTable();
        dt.Columns.Add("CalCulatedPrice");
        dt.Columns.Add("CenterDiamondPrice");
        dt.Columns.Add("SideDiamondPrice");
        dt.TableName = "dtgetprice";
        DataRow _datarows = dt.NewRow();

        carWt = carWt1;
        string MetalType = MetalType1;
        string MetalQlty = MetalQlty1;
        string strColor = strColor1;
        string strclarity = strclarity1;
        string Radioval = "Diamond";
        string Isenable = string.Empty;
        string ISSize = string.Empty;
        OrderLen = Convert.ToDecimal(length);
        string stoneType = stoneType1;

        StoneNo = 0;
        StoneNoForNew = 0;
        decimal stonePrice = 0;
        decimal MetalPrice = 0;
        decimal MetalWeight = 0;
        decimal StoneSettingPrice = 0;
        string CalCulatedPrice = "0";
        decimal SettingPrice = 0;

        StoneNo = 0;
        StoneNoForNew = 0;
        decimal CenterDiamondStoneSettingPrice = 0;
        decimal CenterDiamondstonePrice = 0;
        decimal CenterDiamonds_Price = 0;
        decimal SideStoneSettingPrice = 0;
        decimal SideDiamondstonePrice = 0;
        decimal SideStoneDiamonds_Price = 0;

        int EternityStatus = objComFun.CheckEternityStatus(Pid);

        string query = "SELECT ISEnable,ISSize FROM ProductsGroups WHERE ProductsGroupID IN(SELECT ProductsProductsGroupID FROM ProductsProductsGroups WHERE ProductID='" + Pid + "')";
        DataSet ds = new DataSet();
        ds = objComFun.ExecuteQueryReturnDataSet(query);
        if (ds.Tables[0].Rows.Count != 0)
        {
            Isenable = ds.Tables[0].Rows[0]["ISEnable"].ToString();
            ISSize = ds.Tables[0].Rows[0]["ISSize"].ToString();
        }
        if (EternityStatus == 1)
        {
            Isenable = "ET";
        }

        else if ((Isenable == "True") && (ISSize == "False"))
        {
            Isenable = "T";
        }
        else if ((Isenable == "True") && (ISSize == "True"))
        {
            Isenable = "T";
        }
        else
        {
            Isenable = "F";
        }

        decimal EnternityRingSize = OrderLen;

        DtfixedCharge = objComFun.ExecuteQueryReturnDataTableFixedCharges(Pid);
        ProdCtSize = ProductSize(carWt, Pid, SemiMount);

        MetalWeight = Math.Round(getMetalWeight(MetalType, MetalQlty, Isenable, Pid, carWt1, length), 2, MidpointRounding.AwayFromZero);

        #region  //--- RapNet CenterDiamond Calculate Price----//

        CenterDiamondStoneSettingPrice = Math.Round(CenterDiamondStoneSetting_Price(Pid, Radioval, carWt, SemiMount), 2, MidpointRounding.AwayFromZero);
        CenterDiamondstonePrice = Decimal.Round(CenterDiamondStone_PriceForSpecialPremium(Pid, Radioval, stoneType, strColor, strclarity, carWt, SemiMount), 2, MidpointRounding.AwayFromZero);

        if (RapnetstudsStatus != "1")
        {
            if (Convert.ToDecimal(DtfixedCharge.Rows[0]["Premium_charge"]) > 0)
            {
                CenterDiamondStoneSettingPrice = CenterDiamondStoneSettingPrice * Convert.ToDecimal(DtfixedCharge.Rows[0]["Premium_charge"]);
            }
            CenterDiamonds_Price = CenterDiamondStoneSettingPrice + CenterDiamondstonePrice;
            CenterDiamonds_Price = Decimal.Round(Convert.ToDecimal(CenterDiamonds_Price), 2, MidpointRounding.AwayFromZero);

        }
        if (CenterDiamond.Trim() == "LabCertDiamond" && LabCertDiamondPrice != "")
        {
            if (RapnetstudsStatus != "2")
            {
                decimal LabCertDiamondPrice_Premiumcharge = CenterDiamondStoneSettingPrice + Convert.ToDecimal(LabCertDiamondPrice);

                LabCertDiamondPrice = LabCertDiamondPrice_Premiumcharge.ToString();
            }



        }
        SideStoneSettingPrice = Math.Round(SideStoneSetting_Price(Pid, Radioval, carWt, SemiMount), 2, MidpointRounding.AwayFromZero);
        SideDiamondstonePrice = Decimal.Round(SideStone_PriceForSpecialPremium(Pid, Radioval, stoneType, SideStoneColor, SideStoneClarity, carWt, SemiMount), 2, MidpointRounding.AwayFromZero);
        SideStoneDiamonds_Price = SideStoneSettingPrice + SideDiamondstonePrice;
        if (Convert.ToDecimal(DtfixedCharge.Rows[0]["Premium_charge"]) > 0)
        {
            SideStoneDiamonds_Price = SideStoneDiamonds_Price * Convert.ToDecimal(DtfixedCharge.Rows[0]["Premium_charge"]);
        }
        SideStoneDiamonds_Price = Decimal.Round(Convert.ToDecimal(SideStoneDiamonds_Price), 2, MidpointRounding.AwayFromZero);


        #endregion //---- RapNet Task -----//


        if (GetPriceCalculationOption(Radioval, stoneType, strColor, strclarity, carWt, Pid, SemiMount))
        {
            MetalPrice = Math.Round(EffectiveWeight_MetalPrice(MetalType, MetalQlty, Isenable, Pid, carWt1, length), 2, MidpointRounding.AwayFromZero);
            StoneSettingPrice = Math.Round(StoneSetting_Price(Radioval, carWt, Pid, length, SemiMount, Isenable, EnternityRingSize), 2, MidpointRounding.AwayFromZero);
            stonePrice = Decimal.Round(Stone_PriceForSpecialPremium(Radioval, stoneType, strColor, strclarity, carWt, Pid, length, SemiMount, Isenable, EnternityRingSize), 2, MidpointRounding.AwayFromZero);


            decimal decFinishinPrice = 0;
            if (Convert.ToDecimal(DtfixedCharge.Rows[0]["Premium_charge"]) > 0)
            {
                MetalPrice = MetalPrice * Convert.ToDecimal(DtfixedCharge.Rows[0]["Premium_charge"]);
                StoneSettingPrice = StoneSettingPrice * Convert.ToDecimal(DtfixedCharge.Rows[0]["Premium_charge"]);
                decFinishinPrice = Convert.ToDecimal(DtfixedCharge.Rows[0]["Premium_charge"]) * Convert.ToDecimal(DtfixedCharge.Rows[0]["Finishing_Price"]);
            }

            if (Radioval.Trim() == "")
            {
                CalCulatedPrice = Convert.ToString(MetalPrice + StoneSettingPrice + stonePrice);
            }
            else
            {
                if (Radioval.Trim() == "Diamond")
                {
                    if (CenterDiamond.Trim() == "AnjoleeDiamond")
                    {

                        if (LabCertDiamondPrice != "1")
                        {
                            if (RapnetstudsStatus == "1")
                            {
                                CalCulatedPrice = Convert.ToString(MetalPrice + StoneSettingPrice + stonePrice);
                            }
                            else
                            {
                                CalCulatedPrice = Convert.ToString(MetalPrice + CenterDiamonds_Price + SideStoneDiamonds_Price);
                            }
                        }
                        else
                        {
                            CalCulatedPrice = "2";

                        }

                    }
                    else if (CenterDiamond.Trim() == "LabCertDiamond")
                    {
                        if (LabCertDiamondPrice != "")
                        {
                            if (RapnetstudsStatus == "1")
                            {
                                CalCulatedPrice = Convert.ToString(MetalPrice + StoneSettingPrice + Convert.ToDecimal(LabCertDiamondPrice));
                            }
                            else if (RapnetstudsStatus == "2")
                            {
                                CalCulatedPrice = Convert.ToString(MetalPrice + Convert.ToDecimal(LabCertDiamondPrice) + SideStoneDiamonds_Price);
                            }
                            else
                            {
                                CalCulatedPrice = Convert.ToString(MetalPrice + Convert.ToDecimal(LabCertDiamondPrice) + SideStoneDiamonds_Price);
                            }

                        }
                        else
                        {
                            CalCulatedPrice = "2";

                        }

                    }
                    else
                    {


                        CalCulatedPrice = Convert.ToString(MetalPrice + StoneSettingPrice + stonePrice);

                    }
                }
                if (Radioval.Trim() == "NoDiamond")
                {
                    if (stoneType.Trim() == "")
                    {
                        StoneSettingPrice = 0;
                        stonePrice = 0;
                    }
                    CalCulatedPrice = Convert.ToString(MetalPrice + StoneSettingPrice + stonePrice);
                }
                if (Radioval.Trim() == "CubicZirconia")
                {
                    if (stoneType.Trim() == "")
                    {
                        stonePrice = 0;
                    }
                    CalCulatedPrice = Convert.ToString(MetalPrice + StoneSettingPrice + stonePrice);
                }
            }


            if (MetalType.Trim() == "White Gold" || MetalType.Trim() == "Yellow Gold" || MetalType.Trim() == "Two Tone")
            {
                if (CenterDiamond.Trim() == "AnjoleeDiamond")
                {
                    if (RapnetstudsStatus == "1")
                    {
                        CalCulatedPrice = Convert.ToString(MetalPrice + StoneSettingPrice + stonePrice + decFinishinPrice);  //Finishing_Price
                    }
                    else
                    {

                        CalCulatedPrice = Convert.ToString(MetalPrice + CenterDiamonds_Price + SideStoneDiamonds_Price + decFinishinPrice);  //Finishing_Price
                    }

                }
                else if (CenterDiamond.Trim() == "LabCertDiamond")
                {
                    if (LabCertDiamondPrice != "")
                    {
                        if (RapnetstudsStatus == "1")
                        {
                            CalCulatedPrice = Convert.ToString(MetalPrice + StoneSettingPrice + Convert.ToDecimal(LabCertDiamondPrice) + decFinishinPrice);  //Finishing_Price
                        }
                        else if (RapnetstudsStatus == "2")
                        {
                            CalCulatedPrice = Convert.ToString(MetalPrice + Convert.ToDecimal(LabCertDiamondPrice) + SideStoneDiamonds_Price + decFinishinPrice);  //Finishing_Price
                        }
                        else
                        {
                            CalCulatedPrice = Convert.ToString(MetalPrice + Convert.ToDecimal(LabCertDiamondPrice) + SideStoneDiamonds_Price + decFinishinPrice);
                        }

                    }
                    else
                    {
                        CalCulatedPrice = "2";

                    }
                }
                else
                {

                    CalCulatedPrice = Convert.ToString(MetalPrice + StoneSettingPrice + stonePrice + decFinishinPrice);  //Finishing_Price

                }

            }



            decimal TotPrice = Decimal.Round(Convert.ToDecimal(CalCulatedPrice), 2, MidpointRounding.AwayFromZero);
            CalCulatedPrice = Convert.ToString(TotPrice);

        }
        else
        {
            MetalPrice = Math.Round(EffectiveWeight_MetalPrice(MetalType, MetalQlty, Isenable, Pid, carWt1, length), 2, MidpointRounding.AwayFromZero);
            StoneSettingPrice = Math.Round(StoneSetting_Price(Radioval, carWt, Pid, length, SemiMount, Isenable, EnternityRingSize), 2, MidpointRounding.AwayFromZero);
            stonePrice = Decimal.Round(Stone_Price(Radioval, stoneType, strColor, strclarity, carWt, Pid, length, SemiMount, Isenable, EnternityRingSize), 2, MidpointRounding.AwayFromZero);
            if (Radioval.Trim() == "")
            {
                CalCulatedPrice = Convert.ToString(MetalPrice + StoneSettingPrice + stonePrice);
            }
            else
            {
                if (Radioval.Trim() == "Diamond" && SemiMount == "true")
                {

                    if (CenterDiamond.Trim() == "AnjoleeDiamond" || CenterDiamond.Trim() == "LabCertDiamond")
                    {
                        CalCulatedPrice = Convert.ToString(MetalPrice + SideStoneSettingPrice + SideDiamondstonePrice);
                    }

                    else
                    {

                        CalCulatedPrice = Convert.ToString(MetalPrice + StoneSettingPrice + stonePrice);

                    }

                }

                else if (Radioval.Trim() == "Diamond" && SemiMount == "false")
                {
                    if (CenterDiamond.Trim() == "AnjoleeDiamond")
                    {
                        if (RapnetstudsStatus == "1")
                        {
                            CalCulatedPrice = Convert.ToString(MetalPrice + StoneSettingPrice + stonePrice);
                        }
                        else if (RapnetstudsStatus == "2")
                        {
                            CalCulatedPrice = Convert.ToString(MetalPrice + CenterDiamonds_Price + SideStoneDiamonds_Price);
                        }
                        else
                        {

                            CalCulatedPrice = Convert.ToString(MetalPrice + CenterDiamonds_Price + SideStoneDiamonds_Price);
                        }

                    }
                    else if (CenterDiamond.Trim() == "LabCertDiamond")
                    {
                        if (LabCertDiamondPrice != "")
                        {
                            if (RapnetstudsStatus == "1")
                            {
                                CalCulatedPrice = Convert.ToString(MetalPrice + StoneSettingPrice + Convert.ToDecimal(LabCertDiamondPrice));
                            }
                            else if (RapnetstudsStatus == "2")
                            {
                                CalCulatedPrice = Convert.ToString(MetalPrice + Convert.ToDecimal(LabCertDiamondPrice) + SideStoneDiamonds_Price);
                            }
                            else
                            {

                                CalCulatedPrice = Convert.ToString(MetalPrice + Convert.ToDecimal(LabCertDiamondPrice) + SideStoneDiamonds_Price);
                            }


                        }
                        else
                        {
                            CalCulatedPrice = "2";

                        }
                    }
                    else
                    {

                        CalCulatedPrice = Convert.ToString(MetalPrice + StoneSettingPrice + stonePrice);
                    }




                }

                if (Radioval.Trim() == "NoDiamond")
                {
                    if (stoneType.Trim() == "")
                    {
                        StoneSettingPrice = 0;
                        stonePrice = 0;
                    }
                    CalCulatedPrice = Convert.ToString(MetalPrice + StoneSettingPrice + stonePrice);
                }
                if (Radioval.Trim() == "CubicZirconia")
                {
                    if (stoneType.Trim() == "")
                    {
                        stonePrice = 0;
                    }
                    CalCulatedPrice = Convert.ToString(MetalPrice + StoneSettingPrice + stonePrice);
                }
            }
            if (MetalType.Trim() == "White Gold" && SemiMount == "true" || MetalType.Trim() == "Yellow Gold" && SemiMount == "true" || MetalType.Trim() == "Two Tone" && SemiMount == "true")
            {
                if (CenterDiamond.Trim() == "AnjoleeDiamond" || CenterDiamond.Trim() == "LabCertDiamond")
                {
                    CalCulatedPrice = Convert.ToString(MetalPrice + SideStoneSettingPrice + SideDiamondstonePrice + Convert.ToDecimal(DtfixedCharge.Rows[0]["Finishing_Price"]));  //Finishing_Price
                }
                else
                {
                    CalCulatedPrice = Convert.ToString(MetalPrice + StoneSettingPrice + stonePrice + Convert.ToDecimal(DtfixedCharge.Rows[0]["Finishing_Price"]));  //Finishing_Price

                }


            }

            else if (MetalType.Trim() == "White Gold" && SemiMount == "false" || MetalType.Trim() == "Yellow Gold" && SemiMount == "false" || MetalType.Trim() == "Two Tone" && SemiMount == "false")
            {

                if (CenterDiamond.Trim() == "AnjoleeDiamond")
                {
                    if (RapnetstudsStatus == "1")
                    {
                        CalCulatedPrice = Convert.ToString(MetalPrice + StoneSettingPrice + stonePrice + Convert.ToDecimal(DtfixedCharge.Rows[0]["Finishing_Price"]));  //Finishing_Price);
                    }
                    else
                    {
                        CalCulatedPrice = Convert.ToString(MetalPrice + CenterDiamonds_Price + SideStoneDiamonds_Price + Convert.ToDecimal(DtfixedCharge.Rows[0]["Finishing_Price"]));  //Finishing_Price);  //Finishing_Price
                    }

                }
                else if (CenterDiamond.Trim() == "LabCertDiamond")
                {
                    if (LabCertDiamondPrice != "")
                    {
                        if (RapnetstudsStatus == "1")
                        {
                            CalCulatedPrice = Convert.ToString(MetalPrice + StoneSettingPrice + Convert.ToDecimal(LabCertDiamondPrice) + Convert.ToDecimal(DtfixedCharge.Rows[0]["Finishing_Price"]));  //Finishing_Price);
                        }
                        else if (RapnetstudsStatus == "2")
                        {
                            CalCulatedPrice = Convert.ToString(MetalPrice + Convert.ToDecimal(LabCertDiamondPrice) + SideStoneDiamonds_Price + Convert.ToDecimal(DtfixedCharge.Rows[0]["Finishing_Price"]));  //Finishing_Price);
                        }
                        else
                        {
                            CalCulatedPrice = Convert.ToString(MetalPrice + Convert.ToDecimal(LabCertDiamondPrice) + SideStoneDiamonds_Price + Convert.ToDecimal(DtfixedCharge.Rows[0]["Finishing_Price"]));  //Finishing_Price);
                        }

                    }
                    else
                    {
                        CalCulatedPrice = "2";

                    }
                }
                else
                {

                    CalCulatedPrice = Convert.ToString(MetalPrice + StoneSettingPrice + stonePrice + Convert.ToDecimal(DtfixedCharge.Rows[0]["Finishing_Price"]));  //Finishing_Price);  

                }

            }

            decimal TotSettingPrice = Decimal.Round(Convert.ToDecimal(SettingPrice), 2, MidpointRounding.AwayFromZero);
            decimal TotPrice = Decimal.Round(Convert.ToDecimal(CalCulatedPrice), 2, MidpointRounding.AwayFromZero);
            if (Convert.ToDecimal(DtfixedCharge.Rows[0]["Premium_charge"]) > 0 && SemiMount == "true")
            {
                TotPrice = Decimal.Round((TotPrice * Convert.ToDecimal(DtfixedCharge.Rows[0]["Premium_charge"])), 2, MidpointRounding.AwayFromZero);
                CalCulatedPrice = Convert.ToString(TotPrice);
            }
            if (Convert.ToDecimal(DtfixedCharge.Rows[0]["Premium_charge"]) > 0 && SemiMount == "false")
            {

                if (CenterDiamond.Trim() == "LabCertDiamond")
                {

                    if (RapnetstudsStatus == "1")
                    {

                    }
                    else if (RapnetstudsStatus == "2")
                    {

                    }
                    else
                    {
                        if (LabCertDiamondPrice != "")
                        {
                            TotPrice = Decimal.Round((TotPrice * Convert.ToDecimal(DtfixedCharge.Rows[0]["Premium_charge"])), 2, MidpointRounding.AwayFromZero);
                            CalCulatedPrice = Convert.ToString(TotPrice);
                        }
                        else
                        {
                        }
                    }



                }

                else if (CenterDiamond.Trim() == "AnjoleeDiamond")
                {
                    if (RapnetstudsStatus == "1")
                    {

                    }
                    else if (RapnetstudsStatus == "2")
                    {

                    }

                    else
                    {

                        TotPrice = Decimal.Round((TotPrice * Convert.ToDecimal(DtfixedCharge.Rows[0]["Premium_charge"])), 2, MidpointRounding.AwayFromZero);
                        CalCulatedPrice = Convert.ToString(TotPrice);

                    }

                }
                else
                {
                    TotPrice = Decimal.Round((TotPrice * Convert.ToDecimal(DtfixedCharge.Rows[0]["Premium_charge"])), 2, MidpointRounding.AwayFromZero);
                    CalCulatedPrice = Convert.ToString(TotPrice);
                }

            }

            else
            {
                CalCulatedPrice = Convert.ToString(TotPrice);
            }

        }

        //------------------------------------ Get Carat Premium Calculation-----------------------------------------//

        string CalCulatedPriceWithCaratPremium = CalCulatedPrice;
        DataSet DSProductCaratPremium = objComFun.GetProductCaratPremium(carWt);
        if (DSProductCaratPremium.Tables[0].Rows.Count > 0)
        {
            ProductCaratPremium = DSProductCaratPremium.Tables[0].Rows[0]["CaratPremium"].ToString();
            if (ProductCaratPremium != "" && ProductCaratPremium != "0.00" && ProductCaratPremium != "0")
            {
                if (ProductCaratPremium.Contains("-"))
                {
                    TotPriceCaratPremium = Convert.ToDecimal(CalCulatedPrice) + (Convert.ToDecimal(CalCulatedPrice) * (Convert.ToDecimal(ProductCaratPremium) + 1));
                    TotPriceCaratPremium = Decimal.Round(TotPriceCaratPremium, 2, MidpointRounding.AwayFromZero);
                    CalCulatedPrice = TotPriceCaratPremium.ToString();

                }
                else
                {
                    TotPriceCaratPremium = Convert.ToDecimal(ProductCaratPremium) * Convert.ToDecimal(CalCulatedPrice);
                    TotPriceCaratPremium = Decimal.Round(TotPriceCaratPremium, 2, MidpointRounding.AwayFromZero);
                    CalCulatedPrice = TotPriceCaratPremium.ToString();
                }
            }
        }

        //------------------------------------ Get Carat Premium Calculation-----------------------------------------//



        if (CenterDiamond.Trim() == "AnjoleeDiamond")
        {
            CenterDiamondPrice = CenterDiamonds_Price;
        }
        else
        {
            CenterDiamondPrice = anjoleediamondCenterDiamondPrice;
        }
        _datarows["CalCulatedPrice"] = CalCulatedPrice;

        if (RapnetstudsStatus == "1")
        {
            if (CenterDiamond.Trim() == "LabCertDiamond")
            {

                _datarows["CenterDiamondPrice"] = anjoleediamondCenterDiamondPrice;
            }
            else if (CenterDiamond.Trim() == "AnjoleeDiamond")
            {

                _datarows["CenterDiamondPrice"] = stonePrice;
            }
        }
        else
        {

            _datarows["CenterDiamondPrice"] = CenterDiamondPrice;

            _datarows["SideDiamondPrice"] = SideStoneDiamonds_Price;
        }

        dt.Rows.Add(_datarows);
        return dt;



    }

    public string[] Stone_Price(string Radioval, string stoneType, string strColor, string strclarity, string carWt, string pid, string Length)
    {
        string[] myarray = new string[4];
        bool markups = false;
        decimal StonePrice = 0;
        decimal TotalStonePrice = 0;
        decimal MarkupPrice = 0;     
        decimal EffectWt = 0;
        decimal EffectWtc = 0;
        decimal DiamondStonePrice = 0;
        decimal x = 0;
        decimal y = 0;
        string caratweight = string.Empty;
        string stoneSize = string.Empty;

        try
        {
            string strPrice;
            DataTable dtStoneSetting = ojbProdsizeHelper.ReturnProductStoneDetails(pid, carWt.Trim());

            caratweight = dtStoneSetting.Rows[0]["caratweight"].ToString();
            stoneSize = dtStoneSetting.Rows[0]["stoneSize"].ToString();
            DataSet dsBangle = objComFun.GetBangleDiamondStatus(pid);
            string Banglestatus = dsBangle.Tables[0].Rows[0]["ProductGroup_SubSubCategoryName"].ToString().Trim();
            if (Banglestatus == "BanglesDiamondLength")
            {
                OrderLen = defaltlen;
            }
            if (Radioval.Trim() == "Diamond")
            {
                for (int k = 0; k < dtStoneSetting.Rows.Count; k++)
                {
                    if (dtStoneSetting.Rows[k]["StoneType"].ToString() == "1")
                    {
                        if (OrderLen != defaltlen)
                        {
                            x = ((Convert.ToDecimal(dtStoneSetting.Rows[k]["StoneQty"])) / defaltlen) * OrderLen;

                        }
                        else
                        {
                            x = Convert.ToDecimal(dtStoneSetting.Rows[k]["StoneQty"]);
                        }
                        y = Decimal.Round(x, 0, MidpointRounding.AwayFromZero);


                        StonePrice = (Decimal.Round(x, 0)) * (Convert.ToDecimal(dtStoneSetting.Rows[k]["caratweight"]));
                        if (EffectWt == 0)
                        {
                            EffectWt = (Decimal.Round(x, 0, MidpointRounding.AwayFromZero)) * (Convert.ToDecimal(dtStoneSetting.Rows[k]["caratweight"]));
                        }
                        else
                        {
                            EffectWt = EffectWt + ((Decimal.Round(x, 0, MidpointRounding.AwayFromZero)) * (Convert.ToDecimal(dtStoneSetting.Rows[k]["caratweight"])));
                        }
                        string diamondPrices = "SELECT price,clarity,color FROM Diamonds WHERE Clarity in(select ClarityID from Clarities where  Clarity='" + strclarity + "')";
                        diamondPrices += "and Shape='" + dtStoneSetting.Rows[k]["StoneShapeid"].ToString() + "' and  ((Weight1 <= " + dtStoneSetting.Rows[k]["caratweight"].ToString() + ") AND (Weight2 >=" + dtStoneSetting.Rows[k]["caratweight"].ToString() + ")) and color in(select colorid from colors where color='" + strColor + "')AND VendorID='" + dtStoneSetting.Rows[k]["vendorID"].ToString() + "' and MasterMerchantID is null";

                        strPrice = objComFun.ExecuteQueryReturnSingleString(diamondPrices);
                        if (strPrice.ToString() == string.Empty)
                        {
                            strPrice = "1";
                        }
                        decimal price = Convert.ToDecimal(strPrice.ToString());
                        StonePrice = StonePrice * price;
                        StonePrice = Math.Round(StonePrice, 3, MidpointRounding.AwayFromZero);
                        DiamondStonePrice = DiamondStonePrice + StonePrice;
                        DiamondStonePrice = Math.Round(DiamondStonePrice, 2, MidpointRounding.AwayFromZero);
                    }

                }
            }


            for (int k = 0; k < dtStoneSetting.Rows.Count; k++)
            {
                if (dtStoneSetting.Rows[k]["StoneType"].ToString() != "1")
                {

                    if (y != defaltlen)
                    {
                        x = ((Convert.ToDecimal(dtStoneSetting.Rows[k]["StoneQty"])) / defaltlen) * OrderLen;

                    }
                    else
                    {
                        x = Convert.ToDecimal(dtStoneSetting.Rows[k]["StoneQty"]);
                    }
                    y = Decimal.Round(x, 0, MidpointRounding.AwayFromZero);

                    StonePrice = (Decimal.Round(x, 0)) * (Convert.ToDecimal(dtStoneSetting.Rows[k]["caratweight"]));
                    string colorstoneprice = "SELECT price FROM colorstones WHERE((Weight <= " + dtStoneSetting.Rows[k]["caratweight"].ToString() + ") AND (Weight1 >=" + dtStoneSetting.Rows[k]["caratweight"].ToString() + ")) AND shape='" + dtStoneSetting.Rows[k]["stoneshapeid"].ToString() + "' and colorStoneTypeid in(select colorStoneTypeID from tblColorStone_Type where ColorStoneType='" + stoneType + "')  AND VendorID='" + dtStoneSetting.Rows[k]["vendorID"].ToString() + "' ";
                    strPrice = objComFun.ExecuteQueryReturnSingleString(colorstoneprice);

                    if (EffectWtc == 0)
                    {
                        EffectWtc = (Decimal.Round(x, 0, MidpointRounding.AwayFromZero)) * (Convert.ToDecimal(dtStoneSetting.Rows[k]["caratweight"]));
                    }
                    else
                    {
                        EffectWtc = EffectWtc + ((Decimal.Round(x, 0, MidpointRounding.AwayFromZero)) * (Convert.ToDecimal(dtStoneSetting.Rows[k]["caratweight"])));
                    }


                    if (strPrice.ToString() == string.Empty)
                    {
                        strPrice = "1";
                    }
                    decimal price = Convert.ToDecimal(strPrice.ToString());
                    StonePrice = StonePrice * price;
                    StonePrice = Math.Round(StonePrice, 3, MidpointRounding.AwayFromZero);
                    TotalStonePrice = TotalStonePrice + StonePrice;
                    TotalStonePrice = Math.Round(TotalStonePrice, 2, MidpointRounding.AwayFromZero);
                }
            }

            TotalStonePrice = TotalStonePrice + DiamondStonePrice;
            EffectWt = EffectWt + EffectWtc;
            string strMarkup = objComFun.ExecuteQueryReturnSingleString("SELECT MarkupProduct FROM Products WHERE productid='" + pid + "'");
            if (strMarkup.Trim() != string.Empty)
            {
                decimal marks = Convert.ToDecimal(strMarkup.Trim());
                marks = 100 + marks;
                ///
                MarkupPrice = TotalStonePrice * (marks / 100);
                MarkupPrice = Math.Round(MarkupPrice, 2, MidpointRounding.AwayFromZero);

                markups = true;
            }
            else
            {
                markups = false;

            }
        }
        catch (Exception ee)
        {
            string er = ee.ToString();
            err = true;
        }
        string EffectWtString = string.Empty;
       


      

        if (EffectWt == 0)
        {
            EffectiveWt = string.Empty;
            EffectWtString = EffectiveWt;
        }
        else
        {
            EffectiveWt = (Decimal.Round(EffectWt, 2)).ToString();
            if (EffectWt < 1)
            {
                EffectWtString = EffectiveWt.ToString();

                EffectWtString = EffectWtString.Substring(1, EffectWtString.Length - 1);
            }
            else
            {
                EffectWtString = EffectWt.ToString("0.00");


            }


        }


        myarray[0] = EffectWtString;
        myarray[1] = Convert.ToString(y);
        myarray[2] = Convert.ToString(y) +"X" + caratweight+ "ct.";
        myarray[3] = Convert.ToString(y) + "X" + stoneSize + "mm";
       
        return myarray;



    }

    public string[] Stone_PriceForSpecialPremiums(string Radioval, string stoneType, string strColor, string strclarity, string carWt, string pid, string Length)
    {
        string[] myarray = new string[4];
        
        string caratweight = string.Empty;
        string stoneSize = string.Empty;
        decimal StonePrice = 0;
        decimal TotalStonePrice = 0;
        decimal MarkupPrice = 0;
        bool markups = false;
        decimal EffectWt = 0;
        decimal EffectWtc = 0;
        decimal DiamondStonePrice = 0;
        decimal x = 0;
        decimal y = 0;

        try
        {
            string strPrice;
            DataTable dtStoneSetting = ojbProdsizeHelper.ReturnProductStoneDetails(pid, carWt.Trim());
            caratweight = dtStoneSetting.Rows[0]["caratweight"].ToString();
            stoneSize = dtStoneSetting.Rows[0]["stoneSize"].ToString();
            if (Radioval.Trim() == "Diamond")
            {
                for (int k = 0; k < dtStoneSetting.Rows.Count; k++)
                {
                    if (dtStoneSetting.Rows[k]["StoneType"].ToString() == "1")
                    {
                        if (OrderLen != defaltlen)
                        {
                            x = ((Convert.ToDecimal(dtStoneSetting.Rows[k]["StoneQty"])) / defaltlen) * OrderLen;

                        }
                        else
                        {
                            x = Convert.ToDecimal(dtStoneSetting.Rows[k]["StoneQty"]);
                        }
                        y = Decimal.Round(x, 0);

                        StonePrice = (Decimal.Round(x, 0)) * (Convert.ToDecimal(dtStoneSetting.Rows[k]["caratweight"]));
                        if (EffectWt == 0)
                        {
                            EffectWt = (Decimal.Round(x, 0)) * (Convert.ToDecimal(dtStoneSetting.Rows[k]["caratweight"]));
                        }
                        else
                        {
                            EffectWt = EffectWt + ((Decimal.Round(x, 0)) * (Convert.ToDecimal(dtStoneSetting.Rows[k]["caratweight"])));
                        }
                        string diamondPrices = "SELECT price,specialpremium FROM Diamonds WHERE Clarity in(select ClarityID from Clarities where  Clarity='" + strclarity + "')";
                        diamondPrices += "and Shape='" + dtStoneSetting.Rows[k]["StoneShapeid"].ToString() + "' and  ((Weight1 <= " + dtStoneSetting.Rows[k]["caratweight"].ToString() + ") AND (Weight2 >=" + dtStoneSetting.Rows[k]["caratweight"].ToString() + ")) and color in(select colorid from colors where color='" + strColor + "')AND VendorID='" + dtStoneSetting.Rows[k]["vendorID"].ToString() + "' and MasterMerchantID is null";

                        DataTable dtDiamondPrice = objComFun.ExecuteQueryReturnDataTable(diamondPrices);
                        strPrice = dtDiamondPrice.Rows[0]["price"].ToString();
                        decimal decSpecPrem = Convert.ToDecimal(dtDiamondPrice.Rows[0]["specialpremium"]);
                        if (strPrice.ToString() == string.Empty)
                        {
                            strPrice = "1";
                        }
                        decimal price = Convert.ToDecimal(strPrice.ToString());
                        StonePrice = StonePrice * price;
                        if (decSpecPrem > 0)
                            StonePrice = StonePrice * decSpecPrem;
                        else if (Convert.ToDecimal(DtfixedCharge.Rows[0]["Premium_charge"]) > 0)
                            StonePrice = StonePrice * Convert.ToDecimal(DtfixedCharge.Rows[0]["Premium_charge"]);

                        StonePrice = Math.Round(StonePrice, 3, MidpointRounding.AwayFromZero);
                        DiamondStonePrice = DiamondStonePrice + StonePrice;
                        DiamondStonePrice = Math.Round(DiamondStonePrice, 2, MidpointRounding.AwayFromZero);
                    }

                }
            }


            for (int k = 0; k < dtStoneSetting.Rows.Count; k++)
            {
                if (dtStoneSetting.Rows[k]["StoneType"].ToString() != "1")
                {

                    if (y != defaltlen)
                    {
                        x = ((Convert.ToDecimal(dtStoneSetting.Rows[k]["StoneQty"])) / defaltlen) * OrderLen;

                    }
                    else
                    {
                        x = Convert.ToDecimal(dtStoneSetting.Rows[k]["StoneQty"]);
                    }
                    y = Decimal.Round(x, 0);

                    StonePrice = (Decimal.Round(x, 0)) * (Convert.ToDecimal(dtStoneSetting.Rows[k]["caratweight"]));
                    string colorstoneprice = "SELECT price FROM colorstones WHERE((Weight <= " + dtStoneSetting.Rows[k]["caratweight"].ToString() + ") AND (Weight1 >=" + dtStoneSetting.Rows[k]["caratweight"].ToString() + ")) AND shape='" + dtStoneSetting.Rows[k]["stoneshapeid"].ToString() + "' and colorStoneTypeid in(select colorStoneTypeID from tblColorStone_Type where ColorStoneType='" + stoneType + "')  AND VendorID='" + dtStoneSetting.Rows[k]["vendorID"].ToString() + "' ";
                    strPrice = objComFun.ExecuteQueryReturnSingleString(colorstoneprice);



                    if (EffectWtc == 0)
                    {
                        EffectWtc = (Decimal.Round(x, 0)) * (Convert.ToDecimal(dtStoneSetting.Rows[k]["caratweight"]));
                    }
                    else
                    {
                        EffectWtc = EffectWtc + ((Decimal.Round(x, 0)) * (Convert.ToDecimal(dtStoneSetting.Rows[k]["caratweight"])));
                    }

                    if (strPrice.ToString() == string.Empty)
                    {
                        strPrice = "1";
                    }
                    decimal price = Convert.ToDecimal(strPrice.ToString());
                    StonePrice = StonePrice * price;
                    StonePrice = Math.Round(StonePrice, 3, MidpointRounding.AwayFromZero);
                    TotalStonePrice = TotalStonePrice + StonePrice;
                    TotalStonePrice = Math.Round(TotalStonePrice, 2, MidpointRounding.AwayFromZero);
                }
            }
            if (Convert.ToDecimal(DtfixedCharge.Rows[0]["Premium_charge"]) > 0)
                TotalStonePrice = (TotalStonePrice * Convert.ToDecimal(DtfixedCharge.Rows[0]["Premium_charge"])) + DiamondStonePrice;
            else
                TotalStonePrice = TotalStonePrice + DiamondStonePrice;

            EffectWt = EffectWt + EffectWtc;

            string strMarkup = objComFun.ExecuteQueryReturnSingleString("SELECT MarkupProduct FROM Products WHERE productid='" + pid + "'");
            if (strMarkup.Trim() != string.Empty)
            {
                decimal marks = Convert.ToDecimal(strMarkup.Trim());
                marks = 100 + marks;
                ///
                MarkupPrice = TotalStonePrice * (marks / 100);
                MarkupPrice = Math.Round(MarkupPrice, 2, MidpointRounding.AwayFromZero);

                markups = true;
            }
            else
            {
                markups = false;

            }
        }
        catch (Exception ee)
        {
            string er = ee.ToString();
            err = true;
        }
        string EffectWtString = string.Empty;
        if (EffectWt == 0)
        {
            EffectiveWt = string.Empty;
            EffectWtString = EffectiveWt;
        }
        else
        {
            EffectiveWt = (Decimal.Round(EffectWt, 2)).ToString();

            if (EffectWt < 1)
            {
                EffectWtString = EffectiveWt.ToString();

                EffectWtString = EffectWtString.Substring(1, EffectWtString.Length - 1);
            }
            else
            {
                EffectWtString = EffectWt.ToString("0.00");
            }


        }





        myarray[0] = EffectWtString;
        myarray[1] = Convert.ToString(y);
        myarray[2] = Convert.ToString(y) + "X" + caratweight + "ct.";
        myarray[3] = Convert.ToString(y) + "X" + stoneSize + "mm";
        return myarray;


    }

    public bool GetPriceCalculationOption(string Radioval, string stoneType, string strColor, string strclarity, string carWt, string pid)
    {
        try
        {
            DataTable dtStoneSetting = ojbProdsizeHelper.ReturnProductStoneDetails(pid, carWt.Trim());
            if (Radioval.Trim() == "Diamond")
            {
                for (int k = 0; k < dtStoneSetting.Rows.Count; k++)
                {
                    if (dtStoneSetting.Rows[k]["StoneType"].ToString() == "1")
                    {
                        string diamondPrices = "SELECT specialpremium FROM Diamonds WHERE Clarity in(select ClarityID from Clarities where  Clarity='" + strclarity + "')";
                        diamondPrices += "and Shape='" + dtStoneSetting.Rows[k]["StoneShapeid"].ToString() + "' and  ((Weight1 <= " + dtStoneSetting.Rows[k]["caratweight"].ToString() + ") AND (Weight2 >=" + dtStoneSetting.Rows[k]["caratweight"].ToString() + ")) and color in(select colorid from colors where color='" + strColor + "')AND VendorID='" + dtStoneSetting.Rows[k]["vendorID"].ToString() + "' and MasterMerchantID is null";

                        decimal result = Convert.ToDecimal(objComFun.ExecuteQueryReturnSingleString(diamondPrices));
                        if (result > 0)
                        {
                            return true;
                        }
                    }

                }
            }
            return false;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
         
           
    public string ProductSize(string carwt, string pid, string Semimount)
    {
        string CTSize = string.Empty;

        DataSet dtsize;
        try
        {

            dtsize = ojbProdsizeHelper.ReturnProductSizes(pid.ToString(), carwt.Trim(), Semimount);
            if (dtsize.Tables[0].Rows.Count != 0)
            {

                CTSize = dtsize.Tables[0].Rows[0]["Sizes"].ToString();

            }
        }
        catch (Exception ee)
        {
            string er = ee.ToString();
            err = true;
        }

        return CTSize;
    }

    public decimal getMetalWeight(string MT, string MQ, string Isenable, string pid, string caretid, string Length)
    {
        string Metals = string.Empty;
        decimal PEW = 0;
        decimal EWtAT14k;


        try
        {

            string str = "select [14kDefaultWeight],Platinumlabor,GoldLabor,Defaultlength from ProductSizes where productid='" + pid + "'and Productsizeid='" + carWt + "'";
            DataTable dt = objComFun.ExecuteQueryReturnDataTable(str);
            defaltlen = Convert.ToDecimal(dt.Rows[0]["Defaultlength"].ToString());
            string strSql = "SELECT ltrim(rtrim(case when exists(select Bangle_Length  from products ds where ds.productId='" + pid + "' and ISNULL(ProductGroup_SubSubCategoryName, '') IN ('BanglesDiamondLength','BanglesMetalLength')) then (select Bangle_Length  from products ds where ds.productId='" + pid + "' and ISNULL(ProductGroup_SubSubCategoryName, '') IN ('BanglesDiamondLength','BanglesMetalLength')) else DefaultSize end)) DefaultSize  FROM ProductsGroups WHERE ProductsGroupID IN(SELECT ProductsProductsGroupID FROM ProductsProductsGroups WHERE ProductID='" + pid + "')";
            string strq = objComFun.ExecuteQueryReturnSingleString(strSql);
            if (strq.Trim() == "")
            {

                defaltlen = 1;
                OrderLen = 1;

            }
            if (Isenable.Trim() == "T")
            {
                OrderLen = defaltlen;
            }
            if (Isenable.Trim() == "ET")
            {
                OrderLen = defaltlen;
            }
            if (defaltlen != 0)
            {
                if (OrderLen != defaltlen)
                {
                    EWtAT14k = (Convert.ToDecimal(dt.Rows[0]["14kDefaultWeight"]) * OrderLen) / defaltlen;
                }
                else
                {
                    EWtAT14k = Convert.ToDecimal(dt.Rows[0]["14kDefaultWeight"]);
                }
            }
            else { EWtAT14k = Convert.ToDecimal(dt.Rows[0]["14kDefaultWeight"]); }

            if ((MT.Trim() == "Platinum") && (MQ.Trim() == "950"))
            {
                PEW = Math.Round((EWtAT14k * Convert.ToDecimal("1.642")), 2, MidpointRounding.AwayFromZero);
                Metals = "Platinum";
            }
            if ((MT.Trim() == "Two Tone" || MT.Trim() == "Yellow Gold" || MT.Trim() == "White Gold") && MQ.Trim() == "14k")
            {
                PEW = Math.Round(EWtAT14k, 2, MidpointRounding.AwayFromZero);
                Metals = "Gold";
            }
            if ((MT.Trim() == "Two Tone" || MT.Trim() == "Yellow Gold" || MT.Trim() == "White Gold") && MQ.Trim() == "18k")
            {
                PEW = Math.Round((EWtAT14k * Convert.ToDecimal("1.192")), 2, MidpointRounding.AwayFromZero);
                Metals = "Gold";
            }
            if ((MT.Trim() == "Two Tone" || MT.Trim() == "Yellow Gold" || MT.Trim() == "White Gold") && MQ.Trim() == "10k")
            {
                PEW = (Math.Round((EWtAT14k * Convert.ToDecimal("0.885")), 2, MidpointRounding.AwayFromZero));

            }

            PEW = Math.Round(PEW, 2, System.MidpointRounding.AwayFromZero);


        }
        catch (Exception ee)
        {
            string er = ee.ToString();
            err = true;
        }
        return PEW;
    }

    public decimal EffectiveWeight_MetalPrice(string MT, string MQ, string Isenable, string pid, string caretid, string Length)
    {

        string Metals = string.Empty;
        decimal PEW = 0;
        decimal MetalCost = 0;

        decimal NotIclLoss = 0;
        decimal InclLoss = 0;
        decimal EWtAT14k;

        try
        {

            string str = "select [14kDefaultWeight],Platinumlabor,GoldLabor,Defaultlength from ProductSizes where productid='" + pid + "'and Productsizeid='" + carWt + "'";
            DataTable dt = objComFun.ExecuteQueryReturnDataTable(str);
            defaltlen = Convert.ToDecimal(dt.Rows[0]["Defaultlength"].ToString());
            string strSql = "SELECT ltrim(rtrim(case when exists(select Bangle_Length  from products ds where ds.productId='" + pid + "' and ISNULL(ProductGroup_SubSubCategoryName, '') IN ('BanglesDiamondLength','BanglesMetalLength')) then (select Bangle_Length  from products ds where ds.productId='" + pid + "' and ISNULL(ProductGroup_SubSubCategoryName, '') IN ('BanglesDiamondLength','BanglesMetalLength')) else DefaultSize end)) DefaultSize ,ISSize,case when exists(select Bangle_Length  from products ds where ds.productId='" + pid + "' and ISNULL(ProductGroup_SubSubCategoryName, '') IN ('BanglesDiamondLength','BanglesMetalLength')) then 1 else ISLength end ISLength,ISEnable FROM ProductsGroups WHERE ProductsGroupID IN(SELECT ProductsProductsGroupID FROM ProductsProductsGroups WHERE ProductID='" + pid + "')";
            string strq = objComFun.ExecuteQueryReturnSingleString(strSql);
            if (strq.Trim() == "")
            {

                defaltlen = 1;
                OrderLen = 1;

            }

            if (Isenable.Trim() == "T")
            {
                OrderLen = defaltlen;
            }

            if (defaltlen != 0)
            {
                if (OrderLen != defaltlen)
                {
                    EWtAT14k = (Convert.ToDecimal(dt.Rows[0]["14kDefaultWeight"]) * OrderLen) / defaltlen;

                }
                else
                {
                    EWtAT14k = Convert.ToDecimal(dt.Rows[0]["14kDefaultWeight"]);
                }
            }
            else { EWtAT14k = Convert.ToDecimal(dt.Rows[0]["14kDefaultWeight"]); }

            if ((MT.Trim() == "Platinum") && (MQ.Trim() == "950"))
            {
                PEW = Math.Round((EWtAT14k * Convert.ToDecimal("1.642")), 2, MidpointRounding.AwayFromZero);
                Metals = "Platinum";
            }
            if ((MT.Trim() == "Two Tone" || MT.Trim() == "Yellow Gold" || MT.Trim() == "White Gold") && MQ.Trim() == "14k")
            {
                PEW = Math.Round(EWtAT14k, 2, MidpointRounding.AwayFromZero);
                Metals = "Gold";
            }
            if ((MT.Trim() == "Two Tone" || MT.Trim() == "Yellow Gold" || MT.Trim() == "White Gold") && MQ.Trim() == "18k")
            {
                PEW = Math.Round((EWtAT14k * Convert.ToDecimal("1.192")), 2, MidpointRounding.AwayFromZero);
                Metals = "Gold";
            }
            if ((MT.Trim() == "Two Tone" || MT.Trim() == "Yellow Gold" || MT.Trim() == "White Gold") && MQ.Trim() == "10k")
            {
                PEW = (Math.Round((EWtAT14k * Convert.ToDecimal("0.885")), 2, MidpointRounding.AwayFromZero));

                Metals = "Gold";
            }

            PEW = Math.Round(PEW, 1, System.MidpointRounding.AwayFromZero);


            string PlMarketPrice = "select MetalLoss as price from Metals where MetalName='" + Metals + "'";
            DataTable dts = objComFun.ExecuteQueryReturnDataTable(PlMarketPrice);
            if (dts.Rows[0]["price"] == null)
            {
                dts.Rows[0]["price"] = "0";
            }
            if (Metals == "Platinum")
            {

                decimal PP = (Convert.ToDecimal(dts.Rows[0]["price"]) * Convert.ToDecimal(DtfixedCharge.Rows[0]["Pure_Platinum"])) / 100;
                decimal tot = Convert.ToDecimal(DtfixedCharge.Rows[0]["Vender_Charge"]) + PP;
                decimal TotalCost = ((tot / Convert.ToDecimal("31.103")) * Convert.ToDecimal(DtfixedCharge.Rows[0]["Platinum_Charge"])) + Convert.ToDecimal(dt.Rows[0]["Platinumlabor"].ToString());
                MetalCost = decimal.Round(TotalCost, 2, MidpointRounding.AwayFromZero) * PEW;
                MetalCost = decimal.Round(MetalCost, 2, MidpointRounding.AwayFromZero);
            }
            else
            {
                decimal EFPrice = (Convert.ToDecimal(dts.Rows[0]["price"]) + Convert.ToDecimal(DtfixedCharge.Rows[0]["Gold_Charge"]));//add sunrise charge
                decimal ConverttoGram = EFPrice / (Convert.ToDecimal("31.1033"));
                decimal TotalLaborRate = PEW * (Convert.ToDecimal(dt.Rows[0]["GoldLabor"].ToString()));
                if (MQ == "10k")
                {
                    NotIclLoss = ConverttoGram * 10 / 24;
                }
                if (MQ == "14k")
                {
                    NotIclLoss = ConverttoGram * 14 / 24;
                }
                if (MQ == "18k")
                {
                    NotIclLoss = ConverttoGram * 18 / 24;
                }
                NotIclLoss = Math.Round(NotIclLoss, 2, MidpointRounding.AwayFromZero);
                InclLoss = NotIclLoss * Convert.ToDecimal(DtfixedCharge.Rows[0]["Gold_Loss"]);
                decimal metalPricess = InclLoss * PEW;
                MetalCost = Math.Round((metalPricess + TotalLaborRate), 2, MidpointRounding.AwayFromZero);

            }

        }
        catch (Exception ee)
        {
            string er = ee.ToString();
            err = true;
        }
        return MetalCost;


    }

    public bool GetPriceCalculationOption(string Radioval, string stoneType, string strColor, string strclarity, string carWt, string pid, string SemiMount)
    {
        try
        {
            DataTable dtStoneSetting = ojbProdsizeHelper.ReturnProductStoneDetailsNew(pid, carWt.Trim(), SemiMount);
            if (Radioval.Trim() == "Diamond")
            {
                for (int k = 0; k < dtStoneSetting.Rows.Count; k++)
                {
                    if (dtStoneSetting.Rows[k]["StoneType"].ToString() == "1")
                    {
                        string diamondPrices = "SELECT specialpremium FROM Diamonds WHERE Clarity in(select ClarityID from Clarities where  Clarity='" + strclarity + "')";
                        diamondPrices += "and Shape='" + dtStoneSetting.Rows[k]["StoneShapeid"].ToString() + "' and  ((Weight1 <= " + dtStoneSetting.Rows[k]["caratweight"].ToString() + ") AND (Weight2 >=" + dtStoneSetting.Rows[k]["caratweight"].ToString() + ")) and color in(select colorid from colors where color='" + strColor + "')AND VendorID='" + dtStoneSetting.Rows[k]["vendorID"].ToString() + "' and MasterMerchantID is null";

                        decimal result = Convert.ToDecimal(objComFun.ExecuteQueryReturnSingleString(diamondPrices));
                        if (result > 0)
                        {

                            if (SemiMount == "true")
                            {
                                return false;

                            }
                            return true;
                        }
                    }

                }
            }

            DataSet RapnetStatus = objComFun.GetRapnetStatus(pid);
            if (RapnetStatus.Tables[0].Rows.Count != 0)
            {
                string RapNet = RapnetStatus.Tables[0].Rows[0]["RapNet"].ToString();
                string RapNetStud = RapnetStatus.Tables[0].Rows[0]["RapNetStud"].ToString();
                if (RapNet == "True" || RapNetStud == "True")
                {
                    return true;
                }
            }

            return false;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public decimal StoneSetting_Price(string Radioval, string carWt, string pid, string Length, string SemiMount, string Isenable, decimal EnternityRingSize)
    {
        //Calculation for stoneSetting---

        decimal x = 0;
        decimal y = 0;
        decimal stoneSettingPrice = 0;
        decimal DiamondSettingPrice = 0;
        decimal ColorStoneSettingPrice = 0;

        try
        {
            string str = "select [14kDefaultWeight],Platinumlabor,GoldLabor,Defaultlength from ProductSizes where productid='" + pid + "'and Productsizeid='" + carWt + "'";
            DataTable dt = objComFun.ExecuteQueryReturnDataTable(str);
            defaltlen = Convert.ToDecimal(dt.Rows[0]["Defaultlength"].ToString());

            if (defaltlen == Convert.ToDecimal(0.00))
            {
                defaltlen = 1;
                OrderLen = 1;

            }

            DataSet dsBangle = objComFun.GetBangleDiamondStatus(pid);
            string Banglestatus = dsBangle.Tables[0].Rows[0]["ProductGroup_SubSubCategoryName"].ToString().Trim();
            if (Banglestatus == "BanglesDiamondLength")
            {
                OrderLen = defaltlen;
            }
            DataTable dtStoneSetting = new DataTable();

            if (Isenable.Trim() == "ET")
            {

                dtStoneSetting = ojbProdsizeHelper.ReturnProductStoneDetailsNew1(pid, carWt.Trim(), SemiMount, EnternityRingSize);

            }
            else
            {
                dtStoneSetting = ojbProdsizeHelper.ReturnProductStoneDetailsNew(pid, carWt.Trim(), SemiMount);

            }



            if (dtStoneSetting.Rows.Count != 0)
            {
                for (int i = 0; i < dtStoneSetting.Rows.Count; i++)
                {
                    if ((dtStoneSetting.Rows[i]["stoneType"].ToString() == "1"))
                    {
                        if (Radioval.Trim() != "NoDiamond")
                        {
                            if (OrderLen != defaltlen)
                            {
                                x = ((Convert.ToDecimal(dtStoneSetting.Rows[i]["StoneQty"])) / defaltlen) * OrderLen;

                            }
                            else
                            {
                                x = Convert.ToDecimal(dtStoneSetting.Rows[i]["StoneQty"]);
                            }
                            y = Decimal.Round(x, 0);

                            StoneNo = StoneNo + y;

                            DiamondSettingPrice += Math.Round((Decimal.Round(x, 0)) * (Convert.ToDecimal(dtStoneSetting.Rows[i]["price"])), 2, MidpointRounding.AwayFromZero);
                        }
                        else
                        {
                            if (OrderLen != defaltlen)
                            {
                                x = ((Convert.ToDecimal(dtStoneSetting.Rows[i]["StoneQty"])) / defaltlen) * OrderLen;

                            }
                            else
                            {
                                x = Convert.ToDecimal(dtStoneSetting.Rows[i]["StoneQty"]);
                            }
                            y = Decimal.Round(x, 0);

                            StoneNoForNew = StoneNoForNew + y;
                        }
                    }
                }
                for (int i = 0; i < dtStoneSetting.Rows.Count; i++)
                {
                    if ((dtStoneSetting.Rows[i]["stoneType"].ToString() != "1"))
                    {
                        if (y != defaltlen)
                        {
                            x = ((Convert.ToDecimal(dtStoneSetting.Rows[i]["StoneQty"])) / defaltlen) * OrderLen;

                        }
                        else
                        {
                            x = Convert.ToDecimal(dtStoneSetting.Rows[i]["StoneQty"]);
                        }
                        y = Decimal.Round(x, 0);

                        StoneNo = StoneNo + y;
                        ColorStoneSettingPrice += Math.Round((Decimal.Round(x, 0)) * (Convert.ToDecimal(dtStoneSetting.Rows[i]["price"])), 2, MidpointRounding.AwayFromZero);
                    }
                }
            }
            stoneSettingPrice = ColorStoneSettingPrice + DiamondSettingPrice;


        }
        catch (Exception ee)
        {
            string er = ee.ToString();
            err = true;
        }
        return stoneSettingPrice;

    }

    public decimal Stone_PriceForSpecialPremium(string Radioval, string stoneType, string strColor, string strclarity, string carWt, string pid, string Length, string SemiMount, string Isenable, decimal EnternityRingSize)
    {

        decimal StonePrice = 0;
        decimal TotalStonePrice = 0;
        decimal MarkupPrice = 0;
        bool markups = false;
        decimal EffectWt = 0;
        decimal DiamondStonePrice = 0;
        decimal x = 0;
        decimal y = 0;

        try
        {
            string strPrice;

            DataTable dtStoneSetting = new DataTable();

            if (Isenable.Trim() == "ET")
            {

                dtStoneSetting = ojbProdsizeHelper.ReturnProductStoneDetailsNew1(pid, carWt.Trim(), SemiMount, EnternityRingSize);

            }
            else
            {
                dtStoneSetting = ojbProdsizeHelper.ReturnProductStoneDetailsNew(pid, carWt.Trim(), SemiMount);

            }


            if (Radioval.Trim() == "Diamond")
            {
                for (int k = 0; k < dtStoneSetting.Rows.Count; k++)
                {
                    if (dtStoneSetting.Rows[k]["StoneType"].ToString() == "1")
                    {
                        if (OrderLen != defaltlen)
                        {
                            x = ((Convert.ToDecimal(dtStoneSetting.Rows[k]["StoneQty"])) / defaltlen) * OrderLen;

                        }
                        else
                        {
                            x = Convert.ToDecimal(dtStoneSetting.Rows[k]["StoneQty"]);
                        }
                        y = Decimal.Round(x, 0);


                        StonePrice = (Decimal.Round(x, 0)) * (Convert.ToDecimal(dtStoneSetting.Rows[k]["caratweight"]));
                        if (EffectWt == 0)
                        {
                            EffectWt = (Decimal.Round(x, 0)) * (Convert.ToDecimal(dtStoneSetting.Rows[k]["caratweight"]));
                        }
                        else
                        {
                            EffectWt = EffectWt + ((Decimal.Round(x, 0)) * (Convert.ToDecimal(dtStoneSetting.Rows[k]["caratweight"])));
                        }
                        string diamondPrices = "SELECT price,specialpremium FROM Diamonds WHERE Clarity in(select ClarityID from Clarities where  Clarity='" + strclarity + "')";
                        diamondPrices += "and Shape='" + dtStoneSetting.Rows[k]["StoneShapeid"].ToString() + "' and  ((Weight1 <= " + dtStoneSetting.Rows[k]["caratweight"].ToString() + ") AND (Weight2 >=" + dtStoneSetting.Rows[k]["caratweight"].ToString() + ")) and color in(select colorid from colors where color='" + strColor + "')AND VendorID='" + dtStoneSetting.Rows[k]["vendorID"].ToString() + "' and MasterMerchantID is null";

                        DataTable dtDiamondPrice = objComFun.ExecuteQueryReturnDataTable(diamondPrices);
                        strPrice = dtDiamondPrice.Rows[0]["price"].ToString();
                        decimal decSpecPrem = Convert.ToDecimal(dtDiamondPrice.Rows[0]["specialpremium"]);
                        if (strPrice.ToString() == string.Empty)
                        {
                            strPrice = "1";
                        }
                        decimal price = Convert.ToDecimal(strPrice.ToString());
                        StonePrice = StonePrice * price;
                        if (decSpecPrem > 0)
                            StonePrice = StonePrice * decSpecPrem;
                        else if (Convert.ToDecimal(DtfixedCharge.Rows[0]["Premium_charge"]) > 0)
                            StonePrice = StonePrice * Convert.ToDecimal(DtfixedCharge.Rows[0]["Premium_charge"]);

                        StonePrice = Math.Round(StonePrice, 3, MidpointRounding.AwayFromZero);
                        DiamondStonePrice = DiamondStonePrice + StonePrice;
                        DiamondStonePrice = Math.Round(DiamondStonePrice, 2, MidpointRounding.AwayFromZero);
                    }

                }
            }


            for (int k = 0; k < dtStoneSetting.Rows.Count; k++)
            {
                if (dtStoneSetting.Rows[k]["StoneType"].ToString() != "1")
                {

                    if (y != defaltlen)
                    {
                        x = ((Convert.ToDecimal(dtStoneSetting.Rows[k]["StoneQty"])) / defaltlen) * OrderLen;

                    }
                    else
                    {
                        x = Convert.ToDecimal(dtStoneSetting.Rows[k]["StoneQty"]);
                    }
                    y = Decimal.Round(x, 0);

                    StonePrice = (Decimal.Round(x, 0)) * (Convert.ToDecimal(dtStoneSetting.Rows[k]["caratweight"]));
                    string colorstoneprice = "SELECT price FROM colorstones WHERE((Weight <= " + dtStoneSetting.Rows[k]["caratweight"].ToString() + ") AND (Weight1 >=" + dtStoneSetting.Rows[k]["caratweight"].ToString() + ")) AND shape='" + dtStoneSetting.Rows[k]["stoneshapeid"].ToString() + "' and colorStoneTypeid in(select colorStoneTypeID from tblColorStone_Type where ColorStoneType='" + stoneType + "')  AND VendorID='" + dtStoneSetting.Rows[k]["vendorID"].ToString() + "' ";
                    strPrice = objComFun.ExecuteQueryReturnSingleString(colorstoneprice);


                    if (strPrice.ToString() == string.Empty)
                    {
                        strPrice = "1";
                    }
                    decimal price = Convert.ToDecimal(strPrice.ToString());
                    StonePrice = StonePrice * price;
                    StonePrice = Math.Round(StonePrice, 3, MidpointRounding.AwayFromZero);
                    TotalStonePrice = TotalStonePrice + StonePrice;
                    TotalStonePrice = Math.Round(TotalStonePrice, 2, MidpointRounding.AwayFromZero);
                }
            }
            if (Convert.ToDecimal(DtfixedCharge.Rows[0]["Premium_charge"]) > 0)
                TotalStonePrice = (TotalStonePrice * Convert.ToDecimal(DtfixedCharge.Rows[0]["Premium_charge"])) + DiamondStonePrice;
            else
                TotalStonePrice = TotalStonePrice + DiamondStonePrice;

            string strMarkup = objComFun.ExecuteQueryReturnSingleString("SELECT MarkupProduct FROM Products WHERE productid='" + pid + "'");
            if (strMarkup.Trim() != string.Empty)
            {
                decimal marks = Convert.ToDecimal(strMarkup.Trim());
                marks = 100 + marks;
                ///
                MarkupPrice = TotalStonePrice * (marks / 100);
                MarkupPrice = Math.Round(MarkupPrice, 2, MidpointRounding.AwayFromZero);

                markups = true;
            }
            else
            {
                markups = false;

            }
        }
        catch (Exception ee)
        {
            string er = ee.ToString();
            err = true;
        }
        if (EffectWt == 0)
        {
            EffectiveWt = string.Empty;
        }
        else
        {
            EffectiveWt = (Decimal.Round(EffectWt, 2)).ToString();
        }
        if (markups == true)
        {
            return MarkupPrice;
        }
        else { return TotalStonePrice; }



    }
    
    public decimal Stone_Price(string Radioval, string stoneType, string strColor, string strclarity, string carWt, string pid, string Length, string SemiMount, string Isenable, decimal EnternityRingSize)
    {

        decimal StonePrice = 0;
        decimal TotalStonePrice = 0;
        decimal MarkupPrice = 0;
        bool markups = false;
        decimal EffectWt = 0;
        decimal DiamondStonePrice = 0;
        decimal x = 0;
        decimal y = 0;

        try
        {
            string strPrice;

            DataTable dtStoneSetting = new DataTable();

            if (Isenable.Trim() == "ET")
            {

                dtStoneSetting = ojbProdsizeHelper.ReturnProductStoneDetailsNew1(pid, carWt.Trim(), SemiMount, EnternityRingSize);

            }
            else
            {
                dtStoneSetting = ojbProdsizeHelper.ReturnProductStoneDetailsNew(pid, carWt.Trim(), SemiMount);

            }

            if (Radioval.Trim() == "Diamond")
            {
                for (int k = 0; k < dtStoneSetting.Rows.Count; k++)
                {
                    if (dtStoneSetting.Rows[k]["StoneType"].ToString() == "1")
                    {
                        if (OrderLen != defaltlen)
                        {
                            x = ((Convert.ToDecimal(dtStoneSetting.Rows[k]["StoneQty"])) / defaltlen) * OrderLen;

                        }
                        else
                        {
                            x = Convert.ToDecimal(dtStoneSetting.Rows[k]["StoneQty"]);
                        }
                        y = Decimal.Round(x, 0);


                        StonePrice = (Decimal.Round(x, 0)) * (Convert.ToDecimal(dtStoneSetting.Rows[k]["caratweight"]));
                        if (EffectWt == 0)
                        {
                            EffectWt = (Decimal.Round(x, 0)) * (Convert.ToDecimal(dtStoneSetting.Rows[k]["caratweight"]));
                        }
                        else
                        {
                            EffectWt = EffectWt + ((Decimal.Round(x, 0)) * (Convert.ToDecimal(dtStoneSetting.Rows[k]["caratweight"])));
                        }
                        string diamondPrices = "SELECT price,clarity,color FROM Diamonds WHERE Clarity in(select ClarityID from Clarities where  Clarity='" + strclarity + "')";
                        diamondPrices += "and Shape='" + dtStoneSetting.Rows[k]["StoneShapeid"].ToString() + "' and  ((Weight1 <= " + dtStoneSetting.Rows[k]["caratweight"].ToString() + ") AND (Weight2 >=" + dtStoneSetting.Rows[k]["caratweight"].ToString() + ")) and color in(select colorid from colors where color='" + strColor + "')AND VendorID='" + dtStoneSetting.Rows[k]["vendorID"].ToString() + "' and MasterMerchantID is null";

                        strPrice = objComFun.ExecuteQueryReturnSingleString(diamondPrices);
                        if (strPrice.ToString() == string.Empty)
                        {
                            strPrice = "1";
                        }
                        decimal price = Convert.ToDecimal(strPrice.ToString());
                        StonePrice = StonePrice * price;
                        StonePrice = Math.Round(StonePrice, 3, MidpointRounding.AwayFromZero);
                        DiamondStonePrice = DiamondStonePrice + StonePrice;
                        DiamondStonePrice = Math.Round(DiamondStonePrice, 2, MidpointRounding.AwayFromZero);
                    }

                }
            }


            for (int k = 0; k < dtStoneSetting.Rows.Count; k++)
            {
                if (dtStoneSetting.Rows[k]["StoneType"].ToString() != "1")
                {

                    if (y != defaltlen)
                    {
                        x = ((Convert.ToDecimal(dtStoneSetting.Rows[k]["StoneQty"])) / defaltlen) * OrderLen;

                    }
                    else
                    {
                        x = Convert.ToDecimal(dtStoneSetting.Rows[k]["StoneQty"]);
                    }
                    y = Decimal.Round(x, 0);

                    StonePrice = (Decimal.Round(x, 0)) * (Convert.ToDecimal(dtStoneSetting.Rows[k]["caratweight"]));
                    string colorstoneprice = "SELECT price FROM colorstones WHERE((Weight <= " + dtStoneSetting.Rows[k]["caratweight"].ToString() + ") AND (Weight1 >=" + dtStoneSetting.Rows[k]["caratweight"].ToString() + ")) AND shape='" + dtStoneSetting.Rows[k]["stoneshapeid"].ToString() + "' and colorStoneTypeid in(select colorStoneTypeID from tblColorStone_Type where ColorStoneType='" + stoneType + "')  AND VendorID='" + dtStoneSetting.Rows[k]["vendorID"].ToString() + "' ";
                    strPrice = objComFun.ExecuteQueryReturnSingleString(colorstoneprice);
                    if (strPrice.ToString() == string.Empty)
                    {
                        strPrice = "1";
                    }
                    decimal price = Convert.ToDecimal(strPrice.ToString());
                    StonePrice = StonePrice * price;
                    StonePrice = Math.Round(StonePrice, 3, MidpointRounding.AwayFromZero);
                    TotalStonePrice = TotalStonePrice + StonePrice;
                    TotalStonePrice = Math.Round(TotalStonePrice, 2, MidpointRounding.AwayFromZero);
                }
            }

            TotalStonePrice = TotalStonePrice + DiamondStonePrice;

            string strMarkup = objComFun.ExecuteQueryReturnSingleString("SELECT MarkupProduct FROM Products WHERE productid='" + pid + "'");
            if (strMarkup.Trim() != string.Empty)
            {
                decimal marks = Convert.ToDecimal(strMarkup.Trim());
                marks = 100 + marks;
                ///
                MarkupPrice = TotalStonePrice * (marks / 100);
                MarkupPrice = Math.Round(MarkupPrice, 2, MidpointRounding.AwayFromZero);

                markups = true;
            }
            else
            {
                markups = false;

            }
        }
        catch (Exception ee)
        {
            string er = ee.ToString();
            err = true;
        }
        if (EffectWt == 0)
        {
            EffectiveWt = string.Empty;
        }
        else
        {
            EffectiveWt = (Decimal.Round(EffectWt, 2)).ToString();
        }
        if (markups == true)
        {
            return MarkupPrice;
        }
        else { return TotalStonePrice; }



    }

    public decimal CenterDiamondStoneSetting_Price(string PID, string Radioval, string carWt, string SemiMount)
    {

        decimal x = 0;
        decimal y = 0;
        decimal CenterstoneSettingPrice = 0;
        decimal CenterDiamondSettingPrice = 0;
        decimal ColorStoneSettingPrice = 0;
        decimal CenterDiamondStoneNo = 0;
        decimal CenterDiamondStoneNoForNew = 0;


        try
        {

            DataTable dtStoneSetting = ojbProdsizeHelper.ReturnProductStoneDetailsNew(PID, carWt.Trim(), SemiMount);

            if (dtStoneSetting.Rows.Count != 0)
                if (dtStoneSetting.Rows.Count != 0)
                {
                    for (int i = 0; i < dtStoneSetting.Rows.Count; i++)
                    {
                        if ((dtStoneSetting.Rows[i]["stoneType"].ToString() == "1"))
                        {
                            if (Radioval.Trim() != "NoDiamond")
                            {
                                if (OrderLen != defaltlen)
                                {
                                    x = ((Convert.ToDecimal(dtStoneSetting.Rows[i]["StoneQty"])) / defaltlen) * OrderLen;
                                }
                                else
                                {
                                    x = Convert.ToDecimal(dtStoneSetting.Rows[i]["StoneQty"]);
                                }
                                y = Decimal.Round(x, 0);

                                if ((dtStoneSetting.Rows[i]["CenterStone"].ToString() == "1"))
                                {
                                    CenterDiamondStoneNo = CenterDiamondStoneNo + y;

                                    CenterDiamondSettingPrice += Math.Round((Decimal.Round(x, 0)) * (Convert.ToDecimal(dtStoneSetting.Rows[i]["price"])), 2, MidpointRounding.AwayFromZero);
                                }
                            }
                            else
                            {
                                if (OrderLen != defaltlen)
                                {
                                    x = ((Convert.ToDecimal(dtStoneSetting.Rows[i]["StoneQty"])) / defaltlen) * OrderLen;
                                }
                                else
                                {
                                    x = Convert.ToDecimal(dtStoneSetting.Rows[i]["StoneQty"]);
                                }
                                y = Decimal.Round(x, 0);

                                CenterDiamondStoneNoForNew = CenterDiamondStoneNoForNew + y;
                            }
                        }
                    }
                    for (int i = 0; i < dtStoneSetting.Rows.Count; i++)
                    {
                        if ((dtStoneSetting.Rows[i]["stoneType"].ToString() != "1"))
                        {
                            if (y != defaltlen)
                            {
                                x = ((Convert.ToDecimal(dtStoneSetting.Rows[i]["StoneQty"])) / defaltlen) * OrderLen;
                            }
                            else
                            {
                                x = Convert.ToDecimal(dtStoneSetting.Rows[i]["StoneQty"]);
                            }
                            y = Decimal.Round(x, 0);

                            CenterDiamondStoneNo = CenterDiamondStoneNo + y;
                            ColorStoneSettingPrice += Math.Round((Decimal.Round(x, 0)) * (Convert.ToDecimal(dtStoneSetting.Rows[i]["price"])), 2, MidpointRounding.AwayFromZero);
                        }
                    }
                }
            CenterstoneSettingPrice = CenterDiamondSettingPrice;
        }
        catch (Exception ee)
        {
            string er = ee.ToString();
            err = true;
        }
        return CenterstoneSettingPrice;

    }

    public decimal CenterDiamondStone_PriceForSpecialPremium(string PID, string Radioval, string stoneType, string strColor, string strclarity, string carWt, string SemiMount)
    {

        decimal CenterDiamond_StonePrice = 0;
        decimal TotalCenterDiamondStonePrice = 0;
        decimal CenterDiamondMarkupPrice = 0;
        bool CenterDiamondmarkups = false;
        decimal CenterDiamondEffectWt = 0;
        decimal CenterDiamondEffectWtc = 0;
        decimal Diamond_StonePrice = 0;
        decimal x = 0;
        decimal y = 0;


        try
        {
            string strPrice;
            DataTable dtStoneSetting = ojbProdsizeHelper.ReturnProductStoneDetailsNew(PID, carWt.Trim(), SemiMount);

            if (Radioval.Trim() == "Diamond")
            {
                for (int k = 0; k < dtStoneSetting.Rows.Count; k++)
                {
                    if (dtStoneSetting.Rows[k]["StoneType"].ToString() == "1")
                    {
                        if (OrderLen != defaltlen)
                        {
                            x = ((Convert.ToDecimal(dtStoneSetting.Rows[k]["StoneQty"])) / defaltlen) * OrderLen;
                        }
                        else
                        {
                            if (dtStoneSetting.Rows[k]["CenterStone"].ToString() == "1")
                            {
                                x = Convert.ToDecimal(dtStoneSetting.Rows[k]["StoneQty"]);
                            }
                            else
                            {

                                x = 0;
                            }
                        }
                        y = Decimal.Round(x, 0);


                        CenterDiamond_StonePrice = (Decimal.Round(x, 0)) * (Convert.ToDecimal(dtStoneSetting.Rows[k]["caratweight"]));
                        if (CenterDiamondEffectWt == 0)
                        {
                            CenterDiamondEffectWt = (Decimal.Round(x, 0)) * (Convert.ToDecimal(dtStoneSetting.Rows[k]["caratweight"]));
                        }
                        else
                        {
                            CenterDiamondEffectWt = CenterDiamondEffectWt + ((Decimal.Round(x, 0)) * (Convert.ToDecimal(dtStoneSetting.Rows[k]["caratweight"])));
                        }

                        DataTable dtDiamondPrice = objComFun.GetdiamondstudearringsSPForSpecialPremium(strclarity, dtStoneSetting.Rows[k]["StoneShapeid"].ToString(), dtStoneSetting.Rows[k]["caratweight"].ToString(), strColor, dtStoneSetting.Rows[k]["vendorID"].ToString());


                        strPrice = dtDiamondPrice.Rows[0]["price"].ToString();
                        decimal decSpecPrem = Convert.ToDecimal(dtDiamondPrice.Rows[0]["specialpremium"]);
                        if (strPrice.ToString() == string.Empty)
                        {
                            strPrice = "1";
                        }
                        decimal price = Convert.ToDecimal(strPrice.ToString());
                        CenterDiamond_StonePrice = CenterDiamond_StonePrice * price;
                        if (decSpecPrem > 0)
                            CenterDiamond_StonePrice = CenterDiamond_StonePrice * decSpecPrem;
                        else if (Convert.ToDecimal(DtfixedCharge.Rows[0]["Premium_charge"]) > 0)
                            CenterDiamond_StonePrice = CenterDiamond_StonePrice * Convert.ToDecimal(DtfixedCharge.Rows[0]["Premium_charge"]);

                        CenterDiamond_StonePrice = Math.Round(CenterDiamond_StonePrice, 3, MidpointRounding.AwayFromZero);
                        Diamond_StonePrice = Diamond_StonePrice + CenterDiamond_StonePrice;
                        Diamond_StonePrice = Math.Round(Diamond_StonePrice, 2, MidpointRounding.AwayFromZero);
                    }

                }
            }
            for (int k = 0; k < dtStoneSetting.Rows.Count; k++)
            {
                if (dtStoneSetting.Rows[k]["StoneType"].ToString() != "1")
                {

                    if (y != defaltlen)
                    {
                        x = ((Convert.ToDecimal(dtStoneSetting.Rows[k]["StoneQty"])) / defaltlen) * OrderLen;
                    }
                    else
                    {
                        x = Convert.ToDecimal(dtStoneSetting.Rows[k]["StoneQty"]);
                    }
                    y = Decimal.Round(x, 0);

                    CenterDiamond_StonePrice = (Decimal.Round(x, 0)) * (Convert.ToDecimal(dtStoneSetting.Rows[k]["caratweight"]));

                    string colorstoneprice = "SELECT price FROM colorstones WHERE((Weight <= " + dtStoneSetting.Rows[k]["caratweight"].ToString() + ") AND (Weight1 >=" + dtStoneSetting.Rows[k]["caratweight"].ToString() + ")) AND shape='" + dtStoneSetting.Rows[k]["stoneshapeid"].ToString() + "' and colorStoneTypeid in(select colorStoneTypeID from tblColorStone_Type where ColorStoneType='" + stoneType + "')  AND VendorID='" + dtStoneSetting.Rows[k]["vendorID"].ToString() + "' ";
                    strPrice = objComFun.ExecuteQueryReturnSingleString(colorstoneprice);



                    if (CenterDiamondEffectWtc == 0)
                    {
                        CenterDiamondEffectWtc = (Decimal.Round(x, 0)) * (Convert.ToDecimal(dtStoneSetting.Rows[k]["caratweight"]));
                    }
                    else
                    {
                        CenterDiamondEffectWtc = CenterDiamondEffectWt + ((Decimal.Round(x, 0)) * (Convert.ToDecimal(dtStoneSetting.Rows[k]["caratweight"])));
                    }
                    if (strPrice.ToString() == string.Empty)
                    {
                        strPrice = "1";
                    }
                    if (strPrice.ToString() == string.Empty)
                    {
                        strPrice = "1";
                    }
                    decimal price = Convert.ToDecimal(strPrice.ToString());
                    CenterDiamond_StonePrice = CenterDiamond_StonePrice * price;
                    CenterDiamond_StonePrice = Math.Round(CenterDiamond_StonePrice, 3, MidpointRounding.AwayFromZero);
                    TotalCenterDiamondStonePrice = TotalCenterDiamondStonePrice + CenterDiamond_StonePrice;
                    TotalCenterDiamondStonePrice = Math.Round(TotalCenterDiamondStonePrice, 2, MidpointRounding.AwayFromZero);
                }
            }
            if (Convert.ToDecimal(DtfixedCharge.Rows[0]["Premium_charge"]) > 0)
                TotalCenterDiamondStonePrice = (TotalCenterDiamondStonePrice * Convert.ToDecimal(DtfixedCharge.Rows[0]["Premium_charge"])) + Diamond_StonePrice;
            else
                TotalCenterDiamondStonePrice = TotalCenterDiamondStonePrice + Diamond_StonePrice;

        }
        catch (Exception ee)
        {
            string er = ee.ToString();
            err = true;
        }
        if (CenterDiamondEffectWt == 0)
        {
            EffectiveWt = string.Empty;
        }
        else
        {
            EffectiveWt = (Decimal.Round(CenterDiamondEffectWt, 2)).ToString();
        }
        if (CenterDiamondmarkups == true)
        {
            return CenterDiamondMarkupPrice;
        }
        else { return TotalCenterDiamondStonePrice; }



    }

    public decimal SideStoneSetting_Price(string PID, string Radioval, string carWt, string SemiMount)
    {

        decimal x = 0;
        decimal y = 0;
        decimal CenterstoneSettingPrice = 0;
        decimal CenterDiamondSettingPrice = 0;
        decimal ColorStoneSettingPrice = 0;
        decimal CenterDiamondStoneNo = 0;
        decimal CenterDiamondStoneNoForNew = 0;


        try
        {
            DataTable dtStoneSetting = ojbProdsizeHelper.ReturnProductStoneDetailsNew(PID, carWt.Trim(), SemiMount);

            if (dtStoneSetting.Rows.Count != 0)
                if (dtStoneSetting.Rows.Count != 0)
                {
                    for (int i = 0; i < dtStoneSetting.Rows.Count; i++)
                    {
                        if ((dtStoneSetting.Rows[i]["stoneType"].ToString() == "1"))
                        {
                            if (Radioval.Trim() != "NoDiamond")
                            {
                                if (OrderLen != defaltlen)
                                {
                                    x = ((Convert.ToDecimal(dtStoneSetting.Rows[i]["StoneQty"])) / defaltlen) * OrderLen;
                                }
                                else
                                {
                                    x = Convert.ToDecimal(dtStoneSetting.Rows[i]["StoneQty"]);
                                }
                                y = Decimal.Round(x, 0);

                                if ((dtStoneSetting.Rows[i]["CenterStone"].ToString() == "0"))
                                {
                                    CenterDiamondStoneNo = CenterDiamondStoneNo + y;

                                    CenterDiamondSettingPrice += Math.Round((Decimal.Round(x, 0)) * (Convert.ToDecimal(dtStoneSetting.Rows[i]["price"])), 2, MidpointRounding.AwayFromZero);
                                }
                            }
                            else
                            {
                                if (OrderLen != defaltlen)
                                {
                                    x = ((Convert.ToDecimal(dtStoneSetting.Rows[i]["StoneQty"])) / defaltlen) * OrderLen;
                                }
                                else
                                {
                                    x = Convert.ToDecimal(dtStoneSetting.Rows[i]["StoneQty"]);
                                }
                                y = Decimal.Round(x, 0);

                                CenterDiamondStoneNoForNew = CenterDiamondStoneNoForNew + y;
                            }
                        }
                    }
                    for (int i = 0; i < dtStoneSetting.Rows.Count; i++)
                    {
                        if ((dtStoneSetting.Rows[i]["stoneType"].ToString() != "1"))
                        {
                            if (y != defaltlen)
                            {
                                x = ((Convert.ToDecimal(dtStoneSetting.Rows[i]["StoneQty"])) / defaltlen) * OrderLen;
                            }
                            else
                            {
                                x = Convert.ToDecimal(dtStoneSetting.Rows[i]["StoneQty"]);
                            }
                            y = Decimal.Round(x, 0);

                            CenterDiamondStoneNo = CenterDiamondStoneNo + y;
                            ColorStoneSettingPrice += Math.Round((Decimal.Round(x, 0)) * (Convert.ToDecimal(dtStoneSetting.Rows[i]["price"])), 2, MidpointRounding.AwayFromZero);
                        }
                    }
                }
            CenterstoneSettingPrice = CenterDiamondSettingPrice;
        }
        catch (Exception ee)
        {
            string er = ee.ToString();
            err = true;
        }
        return CenterstoneSettingPrice;

    }

    public decimal SideStone_PriceForSpecialPremium(string PID, string Radioval, string stoneType, string strColor, string strclarity, string carWt, string SemiMount)
    {

        decimal CenterDiamond_StonePrice = 0;
        decimal CenterDiamondMarkupPrice = 0;
        bool CenterDiamondmarkups = false;
        decimal CenterDiamondEffectWt = 0;
        decimal Diamond_StonePrice = 0;
        decimal x = 0;
        decimal y = 0;

        try
        {
            string strPrice;
            DataTable dtStoneSetting = ojbProdsizeHelper.ReturnProductStoneDetailsNew(PID, carWt.Trim(), SemiMount);

            if (Radioval.Trim() == "Diamond")
            {
                for (int k = 0; k < dtStoneSetting.Rows.Count; k++)
                {
                    if (dtStoneSetting.Rows[k]["StoneType"].ToString() == "1")
                    {
                        if (OrderLen != defaltlen)
                        {
                            x = ((Convert.ToDecimal(dtStoneSetting.Rows[k]["StoneQty"])) / defaltlen) * OrderLen;
                        }
                        else
                        {
                            if (dtStoneSetting.Rows[k]["CenterStone"].ToString() == "0")
                            {
                                x = Convert.ToDecimal(dtStoneSetting.Rows[k]["StoneQty"]);
                            }
                            else
                            {

                                x = 0;
                            }
                        }
                        y = Decimal.Round(x, 0);

                        CenterDiamond_StonePrice = (Decimal.Round(x, 0)) * (Convert.ToDecimal(dtStoneSetting.Rows[k]["caratweight"]));
                        if (CenterDiamondEffectWt == 0)
                        {
                            CenterDiamondEffectWt = (Decimal.Round(x, 0)) * (Convert.ToDecimal(dtStoneSetting.Rows[k]["caratweight"]));
                        }
                        else
                        {
                            CenterDiamondEffectWt = CenterDiamondEffectWt + ((Decimal.Round(x, 0)) * (Convert.ToDecimal(dtStoneSetting.Rows[k]["caratweight"])));
                        }

                        DataTable dtDiamondPrice = objComFun.GetdiamondstudearringsSPForSpecialPremium(strclarity, dtStoneSetting.Rows[k]["StoneShapeid"].ToString(), dtStoneSetting.Rows[k]["caratweight"].ToString(), strColor, dtStoneSetting.Rows[k]["vendorID"].ToString());


                        strPrice = dtDiamondPrice.Rows[0]["price"].ToString();
                        decimal price = Convert.ToDecimal(strPrice.ToString());
                        CenterDiamond_StonePrice = CenterDiamond_StonePrice * price;
                        CenterDiamond_StonePrice = Math.Round(CenterDiamond_StonePrice, 3, MidpointRounding.AwayFromZero);
                        Diamond_StonePrice = Diamond_StonePrice + CenterDiamond_StonePrice;
                        Diamond_StonePrice = Math.Round(Diamond_StonePrice, 2, MidpointRounding.AwayFromZero);
                    }

                }
            }



        }
        catch (Exception ee)
        {
            string er = ee.ToString();
            err = true;
        }
        if (CenterDiamondEffectWt == 0)
        {
            EffectiveWt = string.Empty;
        }
        else
        {
            EffectiveWt = (Decimal.Round(CenterDiamondEffectWt, 2)).ToString();
        }
        if (CenterDiamondmarkups == true)
        {
            return CenterDiamondMarkupPrice;
        }
        else { return Diamond_StonePrice; }



    }


    #endregion //--------------- Get Product Price ----------------------//    

    #region  //--------------- Charm Price & Certificate Price ----------------------//
    [WebMethod(Description = "Get Charm Price")]
    public void GetCharmPrice(string MetalValue)
    {
        string CharmPrice = string.Empty;

        DataTable dtcharm = new DataTable("CharmPrice");
        dtcharm.Columns.Add("Price");
        DataRow dr = dtcharm.NewRow();       
       
        try
        {
            if(MetalValue=="14k")
            {
                dr["Price"] = "50";
                dtcharm.Rows.Add(dr);               
            }
            else if (MetalValue == "18k")
            {
                dr["Price"] = "65";
                dtcharm.Rows.Add(dr);               

            }
            else if (MetalValue == "950")
            {
                dr["Price"] = "100";
                dtcharm.Rows.Add(dr);                
            }
            if (dtcharm.Rows.Count > 0)
            {
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.Write(DataTableToJSONWithJavaScriptSerializer(dtcharm));
            }
            else
            {
                Context.Response.Write("False");
            }

        }
        catch (Exception ex)
        {

            Context.Response.Write("False");
        }

        

    }

    [WebMethod(Description = "Get Certificate Price")]
    public void GetCertificatePrice(string CaretWeight)
    {

        string stoneType = string.Empty; ;
        DataSet dsCPrice = new DataSet();
        DataTable table = new DataTable();

        try
        {

            string CPrice = objComFun.GetProductCertiPrice();

            double CertiPrice = Convert.ToDouble(CPrice);

            double CWeight = Convert.ToDouble(CaretWeight);

            string ProductCertiPrice = Convert.ToString(CertiPrice * CWeight);

            table.Columns.Add("CertificatePrice", typeof(string));
            table.Rows.Add(ProductCertiPrice);
            dsCPrice.Tables.Add(table);

            if (dsCPrice.Tables[0].Rows.Count > 0)
            {
                DataTable dt = dsCPrice.Tables[0];
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.Write(DataTableToJSONWithJavaScriptSerializer(dt));
            }
            else
            {
                Context.Response.Write("False");
            }
        }

        catch (Exception ex)
        {
            Context.Response.Write("False");


        }

    }

    #endregion //--------------- Product page ----------------------//

    #region  //--------------- Animation360 & Animation Visualize----------------------//
    [WebMethod(Description = "360 Rotation for White")]
    public void GetAnimation360White(String ProductID)
    {
        //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        String result = "";
        string[] animfiles;
        DataTable filesdt = new DataTable("AnimationFiles");
        filesdt.Columns.Add("WhiteImages");
        DataRow filerow;
        //try
        //{
        String query = "Select [3DAnimationLink] from products where ProductID='" + ProductID.Trim() + "'";
        result = objComFun.ExecuteQueryReturnSingleString(query);
        if (result != "" && result != null)
        {
            result = result.Replace("http://www.anjolee.com/advertising/Animations/", "");
            int idx = result.IndexOf("/");
            result = result.Remove(idx);
            string _strfolder = "d:\\www\\www.anjolee.com\\advertising\\Animations"; //live
            DirectoryInfo di = new DirectoryInfo(_strfolder);
            //di.
            DirectoryInfo[] directories = di.GetDirectories(result, SearchOption.AllDirectories);
            FileInfo[] files = directories[0].GetFiles();
            Int32 filecount = files.Length;

            animfiles = new string[filecount];
            int i = 0;
            foreach (FileInfo fi in files)
            {
                if (fi.FullName.Contains(".JPG") || fi.FullName.Contains(".jpg"))
                {
                    animfiles[i] = fi.FullName;
                    animfiles[i] = animfiles[i].Replace("d:\\www\\www.anjolee.com\\", "http://www.anjolee.com/");
                    animfiles[i] = animfiles[i].Replace("\\", "/");
                    filerow = filesdt.NewRow();
                    filerow["WhiteImages"] = animfiles[i];
                    filesdt.Rows.Add(filerow);
                    i = i + 1;
                }
            }
            if (filesdt.Rows.Count > 0)
            {
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.Write(DataTableToJSONWithJavaScriptSerializer(filesdt));

            }
            //return filesdt;


        }
        else
        {


            filesdt.Columns.Add("AnimationFiles");
            DataRow dr = filesdt.NewRow();
            dr["WhiteImages"] = "false";
            filesdt.Rows.Add(dr);
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Write(DataTableToJSONWithJavaScriptSerializer(filesdt));
            //return filesdt;
        }

    }

    [WebMethod(Description = "360 Rotation for Yellow")]
    public void GetAnimation360Yellow(String ProductID)
    {
        //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        String result = "";
        string[] animfiles;
        DataTable filesdt = new DataTable("AnimationFiles");
        filesdt.Columns.Add("YellowImages");
        DataRow filerow;
        //try
        //{
        String query = "Select [3DAnimationLinkAdditional] from products where ProductID='" + ProductID.Trim() + "'";
        result = objComFun.ExecuteQueryReturnSingleString(query);
        if (result != "" && result != null)
        {
            result = result.Replace("http://www.anjolee.com/advertising/AnimationsAdditional/", "");
            int idx = result.IndexOf("/");
            result = result.Remove(idx);
            string _strfolder = "d:\\www\\www.anjolee.com\\advertising\\AnimationsAdditional"; //live
            DirectoryInfo di = new DirectoryInfo(_strfolder);

            DirectoryInfo[] directories = di.GetDirectories(result, SearchOption.AllDirectories);
            FileInfo[] files = directories[0].GetFiles();
            Int32 filecount = files.Length;

            animfiles = new string[filecount];

            int i = 0;
            foreach (FileInfo fi in files)
            {
                if (fi.FullName.Contains(".JPG") || fi.FullName.Contains(".jpg"))
                {
                    animfiles[i] = fi.FullName;
                    animfiles[i] = animfiles[i].Replace("d:\\www\\www.anjolee.com\\", "http://www.anjolee.com/"); //live
                    animfiles[i] = animfiles[i].Replace("\\", "/");
                    filerow = filesdt.NewRow();
                    filerow["YellowImages"] = animfiles[i];
                    filesdt.Rows.Add(filerow);
                    i = i + 1;
                }
            }

            if (filesdt.Rows.Count > 0)
            {
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.Write(DataTableToJSONWithJavaScriptSerializer(filesdt));

            }
            //return filesdt;


        }
        else
        {

            filesdt.Columns.Add("AnimationFiles");
            DataRow dr = filesdt.NewRow();
            dr["YellowImages"] = "false";
            filesdt.Rows.Add(dr);
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Write(DataTableToJSONWithJavaScriptSerializer(filesdt));
            //return filesdt;

        }

    }

    [WebMethod(Description = "Visualize for White")]
    public void GetAnimation3DVisualizeWhite(String ProductID)
    {
        //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        String result = "";
        string[] animfiles;
        DataTable filesdt = new DataTable("DAnimationFiles");
        filesdt.Columns.Add("DWhiteImages");
        DataRow filerow;
        //try
        //{
        String query = "Select [BodyRenderingWhite] from products where ProductID='" + ProductID.Trim() + "'";
        result = objComFun.ExecuteQueryReturnSingleString(query);
        if (result != "" && result != null)
        {
            result = result.Replace("http://www.anjolee.com/advertising/BodyRenderWhite/", "");
            int idx = result.IndexOf("/");
            result = result.Remove(idx);
            string _strfolder = "d:\\www\\www.anjolee.com\\advertising\\BodyRenderWhite"; //live
            DirectoryInfo di = new DirectoryInfo(_strfolder);
            //di.
            DirectoryInfo[] directories = di.GetDirectories(result, SearchOption.AllDirectories);
            FileInfo[] files = directories[0].GetFiles();
            Int32 filecount = files.Length;

            animfiles = new string[filecount];
            int i = 0;
            foreach (FileInfo fi in files)
            {
                if (fi.FullName.Contains(".JPG") || fi.FullName.Contains(".jpg"))
                {
                    animfiles[i] = fi.FullName;
                    animfiles[i] = animfiles[i].Replace("d:\\www\\www.anjolee.com\\", "http://www.anjolee.com/"); //live
                    animfiles[i] = animfiles[i].Replace("\\", "/");
                    filerow = filesdt.NewRow();
                    filerow["DWhiteImages"] = animfiles[i];
                    filesdt.Rows.Add(filerow);
                    i = i + 1;
                }
            }

            if (filesdt.Rows.Count > 0)
            {
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.Write(DataTableToJSONWithJavaScriptSerializer(filesdt));

            }
            //return filesdt;


        }
        else
        {
            filesdt.Columns.Add("DAnimationFiles");
            DataRow dr = filesdt.NewRow();
            dr["DWhiteImages"] = "false";
            filesdt.Rows.Add(dr);
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Write(DataTableToJSONWithJavaScriptSerializer(filesdt));
            //return filesdt;
        }
    }

    [WebMethod(Description = "Visualize for Yellow")]
    public void GetAnimation3DVisualizeYellow(String ProductID)
    {
        //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        String result = "";
        string[] animfiles;
        DataTable filesdt = new DataTable("DAnimationFiles");
        filesdt.Columns.Add("DYellowImages");
        DataRow filerow;

        String query = "Select [BodyRenderingYellow] from products where ProductID='" + ProductID.Trim() + "'";
        result = objComFun.ExecuteQueryReturnSingleString(query);
        if (result != "" && result != null)
        {
            result = result.Replace("http://www.anjolee.com/advertising/BodyRenderYellow/", "");
            int idx = result.IndexOf("/");
            result = result.Remove(idx);
            string _strfolder = "d:\\www\\www.anjolee.com\\advertising\\BodyRenderYellow"; //live
            DirectoryInfo di = new DirectoryInfo(_strfolder);
            DirectoryInfo[] directories = di.GetDirectories(result, SearchOption.AllDirectories);
            FileInfo[] files = directories[0].GetFiles();
            Int32 filecount = files.Length;

            animfiles = new string[filecount];
            //animlinks = new string[filecount];
            int i = 0;
            foreach (FileInfo fi in files)
            {
                if (fi.FullName.Contains(".JPG") || fi.FullName.Contains(".jpg"))
                {
                    animfiles[i] = fi.FullName;
                    animfiles[i] = animfiles[i].Replace("d:\\www\\www.anjolee.com\\", "http://www.anjolee.com/"); //live
                    animfiles[i] = animfiles[i].Replace("\\", "/");
                    filerow = filesdt.NewRow();
                    filerow["DYellowImages"] = animfiles[i];
                    filesdt.Rows.Add(filerow);
                    i = i + 1;
                }
            }

            if (filesdt.Rows.Count > 0)
            {
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.Write(DataTableToJSONWithJavaScriptSerializer(filesdt));

            }
            //return filesdt;

        }
        else
        {

            filesdt.Columns.Add("3DAnimationFiles");
            DataRow dr = filesdt.NewRow();
            dr["DYellowImages"] = "false";
            filesdt.Rows.Add(dr);
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Write(DataTableToJSONWithJavaScriptSerializer(filesdt));
            //return filesdt;
        }


    }

    #endregion //--------------- Animation360 & Animation Visualize----------------------//

    #region  //--------------- Center Diamond Price for Certified Diamonds and Anjolee Diamonds----------------------//

    [WebMethod(Description = "LabCertified Diamond data")]
    public void LabCertifiedDiamondSearchEngine(string ProductID, string Weight, string Color, string Clarity, string Cut, string Polish, string Symmetry, string Fluorscence, string MinPrice, string MaxPrice, string SortbyColumns, string Orderby, string PageIndex)
    {

        try
        {
            DataSet ds = objComFun.LabCertifiedDiamondSearchEngine(ProductID, Weight, Color, Clarity, Cut, Polish, Symmetry, Fluorscence, MinPrice, MaxPrice, SortbyColumns, Orderby, PageIndex);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.Write(DataTableToJSONWithJavaScriptSerializer(dt));
            }
            else
            {
                Context.Response.Write("False");
            }
        }
        catch (Exception Ex)
        {
            string str = Ex.ToString();
            Context.Response.Write("False");
        }
    }


    [WebMethod(Description = "Search Rapnet Studs Earring Data Paging Wise.")]
    public void LabCertifiedDiamondSearchEngine_StudsEarring(string ProductID, string ProductSizeID, string Color, string Clarity, string Cut, string Polish, string Symmetry, string Fluorscence, string PageIndex)
    {

        try
        {
            string CenterDiamondWeight = string.Empty;
            string CheckStudsEarringsType = objComFun.CheckStudsEarringsTypes(ProductID, ProductSizeID);


            if (CheckStudsEarringsType == "1")
            {


                CenterDiamondWeight = objComFun.GetStudsEarringsCartWeightTYPE1(ProductID, ProductSizeID);


            }
            else if (CheckStudsEarringsType == "2")
            {

                CenterDiamondWeight = objComFun.GetStudsEarringsCartWeightTYPE2(ProductID, ProductSizeID);

            }
            else if (CheckStudsEarringsType == "0")
            {

                CenterDiamondWeight = objComFun.GetStudsEarringsCartWeightTYPE2(ProductID, ProductSizeID);
            }

            CenterDiamondWeight = Convert.ToDecimal(CenterDiamondWeight).ToString("0.00");



            DataSet ds = objComFun.GetRapNetDataShapesEarringstuds(ProductID.Trim(), CenterDiamondWeight.Trim(), Color.Trim(), Clarity.Trim(), Cut.Trim(), Polish.Trim(), Symmetry.Trim(), Fluorscence.Trim(), PageIndex);
           
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable dtearring = ds.Tables[0];
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.Write(DataTableToJSONWithJavaScriptSerializer(dtearring));
            }
            else
            {
                Context.Response.Write("False");
            }
        }
        catch (Exception Ex)
        {
            Context.Response.Write("False");


        }


    }


    [WebMethod(Description = "Get Anjolee Center Diamond Price")]
    public void GetAnjoleeCenterDiamondPrice(string PID, string Radioval, string carWt, string strColor, string strclarity, string length, string SemiMount, string Rapnetstatus)
    {
        string stoneType = string.Empty;        
        DataTable table = new DataTable();
        decimal stonePrice = 0;
        string Isenable = string.Empty;
        string ISSize = string.Empty;
        string status = string.Empty;
        try
        {
            if (strColor == "HI" && strclarity == "SI-3")
            {

                strclarity = "SI-2";
            }


            if (Rapnetstatus == "1")
            {
                int EternityStatus = objComFun.CheckEternityStatus(PID);
                string query = "SELECT ISEnable,ISSize FROM ProductsGroups WHERE ProductsGroupID IN(SELECT ProductsProductsGroupID FROM ProductsProductsGroups WHERE ProductID='" + PID + "')";
                DataSet ds = new DataSet();
                ds = objComFun.ExecuteQueryReturnDataSet(query);
                if (ds.Tables[0].Rows.Count != 0)
                {
                    Isenable = ds.Tables[0].Rows[0]["ISEnable"].ToString();
                    ISSize = ds.Tables[0].Rows[0]["ISSize"].ToString();
                }
                if (EternityStatus == 1)
                {
                    Isenable = "ET";
                }
                else if ((Isenable == "True") && (ISSize == "False"))
                {
                    Isenable = "T";
                }
                else if ((Isenable == "True") && (ISSize == "True"))
                {
                    Isenable = "T";
                }
                else
                {
                    Isenable = "F";
                }

                decimal EnternityRingSize = OrderLen;
                if (GetPriceCalculationOption(Radioval, stoneType, strColor, strclarity, carWt, PID, SemiMount))
                {
                    stonePrice = Decimal.Round(Stone_PriceForSpecialPremium(Radioval, stoneType, strColor, strclarity, carWt, PID, length, SemiMount, Isenable, EnternityRingSize), 2, MidpointRounding.AwayFromZero);
                }
                else
                {
                    stonePrice = Decimal.Round(Stone_Price(Radioval, stoneType, strColor, strclarity, carWt, PID, length, SemiMount, Isenable, EnternityRingSize), 2, MidpointRounding.AwayFromZero);
                }

                status = stonePrice.ToString();
            }
            else
            {
                DtfixedCharge = objComFun.ExecuteQueryReturnDataTableFixedCharges(PID.Trim());

                decimal CenterDiamondStoneSettingPrice = Math.Round(CenterDiamondStoneSetting_Price(PID.Trim(), Radioval.Trim(), carWt.Trim(), SemiMount), 2, MidpointRounding.AwayFromZero);
                decimal CenterDiamondstonePrice = Decimal.Round(CenterDiamondStone_PriceForSpecialPremium(PID.Trim(), Radioval.Trim(), stoneType.Trim(), strColor.Trim(), strclarity.Trim(), carWt.Trim(), SemiMount), 2, MidpointRounding.AwayFromZero);

                if (Convert.ToDecimal(DtfixedCharge.Rows[0]["Premium_charge"]) > 0)
                {
                    CenterDiamondStoneSettingPrice = CenterDiamondStoneSettingPrice * Convert.ToDecimal(DtfixedCharge.Rows[0]["Premium_charge"]);
                }
                decimal CenterDiamonds_Price = CenterDiamondStoneSettingPrice + CenterDiamondstonePrice;
                CenterDiamonds_Price = Decimal.Round(Convert.ToDecimal(CenterDiamonds_Price), 2, MidpointRounding.AwayFromZero);

                status = CenterDiamonds_Price.ToString();
            }
            table.Columns.Add("CenterDiamondsPrice", typeof(string));
            table.Rows.Add(status);
            if (table.Rows.Count > 0)
            {               
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.Write(DataTableToJSONWithJavaScriptSerializer(table));
            }
            else
            {
                Context.Response.Write("False");
            }


        }
        catch (Exception ex)
        {
            string str = ex.ToString();
            Context.Response.Write("False");


        }



    }


    #endregion //---Center Diamond Price for Certified Diamonds and Anjolee Diamonds -----//

    #region  //--------------- Shopping Cart ----------------------//


    [WebMethod]
    public void GetStates()
    {
        DataSet dsstates = new DataSet();

        String str = "select * from Country_States order by stateName";
        dsstates = objComFun.ExecuteQueryReturnDataSet(str);
        if (dsstates.Tables[0].Rows.Count > 0)
        {
            DataTable dt = dsstates.Tables[0];
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Write(DataTableToJSONWithJavaScriptSerializer(dt));
        }
        else
        {
            Context.Response.Write("False");
        }
        
    }

    [WebMethod]
    public void CalculateTax(String OrderTotal, String ZipCode)
    {
        string strtaxrate;
        Decimal TaxAmount;
        Decimal TaxRate;
        Decimal OrderTotalAmount = Convert.ToDecimal(OrderTotal);
        Decimal InsuranceAmount;
        Decimal GrandTotal;
        DataTable dtTax = new DataTable();
        dtTax.TableName = "TaxDetails";
        dtTax.Columns.Add("TaxRate");
        dtTax.Columns.Add("TaxAmount");
        dtTax.Columns.Add("InsuranceAmount");
        dtTax.Columns.Add("GrandTotal");
        DataRow dr = dtTax.NewRow();

        strtaxrate = objComFun.ExecuteQueryReturnSingleString("Select top 1 Rate From tblStore_Tax Where ZipCode='" + ZipCode + "'");

        if (strtaxrate == "0" || String.IsNullOrEmpty(strtaxrate))
        {
            TaxRate = 0.00M;
            TaxAmount = 0.00M;
            InsuranceAmount = 0.00M;
            GrandTotal = OrderTotalAmount;
        }
        else
        {
            //Tax Amount Calculation
            TaxRate = Convert.ToDecimal(strtaxrate);
            TaxAmount = (OrderTotalAmount / 100) * TaxRate;

            // Shipping & Insurance Charges Calculation
            OrderTotalAmount = RoundToNearestTen(OrderTotalAmount);
            InsuranceAmount = (OrderTotalAmount / 100) * 0.65M;
            InsuranceAmount = InsuranceAmount + 55;

            GrandTotal = OrderTotalAmount + TaxAmount + InsuranceAmount;

        }
        dr["TaxRate"] = TaxRate;
        dr["TaxAmount"] = TaxAmount;
        dr["InsuranceAmount"] = InsuranceAmount;
        dr["GrandTotal"] = GrandTotal;
        dtTax.Rows.Add(dr);
        if (dtTax.Rows.Count > 0)
        {           
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Write(DataTableToJSONWithJavaScriptSerializer(dtTax));
        }
        else
        {
            Context.Response.Write("False");
        }

    }
    
    [WebMethod(Description = "Apply Coupon discount)")]
    public void GetApplyCouponDiscount(string CouponNumber, string ProductStyleNumber, decimal gtotal)
    {
        DataTable table = new DataTable();
        try
        {

            int count = Int32.Parse(objComFun.GetApplyCoupnCode(CouponNumber.Trim(), ProductStyleNumber));

            string discount = string.Empty;
            decimal dis = 0;
            if (count == 1)
            {

                DataTable discountamount = objComFun.GetCheckOutBtngoNew(CouponNumber.Trim());
                string discountRate = discountamount.Rows[0]["initialAmount"].ToString();
                string discountAmount = discountamount.Rows[0]["DiscountAmountindollar"].ToString();
                decimal discount1 = 0;
                if (discountRate == "0" && discountAmount != "0")
                {

                    discount = discountAmount;
                    discount1 = Math.Round((Convert.ToDecimal(discountAmount) / Convert.ToDecimal(gtotal) * 100));
                    dis = discount1;
                }
                else if (discountRate != "0" || discountAmount == "0")
                {
                    dis = Convert.ToDecimal(discountRate);
                    discount1 = gtotal * (dis / 100);
                    discount = discount1.ToString();
                }



            }
            else
            {
                discount = string.Empty;

            }
            table.TableName = "GetCouponData";
            table.Columns.Add("DiscountAmount");
            table.Columns.Add("DiscountRate");
            DataRow drow121 = table.NewRow();
            drow121["DiscountAmount"] = discount;
            drow121["DiscountRate"] = dis;
            table.Rows.Add(drow121);
            if (table.Rows.Count > 0)
            {
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.Write(DataTableToJSONWithJavaScriptSerializer(table));
            }
            else
            {
                Context.Response.Write("False");
            }


        }
        catch (Exception ex)
        {

            Context.Response.Write("False");
        }

        


    }
    private decimal RoundToNearestTen(decimal _decPrice)
    {
        decimal @Amount = _decPrice;
        int @toNearest = 10;
        decimal @rest = 0;
        decimal @nearest = 0;
        @rest = @Amount % @toNearest;
        if (@rest != 0)
            @nearest = @Amount - @rest + @toNearest;
        else
            @nearest = @Amount;
        @Amount = @nearest;
        return @Amount;
    }


    [WebMethod(EnableSession = true)]
    public void PlaceOrder(String FirstName,
        String LastName, String Street, String Address2, String Country, String City,
        String State, String ZipCode, String Telephone1, String Email, String AptUnitNo,
        String ShippingFirstName, String ShippingSurname, String ShippingStreet, String ShippingCompany,
        String ShippingSuburb, String ShippingState, String Shippingemail, String ShippingCountry, String ShippingPostCode,
        String ShippingPhoneNo, String ShippingAptUnitNo, String BillingAddress1, String BillingAddress2,
        String BillingAptUnitNo, String BillingCity, String BillingZipCode,
        String OrderTotal, String taxAmountInTotal, String taxAmountAdded, String shippingAmount, String feeAmount,
        String PaymentAmountRequired, String PaymentMethodId, String PaymentMethodName, String CardNumber, String CardExpiryMonth, String CardExpiryYear, String CardName, String CardType, String Cardccv, String CustomerServiceNumber, String CouponCode, String CouponAmount, String TransferAmount

        )
    {
        string OrderStatus = string.Empty;
        string discountRate = string.Empty;
        string discountAmount = string.Empty;
        DataTable dtStatus = new DataTable();
        dtStatus.Columns.Add("OrderNumber");
        DataRow _dataRows = dtStatus.NewRow();
        String[] ordernumbers = new String[3];
        ordernumbers[0] = "";
        ordernumbers[1] = "";
        ordernumbers[2] = "";
        String orderNo = "";
        try
        {
            String contactid = System.Guid.NewGuid().ToString();
            tblContacts.tblContacts objContacts = null;
            objContacts = new tblContacts.tblContacts();
            tblContacts.tblContactsHelper objContactsHelper = null;
            objContactsHelper = new tblContacts.tblContactsHelper();
            objContacts.ContactID = contactid; //0
            objContacts.UserID = System.Guid.NewGuid().ToString();//1
            objContacts.Telephone1 = Telephone1.Trim();


            objContacts.FirstName = FirstName.Trim();

            objContacts.LastName = LastName.Trim();
            objContacts.Email = Email.Trim();
            objContacts.Street = Street.Trim();
            objContacts.City = City.Trim();
            objContacts.State = State.Trim();
            objContacts.Country = Country.Trim();
            objContacts.ZipCode = ZipCode.Trim();

            objContacts.Address2 = Address2.Trim();
            objContacts.AptUnitNo = AptUnitNo;

            objContactsHelper.InsertContacts(objContacts, "WS");

            Session["Contacts"] = objContacts;

            tblOrders.tblOrders objOrder = null;
            objOrder = new tblOrders.tblOrders();

            objOrder.orderID = -1;

            objOrder.orderAck = 0;
            objOrder.orderCancelled = 0;
            objOrder.customerID = contactid;
            objOrder.membershipID = -1;
            objOrder.membershipType = "";

            objOrder.orderDate = Convert.ToDateTime(DateTime.Today.ToShortDateString());
            objOrder.orderTotal = Convert.ToDecimal(OrderTotal);
            objOrder.taxAmountInTotal = Convert.ToDecimal(taxAmountInTotal);
            objOrder.taxAmountAdded = Convert.ToDecimal(taxAmountAdded);
            objOrder.shippingAmount = Convert.ToDecimal(shippingAmount);
            objOrder.shippingMethod = -1;

            objOrder.feeAmount = Convert.ToDecimal(feeAmount);
            objOrder.paymentAmountRequired = Convert.ToDecimal(PaymentAmountRequired);
            objOrder.paymentMethod = PaymentMethodName;
            objOrder.paymentMethodID = Convert.ToInt32(PaymentMethodId);

            objOrder.paymentMethodIsCC = 1;
            objOrder.paymentMethodIsSC = 1;


            objOrder.cardStoreInfo = null;

            objOrder.shipping_Company = ShippingCompany.Trim();
            objOrder.shipping_FirstName = ShippingFirstName.Trim();
            objOrder.shipping_Surname = ShippingSurname.Trim();
            objOrder.shipping_Street = ShippingStreet.Trim();
            objOrder.shipping_Suburb = ShippingSuburb.Trim();
            objOrder.shipping_State = ShippingState.Trim();
            objOrder.shipping_Country = ShippingCountry.Trim();
            objOrder.shipping_PostCode = ShippingPostCode.Trim();
            objOrder.shipping_Phone = ShippingPhoneNo.Trim();
            objOrder.paymentProcessedDate = DateTime.Now;
            objOrder.specialInstructions = null;
            objOrder.paymentProcessed = 1;
            objOrder.paymentSuccessful = 1;

            objOrder.archived = 0;


            objOrder.Shipping_Email = Shippingemail;

            objOrder.shipping_AptUnitNo = ShippingAptUnitNo.Trim();//51

            objOrder.Billing_Address1 = BillingAddress1.Trim();
            objOrder.Billing_Address2 = BillingAddress2.Trim();
            objOrder.Billing_AptUnitNo = BillingAptUnitNo.Trim();
            objOrder.Billing_City = BillingCity.Trim();
            objOrder.Billing_ZipCode = BillingZipCode.Trim();

            objOrder.cardCCV = Cardccv;
            objOrder.cardExpiryMonth = CardExpiryMonth;
            objOrder.cardExpiryYear = CardExpiryYear;
            objOrder.cardName = CardName;
            objOrder.cardNumber = objEncry_Decrypt.Encrypter(CardNumber.Trim());
            objOrder.cardType = CardType;
            Session["Order"] = objOrder;
            objOrder.CustomeServiceNumber = CustomerServiceNumber;

            objOrder.CouponCode = CouponCode;
            if (CouponAmount.Trim() != "")
            {

                DataTable discountamount = objComFun.GetCheckOutBtngoNew(CouponCode.Trim());
                if (discountamount.Rows.Count > 0)
                {

                     discountRate = discountamount.Rows[0]["initialAmount"].ToString();
                     discountAmount = discountamount.Rows[0]["DiscountAmountindollar"].ToString();

                    if (discountRate == "0" && discountAmount != "0")
                    {

                        objOrder.CouponVoucherAmount = Convert.ToDecimal(discountAmount);
                        objOrder.CouponApplied = 1;
                        objOrder.CouponAppliedDate = DateTime.Now;

                    }
                    else if (discountRate != "0" || discountAmount == "0")
                    {
                        objOrder.CouponAmount = Convert.ToDecimal(CouponAmount);
                        objOrder.CouponApplied = 1;
                        objOrder.CouponAppliedDate = DateTime.Now;
                    }
                }
                else
                {

                    objOrder.CouponAppliedDate = DateTime.Now;

                }
                
            }
            else
            {

                objOrder.CouponAppliedDate = DateTime.Now;
            }
            if (TransferAmount.Trim() != "")
                objOrder.TransferAmount = Convert.ToDecimal(TransferAmount);


            if (PaymentMethodId.Trim() == "1")
            {

                string result = ProcessTransaction();
                string[] response = result.Split('|');


                if (response[0] == "1")
                {

                    orderNo = objOrdersHelper.InsertOrder(objOrder, "Insert");

                    objOrder.orderNo = orderNo;
                    Session["Order"] = objOrder;
                    //string mail = string.Empty;

                    string orderid = orderNo.Substring(3);
                    //ordernumbers[0] = orderid;
                    //ordernumbers[1] = orderNo;
                    //ordernumbers[2] = mail;
                    _dataRows["OrderNumber"] = orderid;                   

                }
                else
                {
                     OrderStatus = "Card Declined";
                    //ordernumbers[1] = "Card Error";
                    //ordernumbers[2] = "Mail Not Sent";
                    _dataRows["OrderNumber"] = OrderStatus;
                   
                }
                //dtStatus.Rows.Add(_dataRows);
                //Context.Response.Clear();
                //Context.Response.ContentType = "application/json";
                //Context.Response.Write(DataTableToJSONWithJavaScriptSerializer(dtStatus));
            }

            if (PaymentMethodId.Trim() == "2")
            {
                orderNo = objOrdersHelper.InsertOrder(objOrder, "Insert");

                objOrder.orderNo = orderNo;
                Session["Order"] = objOrder;
                string mail = string.Empty;

                string orderid = orderNo.Substring(3);
                _dataRows["OrderNumber"] = orderid;
                // ordernumbers[0] = orderid;
               // ordernumbers[1] = orderNo;
               // ordernumbers[2] = mail;
            }

            if (PaymentMethodId.Trim() == "3")
            {
                orderNo = objOrdersHelper.InsertOrder(objOrder, "Insert");

                objOrder.orderNo = orderNo;
                Session["Order"] = objOrder;
                string mail = string.Empty;

                string orderid = orderNo.Substring(3);
               // ordernumbers[0] = orderid;
                //ordernumbers[1] = orderNo;
                // ordernumbers[2] = mail;
                _dataRows["OrderNumber"] = orderid;
            }
           
            
            dtStatus.Rows.Add(_dataRows);
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Write(DataTableToJSONWithJavaScriptSerializer(dtStatus));

        }
        catch (Exception ex)
        {

            //ordernumbers[0] = ex.Message.ToString();
            _strError = ex.ToString();
            _dataRows["OrderNumber"] = "False";
            dtStatus.Rows.Add(_dataRows);
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Write(DataTableToJSONWithJavaScriptSerializer(dtStatus));


        }

        //return ordernumbers;
    }



    private string ProcessTransaction()
    {

        //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        string result = readHtmlPage("https://secure2.authorize.net/gateway/transact.dll");
        return result;


    }
    private string readHtmlPage(string url)
    {


        if (Session["Contacts"] != null)
        {
            objContacts = (tblContacts.tblContacts)Session["Contacts"];
        }
        if (Session["Order"] != null)
        {
            objOrderNew = (tblOrders.tblOrders)Session["Order"];
        }

        string CardNo = objEncry_Decrypt.Decrypter(objOrderNew.cardNumber);

        DataSet ds = objPayment_InfoHelper.GetPaymentMethodInfo(1);
        string UserId = ds.Tables[0].Rows[0]["userName"].ToString();
        string Password = ds.Tables[0].Rows[0]["userPassword"].ToString();
        string Userkey = ds.Tables[0].Rows[0]["userKey"].ToString();
        bool LiveMode = (bool)ds.Tables[0].Rows[0]["LiveMode"];
        string Mode = string.Empty;
        string result = string.Empty;
        if (LiveMode == false)
        {
            Mode = "true";
        }
        else
        {
            Mode = "false";
        }
        string strPost = "x_Login=88X3ucGrFPX&x_tran_key=858X7g3t9KRfc4P5";
        strPost += "&x_Version=3.0&x_Test_Request= false";
        strPost += "&x_type=AUTH_CAPTURE&x_ADC_Delim_Data=TRUE&x_ADC_URL=FALSE&x_method=CC";
        strPost += "&x_Card_Num=" + CardNo.Trim();

        strPost += "&x_Exp_Date=" + objOrderNew.cardExpiryMonth.Trim() + objOrderNew.cardExpiryYear.Trim();
        strPost += "&x_Amount=" + objOrderNew.orderTotal;
        strPost += "&x_card_code=" + objOrderNew.cardCCV;
        strPost += "&x_cust_id=" + objContacts.ContactID;
        strPost += "&x_first_name=" + objContacts.FirstName.ToString();
        strPost += "&x_last_name= " + objContacts.LastName.ToString();
        strPost += "&x_phone=" + objContacts.Telephone1;
        strPost += "&x_Address=" + objContacts.Street;
        strPost += "&x_City=" + objContacts.City;
        strPost += "&x_State=" + objContacts.State;
        strPost += "&x_Zip=" + objContacts.ZipCode;
        strPost += "&x_country=" + objContacts.Country;
        strPost += "&x_email=" + objOrderNew.Shipping_Email;
        string[] _strShippingName = null;
        if (objOrderNew.shipping_FirstName.Trim().Contains(" "))
        {
            _strShippingName = objOrderNew.shipping_FirstName.Split(' ');
        }
        else
        {
            _strShippingName = new string[2];
            _strShippingName[0] = objOrderNew.shipping_FirstName;
            _strShippingName[1] = "";
        }
        string strSqlM = "Select ident_current('tblorders')";
        int intSqlMOID = int.Parse(objComFun.ExecuteQueryReturnSingleString(strSqlM));
        int MOID = intSqlMOID + 1;
        strPost += "&x_ship_to_first_name=" + _strShippingName[0].ToString();
        strPost += "&x_ship_to_last_name=" + _strShippingName[1].ToString();
        strPost += "&x_ship_to_address = " + objOrderNew.shipping_Street;
        strPost += "&x_ship_to_city = " + objOrderNew.shipping_Suburb;
        strPost += "&x_ship_to_state=" + objOrderNew.shipping_State;
        strPost += "&x_ship_to_zip=" + objOrderNew.shipping_PostCode;
        strPost += "&x_ship_to_country=" + objOrderNew.shipping_Country;
        strPost += "&x_description=" + "This is a Live trans.";
        strPost += "&x_invoice_num=" + "JDC" + MOID;
        strPost += "&x_delim_char=|&x_currency_code =USD";
        StreamWriter myWriter = null;
        HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(url);
        objRequest.Method = "POST";
        objRequest.ContentLength = strPost.Length;
        objRequest.ContentType = "application/x-www-form-urlencoded";
        try
        {
            myWriter = new StreamWriter(objRequest.GetRequestStream());
            myWriter.Write(strPost);
        }
        catch (Exception ex)
        {
            _strError = ex.Message.ToString();
        }
        finally
        {
            myWriter.Close();
        }
        HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();
        using (StreamReader sr = new StreamReader(objResponse.GetResponseStream()))
        {
            result = sr.ReadToEnd();
            sr.Close();
        }
        return result;
    }


    [WebMethod]
    public void ProductsOrdered(String orderID, String productID, String optionID, String productName, String productCode, String productPrice, String productQuantity, String inStock, String delivered, String deliveredDate, String deliveryTrackingNumber, String productWeight, String metalType, String CaratWeight, String DiamondQlty, String ItemLength, String StoneSetting, String StoneShape, String StoneCaratWeight, String StoneMM, String StoneNumber, String ItemLengthType, String effectiveCaratWeight, String ColorStoneColor, String DiamondType, String ShippedByMM, String ShippedDateByMM, String MasterMerchantID, String ShippedBySubMM, String ShippedDateBySubMM, String SubMMID, String adminPrice1, String adminPrice2, String charmStyle, String fsFontType, String fsEngraveText, String bsFontType, String bsEngraveText, String NewGiftId, String NewGiftName, String EngraveMetal, String Engravefonttype, String Engravefonttext, String CenterDiamondCarat, String CenterDiamondCut, String CenterDiamondColor, String CenterDiamondClarity, String Certificate, String DiamondID, String SilverReplicaText, String VendorStoneInformationQty, String LooseShape, String LooseWeight, String LooseCut, String LooseColor, String LooseClarity, String LooseDepth, String LooseTable, String LooseGirdle, String LooseSymmetry, String LoosePolish, String LooseCuletSize, String LooseFluorescenceIntensity, String LooseMeasurements, String LooseLab, String LooseDiamondID)
    {
        DBComponent.CommonFunctions objComFun1 = new DBComponent.CommonFunctions();
        //string result = "false";
        string DiamondsCaratStuds1 = string.Empty;
        string DiamondsCutStuds1 = string.Empty;
        string DiamondColorStuds1 = string.Empty;
        string DiamondClarityStuds1 = string.Empty;
        string CertificateStuds1 = string.Empty;
        string DiamondIDStuds1 = string.Empty;
        string DiamondsCaratStuds2 = string.Empty;
        string DiamondsCutStuds2 = string.Empty;
        string DiamondColorStuds2 = string.Empty;
        string DiamondClarityStuds2 = string.Empty;
        string CertificateStuds2 = string.Empty;
        string DiamondIDStuds2 = string.Empty;

        objComFun.ErrorLog("Ex.Source", "Ex.Message", "Ex.TargetSite.ToString()", "Ex.StackTrace", "ProductsOrdered1");
        try
        {

            if (optionID == "")
                optionID = "0";
            if (ShippedByMM == "")
                ShippedByMM = "0";


            if ((CenterDiamondCarat.Contains(",")) && (CenterDiamondCut.Contains(",")) && (CenterDiamondColor.Contains(",")) && (CenterDiamondClarity.Contains(",")) && (Certificate.Contains(",")) && (DiamondID.Contains(",")))
            {
                string[] CenterDiamondCaratwords = CenterDiamondCarat.Split(',');
                if (CenterDiamondCaratwords[0] != "")
                {
                    DiamondsCaratStuds1 = CenterDiamondCaratwords[0].ToString();
                    CenterDiamondCarat = "";

                }
                if (CenterDiamondCaratwords[1] != "")
                {
                    DiamondsCaratStuds2 = CenterDiamondCaratwords[1].ToString();
                    CenterDiamondCarat = "";
                }

                string[] CenterDiamondCutwords = CenterDiamondCut.Split(',');
                if (CenterDiamondCutwords[0] != "")
                {
                    DiamondsCutStuds1 = CenterDiamondCutwords[0].ToString();
                    CenterDiamondCut = "";
                }
                if (CenterDiamondCutwords[1] != "")
                {
                    DiamondsCutStuds2 = CenterDiamondCutwords[1].ToString();
                    CenterDiamondCut = "";
                }
                string[] CenterDiamondColorwords = CenterDiamondColor.Split(',');
                if (CenterDiamondColorwords[0] != "")
                {
                    DiamondColorStuds1 = CenterDiamondColorwords[0].ToString();
                    CenterDiamondColor = "";
                }
                if (CenterDiamondColorwords[1] != "")
                {
                    DiamondColorStuds2 = CenterDiamondColorwords[1].ToString();
                    CenterDiamondColor = "";
                }

                string[] CenterDiamondClaritywords = CenterDiamondClarity.Split(',');
                if (CenterDiamondClaritywords[0] != "")
                {
                    DiamondClarityStuds1 = CenterDiamondClaritywords[0].ToString();
                    CenterDiamondClarity = "";
                }
                if (CenterDiamondClaritywords[1] != "")
                {
                    DiamondClarityStuds2 = CenterDiamondClaritywords[1].ToString();
                    CenterDiamondClarity = "";
                }
                string[] Certificatewords = Certificate.Split(',');
                if (Certificatewords[0] != "")
                {
                    CertificateStuds1 = Certificatewords[0].ToString();
                    Certificate = "";
                }
                if (Certificatewords[1] != "")
                {
                    CertificateStuds2 = Certificatewords[1].ToString();
                    Certificate = "";
                }
                string[] DiamondIDwords = DiamondID.Split(',');
                if (DiamondIDwords[0] != "")
                {
                    DiamondIDStuds1 = DiamondIDwords[0].ToString();
                    DiamondID = "";
                }
                if (DiamondIDwords[1] != "")
                {
                    DiamondIDStuds2 = DiamondIDwords[1].ToString();
                    DiamondID = "";
                }
            }
            string query = "insert into  tblorders_products (orderID,productID,optionID,productName,productPrice,productQuantity,inStock,delivered,deliveryTrackingNumber,productWeight,metalType,CaratWeight,DiamondQlty,ItemLength,StoneSetting,StoneShape,StoneCaratWeight,StoneMM,StoneNumber,ItemLengthType,effectiveCaratWeight,ColorStoneColor,DiamondType,ShippedByMM,ShippedBySubMM,adminPrice1,adminPrice2,charmStyle,fsFontType,fsEngraveText,bsFontType,bsEngraveText,NewGiftId,NewGiftName,EngraveMetal,Engravefonttype,Engravefonttext,CenterDiamondCarat,CenterDiamondCut,CenterDiamondColor,CenterDiamondClarity,Certificate,DiamondID,DiamondsCaratStuds1,DiamondsCutStuds1,DiamondColorStuds1,DiamondClarityStuds1,CertificateStuds1,DiamondIDStuds1,DiamondsCaratStuds2,DiamondsCutStuds2,DiamondColorStuds2,DiamondClarityStuds2,CertificateStuds2,DiamondIDStuds2,SilverReplicaText,VendorStoneInformationQty, LooseShape, LooseWeight, LooseCut, LooseColor, LooseClarity, LooseDepth, LooseTable, LooseGirdle, LooseSymmetry, LoosePolish, LooseCuletSize, LooseFluorescenceIntensity, LooseMeasurements, LooseLab, LooseDiamondID) values (" + Convert.ToInt32(orderID) + ",'" + productID + "', " + Convert.ToInt32(optionID) + ", '" + productName + "','" + productPrice + "', " + Convert.ToInt32(productQuantity) + ", '" + inStock + "', '" + delivered + "','" + deliveryTrackingNumber + "', '" + productWeight + "', '" + metalType + "', '" + CaratWeight + "', '" + DiamondQlty + "', '" + ItemLength + "',  '" + StoneSetting + "', '" + StoneShape + "', '" + StoneCaratWeight + "', '" + StoneMM + "',  '" + StoneNumber + "', '" + ItemLengthType + "', '" + effectiveCaratWeight + "', '" + ColorStoneColor + "', '" + DiamondType + "',  '" + Convert.ToInt32(ShippedByMM) + "', '" + ShippedBySubMM + "', '" + adminPrice1 + "',  '" + adminPrice2 + "', '" + charmStyle + "',  '" + fsFontType + "',  '" + fsEngraveText + "', '" + bsFontType + "', '" + bsEngraveText + "', '" + NewGiftId + "', '" + NewGiftName + "', '" + EngraveMetal + "', '" + Engravefonttype + "',  '" + Engravefonttext + "', '" + CenterDiamondCarat + "',  '" + CenterDiamondCut + "', '" + CenterDiamondColor + "', '" + CenterDiamondClarity + "', '" + Certificate + "', '" + DiamondID + "', '" + DiamondsCaratStuds1 + "', '" + DiamondsCutStuds1 + "', '" + DiamondColorStuds1 + "', '" + DiamondClarityStuds1 + "', '" + CertificateStuds1 + "', '" + DiamondIDStuds1 + "', '" + DiamondsCaratStuds2 + "', '" + DiamondsCutStuds2 + "', '" + DiamondColorStuds2 + "', '" + DiamondClarityStuds2 + "', '" + CertificateStuds2 + "', '" + DiamondIDStuds2 + "', '" + SilverReplicaText + "', '" + VendorStoneInformationQty + "', '" + LooseShape + "', '" + LooseWeight + "', '" + LooseCut + "', '" + LooseColor + "', '" + LooseClarity + "', '" + LooseDepth + "', '" + LooseTable + "', '" + LooseGirdle + "', '" + LooseSymmetry + "', '" + LoosePolish + "', '" + LooseCuletSize + "', '" + LooseFluorescenceIntensity + "', '" + LooseMeasurements + "', '" + LooseLab + "', '" + LooseDiamondID + "')";

            objComFun.ExecuteQueryReturnNothing(query);

            objComFun.ErrorLog("Ex.Source", "Ex.Message", "Ex.TargetSite.ToString()", "Ex.StackTrace", "ProductsOrdered2");
            if (NewGiftId.Trim() != "" && NewGiftId.Trim() != null)
            {
                objOrdersHelper.Insert_SilverPendant(Convert.ToInt32(orderID), 1);
            }
            else
            {
                objOrdersHelper.Insert_SilverPendant(Convert.ToInt32(orderID), 0);
            }

            //result = "true";
            objComFun.ErrorLog("Ex.Source", "Ex.Message", "Ex.TargetSite.ToString()", "Ex.StackTrace", "ProductsOrdered1");

            //Context.Response.Write("true");
            DataTable dtStatus = new DataTable();
            dtStatus.Columns.Add("Status");
            DataRow _dataRows = dtStatus.NewRow();
            _dataRows["Status"] = "true";
            dtStatus.Rows.Add(_dataRows);

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Write(DataTableToJSONWithJavaScriptSerializer(dtStatus));
        }
        catch (Exception Ex)
        {
            objComFun.ErrorLog(Ex.Source, Ex.Message, Ex.TargetSite.ToString(), Ex.StackTrace, "ProductsOrdered");
            //result = "Error: " + ex.Message.ToString();
            DataTable dtError = new DataTable();           
            dtError.Columns.Add("Status");
            DataRow _dataErrorrows = dtError.NewRow();
            _dataErrorrows["Status"] = "DataNotFound";
            dtError.Rows.Add(_dataErrorrows);

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Write(DataTableToJSONWithJavaScriptSerializer(dtError));
        }
       // return result;
    }

    #endregion //---Shopping Cart -----//

    #region  //--------------- PayPal ----------------------//
    [WebMethod]
    public void PayPalPaymentDetails(string paymentAmount, string PaypalProductTitle, string PaypalProductDesc)
    {

        try
        {
            NVPAPICaller PaypalCaller = new NVPAPICaller();
            string retMsg = "";
            string token = "";

            bool ret = PaypalCaller.ShortcutExpressCheckout(paymentAmount, PaypalProductTitle, PaypalProductDesc, ref token, ref retMsg);
            if (ret)
            {

                DataTable dtPayPal = new DataTable();
                dtPayPal.Columns.Add("token");
                dtPayPal.Columns.Add("retMsg");
                dtPayPal.TableName = "dtPaypal";
                DataRow _datarows = dtPayPal.NewRow();
                _datarows["token"] = token;
                _datarows["retMsg"] = retMsg;
                dtPayPal.Rows.Add(_datarows);               

                if (dtPayPal.Rows.Count > 0)
                {                   
                    Context.Response.Clear();
                    Context.Response.ContentType = "application/json";
                    Context.Response.Write(DataTableToJSONWithJavaScriptSerializer(dtPayPal));
                }
                else
                {
                    Context.Response.Write("False");
                }
               
            }
            else
            {

                Context.Response.Write("False");
            }



        }
        catch (Exception Ex)
        {        
            objComFun.ErrorLog(Ex.Source, Ex.Message, Ex.TargetSite.ToString(), Ex.StackTrace, "PayPalPaymentDetails");
            string str = Ex.ToString();
            Context.Response.Write("False");
        }


    }

    [WebMethod]
    public void PaypalShippingDetails(string token)
    {
        try
        {
            NVPAPICaller PaypalCaller = new NVPAPICaller();
            string retMsg = "";
            string payerId = "";
            string shippingAddress = "";

            bool ret = PaypalCaller.GetShippingDetails(token, ref payerId, ref shippingAddress, ref retMsg);
            if (ret)
            {


                DataTable dt = new DataTable();
                dt.Columns.Add("PayerId");
                dt.Columns.Add("ShippingAddress");
                dt.Columns.Add("retMsg");
                dt.TableName = "dtPaypalStatus";

                DataRow _datarows = dt.NewRow();
                _datarows["PayerId"] = payerId;
                _datarows["ShippingAddress"] = shippingAddress;
                _datarows["retMsg"] = retMsg;
                dt.Rows.Add(_datarows);
               

                if (dt.Rows.Count > 0)
                {                  
                    Context.Response.Clear();
                    Context.Response.ContentType = "application/json";
                    Context.Response.Write(DataTableToJSONWithJavaScriptSerializer(dt));
                }
                else
                {
                    Context.Response.Write("False");
                }
                

            }
            else
            {
                Context.Response.Write("False");
            }
        }
        catch (Exception Ex)
        {
            objComFun.ErrorLog(Ex.Source, Ex.Message, Ex.TargetSite.ToString(), Ex.StackTrace, "PaypalShippingDetails");
            string str = Ex.ToString();
            Context.Response.Write("False");


        }
    }

    [WebMethod]
    public void Paypalordercomfirmation(string token, string payerId, string finalPaymentAmount, string PaypalProductTitle, string PaypalProductDesc)
    {
        try
        {
            DataTable dtNewsletter = new DataTable("table");
            dtNewsletter.Columns.Add("NewsletterStatus");
            DataRow dr = dtNewsletter.NewRow();
            dtNewsletter.Rows.Add(dr);
            NVPAPICaller test = new NVPAPICaller();
            string retMsg = "";
            NVPCodec decoder = new NVPCodec();
            bool ret = test.ConfirmPayment(finalPaymentAmount, token, payerId, PaypalProductTitle, PaypalProductDesc, ref decoder, ref retMsg);
            objComFun.ErrorLog(ret.ToString(), retMsg, "Ex.TargetSite.ToString()", "Ex.StackTrace", "Paypalorderc");
            if (ret)
            {
                // Unique transaction ID of the payment. Note:  If the PaymentAction of the request was Authorization or Order, this value is your AuthorizationID for use with the Authorization & Capture APIs. 
                string transactionId = decoder["TRANSACTIONID"];

                // The type of transaction Possible values: l  cart l  express-checkout 
                string transactionType = decoder["TRANSACTIONTYPE"];

                // Indicates whether the payment is instant or delayed. Possible values: l  none l  echeck l  instant 
                string paymentType = decoder["PAYMENTTYPE"];

                // Time/date stamp of payment
                string orderTime = decoder["ORDERTIME"];

                // The final amount charged, including any shipping and taxes from your Merchant Profile.
                string amt = decoder["AMT"];

                // A three-character currency code for one of the currencies listed in PayPay-Supported Transactional Currencies. Default: USD.    
                string currencyCode = decoder["CURRENCYCODE"];

                // PayPal fee amount charged for the transaction    
                string feeAmt = decoder["FEEAMT"];

                // Amount deposited in your PayPal account after a currency conversion.    
                string settleAmt = decoder["SETTLEAMT"];

                // Tax charged on the transaction.    
                string taxAmt = decoder["TAXAMT"];
                // Session["taxAmt"] = taxAmt;

                //' Exchange rate if a currency conversion occurred. Relevant only if your are billing in their non-primary currency. If 
                string exchangeRate = decoder["EXCHANGERATE"];

                //Context.Response.Write("true");                
                dr["NewsletterStatus"] = "true";              
                   
              

            }
            else
            {
                dr["NewsletterStatus"] = "false";
                
               // objComFun.ErrorLog("Ex.Source", "Ex.Message", "Ex.TargetSite.ToString()", "Ex.StackTrace", "Paypalordercomfirmationsdfsdf");
                //Context.Response.Write("False");  

            }
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Write(DataTableToJSONWithJavaScriptSerializer(dtNewsletter));
        }
        catch (Exception Ex)
        {
            DataTable dtErrorNewsletter = new DataTable("table");
            dtErrorNewsletter.Columns.Add("NewsletterStatus");
            DataRow drError = dtErrorNewsletter.NewRow();
            drError["NewsletterStatus"] = "true";
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Write(DataTableToJSONWithJavaScriptSerializer(dtErrorNewsletter));
            objComFun.ErrorLog(Ex.Source, Ex.Message, Ex.TargetSite.ToString(), Ex.StackTrace, "Paypalordercomfirmation");
            string str = Ex.ToString();
            //Context.Response.Write("False");

        }
    }


    [WebMethod]
    public void GetPayPalTokenID(string ShoppingCartReturnURL, string PaymentAmount, string shoppingCartItems, string PayPalCoupon, string PayPalCouponAmount, string PayPalCouponDiscountRate, string PaypalProductTitle, string PaypalProductCustom, string PayerId, string SilverPendantStatus, string SilverPendantStylenoStatus, string TotalAmount)
    {

        try
        {
            NVPAPICaller PaypalCaller = new NVPAPICaller();
            string retMsg = "";
            string token = "";

            bool ret = PaypalCaller.ShortcutExpressCheckout(PaymentAmount, PaypalProductTitle, PaypalProductCustom, ref token, ref retMsg);
            if (ret)
            {


                objComFun.GetPayaplInsertProd(ShoppingCartReturnURL, PaymentAmount, shoppingCartItems, PayPalCoupon, PayPalCouponAmount, PayPalCouponDiscountRate, PaypalProductTitle, PaypalProductCustom, token, PayerId, SilverPendantStatus, SilverPendantStylenoStatus, TotalAmount);

                DataTable dtPayPal = new DataTable();
                dtPayPal.Columns.Add("PayPalToken");
                dtPayPal.Columns.Add("PayPalUrlToRedirect");
                dtPayPal.TableName = "dtPaypal";
                DataRow _datarows = dtPayPal.NewRow();
                _datarows["PayPalToken"] = token;
                _datarows["PayPalUrlToRedirect"] = retMsg;
                dtPayPal.Rows.Add(_datarows);

                if (dtPayPal.Rows.Count > 0)
                {
                    Context.Response.Clear();
                    Context.Response.ContentType = "application/json";
                    Context.Response.Write(DataTableToJSONWithJavaScriptSerializer(dtPayPal));
                }
                else
                {

                    DataTable dtPayPalTokenID = new DataTable();
                    dtPayPalTokenID.Columns.Add("Status");
                    DataRow _dataRow = dtPayPalTokenID.NewRow();
                    _dataRow["Status"] = "true";
                    dtPayPalTokenID.Rows.Add(_dataRow);

                    Context.Response.Clear();
                    Context.Response.ContentType = "application/json";
                    Context.Response.Write(DataTableToJSONWithJavaScriptSerializer(dtPayPalTokenID));
                    Context.Response.Write("true");
                    Context.Response.Write("False");
                }                               

                
            }
            else
            {

                DataTable dtPayPalError = new DataTable();
                dtPayPalError.Columns.Add("Status");
                DataRow _dataErrorRow = dtPayPalError.NewRow();
                _dataErrorRow["Status"] = "true";
                dtPayPalError.Rows.Add(_dataErrorRow);

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.Write(DataTableToJSONWithJavaScriptSerializer(dtPayPalError));
                Context.Response.Write("true");
                Context.Response.Write("False");
            }



        }
        catch (Exception Ex)
        {
            objComFun.ErrorLog(Ex.Source, Ex.Message, Ex.TargetSite.ToString(), Ex.StackTrace, "GetPayPalTokenID");
            string str = Ex.ToString();
            Context.Response.Write("False");
        }


    }


    [WebMethod]
    public void GetPayPalProdsDetails(string TokenID)
    {

        try
        {
            DataSet ds = objComFun.GetPayPalProdsDetails(TokenID);

            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.Write(DataTableToJSONWithJavaScriptSerializer(dt));
            }
            else
            {
                Context.Response.Write("False");
            }
            
        }
        catch (Exception Ex)
        {
            objComFun.ErrorLog(Ex.Source, Ex.Message, Ex.TargetSite.ToString(), Ex.StackTrace, "GetPayPalProdsDetails");
            string str = Ex.ToString();
            Context.Response.Write("False");


        }


    }
    
    [WebMethod(Description = "update SilverPendant Data for PayPal)")]
    public void UpdateSilverPendantStatus(string TokenID, string SilverPendantData)
    {
        objComFun.UpdateSilverPendantStatus(TokenID, SilverPendantData);
        DataTable dtSilverPendant = new DataTable();        
        dtSilverPendant.Columns.Add("Status");
        DataRow _dataErrorrows = dtSilverPendant.NewRow();
        _dataErrorrows["Status"] = "true";
        dtSilverPendant.Rows.Add(_dataErrorrows);

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Write(DataTableToJSONWithJavaScriptSerializer(dtSilverPendant));
        Context.Response.Write("true");

    }
    
    [WebMethod(Description = "update Shopping Cart Items data  for PayPal)")]
    public void UpdateShoppingCartItemsPart1data(string TokenID, string ShoppingCartItems)
    {
        //string decodedUrl = HttpUtility.UrlDecode(ShoppingCartItems);
        objComFun.UpdateShoppingCartItemsPart1data(TokenID, ShoppingCartItems);
        objComFun.ErrorLog("ex.Source", "ex.Message", "ex.TargetSite.ToString()", "ex.StackTrace", "UpdateShoppingCartItemsPart1datafhfgh");
        DataTable dtShoppingCartItems = new DataTable();
        dtShoppingCartItems.Columns.Add("Status");
        DataRow _dataErrorrows = dtShoppingCartItems.NewRow();
        _dataErrorrows["Status"] = "true";
        dtShoppingCartItems.Rows.Add(_dataErrorrows);

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Write(DataTableToJSONWithJavaScriptSerializer(dtShoppingCartItems));
        //Context.Response.Write("true");

    }

    [WebMethod(Description = "update Shopping Cart Items data  for PayPal)")]
    public void UpdateShoppingCartItemsPart2data(string TokenID, string ShoppingCartItems)
    {
        objComFun.UpdateShoppingCartItemsPart2data(TokenID, ShoppingCartItems);
        //Context.Response.Write("true");
        DataTable dtShoppingCart = new DataTable();
        dtShoppingCart.Columns.Add("Status");
        DataRow _dataErrorrows = dtShoppingCart.NewRow();
        _dataErrorrows["Status"] = "true";
        dtShoppingCart.Rows.Add(_dataErrorrows);

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Write(DataTableToJSONWithJavaScriptSerializer(dtShoppingCart));
        Context.Response.Write("true");
    }

    [WebMethod(Description = "Update Oderid  for PayPal)")]
    public void UpdatePayPalOrderId(string TokenID, string Orderid)
    {
        objComFun.UpdatePayPalOrderId(TokenID, Orderid);
        DataTable dtPayPalOrderId = new DataTable();
        dtPayPalOrderId.Columns.Add("Status");
        DataRow _dataErrorrows = dtPayPalOrderId.NewRow();
        _dataErrorrows["Status"] = "true";
        dtPayPalOrderId.Rows.Add(_dataErrorrows);

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Write(DataTableToJSONWithJavaScriptSerializer(dtPayPalOrderId));
        Context.Response.Write("true");
    }

    [WebMethod]
    public void GetPayPalOderId(string TokenID)
    {

        try
        {
            DataSet ds = objComFun.GetPayPalOrderID(TokenID);
            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.Write(DataTableToJSONWithJavaScriptSerializer(dt));
            }
            else
            {
                Context.Response.Write("False");
            }
        }
        catch (Exception Ex)
        {
            objComFun.ErrorLog(Ex.Source, Ex.Message, Ex.TargetSite.ToString(), Ex.StackTrace, "GetPayPalOderId");
            string str = Ex.ToString();
            Context.Response.Write("False");



        }


    }

    #endregion //--------------- PayPal ----------------------//

    #region  //--------------- Mail Send ----------------------//

    [WebMethod]
    public void MailSend(string OrderId)
    {
        string MailStatus = SendMail(Convert.ToInt32(OrderId));
       // objComFun.SendTOQuickBase(OrderId);       
        DataTable dtStatus = new DataTable();
        dtStatus.Columns.Add("Status");
        DataRow _dataRows = dtStatus.NewRow();
        _dataRows["Status"] = "Mail Sent";
        dtStatus.Rows.Add(_dataRows);
        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Write(DataTableToJSONWithJavaScriptSerializer(dtStatus));
        
    }

    protected string SendMail(int OrderIdIphone)
    {
        try
        {
            string errMsg = string.Empty;

            string ccMailIDs = string.Empty;
            string[] body = PrepareBody(OrderIdIphone);
            MailMessage objMail = new MailMessage();
            DataSet ds = objComFun.GetStoreEmailDetail();
            if (ds.Tables[0].Rows.Count > 0)
            {
                string from = ds.Tables[0].Rows[0]["emailFromAddress"].ToString().Trim();
                objMail.From = new MailAddress(from);
                //Appending the staff Members email to send the same mail message to them also
                bool ccStaff = bool.Parse(ds.Tables[0].Rows[0]["ccStaff"].ToString());
                if (ccStaff == true)
                {
                    ccMailIDs = ds.Tables[0].Rows[0]["staffEmail1"].ToString().Trim();
                    objMail.Bcc.Add(new MailAddress(ccMailIDs));
                }

                String to = body[1].ToString();
                objMail.To.Add(new MailAddress(to));
                string strSql = "SELECT receiptSubject FROM tblStore_Email_anjolee;";
                string sTemp = objComFun.ExecuteQueryReturnSingleString(strSql);

                if (sTemp == string.Empty)
                    sTemp = "The Transaction is under process";
                objMail.Subject = sTemp;
                objMail.Body = body[0];
                objMail.IsBodyHtml = true;
                SmtpClient smtpClient = new SmtpClient();
                string hostname = ds.Tables[0].Rows[0]["emailSystemServer"].ToString();
                try
                {

                    //smtpClient.Host = hostname;
                    //smtpClient.Port = 25;

                    smtpClient.Host = "smtpout.secureserver.net";
                    smtpClient.Credentials = new System.Net.NetworkCredential("service@anjolee.com", "rinrin77#12");

                    smtpClient.Send(objMail);

                    //This is for sending SMS
                    objMail.To.Clear();
                    objMail.CC.Clear();
                    objMail.Bcc.Clear();
                    objMail.Subject = string.Empty;
                    objMail.Body = string.Empty;

                   // objMail.To.Add("8583350583@cingularme.com");
                    //objMail.Body = "New order from Anjolee.com $" + Decimal.Round(objOrderNew.orderTotal, 2);


                   /// smtpClient.Send(objMail);
                    errMsg = "Mail Sent";
                }
                catch (Exception ex)
                {
                    errMsg = ex.ToString();

                }

            }
            return errMsg;
        }
        catch (Exception ex)
        {
            _strError = ex.ToString();
            return _strError;
        }
    }

    private string[] PrepareBody(int OrderIdIphone)
    {
        objOrderNew = new tblOrders.tblOrders();
        DataTable dt = DBComponent.CommonFunctions.GetOrderData(OrderIdIphone);
        int SilverPendant = objOrdersHelper.Check_SilverPendant(OrderIdIphone);
        decimal FinalAmount = 0;
        if (dt.Rows.Count > 0)
        {
            objOrderNew.shipping_FirstName = dt.Rows[0]["shipping_FirstName"].ToString();
            objOrderNew.orderNo = dt.Rows[0]["orderNo"].ToString();
            objOrderNew.Shipping_Email = dt.Rows[0]["shipping_Email"].ToString();
            objOrderNew.shipping_Surname = dt.Rows[0]["shipping_Surname"].ToString();
            objOrderNew.shipping_Street = dt.Rows[0]["shipping_Street"].ToString();
            objOrderNew.shipping_Company = dt.Rows[0]["shipping_Company"].ToString();
            objOrderNew.shipping_AptUnitNo = dt.Rows[0]["shipping_AptUnitNo"].ToString();
            objOrderNew.shipping_Suburb = dt.Rows[0]["shipping_Suburb"].ToString();
            objOrderNew.shipping_State = dt.Rows[0]["shipping_State"].ToString();
            objOrderNew.shipping_PostCode = dt.Rows[0]["shipping_PostCode"].ToString();
            objOrderNew.shipping_Country = dt.Rows[0]["shipping_Country"].ToString();
            objOrderNew.shipping_Phone = dt.Rows[0]["shipping_Phone"].ToString();
            objOrderNew.orderTotal = Convert.ToDecimal(dt.Rows[0]["OrderTotal"].ToString());
            objOrderNew.CouponAmount = Convert.ToDecimal(dt.Rows[0]["CouponAmount"].ToString());
            objOrderNew.CouponCode = dt.Rows[0]["CouponCode"].ToString();
            objOrderNew.shippingAmount = Convert.ToDecimal(dt.Rows[0]["shippingamount"].ToString());


        }
        DataSet dsgetmail = objOrdersHelper.GetmailData(OrderIdIphone, objOrderNew.CouponCode, SilverPendant);
        string SubTotal, Discount, TransferDiscount, TaxAmount, TotalAmount, TotalAmountAFterDiscount = "";
        SubTotal = dsgetmail.Tables[0].Rows[0]["SubTotal"].ToString();
        Discount = dsgetmail.Tables[0].Rows[0]["Discount"].ToString();
        TransferDiscount = dsgetmail.Tables[0].Rows[0]["TransferDiscount"].ToString();
        TaxAmount = dsgetmail.Tables[0].Rows[0]["TaxAmount"].ToString();
        TotalAmount = dsgetmail.Tables[0].Rows[0]["TotalAmount"].ToString();
        TotalAmountAFterDiscount = dsgetmail.Tables[0].Rows[0]["AmountAfterDiscount"].ToString();

        int OrderID = DBComponent.CommonFunctions.GetOrderID(objOrderNew.orderNo);
        objEmailHelper = new tblStore_Email.tblStore_EmailHelper();
        DataSet ds = objEmailHelper.GetStoreEmail();
        if (ds.Tables[0].Rows.Count > 0)
        {
            receiptEmail = ds.Tables[0].Rows[0]["receiptEmail"].ToString();
            receiptSubject = ds.Tables[0].Rows[0]["receiptSubject"].ToString();

            #region Header Information
            eur_msg = "<!DOCTYPE HTML PUBLIC '-//IETF//DTD HTML//EN'>";
            eur_msg = eur_msg + "<html>";
            eur_msg = eur_msg + "<head>";
            eur_msg = eur_msg + "<meta http-equiv='Content-Type' content='text/html; charset=iso-8859-1'>";
            eur_msg = eur_msg + "<title>Dear" + objOrderNew.shipping_FirstName + "</title></head>";
            eur_msg = eur_msg + "<body>";
            eur_msg = eur_msg + "<table width=600 style= 'border:1px solid #8c8e8c;'  align=center cellpadding=0 cellspacing=0>";
            eur_msg = eur_msg + receiptEmail.Replace(System.Environment.NewLine, "<br>") + "||-LINEBREAK-||";
            eur_msg = eur_msg + "</td></tr><tr><td>&nbsp;</td></tr><tr><td>&nbsp;</td></tr></table>";
            eur_msg = eur_msg + "</body></html>";

            #endregion



            #region Top Text
            string _strText = "<tr><td><img src=http://www.anjolee.com/images/anjolee-head-img.jpg width=600 height=84/></td></tr>";
            _strText = _strText + "<tr><td valign=top><table width=520 border=0 align=center cellpadding=0 cellspacing=0><tr><td>&nbsp;</td></tr>";

            _strText = _strText + "<tr><td style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;><p>Dear " + objOrderNew.shipping_FirstName + ",</p>";
            _strText = _strText + "<p>At Anjolee.com, our Master Jewelers carefully craft your order to the highest quality standards in the industry, yet we offer competitive prices. We know you will be satisfied with this purchase:</p></td></tr><tr><td>&nbsp;</td></tr>";
            _strText = _strText + "<tr><td style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Your Order Number is " + objOrderNew.orderNo + "</td></tr><tr><td>&nbsp;</td></tr>";
            _strText = _strText + "<tr><td style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;><p>To check the status of your order " + "<a href=http://www.anjolee.com/order_status.aspx> click here</a></p>" + "</td></tr><tr><td>&nbsp;</td></tr>";
            eur_msg = eur_msg.Replace("[TopText]", _strText);
            #endregion



            #region Product Information
            eur_replace = "<tr><td><table width=100% border=0 style=border:1px solid #8c8e8c; cellpadding=2 cellspacing=2>";
            eur_replace = eur_replace + "<tr><td colspan=2 style=font-family:Arial;font-size:13px;font-weight:bold;color:#ffffff;background-color:#29598c;padding-left:6px;height:20px;>Product Information</td></tr>";

            eur_replace = eur_replace + GetProducts(OrderIdIphone, SilverPendant);

            eur_replace = eur_replace + "<tr><td align=right><table width=231 border=0 cellpadding=2 cellspacing=2>";
            eur_replace = eur_replace + "<tr style=height:15px;><td width=181 align=right style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000; >Sub Total:</td><td width=65 align=right style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + MoneyFormate(SubTotal) + "</td></tr>";

            if (objOrderNew.CouponAmount > 0)
            {
                eur_replace = eur_replace + "<tr style=height:15px;><td align=right style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Coupon Amount:</td><td align=right style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + MoneyFormate(Discount) + "</td></tr>";


            }



            if (Convert.ToDecimal(TransferDiscount) > 0)
            {
                eur_replace = eur_replace + "<tr style=height:15px;><td align=right style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Transfer Discount: </td><td align=right style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + MoneyFormate(TransferDiscount) + "</td></tr>";


            }

            if (objOrderNew.CouponAmount > 0 || Convert.ToDecimal(TransferDiscount) > 0)
            {
                eur_replace = eur_replace + "<tr style=height:15px;><td align=right style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Total: </td><td align=right style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + MoneyFormate(TotalAmountAFterDiscount) + "</td></tr>";
            }

            if (SilverPendant == 1)
            {
                eur_replace = eur_replace + "<tr style=height:15px;><td align=right style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Silver Pendant:</td><td align=right style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + "$19.95" + "</td></tr>";
                FinalAmount = FinalAmount + Convert.ToDecimal(19.95);
            }


            eur_replace = eur_replace + "<tr style=height:15px;><td align=right style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>State Tax:</td><td align=right style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + MoneyFormate(TaxAmount) + "</td></tr>";

            if (objOrderNew.shippingAmount > 0)
            {
                eur_replace = eur_replace + "<tr style=height:15px;><td align=right style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Shipping Amount:</td><td align=right style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + MoneyFormate(Convert.ToString(objOrderNew.shippingAmount)) + "</td></tr>";
            }

            eur_replace = eur_replace + "<tr style=height:15px;><td align=right style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Order Total:</td><td align=right style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + MoneyFormate(Convert.ToString(TotalAmount)) + "</td></tr></table></td></tr>";

            eur_replace = eur_replace + "<tr><td>&nbsp;</td></tr><tr><td style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>We take every step possible to ensure customer satisfaction from point of sale to delivery. Your order will be shipped to:</td></tr><tr><td>&nbsp;</td></tr>";
            eur_msg = eur_msg.Replace("[ProductInformation]", eur_replace);
            #endregion





            #region Shipping Information

            eur_ship = eur_ship + "<tr><td><table width=72% border=0 cellpadding=2 cellspacing=2><tr><td colspan=2 style=font-family:Arial;font-size:13px;font-weight:bold;color:#ffffff;background-color:#29598c;padding-left:6px;height:20px;>Shipping Information</td></tr>";

            eur_ship = eur_ship + "<tr><td width=137 align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Name:</td><td width=247 align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(objOrderNew.shipping_FirstName) + " " + DBComponent.CommonFunctions.eur_returnHtml(objOrderNew.shipping_Surname) + "</td></tr>";

            eur_ship = eur_ship + "<tr style=height:15px;><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Address 1:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(objOrderNew.shipping_Street) + "</td></tr>";

            eur_ship = eur_ship + "<tr><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Address 2:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(objOrderNew.shipping_Company) + "</td></tr>";

            eur_ship = eur_ship + "<tr style=height:15px;><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Apt/Unit No.:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(objOrderNew.shipping_AptUnitNo) + "</td></tr>";

            eur_ship = eur_ship + "<tr><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>City:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(objOrderNew.shipping_Suburb) + "</td></tr>";

            eur_ship = eur_ship + "<tr style=height:15px;><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>State:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(objOrderNew.shipping_State) + "</td></tr>";

            eur_ship = eur_ship + "<tr><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Post Code:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(objOrderNew.shipping_PostCode) + "</td></tr>";

            eur_ship = eur_ship + "<tr style=height:15px;><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Country:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(objOrderNew.shipping_Country) + "</td></tr>";




            eur_ship = eur_ship + "<tr><td>&nbsp;</td></tr><tr><td colspan='2'align=center style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;><div style=width: 190px;float: left;>";


            eur_msg = eur_msg.Replace("[ShippingInformation]", eur_ship);


            #endregion




        }

        eur_msg = eur_msg.Replace("||-LINEBREAK-||", System.Environment.NewLine);

        string[] orderarray = new string[2];
        orderarray[0] = eur_msg;
        orderarray[1] = objOrderNew.Shipping_Email;
        return orderarray;


    }


    private string GetProducts(int OrderID, int silverpendant)
    {

        DataTable dt = objComFun.GetProductsForMail(OrderID);
        string eur_replace1 = "";
        string SilverPendantName = "";
        if (dt.Rows.Count > 0)
        {
            int j = 12;
            for (int k = 0; k < dt.Rows.Count; k++)
            {
                if (dt.Rows[k]["productID"] != null)
                {
                    if (dt.Rows[k]["productID"].ToString() != "")
                    {
                        if (dt.Rows[k]["productName"].ToString().Length > j)
                        {
                            j = dt.Rows[k]["productName"].ToString().Length;
                        }
                    }
                }
            }
            j = j + 3;

            decimal fsubTotal = 0;

            for (int k = 0; k < dt.Rows.Count; k++)
            {
                string _strDiamQuality = dt.Rows[k]["DiamondQlty"].ToString();
                string _stoneNumber = dt.Rows[k]["StoneNumber"].ToString();
                if (dt.Rows[k]["diamondtype"].ToString() == "CZ")
                {
                    _strDiamQuality = "";
                    _stoneNumber = _stoneNumber + " " + "CUBIC ZIRCONIA STONES.";
                }
                else if (dt.Rows[k]["diamondtype"].ToString() == "ND")
                {
                    _strDiamQuality = "";
                    _stoneNumber = _stoneNumber + " " + "MOUNTING ONLY.";
                }

                if (dt.Rows[k]["productID"] != null)
                {
                    if (dt.Rows[k]["productID"].ToString() != "")
                    {
                        if (dt.Rows[k]["productName"].ToString() == "I.G.I Appraisal Certificate")
                        {
                            fsubTotal = decimal.Parse(dt.Rows[k]["productPrice"].ToString()) * decimal.Parse(dt.Rows[k]["productQuantity"].ToString());

                            eur_replace1 = eur_replace1 + "<tr><td width=137 align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Style Number:</td><td width=247 align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["ProductStyleNumber"].ToString()) + "</td></tr>" +
                            "<tr style=height:15px;><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Product:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["productName"].ToString()) + "</td></tr>" +
                            "<tr><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Price:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + MoneyFormate(DBComponent.CommonFunctions.eur_returnHtml(decimal.Parse(dt.Rows[k]["productPrice"].ToString()).ToString("N"))) + "</td></tr>" +
                            "<tr style=height:15px;><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Quantity:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["productQuantity"].ToString()) + "</td></tr>" +
                            "<tr><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Amount:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + MoneyFormate(DBComponent.CommonFunctions.eur_returnHtml(fsubTotal.ToString("N"))) + "</td></tr>" +
                              "<tr><td colspan=2><hr style=color:#8c8e8c;height:1px;></hr></td></tr>";

                        }
                        else if (dt.Rows[k]["productName"].ToString() == "Charm")
                        {
                            fsubTotal = decimal.Parse(dt.Rows[k]["productPrice"].ToString()) * decimal.Parse(dt.Rows[k]["productQuantity"].ToString());

                            eur_replace1 = eur_replace1 + "<tr><td width=137 align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Style Number:</td><td width=247 align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["ProductStyleNumber"].ToString()) + "</td></tr>" +
                            "<tr style=height:15px;><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Product:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["productName"].ToString()) + "</td></tr>" +
                            "<tr><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Charm Style:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["charmStyle"].ToString()) + "</td></tr>" +
                            "<tr style=height:15px;><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Front Side Font Type:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["fsFontType"].ToString()) + "</td></tr>" +
                            "<tr><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Front Side Engraving Text:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["fsEngraveText"].ToString()) + "</td></tr>" +
                            "<tr style=height:15px;><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Back Side Font Type:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["bsFontType"].ToString()) + "</td></tr>" +
                            "<tr><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Back Side Engraving Text:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["bsEngraveText"].ToString()) + "</td></tr>" +
                            "<tr style=height:15px;><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Metal Type:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["metalType"].ToString()) + "</td></tr>" +
                            "<tr><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Price:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + MoneyFormate(DBComponent.CommonFunctions.eur_returnHtml(decimal.Parse(dt.Rows[k]["productPrice"].ToString()).ToString("N"))) + "</td></tr>" +
                            "<tr style=height:15px;><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Quantity:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["productQuantity"].ToString()) + "</td></tr>" +
                            "<tr><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Amount:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + MoneyFormate(DBComponent.CommonFunctions.eur_returnHtml(fsubTotal.ToString("N"))) + "</td></tr>" +
                              "<tr><td colspan=2><hr style=color:#8c8e8c;height:1px;></hr></td></tr>";

                        }

                        else if (dt.Rows[k]["productName"].ToString() == "Ring Engraving")
                        {
                            fsubTotal = decimal.Parse(dt.Rows[k]["productPrice"].ToString()) * decimal.Parse(dt.Rows[k]["productQuantity"].ToString());

                            eur_replace1 = eur_replace1 + "<tr><td width=137 align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Product:</td><td width=247 align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["productName"].ToString()) + "</td></tr>" +
                            "<tr style=height:15px;><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Font Type:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["Engravefonttype"].ToString()) + "</td></tr>" +
                            "<tr><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Engraving Text:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["Engravefonttext"].ToString()) + "</td></tr>" +
                            "<tr style=height:15px;><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Price:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + MoneyFormate(DBComponent.CommonFunctions.eur_returnHtml(decimal.Parse(dt.Rows[k]["productPrice"].ToString()).ToString("N"))) + "</td></tr>" +
                            "<tr><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Quantity:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["productQuantity"].ToString()) + "</td></tr>" +
                            "<tr style=height:15px;><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Amount:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + MoneyFormate(DBComponent.CommonFunctions.eur_returnHtml(fsubTotal.ToString("N"))) + "</td></tr>" +
                              "<tr><td colspan=2><hr style=color:#8c8e8c;height:1px;></hr></td></tr>";

                        }

                        else if (dt.Rows[k]["productName"].ToString() == "Silver & CZ Replica" || dt.Rows[k]["productName"].ToString() == "Silver amp; CZ Replica")
                        {
                            fsubTotal = decimal.Parse(dt.Rows[k]["productPrice"].ToString()) * decimal.Parse(dt.Rows[k]["productQuantity"].ToString());

                            eur_replace1 = eur_replace1 + "<tr style=height:15px;><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Exact replica of the item purchased:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["SilverReplicaText"].ToString().Replace("amp;", "&")) + "</td></tr>" +
                            "<tr><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Quantity:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["productQuantity"].ToString()) + "</td></tr>" +
                            "<tr style=height:15px;><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Amount:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + MoneyFormate(DBComponent.CommonFunctions.eur_returnHtml(fsubTotal.ToString("N"))) + "</td></tr>" +
                              "<tr><td colspan=2><hr style=color:#8c8e8c;height:1px;></hr></td></tr>";

                        }
                        else if (dt.Rows[k]["DiamondsCaratStuds1"].ToString() != "" && dt.Rows[k]["DiamondsCaratStuds2"].ToString() != "")
                        {

                            fsubTotal = decimal.Parse(dt.Rows[k]["productPrice"].ToString()) * decimal.Parse(dt.Rows[k]["productQuantity"].ToString());

                            eur_replace1 = eur_replace1 + "<tr><td width=137 align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Style Number:</td><td width=247 align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["ProductStyleNumber"].ToString()) + "</td></tr>" +
                            "<tr style=height:15px;><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Product:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["productName"].ToString()) + "</td></tr>" +

                            "<tr style=height:15px;><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Metal Type:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["metalType"].ToString()) + "</td></tr>" +

                            "<tr style=height:15px;><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Center Stones Carats:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["StoneCaratWeight"].ToString()) + "</td></tr>" +
                            "<tr><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Effective Carat Weight:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["CaratWeight"].ToString()) + "</td></tr>" +
                            "<tr style=height:15px;><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Center Stones Size(MM):</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["StoneMM"].ToString()) + "</td></tr>" +
                            "<tr><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Setting Type:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["StoneSetting"].ToString()) + "</td></tr>" +
                            "<tr style=height:15px;><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Stone Shape:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["StoneShape"].ToString()) + "</td></tr>" +
                            "<tr><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Number of Stones:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(_stoneNumber) + "</td></tr>";

                            if (dt.Rows[k]["colorstonecolor"].ToString() != "")
                            {
                                eur_replace1 = eur_replace1 + "<tr style=height:15px;><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Color Stone:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["colorstonecolor"].ToString()) + "</td></tr>";

                            }


                            eur_replace1 = eur_replace1 + "<tr><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Center Diamond Carat1:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["DiamondsCaratStuds1"].ToString()) + "</td></tr>" +               // For Rapnet Studs earring- 02-Sep-2013
                           "<tr style=height:15px;><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Center Diamond Cut1:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["DiamondsCutStuds1"].ToString()) + "</td></tr>" +
                           "<tr><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Center Diamond Color1:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["DiamondColorStuds1"].ToString()) + "</td></tr>" +
                           "<tr style=height:15px;><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Center Diamond Clarity1:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["DiamondClarityStuds1"].ToString()) + "</td></tr>" +
                           "<tr><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Certificate1:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["CertificateStuds1"].ToString()) + "</td></tr>" +
                           "<tr style=height:15px;><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Diamond ID1:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["DiamondIDStuds1"].ToString()) + "</td></tr>" +

                            "<tr><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Center Diamond Carat2:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["DiamondsCaratStuds2"].ToString()) + "</td></tr>" +               // For Rapnet Studs earring- 02-Sep-2013
                           "<tr style=height:15px;><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Center Diamond Cut2:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["DiamondsCutStuds2"].ToString()) + "</td></tr>" +
                           "<tr><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Center Diamond Color2:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["DiamondColorStuds2"].ToString()) + "</td></tr>" +
                           "<tr style=height:15px;><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Center Diamond Clarity2:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["DiamondClarityStuds2"].ToString()) + "</td></tr>" +
                           "<tr><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Certificate2:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["CertificateStuds2"].ToString()) + "</td></tr>" +
                           "<tr style=height:15px;><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Diamond ID2:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["DiamondIDStuds2"].ToString()) + "</td></tr>" +


                           "<tr><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Price:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + MoneyFormate(DBComponent.CommonFunctions.eur_returnHtml(decimal.Parse(dt.Rows[k]["productPrice"].ToString()).ToString("N"))) + "</td></tr>" +
                           "<tr style=height:15px;><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Quantity:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["productQuantity"].ToString()) + "</td></tr>" +
                           "<tr><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Amount:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + MoneyFormate(DBComponent.CommonFunctions.eur_returnHtml(fsubTotal.ToString("N"))) + "</td></tr>" +
                           "<tr><td colspan=2><hr style=color:#8c8e8c;height:1px;></hr></td></tr>";


                        }

                        else if (dt.Rows[k]["CenterDiamondCarat"].ToString() != "")
                        {

                            if (dt.Rows[k]["DiamondID"].ToString() != "")
                            {
                                fsubTotal = decimal.Parse(dt.Rows[k]["productPrice"].ToString()) * decimal.Parse(dt.Rows[k]["productQuantity"].ToString());

                                eur_replace1 = eur_replace1 + "<tr><td width=137 align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Style Number:</td><td width=247 align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["ProductStyleNumber"].ToString()) + "</td></tr>" +
                                "<tr style=height:15px;><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Product:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["productName"].ToString()) + "</td></tr>";

                                if (dt.Rows[k]["ItemLength"].ToString() != "0")
                                {
                                    eur_replace1 = eur_replace1 + "<tr><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Item Length:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["ItemLength"].ToString()) + "</td></tr>";

                                }

                                eur_replace1 = eur_replace1 + "<tr style=height:15px;><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Metal Type:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["metalType"].ToString()) + "</td></tr>" +
                                "<tr><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Side Stones Quality:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(_strDiamQuality) + "</td></tr>";

                                if (dt.Rows[k]["StoneCaratWeight"].ToString() != "")
                                {

                                    eur_replace1 = eur_replace1 + "<tr style=height:15px;><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Side Stones Carats:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["StoneCaratWeight"].ToString()) + "</td></tr>";

                                }
                                eur_replace1 = eur_replace1 + "<tr><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Effective Carat Weight:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["CaratWeight"].ToString()) + "</td></tr>";

                                if (dt.Rows[k]["StoneMM"].ToString() != "")
                                {
                                    eur_replace1 = eur_replace1 + "<tr style=height:15px;><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Side Stones Size(MM):</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["StoneMM"].ToString()) + "</td></tr>";
                                }
                                eur_replace1 = eur_replace1 + "<tr><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Setting Type:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["StoneSetting"].ToString()) + "</td></tr>" +
                                "<tr style=height:15px;><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Stone Shape:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["StoneShape"].ToString()) + "</td></tr>" +
                                "<tr><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Number of Stones:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(_stoneNumber) + "</td></tr>";

                                if (dt.Rows[k]["colorstonecolor"].ToString() != "")
                                {
                                    eur_replace1 = eur_replace1 + "<tr style=height:15px;><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Color Stone:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["colorstonecolor"].ToString()) + "</td></tr>";

                                }



                                eur_replace1 = eur_replace1 + "<tr><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Center Diamond Carat:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["CenterDiamondCarat"].ToString()) + "</td></tr>" +               // For Rapnet
                                "<tr style=height:15px;><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Center Diamond Cut:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["CenterDiamondCut"].ToString()) + "</td></tr>" +
                                "<tr><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Center Diamond Color:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["CenterDiamondColor"].ToString()) + "</td></tr>" +
                                "<tr style=height:15px;><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Center Diamond Clarity:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["CenterDiamondClarity"].ToString()) + "</td></tr>" +
                                "<tr><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Certificate:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["Certificate"].ToString()) + "</td></tr>" +
                                "<tr style=height:15px;><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Diamond ID:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["DiamondID"].ToString()) + "</td></tr>" +

                                "<tr><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Price:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + MoneyFormate(DBComponent.CommonFunctions.eur_returnHtml(decimal.Parse(dt.Rows[k]["productPrice"].ToString()).ToString("N"))) + "</td></tr>" +
                                "<tr style=height:15px;><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Quantity:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["productQuantity"].ToString()) + "</td></tr>" +
                                "<tr><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Amount:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + MoneyFormate(DBComponent.CommonFunctions.eur_returnHtml(fsubTotal.ToString("N"))) + "</td></tr>" +
                                "<tr><td colspan=2><hr style=color:#8c8e8c;height:1px;></hr></td></tr>";
                            }
                            else
                            {

                                fsubTotal = decimal.Parse(dt.Rows[k]["productPrice"].ToString()) * decimal.Parse(dt.Rows[k]["productQuantity"].ToString());

                                eur_replace1 = eur_replace1 + "<tr><td width=137 align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Style Number:</td><td width=247 align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["ProductStyleNumber"].ToString()) + "</td></tr>" +
                                "<tr style=height:15px;><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Product:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["productName"].ToString()) + "</td></tr>";

                                if (dt.Rows[k]["ItemLength"].ToString() != "0")
                                {
                                    eur_replace1 = eur_replace1 + "<tr><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Item Length:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["ItemLength"].ToString()) + "</td></tr>";

                                }


                                eur_replace1 = eur_replace1 + "<tr style=height:15px;><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Metal Type:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["metalType"].ToString()) + "</td></tr>";

                                if (dt.Rows[k]["TotalTypeOfStones"].ToString() == "Studs1")
                                {


                                    if (dt.Rows[k]["StoneCaratWeight"].ToString() != "")
                                    {

                                        eur_replace1 = eur_replace1 + "<tr style=height:15px;><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Stone Carat Weight :</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["StoneCaratWeight"].ToString()) + "</td></tr>";

                                    }

                                    eur_replace1 = eur_replace1 + "<tr><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Effective Carat Weight:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["CaratWeight"].ToString()) + "</td></tr>";

                                    if (dt.Rows[k]["StoneMM"].ToString() != "")
                                    {
                                        eur_replace1 = eur_replace1 + "<tr style=height:15px;><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Stones Size(MM):</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["StoneMM"].ToString()) + "</td></tr>";
                                    }


                                    eur_replace1 = eur_replace1 + "<tr><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Setting Type:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["StoneSetting"].ToString()) + "</td></tr>" +
                                   "<tr style=height:15px;><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Stone Shape:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["StoneShape"].ToString()) + "</td></tr>" +
                                   "<tr><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Number of Stones:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(_stoneNumber) + "</td></tr>";
                                }
                                else
                                {
                                    eur_replace1 = eur_replace1 + "<tr><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Side Stones Quality:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(_strDiamQuality) + "</td></tr>" +
                                    "<tr style=height:15px;><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Side Stones Carat :</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["StoneCaratWeight"].ToString()) + "</td></tr>" +
                                    "<tr><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Effective Carat Weight:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["CaratWeight"].ToString()) + "</td></tr>" +
                                    "<tr style=height:15px;><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Side Stones Size(MM):</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["StoneMM"].ToString()) + "</td></tr>" +
                                    "<tr><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Setting Type:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["StoneSetting"].ToString()) + "</td></tr>" +
                                    "<tr style=height:15px;><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Stone Shape:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["StoneShape"].ToString()) + "</td></tr>" +
                                    "<tr><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Number of Stones:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(_stoneNumber) + "</td></tr>";
                                }
                                if (dt.Rows[k]["colorstonecolor"].ToString() != "")
                                {
                                    eur_replace1 = eur_replace1 + "<tr style=height:15px;><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Color Stone:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["colorstonecolor"].ToString()) + "</td></tr>";

                                }


                                eur_replace1 = eur_replace1 + "<tr><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Center Diamond Carat:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["CenterDiamondCarat"].ToString()) + "</td></tr>" +               // For Rapnet
                                "<tr style=height:15px;><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Center Diamond Cut:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["CenterDiamondCut"].ToString()) + "</td></tr>" +
                                "<tr><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Center Diamond Color:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["CenterDiamondColor"].ToString()) + "</td></tr>" +
                                "<tr style=height:15px;><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Center Diamond Clarity:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["CenterDiamondClarity"].ToString()) + "</td></tr>" +
                                "<tr><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Certificate:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["Certificate"].ToString()) + "</td></tr>" +

                                "<tr style=height:15px;><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Price:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + MoneyFormate(DBComponent.CommonFunctions.eur_returnHtml(decimal.Parse(dt.Rows[k]["productPrice"].ToString()).ToString("N"))) + "</td></tr>" +
                                "<tr><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Quantity:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["productQuantity"].ToString()) + "</td></tr>" +
                                "<tr style=height:15px;><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Amount:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + MoneyFormate(DBComponent.CommonFunctions.eur_returnHtml(fsubTotal.ToString("N"))) + "</td></tr>" +
                                "<tr><td colspan=2><hr style=color:#8c8e8c;height:1px;></hr></td></tr>";

                            }
                        }
                        else if (dt.Rows[k]["LooseShape"].ToString() != "" && dt.Rows[k]["LooseWeight"].ToString() != "")   // Loose diamond task
                        {

                            fsubTotal = decimal.Parse(dt.Rows[k]["productPrice"].ToString()) * decimal.Parse(dt.Rows[k]["productQuantity"].ToString());

                            eur_replace1 = eur_replace1 + "<tr><td width=137 align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Product:</td><td width=247 align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["productName"].ToString()) + "</td></tr>" +
                            "<tr style=height:15px;><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Shape:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["LooseShape"].ToString()) + "</td></tr>" +
                            "<tr ><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Weight:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["LooseWeight"].ToString()) + "</td></tr>" +
                            "<tr style=height:15px;><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Cut:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["LooseCut"].ToString()) + "</td></tr>" +
                            "<tr ><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Color:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["LooseColor"].ToString()) + "</td></tr>" +
                            "<tr style=height:15px;><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Clarity:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["LooseClarity"].ToString()) + "</td></tr>" +
                            "<tr ><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Depth:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["LooseDepth"].ToString()) + "</td></tr>" +
                            "<tr style=height:15px;><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Table:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["LooseTable"].ToString()) + "</td></tr>" +
                            "<tr><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Girdle:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["LooseGirdle"].ToString()) + "</td></tr>" +
                            "<tr style=height:15px;><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Symmetry:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["LooseSymmetry"].ToString()) + "</td></tr>" +
                            "<tr><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Polish:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["LoosePolish"].ToString()) + "</td></tr>" +
                            "<tr style=height:15px;><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Culet Size:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["LooseCuletSize"].ToString()) + "</td></tr>" +
                            "<tr><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Fluorescence:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["LooseFluorescenceIntensity"].ToString()) + "</td></tr>" +
                            "<tr style=height:15px;><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Measurements:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["LooseMeasurements"].ToString()) + "</td></tr>" +
                             "<tr><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Lab:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["LooseLab"].ToString()) + "</td></tr>" +
                            "<tr style=height:15px;><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Diamond Id:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["LooseDiamondID"].ToString()) + "</td></tr>" +
                            "<tr><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Price:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + MoneyFormate(DBComponent.CommonFunctions.eur_returnHtml(decimal.Parse(dt.Rows[k]["productPrice"].ToString()).ToString("N"))) + "</td></tr>" +
                            "<tr style=height:15px;><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Quantity:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["productQuantity"].ToString()) + "</td></tr>" +
                            "<tr><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Amount:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + MoneyFormate(DBComponent.CommonFunctions.eur_returnHtml(fsubTotal.ToString("N"))) + "</td></tr>" +
                            "<tr><td colspan=2><hr style=color:#8c8e8c;height:1px;></hr></td></tr>";


                        }
                        else
                        {
                            fsubTotal = decimal.Parse(dt.Rows[k]["productPrice"].ToString()) * decimal.Parse(dt.Rows[k]["productQuantity"].ToString());

                            eur_replace1 = eur_replace1 + "<tr><td width=137 align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Style Number:</td><td width=247 align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["ProductStyleNumber"].ToString()) + "</td></tr>" +
                            "<tr style=height:15px;><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Product:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["productName"].ToString()) + "</td></tr>";

                            if (dt.Rows[k]["ItemLength"].ToString() != "0")
                            {
                                eur_replace1 = eur_replace1 + "<tr><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Item Length:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["ItemLength"].ToString()) + "</td></tr>";

                            }

                            eur_replace1 = eur_replace1 + "<tr style=height:15px;><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Metal Type:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["metalType"].ToString()) + "</td></tr>";

                            if (dt.Rows[k]["WeddingStatus"].ToString() == "1")
                            {
                                eur_replace1 = eur_replace1 + "<tr><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Width:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["CaratWeight"].ToString()) + "</td></tr>";
                            }
                            else if (dt.Rows[k]["VendorStoneInformationQty"].ToString() != "")
                            {
                                eur_replace1 = eur_replace1 + "<tr><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Carat Weight:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["CaratWeight"].ToString()) + "</td></tr>" +
                                "<tr><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Stone Information Quantity:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["VendorStoneInformationQty"].ToString()) + "</td></tr>";

                            }
                            else
                            {
                                if (_strDiamQuality != "")
                                {

                                    eur_replace1 = eur_replace1 + "<tr><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Diamond Quality:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(_strDiamQuality) + "</td></tr>";
                                }

                                eur_replace1 = eur_replace1 + "<tr style=height:15px;><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Stone Carat Weight:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["StoneCaratWeight"].ToString()) + "</td></tr>" +
                                "<tr><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Effective Carat Weight:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["CaratWeight"].ToString()) + "</td></tr>" +
                                "<tr style=height:15px;><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Millimeter Size(MM):</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["StoneMM"].ToString()) + "</td></tr>" +
                                "<tr><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Setting Type:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["StoneSetting"].ToString()) + "</td></tr>" +
                                "<tr style=height:15px;><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Stone Shape:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["StoneShape"].ToString()) + "</td></tr>" +
                                "<tr><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Number of Stones:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(_stoneNumber) + "</td></tr>" +
                                "<tr style=height:15px;><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Color Stone:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["colorstonecolor"].ToString()) + "</td></tr>";

                            }
                            eur_replace1 = eur_replace1 + "<tr><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Width:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["CaratWeight"].ToString()) + "</td></tr>" +
                           "<tr style=height:15px;><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Price:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + MoneyFormate(DBComponent.CommonFunctions.eur_returnHtml(decimal.Parse(dt.Rows[k]["productPrice"].ToString()).ToString("N"))) + "</td></tr>" +
                          "<tr ><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Quantity:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["productQuantity"].ToString()) + "</td></tr>" +
                          "<tr style=height:15px;><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Amount:</td><td align=left style=font-family:Arial;font-size:12px;font-weight:normal;color:#000000;>" + MoneyFormate(DBComponent.CommonFunctions.eur_returnHtml(fsubTotal.ToString("N"))) + "</td></tr>" +
                           "<tr><td colspan=2><hr style=color:#8c8e8c;height:1px;></hr></td></tr>";
                        }
                        SilverPendantName = DBComponent.CommonFunctions.eur_returnHtml(dt.Rows[k]["NewGiftName"].ToString().Trim());
                    }
                }

            }
            if (silverpendant == 1)
            {
                eur_replace1 = eur_replace1 + "<tr style=height:15px;><td align=left style=font-family:Arial;font-size:12px;font-weight:bold;color:#000000;>Silver Pendant: </td><td align=left style=font-family:Arial;font-size:12px;color:#000000;>" + SilverPendantName + "</td></tr>";

            }
            eur_replace1 = eur_replace1 + "</table></td></tr>";
        }
        return eur_replace1;
    }

    //*Method Start For Converting String To Money Format ''''''''''''''''''''''''''''''''''''''''
    public string MoneyFormate(string SumVal)
    {
        decimal DataStr = Convert.ToDecimal(SumVal);
        string html = String.Format(" {0:C}", DataStr);
        return html;
    }
    //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

    #endregion //--------------- Mail Send ----------------------//

    #region  //--------------- Track My Order & Return & Repair Product----------------------//
    [WebMethod(Description = "Track My Order")]
    public string TrackMyOrder(string OrderNo, string EmailAddress)
    {
        string DayName = "";
        DateTime OrderDate;
        int Status = -1;
        string DeliveryBox = string.Empty;
        string NoteBox = string.Empty;

        string DeliveryTrackingNumber = null;
        DataSet obj_dsobjetOrderStatus;
        string Status1 = string.Empty;
        string Status2 = string.Empty;
        string Status3 = string.Empty;
        string Status4 = string.Empty;
        string Status5 = string.Empty;
        DataSet DSOrderStatus = new DataSet();
        DataSet DsShipping = new DataSet();
        DataSet dsColorChange = new DataSet();
        DataSet obj_dsobjGetOrderDetails = objComFun.GetOrderDetails("JDC" + OrderNo, EmailAddress);
        if (obj_dsobjGetOrderDetails.Tables[0].Rows.Count > 0)
        {
            DayName = obj_dsobjGetOrderDetails.Tables[0].Rows[0][5].ToString();
            OrderDate = Convert.ToDateTime(obj_dsobjGetOrderDetails.Tables[0].Rows[0][2].ToString());

            DataSet obj_dsobjGetStatus = objComFun.GetStatus("JDC" + OrderNo);
            if (obj_dsobjGetStatus.Tables[0].Rows.Count > 0)
            {
                Status = Convert.ToInt32(obj_dsobjGetStatus.Tables[0].Rows[0][3].ToString());
                DeliveryTrackingNumber = obj_dsobjGetStatus.Tables[0].Rows[0][4].ToString();
            }
            obj_dsobjetOrderStatus = objComFun.GetOrderStatuses();
            int IGI_Payment = objComFun.CheckIGIPayment("JDC" + OrderNo);

            if (obj_dsobjetOrderStatus.Tables[0].Rows.Count > 0)
            {
                Status1 = obj_dsobjetOrderStatus.Tables[0].Rows[0][1].ToString();
                Status2 = obj_dsobjetOrderStatus.Tables[0].Rows[1][1].ToString();
                Status3 = obj_dsobjetOrderStatus.Tables[0].Rows[2][1].ToString();
                if (DeliveryTrackingNumber == "" || DeliveryTrackingNumber == null)
                {

                    Status5 = obj_dsobjetOrderStatus.Tables[0].Rows[4][1].ToString();
                    DeliveryBox = "true";
                    NoteBox = "true";
                }
                else
                {
                    DeliveryBox = "false";
                    NoteBox = "false";

                    Status5 = obj_dsobjetOrderStatus.Tables[0].Rows[4][1].ToString() + " " + DeliveryTrackingNumber;
                }
                if (IGI_Payment == 1)
                {
                    DeliveryBox = "false";
                    Status4 = obj_dsobjetOrderStatus.Tables[0].Rows[5][1].ToString();
                }
                else
                {
                    DeliveryBox = "true";
                    Status4 = obj_dsobjetOrderStatus.Tables[0].Rows[3][1].ToString();
                }
            }

            DataTable dt = new DataTable();
            dt.Columns.Add("Status1");
            dt.Columns.Add("Status2");
            dt.Columns.Add("Status3");
            dt.Columns.Add("Status4");
            dt.Columns.Add("Status5");
            dt.Columns.Add("DeliveryBox");
            dt.Columns.Add("NoteBox");
            dt.TableName = "dtOrderStatus";
            DataRow _datarows = dt.NewRow();

            _datarows["Status1"] = Status1;
            _datarows["Status2"] = Status2;
            _datarows["Status3"] = Status3;
            _datarows["Status4"] = Status4;
            _datarows["Status5"] = Status5;
            _datarows["DeliveryBox"] = DeliveryBox;
            _datarows["NoteBox"] = NoteBox;
            dt.Rows.Add(_datarows);

            DSOrderStatus.Tables.Add(dt);
            if (Status != -1)
            {
                DataTable dtColorChange = objComFun.LabelColorChange(Status);
                DataTable dtstatus = objComFun.SetTimeZone(DayName, OrderDate);
                dsColorChange.Tables.Add(dtColorChange);
                DsShipping.Tables.Add(dtstatus);
                DSOrderStatus.Merge(dsColorChange);
                DSOrderStatus.Merge(DsShipping);
            }

            if (DSOrderStatus.Tables[0].Rows.Count > 0)
            {
               
                jsonString = Serialize(DSOrderStatus);
                return jsonString;
                
            }
            else
            {
                DataTable dtError = new DataTable();
                dtError.Columns.Add("Status");
                DataRow _dataErrorrows = dtError.NewRow();
                _dataErrorrows["Status"] = "DataNotFound";
                dtError.Rows.Add(_dataErrorrows);
                DataSet ds = new DataSet();
                ds.Tables.Add(dtError);
                jsonString = Serialize(ds);
                return jsonString;


                //Context.Response.Write("False");
                //return "Data not found";
            }
           
        }
        else
        {
            DataTable dtError = new DataTable();
            dtError.Columns.Add("Status");
            DataRow _dataErrorrows = dtError.NewRow();
            _dataErrorrows["Status"] = "DataNotFound";
            dtError.Rows.Add(_dataErrorrows);
            DataSet ds = new DataSet();
            ds.Tables.Add(dtError);
            jsonString = Serialize(ds);
            return jsonString;
            //return "Data not found";
        }

    }
    
    [WebMethod(Description = "Return & Repair Product")]
    public void GetReturnProductDetails(string OrderNumber, string EmailAddress)
    {
        string ShippingStatus = string.Empty;
        DataSet ds = new DataSet();
        ds = objComFun.GetReturnProductDetails(OrderNumber, EmailAddress);
        if (ds.Tables[0].Rows.Count != 0)
        {
            ShippingStatus = ds.Tables[0].Rows[0]["ShippingDate"].ToString();

            if (ShippingStatus != string.Empty)
            {
                    DataTable dtOrderStatus = ds.Tables[0];
                    Context.Response.Clear();
                    Context.Response.ContentType = "application/json";
                    Context.Response.Write(DataTableToJSONWithJavaScriptSerializer(dtOrderStatus));               
            }
            else
            {
                DataTable dtError = new DataTable();
                dtError.Columns.Add("Status");
                DataRow _dataErrorrows = dtError.NewRow();
                _dataErrorrows["Status"] = "DataNotFound";
                dtError.Rows.Add(_dataErrorrows);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.Write(DataTableToJSONWithJavaScriptSerializer(dtError));
                //Context.Response.Write("False");
            }
        }
        else
        {
            DataTable dtError = new DataTable();
            dtError.Columns.Add("Status");
            DataRow _dataErrorrows = dtError.NewRow();
            _dataErrorrows["Status"] = "DataNotFound";
            dtError.Rows.Add(_dataErrorrows);
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Write(DataTableToJSONWithJavaScriptSerializer(dtError));
            //Context.Response.Write("False");
        }

    }
    
    [WebMethod(Description = "Return Reason")]
    public void GetReturnReason()
    {
        DataSet ds = new DataSet();
        ds = objComFun.GetReturnReason();
        if (ds.Tables[0].Rows.Count > 0)
        {
            DataTable dtOrderStatus = ds.Tables[0];
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Write(DataTableToJSONWithJavaScriptSerializer(dtOrderStatus));
        }
        else
        {
            Context.Response.Write("False");
        }

    }
     
    [WebMethod(Description = "Return & Repair Product")]
    public void SaveReturnRepairProduct(string OrderId, string ProductstyleNumber, string Amount, string ReturnReasonID, string CustomerFeedback, string ReturnStatus)
    {
        DataTable dtError = new DataTable();
        dtError.Columns.Add("Status");
        DataRow _dataErrorrows = dtError.NewRow();
        string SaveReturnProducts = objComFun.InsertReturnOrders(OrderId, ProductstyleNumber, Amount, ReturnReasonID, CustomerFeedback, ReturnStatus);
        if (SaveReturnProducts == "true")
        {
            
            _dataErrorrows["Status"] = "true";
            
            //Context.Response.Write("False");
        }
        else
        {

            _dataErrorrows["Status"] = "DataNotFound";

        }
        dtError.Rows.Add(_dataErrorrows);
        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Write(DataTableToJSONWithJavaScriptSerializer(dtError));
       
    }

    [WebMethod]
    public string ReturnOrderSendmail(string OrderId, string EmailAddress)
    {
        string Status = "False";
        DataSet DsGetOrderDetails = objComFun.GetReturnOrderdetails(OrderId, EmailAddress);
        if (DsGetOrderDetails.Tables[0].Rows.Count != 0)
        {
            string FName = DsGetOrderDetails.Tables[0].Rows[0]["FirstName"].ToString();
            string LName = DsGetOrderDetails.Tables[0].Rows[0]["LastName"].ToString();

            string _strBody = PopulateBody(FName, LName, OrderId);

            MailMessage objMail = new MailMessage();

            try
            {
                DataSet ds = objComFun.GetStoreEmailDetail();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    // Set the sender address of the mail message
                    //string Subject = "Anjolee Return Request Confirmation";
                    string from = "service@anjolee.com";
                    objMail.From = new MailAddress(from);
                    objMail.Bcc.Add(new MailAddress(from));
                    //Appending the staff Members email to send the same mail message to them also
                    string to = EmailAddress;
                    objMail.To.Add(new MailAddress(to));
                    objMail.Subject = "Anjolee Return Request Confirmation";
                    objMail.Body = _strBody;
                    objMail.IsBodyHtml = true;
                    SmtpClient smtpClient = new SmtpClient();
                    string hostname = ds.Tables[0].Rows[0]["emailSystemServer"].ToString();

                    smtpClient.Host = "smtpout.secureserver.net";
                    smtpClient.Credentials = new System.Net.NetworkCredential("service@anjolee.com", "rinrin77#12");

                    smtpClient.Send(objMail);
                    Status = "true";
                    return Status;
                }
            }
            catch (Exception ex)
            {
                string errMsg = ex.ToString();
                Status = "False";
                return Status;
            }
        }

        return Status;

    }

    private string PopulateBody(string FirstName, string LastName, string OrderNo)
    {
        string body = string.Empty;
        using (StreamReader reader = new StreamReader(Server.MapPath("~/popup_return.html")))
        {
            body = reader.ReadToEnd();
        }
        body = body.Replace("{FirstName}", FirstName);
        body = body.Replace("{LastName}", LastName);
        body = body.Replace("{OrderNo}", OrderNo);
        return body;
    }
    #endregion //---------------Track My Order & Return & Repair Product ----------------------//

    #region  //--------------- Special Promotion ----------------------//

    [WebMethod]
    public void SpecialPromotion(string StrEmailID,string status)
    {
        if (object.Equals(objTblNewsLetter, null))
            objTblNewsLetter = new tbl_Newsletter.tbl_Newsletter();
        if (object.Equals(otbl_NewsletterOnlyHelper, null))
            otbl_NewsletterOnlyHelper = new tbl_Newsletter.tbl_NewsletterHelper();
        objTblNewsLetter.email_address = StrEmailID;
        objTblNewsLetter.date_signed = DateTime.Now.ToShortDateString();
        objTblNewsLetter.Flag = Convert.ToInt32(status);

        DataTable dtSpecialPromotion = new DataTable();
        dtSpecialPromotion.Columns.Add("Status");
        DataRow _datarows = dtSpecialPromotion.NewRow();


        int strstatus = otbl_NewsletterOnlyHelper.Inserttbl_NewsSubcribeletter(this.objTblNewsLetter);

        if (strstatus == Convert.ToInt32("1")) 
        {

            _datarows["Status"] = "Thank for subscribing to Anjolee's newsletters. Please look forward to receiving special promotions exclusive to our mobile device users.";
            dtSpecialPromotion.Rows.Add(_datarows);
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Write(DataTableToJSONWithJavaScriptSerializer(dtSpecialPromotion));
        }
        else if (strstatus == Convert.ToInt32("2")) 
        {
            _datarows["Status"] = "The email address you entered is already subscribed to receive our newsletters.";
            dtSpecialPromotion.Rows.Add(_datarows);
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Write(DataTableToJSONWithJavaScriptSerializer(dtSpecialPromotion));

        }
        else
        {
            _datarows["Status"] = "Failed to subscribe! Please contact administrator..";
            dtSpecialPromotion.Rows.Add(_datarows);
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Write(DataTableToJSONWithJavaScriptSerializer(dtSpecialPromotion));
        }

       
    }


    #endregion //---------------Special Promotion ----------------------//
    
    #region  //--------------- Meta Tag for Main Pages , Thumbnail Pages and Landing Pages ----------------------//

    [WebMethod(Description = " Meta Tag data for Main pages and thumbnail pages.)")]
    public void GetMetaData(String ProductGroupID)
    {
        try
        {
            DataSet DsProductGroupID = new DataSet();
            DsProductGroupID = objComFun.GetCategoryMetaTag(ProductGroupID);

            if (DsProductGroupID.Tables.Count > 0)
            {
                DataTable dt = DsProductGroupID.Tables[0];
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.Write(DataTableToJSONWithJavaScriptSerializer(dt));

            }
            else
            {
                Context.Response.Write("False");

            }

        }
        catch (Exception ex)
        {

            Context.Response.Write("False");

        }


    }


    [WebMethod(Description = " Meta Tag data for Landing pages.)")]
    public void GetLandingPageMetaData(String ProductGroupID)
    {
        try
        {
            
            DataTable dtProductGroupID = new DataTable();
            dtProductGroupID = objComFun.GetLandingPageMetaTag(ProductGroupID);

            if (dtProductGroupID.Rows.Count > 0)
            {                
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.Write(DataTableToJSONWithJavaScriptSerializer(dtProductGroupID));

            }
            else
            {
                Context.Response.Write("False");

            }

        }
        catch (Exception ex)
        {

            Context.Response.Write("False");

        }


    }

    [WebMethod(Description = "GET ProductList URL  )")]
    public void GetProductListURL(string ProductURL)
    {
        DataSet ds = objComFun.GetProductListURL(ProductURL);
        if (ds.Tables.Count > 0)
        {
            DataTable dt = ds.Tables[0];
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Write(DataTableToJSONWithJavaScriptSerializer(dt));

        }
        else
        {
            DataTable dtStatus = new DataTable();
            dtStatus.Columns.Add("Status");
            DataRow _dataRows = dtStatus.NewRow();
            _dataRows["Status"] = "False";
            dtStatus.Rows.Add(_dataRows);
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Write(DataTableToJSONWithJavaScriptSerializer(dtStatus));

        }
    }

    #endregion //---------------Meta Tag for Main Pages and Thumbnail pages ----------------------//           

    #region  //--------------- Email a Hint for Product page ----------------------//
    [WebMethod(Description = "Email a Hint")]
    public void SendMailForTellAFriend(string ProductID ,string ProductTitle,string Name,string SenderName,string SenderEmailID, string ReceiverName, string ReceiverEmailID, string ClassicWeight,string ProductURL)
    {
        DataTable dtStatus = new DataTable();
        dtStatus.Columns.Add("Status");
        DataRow _dataRows = dtStatus.NewRow();
        try
        {
            string _strBody = string.Empty;   
            DataSet dsnew = objComFun.GetProductDetailBindProducts1(ProductID, ClassicWeight);
            string ImageURL = GetHDPaths(dsnew.Tables[0].Rows[0]["HDImageName"].ToString().Trim());

            _strBody = PopulateBody(ReceiverName, SenderName, ProductURL, ProductTitle, ImageURL);
            SendMailForTellAFriend(SenderName, SenderEmailID, ReceiverName, ReceiverEmailID, _strBody);
            _dataRows["Status"] = "Mail Sent";
            dtStatus.Rows.Add(_dataRows);
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Write(DataTableToJSONWithJavaScriptSerializer(dtStatus));

        }
        catch (Exception ex)
        {
            _strError = ex.ToString();
            _dataRows["Status"] = "False";
            dtStatus.Rows.Add(_dataRows);
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Write(DataTableToJSONWithJavaScriptSerializer(dtStatus));
        }
        

    }

    public string GetHDPaths(string strname)
    {
        string str2 = "/Admin/UploadImages/MediumGoldImages/imageNotFound.gif";

        if (strname.Trim() != "")
        {
            str2 = "https://" + strname.Trim();
        }
        return str2;
    }

    private string PopulateBody(string recipientsname, string sendername, string strUrl, string producttitle, string image)
    {
        string body = string.Empty;
        using (StreamReader reader = new StreamReader(Server.MapPath("~/popup_taf.html")))
        {
            body = reader.ReadToEnd();
        }
        body = body.Replace("{recipientsname}", recipientsname);
        body = body.Replace("{sendername}", sendername);
        body = body.Replace("{Url}", strUrl);
        body = body.Replace("{producttitle}", producttitle);
        body = body.Replace("{image}", image);
        return body;
    }
    protected void SendMailForTellAFriend(string _strName, string _strFrmMail, string strFriendEmail, string _strToMail, string _strBody)
    {
        MailMessage objMail = new MailMessage();
        DataSet ds = objComFun.GetStoreEmailDetail();
        if (ds.Tables[0].Rows.Count > 0)
        {
            try
            {
                string name = UppercaseFirst(_strName);
                string from = _strFrmMail;

                objMail.From = new MailAddress(from);
                bool ccStaff = bool.Parse(ds.Tables[0].Rows[0]["ccStaff"].ToString());

                if (ccStaff == true)
                {
                    string ccMailIDs = ds.Tables[0].Rows[0]["staffEmail1"].ToString().Trim();

                    objMail.Bcc.Add(new MailAddress(ccMailIDs));
                }

                string to = _strToMail;

                objMail.To.Add(new MailAddress(to));

                objMail.Subject = "A Hint From " + name;

                objMail.Body = _strBody;
                objMail.IsBodyHtml = true;

                SmtpClient smtpClient = new SmtpClient();
                string hostname = ds.Tables[0].Rows[0]["emailSystemServer"].ToString();

                //smtpClient.Host = hostname;
                //smtpClient.Port = 25;
                smtpClient.Host = "smtpout.secureserver.net";
                smtpClient.Credentials = new System.Net.NetworkCredential("service@anjolee.com", "rinrin77#12");
                smtpClient.Send(objMail);
            }
            catch (Exception ex)
            {
                string errMsg = ex.ToString();


            }
        }
    }
    static string UppercaseFirst(string s)
    {
        if (string.IsNullOrEmpty(s))
        {
            return string.Empty;
        }
        return char.ToUpper(s[0]) + s.Substring(1);
    }

    #endregion //---------------Email a Hint on Product page ----------------------//

    #region  //--------------- Json Data Funcions---------------------//

    public string DataTableToJSONWithJavaScriptSerializer(DataTable table)
    {
        JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
        List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
        Dictionary<string, object> childRow;
        foreach (DataRow row in table.Rows)
        {
            childRow = new Dictionary<string, object>();
            foreach (DataColumn col in table.Columns)
            {
                childRow.Add(col.ColumnName, row[col]);
            }
            parentRow.Add(childRow);
        }
        return jsSerializer.Serialize(parentRow);
    }
    public string Serialize(object value)
    {
        JavaScriptSerializer ser = new JavaScriptSerializer();

        List<JavaScriptConverter> converters = new List<JavaScriptConverter>();

        if (value != null)
        {
            Type type = value.GetType();
            if (type == typeof(DataTable) || type == typeof(DataRow) || type == typeof(DataSet))
            {
                converters.Add(new JSONDataRowConverter());
                converters.Add(new JSONDataTableConverter());
                converters.Add(new JSONDataSetConverter());
            }

            if (converters.Count > 0)
                ser.RegisterConverters(converters);
        }
        ser.MaxJsonLength = 20971520;
        return ser.Serialize(value);
    }
    public object Deserialize(string jsonText, Type valueType)
    {
        JavaScriptSerializer ser = new JavaScriptSerializer();
        object result = ser.GetType()
                           .GetMethod("Deserialize")
                           .MakeGenericMethod(valueType)
                          .Invoke(ser, new object[1] { jsonText });
        return result;
    }

    internal class JSONDataTableConverter : JavaScriptConverter
    {
        public override IEnumerable<Type> SupportedTypes
        {
            get { return new Type[] { typeof(DataTable) }; }
        }

        public override object Deserialize(IDictionary<string, object> dictionary, Type type,
                                           JavaScriptSerializer serializer)
        {
            throw new NotImplementedException();
        }


        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            DataTable table = obj as DataTable;

            // *** result 'object'
            Dictionary<string, object> result = new Dictionary<string, object>();

            if (table != null)
            {
                // *** We'll represent rows as an array/listType
                List<object> rows = new List<object>();

                foreach (DataRow row in table.Rows)
                {
                    rows.Add(row);  // Rely on DataRowConverter to handle
                }
                result["Rows"] = rows;

                return result;
            }

            return new Dictionary<string, object>();
        }


    }
    internal class JSONDataRowConverter : JavaScriptConverter
    {
        public override IEnumerable<Type> SupportedTypes
        {
            get { return new Type[] { typeof(DataRow) }; }
        }

        public override object Deserialize(IDictionary<string, object> dictionary, Type type,
                                           JavaScriptSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            DataRow dataRow = obj as DataRow;
            Dictionary<string, object> propValues = new Dictionary<string, object>();

            if (dataRow != null)
            {
                foreach (DataColumn dc in dataRow.Table.Columns)
                {
                    propValues.Add(dc.ColumnName, dataRow[dc]);
                }
            }

            return propValues;
        }
    }

    internal class JSONDataSetConverter : JavaScriptConverter
    {
        public override IEnumerable<Type> SupportedTypes
        {
            get { return new Type[] { typeof(DataSet) }; }
        }

        public override object Deserialize(IDictionary<string, object> dictionary, Type type,
                                           JavaScriptSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            DataSet dataSet = obj as DataSet;
            Dictionary<string, object> tables = new Dictionary<string, object>();

            if (dataSet != null)
            {
                foreach (DataTable dt in dataSet.Tables)
                {
                    tables.Add(dt.TableName, dt);
                }
            }
            return tables;
        }
    }

    #endregion //---- Json Data Funcions -----//

    #region  //--------------- Shopping Cart Session Data ----------------------//
    [WebMethod(Description = "Add to cart data )")]
    public void AddtocartSessionProduct(string SessionID, string SessionData, string CertificateData)
    {
        DataTable dtStatus = new DataTable();
        dtStatus.Columns.Add("AddtocartSessionData");
        DataRow _dataRows = dtStatus.NewRow();
        try
        {

            string DsProductID = objComFun.Addtocart(SessionID, SessionData, CertificateData);

            _dataRows["AddtocartSessionData"] = DsProductID;
            

        }
        catch (Exception ex)
        {

            _dataRows["Status"] = "false";

        }
        dtStatus.Rows.Add(_dataRows);
        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Write(DataTableToJSONWithJavaScriptSerializer(dtStatus));

    }

    [WebMethod(Description = "Get shopping cart data)")]
    public void GetCartSessionProduct(String CSPID)
    {
       
        try
        {
            DataSet DsProductID = new DataSet();
            DsProductID = objComFun.GetCartSessionProduct(CSPID);

            if (DsProductID.Tables[0].Rows.Count != 0)
            {
                DataTable dt = DsProductID.Tables[0];
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.Write(DataTableToJSONWithJavaScriptSerializer(dt));
            }
            else
            {
                Context.Response.Write("False");
            }

        }
        catch (Exception ex)
        {

            Context.Response.Write("False");

        }

       
    }

    [WebMethod(Description = "Add to cart data)")]
    public void ClearCartSessionProduct(string SessionID)
    {
        DataTable dtStatus = new DataTable();
        dtStatus.Columns.Add("DeleteSessiondata");
        DataRow _dataRows = dtStatus.NewRow();
        try
        {
            string DsProductID = objComFun.ClearCartSessionProduct(SessionID);
            _dataRows["DeleteSessiondata"] = DsProductID;

        }
        catch (Exception ex)
        {

            _dataRows["DeleteSessiondata"] = "False";

        }
        dtStatus.Rows.Add(_dataRows);
        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Write(DataTableToJSONWithJavaScriptSerializer(dtStatus));

    }

    [WebMethod(Description = "Update Shooping Cart data )")]
    public void UpdateCartSessionProduct(string CSPID, string ProductName, string ProductData)
    {
        int ID = 0;
        DataTable dtStatus = new DataTable();
        dtStatus.Columns.Add("UpdateSessionCartdata");
        DataRow _dataRows = dtStatus.NewRow();
        try
        {
            if (ProductName == "CertificateData")
            {
                ID = 1;
            }
            else if (ProductName == "charmPriceShoppingcart")
            {
                ID = 2;
            }
            else if (ProductName == "silverReplicaPriceShoppingcart")
            {

                ID = 3;

            }

            string DsProductID = objComFun.UpdateCartSessionProduct(CSPID, ID.ToString(), ProductData);
            _dataRows["DeleteSessiondata"] = DsProductID;

        }
        catch (Exception ex)
        {

            _dataRows["DeleteSessiondata"] = "false";

        }

        dtStatus.Rows.Add(_dataRows);
        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Write(DataTableToJSONWithJavaScriptSerializer(dtStatus));

    }


    [WebMethod]
    public void GetCountries()
    {
        DataSet dscountries = new DataSet();
        String str = "select * from countries order by countryName";
        dscountries = objComFun.ExecuteQueryReturnDataSet(str);
        if (dscountries.Tables[0].Rows.Count != 0)
        {
            DataTable dtcountries = dscountries.Tables[0];
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Write(DataTableToJSONWithJavaScriptSerializer(dtcountries));
        }
        else
        {
            Context.Response.Write("False");
        }

        
    }
    #endregion //---------------Shopping Cart Session Data ----------------------//


    [WebMethod]
    public void GetStoneCaretWeight(string ProductID, string CaretWeight,string LengthSize)
    {
             
        string SelectedLength = LengthSize;
        string BangleStatus = string.Empty;
        string BangleLength = string.Empty;
        DataTable dt = new DataTable();
        DataTable dtStoneCaret = new DataTable("table");
        dtStoneCaret.Columns.Add("StoneCaret");
        dtStoneCaret.Columns.Add("Stonemm");
        dtStoneCaret.Columns.Add("StoneQty");
        DataRow _dataRow = dtStoneCaret.NewRow();
        int EternityStatus = objComFun.CheckEternityStatus(ProductID);
        try
        {
            string size = string.Empty;
            string SettingType = string.Empty;
            string shape = string.Empty;
            string StoneQty = string.Empty;
            string StrStoneType = string.Empty;
            string _strStoneCaratWeight = string.Empty;
            string _strStoneCaratWeightNew = string.Empty;
            string _strStoneCaratWeightCenterStone = string.Empty;
            string StrStoneCaratWeight_Rapnet = string.Empty;
            string StrStoneSize_Rapnet = string.Empty;
            string sizeLen = string.Empty;
            string StoneQtyLen = string.Empty;
            string _strStoneCaratWeightLen = string.Empty;
            string _strStoneCaratWeightNewLen = string.Empty;
            string _strStoneCaratWeightCenterStoneLen = string.Empty;
            string StrStoneCaratWeight_RapnetLen = string.Empty;
            string StrStoneSize_RapnetLen = string.Empty;
            string isEnable = string.Empty;
            string DefaultLength = string.Empty;
            string DefaultSize = string.Empty;
            string txtRingSize = string.Empty;
            string Centerdiamond = "0";
            DataTable dtDiamond = new DataTable();
            DataTable dtColorStone = new DataTable();
            DataTable DtstoneConfig = new DataTable();
            DataSet DsLengthConfig = new DataSet();


            DsLengthConfig = objComFun.GetProductDetailLengthSizeCal(ProductID);

            if (DsLengthConfig.Tables[0].Rows[0]["DefaultSize"].ToString().Trim() == "")
            {
                LengthSize = "-1";
            }
            DefaultLength = DsLengthConfig.Tables[0].Rows[0]["ISLength"].ToString();
            DefaultSize = DsLengthConfig.Tables[0].Rows[0]["ISSize"].ToString();
            isEnable = DsLengthConfig.Tables[0].Rows[0]["ISEnable"].ToString();

            if (isEnable == "False")
            {
                //txtItemLengthSizeDiv.Value = "1";
                //txtPendentLengthDiv.Value = "0";
            }
            else if ((isEnable == "True") && (DefaultLength == "True") && (DefaultSize == "False"))
            {
                //txtItemLengthSizeDiv.Value = "0";
                // txtPendentLengthDiv.Value = "1";
            }
            else if ((isEnable == "True") && (DefaultLength == "False") && (DefaultSize == "True"))
            {
                // txtPendentLengthDiv.Value = "0";
                txtRingSize = "1";
                //txtItemLengthSizeDiv.Value = "1";
            }
            SelectedLength = LengthSize;
            string chkSemiMount = "false";
            if (chkSemiMount == "true")
            {
                DtstoneConfig = objComFun.GetProductDetailStoneType1(ProductID.Trim(), CaretWeight);
            }
            else
            {
                DtstoneConfig = objComFun.GetProductDetailStoneType2(ProductID.Trim(), CaretWeight);
            }

            if (DtstoneConfig.Rows.Count != 0)
            {
                foreach (DataRow DtstoneConfigRow in DtstoneConfig.Rows)
                {
                    if (DtstoneConfigRow["StoneType"].ToString() == "1")
                    {

                        if (LengthSize == "-1")
                        {
                            dtDiamond = objComFun.GetProductDetailDiamond(ProductID.Trim(), CaretWeight, DtstoneConfigRow["stoneConfigurationID"].ToString());
                        }
                        else if (txtRingSize == "1")
                        {


                            dtDiamond = objComFun.GetProductDetailDiamond(ProductID.Trim(), CaretWeight, DtstoneConfigRow["stoneConfigurationID"].ToString());
                        }

                        else
                        {


                            SelectedLength = string.Empty;
                            SelectedLength = LengthSize;
                            dtDiamond = objComFun.GetProductDetailDiamondNew(ProductID.Trim(), CaretWeight, DtstoneConfigRow["stoneConfigurationID"].ToString(), SelectedLength);
                        }

                        if (dtDiamond.Rows.Count != 0)
                        {
                            foreach (DataRow dtDiamondrow in dtDiamond.Rows)
                            {
                                Centerdiamond = dtDiamondrow["CenterStone"].ToString().Trim();
                                if (size.Trim() == string.Empty)
                                {
                                    size = Convert.ToString(dtDiamondrow["StoneSize"].ToString());

                                    sizeLen = Convert.ToString(dtDiamondrow["StoneSizeLen"].ToString());
                                }
                                else
                                {
                                    size = size + ", " + Convert.ToString(dtDiamondrow["StoneSize"].ToString());

                                    sizeLen = sizeLen + ", " + Convert.ToString(dtDiamondrow["StoneSizeLen"].ToString());
                                }
                                if (_strStoneCaratWeight.Trim() == string.Empty)
                                {
                                    _strStoneCaratWeight = Convert.ToString(dtDiamondrow["CaratWeight"].ToString());
                                    _strStoneCaratWeightNew = Convert.ToString(dtDiamondrow["CaratWeightNew"].ToString());

                                    _strStoneCaratWeightLen = Convert.ToString(dtDiamondrow["CaratWeightLen"].ToString());
                                    _strStoneCaratWeightNewLen = Convert.ToString(dtDiamondrow["CaratWeightNewLen"].ToString());

                                    if (dtDiamondrow["CenterStone"].ToString().Trim() == "1")
                                    {
                                        _strStoneCaratWeightCenterStone = _strStoneCaratWeightNew + "(center stone)";

                                        _strStoneCaratWeightCenterStoneLen = _strStoneCaratWeightNewLen + "(center stone)";

                                    }
                                    else
                                    {
                                        _strStoneCaratWeightCenterStone = _strStoneCaratWeightNew;

                                        _strStoneCaratWeightCenterStoneLen = _strStoneCaratWeightNewLen;
                                        //Centerdiamond = "1";
                                    }
                                    if (dtDiamondrow["CenterStone"].ToString().Trim() == "0")
                                    {
                                        StrStoneCaratWeight_Rapnet = _strStoneCaratWeight;
                                        StrStoneSize_Rapnet = size;

                                        StrStoneCaratWeight_RapnetLen = _strStoneCaratWeightLen;
                                        StrStoneSize_RapnetLen = sizeLen;
                                    }

                                }
                                else
                                {
                                    string strChkFr1 = dtDiamondrow["CaratWeight"].ToString();
                                    _strStoneCaratWeight = _strStoneCaratWeight + ", " + Convert.ToString(dtDiamondrow["CaratWeight"].ToString());
                                    _strStoneCaratWeightNew = _strStoneCaratWeightNew + ", " + Convert.ToString(dtDiamondrow["CaratWeightNew"].ToString());

                                    _strStoneCaratWeightLen = _strStoneCaratWeightLen + ", " + Convert.ToString(dtDiamondrow["CaratWeightLen"].ToString());
                                    _strStoneCaratWeightNewLen = _strStoneCaratWeightNewLen + ", " + Convert.ToString(dtDiamondrow["CaratWeightNewLen"].ToString());


                                    if (dtDiamondrow["CenterStone"].ToString().Trim() == "1")
                                    {
                                        _strStoneCaratWeightCenterStone = _strStoneCaratWeightCenterStone + ", " + Convert.ToString(dtDiamondrow["CaratWeightNew"].ToString()) + "(center stone)";

                                        _strStoneCaratWeightCenterStoneLen = _strStoneCaratWeightCenterStoneLen + ", " + Convert.ToString(dtDiamondrow["CaratWeightNewLen"].ToString()) + "(center stone)";
                                    }
                                    else
                                    {
                                        _strStoneCaratWeightCenterStone = _strStoneCaratWeightCenterStone + ", " + Convert.ToString(dtDiamondrow["CaratWeightNew"].ToString());

                                        _strStoneCaratWeightCenterStoneLen = _strStoneCaratWeightCenterStoneLen + ", " + Convert.ToString(dtDiamondrow["CaratWeightNewLen"].ToString());
                                    }
                                    if (dtDiamondrow["CenterStone"].ToString().Trim() == "0")
                                    {
                                        StrStoneCaratWeight_Rapnet = _strStoneCaratWeight;
                                        StrStoneSize_Rapnet = size;

                                        StrStoneCaratWeight_RapnetLen = _strStoneCaratWeightLen;
                                        StrStoneSize_RapnetLen = sizeLen;
                                    }

                                }

                                if (StoneQty.Trim() == string.Empty)
                                {

                                    StoneQty = Convert.ToString(dtDiamondrow["StoneQTy"].ToString());

                                    StoneQtyLen = Convert.ToString(dtDiamondrow["StoneQTyLen"].ToString());
                                }
                                else
                                {
                                    decimal x = Convert.ToDecimal(StoneQty) + Convert.ToDecimal(dtDiamondrow["StoneQTy"].ToString());
                                    StoneQty = Convert.ToString(Decimal.Round(x, 1));

                                    decimal x_Len = Convert.ToDecimal(StoneQtyLen) + Convert.ToDecimal(dtDiamondrow["StoneQTyLen"].ToString());
                                    StoneQtyLen = Convert.ToString(Decimal.Round(x_Len, 1));
                                }
                            }
                        }


                    }
                    else
                    {
                        if (LengthSize == "-1")
                        {
                            dtColorStone = objComFun.GetProductDetailColorStone(ProductID.Trim(), CaretWeight, DtstoneConfigRow["stoneConfigurationID"].ToString());
                        }
                        else if (txtRingSize == "1")
                        {

                            dtColorStone = objComFun.GetProductDetailColorStone(ProductID.Trim(), CaretWeight, DtstoneConfigRow["stoneConfigurationID"].ToString());
                        }

                        else
                        {
                            dtColorStone = objComFun.GetProductDetailColorStoneNew(ProductID.Trim(), CaretWeight, DtstoneConfigRow["stoneConfigurationID"].ToString(), SelectedLength);
                        }
                        if (dtColorStone.Rows.Count != 0)
                        {
                            foreach (DataRow dtColorStoneRow in dtColorStone.Rows)
                            {
                                if (size.Trim() == string.Empty)
                                {
                                    size = Convert.ToString(dtColorStoneRow["StoneSize"].ToString());
                                    sizeLen = Convert.ToString(dtColorStoneRow["StoneSizeLen"].ToString());
                                }
                                else
                                {
                                    size = size + ", " + Convert.ToString(dtColorStoneRow["StoneSize"].ToString());
                                    sizeLen = sizeLen + ", " + Convert.ToString(dtColorStoneRow["StoneSizeLen"].ToString());
                                }
                                if (_strStoneCaratWeight.Trim() == string.Empty)
                                {
                                    _strStoneCaratWeight = Convert.ToString(dtColorStoneRow["CaratWeight"].ToString());
                                    _strStoneCaratWeightNew = Convert.ToString(dtColorStoneRow["CaratWeightNew"].ToString());
                                    _strStoneCaratWeightLen = Convert.ToString(dtColorStoneRow["CaratWeightLen"].ToString());
                                    _strStoneCaratWeightNewLen = Convert.ToString(dtColorStoneRow["CaratWeightNewLen"].ToString());

                                    if (dtColorStoneRow["CenterStone"].ToString().Trim() == "1")
                                    {
                                        _strStoneCaratWeightCenterStone = _strStoneCaratWeightNew + "(center stone)";
                                        _strStoneCaratWeightCenterStoneLen = _strStoneCaratWeightNewLen + "(center stone)";
                                    }
                                    else
                                    {
                                        _strStoneCaratWeightCenterStone = _strStoneCaratWeightNew;
                                        _strStoneCaratWeightCenterStoneLen = _strStoneCaratWeightNewLen;
                                    }
                                }
                                else
                                {
                                    _strStoneCaratWeight = _strStoneCaratWeight + ", " + Convert.ToString(dtColorStoneRow["CaratWeight"].ToString());
                                    _strStoneCaratWeightNew = _strStoneCaratWeightNew + ", " + Convert.ToString(dtColorStoneRow["CaratWeightNew"].ToString());

                                    _strStoneCaratWeightLen = _strStoneCaratWeightLen + ", " + Convert.ToString(dtColorStoneRow["CaratWeightLen"].ToString());
                                    _strStoneCaratWeightNewLen = _strStoneCaratWeightNewLen + ", " + Convert.ToString(dtColorStoneRow["CaratWeightNewLen"].ToString());

                                    if (dtColorStoneRow["CenterStone"].ToString().Trim() == "1")
                                    {
                                        _strStoneCaratWeightCenterStone = _strStoneCaratWeightCenterStone + ", " + Convert.ToString(dtColorStoneRow["CaratWeightNew"].ToString()) + "(center stone)";

                                        _strStoneCaratWeightCenterStoneLen = _strStoneCaratWeightCenterStoneLen + ", " + Convert.ToString(dtColorStoneRow["CaratWeightNewLen"].ToString()) + "(center stone)";
                                    }
                                    else
                                    {
                                        _strStoneCaratWeightCenterStone = _strStoneCaratWeightCenterStone + ", " + Convert.ToString(dtColorStoneRow["CaratWeightNew"].ToString());

                                        _strStoneCaratWeightCenterStoneLen = _strStoneCaratWeightCenterStoneLen + ", " + Convert.ToString(dtColorStoneRow["CaratWeightNewLen"].ToString());
                                    }
                                }

                                if (StoneQty.Trim() == string.Empty)
                                {
                                    StoneQty = Convert.ToString(dtColorStoneRow["StoneQTy"].ToString());
                                    StoneQtyLen = Convert.ToString(dtColorStoneRow["StoneQTyLen"].ToString());
                                }
                                else
                                {
                                    decimal x = Convert.ToDecimal(StoneQty) + Convert.ToDecimal(dtColorStoneRow["StoneQTy"].ToString());
                                    StoneQty = Convert.ToString(Decimal.Round(x, 1));

                                    decimal xLen = Convert.ToDecimal(StoneQty) + Convert.ToDecimal(dtColorStoneRow["StoneQTyLen"].ToString());
                                    StoneQtyLen = Convert.ToString(Decimal.Round(xLen, 1));
                                }
                            }
                        }



                    }
                }
            }

            if (EternityStatus == 1)
            {
                DataTable dtEternity = new DataTable();
                dtEternity = objComFun.GetEternityStoneCarat_Sizes(2, ProductID, CaretWeight, LengthSize);

                if (dtEternity.Rows.Count > 0)
                {
                    _dataRow["StoneCaret"] = dtEternity.Rows[0]["QTY_CW_STRING1"].ToString();
                    _dataRow["Stonemm"] = dtEternity.Rows[0]["QTY_RINGSIZE"].ToString();
                    _dataRow["StoneQty"] = dtEternity.Rows[0]["GRND_TOT_QTY"].ToString();

                }
            }
            else
            {

                if (Centerdiamond == "1")
                {
                    _dataRow["StoneCaret"] = StrStoneCaratWeight_Rapnet;
                    _dataRow["Stonemm"] = StrStoneSize_Rapnet;
                    //_dataRow["StoneCaret"] = _strStoneCaratWeight;
                    //_dataRow["Stonemm"] = size;
                    _dataRow["StoneQty"] = StoneQty;

                }
                else
                {
                    _dataRow["StoneCaret"] = _strStoneCaratWeight;
                    _dataRow["Stonemm"] = size;
                    _dataRow["StoneQty"] = StoneQty;
                }
            }
            dtStoneCaret.Rows.Add(_dataRow);
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Write(DataTableToJSONWithJavaScriptSerializer(dtStoneCaret));

        }
        catch (Exception ex)
        {
            _strError = ex.ToString();
        }
    }

    [WebMethod(Description = "Product Configurations details")]
    public void GetProductsConfigurationsNew(string ProductID)
    {

        try
        {
            DataSet ds = objComFun.GetProductsConfigurations(ProductID);
            if (ds.Tables[0].Rows.Count != 0)
            {
                jsonString = Serialize(ds);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.Write(jsonString);

            }
            else
            {
                Context.Response.Write("{Status:false}");
            }


        }
        catch (Exception ex)
        {

            Context.Response.Write("{Status:false}");

        }


    }
}