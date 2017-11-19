namespace Hotel.App.Model.Sale
{
   using System;
   public partial class yx_booklist : IEntityBase
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
      public int HouseType { get; set; }
      ///<summary>
      ///
      ///</summary>
      public string HouseCode { get; set; }
      ///<summary>
      ///房价活动
      ///</summary>
      public string PriceActive { get; set; }
      ///<summary>
      ///房价方案
      ///</summary>
      public string PriceProject { get; set; }
      ///<summary>
      ///房价
      ///</summary>
      public int HousePrice { get; set; }
      ///<summary>
      ///
      ///</summary>
      public string Remark { get; set; }
   }
}
