using Common.Entities;
using FineUI;
/**编写人：方淑云
 * 时间：2014年8月17号
 * 功能：专利添加界面的相关操作
 * 修改履历：    1、修改人：吕博杨
 *                 修改时间：2015年11月28日
 *                 修改内容：隐藏经费、成员、申请号字段有关的后台代码
 *                          将附件分为申请书、专利证书两部分上传
 *                          新增专利授权号、专利证书号字段的读写
 **/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class Add_Patent : System.Web.UI.Page
    {
        BLHelper.BLLAchievement ach = new BLHelper.BLLAchievement();
        BLHelper.BLLPatent patent = new BLHelper.BLLPatent();
        BLHelper.BLLAgency agency = new BLHelper.BLLAgency();
        BLHelper.BLLUser user = new BLHelper.BLLUser();
        BLHelper.BLLAttachment at = new BLHelper.BLLAttachment();
        BLHelper.BLLOperationLog op = new BLHelper.BLLOperationLog();
        OperationLog operate = new OperationLog();
        Patent pa = new Patent();
        BLHelper.BLLBasicCode ba = new BLHelper.BLLBasicCode();
        BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                tApplicationTime.MaxDate = DateTime.Now;
                tAccreditTime.MaxDate = DateTime.Now;
                InitAchievement();
                InitdPatentForm();
                InitDropListAgency();
                InitPatentCondition();
                InitSecrecyLevell();
            }
        }
        public void InitAchievement()
        {
            try
            {
                BLHelper.BLLAchievement achevement = new BLHelper.BLLAchievement();
                List<Common.Entities.Achievement> list = achevement.FindAllAchievementName();
                for (int i = 0; i < list.Count(); i++)
                {
                    tAchievement.Items.Add(list[i].AchievementName.ToString(), list[i].AchievementID.ToString());
                }
                tAchievement.SelectedValue = list[0].AchievementID.ToString();
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //初始化等级下拉框
        public void InitSecrecyLevell()
        {       
            for (int i = 0; i < Convert.ToInt32(Session["SecrecyLevel"]); i++)
            {
                string[] SecrecyLevels = new string[] { "四级", "三级", "二级", "一级", "管理员" };
                drSecrecyLevel.Items.Add(SecrecyLevels[i], i.ToString());
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
      
        //插入赋值
        public void InsertValue()
        {            
            pa.AccreditTime = tAccreditTime.SelectedDate;
            pa.ApplicationTime = tApplicationTime.SelectedDate;
            pa.CertificateNumber = tCertificateNumber.Text.Trim();
            if (tRemark.Text.Trim() != "")
            {
                pa.Comment = tRemark.Text.Trim();
            }
            else
            {
                pa.Comment = "";
            }
            pa.AgencyID = DropDownListAgency.SelectedText.Trim();
            pa.FirstPeople = FirstPeople.Text.Trim();
            //pa.Fund = Fund.Text;
            pa.PatentCondition = dPatentCondition.SelectedText;
            //pa.ApplyNum = ApplyNum.Text.Trim();
            pa.GivenUnit = tGivenUnit.Text.Trim();
            pa.PatentForm = dPatentForm.SelectedText;
            pa.PatentName = tPatentName.Text.Trim();
            pa.PatentNumber = tPatentNumber.Text.Trim();
            //if (tSecrecyLevel.Text.Trim() == "公开")
            //{
            //    pa.SecrecyLevel = 1;
            //}
            //if (tSecrecyLevel.Text.Trim() == "秘密")
            //{
            //    pa.SecrecyLevel = 3;
            //}
            pa.SecrecyLevel = Convert.ToInt32(drSecrecyLevel.SelectedIndex + 1); 
            pa.State = tState.Text.Trim();
            //pa.Member = PatentMember.Text.Trim();
            if (tAchievement.Text.Trim() != "")
            {
                pa.AchievementID = tAchievement.Text.Trim();
            }
            else
            {
                pa.AchievementID = null;
            }
            pa.PatentPeople = PatentPeople.Text.Trim();       
 
            //lby ↓
            bool IsHasLetter = false;
            foreach (char temp in tPatentAuthorization.Text)
                if (char.IsLetter(temp))
                {
                    IsHasLetter = true;
                    break;
                }
            if (!IsHasLetter)
                pa.PatentAuthorization = Convert.ToInt32(tPatentAuthorization.Text);
            foreach (char temp in tPatentCertificate.Text)
                if (char.IsLetter(temp))
                {
                    IsHasLetter = true;
                    break;
                }
            if (!IsHasLetter)
                pa.PatentCertificate = Convert.ToInt32(tPatentCertificate.Text);
        }
     
        //保存
        protected void Save_Click(object sender, EventArgs e)
        {
            try
            {
                if (tPatentName.Text.Trim() == "")
                {
                    Alert.Show("专利名称不能为空！");
                    return;
                }
                if (tPatentNumber.Text.Trim() == "")
                {
                    Alert.Show("专利号不能为空！");
                    return;
                }
                if (tGivenUnit.Text.Trim() == "")
                {
                    Alert.Show("授予机构不能为空！");
                    return;
                }
                if (tState.Text.Trim() == "")
                {
                    Alert.Show("状态不能为空！");
                    return;
                }
                if (PatentPeople.Text.Trim() == "")
                {
                    Alert.Show("全部发明人不能为空！");
                    return;
                }
                if (FirstPeople.Text.Trim() == "")
                {
                    Alert.Show("第一发明人不能为空！");
                    return;
                }
                if (tAccreditTime.SelectedDate < tApplicationTime.SelectedDate)
                {
                    Alert.ShowInTop("授权时间不能小于申请时间！");
                    return;
                }
                string username = user.FindByLoginName(Session["LoginName"].ToString()).UserName;
                pa.EntryPerson = username;
                //pa.Member = PatentMember.Text.Trim();
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
                    pa.PatentDepartment = unit;
                }
                else
                {
                    pa.PatentDepartment = null;
                }
                if (Convert.ToInt32(Session["SecrecyLevel"]) == 5)
                {
                    InsertValue();
                    pa.IsPass = true;
                    //lby ↓
                    int attachid = pm.UpLoadFile(PatentFile).Attachid;
                    switch (attachid)
                    {
                        case -1:
                            Alert.ShowInTop("专利证书文件类型不符，请重新选择！");
                            return;
                        case 0:
                            Alert.ShowInTop("专利证书文件名已经存在！");
                            return;
                        case -2:
                            Alert.ShowInTop("专利证书文件不能大于150M");
                            return;
                    }
                    if (attachid != -3)
                    {
                        pa.Attachment_Patent = attachid;
                    }
                    else
                    {
                        pa.Attachment_Patent = null;
                    }

                    attachid = pm.UpLoadFile(ApplicationFile).Attachid;
                    switch (attachid)
                    {
                        case -1:
                            Alert.ShowInTop("申请书文件类型不符，请重新选择！");
                            return;
                        case 0:
                            Alert.ShowInTop("申请书文件名已经存在！");
                            return;
                        case -2:
                            Alert.ShowInTop("申请书文件不能大于150M");
                            return;
                    }
                    if (attachid != -3)
                    {
                        pa.Attachment_Application = attachid;
                    }
                    else
                    {
                        pa.Attachment_Application = null;
                    }
                    if(tAchievement.SelectedItem!=null)
                    {
                        pa.AchievementID = tAchievement.SelectedItem.Text;
                    }
                    else
                    {
                        pa.AchievementID = tAchievement.Text;
                    }
                    patent.Insert(pa);
                    PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("保存成功！"));
                }
                else
                {
                    InsertValue();
                    pa.IsPass = false;
                    //lby ↓
                    int attachid = pm.UpLoadFile(PatentFile).Attachid;
                    switch (attachid)
                    {
                        case -1:
                            Alert.ShowInTop("专利证书文件类型不符，请重新选择！");
                            return;
                        case 0:
                            Alert.ShowInTop("专利证书文件名已经存在！");
                            return;
                        case -2:
                            Alert.ShowInTop("专利证书文件不能大于150M");
                            return;
                    }
                    if (attachid != -3)
                    {
                        pa.Attachment_Patent = attachid;
                    }
                    else
                    {
                        pa.Attachment_Patent = null;
                    }

                    attachid = pm.UpLoadFile(ApplicationFile).Attachid;
                    switch (attachid)
                    {
                        case -1:
                            Alert.ShowInTop("申请书文件类型不符，请重新选择！");
                            return;
                        case 0:
                            Alert.ShowInTop("申请书文件名已经存在！");
                            return;
                        case -2:
                            Alert.ShowInTop("申请书文件不能大于150M");
                            return;
                    }
                    if (attachid != -3)
                    {
                        pa.Attachment_Application = attachid;
                    }
                    else
                    {
                        pa.Attachment_Application = null;
                    }

                    patent.Insert(pa);
                    operate.LoginName = username;
                    operate.OperationTime = DateTime.Now;
                    operate.LoginIP = " ";
                    operate.OperationContent = "Patent";
                    operate.OperationType = "添加";
                    operate.OperationDataID = pa.PatentID;
                    op.Insert(operate);
                    PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("您的数据已提交，请等待确认！"));
                }
            }
            catch (Exception ex)
            {
                //lby ↓
                if(pa.Attachment_Patent != null && pa.Attachment_Application != null)
                {
                    int[] attachid = { pa.Attachment_Patent.Value, pa.Attachment_Application.Value };
                    string path_Patent = at.FindPath(attachid[0]);
                    string path_Application = at.FindPath(attachid[1]);
                    pm.DeleteFile(attachid[0], path_Patent);
                    pm.DeleteFile(attachid[1], path_Application);
                }
                pm.SaveError(ex, this.Request);
            }
        }
     
        //重置
        protected void DeleteAll_Click(object sender, EventArgs e)
        {
            try
            {
                //PatentMember.Reset();
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
                //ApplyNum.Reset();
                
                //lby ↓
                tPatentAuthorization.Reset();
                tPatentCertificate.Reset();

                PageContext.RegisterStartupScript("clearFile();");
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //成果名称验证
        protected void tAchievement_TextChanged(object sender, EventArgs e)
        {
            if (tAchievement.Text.Trim() != "")
            {
                if (ach.IsExitAchieveName(tAchievement.Text.Trim()) == null)
                {
                    Alert.Show("该成果名不存在！");
                    tAchievement.Text = "";
                    return;
                }
                else
                {
                    if (ach.IsExitAchieveName(tAchievement.Text.Trim()).IsPass == false)
                    {
                        Alert.Show("该成果名称正在审核中！");
                        tAchievement.Text = "";
                        return;
                    }
                }
            }
        }

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
    }
}