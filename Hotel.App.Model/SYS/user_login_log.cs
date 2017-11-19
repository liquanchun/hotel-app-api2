namespace Hotel.App.Model.SYS
{
   using System;
   public partial class user_login_log : IEntityBase
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
      ///登录机器信息
      ///</summary>
      public string LoginInfo { get; set; }
      ///<summary>
      ///
      ///</summary>
      public string LoginIP { get; set; }
      ///<summary>
      ///
      ///</summary>
      public DateTime CreatedAt { get; set; }
      ///<summary>
      ///
      ///</summary>
      public DateTime UpdatedAt { get; set; }
   }
}
