/**编写人：张凡凡
 * 时间：2014年8月16号
 * 功能：上传，下载的相关操作
 * 修改履历：    1、修改人：吕博杨
 *                 修改时间：2015年11月29日
 *                 修改内容：增加下载图片函数，修改了上传图片函数的BUG
 */
using FineUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Telerik.WebControls;

namespace BLCommon
{
    public class PublicMethod
    {
      
        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="aFilepath"></param>
        public void DownloadFile(string aFilepath)
        {
            FileInfo file = new System.IO.FileInfo(aFilepath);
            HttpContext.Current.Response.AddHeader("Content-Length", file.Length.ToString());
            HttpContext.Current.Response.ContentType = "application/ms-download";
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader("Content-Type", "application/octet-stream");
            HttpContext.Current.Response.Charset = "utf-8";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode(file.Name, System.Text.Encoding.UTF8));
            HttpContext.Current.Response.WriteFile(file.FullName);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// lby 下载图片
        /// </summary>
        /// <param name="photoPath"></param>
        public void DownloadPhoto(string photoPath)
        {
            FileInfo photo = new System.IO.FileInfo(photoPath);
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.Buffer = false;
            HttpContext.Current.Response.ContentType = "application/octet-stream";
            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(photo.Name, System.Text.Encoding.ASCII));
            HttpContext.Current.Response.AppendHeader("Content-Length", photo.Length.ToString());
            HttpContext.Current.Response.WriteFile(photo.FullName);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }

        //上传大文件,返回附件id
        public int UpLoad(System.Web.UI.WebControls.FileUpload file)
        {
            BLHelper.BLLAttachment BLLattachment = new BLHelper.BLLAttachment();

            foreach (UploadedFile files in RadUploadContext.Current.UploadedFiles)
            {
                string Path = System.Web.HttpContext.Current.Server.MapPath(@"Uploads");
                string fileName = files.GetName().ToString();

                string Filename = DateTime.Now.Ticks.ToString() + "_" + files.GetName().ToString();
                //如果路径不存在，则创建
                if (System.IO.Directory.Exists(Path) == false)
                {
                    System.IO.Directory.CreateDirectory(Path);
                }
                if (!BLLattachment.IsAttachmentName(fileName))
                {
                    Common.Entities.Attachment attachment = new Common.Entities.Attachment();
                    //向附件表中插入数据
                    attachment.FileName = fileName;
                    attachment.FilePath = Path + Filename;
                    Path = Path + "/" + Filename;
                    //保存
                    files.SaveAs(Path, true);

                    BLLattachment.Insert(attachment);
                    return attachment.AttachmentID;
                }
                else
                {
                    //该文件名已存在
                    return 0;
                }
            }
            //没存 
            return -1;

        }

