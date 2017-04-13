/**编写人：王会会
 * 时间：2014年8月3号
 * 功能：添加项目基本信息的相关操作
 * 修改履历：
 **/
using Common.Entities;
using FineUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

namespace WDFramework.Projects
{
    public partial class ADD_Projects : System.Web.UI.Page
    {
        BLHelper.BLLProject bllProject = new BLHelper.BLLProject();
        BLHelper.BLLBasicCode bllBasicCode = new BLHelper.BLLBasicCode();
        BLHelper.BLLAgency bllAgency = new BLHelper.BLLAgency();
        BLHelper.BLLAttachment bllAttachment = new BLHelper.BLLAttachment();
        BLHelper.BLLOperationLog bllOperate = new BLHelper.BLLOperationLog();
        BLHelper.BLLAttachment attachment = new BLHelper.BLLAttachment();
        BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
        Attachment a = new Attachment();
        Common.Entities.Project aproject = new Common.Entities.Project();
        BLHelper.BLLFundingSet bllFundingSet = new BLHelper.BLLFundingSet();
        BLHelper.BLLUser bllUser = new BLHelper.BLLUser();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitDropListAgencyP();
                //InitDropListSubjectSort();
                InitDropListSecrecyLevel();
                InitDropDownListNature();
                InitDropDownListState();
                InitDropDownListProjectLevel();
                //DatePickerEndTime.MaxDate = DateTime.Now;
                //DatePickerExpectEndTime.MaxDate = DateTime.Now;
                //DatePickerStartTime.MaxDate = DateTime.Now;
                //初始化合作形式下拉框
                InitDropDownListCooperationForms();
                //初始化预期成果下拉框
                InitDropDownListExpecteResults();
                //初始化项目分类名称
                DropDownListProjectSorName();
               //DropDownListProjectSortName.Attributes.Add("onmouseover", "javascript:showToolTip(1);");
                //DropDownListProjectSortName.Attributes.Add("onmouseout", "javascript:showToolTip(0);");
                
            }
        }
        //项目添加
        public void AddProjects()
        {
            try
            {
                if (SourceUnit2.Text.Trim() == "")
                {
                    Alert.ShowInTop("来源单位不能为空！");
                    SourceUnit2.Text = "";
                    return;
                }
                if (ProjectManager.Text.Trim() == "")
                {
                    Alert.ShowInTop("项目负责人（前三）不能为空！");
                    ProjectManager.Text = "";
                    return;
                }
                if (ProjectHeads2.Text.Trim() == "")
                {
                    Alert.ShowInTop("实际负责人不能为空！");
                    ProjectHeads2.Text = "";
                    return;
                }
                if (AcceptUnit2.Text.Trim() == "")
                {
                    Alert.ShowInTop("承担部门不能为空！");
                    AcceptUnit2.Text = "";
                    return;
                }
                if (GivenMoneyUnits2.Text.Trim() == "")
                {
                    Alert.ShowInTop("来款单位不能为空！");
                    GivenMoneyUnits2.Text = "";
                    return;
                }
                if (ProjectInNum.Text.Trim() == "")
                {
                    Alert.ShowInTop("项目内部编号（科技处）不能为空！");
                    ProjectInNum.Text = "";
                    return;
                }
                if (Convert.ToInt32(Session["SecrecyLevel"]) != 5)
                    aproject.IsPass = false;
                else
                    aproject.IsPass = true;
                aproject.ProjectName = ProjectName2.Text.Trim();
                aproject.AgencyID = bllAgency.SelectAgencyID(DropDownListAgencyP.SelectedText);
                aproject.AcceptUnit = AcceptUnit2.Text.Trim();
                aproject.SourceUnit = SourceUnit2.Text.Trim();
                aproject.ProjectSortName = DropDownListProjectSortName.SelectedItem.Text;
                aproject.ProjectState = DropDownListState.SelectedItem.Text;
                aproject.ApprovedMoney = ApprovedMoney2.Text.Trim();
                if (!string.IsNullOrEmpty(GetMoney2.Text))
                {
                    if (Convert.ToDecimal(ApprovedMoney2.Text) - Convert.ToDecimal(GetMoney2.Text)<0)
                    {
                        GetMoney2.Reset();
                        Alert.ShowInTop("到账金额小于等于项目经费");
                        return;
                    }
                    else
                        aproject.GetMoney = GetMoney2.Text.Trim();
                }
                else
                    aproject.GetMoney = GetMoney2.Text.Trim();
                aproject.GetMoney = GetMoney2.Text.Trim();
                aproject.CooperationForms = DropDownListCooperationForms.SelectedItem.Text;//CooperationForms2.Text.Trim();
                aproject.ProjectLevel = DropDownListProjectLevel.SelectedItem.Text;
                aproject.ProjectHeads = ProjectHeads2.Text.Trim();
                aproject.StartTime = DatePickerStartTime.SelectedDate.Value;
                if (DatePickerEndTime.SelectedDate.HasValue)
                {
                    if (DatePickerEndTime.SelectedDate < DatePickerStartTime.SelectedDate)
                    {
                        DatePickerEndTime.Reset();
                        Alert.ShowInTop("结束时间不能小于开始时间！");
                        return;
                    }
                    else
                        aproject.EndTime = DatePickerEndTime.SelectedDate;
                }
                if (DatePickerExpectEndTime.SelectedDate.HasValue)
                {
                    if (DatePickerExpectEndTime.SelectedDate < DatePickerStartTime.SelectedDate)
                    {
                        DatePickerExpectEndTime.Reset();
                        Alert.ShowInTop("预期结束时间不能小于开始时间！");
                        return;
                    }
                    else
                        aproject.ExpectEndTime = DatePickerExpectEndTime.SelectedDate;
                }
                aproject.ExpecteResults = DropDownListExpecteResults.SelectedItem.Text;//ExpecteResults2.Text.Trim();
                aproject.GivenMoneyUnits = GivenMoneyUnits2.Text.Trim();
                aproject.ProjectNature = DropDownListNature.SelectedItem.Text;
                aproject.Remark = Remark2.Text.Trim();
                aproject.SecrecyLevel = DropDownListSecrecyLevel.SelectedIndex + 1;
                
                //管理费比例
                if (!string.IsNullOrEmpty(ManageMoney.Text))
                {
                    double num = 0.0;
                    if (double.TryParse(ManageMoney.Text.Trim(), out num))
                    {
                        if (Convert.ToDouble(ManageMoney.Text) > 0 && Convert.ToDouble(ManageMoney.Text) < 100)
                            aproject.ManageMoney = ManageMoney.Text.Trim();
                        else
                        {
                            ManageMoney.Text = "";
                            Alert.ShowInTop("管理费比例为0~100%！");
                            return;
                        }
                    }
                    else
                    {
                        ManageMoney.Text = "";
                        Alert.ShowInTop("请输入数字！");
                        return;
                    }                   
                }
                else
                {
                    aproject.ManageMoney = bllFundingSet.FindProportion(aproject.ProjectNature, "管理费");
                }
                aproject.PactNum = PactNum2.Text.Trim();
                aproject.TaskNum = TaskNum2.Text.Trim();
                aproject.ProjectManager = ProjectManager.Text.Trim();//项目负责人(前三)
                aproject.ProjectInNum = ProjectInNum.Text.Trim();//项目内部编号(科技处)
                aproject.ProjectMember = ProjectMember.Text.Trim();//项目成员
                //经济效益附件
                int attachidbenefit = pm.UpLoadFile(FileUploadFile).Attachid;
                switch (attachidbenefit)
                {
                    case -1:
                        Alert.ShowInTop("经济效益文件类型不符，请重新选择！");
                        return;
                    case 0:
                        Alert.ShowInTop("经济效益文件名已经存在！");
                        return;
                    case -2:
                        Alert.ShowInTop("经济效益文件不能大于150M");
                        return;
                }
                //经济预算效益
                int attachidbudget = pm.UpLoadFile(FileUploadFileM).Attachid;
                switch (attachidbudget)
                {
                    case -1:
                        Alert.ShowInTop("经费预算文件类型不符，请重新选择！");
                        if (attachidbenefit != -1 && attachidbenefit != 0 && attachidbenefit != -2)
                            pm.DeleteFile(attachidbenefit, bllAttachment.FindPath(attachidbenefit));
                        return;
                    case 0:
                        Alert.ShowInTop("经费预算文件名已经存在！");
                        if (attachidbenefit != -1 && attachidbenefit != 0 && attachidbenefit != -2)
                            pm.DeleteFile(attachidbenefit, bllAttachment.FindPath(attachidbenefit));
                        return;
                    case -2:
                        Alert.ShowInTop("经费预算文件不能大于150M");
                        if (attachidbenefit != -1 && attachidbenefit != 0 && attachidbenefit != -2)
                            pm.DeleteFile(attachidbenefit, bllAttachment.FindPath(attachidbenefit));
                        return;
                }
                //经济效益
                if (attachidbenefit != -3)
                {
                    aproject.BenefitAttachment = attachidbenefit;
                }
                else
                {
                    aproject.BenefitAttachment = null;
                }
                //经费预算
                if (attachidbudget != -3)
                {
                    aproject.BudgetAttachment = attachidbudget;
                }
                else
                {
                    aproject.BudgetAttachment = null;
                }
                aproject.EntryPerson = bllUser.FindByLoginName(Session["LoginName"].ToString()).UserName;
                bllProject.InsertProject(aproject);
                if (Convert.ToInt32(Session["SecrecyLevel"]) != 5)
                {
                    //向操作日志表中插入
                    OperationLog operate = new OperationLog();
                    operate.LoginName = bllUser.FindByLoginName(Session["LoginName"].ToString()).UserName;
                    operate.LoginIP = "";
                    operate.OperationType = "添加";
                    operate.OperationContent = "Project";
                    operate.OperationDataID = bllProject.SelectProjectID(ProjectName2.Text);
                    operate.OperationTime = System.DateTime.Now;
                    operate.Remark = "";
                    bllOperate.Insert(operate);
                    PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("您的操作已提交审核，请等待！"));
                }
                else
                    PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("项目信息已添加完成！"));
            }
            catch (Exception ex)
            {
                pm.DeleteFile(Convert.ToInt32(aproject.BenefitAttachment), bllAttachment.FindPath(Convert.ToInt32(aproject.BenefitAttachment)));
                pm.DeleteFile(Convert.ToInt32(aproject.BudgetAttachment), bllAttachment.FindPath(Convert.ToInt32(aproject.BudgetAttachment)));
                pm.SaveError(ex, this.Request);
            }
        }
        //保存
        protected void Save_Click(object sender, EventArgs e)
        {
            if (ProjectName2.Text.Trim()!="")
            {
                if (bllProject.IsNullProject(ProjectName2.Text.Trim().ToString()) == null)
                {
                    if (Convert.ToInt32(Session["SecrecyLevel"]) == 5)
                    {
                        AddProjects();
                    }
                    else
                    {
                        AddProjects();
                    }
                }

                else
                {
                    if (bllProject.IsNullProject(ProjectName2.Text.Trim().ToString()).IsPass == false)
                    {
                        Alert.ShowInTop("该项目名称正在审核中，请等待！");
                        ProjectName2.Text = "";
                    }
                    else
                    {
                        Alert.ShowInTop("项目名称已存在！");
                        ProjectName2.Text = "";
                    }
                }
            }
            else
            {
                Alert.ShowInTop("项目名称不能为空！");
                ProjectName2.Text = "";
                return;
            }
        }

        //初始化项目所属机构下拉框
        public void InitDropListAgencyP()
        {
            try
            {
                BLHelper.BLLAgency agency = new BLHelper.BLLAgency();
                List<Common.Entities.Agency> list = agency.FindAllAgencyName();
                for (int i = 0; i < list.Count(); i++)
                {
                    DropDownListAgencyP.Items.Add(list[i].AgencyName.ToString(), list[i].AgencyName.ToString());
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        ////初始化项目分类名称下拉框
        //public void InitDropListProjectSort()
        //{
        //    try
        //    {
        //        List<BasicCode> list = bllBasicCode.FindALLName("项目分类名称");
        //        for (int i = 0; i < list.Count(); i++)
        //        {
        //            DropDownListProjectSort.Items.Add(list[i].CategoryContent.ToString(), list[i].CategoryContent.ToString());
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        pm.SaveError(ex, this.Request);
        //    }
        //}      
        //初始化项目性质下拉框
        public void InitDropDownListNature()
        {
            try
            {
                List<BasicCode> list = bllBasicCode.FindALLName("项目性质");
                for (int i = 0; i < list.Count(); i++)
                {
                    DropDownListNature.Items.Add(list[i].CategoryContent.ToString(), list[i].CategoryContent.ToString());
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //初始化项目状态下拉框
        public void InitDropDownListState()
        {
            try
            {
                List<BasicCode> list = bllBasicCode.FindALLName("项目状态");
                for (int i = 0; i < list.Count(); i++)
                {
                    DropDownListState.Items.Add(list[i].CategoryContent.ToString(), list[i].CategoryContent.ToString());
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //初始化项目级别下拉框
        public void InitDropDownListProjectLevel()
        {
            try
            {
                List<BasicCode> list = bllBasicCode.FindALLName("级别");
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
        //初始化项目涉密等级和附件涉密等级
        public void InitDropListSecrecyLevel()
        {
            try
            {
                string[] SecrecyLevels = new string[] { "四级", "三级", "二级", "一级", "管理员" };
                //string[] SecrecyLevels = new string[] { "公开", "内部", "秘密", "机密", "管理员" };
                for (int i = 0; i < Convert.ToInt32(Session["SecrecyLevel"]); i++)
                {
                    DropDownListSecrecyLevel.Items.Add(SecrecyLevels[i].ToString(), SecrecyLevels[i].ToString());
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //重置
        protected void Reset_Click(object sender, EventArgs e)
        {
            try
            {
                ProjectName2.Reset();
                ProjectName2.Reset();
                DropDownListAgencyP.Reset();
                AcceptUnit2.Reset();
                SourceUnit2.Reset();
                DropDownListProjectSort.Reset();
                DropDownListProjectSortName.Reset();
                DropDownListState.Reset();
                ApprovedMoney2.Reset();
                GetMoney2.Reset();
                DropDownListCooperationForms.Reset();//CooperationForms2.Reset();
                DropDownListProjectLevel.Reset();
                ProjectHeads2.Reset();
                DatePickerStartTime.Reset();
                DatePickerEndTime.Reset();
                DatePickerExpectEndTime.Reset();
                DropDownListExpecteResults.Reset();// ExpecteResults2.Reset();
                GivenMoneyUnits2.Reset();
                DropDownListNature.Reset();
                Remark2.Reset();
                DropDownListSecrecyLevel.Reset();
                ManageMoney.Reset();
                PactNum2.Reset();
                TaskNum2.Reset();
                ProjectManager.Reset();//项目负责人（前三）
                PageContext.RegisterStartupScript("clearFile();");
                PageContext.RegisterStartupScript("clearFiles();");
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //初始化合作形式下拉框
        public void InitDropDownListCooperationForms()
        {
            try
            {
                List<BasicCode> list = bllBasicCode.FindALLName("合作形式");
                for (int i = 0; i < list.Count(); i++)
                {
                    DropDownListCooperationForms.Items.Add(list[i].CategoryContent.ToString(), list[i].CategoryContent.ToString());
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //初始化预期成果下拉框
        public void InitDropDownListExpecteResults()
        {
            try
            {
                List<BasicCode> list = bllBasicCode.FindALLName("预期成果");
                for (int i = 0; i < list.Count(); i++)
                {
                    DropDownListExpecteResults.Items.Add(list[i].CategoryContent.ToString(), list[i].CategoryContent.ToString());
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //初始化项目分类名称
        public void DropDownListProjectSorName()
        {
            DropDownListProjectSort.SelectedItem.Text = "一类";
            List<BasicCode> list1 = bllBasicCode.FindALLName("项目等级（一类）");
            for (int i = 0; i < list1.Count(); i++)
            {
                DropDownListProjectSortName.Items.Add(list1[i].CategoryContent.ToString(), list1[i].CategoryContent.ToString());
            }
            DropDownListProjectSortName.SelectedIndex = 0;
        }
        //项目分类名称下拉框变化
        protected void DropDownListProjectSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                switch (DropDownListProjectSort.SelectedItem.Text)
                {
                    case "一类":
                        DropDownListProjectSortName.Items.Clear();
                        List<BasicCode> list1 = bllBasicCode.FindALLName("项目等级（一类）");
                        for (int i = 0; i < list1.Count(); i++)
                        {
                            DropDownListProjectSortName.Items.Add(list1[i].CategoryContent.ToString(), list1[i].CategoryContent.ToString());
                        }
                        DropDownListProjectSortName.SelectedIndex = 0;
                        break;
                    case "二类":
                        DropDownListProjectSortName.Items.Clear();
                        List<BasicCode> list2 = bllBasicCode.FindALLName("项目等级（二类）");
                        for (int i = 0; i < list2.Count(); i++)
                        {
                            DropDownListProjectSortName.Items.Add(list2[i].CategoryContent.ToString(), list2[i].CategoryContent.ToString());
                        }
                        DropDownListProjectSortName.SelectedIndex = 0;
                        break;
                    case "三类":
                        DropDownListProjectSortName.Items.Clear();
                        List<BasicCode> list3 = bllBasicCode.FindALLName("项目等级（三类）");
                        for (int i = 0; i < list3.Count(); i++)
                        {
                            DropDownListProjectSortName.Items.Add(list3[i].CategoryContent.ToString(), list3[i].CategoryContent.ToString());
                        }
                        DropDownListProjectSortName.SelectedIndex = 0;
                        break;
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }

      
    }
}