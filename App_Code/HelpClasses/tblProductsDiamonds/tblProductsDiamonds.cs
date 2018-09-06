using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace tblproductsdiamonds
{
    /// <summary>
    /// Summary description for tblProductsDiamonds
    /// </summary>
    public class tblProductsDiamonds
    {
        public tblProductsDiamonds()
        {
        }

        # region Private Data Member Declaration

        private string _ProductDiamondID = null;
        private string _DiamondID = null;
        private string _ProductID = null;
        private string _StoneSettingID = null;
        private string _StoneSettingVendorID = null;
        private int _NoOfDiamondsForStandardSize;

        #endregion

        # region Public Properties Implementation

        public string ProductDiamondID
        {
            get { return _ProductDiamondID; }
            set { _ProductDiamondID = value; }
        }

        public string DiamondID
        {
            get { return _DiamondID; }
            set { _DiamondID = value; }
        }

        public string ProductID
        {
            get { return _ProductID; }
            set { _ProductID = value; }
        }

        public string StoneSettingID
        {
            get { return _StoneSettingID; }
            set { _StoneSettingID = value; }
        }

        public string StoneSettingVendorID
        {
            get { return _StoneSettingVendorID; }
            set { _StoneSettingVendorID = value; }
        }          

        public int NoOfDiamondsForStandardSize
        {
            get { return _NoOfDiamondsForStandardSize; }
            set { _NoOfDiamondsForStandardSize = value; }
        }

        #endregion
    }
}
