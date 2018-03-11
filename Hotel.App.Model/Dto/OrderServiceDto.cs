namespace Hotel.App.Model.Sale
{
   using System;
   public partial class OrderServiceDto
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

      public string TypeIdTxt { get; set; }
        ///<summary>
        ///
        ///</summary>
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
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
      public string Operator { get; set; }
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
