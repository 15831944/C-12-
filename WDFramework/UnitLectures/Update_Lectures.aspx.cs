/**编写人：李金秋
 * 时间：2014年6月20号
 * 功能：修改单位讲学界面
 * 修改履历：正则表达式验证没加
 **/
using FineUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WDFramework.UnitLectures
{
    public partial class Update_Lectures : System.Web.UI.Page
    {
        BLHelper.BLLOperationLog BLLOL = new BLHelper.BLLOperationLog();
        BLHelper.BLLAttachment BLLattachment = new BLHelper.BLLAttachment();
        BLHelper.BLLUnitLectures BLLUL = new BLHelper.BLLUnitLectures();
        BLHelper.BLLAgency BLLAgency = new BLHelper.BLLAgency();
        BLHelper.BLLUser BLLUser = new BLHelper.BLLUser();
        BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
        Common.Entities.UnitLectures unitLectures = new Common.Entities.UnitLectures();
        Common.Entities.UnitLectures NewUnitLectures = new Common.Entities.UnitLectures();
        //static int StaticUnitLecturesIID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindAgencyName();
                InitData();
                //等级下拉框绑定(可选等级不大于登陆等级)
                string[] arraySecrecyLevel = new string[5] { "四级", "三级", "二级", "一级", "管理员" };
                for (int i = 0; i < Convert.ToInt32(Session["SecrecyLevel"]); i++)
                    DropDownList_SecrecyLevel.Items.Add(arraySecrecyLevel[i], (i + 1).ToString());
                DatePikerLecturesTime.MaxDate = DateTime.Now;
            }
        }
        //初始化
        public void InitData()
        {
            try
            {
                if (Session["UnitLecturesID"].ToString() != "")
                {
                    // OldUnitLectures = BLLUL.FindByUnitLecturesID(Convert.ToInt32(Session["UnitLecturesID"]));
                    unitLectures = BLLUL.FindByUnitLecturesID(Convert.ToInt32(Session["UnitLecturesID"]));
                    txtLecturesName.Text = unitLectures.LecturesName;
                    DropDownList_Agency.Text = BLLAgency.FindAgenName(unitLectures.AgencyID);
                    txtUReportName.Text = unitLectures.UReportName;
                    DatePikerLecturesTime.Text = unitLectures.LecturesTime.Value.Year + "-" + unitLectures.LecturesTime.Value.Month + "-" + unitLectures.LecturesTime.Value.Day;
                    txtWorkPlace.Text = unitLectures.WorkUnit;
                    txtlistenerNumber.Text = unitLectures.listenerNumber.ToString();
                    DropDownList_SecrecyLevel.SelectedValue = unitLectures.SecrecyLevel.ToString();
                    txtLecturesPlace.Text = unitLectures.LecturesPlace;
                    txtIDCard.Text = unitLectures.Identity;
                    txtjobtitle.Text = unitLectures.WorkTitle;
                    txtTel.Text = unitLectures.Telephone;
                    Remark.Text = unitLectures.Remark;
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
                pm.SaveError(ex, this.Request);
            }

        }
        //对所属部门下来框的绑定
        public void BindAgencyName()
        {
            List<string> listAgencyName = BLLAgency.FindAgencyBySecrecyLevel(Convert.ToInt32(Session["SecrecyLevel"]));
            for (int i = 0; i < listAgencyName.Count(); i++)
                DropDownList_Agency.Items.Add(listAgencyName[i].ToString(), (i + 1).ToString());
        }
        //返回插入操作日志对象
        public Common.Entities.OperationLog InsertOperationLog()
        {
            //向操作日志表插入信息
            Common.Entities.OperationLog operationLog = new Common.Entities.OperationLog();
            operationLog.LoginIP = " ";
            operationLog.LoginName = BLLUser.FindByLoginName(Session["LoginName"].ToString()).UserName;
            operationLog.OperationType = "更新";
            operationLog.OperationContent = "UnitLectures";
            operationLog.OperationTime = DateTime.Now;
            operationLog.OperationLogID = unitLectures.UnitLecturesID;
            operationLog.Remark = NewUnitLectures.UnitLecturesID.ToString();
            return operationLog;
        }
        //保存
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtLecturesName.Text.Trim() == "")
                {
                    Alert.ShowInTop("姓名不能为空！");
                    txtLecturesName.Reset();
                    return;
                }
                if (txtWorkPlace.Text.Trim() == "")
                {
                    Alert.ShowInTop("工作单位不能为空！");
                    txtWorkPlace.Reset();
                    return;
                }
                //原单位讲学对象
                unitLectures = BLLUL.FindByUnitLecturesID(Convert.ToInt32(Session["UnitLecturesID"]));
                //单位讲学表对象
                NewUnitLectures.LecturesName = txtLecturesName.Text;
                NewUnitLectures.AgencyID = BLLAgency.SelectAgencyID(DropDownList_Agency.SelectedText);
                NewUnitLectures.UReportName = txtUReportName.Text;
                NewUnitLectures.LecturesTime = Convert.ToDateTime(DatePikerLecturesTime.Text);
                NewUnitLectures.WorkUnit = txtWorkPlace.Text;
                NewUnitLectures.listenerNumber = Convert.ToInt32(txtlistenerNumber.Text);
                NewUnitLectures.SecrecyLevel = Convert.ToInt32(DropDownList_SecrecyLevel.SelectedValue);
                NewUnitLectures.LecturesPlace = txtLecturesPlace.Text;
                NewUnitLectures.WorkTitle = txtjobtitle.Text;
                NewUnitLectures.Identity = txtIDCard.Text;
                NewUnitLectures.Telephone = txtTel.Text;
                NewUnitLectures.Remark = Remark.Text;
                NewUnitLectures.EntryPerson = unitLectures.EntryPerson;
                //用户等级为5级可直接通过
                if (Convert.ToInt32(Session["SecrecyLevel"]) == 5)
                    NewUnitLectures.IsPass = true;
                else
                    NewUnitLectures.IsPass = false;
               
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
                        NewUnitLectures.AttachmentID = unitLectures.AttachmentID;
                        break;
                    default:
                        NewUnitLectures.AttachmentID = AttachID;
                        break;
                }
                if (Convert.ToInt32(Session["SecrecyLevel"]) != 5)
                {
                    //操作日志表对象
                    Common.Entities.OperationLog operationLog = InsertOperationLog();
                    operationLog.OperationDataID = Convert.ToInt32(Session["UnitLecturesID"]);
                    NewUnitLectures.EntryPerson = BLLUser.FindByLoginName(Session["LoginName"].ToString()).UserName;
                    NewUnitLectures.IsPass = false;
                    unitLectures.IsPass = false;
                    BLLUL.Insert(NewUnitLectures);//向单位讲学表插入信息
                    operationLog.Remark = NewUnitLectures.UnitLecturesID.ToString();
                    BLLOL.Insert(operationLog);//向操作日志表插入信息
                    Alert.ShowInTop("操数据已经提交，等待管理员确认！");
                }
                else
                {
                    NewUnitLectures.UnitLecturesID = Convert.ToInt32(Session["UnitLecturesID"]);
                    BLLUL.Update(NewUnitLectures);
                    Alert.ShowInTop("保存成功！");
                }
                PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference());
            }
            catch (Exception ex)
            {
                //删除附件文件
                string path = BLLattachment.FindPath(Convert.ToInt32(unitLectures.AttachmentID));
                if (path != "")
                {
                    pm.DeleteFile(Convert.ToInt32(unitLectures.AttachmentID), path);
                    //删除附件表中的数据
                    BLLattachment.Delete(Convert.ToInt32(unitLectures.AttachmentID));//删除成功返回true    
                }
                //BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
                pm.SaveError(ex, this.Request);
            }
        }
        //重置
        protected void btnReset_Click(object sender, EventArgs e)
        {
            InitData();
        }
        //取消
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }


    }
}