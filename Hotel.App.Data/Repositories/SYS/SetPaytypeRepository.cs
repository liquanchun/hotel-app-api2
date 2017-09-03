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
    public class SetPaytypeRepository : EntityBaseRepository<set_paytype>, ISetPaytypeRepository
    {
        public SetPaytypeRepository(HotelAppContext context)
            : base(context)
        { }
    }
}
