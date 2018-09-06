using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace tblproducts
{
    /// <summary>
    /// Summary description for tblProducts
    /// </summary>
    public class tblProducts
    {
        // Default Constructor
        public tblProducts() 
        {
            //
            // TODO: Add constructor logic here
            //
        }       

        # region Private Data Member Declaration


        private string _ProductID;
        private string _ProductName = null;
        private string _AffTitle = null;
        private string _AffDescription = null;
        private string _Description = null;
        private string _MetalID = null;
        private string _MetalVendorID;
        private float _MetalWeightInGramsForStandardSize;
        private float _LaborRateForOneGramOfMetal;
        private string _PriceCalculationFormulaID = null;
        private float _FixedPrice;
        private int _VisualOrderIndex = 0;
        private float _StandardSize;
        private float _MinimumSize;
        private float _MaximumSize;        
	    private int _AvailableSize = 1;
	    private string _3DAnimationLink= null;
	    private string _VoiceoverLink = null;
        private int _AvailableInTwoTones = 0;
        private string _ProductStyleNumber;
        private float _MarkupProduct;
        private float _FakeDiscount;        
        private float _Weight;
        private int _Formula_ID=0;
        private int __ShowOnline = 0;
        private int __BestSeller = 0;
        private int __LimitedTimeOffer = 0; 
        private string _MetaKeyword = null;
        private string _AltTag = null;
        private string _MetaTitle = null;
        private string _MetaDescription = null; 
        
        
        


        #endregion

        # region Public Properties Implementation

       
        public string ProductID
        {
            get { return _ProductID; }
            set { _ProductID = value; }
        }

        public string ProductName
        {
            get { return _ProductName; }
            set { _ProductName = value; }
        }
        public string AffTitle
        {
            get { return _AffTitle; }
            set { _AffTitle = value; }
        }
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        public string AffDescription
        {
            get { return _AffDescription; }
            set { _AffDescription = value; }
        }
        public string MetalID
        {
            get { return _MetalID; }
            set { _MetalID = value; }
        }

        public string MetalVendorID
        {
            get { return _MetalVendorID; }
            set { _MetalVendorID = value; }
        }

        public float MetalWeightInGramsForStandardSize
        {
            get { return _MetalWeightInGramsForStandardSize; }
            set { _MetalWeightInGramsForStandardSize = value; }
        }

        public float LaborRateForOneGramOfMetal
        {
            get { return _LaborRateForOneGramOfMetal; }
            set { _LaborRateForOneGramOfMetal = value; }
        }

        public string PriceCalculationFormulaID
        {
            get { return _PriceCalculationFormulaID; }
            set { _PriceCalculationFormulaID = value; }
        }

        public float FixedPrice
        {
            get { return _FixedPrice; }
            set { _FixedPrice = value; }
        }

        public int VisualOrderIndex
        {
            get { return _VisualOrderIndex; }
            set { _VisualOrderIndex = value; }
        }

        public float StandardSize
        {
            get { return _StandardSize; }
            set { _StandardSize = value; }
        }

        public float MinimumSize
        {
            get { return _MinimumSize; }
            set { _MinimumSize = value; }
        }

        public float MaximumSize
        {
            get { return _MaximumSize; }
            set { _MaximumSize = value; }
        }


        public int  AvailableSize
        {
            get { return _AvailableSize; }
            set { _AvailableSize = value; }
        }
        
        public string D3AnimationLink
        {
            get { return _3DAnimationLink; }
            set { _3DAnimationLink = value; }
        }
        
        public string VoiceoverLink
        {
            get { return _VoiceoverLink; }
            set { _VoiceoverLink = value; }
        }

        public int AvailableInTwoTones
        {
            get { return _AvailableInTwoTones; }
            set { _AvailableInTwoTones = value; }
        }
        public string ProductStyleNumber
        {
            get { return _ProductStyleNumber; }
            set { _ProductStyleNumber = value; }
        }
        public float MarkupProduct
        {
            get { return _MarkupProduct; }
            set { _MarkupProduct = value; }
        }
        public float FakeDiscount
        {
            get { return _FakeDiscount; }
            set { _FakeDiscount = value; }
        }
        public float Weight
        {
            get { return _Weight; }
            set { _Weight = value; }
        }
        public int Formula_ID
        {
            get { return _Formula_ID; }
            set { _Formula_ID = value; }
        }

        public int ShowOnline
        {
            get { return __ShowOnline; }
            set { __ShowOnline = value; }
        }

        public int BestSeller
        {
            get { return __BestSeller; }
            set { __BestSeller = value; }
        }

        public int LimitedTimeOffer
        {
            get { return __LimitedTimeOffer; }
            set { __LimitedTimeOffer = value; }
        }

        public string MetaKeyword
        {
            get { return _MetaKeyword; }
            set { _MetaKeyword = value; }
        }

        public string AltTag
        {
            get { return _AltTag; }
            set { _AltTag = value; }
        }

        public string MetaTitle
        {
            get { return _MetaTitle; }
            set { _MetaTitle = value; }
        }

        public string MetaDescription
        {
            get { return _MetaDescription; }
            set { _MetaDescription = value; }
        }

        

        #endregion

    }
}