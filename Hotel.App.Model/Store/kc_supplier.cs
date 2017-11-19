namespace Hotel.App.Model.Store
{
   using System;
   public partial class kc_supplier : IEntityBase
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
      ///
      ///</summary>
      public string LinkMan { get; set; }
      ///<summary>
      ///
      ///</summary>
      public string Tel { get; set; }
      ///<summary>
      ///
      ///</summary>
      public string Address { get; set; }
      ///<summary>
      ///
      ///</summary>
      public string BankName { get; set; }
      ///<summary>
      ///
      ///</summary>
      public string BankAcc { get; set; }
      ///<summary>
      ///
      ///</summary>
      public string BankAccNo { get; set; }
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
