namespace Hotel.App.Model.SYS
{
   using System;
   public partial class set_rent : IEntityBase
   {
      ///<summary>
      ///
      ///</summary>
      public int Id { get; set; }
      ///<summary>
      ///
      ///</summary>
      public string GoodsName { get; set; }
      ///<summary>
      ///
      ///</summary>
      public decimal RentPrice { get; set; }
      ///<summary>
      ///
      ///</summary>
      public decimal Compensation { get; set; }
      ///<summary>
      ///
      ///</summary>
      public int Amount { get; set; }
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
