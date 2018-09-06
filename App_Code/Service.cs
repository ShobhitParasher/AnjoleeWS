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

        deliverDate = "Order Today, Delivery on " + deliverDate;
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

    [WebMethod]
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

    #endregion //----Common Methods -----//

    [WebMethod(Description = "Thumbnail page Search")]
    public void  ThumbnailSearch(string Price, string Caratweight, string StoneShapes, string Stonesettings, string JewelleryCollections, string GroupId, string NewArival, string SpecialOffer, string SortingBy)
    {
        try
        {
            DataSet dsthumbnailSearch = new DataSet();
            dsthumbnailSearch = objComFun.ProductNarrowSearch(Price, Caratweight, StoneShapes, Stonesettings, JewelleryCollections, GroupId, NewArival, SpecialOffer, SortingBy);

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

}