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
    public class SetOtherhousePriceRepository : EntityBaseRepository<set_otherhouse_price>, ISetOtherhousePriceRepository
    {
        public SetOtherhousePriceRepository(HotelAppContext context)
            : base(context)
        { }
    }
}
