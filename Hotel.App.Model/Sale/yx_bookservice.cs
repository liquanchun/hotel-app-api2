namespace Hotel.App.Model.Sale
{
   using System;
   public partial class yx_bookservice : IEntityBase
   {
      ///<summary>
      ///
      ///</summary>
      public int Id { get; set; }
      ///<summary>
      ///
      ///</summary>
      public string OrderNo { get; set; }
      ///<summary>
      ///
      ///</summary>
      public int TypeId { get; set; }
      ///<summary>
      ///
      ///</summary>
      public string ItemCode { get; set; }
      ///<summary>
      ///房价活动
      ///</summary>
      public DateTime ServiceTime { get; set; }
      ///<summary>
      ///房价方案
      ///</summary>
      public decimal Times { get; set; }
      ///<summary>
      ///房价
      ///</summary>
      public decimal Price { get; set; }
      ///<summary>
      ///
      ///</summary>
      public string Remark { get; set; }
   }
}
