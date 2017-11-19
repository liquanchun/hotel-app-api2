namespace Hotel.App.Model.House
{
   using System;
   public partial class fw_repair : IEntityBase
   {
      ///<summary>
      ///
      ///</summary>
      public int Id { get; set; }
      ///<summary>
      ///房号
      ///</summary>
      public string HouseCode { get; set; }
      ///<summary>
      ///开始时间
      ///</summary>
      public DateTime StartTime { get; set; }
      ///<summary>
      ///结束时间
      ///</summary>
      public DateTime EndTime { get; set; }
      ///<summary>
      ///状态
      ///</summary>
      public string Status { get; set; }
      ///<summary>
      ///原因
      ///</summary>
      public string Reason { get; set; }
      ///<summary>
      ///结果
      ///</summary>
      public string Result { get; set; }
      ///<summary>
      ///维修人
      ///</summary>
      public string RepairMan { get; set; }
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
