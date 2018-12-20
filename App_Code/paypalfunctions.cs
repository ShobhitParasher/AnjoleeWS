using System;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using System.Data;
using System.Configuration;
using System.Web;

/// <summary>
/// Summary description for NVPAPICaller
/// </summary>
public class NVPAPICaller
{
    //private static readonly ILog log = LogManager.GetLogger(typeof(NVPAPICaller));

    private string pendpointurl = "https://api-3t.paypal.com/nvp";
    private string host = "www.paypal.com";
    private const string CVV2 = "CVV2";

    //Flag that determines the PayPal environment (live or sandbox)
    //private const bool bSandbox = false;
    private const bool livePaypal = true;

    private const string SIGNATURE = "SIGNATURE";
    private const string PWD = "PWD";
    private const string ACCT = "ACCT";

    //Replace <API_USERNAME> with your API Username
    //Replace <API_PASSWORD> with your API Password
    //Replace <API_SIGNATURE> with your Signature
    //public string APIUsername = "sharma_1311777231_biz_api1.rediffmail.com";
    //private string APIPassword = "6BDJLD9MHJK2L9WN";
    //private string APISignature = "AFcWxV21C7fd0v3bYYYRCpSSRl31Axyp29EDlxuEn-sVy2-KXfFdpeo4";
    public string APIUsername = "nir_api1.anjolee.com";
    private string APIPassword = "RQF7HHJXY33GBLTH";
    private string APISignature = "AFcWxV21C7fd0v3bYYYRCpSSRl31AgZzUA3AiGvj5qOe.M1uGxew4ObS";
    private string Subject = "";
    private string BNCode = "PP-ECWizard";

    //HttpWebRequest Timeout specified in milliseconds 
    private const int Timeout = 25000;
    private static readonly string[] SECURED_NVPS = new string[] { ACCT, CVV2, SIGNATURE, PWD };

    DBComponent.CommonFunctions objComFun = new DBComponent.CommonFunctions();
    /// <summary>
    /// Sets the API Credentials
    /// </summary>
    /// <param name="Userid"></param>
    /// <param name="Pwd"></param>
    /// <param name="Signature"></param>
    /// <returns></returns>
    public void SetCredentials(string Userid, string Pwd, string Signature)
    {
        APIUsername = Userid;
        APIPassword = Pwd;
        APISignature = Signature;
    }

    /// <summary>
    /// ShortcutExpressCheckout: The method that calls SetExpressCheckout API
    /// </summary>
    /// <param name="amt"></param>
    /// <param ref name="token"></param>
    /// <param ref name="retMsg"></param>
    /// <returns></returns>
    public bool ShortcutExpressCheckout(string amt, string DESC, string CUSTOM, ref string token, ref string retMsg)
    {
        //string host = "www.paypal.com";
        //if (bSandbox)
        //{
        //    pendpointurl = "https://api-3t.sandbox.paypal.com/nvp";
        //    host = "www.sandbox.paypal.com";
        //}

        if (livePaypal)
        {
            pendpointurl = "https://api-3t.paypal.com/nvp";
            host = "www.paypal.com";
        }

        // For Staging Server
          //  string returnURL = "http://23.253.29.72:8090/confirmPaypal.html";
          //  string cancelURL = "http://23.253.29.72:8090/cancelPaypal.html";

        // For Live Server
        string returnURL = "http://m.anjolee.com/confirmPaypal.html";
        string cancelURL = "http://m.anjolee.com/cancelPaypal.html";

        NVPCodec encoder = new NVPCodec();
        encoder["METHOD"] = "SetExpressCheckout";
        encoder["RETURNURL"] = returnURL;
        encoder["CANCELURL"] = cancelURL;
        encoder["AMT"] = amt;
        encoder["DESC"] = DESC;
        encoder["CUSTOM"] = CUSTOM;
        //encoder["ITEM_NAME"] = "Test Item";

        encoder["PAYMENTACTION"] = "Sale";
        encoder["CURRENCYCODE"] = "USD";
        ServicePointManager.Expect100Continue = true;
       // System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
        string pStrrequestforNvp = encoder.Encode();
        string pStresponsenvp = HttpCall(pStrrequestforNvp);

        NVPCodec decoder = new NVPCodec();
        decoder.Decode(pStresponsenvp);

        string strAck = decoder["ACK"].ToLower();
        if (strAck != null && (strAck == "success" || strAck == "successwithwarning"))
        {
            token = decoder["TOKEN"];

            string ECURL = "https://" + host + "/cgi-bin/webscr?cmd=_express-checkout" + "&token=" + token;

            retMsg = ECURL;
            return true;
        }
        else
        {
            retMsg = "ErrorCode=" + decoder["L_ERRORCODE0"] + "&" +
                "Desc=" + decoder["L_SHORTMESSAGE0"] + "&" +
                "Desc2=" + decoder["L_LONGMESSAGE0"];

            return false;
        }
    }

