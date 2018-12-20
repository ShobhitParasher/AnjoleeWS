using System;
using System.Data;
using System.Data.SqlClient;
using DBComponent;
namespace tbl_Newsletter
{
	/// <summary>
	/// Summary description for tbl_NewsletterOnlyHelper.
	/// </summary>
	public class tbl_NewsletterHelper
	{
		private DataBase db;
		SqlParameter[] param;
        //DataSet ds;
		public tbl_NewsletterHelper()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		public int Inserttbl_Newsletter(tbl_Newsletter otbl_Newsletter)
		{
			int status=-1;
			try
			{
				if(object.Equals(db,null))
				{
					db=new DataBase();
				}
				param=new SqlParameter[4];
				param[0]=db.MakeInParameter("@email_address",SqlDbType.VarChar,200,otbl_Newsletter.email_address);
				param[1]=db.MakeInParameter("@ip_address",SqlDbType.VarChar,50,otbl_Newsletter.ip_address);
				param[2]=db.MakeInParameter("@date_signed",SqlDbType.VarChar,50,otbl_Newsletter.date_signed);
				param[3]=db.MakeOutParameter("@Status",SqlDbType.Int,4);
                db.RunProcedure("P_InsertNewsletterIphone", param);
				status=(int)param[3].Value;
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
        public int Inserttbl_Newsletterweb(tbl_Newsletter otbl_Newsletter)
        {
            int status = -1;
            try
            {
                if (object.Equals(db, null))
                {
                    db = new DataBase();
                }
                param = new SqlParameter[4];
                param[0] = db.MakeInParameter("@email_address", SqlDbType.VarChar, 200, otbl_Newsletter.email_address);
                param[1] = db.MakeInParameter("@ip_address", SqlDbType.VarChar, 50, otbl_Newsletter.ip_address);
                param[2] = db.MakeInParameter("@date_signed", SqlDbType.VarChar, 50, otbl_Newsletter.date_signed);
                param[3] = db.MakeOutParameter("@Status", SqlDbType.Int, 4);
                db.RunProcedure("P_InsertNewsletter", param);
                status = (int)param[3].Value;
            }
            catch (Exception ex)
            {
                string test = ex.ToString();
                string test1 = ex.Message;
            }
            finally
            {
                ResetAll();
            }
            return status;
        }

        public int Inserttbl_NewsSubcribeletter(tbl_Newsletter otbl_Newsletter)
        {
            int status = -1;
            try
            {
                if (object.Equals(db, null))
                {
                    db = new DataBase();
                }
                param = new SqlParameter[5];
                param[0] = db.MakeInParameter("@email_address", SqlDbType.VarChar, 200, otbl_Newsletter.email_address);
                param[1] = db.MakeInParameter("@ip_address", SqlDbType.VarChar, 50, otbl_Newsletter.ip_address);
                param[2] = db.MakeInParameter("@date_signed", SqlDbType.VarChar, 50, otbl_Newsletter.date_signed);
                param[3] = db.MakeInParameter("@Flag", SqlDbType.Int, 50, otbl_Newsletter.Flag);
                param[4] = db.MakeOutParameter("@Status", SqlDbType.Int, 4);
                db.RunProcedure("P_InsertSubcribeNewsletter", param);
                status = (int)param[4].Value;
            }
            catch (Exception ex)
            {
                string test = ex.ToString();
                string test1 = ex.Message;
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