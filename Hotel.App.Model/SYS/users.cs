namespace Hotel.App.Model.SYS
{
   using System;
   public partial class users : IEntityBase
   {
      ///<summary>
      ///
      ///</summary>
      public int Id { get; set; }
      ///<summary>
      ///
      ///</summary>
      public string Name { get; set; }
      ///<summary>
      ///
      ///</summary>
      public string Avatar { get; set; }
      ///<summary>
      ///
      ///</summary>
      public string Profession { get; set; }
   }
}
