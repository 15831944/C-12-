/**编写人：方淑云
 * 时间：2014年8月1号
 * 功能:进修学习（派遣）更新界面后台
 * 修改履历：
 **/
using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUI;

namespace WebApplication1
{
    public partial class Update_DFurtherStudy : System.Web.UI.Page
    {
        BLHelper.BLLDFurtherStudy df = new BLHelper.BLLDFurtherStudy();
        DFurtherStudy dfurtherstudy = new DFurtherStudy();
        BLHelper.BLLUser user = new BLHelper.BLLUser();
        BLHelper.BLLOperationLog op = new BLHelper.BLLOperationLog();
        OperationLog log = new OperationLog();
        BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                tDBegainTime.MaxDate = DateTime.Now;
                InitSecrecyLevel();   
                InitData();        
            }
        }
        public void InitData()
        {
            try
            {
                if (Session["ID"].ToString() != "")
                {
                    DFurtherStudy dfu = df.FindByID(Convert.ToInt32(Session["ID"]));

                    tUser.Text = user.FindByUserID(dfu.UserInfoID.Value);
                    tStudyPlace.Text = dfu.StudyPlace;
                    tStudySchool.Text = dfu.StudySchool;
                    dSecrecyLevel.SelectedValue = (dfu.SecrecyLevel - 1).ToString();
                    tContent.Text = dfu.StudyContent;
                    tDBegainTime.SelectedDate = dfu.DBegainTime;
                    if (dfu.DEndTime != null)
                    {
                        tDEndTime.SelectedDate = dfu.DEndTime;
                    }
                    else
                    {
                        tDEndTime.SelectedDate = null;
                    }

                }
                else
                    return;
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //更新赋值
        public void UpdateValue()
        {
            dfurtherstudy.UserInfoID = user.FindID(tUser.Text.Trim());
            dfurtherstudy.StudySchool = tStudySchool.Text.Trim();
            dfurtherstudy.StudyPlace = tStudyPlace.Text.Trim();
            dfurtherstudy.SecrecyLevel = Convert.ToInt32(dSecrecyLevel.SelectedIndex + 1);
            dfurtherstudy.DBegainTime = tDBegainTime.SelectedDate;
            if (tDEndTime.Text.Trim() != "")
            {
                dfurtherstudy.DEndTime = Convert.ToDateTime(tDEndTime.Text.Trim());
            }
            else
            {
                dfurtherstudy.DEndTime = null;
            }
            dfurtherstudy.StudyContent = tContent.Text.Trim();
            dfurtherstudy.EntryPerson = df.FindByID(Convert.ToInt32(Session["ID"])).EntryPerson;
        }
        
        //用户名textbox验证
        protected void tUser_TextChanged(object sender, EventArgs e)
        {
            if (tUser.Text.Trim() != "")
            {
                if (user.IsUser(tUser.Text.Trim()) == null)
                {
                    Alert.Show("该用户不存在！");
                    tUser.Text = "";
                    return;
                }
                else
                {
                    if (user.IsUser(tUser.Text.Trim()).IsPass == false)
                    {
                        Alert.Show("该用户信息正在审核中！");
                        tUser.Text = "";
                        return;
                    }
                }
            }
        }
         
            //初始化等级下拉框
        public void InitSecrecyLevel()
        {
            for (int i = 0; i < Convert.ToInt32(Session["SecrecyLevel"]); i++)
            {
                string[] SecrecyLevels = new string[] { "四级", "三级", "二级", "一级", "管理员" };
                dSecrecyLevel.Items.Add(SecrecyLevels[i], i.ToString());
            }
        }
        //重置
        protected void DeleteAll_Click(object sender, EventArgs e)
        {
            try
            {
                tUser.Reset();
                tStudySchool.Reset();
                tStudyPlace.Reset();
                dSecrecyLevel.Reset();
                tDBegainTime.Reset();
                tDEndTime.Reset();
                tContent.Reset();
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //保存
        protected void Save_Click(object sender, EventArgs e)
        {
            try
            {
                if (tUser.Text.Trim() == "")
                {
                    Alert.Show("人员姓名不能为空！");
                    return;
                }
                if (tStudyPlace.Text.Trim() == "")
                {
                    Alert.Show("进修地点不能为空!");
                    return;
                }
                if (tStudySchool.Text.Trim() == "")
                {
                    Alert.Show("进修学校为空！");
                    return;
                }
                if (tContent.Text.Trim() == "")
                {
                    Alert.Show("进修内容不能为空！");
                    return;
                }
                if (tDEndTime.SelectedDate < tDBegainTime.SelectedDate)
                {
                    Alert.ShowInTop("结束时间不能小于开始时间！");
                    return;
                }
                if (Convert.ToInt32(Session["SecrecyLevel"]) == 5)
                {
                    dfurtherstudy.DFurtherStudyID = Convert.ToInt32(Session["ID"]);
                    dfurtherstudy.IsPass = true;
                    UpdateValue();
                    df.Update(dfurtherstudy);
                    PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("保存成功"));
                }
                else
                {

                    UpdateValue();
                    dfurtherstudy.IsPass = false;
                    df.Insert(dfurtherstudy);
                    log.LoginName = user.FindByLoginName(Session["LoginName"].ToString()).UserName;
                    log.OperationTime = DateTime.Now;
                    log.LoginIP = " ";
                    log.OperationContent = "DFurtherStudy";
                    log.OperationType = "更新";
                    log.OperationDataID = Convert.ToInt32(Session["ID"]);
                    log.Remark = dfurtherstudy.DFurtherStudyID.ToString();
                    op.Insert(log);
                    df.ChangePass(Convert.ToInt32(Session["ID"]), false);
                    PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("您的数据已提交，请等待确认!"));
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        
    }
}