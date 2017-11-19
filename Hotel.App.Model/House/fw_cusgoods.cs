namespace Hotel.App.Model.House
{
   using System;
   public partial class fw_cusgoods : IEntityBase
   {
      ///<summary>
      ///
      ///</summary>
      public int Id { get; set; }
      ///<summary>
      ///类型
      ///</summary>
      public string TypeName { get; set; }
      ///<summary>
      ///物品名称
      ///</summary>
      public string GoodsName { get; set; }
      ///<summary>
      ///物品估价
      ///</summary>
      public decimal GoodsPrice { get; set; }
      ///<summary>
      ///客人类型
      ///</summary>
      public string CusType { get; set; }
      ///<summary>
      ///房号
      ///</summary>
      public string HouseCode { get; set; }
      ///<summary>
      ///订单号
      ///</summary>
      public string OrderNo { get; set; }
      ///<summary>
      ///客户姓名
      ///</summary>
      public string CusName { get; set; }
      ///<summary>
      ///电话
      ///</summary>
      public string Mobile { get; set; }
      ///<summary>
      ///
      ///</summary>
      public string Remark { get; set; }
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
      public bool IsValid { get; set; }
      ///<summary>
      ///
      ///</summary>
      public string CreatedBy { get; set; }
      ///<summary>
      ///
      ///</summary>
      public string UpdatedBy { get; set; }
   }
}
