namespace Hotel.App.Model.SYS
{
   using System;
   public partial class set_house_type : IEntityBase
   {
      ///<summary>
      ///
      ///</summary>
      public int Id { get; set; }
      ///<summary>
      ///
      ///</summary>
      public string TypeName { get; set; }
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
      public DateTime UpdatedAt { get; set; }
      ///<summary>
      ///
      ///</summary>
      public string CreatedBy { get; set; }
      ///<summary>
      ///全价
      ///</summary>
      public int AllPrice { get; set; }
      ///<summary>
      ///起步价
      ///</summary>
      public int StartPrice { get; set; }
      ///<summary>
      ///单位时间内加价
      ///</summary>
      public int AddPrice { get; set; }
      ///<summary>
      ///加价封顶额
      ///</summary>
      public int AddMaxPrice { get; set; }
      ///<summary>
      ///预收房费
      ///</summary>
      public int PreReceiveFee { get; set; }
      ///<summary>
      ///备注
      ///</summary>
      public string Remark { get; set; }
   }
}
