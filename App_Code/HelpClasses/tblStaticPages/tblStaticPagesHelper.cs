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
namespace tblStaticPages
{
    /// <summary>
    /// Summary description for tblStaticPagesHelper
    /// </summary>
    public class tblStaticPagesHelper
    {
        public tblStaticPagesHelper()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        private DataBase db;
        SqlParameter[] param;
        DataSet ds;
        public DataSet GetStaticPages()
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
            db.RunProcedure("P_GetStaticPagesForAnjolee", null, out ds);
            ResetAll();
            return ds;
        }
        public DataSet GetStaticPages(int PageID)
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
            param[0] = db.MakeInParameter("@PageID", SqlDbType.Int, 4, PageID);
            db.RunProcedure("P_GetStaticPagesContentsForAnjolee", param, out ds);
            ResetAll();
            return ds;
        }
        
        public int UpdateStaticPagesContents(tblStaticPages otblStaticPages)
        {
            int status = -1;
            try
            {
                if (object.Equals(db, null))
                {
                    db = new DataBase();
                }
                param = new SqlParameter[3];
                param[0] = db.MakeInParameter("@PageID", SqlDbType.Int, 4, otblStaticPages.PageID);
                param[1] = db.MakeInParameter("@PageContents", SqlDbType.NText, 0, otblStaticPages.PageContents);
                param[2] = db.MakeOutParameter("@Status", SqlDbType.Int, 4);
                db.RunProcedure("P_UpdateStaticPagesContentsForAnjolee", param);
                status = (int)param[2].Value;
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
       
        private void ResetAll()
        {
            param = null;
            db = null;
        }
    }
}
