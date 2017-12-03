namespace Hotel.App.Model.SYS
{
   using System;
   public partial class set_agent : IEntityBase
   {
      ///<summary>
      ///
      ///</summary>
      public int Id { get; set; }
      ///<summary>
      ///名称
      ///</summary>
      public string Name { get; set; }
      ///<summary>
      ///所属类别
      ///</summary>
      public string TypeName { get; set; }
      ///<summary>
      ///联系人
      ///</summary>
      public string LinkMan { get; set; }
      ///<summary>
      ///电话
      ///</summary>
      public string Tel { get; set; }
      ///<summary>
      ///手机
      ///</summary>
      public string Mobile { get; set; }
      ///<summary>
      ///
      ///</summary>
      public string Email { get; set; }
      ///<summary>
      ///地址
      ///</summary>
      public string Address { get; set; }
      ///<summary>
      ///合同号
      ///</summary>
      public string ContractNo { get; set; }
      ///<summary>
      ///合同起始
      ///</summary>
      public DateTime ContractDate1 { get; set; }
      ///<summary>
      ///合同截止
      ///</summary>
      public DateTime ContractDate2 { get; set; }
      ///<summary>
      ///挂账金额
      ///</summary>
      public int AccountFee { get; set; }
      ///<summary>
      ///银行账号
      ///</summary>
      public string AccountNo { get; set; }
      ///<summary>
      ///销售
      ///</summary>
      public string Seller { get; set; }
      ///<summary>
      ///早餐券
      ///</summary>
      public int Coupons { get; set; }
      ///<summary>
      ///返佣模式
      ///</summary>
      public string CommissionType { get; set; }
      ///<summary>
      ///返佣点数
      ///</summary>
      public int CommissionRate { get; set; }
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
