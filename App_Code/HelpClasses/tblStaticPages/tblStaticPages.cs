using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
namespace tblStaticPages
{
    /// <summary>
    /// Summary description for tblStaticPages
    /// </summary>
    public class tblStaticPages
    {
        public tblStaticPages()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #region Private variables Declaration
        private int _PageID;
        private string _PageName;
        private string _PageContents;
        #endregion

        #region Public Get Set Properties
        public int PageID
        {
            get { return _PageID; }
            set { _PageID = value; }
        }
        public string PageName
        {
            get { return _PageName; }
            set { _PageName = value; }
        }
        public string PageContents
        {
            get { return _PageContents; }
            set { _PageContents = value; }
        }
        #endregion
    }
}
