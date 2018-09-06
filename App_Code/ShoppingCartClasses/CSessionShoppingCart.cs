using System;
using System.Collections;
using System.Web;

namespace ShoppingCartGeneric
{
	/// <summary>
	/// Summary description for CSessionShoppingCart.
	/// </summary>
	public class CSessionShoppingCart:IShoppingCart
	{

		public int Add(string cartid, IShoppingCartItem item)
		{
			ArrayList arr;
			int flag=0;
			if(HttpContext.Current.Session["mycart"]!=null)
			{
				arr=(ArrayList)HttpContext.Current.Session["mycart"];
			}
			else
			{
				arr=new ArrayList();
				HttpContext.Current.Session["mycart"]=arr;
			}
			for(int i=0;i<arr.Count;i++)
			{
				if(((IShoppingCartItem)arr[i]).ProductID==item.ProductID)
				{
					IShoppingCartItem temp=(IShoppingCartItem)arr[i];
					//Rohit 25-Feb-2007
					if(temp.ProductName != "Gift Voucher")
					{
						temp.Quantity=temp.Quantity+1;
						arr[i]=temp;
						flag=1;
					}
					else
						flag=0;
					//
					break;
				}
			}
			if(flag==0)
			{
				arr.Add(item);
			}
			return 0;
		}

		public int Remove(string cartid, IShoppingCartItem item,int cspid)
		{
			ArrayList items=(ArrayList)HttpContext.Current.Session["mycart"];
			for(int i=0;i<items.Count;i++)
			{
				if(((IShoppingCartItem)items[i]).ProductID==item.ProductID)
				{
					items.RemoveAt(i);
					break;
				}
			}
			return 0;
		}

        public int ClearCart(string cartid,string value1)
		{
			ArrayList items=(ArrayList)HttpContext.Current.Session["mycart"];
			items.Clear();
			return 0;
		}

		public int Update(string cartid, IShoppingCartItem item)
		{
			ArrayList items=(ArrayList)HttpContext.Current.Session["mycart"];
			for(int i=0;i<items.Count;i++)
			{
				if(((IShoppingCartItem)items[i]).ProductID==item.ProductID)
				{
					((IShoppingCartItem)items[i]).Quantity=item.Quantity;
					break;
				}
			}
			return 0;
		}

		public int GetPreviousQuantity(string cartid,string productID)
		{
			ArrayList arr;
			if(HttpContext.Current.Session["mycart"]!=null)
			{
				arr=(ArrayList)HttpContext.Current.Session["mycart"];
				for(int i=0;i<arr.Count;i++)
				{
					if(((IShoppingCartItem)arr[i]).ProductID==productID)
					{
						IShoppingCartItem temp=(IShoppingCartItem)arr[i];
						int qty=temp.Quantity;
						return qty;					
					}
				}
			}
			return 0;
		}

		public System.Collections.ArrayList GetItems(string cartid)
		{
			return (ArrayList)HttpContext.Current.Session["mycart"];
		}

        public int UpdateQuantity(string cartid, int newqty,int cspid)
        {
            return 0;
        }
	}
}
