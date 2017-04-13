/**编写人：李金秋
 * 时间：2014年6月20号
 * 功能：新增单位讲学界面
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

namespace WebApplication1
{
    public partial class 新增讲学信息页面 : System.Web.UI.Page
    {
        BLHelper.BLLOperationLog BLLOL = new BLHelper.BLLOperationLog();
        BLHelper.BLLAttachment BLLattachment = new BLHelper.BLLAttachment();
        BLHelper.BLLUnitLectures BLLUL = new BLHelper.BLLUnitLectures();
        BLHelper.BLLAgency BLLAgency = new BLHelper.BLLAgency();
        BLHelper.BLLUser BLLUser = new BLHelper.BLLUser();
        BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
        Common.Entities.UnitLectures unitLectures = new Common.Entities.UnitLectures();
        //static int StaticUnitLecturesIID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindAgencyName();
                //等级下拉框绑定(可选等级不大于登陆等级)
                string[] arraySecrecyLevel = new string[5] { "四级", "三级", "二级", "一级", "管理员" };
                for (int i = 0; i < Convert.ToInt32(Session["SecrecyLevel"]); i++)
                    DropDownList_SecrecyLevel.Items.Add(arraySecrecyLevel[i], (i + 1).ToString());
                DatePikerLecturesTime.MaxDate = DateTime.Now;
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
            operationLog.LoginName = Session["LoginName"].ToString();
            operationLog.OperationType = "添加";
            operationLog.OperationContent = "UnitLectures";
            operationLog.OperationTime = DateTime.Now;
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
                //change_1
                //if (DropDownList_Agency.Items.Count == 0)
                //{
                //    Alert.ShowInTop("请先添加机构!");
                //    return;
                //}
                //单位讲学表对象
                unitLectures.LecturesName = txtLecturesName.Text;
                unitLectures.AgencyID = BLLAgency.SelectAgencyID(DropDownList_Agency.SelectedText);
                unitLectures.UReportName = txtUReportName.Text;
                unitLectures.LecturesTime = Convert.ToDateTime(DatePikerLecturesTime.Text);
                unitLectures.WorkUnit = txtWorkPlace.Text;
                if (txtlistenerNumber.Text.Trim() == "")
                    unitLectures.listenerNumber = null;
                else
                    unitLectures.listenerNumber = Convert.ToInt32(txtlistenerNumber.Text.Trim());
                unitLectures.SecrecyLevel = Convert.ToInt32(DropDownList_SecrecyLevel.SelectedValue);
                unitLectures.LecturesPlace = txtLecturesPlace.Text;
                unitLectures.WorkTitle = txtjobtitle.Text;
                unitLectures.Identity = txtIDCard.Text;
                unitLectures.Telephone = txtTel.Text;
                unitLectures.Remark = Remark.Text;
                //用户等级为5级可直接通过
                if (Convert.ToInt32(Session["SecrecyLevel"]) == 5)
                    unitLectures.IsPass = true;
                else
                    unitLectures.IsPass = false;
                unitLectures.EntryPerson = BLLUser.FindByLoginName(Session["LoginName"].ToString()).UserName;

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
                        unitLectures.AttachmentID = null;
                        break;
                    //Alert.ShowInTop("请上传附件");
                    //return;
                    default:
                        unitLectures.AttachmentID = AttachID;
                        break;
                }
                //向单位讲学表中插入信息
                BLLUL.Insert(unitLectures);
                if (Convert.ToInt32(Session["SecrecyLevel"]) != 5)
                {
                    //操作日志表对象
                    Common.Entities.OperationLog operationLog = InsertOperationLog();
                    operationLog.OperationDataID = unitLectures.UnitLecturesID;
                    BLLOL.Insert(operationLog);
                    Alert.ShowInTop("您的数据已提交，请等待确认");
                    //PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("数据已缓存，正在等待审核！"));
                    //return;
                }
                else
                    Alert.ShowInTop("保存成功");
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
            //PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("保存成功！"));
        }
        //重置
        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtLecturesName.Reset();
            txtUReportName.Reset();
            DatePikerLecturesTime.Reset();
            txtWorkPlace.Reset();
            txtlistenerNumber.Reset();
            DropDownList_SecrecyLevel.SelectedValue = "0";
            DropDownList_Agency.Reset();
            txtLecturesPlace.Reset();
            txtIDCard.Reset();
            txtjobtitle.Reset();
            Remark.Reset();
            txtTel.Reset();

        }
        //取消
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
    }
}