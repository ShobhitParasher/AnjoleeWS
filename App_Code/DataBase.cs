using System;
using System.ComponentModel;
using System.Runtime.Remoting;
using System.Collections;
using System.Diagnostics;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Specialized;
using System.Web;

namespace DBComponent
{

	public class DataBase
	{
		private SqlConnection objConnection = null;

		public DataBase()
		{
			
		}
			
		
		public int RunProcedure(string sProcName) 
		{
			SqlCommand objCommand = funCreateCommand(funOpenConnection(), sProcName, null);
			objCommand.ExecuteNonQuery();
			this.funCloseConnection();
			return (int)objCommand.Parameters["ReturnValue"].Value;
		}

		
		public int RunProcedure(string sProcName, SqlParameter[] objaPrams) 
		{
			SqlCommand objCommand = funCreateCommand(funOpenConnection(), sProcName, objaPrams);
			objCommand.ExecuteNonQuery();
			this.funCloseConnection();
			return (int)objCommand.Parameters["ReturnValue"].Value;
		}

	
		public void RunProcedure(string sProcName, out SqlDataReader objDataReader) 
		{
			SqlCommand objCommand = funCreateCommand(funOpenConnection(), sProcName, null);
			objDataReader = objCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
		}

	
		public void RunProcedure(string sProcName, SqlParameter[] objaPrams, out SqlDataReader objDataReader) 
		{
			SqlCommand objCommand = funCreateCommand(funOpenConnection(), sProcName, objaPrams);
			objDataReader = objCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
		}
		
		
		public void RunProcedure(string sProcName, SqlParameter[] objaPrams, out DataSet objDataSet) 
		{
			
			SqlDataAdapter objDataAdapter = new SqlDataAdapter();
			
            objConnection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Constr"].ToString());
			SqlCommand objCommand = funCreateCommand(funOpenConnection(), sProcName, objaPrams);
			objDataAdapter.SelectCommand = objCommand;
			objDataSet  = new DataSet();
			objDataAdapter.Fill(objDataSet);
			
			this.funCloseConnection();
		}
        public void RunProcedure(string sProcName, SqlParameter[] objaPrams, out DataTable objDataTable)
        {
            
            SqlDataAdapter objDataAdapter = new SqlDataAdapter();
            
            objConnection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Constr"].ToString());
            SqlCommand objCommand = funCreateCommand(funOpenConnection(), sProcName, objaPrams);
            objDataAdapter.SelectCommand = objCommand;
            objDataTable = new DataTable();
            objDataAdapter.Fill(objDataTable);
            
            this.funCloseConnection();
        }
		
		private SqlCommand funCreateCommand(SqlConnection objConnection, string sProcName, SqlParameter[] objaPrams) 
		{
			SqlCommand objCommand = new SqlCommand(sProcName, objConnection);
			objCommand.CommandType = CommandType.StoredProcedure;

		
			if (objaPrams != null) 
			{
				foreach (SqlParameter objParameter in objaPrams)
					objCommand.Parameters.Add(objParameter);
			}			
			
			objCommand.Parameters.Add(new SqlParameter("ReturnValue", SqlDbType.Int,  4, ParameterDirection.ReturnValue, false, 0, 0,string.Empty, DataRowVersion.Default, null));

			return objCommand;
		}

		
		private SqlConnection funOpenConnection() 
		{
			try
			{
                string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["Constr"].ToString();
				objConnection = new SqlConnection(connStr);
				objConnection.Open();		
			}
			catch(Exception objException)
			{ 
				Debug.WriteLine("Exception :  " + objException.StackTrace );
				return null;
			}		
			return objConnection;
		}

		
		public void funCloseConnection() 
		{
			if(objConnection != null)
				objConnection.Close();
		}

		public void Dispose() 
		{
		
			if (objConnection != null) 
			{
				objConnection.Dispose();
				objConnection = null;
			}				
		}

		
		public SqlParameter MakeInParameter(string sParamName, SqlDbType objDbType, int iSize, object objValue) 
		{
			return MakeParameter(sParamName, objDbType, iSize, ParameterDirection.Input, objValue);
		}

       
        public SqlParameter MakeInParameter(string sParamName, object objValue)
        {
            return MakeParameter(sParamName, objValue);
        }

        public SqlParameter MakeOutParameter(string sParamName)
        {
            return MakeParameter(sParamName, ParameterDirection.Output, null);
        }

		
		public SqlParameter MakeOutParameter(string sParamName, SqlDbType objDbType, int iSize) 
		{
			return MakeParameter(sParamName, objDbType, iSize, ParameterDirection.Output, null);
		}		

		
		public SqlParameter MakeParameter(string sParamName, SqlDbType objDbType, Int32 iSize, ParameterDirection objDirection, object objValue) 
		{
			SqlParameter objParameter;

			if(iSize > 0)
				objParameter = new SqlParameter(sParamName, objDbType, iSize);
			else
				objParameter  = new SqlParameter(sParamName, objDbType);

			objParameter.Direction = objDirection;
			if (!(objDirection == ParameterDirection.Output && objValue == null))
				objParameter.Value = objValue;

			return objParameter;
		}
        public SqlParameter MakeParameter(string sParamName, ParameterDirection objDirection, object objValue)
        {
            SqlParameter objParameter;


            objParameter = new SqlParameter(sParamName, objValue);

           
            objParameter.Direction = objDirection;
            if (!(objDirection == ParameterDirection.Output && objValue == null))
                objParameter.Value = objValue;

            return objParameter;
        }


       
        public SqlParameter MakeParameter(string sParamName, object objValue)
        {
            SqlParameter objParameter;


            objParameter = new SqlParameter(sParamName, objValue);

         

            return objParameter;
        }
	}
}
