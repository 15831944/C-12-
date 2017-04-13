/**编写人：王会会
 * 时间：2014年9月14号
 * 功能:修改密码界面后台
 * 修改履历：
 **/ 
using BLHelper;
using Common.Entities;
using FineUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace WDFramework.WebForms
{
    public partial class Pop_Option : System.Web.UI.Page
    {
        BLHelper.BLLUser bllUser = new BLHelper.BLLUser();
        BLCommon.Encrypt encrypt = new BLCommon.Encrypt();
        //UserInfo user = new UserInfo();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        //保存
        protected void Save_Click(object sender, EventArgs e)
        {
            int UserID = bllUser.FindByLoginName(Session["LoginName"].ToString(), Convert.ToInt32(Session["SecrecyLevel"])).FirstOrDefault().UserInfoID;
            if (encrypt.MD5(OldLoginPWD.Text.Trim()) == bllUser.IsUser(bllUser.FindByUserID(UserID)).LoginPWD)
            {
                if (!string.IsNullOrEmpty(NewPWD.Text.Trim()))
                {
                    if (!string.IsNullOrEmpty(IsNewPWD.Text.Trim()))
                    {
                        if (encrypt.MD5(IsNewPWD.Text.Trim()) == encrypt.MD5(NewPWD.Text.Trim()))
                        {
                            bllUser.ChangePWD(UserID, encrypt.MD5(NewPWD.Text.Trim()));
                            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference("密码已更改"));
                        }
                        else
                            Alert.ShowInTop("确认密码填写不正确！");
                    }
                    else
                        Alert.ShowInTop("请填写确认密码！");
                }
                else
                    Alert.ShowInTop("新密码不能为空！");
            }
            else
                Alert.ShowInTop("原密码填写不正确！");
        }

        //protected void OldLoginPWD_TextChanged(object sender, EventArgs e)
        //{
        //    int UserID = bllUser.FindByLoginName(Session["LoginName"].ToString(), Convert.ToInt32(Session["SecrecyLevel"])).FirstOrDefault().UserInfoID;
        //    if (encrypt.MD5(OldLoginPWD.Text) == bllUser.IsUser(bllUser.FindByUserID(UserID)).LoginPWD)
        //        return;
        //    else
        //    {
        //        OldLoginPWD.Text = "";
        //        Alert.ShowInTop("原密码填写不正确！");
        //    }
        //}

        //protected void NewPWD_TextChanged(object sender, EventArgs e)
        //{
        //    if (!string.IsNullOrEmpty(IsNewPWD.Text))
        //        return;
        //    else
        //        Alert.ShowInTop("新密码不能为空！");
        //}

        //protected void IsNewPWD_TextChanged(object sender, EventArgs e)
        //{
        //    if (!string.IsNullOrEmpty(IsNewPWD.Text))
        //    {
        //        if (encrypt.MD5(IsNewPWD.Text) == encrypt.MD5(NewPWD.Text))
        //        {
        //            return;
        //        }
        //        else
        //        {
        //            IsNewPWD.Text = "";
        //            Alert.ShowInTop("确认密码填写不正确！");
        //        }
        //    }
        //    else
        //        Alert.ShowInTop("请填写确认密码！");
        //}
    }
}