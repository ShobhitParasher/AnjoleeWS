using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace tbldiamonds
{
    /// <summary>
    /// Summary description for tblDiamonds
    /// </summary>
    public class tblDiamonds
    {       
        private string _DiamondID = null;       
        private string _Weight1 = null;
        private string _Weight2 = null;
        private string _Color = null;        
        private string _Clarity = null;
        private string _Cut = null;
        private string _Shape = null;
        private string _VendorID = null;
        private string _ImageID = null;
        private double _Price;
        private float _SpecialPremium=0;

        public tblDiamonds()
        {
        }

        public string DiamondID
        {
            get { return _DiamondID;}
            set { _DiamondID = value ;}
        }        

        public string Weight1
        {
            get { return _Weight1; }
            set { _Weight1 = value; }
        }
        public string Weight2
        {
            get { return _Weight2; }
            set { _Weight2 = value; }
        }
        public string Color
        {
            get { return _Color; }
            set { _Color = value; }
        }

        public string Clarity
        {
            get { return _Clarity; }
            set { _Clarity = value; }
        }

        public string Cut
        {
            get { return _Cut; }
            set { _Cut = value; }
        }

        public string Shape
        {
            get { return _Shape; }
            set { _Shape = value; }
        }

        public string VendorID
        {
            get { return _VendorID; }
            set { _VendorID = value; }
        }

        public string ImageID
        {
            get { return _ImageID; }
            set { _ImageID = value; }
        }

        public double Price
        {
            get { return _Price; }
            set { _Price = value; }
        }
        public float SpecialPremium
        {
            get { return _SpecialPremium; }
            set { _SpecialPremium = value; }
        }
    }
}