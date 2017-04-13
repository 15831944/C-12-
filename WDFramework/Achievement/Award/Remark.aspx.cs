/**编写人：方淑云
 * 时间：2014年8月16号
 * 功能:备注界面后台
 * 修改履历：
 **/ 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WDFramework.Acheievement.Award
{
    public partial class Remark : System.Web.UI.Page
    {
        BLHelper.BLLAward aw = new BLHelper.BLLAward();
        BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
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
                string str = aw.FindRemark(id);
                if (str != "" || str != null)
                {
                    Content.Text = str;
                }
                else
                {
                    Content.Text = " ";
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
    }
}