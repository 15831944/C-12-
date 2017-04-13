
/**编写人：方淑云
 * 时间：2014年8月14号
 * 功能:项目验收添加界面后台
 * 修改履历：
 **/
using FineUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common.Entities;

namespace WebApplication1.Achievement.AchievementCA
{
    public partial class Add_AchievementCA : System.Web.UI.Page
    {
        BLHelper.BLLAchievement ach = new BLHelper.BLLAchievement();
        BLHelper.BLLOperationLog op = new BLHelper.BLLOperationLog();
        BLHelper.BLLAchievementCA ca = new BLHelper.BLLAchievementCA();
        BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
        BLHelper.BLLAttachment at = new BLHelper.BLLAttachment();
        BLHelper.BLLUser user = new BLHelper.BLLUser();
        Common.Entities.AchievementCA ahievementca = new Common.Entities.AchievementCA();
        OperationLog operate = new OperationLog();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dCATime.MaxDate = DateTime.Now;
                InitSecrecyLevel();
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
        //项目验证
        protected void tAchievement_TextChanged(object sender, EventArgs e)
        {
            if (tAchievement.Text.Trim() != "")
            {
                if (ach.IsExitAchieveName(tAchievement.Text.Trim()) == null)
                {
                    Alert.Show("该项目名不存在！");
                    tAchievement.Text = "";
                    return;
                }
                else
                {
                    if (ach.IsExitAchieveName(tAchievement.Text.Trim()).IsPass == false)
                    {
                        Alert.Show("该项目名称正在审核中！");
                        tAchievement.Text = "";
                        return;
                    }
                }
            }
        }
        //保存赋值
        public void InsertValue()
        {
            ahievementca.CACommnetLevel = tCACommnetLevel.Text.Trim();
            ahievementca.AchievementID = ach.FindByAchievementName(tAchievement.Text.Trim());
            ahievementca.CATime = dCATime.SelectedDate;
            ahievementca.CAUnit = tCAUnit.Text.Trim();
            ahievementca.ProjectMember = tMember.Text.Trim();
            ahievementca.SecrecyLevel =Convert.ToInt32 (dSecrecyLevel.SelectedIndex + 1);
        }
        //保存
        protected void Save_Click(object sender, EventArgs e)
        {
            /*try
            {*/
      
                if (tAchievement.Text.Trim() == "")
                {
                    Alert.Show("项目名称不能为空！");
                    return;
                }
                if (tCAUnit.Text.Trim() == "")
                {
                    Alert.Show("验收部门不能为空！");
                    return;
                }
                if (tCAUnit.Text.Trim() == "")
                {
                    Alert.Show("成员不能为空！");
                    return;
                }
                if (tCACommnetLevel.Text.Trim() == "")
                {
                    Alert.Show("验收评语级别不能为空！");
                    return;
                }
                string username = user.FindByLoginName(Session["LoginName"].ToString()).UserName;
                ahievementca.EntryPerson = username;
                if (Convert.ToInt32(Session["SecrecyLevel"]) == 5)
                {
                    ahievementca.IsPass = true;
                    int attachid = pm.UpLoadFile(fileupload).Attachid;
                    switch (attachid)
                    {
                        case -1:
                            Alert.ShowInTop("文件类型不符，请重新选择！");
                            return;
                        case 0:
                            Alert.ShowInTop("文件名已经存在！");
                            return;
                        case -2:
                            Alert.ShowInTop("文件不能大于150M");
                            return;
                    }
                    if (attachid != -3)
                    {
                        ahievementca.AttachmentID = attachid;
                    }
                    else
                    {
                        ahievementca.AttachmentID = null;
                    }
                    InsertValue();
                    ca.Insert(ahievementca);
                    PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("保存成功！"));
                }
                else
                {
                    ahievementca.IsPass = false;
                    int attachid = pm.UpLoadFile(fileupload).Attachid;
                    switch (attachid)
                    {
                        case -1:
                            Alert.ShowInTop("文件类型不符，请重新选择！");
                            return;
                        case 0:
                            Alert.ShowInTop("文件名已经存在！");
                            return;
                        case -2:
                            Alert.ShowInTop("文件不能大于150M");
                            return;
                    }
                    if (attachid != -3)
                    {
                        ahievementca.AttachmentID = attachid;
                    }
                    else
                    {
                        ahievementca.AttachmentID = null;
                    }
                    InsertValue();
                    ca.Insert(ahievementca);
                    operate.LoginName = username;
                    operate.OperationTime = DateTime.Now;
                    operate.LoginIP = " ";
                    operate.OperationContent = "AchievementCA";
                    operate.OperationType = "添加";
                    operate.OperationDataID = ahievementca.AchievementCAID;
                    op.Insert(operate);
                    PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("您的数据已提交，请等待确认！"));
                }
            /*}
            catch (Exception ex)
            {
                int attachid = Convert.ToInt32(ahievementca.AttachmentID);
                string path = at.FindPath(attachid);
                pm.DeleteFile(attachid, path);
                pm.SaveError(ex, this.Request);
            }*/
        }
        //重置
        protected void DeleteAll_Click(object sender, EventArgs e)
        {
            try
            {
                tCACommnetLevel.Reset();
                tAchievement.Reset();
                dCATime.Reset();
                tCAUnit.Reset();
                tMember.Reset();
                dSecrecyLevel.Reset();
                PageContext.RegisterStartupScript("clearFile();");
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }

    }
}