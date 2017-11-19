namespace Hotel.App.Model.House
{
   using System;
   public partial class fw_clean : IEntityBase
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
      ///扫房时间
      ///</summary>
      public DateTime CleanTime { get; set; }
      ///<summary>
      ///扫房人
      ///</summary>
      public string CleanMan { get; set; }
      ///<summary>
      ///是否续住
      ///</summary>
      public bool IsOverStay { get; set; }
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
