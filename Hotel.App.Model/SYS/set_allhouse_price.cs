namespace Hotel.App.Model.SYS
{
   using System;
   public partial class set_allhouse_price : IEntityBase
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
      ///首日计半价时长
      ///</summary>
      public int HalfPriceHours { get; set; }
      ///<summary>
      ///首日计全价时长
      ///</summary>
      public int AllPriceHours { get; set; }
      ///<summary>
      ///退房时间
      ///</summary>
      public string LeaveTime { get; set; }
      ///<summary>
      ///加收缓冲时长
      ///</summary>
      public int AddFeeHours { get; set; }
      ///<summary>
      ///加收方式
      ///</summary>
      public string AddFeeType { get; set; }
      ///<summary>
      ///固定加收全日租
      ///</summary>
      public string AddAllDay { get; set; }
      ///<summary>
      ///加租全日租时长
      ///</summary>
      public int AddAllHours { get; set; }
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
