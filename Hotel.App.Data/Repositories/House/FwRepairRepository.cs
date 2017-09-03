using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hotel.App.Model.House;
using Hotel.App.Data;
using Hotel.App.Data.Repositories;
using Hotel.App.Data.Abstract;

namespace Hotel.App.Data.Repositories
{
    public class FwRepairRepository : EntityBaseRepository<fw_repair>, IFwRepairRepository
    {
        public FwRepairRepository(HotelAppContext context)
            : base(context)
        { }
    }
}
