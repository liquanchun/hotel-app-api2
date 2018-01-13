using System;
using System.Collections.Generic;
using System.Text;

namespace Hotel.App.Model.Dto
{
    public class StoreInGridDto
    {
        ///<summary>
        ///
        ///</summary>
        public int Id { get; set; }
        ///<summary>
        ///单号
        ///</summary>
        public string OrderNo { get; set; }

        public string BillNo { get; set; }
        ///<summary>
        ///类型
        ///</summary>
        public int TypeId { get; set; }
        ///<summary>
        ///类型
        ///</summary>
        public string TypeIdTxt { get; set; }
        ///<summary>
        ///
        ///</summary>
        public int SupplierId { get; set; }
        public string SupplierIdTxt { get; set; }
        ///<summary>
        ///日期
        ///</summary>
        public DateTime InTime { get; set; }
        ///<summary>
        ///仓库
        ///</summary>
        public int StoreId { get; set; }
        public string StoreIdTxt { get; set; }
        ///<summary>
        ///部门
        ///</summary>
        public int OrgId { get; set; }
        public string OrgIdTxt { get; set; }
        ///<summary>
        ///经办人
        ///</summary>
        public string Operator { get; set; }
        public string OperatorTxt { get; set; }
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
