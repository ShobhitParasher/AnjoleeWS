using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace tblProductsStone
{
    /// <summary>
    /// Summary description for tblProducts
    /// </summary>
    public class tblProductsStone
    {
        // Default Constructor
        public tblProductsStone() 
        {
            //
            // TODO: Add constructor logic here
            //
        }       

        # region Private Data Member Declaration

        private string _ProductID;
        private string _ProductSizeID = null;
        private string _StoneConfigurationID = null;
        private int _StoneType;
        private string _StoneSize;
        private int _StoneQTy;
        private string _StoneShapeID=null;
        private string _StoneSettingID = null;
        private float _CaratWeight;
        private string _VendorID = null;
        


        #endregion

        # region Public Properties Implementation

       
        public string ProductID
        {
            get { return _ProductID; }
            set { _ProductID = value; }
        }

        public string ProductSizeID
        {
            get { return _ProductSizeID; }
            set { _ProductSizeID = value; }
        }

        public string StoneConfigurationID
        {
            get { return _StoneConfigurationID; }
            set { _StoneConfigurationID = value; }
        }

        public string StoneShapeID
        {
            get { return _StoneShapeID; }
            set { _StoneShapeID = value; }
        }
        public string StoneSettingID
        {
            get { return _StoneSettingID; }
            set { _StoneSettingID = value; }
        }


        public int StoneType
        {
            get { return _StoneType; }
            set { _StoneType = value; }
        }


        public int StoneQTy
        {
            get { return _StoneQTy; }
            set { _StoneQTy = value; }
        }


        public string StoneSize
        {
            get { return _StoneSize; }
            set { _StoneSize = value; }
        }

        public float CaratWeight
        {
            get { return _CaratWeight; }
            set { _CaratWeight = value; }
        }
        public string VendorID
        {
            get { return _VendorID; }
            set { _VendorID = value; }
        }
        
        
        #endregion

    }
}