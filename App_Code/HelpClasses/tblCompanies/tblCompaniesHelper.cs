using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace tblcompanies
{
    /// <summary>
    /// Summary description for tblCompaniesHelper
    /// </summary>
    public class tblCompaniesHelper
    {
        DBComponent.DataBase db = null;
        SqlParameter[] param;
        DataSet ds;

        // Default Constructor
        public tblCompaniesHelper() { }

        /* The following method will insert a new record in the table [Companies] */
        public int InsertCompany(tblcompanies.tblCompanies objCompanies)
        {
             int status = -1; // denote failure
             try
             {
                 if (object.Equals(db, null))
                 {
                     db = new DBComponent.DataBase();
                     param = new SqlParameter[25];
                     param[0] = db.MakeInParameter("@CompanyID", objCompanies.CompanyID);
                     param[1] = db.MakeInParameter("@CompanyName", SqlDbType.NVarChar, 30, objCompanies.CompanyName);
                     param[2] = db.MakeInParameter("@JBTAccoNo", SqlDbType.NVarChar, 30, objCompanies.JBTAccoNo);
                     param[3] = db.MakeInParameter("@SiteStreet", SqlDbType.NVarChar, 30, objCompanies.SiteStreet);
                     param[4] = db.MakeInParameter("@SiteCity", SqlDbType.NVarChar, 30, objCompanies.SiteCity);
                     param[5] = db.MakeInParameter("@SiteState", SqlDbType.NVarChar, 30, objCompanies.SiteState);
                     param[6] = db.MakeInParameter("@SiteCountry", SqlDbType.NVarChar, 30, objCompanies.SiteCountry);
                     param[7] = db.MakeInParameter("@SiteZipCode", SqlDbType.NVarChar, 30, objCompanies.SiteZipCode);
                     param[8] = db.MakeInParameter("@ShippingStreet", SqlDbType.NVarChar, 30, objCompanies.ShippingStreet);
                     param[9] = db.MakeInParameter("@ShippingCity", SqlDbType.NVarChar, 30, objCompanies.ShippingCity);
                     param[10] = db.MakeInParameter("@ShippingState", SqlDbType.NVarChar, 30, objCompanies.ShippingState);
                     param[11] = db.MakeInParameter("@ShippingCountry", SqlDbType.NVarChar, 30, objCompanies.ShippingCountry);
                     param[12] = db.MakeInParameter("@ShippingZipCode", SqlDbType.NVarChar, 30, objCompanies.ShippingZipCode);
                     param[13] = db.MakeInParameter("@Telephone1", SqlDbType.NVarChar, 30, objCompanies.Telephone1);
                     param[14] = db.MakeInParameter("@Telephone2", SqlDbType.NVarChar, 30, objCompanies.Telephone2);
                     param[15] = db.MakeInParameter("@Telephone3", SqlDbType.NVarChar, 30, objCompanies.Telephone3);
                     param[16] = db.MakeInParameter("@Telephone4", SqlDbType.NVarChar, 30, objCompanies.Telephone4);
                     param[17] = db.MakeInParameter("@Telephone5", SqlDbType.NVarChar, 30, objCompanies.Telephone5);
                     param[18] = db.MakeInParameter("@PaymentTerms", SqlDbType.NVarChar, 30, objCompanies.PaymentTerms);
                     param[19] = db.MakeInParameter("@ReturnPolicyID", SqlDbType.UniqueIdentifier, 0, objCompanies.ReturnPolicyID);
                     param[20] = db.MakeInParameter("@AnnualSalesRangeFrom", SqlDbType.NVarChar, 30, objCompanies.AnnualSalesRangeFrom);
                     param[21] = db.MakeInParameter("@AnnualSalesRangeTo", SqlDbType.NVarChar, 30, objCompanies.AnnualSalesRangeTo);
                     param[22] = db.MakeInParameter("@IsActive", SqlDbType.NVarChar, 30, objCompanies.IsActive);
                     param[23] = db.MakeInParameter("@TypeID", objCompanies.TypeID);
                     param[24] = db.MakeInParameter("@RefCompany", objCompanies.RefCompany);
                     db.RunProcedure("", param);
                     status = 0;
                 }
             }
             catch (Exception ex)
             {
                 throw new Exception(ex.Message);
             }
             finally
             {
                 ResetAll();
             }

            return status;
        }
        public DataSet GetCompanies()
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
            db.RunProcedure("GetCompanies", null, out ds);
            ResetAll();
            return ds;
        }
        public DataSet GetCompanies(string UID)
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
            param[0] = db.MakeInParameter("@UID",UID);
            db.RunProcedure("GetCompanies",param, out ds);
            ResetAll();
            return ds;
        }
        protected void ResetAll()
        {
            param = null;
            db = null;
        }
    }
}
