//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Hotel.App.Model.SYS
{
    using System;
    using System.Collections.Generic;
    
    public partial class SetIneExchangDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ExchangeType { get; set; }
        public int CardType { get; set; }
        public string CardTypeTxt { get; set; }
        public string GiftName { get; set; }
        public int ExchangeInte { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Remark { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public System.DateTime UpdatedAt { get; set; }
        public bool IsValid { get; set; }
        public string CreatedBy { get; set; }
    }
}
