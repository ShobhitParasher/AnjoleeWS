using System;

namespace ShoppingCartGeneric
{
    public class CShoppingCartItem : IShoppingCartItem
    {
        private string intProductID;
        private string strProductName;
        private decimal decUnitPrice;
        private int intQuantity;
        private decimal _totalamount;
        private string _voucherCode;
        private string _customerID;
        private decimal _ItemLength;
        private string _DiamondQlty;
        private string _caratWeight;
        private string _effectiveCaratWeight;                   
        private string _stoneCaratWeight;
        private string _metalType;
        private int _CSPID;
        private string _productStyleNumber;
        private string _StoneShape;
        private string _StoneSetting;
        private int _StoneNumber;
        private string _StoneMM;
        private string _ProductImageName;
        private string _DiamondType;
        private string _ItemLengthType;
        private string _ColorStone;
        private string _MetalWeight;



        public string ProductID
        {
            get
            {
                return intProductID;
            }
            set
            {
                intProductID = value;
            }
        }

        public string ProductName
        {
            get
            {
                return strProductName;
            }
            set
            {
                strProductName = value;
            }
        }

        public decimal UnitPrice
        {
            get
            {
                return decUnitPrice;
            }
            set
            {
                decUnitPrice = value;
            }
        }
        public decimal totalamount
        {
            get
            {
                return _totalamount;
            }
            set
            {
                _totalamount = value;
            }
        }

        public int Quantity
        {
            get
            {
                return intQuantity;
            }
            set
            {
                intQuantity = value;
            }
        }
        public string voucherCode
        {
            get
            {
                return _voucherCode;
            }
            set
            {
                _voucherCode = value;
            }
        }

        public string customerID
        {
            get
            {
                return _customerID;
            }
            set
            {
                _customerID = value;
            }
        }

        public decimal ItemLength
        {
            get
            {
                return _ItemLength;
            }
            set
            {
                _ItemLength = value;
            }
        }

        public string DiamondQlty
        {
            get
            {
                return _DiamondQlty;
            }
            set
            {
                _DiamondQlty = value;
            }
        }
        public string caratWeight
        {
            get
            {
                return _caratWeight;
            }
            set
            {
                _caratWeight = value;
            }
        }

        public string effectiveCaratWeight
        {
            get
            {
                return _effectiveCaratWeight;
            }
            set
            {
                _effectiveCaratWeight = value;
            }
        }

        public string stoneCaratWeight
        {
            get
            {
                return _stoneCaratWeight;
            }
            set
            {
                _stoneCaratWeight = value;
            }
        }

        public string metalType
        {
            get
            {
                return _metalType;
            }
            set
            {
                _metalType = value;
            }
        }
        public int CSPID
        {
            get
            {
                return _CSPID;
            }
            set
            {
                _CSPID = value;
            }
        }

        public string ProductStyleNumber
        {
            get
            {
                return _productStyleNumber;
            }
            set
            {
                _productStyleNumber = value;
            }
        }

        public string StoneShape
        {
            get
            {
                return _StoneShape;
            }
            set
            {
                _StoneShape = value;
            }
        }

        public string StoneSetting
        {
            get
            {
                return _StoneSetting;
            }
            set
            {
                _StoneSetting = value;
            }
        }

        public int StoneNumber
        {
            get
            {
                return _StoneNumber;
            }
            set
            {
                _StoneNumber = value;
            }
        }
        public string StoneMM
        {
            get
            {
                return _StoneMM;
            }
            set
            {
                _StoneMM = value;
            }
        }

        public string ProductImageName
        {
            get
            {
                return _ProductImageName;
            }
            set
            {
                _ProductImageName = value;
            }
        }

        public string DiamondType
        {
            get
            {
                return _DiamondType;
            }
            set
            {
                _DiamondType = value;
            }
        }
        public string ItemLengthType
        {
            get
            {
                return _ItemLengthType;
            }
            set
            {
                _ItemLengthType = value;
            }
        }
        public string ColorStone
        {
            get
            {
                return _ColorStone;
            }
            set
            {
                _ColorStone = value;
            }
        }
        public string MetalWeight
        {
            get
            {
                return _MetalWeight;
            }
            set
            {
                _MetalWeight = value;
            }
        }
    }
}
