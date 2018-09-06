using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace tblproductscolorstones
{
    /// <summary>
    /// Summary description for tblProductsColorStones
    /// </summary>
    public class tblProductsColorStones
    {
        public tblProductsColorStones()
        {
        }

        # region Private Data Member Declaration

        private string _ColorStoneWeight = null;
        private string _ColorStoneColor = null;
        private string _ColorStoneClarity = null;
        private string _ColorStoneShape = null;
        private string _ColorStoneCut = null;      
        private string _ColorStoneVenderID = null;
        private string _ColorStoneType = null;
        private string _ColorStoneSize = null;
        private string _ProductID = null;
        private string _StoneSettingID = null;
        private string _StoneSettingVenderID = null;
        private int _NoOfColorStonesForStandardSize = 0;

        #endregion

        # region Public Properties Implementation

        public string ColorStoneWeight
        {
            get { return _ColorStoneWeight; }
            set { _ColorStoneWeight = value; }
        }

        public string ColorStoneColor
        {
            get { return _ColorStoneColor; }
            set { _ColorStoneColor = value; }
        }

        public string ColorStoneClarity
        {
            get { return _ColorStoneClarity; }
            set { _ColorStoneClarity = value; }
        }

        public string ColorStoneShape
        {
            get { return _ColorStoneShape; }
            set { _ColorStoneShape = value; }
        }

        public string ColorStoneCut
        {
            get { return _ColorStoneCut; }
            set { _ColorStoneCut = value; }
        }

        public string ColorStoneVenderID
        {
            get { return _ColorStoneVenderID; }
            set { _ColorStoneVenderID = value; }
        }

        public string ColorStoneSize
        {
            get { return _ColorStoneSize; }
            set { _ColorStoneSize = value; }
        }

        public string ColorStoneType
        {
            get { return _ColorStoneType; }
            set { _ColorStoneType = value; }
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

        public string StoneSettingVenderID
        {
            get { return _StoneSettingVenderID; }
            set { _StoneSettingVenderID = value; }
        }

        public int  NoOfColorStonesForStandardSize
        {
            get { return _NoOfColorStonesForStandardSize; }
            set { _NoOfColorStonesForStandardSize = value; }
        }

        #endregion
    }

}