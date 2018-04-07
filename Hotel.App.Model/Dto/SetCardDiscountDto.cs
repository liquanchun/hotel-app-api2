//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Hotel.App.Model.SYS
{
    using System;
    using System.Collections.Generic;
    
    public partial class SetCardDiscountDto
    {
        ///<summary>
        ///
        ///</summary>
        public int Id { get; set; }
        ///<summary>
        ///活动名称
        ///</summary>
        public string Name { get; set; }
        ///<summary>
        ///会员卡类型
        ///</summary>
        public int CardTypeId { get; set; }
        public string CardTypeTxt { get; set; }
        /// <summary>
        /// 项目ID
        /// </summary>
        public int? ServiceItemId { get; set; }
        public string ServiceItemTxt { get; set; }
        ///<summary>
        ///房型ID
        ///</summary>
        public int? HouseTypeId { get; set; }
        public string HouseTypeTxt { get; set; }

        ///<summary>
        ///房型ID
        ///</summary>
        public int? GoodsId { get; set; }
        public string GoodsTxt { get; set; }
        ///<summary>
        ///折扣
        ///</summary>
        public decimal Discount { get; set; }
        ///<summary>
        ///活动开始日期
        ///</summary>
        public DateTime StartDate { get; set; }
        ///<summary>
        ///活动结束日期
        ///</summary>
        public DateTime EndDate { get; set; }
        ///<summary>
        ///
        ///</summary>
        public string Remark { get; set; }
        ///<summary>
        ///
        ///</summary>
        public DateTime CreatedAt { get; set; }
        ///<summary>
        ///
        ///</summary>
        public DateTime UpdatedAt { get; set; }
        ///<summary>
        ///
        ///</summary>
        public bool IsValid { get; set; }
        ///<summary>
        ///
        ///</summary>
        public string CreatedBy { get; set; }
    }
}
