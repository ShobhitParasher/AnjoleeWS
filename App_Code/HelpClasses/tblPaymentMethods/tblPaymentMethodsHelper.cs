using System;
using System.Data.SqlClient;
using DBComponent;
using System.Data;
namespace tblPaymentMethods
{
	/// <summary>
	/// Summary description for tblPaymentMethodsHelper.
	/// </summary>
	public class tblPaymentMethodsHelper
	{
		private DataBase db;
		SqlParameter[] param;
		DataSet ds;
		public tblPaymentMethodsHelper()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		public int insertpaymentMethod(tblPaymentMethods otblPaymentMethods, string Mode)
		{
			int status=-1;
			try
			{
				if(object.Equals(db,null))
				{
					db=new DataBase();
				}
				param=new SqlParameter[9];
				param[0]=db.MakeInParameter("@paymentMethodID",SqlDbType.Int,4,otblPaymentMethods.paymentMethodID);
				param[1]=db.MakeInParameter("@online",SqlDbType.Int,4,otblPaymentMethods.online);
				param[2]=db.MakeInParameter("@paymentMethodName",SqlDbType.VarChar,50,otblPaymentMethods.paymentMethodName);
				param[3]=db.MakeInParameter("@shortDescription",SqlDbType.VarChar,200,otblPaymentMethods.shortDescription);
				param[4]=db.MakeInParameter("@isCC",SqlDbType.Int,4,otblPaymentMethods.isCC);
				param[5]=db.MakeInParameter("@isDefault",SqlDbType.Int,4,otblPaymentMethods.isDefault);
				param[6]=db.MakeInParameter("@acceptedCards",SqlDbType.NText,0,otblPaymentMethods.acceptedCards);
				param[7]=db.MakeInParameter("@Mode",SqlDbType.VarChar,10,Mode);				
				param[8]=db.MakeOutParameter("@Status",SqlDbType.Int,4);
				db.RunProcedure("P_InsertPaymentMethods",param);
     		    status=(int)param[8].Value;               
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
		public DataSet GetPaymentMethod(int paymentMethodID)
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
			param[0]=db.MakeInParameter("@paymentMethodID",SqlDbType.Int,4,paymentMethodID);
			db.RunProcedure("P_GetPaymentMethod",param,out ds);
			ResetAll();
			return ds;
		}
		public void DeletePaymentMethods(int paymentMethodID)
		{
			if(object.Equals(db,null))
			{
				db=new DataBase();
			}
			param=new SqlParameter[1];
			param[0]=db.MakeInParameter("@paymentMethodID",SqlDbType.Int,4,paymentMethodID);
			db.RunProcedure("P_DeletePaymentMethods",param);
			ResetAll();
		}
		private void ResetAll()
		{
			param=null;
			db=null;
		}	
	}
}