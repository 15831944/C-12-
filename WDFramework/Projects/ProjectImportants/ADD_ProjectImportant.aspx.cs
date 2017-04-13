/**编写人：王会会
 * 时间：2014年8月9号
 * 功能：添加项目重大节点信息的相关操作
 * 修改履历：    1、修改人：吕博杨
 *                 修改时间：2015年12月2日
 *                 修改内容：负责研究室字段改为机构并从机构表中获取数据
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
    public partial class ADD_ProjectImportant : System.Web.UI.Page
    {
        BLHelper.BLLProject bllProject = new BLHelper.BLLProject();
        BLHelper.BLLProjectImportantNode bllImportant = new BLHelper.BLLProjectImportantNode(); 
        ProjectImportantNode Important = new ProjectImportantNode();
        BLHelper.BLLOperationLog bllOperate = new BLHelper.BLLOperationLog();
        BLHelper.BLLUser bllUser = new BLHelper.BLLUser();
        BLCommon.PublicMethod publicmethod = new BLCommon.PublicMethod();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitSecrecyLevel();
                InitDropListProjectName();
                //lby ↓
                InitDropListAgency();

                DatePickerEndTime.MaxDate = DateTime.Now;
                DatePickerStartTime.MaxDate = DateTime.Now;
               // DatePickerTime.MaxDate = DateTime.Now;
            }
        }
        //添加按钮
        protected void Save_Click(object sender, EventArgs e)
        {
            try
            {
                if (MissionName.Text.Trim() == "")
                {
                    Alert.ShowInTop("节点名称不能为空！");
                    MissionName.Text = "";
                    return;
                }
                //一个项目里节点名称是可以重复的，不需要判断
                Important.MissionName = MissionName.Text.Trim();
                Important.ProjectID = bllProject.SelectProjectID(DropDownListProjectID.SelectedText);
                // Important.Time = DatePickerTime.SelectedDate;
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
                Important.EntryPerson = bllUser.FindByLoginName(Session["LoginName"].ToString()).UserName;
                if (Convert.ToInt32(Session["SecrecyLevel"]) != 5)
                {
                    Important.IsPass = false;
                    bllImportant.insert(Important);
                    OperationLog operate = new OperationLog();
                    operate.LoginName = bllUser.FindByLoginName(Session["LoginName"].ToString()).UserName;
                    operate.OperationType = "添加";
                    operate.OperationContent = "ProjectImportantNode";
                    operate.OperationDataID = bllImportant.FindImportantID(Important.MissionName.ToString(), Convert.ToDateTime(Important.StartTime.ToString()), Session["LoginName"].ToString());
                    operate.LoginIP = "";
                    operate.OperationTime = DateTime.Now;
                    bllOperate.Insert(operate);
                    PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("您的操作已提交审核，请等待！"));
                }
                else
                {
                    Important.IsPass = true;
                    bllImportant.insert(Important);
                    PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("项目重大节点添加完成！"));
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