    /// <summary>
    /// MarkExpressCheckout: The method that calls SetExpressCheckout API, invoked from the 
    /// Billing Page EC placement
    /// </summary>
    /// <param name="amt"></param>
    /// <param ref name="token"></param>
    /// <param ref name="retMsg"></param>
    /// <returns></returns>
    public bool MarkExpressCheckout(string amt,
                        string shipToName, string shipToStreet, string shipToStreet2,
                        string shipToCity, string shipToState, string shipToZip,
                        string shipToCountryCode, ref string token, ref string retMsg)
    {
        //string host = "www.paypal.com";
        //if (bSandbox)
        //{
        //    pendpointurl = "https://api-3t.sandbox.paypal.com/nvp";
        //    host = "www.sandbox.paypal.com";
        //}

        if (livePaypal)
        {
            pendpointurl = "https://api-3t.paypal.com/nvp";
            host = "www.paypal.com";
        }

        //string returnURL = "http://74.54.137.138:2025/confirm.aspx";
        //string cancelURL = "http://74.54.137.138:2025/cancel.aspx";

        string returnURL = "http://m.anjolee.com/confirmPaypal.html";
        string cancelURL = "http://m.anjolee.com/cancelPaypal.html";

        NVPCodec encoder = new NVPCodec();
        encoder["METHOD"] = "SetExpressCheckout";
        encoder["RETURNURL"] = returnURL;
        encoder["CANCELURL"] = cancelURL;
        encoder["AMT"] = amt;
        encoder["PAYMENTACTION"] = "Sale";
        encoder["CURRENCYCODE"] = "USD";

        //Optional Shipping Address entered on the merchant site
        encoder["SHIPTONAME"] = shipToName;
        encoder["SHIPTOSTREET"] = shipToStreet;
        encoder["SHIPTOSTREET2"] = shipToStreet2;
        encoder["SHIPTOCITY"] = shipToCity;
        encoder["SHIPTOSTATE"] = shipToState;
        encoder["SHIPTOZIP"] = shipToZip;
        encoder["SHIPTOCOUNTRYCODE"] = shipToCountryCode;


        string pStrrequestforNvp = encoder.Encode();
        string pStresponsenvp = HttpCall(pStrrequestforNvp);

        NVPCodec decoder = new NVPCodec();
        decoder.Decode(pStresponsenvp);

        string strAck = decoder["ACK"].ToLower();
        if (strAck != null && (strAck == "success" || strAck == "successwithwarning"))
        {
            token = decoder["TOKEN"];

            string ECURL = "https://" + host + "/cgi-bin/webscr?cmd=_express-checkout" + "&token=" + token;

            retMsg = ECURL;
            return true;
        }
        else
        {
            retMsg = "ErrorCode=" + decoder["L_ERRORCODE0"] + "&" +
                "Desc=" + decoder["L_SHORTMESSAGE0"] + "&" +
                "Desc2=" + decoder["L_LONGMESSAGE0"];

            return false;
        }
    }



