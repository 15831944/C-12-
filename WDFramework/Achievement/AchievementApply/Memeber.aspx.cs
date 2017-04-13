using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WDFramework.Achievement.AchievementApply
{
    public partial class Memeber : System.Web.UI.Page
    {

        BLHelper.BLLAchievementApply sha = new BLHelper.BLLAchievementApply();

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
                string str = sha.FindName(id);
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