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
    public class SetGroupRepository : EntityBaseRepository<set_group>, ISetGroupRepository
    {
        public SetGroupRepository(HotelAppContext context)
            : base(context)
        { }
    }
}
