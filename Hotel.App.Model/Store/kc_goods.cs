//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Hotel.App.Model.Store
{
    using System;
    using System.Collections.Generic;
    
    public partial class kc_goods : IEntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TypeName { get; set; }
        public string Unit { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<decimal> EmpPrice { get; set; }
        public Nullable<decimal> MaxAmount { get; set; }
        public Nullable<decimal> MinAmount { get; set; }
        public string Remark { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public System.DateTime UpdatedAt { get; set; }
        public bool IsValid { get; set; }
        public string CreatedBy { get; set; }
    }
}