using System;
using System.Web;
using System.Collections;

namespace ShoppingCartGeneric
{
	public class CCookieShoppingCart:IShoppingCart
	{

		public int Add(string cartid, IShoppingCartItem item)
		{
			HttpCookie c=null;
			if(HttpContext.Current.Request.Cookies["shoppingcart"]==null)
				c=new HttpCookie("shoppingcart");
			else
				c=HttpContext.Current.Request.Cookies["shoppingcart"];
			string itemdetails;
			itemdetails=item.ProductID + "|" + item.ProductName + "|" + item.UnitPrice;
			c.Values[item.ProductID.ToString()]=itemdetails;
			HttpContext.Current.Response.Cookies.Add(c);
			return 1;
		}

		public int Remove(string cartid,IShoppingCartItem item,int cspid)
		{
			HttpCookie c=HttpContext.Current.Request.Cookies["shoppingcart"];
			c.Values.Remove(item.ProductID.ToString());
			HttpContext.Current.Response.Cookies.Add(c);
			return 1;
		}

		public int Update(string cartid, IShoppingCartItem item)
		{
			return 0;
		}

        public int ClearCart(string cartid,string value1)
		{
			return 0;
		}

        public int UpdateQuantity(string cartid,int newqty,int cspid)
        {
            return 0;
        }

		public int GetPreviousQuantity(string cartid,string productID)
		{
			return 0;
		}

		public ArrayList GetItems(string cartid)
		{
			HttpCookie c=HttpContext.Current.Request.Cookies["shoppingcart"];
			ArrayList items=new ArrayList();

			for(int i=0;i<c.Values.Count;i++)
			{
				string[] vals=c.Values[i].Split('|');
				CShoppingCartItem item=new CShoppingCartItem();
                item.ProductID = (vals[0]).ToString();
				item.ProductName=vals[1];
				item.UnitPrice=decimal.Parse(vals[2]);
				item.Quantity=1;

				items.Add(item);
			}
			return items;
		}

	}
}
