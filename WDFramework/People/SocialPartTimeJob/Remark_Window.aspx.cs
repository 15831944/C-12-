using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WDFramework.People.SocialPartTimeJob
{
    public partial class Remark_Window : System.Web.UI.Page
    {
        BLHelper.BLLSocialPartTime  bllSocial = new BLHelper.BLLSocialPartTime ();
        BLCommon.PublicMethod publicmethod = new BLCommon.PublicMethod();
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
                string str = bllSocial.FindRemark(id);
                if (str != "")
                {
                    Content.Text = str;
                }
                else
                {
                    Content.Text = "暂无";
                }
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
    }
}