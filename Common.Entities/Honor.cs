using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Common.Entities
{
    public class Honor
    {
        public int HonorID { get; set; }	//ID
         [Required]
         [StringLength(100)]
        public string TitleName { get; set; }	//称号名称	
         [Required]
         [StringLength(5)]
        public string Sort { get; set; }	//分类(级别)
         [StringLength(200)]
        public string Remark { get; set; }	//备注	       
        public DateTime? GiveTime { get; set; }	//授予时间
         [Required]
        [StringLength(40)]
        public string GivDivision { get; set; }	//授予部门
         [Required]
        public int UserInfoID { get; set; }   //人员ID
         [Required]
        public int? SecrecyLevel { get; set; }//涉密级别
        [Required]
        [StringLength(20)]
        public string EntryPerson { get; set; }//录入人
        [Required]
        public bool? IsPass { get; set; } //是否审核
    }
}
