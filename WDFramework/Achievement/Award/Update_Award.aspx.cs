/**编写人：方淑云
 * 时间：2014年8月16号
 * 功能:成果获奖更新界面后台
 * 修改履历：    修改人：吕博扬
 *              修改时间：2015年9月23日
 *              修改内容：取消所有输入项的数据校验
 **/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common.Entities;
using FineUI;

namespace WDFramework.Achievement.Award
{
    public partial class Update_Award : System.Web.UI.Page
    {
        BLHelper.BLLAchievement ach = new BLHelper.BLLAchievement();
        Common.Entities.Award aww = new Common.Entities.Award();
        BLHelper.BLLAward award = new BLHelper.BLLAward();
        BLHelper.BLLBasicCode ba = new BLHelper.BLLBasicCode();
        BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
        BLHelper.BLLOperationLog op = new BLHelper.BLLOperationLog();
        OperationLog log = new OperationLog();
        BLHelper.BLLUser user = new BLHelper.BLLUser();
        BLHelper.BLLAttachment at = new BLHelper.BLLAttachment();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dAwardTime.MaxDate = DateTime.Now;
                InitSecrecyLevel();
                InitPatentGrade();
                InitAwardwSpecies();
                InitAwardForm();
                InitData();

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
        //初始化等级下拉框
        public void InitSecrecyLevel()
        {
            for (int i = 0; i < Convert.ToInt32(Session["SecrecyLevel"]); i++)
            {
                //string[] SecrecyLevels = new string[] { "公开", "内部", "秘密", "机密", "管理员" };
                string[] SecrecyLevels = new string[] { "四级", "三级", "二级", "一级", "管理员" };
                dSecrecyLevel.Items.Add(SecrecyLevels[i], SecrecyLevels[i]);
            }
        }
        //初始化界面
        public void InitData()
        {
            try
            {
                List<Common.Entities.Award> listw = award.FindAll(Convert.ToInt32(Session["AwardID"]));
                Common.Entities.Award aw = listw.FirstOrDefault();
                if (aw.Acheivement != null)
                {
                    tachievement.Text = aw.Acheivement;
                }
                else
                {
                    tachievement.Text = "";
                }
                dAwardForm.SelectedValue = aw.AwardForm;
                FirstAward.Text = aw.FirstAward;
                AwardNum.Text = aw.AwardNum;
                tAwardName.Text = aw.AwardName;
                dAwardTime.SelectedDate = aw.AwardTime;
                dAwardwSpecies.SelectedValue = aw.AwardwSpecies;
                dGrade.SelectedValue = aw.Grade;
                //AwardPeople.Text = aw.AwardPeople;
                DropDownList_Sort.SelectedValue = aw.Sort;
                Members.Text = aw.Member;
                if (aw.Remark != null)
                {
                    tRemark.Text = aw.Remark;
                }
                else
                {
                    tRemark.Text = "";
                }
                dSecrecyLevel.SelectedIndex = Convert.ToInt32(aw.SecrecyLevel - 1);
                List<string> res = new List<string>();
                if (aw.Unit != null)
                {
                    string[] str = aw.Unit.Split(',');
                    foreach (string ss in str)
                    {
                        res.Add(ss);
                    }
                    switch (res.Count)
                    {
                        case 1:
                            Unit1.Text = res[0];
                            break;
                        case 2:
                            Unit1.Text = res[0];
                            Unit2.Text = res[1];
                            break;
                        case 3:
                            Unit1.Text = res[0];
                            Unit2.Text = res[1];
                            Unit3.Text = res[2];
                            break;
                        case 4:
                            Unit1.Text = res[0];
                            Unit2.Text = res[1];
                            Unit3.Text = res[2];
                            Unit4.Text = res[3];
                            break;
                        case 5:
                            Unit1.Text = res[0];
                            Unit2.Text = res[1];
                            Unit3.Text = res[2];
                            Unit4.Text = res[3];
                            Unit5.Text = res[4];
                            break;
                    }
                }
                else
                {
                    Unit1.Text = "";
                    Unit2.Text = "";
                    Unit3.Text = "";
                    Unit4.Text = "";
                    Unit5.Text = "";
                }
                tGivenAgency.Text = aw.GivAgency;
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
       
        //更新赋值
        public void UpdateValue()
        {
            if (tachievement.Text.Trim() != "")
            {
                aww.Acheivement = tachievement.Text.Trim();
            }
            else
            {
                aww.Acheivement = null;
            }
            aww.AwardName = tAwardName.Text.Trim();
            aww.AwardTime = dAwardTime.SelectedDate;
            aww.FirstAward = FirstAward.Text.Trim();
            aww.AwardNum = AwardNum.Text.Trim();
            aww.AwardForm = dAwardForm.SelectedText;
           // aww.EntryPerson = Session["LoginName"].ToString();
            aww.EntryPerson = award.Findmodel(Convert.ToInt32(Session["AwardID"])).EntryPerson;
            aww.AwardwSpecies = dAwardwSpecies.SelectedText;
            aww.Grade = dGrade.SelectedText;
            aww.Remark = tRemark.Text.Trim();
            aww.SecrecyLevel = Convert.ToInt32(dSecrecyLevel.SelectedIndex + 1);
            aww.GivAgency = tGivenAgency.Text.Trim();
            //aww.AwardPeople = AwardPeople.Text.Trim();
            aww.Sort = DropDownList_Sort.SelectedText;
            aww.Member = Members.Text.Trim();
        }
      
        //保存
        protected void Save_Click(object sender, EventArgs e)
        {
            try
            {
                //if (tAwardName.Text.Trim() == "")
                //{
                //    Alert.Show("获奖名称不能为空！");
                //    return;
                //}
                //if (tGivenAgency.Text.Trim() == "")
                //{
                //    Alert.Show("授予机构不能为空！");
                //    return;
                //}
                //if (AwardPeople.Text.Trim() == "")
                //{
                //    Alert.Show("获奖人不能为空！");
                //    return;
                //}
                //if (Member.Text.Trim() == "")
                //{
                //    Alert.Show("成员不能为空！");
                //    return;
                //}
                BLHelper.BLLAgency agency = new BLHelper.BLLAgency();
                int AttachmentID = award.FindAttachmentID(Convert.ToInt32(Session["AwardID"]));
                string path = at.FindPath(AttachmentID);
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
                    aww.Unit = unit;
                }
                else
                {
                    aww.Unit = null;
                }
                UpdateValue();
                if (Convert.ToInt32(Session["SecrecyLevel"]) == 5)//如果等于5级
                {
                    aww.IsPass = true;
                    aww.AwardID = Convert.ToInt32(Session["AwardID"]);
                    int attachid = pm.UpLoadFile(fileupload).Attachid;
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
                        aww.AttachmentID = attachid;//附件为新插入的附件ID
                        award.Update(aww);//更新
                        pm.DeleteFile(AttachmentID, path);
                        PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("更新成功！"));
                    }
                    else //上传空间没有值
                    {
                        if (AttachmentID != 0)
                        {
                            aww.AttachmentID = AttachmentID;
                        }
                        award.Update(aww); ;//更新成果表
                        PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("更新成功！"));
                    }
                }
                else//小于5级
                {
                    string username = user.FindByLoginName(Session["LoginName"].ToString()).UserName;
                    log.LoginName = username;
                    log.OperationTime = DateTime.Now;
                    log.LoginIP = " ";
                    log.OperationContent = "Award";
                    log.OperationType = "更新";
                    aww.IsPass = false;
                    int attachid = pm.UpLoadFile(fileupload).Attachid;
                    if (attachid != -3)//有值
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
                        aww.AttachmentID = attachid;//附件为新插入的附件ID
                        award.Insert(aww);//插入
                        log.OperationDataID = Convert.ToInt32(Session["AwardID"]);
                        log.Remark = aww.AwardID.ToString();
                        op.Insert(log);//将成果更新插入操作表                    
                        award.UpdateIsPass(Convert.ToInt32(Session["AwardID"]), false);
                        PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("数据已缓存，正在等待审核！"));

                    }
                    else//上传控件没有值
                    {
                        if (AttachmentID != 0)//原来有附件
                        {
                            aww.AttachmentID = AttachmentID;
                        }
                        award.Insert(aww);
                        log.OperationDataID = Convert.ToInt32(Session["AwardID"]);
                        log.Remark = aww.AwardID.ToString();
                        op.Insert(log);
                        award.UpdateIsPass(Convert.ToInt32(Session["AwardID"]), false);
                        PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("数据已缓存，正在等待审核！"));
                    }
                }
            }
            catch (Exception ex)
            {
                int attachid = Convert.ToInt32(aww.AttachmentID);
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
                tachievement.Reset();
                tAwardName.Reset();
                dAwardTime.Reset();
                dAwardwSpecies.Reset();
                dGrade.Reset();
                tRemark.Reset();
                FirstAward.Reset();
                AwardNum.Reset();
                dAwardForm.Reset();
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