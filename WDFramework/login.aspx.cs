/**编写人：方淑云
 * 时间：2014年8月14号
 * 功能:登录界面
 * 修改履历：
 **/
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using FineUI;
using BLHelper;
using Common.Entities;

namespace WDFramework
{
    public partial class login : System.Web.UI.Page
    {
        BLHelper.BLLUser bll = new BLHelper.BLLUser();
        BLCommon.Encrypt bllEncryt = new BLCommon.Encrypt();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                tbxUserName.Text = "";
                tbxPassword.Text = "";
                Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
                //LoadData();
            }
           
            Session["LoginName"] = tbxUserName.Text;
        }


        //private void LoadData()
        //{
        //    InitCaptchaCode();
        //}

        /// <summary>
        /// 初始化验证码
        /// </summary>
        //private void InitCaptchaCode()
        //{
        //    // 创建一个 6 位的随机数并保存在 Session 对象中
        //    Session["CaptchaImageText"] = GenerateRandomCode();
        //    imgCaptcha.ImageUrl = "~/captcha/captcha.ashx?w=150&h=30&t=" + DateTime.Now.Ticks;
        //}

        /// <summary>
        /// 创建一个 6 位的随机数
        /// </summary>
        /// <returns></returns>
        //private string GenerateRandomCode()
        //{
        //    string s = String.Empty;
        //    Random random = new Random();
        //    for (int i = 0; i < 6; i++)
        //    {
        //        s += random.Next(10).ToString();
        //    }
        //    return s;
        //}

        //protected void btnRefresh_Click(object sender, EventArgs e)
        //{
        //    InitCaptchaCode();
        //}

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            BLLUser bu = new BLLUser();
            UserInfo user = bu.Login(tbxUserName.Text, bllEncryt.MD5(tbxPassword.Text));
            //if (tbxCaptcha.Text != Session["CaptchaImageText"].ToString())
            //{
            //    Alert.ShowInTop("验证码错误！");
            //    return;
            //}
           
            if (tbxUserName.Text.Trim() == "" && tbxUserName.Text.Trim() == "")
            {
                Alert.ShowInTop("用户名或密码不能为空！", MessageBoxIcon.Error);
                return;
            }
            //if (tbxCaptcha.Text == "")
            //{
            //    Alert.ShowInTop("请输入验证码！", MessageBoxIcon.Error);
            //    return;
            //}
            if (user != null)
            {
                Session["SecrecyLevel"] = bll.FindLevel(tbxUserName.Text);
                Session["LoginName"] = tbxUserName.Text;
                Session["load"] = true;
                Session["IsLogin"] = true;
               // this.Session.Timeout = 60;
                Alert.ShowInTop("成功登录！");
                Response.Redirect("Default.aspx");
            }
            else
            {
                Alert.ShowInTop("用户名或密码错误！", MessageBoxIcon.Error);
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }
    }
}