    /// <summary>
    /// GetShippingDetails: The method that calls SetExpressCheckout API, invoked from the 
    /// Billing Page EC placement
    /// </summary>
    /// <param name="token"></param>
    /// <param ref name="retMsg"></param>
    /// <returns></returns>
    public bool GetShippingDetails(string token, ref string PayerId, ref string ShippingAddress, ref string retMsg)
    {
        try
        {
            //if (bSandbox)
            //{
            //    pendpointurl = "https://api-3t.sandbox.paypal.com/nvp";
            //}

            if (livePaypal)
            {
                pendpointurl = "https://api-3t.paypal.com/nvp";
                host = "www.paypal.com";
            }

            NVPCodec encoder = new NVPCodec();
            encoder["METHOD"] = "GetExpressCheckoutDetails";
            encoder["TOKEN"] = token;
            ServicePointManager.Expect100Continue = true;
            //System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            string pStrrequestforNvp = encoder.Encode();
            string pStresponsenvp = HttpCall(pStrrequestforNvp);

            NVPCodec decoder = new NVPCodec();
            decoder.Decode(pStresponsenvp);

            string strAck = decoder["ACK"].ToLower();
            if (strAck != null && (strAck == "success" || strAck == "successwithwarning"))
            {
                //////////////ShippingAddress = "<table><tr>";
                //////////////ShippingAddress += "<td> First Name </td><td>" + decoder["FIRSTNAME"] + "</td></tr>";
                //////////////ShippingAddress += "<td> Last Name </td><td>" + decoder["LASTNAME"] + "</td></tr>";
                //////////////ShippingAddress += "<td colspan='2'> Shipping Address</td></tr>";
                //////////////ShippingAddress += "<td> Name </td><td>" + decoder["SHIPTONAME"] + "</td></tr>";
                //////////////ShippingAddress += "<td> Street1 </td><td>" + decoder["SHIPTOSTREET"] + "</td></tr>";
                //////////////ShippingAddress += "<td> Street2 </td><td>" + decoder["SHIPTOSTREET2"] + "</td></tr>";
                //////////////ShippingAddress += "<td> City </td><td>" + decoder["SHIPTOCITY"] + "</td></tr>";
                //////////////ShippingAddress += "<td> State </td><td>" + decoder["SHIPTOSTATE"] + "</td></tr>";
                //////////////ShippingAddress += "<td> Zip </td><td>" + decoder["SHIPTOZIP"] + "</td>";
                //////////////ShippingAddress += "</tr>";
                ShippingAddress = decoder["SHIPTONAME"] + "|";
                ShippingAddress += decoder["SHIPTONAME1"] + "|";
                ShippingAddress += decoder["SHIPTOSTREET"] + "|";
                ShippingAddress += decoder["SHIPTOSTREET2"] + "|";
                ShippingAddress += decoder["SHIPTOCITY"] + "|";
                ShippingAddress += decoder["SHIPTOSTATE"] + "|";
                ShippingAddress += decoder["SHIPTOCOUNTRYCODE"] + "|";
                ShippingAddress += decoder["EMAIL"] + "|";
                ShippingAddress += decoder["SHIPTOZIP"] + "|";
                ShippingAddress += decoder["SHIPTOPHONENUM"];
                PayerId = decoder["payerId"].ToString();
                return true;
            }
            else
            {
                retMsg = "ErrorCode=" + decoder["L_ERRORCODE0"] + "&" +
                    "Desc=" + decoder["L_SHORTMESSAGE0"] + "&" +
                    "Desc2=" + decoder["L_LONGMESSAGE0"];

                return false;
            }
        }

        catch(Exception ex)
        {

            objComFun.ErrorLog(ex.Source, ex.Message, ex.TargetSite.ToString(), ex.StackTrace, "GetShippingDetails");
            return false;
        }
    }


    /// <summary>
    /// ConfirmPayment: The method that calls SetExpressCheckout API, invoked from the 
    /// Billing Page EC placement
    /// </summary>
    /// <param name="token"></param>
    /// <param ref name="retMsg"></param>
    /// <returns></returns>
    public bool ConfirmPayment(string finalPaymentAmount, string token, string PayerId, string DESC, string CUSTOM, ref NVPCodec decoder, ref string retMsg)
    {
        try
        {

            //if (bSandbox)
            //{
            //    pendpointurl = "https://api-3t.sandbox.paypal.com/nvp";
            //}

            if (livePaypal)
            {
                pendpointurl = "https://api-3t.paypal.com/nvp";
                host = "www.paypal.com";
            }

            NVPCodec encoder = new NVPCodec();
            encoder["METHOD"] = "DoExpressCheckoutPayment";
            encoder["TOKEN"] = token;
            encoder["PAYMENTACTION"] = "Sale";
            encoder["PAYERID"] = PayerId;
            encoder["AMT"] = finalPaymentAmount;
            encoder["DESC"] = DESC;
            encoder["CUSTOM"] = CUSTOM;
            ServicePointManager.Expect100Continue = true;
            //System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            string pStrrequestforNvp = encoder.Encode();
            string pStresponsenvp = HttpCall(pStrrequestforNvp);

            decoder = new NVPCodec();
            decoder.Decode(pStresponsenvp);

            string strAck = decoder["ACK"].ToLower();
            if (strAck != null && (strAck == "success" || strAck == "successwithwarning"))
            {
                return true;
            }
            else
            {
                retMsg = "ErrorCode=" + decoder["L_ERRORCODE0"] + "&" +
                    "Desc=" + decoder["L_SHORTMESSAGE0"] + "&" +
                    "Desc2=" + decoder["L_LONGMESSAGE0"];

                return false;
            }
        }
        catch(Exception ex)
        {
            objComFun.ErrorLog(ex.Source, ex.Message, ex.TargetSite.ToString(), ex.StackTrace, "ConfirmPayment");
            return false;

        }
    }


