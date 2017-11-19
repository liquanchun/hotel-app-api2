namespace Hotel.App.Model.SYS
{
   using System;
   public partial class set_integral : IEntityBase
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
      ///方式类型
      ///</summary>
      public string InteType { get; set; }
      ///<summary>
      ///会员卡类型
      ///</summary>
      public int CardType { get; set; }
      ///<summary>
      ///天数/金额
      ///</summary>
      public int DayOrFee { get; set; }
      ///<summary>
      ///积分
      ///</summary>
      public int Integral { get; set; }
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
