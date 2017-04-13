/*
 * 作者：未知
 * 修改履历：    修改人：吕博扬
 *              修改时间：2015年9月23日
 *              修改内容：设置所有属性可以为空
 */
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Common.Entities
{
    public class Equipment
    {
        public int EquipmentID{ get; set; } //设备id
        [StringLength(50)]
        public string EquipmentName{ get; set; }   //资产名称
        [StringLength(20)]
        public string Purchase{ get; set; } //购买人
        public DateTime? PurchaseTime{ get; set; }  //购置日期
        [StringLength(20)]
        public string Price{ get; set; }    //价格
        [StringLength(50)]
        public string UsePerson{ get; set; }    //使用人
        //public int? AgencyID{ get; set; }   //所属机构
        [StringLength(100)]
        public string StorageLocation { get; set; } //存放地点

        public bool? IsShare{ get; set; }    //是否共享
        public bool? IsGowerProcu{ get; set; }   //是否政府采购
        //[Required]
        public int? SecrecyLevel { get; set; }    //保密级别

        //[Required]
        [StringLength(20)]
        public string EntryPerson { get; set; }   //录入人

        //[Required]
        public bool? IsPass { get; set; }         //是否审核

        [StringLength(50)]
        public string EquipNum { get; set; }   //资产编号


        [StringLength(30)]
        public string MeasurementUnit { get; set; }   //计量单位

        [StringLength(30)]
        public string Manufacturer { get; set; }   //生产厂家

        [StringLength(30)]
        public string Model { get; set; }   //型号

        [StringLength(30)]
        public string ClassNum { get; set; }   //分类号

        [StringLength(30)]
        public string CategoryName { get; set; }   //分类:盘盈设备，盘亏设备

        [StringLength(100)]
        public string Remarks { get; set; }   //备注

        [StringLength(30)]
        public string AgencName { get; set; }   //所属机构
    }
}
