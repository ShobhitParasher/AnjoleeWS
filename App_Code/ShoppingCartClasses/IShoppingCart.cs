using System;
using System.Collections;

namespace ShoppingCartGeneric
{
    public interface IShoppingCartItem
    {
        string ProductID
        {
            get;
            set;
        }
        string ProductName
        {
            get;
            set;
        }
        decimal UnitPrice
        {
            get;
            set;
        }
        int Quantity
        {
            get;
            set;
        }
        string voucherCode
        {
            get;
            set;
        }
        string metalType
        {
            get;
            set;
        }
        string caratWeight
        {
            get;
            set;
        }
        string effectiveCaratWeight
        {
            get;
            set;
        }

        string stoneCaratWeight
        {
            get;
            set;
        }

        string DiamondQlty
        {
            get;
            set;
        }

        decimal ItemLength
        {
            get;
            set;
        }

        string customerID
        {
            get;
            set;
        }
        int CSPID
        {
            get;
            set;
        }
        string ProductStyleNumber
        {
            get;
            set;
        }
        string StoneSetting
        {
            get;
            set;
        }
        int StoneNumber
        {
            get;
            set;
        }
        string StoneShape
        {
            get;
            set;
        }
        string StoneMM
        {
            get;
            set;
        }
        string ProductImageName
        {
            get;
            set;
        }
        string DiamondType
        {
            get;
            set;
        }
        string ItemLengthType
        {
            get;
            set;
        }
        string ColorStone
        {
            get;
            set;
        }
        string MetalWeight
        {
            get;
            set;
        }
    }

    public interface IShoppingCart
    {
        int Add(string cartid, IShoppingCartItem item);
        int Remove(string cartid, IShoppingCartItem item, int cspid);
        int Update(string cartid, IShoppingCartItem item);
        int UpdateQuantity(string cartid, int newqty, int cspid);
        int ClearCart(string cartid, string value1);
        int GetPreviousQuantity(string cartid, string productID);
        ArrayList GetItems(string cartid);
    }
}
