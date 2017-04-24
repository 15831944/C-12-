using DataBase;
using FineUI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLCommon;
using System.Threading;
using System.Data.SqlClient;//http://localhost:43224/AcademicMeeting/ImportExcel.aspx.cs
using System.Text;
//using System.Transactions;


namespace WDFramework.AcademicMeeting
{
    public partial class ImportExcel : System.Web.UI.Page
    {
        BLCommon.PublicMethod publicMethod = new BLCommon.PublicMethod();
        BLHelper.BLLAttachment BLLAttachment = new BLHelper.BLLAttachment();
        BLHelper.BLLUser BLLUser = new BLHelper.BLLUser();
        BLHelper.BLLAgency BLLAgency = new BLHelper.BLLAgency();
        BLHelper.BLLAward BLLAward = new BLHelper.BLLAward();
        BLHelper.BLLAchievement BLLAchievement = new BLHelper.BLLAchievement();
        BLHelper.BLLPaper BLLPaper = new BLHelper.BLLPaper();
        BLHelper.BLLPatent BLLPatent = new BLHelper.BLLPatent();
        BLHelper.BLLMonograph BLLMonograph = new BLHelper.BLLMonograph();
        BLHelper.BLLStaffAchieve BLLStaffAchieve = new BLHelper.BLLStaffAchieve();
        BLHelper.BLLProject BLLProject = new BLHelper.BLLProject();
        BLHelper.BLLStaffDevote BLLStaffDevote = new BLHelper.BLLStaffDevote();
        BLHelper.BLLAcademicMeeting BLLAcademicMeeting = new BLHelper.BLLAcademicMeeting();
        BLHelper.BLLAttendMeeting BLLAttendMeeting = new BLHelper.BLLAttendMeeting();
        BLHelper.BLLEquipment BLLEquipment = new BLHelper.BLLEquipment();
        BLHelper.BLLFurniture BLLFurniture = new BLHelper.BLLFurniture();
        BLCommon.Encrypt encrypt = new BLCommon.Encrypt();
        DataBaseContext dbcontext = new DataBaseContext();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        //提交
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int AttachID = 0;
            string strPath = null;
            string strTypeName = null;
            if (Request["name"] != null)
            {
                strTypeName = Request["name"].ToString();
            }

