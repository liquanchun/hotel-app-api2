namespace Hotel.App.Model.SYS
{
   using System;
   public partial class sys_user : IEntityBase
   {
      ///<summary>
      ///
      ///</summary>
      public int Id { get; set; }
      ///<summary>
      ///
      ///</summary>
      public string UserId { get; set; }
      ///<summary>
      ///
      ///</summary>
      public string UserName { get; set; }
      ///<summary>
      ///
      ///</summary>
      public string Mobile { get; set; }
      ///<summary>
      ///微信
      ///</summary>
      public string Weixin { get; set; }
      ///<summary>
      ///
      ///</summary>
      public string Email { get; set; }
      ///<summary>
      ///
      ///</summary>
      public string Pwd { get; set; }
      ///<summary>
      ///
      ///</summary>
      public DateTime LastLoginTime { get; set; }
      ///<summary>
      ///
      ///</summary>
      public int OrgId { get; set; }
      ///<summary>
      ///
      ///</summary>
      public string RoleIds { get; set; }
      ///<summary>
      ///
      ///</summary>
      public DateTime UpdatedAt { get; set; }
      ///<summary>
      ///
      ///</summary>
      public DateTime CreatedAt { get; set; }
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
