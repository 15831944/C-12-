using Common.Entities;
using FineUI;
/**编写人：方淑云
 * 时间：2014年8月14号
 * 功能:成果基本信息添加界面后台
 * 修改履历：
 **/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1.Achievement.Achievement
{
    public partial class Add_Achievement : System.Web.UI.Page
    {

        BLHelper.BLLAchievement achieve = new BLHelper.BLLAchievement();
        Common.Entities.Achievement ach = new Common.Entities.Achievement();
        BLHelper.BLLOperationLog op = new BLHelper.BLLOperationLog();
        BLHelper.BLLProject project = new BLHelper.BLLProject();
        BLHelper.BLLUser user = new BLHelper.BLLUser();
        BLHelper.BLLAttachment at = new BLHelper.BLLAttachment();
        BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
        OperationLog log = new OperationLog();
        BLHelper.BLLBasicCode bllBasicCode = new BLHelper.BLLBasicCode();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dAppraisalTime.MaxDate = DateTime.Now;

                InitSecrecyLevel();
                InitDropListAgency();
                //初始化鉴定级别下拉框
                InitDropDownListProjectRank();
                //初始化鉴定形式下拉框
                InitDropDownListProjectForm();
                //初始化鉴定水平下拉框
                InitDropDownListProjectLevel();
            }
        }
        //初始化机构下拉框
        public void InitDropListAgency()
        {
            BLHelper.BLLAgency agency = new BLHelper.BLLAgency();
            List<Common.Entities.Agency> list = agency.FindAllAgencyName();
            for (int i = 0; i < list.Count(); i++)
            {
                DropDownListAgency.Items.Add(list[i].AgencyName.ToString(), list[i].AgencyName.ToString());
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

        //添加
        protected void Save_Click(object sender, EventArgs e)
        {
            try
            {

                if (tAchievementName.Text.Trim() == "")
                {
                    Alert.Show("成果名称不能为空！");
                    return;
                }
                if (tAppraisalUnit.Text.Trim() == "")
                {
                    Alert.Show("鉴定组织部门不能为空！");
                    return;
                }
                if (tApRemarkRank.Text.Trim() == "")
                {
                    Alert.Show("成果登记号不能为空！");
                    return;
                }
                if (tApprovalNum.Text.Trim() == "")
                {
                    Alert.Show("证书文号不能为空！");
                    return;
                }
                if (ProjectInNum.Text.Trim() == "")
                {
                    Alert.Show("项目内部编号不能为空！");
                    return;
                }
                if (FirstFinishedPeople.Text.Trim() == "")
                {
                    Alert.Show("成果第一完成人不能为空！");
                    return;
                }
                if (ProjectPeople.Text.Trim() == "")
                {
                    Alert.Show("成员不能为空！");
                    return;
                }
                BLHelper.BLLAgency agency = new BLHelper.BLLAgency();
                ach.AchievementName = tAchievementName.Text.Trim();
                ach.AgencyID = agency.SelectAgencyID(DropDownListAgency.SelectedText);
                ach.ProjectName = tProjectID.Text.Trim();
                ach.AppraisalTime = dAppraisalTime.SelectedDate;
                ach.AppraisalUnit = tAppraisalUnit.Text.Trim();
                ach.ApRemarkRank = tApRemarkRank.Text.Trim();
                ach.ApprovalNum = tApprovalNum.Text.Trim();
                ach.ProjectInNum = ProjectInNum.Text.Trim();//项目分类编号
                ach.ProjectPeople = ProjectPeople.Text.Trim();//成员
                ach.FirstFinishedPeople = FirstFinishedPeople.Text.Trim();//成果第一完成人
                //ach.ProjectRank = DropDownListProjectRank.SelectedText;//鉴定级别
                ach.ProjectLevel = DropDownListProjectLevel.SelectedText;//鉴定水平
                ach.ProjectForm = DropDownListProjectForm.SelectedText;//鉴定形式
                ach.SecrecyLevel = Convert.ToInt32(dSecrecyLevel.SelectedIndex + 1);
                string username = user.FindByLoginName(Session["LoginName"].ToString()).UserName;
                ach.EntryPerson = username;

                if (Convert.ToInt32(Session["SecrecyLevel"]) == 5)
                {
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
                        ach.AttachmentID = attachid;
                    }
                    else
                    {
                        ach.AttachmentID = null;
                    }
                    int opinion = pm.UpLoadFile(OpinionPage).Attachid;
                    switch (opinion)
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
                    if (opinion != -3)
                    {
                        ach.OpinionPage = opinion;
                    }
                    else
                    {
                        ach.OpinionPage = null;
                    }
                    int member = pm.UpLoadFile(MemberPage).Attachid;
                    switch (member)
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
                    if (member != -3)
                    {
                        ach.MemberPage = member;
                    }
                    else
                    {
                        ach.MemberPage = null;
                    }
                    int seal = pm.UpLoadFile(SealPage).Attachid;
                    switch (seal)
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
                    if (seal != -3)
                    {
                        ach.SealPage = seal;
                    }
                    else
                    {
                        ach.SealPage = null;
                    }
                    ach.IsPass = true;
                    achieve.Insert(ach);
                    PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("保存成功！"));
                }
                else
                {
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
                        ach.AttachmentID = attachid;
                    }
                    else
                    {
                        ach.AttachmentID = null;
                    }
                   int opinion = pm.UpLoadFile(OpinionPage).Attachid;
                    switch (opinion)
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
                    if (opinion != -3)
                    {
                        ach.OpinionPage = opinion;
                    }
                    else
                    {
                        ach.OpinionPage = null;
                    }
                    int member = pm.UpLoadFile(MemberPage).Attachid;
                    switch (member)
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
                    if (member != -3)
                    {
                        ach.MemberPage = member;
                    }
                    else
                    {
                        ach.MemberPage = null;
                    }
                    int seal = pm.UpLoadFile(SealPage).Attachid;
                    switch (seal)
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
                    if (seal != -3)
                    {
                        ach.SealPage = seal;
                    }
                    else
                    {
                        ach.SealPage = null;
                    }
                    ach.IsPass = false;
                    achieve.Insert(ach);
                    log.LoginName = username;
                    log.OperationTime = DateTime.Now;
                    log.LoginIP = " ";
                    log.OperationContent = "Achievement";
                    log.OperationType = "添加";
                    log.OperationDataID = ach.AchievementID;
                    op.Insert(log);
                    PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("您的数据已提交，请等待确认！"));
                }
            }
            catch (Exception ex)
            {
                int attachid = Convert.ToInt32(ach.AttachmentID);
                string path = at.FindPath(attachid);
                pm.DeleteFile(attachid, path);
                pm.SaveError(ex, this.Request);
            }
        }

        //成果名称验证
        protected void tAchievementName_TextChanged(object sender, EventArgs e)
        {
            if (tAchievementName.Text.Trim() != "")
            {
                if (achieve.IsExitAchieveName(tAchievementName.Text.Trim()) != null)
                {
                    if (achieve.IsExitAchieveName(tAchievementName.Text.Trim()).IsPass == false)
                    {
                        Alert.Show("该成果名称正在审核中！");
                        tAchievementName.Text = "";
                        return;
                    }
                    else
                    {
                        Alert.Show("该成果名称已存在！");
                        tAchievementName.Text = "";
                        return;
                    }
                }
            }
        }
        //项目验证
        protected void tProjectID_TextChanged(object sender, EventArgs e)
        {
            if (tProjectID.Text.Trim() != "")
            {
                if (project.IsNullProject(tProjectID.Text.Trim()) == null)
                {
                    Alert.Show("该项目名称不存在！");
                    tProjectID.Text = "";
                    return;
                }
                else
                {
                    if (project.IsNullProject(tProjectID.Text.Trim()).IsPass == false)
                    {
                        Alert.Show("该项目名称正在审核中！");
                        tProjectID.Text = "";
                        return;
                    }
                }
            }
        }
        //重置
        protected void DeleteAll_Click(object sender, EventArgs e)
        {
            try
            {
                tAchievementName.Reset();
                DropDownListAgency.Reset();
                tProjectID.Reset();
                dAppraisalTime.Reset();
                tAppraisalUnit.Reset();
                tApRemarkRank.Reset();
                tApprovalNum.Reset();
                dSecrecyLevel.Reset();
                ProjectInNum.Reset(); //项目分类编号
                ProjectPeople.Reset();//成员
                FirstFinishedPeople.Reset();//成果第一完成人
                DropDownListProjectForm.Reset();//鉴定形式
                DropDownListProjectLevel.Reset();//鉴定水平
               // DropDownListProjectRank.Reset();//鉴定级别
                PageContext.RegisterStartupScript("clearFile();");
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //初始化鉴定级别下拉框
        public void InitDropDownListProjectRank()
        {
            try
            {
                List<BasicCode> list = bllBasicCode.FindALLName("鉴定级别");
                for (int i = 0; i < list.Count(); i++)
                {
                    //DropDownListProjectRank.Items.Add(list[i].CategoryContent.ToString(), list[i].CategoryContent.ToString());
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //初始化鉴定形式下拉框
        public void InitDropDownListProjectForm()
        {
            try
            {
                List<BasicCode> list = bllBasicCode.FindALLName("鉴定形式");
                for (int i = 0; i < list.Count(); i++)
                {
                    DropDownListProjectForm.Items.Add(list[i].CategoryContent.ToString(), list[i].CategoryContent.ToString());
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //初始化鉴定水平下拉框
        public void InitDropDownListProjectLevel()
        {
            try
            {
                List<BasicCode> list = bllBasicCode.FindALLName("鉴定水平");
                for (int i = 0; i < list.Count(); i++)
                {
                    DropDownListProjectLevel.Items.Add(list[i].CategoryContent.ToString(), list[i].CategoryContent.ToString());
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
    }
}

