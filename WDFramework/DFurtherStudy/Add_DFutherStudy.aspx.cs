/**编写人：方淑云
 * 时间：2014年8月1号
 * 功能:进修学习（派遣）添加界面后台
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

namespace WebApplication1
{
    public partial class Add_DFutherStudy : System.Web.UI.Page
    {
        BLHelper.BLLDFurtherStudy df = new BLHelper.BLLDFurtherStudy();
        BLHelper.BLLUser user = new BLHelper.BLLUser();
        BLHelper.BLLOperationLog op = new BLHelper.BLLOperationLog();
        OperationLog log = new OperationLog();
        BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
        DFurtherStudy furtherstudy = new DFurtherStudy();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                tDBegainTime.MaxDate = DateTime.Now;
                InitSecrecyLevel();
            }
        }

        //插入赋值
        public void InSertValue()
        {
            furtherstudy.StudyPlace = tStudyPlace.Text.Trim();
            furtherstudy.StudySchool = tStudySchool.Text.Trim();
            furtherstudy.SecrecyLevel = Convert.ToInt32(dSecrecyLevel.SelectedIndex + 1);
            furtherstudy.DBegainTime = tDBegainTime.SelectedDate;               
            furtherstudy.DEndTime = tDEndTime.SelectedDate;         
            furtherstudy.UserInfoID = user.FindID(tUser.Text.Trim());
            furtherstudy.StudyContent = tContent.Text.Trim();
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
                string username = user.FindByLoginName(Session["LoginName"].ToString()).UserName;
                furtherstudy.EntryPerson = username;
                if (Convert.ToInt32(Session["SecrecyLevel"]) == 5)
                {
                    furtherstudy.IsPass = true;
                    InSertValue();
                    df.Insert(furtherstudy);
                    PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("保存成功"));
                }
                else
                {
                    furtherstudy.IsPass = false;
                    InSertValue();
                    df.Insert(furtherstudy);
                    log.LoginName = username;
                    log.OperationTime = DateTime.Now;
                    log.LoginIP = " ";
                    log.OperationContent = "DFurtherStudy";
                    log.OperationType = "添加";
                    log.OperationDataID = furtherstudy.DFurtherStudyID;
                    op.Insert(log);
                    PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("您的数据已提交，请等待确认！"));
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
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


    }
}