    /// <summary>
    /// HttpCall: The main method that is used for all API calls
    /// </summary>
    /// <param name="NvpRequest"></param>
    /// <returns></returns>
    public string HttpCall(string NvpRequest) //CallNvpServer
    {
        try
        {
            string url = pendpointurl;
            NVPCodec obj1 = new NVPCodec();
            //To Add the credentials from the profile
            string strPost = NvpRequest + "&" + buildCredentialsNVPString();
            strPost = strPost + "&BUTTONSOURCE=" + HttpUtility.UrlEncode(BNCode);

            HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(url);
            objRequest.Timeout = Timeout;
            objRequest.Method = "POST";
            objRequest.ContentLength = strPost.Length;

            try
            {
                using (StreamWriter myWriter = new StreamWriter(objRequest.GetRequestStream()))
                {
                    myWriter.Write(strPost);
                }
            }
            catch (Exception e)
            {
                /*
                if (log.IsFatalEnabled)
                {
                    log.Fatal(e.Message, this);
                }*/
                objComFun.ErrorLog(e.Source, e.Message, e.TargetSite.ToString(), e.StackTrace, "HttpCall");
            }

            //Retrieve the Response returned from the NVP API call to PayPal
            HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();
            string result;
            using (StreamReader sr = new StreamReader(objResponse.GetResponseStream()))
            {
                result = sr.ReadToEnd();
            }

            //Logging the response of the transaction
            /* if (log.IsInfoEnabled)
             {
                 log.Info("Result :" +
                           " Elapsed Time : " + (DateTime.Now - startDate).Milliseconds + " ms" +
                          result);
             }
             */
            return result;
        }
        catch (Exception e)
        {
           
            objComFun.ErrorLog(e.Source, e.Message, e.TargetSite.ToString(), e.StackTrace, "HttpCall1");
            return e.ToString();
        }
    }

    /// <summary>
    /// Credentials added to the NVP string
    /// </summary>
    /// <param name="profile"></param>
    /// <returns></returns>
    private string buildCredentialsNVPString()
    {
        NVPCodec codec = new NVPCodec();

        if (!IsEmpty(APIUsername))
            codec["USER"] = APIUsername;

        if (!IsEmpty(APIPassword))
            codec[PWD] = APIPassword;

        if (!IsEmpty(APISignature))
            codec[SIGNATURE] = APISignature;

        if (!IsEmpty(Subject))
            codec["SUBJECT"] = Subject;

        codec["VERSION"] = "53.0";

        return codec.Encode();
    }

    /// <summary>
    /// Returns if a string is empty or null
    /// </summary>
    /// <param name="s">the string</param>
    /// <returns>true if the string is not null and is not empty or just whitespace</returns>
    public static bool IsEmpty(string s)
    {
        return s == null || s.Trim() == string.Empty;
    }
}


public sealed class NVPCodec : NameValueCollection
{
    private const string AMPERSAND = "&";
    private const string EQUALS = "=";
    private static readonly char[] AMPERSAND_CHAR_ARRAY = AMPERSAND.ToCharArray();
    private static readonly char[] EQUALS_CHAR_ARRAY = EQUALS.ToCharArray();

    /// <summary>
    /// Returns the built NVP string of all name/value pairs in the Hashtable
    /// </summary>
    /// <returns></returns>
    public string Encode()
    {
        StringBuilder sb = new StringBuilder();
        bool firstPair = true;
        foreach (string kv in AllKeys)
        {
            string name = UrlEncode(kv);
            string value = UrlEncode(this[kv]);
            if (!firstPair)
            {
                sb.Append(AMPERSAND);
            }
            sb.Append(name).Append(EQUALS).Append(value);
            firstPair = false;
        }
        return sb.ToString();
    }

    /// <summary>
    /// Decoding the string
    /// </summary>
    /// <param name="nvpstring"></param>
    public void Decode(string nvpstring)
    {
        Clear();
        foreach (string nvp in nvpstring.Split(AMPERSAND_CHAR_ARRAY))
        {
            string[] tokens = nvp.Split(EQUALS_CHAR_ARRAY);
            if (tokens.Length >= 2)
            {
                string name = UrlDecode(tokens[0]);
                string value = UrlDecode(tokens[1]);
                Add(name, value);
            }
        }
    }

    private static string UrlDecode(string s) { return HttpUtility.UrlDecode(s); }
    private static string UrlEncode(string s) { return HttpUtility.UrlEncode(s); }

    #region Array methods
    public void Add(string name, string value, int index)
    {
        this.Add(GetArrayName(index, name), value);
    }

    public void Remove(string arrayName, int index)
    {
        this.Remove(GetArrayName(index, arrayName));
    }

    /// <summary>
    /// 
    /// </summary>
    public string this[string name, int index]
    {
        get
        {
            return this[GetArrayName(index, name)];
        }
        set
        {
            this[GetArrayName(index, name)] = value;
        }
    }

    private static string GetArrayName(int index, string name)
    {
        if (index < 0)
        {
            throw new ArgumentOutOfRangeException("index", "index can not be negative : " + index);
        }
        return name + index;
    }
    #endregion
}