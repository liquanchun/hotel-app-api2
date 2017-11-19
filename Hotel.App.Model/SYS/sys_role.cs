namespace Hotel.App.Model.SYS
{
   using System;
   public partial class sys_role : IEntityBase
   {
      ///<summary>
      ///
      ///</summary>
      public int Id { get; set; }
      ///<summary>
      ///
      ///</summary>
      public string RoleName { get; set; }
      ///<summary>
      ///
      ///</summary>
      public string RoleDesc { get; set; }
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
      public string CreatedBy { get; set; }
      ///<summary>
      ///
      ///</summary>
      public bool IsValid { get; set; }
   }
}
