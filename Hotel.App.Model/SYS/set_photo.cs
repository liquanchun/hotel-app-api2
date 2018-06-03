namespace Hotel.App.Model.SYS
{
   using System;
   public partial class set_photo : IEntityBase
   {
      ///<summary>
      ///
      ///</summary>
      public int Id { get; set; }
      /// <summary>
      /// 
      /// </summary>
      public string TypeName { get; set; }
      ///<summary>
      ///
      ///</summary>
      public string FileName { get; set; }
      ///<summary>
      ///
      ///</summary>
      public string Tags { get; set; }
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
