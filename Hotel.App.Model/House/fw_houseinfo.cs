//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Hotel.App.Model.House
{
    using System;
    using System.Collections.Generic;
    
    public partial class fw_houseinfo : IEntityBase
    {
        public int Id { get; set; }
        public string Building { get; set; }
        public string Floor { get; set; }
        public string Code { get; set; }
        public Nullable<int> HouseType { get; set; }
        public string Tags { get; set; }
        public string Remark { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public System.DateTime UpdatedAt { get; set; }
        public bool IsValid { get; set; }
        public string CreatedBy { get; set; }
    }
}
