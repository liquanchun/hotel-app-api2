namespace Hotel.App.Model.SYS
{
   using System;
   public partial class sys_dic : IEntityBase
   {
      ///<summary>
      ///
      ///</summary>
      public int Id { get; set; }
      ///<summary>
      ///
      ///</summary>
      public string DicName { get; set; }
      ///<summary>
      ///
      ///</summary>
      public int ParentId { get; set; }
      ///<summary>
      ///
      ///</summary>
      public DateTime CreatedAt { get; set; }
      ///<summary>
      ///
      ///</summary>
      public string CreatedBy { get; set; }
      ///<summary>
      ///
      ///</summary>
      public bool IsValid { get; set; }
      /// <summary>
      /// 是否默认值
      /// </summary>
      public bool IsDefault { get; set; }
      /// <summary>
      /// 序号
      /// </summary>
      public int IndexNo { get; set; }
      /// <summary>
      /// 备注
      /// </summary>
      public string Remark { get; set; }
    }
}
