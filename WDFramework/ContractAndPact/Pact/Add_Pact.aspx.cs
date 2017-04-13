/**编写人：李金秋
 * 时间：2014年6月20号
 * 功能：新增合同档案界面
 * 修改履历：
 **/
using FineUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1.ContractAndPact.Pact
{
    public partial class Add_Pact : System.Web.UI.Page
    {
        BLHelper.BLLProject BLLProject = new BLHelper.BLLProject();
        BLHelper.BLLPact BLLPact = new BLHelper.BLLPact();
        BLHelper.BLLAttachment BLLattachment = new BLHelper.BLLAttachment();
        BLHelper.BLLOperationLog BLLOp = new BLHelper.BLLOperationLog();
        BLHelper.BLLUser BLLUser = new BLHelper.BLLUser();
        BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
        Common.Entities.Pact NewPact = new Common.Entities.Pact();
        Common.Entities.OperationLog operationLog = new Common.Entities.OperationLog();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitData();
                //DatePicker_StartTime.MaxDate = DateTime.Now;
                //DatePicker_EndTime.MaxDate = DateTime.Now;
            }
        }
        //界面出初始化
        public void InitData()
        {
            //等级下拉框绑定(可选等级不大于登陆等级)
            //string[] arraySecrecyLevel = new string[5] { "公开", "内部", "秘密", "机密", "管理员" };
            string[] arraySecrecyLevel = new string[5] { "四级", "三级", "二级", "一级", "管理员" };
            for (int i = 0; i < Convert.ToInt32(Session["SecrecyLevel"]); i++)
                DropDownList_SecrecyLevel.Items.Add(arraySecrecyLevel[i], (i + 1).ToString());
            List<Common.Entities.Project> list = BLLProject.FindBySecrecyLevel(Convert.ToInt32(Session["SecrecyLevel"]));
            //所属项目下拉框绑定
            for (int i = 0; i < list.Count(); i++)
                DropDownList_Project.Items.Add(list[i].ProjectName, (i + 1).ToString());
        }
        //保存
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPactNum.Text.Trim() == "")
                {
                    Alert.ShowInTop("合同编号不能为空！");
                    txtPactNum.Reset();
                    return;
                }
                if (txtPactType.Text.Trim() == "")
                {
                    Alert.ShowInTop("合同类别不能为空！");
                    txtPactType.Reset();
                    return;
                }
                if (DatePicker_StartTime.SelectedDate > DatePicker_EndTime.SelectedDate)
                {
                    Alert.ShowInTop("结束日期应该大于开始日期！");
                    return;
                }
                NewPact.EndTime = DatePicker_EndTime.SelectedDate;
                NewPact.EntryPerson = BLLUser.FindByLoginName(Session["LoginName"].ToString()).UserName;
                NewPact.PactNum = txtPactNum.Text;
                NewPact.PactType = txtPactType.Text;
                NewPact.ProjectID = BLLProject.SelectProjectID(DropDownList_Project.SelectedText);
                NewPact.SecrecyLevel = Convert.ToInt32(DropDownList_SecrecyLevel.SelectedValue);
                NewPact.StartTime = DatePicker_StartTime.SelectedDate;
                NewPact.PactName = txtPactName.Text;
                NewPact.ChargePerson = txtChargePerson.Text;
                NewPact.PactMoney = txtPactMoney.Text;
                NewPact.RealMoney = txtRealMoney.Text;
                NewPact.PactCompletion = txtPactCompletion.Text;
                NewPact.IsExistingFile = txtIsExistingFile.Text;
                NewPact.FileNum = txtFileNum.Text;
                if (Convert.ToInt32(Session["SecrecyLevel"]) == 5)
                    NewPact.IsPass = true;
                else
                    NewPact.IsPass = false;
                int AttachID = pm.UpLoadFile(fileupload).Attachid;
                switch (AttachID)
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
                    case -3:
                        NewPact.AttachmentID = null;
                        break;
                    //Alert.ShowInTop("请上传附件");
                    //return;
                    default:
                        NewPact.AttachmentID = AttachID;
                        break;
                }

                if (Convert.ToInt32(Session["SecrecyLevel"]) != 5)
                {
                    NewPact.IsPass = false;
                }
                else
                    NewPact.IsPass = true;

                //向合同档案表中插入数据
                BLLPact.Insert(NewPact);

                //向操作日志表中插入数据
                if (Convert.ToInt32(Session["SecrecyLevel"]) != 5)
                {
                    operationLog.LoginIP = " ";
                    operationLog.LoginName = Session["LoginName"].ToString();
                    operationLog.OperationType = "添加";
                    operationLog.OperationContent = "Pact";
                    operationLog.OperationTime = DateTime.Now;
                    operationLog.OperationDataID = NewPact.PactID;
                    BLLOp.Insert(operationLog);
                    Alert.ShowInTop("您的数据已提交,请等待确认！");
                }
                else
                    Alert.ShowInTop("保存成功！");
                PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference());
            }
            catch (Exception ex)
            {
                int PactID = Convert.ToInt32(NewPact.AttachmentID);
                //删除资料附件
                int AttactID = BLLPact.FindAttachmentID(PactID);
                string strPath;
                if (AttactID != 0)
                {
                    strPath = BLLattachment.FindPath(AttactID);
                    if (strPath != "")
                    {
                        //在附件表中删除附件数据
                        BLLattachment.Delete(AttactID);
                        //删除附件文件
                        pm.DeleteFile(AttactID, strPath);
                    }
                }
                //BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
                pm.SaveError(ex, this.Request);
            }
        }
        //重置
        protected void btnReSet_Click(object sender, EventArgs e)
        {
            txtPactNum.Reset();
            txtPactName.Reset();
            DropDownList_Project.SelectedValue = "0";
            DropDownList_SecrecyLevel.SelectedValue = "1";
            DatePicker_StartTime.Reset();
            DatePicker_EndTime.Reset();
            txtPactType.Reset();
            PageContext.RegisterStartupScript("clearFile();");
            //filePath.Reset();
        }
        //取消
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }

    }
}