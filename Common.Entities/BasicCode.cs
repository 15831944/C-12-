using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Common.Entities
{
    public class BasicCode
    {
        public int BasicCodeID { get; set; }	//ID 	
        [Required]
        public int CategoryID { get; set; }	//分类编号	
         [Required]
        [StringLength(20)]
        public string CategoryName { get; set; }	//分类名称
        [Required]
        [StringLength(100)]
        public string CategoryContent { get; set; }	//分类内容	
    }
}
