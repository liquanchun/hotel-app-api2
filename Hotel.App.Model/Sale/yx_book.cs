namespace Hotel.App.Model.Sale
{
   using System;
   public partial class yx_book : IEntityBase
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
      ///客户姓名
      ///</summary>
      public string CusName { get; set; }
      ///<summary>
      ///电话
      ///</summary>
      public string Mobile { get; set; }
      ///<summary>
      ///身份证
      ///</summary>
      public string IDCard { get; set; }
      ///<summary>
      ///
      ///</summary>
      public string Status { get; set; }
      ///<summary>
      ///团体单位
      ///</summary>
      public int GroupId { get; set; }
      ///<summary>
      ///预约时间
      ///</summary>
      public DateTime BookTime { get; set; }
      ///<summary>
      ///预抵时间
      ///</summary>
      public DateTime ReachTime { get; set; }
      ///<summary>
      ///天数
      ///</summary>
      public int Days { get; set; }
      ///<summary>
      ///预离时间
      ///</summary>
      public DateTime LeaveTime { get; set; }
      ///<summary>
      ///保留时间
      ///</summary>
      public DateTime RetainTime { get; set; }
      ///<summary>
      ///入住方式
      ///</summary>
      public int CheckInType { get; set; }
      ///<summary>
      ///入住标准
      ///</summary>
      public string CheckInMode { get; set; }
      ///<summary>
      ///渠道
      ///</summary>
      public string Channels { get; set; }
      ///<summary>
      ///订金
      ///</summary>
      public int Deposit { get; set; }
      ///<summary>
      ///批次号
      ///</summary>
      public string BatchNo { get; set; }
      ///<summary>
      ///销售员
      ///</summary>
      public string Saller { get; set; }
      ///<summary>
      ///接单员
      ///</summary>
      public string OrderTaker { get; set; }
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

      public  int HouseTypeId { get; set; }

      public int HouseNum { get; set; }
    }
}
