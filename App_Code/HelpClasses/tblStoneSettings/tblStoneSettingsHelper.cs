using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DBComponent;

namespace tblstonesettings
{
/// <summary>
/// Summary description for StoneSettingsHelper
/// </summary>
public class tblStoneSettingsHelper
{
    DataBase db = null;
    SqlParameter[] param;
    //DataSet ds;

    public tblStoneSettingsHelper()
	{
    }

    # region Public Methods

    public void InsertStonesSettings(tblstonesettings.tblStoneSettings otblStoneSettings)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ToString()); ;
        try
        {
            con.Open();
            SqlCommand cmd1 = new SqlCommand("InsertStoneSettings", con);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.Add(new SqlParameter("@StoneSettingID", otblStoneSettings.StoneSettingID));
            cmd1.Parameters.Add(new SqlParameter("@StoneSettingName", otblStoneSettings.StoneSettingName));
            cmd1.Parameters.Add(new SqlParameter("@Description", otblStoneSettings.Description));
            cmd1.Parameters.Add(new SqlParameter("@Price", otblStoneSettings.Price));
            cmd1.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            string strMsg = ex.Message;
            throw new Exception("Problem In Saving Record.");
        }
        finally
        {
            if (con.State.ToString() == "Open")
                con.Close();
            ResetAll();
        }
    }

    public void UpdateStonesSettings(tblstonesettings.tblStoneSettings otblStoneSettings)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ToString()); ;
        try
        {
            con.Open();
            SqlCommand cmd1 = new SqlCommand("UpdateStoneSettings", con);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.Add(new SqlParameter("@StoneSettingID", otblStoneSettings.StoneSettingID));
            cmd1.Parameters.Add(new SqlParameter("@StoneSettingName", otblStoneSettings.StoneSettingName));
            cmd1.Parameters.Add(new SqlParameter("@Description", otblStoneSettings.Description));
            cmd1.Parameters.Add(new SqlParameter("@Price", otblStoneSettings.Price));
            cmd1.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            string strMsg = ex.Message;
            throw new Exception("Problem In Updating Record.");
        }
        finally
        {
            if (con.State.ToString() == "Open")
                con.Close();
            ResetAll();
        }
    }

    #endregion

    #region Private Methods

    private void ResetAll()
    {
        db = null;
        param = null;
    }

    #endregion
}
}