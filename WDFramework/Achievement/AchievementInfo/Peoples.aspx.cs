using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WDFramework.Achievement.AchievementInfo
{
    public partial class Peoples : System.Web.UI.Page
    {
        BLHelper.BLLStaffAchieve sp = new BLHelper.BLLStaffAchieve();
        BLHelper.BLLUser us = new BLHelper.BLLUser();
        BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                InitData();
            }
        }
        //初始化
        public void InitData()
        {
            try
            {
                int id = Convert.ToInt32(Request.QueryString["id"].ToString());
                List<int> list = sp.FindStaName(id);
                string writername = "";
                for (int i = 0; i < list.Count; i++)
                {
                    writername += us.FindUserName(list[i]);
                    if (i == list.Count() - 1)
                    {
                        break;
                    }
                    writername += ",";
                }
                if (writername != "" || writername != null)
                {
                    Contents.Text = writername;
                }
                else
                {
                    Contents.Text = " ";
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
    }
}