using System;
using System.Data;
using System.Data.SqlClient;
using DBComponent;

namespace tblStore_Tax
{
	/// <summary>
	/// Summary description for tblStore_TaxHelper.
	/// </summary>
	public class tblStore_TaxHelper
	{
		public tblStore_TaxHelper()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		private DataBase db;
		SqlParameter[] param;
		DataSet ds;
		public DataSet GetTaxDescription(int taxId)
		{
			if(object.Equals(db,null))
			{
				db=new DataBase();
			}
			if(object.Equals(ds,null))
			{
				ds=new DataSet();
			}
			param = new SqlParameter[1];
			param[0]=db.MakeInParameter("@taxId",SqlDbType.Int,4,taxId);
			db.RunProcedure("P_seltaxdata",param,out ds);
			ResetAll();
			return ds;
		}
		public DataSet GetTaxrule()
		{
			if(object.Equals(db,null))
			{
				db=new DataBase();
			}
			if(object.Equals(ds,null))
			{
				ds=new DataSet();
			}
			param=new SqlParameter[2];
			db.RunProcedure("P_GetTaxrule",null,out ds);
			ResetAll();
			return ds;
		}
		public void DeleteTax(int @taxID)
		{
			if(object.Equals(db,null))
			{
				db=new DataBase();
			}
			param=new SqlParameter[1];
			param[0]=db.MakeInParameter("@taxID",SqlDbType.Int,4,@taxID);
			db.RunProcedure("P_DeleteTax",param);
			ResetAll();
		}
		public int InsertTax(tblStore_Tax otblStore_Tax, string Mode)
		{
			int status=-1;
			try
			{
				if(object.Equals(db,null))
				{
					db=new DataBase();
				}
				param=new SqlParameter[5];
				param[0]=db.MakeInParameter("@taxID",SqlDbType.Int,4,otblStore_Tax.taxID);
                //param[1]=db.MakeInParameter("@countryID",SqlDbType.Int,4,otblStore_Tax.countryID);
                //param[2]=db.MakeInParameter("@stateID",SqlDbType.Int,4,otblStore_Tax.stateID);
                //param[3]=db.MakeInParameter("@description",SqlDbType.VarChar,30,otblStore_Tax.description);
				param[1]=db.MakeInParameter("@rate",SqlDbType.Decimal,8,otblStore_Tax.rate);
                param[2] = db.MakeInParameter("@zipcode", SqlDbType.VarChar, 20, otblStore_Tax.zipcode);
				param[3]=db.MakeInParameter("@Mode",SqlDbType.VarChar,10,Mode);				
				param[4]=db.MakeOutParameter("@Status",SqlDbType.Int,4);
				db.RunProcedure("P_InsertTax",param);
				status=(int)param[4].Value;
			}
			catch(Exception ex)
			{
				string test=ex.ToString();
				string test1=ex.Message;
			}
			finally
			{
				ResetAll();
			}
			return status;
		}
		private void ResetAll()
		{
			param=null;
			db=null;
		}
	}
}