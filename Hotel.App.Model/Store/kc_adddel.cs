namespace Hotel.App.Model.Store
{
   using System;
   public partial class kc_adddel : IEntityBase
   {
      ///<summary>
      ///
      ///</summary>
      public int Id { get; set; }
      ///<summary>
      ///单号
      ///</summary>
      public string OrderNo { get; set; }
      ///<summary>
      ///类型
      ///</summary>
      public string TypeName { get; set; }
      ///<summary>
      ///
      ///</summary>
      public int SupplierId { get; set; }
      ///<summary>
      ///日期
      ///</summary>
      public DateTime InTime { get; set; }
      ///<summary>
      ///报损报溢
      ///</summary>
      public string AddDel { get; set; }
      ///<summary>
      ///仓库
      ///</summary>
      public string Storage { get; set; }
      ///<summary>
      ///经办人
      ///</summary>
      public string Operator { get; set; }
      ///<summary>
      ///商品
      ///</summary>
      public int GoodsId { get; set; }
      ///<summary>
      ///数量
      ///</summary>
      public decimal GoodsAmount { get; set; }
      ///<summary>
      ///价格
      ///</summary>
      public decimal GoodsPrice { get; set; }
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
   }
}
