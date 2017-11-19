namespace Hotel.App.Model.SYS
{
   using System;
   public partial class sys_function : IEntityBase
   {
      ///<summary>
      ///
      ///</summary>
      public int Id { get; set; }
      ///<summary>
      ///
      ///</summary>
      public string FunctionName { get; set; }
      ///<summary>
      ///
      ///</summary>
      public string FunctionAddr { get; set; }
      ///<summary>
      ///组件名称
      ///</summary>
      public string Component { get; set; }
      ///<summary>
      ///
      ///</summary>
      public int MenuId { get; set; }
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
