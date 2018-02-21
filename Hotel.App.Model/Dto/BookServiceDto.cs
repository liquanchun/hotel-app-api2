namespace Hotel.App.Model.Sale
{
   using System;
   public partial class BookServiceDto
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
      public int ServiceType { get; set; }

      public string ServiceTypeTxt { get; set; }
        ///<summary>
        ///
        ///</summary>
        public string ServiceCode { get; set; }
        public string ServiceName { get; set; }
        ///<summary>
        ///房价活动
        ///</summary>
        public string ServiceTime { get; set; }
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
