using Common.Entities;
using FineUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WDFramework.People
{
    public partial class PeopleResume : System.Web.UI.Page
    {
        BLHelper.BLLEducation bllEducation = new BLHelper.BLLEducation();
        BLHelper.BLLUser bllUser = new BLHelper.BLLUser();
        BLHelper.BLLWorkExperience bllWork = new BLHelper.BLLWorkExperience();
        BLHelper.BLLAgency bllAgency = new BLHelper.BLLAgency();
        BLHelper.BLLAttachment bllAttachment = new BLHelper.BLLAttachment();
        BLHelper.BLLOperationLog bllOperate = new BLHelper.BLLOperationLog();
        BLHelper.BLLPhotos bllPhotos = new BLHelper.BLLPhotos();
        BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
        OperationLog log = new OperationLog();
        Photos photo = new Photos();
        Attachment attachment = new Attachment();
        BLHelper.BLLEduExperience bllEduE = new BLHelper.BLLEduExperience();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }
        }
        public void BindData()
        {
            try
            {
                List<UserInfo> userlist = bllUser.FindByLoginName(Session["LoginName"].ToString(), Convert.ToInt32(Session["SecrecyLevel"])).ToList();
                UserInfo user = userlist.FirstOrDefault();

                UserName.Text = bllUser.FindByUserID(user.UserInfoID);
                if (user.Sex == true)
                    Sex.Text = "女";
                else
                    Sex.Text = "男";
                Nation.Text = user.Nation;
                Hometown.Text = user.Hometown;
                Birth.Text = user.Birth.Value.ToShortDateString();
                 AgencyID.Text = bllAgency.FindAgenName(user.AgencyID);
                string[] SecrecyLevels = new string[] { "四级", "三级", "二级", "一级", "管理员" };
                //string[] SecrecyLevels = new string[] { "公开", "内部", "秘密", "机密", "管理员" };
                SecrecyLevel.Text = SecrecyLevels[Convert.ToInt32(Session["SecrecyLevel"]) - 1];
                //TextAreaSearch.Text = "研究方向： " + user.ResearchDirection + "。专长：" + user.Specialty + "。";
                if (!string.IsNullOrEmpty(user.ResearchDirection))
                {
                    TextAreaResearchDirection.Text = user.ResearchDirection;
                }
                else
                    TextAreaResearchDirection.Text = "暂无";
                if (!string.IsNullOrEmpty(user.Specialty))
                {
                    TextAreaSpecialty.Text = user.Specialty;
                }
                else
                    TextAreaSpecialty.Text = "暂无";
                SelectByEducation();
                SelectByWork();
                SelectByEduE();
                FindUrl();
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //根据userID找到图片的路径显示在Image
        public void FindUrl()
        {
            try
            {
                int UserID = bllUser.FindByLoginName(Session["LoginName"].ToString(), Convert.ToInt32(Session["SecrecyLevel"])).FirstOrDefault().UserInfoID;
                int PhtosID = bllPhotos.FindPhotoID(UserID);
                if (PhtosID != 0)
                {
                    string FindPath = bllAttachment.FindPath(bllPhotos.FindAttachmentID(PhtosID));
                    imgPhoto.ImageUrl = FindPath;
                }
                else
                    //imgPhoto.ImageUrl = @"C:\\Users\\MBC\\Desktop\\WDFramework\\WDFramework\\images\blank.png";
                    imgPhoto.ImageUrl = "~/images/blank.png";
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //根据登录名找到UserInfoID在查找学历信息
        public void SelectByEducation()
        {
            try
            {
                int UserID = bllUser.FindByLoginName(Session["LoginName"].ToString(), Convert.ToInt32(Session["SecrecyLevel"])).FirstOrDefault().UserInfoID;
                List<Education> educationlist = bllEducation.SelectByID(UserID, Convert.ToInt32(Session["SecrecyLevel"]));
                if (educationlist.Count != 0)
                {


                    for (int i = 0; i < educationlist.Count; i++)
                    {
                        string temp = bllUser.FindByUserID(UserID).ToString() + "在" + educationlist[i].SchoolName + "、" + educationlist[i].College + "、" + educationlist[i].Series + "系、" + educationlist[i].Major + "专业、" + "在" + educationlist[i].EduTime.Value.ToShortDateString() + ",取得" + educationlist[i].Degree + "学位。\n";
                        TextAreaEducation.Text += temp;
                    }
                }
                else
                    TextAreaEducation.Text = "暂无";
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //根据登录名找到UserInfoID在查找工作经历信息
        public void SelectByWork()
        {
            try
            {
                int UserID = bllUser.FindByLoginName(Session["LoginName"].ToString(), Convert.ToInt32(Session["SecrecyLevel"])).FirstOrDefault().UserInfoID;
                List<WorkExperience> worklist = bllWork.SelectByUserID(UserID, Convert.ToInt32(Session["SecrecyLevel"]));

                if (worklist.Count != 0)
                {
                    for (int i = 0; i < worklist.Count; i++)
                    {
                        string temp = bllUser.FindByUserID(UserID).ToString() + "在" + worklist[i].StartTime.Value.ToShortDateString() + " ~ " + worklist[i].EndTime.Value.ToShortDateString() + ",工作在" + worklist[i].WorkUnit + ",职务为" + worklist[i].Post + ",职称为" + worklist[i].JobTitle + "。\n";
                        TextAreaWork.Text += temp;
                    }

                }
                else
                    TextAreaWork.Text = "暂无";
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //根据登录名找到UserInfoID在查找教育经历信息
        public void SelectByEduE()
        {
            try
            {
                int UserID = bllUser.FindByLoginName(Session["LoginName"].ToString(), Convert.ToInt32(Session["SecrecyLevel"])).FirstOrDefault().UserInfoID;
                List<EduExperience> eduelist = bllEduE.SelectByID(UserID, Convert.ToInt32(Session["SecrecyLevel"]));

                if (eduelist.Count != 0)
                {
                    for (int i = 0; i < eduelist.Count; i++)
                    {
                        string temp = bllUser.FindByUserID(UserID).ToString() + "在" + eduelist[i].StartTime.Value.ToShortDateString() + " ~ " + eduelist[i].EndTime + ",教育单位为" + eduelist[i].EHoldOffice + ",所学专业为" + eduelist[i].Major + "。\n";
                        TextAreaEducations.Text += temp;
                    }

                }
                else
                    TextAreaEducations.Text = "暂无";
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }

        }
        //上传照片
        public void UploadPhotos()
        {
            try
            {
                int UserID = bllUser.FindByLoginName(Session["LoginName"].ToString(), Convert.ToInt32(Session["SecrecyLevel"])).FirstOrDefault().UserInfoID;
                Session["PhotoID"] = bllPhotos.FindPhotoID(UserID);
                if (bllPhotos.FindPhotoID(UserID) == 0)//判断是否有照片，是添加还是更新
                {
                    //先进行附件判断
                    int AttachID = pm.UpLoadPhoto(filePhoto);
                    switch (AttachID)
                    {
                        case 0:
                            Alert.ShowInTop("无效的文件类型！");
                            return;
                        case -1:
                            Alert.ShowInTop("文件名已经存在！");
                            return;
                        case -2:
                            Alert.ShowInTop("文件不能大于5M");
                            return;
                        case -3:
                            Alert.ShowInTop("没有选择照片！");
                            return;
                    }
                    photo.AttachmentID = AttachID;
                    photo.UserInfoID = UserID;
                    photo.SecrecyLevel = Convert.ToInt32(Session["SecrecyLevel"]);
                    photo.EntryPerson = bllUser.FindByLoginName(Session["LoginName"].ToString()).UserName;
                    if (Convert.ToInt32(Session["SecrecyLevel"]) < 5)
                    {
                        photo.IsPass = false;
                        bllPhotos.Insert(photo);//插入照片表
                        log.LoginName = bllUser.FindByLoginName(Session["LoginName"].ToString()).UserName;
                        log.OperationTime = DateTime.Now;
                        log.LoginIP = " ";
                        log.OperationType = "添加";
                        log.OperationContent = "Photos";
                        log.OperationDataID = photo.PhotosID;
                        log.Remark = "";
                        bllOperate.Insert(log);//插入操作日志表
                        Alert.ShowInTop("照片已上传，等待管理员审核！");
                        filePhoto.Reset();
                    }
                    else
                    {
                        photo.IsPass = true;
                        bllPhotos.Insert(photo);//插入照片表
                        //显示照片
                        string FindPath = bllAttachment.FindPath(bllPhotos.FindAttachmentID(photo.PhotosID));
                        imgPhoto.ImageUrl = FindPath;
                        Alert.ShowInTop("照片上传成功！");
                        filePhoto.Reset();
                    }
                }
                else//更新
                {
                    //先进行附件判断
                    int AttachID = pm.UpLoadPhoto(filePhoto);
                    switch (AttachID)
                    {
                        case 0:
                            Alert.ShowInTop("无效的文件类型！");
                            return;
                        case -1:
                            Alert.ShowInTop("文件名已经存在！");
                            return;
                        case -2:
                            Alert.ShowInTop("文件不能大于5M");
                            return;
                        case -3:
                            Alert.ShowInTop("没有选择照片！");
                            return;
                    }
                    if (Convert.ToInt32(Session["SecrecyLevel"]) < 5)
                    {
                        //bllPhotos.UpdateIsPass(Convert.ToInt32(Session["PhotoID"]), false);//原照片状态为false
                        photo.AttachmentID = AttachID;
                        photo.UserInfoID = UserID;
                        photo.SecrecyLevel = Convert.ToInt32(Session["SecrecyLevel"]);
                        photo.EntryPerson = bllPhotos.find(Convert.ToInt32(Session["PhotoID"])).EntryPerson;
                        photo.IsPass = false;
                        bllPhotos.Insert(photo);//插入照片表
                        log.LoginName = bllPhotos.find(Convert.ToInt32(Session["PhotoID"])).EntryPerson;
                        log.OperationTime = DateTime.Now;
                        log.LoginIP = " ";
                        log.OperationType = "更新";
                        log.OperationContent = "Photos";
                        log.OperationDataID = Convert.ToInt32(Session["PhotoID"]);//原照片ID
                        log.Remark = photo.PhotosID.ToString();
                        bllOperate.Insert(log);//插入操作日志表
                        Alert.ShowInTop("照片已上传，等待管理员审核！");
                        filePhoto.Reset();
                    }
                    else
                    {
                        int AttanchmentID = bllPhotos.FindAttachmentID(Convert.ToInt32(Session["PhotoID"]));
                        string path = bllAttachment.FindPath(AttanchmentID);
                        pm.DeleteFile(AttanchmentID, path);
                        photo.PhotosID = Convert.ToInt32(Session["PhotoID"]);
                        photo.AttachmentID = AttachID;
                        photo.UserInfoID = bllUser.FindByLoginName(Session["LoginName"].ToString(), Convert.ToInt32(Session["SecrecyLevel"])).FirstOrDefault().UserInfoID;
                        photo.SecrecyLevel = Convert.ToInt32(Session["SecrecyLevel"]);
                        photo.EntryPerson = Session["LoginName"].ToString();
                        photo.IsPass = true;
                        bllPhotos.Update(photo);//更新照片表
                        //显示照片
                        string FindPath = bllAttachment.FindPath(bllPhotos.FindAttachmentID(photo.PhotosID));
                        imgPhoto.ImageUrl = FindPath;
                        Alert.ShowInTop("照片修改成功！");
                        filePhoto.Reset();
                    }
                }
            }
            catch (Exception ex)
            {
                pm.DeleteFile(Convert.ToInt32(photo.AttachmentID), bllAttachment.FindPath(Convert.ToInt32(photo.AttachmentID)));
                pm.SaveError(ex, this.Request);
            }
        }
        //保存
        protected void Save_Click(object sender, EventArgs e)
        {
            UploadPhotos();
        }
    }
}