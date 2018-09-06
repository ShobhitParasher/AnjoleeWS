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

namespace tblOrders
{
    /// <summary>
    /// Summary description for tblOrdersShip
    /// </summary>
    public class tblOrdersShip
    {
        private string adm_Sqlquery;
        private SqlConnection con;
        public bool lusi;
        private string OrderNo;
        private string MailAddress;
        public tblOrdersShip()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public SqlConnection SetConnection()
        {
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Constr"].ToString());
            con.Open();
            return con;
        }

        public void CloseConnection()
        {
            con.Close();
            con.Dispose();
            con = null;
        }

        public bool lookUpItem(string tableName, string returnFieldName, string fieldName, int fieldValue)
        {

            DataTable dt;
            if (fieldValue != 0)
            {
                adm_Sqlquery = "SELECT " + returnFieldName + " FROM " + tableName + " WHERE " + fieldName + "=" + fieldValue + ";";
            }
            else
            {
                adm_Sqlquery = "SELECT " + returnFieldName + " FROM " + tableName + " WHERE " + fieldName + "='" + fieldValue + "';";
            }
            dt = GetDataTable(adm_Sqlquery);
            if (dt.Rows.Count > 0)
            {
                //lusi = dt.Rows
                //				while (dt.Rows.Count>i)
                //				{
                //					while (dt.Rows.Count >0)
                //					{
                //						lusi =  dt.Rows(i).item("requestConfirmTrackingNo");
                //                     
                //					}
                //					i = i+1;
                //				}

            }
            return lusi;

        }

        public DataTable GetDataTable(string sqlquery)
        {
            con = SetConnection();
            SqlDataAdapter da = new SqlDataAdapter(sqlquery, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            CloseConnection();
            return dt;
        }

        public DataSet GetDataSet(string sqlquery)
        {
            con = SetConnection();
            SqlDataAdapter da = new SqlDataAdapter(sqlquery, con);
            System.Data.DataSet ds = new DataSet();
            da.Fill(ds);
            CloseConnection();
            return ds;
        }
        public void CommandExec(string sql)
        {
            con = SetConnection();
            SqlCommand com = new SqlCommand(sql, con);
            com.ExecuteNonQuery();
            CloseConnection();
        }
        public DataSet GetDataSetforCustName(string OrderID)
        {
            con = SetConnection();
            SqlDataAdapter da = new SqlDataAdapter("asp_getCustomerInfo", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@intOrderId", SqlDbType.Int).Value = OrderID;
            System.Data.DataSet ds = new DataSet();
            da.Fill(ds);
            CloseConnection();
            return ds;
        }
        public void MailSender(string sql)
        {
            con = SetConnection();
            SqlCommand com = new SqlCommand(sql, con);
            SqlDataReader dr = com.ExecuteReader();
            dr.Read();
            OrderNo = dr["orderNo"].ToString();
            MailAddress = dr["email"].ToString();
            CloseConnection();
            getFromMail();
        }
        public void getFromMail()
        {
            con = SetConnection();
            string sqlquery = "SELECT confirmSubject, confirmEmailPartial, emailFromAddress, staffEmail1, staffEmail2, staffEmail3, ccStaff FROM tblStore_Email_anjolee";
            SqlCommand com = new SqlCommand(sqlquery, con);
            System.Data.SqlClient.SqlDataReader dr = com.ExecuteReader();
            dr.Read();
            string confirmSubject = dr["confirmSubject"].ToString();
            string confirmEmailPartial = dr["confirmEmailPartial"].ToString();
            string staffEmail1 = dr["staffEmail1"].ToString();
            string staffEmail2 = dr["staffEmail2"].ToString();
            string staffEmail3 = dr["staffEmail3"].ToString();
            string emailFromAddress = dr["emailFromAddress"].ToString();
            string ccStaff = dr["ccStaff"].ToString();

            string Body;
            Body = "<table><tr><td><IMG src=http://localhost/admin/adminImages/logo.gif></td></tr><tr><Td><font face='verdana' size='2'><p>" + confirmEmailPartial + "</font></td><Tr></table>";
            clsMail ObjMail = new clsMail();
            //ObjMail.mailSmtp(emailFromAddress, MailAddress, confirmSubject, Body);
            CloseConnection();

        }
        public static bool IsNumeric(string text)
        {
            try
            {
                int integerValue = Convert.ToInt32(text);
            }
            catch
            {
                return false;
            }
            return true;
        }

    }
}
