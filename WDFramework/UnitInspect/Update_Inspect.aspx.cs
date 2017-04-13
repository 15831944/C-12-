/**编写人：方淑云
 * 时间：2014年8月10号
 * 功能:来单位考察添加界面后台
 * 修改履历：
 **/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common.Entities;
using FineUI;
using System.IO;


namespace WDFramework.UnitInspect
{
    public partial class Update_Inspect : System.Web.UI.Page
    {
        BLHelper.BLLUnitInspect inspect = new BLHelper.BLLUnitInspect();
        Common.Entities.UnitInspect ins = new Common.Entities.UnitInspect();
        BLHelper.BLLAgency agency = new BLHelper.BLLAgency();
        BLHelper.BLLUser user = new BLHelper.BLLUser();
        BLHelper.BLLAttachment attachment = new BLHelper.BLLAttachment();
        BLHelper.BLLOperationLog op = new BLHelper.BLLOperationLog();
        BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
        OperationLog log = new OperationLog();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dInspectTime.MaxDate = DateTime.Now;
                InitSecrecyLevel();
                InitDropListAgency();
                InitData();
            }
        }
        //初始化
        public void InitData()
        {
            try
            {
                Common.Entities.UnitInspect ins = inspect.FindInspectInfo(Convert.ToInt32(Session["InspectID"]), true);
                if (!string.IsNullOrEmpty(ins.Duty))
                {
                    tDuty.Text = ins.Duty;
                }
                else
                {
                    tDuty.Text = "";
                }
                dInspectTime.SelectedDate = ins.InspectTime;
                tInspectName.Text = ins.InspectName;
                tWorkPlace.Text = ins.WorkPlace;
                tVisitContent.Text = ins.VisitContent;
                dSecrecyLevel.SelectedIndex = Convert.ToInt32(ins.SecrecyLevel - 1);
                DropDownListAgency.SelectedValue = agency.FindAgenName(ins.AgencyID.Value);
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //初始化等级下拉框
        public void InitSecrecyLevel()
        {
            string[] SecrecyLevels = new string[] { "四级", "三级", "二级", "一级", "管理员" };
           // string[] SecrecyLevels = new string[] { "公开", "内部", "秘密", "机密", "管理员" };
            for (int i = 0; i < Convert.ToInt32(Session["SecrecyLevel"]); i++)
            {
                dSecrecyLevel.Items.Add(SecrecyLevels[i], i.ToString());
            }
        }
        //初始化机构下拉框
        public void InitDropListAgency()
        {
            try
            {
                BLHelper.BLLAgency agency = new BLHelper.BLLAgency();
                List<Common.Entities.Agency> list = agency.FindAllAgencyName();
                for (int i = 0; i < list.Count(); i++)
                {
                    DropDownListAgency.Items.Add(list[i].AgencyName.ToString(), list[i].AgencyName.ToString());
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
    
        //保存更改
        protected void Save_Click(object sender, EventArgs e)
        {
            try
            {
                if (tInspectName.Text.Trim() == "")
                {
                    Alert.Show("姓名不能为空！");
                    return;
                }
                if (tWorkPlace.Text.Trim() == "")
                {
                    Alert.Show("工作单位不能不空！");
                    return;
                }
                if (tVisitContent.Text.Trim() == "")
                {
                    Alert.Show("参观内容不能为空！");
                    return;
                }
                BLHelper.BLLAgency agency = new BLHelper.BLLAgency();
                ins.EntryPerson = inspect.Findmodel(Convert.ToInt32(Session["InspectID"])).EntryPerson;
                ins.InspectName = tInspectName.Text.Trim();
                ins.VisitContent = tVisitContent.Text.Trim();
                ins.InspectTime = dInspectTime.SelectedDate;
                ins.WorkPlace = tWorkPlace.Text.Trim();
                ins.SecrecyLevel = Convert.ToInt32(dSecrecyLevel.SelectedIndex + 1);
                ins.AgencyID = agency.SelectAgencyID(DropDownListAgency.SelectedText);
                ins.Duty = tDuty.Text.Trim();
                int AttachmentID = inspect.FindAttachmentID(Convert.ToInt32(Session["InspectID"]));
                string path = attachment.FindPath(AttachmentID);
                string username = user.FindByLoginName(Session["LoginName"].ToString()).UserName;
                if (Convert.ToInt32(Session["SecrecyLevel"]) == 5)
                {
                    ins.IsPass = true;
                    ins.UnitInspectID = Convert.ToInt32(Session["InspectID"]);
                    int Attachment = pm.UpLoadFile(fileupload).Attachid;
                    if (Attachment != -3)
                    {
                        switch (Attachment)
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
                        ins.AccessoryID = Attachment;
                        inspect.Update(ins);
                        pm.DeleteFile(AttachmentID, path);
                        PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("保存成功！"));
                    }
                    else
                    {
                        if (AttachmentID != 0)
                        {
                            ins.AccessoryID = AttachmentID;
                        }
                        inspect.Update(ins);
                        PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("保存成功！"));
                    }
                }
                else
                {
                    log.LoginName = username;
                    log.OperationTime = DateTime.Now;
                    log.LoginIP = " ";
                    log.OperationContent = "UnitInspect";
                    log.OperationType = "更新";
                    ins.IsPass = false;
                    int Attachment = pm.UpLoadFile(fileupload).Attachid;
                    if (Attachment != -3)
                    {
                        switch (Attachment)
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
                        ins.AccessoryID = Attachment;
                        inspect.Insert(ins);
                        log.OperationDataID = Convert.ToInt32(Session["InspectID"]);
                        log.Remark = ins.UnitInspectID.ToString();
                        op.Insert(log);
                        inspect.ChangePass(Convert.ToInt32(Session["InspectID"]), false);
                        PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("您的数据已提交，请等待确认！"));
                    }
                    else
                    {
                        if (AttachmentID != 0)
                        {
                            ins.AccessoryID = AttachmentID;
                        }
                        inspect.Insert(ins);
                        log.OperationDataID = Convert.ToInt32(Session["InspectID"]);
                        log.Remark = ins.UnitInspectID.ToString();
                        op.Insert(log);
                        inspect.ChangePass(Convert.ToInt32(Session["InspectID"]), false);
                        PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("您的数据已提交，请等待确认！"));
                    }

                }
            }
            catch (Exception ex)
            {
                int attachid = Convert.ToInt32(ins.AccessoryID);
                string path = attachment.FindPath(attachid);
                pm.DeleteFile(attachid, path);
                pm.SaveError(ex, this.Request);
            }
        }
        //重置
        protected void DeleteAll_Click(object sender, EventArgs e)
        {
            try
            {
                tInspectName.Reset();
                tVisitContent.Reset();
                dInspectTime.Reset();
                tWorkPlace.Reset();
                dSecrecyLevel.Reset();
                DropDownListAgency.Reset();
                tDuty.Reset();
                PageContext.RegisterStartupScript("clearFile();");
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }      
    }
}