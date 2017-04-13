/**编写人：方淑云
 * 时间：2014年9月13号
 * 功能:错误界面
 * 修改履历：
 **/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WDFramework.Information
{
    public partial class Error : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        //返回首页
        protected void Back_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/login.aspx");
        }
    }
}