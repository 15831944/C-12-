/**编写人：方淑云
 * 时间：2014年8月17号
 * 功能：备注界面的相关操作
 * 修改履历：
 **/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WDFramework.Acheievement.Patent
{
    public partial class Remark : System.Web.UI.Page
    {
        BLHelper.BLLPatent patent = new BLHelper.BLLPatent();
        BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
        protected void Page_Load(object sender, EventArgs e)
        {

            InitData();
        }
        //初始化
        public void InitData()
        {
            try
            {
                int id = Convert.ToInt32(Request.QueryString["id"].ToString());
                string str = patent.FindByPatentID(id).Comment;
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