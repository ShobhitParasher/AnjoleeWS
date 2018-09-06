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
using System.Data.SqlTypes;

/// <summary>
/// Summary description for tblContactsHelper
/// </summary>

namespace tblContacts
{
    public class tblContactsHelper
    {
        private DataBase db;
        SqlParameter[] param;
        DataSet ds;
        DBComponent.CommonFunctions objComFun = new DBComponent.CommonFunctions();

        public tblContactsHelper()
        {
            
        }



        public DataSet GetContacts(string UID)
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
            param[0] = db.MakeInParameter("@UID", UID);
            db.RunProcedure("GetContacts", param, out ds);
            ResetAll();
            return ds;
        }
        public DataSet GetContacts1(string UID)
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
            param[0] = db.MakeInParameter("@UID", UID);
            db.RunProcedure("GetContacts_Order", param, out ds);
            ResetAll();
            return ds;
        }
        public int InsertContacts(tblContacts otblContacts, string mode)
        {
            int status = -1;
            try
            {
                if (object.Equals(db, null))
                {
                    db = new DataBase();
                }
                param = new SqlParameter[24];
                //if (otblContacts.ContactID == "-1")
                //{
                //    param[0] = db.MakeInParameter("@ContactID", SqlDbType.UniqueIdentifier, 64, null);
                //}
                //else
                //{
                    param[0] = db.MakeInParameter("@ContactID", otblContacts.ContactID);
                //}
                param[1] = db.MakeInParameter("@UserID", SqlGuid.Parse(otblContacts.UserID));
                
                param[2] = db.MakeInParameter("@Cell", SqlDbType.VarChar, 20, otblContacts.Cell);
                param[3] = db.MakeInParameter("@Fax", SqlDbType.VarChar, 20, otblContacts.Fax);
                param[4] = db.MakeInParameter("@Telephone1", SqlDbType.VarChar, 20, otblContacts.Telephone1);
                param[5] = db.MakeInParameter("@Telephone2", SqlDbType.VarChar, 20, otblContacts.Telephone2);
                param[6] = db.MakeInParameter("@Telephone3", SqlDbType.VarChar, 20, otblContacts.Telephone3);
                param[7] = db.MakeInParameter("@FirstName", SqlDbType.VarChar, 30, otblContacts.FirstName);
                param[8] = db.MakeInParameter("@MiddleName", SqlDbType.VarChar, 30, otblContacts.MiddleName);
                param[9] = db.MakeInParameter("@LastName", SqlDbType.VarChar, 30, otblContacts.LastName);
                param[10] = db.MakeInParameter("@Email", SqlDbType.VarChar, 70, otblContacts.Email);
                param[11] = db.MakeInParameter("@Street", SqlDbType.VarChar, 50, otblContacts.Street);
                param[12] = db.MakeInParameter("@City", SqlDbType.VarChar, 30, otblContacts.City);
                param[13] = db.MakeInParameter("@State", SqlDbType.VarChar, 30, otblContacts.State);
                param[14] = db.MakeInParameter("@Country", SqlDbType.VarChar, 30, otblContacts.Country);
                param[15] = db.MakeInParameter("@ZipCode", SqlDbType.VarChar, 16, otblContacts.ZipCode);
                param[16] = db.MakeInParameter("@Position", SqlDbType.VarChar, 30, otblContacts.Position);
                
                //param[18] = db.MakeInParameter("@CompanyID",otblContacts.CompanyID);
                //  param[19] = db.MakeOutParameter("@Status", SqlDbType.Int, 4);
                param[17] = db.MakeInParameter("@Mode", SqlDbType.NVarChar, 6, mode);
                param[18] = db.MakeInParameter("@Address2", SqlDbType.VarChar, 100, otblContacts.Address2);
                param[19]=db.MakeInParameter("@Newsletters",SqlDbType.Bit,1,otblContacts.Newsletters);
                
                param[20] = db.MakeInParameter("@DOB", SqlDbType.VarChar, 30,otblContacts.DOB.ToString());

                if (otblContacts.HearAbtUs != null)
                   param[21] = db.MakeInParameter("@HearAbtUs",SqlDbType.UniqueIdentifier,50, SqlGuid.Parse(otblContacts.HearAbtUs));
                else
                   param[21] = db.MakeInParameter("@HearAbtUs", SqlDbType.UniqueIdentifier, 50,DBNull.Value);

                param[22] = db.MakeInParameter("@AptUnitNo", SqlDbType.VarChar, 30, otblContacts.AptUnitNo);
                param[23] = db.MakeInParameter("@Gender", SqlDbType.VarChar, 10, otblContacts.Gender);

                db.RunProcedure("P_InsertContacts_Iphone", param);
                // status = (int)param[19].Value;
            }
            catch (Exception ex)
            {
                string test = ex.ToString();
                string test1 = ex.Message;
                objComFun.ErrorLog(ex.Source, ex.Message, ex.TargetSite.ToString(), ex.StackTrace, "InsertContacts");
            }
            finally
            {
                ResetAll();
            }
            return status;
        }



        public int InsertContactsMasterMerchant(tblContacts otblContacts, string mode)
        {
            int status = -1;
            try
            {
                if (object.Equals(db, null))
                {
                    db = new DataBase();
                }
                param = new SqlParameter[28];

                //if (otblContacts.ContactID == "-1")
                //{
                //    param[0] = db.MakeInParameter("@ContactID", SqlDbType.UniqueIdentifier, 64, null);
                //}
                //else
                //{
                //    param[0] = db.MakeInParameter("@ContactID", otblContacts.ContactID);
                //}

                param[0] = db.MakeInParameter("@ContactID", otblContacts.ContactID);
                param[1] = db.MakeInParameter("@UserID", otblContacts.UserID);
                param[2] = db.MakeInParameter("@Cell", SqlDbType.VarChar, 20, otblContacts.Cell);
                param[3] = db.MakeInParameter("@Fax", SqlDbType.VarChar, 20, otblContacts.Fax);
                param[4] = db.MakeInParameter("@Telephone1", SqlDbType.VarChar, 20, otblContacts.Telephone1);
                param[5] = db.MakeInParameter("@Telephone2", SqlDbType.VarChar, 20, otblContacts.Telephone2);
                param[6] = db.MakeInParameter("@Telephone3", SqlDbType.VarChar, 20, otblContacts.Telephone3);
                param[7] = db.MakeInParameter("@FirstName", SqlDbType.VarChar, 30, otblContacts.FirstName);
                param[8] = db.MakeInParameter("@MiddleName", SqlDbType.VarChar, 30, otblContacts.MiddleName);
                param[9] = db.MakeInParameter("@LastName", SqlDbType.VarChar, 30, otblContacts.LastName);
                param[10] = db.MakeInParameter("@Email", SqlDbType.VarChar, 70, otblContacts.Email);
                param[11] = db.MakeInParameter("@Street", SqlDbType.VarChar, 50, otblContacts.Street);
                param[12] = db.MakeInParameter("@City", SqlDbType.VarChar, 30, otblContacts.City);
                param[13] = db.MakeInParameter("@State", SqlDbType.VarChar, 30, otblContacts.State);
                param[14] = db.MakeInParameter("@Country", SqlDbType.VarChar, 30, otblContacts.Country);
                param[15] = db.MakeInParameter("@ZipCode", SqlDbType.VarChar, 16, otblContacts.ZipCode);
                param[16] = db.MakeInParameter("@Position", SqlDbType.VarChar, 30, otblContacts.Position);
                param[17] = db.MakeInParameter("@Mode", SqlDbType.NVarChar, 6, mode);
                param[18] = db.MakeInParameter("@Address2", SqlDbType.VarChar, 100, otblContacts.Address2);
                param[19] = db.MakeInParameter("@Newsletters", SqlDbType.Bit, 1, otblContacts.Newsletters);
                param[20] = db.MakeInParameter("@DOB", SqlDbType.VarChar, 30, otblContacts.DOB.ToString());
                //param[21] = db.MakeInParameter("@HearAbtUs", SqlDbType.UniqueIdentifier, 50, otblContacts.HearAbtUs);
                if (otblContacts.HearAbtUs != "-1" && otblContacts.HearAbtUs != null)
                    param[21] = db.MakeInParameter("@HearAbtUs", SqlDbType.UniqueIdentifier, 50, SqlGuid.Parse(otblContacts.HearAbtUs));
                else
                    param[21] = db.MakeInParameter("@HearAbtUs", SqlDbType.UniqueIdentifier, 50, DBNull.Value);

                param[22] = db.MakeInParameter("@AptUnitNo", SqlDbType.VarChar, 30, otblContacts.AptUnitNo);
                param[23] = db.MakeInParameter("@Gender", SqlDbType.VarChar, 10, otblContacts.Gender);

                param[24] = db.MakeInParameter("@Email2", SqlDbType.VarChar, 50, otblContacts.Email2);
                param[25] = db.MakeInParameter("@Email3", SqlDbType.VarChar, 50, otblContacts.Email3);
                param[26] = db.MakeInParameter("@JBTAccountNo", SqlDbType.VarChar, 50, otblContacts.JBTAccountNo);
                param[27] = db.MakeInParameter("@CompanyName", SqlDbType.VarChar, 100, otblContacts.CompanyName);


                db.RunProcedure("P_InsertContacts_Merchant", param);
                // status = (int)param[19].Value;
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



        public string GetContactID(string uid)
        {
            string contactID = String.Empty;
            {
                if (object.Equals(db, null))
                {
                    db = new DataBase();
                }
                param = new SqlParameter[2];
                param[0] = db.MakeInParameter("@UID", uid);
                param[1] = db.MakeOutParameter("@contactID", SqlDbType.UniqueIdentifier, 64);
                db.RunProcedure("P_GetContactID", param);
                contactID = param[1].Value.ToString();
                return contactID;
            }

        }


        protected void ResetAll()
        {
            param = null;
            db = null;
        }

    }
}