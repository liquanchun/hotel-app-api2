namespace Hotel.App.Model.SYS
{
   using System;
   public partial class sys_menu : IEntityBase
   {
      ///<summary>
      ///
      ///</summary>
      public int Id { get; set; }
      ///<summary>
      ///
      ///</summary>
      public string MenuName { get; set; }
      ///<summary>
      ///
      ///</summary>
      public int ParentId { get; set; }
      ///<summary>
      ///
      ///</summary>
      public int MenuLevel { get; set; }
      ///<summary>
      ///
      ///</summary>
      public string MenuAddr { get; set; }
      ///<summary>
      ///
      ///</summary>
      public string Icon { get; set; }
      ///<summary>
      ///
      ///</summary>
      public string RoleIds { get; set; }
      ///<summary>
      ///
      ///</summary>
      public int MenuOrder { get; set; }
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
