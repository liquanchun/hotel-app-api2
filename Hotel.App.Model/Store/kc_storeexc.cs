namespace Hotel.App.Model.Store
{
   using System;
   public partial class kc_storeexc : IEntityBase
   {
      ///<summary>
      ///
      ///</summary>
      public int Id { get; set; }
      ///<summary>
      ///单号
      ///</summary>
      public string OrderNo { get; set; }
      ///<summary>
      ///
      ///</summary>
      public string StorageOut { get; set; }
      ///<summary>
      ///仓库
      ///</summary>
      public string StorageIn { get; set; }
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
