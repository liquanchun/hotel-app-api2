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
    public class FwHouseinfoRepository : EntityBaseRepository<fw_houseinfo>, IFwHouseinfoRepository
    {
        public FwHouseinfoRepository(HotelAppContext context)
            : base(context)
        { }
    }
}
