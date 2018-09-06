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

namespace tblpricecalculationformula
{
    /// <summary>
    /// Summary description for tblPriceCalculationFormulaHelper
    /// </summary>
    public class tblPriceCalculationFormulaHelper
    {
        DataBase db = null;
        SqlParameter[] param;
        //DataSet ds;

        public tblPriceCalculationFormulaHelper()
        {

        }

        public void InsertPriceCalculationFormula(tblpricecalculationformula.tblPriceCalculationFormula otblPriceCalculationFormula)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ToString()); ;
            try
            {
                con.Open();
                SqlCommand cmd1 = new SqlCommand("InsertPCFormula", con);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add(new SqlParameter("@FormulaID", otblPriceCalculationFormula.FormulaID));
                cmd1.Parameters.Add(new SqlParameter("@FormulaName", otblPriceCalculationFormula.FormulaName));
                cmd1.Parameters.Add(new SqlParameter("@Formula", otblPriceCalculationFormula.Formula));
                cmd1.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
                throw new Exception("Problem In Saving Record.");
            }
            finally
            {
                if (con.State.ToString() == "Open")
                    con.Close();
                ResetAll();
            }
        }

        #region Private Methods

        private void ResetAll()
        {
            db = null;
            param = null;
        }

        #endregion

    }
}