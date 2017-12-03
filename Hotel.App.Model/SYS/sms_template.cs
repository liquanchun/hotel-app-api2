namespace Hotel.App.Model.SYS
{
   using System;
   public partial class sms_template : IEntityBase
   {
      ///<summary>
      ///
      ///</summary>
      public int Id { get; set; }
      ///<summary>
      ///模板名称
      ///</summary>
      public string tmp_name { get; set; }
      ///<summary>
      ///
      ///</summary>
      public string to_business { get; set; }
      ///<summary>
      ///
      ///</summary>
      public string tmp_content { get; set; }
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
