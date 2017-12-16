namespace Hotel.App.Model.Account
{
   using System;
   public partial class cw_invoice : IEntityBase
   {
      ///<summary>
      ///
      ///</summary>
      public int Id { get; set; }
      ///<summary>
      ///客户
      ///</summary>
      public string CusName { get; set; }
      ///<summary>
      ///发票抬头
      ///</summary>
      public string InvoiceTitle { get; set; }
      ///<summary>
      ///房号
      ///</summary>
      public string HouseCode { get; set; }
      ///<summary>
      ///金额
      ///</summary>
      public int Amount { get; set; }
      ///<summary>
      ///开票金额
      ///</summary>
      public int InvoiceAmount { get; set; }
      ///<summary>
      ///税费
      ///</summary>
      public decimal TaxFee { get; set; }
      ///<summary>
      ///单据号
      ///</summary>
      public string OrderNo { get; set; }
      ///<summary>
      ///发票号
      ///</summary>
      public string InvoiceNo { get; set; }
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
