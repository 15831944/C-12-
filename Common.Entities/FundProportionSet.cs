using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Common.Entities
{
    public class FundProportionSet
    {
        public int FundProportionSetID { get; set; } //ID
        [Required]
        [StringLength(50)]
        public string ProportionName { get; set; }  //比例名称
        [StringLength (10)]
        public string Proportion { get; set; }   //比例
        [Required]
        [StringLength(20)]
        public string ProjectNature { get; set; }   //项目性质
        [StringLength(10)]
        public string Mount { get; set; }  //数量
    }
}
