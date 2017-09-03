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
    public class FwCleanRepository : EntityBaseRepository<fw_clean>, IFwCleanRepository
    {
        public FwCleanRepository(HotelAppContext context)
            : base(context)
        { }
    }
}
