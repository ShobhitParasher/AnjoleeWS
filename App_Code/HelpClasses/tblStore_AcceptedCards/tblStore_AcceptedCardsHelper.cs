using System;
using System.Data;
using System.Data.SqlClient;
using DBComponent;
namespace tblStore_AcceptedCards
{
    /// <summary>
    /// Summary description for tblStore_AcceptedCardsHelper.
    /// </summary>
    public class tblStore_AcceptedCardsHelper
    {
        public tblStore_AcceptedCardsHelper()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        private DataBase db;
        SqlParameter[] param;
        DataSet ds;
        public DataSet GetCards(string cardIDs)
        {
            if (object.Equals(db, null))
            {
                db = new DataBase();
            }
            if (object.Equals(ds, null))
            {
                ds = new DataSet();
            }
            param = new SqlParameter[1];
            param[0] = db.MakeInParameter("@cardIDs", SqlDbType.VarChar, 200, cardIDs);
            db.RunProcedure("P_GetPaymentCards", param, out ds);
            ResetAll();
            return ds;
        }
        public DataSet GetCards(int cardID)
        {
            if (object.Equals(db, null))
            {
                db = new DataBase();
            }
            if (object.Equals(ds, null))
            {
                ds = new DataSet();
            }
            param = new SqlParameter[1];
            param[0] = db.MakeInParameter("@cardID", SqlDbType.Int, 4, cardID);
            db.RunProcedure("P_GetCardsByID", param, out ds);
            ResetAll();
            return ds;
        }
        public DataSet GetCards()
        {
            if (object.Equals(db, null))
            {
                db = new DataBase();
            }
            if (object.Equals(ds, null))
            {
                ds = new DataSet();
            }
            db.RunProcedure("P_GetCards", null, out ds);
            ResetAll();
            return ds;
        }
        public DataSet GetSelectedCards(string cardIDs)
        {
            if (object.Equals(db, null))
            {
                db = new DataBase();
            }
            if (object.Equals(ds, null))
            {
                ds = new DataSet();
            }
            param = new SqlParameter[1];
            param[0] = db.MakeInParameter("@cardIDs", SqlDbType.VarChar, 200, cardIDs);
            db.RunProcedure("P_GetSelectedCardCards", param, out ds);
            ResetAll();
            return ds;
        }
        public void DeleteCard(int cardID)
        {
            if (object.Equals(db, null))
            {
                db = new DataBase();
            }
            param = new SqlParameter[1];
            param[0] = db.MakeInParameter("@cardID", SqlDbType.Int, 4, cardID);
            db.RunProcedure("P_DeleteCard", param);
            ResetAll();
        }
        public int InsertCard(tblStore_AcceptedCards otblStore_AcceptedCards, string Mode)
        {
            int status = -1;
            try
            {
                if (object.Equals(db, null))
                {
                    db = new DataBase();
                }
                param = new SqlParameter[6];
                param[0] = db.MakeInParameter("@cardID", SqlDbType.Int, 4, otblStore_AcceptedCards.cardID);
                param[1] = db.MakeInParameter("@cardName", SqlDbType.NVarChar, 20, otblStore_AcceptedCards.cardName);
                param[2] = db.MakeInParameter("@cardLogo", SqlDbType.NVarChar, 50, otblStore_AcceptedCards.cardLogo);
                param[3] = db.MakeInParameter("@cardOnline", SqlDbType.Int, 4, otblStore_AcceptedCards.cardOnline);
                param[4] = db.MakeInParameter("@Mode", SqlDbType.VarChar, 10, Mode);
                param[5] = db.MakeOutParameter("@Status", SqlDbType.Int, 4);
                db.RunProcedure("P_InsertCard", param);
                status = (int)param[5].Value;
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
            param = null;
            db = null;
        }
    }
}