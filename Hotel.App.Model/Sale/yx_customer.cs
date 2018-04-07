namespace Hotel.App.Model.Sale
{
   using System;
   public partial class yx_customer : IEntityBase
   {
      ///<summary>
      ///
      ///</summary>
      public int Id { get; set; }
      ///<summary>
      ///客户姓名
      ///</summary>
      public string Name { get; set; }
      ///<summary>
      ///性别
      ///</summary>
      public string Sex { get; set; }
      ///<summary>
      ///身份证号
      ///</summary>
      public string IDCardNo { get; set; }
      ///<summary>
      ///手机号码
      ///</summary>
      public string Mobile { get; set; }
       ///<summary>
       ///微信
       ///</summary>
       public string wechat { get; set; }
        ///<summary>
        ///
        ///</summary>
        public string Address { get; set; }
      ///<summary>
      ///
      ///</summary>
      public string Birthday { get; set; }
      ///<summary>
      ///是否会员
      ///</summary>
      public bool IsCard { get; set; }
      ///<summary>
      ///会员类型
      ///</summary>
      public int? CardTypeId { get; set; }
      /// <summary>
      /// 累计积分
      /// </summary>
      public int SumIntegral { get; set; }
      /// <summary>
      /// 剩余积分
      /// </summary>
      public int RemainIntegral { get; set; }
      /// <summary>
      /// 累计消费金额
      /// </summary>

      public decimal SumConsume { get; set; }
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
