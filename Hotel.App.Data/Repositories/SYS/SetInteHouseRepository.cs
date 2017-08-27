using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hotel.App.Model.SYS;
using Hotel.App.Data;
using Hotel.App.Data.Repositories;
using Hotel.App.Data.Abstract;

namespace Hotel.App.Data.Repositories
{
    public class SetInteHouseRepository : EntityBaseRepository<set_inte_house>, ISetInteHouseRepository
    {
        public SetInteHouseRepository(HotelAppContext context)
            : base(context)
        { }
    }
}
