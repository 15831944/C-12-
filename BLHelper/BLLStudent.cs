/**编写人：方淑云
 * 时间：2014年6月20号
 * 功能:学生情况表的相关操作
 * 修改履历： 1.时间：8月11日
 *           修改人：张凡凡
 *           修改内容：添加更新IsPass状态函数UpdateIsPass(int,bool)
 *           2.时间：8月13日
 *           修改人：王会会
 *           修改内容：学生情况权限为1级公开，去掉部分函数相关的等级;添加根据是否毕业和学生类型查询相关信息FindStudent(string type, bool? IsGraduation);
 *           3.时间：10月8日
 *           修改人：吕博扬
 *           修改内容：添加按所属部门、学生类型查询的函数
 **/
using Common.Entities;
using DataBase;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLHelper
{
    public class BLLStudent
    {
        DataBaseContext dbcontext = new DataBaseContext();

        //根据指导教师查询
        public List<Student> FindByTeacher(int teacher)
        {
            try
            {
                if (teacher != 0)
                {
                    return dbcontext.StudentContext.Where(s => s.UserInfoID == teacher && s.IsPass == true).ToList();
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                throw;
            }
        }

        //根据学生姓名查询
        public List<Student> FindByName(string name)
        {
            try
            {
                if (name != null)
                {
                    return dbcontext.StudentContext.Where(s => s.Sname == name && s.IsPass == true).ToList();
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                throw;
            }
        }

        //根据所属部门查询
        public List<Student> FindByDepartment(int departmentID)
        {
            try
            {
                if (departmentID != null)
                {
                    return dbcontext.StudentContext.Where(s => s.AgencyID == departmentID && s.IsPass == true).ToList();
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                throw;
            }
        }

        //根据学生类型查询
        public List<Student> FindByStudentType(string type)
        {
            try
            {
                if (type != null)
                {
                    return dbcontext.StudentContext.Where(s => s.Type == type && s.IsPass == true).ToList();
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                throw;
            }
        }

        //根据学生专业查询
        public List<Student> FindByMajor(string major)
        {
            try
            {
                if (major != null)
                {
                    return dbcontext.StudentContext.Where(s => s.Specialty == major && s.IsPass == true).ToList();
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                throw;
            }
        }

        //更新IsPass状态
        public void UpdateIsPass(int ID, bool isPass)
        {
            try
            {
                Student Student = dbcontext.StudentContext.Find(ID);
                if (Student == null)
                    return;
                Student.IsPass = isPass;
                dbcontext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }       
        //为某用户添加学生信息
        public bool InsertForPeople(Student student)
        {
            try
            {
                if (student != null)
                {
                  
                    dbcontext.StudentContext.Add(student);
                    dbcontext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                throw e;
            }
        }

        //根据StudentID查找学生
        public Student FindStudents(int StudentID)
        {
            if (StudentID != null)
            {
                return dbcontext.StudentContext.Where(t => t.StudentID == StudentID).FirstOrDefault();
            }
            else
            {
                return null;
            }
        }
       
        //判断学号是否存在
        public Student IsSno(string Sno)
        {
            if (Sno != null)
            {
                return dbcontext.StudentContext.Where(u => u.Sno == Sno).FirstOrDefault();
            }
            else
            {
                return null;
            }
        }
        //根据学生名查找StudentID
        public int SelectByStudentID(string Sno)
        {
            if (!string.IsNullOrEmpty(Sno))
            {
                var results = dbcontext.StudentContext.Where(t => t.Sno == Sno).Select(u => new { u.StudentID }).ToList ();
                if (results.Count() != 0)
                {
                    if (results.FirstOrDefault().StudentID  != 0)
                    {
                        return results.FirstOrDefault().StudentID;
                    }
                    else
                        return 0;
                }
                else
                    return 0;
            }
            else
            {
                return 0;
            }
        }
        //根据人员ID和学生状态查看学生信息
        public List<Student> FindByIS(int UserInfoID, bool? IsGraduation, string Type)
        {
            if (UserInfoID != null && IsGraduation != null && Type != null)
            {
                return dbcontext.StudentContext.Where(s => s.UserInfoID == UserInfoID && s.IsGraduation == IsGraduation && s.Type == Type && s.IsPass == true).ToList();
            }
            else
            {
                return null;
            }
        }
        //更新用户学生情况
        public bool Update(Student student)
        {
            try
            {
                if (student != null)
                {
                    Student newstudent = dbcontext.StudentContext.Find(student.StudentID);
                    newstudent.UserInfoID = student.UserInfoID;
                    newstudent.StudentID = student.StudentID;
                    newstudent.Sno = student.Sno;
                    newstudent.Sname = student.Sname;
                    newstudent.Sex = student.Sex;
                    newstudent.DocumentType = student.DocumentType;
                    newstudent.DocumentNumber = student.DocumentNumber;
                    newstudent.Contact = student.Contact;
                    newstudent.IsGraduation = student.IsGraduation;
                    newstudent.Specialty = student.Specialty;
                    newstudent.SResearch = student.SResearch;
                    newstudent.SGraduationDirection = student.SGraduationDirection;
                    newstudent.Type = student.Type;
                    newstudent.EnterTime = student.EnterTime;
                    newstudent.GraduationTime = student.GraduationTime;
                    newstudent.SecrecyLevel = student.SecrecyLevel;
                    newstudent.AgencyID = student.AgencyID;
                    dbcontext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                throw e;
            }
        }
        //删除用户学生情况
        public bool Delete(int  studentid)
        {
            try
            {
                Student student = dbcontext.StudentContext.Where(u => u.StudentID == studentid).FirstOrDefault();
                    dbcontext.StudentContext.Attach(student);
                    dbcontext.StudentContext.Remove(student);
                    dbcontext.SaveChanges();
                    return true;
              
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                throw e;
            }
        }
        //分页
        public List<Student> FindPaged( int? SecrecyLevel)
        {
            return dbcontext.StudentContext.Where(u => u.SecrecyLevel <= SecrecyLevel && u.IsPass ==true).ToList();
        }
        //根据是否毕业和学生类型查询学生情况
        public List<Student> FindStudent(string type, bool? IsGraduation)
        {
            return dbcontext.StudentContext.Where(u => u.Type == type && u.IsGraduation == IsGraduation && u.IsPass ==true).ToList();
        }
        //根据是否毕业和学生类型,学生姓名查询学生情况
        public List<Student> FindStudentName(string type,bool? IsGraduation, string Sname)
        {
            return dbcontext.StudentContext.Where(u => u.Type == type && u.IsGraduation == IsGraduation && u.Sname.Contains(Sname) && u.IsPass == true).ToList();
        }
        //根据学号判断授课老师是否存在自学生            
        public Student IsSnoAndTeacher(string Sno ,int UserID)
        {
            if (Sno != null)
            {
                return dbcontext.StudentContext.Where(u => u.Sno == Sno &&  u.UserInfoID == UserID).FirstOrDefault();
            }
            else
            {
                return null; 
            }
        }
        //根据人员ID查找StudentID
        public List<int> FindStudentIDList(int UserID)
        {
            List<int> list = new List<int>();
            var newlist = dbcontext.StudentContext.Where(a => a.UserInfoID == UserID && a.IsPass == true).ToList();
            for (int i = 0; i < newlist.Count(); i++)
            {
                list.Add(Convert.ToInt32(newlist[i].StudentID));
            }
            return list;
        }
    }
}
