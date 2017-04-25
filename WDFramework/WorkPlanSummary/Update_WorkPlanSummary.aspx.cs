using Common.Entities;
using FineUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WDFramework.WorkPlanSummary
{
    public partial class Update_WorkPlanSummary : System.Web.UI.Page
    {
        //	分类 0:个人 1:部门
        BLHelper.BLLWorkPlanSummary BLLWorkPlanSummary = new BLHelper.BLLWorkPlanSummary();
        BLHelper.BLLAgency BLLAgency = new BLHelper.BLLAgency();
        BLHelper.BLLUser BLLUser = new BLHelper.BLLUser();
        BLHelper.BLLAttachment BLLattachment = new BLHelper.BLLAttachment();
        Common.Entities.WorkPlanSummary NewWorkPlanSummary = new Common.Entities.WorkPlanSummary();
        Common.Entities.OperationLog OL = new Common.Entities.OperationLog();
        BLHelper.BLLOperationLog operationLog = new BLHelper.BLLOperationLog();
        BLCommon.PublicMethod upLoad = new BLCommon.PublicMethod();
        BLHelper.BLLBasicCode bllBasicCode = new BLHelper.BLLBasicCode();
        protected void Page_Load(object sender, EventArgs e)
        {
            //BindDropDowmList();
            if (!IsPostBack)
            {
                //初始化工作计划与总结分类下拉框
                //InitDropDownListSort();
                int value = 0;
                List<BasicCode> list = bllBasicCode.FindALLName("工作计划与总结分类");
                for (int i = 0; i < list.Count(); i++)
                {
                    DropDownListSort.Items.Add(list[i].CategoryContent.ToString(), (i + 1).ToString());
                }
                //等级下拉框绑定(可选等级不大于登陆等级)
                string[] arraySecrecyLevel = new string[5] { "四级", "三级", "二级", "一级", "管理员" };
                for (int i = 0; i < Convert.ToInt32(Session["SecrecyLevel"]); i++)
                    DropDownList_SecrecyLevel.Items.Add(arraySecrecyLevel[i], (i + 1).ToString());
                if (Session["WorkPlanSummary"] != null)
                {

                    int id = Convert.ToInt32(Session["WorkPlanSummary"]);
                    NewWorkPlanSummary = BLLWorkPlanSummary.FindByWorkPlanSummaryID(id);

                    txtPlanWork.Text = NewWorkPlanSummary.PlanWork;

                    for (int i = 0; i < list.Count(); i++)
                    {
                        if (NewWorkPlanSummary.Sort == list[i].CategoryContent.ToString())
                            value = i + 1;
                    }
                    DropDownListSort.SelectedValue = value.ToString();

                    List<string> list_s = new List<string>();
                    DropDownList.Reset();
                    DropDownList.Items.Clear();
                    DropDownList.Items.Add("请选择", "0");
                    value = 0;
                    if (DropDownListSort.SelectedText == "个人")
                    {
                        labSort.Text = "用户名";

                        //用户名绑定
                        list_s = BLLUser.FindUserBySecrecyLevel(Convert.ToInt32(Session["SecrecyLevel"]));
                        for (int i = 0; i < list.Count(); i++)
                        {
                            DropDownList.Items.Add(list_s[i], (i + 1).ToString());
                            if (BLLUser.FindByUserName(BLLUser.FindByUserID(NewWorkPlanSummary.UserInfoID)).UserName == list_s[i])
                            {
                                value = i + 1;
                            }

                        }

                    }
                    else if (DropDownListSort.SelectedText == "请选择")
                    {
                        labSort.Text = "输入";
                        //labSort.Reset();
                    }
                    else
                    {
                        labSort.Text = "机构名";
                        //DropDownList.Reset();
                        //DropDownList.Items.Clear();
                        //机构名绑定
                        list_s = BLLAgency.FindAgencyBySecrecyLevel(Convert.ToInt32(Session["SecrecyLevel"]));
                        for (int i = 0; i < list.Count(); i++)
                        {
                            DropDownList.Items.Add(list_s[i], (i + 1).ToString());
                            if (BLLAgency.FindByid(NewWorkPlanSummary.AgencyID).AgencyName == list_s[i])
                            {
                                value = i + 1;
                            }
                        }
                        //txtSort.Reset();
                        //txtSort.EmptyText = "机构名";
                    }
                    DropDownList.SelectedValue = value.ToString();

                    DatePikerTime.SelectedDate = NewWorkPlanSummary.Time;
                    DropDownList_SecrecyLevel.SelectedValue = (Convert.ToInt32(NewWorkPlanSummary.SecrecyLevel)).ToString();
                }
            }
        }

        //保存
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["WorkPlanSummary"] != null)
                {

                    int id = Convert.ToInt32(Session["WorkPlanSummary"]);
                    NewWorkPlanSummary.WorkPlanSummaryID = id;
                }
                if (DropDownListSort.SelectedText == "请选择")
                {
                    Alert.ShowInTop("请选择分类！");
                    return;
                }
                if (DropDownList.SelectedText == "请选择")
                {
                    switch (DropDownListSort.SelectedText)
                    {
                        case "个人":
                            Alert.ShowInTop("请选择用户名！");
                            return;
                        case "部门":
                            Alert.ShowInTop("请选择机构名！");
                            return;
                    }
                }
                if (txtPlanWork.Text.Trim() == "")
                {
                    Alert.ShowInTop("工作计划与总结名称不能为空！");
                    txtPlanWork.Reset();
                    return;
                }

                NewWorkPlanSummary = ObjectWorkPlanSummary();
                //UpLoad();
                if (fileupload.Size>0)
                {
                    int AttachID = upLoad.UpLoadFile(fileupload).Attachid;
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
                            Alert.ShowInTop("请上传附件");
                            return;
                        default:
                            NewWorkPlanSummary.Attachment = AttachID;
                            break;
                    }
                }
                else
                {
                    NewWorkPlanSummary.Attachment = -4;
                }

                //向工作计划与总结表中插入数据
                if (NewWorkPlanSummary != null && Convert.ToInt32(Session["SecrecyLevel"]) == 5)
                {
                    BLLWorkPlanSummary.Update(NewWorkPlanSummary);
                    Alert.ShowInTop("保存成功！");
                }
                if (Convert.ToInt32(Session["SecrecyLevel"]) != 5)
                {
                    BLLWorkPlanSummary.Update(NewWorkPlanSummary);
                    //向操作日志表中插入数据
                    OL = InsertOperationLog();
                    OL.OperationDataID = NewWorkPlanSummary.WorkPlanSummaryID;
                    operationLog.Insert(OL);
                    Alert.ShowInTop("您的数据已提交，请等待确认！");
                }
                PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference());
            }
            catch (Exception ex)
            {
                //删除附件文件
                string path = BLLattachment.FindPath(Convert.ToInt32(NewWorkPlanSummary.Attachment));
                if (path != "")
                {
                    upLoad.DeleteFile(Convert.ToInt32(NewWorkPlanSummary.Attachment), path);
                    //删除附件表中的数据
                    BLLattachment.Delete(Convert.ToInt32(NewWorkPlanSummary.Attachment));//删除成功返回true       
                }
                //BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
                upLoad.SaveError(ex, this.Request);
            }
        }
        //重置
        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtPlanWork.Reset();
            DropDownListSort.Reset();
            DropDownList.Reset();
            //txtSort.Reset();
            DatePikerTime.Reset();
            DropDownListSort.Reset();
            DropDownList.SelectedValue = "0";
            PageContext.RegisterStartupScript("clearFile();");
            //filePath.Reset();
        }
        //取消
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        //返回插入操作日志对象
        public Common.Entities.OperationLog InsertOperationLog()
        {
            //向操作日志表插入信息
            Common.Entities.OperationLog operationLog = new Common.Entities.OperationLog();
            operationLog.LoginIP = " ";
            operationLog.LoginName = Session["LoginName"].ToString();
            operationLog.OperationType = "更新";
            operationLog.OperationContent = "WorkPlanSummary";
            operationLog.OperationTime = DateTime.Now;
            return operationLog;
        }
        //返回工作计划与总结对象
        public Common.Entities.WorkPlanSummary ObjectWorkPlanSummary()
        {

            NewWorkPlanSummary.PlanWork = txtPlanWork.Text;
            if (DropDownListSort.SelectedText == "个人")
            {
                List<string> listName = BLLUser.FindUserBySecrecyLevel(Convert.ToInt32(Session["SecrecyLevel"]));
                NewWorkPlanSummary.Sort = "个人";
                NewWorkPlanSummary.UserInfoID = BLLUser.FindByUserName(DropDownList.SelectedText).UserInfoID;
                //NewWorkPlanSummary.Sort = false;
            }
            else
            {
                NewWorkPlanSummary.Sort = "部门";
                NewWorkPlanSummary.AgencyID = BLLAgency.SelectAgencyID(DropDownList.SelectedText);
                // NewWorkPlanSummary.Sort = true;
            }
            NewWorkPlanSummary.Time = Convert.ToDateTime(DatePikerTime.Text);
            NewWorkPlanSummary.SecrecyLevel = Convert.ToInt32(DropDownList_SecrecyLevel.SelectedValue);
            //用户等级为5级可直接通过
            if (Convert.ToInt32(Session["SecrecyLevel"]) == 5)
                NewWorkPlanSummary.IsPass = true;
            else
                NewWorkPlanSummary.IsPass = false;
            NewWorkPlanSummary.EntryPerson = BLLUser.FindByLoginName(Session["LoginName"].ToString()).UserName;
            return NewWorkPlanSummary;

        }

        protected void DropDownListSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            DropDownList.Reset();
            DropDownList.Items.Clear();
            DropDownList.Items.Add("请选择", "0");
            if (DropDownListSort.SelectedText == "个人")
            {
                labSort.Text = "用户名";
                //DropDownList.Reset();
                //DropDownList.Items.Clear();
                //用户名绑定
                list = BLLUser.FindUserBySecrecyLevel(Convert.ToInt32(Session["SecrecyLevel"]));
                for (int i = 0; i < list.Count(); i++)
                    DropDownList.Items.Add(list[i], (i + 1).ToString());
                //txtSort.Reset();
                //txtSort.EmptyText = "用户名";
            }
            else if (DropDownListSort.SelectedText == "请选择")
            {
                labSort.Text = "输入";
                //labSort.Reset();
            }
            else
            {
                labSort.Text = "机构名";
                //DropDownList.Reset();
                //DropDownList.Items.Clear();
                //机构名绑定
                list = BLLAgency.FindAgencyBySecrecyLevel(Convert.ToInt32(Session["SecrecyLevel"]));
                for (int i = 0; i < list.Count(); i++)
                    DropDownList.Items.Add(list[i], (i + 1).ToString());
                //txtSort.Reset();
                //txtSort.EmptyText = "机构名";
            }
        }

        //初始化工作计划与总结分类下拉框
        public void InitDropDownListSort()
        {
            List<BasicCode> list = bllBasicCode.FindALLName("工作计划与总结分类");
            for (int i = 0; i < list.Count(); i++)
            {
                DropDownListSort.Items.Add(list[i].CategoryContent.ToString(), (i + 1).ToString());
            }
        }
    }
}