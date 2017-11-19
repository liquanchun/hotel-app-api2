namespace Hotel.App.Model.SYS
{
   using System;
   public partial class set_inte_exchange : IEntityBase
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
      ///兑换类型
      ///</summary>
      public string ExchangeType { get; set; }
      ///<summary>
      ///
      ///</summary>
      public int CardType { get; set; }
      ///<summary>
      ///礼品名称
      ///</summary>
      public string GiftName { get; set; }
      ///<summary>
      ///兑换所需积分
      ///</summary>
      public int ExchangeInte { get; set; }
      ///<summary>
      ///活动开始日期
      ///</summary>
      public string StartDate { get; set; }
      ///<summary>
      ///活动结束日期
      ///</summary>
      public string EndDate { get; set; }
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
