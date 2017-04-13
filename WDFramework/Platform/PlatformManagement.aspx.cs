using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WDFramework.Platform
{
    public partial class PlatformManagement : System.Web.UI.Page
    {
        BLHelper.BLLPlatform BLLPlatform = new BLHelper.BLLPlatform();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                InitData();
            }
        }

        public void InitData()
        {
            try
            {
                int id = Convert.ToInt32(Request.QueryString["id"].ToString());
                string str = BLLPlatform.FindByPlatformID(id).PlatformManagement;
                if (str != "")
                {
                    Contents.Text = str;
                }
                else
                {
                    Contents.Text = "暂无";
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}