namespace Hotel.App.Model.SYS
{
   using System;
   public partial class set_paytype : IEntityBase
   {
      ///<summary>
      ///
      ///</summary>
      public int Id { get; set; }
      ///<summary>
      ///
      ///</summary>
      public string Code { get; set; }
      ///<summary>
      ///
      ///</summary>
      public string Name { get; set; }
      ///<summary>
      ///
      ///</summary>
      public string PayType { get; set; }
      ///<summary>
      ///
      ///</summary>
      public bool IsRecover { get; set; }
      ///<summary>
      ///
      ///</summary>
      public bool IsReturn { get; set; }
      ///<summary>
      ///
      ///</summary>
      public bool IsIntegral { get; set; }
      ///<summary>
      ///
      ///</summary>
      public bool IsDefault { get; set; }
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
