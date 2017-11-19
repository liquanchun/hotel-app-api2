namespace Hotel.App.Model.SYS
{
   using System;
   public partial class user_access_log : IEntityBase
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
      public int MenuId { get; set; }
      ///<summary>
      ///
      ///</summary>
      public int FunctionId { get; set; }
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
      public string Desc { get; set; }
   }
}
