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

/// <summary>
/// Summary description for tblUsersHelper
/// </summary>
/// 
namespace tblUsers
{
    public class tblUsersHelper
    {

        private DataBase db;
        SqlParameter[] param;
        DataSet ds;
        public tblUsersHelper()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public int InsertUsers(tblUsers otblUsers,string mode)
        {
            int status = -1;
            try
            {
                if (object.Equals(db, null))
                {
                    db = new DataBase();
                }
                param = new SqlParameter[8];
                param[0] = db.MakeInParameter("@UserID", otblUsers.UserID);
                param[1] = db.MakeInParameter("@Name", SqlDbType.NVarChar, 50, otblUsers.Name);
                param[2] = db.MakeInParameter("@Password", SqlDbType.NVarChar, 20, otblUsers.Password);
                param[3] = db.MakeInParameter("@FirstLogin", SqlDbType.DateTime, 8, otblUsers.FirstLogin);
                param[4] = db.MakeInParameter("@LastLogin", SqlDbType.DateTime, 8, otblUsers.LastLogin);
                param[5] = db.MakeInParameter("@Creation", SqlDbType.DateTime, 8, otblUsers.Creation);
                param[6] = db.MakeInParameter("@Mode", SqlDbType.NVarChar, 6, mode);
                param[7] = db.MakeInParameter("@DomainName", SqlDbType.NVarChar,50,otblUsers.DomainName);
                // param[5] = db.MakeInParameter("@AccessLevelID",otblUsers.AccessLevelID);
                //  param[6] = db.MakeOutParameter("@UseridOut");
                db.RunProcedure("P_InsertUser", param);
                // userid = (int)param[6].Value;
                return status;
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


        public int InsertUsersMasterMerchant(tblUsers otblUsers, string mode)
        {
            int status = -1;
            try
            {
                if (object.Equals(db, null))
                {
                    db = new DataBase();
                }
                param = new SqlParameter[16];
                param[0] = db.MakeInParameter("@UserID", otblUsers.UserID);
                param[1] = db.MakeInParameter("@Name", SqlDbType.NVarChar, 50, otblUsers.Name);
                param[2] = db.MakeInParameter("@Password", SqlDbType.NVarChar, 20, otblUsers.Password);
                param[3] = db.MakeInParameter("@FirstLogin", SqlDbType.DateTime, 8, otblUsers.FirstLogin);
                param[4] = db.MakeInParameter("@LastLogin", SqlDbType.DateTime, 8, otblUsers.LastLogin);
                param[5] = db.MakeInParameter("@Creation", SqlDbType.DateTime, 8, otblUsers.Creation);
                param[6] = db.MakeInParameter("@Mode", SqlDbType.NVarChar, 6, mode);
                param[7] = db.MakeInParameter("@DomainName", SqlDbType.NVarChar, 100, otblUsers.DomainName);
                param[8] = db.MakeInParameter("@MasterMerchant", SqlDbType.Bit, 1, otblUsers.MasterMerchant);
                param[9] = db.MakeInParameter("@Merchant", SqlDbType.Bit, 1, otblUsers.Merchant);
                param[10] = db.MakeInParameter("@EndCustomer", SqlDbType.Bit, 1, otblUsers.EndCustomer);
                //param[11] = db.MakeInParameter("@DomainOwner", SqlDbType.Bit, 1, otblUsers.DomainOwner);
                param[11] = db.MakeInParameter("@Approve", SqlDbType.Bit, 1, otblUsers.Approve);
                param[12] = db.MakeInParameter("@PaymentTerms", SqlDbType.NVarChar, 10, otblUsers.PaymentTerms);
                param[13] = db.MakeInParameter("@PremiumCharges", SqlDbType.Decimal, 8, otblUsers.PremiumCharges);
                param[14] = db.MakeInParameter("@MasterMerchantID", otblUsers.MasterMerchantID);
                param[15] = db.MakeInParameter("@MerchantID", otblUsers.MerchantID);


                // param[5] = db.MakeInParameter("@AccessLevelID",otblUsers.AccessLevelID);
                //  param[6] = db.MakeOutParameter("@UseridOut");

                db.RunProcedure("P_InsertUser_Merchant", param);

                // userid = (int)param[6].Value;
                return status;
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


        public int UpdateUsersMasterMerchant(tblUsers otblUsers, string mode)
        {
            int status = -1;
            try
            {
                if (object.Equals(db, null))
                {
                    db = new DataBase();
                }
                param = new SqlParameter[13];
                param[0] = db.MakeInParameter("@UserID", otblUsers.UserID);
                param[1] = db.MakeInParameter("@Name", SqlDbType.NVarChar, 50, otblUsers.Name);
                param[2] = db.MakeInParameter("@Password", SqlDbType.NVarChar, 20, otblUsers.Password);
                param[3] = db.MakeInParameter("@DomainName", SqlDbType.NVarChar, 100, otblUsers.DomainName);
                param[4] = db.MakeInParameter("@MasterMerchant", SqlDbType.Bit, 1, otblUsers.MasterMerchant);
                param[5] = db.MakeInParameter("@Merchant", SqlDbType.Bit, 1, otblUsers.Merchant);
                param[6] = db.MakeInParameter("@EndCustomer", SqlDbType.Bit, 1, otblUsers.EndCustomer);
                //param[7] = db.MakeInParameter("@DomainOwner", SqlDbType.Bit, 1, otblUsers.DomainOwner);
                param[7] = db.MakeInParameter("@Approve", SqlDbType.Bit, 1, otblUsers.Approve);
                param[8] = db.MakeInParameter("@PaymentTerms", SqlDbType.NVarChar, 10, otblUsers.PaymentTerms);
                param[9] = db.MakeInParameter("@PremiumCharges", SqlDbType.Decimal, 8, otblUsers.PremiumCharges);
                param[10] = db.MakeInParameter("@MasterMerchantID", otblUsers.MasterMerchantID);
                param[11] = db.MakeInParameter("@MerchantID", otblUsers.MerchantID);
                param[12] = db.MakeInParameter("@Mode", SqlDbType.NVarChar, 6, mode);

                db.RunProcedure("P_InsertUser_Merchant", param);

                return status;
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

        public string GetUserID(string username, string password)
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
                param = new SqlParameter[3];
                param[0] = db.MakeInParameter("@UserName", SqlDbType.NVarChar, 50, username);
                param[1] = db.MakeInParameter("@Password", SqlDbType.NVarChar, 20, password);
                param[2] = db.MakeOutParameter("@UserID", SqlDbType.UniqueIdentifier, 16);
                db.RunProcedure("P_GetUserID", param);
                UserID = param[2].Value.ToString().Trim();

                return UserID;
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
            return UserID;
        }

        public string GetUserIDForCom(string username, string password)
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
                param = new SqlParameter[3];
                param[0] = db.MakeInParameter("@UserName", SqlDbType.NVarChar, 50, username);
                param[1] = db.MakeInParameter("@Password", SqlDbType.NVarChar, 20, password);
                param[2] = db.MakeOutParameter("@UserID", SqlDbType.UniqueIdentifier, 16);
                db.RunProcedure("P_GetUserIDForCom", param);
                UserID = param[2].Value.ToString().Trim();

                return UserID;
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
            return UserID;
        }



        public string GetUserID(string username)
        {
            string UserID = "";
            try
            {
            
            string password = String.Empty;
            if (object.Equals(db, null))
            {
                db = new DBComponent.DataBase();
            }
            if (object.Equals(ds, null))
            {
                ds = new DataSet();
            }
            param = new SqlParameter[3];
            param[0] = db.MakeInParameter("@UserName", SqlDbType.NVarChar, 50, username);
            param[1] = db.MakeInParameter("@Password", SqlDbType.NVarChar, 20, password);
            param[2] = db.MakeOutParameter("@UserID", SqlDbType.UniqueIdentifier, 16);
            db.RunProcedure("P_GetUserID", param);
            UserID = param[2].Value.ToString().Trim();
            
            return UserID;
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
            return UserID;
        }


        private void ResetAll()
        {
            param = null;
            db = null;
        }
    }
}