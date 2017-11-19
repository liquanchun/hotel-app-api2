namespace Hotel.App.Model.SYS
{
   using System;
   public partial class sys_role_function : IEntityBase
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
      public int FunctionId { get; set; }
   }
}
