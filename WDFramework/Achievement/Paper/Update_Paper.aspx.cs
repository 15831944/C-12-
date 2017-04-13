/**编写人：方淑云
 * 时间：2014年8月12号
 * 功能:论文更新界面后台
 * 修改履历： 1、修改人：吕博扬
 *              修改时间：2015年9月23日
 *              修改内容：取消所有输入项的数据校验
 *           2、修改人：吕博杨
 *              修改时间：2015年11月30日
 *              修改内容：新增上传文档功能
 **/
using Common.Entities;
using FineUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1.Achievement.Paper
{

    public partial class Update_Paper : System.Web.UI.Page
    {
        BLHelper.BLLPaper papers = new BLHelper.BLLPaper();
        BLHelper.BLLAchievement ach = new BLHelper.BLLAchievement();
        BLHelper.BLLOperationLog op = new BLHelper.BLLOperationLog();
        OperationLog log = new OperationLog();
        BLHelper.BLLUser user = new BLHelper.BLLUser();
        BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
        BLHelper.BLLBasicCode ba = new BLHelper.BLLBasicCode();
        Common.Entities.Paper paper = new Common.Entities.Paper();
        BLHelper.BLLBasicCode bllBasicCode = new BLHelper.BLLBasicCode();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dPublicDate.MaxDate = DateTime.Now;
                InitdRetrieveSituation();
                InitDropListAgency();
                InitDroplistForm();
                InitData();
               
            }
        }
        //初始化刊物级别
        public void InitDroplistForm()
        {
            try
            {
                List<BasicCode> list = bllBasicCode.FindALLName("刊物级别");
                for (int i = 0; i < list.Count(); i++)
                {
                    dPaperRank.Items.Add(list[i].CategoryContent.ToString(), list[i].CategoryContent.ToString());
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
        //初始化收录情况
        public void InitdRetrieveSituation()
        {
            try
            {
                List<BasicCode> listname = ba.FindByCategoryName("收录情况");
                for (int i = 0; i < listname.Count(); i++)
                {
                    dRetrieveSituation.Items.Add(listname[i].CategoryContent.ToString(), listname[i].CategoryContent.ToString());
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //初始化界面
        public void InitData()
        {
            try
            {
                Common.Entities.Paper paper = papers.FindAll(Convert.ToInt32(Session["PaperID"]));
                tEndPageNum.Text = paper.EndPageNum.ToString();
                tImpactFactor.Text = paper.ImpactFactor;
                if (paper.JournalNum != "")
                {
                    tJournalNum.Text = paper.JournalNum;
                }
                else
                {
                    tJournalNum.Text = "";
                }
                dPaperIdentity.SelectedValue = paper.WriterIdentity;
                dPublicDate.SelectedDate = paper.PublicDate;
                dPaperRank.SelectedValue = paper.PaperRank;
                FirstWriter.Text = paper.FirstWriter;
                MessageWriter.Text = paper.MessageWriter;
                MWAgency.Text = paper.MWAgency;
                DropDownListAgency.SelectedValue = paper.PaperUnit;
                DropDownList_Sort.SelectedValue = paper.Sort;
                if (paper.PublicJournalName != "")
                {
                    tPublicJournalName.Text = paper.PublicJournalName;
                }
                else
                {
                    tPublicJournalName.Text = "";
                }
                tQuoteNum.Text = paper.QuoteNum.ToString();
                if (paper.Remark != "")
                {
                    tRemark.Text = paper.Remark;
                }
                else
                {
                    tRemark.Text = "";
                }

                dRetrieveSituation.SelectedValue = paper.RetrieveSituation;
                if (paper.SerialNum != "")
                {
                    tSerialNum.Text = paper.SerialNum;
                }
                else
                {
                    tSerialNum.Text = "";
                }
                tStartPageNum.Text = paper.StartPageNum.ToString();
                tSubject.Text = paper.Subject;
                tVolumesNum.Text = paper.VolumesNum;
                int id = papers.FindByPaperID(Convert.ToInt32(Session["PaperID"]));
                if (ach.FindAchieveName(id) != null && ach.FindAchieveName(id) != "")
                {
                    tAchievement.Text = ach.FindAchieveName(id);
                }
                else
                {
                    tAchievement.Text = "";
                }
                PaperPeople.Text = paper.PaperPeople;
                tIncludeNum.Text = paper.IncludeNum;
                dSecrecyLevel.SelectedValue = "公开";
                tHQuoteNum.Text = paper.HQuoteNum.ToString();
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //论文题目验证
        protected void tSubject_TextChanged(object sender, EventArgs e)
        {
            if (tSubject.Text.Trim() != "")
            {
               
                    if (papers.IsExitName(tSubject.Text.Trim()) != null)
                    {
                        if (papers.IsExitName(tSubject.Text.Trim()).IsPass == false)
                        {
                            Alert.Show("该论文题目已在审核中！");
                            tSubject.Text = "";
                            return;
                        }
                        else
                        {
                            Alert.Show("该论文题目已存在！");
                            tSubject.Text = "";
                            return;
                        }
                    }
                
            }

        }
    
        //更新赋值
        public void UpdateValue()
        {           
            paper.EndPageNum = Convert.ToInt32(tEndPageNum.Text.Trim());       
            paper.ImpactFactor = tImpactFactor.Text.Trim();
            paper.JournalNum = tJournalNum.Text.Trim();
            //paper.PaperForm = dPaperForm.Text.Trim();
            paper.WriterIdentity = dPaperIdentity.SelectedText.Trim();
            paper.PaperRank = dPaperRank.SelectedText.Trim();
            paper.PaperUnit = DropDownListAgency.SelectedText;
            paper.PublicDate = dPublicDate.SelectedDate;
            paper.PublicJournalName = tPublicJournalName.Text.Trim();
            paper.QuoteNum = Convert.ToInt32(tQuoteNum.Text.Trim());       
            paper.Remark = tRemark.Text.Trim();
            paper.FirstWriter = FirstWriter.Text.Trim();
            paper.MessageWriter = MessageWriter.Text.Trim();
            paper.MWAgency = MWAgency.Text.Trim();
            paper.RetrieveSituation = dRetrieveSituation.SelectedText;
            paper.SerialNum = tSerialNum.Text.Trim();         
            paper.StartPageNum = Convert.ToInt32(tStartPageNum.Text.Trim());        
            paper.Subject = tSubject.Text.Trim();
            paper.VolumesNum = tVolumesNum.Text.Trim();
            paper.Sort = DropDownList_Sort.SelectedText;
            int id = ach.FindByAchievementName(tAchievement.Text.Trim());
            if (id != 0)
            {
                paper.AchievementID = id;
            }
            else
            {
                paper.AchievementID = null;
            }
            paper.IncludeNum = tIncludeNum.Text.Trim();
            paper.SecrecyLevel = 1;  
            paper.HQuoteNum = Convert.ToInt32(tHQuoteNum.Text.Trim());
            paper.PaperPeople = PaperPeople.Text.Trim();
            //paper.EntryPerson = Session["LoginName"].ToString();
            paper.EntryPerson = papers.FindAll(Convert.ToInt32(Session["PaperID"])).EntryPerson;
        }
        //保存更新
        protected void AddWriter_Click(object sender, EventArgs e)
        {
            try 
            {
                //if (tSubject.Text.Trim() == "")
                //{
                //    Alert.Show("题目不能为空！");
                //    return;
                //}
                //if (tPublicJournalName.Text.Trim() == "")
                //{
                //    Alert.Show("发表刊物不能为空！");
                //    return;
                //}
                //if (PaperPeople.Text.Trim() == "")
                //{
                //    Alert.Show("全部作者不能为空！");
                //    return;
                //}
                //if (FirstWriter.Text.Trim() == "")
                //{
                //    Alert.Show("第一作者不能为空！");
                //    return;
                //}
                if (Convert.ToInt32(Session["SecrecyLevel"]) == 5)
                {
                    UpdateValue();
                    paper.PaperID = Convert.ToInt32(Session["PaperID"]);
                    paper.IsPass = true;
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
                        paper.AttachmentID = attachid;
                    }
                    else
                    {
                        if (papers.FindAttachment(Convert.ToInt32(Session["PaperID"])) != 0)
                            paper.AttachmentID = null;
                    }
                    papers.Update(paper);
                    PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("保存成功"));
                }
                else
                {
                    UpdateValue();
                    paper.IsPass = false;
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
                        paper.AttachmentID = attachid;
                    }
                    else
                    {
                        paper.AttachmentID = null;
                    }
                    papers.Insert(paper);
                    string username = user.FindByLoginName(Session["LoginName"].ToString()).UserName;
                    log.LoginName = username;
                    log.OperationTime = DateTime.Now;
                    log.LoginIP = " ";
                    log.OperationContent = "Paper";
                    log.OperationType = "更新";
                    log.OperationDataID = Convert.ToInt32(Session["PaperID"]);
                    log.Remark = (Convert.ToInt32(papers.FindByPaperNames(paper.Subject, Convert.ToInt32(Session["PaperID"])))).ToString();
                    op.Insert(log);
                    papers.UpdateIsPass(Convert.ToInt32(Session["PaperID"]), false);
                    PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("您的数据已提交，请等待确认!"));
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //他引次数验证
        protected void tHQuoteNum_TextChanged(object sender, EventArgs e)
        {
            Regex RegNumber = new Regex("^[0-9]+$");           
            if (tHQuoteNum.Text.Trim() != "" && tQuoteNum.Text.Trim() != "" )
            {
                if (!RegNumber.Match(tHQuoteNum.Text.Trim()).Success)
                {
                    tHQuoteNum.Text = "";
                    if (!RegNumber.Match(tQuoteNum.Text.Trim()).Success)
                    {
                        Alert.Show("只能输入数字！");
                        tQuoteNum.Text = "";
                        return;
                    }
                    Alert.Show("只能输入数字！");
                    return;
                }
                else
                {
                    if (!RegNumber.Match(tQuoteNum.Text.Trim()).Success)
                    {
                        Alert.Show("只能输入数字！");
                        tQuoteNum.Text = "";
                        return;
                    }
                }
              
                if (Convert.ToInt32(tHQuoteNum.Text.Trim()) > Convert.ToInt32(tQuoteNum.Text.Trim()))
                {
                    Alert.Show("他引次数不能大于引用次数！");
                    tHQuoteNum.Text = "";
                }
            }
            
        }
        //结束页码验证
        protected void tEndPageNum_TextChanged(object sender, EventArgs e)
        {

            Regex RegNumber = new Regex("^[0-9]+$");
            if (tEndPageNum.Text.Trim() != "" && tStartPageNum.Text.Trim() != "")
            {

                if (!RegNumber.Match(tStartPageNum.Text.Trim()).Success)
                {
                    tStartPageNum.Text = "";
                    if (!RegNumber.Match(tEndPageNum.Text.Trim()).Success)
                    {
                        Alert.Show("只能输入数字！");
                        tEndPageNum.Text = "";
                        return;
                    }
                    Alert.Show("只能输入数字！");
                    return;
                }
                else
                {
                    if (!RegNumber.Match(tEndPageNum.Text.Trim()).Success)
                    {
                        Alert.Show("只能输入数字！");
                        tEndPageNum.Text = "";
                        return;
                    }
                }
                if (Convert.ToInt32(tStartPageNum.Text.Trim()) > Convert.ToInt32(tEndPageNum.Text.Trim()))
                {
                    Alert.Show("结束页码不能大于起始页码！");
                    tEndPageNum.Text = "";
                }
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
        //重置
        protected void DeleteAll_Click(object sender, EventArgs e)
        {
            try
            {
                PaperPeople.Reset();
                tEndPageNum.Reset();
                tImpactFactor.Reset();
                tJournalNum.Reset();
                //dPaperForm.Reset();
                dPaperRank.Reset();
                dPaperIdentity.Reset();
                DropDownListAgency.Reset();
                dPublicDate.Reset();
                tPublicJournalName.Reset();
                tQuoteNum.Reset();
                tRemark.Reset();
                dRetrieveSituation.Reset();
                tSerialNum.Reset();
                tStartPageNum.Reset();
                tSubject.Reset();
                tVolumesNum.Reset();
                tAchievement.Reset();
                tIncludeNum.Reset();
                dSecrecyLevel.Reset();
                tHQuoteNum.Reset();
                FirstWriter.Reset();
                MessageWriter.Reset();
                MWAgency.Reset();
                DropDownList_Sort.Reset();
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }

    }
}