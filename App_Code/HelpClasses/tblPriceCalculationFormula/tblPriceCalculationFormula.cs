using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace tblpricecalculationformula
{
    /// <summary>
    /// Summary description for tblPriceCalculationFormula
    /// </summary>
    public class tblPriceCalculationFormula
    {
        public tblPriceCalculationFormula()
        {
        }

        # region Private Data Member Declaration

        private string _FormulaID = null;
        private string _FormulaName = null;
        private string _Formula = null;

        #endregion

        # region Public Properties Implementation

        public string FormulaID
        {
            get { return _FormulaID; }
            set { _FormulaID = value; }
        }

        public string FormulaName
        {
            get { return _FormulaName;}
            set { _FormulaName = value;}
        }

        public string Formula
        {
            get { return _Formula;}
            set { _Formula = value;}
        }

        #endregion
    }
}