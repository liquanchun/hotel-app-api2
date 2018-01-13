namespace Hotel.App.Model.Store
{
   using System;
   public partial class kc_storeinlist : IEntityBase
   {
      ///<summary>
      ///
      ///</summary>
      public int Id { get; set; }
      ///<summary>
      ///
      ///</summary>
      public int GoodsId { get; set; }
      ///<summary>
      ///
      ///</summary>
      public decimal number { get; set; }
      ///<summary>
      ///
      ///</summary>
      public decimal price { get; set; }
       ///<summary>
       ///
       ///</summary>
       public decimal amount { get; set; }
        ///<summary>
        ///
        ///</summary>
        public string orderno { get; set; }

        public string batchno { get; set; }

        public string goodscode { get; set; }
   }
}
