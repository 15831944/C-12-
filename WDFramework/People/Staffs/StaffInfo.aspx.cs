/**编写人：王会会
 * 时间：2014年8月12号
 * 功能：人员基本信息，增删改查
 * 修改履历：8月8号修改权限，删除修改只能是本人录入的
 *           2015年3月4日，添加搜索项
 *           2.修改人：陈起明
 *             修改时间：10月10日
 *             修改内容：撤消了静态变量page
 *             3.修改人：马睿杰
 *             修改时间：九月二十五日
 *             修改内容：增加员工类型字段
 **/  
using BLHelper;
using Common.Entities;
using FineUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WDFramework.People.Staffs
{
    public partial class StaffInfo : System.Web.UI.Page
    {
        BLHelper.BLLOperationLog bllOperate = new BLHelper.BLLOperationLog();
        BLHelper.BLLUser bllUser = new BLHelper.BLLUser();
        BLHelper.BLLAgency bllAgency = new BLLAgency();
        BLCommon.PublicMethod publicmethod = new BLCommon.PublicMethod();
        BLHelper.BLLStudent bllStudent = new BLLStudent();
        BLHelper.BLLSocialPartTime bllSocialPartTime = new BLLSocialPartTime();
        BLHelper.BLLSpeakClass bllSpeakClass = new BLLSpeakClass();
        BLHelper.BLLWorkExperience bllWork = new BLLWorkExperience();
        BLHelper.BLLPhotos bllPhotos = new BLLPhotos();
        BLHelper.BLLAttachment bllAttachment = new BLLAttachment();
        BLHelper.BLLEducation bllEducation = new BLLEducation();
        BLHelper.BLLEduExperience bllEduE = new BLLEduExperience();
        BLHelper.BLLHonor bllHonor = new BLLHonor();
        private int page;
        protected void Page_Load(object sender, EventArgs e)
        {
            page = ViewState["page"] == null ? 0 : (int)ViewState["page"];
            btnSelect_All.Text = "全选";
            if (!IsPostBack)
            {
                //添加数据
                btnNew.OnClientClick = WindowADD.GetShowReference("Add_StaffInfos.aspx");               
                BindData();
                btnDelete.Enabled = false;
                TriggerBox.Enabled = false;
            }

        }
        //数据绑定
        public void BindData()
        {
            try
            {
                ViewState["page"] = 0;
                List<UserInfo> UserList = bllUser.FindPaged(Convert.ToInt32(Session["SecrecyLevel"]));
                var result = UserList.Skip(People_Info.PageIndex * People_Info.PageSize).Take(People_Info.PageSize).ToList();
                People_Info.RecordCount = UserList.Count();
                this.People_Info.DataSource = result;
                this.People_Info.DataBind();
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
                Alert.ShowInTop("系统出错，请联系管理员！！");
            }
        }

        //根据学历查看用户信息
        public void FindByEducation()
        {
            try
            {
                ViewState["page"] = 1;
                List<UserInfo> UserInfoList = bllUser.FindByEducation(TriggerBox.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"])).ToList();
                People_Info.RecordCount = UserInfoList.Count();
                var result = UserInfoList.Skip(People_Info.PageIndex * People_Info.PageSize).Take(People_Info.PageSize).ToList();
                this.People_Info.DataSource = result;
                this.People_Info.DataBind();
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
                Alert.ShowInTop("系统出错，请联系管理员！！");
            }

        }

        //根据用户名查看用户信息（模糊查询）
        public void FindByUserName()
        {
            try
            {
                ViewState["page"] = 2;
                List<UserInfo> UserInfoList = bllUser.FindByName(TriggerBox.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"])).ToList();
                People_Info.RecordCount = UserInfoList.Count();
                var result = UserInfoList.Skip(People_Info.PageIndex * People_Info.PageSize).Take(People_Info.PageSize).ToList();
                this.People_Info.DataSource = result;
                this.People_Info.DataBind();
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
                Alert.ShowInTop("系统出错，请联系管理员！！");
            }
        }

        //根据入学时间查询
        protected void FindByEnterSchoolTime()
        {
            int year = 0;
            try
            {
                ViewState["page"] = 3;
                year = Convert.ToInt32(TriggerBox.Text.Trim());
                List<UserInfo> UserInfoList = bllUser.FindByEnterSchoolTime(year, Convert.ToInt32(Session["SecrecyLevel"])).ToList();
                People_Info.RecordCount = UserInfoList.Count();
                var result = UserInfoList.Skip(People_Info.PageIndex * People_Info.PageSize).Take(People_Info.PageSize).ToList();
                this.People_Info.DataSource = result;
                this.People_Info.DataBind();
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                if(year == 0)
                {
                    Alert.ShowInTop("请输入正确的年份！");
                    TriggerBox.Text = "";
                    return;
                }
                publicmethod.SaveError(ex, this.Request);
                Alert.ShowInTop("系统出错，请联系管理员！！");
            }
        }

        //根据政治面貌查询人员信息
        public void FindByPolitical()
        {
            try
            {
                ViewState["page"] = 4;
                List<UserInfo> UserInfoList = bllUser.FindByPolitical(TriggerBox.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"])).ToList();
                People_Info.RecordCount = UserInfoList.Count();
                var result = UserInfoList.Skip(People_Info.PageIndex * People_Info.PageSize).Take(People_Info.PageSize).ToList();
                this.People_Info.DataSource = result;
                this.People_Info.DataBind();
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
                Alert.ShowInTop("系统出错，请联系管理员！！");
            }
        }

        //根据行政级别查询人员信息
        public void FindByAdministrativeLevel()
        {
            try
            {
                ViewState["page"] = 5;
                List<UserInfo> UserInfoList = bllUser.FindByAdministrativeLevel(TriggerBox.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"])).ToList();
                People_Info.RecordCount = UserInfoList.Count();
                var result = UserInfoList.Skip(People_Info.PageIndex * People_Info.PageSize).Take(People_Info.PageSize).ToList();
                this.People_Info.DataSource = result;
                this.People_Info.DataBind();
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
                Alert.ShowInTop("系统出错，请联系管理员！！");
            }
        }

        //根据部门查询人员信息
        public void FindByAgency()
        {
            try
            {
                ViewState["page"] = 6;
                List<UserInfo> UserInfoList = bllUser.FindByAgency(TriggerBox.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"])).ToList();
                People_Info.RecordCount = UserInfoList.Count();
                var result = UserInfoList.Skip(People_Info.PageIndex * People_Info.PageSize).Take(People_Info.PageSize).ToList();
                this.People_Info.DataSource = result;
                this.People_Info.DataBind();
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
                Alert.ShowInTop("系统出错，请联系管理员！！");
            }
        }
        //根据员工类型
        private void FindByStaffType()
        {
            try
            {
                ViewState["page"] = 11;
                List<UserInfo> UserInfoList = bllUser.FindByStaffType(dCondition.SelectedText.Trim(), Convert.ToInt32(Session["SecrecyLevel"])).ToList();
                People_Info.RecordCount = UserInfoList.Count();
                var result = UserInfoList.Skip(People_Info.PageIndex * People_Info.PageSize).Take(People_Info.PageSize).ToList();
                this.People_Info.DataSource = result;
                this.People_Info.DataBind();
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
                Alert.ShowInTop("系统出错，请联系管理员！！");
            }
        }
        //根据学位查找人员信息
        public void FindByDegree()
        {
            try
            {
                ViewState["page"] = 7;
                List<UserInfo> UserInfoList = bllUser.FindByDegree(TriggerBox.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"])).ToList();
                People_Info.RecordCount = UserInfoList.Count();
                var result = UserInfoList.Skip(People_Info.PageIndex * People_Info.PageSize).Take(People_Info.PageSize).ToList();
                this.People_Info.DataSource = result;
                this.People_Info.DataBind();
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
                Alert.ShowInTop("系统出错，请联系管理员！！");
            }

        }

        //根据研究方向查找人员信息
        public void FindByResearchDirection()
        {
            try
            {
                ViewState["page"] = 8;
                List<UserInfo> UserInfoList = bllUser.FindByResearchDirection(TriggerBox.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"])).ToList();
                People_Info.RecordCount = UserInfoList.Count();
                var result = UserInfoList.Skip(People_Info.PageIndex * People_Info.PageSize).Take(People_Info.PageSize).ToList();
                this.People_Info.DataSource = result;
                this.People_Info.DataBind();
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
                Alert.ShowInTop("系统出错，请联系管理员！！");
            }

        }

        //根据最后毕业学校查找人员信息
        public void FindByLastSchool()
        {
            try
            {
                ViewState["page"] = 9;
                List<UserInfo> UserInfoList = bllUser.FindByLastSchool(TriggerBox.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"])).ToList();
                People_Info.RecordCount = UserInfoList.Count();
                var result = UserInfoList.Skip(People_Info.PageIndex * People_Info.PageSize).Take(People_Info.PageSize).ToList();
                this.People_Info.DataSource = result;
                this.People_Info.DataBind();
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
                Alert.ShowInTop("系统出错，请联系管理员！！");
            }

        }

        //根据职称查找人员信息
        public void FindByJobTitle()
        {
            try
            {
                ViewState["page"] = 10;
                List<UserInfo> UserInfoList = bllUser.FindByJobTitle(TriggerBox.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"])).ToList();
                People_Info.RecordCount = UserInfoList.Count();
                var result = UserInfoList.Skip(People_Info.PageIndex * People_Info.PageSize).Take(People_Info.PageSize).ToList();
                this.People_Info.DataSource = result;
                this.People_Info.DataBind();
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
                Alert.ShowInTop("系统出错，请联系管理员！！");
            }

        }

        //分页每页项的个数
        protected void ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            People_Info.PageIndex = 0;
            this.People_Info.PageSize = Convert.ToInt32(ddlGridPageSize.SelectedValue);
            switch (page)
            {
                case 0:
                    BindData();
                    break;
                case 1:
                    FindByEducation();
                    break;
                case 2:
                    FindByUserName();
                    break;
                case 3:
                    FindByEnterSchoolTime();
                    break;
                case 4:
                    FindByPolitical();
                    break;
                case 5:
                    FindByAdministrativeLevel();
                    break;
                case 6:
                    FindByAgency();
                    break;
                case 7:
                    FindByDegree();
                    break;
                case 8:
                    FindByResearchDirection();
                    break;
                case 9:
                    FindByLastSchool();
                    break;
                case 10:
                    FindByJobTitle();
                    break;
                case 11:
                    FindByStaffType();
                    break;
            }
        }

        //分页页数
        protected void People_Info_PageIndexChange(object sender, FineUI.GridPageEventArgs e)
        {
            People_Info.PageIndex = e.NewPageIndex;
            switch (page)
            {
                case 0:
                    BindData();
                    break;
                case 1:
                    FindByEducation();
                    break;
                case 2:
                    FindByUserName();
                    break;
                case 3:
                    FindByEnterSchoolTime();
                    break;
                case 4:
                    FindByPolitical();
                    break;
                case 5:
                    FindByAdministrativeLevel();
                    break;
                case 6:
                    FindByAgency();
                    break;
                case 7:
                    FindByDegree();
                    break;
                case 8:
                    FindByResearchDirection();
                    break;
                case 9:
                    FindByLastSchool();
                    break;
                case 10:
                    FindByJobTitle();
                    break;
                case 11:
                    FindByStaffType();
                    break;
            }
        }

        //判断男女
        public string getgender(string bx)
        {
            try
            {
                return bllUser.getgender(bx);
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
                return "";
            }
        }

        //判断婚姻状况
        public string getMarried(string xb)
        {
            try
            {
                if (xb == "True")
                    return "已婚";
                else if (xb == "False")
                    return "未婚";
                else
                    return "";
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
                return "";
            }
        }

        //判断是否博士生导师
        public string getDoctorTeacher(string xb)
        {
            try
            {
                if (xb == "True")
                    return "是";
                else
                    return "否";
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
                return "";
            }
        }

        //判断是否为硕士生导师
        public string getMasterTeacher(string xb)
        {
            try
            {
                if (xb == "True")
                    return "是";
                else
                    return "否";
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
                return "";
            }
        }

        //删除选择行的点击事件
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                List<int> selections = publicmethod.GridCount(People_Info, CBoxSelect);
                if (selections.Count != 0)
                {
                    for (int i = 0; i < selections.Count(); i++)
                    {
                        string AdminUserName = bllUser.Find(Convert.ToInt32(People_Info.DataKeys[selections[i]][0]), true).UserName;
                        if (AdminUserName == "超级管理员")
                        {
                            Alert.ShowInTop("您没有对“超级管理员”操作的权限，请重新选择！");
                            return;
                        }
                        if (Session["LoginName"].ToString() == bllUser.Find(Convert.ToInt32(People_Info.DataKeys[selections[i]][0]), true).LoginName)
                        {
                            Alert .ShowInTop ("您没有删除本人的权限！");
                            return;                               
                        }
                    }
                }
                if (Convert.ToInt32(Session["SecrecyLevel"]) == 5)
                {
                    for (int i = 0; i < selections.Count(); i++)
                    {
                        //删除人员相关学生信息
                        List<int> Studentlist = bllStudent.FindStudentIDList(Convert.ToInt32(People_Info.DataKeys[selections[i]][0]));
                        for (int j = 0; j < Studentlist.Count(); j++)
                        {
                            bllStudent.Delete(Convert.ToInt32(Studentlist[j]));
                        }
                        //删除人员相关社会兼职信息
                        List<int> SocialPartTime = bllSocialPartTime.FindSocialPartTimeIDList(Convert.ToInt32(People_Info.DataKeys[selections[i]][0]));
                        for (int j = 0; j < SocialPartTime.Count(); j++)
                        {
                            bllSocialPartTime.Delete(SocialPartTime[j]);
                        }
                        //删除人员相关主讲课程信息
                        List<int> SpeakClass = bllSpeakClass.FindSpeakClassIDList(Convert.ToInt32(People_Info.DataKeys[selections[i]][0]));
                        for (int j = 0; j < SpeakClass.Count(); j++)
                        {
                            bllSpeakClass.Delete(SpeakClass[j]);
                        }
                        //删除人员相关工作经历信息
                        List<int> Work = bllWork.FindWorkExperienceIDList(Convert.ToInt32(People_Info.DataKeys[selections[i]][0]));
                        for (int j = 0; j < Work.Count(); j++)
                        {
                            bllWork.Delete(Work[j]);
                        }
                        //删除人员相关照片信息
                        int PhotoID = bllPhotos.FindPhotoID(Convert.ToInt32(People_Info.DataKeys[selections[i]][0]));
                        if (PhotoID != 0)
                        {
                            int AttachmentID = bllPhotos.FindAttachmentID(PhotoID);
                            string path = bllAttachment.FindPath(AttachmentID);
                            publicmethod.DeleteFile(AttachmentID, path);
                            bllPhotos.Delete(PhotoID);
                        }
                        //删除人员相关学历信息
                        List<int> Education = bllEducation.FindEducationIDList(Convert.ToInt32(People_Info.DataKeys[selections[i]][0]));
                        for (int j = 0; j < Education.Count(); j++)
                        {
                            bllEducation.Delete(Education[j]);
                        }
                        //删除人员相关教育经历信息
                        List<int> EduE = bllEduE.FindEduExperienceIDList(Convert.ToInt32(People_Info.DataKeys[selections[i]][0]));
                        for (int j = 0; j < EduE.Count(); j++)
                        {
                            bllEduE.Delete(EduE[j]);
                        }
                        //删除人员相关荣誉称号信息
                        List<int> Honor = bllHonor.FindHonorIDList(Convert.ToInt32(People_Info.DataKeys[selections[i]][0]));
                        for (int j = 0; j < Honor.Count(); j++)
                        {
                            bllHonor.Delete(Honor[j]);
                        }
                        //删除人员派遣学习
                        BLHelper.BLLDFurtherStudy bllDFutherStudy = new BLLDFurtherStudy();
                        List<int> DFurtherStudy = bllDFutherStudy.FindByUserInfoID(Convert.ToInt32(People_Info.DataKeys[selections[i]][0]));
                        for (int j = 0; j < DFurtherStudy.Count(); j++)
                        {
                            bllDFutherStudy.Delete(DFurtherStudy[j]);
                        }
                        //删除学术会议参加人员
                        BLHelper.BLLAttendMeeting bllAttendMeeting = new BLLAttendMeeting();
                        List<int?> AttendMeetinglist = bllAttendMeeting.FindAcademicMeetingID(Convert.ToInt32(People_Info.DataKeys[selections[i]][0]));
                        for (int j = 0; j < AttendMeetinglist.Count(); j++)
                        {
                            bllAttendMeeting.Delete(Convert.ToInt32(AttendMeetinglist[j]));
                        }
                        //删除借阅记录
                        BLHelper.BLLWorkPlanSummary bllWorkPlan = new BLLWorkPlanSummary();
                        List<int> WorkPlanlist = bllWorkPlan.FindWorkPlanSummaryIDList (Convert.ToInt32(People_Info.DataKeys[selections[i]][0]));
                        for (int j = 0; j < WorkPlanlist.Count(); j++)
                        {
                            bllWorkPlan.Delete(Convert.ToInt32(WorkPlanlist[j]));
                        }
                        BLHelper.BLLLibraryRecord bllLibrary = new BLLLibraryRecord();
                        List<int> LibraryRecordlist = bllLibrary.FindUserInfoIDList(Convert.ToInt32(People_Info.DataKeys[selections[i]][0]));
                        for (int j = 0; j < LibraryRecordlist.Count(); j++)
                        {
                            bllLibrary.Delete(LibraryRecordlist[j]);
                        }
                        //删除工作计划总结
                        //删除人员相关成果获奖信息
                        string UserName = bllUser.Find(Convert.ToInt32(People_Info.DataKeys[selections[i]][0]), true).UserName;
                        BLHelper.BLLAward bllAward = new BLLAward();
                        List<string> Awardlist = bllAward.FindAwardPeopleList();
                        List<int> AwardIDlists = new List<int>();
                        for (int n = 0; n < Awardlist.Count(); n++)
                        {
                            if (UserName == Awardlist[n])
                            {
                                for (int m = 0; m < bllAward.FindAwardIDList(Awardlist[n]).Count(); m++)
                                {
                                    AwardIDlists.Add((bllAward.FindAwardIDList(Awardlist[n]))[m]);
                                }
                            }
                        }
                        for (int k = 0; k < AwardIDlists.Count(); k++)
                        {
                            bllAward.Delete(AwardIDlists[k]);
                        }
                        //删除人员相关成果报奖信息
                        BLHelper.BLLAchieveAward bllAchieveAward = new BLLAchieveAward();
                        List<string> AchieveAwardlist = bllAchieveAward.FindAwardPeopleList();
                        List<int> AchieveAwardID = new List<int>();
                        for (int n = 0; n < AchieveAwardlist.Count(); n++)
                        {
                            if (UserName == AchieveAwardlist[n])
                            {
                                for (int m = 0; m < bllAchieveAward.FindAwardIDList(AchieveAwardlist[n]).Count(); m++)
                                {
                                    AchieveAwardID.Add((bllAchieveAward.FindAwardIDList(AchieveAwardlist[n]))[m]);
                                }
                            }
                        }
                        for (int k = 0; k < AchieveAwardID.Count(); k++)
                        {
                            bllAchieveAward.Delete(AchieveAwardID[k]);
                        }
                        //删除人员相关成果鉴定信息①②
                        BLHelper.BLLAchievement bllAchivement = new BLLAchievement();
                        BLHelper.BLLStaffAchieve bllStaffAchieve = new BLLStaffAchieve();
                        List<int> AchivementIDlist = bllStaffAchieve.FindByUserID(Convert.ToInt32(People_Info.DataKeys[selections[i]][0]));
                        List<int> StaffAchievelist = bllStaffAchieve.FindByStaffAchiveID(Convert.ToInt32(People_Info.DataKeys[selections[i]][0]));
                        //删除只有该人员一人为完成人的人员成果表信息 
                        for (int n = 0; n < StaffAchievelist.Count(); n++)
                        {
                            bllStaffAchieve.Delete(StaffAchievelist[n]);
                        }
                        //删除成果报奖
                        List<int> AchieveAwardIDlist = new List<int>();
                        for (int n = 0; n < AchivementIDlist.Count(); n++)
                        {
                            if (bllAchieveAward.FindByAchievement(AchivementIDlist[n]).Count() != 0)
                            {
                                for (int j = 0; j < bllAchieveAward.FindByAchievement(AchivementIDlist[n]).Count(); j++)
                                {
                                    AchieveAwardIDlist.Add(bllAchieveAward.FindByAchievement(AchivementIDlist[n])[j]);
                                }
                            }
                        }
                        for (int n = 0; n < AchieveAwardIDlist.Count(); n++)
                        {
                            bllAchieveAward.Delete(AchieveAwardIDlist[n]);
                        }
                        //删除成果应用
                        BLHelper.BLLAchievementApply bllAchieveApply = new BLLAchievementApply();
                        List<int> AchieveApplylist = new List<int>();
                        for (int n = 0; n < AchivementIDlist.Count(); n++)
                        {
                            if (bllAchieveApply.FindByAchievement(AchivementIDlist[n]).Count() != 0)
                            {
                                for (int j = 0; j < bllAchieveApply.FindByAchievement(AchivementIDlist[n]).Count(); j++)
                                {
                                    AchieveApplylist.Add(bllAchieveApply.FindByAchievement(AchivementIDlist[n])[j]);
                                }
                            }
                        }
                        for (int n = 0; n < AchieveApplylist.Count(); n++)
                        {
                            bllAchieveApply.Delete(AchieveApplylist[n]);
                        }
                        //删除成果验收
                        BLHelper.BLLAchievementCA bllAchieveCA = new BLLAchievementCA();
                        List<int> AchieveCAlist = new List<int>();
                        for (int n = 0; n < AchivementIDlist.Count(); n++)
                        {
                            if (bllAchieveCA.FindByAchievement(AchivementIDlist[n]).Count() != 0)
                            {
                                for (int j = 0; j < bllAchieveCA.FindByAchievement(AchivementIDlist[n]).Count(); j++)
                                {
                                    AchieveCAlist.Add(bllAchieveCA.FindByAchievement(AchivementIDlist[n])[j]);
                                }
                            }
                        }
                        for (int n = 0; n < AchieveCAlist.Count(); n++)
                        {
                            bllAchieveCA.Delete(AchieveCAlist[n]);
                        }
                        //删除只有该人员一人为完成人的成果鉴定
                        for (int j = 0; j < AchivementIDlist.Count(); j++)
                        {
                            bllAchivement.Delete(AchivementIDlist[j]);
                        }
                        //删除人员相关专利信息
                        BLHelper.BLLPatent bllPatent = new BLLPatent();
                        List<string> Patentlist = bllPatent.FindPatentPeopleList();
                        List<int> PatentID = new List<int>();
                        for (int n = 0; n < Patentlist.Count(); n++)
                        {
                            if (UserName == Patentlist[n])
                            {
                                for (int m = 0; m < bllPatent.FindPatentIDList(Patentlist[n]).Count(); m++)
                                {
                                    PatentID.Add((bllPatent.FindPatentIDList(Patentlist[n]))[m]);
                                }
                            }
                        }
                        for (int k = 0; k < PatentID.Count(); k++)
                        {
                            bllPatent.Delete(PatentID[k]);
                        }
                        //删除人员相关专著信息
                        BLHelper.BLLMonograph bllMonograph = new BLLMonograph();
                        List<string> Monographlist = bllMonograph.FindMonographPeopleList();
                        List<int> MonographID = new List<int>();
                        for (int n = 0; n < Monographlist.Count(); n++)
                        {
                            if (UserName == Monographlist[n])
                            {
                                for (int m = 0; m < bllMonograph.FindMonographIDList(Monographlist[n]).Count(); m++)
                                {
                                    MonographID.Add((bllMonograph.FindMonographIDList(Monographlist[n]))[m]);
                                }
                            }
                        }
                        for (int k = 0; k < MonographID.Count(); k++)
                        {
                            bllMonograph.Delete(MonographID[k]);
                        }
                        //删除人员相关论文信息
                        BLHelper.BLLPaper bllPaper = new BLLPaper();
                        List<string> Paperlist = bllPaper.FindPaperPeopleList();
                        List<int> PaperID = new List<int>();
                        for (int n = 0; n < Paperlist.Count(); n++)
                        {
                            if (UserName == Paperlist[n])
                            {
                                for (int m = 0; m < bllPaper.FindPaperIDList(Paperlist[n]).Count(); m++)
                                {
                                    PaperID.Add((bllPaper.FindPaperIDList(Paperlist[n]))[m]);
                                }
                            }
                        }
                        for (int k = 0; k < PaperID.Count(); k++)
                        {
                            bllPaper.Delete(PaperID[k]);
                        }
                        //删除人员
                        bllUser.Delete(Convert.ToInt32(People_Info.DataKeys[selections[i]][0]));
                    }
                    Alert.ShowInTop("删除成功!");
                    btnSelect_All.Text = "全选";
                    BindData();
                }
                else
                {
                    for (int i = 0; i < publicmethod.GridCount(People_Info, CBoxSelect).Count(); i++)
                    {
                        bllUser.ChangePass(Convert.ToInt32(People_Info.DataKeys[selections[i]][0]), false);
                        //向操作日志表中插入
                        OperationLog operate = new OperationLog();
                        operate.LoginName = bllUser.FindByLoginName(Session["LoginName"].ToString()).UserName;
                        operate.LoginIP = "";
                        operate.OperationType = "删除";
                        operate.OperationContent = "UserInfo";
                        operate.OperationDataID = Convert.ToInt32(People_Info.DataKeys[selections[i]][0]);
                        operate.OperationTime = System.DateTime.Now;
                        operate.Remark = "";
                        bllOperate.Insert(operate);                       
                    }
                    Alert.ShowInTop("您的数据已提交，请等待确认！");
                    BindData();
                    btnSelect_All.Text = "全选";

                }
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }

        //People_Info行命令
        protected void People_Info_RowCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                string Person = People_Info.Rows[e.RowIndex].Values[2].ToString();
                string strs = bllUser.FindByLoginName(Session["LoginName"].ToString()).UserName;
                if (Person == "admin")
                {
                    Alert.ShowInTop("超级管理员信息，禁止修改！");
                    CBoxSelect.SetCheckedState(e.RowIndex, false);
                }
                if (Person != strs && Convert.ToInt32(Session["SecrecyLevel"]) != 5)
                {
                    string str = "您无对此行操作的权限！此行信息为" + Person + "录入，请与管理员联系!";
                    CBoxSelect.SetCheckedState(e.RowIndex, false);
                    Alert.ShowInTop(str);
                    return;
                }
                if (publicmethod.GridCount(People_Info, CBoxSelect).Count == 0)
                {
                    //Alert.ShowInTop("请选中需删除的数据！");
                    btnDelete.Enabled = false;
                    return;
                }
                if (publicmethod.GridCount(People_Info, CBoxSelect).Count != 0)
                {
                    btnDelete.Enabled = true;
                    return;
                }
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }

        //根据机构ID找机构名称
        public string AgencyName(int AgencyID)
        {
            try
            {
                if (AgencyID != 0)
                    return bllAgency.FindAgenName(AgencyID);
                else
                    return "";
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
                return "";
            }
        }       

        //查询
        protected void FindObjectAll_Click(object sender, EventArgs e)
        {
          
            People_Info.PageIndex = 0;
            if (!string.IsNullOrEmpty(TriggerBox.Text.Trim()) && dCondition.SelectedText.Trim()=="")
            {
                if (TriggerBox.Text.Trim().Length <= 20)
                {
                    switch (ddlsearch.SelectedItem.Text)
                    {
                        case "用户姓名":
                            FindByUserName();                           
                            break;
                        case "学历":
                            FindByEducation();
                            break;
                            
                        case "入校时间":
                            FindByEnterSchoolTime();
                            break;
                        case "政治面貌":
                            FindByPolitical();
                            break;
                        case "行政级别":
                            FindByAgency();
                            break;
                        case "所属机构":
                            FindByEducation();
                            break;
                        case "学位":
                            FindByDegree();
                            break;
                        case "研究方向":
                            FindByResearchDirection();
                            break;
                        case "最后毕业学校":
                            FindByLastSchool();
                            break;
                        case "职称":
                            FindByJobTitle();
                            break;
                        case"员工类型":
                            FindByStaffType();
                            break;

                    }
                }
                else
                    Alert.ShowInTop("最多输入20个字符！");

            }
            else
            {
                if (ddlsearch.SelectedItem.Text == "全部")
                {
                    BindData();
                }
                else
                {
                    if(ddlsearch.SelectedItem.Text == "员工类型")
                        FindByStaffType();
                    else
                    { Alert.ShowInTop("请填写查询条件！"); }
                }
                    
            }
        }

        //刷新
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            BindData();
            TriggerBox.Reset();
            ddlsearch.SelectedValue = "全部";
            btnDelete.Enabled = false;
            TriggerBox.Enabled = false;
        }
        //修改
        protected void ButtonUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                List<int> selections = publicmethod.GridCount(People_Info, CBoxSelect);
                if (selections.Count() != 0)
                {
                    if (selections.Count() == 1)
                    {
                        int rowID = Convert.ToInt32(People_Info.DataKeys[selections[0]][0]);
                        Session["UserInfoID"] = rowID;

                        Alert.Show("你确定要修改该行数据吗!", "确认消息", MessageBoxIcon.Information, WindowUpdate.GetShowReference("Update_StaffInfos.aspx"), Target.Top);
                    }
                    else
                    {
                        Alert.Show("一次仅可以对一行进行编辑！");
                    }
                }
                else
                {
                    Alert.Show("请选择一行！");
                }
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
        //备注界面跳转
        protected string GetEditUrl(object UserInfoID)
        {
            return Remark.GetShowReference("Remark_Window.aspx?id=" + UserInfoID, "备注");
        }
        //个人简介界面跳转
        protected string GetEditUrlP(object UserInfoID)
        {
            return Remark.GetShowReference("Profile_Window.aspx?id=" + UserInfoID, "个人简介");
        }
        //照片界面跳转
        protected string GetPhotoUrl(object UserInfoID)
        {
            return Remark.GetShowReference("Down_Photos.aspx?id=" + UserInfoID, "人员照片");
        }
        //等级名称
        public string SecrecyLevelName(int level)
        {
            try
            {
                string[] SecrecyLevels = new string[] { "四级", "三级", "二级", "一级", "管理员" };
                //string[] SecrecyLevels = new string[] { "公开", "内部", "秘密", "机密", "管理员" };
                return SecrecyLevels[level - 1];
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
                return "";
            }
        }
        //导出
        protected void btn_Get_Click(object sender, EventArgs e)
        {
            try
            {
                switch (page)
                {
                    case 0:
                        List<UserInfo> UserList = bllUser.FindPaged(Convert.ToInt32(Session["SecrecyLevel"]));
                        this.People_Info.DataSource = UserList;
                        this.People_Info.DataBind();
                        break;
                    case 1:
                        List<UserInfo> UserInfoList = bllUser.FindByLoginName(TriggerBox.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"])).ToList();
                        this.People_Info.DataSource = UserInfoList;
                        this.People_Info.DataBind();
                        break;
                    case 2:
                        List<UserInfo> UserInfoLists = bllUser.FindByName(TriggerBox.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"])).ToList();
                        this.People_Info.DataSource = UserInfoLists;
                        this.People_Info.DataBind();
                        break;
                }
                publicmethod.ExportExcel(3, People_Info, 2);
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
        //搜索框变化
        protected void ddlsearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlsearch.SelectedItem.Text == "全部")
            {
                TriggerBox.Text = "";
                TriggerBox.Enabled = false;
                dCondition.SelectedIndex = 0;
                dCondition.Enabled = false;
            }
            else if(ddlsearch.SelectedItem.Text=="员工类型")
            {
                TriggerBox.Text = "";
                dCondition.Enabled = true;
                dCondition.SelectedIndex = 0;
                TriggerBox.Enabled = false;
            }
            else
            {
                TriggerBox.Enabled = true;
                dCondition.SelectedIndex = 0;
                dCondition.Enabled = false;
            }
            if(ddlsearch.SelectedItem.Text == "员工类型")
                dCondition.Items.Clear();
            string[] Stafftype = new string[] { "兼职人员", "专职人员" };
            for (int i = 0; i <= 1; i++)
            {
                dCondition.Items.Add(Stafftype[i].ToString(), Stafftype[i].ToString());
            }
                 
                 
        }
        public int RowNumber(int dataItemIndex)
        {
            return dataItemIndex + (People_Info.PageIndex) * People_Info.PageSize;
        }


        //全选按钮
        protected void btnSelect_All_Click(object sender, EventArgs e)
        {
            People_Info.SelectAllRows();
            int[] select = People_Info.SelectedRowIndexArray;
            int m;
            //取整数（不是四舍五入，全舍）
            int Pages = (int)Math.Floor(Convert.ToDouble(People_Info.RecordCount / this.People_Info.PageSize));

            if (People_Info.PageIndex == Pages)
                m = (People_Info.RecordCount - this.People_Info.PageSize * People_Info.PageIndex);
            else
                m = this.People_Info.PageSize;
            bool isCheck = false;
            for (int i = 0; i < m; i++)
            {
                if (CBoxSelect.GetCheckedState(i) == false)
                    isCheck = true;
            }
            if (isCheck)
            {
                foreach (int item in select)
                {
                    CBoxSelect.SetCheckedState(item, true);
                }
                btnDelete.Enabled = true;
                btnSelect_All.Text = "取消全选";
            }
            else
            {
                foreach (int item in select)
                {
                    CBoxSelect.SetCheckedState(item, false);
                }
                btnDelete.Enabled = false;
                btnSelect_All.Text = "全选";
            }
        }
        //清空数据库中表数据
        protected void btn_delete_all_Click(object sender, EventArgs e)
        {
            try
            {
                List<int> selections = bllUser.search(Session["LoginName"].ToString());
                //if (selections.Count != 0)
                //{
                //    for (int i = 0; i < selections.Count(); i++)
                //    {
                //        string AdminUserName = bllUser.Find(selections[i], true).UserName;
                //        if (AdminUserName == "超级管理员")
                //        {
                //            Alert.ShowInTop("您没有对“超级管理员”操作的权限，请重新选择！");
                //            selections.Remove(i);
                //        }
                //        if (Session["LoginName"].ToString() == bllUser.Find(selections[i], true).LoginName)
                //        {
                //            Alert.ShowInTop("您没有删除本人的权限！");
                //            selections.Remove(i);
                //        }
                //    }
                //}
                if (Convert.ToInt32(Session["SecrecyLevel"]) == 5)
                {
                    for (int i = 0; i < selections.Count(); i++)
                    {
                        string AdminUserName = bllUser.Find(selections[i], true).UserName;
                        if (AdminUserName == "超级管理员")
                        {
                            Alert.ShowInTop("您没有对“超级管理员”操作的权限，请重新选择！");
                            continue;

                        }
                        if (Session["LoginName"].ToString() == bllUser.Find(selections[i], true).LoginName)
                        {
                            Alert.ShowInTop("您没有删除本人的权限！");
                            continue;
                        }
                        //删除人员相关学生信息
                        List<int> Studentlist = bllStudent.FindStudentIDList(selections[i]);
                        for (int j = 0; j < Studentlist.Count(); j++)
                        {
                            bllStudent.Delete(Convert.ToInt32(Studentlist[j]));
                        }
                        //删除人员相关社会兼职信息
                        List<int> SocialPartTime = bllSocialPartTime.FindSocialPartTimeIDList(selections[i]);
                        for (int j = 0; j < SocialPartTime.Count(); j++)
                        {
                            bllSocialPartTime.Delete(SocialPartTime[j]);
                        }
                        //删除人员相关主讲课程信息
                        List<int> SpeakClass = bllSpeakClass.FindSpeakClassIDList(selections[i]);
                        for (int j = 0; j < SpeakClass.Count(); j++)
                        {
                            bllSpeakClass.Delete(SpeakClass[j]);
                        }
                        //删除人员相关工作经历信息
                        List<int> Work = bllWork.FindWorkExperienceIDList(selections[i]);
                        for (int j = 0; j < Work.Count(); j++)
                        {
                            bllWork.Delete(Work[j]);
                        }
                        //删除人员相关照片信息
                        int PhotoID = bllPhotos.FindPhotoID(selections[i]);
                        if (PhotoID != 0)
                        {
                            int AttachmentID = bllPhotos.FindAttachmentID(PhotoID);
                            string path = bllAttachment.FindPath(AttachmentID);
                            publicmethod.DeleteFile(AttachmentID, path);
                            bllPhotos.Delete(PhotoID);
                        }
                        //删除人员相关学历信息
                        List<int> Education = bllEducation.FindEducationIDList(selections[i]);
                        for (int j = 0; j < Education.Count(); j++)
                        {
                            bllEducation.Delete(Education[j]);
                        }
                        //删除人员相关教育经历信息
                        List<int> EduE = bllEduE.FindEduExperienceIDList(selections[i]);
                        for (int j = 0; j < EduE.Count(); j++)
                        {
                            bllEduE.Delete(EduE[j]);
                        }
                        //删除人员相关荣誉称号信息
                        List<int> Honor = bllHonor.FindHonorIDList(selections[i]);
                        for (int j = 0; j < Honor.Count(); j++)
                        {
                            bllHonor.Delete(Honor[j]);
                        }
                        //删除人员派遣学习
                        BLHelper.BLLDFurtherStudy bllDFutherStudy = new BLLDFurtherStudy();
                        List<int> DFurtherStudy = bllDFutherStudy.FindByUserInfoID(selections[i]);
                        for (int j = 0; j < DFurtherStudy.Count(); j++)
                        {
                            bllDFutherStudy.Delete(DFurtherStudy[j]);
                        }
                        //删除学术会议参加人员
                        BLHelper.BLLAttendMeeting bllAttendMeeting = new BLLAttendMeeting();
                        List<int?> AttendMeetinglist = bllAttendMeeting.FindAcademicMeetingID(selections[i]);
                        for (int j = 0; j < AttendMeetinglist.Count(); j++)
                        {
                            bllAttendMeeting.Delete(Convert.ToInt32(AttendMeetinglist[j]));
                        }
                        //删除借阅记录
                        BLHelper.BLLWorkPlanSummary bllWorkPlan = new BLLWorkPlanSummary();
                        List<int> WorkPlanlist = bllWorkPlan.FindWorkPlanSummaryIDList(selections[i]);
                        for (int j = 0; j < WorkPlanlist.Count(); j++)
                        {
                            bllWorkPlan.Delete(Convert.ToInt32(WorkPlanlist[j]));
                        }
                        BLHelper.BLLLibraryRecord bllLibrary = new BLLLibraryRecord();
                        List<int> LibraryRecordlist = bllLibrary.FindUserInfoIDList(selections[i]);
                        for (int j = 0; j < LibraryRecordlist.Count(); j++)
                        {
                            bllLibrary.Delete(LibraryRecordlist[j]);
                        }
                        //删除工作计划总结
                        //删除人员相关成果获奖信息
                        string UserName = bllUser.Find(selections[i], true).UserName;
                        BLHelper.BLLAward bllAward = new BLLAward();
                        List<string> Awardlist = bllAward.FindAwardPeopleList();
                        List<int> AwardIDlists = new List<int>();
                        for (int n = 0; n < Awardlist.Count(); n++)
                        {
                            if (UserName == Awardlist[n])
                            {
                                for (int m = 0; m < bllAward.FindAwardIDList(Awardlist[n]).Count(); m++)
                                {
                                    AwardIDlists.Add((bllAward.FindAwardIDList(Awardlist[n]))[m]);
                                }
                            }
                        }
                        for (int k = 0; k < AwardIDlists.Count(); k++)
                        {
                            bllAward.Delete(AwardIDlists[k]);
                        }
                        //删除人员相关成果报奖信息
                        BLHelper.BLLAchieveAward bllAchieveAward = new BLLAchieveAward();
                        List<string> AchieveAwardlist = bllAchieveAward.FindAwardPeopleList();
                        List<int> AchieveAwardID = new List<int>();
                        for (int n = 0; n < AchieveAwardlist.Count(); n++)
                        {
                            if (UserName == AchieveAwardlist[n])
                            {
                                for (int m = 0; m < bllAchieveAward.FindAwardIDList(AchieveAwardlist[n]).Count(); m++)
                                {
                                    AchieveAwardID.Add((bllAchieveAward.FindAwardIDList(AchieveAwardlist[n]))[m]);
                                }
                            }
                        }
                        for (int k = 0; k < AchieveAwardID.Count(); k++)
                        {
                            bllAchieveAward.Delete(AchieveAwardID[k]);
                        }
                        //删除人员相关成果鉴定信息①②
                        BLHelper.BLLAchievement bllAchivement = new BLLAchievement();
                        BLHelper.BLLStaffAchieve bllStaffAchieve = new BLLStaffAchieve();
                        List<int> AchivementIDlist = bllStaffAchieve.FindByUserID(selections[i]);
                        List<int> StaffAchievelist = bllStaffAchieve.FindByStaffAchiveID(selections[i]);
                        //删除只有该人员一人为完成人的人员成果表信息 
                        for (int n = 0; n < StaffAchievelist.Count(); n++)
                        {
                            bllStaffAchieve.Delete(StaffAchievelist[n]);
                        }
                        //删除成果报奖
                        List<int> AchieveAwardIDlist = new List<int>();
                        for (int n = 0; n < AchivementIDlist.Count(); n++)
                        {
                            if (bllAchieveAward.FindByAchievement(AchivementIDlist[n]).Count() != 0)
                            {
                                for (int j = 0; j < bllAchieveAward.FindByAchievement(AchivementIDlist[n]).Count(); j++)
                                {
                                    AchieveAwardIDlist.Add(bllAchieveAward.FindByAchievement(AchivementIDlist[n])[j]);
                                }
                            }
                        }
                        for (int n = 0; n < AchieveAwardIDlist.Count(); n++)
                        {
                            bllAchieveAward.Delete(AchieveAwardIDlist[n]);
                        }
                        //删除成果应用
                        BLHelper.BLLAchievementApply bllAchieveApply = new BLLAchievementApply();
                        List<int> AchieveApplylist = new List<int>();
                        for (int n = 0; n < AchivementIDlist.Count(); n++)
                        {
                            if (bllAchieveApply.FindByAchievement(AchivementIDlist[n]).Count() != 0)
                            {
                                for (int j = 0; j < bllAchieveApply.FindByAchievement(AchivementIDlist[n]).Count(); j++)
                                {
                                    AchieveApplylist.Add(bllAchieveApply.FindByAchievement(AchivementIDlist[n])[j]);
                                }
                            }
                        }
                        for (int n = 0; n < AchieveApplylist.Count(); n++)
                        {
                            bllAchieveApply.Delete(AchieveApplylist[n]);
                        }
                        //删除成果验收
                        BLHelper.BLLAchievementCA bllAchieveCA = new BLLAchievementCA();
                        List<int> AchieveCAlist = new List<int>();
                        for (int n = 0; n < AchivementIDlist.Count(); n++)
                        {
                            if (bllAchieveCA.FindByAchievement(AchivementIDlist[n]).Count() != 0)
                            {
                                for (int j = 0; j < bllAchieveCA.FindByAchievement(AchivementIDlist[n]).Count(); j++)
                                {
                                    AchieveCAlist.Add(bllAchieveCA.FindByAchievement(AchivementIDlist[n])[j]);
                                }
                            }
                        }
                        for (int n = 0; n < AchieveCAlist.Count(); n++)
                        {
                            bllAchieveCA.Delete(AchieveCAlist[n]);
                        }
                        //删除只有该人员一人为完成人的成果鉴定
                        for (int j = 0; j < AchivementIDlist.Count(); j++)
                        {
                            bllAchivement.Delete(AchivementIDlist[j]);
                        }
                        //删除人员相关专利信息
                        BLHelper.BLLPatent bllPatent = new BLLPatent();
                        List<string> Patentlist = bllPatent.FindPatentPeopleList();
                        List<int> PatentID = new List<int>();
                        for (int n = 0; n < Patentlist.Count(); n++)
                        {
                            if (UserName == Patentlist[n])
                            {
                                for (int m = 0; m < bllPatent.FindPatentIDList(Patentlist[n]).Count(); m++)
                                {
                                    PatentID.Add((bllPatent.FindPatentIDList(Patentlist[n]))[m]);
                                }
                            }
                        }
                        for (int k = 0; k < PatentID.Count(); k++)
                        {
                            bllPatent.Delete(PatentID[k]);
                        }
                        //删除人员相关专著信息
                        BLHelper.BLLMonograph bllMonograph = new BLLMonograph();
                        List<string> Monographlist = bllMonograph.FindMonographPeopleList();
                        List<int> MonographID = new List<int>();
                        for (int n = 0; n < Monographlist.Count(); n++)
                        {
                            if (UserName == Monographlist[n])
                            {
                                for (int m = 0; m < bllMonograph.FindMonographIDList(Monographlist[n]).Count(); m++)
                                {
                                    MonographID.Add((bllMonograph.FindMonographIDList(Monographlist[n]))[m]);
                                }
                            }
                        }
                        for (int k = 0; k < MonographID.Count(); k++)
                        {
                            bllMonograph.Delete(MonographID[k]);
                        }
                        //删除人员相关论文信息
                        BLHelper.BLLPaper bllPaper = new BLLPaper();
                        List<string> Paperlist = bllPaper.FindPaperPeopleList();
                        List<int> PaperID = new List<int>();
                        for (int n = 0; n < Paperlist.Count(); n++)
                        {
                            if (UserName == Paperlist[n])
                            {
                                for (int m = 0; m < bllPaper.FindPaperIDList(Paperlist[n]).Count(); m++)
                                {
                                    PaperID.Add((bllPaper.FindPaperIDList(Paperlist[n]))[m]);
                                }
                            }
                        }
                        for (int k = 0; k < PaperID.Count(); k++)
                        {
                            bllPaper.Delete(PaperID[k]);
                        }
                        //删除人员
                        bllUser.Delete(selections[i]);
                    }
                    Alert.ShowInTop("删除成功!");
                    btnSelect_All.Text = "全选";
                    BindData();
                }
                else
                {
                    for (int i = 0; i < publicmethod.GridCount(People_Info, CBoxSelect).Count(); i++)
                    {
                        bllUser.ChangePass(Convert.ToInt32(People_Info.DataKeys[selections[i]][0]), false);
                        //向操作日志表中插入
                        OperationLog operate = new OperationLog();
                        operate.LoginName = bllUser.FindByLoginName(Session["LoginName"].ToString()).UserName;
                        operate.LoginIP = "";
                        operate.OperationType = "删除";
                        operate.OperationContent = "UserInfo";
                        operate.OperationDataID = Convert.ToInt32(People_Info.DataKeys[selections[i]][0]);
                        operate.OperationTime = System.DateTime.Now;
                        operate.Remark = "";
                        bllOperate.Insert(operate);
                    }
                    Alert.ShowInTop("您的数据已提交，请等待确认！");
                    BindData();
                    btnSelect_All.Text = "全选";

                }
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
    }
}