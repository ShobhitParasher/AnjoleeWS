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
namespace tblStore_Prefs
{
    /// <summary>
    /// Summary description for tblStorePrefsHelper
    /// </summary>
    public class tblStorePrefsHelper
    {
        public tblStorePrefsHelper()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        //private DataBase db;
        //SqlParameter[] param;
        //DataSet ds;
        //public int UpdateStorePrefs(tblStore_Prefs otblStore_Prefs)
        //{
        //    int status = -1;
        //    try
        //    {
        //        if (object.Equals(db, null))
        //        {
        //            db = new DataBase();
        //        }
        //        param = new SqlParameter[12];
        //        param[0] = db.MakeInParameter("@storeName", SqlDbType.VarChar, 50, otblStore_Prefs.StoreName);
        //        param[1] = db.MakeInParameter("@storeStreet", SqlDbType.VarChar, 50, otblStore_Prefs.Street);
        //        param[2] = db.MakeInParameter("@storeSuburb", SqlDbType.VarChar, 50, otblStore_Prefs.suburb);
        //        param[3] = db.MakeInParameter("@storeState", SqlDbType.VarChar, 50, otblStore_Prefs.state);
        //        param[4] = db.MakeInParameter("@storePostCode", SqlDbType.VarChar, 50, otblStore_Prefs.postCode);
        //        param[5] = db.MakeInParameter("@storeCountry", SqlDbType.VarChar, 50, otblStore_Prefs.country);
        //        param[6] = db.MakeInParameter("@storePhone", SqlDbType.VarChar, 50, otblStore_Prefs.phone);
        //        param[7] = db.MakeInParameter("@storeFax", SqlDbType.VarChar, 50, otblStore_Prefs.fax);
        //        param[8] = db.MakeInParameter("@storeEmail", SqlDbType.VarChar, 50, otblStore_Prefs.email);
        //        param[9] = db.MakeInParameter("@storeWebsite", SqlDbType.VarChar, 150, otblStore_Prefs.website);
        //        param[10] = db.MakeInParameter("@storePageTitle", SqlDbType.VarChar, 100, otblStore_Prefs.StorePageTitle);
        //        param[11] = db.MakeOutParameter("@Status", SqlDbType.Int, 4);
        //        db.RunProcedure("P_UpdateStorePref", param);
        //        status = (int)param[11].Value;
        //    }
        //    catch (Exception ex)
        //    {
        //        string test = ex.ToString();
        //        string test1 = ex.Message;
        //    }
        //    finally
        //    {
        //        ResetAll();
        //    }
        //    return status;
        //}
        //public DataSet GetStorePrefs()
        //{
        //    if (object.Equals(db, null))
        //    {
        //        db = new DataBase();
        //    }
        //    if (object.Equals(ds, null))
        //    {
        //        ds = new DataSet();
        //    }
        //    param = new SqlParameter[2];
        //    db.RunProcedure("P_GetStorePref", null, out ds);
        //    ResetAll();
        //    return ds;
        //}
        //public int InsertCountrie(tblStore_Prefs otblStore_Prefs, string Mode)
        //{
        //    int status = -1;
        //    try
        //    {
        //        if (object.Equals(db, null))
        //        {
        //            db = new DataBase();
        //        }
        //        param = new SqlParameter[6];
        //        param[0] = db.MakeInParameter("@countryID", SqlDbType.VarChar, 50, otblStore_Prefs.countryID);
        //        param[1] = db.MakeInParameter("@countryName", SqlDbType.VarChar, 50, otblStore_Prefs.countryName);
        //        param[2] = db.MakeInParameter("@countryVisible", SqlDbType.Bit, 1, otblStore_Prefs.countryVisible);
        //        param[3] = db.MakeInParameter("@countryDefault", SqlDbType.Bit, 1, otblStore_Prefs.countryDefault);
        //        param[4] = db.MakeInParameter("@Mode", SqlDbType.VarChar, 10, Mode);
        //        param[5] = db.MakeOutParameter("@Status", SqlDbType.Int, 4);
        //        db.RunProcedure("P_InsertCountrie", param);
        //        status = (int)param[5].Value;
        //    }
        //    catch (Exception ex)
        //    {
        //        string test = ex.ToString();
        //        string test1 = ex.Message;
        //    }
        //    finally
        //    {
        //        ResetAll();
        //    }
        //    return status;
        //}
        //public DataSet GetCountries(string searchText)
        //{
        //    if (object.Equals(db, null))
        //    {
        //        db = new DataBase();
        //    }
        //    if (object.Equals(ds, null))
        //    {
        //        ds = new DataSet();
        //    }
        //    param = new SqlParameter[1];
        //    param[0] = db.MakeInParameter("@countryName", SqlDbType.VarChar, 100, searchText);
        //    db.RunProcedure("P_GetCountries", param, out ds);
        //    ResetAll();
        //    return ds;
        //}
        //public DataSet GetCountriesByID(int ID)
        //{
        //    if (object.Equals(db, null))
        //    {
        //        db = new DataBase();
        //    }
        //    if (object.Equals(ds, null))
        //    {
        //        ds = new DataSet();
        //    }
        //    param = new SqlParameter[1];
        //    param[0] = db.MakeInParameter("@countryID", SqlDbType.VarChar, 100, ID);
        //    db.RunProcedure("P_GetCountriesByID", param, out ds);
        //    ResetAll();
        //    return ds;
        //}
        //public DataSet GetAllCountries()
        //{
        //    if (object.Equals(db, null))
        //    {
        //        db = new DataBase();
        //    }
        //    if (object.Equals(ds, null))
        //    {
        //        ds = new DataSet();
        //    }
        //    db.RunProcedure("P_GetAllCountries", null, out ds);
        //    ResetAll();
        //    return ds;
        //}
        //public void DeleteCountrie(int CountrieID)
        //{
        //    if (object.Equals(db, null))
        //    {
        //        db = new DataBase();
        //    }
        //    param = new SqlParameter[1];
        //    param[0] = db.MakeInParameter("@countryID", SqlDbType.Int, 4, CountrieID);
        //    db.RunProcedure("P_DeleteCountry", param);
        //    ResetAll();
        //}