            try
            {
                AttachID = publicMethod.UpLoad(filePath);
                strPath = BLLAttachment.FindPath(AttachID);
                string fileName = filePath.ShortFileName;//文件名+扩展名     
                switch (AttachID)
                {
                    case -1:
                        //Alert.Show("该文件名已存在！");
                        //Alert.Show("Excel名称存在，导入失败！");
                        //return;
                        int attachmentID = BLLAttachment.SelectAttachmentID(fileName);
                        strPath = BLLAttachment.FindPath(attachmentID);
                        //删除文件
                        publicMethod.DeleteFile(attachmentID, strPath);
                        //删除附件表信息
                        BLLAttachment.Delete(attachmentID);
                        //重新赋值
                        AttachID = publicMethod.UpLoad(filePath);
                        strPath = BLLAttachment.FindPath(AttachID);
                        fileName = filePath.ShortFileName;//文件名+扩展名
                        break;
                    case -2:
                        //Alert.Show("上传的文件不能大于5M！");
                        Alert.Show("导入失败，上传的文件不能大于5M！");
                        return;
                }
                string fileExtension = Path.GetExtension(strPath);//获得excel扩展名
                fileName = fileName.Replace(fileExtension, "");       //去掉扩展名
                if (strTypeName != null && strTypeName != fileName)
                {
                    Alert.Show("请选择名为" + strTypeName + "的导入模板!");
                    return;
                }
                string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + strPath + ";" + "Extended Properties='Excel 8.0;HDR=Yes;IMEX=1'";
                //string strConn = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR=Yes;IMEX=1;'", excelFilePath);
                //string strConn = "Provider=Microsoft.Ace.OleDb.12.0;" + "data source=" + Microsoft.SqlServer.Server.MapPath("ExcelFiles/Mydata2007.xlsx") + ";Extended Properties='Excel 12.0; HDR=Yes; IMEX=1'"; 
                OleDbDataAdapter da = new OleDbDataAdapter("SELECT *  FROM [Sheet1$]", strConn);
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                //int IsImport = 0;
                string IsImport = null;
                da.Fill(ds);
                //DataTable dt;
                dt = ds.Tables[0];
                switch (fileName)
                {
                    case "人员基本信息表":
                        IsImport = insertUserInfo(dt);
                        break;
                    case "项目基本信息表":
                        IsImport = insertProject(dt);
                        break;
                    case "项目人员投入表":
                        IsImport = insertStaffDevote(dt);
                        break;
                    case "论文情况表":
                        IsImport = insertPaper(dt);
                        break;
                    case "专利情况表":
                        IsImport = insertPatent(dt);
                        break;
                    case "专著情况表":
                        IsImport = insertMonograph(dt);
                        break;
                    case "人员鉴定成果表":
                        IsImport = insertStaffAchieve(dt);
                        break;
                    case "会议参加人员表":
                        IsImport = insertAttendMeeting(dt);
                        break;
                    case "获奖情况表":
                        IsImport = insertAward(dt);
                        break;
                    case "学术会议表":
                        IsImport = insertAcademicMeeting(dt);
                        break;
                    case "学术报告表":
                        IsImport = insertScienceReport(dt);
                        break;
                    case "学术报告表（独立）":
                        IsImport = insertNewScienceReport(dt);
                        break;
                    case "仪器设备表":
                        IsImport = insertEquipment(dt);
                        break;
                    case "家具表":
                        IsImport = insertFurniture(dt);
                        break;
                    case "学生情况信息表":
                        IsImport = insertStudentInfo(dt);
                        break;
                }
                if (IsImport == null)
                    Alert.Show("导入成功！");
                else
                {
                    //BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
                    //pm.SaveImportError(IsImport);
                    Alert.Show("部分数据未导入，请根据错误日志进行修改！\n" + IsImport);
                    
                }
                //switch (IsImport)
                //{
                //    case 0:
                //        Alert.Show("导入成功！");
                //        break;
                //    case -1:
                //        Alert.Show("excel表中无内容！");
                //        break;
                //    case -2:
                //        Alert.Show("字段错误，导入失败！");
                //        break;
                //    default:
                //        Alert.Show("第" + IsImport + "行错误，导入失败！");
                //        break;
                //}
            }
            catch (Exception ex)
            {
                BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
                pm.SaveError(ex, this.Request);
                Alert.Show("导入失败！");
            }
            finally
            {
                //删除文件
                publicMethod.DeleteFile(AttachID, strPath);
                //删除附件表信息
                BLLAttachment.Delete(AttachID);
            }

        }
        //人员基本信息(-1：导入信息有误，0：导入成功)
        public string insertUserInfo(DataTable dt)
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                int row = 0;
                string Error = null;
                try
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = null;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            Common.Entities.UserInfo userInfo = new Common.Entities.UserInfo();
                            row = i + 1;
                            dr = dt.Rows[i];
                            if (dr["序号"].ToString() != "")
                            {
                                //userInfo.UserInfoBH = dr["校园一卡通号"].ToString();
                                userInfo.LoginName = dr["登录名（校园一卡通号）"].ToString();
                                //userInfo.LoginPWD = dr["用户密码"].ToString();
                                userInfo.LoginPWD = encrypt.MD5(dr["用户密码"].ToString());//加密
                                userInfo.UserName = dr["用户姓名"].ToString();
                                bool IsExit = BLLUser.IsExit(userInfo.UserName, userInfo.LoginName);//存在返回true
                                if (IsExit)
                                {
                                    Error += "第" + row + "行出错，该用户信息已存在！\n";
                                    continue;
                                }
                                if (dr["性别"].ToString() == "男")
                                    userInfo.Sex = true;
                                else
                                    userInfo.Sex = false;
                                userInfo.Nation = dr["民族"].ToString();
                                userInfo.Hometown = dr["籍贯"].ToString();
                                string str = dr["出生年月"].ToString();
                                if (dr["出生年月"].ToString() == "")
                                    userInfo.Birth = null;
                                //userInfo.Birth = Convert.ToDateTime("1/1/1753 12:00:00");//默认时间
                                else
                                    userInfo.Birth = Convert.ToDateTime(dr["出生年月"].ToString());
                                if (dr["婚姻状况"].ToString() == "未婚")
                                    userInfo.Marriage = false;
                                else
                                    userInfo.Marriage = true;
                                userInfo.AgencyID = BLLAgency.SelectAgencyID(dr["所属科研机构"].ToString());//AgencyID为0则不存在该机构
                                userInfo.TeleNum = dr["手机号码"].ToString();
                                userInfo.HomeNum = dr["家庭号码"].ToString();
                                userInfo.OfficeNum = dr["办公电话"].ToString();
                                userInfo.DocumentsType = dr["证件类型"].ToString();
                                userInfo.DocumentsNum = dr["证件号码"].ToString();
                                userInfo.PoliticalStatus = dr["政治面貌"].ToString();
                                if (dr["政治面貌获得时间"].ToString() == "")
                                    userInfo.PoliticalStatusTime = null;
                                //userInfo.PoliticalStatusTime = Convert.ToDateTime("1/1/1753 12:00:00");//默认时间
                                else
                                    userInfo.PoliticalStatusTime = Convert.ToDateTime(dr["政治面貌获得时间"]);
                                userInfo.Education = dr["学历"].ToString();
                                userInfo.Degree = dr["学位"].ToString();
                                userInfo.ResearchDirection = dr["研究方向"].ToString();
                                userInfo.Specialty = dr["专长"].ToString();
                                userInfo.qqNum = dr["qq号码"].ToString();
                                userInfo.Email = dr["电子信箱"].ToString();
                                userInfo.Fax = dr["传真"].ToString();
                                userInfo.HomeAddress = dr["家庭住址"].ToString();
                                userInfo.PostalCode = dr["邮政编码"].ToString();
                                userInfo.UnitName = dr["单位名称"].ToString();
                                //userInfo.StaffType = dr["员工类型"].ToString();
                                userInfo.Domicile = dr["户籍地"].ToString();
                                userInfo.AdministrativeLevelName = dr["行政级别"].ToString();
                                userInfo.SubjectSortName = dr["学科分类"].ToString();
                                userInfo.JobTitle = dr["职称"].ToString();
                                if (dr["职称获得时间"].ToString() == "")
                                    userInfo.JobTitleTime = null;
                                //userInfo.JobTitleTime = Convert.ToDateTime("1/1/1753 12:00:00");//默认时间
                                else
                                    userInfo.JobTitleTime = Convert.ToDateTime(dr["职称获得时间"].ToString());
                                if (dr["是否博士生导师"].ToString() == "是")
                                    userInfo.IsDocdorTeacher = true;
                                else
                                    userInfo.IsDocdorTeacher = false;
                                //userInfo.IsDocdorTeacher = dr["是否博士生导师"].ToString();
                                if (dr["博士生导师取得时间"].ToString() == "")
                                    userInfo.DoctorTeacherTime = null;
                                //userInfo.DoctorTeacherTime = Convert.ToDateTime("1/1/1753 12:00:00");//默认时间
                                else
                                    userInfo.DoctorTeacherTime = Convert.ToDateTime(dr["博士生导师取得时间"].ToString());
                                if (dr["是否为硕士生导师"].ToString() == "是")
                                    userInfo.IsMasteTeacher = true;
                                else
                                    userInfo.IsMasteTeacher = false;
                                //userInfo.IsMasteTeacher = dr["是否为硕士生导师"].ToString();
                                if (dr["硕士生导师取得时间"].ToString() == "")
                                    userInfo.MasterTeacherTime = null;
                                //userInfo.MasterTeacherTime = Convert.ToDateTime("1/1/1753 12:00:00");//默认时间
                                else
                                    userInfo.MasterTeacherTime = Convert.ToDateTime(dr["硕士生导师取得时间"].ToString());
                                //1公开 2内部 3秘密 4机密 5管理员
                                switch (dr["保密级别"].ToString())
                                {
                                    case "四级":
                                        userInfo.SecrecyLevel = 1;
                                        break;
                                    case "三级":
                                        userInfo.SecrecyLevel = 2;
                                        break;
                                    case "二级":
                                        userInfo.SecrecyLevel = 3;
                                        break;
                                    case "一级":
                                        userInfo.SecrecyLevel = 4;
                                        break;
                                    case "管理员":
                                        userInfo.SecrecyLevel = 5;
                                        break;
                                    default:
                                        userInfo.SecrecyLevel = 0;
                                        break;
                                }
                                //userInfo.SecrecyLevel = dr["保密级别"].ToString();
                                userInfo.Remark = dr["备注"].ToString();
                                userInfo.Profile = dr["个人简介"].ToString();
                                userInfo.LastSchool = dr["最后毕业学校"].ToString();
                                if (dr["入校时间"].ToString() == "")
                                    userInfo.EnterSchoolTime = null;
                                else
                                    userInfo.EnterSchoolTime = Convert.ToDateTime(dr["入校时间"].ToString());
                                userInfo.StudySource = dr["学缘"].ToString();
                                //bool IsExit = BLLUser.IsExit(userInfo.UserName, userInfo.LoginName, userInfo.UserInfoBH);
                                if (userInfo.SecrecyLevel != 0)
                                {
                                    userInfo.IsPass = true;
                                    userInfo.EntryPerson = BLLUser.FindByLoginName(Session["LoginName"].ToString()).UserName;
                                }
                                else
                                {
                                    //return row;//返回错误行号
                                    //返回错误信息
                                    Error += "第" + row + "行出错，保密级别不存在！\n";
                                    continue;
                                }
                                context.UserInfoContext.Add(userInfo);
                            }
                            //else
                            //    return row;
                        }
                        context.SaveChanges();
                        return Error;
                    }
                    else
                        return "Excel无数据导入！";
                    //return -1;//Excel无数据导入
                }
                catch (Exception ex)
                {
                    BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
                    pm.SaveError(ex, this.Request);
                    return "Excel 字段错误 导入失败！";
                    //return row;//Excel 字段错误 导入失败
                }
            }
        }
        //项目基本信息表
        public string insertProject(DataTable dt)
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                int row = 0;
                string Error = null;
                try
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = null;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            Common.Entities.Project project = new Common.Entities.Project();
                            row = i + 1;
                            dr = dt.Rows[i];
                            if (dr["序号"].ToString() != "")
                            {
                                project.ProjectName = dr["项目名称"].ToString();
                                if (BLLProject.IsNullProject(project.ProjectName) != null)
                                {
                                    Error += "第" + row + "行出错，该项目名称已存在！\n";
                                    continue;
                                }
                                project.AgencyID = BLLAgency.SelectAgencyID(dr["项目所属机构"].ToString());//AgencyID为0则不存在该机构
                                if (project.AgencyID == 0)
                                {
                                    return "第" + row + "行出错，该项目所属机构不存在！";
                                }
                                project.AcceptUnit = dr["承接单位"].ToString();
                                project.SourceUnit = dr["来源单位"].ToString();
                                project.ProjectManager = dr["项目前三名"].ToString();
                                project.ProjectHeads = dr["实际负责人"].ToString();
                                project.PactNum = dr["合同编号"].ToString();
                                project.TaskNum = dr["课题编号"].ToString();
                                project.ProjectSortName = dr["项目分类"].ToString();
                                if (dr["开始时间"].ToString() == "")
                                    project.StartTime = null;
                                //project.StartTime = Convert.ToDateTime("1/1/1753 12:00:00");//默认时间
                                else
                                    project.StartTime = Convert.ToDateTime(dr["开始时间"].ToString());
                                if (dr["结束时间"].ToString() == "")
                                    project.EndTime = null;
                                //project.EndTime = Convert.ToDateTime("1/1/1753 12:00:00");//默认时间
                                else
                                    project.EndTime = Convert.ToDateTime(dr["结束时间"].ToString());
                                if (dr["预期结束时间"].ToString() == "")
                                    project.ExpectEndTime = null;
                                //project.ExpectEndTime = Convert.ToDateTime("1/1/1753 12:00:00");//默认时间
                                else
                                    project.ExpectEndTime = Convert.ToDateTime(dr["预期结束时间"].ToString());
                                project.ApprovedMoney = dr["项目经费（万元）"].ToString();
                                project.GetMoney = dr["到账金额（万元）"].ToString();
                                project.CooperationForms = dr["合作形式"].ToString();
                                project.ProjectLevel = dr["项目级别"].ToString();
                                project.ExpecteResults = dr["预期成果"].ToString();
                                project.GivenMoneyUnits = dr["来款单位"].ToString();
                                project.ProjectNature = dr["项目性质"].ToString();
                                project.ProjectState = dr["项目状态"].ToString();
                                //1公开 2内部 3秘密 4机密 5管理员
                                switch (dr["保密级别"].ToString())
                                {
                                    case "四级":
                                        project.SecrecyLevel = 1;
                                        break;
                                    case "三级":
                                        project.SecrecyLevel = 2;
                                        break;
                                    case "二级":
                                        project.SecrecyLevel = 3;
                                        break;
                                    case "一级":
                                        project.SecrecyLevel = 4;
                                        break;
                                    case "管理员":
                                        project.SecrecyLevel = 5;
                                        break;
                                    default:
                                        project.SecrecyLevel = 0;
                                        break;
                                }
                                project.ManageMoney = dr["管理费比例"].ToString();
                                project.Remark = dr["备注"].ToString();
                                project.ProjectInNum = dr["项目内部编号"].ToString();
                                if (project.SecrecyLevel != 0)
                                {
                                    project.IsPass = true;
                                    project.EntryPerson = BLLUser.FindByLoginName(Session["LoginName"].ToString()).UserName;
                                    context.ProjectContext.Add(project);
                                }
                                else
                                {
                                    //return row; 
                                    Error += "第" + row + "行出错，保密级别不存在！\n";
                                    continue;
                                }
                            }
                        }
                        context.SaveChanges();
                        return Error;
                    }
                    else
                        return "Excel无数据导入！";
                    //return -1;
                }
                catch (Exception ex)
                {
                    BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
                    pm.SaveError(ex, this.Request);
                    return "Excel 字段错误 导入失败！";
                    //return row;
                }
            }

        }
        //项目人员投入表
        public string insertStaffDevote(DataTable dt)
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                int row = 0;
                string Error = null;
                try
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = null;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            Common.Entities.StaffDevote staffDevote = new Common.Entities.StaffDevote();
                            row = i + 1;
                            dr = dt.Rows[i];
                            if (dr["序号"].ToString() != "")
                            {
                                string userName = dr["姓名"].ToString();
                                staffDevote.UserInfoID = BLLUser.FindID(userName);//UserInfoID为零则不存在该用户
                                if (staffDevote.UserInfoID == 0)
                                {
                                    return "第" + row + "行出错，姓名不存在！";
                                }
                                string ProjectName = dr["项目名称"].ToString();
                                staffDevote.ProjectID = BLLProject.SelectProjectID(ProjectName);//ProjectID为零则不存在该项目
                                if (staffDevote.ProjectID == 0)
                                {
                                    Error += "第" + row + "行出错，项目名称不存在！\n";
                                    continue;
                                }
                                //paper.PublicDate = dr["发表日期"].ToString();
                                if (dr["投入时间"].ToString() == "")
                                    staffDevote.DevoteTime = null;
                                //staffDevote.DevoteTime = Convert.ToDateTime("1/1/1753 12:00:00");//默认时间
                                else
                                    staffDevote.DevoteTime = Convert.ToDateTime(dr["投入时间"].ToString());
                                if (dr["退出时间"].ToString() == "")
                                    staffDevote.ExitTime = null;
                                //staffDevote.ExitTime = Convert.ToDateTime("1/1/1753 12:00:00");//默认时间
                                else
                                    staffDevote.ExitTime = Convert.ToDateTime(dr["退出时间"].ToString());
                                staffDevote.Sort = Convert.ToInt32(dr["排序"].ToString());
                                //1公开 2内部 3秘密 4机密 5管理员
                                switch (dr["保密级别"].ToString())
                                {
                                    case "四级":
                                        staffDevote.SecrecyLevel = 1;
                                        break;
                                    case "三级":
                                        staffDevote.SecrecyLevel = 2;
                                        break;
                                    case "二级":
                                        staffDevote.SecrecyLevel = 3;
                                        break;
                                    case "一级":
                                        staffDevote.SecrecyLevel = 4;
                                        break;
                                    case "管理员":
                                        staffDevote.SecrecyLevel = 5;
                                        break;
                                    default:
                                        staffDevote.SecrecyLevel = 0;
                                        break;

                                }
                                if (staffDevote.SecrecyLevel == 0)
                                {
                                    //Alert.Show("导入失败，姓名或项目名称错误！");
                                    //return row;
                                    Error += "第" + row + "行出错，保密级别不存在！\n";
                                    continue;
                                }
                                else
                                {
                                    staffDevote.IsPass = true;
                                    staffDevote.EntryPerson = BLLUser.FindByLoginName(Session["LoginName"].ToString()).UserName;
                                    context.StaffDevoteContext.Add(staffDevote);
                                    //return null;
                                }
                            }
                        }
                        context.SaveChanges();
                        return Error;
                        //return 0;
                    }
                    else
                        return "Excel无数据导入！";
                    //return -1;
                }
                catch (Exception ex)
                {
                    BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
                    pm.SaveError(ex, this.Request);
                    return "Excel 字段错误 导入失败！";
                    //return row;
                }
            }
        }
        //论文情况表
        public string insertPaper(DataTable dt)
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                int row = 0;
                string Error = null;
                try
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = null;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            Common.Entities.Paper paper = new Common.Entities.Paper();
                            row = i + 1;
                            dr = dt.Rows[i];
                            if (dr["序号"].ToString() != "")
                            {
                                paper.Subject = dr["题目"].ToString();
                                int IsExit = BLLPaper.FindByPaperName(paper.Subject);
                                if (IsExit != 0)
                                {
                                    Error += "第" + row + "行出错，该题目已存在！\n";
                                    continue;
                                }
                                paper.PublicJournalName = dr["发布刊物"].ToString();
                                paper.PaperPeople = dr["全部作者"].ToString();
                                paper.FirstWriter = dr["第一作者"].ToString();
                                paper.MessageWriter = dr["通讯作者"].ToString();
                                paper.MWAgency = dr["通讯作者部门"].ToString();
                                paper.PaperRank = dr["刊物级别"].ToString();
                                paper.WriterIdentity = dr["论文作者身份"].ToString();
                                //paper.PaperForm = dr["论文形式"].ToString();
                                //paper.AchievementID = dr["所属成果名称"].ToString();
                                string AchieveName = dr["所属成果名称"].ToString();
                                paper.AchievementID = BLLAchievement.FindByAchievementName(AchieveName);//AchievementID为零则不存在成果名称
                                if (AchieveName != "" && paper.AchievementID == 0)
                                    return "第" + row + "行出错，所属成果名称不存在！";
                                //paper.PublicDate = dr["发表日期"].ToString();
                                if (dr["发表时间"].ToString() == "")
                                    paper.PublicDate = null;
                                //paper.PublicDate = Convert.ToDateTime("1/1/1753 12:00:00");//默认时间
                                else
                                    paper.PublicDate = Convert.ToDateTime(dr["发表时间"].ToString());
                                paper.PaperUnit = dr["论文所属单位"].ToString();
                                if (dr["他引次数"].ToString() != "")
                                    paper.HQuoteNum = Convert.ToInt32(dr["他引次数"].ToString());
                                if (dr["引用次数"].ToString() != "")
                                    paper.QuoteNum = Convert.ToInt32(dr["引用次数"].ToString());
                                paper.VolumesNum = dr["卷号"].ToString();
                                paper.JournalNum = dr["期号"].ToString();
                                paper.SerialNum = dr["刊号"].ToString();
                                paper.StartPageNum = Convert.ToInt32(dr["起始页码"].ToString());
                                paper.EndPageNum = Convert.ToInt32(dr["结束页码"].ToString());
                                //string Record = dr["是否收录"].ToString();
                                //if (Record == "是")
                                //    paper.Record = true;
                                //else
                                //paper.Record = false;
                                paper.RetrieveSituation = dr["收录情况"].ToString();
                                paper.ImpactFactor = dr["影响因子"].ToString();
                                paper.IncludeNum = dr["收录号"].ToString();
                                //1公开 2内部 3秘密 4机密 5管理员
                                switch (dr["保密级别"].ToString())
                                {
                                    case "四级":
                                        paper.SecrecyLevel = 1;
                                        break;
                                    case "三级":
                                        paper.SecrecyLevel = 2;
                                        break;
                                    case "二级":
                                        paper.SecrecyLevel = 3;
                                        break;
                                    case "一级":
                                        paper.SecrecyLevel = 4;
                                        break;
                                    case "管理员":
                                        paper.SecrecyLevel = 5;
                                        break;
                                    default:
                                        paper.SecrecyLevel = 0;
                                        break;
                                }

                                paper.Remark = dr["备注"].ToString();
                                //int IsExit = BLLPaper.FindByPaperName(paper.Subject);
                                if (paper.SecrecyLevel != 0)
                                {
                                    paper.IsPass = true;
                                    paper.EntryPerson = BLLUser.FindByLoginName(Session["LoginName"].ToString()).UserName;
                                    context.PaperContext.Add(paper);
                                }
                                else
                                {
                                    Error += "第" + row + "行出错，保密级别不存在！\n";
                                    continue;
                                }
                                //return row;
                                //if(dr["所属成果名称"].ToString() == ""&& paper.AchievementID==0)
                            }
                        }
                        if (Error != null)
                            return Error;
                        context.SaveChanges();
                        return null;
                        //return 0;
                    }
                    else
                        return "Excel无数据导入！";
                    //return -1;
                }
                catch (Exception ex)
                {
                    BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
                    pm.SaveError(ex, this.Request);
                    return "Excel 字段错误 导入失败！";
                    //return row;
                }
            }
        }
        //专利情况表
        public string insertPatent(DataTable dt)
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                int row = 0;
                string Error = null;
                try
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = null;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            Common.Entities.Patent patent = new Common.Entities.Patent();
                            row = i + 1;
                            dr = dt.Rows[i];
                            if (dr["序号"].ToString() != "")
                            {
                                patent.PatentName = dr["专利名称"].ToString();
                                bool IsExit = BLLPatent.FindByPaperName(patent.PatentName);//存在返回true
                                if (IsExit)
                                {
                                    Error += "第" + row + "行出错，该专利名称已存在！\n";
                                    continue;
                                }
                                patent.PatentNumber = dr["专利号"].ToString();
                                patent.PatentPeople = dr["全部发明人"].ToString();
                                patent.FirstPeople = dr["第一发明人"].ToString();
                                patent.AgencyID = dr["所属部门"].ToString();
                                patent.Fund = dr["资助经费（万元）"].ToString();
                                patent.PatentCondition = dr["专利情况"].ToString();
                                patent.ApplyNum = dr["申请号"].ToString();
                                //patent.PatentType = dr["类别"].ToString();
                                if (dr["申请时间"].ToString() == "")
                                    patent.ApplicationTime = null;
                                //patent.ApplicationTime = Convert.ToDateTime("1/1/1753 12:00:00");//默认时间
                                else
                                    patent.ApplicationTime = Convert.ToDateTime(dr["申请时间"].ToString());
                                patent.CertificateNumber = dr["证书号"].ToString();
                                patent.PatentForm = dr["专利类型"].ToString();
                                patent.PatentDepartment = dr["单位"].ToString();
                                //patent.PatentDepartment = dr["所属成果名称 "].ToString();
                                string AchieveName = dr["所属成果名称"].ToString();
                                if (AchieveName == "")
                                    patent.AchievementID = null;
                                else
                                    patent.AchievementID = BLLAchievement.FindByAchievementName(AchieveName);//AchievementID为零则不存在成果名称
                                if (AchieveName != "" && patent.AchievementID == 0)
                                    return "第" + row + "行出错，所属成果名称不存在！";
                                if (dr["授权时间"].ToString() == "")
                                    patent.AccreditTime = null;
                                //patent.AccreditTime = Convert.ToDateTime("1/1/1753 12:00:00");//默认时间
                                else
                                    patent.AccreditTime = Convert.ToDateTime(dr["授权时间"].ToString());
                                patent.GivenUnit = dr["授予机构"].ToString();
                                patent.State = dr["状态"].ToString();
                                //1公开 2内部 3秘密 4机密 5管理员
                                switch (dr["保密级别"].ToString())
                                {
                                    case "四级":
                                        patent.SecrecyLevel = 1;
                                        break;
                                    case "三级":
                                        patent.SecrecyLevel = 2;
                                        break;
                                    case "二级":
                                        patent.SecrecyLevel = 3;
                                        break;
                                    case "一级":
                                        patent.SecrecyLevel = 4;
                                        break;
                                    case "管理员":
                                        patent.SecrecyLevel = 5;
                                        break;
                                    default:
                                        patent.SecrecyLevel = 0;
                                        break;
                                }
                                patent.Comment = dr["备注"].ToString();

                                if (patent.SecrecyLevel != 0)
                                {
                                    patent.IsPass = true;
                                    patent.EntryPerson = BLLUser.FindByLoginName(Session["LoginName"].ToString()).UserName;
                                    context.PatentContext.Add(patent);
                                }
                                else
                                {
                                    //return row;
                                    Error += "第" + row + "行出错，保密级别不存在！";
                                    continue;
                                }
                            }
                        }
                        context.SaveChanges();
                        return Error;
                        //return 0;
                    }
                    else
                        return "Excel无数据导入！";
                    //return -1;
                }
                catch (Exception ex)
                {
                    BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
                    pm.SaveError(ex, this.Request);
                    return "Excel 字段错误 导入失败！";
                    //return row;
                }
            }
        }
        //专著情况表
        public string insertMonograph(DataTable dt)
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                int row = 0;
                string Error = null;
                try
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = null;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            Common.Entities.Monograph monograph = new Common.Entities.Monograph();
                            row = i + 1;
                            dr = dt.Rows[i];
                            if (dr["序号"].ToString() != "")
                            {
                                monograph.MonographName = dr["专著名称"].ToString();
                                if (BLLMonograph.IsEixtName(monograph.MonographName) != null)
                                {
                                    return "第" + row + "行出错，该专著名称已存在！";
                                }
                                monograph.MonographPeople = dr["作者"].ToString();
                                string AchieveName = dr["所属成果名称"].ToString();
                                monograph.AchievementID = BLLAchievement.FindByAchievementName(AchieveName);//AchievementID为零则不存在成果名称
                                if (AchieveName != "" && monograph.AchievementID == 0)
                                {
                                    Error += "第" + row + "行出错，所属成果名称不存在！\n";
                                    continue;
                                }
                                if (dr["出版时间"].ToString() == "")
                                    monograph.PUblicationTime = null;
                                //monograph.PUblicationTime = Convert.ToDateTime("1/1/1753 12:00:00");//默认时间
                                else
                                    monograph.PUblicationTime = Convert.ToDateTime(dr["出版时间"].ToString());
                                monograph.Publisher = dr["出版社"].ToString();
                                monograph.BookNuber = dr["图书编号"].ToString();
                                monograph.Revision = dr["版次"].ToString();
                                monograph.IssueRegin = dr["发行国家和地区"].ToString();
                                monograph.Sort = dr["类别"].ToString();
                                //1公开 2内部 3秘密 4机密 5管理员
                                switch (dr["保密级别"].ToString())
                                {
                                    case "四级":
                                        monograph.SecrecyLevel = 1;
                                        break;
                                    case "三级":
                                        monograph.SecrecyLevel = 2;
                                        break;
                                    case "二级":
                                        monograph.SecrecyLevel = 3;
                                        break;
                                    case "一级":
                                        monograph.SecrecyLevel = 4;
                                        break;
                                    case "管理员":
                                        monograph.SecrecyLevel = 5;
                                        break;
                                    default:
                                        monograph.SecrecyLevel = 0;
                                        break;
                                }
                                monograph.Remark = dr["备注"].ToString();
                                monograph.MonographType = dr["著作类型"].ToString();
                                monograph.ISBNNum = dr["ISBN号"].ToString();
                                monograph.CIPNum = dr["CIP号"].ToString();
                                monograph.FirstWriter = dr["第一作者"].ToString();
                                if (monograph.SecrecyLevel != 0)
                                {
                                    monograph.IsPass = true;
                                    monograph.EntryPerson = BLLUser.FindByLoginName(Session["LoginName"].ToString()).UserName;
                                    context.MonographContext.Add(monograph);
                                }
                                else
                                {
                                    Error += "第" + row + "行出错，保密级别不存在！\n";
                                    continue;
                                }
                                //return row;
                            }
                        }
                        context.SaveChanges();
                        return Error;
                        //return 0;
                    }
                    else
                        return "Excel无数据导入！";
                    //return -1;
                }
                catch (Exception ex)
                {
                    BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
                    pm.SaveError(ex, this.Request);
                    return "Excel 字段错误 导入失败！";
                    //return row;
                }
            }
        }
        //人员鉴定成果表
        public string insertStaffAchieve(DataTable dt)
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                int row = 0;
                string Error = null;
                try
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = null;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            Common.Entities.StaffAchieve staffAchieve = new Common.Entities.StaffAchieve();
                            row = i + 1;
                            dr = dt.Rows[i];
                            if (dr["序号"].ToString() != "")
                            {
                                string userName = dr["人员姓名"].ToString();
                                staffAchieve.UserInfoID = BLLUser.FindID(userName);//UserInfoID为0则不存在该用户
                                if (staffAchieve.UserInfoID == 0)
                                {
                                    Error += "第" + row + "行出错，人员姓名不存在！\n";
                                    continue;
                                }
                                string AchieveName = dr["鉴定成果名称"].ToString();
                                staffAchieve.AchievementID = BLLAchievement.FindByAchievementName(AchieveName);//AchievementID为零则不存在成果名称
                                if (staffAchieve.AchievementID == 0)
                                {
                                    return "第" + row + "行出错，鉴定成果名称不存在！";
                                }
                                //1公开 2内部 3秘密 4机密 5管理员
                                switch (dr["保密级别"].ToString())
                                {
                                    case "四级":
                                        staffAchieve.SecrecyLevel = 1;
                                        break;
                                    case "三级":
                                        staffAchieve.SecrecyLevel = 2;
                                        break;
                                    case "二级":
                                        staffAchieve.SecrecyLevel = 3;
                                        break;
                                    case "一级":
                                        staffAchieve.SecrecyLevel = 4;
                                        break;
                                    case "管理员":
                                        staffAchieve.SecrecyLevel = 5;
                                        break;
                                    default:
                                        staffAchieve.SecrecyLevel = 0;
                                        break;
                                }
                                if (staffAchieve.SecrecyLevel == 0)
                                {
                                    Error += "第" + row + "行出错，保密级别不存在！\n";
                                    continue;
                                    //return row;
                                }
                                else
                                {
                                    staffAchieve.IsPass = true;
                                    staffAchieve.EntryPerson = BLLUser.FindByLoginName(Session["LoginName"].ToString()).UserName;
                                    context.StaffAchieveContext.Add(staffAchieve);
                                }
                            }
                        }
                        context.SaveChanges();
                        return Error;
                        //return 0;
                    }
                    else
                        return "Excel无数据导入！";
                    //return -1;
                }
                catch (Exception ex)
                {
                    BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
                    pm.SaveError(ex, this.Request);
                    return "Excel 字段错误 导入失败！";
                    //return row;
                }
            }
        }
        //会议参加人员表
        public string insertAttendMeeting(DataTable dt)
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                int row = 0;
                string Error = null;
                try
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = null;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            Common.Entities.AttendMeeting attendMeeting = new Common.Entities.AttendMeeting();
                            StringBuilder stringBuilder = new StringBuilder();
                            row = i + 1;
                            dr = dt.Rows[i];
                            if (dr["序号"].ToString() != "")
                            {
                                string userName = dr["姓名"].ToString();
                                attendMeeting.UserInfoID = BLLUser.FindID(userName);//UserInfoID为零则不存在该用户
                                if (attendMeeting.UserInfoID == 0)
                                {
                                    Error += "第" + row + "行出错，姓名不存在！\n";
                                    continue;
                                }
                                //int UserInfoID = BLLUser.FindByUserID(userName);
                                string MeetingName = dr["会议名称"].ToString();
                                attendMeeting.AcademicMeetingID = BLLAcademicMeeting.FindMeetingID(MeetingName);//AcademicMeetingID为零则不存在该项目
                                if (attendMeeting.AcademicMeetingID == 0)
                                {
                                    return "第" + row + "行出错，会议名称不存在！";
                                }
                                //int AcademicMeetingID = BLLAcademicMeeting.FindByMeetingID(MeetingName);
                                //1公开 2内部 3秘密 4机密 5管理员
                                switch (dr["保密级别"].ToString())
                                {
                                    case "四级":
                                        attendMeeting.SecrecyLevel = 1;
                                        break;
                                    case "三级":
                                        attendMeeting.SecrecyLevel = 2;
                                        break;
                                    case "二级":
                                        attendMeeting.SecrecyLevel = 3;
                                        break;
                                    case "一级":
                                        attendMeeting.SecrecyLevel = 4;
                                        break;
                                    case "管理员":
                                        attendMeeting.SecrecyLevel = 5;
                                        break;
                                    default:
                                        attendMeeting.SecrecyLevel = 0;
                                        break;
                                }
                                attendMeeting.IsPass = true;
                                attendMeeting.EntryPerson = BLLUser.FindByLoginName(Session["LoginName"].ToString()).UserName;
                                if (attendMeeting.SecrecyLevel == 0)
                                {
                                    Error += "第" + row + "行出错，保密级别不存在！\n";
                                    continue;
                                    //return row;
                                }
                                context.AttendMeetingContext.Add(attendMeeting);
                            }
                        }
                        context.SaveChanges();
                        return Error;
                        //return 0;
                    }
                    else
                        return "Excel无数据导入！";
                    //return -1;
                }
                catch (Exception ex)
                {
                    BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
                    pm.SaveError(ex, this.Request);
                    return "Excel 字段错误 导入失败！";
                    //return row;
                }
            }
        }
        //获奖情况表
        public string insertAward(DataTable dt)
        {
            //获奖名称不能重复
            using (DataBaseContext context = new DataBaseContext())
            {
                int row = 0;
                string Error = null;
                try
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = null;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            Common.Entities.Award award = new Common.Entities.Award();
                            row = i + 1;
                            dr = dt.Rows[i];
                            if (dr["序号"].ToString() != "")
                            {
                                award.AwardName = dr["获奖名称"].ToString();
                                if (BLLAward.IsExitAwardName(award.AwardName) != null)
                                {
                                    Error += "第" + row + "行出错，该获奖名称已存在！\n";
                                    continue;
                                }
                                if (dr["获奖时间"].ToString() == "")
                                    award.AwardTime = null;
                                //award.AwardTime = Convert.ToDateTime("1/1/1753 12:00:00");//默认时间
                                else
                                    award.AwardTime = Convert.ToDateTime(dr["获奖时间"].ToString());
                                award.AwardwSpecies = dr["获奖类别"].ToString();
                                award.AwardPeople = dr["获奖人"].ToString();
                                //string AchieveName = dr["成果名称"].ToString();
                                //award.Acheivement = BLLAchievement.FindByAchievementName(AchieveName);//AchievementID为零则不存在成果名称
                                award.Acheivement = dr["成果名称"].ToString();
                                award.Grade = dr["获奖等级"].ToString();
                                award.GivAgency = dr["赋予机构"].ToString();
                                award.Unit = dr["单位"].ToString();
                                award.FirstAward = dr["第一获奖人"].ToString();
                                award.AwardNum = dr["获奖证书号"].ToString();
                                award.AwardForm = dr["获奖类型"].ToString();
                                //1公开 2内部 3秘密 4机密 5管理员
                                switch (dr["保密级别"].ToString())
                                {
                                    case "四级":
                                        award.SecrecyLevel = 1;
                                        break;
                                    case "三级":
                                        award.SecrecyLevel = 2;
                                        break;
                                    case "二级":
                                        award.SecrecyLevel = 3;
                                        break;
                                    case "一级":
                                        award.SecrecyLevel = 4;
                                        break;
                                    case "管理员":
                                        award.SecrecyLevel = 5;
                                        break;
                                    default:
                                        award.SecrecyLevel = 0;
                                        break;
                                }
                                award.Remark = dr["备注"].ToString();
                                if (award.SecrecyLevel == 0)
                                {
                                    Error += "第" + row + "行出错，保密级别不存在！\n";
                                    continue;
                                    //return row;
                                }
                                else
                                {
                                    award.IsPass = true;
                                    award.EntryPerson = BLLUser.FindByLoginName(Session["LoginName"].ToString()).UserName;
                                    context.AwardContext.Add(award);
                                }
                            }
                        }
                        context.SaveChanges();
                        return Error;
                        //return 0;
                    }
                    else
                        return "Excel无数据导入！";
                    //return -1;
                }
                catch (Exception ex)
                {
                    BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
                    pm.SaveError(ex, this.Request);
                    return "Excel 字段错误 导入失败！";
                    //return row;
                }
            }
        }
        //学术会议表
        public string insertAcademicMeeting(DataTable dt)
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                int row = 0;
                string Error = null;
                try
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = null;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            Common.Entities.AcademicMeeting AM = new Common.Entities.AcademicMeeting();
                            row = i + 1;
                            dr = dt.Rows[i];
                            if (dr["序号"].ToString() != "")
                            {
                                AM.MeetingName = dr["会议名称"].ToString();
                                AM.Organizers = dr["主办方"].ToString();
                                AM.Coorganizers = dr["协办方"].ToString();
                                //AM.StratTime =Convert.ToDateTime(dr["会议开始时间"].ToString());
                                if (dr["会议开始时间"].ToString() == "")
                                    AM.StratTime = null;
                                //AM.StratTime = null;
                                else
                                    AM.StratTime = Convert.ToDateTime(dr["会议开始时间"].ToString());
                                if (dr["会议结束时间"].ToString() == "")
                                    AM.EndTime = null;
                                else
                                    AM.EndTime = Convert.ToDateTime(dr["会议结束时间"].ToString());
                                // AM.EndTime = Convert.ToDateTime(dr["会议结束时间"].ToString());
                                AM.MeetingPlace = dr["会议地点"].ToString();
                                AM.ProceedingsofTitle = dr["论文集名称"].ToString();
                                AM.MeetingSortName = dr["会议分类名称"].ToString();
                                AM.MeetingCount = dr["会议参加人员"].ToString();
                                AM.MeetingMajorPerson = dr["会议主席"].ToString();
                                AM.MeetingMajorTheme = dr["会议主题"].ToString();
                                AM.MeetingHost = dr["会议主持人"].ToString();
                                AM.MeetingContent = dr["会议内容简介"].ToString();
                                AM.AttendMeetingPeople = dr["会议参加人员"].ToString();

                                //1公开 2内部 3秘密 4机密 5管理员
                                switch (dr["保密级别"].ToString())
                                {
                                    case "四级":
                                        AM.SecrecyLevel = 1;
                                        break;
                                    case "三级":
                                        AM.SecrecyLevel = 2;
                                        break;
                                    case "二级":
                                        AM.SecrecyLevel = 3;
                                        break;
                                    case "一级":
                                        AM.SecrecyLevel = 4;
                                        break;
                                    case "管理员":
                                        AM.SecrecyLevel = 5;
                                        break;
                                    default:
                                        AM.SecrecyLevel = 0;
                                        break;
                                }
                                if (AM.SecrecyLevel == 0)
                                {
                                    Error += "第" + row + "行出错，保密级别不存在！\n";
                                    continue;
                                    //return row;
                                }
                                else
                                {
                                    AM.IsPass = true;
                                    AM.EntryPerson = BLLUser.FindByLoginName(Session["LoginName"].ToString()).UserName;
                                    context.AcademicMeetingContext.Add(AM);
                                }
                            }
                        }
                        context.SaveChanges();
                        return Error; ;
                        //return 0;
                    }
                    else
                        return "Excel无数据导入！";
                    //return -1;
                }
                catch (Exception ex)
                {
                    BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
                    pm.SaveError(ex, this.Request);
                    return "Excel 字段错误 导入失败！";
                    //return row;
                }
            }
        }
        //学术报告表（属于相关学术会议的学术报告）
        public string insertScienceReport(DataTable dt)
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                int row = 0;
                string Error = null;
                try
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = null;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            Common.Entities.ScienceReport SR = new Common.Entities.ScienceReport();
                            row = i + 1;
                            dr = dt.Rows[i];
                            if (dr["序号"].ToString() != "")
                            {
                                SR.SReportName = dr["报告名称"].ToString();
                                SR.SReportPeople = dr["报告人"].ToString();
                                if (dr["报告时间"].ToString() == "")
                                    SR.SReportTime = null;
                                else
                                    SR.SReportTime = Convert.ToDateTime(dr["报告时间"].ToString());
                                SR.SReportPlace = dr["报告地点"].ToString();
                                SR.MeetingID = BLLAcademicMeeting.FindMeetingID(dr["所属会议"].ToString());
                                if (SR.MeetingID == 0)
                                {
                                    Error += "第" + row + "行出错，不存在所属会议名称\n";
                                    continue;
                                }
                                SR.AgencyID = BLLAgency.SelectAgencyID(dr["所属机构"].ToString());

                                //1公开 2内部 3秘密 4机密 5管理员
                                switch (dr["保密级别"].ToString())
                                {
                                    case "四级":
                                        SR.SecrecyLevel = 1;
                                        break;
                                    case "三级":
                                        SR.SecrecyLevel = 2;
                                        break;
                                    case "二级":
                                        SR.SecrecyLevel = 3;
                                        break;
                                    case "一级":
                                        SR.SecrecyLevel = 4;
                                        break;
                                    case "管理员":
                                        SR.SecrecyLevel = 5;
                                        break;
                                    default:
                                        SR.SecrecyLevel = 0;
                                        break;
                                }
                                if (SR.SecrecyLevel == 0)
                                {
                                    Error += "第" + row + "行出错，保密级别不存在！\n";
                                    continue;
                                    //return row;
                                }
                                else
                                {
                                    SR.IsPass = true;
                                    SR.EntryPerson = BLLUser.FindByLoginName(Session["LoginName"].ToString()).UserName;
                                    context.ScienceReportContext.Add(SR);
                                }
                            }
                        }
                        context.SaveChanges();
                        return Error;
                        //return 0;
                    }
                    else
                        return "Excel无数据导入！";
                    //return -1;
                }
                catch (Exception ex)
                {
                    BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
                    pm.SaveError(ex, this.Request);
                    return "Excel 字段错误 导入失败！";
                    //return row;
                }
            }
        }
        //学术报告表（不属于学术会议的报告）
        public string insertNewScienceReport(DataTable dt)
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                int row = 0;
                string Error = null;
                try
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = null;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            Common.Entities.NewAcademicReporting SR = new Common.Entities.NewAcademicReporting();
                            row = i + 1;
                            dr = dt.Rows[i];
                            if (dr["序号"].ToString() != "")
                            {
                                SR.ReportPeople = dr["报告人"].ToString();
                                SR.ReportName = dr["学术报告名称"].ToString();
                                SR.JobName = dr["职称"].ToString();
                                SR.JobMission = dr["职务"].ToString();
                                SR.ReportUnit = dr["报告人单位"].ToString();
                                SR.Report = dr["报告人身份证号"].ToString();
                                SR.ReportTele = dr["报告人手机号"].ToString();
                                SR.Remark = dr["备注"].ToString();
                                SR.AcademicTitle = dr["学术兼职及荣誉称号"].ToString();
                                SR.ReportPlace = dr["报告地点"].ToString();

                                SR.ApplyFund = dr["申请经费（万元）"].ToString();
                                if (dr["参与人数"].ToString().Trim() == "")
                                    SR.PeopleCount = 0;
                                else
                                    SR.PeopleCount = Convert.ToInt32(dr["参与人数"].ToString().Trim());
                                SR.Organizers = dr["主办单位"].ToString();
                                SR.Coorganizer = dr["协办单位"].ToString();
                                SR.ReportType = dr["报告类别"].ToString();
                                SR.MajorPeople = dr["主要参与人"].ToString();
                                if (dr["报告时间"].ToString() == "")
                                    SR.ReportTime = null;
                                else
                                    SR.ReportTime = Convert.ToDateTime(dr["报告时间"].ToString());

                                //1公开 2内部 3秘密 4机密 5管理员
                                switch (dr["保密级别"].ToString())
                                {
                                    case "四级":
                                        SR.SecrecyLevel = 1;
                                        break;
                                    case "三级":
                                        SR.SecrecyLevel = 2;
                                        break;
                                    case "二级":
                                        SR.SecrecyLevel = 3;
                                        break;
                                    case "一级":
                                        SR.SecrecyLevel = 4;
                                        break;
                                    case "管理员":
                                        SR.SecrecyLevel = 5;
                                        break;
                                    default:
                                        SR.SecrecyLevel = 0;
                                        break;
                                }
                                if (SR.SecrecyLevel == 0)
                                {
                                    Error += "第" + row + "行出错，保密级别不存在！\n";
                                    continue;
                                    //return row;
                                }
                                else
                                {
                                    SR.IsPass = true;
                                    SR.EntryPerson = BLLUser.FindByLoginName(Session["LoginName"].ToString()).UserName;
                                    context.NewAcademicReportingContext.Add(SR);
                                }
                            }
                        }
                        context.SaveChanges();
                        return Error;
                        //return 0;
                    }
                    else
                        return "Excel无数据导入！";
                    //return -1;
                }
                catch (Exception ex)
                {
                    BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
                    pm.SaveError(ex, this.Request);
                    return "Excel 字段错误 导入失败！";
                    //return row;
                }
            }
        }

        //仪器设备表
        public string insertEquipment(DataTable dt)
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                int row = 0;
                string Error = null;
                try
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = null;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            Common.Entities.Equipment equipment = new Common.Entities.Equipment();
                            row = i + 1;
                            dr = dt.Rows[i];
                            if (dr["序号"].ToString() != "")
                            {
                                equipment.EquipmentName = dr["资产名称"].ToString();
                                //int count = BLLEquipment.FindByEquipmentName(equipment.EquipmentName,5).Count;
                                //if (count!=0)
                                //{
                                //    return "第" + row + "行出错，该资产名称已存在！";
                                //}
                                equipment.EquipNum = dr["资产编号"].ToString();
                                equipment.Purchase = dr["购买人"].ToString();
                                if (dr["购置日期"].ToString() == "")
                                    equipment.PurchaseTime = null;
                                else
                                    equipment.PurchaseTime = Convert.ToDateTime(dr["购置日期"].ToString());
                                //equipment.PurchaseTime = dr["购买时间"].ToString();
                                equipment.Price = dr["价格"].ToString();
                                equipment.UsePerson = dr["使用人"].ToString();
                                equipment.StorageLocation = dr["存放地点"].ToString();
                                equipment.MeasurementUnit = dr["计量单位"].ToString();
                                equipment.Manufacturer = dr["生产厂家"].ToString();
                                equipment.Model = dr["型号"].ToString();
                                equipment.ClassNum = dr["分类号"].ToString();
                                equipment.CategoryName = dr["分类名称"].ToString();
                                equipment.AgencName = dr["所属机构"].ToString();
                                equipment.Remarks = dr["备注"].ToString();
                                if (dr["是否共享"].ToString() == "是")
                                    equipment.IsShare = true;
                                else
                                    equipment.IsShare = false;
                                //equipment.IsShare = dr["是否共享"].ToString();
                                if (dr["是否政府采购"].ToString() == "是")
                                    equipment.IsGowerProcu = true;
                                else
                                    equipment.IsGowerProcu = false;
                                //equipment.IsGowerProcu = dr["是否政府采购"].ToString();
                                //equipment.AgencyID = BLLAgency.SelectAgencyID(dr["所属机构"].ToString());
                                //if (dr["所属机构"].ToString() != "")
                                //{
                                //    if(equipment.AgencyID==0)
                                //        return "第" + row + "行出错，所属机构名称不存在！";
                                //}

                                //1公开 2内部 3秘密 4机密 5管理员
                                switch (dr["保密级别"].ToString())
                                {
                                    case "四级":
                                        equipment.SecrecyLevel = 1;
                                        break;
                                    case "三级":
                                        equipment.SecrecyLevel = 2;
                                        break;
                                    case "二级":
                                        equipment.SecrecyLevel = 3;
                                        break;
                                    case "一级":
                                        equipment.SecrecyLevel = 4;
                                        break;
                                    case "管理员":
                                        equipment.SecrecyLevel = 5;
                                        break;
                                    default:
                                        equipment.SecrecyLevel = 0;
                                        break;

                                }
                                if (equipment.SecrecyLevel == 0)
                                {
                                    //Alert.Show("导入失败，姓名或项目名称错误！");
                                    //return row;
                                    Error += "第" + row + "行出错，保密级别不存在！\n";
                                    continue;
                                }
                                else
                                {
                                    equipment.IsPass = true;
                                    equipment.EntryPerson = BLLUser.FindByLoginName(Session["LoginName"].ToString()).UserName;
                                    context.EquipmentContext.Add(equipment);
                                }
                            }
                        }
                        context.SaveChanges();
                        return Error;
                        //return 0;
                    }
                    else
                        return "Excel无数据导入！";
                    //return -1;
                }
                catch (Exception ex)
                {
                    BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
                    pm.SaveError(ex, this.Request);
                    return row + "行 Excel 字段错误 导入失败！";
                    //return row;
                }
            }
        }

        //家具表
        public string insertFurniture(DataTable dt)
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                int row = 0;
                string Error = null;
                try
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = null;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            Common.Entities.Furniture furniture = new Common.Entities.Furniture();
                            row = i + 1;
                            dr = dt.Rows[i];
                            if (dr["序号"].ToString() != "")
                            {
                                furniture.FurnitureName = dr["资产名称"].ToString();
                                furniture.EquipNum = dr["资产编号"].ToString();
                                //int count = BLLFurniture.FindByFurnitureName(furniture.FurnitureName, 5).Count;
                                //if (count!=0)
                                //{
                                //    return "第" + row + "行出错，该设备名称已存在！";
                                //}
                                furniture.Purchase = dr["购买人"].ToString();
                                if (dr["购买时间"].ToString() == "")
                                    furniture.PurchaseTime = null;
                                else
                                    furniture.PurchaseTime = Convert.ToDateTime(dr["购买时间"].ToString());
                                //equipment.PurchaseTime = dr["购买时间"].ToString();
                                furniture.Price = dr["价格"].ToString();
                                furniture.UsePerson = dr["使用人"].ToString();
                                //equipment.AgencyName = dr["所属部门"].ToString();
                                furniture.StorageLocation = dr["存放地点"].ToString();
                                furniture.MeasurementUnit = dr["计量单位"].ToString();
                                furniture.Manufacturer = dr["生产厂家"].ToString();
                                furniture.Model = dr["型号"].ToString();
                                furniture.ClassNum = dr["分类号"].ToString();
                                furniture.CategoryName = dr["分类"].ToString();
                                furniture.Remarks = dr["备注"].ToString();
                                furniture.AgencName = dr["所属机构"].ToString();
                                if (dr["是否政府采购"].ToString() == "是")
                                    furniture.IsGowerProcu = true;
                                else
                                    furniture.IsGowerProcu = false;
                                if (dr["是否共享"].ToString() == "是")
                                    furniture.IsShare = true;
                                else
                                    furniture.IsShare = false;
                                //equipment.IsGowerProcu = dr["是否政府采购"].ToString();
                                //furniture.AgencyID = BLLAgency.SelectAgencyID(dr["所属机构"].ToString());
                                // if (dr["所属机构"].ToString() != "")
                                //{
                                //if (furniture.AgencyID == 0)
                                //    return "第" + row + "行出错，所属机构名称不存在！";
                                //}

                                //1公开 2内部 3秘密 4机密 5管理员
                                switch (dr["保密级别"].ToString())
                                {
                                    case "四级":
                                        furniture.SecrecyLevel = 1;
                                        break;
                                    case "三级":
                                        furniture.SecrecyLevel = 2;
                                        break;
                                    case "二级":
                                        furniture.SecrecyLevel = 3;
                                        break;
                                    case "一级":
                                        furniture.SecrecyLevel = 4;
                                        break;
                                    case "管理员":
                                        furniture.SecrecyLevel = 5;
                                        break;
                                    default:
                                        furniture.SecrecyLevel = 0;
                                        break;

                                }
                                if (furniture.SecrecyLevel == 0)
                                {
                                    //Alert.Show("导入失败，姓名或项目名称错误！");
                                    //return row;
                                    Error += "第" + row + "行出错，保密级别不存在！\n";
                                    continue;
                                }
                                else
                                {
                                    furniture.IsPass = true;
                                    furniture.EntryPerson = BLLUser.FindByLoginName(Session["LoginName"].ToString()).UserName;
                                    context.FurnitureContext.Add(furniture);
                                }
                            }
                        }
                        context.SaveChanges();
                        return Error;
                        //return 0;
                    }
                    else
                        return "Excel无数据导入！";
                    //return -1;
                }
                catch (Exception ex)
                {
                    BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
                    pm.SaveError(ex, this.Request);
                    return "Excel 字段错误 导入失败！";
                    //return row;
                }
            }
        }

        //学生情况信息表
        public string insertStudentInfo(DataTable dt)
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                int row = 0;
                string Error = null;
                try
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = null;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            Common.Entities.Student student = new Common.Entities.Student();
                            row = i + 1;
                            dr = dt.Rows[i];
                            if (dr["序号"].ToString() != "")
                            {
                                student.AgencyID = BLLAgency.SelectAgencyID(dr["所属部门"].ToString());
                                student.Contact = dr["联系方式"].ToString();
                                student.DocumentNumber = dr["证件号码"].ToString();
                                student.DocumentType = dr["证件类型"].ToString();
                                if (dr["入学时间"].ToString() == "")
                                    student.EnterTime = null;
                                //student.EnterTime = Convert.ToDateTime("1/1/1753 12:00:00");//默认时间
                                else
                                    student.EnterTime = Convert.ToDateTime(dr["入学时间"].ToString());
                                student.EntryPerson = dr["录入人"].ToString();
                                if (dr["毕业时间"].ToString() == "")
                                    student.GraduationTime = null;
                                //student.GraduationTime = Convert.ToDateTime("1/1/1753 12:00:00");//默认时间
                                else
                                    student.GraduationTime = Convert.ToDateTime(dr["毕业时间"].ToString());
                                if (dr["是否毕业"].ToString() == "已毕业")
                                    student.IsGraduation = true;
                                else
                                    student.IsGraduation = false;
                                if (dr["性别"].ToString() == "男")
                                    student.Sex = true;
                                else
                                    student.Sex = false;
                                student.SGraduationDirection = dr["毕业去向单位"].ToString();
                                student.Sname = dr["姓名"].ToString();
                                student.Sno = dr["学号"].ToString();
                                student.Specialty = dr["专业"].ToString();
                                student.SResearch = dr["研究方向"].ToString();
                                student.Type = dr["学生类型"].ToString();
                                string userName = dr["指导教师"].ToString();
                                student.UserInfoID = BLLUser.FindID(userName);//UserInfoID为零则不存在该用户
                                if (student.UserInfoID == 0)
                                {
                                    Error += "第" + row + "行出错，指导教师不存在！\n";
                                    continue;
                                }

                                switch (dr["保密级别"].ToString())
                                {
                                    case "四级":
                                        student.SecrecyLevel = 1;
                                        break;
                                    case "三级":
                                        student.SecrecyLevel = 2;
                                        break;
                                    case "二级":
                                        student.SecrecyLevel = 3;
                                        break;
                                    case "一级":
                                        student.SecrecyLevel = 4;
                                        break;
                                    case "管理员":
                                        student.SecrecyLevel = 5;
                                        break;
                                    default:
                                        student.SecrecyLevel = 0;
                                        break;

                                }
                                if (student.SecrecyLevel == 0)
                                {
                                    //Alert.Show("导入失败，姓名或项目名称错误！");
                                    //return row;
                                    Error += "第" + row + "行出错，保密级别不存在！\n";
                                    continue;
                                }
                                else
                                {
                                    student.IsPass = true;
                                    student.EntryPerson = BLLUser.FindByLoginName(Session["LoginName"].ToString()).UserName;
                                    context.StudentContext.Add(student);
                                }
                            }
                        }
                        context.SaveChanges();
                        return Error;
                        //return 0;
                    }
                    else
                        return "Excel无数据导入！";
                    //return -1;
                }
                catch (Exception ex)
                {
                    BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
                    pm.SaveError(ex, this.Request);
                    return "Excel 字段错误 导入失败！";
                    //return row;
                }
            }
        }
    }
}