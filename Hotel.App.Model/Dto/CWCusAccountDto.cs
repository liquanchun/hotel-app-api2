namespace Hotel.App.Model.Account
{
   using System;
   public partial class CWCusAccountDto
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
      ///项目
      ///</summary>
      public string ItemName { get; set; }
      ///<summary>
      ///数量
      ///</summary>
      public int Number { get; set; }
      ///<summary>
      ///金额
      ///</summary>
      public int Amount { get; set; }
      ///<summary>
      ///批次
      ///</summary>
      public string WorkNum { get; set; }
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

      public int PayType { get; set; }

      public string PayTypeTxt { get; set; }

      public string OrderNo { get; set; }
   }
}