        //上传,返回附件id
        public int UpLoad(FileUpload file)
        {
            string filepath = "~/Uploads/";
            BLHelper.BLLAttachment BLLattachment = new BLHelper.BLLAttachment();
            if (file.HasFile)
            {
                if (file.PostedFile.ContentLength < 5120000)
                {
                    string fileName = file.ShortFileName;
                    string fileExtension = Path.GetExtension(fileName);
                    

                    if (!ValidateFileType(fileExtension))
                    {
                        return 0;
                    }
                    if (Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(filepath)) == false)//如果不存在就创建file文件夹
                    {
                        Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(filepath));
                    }

                    fileName = fileName.Replace(":", "_").Replace(" ", "_").Replace("\\", "_").Replace("/", "_");
                    string FileName = DateTime.Now.Ticks.ToString() + "_" + fileName;

                    if (!BLLattachment.IsAttachmentName(fileName))
                    {
                        Common.Entities.Attachment attachment = new Common.Entities.Attachment();
                        //向附件表中插入数据
                        attachment.FileName = fileName;
                        attachment.FilePath = System.Web.HttpContext.Current.Server.MapPath(filepath) + FileName;
                        file.SaveAs(System.Web.HttpContext.Current.Server.MapPath(filepath) + FileName);
                        BLLattachment.Insert(attachment);
                        return Convert.ToInt32(attachment.AttachmentID);
                    }
                    else
                    {
                        //该文件名已存在
                        return -1;
                    }
                }
                else
                {
                    //上传的文件不能大于50M
                    return -2;
                }
            }
            else
                return -3;
        }

        //判断文件类型
        public bool ValidateFileType(string str)
        {
            bool isFileType = false;
            string thestr = str.ToLower();

            string[] allowExtension = { ".txt", ".xls", ".doc", ".pdf", ".jpg",".zip",".rar",".ppt",".png",".docx",".xlsx" };
            //对上传的文件的类型进行一个个匹对
            for (int i = 0; i < allowExtension.Length; i++)
            {
                if (thestr == allowExtension[i])
                {
                    isFileType = true;
                    break;
                }
            }
            return isFileType;
        }

        //删除文件
        public void DeleteFile(int AttachId, string path)
        {
            BLHelper.BLLAttachment BLLattachment = new BLHelper.BLLAttachment();

            if (File.Exists(path))
            {
                FileInfo file = new FileInfo(path);
                if (file.Attributes.ToString().IndexOf("ReadOnly") != -1)
                    file.Attributes = FileAttributes.Normal;
                File.Delete(path);
            }
            BLLattachment.Delete(AttachId);
        }
        //上传照片
        public int UpLoadPhoto(FileUpload file)
        {
            string filepath = "~/Uploads/";
            //UpLoadFile(file, filepath, "wang", true);
            BLHelper.BLLAttachment BLLattachment = new BLHelper.BLLAttachment();
            if (file.HasFile)
            {
                if (file.PostedFile.ContentLength < 5120000)
                {
                    string fileName = file.ShortFileName;
                    string fileExtension = Path.GetExtension(fileName);
                    if (!ValidateFileTypePhotos(fileExtension))
                    {
                        return -1;
                    }
                    if (Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(filepath)) == false)//如果不存在就创建file文件夹
                    {
                        Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(filepath));
                    }
                    fileName = fileName.Replace(":", "_").Replace(" ", "_").Replace("\\", "_").Replace("/", "_");
                    string FileName = DateTime.Now.Ticks.ToString() + "_" + fileName;

                    if (!BLLattachment.IsAttachmentName(fileName))
                    {
                        Common.Entities.Attachment attachment = new Common.Entities.Attachment();
                        //向附件表中插入数据
                        attachment.FileName = fileName;
                        attachment.FilePath = System.Web.HttpContext.Current.Server.MapPath(filepath) + FileName;
                        file.SaveAs(System.Web.HttpContext.Current.Server.MapPath(filepath) + FileName);
                        BLLattachment.Insert(attachment);
                        return Convert.ToInt32(attachment.AttachmentID);
                    }
                    else
                    {
                        //该文件名已存在
                        return 0;
                    }
                }
                else
                {
                    //上传的文件不能大于50M
                    return -2;
                }
            }
            else
                return -3;
        }
        
        //判断照片类型
        public bool ValidateFileTypePhotos(string str)
        {
            bool isFileType = false;
            string thestr = str.ToLower();

            string[] allowExtension = {".jpg", ".gif","png"};
            //对上传的文件的类型进行一个个匹对
            for (int i = 0; i < allowExtension.Length; i++)
            {
                if (thestr == allowExtension[i])
                {
                    isFileType = true;
                    break;
                }
            }
            return isFileType;
        }

        //判断grid选中行
        public List<int> GridCount(Grid grid, CheckBoxField checkbox)
        {
            int m;
            //取整数（不是四舍五入，全舍）
            int Pages = (int)Math.Floor(Convert.ToDouble(grid.RecordCount / grid.PageSize));
            List<int> selections = new List<int>();
            if (grid.PageIndex == Pages)
                m = (grid.RecordCount - grid.PageSize * grid.PageIndex);
            else
                m = grid.PageSize;
            for (int i = 0; i < m; i++)
            {
                if (checkbox.GetCheckedState(i))
                    selections.Add(i);
            }
            return selections;
        }
       

        //文件上传
        public FileUpLoad UpLoadFile(HtmlInputFile InputFile)
        {
            //文件路径
            string filepath = "~/Uploads/";

            FileUpLoad fp = new FileUpLoad();
            string fileName, fileExtension;
            // 
            //建立上传对象 
            // 
            HttpPostedFile postedFile = InputFile.PostedFile;

            fileName = System.IO.Path.GetFileName(postedFile.FileName);
            fileExtension = System.IO.Path.GetExtension(fileName);

            if (fileName == "")
            {
                fp.Attachid = -3;
                return fp;
            }
            //上传文件大小
            int i = 100;
            if (InputFile.PostedFile.ContentLength > i * 1024 * 1024)
            {
                fp.Attachid = -2;
                return fp;
            }
            //根据类型确定文件格式 
            if (!ValidateFileType(fileExtension))
            {
                //无效的文件类型
                fp.Attachid = -1;
                return fp;
            }
            string phyPath = HttpContext.Current.Request.MapPath(filepath);

            //判断路径是否存在,若不存在则创建路径 
            DirectoryInfo upDir = new DirectoryInfo(phyPath);
            if (!upDir.Exists) 
            {
                upDir.Create();
            }

            // 
            //保存文件 
            // 
            try
            {
                BLHelper.BLLAttachment blat = new BLHelper.BLLAttachment();
                if (!blat.IsAttachmentName(fileName))
                {
                    fp.FilePath = HttpContext.Current.Request.MapPath(filepath) + fileName;
                    fp.FileExtension = fileExtension;
                    fp.FileName = fileName;
                    fileName = fileName.Replace(":", "_").Replace(" ", "_").Replace("\\", "_").Replace("/", "_");
                    string Filename = DateTime.Now.Ticks.ToString() + "_" + fileName;

                    //插入附件表
                    Common.Entities.Attachment at = new Common.Entities.Attachment();
                    at.FileName = System.IO.Path.GetFileName(postedFile.FileName);
                    at.FilePath = HttpContext.Current.Request.MapPath(filepath) + Filename;
                    blat.Insert(at);
                    fp.Attachid = at.AttachmentID;
                    postedFile.SaveAs(phyPath + Filename);
                    return fp;
                }
                else
                {
                    fp.Attachid = 0;
                    return fp;
                }

            }
            catch
            {
                throw new ApplicationException("上传失败!");
            }
        }



        //导出全部
        public void ExportExcel(int k, Grid grid, int j)
        {
            List<int> rownumber = null;
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=myexcel.xls");
            HttpContext.Current.Response.ContentType = "application/excel";
            HttpContext.Current.Response.Charset = "GB2312";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            HttpContext.Current.Response.Write(GetGridTableHtml(k, grid, j, 0, rownumber));
            HttpContext.Current.Response.End();
        }

        //导出所选行
        public void ExportExcel(int k, Grid grid, int j, List<int> rownum)
        {
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=myexcel.xls");
            HttpContext.Current.Response.ContentType = "application/excel";
            HttpContext.Current.Response.Charset = "GB2312";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            HttpContext.Current.Response.Write(GetGridTableHtml(k, grid, j, 1, rownum));
            HttpContext.Current.Response.End();
        }

        public string GetGridTableHtml(int k, Grid grid, int j, int flag, List<int> rownum) //flag=0全部 flag = 1所选行
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table cellspacing=\"0\" rules=\"all\" border=\"1\" style=\"border-collapse:collapse;\">");
            sb.Append("<tr>");
            for (int i = k; i < grid.Columns.Count - j; i++)
            {
                sb.AppendFormat("<td>{0}</td>", grid.Columns[i].HeaderText);
            }
            sb.Append("</tr>");
            List<int> mo = new List<int>();
            if (flag == 0)
            {
                for (int le = 0; le < grid.Rows.Count; le++)
                    mo.Add(le);
                rownum = mo;
            }

            for (int line = 0; line < rownum.Count; line++ )
            //foreach (GridRow row in grid.Rows)
            {
                GridRow row = grid.Rows[rownum[line]];
                sb.Append("<tr>");
                for (int i = k; i < grid.Columns.Count - j; i++)
                {
                    object value = row.Values[i];
                    string html = value.ToString();
                    double num = 0.0;
                    if (double.TryParse(html, out num) && html.Length > 11)
                        html += ";";
                    if (html.StartsWith(Grid.TEMPLATE_PLACEHOLDER_PREFIX))
                    {

                        // 模板列
                        string templateID = html.Substring(Grid.TEMPLATE_PLACEHOLDER_PREFIX.Length);
                        Control templateCtrl = row.FindControl(templateID);
                        html = GetRenderedHtmlSource(templateCtrl);
                    }
                    else
                    {
                        // 处理CheckBox
                        if (html.Contains("f-grid-static-checkbox"))
                        {
                            if (html.Contains("uncheck"))
                            {
                                html = "×";
                            }
                            else
                            {
                                html = "√";
                            }
                        }


                    }

                    sb.AppendFormat("<td>{0}</td>", html);
                }
                sb.Append("</tr>");
            }

            sb.Append("</table>");

            return sb.ToString();
        }

        /// <summary>
        /// 获取控件渲染后的HTML源代码
        /// </summary>
        /// <param name="ctrl"></param>
        /// <returns></returns>
        public string GetRenderedHtmlSource(Control ctrl)
        {
            if (ctrl != null)
            {
                using (StringWriter sw = new StringWriter())
                {
                    using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                    {
                        ctrl.RenderControl(htw);

                        return sw.ToString();
                    }
                }
            }
            return String.Empty;
        }

        //写错误日志
        public void SaveError(Exception ex, HttpRequest hr)
        {
            string path = "~/ErrorLog/";
            //判断路径是否存在,若不存在则创建路径 
            DirectoryInfo upDir = new DirectoryInfo(HttpContext.Current.Request.MapPath(path));
            if (!upDir.Exists)
            {
                upDir.Create();
            }
            string filename = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + "Error.txt";
            
            using (StreamWriter writer = new StreamWriter(HttpContext.Current.Request.MapPath(path) + filename, true))
            {
                writer.WriteLine(DateTime.Now.ToString());
                writer.WriteLine("------------------------------------------------------------------------------");
                writer.WriteLine("\n错误消息:" + ex.Message);
                writer.WriteLine("\n导致错误的应用程序或对象的名称:" + ex.Source);
                writer.WriteLine("\n堆栈内容:" + ex.StackTrace);
                writer.WriteLine(" ");
                writer.WriteLine("\n引发异常的方法:" + ex.TargetSite);
                writer.WriteLine("\n错误页面:" + hr.RawUrl);
                writer.WriteLine("------------------------------------------------------------------------------");
            }

        }

        //导入错误日志
        public void SaveImportError(string Error)
        {
            try
            {
                //string filename = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + "ImportError.doc";
                //HttpContext.Current.Response.ClearContent();
                //HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=myexcel.xls");
                //HttpContext.Current.Response.ContentType = "application/excel";
                //HttpContext.Current.Response.Charset = "GB2312";
                //HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
                //HttpContext.Current.Response.Write(Error);
                //HttpContext.Current.Response.End();
                //HttpContext.Current.Response.ClearContent();
                //HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + filename);
                //HttpContext.Current.Response.ContentType = "application/word";
                //HttpContext.Current.Response.Charset = "GB2312";
                //HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
                //HttpContext.Current.Response.Write(Error);
                //HttpContext.Current.Response.End();
                //HttpContext.Current.Response.Clear();
                //HttpContext.Current.Response.Buffer = true;
                //HttpContext.Current.Response.Charset = "GB2312";  //设置了类型为中文防止乱码的出现 
                //HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename);
                //HttpContext.Current.Response.ContentType = "application/ms-word;charset=GB2312";
                //HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312"); //设置输出流为简体中文
                //HttpContext.Current.Response.Write(Error);
                //HttpContext.Current.ApplicationInstance.CompleteRequest();

                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.Charset = "GB2312";
                HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=abc.txt");
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");//设置输出流为简体中文
                HttpContext.Current.Response.ContentType = "text/plain";//设置输出文件类型为txt文件。 
                System.Globalization.CultureInfo myCItrad = new System.Globalization.CultureInfo("ZH-CN", true);
                System.IO.StringWriter oStringWriter = new System.IO.StringWriter(myCItrad);
                HttpContext.Current.Response.Write(Error);
                HttpContext.Current.Response.End();
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }
    }
}
