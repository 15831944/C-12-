using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Common.Entities
{
    public class OperationLog
    {
        public int OperationLogID { get; set; }	//	ID
         [Required]
        [StringLength (20)]
        public string LoginName { get; set; }   //用户登录名 
        [StringLength(15)]
        public string LoginIP { get; set; }	//	登录IP
         [Required]
        [StringLength(10)]
        public string OperationType { get; set; }	//	操作类型
         [Required]
        [StringLength(100)]
        public string OperationContent { get; set; }	//	操作内容
        [Required]
         public int? OperationDataID { get; set; } //操作数据行的ID
         [Required]
        public DateTime? OperationTime { get; set; }	//	操作时间
        [StringLength(200)]
        public string Remark { get; set; }	//	备注
        [Required]
        public bool? IsPass { get; set; } //是否审核
    }
}
