namespace Hotel.App.Model.SYS
{
   using System;
   public partial class set_card_upgrade : IEntityBase
   {
      ///<summary>
      ///
      ///</summary>
      public int Id { get; set; }
      ///<summary>
      ///
      ///</summary>
      public int OldCard { get; set; }
      ///<summary>
      ///
      ///</summary>
      public int NewCard { get; set; }
      ///<summary>
      ///升级所需积分
      ///</summary>
      public int NeedInte { get; set; }
      ///<summary>
      ///升级消耗积分
      ///</summary>
      public int TakeInte { get; set; }
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
