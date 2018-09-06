using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace tblstonesettings
{
    /// <summary>
    /// Summary description for tblStoneSettings
    /// </summary>
    public class tblStoneSettings
    {
        public tblStoneSettings()
        {
        }

        #region Private Data Declaration

        private string _StoneSettingID = null;
        private string _StoneSettingName = null;
        private string _Description = null;
        private float _Price;

        #endregion

        #region  Public Properties Implementation

        public string StoneSettingID
        {
            get { return _StoneSettingID; }
            set { _StoneSettingID = value; }
        }

        public string StoneSettingName
        {
            get { return _StoneSettingName;}
            set { _StoneSettingName = value; }
        }

        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        public float Price
        {
            get { return _Price; }
            set { _Price = value; }
        }

        #endregion

    }
}