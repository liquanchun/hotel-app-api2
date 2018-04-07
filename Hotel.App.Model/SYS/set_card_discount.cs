namespace Hotel.App.Model.SYS
{
   using System;
   public partial class set_card_discount : IEntityBase
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
       /// <summary>
       /// 项目ID
       /// </summary>
       public int? ServiceItemId { get; set; }
       ///<summary>
       ///房型ID
       ///</summary>
       public int? HouseTypeId { get; set; }
       /// <summary>
       /// 商品ID
       /// </summary>
       public int? GoodsId { get; set; }
       ///<summary>
       ///积分
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
