namespace Hotel.App.Model.SYS
{
   using System;
   public partial class set_hourhouse_price : IEntityBase
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
      ///时长
      ///</summary>
      public int Hours { get; set; }
      ///<summary>
      ///计半价时长
      ///</summary>
      public int HalfPriceHours { get; set; }
      ///<summary>
      ///入住时间起
      ///</summary>
      public string CheckInTime1 { get; set; }
      ///<summary>
      ///入住时间止
      ///</summary>
      public string CheckInTime2 { get; set; }
      ///<summary>
      ///加收缓冲时间
      ///</summary>
      public int AddBuffTime { get; set; }
      ///<summary>
      ///超过多少分钟转正常
      ///</summary>
      public int TurnNormal { get; set; }
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
