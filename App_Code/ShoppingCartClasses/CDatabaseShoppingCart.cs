using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using DBComponent;


namespace ShoppingCartGeneric
{
    public class CDatabaseShoppingCart : IShoppingCart
    {
        //private static string connstr=@"data source=.\vsdotnet;initial catalog=northwind;user id=sa";
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ToString());
        DBComponent.CommonFunctions ComFun = new DBComponent.CommonFunctions();
        private DataBase db;
        SqlParameter[] param;

        public int Add(string cartid, IShoppingCartItem item)
        {
            int cart;
            bool chk = false;
            int cartcspid=0;
            string qry = "select CSPID from  tbl_CartSession_Product where sessionID = '" + cartid + "' AND productID='" + item.ProductID + "'";
            DataTable dt = ComFun.ExecuteQueryReturnDataTable(qry);
            if (dt.Rows.Count != 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                     cartcspid = Convert.ToInt32(dt.Rows[i]["CSPID"].ToString());
                    string newqry = "select * from tbl_CartSessionProduct_ProductOption where CSPID = " + cartcspid;
                    DataSet dsone = ComFun.ExecuteQueryReturnDataSet(newqry);
                    if (dsone.Tables[0].Rows.Count > 0)
                    {
                        decimal ItemLength = Convert.ToDecimal(dsone.Tables[0].Rows[0]["ItemLength"].ToString());
                        string metalType = dsone.Tables[0].Rows[0]["metalType"].ToString();
                        string DiamondQlty = dsone.Tables[0].Rows[0]["DiamondQlty"].ToString();
                        string caratWeight = dsone.Tables[0].Rows[0]["CaratWeight"].ToString();
                        string ColorStone = dsone.Tables[0].Rows[0]["ColorStone"].ToString();
                        string _strEffectiveCaratWeight = dsone.Tables[0].Rows[0]["effectiveCaratWeight"].ToString();
                        string _strStoneCaratWeight = dsone.Tables[0].Rows[0]["stoneCaratWeight"].ToString();
                        if (ColorStone.Trim() == "")
                        {
                            ColorStone = null;
                        }
                        if (ItemLength == item.ItemLength && metalType == item.metalType && DiamondQlty == item.DiamondQlty && caratWeight == item.caratWeight && ColorStone == item.ColorStone && _strEffectiveCaratWeight == item.effectiveCaratWeight && _strStoneCaratWeight==item.stoneCaratWeight)
                        {
                            chk = true;
                            break;
                        }
                        else
                        {
                            chk = false;
                        }
                    }
                }
            }
            if (chk == true)
            {
                ComFun.ExecuteQueryReturnNothing("update tbl_CartSession_Product set Quantity=Quantity + 1 where SessionID='" + cartid + "' and productID='" + item.ProductID + "'");
                return cartcspid;
            }
            else 
            {
                cart = Addtocart(cartid, item);
                return cart;

            }
           // string cspid = ComFun.ExecuteQueryReturnSingleString(qry);
           /* if (cspid != "")
            {
                //int cartcspid = Convert.ToInt32(cspid);
                string newqry = "select * from tbl_CartSessionProduct_ProductOption where CSPID = " + cspid + "";
                DataSet dsone = ComFun.ExecuteQueryReturnDataSet(newqry);
                decimal ItemLength = Convert.ToDecimal(dsone.Tables[0].Rows[0]["ItemLength"].ToString());
                string metalType = dsone.Tables[0].Rows[0]["metalType"].ToString();
                string DiamondQlty = dsone.Tables[0].Rows[0]["DiamondQlty"].ToString();
                string caratWeight = dsone.Tables[0].Rows[0]["CaratWeight"].ToString();
                string ColorStone = dsone.Tables[0].Rows[0]["ColorStone"].ToString();
                if (ItemLength == item.ItemLength && metalType == item.metalType && DiamondQlty == item.DiamondQlty && caratWeight == item.caratWeight)
                {
                    ComFun.ExecuteQueryReturnNothing("update tbl_CartSession_Product set Quantity=Quantity + 1 where SessionID='" + cartid + "' and productID='" + item.ProductID + "'");
                    return cartcspid;

                }
                else
                {
                    cart = Addtocart(cartid, item);
                    return cart;
                }
            }
            else
            {
                cart = Addtocart(cartid, item);
                return cart;
            }*/
        }


        public int Addtocart(string cartid, IShoppingCartItem item)
        {
            int cartSessionIdentity = 0;
            if (object.Equals(db, null))
            {
                db = new DataBase();
            }
            param = new SqlParameter[22];
            param[0] = db.MakeInParameter("@cartID", cartid);
            param[1] = db.MakeInParameter("@prodID", item.ProductID);
            param[2] = db.MakeInParameter("@prodName", SqlDbType.NVarChar, 100, item.ProductName);
            param[3] = db.MakeInParameter("@unitPrice", SqlDbType.Money, 8, item.UnitPrice);
            param[4] = db.MakeInParameter("@Qty", SqlDbType.Int, 4, item.Quantity);
            param[5] = db.MakeInParameter("@customerID", item.customerID);
            param[6] = db.MakeInParameter("@Itemlength", SqlDbType.Float, 8, item.ItemLength);
            param[7] = db.MakeInParameter("@DiamondQlty", SqlDbType.NVarChar, 50, item.DiamondQlty);
            param[8] = db.MakeInParameter("@metalType", SqlDbType.NVarChar, 128, item.metalType);
            param[9] = db.MakeInParameter("@caratWeigt", SqlDbType.NVarChar, 50, item.caratWeight);
            param[10] = db.MakeInParameter("@ProductStyleNumber", SqlDbType.NVarChar, 50, item.ProductStyleNumber);
            param[11] = db.MakeInParameter("@StoneSetting", SqlDbType.NVarChar, 50, item.StoneSetting);
            param[12] = db.MakeInParameter("@StoneNumber", SqlDbType.Int, 4, item.StoneNumber);
            param[13] = db.MakeInParameter("@StoneShape", SqlDbType.NVarChar, 50, item.StoneShape);
            param[14] = db.MakeInParameter("@StoneMM", SqlDbType.NVarChar, 100, item.StoneMM);
            param[15] = db.MakeInParameter("@ProductImageName", SqlDbType.NVarChar, 100, item.ProductImageName);
            param[16] = db.MakeInParameter("@DiamondType", SqlDbType.NVarChar, 2, item.DiamondType);
            param[17] = db.MakeInParameter("@ItemLenthType", SqlDbType.NVarChar, 2, item.ItemLengthType);
            param[18] = db.MakeInParameter("@ColorStone", SqlDbType.NVarChar, 50, item.ColorStone);
            param[19] = db.MakeOutParameter("@csid", SqlDbType.Int, 4);
            param[20] = db.MakeInParameter("@effectiveCaratWeight", SqlDbType.NVarChar, 100, item.effectiveCaratWeight);
            param[21] = db.MakeInParameter("@stoneCaratWeight", SqlDbType.NVarChar, 100, item.stoneCaratWeight);
            
            db.RunProcedure("InsertProductIntoCart", param);
            cartSessionIdentity = (int)param[19].Value;

            //     ComFun.ExecuteQueryReturnNothing("update tbl_CartSession_Product set Quantity= Quantity + 1 where SessionID='"+cartid+"'");

            return cartSessionIdentity;
        }


        public int UpdateQuantity(string cartid, int newqty, int cspid)
        {
            //SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ToString());
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "UPDATE tbl_CartSession_Product SET Quantity=@qty where sessionID=@cartid  AND CSPID=@cspid";

            SqlParameter p1 = new SqlParameter("@qty", newqty);
            SqlParameter p2 = new SqlParameter("@cartid", cartid);
            //SqlParameter p3 = new SqlParameter("@prodid", productid);
            SqlParameter p3 = new SqlParameter("@cspid", cspid);

            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);
            cmd.Parameters.Add(p3);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return 0;
        }

        public int Update(string cartid, IShoppingCartItem item)
        {
            return 0;
        }
        public int ClearCart(string cartid, string value1)
        {
            ComFun.ExecuteQueryReturnNothing("DELETE FROM tbl_CartSessionProduct_ProductOption WHERE CSPID in (" + value1 + ")");
            ComFun.ExecuteQueryReturnNothing("DELETE FROM tbl_CartSession_Product WHERE sessionID='" + cartid + "'");
            //con.Close();
            return 0;
        }

        public int GetPreviousQuantity(string cartid, string productID)
        {
            return 0;
        }

        public int Remove(string cartid, IShoppingCartItem item, int cspid)
        {
            ComFun.ExecuteQueryReturnNothing("DELETE FROM tbl_CartSession_Product where sessionID = '" + cartid + "' AND CSPID = " + cspid + "");
            ComFun.ExecuteQueryReturnNothing("DELETE FROM tbl_CartSessionProduct_ProductOption where CSPID = " + cspid + "");
            return 0;
        }

        public System.Collections.ArrayList GetItems(string cartid)
        {
            string qry = "SELECT  tbl_CartSession_Product.ProductStyleNumber,tbl_CartSessionProduct_ProductOption.ItemLengthType,tbl_CartSession_Product.DiamondType,tbl_CartSession_Product.ProductImageName,tbl_CartSession_Product.CSPID,tbl_CartSession_Product.productName,tbl_CartSession_Product.productID,tbl_CartSession_Product.Quantity, " +
                         " tbl_CartSession_Product.productPrice, tbl_CartSession_Product.categoryID, tbl_CartSession_Product.customerID,tbl_CartSessionProduct_ProductOption.metalType," +
                         " tbl_CartSessionProduct_ProductOption.CaratWeight,tbl_CartSessionProduct_ProductOption.effectiveCaratWeight,tbl_CartSessionProduct_ProductOption.stoneCaratWeight,tbl_CartSessionProduct_ProductOption.DiamondQlty, tbl_CartSessionProduct_ProductOption.ItemLength ,tbl_CartSessionProduct_ProductOption.ColorStone," +
                         " tbl_CartSessionProduct_ProductOption.StoneSetting,tbl_CartSessionProduct_ProductOption.StoneNumber,tbl_CartSessionProduct_ProductOption.StoneShape,tbl_CartSessionProduct_ProductOption.StoneMM " +
                         " FROM tbl_CartSessionProduct_ProductOption INNER JOIN tbl_CartSession_Product ON tbl_CartSessionProduct_ProductOption.CSPID = tbl_CartSession_Product.CSPID AND tbl_CartSession_Product.sessionID = '" + cartid + "'";
            DataSet ds = new DataSet();
            ds = ComFun.ExecuteQueryReturnDataSet(qry);

            ArrayList arr = new ArrayList();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                CShoppingCartItem item = new CShoppingCartItem();
                item.ProductID = row["productid"].ToString();
                item.CSPID = Convert.ToInt32(row["CSPID"].ToString());
                item.ProductName = (String)row["productname"];
                item.Quantity = (int)row["quantity"];
                item.UnitPrice = decimal.Round(Convert.ToDecimal(row["productPrice"].ToString()), 2);
                item.ItemLength = Convert.ToDecimal(row["ItemLength"].ToString());
                item.metalType = row["metalType"].ToString();
                item.DiamondQlty = row["DiamondQlty"].ToString();
                item.caratWeight = row["CaratWeight"].ToString();
                item.effectiveCaratWeight = row["effectiveCaratWeight"].ToString();
                item.stoneCaratWeight = row["stoneCaratWeight"].ToString();
               

                item.ProductStyleNumber = row["ProductStyleNumber"].ToString();
                item.StoneSetting = row["StoneSetting"].ToString();
                item.StoneShape = row["StoneShape"].ToString();
                item.StoneNumber = Convert.ToInt32(row["StoneNumber"].ToString());
                item.StoneMM = row["StoneMM"].ToString();
                item.DiamondType = row["DiamondType"].ToString();
                item.ProductImageName = row["ProductImageName"].ToString();
                item.ItemLengthType = row["ItemLengthType"].ToString();
                item.ColorStone = row["ColorStone"].ToString();
                arr.Add(item);
            }
            return arr;
        }

    }
}
