//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Hotel.App.Model.SYS
{
    using System;
    using System.Collections.Generic;
    
    public partial class set_card : IEntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<int> Level { get; set; }
        public Nullable<decimal> CardFee { get; set; }
        public Nullable<bool> IsRecharge { get; set; }
        public string Remark { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public System.DateTime UpdatedAt { get; set; }
        public bool IsValid { get; set; }
        public string CreatedBy { get; set; }
    }
}
