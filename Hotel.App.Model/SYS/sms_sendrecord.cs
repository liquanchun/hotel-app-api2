namespace Hotel.App.Model.SYS
{
   using System;
   public partial class sms_sendrecord : IEntityBase
   {
      ///<summary>
      ///
      ///</summary>
      public int Id { get; set; }
      ///<summary>
      ///
      ///</summary>
      public string Mobile { get; set; }
      ///<summary>
      ///
      ///</summary>
      public string Content { get; set; }
      ///<summary>
      ///
      ///</summary>
      public string Business { get; set; }
      ///<summary>
      ///
      ///</summary>
      public string Status { get; set; }
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
