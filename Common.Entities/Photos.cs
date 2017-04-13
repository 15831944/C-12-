using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Common.Entities
{
    public class Photos
    {
        public int PhotosID { get; set; }
        [Required]	
        public int? AttachmentID { get; set; }//附件ID
        [Required]
        public int? UserInfoID { get; set; }   //人员ID
        [Required]
        public int? SecrecyLevel { get; set; }//涉密级别
        [Required]
        [StringLength(20)]
        public string EntryPerson { get; set; }//录入人
        [Required]
        public bool? IsPass { get; set; } //是否审核
    }
}
