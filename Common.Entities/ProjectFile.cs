/*
 *  编写人：吕博扬
 *  时间：2015年12月5日
 *  功能：项目-项目全部信息 相关文件信息类
 *  修改履历：   暂无
*/
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Common.Entities
{
    public class ProjectFile
    {
        //文件ID
        public int ProjectFileID { set; get; }

        //项目ID
        [Required]
        public int ProjectID { set; get; }
        
        //文件编号
        [StringLength(20)]
        public string FileCode { set; get; }

        //文件名称
        [StringLength(20)]
        public string FileName { set; get; }

        //文件类型
        [StringLength(5)]
        public string FileType { set; get; }

        //附件ID
        [Required]
        public int AttachmentID { set; get; }

        //是否审核
        [Required]
        public bool IsPass { get; set; }

        //涉密级别
        [Required]
        public int SecrecyLevel { get; set; }

        //录入人
        [StringLength(20)]
        public string EntryPerson { get; set; }
    }
}
