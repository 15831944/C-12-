/**编写人：王会会
 * 时间：2014年8月9号
 * 功能：修改项目重大节点基本信息的相关操作
 * 修改履历： 1、修改人：吕博扬
 *              修改时间：2015年9月23日
 *              修改内容：取消所有输入项的数据校验
 *           2、修改人：吕博杨
 *              修改时间：2015年12月2日
 *              修改内容：负责研究室字段改为机构并从机构表中获取数据
 **/
using Common.Entities;
using FineUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WDFramework.Projects
{
    public partial class Update_ProjectImportant : System.Web.UI.Page
    {
        BLHelper.BLLProject bllProject = new BLHelper.BLLProject();
       BLHelper.BLLProjectImportantNode bllImportant = new BLHelper.BLLProjectImportantNode();
        BLHelper.BLLOperationLog bllOperate = new BLHelper.BLLOperationLog();
        BLCommon.PublicMethod publicmethod = new BLCommon.PublicMethod();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitSecrecyLevel();
                InitDropListProjectName();
                //lby ↓
                InitDropListAgency();

                BindData();
                DatePickerEndTime.MaxDate = DateTime.Now;
                DatePickerStartTime.MaxDate = DateTime.Now;
               // DatePickerTime.MaxDate = DateTime.Now;
            }
        }
        public void BindData()
        {
            try
            {
                ProjectImportantNode Important = bllImportant.FindProjectImportant(Convert.ToInt32(Session["ProjectImportantID"]), true);
                MissionName.Text = Important.MissionName;
                //DatePickerTime.SelectedDate = Important.Time.Value;
                DatePickerStartTime.SelectedDate = Important.StartTime.Value;
                DatePickerEndTime.SelectedDate = Important.EndTime.Value;
                txtPersonCharge.Text = Important.PersonCharge;
                txtActualComleption.Text = Important.ActualComleption;
                txtProjectCompletion.Text = Important.ProjectCompletion;
                CompleteSpecificPerson.Text = Important.CompleteSpecificPerson;
                DropDownListProjectID.SelectedValue = bllProject.SelectProjectName(Convert.ToInt32(Important.ProjectID));
                DropDownListSecrecyLevel.SelectedIndex = Convert.ToInt32(Important.SecrecyLevel - 1);
                Remark.Text = Important.Remark;
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request); ;
            }
        }
        //保存
        protected void Save_Click(object sender, EventArgs e)
        {
            try
            {
                //if (MissionName.Text.Trim() == "")
                //{
                //    Alert.ShowInTop("节点名称不能为空！");
                //    MissionName.Text = "";
                //    return;
                //}
                ProjectImportantNode Important = new ProjectImportantNode();
                Important.MissionName = MissionName.Text.Trim();
                Important.ProjectID = bllProject.SelectProjectID(DropDownListProjectID.SelectedText.ToString());
                //Important.Time = DatePickerTime.SelectedDate;
                Important.StartTime = DatePickerStartTime.SelectedDate;
                Important.EndTime = DatePickerEndTime.SelectedDate;
                Important.CompleteSpecificPerson = CompleteSpecificPerson.Text.Trim();
                //lby ↓
                Important.ResearchCharge = Agency.SelectedText;

                Important.PersonCharge = txtPersonCharge.Text.Trim();
                Important.ActualComleption = txtActualComleption.Text.Trim();
                Important.ProjectCompletion = txtProjectCompletion.Text.Trim();
                Important.Remark = Remark.Text.Trim();
                Important.SecrecyLevel = DropDownListSecrecyLevel.SelectedIndex + 1;
                Important.EntryPerson = bllImportant.FindProjectImportant(Convert.ToInt32(Session["ProjectImportantID"]), true).EntryPerson;
                if (Convert.ToInt32(Session["SecrecyLevel"]) == 5)
                {
                    Important.ProjectImportantNodeID = Convert.ToInt32(Session["ProjectImportantID"]);
                    Important.IsPass = true;
                    bllImportant.Update(Important);    //插入项目重大节点表  
                    //bllImportant.ChangePass(Convert.ToInt32(Session["ProjectImportantID"]), false);
                    PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("项目重大节点修改完成！"));
                }
                else
                {
                    bllImportant.ChangePass(Convert.ToInt32(Session["ProjectImportantID"]), false);
                    Important.IsPass = false;
                    bllImportant.insert(Important);//插入项目重大节点表
                    OperationLog operate = new OperationLog();
                    operate.LoginName = Important.EntryPerson;
                    operate.OperationType = "更新";
                    operate.OperationContent = "ProjectImportantNode";
                    operate.OperationDataID = Convert.ToInt32(Session["ProjectImportantID"]);
                    operate.LoginIP = "";
                    operate.OperationTime = DateTime.Now;
                    operate.Remark = Important.ProjectImportantNodeID.ToString();
                    bllOperate.Insert(operate);//插入操作日志表
                    PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("您的操作已提交审核，请等待！"));
                }
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
        //初始化项目涉密等级等级
        public void InitSecrecyLevel()
        {
            try
            {
                string[] SecrecyLevels = new string[] { "四级", "三级", "二级", "一级", "管理员" };
                for (int i = 0; i < Convert.ToInt32(Session["SecrecyLevel"]); i++)
                {
                    DropDownListSecrecyLevel.Items.Add(SecrecyLevels[i], i.ToString());
                }
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
        //初始化项目名称下拉框
        public void InitDropListProjectName()
        {
            try
            {
                List<Common.Entities.Project> list = bllProject.FindALLName();
                for (int i = 0; i < list.Count(); i++)
                {
                    DropDownListProjectID.Items.Add(list[i].ProjectName.ToString(), list[i].ProjectName.ToString());
                }
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
        //lby 初始化机构下拉框
        public void InitDropListAgency()
        {
            try
            {
                List<Common.Entities.Agency> list = new BLHelper.BLLAgency().FindAll(Convert.ToInt32(Session["SecrecyLevel"]));
                for (int i = 0; i < list.Count(); ++i)
                    Agency.Items.Add(list[i].AgencyName, list[i].AgencyName);
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
                MissionName.Reset();
                DropDownListProjectID.Reset();
                DatePickerStartTime.Reset();
                DatePickerEndTime.Reset();
                Remark.Reset();
                DropDownListSecrecyLevel.Reset();
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }    
    }
}