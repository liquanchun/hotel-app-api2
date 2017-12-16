namespace Hotel.App.Model.Account
{
   using System;
   public partial class CWPreFeeDto
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
      ///支付方式
      ///</summary>
      public int PayType { get; set; }
       ///<summary>
       ///支付方式
       ///</summary>
       public string PayTypeTxt { get; set; }
        ///<summary>
        ///金额
        ///</summary>
        public int Amount { get; set; }
      ///<summary>
      ///批次
      ///</summary>
      public string WorkNum { get; set; }
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
