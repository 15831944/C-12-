/**编写人：王会会
 * 时间：2014年8月24号
 * 功能：管理员修改基本代码表数据的相关操作
 * 修改履历：
 **/
using Common.Entities;
using FineUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WDFramework.People.ManageBasicCode
{
    public partial class Update_BasicCode : System.Web.UI.Page
    {
        BLHelper.BLLBasicCode bllBasicCode = new BLHelper.BLLBasicCode();
        BasicCode basiccode = new BasicCode();
        BLCommon.PublicMethod publicmethod = new BLCommon.PublicMethod();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }
        }
        public void BindData()
        {
            try
            {
                List<BasicCode> list = bllBasicCode.FindByBasicCodeID(Convert.ToInt32(Session["BasicCodeID"]));
                BasicCode basic = list.FirstOrDefault();
                CategoryName.Text = basic.CategoryName;
                //DropDownListCategoryName.SelectedValue = basic.CategoryName;
                CategoryContent.Text = basic.CategoryContent;
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
        //保存
        protected void Save_Click(object sender, EventArgs e)
        {
            try
            {
                if (CategoryContent.Text.Trim() == "")
                {
                    Alert.ShowInTop("分类内容不能为空！");
                    CategoryContent.Text = "";
                    return;
                }
                basiccode.BasicCodeID = Convert.ToInt32(Session["BasicCodeID"]);
                basiccode.CategoryName = CategoryName.Text.Trim();
                basiccode.CategoryContent = CategoryContent.Text.Trim ();
                bllBasicCode.Update(basiccode);
                PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("修改成功！"));
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
        //重置
        protected void Reset_Click(object sender, EventArgs e)
        {
            try
            {
                CategoryName.Reset();
                CategoryContent.Reset();
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
    }
}