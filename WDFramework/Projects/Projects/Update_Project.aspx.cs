/**编写人：王会会
 * 时间：2014年8月3号
 * 功能：修改项目基本信息的相关操作
 * 修改履历：    修改人：吕博扬
 *              修改时间：2015年9月24日
 *              修改内容：取消所有输入项的数据校验，读取project.ProjectSortName为空的检测
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
using System.Windows.Forms;
using System.Text;
namespace WDFramework.Projects

{
    public partial class UpdateProject : System.Web.UI.Page
    {

        BLHelper.BLLProject bllProject = new BLHelper.BLLProject();
        BLHelper.BLLAgency bllAgency = new BLHelper.BLLAgency();
        BLHelper.BLLAttachment bllAttachment = new BLHelper.BLLAttachment();
        BLHelper.BLLOperationLog bllOperate = new BLHelper.BLLOperationLog();
        BLHelper.BLLFiles file = new BLHelper.BLLFiles();
        BLHelper.BLLAttachment attachment = new BLHelper.BLLAttachment();
        BLCommon.PublicMethod publicmethod = new BLCommon.PublicMethod();
        Attachment a = new Attachment();
        Common.Entities.Project aproject = new Common.Entities.Project();
        BLHelper.BLLBasicCode bllBasicCode = new BLHelper.BLLBasicCode();
        BLHelper.BLLFundingSet bllFundingSet = new BLHelper.BLLFundingSet();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitDropListAgencyP();
                InitDropListProjectSort();
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
                BindData();
            }
        }
        
        public void BindData()
        {
            try
            {
                List<Common.Entities.Project> list = bllProject.FindProject(Convert.ToInt32(Session["ProjectID"]), Convert.ToInt32(Session["SecrecyLevel"]));
                Common.Entities.Project project = list.FirstOrDefault();
                ProjectName2.Text = project.ProjectName;
                DropDownListAgencyP.SelectedValue = bllAgency.FindAgenName(project.AgencyID);
                AcceptUnit2.Text = project.AcceptUnit;
                SourceUnit2.Text = project.SourceUnit;
                DropDownListProjectSortName.SelectedValue = project.ProjectSortName;
                if (project.ProjectSortName.Length > 2)
                {
                    switch (project.ProjectSortName.Substring(0, 2))
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
                    DropDownListProjectSort.SelectedValue = project.ProjectSortName.Substring(0, 2);
                }
                else
                {
                    DropDownListProjectSort.SelectedValue = "";
                }
                DropDownListState.SelectedValue = project.ProjectState;
                ApprovedMoney2.Text = project.ApprovedMoney;
                GetMoney2.Text = project.GetMoney;
                DropDownListCooperationForms.SelectedValue = project.CooperationForms;// CooperationForms2.Text = project.CooperationForms;
                DropDownListProjectLevel.SelectedIndex = Convert.ToInt32(project.SecrecyLevel) - 1;
                ProjectHeads2.Text = project.ProjectHeads;
                DatePickerStartTime.SelectedDate = project.StartTime;
                DatePickerEndTime.SelectedDate = project.EndTime;
                DatePickerExpectEndTime.SelectedDate = project.ExpectEndTime;
                DropDownListExpecteResults.SelectedValue = project.ExpecteResults;//ExpecteResults2.Text=project.ExpecteResults;
                GivenMoneyUnits2.Text = project.GivenMoneyUnits;
                DropDownListNature.SelectedValue = project.ProjectNature;
                Remark2.Text = project.Remark;
                ManageMoney.Text = project.ManageMoney.ToString();
                PactNum2.Text = project.PactNum;
                TaskNum2.Text = project.TaskNum;
                DropDownListSecrecyLevel.SelectedIndex = Convert.ToInt32(project.SecrecyLevel - 1);
                ProjectManager.Text = project.ProjectManager;//项目负责人（前三）
                ProjectInNum.Text = project.ProjectInNum;//项目内部编号（科技处）
                ProjectMember.Text = project.ProjectMember; //项目成员
            }
            catch (Exception ex)
            {
                publicmethod.DeleteFile(Convert.ToInt32(aproject.BenefitAttachment), bllAttachment.FindPath(Convert.ToInt32(aproject.BenefitAttachment)));
                publicmethod.DeleteFile(Convert.ToInt32(aproject.BudgetAttachment), bllAttachment.FindPath(Convert.ToInt32(aproject.BudgetAttachment)));
                publicmethod.SaveError(ex, this.Request);
            }
        }
        //项目修改
        public void AddProjects()
        {
            try
            {
                if (SourceUnit2.Text.Trim() == "")
                {
                    //Alert.ShowInTop("来源单位不能为空！");
                    SourceUnit2.Text = "";
                    //return;
                }
                if (ProjectManager.Text.Trim() == "")
                {
                    //Alert.ShowInTop("项目负责人（前三）不能为空！");
                    ProjectManager.Text = "";
                    //return;
                }
                if (ProjectHeads2.Text.Trim() == "")
                {
                    //Alert.ShowInTop("实际负责人不能为空！");
                    ProjectHeads2.Text = "";
                    //return;
                }
                if (AcceptUnit2.Text.Trim() == "")
                {
                    //Alert.ShowInTop("承担部门不能为空！");
                    AcceptUnit2.Text = "";
                    //return;
                }
                if (GivenMoneyUnits2.Text.Trim() == "")
                {
                    //Alert.ShowInTop("来款单位不能为空！");
                    GivenMoneyUnits2.Text = "";
                    //return;
                }
                aproject.ProjectName = ProjectName2.Text.Trim();
                aproject.AgencyID = bllAgency.SelectAgencyID(DropDownListAgencyP.SelectedItem.Text);
                aproject.AcceptUnit = AcceptUnit2.Text.Trim();
                aproject.SourceUnit = SourceUnit2.Text.Trim();
                if (DropDownListProjectSortName.SelectedItem != null)
                    aproject.ProjectSortName = DropDownListProjectSortName.SelectedItem.Text;
                else
                    aproject.ProjectSortName = "";
                aproject.ProjectState = DropDownListState.SelectedItem.Text;
                aproject.ApprovedMoney = ApprovedMoney2.Text.Trim();
                aproject.ProjectMember = ProjectMember.Text; //项目成员
                //if (!string.IsNullOrEmpty(GetMoney2.Text))
                //{
                //    if (Convert.ToDecimal(ApprovedMoney2.Text) - Convert.ToDecimal(GetMoney2.Text) < 0)
                //    {
                //        GetMoney2.Reset();
                //        Alert.ShowInTop("到账金额小于等于项目经费");
                //        return;
                //    }
                //    else
                //        aproject.GetMoney = GetMoney2.Text.Trim();
                //}
                //else
                    aproject.GetMoney = GetMoney2.Text.Trim();
                aproject.CooperationForms = DropDownListCooperationForms.SelectedItem.Text;//CooperationForms2.Text.Trim();
                aproject.ProjectLevel = DropDownListProjectLevel.SelectedItem.Text;
                aproject.ProjectHeads = ProjectHeads2.Text.Trim();
                aproject.StartTime = DatePickerStartTime.SelectedDate;
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
                        ManageMoney.Reset();
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
                aproject.ProjectManager = ProjectManager.Text.Trim();//项目负责人（前三）
                aproject.ProjectInNum = ProjectInNum.Text.Trim();//项目内部编号（科技处）
                aproject.EntryPerson = bllProject.FindByid(Convert.ToInt32(Session["ProjectID"])).EntryPerson;
                //原经济效益附件
                int BenefitID = bllProject.FindBenefit(Convert.ToInt32(Session["ProjectID"]));
                string path = bllAttachment.FindPath(BenefitID);
                //原经费预算附件
                int BudgetID = bllProject.FindBudget(Convert.ToInt32(Session["ProjectID"]));
                string budgetpath = bllAttachment.FindPath(BudgetID);

                if (Convert.ToInt32(Session["SecrecyLevel"]) == 5)//如果等于5级
                {
                    aproject.IsPass = true;
                    aproject.ProjectID = Convert.ToInt32(Session["ProjectID"]);
                    int Attachment = publicmethod.UpLoadFile(FileUploadFile).Attachid;//经济效益附件                   
                    switch (Attachment)
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
                    int budgetAttanchment = publicmethod.UpLoadFile(FileUploadFileM).Attachid;//经费预算附件
                    switch (budgetAttanchment)
                    {
                        case -1:
                            Alert.ShowInTop("经费预算文件类型不符，请重新选择！");
                            if (Attachment != -1 && Attachment != 0 && Attachment != -2)
                                publicmethod.DeleteFile(Attachment, bllAttachment.FindPath(Attachment));
                            return;
                        case 0:
                            Alert.ShowInTop("经费预算文件名已经存在！");
                            if (Attachment != -1 && Attachment != 0 && Attachment != -2)
                                publicmethod.DeleteFile(Attachment, bllAttachment.FindPath(Attachment));
                            return;
                        case -2:
                            Alert.ShowInTop("经费预算文件不能大于150M");
                            if (Attachment != -1 && Attachment != 0 && Attachment != -2)
                                publicmethod.DeleteFile(Attachment, bllAttachment.FindPath(Attachment));
                            return;
                    }
                    if (Attachment != -3)//上传控件是否有值
                    {
                        aproject.BenefitAttachment = Attachment;//附件为新插入的经济效益附件ID
                        publicmethod.DeleteFile(BenefitID, path);
                    }
                    else //上传空间没有值
                    {
                        if (BenefitID != 0)//原来有附件
                        {
                            aproject.BenefitAttachment = Attachment;
                        }
                    }
                    if (budgetAttanchment != -3)//上传控件是否有值
                    {
                        aproject.BudgetAttachment = budgetAttanchment;//附件为新插入的经费预算附件
                        publicmethod.DeleteFile(BudgetID, budgetpath);
                    }
                    else//上传空间没有值
                    {
                        if (BudgetID != 0)
                        {
                            aproject.BudgetAttachment = budgetAttanchment;
                        }
                    }
                    bllProject.Update(aproject);//更新              
                    PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("保存成功！"));
                }
                else//小于5级
                {
                    aproject.IsPass = false;
                    int Attachment = publicmethod.UpLoadFile(FileUploadFile).Attachid;//经济效益附件                   
                    switch (Attachment)
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
                    int budgetAttanchment = publicmethod.UpLoadFile(FileUploadFileM).Attachid;//经费预算附件
                    switch (budgetAttanchment)
                    {
                        case -1:
                            Alert.ShowInTop("经费预算文件类型不符，请重新选择！");
                            if (Attachment != -1 && Attachment != 0 && Attachment != -2)
                                publicmethod.DeleteFile(Attachment, bllAttachment.FindPath(Attachment));
                            return;
                        case 0:
                            Alert.ShowInTop("经费预算文件名已经存在！");
                            if (Attachment != -1 && Attachment != 0 && Attachment != -2)
                                publicmethod.DeleteFile(Attachment, bllAttachment.FindPath(Attachment));
                            return;
                        case -2:
                            Alert.ShowInTop("经费预算文件不能大于150M");
                            if (Attachment != -1 && Attachment != 0 && Attachment != -2)
                                publicmethod.DeleteFile(Attachment, bllAttachment.FindPath(Attachment));
                            return;
                    }
                    if (Attachment != -3)//有值
                    {
                        aproject.BenefitAttachment = Attachment;//附件为新插入的经济效益附件ID
                    }
                    else//上传控件没有值
                    {
                        if (BenefitID != 0)//原来有附件
                        {
                            aproject.BenefitAttachment = Attachment;
                        }
                    }
                    if (budgetAttanchment != -3)//有值
                    {
                        aproject.BudgetAttachment = budgetAttanchment;///附件为新插入的经费预算附件
                    }
                    else
                    {
                        if (BudgetID != 0)//原来有附件
                        {
                            aproject.BudgetAttachment = budgetAttanchment;
                        }
                    }
                    //向操作日志表中插入
                    bllProject.InsertProject(aproject);//插入       
                    OperationLog operate = new OperationLog();
                    operate.LoginName = bllProject.FindByid(Convert.ToInt32(Session["ProjectID"])).EntryPerson;
                    operate.LoginIP = "";
                    operate.OperationType = "更新";
                    operate.OperationContent = "Project";
                    operate.OperationDataID = Convert.ToInt32(Session["ProjectID"]);
                    operate.OperationTime = System.DateTime.Now;
                    operate.Remark = bllProject.SelectProjectID(ProjectName2.Text).ToString();
                    bllOperate.Insert(operate);
                    PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("你的数据已提交，请等待确认！"));
                }              
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }       
        //保存
        protected void Save_Click(object sender, EventArgs e)
        {
            //if (ProjectName2.Text.Trim() != "")
            //{
                //更新数据ProjectName与原数据相同
                if (ProjectName2.Text.Trim() == bllProject.FindProject(Convert.ToInt32(Session["ProjectID"]), Convert.ToInt32(Session["SecrecyLevel"])).FirstOrDefault().ProjectName)
                {
                    if (Convert.ToInt32(Session["SecrecyLevel"]) == 5)
                    {
                        AddProjects();
                    }
                    else
                    {
                        bllProject.ChangePass(Convert.ToInt32(Session["ProjectID"]), false);
                        AddProjects();
                    }
                }
                else
                {
                    if (bllProject.IsNullProject(ProjectName2.Text.Trim()) == null)
                    {
                        if (Convert.ToInt32(Session["SecrecyLevel"]) == 5)
                        {
                            AddProjects();
                        }
                        else
                        {
                            bllProject.ChangePass(Convert.ToInt32(Session["ProjectID"]), false);
                            AddProjects();
                        }
                    }
                    else
                    {
                        if (bllProject.IsNullProject(ProjectName2.Text.Trim()).IsPass == true)
                        {
                            Alert.ShowInTop("项目名称已存在！");
                            ProjectName2.Text = "";
                        }
                        if (bllProject.IsNullProject(ProjectName2.Text.Trim()).IsPass == false)
                        {
                            Alert.ShowInTop("项目名称已提交审核，请等待！");
                            ProjectName2.Text = "";
                        }
                    }
                }
            //}
            //else
            //{
            //    Alert.ShowInTop("项目名称不能为空！");
            //    ProjectName2.Text = "";
            //    return;
            //}
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
                publicmethod.SaveError(ex, this.Request);
            }
        }
        //初始化项目分类名称下拉框
        public void InitDropListProjectSort()
        {
            try
            {
                List<BasicCode> list = bllBasicCode.FindALLName("项目分类名称");
                for (int i = 0; i < list.Count(); i++)
                {
                    DropDownListProjectSort.Items.Add(list[i].CategoryContent.ToString(), list[i].CategoryContent.ToString());
                }
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
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
                publicmethod.SaveError(ex, this.Request);
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
                publicmethod.SaveError(ex, this.Request);
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
                publicmethod.SaveError(ex, this.Request);
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
                publicmethod.SaveError(ex, this.Request);
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
                DropDownListExpecteResults.Reset();//CooperationForms2.Reset();
                DropDownListProjectLevel.Reset();
                ProjectHeads2.Reset();
                DatePickerStartTime.Reset();
                DatePickerEndTime.Reset();
                DatePickerExpectEndTime.Reset();
                DropDownListExpecteResults.Reset();//ExpecteResults2.Reset();
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
                publicmethod.SaveError(ex, this.Request);
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
                publicmethod.SaveError(ex, this.Request);
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
                publicmethod.SaveError(ex, this.Request);
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
                publicmethod.SaveError(ex, this.Request);
            }
        }
    }
}