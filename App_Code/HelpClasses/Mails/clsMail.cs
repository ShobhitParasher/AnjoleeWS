using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Net.Mail;
namespace tblOrders
{
    /// <summary>
    /// Summary description for clsMail
    /// </summary>
    public class clsMail
    {
        public clsMail()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private tblStore_Prefs.tblStorePrefsHelper otblStorePrefsHelper = null;
        DBComponent.CommonFunctions objData = new DBComponent.CommonFunctions();


        public string mailSmtp(string strToEmail, string strSubject, string strBody)
        {
            SmtpClient smtpClient = new SmtpClient();


            MailMessage message = new MailMessage();
            try
            {
                if (object.Equals(otblStorePrefsHelper, null))
                {
                    otblStorePrefsHelper = new tblStore_Prefs.tblStorePrefsHelper();
                }
                //DataSet ds = otblStorePrefsHelper.GetStorePrefs();
                string _strMailQuery = "SELECT  * FROM tblStore_Email_anjolee";
                DataSet ds = objData.ExecuteQueryReturnDataSet(_strMailQuery);
       
                string sHostName = String.Empty;
                string strEmailFrom = String.Empty;
                string sFromName = "Anjolee.com";
               
                sHostName = ds.Tables[0].Rows[0]["emailSystemServer"].ToString();
                strEmailFrom = ds.Tables[0].Rows[0]["emailFromAddress"].ToString();
                
                MailAddress fromAddress = new MailAddress(strEmailFrom, sFromName);

                // You can specify the host name or ipaddress of your server
                // Default in IIS will be localhost 
                smtpClient.Host = sHostName;
                //Default port will be 25
                smtpClient.Port = 25;
                //From address will be given as a MailAddress Object
                message.From = fromAddress;
                // To address collection of MailAddress
                message.To.Add(strToEmail);
                message.Subject = strSubject;
                string ccMailIDs = string.Empty;
                bool ccStaff = bool.Parse(ds.Tables[0].Rows[0]["ccStaff"].ToString());
                if (ccStaff == true)
                {
                    ccMailIDs = ds.Tables[0].Rows[0]["staffEmail1"].ToString().Trim();
                    message.CC.Add(new MailAddress(ccMailIDs));
                    if (ds.Tables[0].Rows[0]["staffEmail2"].ToString().Trim() != string.Empty)
                    {
                        ccMailIDs = ds.Tables[0].Rows[0]["staffEmail2"].ToString().Trim();
                        message.CC.Add(new MailAddress(ccMailIDs));
                    }
                    if (ds.Tables[0].Rows[0]["staffEmail3"].ToString().Trim() != string.Empty)
                    {
                        ccMailIDs = ds.Tables[0].Rows[0]["staffEmail3"].ToString().Trim();
                        message.CC.Add(new MailAddress(ccMailIDs));
                    }
                }
                //Body can be Html or text format
                //Specify true if it  is html message
                message.IsBodyHtml = true;
                // Message body content
                message.Body = strBody;

                //message.Headers.Add("X-Organization", "My Company LLC");

                // Send SMTP mail
                smtpClient.Send(message);
                return "Success";
            }

            catch (Exception ex)
            {
                string s = ex.Message;
                return "Falure: " + s;
            }
        }
    }
}