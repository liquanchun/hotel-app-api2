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
      ///兑换项目
      ///</summary>
      public string ItemName { get; set; }
      ///<summary>
      ///兑换类型
      ///</summary>
      public string ExchangeType { get; set; }
      ///<summary>
      ///会员ID
      ///</summary>
      public int CustomerId { get; set; }
      ///<summary>
      ///兑换时间
      ///</summary>
      public DateTime ExchangeTime { get; set; }
      ///<summary>
      ///兑换所需积分
      ///</summary>
      public int ExchangeInte { get; set; }
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
