namespace Hotel.App.Model.SYS
{
   using System;
   public partial class set_card : IEntityBase
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
      ///级别
      ///</summary>
      public int Level { get; set; }
      ///<summary>
      ///卡费
      ///</summary>
      public int CardFee { get; set; }
      ///<summary>
      ///是否可充值
      ///</summary>
      public bool IsRecharge { get; set; }
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
