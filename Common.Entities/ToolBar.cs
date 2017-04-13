using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities
{
   public  class ToolBar
    {
        public string WebBM { get; set; }//页面唯一编码
         [Required]
        public int ToolBarID { get; set; }//ToolBar唯一ID
        public int? ParentID { get; set; }
        public string TBname { get; set; }//控件名称
        public string TBtext { get; set; }//控件显示文本
        public string TBExplain { get; set; }//功能说明
        public string TBtype { get; set; }//控件类型
        public string TBevent { get; set; }//执行方法
        public bool? Isvisible { get; set; }//是否显示
        public int? TBorder { get; set; }//序号
        public string Tooltip { get; set; }//提示文本
        public string IconUrl { get; set; }
        public string OpenWindow { get; set; }//是点击时弹窗
        public string WindowUrl { get; set; }//弹窗路径
        public string WindwText { get; set; }// 弹窗名称
        public virtual TreeNav TreeNav { get; set; }
    }
}
