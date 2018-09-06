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
using DBComponent;
namespace tblCustomers
{

    /// <summary>
    /// Summary description for tblCustomersHelper
    /// </summary>
    public class tblCustomersHelper
    {
        private DataBase db;
        SqlParameter[] param;
        DataSet ds;
        public tblCustomersHelper()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public int InsertCustomer(tblCustomers otblCustomers, string mode)
        {
            int status = -1;
            try
            {
                if (object.Equals(db, null))
                {
                    db = new DataBase();
                }
                param = new SqlParameter[20];
                if (otblCustomers.customerID == "-1")
                {
                    param[0] = db.MakeInParameter("@customerID", SqlDbType.UniqueIdentifier, 64, null);
                }
                else
                {
                    param[0] = db.MakeInParameter("@customerID", otblCustomers.customerID);
                }                      
                param[1] = db.MakeInParameter("@firstName", SqlDbType.VarChar, 50, otblCustomers.firstName);
                param[2] = db.MakeInParameter("@surName", SqlDbType.VarChar, 50, otblCustomers.surName);
                param[3] = db.MakeInParameter("@company", SqlDbType.VarChar, 100, otblCustomers.company);
                param[4] = db.MakeInParameter("@street", SqlDbType.VarChar, 100, otblCustomers.street);
                param[5] = db.MakeInParameter("@suburb", SqlDbType.VarChar, 50, otblCustomers.suburb);
                param[6] = db.MakeInParameter("@postCode", SqlDbType.VarChar, 15, otblCustomers.postCode);
                param[7] = db.MakeInParameter("@state", SqlDbType.VarChar, 50, otblCustomers.state);
                param[8] = db.MakeInParameter("@country", SqlDbType.VarChar, 50, otblCustomers.country);
                param[9] = db.MakeInParameter("@phone", SqlDbType.VarChar, 50, otblCustomers.phone);
                param[10] = db.MakeInParameter("@fax", SqlDbType.VarChar, 50, otblCustomers.fax);
                param[11] = db.MakeInParameter("@mobilePhone", SqlDbType.VarChar, 50, otblCustomers.mobilePhone);
                param[12] = db.MakeInParameter("@email", SqlDbType.VarChar, 128, otblCustomers.email);
                param[13] = db.MakeInParameter("@website", SqlDbType.VarChar, 128, otblCustomers.website);
                param[14] = db.MakeInParameter("@customerPassword", SqlDbType.VarChar, 15, otblCustomers.customerPassword);
                param[15] = db.MakeInParameter("@newsletter", SqlDbType.Bit, 1, otblCustomers.newsletter);
                param[16] = db.MakeInParameter("@membershipType", SqlDbType.Int, 4, otblCustomers.membershipType);
                param[17] = db.MakeInParameter("@membershipNo", SqlDbType.VarChar, 50, otblCustomers.membershipNo);
                param[18] = db.MakeInParameter("@Mode", SqlDbType.VarChar, 10, mode);
                param[19] = db.MakeOutParameter("@Status", SqlDbType.Int, 4);
                db.RunProcedure("P_InsertCustomer", param);
                status = (int)param[19].Value;
            }
            catch (Exception ex)
            {
                string test = ex.ToString();
                string test1 = ex.Message;
            }
            finally
            {
                ResetAll();
            }
            return status;
        }

        public DataSet GetCustomer(string emailID, string password)
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
            param[0] = db.MakeInParameter("@email", SqlDbType.NVarChar, 128, emailID);
            param[1] = db.MakeInParameter("@customerPassword", SqlDbType.NVarChar, 15, password);
            db.RunProcedure("P_GetCustomer", param, out ds);
            ResetAll();
            return ds;
        }

        public DataSet GetCustomer(int customerID)
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
            param[0] = db.MakeInParameter("@customerID", SqlDbType.Int, 4, customerID);
            db.RunProcedure("P_GetCustomerBYID", param, out ds);
            ResetAll();
            return ds;
        }

        public string GetCustomerID(string emailID, string password)
        {
            string customerID = "";
            if (object.Equals(db, null))
            {
                db = new DataBase();
            }
            param = new SqlParameter[3];
            param[0] = db.MakeInParameter("@email",SqlDbType.NVarChar,128,emailID);
            param[1] = db.MakeInParameter("@customerPassword", SqlDbType.NVarChar, 15, password);
            param[2] = db.MakeOutParameter("@customerID",SqlDbType.UniqueIdentifier,16);
            db.RunProcedure("P_GetCustomerID", param);
            string test = param[2].Value.ToString();
            customerID = param[2].Value.ToString().Trim();
            ResetAll();
            return customerID;
        }

        public string GetCustomerPassword(string emailID)
        {
            string cPassword = string.Empty;
            if (object.Equals(db, null))
            {
                db = new DataBase();
            }
            param = new SqlParameter[2];
            param[0] = db.MakeInParameter("@email", SqlDbType.NVarChar, 128, emailID.Trim());
            param[1] = db.MakeOutParameter("@password", SqlDbType.VarChar, 50);
            db.RunProcedure("P_GetCustomerPassword", param);
            cPassword = param[1].Value.ToString();
            ResetAll();
            return cPassword;
        }


        /// <summary>
        /// This method is used to verify the customer credentials
        /// </summary>
        /// <param name="emailID">EmailID of customer</param>
        /// <param name="password">Password of customer</param>
        /// <returns>Return 1 if exists else return 0</returns>
        public int VerifyCustomer(string emailID, string password)
        {
            int IsExist = -1;
            if (object.Equals(db, null))
            {
                db = new DataBase();
            }
            param = new SqlParameter[3];
            param[0] = db.MakeInParameter("@email", SqlDbType.NVarChar, 128, emailID);
            param[1] = db.MakeInParameter("@customerPassword", SqlDbType.NVarChar, 15, password);
            param[2] = db.MakeOutParameter("@IsExist", SqlDbType.Int, 4);
            db.RunProcedure("P_VerifyCustomer", param, out ds);
            IsExist = (int)param[2].Value;
            ResetAll();
            return IsExist;
        }
        public DataSet SearchCustomers(string custName, int filter)
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
            param[0] = db.MakeInParameter("@custName", SqlDbType.VarChar, 100, custName);
            db.RunProcedure("P_SearchCustomers", param, out ds);
            ResetAll();
            return ds;
        }
        public void DeleteCustomer(string customerID)
        {
            if (object.Equals(db, null))
            {
                db = new DataBase();
            }
            param = new SqlParameter[1];
            param[0] = db.MakeInParameter("@customerID", SqlDbType.VarChar, 100, customerID);
            db.RunProcedure("P_DeleteCustomer", param);
            ResetAll();
        }
        private void ResetAll()
        {
            param = null;
            db = null;
        }
    }
}