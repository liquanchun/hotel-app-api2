namespace Hotel.App.Model.Dto
{
   using System;
   public partial class ServiceItemDto
   {
      ///<summary>
      ///
      ///</summary>
      public int Id { get; set; }
      ///<summary>
      ///产品代码
      ///</summary>
      public string ItemCode { get; set; }
      ///<summary>
      ///产品名称
      ///</summary>
      public string Name { get; set; }
      ///<summary>
      ///类别
      ///</summary>
      public int TypeId { get; set; }

       ///<summary>
       ///类别名称
       ///</summary>
       public string TypeName { get; set; }
        ///<summary>
        ///产品型号
        ///</summary>
        public string Unit { get; set; }
      ///<summary>
      ///参考价格
      ///</summary>
      public decimal Price { get; set; }
      ///<summary>
      ///最大库存
      ///</summary>
      public int Integral { get; set; }
      ///<summary>
      ///产品说明
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
