namespace Hotel.App.Model.SYS
{
   using System;
   public partial class set_card_discount : IEntityBase
   {
       ///<summary>
       ///
       ///</summary>
       public int Id { get; set; }
       ///<summary>
       ///�����
       ///</summary>
       public string Name { get; set; }
       ///<summary>
       ///��Ա������
       ///</summary>
       public int CardTypeId { get; set; }
       /// <summary>
       /// ��ĿID
       /// </summary>
       public int? ServiceItemId { get; set; }
       ///<summary>
       ///����ID
       ///</summary>
       public int? HouseTypeId { get; set; }
       /// <summary>
       /// ��ƷID
       /// </summary>
       public int? GoodsId { get; set; }
       ///<summary>
       ///����
       ///</summary>
       public decimal Discount { get; set; }
       ///<summary>
       ///���ʼ����
       ///</summary>
       public DateTime StartDate { get; set; }
       ///<summary>
       ///���������
       ///</summary>
       public DateTime EndDate { get; set; }
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