        //public DataSet GetState(int countryID, string stateName, string mode)
        //{
        //    if (object.Equals(db, null))
        //    {
        //        db = new DataBase();
        //    }
        //    if (object.Equals(ds, null))
        //    {
        //        ds = new DataSet();
        //    }
        //    param = new SqlParameter[3];
        //    param[0] = db.MakeInParameter("@countryID", SqlDbType.Int, 4, countryID);
        //    param[1] = db.MakeInParameter("@stateName", SqlDbType.VarChar, 100, stateName);
        //    param[2] = db.MakeInParameter("@mode", SqlDbType.VarChar, 10, mode);
        //    db.RunProcedure("P_GetStates", param, out ds);
        //    ResetAll();
        //    return ds;
        //}
        //public DataSet GetStateByID(int ID)
        //{
        //    if (object.Equals(db, null))
        //    {
        //        db = new DataBase();
        //    }
        //    if (object.Equals(ds, null))
        //    {
        //        ds = new DataSet();
        //    }
        //    param = new SqlParameter[1];
        //    param[0] = db.MakeInParameter("@stateID", SqlDbType.VarChar, 100, ID);
        //    db.RunProcedure("P_GetStateByID", param, out ds);
        //    ResetAll();
        //    return ds;
        //}
        //public void DeleteStates(int stateID)
        //{
        //    if (object.Equals(db, null))
        //    {
        //        db = new DataBase();
        //    }
        //    param = new SqlParameter[1];
        //    param[0] = db.MakeInParameter("@stateID", SqlDbType.Int, 4, stateID);
        //    db.RunProcedure("P_DeleteState", param);
        //    ResetAll();
        //}
        //public DataSet GetCity(int stateID, int countryID, string cityName, string mode)
        //{
        //    if (object.Equals(db, null))
        //    {
        //        db = new DataBase();
        //    }
        //    if (object.Equals(ds, null))
        //    {
        //        ds = new DataSet();
        //    }
        //    param = new SqlParameter[4];
        //    param[0] = db.MakeInParameter("@StateID", SqlDbType.Int, 4, stateID);
        //    param[1] = db.MakeInParameter("@countryID", SqlDbType.Int, 4, countryID);
        //    param[2] = db.MakeInParameter("@cityName", SqlDbType.VarChar, 100, cityName);
        //    param[3] = db.MakeInParameter("@mode", SqlDbType.VarChar, 10, mode);
        //    db.RunProcedure("P_GetCity", param, out ds);
        //    ResetAll();
        //    return ds;
        //}
        //public DataSet GetCityByID(int ID)
        //{
        //    if (object.Equals(db, null))
        //    {
        //        db = new DataBase();
        //    }
        //    if (object.Equals(ds, null))
        //    {
        //        ds = new DataSet();
        //    }
        //    param = new SqlParameter[1];
        //    param[0] = db.MakeInParameter("@CityID", SqlDbType.VarChar, 100, ID);
        //    db.RunProcedure("P_GetCityByID", param, out ds);
        //    ResetAll();
        //    return ds;
        //}
        //public void DeleteCity(int CityID)
        //{
        //    if (object.Equals(db, null))
        //    {
        //        db = new DataBase();
        //    }
        //    param = new SqlParameter[1];
        //    param[0] = db.MakeInParameter("@CityID", SqlDbType.Int, 4, CityID);
        //    db.RunProcedure("P_DeleteCity", param);
        //    ResetAll();
        //}
        //public int InsertCity(tblStore_Prefs otblStore_Prefs, string Mode)
        //{
        //    int status = -1;
        //    try
        //    {
        //        if (object.Equals(db, null))
        //        {
        //            db = new DataBase();
        //        }
        //        param = new SqlParameter[5];
        //        param[0] = db.MakeInParameter("@CityID", SqlDbType.Int, 4, otblStore_Prefs.CityID);
        //        param[1] = db.MakeInParameter("@stateID", SqlDbType.Int, 4, otblStore_Prefs.stateID);
        //        param[2] = db.MakeInParameter("@CityName", SqlDbType.VarChar, 50, otblStore_Prefs.cityName);
        //        param[3] = db.MakeInParameter("@Mode", SqlDbType.VarChar, 10, Mode);
        //        param[4] = db.MakeOutParameter("@Status", SqlDbType.Int, 4);
        //        db.RunProcedure("P_InsertCity", param);
        //        status = (int)param[4].Value;
        //    }
        //    catch (Exception ex)
        //    {
        //        string test = ex.ToString();
        //        string test1 = ex.Message;
        //    }
        //    finally
        //    {
        //        ResetAll();
        //    }
        //    return status;
        //}
        //public int InsertState(tblStore_Prefs otblStore_Prefs, string Mode)
        //{
        //    int status = -1;
        //    try
        //    {
        //        if (object.Equals(db, null))
        //        {
        //            db = new DataBase();
        //        }
        //        param = new SqlParameter[5];
        //        param[0] = db.MakeInParameter("@countryID", SqlDbType.Int, 4, otblStore_Prefs.countryID);
        //        param[1] = db.MakeInParameter("@stateID", SqlDbType.Int, 4, otblStore_Prefs.stateID);
        //        param[2] = db.MakeInParameter("@stateName", SqlDbType.VarChar, 50, otblStore_Prefs.stateName);
        //        param[3] = db.MakeInParameter("@Mode", SqlDbType.VarChar, 10, Mode);
        //        param[4] = db.MakeOutParameter("@Status", SqlDbType.Int, 4);
        //        db.RunProcedure("P_InsertState", param);
        //        status = (int)param[4].Value;
        //    }
        //    catch (Exception ex)
        //    {
        //        string test = ex.ToString();
        //        string test1 = ex.Message;
        //    }
        //    finally
        //    {
        //        ResetAll();
        //    }
        //    return status;
        //}
        //public DataSet selectstoreme(bool storeClosed)
        //{
        //    if (object.Equals(db, null))
        //    {
        //        db = new DataBase();
        //    }
        //    if (object.Equals(ds, null))
        //    {
        //        ds = new DataSet();
        //    }
        //    param = new SqlParameter[1];
        //    param[0] = db.MakeInParameter("@storeClosed", SqlDbType.Bit, 0, storeClosed);
        //    db.RunProcedure("P_selectmsg", param, out ds);
        //    ResetAll();
        //    return ds;
        //}
        //public int UpdateStoreclosed(tblStore_Prefs otblStore_Prefs)
        //{
        //    int status = -1;
        //    try
        //    {
        //        if (object.Equals(db, null))
        //        {
        //            db = new DataBase();
        //        }
        //        param = new SqlParameter[3];
        //        param[0] = db.MakeInParameter("@storeClosed", SqlDbType.Bit, 0, otblStore_Prefs.storeClosed);
        //        param[1] = db.MakeInParameter("@storeClosedMessage", SqlDbType.VarChar, 50, otblStore_Prefs.storeClosedMessage);

        //        param[2] = db.MakeOutParameter("@Status", SqlDbType.Int, 4);
        //        db.RunProcedure("P_UpdateStoreclosed", param);
        //        status = (int)param[2].Value;
        //    }
        //    catch (Exception ex)
        //    {
        //        string test = ex.ToString();
        //        string test1 = ex.Message;
        //    }
        //    finally
        //    {
        //        ResetAll();
        //    }
        //    return status;
        //}
        //public DataSet GetCartStatistics()
        //{
        //    if (object.Equals(db, null))
        //    {
        //        db = new DataBase();
        //    }
        //    if (object.Equals(ds, null))
        //    {
        //        ds = new DataSet();
        //    }
        //    param = new SqlParameter[2];
        //    db.RunProcedure("P_Main", null, out ds);
        //    ResetAll();
        //    return ds;
        //}
        //private void ResetAll()
        //{
        //    param = null;
        //    db = null;
        //}
    }
}