using System;
using System.Collections;

namespace ShoppingCartGeneric
{
	public enum CShoppingCartType
	{
		Cookie,Session,Database
	}

	public class CShoppingCart:IShoppingCart
	{
		private IShoppingCart cart=null;

		public CShoppingCart(CShoppingCartType type)
		{
			switch(type)
			{
				case CShoppingCartType.Cookie:
					cart=new CCookieShoppingCart();
					break;
				case CShoppingCartType.Session:
					cart=new CSessionShoppingCart();
					break;
				case CShoppingCartType.Database:
					cart=new CDatabaseShoppingCart();
					break;
			}
		}

		public int Add(string cartid, IShoppingCartItem item)
		{
			return cart.Add(cartid,item);
		}

		public int Remove(string cartid, IShoppingCartItem item,int cspid)
		{
			return cart.Remove(cartid,item,cspid);
		}

        public int ClearCart(string cartid,string value1)
		{
			return cart.ClearCart(cartid,value1);
		}

		public int Update(string cartid, IShoppingCartItem item)
		{
			return cart.Update(cartid,item);
		}

        public System.Collections.ArrayList GetItems(string cartid)
		{
			return cart.GetItems(cartid);
		}
		public int GetPreviousQuantity(string cartid,string productID)        
		{
			return cart.GetPreviousQuantity(cartid,productID);
		}
        public int UpdateQuantity(string cartid,int newqty,int cspid)
        {
            return cart.UpdateQuantity(cartid,newqty,cspid);
        }

	}
}
