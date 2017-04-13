/**编写人：方淑云
 * 时间：2014年8月16号
 * 功能:成果获奖添加界面后台
 * 修改履历：
 **/
using FineUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common.Entities;

namespace WebApplication1
{
    public partial class Add_Adward : System.Web.UI.Page
    {
        BLHelper.BLLAchievement ach = new BLHelper.BLLAchievement();
        Common.Entities.Award aw = new Common.Entities.Award();
        BLHelper.BLLAttachment at = new BLHelper.BLLAttachment();
        BLHelper.BLLAward award = new BLHelper.BLLAward();
        BLHelper.BLLBasicCode ba = new BLHelper.BLLBasicCode();
        BLHelper.BLLUser user = new BLHelper.BLLUser();
        BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
        BLHelper.BLLOperationLog op = new BLHelper.BLLOperationLog();
        OperationLog operate = new OperationLog();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dAwardTime.MaxDate = DateTime.Now;
                InitSecrecyLevel();
                InitAwardwSpecies();
                InitPatentGrade();
                InitAwardForm();
            }
        }
        //初始化获奖等级
        public void InitPatentGrade()
        {
            try
            {
                List<BasicCode> listname = ba.FindByCategoryName("获奖等级");
                for (int i = 0; i < listname.Count(); i++)
                {
                    dGrade.Items.Add(listname[i].CategoryContent.ToString(), listname[i].CategoryContent.ToString());
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //初始化获奖类别
        public void InitAwardwSpecies()
        {
            try
            {
                List<BasicCode> listname = ba.FindByCategoryName("获奖类别");
                for (int i = 0; i < listname.Count(); i++)
                {
                    dAwardwSpecies.Items.Add(listname[i].CategoryContent.ToString(), listname[i].CategoryContent.ToString());
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //初始化获奖类型
        public void InitAwardForm()
        {
            try
            {
                List<BasicCode> listname = ba.FindByCategoryName("获奖类型");
                for (int i = 0; i < listname.Count(); i++)
                {
                    dAwardForm.Items.Add(listname[i].CategoryContent.ToString(), listname[i].CategoryContent.ToString());
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //初始化等级下拉框
        public void InitSecrecyLevel()
        {
            for (int i = 0; i < Convert.ToInt32(Session["SecrecyLevel"]); i++)
            {
                //string[] SecrecyLevels = new string[] { "公开", "内部", "秘密", "机密", "管理员" };
                string[] SecrecyLevels = new string[] { "四级", "三级", "二级", "一级", "管理员" };
                dSecrecyLevel.Items.Add(SecrecyLevels[i], i.ToString());
            }
        }
     
        //插入赋值
        public void InsertValue()
        {
            if (tachievement.Text.Trim() != "")
            {
                aw.Acheivement = tachievement.Text.Trim();
            }
            else
            {
                aw.Acheivement = null;
            }
            aw.AwardName = tAwardName.Text.Trim();
            aw.AwardTime = dAwardTime.SelectedDate;
            aw.FirstAward = FirstAward.Text.Trim();
            aw.AwardNum = AwardNum.Text.Trim();
            aw.AwardForm = dAwardForm.SelectedText;
            aw.AwardwSpecies = dAwardwSpecies.SelectedText;
            aw.Grade = dGrade.SelectedText;
            aw.Sort = DropDownList_Sort.SelectedText;
            aw.Member = Members.Text.Trim();
            if (tRemark.Text.Trim() != "")
            {
                aw.Remark = tRemark.Text.Trim();
            }
            else
            {
                aw.Remark = null;
            }
            aw.SecrecyLevel =Convert.ToInt32 (dSecrecyLevel.SelectedIndex + 1);
            aw.GivAgency = tGivenAgency.Text.Trim();
            //aw.AwardPeople = AwardPeople.Text.Trim();   
        }
       
      
        //保存
        protected void Save_Click(object sender, EventArgs e)
        {
            try
            {
     
                if (tAwardName.Text.Trim() == "")
                {
                    Alert.Show("获奖名称不能为空！");
                    return;
                }
                if (tGivenAgency.Text.Trim() == "")
                {
                    Alert.Show("授予机构不能为空！");
                    return;
                }
                //if (AwardPeople.Text.Trim() == "")
                //{
                //    Alert.Show("获奖人不能为空！");
                //    return;
                //}
                string username = user.FindByLoginName(Session["LoginName"].ToString()).UserName;
                aw.EntryPerson = username;
                List<string> list = new List<string>();
                string unit = "";
                list.Add(Unit1.Text.ToString());
                list.Add(Unit2.Text.ToString());
                list.Add(Unit3.Text.ToString());
                list.Add(Unit4.Text.ToString());
                list.Add(Unit5.Text.ToString());
                List<string> newlist = new List<string>();
                List<string> newlist1 = new List<string>();
                for (int i = 0; i < 5; i++)
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
                    aw.Unit = unit;
                }
                else
                {
                    aw.Unit = null;
                }
                if (Convert.ToInt32(Session["SecrecyLevel"]) == 5)
                {
                    InsertValue();
                    int attachid = pm.UpLoadFile(fileupload).Attachid;
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
                    if (attachid != -3)
                    {
                        aw.AttachmentID = attachid;
                    }
                    else
                    {
                        aw.AttachmentID = null;
                    }
                    aw.IsPass = true;
                    award.Insert(aw);
                    PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("保存成功！"));
                }
                else
                {
                    InsertValue();
                    int attachid = pm.UpLoadFile(fileupload).Attachid;
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
                    if (attachid != -3)
                    {
                        aw.AttachmentID = attachid;
                    }
                    else
                    {
                        aw.AttachmentID = null;
                    }
                    aw.IsPass = false;
                    award.Insert(aw);
                    operate.LoginName = username;
                    operate.OperationTime = DateTime.Now;
                    operate.LoginIP = " ";
                    operate.OperationContent = "Award";
                    operate.OperationType = "添加";
                    operate.OperationDataID = aw.AwardID;
                    op.Insert(operate);
                    PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("您的数据已提交，请等待确认！"));
                }
            }
            catch (Exception ex)
            {
                int attachid = Convert.ToInt32(aw.AttachmentID);
                string path = at.FindPath(attachid);
                pm.DeleteFile(attachid, path);
                pm.SaveError(ex, this.Request);
            }
        }
        //重置
        protected void DeleteAll_Click(object sender, EventArgs e)
        {
            try
            {
                //AwardPeople.Reset();
                //tAchievement.Reset();
                tachievement.Reset();
                tAwardName.Reset();
                dAwardTime.Reset();
                dAwardwSpecies.Reset();
                FirstAward.Reset();
                AwardNum.Reset();
                dAwardForm.Reset();
                dGrade.Reset();
                tRemark.Reset();
                dSecrecyLevel.Reset();
                tGivenAgency.Reset();
                Unit1.Reset();
                Unit2.Reset();
                Unit3.Reset();
                Unit4.Reset();
                Unit5.Reset();
                DropDownList_Sort.Reset();
                Members.Reset();
                PageContext.RegisterStartupScript("clearFile();");
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //获奖名称验证
        protected void tAwardName_TextChanged(object sender, EventArgs e)
        {
            if (tAwardName.Text.Trim() != "")
            {
                if (award.IsExitAwardName(tAwardName.Text.Trim()) != null)
                {
                    if (award.IsExitAwardName(tAwardName.Text.Trim()).IsPass == false)
                    {
                        Alert.Show("该获奖名称正在审核中！");
                        tAwardName.Text = "";
                        return;
                    }
                    else
                    {
                        Alert.Show("该获奖名称已存在！");
                        tAwardName.Text = "";
                        return;
                    }
                }
             
            }
        }
        //成果名称验证
        protected void tachievement_TextChanged(object sender, EventArgs e)
        {
            if (tachievement.Text.Trim() != "")
            {
                string achievement = tachievement.Text.Replace("，", ",");
                string[] str = achievement.Split(',');
                foreach (string ss in str)
                {
                   
                        if (ach.IsExitAchieveName(ss) == null)
                        {
                            Alert.Show("拥有不存在的成果名称，请仔细检查！");
                            tachievement.Text = "";
                            return;
                        }
                        else
                        {
                            if (ach.IsExitAchieveName(ss).IsPass == false)
                            {
                                Alert.Show("拥有正在审核中的成果名称，请仔细检查！");
                                tachievement.Text = "";
                                return;
                            }
                        }                 
                }
            }
        }
    }
}