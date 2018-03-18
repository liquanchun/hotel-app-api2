namespace Hotel.App.Model.SYS
{
   using System;
   public partial class sys_staff : IEntityBase
   {
      ///<summary>
      ///
      ///</summary>
      public int Id { get; set; }
      ///<summary>
      ///
      ///</summary>
      public string EmployeeNo { get; set; }
      ///<summary>
      ///
      ///</summary>
      public string Name { get; set; }
      ///<summary>
      ///
      ///</summary>
      public string Mobile { get; set; }

      public string Tel { get; set; }
      ///<summary>
      ///
      ///</summary>
      public string Title { get; set; }
       ///<summary>
       ///
       ///</summary>
       public string Address { get; set; }
       ///<summary>
       ///
       ///</summary>
       public string IDCard { get; set; }
       ///<summary>
       ///
       ///</summary>
       public string WebChat { get; set; }
      ///<summary>
      ///
      ///</summary>
      public int OrgId { get; set; }
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
