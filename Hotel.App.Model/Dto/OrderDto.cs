using System;
using System.Collections.Generic;
using System.Text;

namespace Hotel.App.Model.Dto
{
    public class OrderDto
    {
        ///<summary>
        ///
        ///</summary>
        public int Id { get; set; }
        ///<summary>
        ///订单号
        ///</summary>
        public string OrderNo { get; set; }
        ///<summary>
        ///客人姓名
        ///</summary>
        public string CusName { get; set; }
        ///<summary>
        ///客人电话
        ///</summary>
        public string CusPhone { get; set; }
        ///<summary>
        ///客人身份证
        ///</summary>
        public string IdCard { get; set; }
        ///<summary>
        ///入住方式
        ///</summary>
        public string InType { get; set; }
        ///<summary>
        ///支付方式
        ///</summary>
        public int PayType { get; set; }
        ///<summary>
        ///支付方式
        ///</summary>
        public string PayTypeTxt { get; set; }
        ///<summary>
        ///合计房费
        ///</summary>
        public int HouseFee { get; set; }
        ///<summary>
        ///预收押金
        ///</summary>
        public int PreReceivefee { get; set; }
        /// <summary>
        /// 单据号、授权码
        /// </summary>
        public string BillNo { get; set; }
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
