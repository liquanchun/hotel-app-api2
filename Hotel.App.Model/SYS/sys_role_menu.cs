namespace Hotel.App.Model.SYS
{
   using System;
   public partial class sys_role_menu : IEntityBase
   {
      ///<summary>
      ///
      ///</summary>
      public int Id { get; set; }
      ///<summary>
      ///
      ///</summary>
      public int RoleId { get; set; }
      ///<summary>
      ///
      ///</summary>
      public int MenuId { get; set; }
   }
}
