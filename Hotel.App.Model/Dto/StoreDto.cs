using System;
using System.Collections.Generic;
using System.Text;

namespace Hotel.App.Model.Dto
{
    public class StoreDto
    {
        ///<summary>
        ///
        ///</summary>
        public int Id { get; set; }
        ///<summary>
        ///仓库
        ///</summary>
        public int StoreId { get; set; }

        public string StoreIdTxt { get; set; }
        ///<summary>
        ///商品ID
        ///</summary>
        public int GoodsId { get; set; }
        public string GoodsIdTxt { get; set; }

        public int GoodsTypeId { get; set; }
        public string GoodsTypeIdTxt { get; set; }
        ///<summary>
        ///金额
        ///</summary>
        public decimal Amount { get; set; }
        ///<summary>
        ///数量
        ///</summary>
        public decimal Number { get; set; }
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

        public string GoodsSite { get; set; }

        public int OrgId { get; set; }

        public string OrgTxt { get; set; }
    }
}
