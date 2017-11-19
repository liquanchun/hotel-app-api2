namespace Hotel.App.Model.House
{
   using System;
   public partial class fw_houseinfo : IEntityBase
   {
      ///<summary>
      ///
      ///</summary>
      public int Id { get; set; }
      ///<summary>
      ///楼栋
      ///</summary>
      public string Building { get; set; }
      ///<summary>
      ///楼层
      ///</summary>
      public string Floor { get; set; }
      ///<summary>
      ///房号
      ///</summary>
      public string Code { get; set; }
      ///<summary>
      ///房型
      ///</summary>
      public int HouseType { get; set; }
      ///<summary>
      ///标签
      ///</summary>
      public string Tags { get; set; }
      ///<summary>
      ///房屋状态
      ///</summary>
      public int State { get; set; }
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
