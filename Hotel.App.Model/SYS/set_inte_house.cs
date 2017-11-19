namespace Hotel.App.Model.SYS
{
   using System;
   public partial class set_inte_house : IEntityBase
   {
      ///<summary>
      ///
      ///</summary>
      public int Id { get; set; }
      ///<summary>
      ///
      ///</summary>
      public string Name { get; set; }
      ///<summary>
      ///所需积分
      ///</summary>
      public int TakeInte { get; set; }
      ///<summary>
      ///会员类型
      ///</summary>
      public int CardType { get; set; }
      ///<summary>
      ///兑换房型
      ///</summary>
      public int HouseType { get; set; }
      ///<summary>
      ///有效星期
      ///</summary>
      public string UseWeeks { get; set; }
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
