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
    
    public partial class SetInteHouseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<int> TakeInte { get; set; }
        public int CardType { get; set; }
        public string CardTypeTxt { get; set; }
        public int HouseType { get; set; }
        public string HouseTypeTxt { get; set; }
        public string UseWeeks { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public string Remark { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public System.DateTime UpdatedAt { get; set; }
        public bool IsValid { get; set; }
        public string CreatedBy { get; set; }
    }
}
