using Common.Entities;
using FineUI;
/**编写人：方淑云
 * 时间：2014年8月17号
 * 功能：专利更新界面的相关操作
 * 修改履历：     1、修改人：吕博扬
 *                  修改时间：2015年9月23日
 *                  修改内容：取消所有输入项的数据校验
 *               2、修改人：吕博杨
 *                  修改时间：2015年11月28日
 *                  修改内容：隐藏经费、成员字段相关的后台代码
 *                           将附件分为专利证书、申请书分别上传
 *                           新增专利授权号、专利证书号字段的读写
 **/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1.Achievement.Patent
{
    public partial class Update_Patent : System.Web.UI.Page
    {
        BLHelper.BLLAchievement ach = new BLHelper.BLLAchievement();
        BLHelper.BLLPatent patent = new BLHelper.BLLPatent();
        BLHelper.BLLOperationLog op = new BLHelper.BLLOperationLog();
        OperationLog operate = new OperationLog();
        BLHelper.BLLUser user = new BLHelper.BLLUser();
        BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
        Common.Entities.Patent pat = new Common.Entities.Patent();
        BLHelper.BLLAttachment at = new BLHelper.BLLAttachment();
        BLHelper.BLLBasicCode ba = new BLHelper.BLLBasicCode();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                tApplicationTime.MaxDate = DateTime.Now;
                tAccreditTime.MaxDate = DateTime.Now;
                InitdPatentForm();
                InitDropListAgency();
                InitPatentCondition();
                InitSecrecyLevel();
                InitData();
            }
        }
        //初始化等级下拉框
        public void InitSecrecyLevel()
        {
            //string level = "公开";
            //tSecrecyLevel.Text = level;
            for (int i = 0; i < Convert.ToInt32(Session["SecrecyLevel"]); i++)
            {
                string[] SecrecyLevels = new string[] { "四级", "三级", "二级", "一级", "管理员" };
                drSecrecyLevel.Items.Add(SecrecyLevels[i], i.ToString());
            }
        }
        //初始化专利情况
        public void InitPatentCondition()
        {
            try
            {
                List<BasicCode> listname = ba.FindByCategoryName("专利情况");
                for (int i = 0; i < listname.Count(); i++)
                {
                    dPatentCondition.Items.Add(listname[i].CategoryContent.ToString(), listname[i].CategoryContent.ToString());
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
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
        //初始化专利类型对话框
        public void InitdPatentForm()
        {
            try
            {
                List<BasicCode> listname = ba.FindByCategoryName("专利类型");
                for (int i = 0; i < listname.Count(); i++)
                {
                    dPatentForm.Items.Add(listname[i].CategoryContent.ToString(), listname[i].CategoryContent.ToString());
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }

        }


        //初始化
        public void InitData()
        {
            try
            {
                List<Common.Entities.Patent> list = patent.FindAll(Convert.ToInt32(Session["PatentID"]));
                Common.Entities.Patent pa = list.FirstOrDefault();
                tAccreditTime.SelectedDate = pa.AccreditTime;
                tApplicationTime.SelectedDate = pa.ApplicationTime;
                tCertificateNumber.Text = pa.CertificateNumber;
                if (pa.Comment != null)
                {
                    tRemark.Text = pa.Comment;
                }
                else
                {
                    tRemark.Text = "";
                }
                DropDownListAgency.SelectedValue = pa.AgencyID;
                //Fund.Text = pa.Fund;
                FirstPeople.Text = pa.FirstPeople;
                dPatentCondition.SelectedValue = pa.PatentCondition;
                //ApplyNum.Text = pa.ApplyNum;
                //pa.EntryPerson = Session["LoginName"].ToString();
                tGivenUnit.Text = pa.GivenUnit;
                dPatentForm.SelectedValue = pa.PatentForm;
                tPatentName.Text = pa.PatentName;
                tPatentNumber.Text = pa.PatentNumber;
                //if (pa.SecrecyLevel == 1)
                //{
                //    tSecrecyLevel.Text = "公开";

                //}
                //if (pa.SecrecyLevel == 3)
                //{
                //    tSecrecyLevel.Text = "秘密";
                //}
                drSecrecyLevel.SelectedIndex = Convert.ToInt32(pa.SecrecyLevel - 1);
                tState.Text = pa.State;
                if (pa.AchievementID != null)
                {
                    tAchievement.Text = ach.FindAchieveName(Convert.ToInt32(pa.AchievementID));
                    tAchievement.Text = pa.AchievementID;
                }
                else
                {
                    tAchievement.Text = "";
                }
                PatentPeople.Text = pa.PatentPeople;
                //PatentMember.Text = pa.Member;
                if (pa.PatentDepartment != null)
                {
                    List<string> res = new List<string>();
                    string[] str = pa.PatentDepartment.Split(',');
                    foreach (string ss in str)
                    {
                        res.Add(ss);
                    }
                    switch (res.Count)
                    {
                        case 1:
                            tPatentDepartment1.Text = res[0];
                            break;
                        case 2:
                            tPatentDepartment1.Text = res[0];
                            tPatentDepartment2.Text = res[1];
                            break;
                        case 3:
                            tPatentDepartment1.Text = res[0];
                            tPatentDepartment2.Text = res[1];
                            tPatentDepartment3.Text = res[2];
                            break;
                    }
                }
                else
                {
                    tPatentDepartment1.Text = "";
                    tPatentDepartment2.Text = "";
                    tPatentDepartment3.Text = "";
                }
                //lby ↓
                tPatentAuthorization.Text = pa.PatentAuthorization.ToString();
                tPatentCertificate.Text = pa.PatentCertificate.ToString();
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //更新赋值
        public void UpdateValue()
        {
            pat.AccreditTime = tAccreditTime.SelectedDate;
            pat.ApplicationTime = tApplicationTime.SelectedDate;
            pat.CertificateNumber = tCertificateNumber.Text.Trim();
            pat.Comment = tRemark.Text.Trim();
            pat.EntryPerson = patent.FindByPatentID(Convert.ToInt32(Session["PatentID"])).EntryPerson;
            pat.GivenUnit = tGivenUnit.Text.Trim();
            pat.PatentForm = dPatentForm.SelectedText;
            pat.AgencyID = DropDownListAgency.SelectedText.Trim();
            pat.FirstPeople = FirstPeople.Text.Trim();
            //pat.Fund = Fund.Text;
            pat.PatentCondition = dPatentCondition.SelectedText;
            //pat.ApplyNum = ApplyNum.Text.Trim();
            pat.PatentName = tPatentName.Text.Trim();
            pat.PatentNumber = tPatentNumber.Text.Trim();
            //if (tSecrecyLevel.Text.Trim() == "公开")
            //{
            //    pat.SecrecyLevel = 1;
            //}
            //if (tSecrecyLevel.Text.Trim() == "秘密")
            //{
            //    pat.SecrecyLevel = 3;
            //}
            pat.SecrecyLevel = Convert.ToInt32(drSecrecyLevel.SelectedIndex + 1);
            pat.State = tState.Text.Trim();
            pat.PatentPeople = PatentPeople.Text.Trim();
            //pat.Member = PatentMember.Text.Trim();

            //lby ↓
            bool IsHasLetter = false;
            foreach (char temp in tPatentAuthorization.Text)
                if (char.IsLetter(temp))
                {
                    IsHasLetter = true;
                    break;
                }
            if (!IsHasLetter)
                pat.PatentAuthorization = Convert.ToInt32(tPatentAuthorization.Text);
            foreach (char temp in tPatentCertificate.Text)
                if (char.IsLetter(temp))
                {
                    IsHasLetter = true;
                    break;
                }
            if (!IsHasLetter)
                pat.PatentCertificate = Convert.ToInt32(tPatentCertificate.Text);

            if (tAchievement.Text.Trim() != "")
            {
                pat.AchievementID = tAchievement.Text.Trim();
            }
            else
            {
                pat.AchievementID = null;
            }

        }
        //更新保存
        protected void Save_Click(object sender, EventArgs e)
        {
            try
            {
                //if (tPatentName.Text.Trim() == "")
                //{
                //    Alert.Show("专利名称不能为空！");
                //    return;
                //}
                //if (tPatentNumber.Text.Trim() == "")
                //{
                //    Alert.Show("专利号不能为空！");
                //    return;
                //}
                //if (tGivenUnit.Text.Trim() == "")
                //{
                //    Alert.Show("授予机构不能为空！");
                //    return;
                //}
                //if (tState.Text.Trim() == "")
                //{
                //    Alert.Show("状态不能为空！");
                //    return;
                //}
                //if (PatentPeople.Text.Trim() == "")
                //{
                //    Alert.Show("发明人不能为空！");
                //    return;
                //}
                //if (FirstPeople.Text.Trim() == "")
                //{
                //    Alert.Show("第一发明人不能为空！");
                //    return;
                //}
                //if (tAccreditTime.SelectedDate < tApplicationTime.SelectedDate)
                //{
                //    Alert.ShowInTop("授权时间不能小于申请时间！");
                //    return;
                //}
                List<string> list = new List<string>();
                string unit = "";
                list.Add(tPatentDepartment1.Text.ToString());
                list.Add(tPatentDepartment2.Text.ToString());
                list.Add(tPatentDepartment3.Text.ToString());
                List<string> newlist = new List<string>();
                for (int i = 0; i < 3; i++)
                {
                    if (!string.IsNullOrEmpty(list[i].ToString()))
                    {
                        newlist.Add(list[i].ToString());
                    }
                }

                if (newlist.Count != 0)
                {
                    for (int j = 0; j < newlist.Count; j++)
                    {
                        unit += newlist[j].ToString();
                        if (j == newlist.Count() - 1)
                        {
                            break;
                        }
                        unit += ",";
                    }
                    pat.PatentDepartment = unit;
                }
                else
                {
                    pat.PatentDepartment = null;
                }
                //lby ↓
                int[] AttachmentID = patent.FindAttachmentID(Convert.ToInt32(Session["PatentID"]));
                UpdateValue();
                if (Convert.ToInt32(Session["SecrecyLevel"]) == 5)//如果等于5级
                {
                    string path_Patent, path_Application;
                    if (AttachmentID != null)
                    {
                        path_Patent = at.FindPath(AttachmentID[0]);
                        path_Application = at.FindPath(AttachmentID[1]);
                    }
                    else
                    {
                        path_Patent = null;
                        path_Application = null;
                    }
                    pat.IsPass = true;
                    pat.PatentID = Convert.ToInt32(Session["PatentID"]);
                    //lby ↓
                    int attachid = pm.UpLoadFile(PatentFile).Attachid;
                    if (attachid != -3)//上传控件是否有值
                    {
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
                        pat.Attachment_Patent = attachid;//附件为新插入的附件ID
                        pm.DeleteFile(AttachmentID[0], path_Patent);
                    }
                    else //上传控件没有值
                    {
                        if (AttachmentID != null && AttachmentID[0] != 0)
                            pat.Attachment_Patent = AttachmentID[0];
                    }

                    attachid = pm.UpLoadFile(ApplicationFile).Attachid;
                    if (attachid != -3)//上传控件是否有值
                    {
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
                        pat.Attachment_Application = attachid;//附件为新插入的附件ID
                        pm.DeleteFile(AttachmentID[1], path_Application);
                    }
                    else //上传控件没有值
                    {
                        if (AttachmentID != null && AttachmentID[1] != 0)
                            pat.Attachment_Application = AttachmentID[1];
                    }
                    patent.Update(pat);//更新
                    PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("更新成功！"));
                }
                else//小于5级
                {
                    string username = user.FindByLoginName(Session["LoginName"].ToString()).UserName;
                    operate.LoginName = username;
                    operate.OperationTime = DateTime.Now;
                    operate.LoginIP = " ";
                    operate.OperationContent = "Patent";
                    operate.OperationType = "更新";
                    pat.IsPass = false;

                    //lby ↓
                    int attachid = pm.UpLoadFile(PatentFile).Attachid;
                    if (attachid != -3)//上传控件是否有值
                    {
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
                        pat.Attachment_Patent = attachid;//附件为新插入的附件ID
                    }
                    else//上传控件没有值
                    {
                        if (AttachmentID[0] != 0)//原来有附件
                            pat.Attachment_Patent = AttachmentID[0];
                    }

                    attachid = pm.UpLoadFile(ApplicationFile).Attachid;
                    if (attachid != -3)//上传控件是否有值
                    {
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
                        pat.Attachment_Application = attachid;//附件为新插入的附件ID
                    }
                    else//上传控件没有值
                    {
                        if (AttachmentID[1] != 0)//原来有附件
                            pat.Attachment_Application = AttachmentID[1];
                    }

                    patent.Insert(pat);//插入
                    operate.OperationDataID = Convert.ToInt32(Session["PatentID"]);
                    operate.Remark = pat.PatentID.ToString();
                    op.Insert(operate);//将成果更新插入操作表      
                    patent.UpdateIsPass(Convert.ToInt32(Session["PatentID"]), false);
                    PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("数据已缓存，正在等待审核！"));
                }
            }
            catch (Exception ex)
            {
                //lby ↓
                if (pat.Attachment_Patent != null && pat.Attachment_Application != null)
                {
                    int[] attachid = { pat.Attachment_Patent.Value, pat.Attachment_Application.Value };
                    string path_Patent = at.FindPath(attachid[0]);
                    string path_Application = at.FindPath(attachid[1]);
                    pm.DeleteFile(attachid[0], path_Patent);
                    pm.DeleteFile(attachid[1], path_Application);
                }
                pm.SaveError(ex, this.Request);
            }
        }
        //等级变化
        //protected void dPatentForm_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (dPatentForm.SelectedText == "国防发明")
        //    {

        //        tSecrecyLevel.Text = "秘密";
        //    }
        //    else
        //    {

        //        tSecrecyLevel.Text = "公开";
        //    }           
        //}
        //重置
        protected void DeleteAll_Click(object sender, EventArgs e)
        {
            try
            {
                PatentPeople.Reset();
                tAccreditTime.Reset();
                tApplicationTime.Reset();
                tCertificateNumber.Reset();
                tRemark.Reset();
                tGivenUnit.Reset();
                dPatentForm.Reset();
                tPatentName.Reset();
                tPatentNumber.Reset();
                drSecrecyLevel.Reset();
                tState.Reset();
                tAchievement.Reset();
                tPatentDepartment1.Reset();
                tPatentDepartment2.Reset();
                tPatentDepartment3.Reset();
                DropDownListAgency.Reset();
                FirstPeople.Reset();
                //Fund.Reset();
                dPatentCondition.Reset();

                //lby ↓
                tPatentAuthorization.Reset();
                tPatentCertificate.Reset();

                //ApplyNum.Reset();
                PageContext.RegisterStartupScript("clearFile();");
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        ////成果名称验证
        //protected void tAchievement_TextChanged(object sender, EventArgs e)
        //{
        //    if (tAchievement.Text.Trim() != "")
        //    {
        //        if (ach.IsExitAchieveName(tAchievement.Text.Trim()) == null)
        //        {
        //            Alert.Show("该成果名不存在！");
        //            tAchievement.Text = "";
        //            return;
        //        }
        //        else
        //        {
        //            if (ach.IsExitAchieveName(tAchievement.Text.Trim()).IsPass == false)
        //            {
        //                Alert.Show("该成果名称正在审核中！");
        //                tAchievement.Text = "";
        //                return;
        //            }
        //        }
        //    }
        //}
    }
}