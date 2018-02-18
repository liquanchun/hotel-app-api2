namespace Hotel.App.Model.Store
{
   using System;
   public partial class kc_storeout : IEntityBase
   {
       ///<summary>
       ///
       ///</summary>
       public int Id { get; set; }
       ///<summary>
       ///单号
       ///</summary>
       public string OrderNo { get; set; }

       ///<summary>
       ///类型
       ///</summary>
       public int TypeId { get; set; }
       ///<summary>
       ///日期
       ///</summary>
       public DateTime OutTime { get; set; }
       ///<summary>
       ///仓库
       ///</summary>
       public int StoreId { get; set; }
       ///<summary>
       ///仓库
       ///</summary>
       public int OrgId { get; set; }
       ///<summary>
       ///经办人
       ///</summary>
       public int Operator { get; set; }
       ///<summary>
       ///合计金额
       ///</summary>
       public decimal Amount { get; set; }
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

       public string Status { get; set; }
    }
}
