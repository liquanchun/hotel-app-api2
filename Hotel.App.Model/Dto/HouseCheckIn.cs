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
    
    public partial class HouseCheckInList
    {
        public List<HouseCheckIn> HouseCheckIns { get; set; }
        public List<HouseInType> HouseInTypeList { get; set; }

        public List<HouseComeType> HouseComeTypeList { get; set; }
    }

    public partial class HouseCheckIn
    {
        public string Code { get; set; }
        public int InType { get; set; }
        public string InTypeTxt { get; set; }
        public int ComeType { get; set; }
        public string ComeTypeTxt { get; set; }
    }

    public partial class HouseInType
    {
        public int InType { get; set; }
        public string InTypeTxt { get; set; }
        public int Count { get; set; }
    }
    public partial class HouseComeType
    {
        public int ComeType { get; set; }
        public string ComeTypeTxt { get; set; }
        public int Count { get; set; }
    }
}
