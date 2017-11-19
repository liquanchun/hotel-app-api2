namespace Hotel.App.Model.Sale
{
   using System;
   public partial class yx_orderlist : IEntityBase
   {
      ///<summary>
      ///
      ///</summary>
      public int Id { get; set; }
      ///<summary>
      ///订单ID
      ///</summary>
      public int OrderId { get; set; }
      ///<summary>
      ///客人姓名
      ///</summary>
      public string CusName { get; set; }
      ///<summary>
      ///客人身份证
      ///</summary>
      public string IdCard { get; set; }
      ///<summary>
      ///房号
      ///</summary>
      public string HouseCode { get; set; }
      ///<summary>
      ///房型
      ///</summary>
      public int HouseType { get; set; }
      ///<summary>
      ///入住天数
      ///</summary>
      public int Days { get; set; }
      ///<summary>
      ///房费
      ///</summary>
      public int HouseFee { get; set; }
      /// <summary>
      /// 预退房时间
      /// </summary>
      public DateTime PreLeaveTime { get; set; }
      ///<summary>
      ///预收押金
      ///</summary>
        public int PreReceivefee { get; set; }
      /// <summary>
      /// 早餐券
      /// </summary>
      public int Coupons { get; set; }
      ///<summary>
      ///备注
      ///</summary>
        public string Remark { get; set; }
      ///<summary>
      ///状态
      ///</summary>
      public string Status { get; set; }
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
