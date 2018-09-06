using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace tblProductsSizes
{
    /// <summary>
    /// Summary description for tblProducts
    /// </summary>
    public class tblProductsSizes
    {
        // Default Constructor
        public tblProductsSizes() 
        {
            //
            // TODO: Add constructor logic here
            //
        }       

        # region Private Data Member Declaration


        private string _ProductID;
        private string _ProductSizeID = null;
        private float _Sizes;
        private float _14kDefaultWeight;
        private float _GoldLabor;
        private float _PlatinumLabor;
        private int _TotalTypeOfStones=0;
        private float _DefaultLength;
        private int _TotalNoOfStones=0;
        private bool _StandardSize=false;
        private string _ProductsImage;
        private float _Height;
        private float _Width;
        private bool __SelectImage = false;


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

        public float Sizes
        {
            get { return _Sizes; }
            set { _Sizes = value; }
        }


        public float K14DefaultWeight
        {
            get { return _14kDefaultWeight; }
            set { _14kDefaultWeight = value; }
        }

        public float GoldLabor
        {
            get { return _GoldLabor; }
            set { _GoldLabor = value; }
        }

        public float PlatinumLabor
        {
            get { return _PlatinumLabor; }
            set { _PlatinumLabor = value; }
        }

        public float DefaultLength
        {
            get { return _DefaultLength; }
            set { _DefaultLength = value; }
        }


        public int TotalTypeOfStones
        {
            get { return _TotalTypeOfStones; }
            set { _TotalTypeOfStones = value; }
        }


        public int TotalNoOfStones
        {
            get { return _TotalNoOfStones; }
            set { _TotalNoOfStones = value; }
        }


        public bool StandardSize
        {
            get { return _StandardSize; }
            set { _StandardSize = value; }
        }

        public string ProductsImage
        {
            get { return _ProductsImage; }
            set { _ProductsImage = value; }
        }

        public float Height
        {
            get { return _Height; }
            set { _Height = value; }
        }

        public float Width
        {
            get { return _Width; }
            set { _Width = value; }
        }
        public bool SelectImage
        {
            get { return __SelectImage; }
            set { __SelectImage = value; }
        }
        
        
        #endregion

    }
}