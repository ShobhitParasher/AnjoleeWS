using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace tblcolorstones
{
    /// <summary>
    /// Summary description for tblColorStones
    /// </summary>
    public class tblColorStones
    {
        private string _ColorStoneID = null;       
        private string _Weight = null;
        private string _Color = null;
        private string _Clarity = null;
        private string _Cut = null;
        private string _Shape = null;
        private string _Type = null;
        private string _Size=null;
        private string _VendorID = null;
        private string _ImageID = null;
        private double _Price;
        private string _Weight1 = null;
        private string _ColorStoneType = null;
        

        public tblColorStones()
        {
        }

        public string ColorStoneID
        {
            get { return _ColorStoneID; }
            set { _ColorStoneID = value; }
        }        

        public string Weight
        {
            get { return _Weight; }
            set { _Weight = value; }
        }
        public string Weight1
        {
            get { return _Weight1; }
            set { _Weight1 = value; }
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

        public string Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

        public string Size
        {
            get { return _Size; }
            set { _Size = value; }
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
        public string ColorStoneType
        {
            get { return _ColorStoneType; }
            set { _ColorStoneType = value; }
        }
    }
}