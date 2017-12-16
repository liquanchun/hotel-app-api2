namespace Hotel.App.Model.Account
{
   using System;
   public partial class cw_preauth : IEntityBase
   {
      ///<summary>
      ///
      ///</summary>
      public int Id { get; set; }
      ///<summary>
      ///房号
      ///</summary>
      public string HouseCode { get; set; }
      ///<summary>
      ///客户
      ///</summary>
      public string CusName { get; set; }
      ///<summary>
      ///预授权号
      ///</summary>
      public string AuthNo { get; set; }
      ///<summary>
      ///状态
      ///</summary>
      public string Status { get; set; }
      ///<summary>
      ///金额
      ///</summary>
      public int Amount { get; set; }
      ///<summary>
      ///有效日期
      ///</summary>
      public DateTime ExpDate { get; set; }
      ///<summary>
      ///单据号
      ///</summary>
      public string OrderNo { get; set; }
      ///<summary>
      ///备注
      ///</summary>
      public string Remark { get; set; }
      ///<summary>
      ///
      ///</summary>
      public string CreatedBy { get; set; }
      ///<summary>
      ///
      ///</summary>
      public DateTime CreatedAt { get; set; }
      ///<summary>
      ///
      ///</summary>
      public bool IsValid { get; set; }
   }
}
