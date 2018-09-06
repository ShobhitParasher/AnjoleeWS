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

namespace tblproductsproductsgroups
{
    /// <summary>
    /// Summary description for tblProductsProductsGroupsHelper
    /// </summary>
    public class tblProductsProductsGroupsHelper
    {
        DataBase db = null;
        SqlParameter[] param;
        DataSet ds;

    #region Public Methods

        // Default Constructor
        public tblProductsProductsGroupsHelper()
        {
        }

        public void OldMethod_InsertRecord(tblProductsProductsGroups otblProductsProductsGroups)
        {
           // int status = -1;// -1 denote Insertion Failure
            try
            {
                if (object.Equals(db, null))
                    db = new DataBase();
                param = new SqlParameter[2];
                param[0] = db.MakeInParameter("@ProductsProductsGroupID", otblProductsProductsGroups.ProductsProductsGroupsID);
                param[1] = db.MakeInParameter("@ProductID", otblProductsProductsGroups.ProductID);
                db.RunProcedure("InsertProductsProductsGroups", param);
               // status = 0;
            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            finally
            {
                ResetAll();
            }
            //return status;
        }

        public DataSet GetChildProductsGroup(string ID)
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
            param[0] = db.MakeInParameter("@ParentProductGroupID", SqlDbType.VarChar, 100, ID);
            db.RunProcedure("spGetChildProductsGroup", param, out ds);
            ResetAll();
            return ds;
        }

    #endregion

    #region Private Methods

        private void ResetAll()
        {
            db = null;
            param = null;
        }
        
        #endregion
       
    }
}
