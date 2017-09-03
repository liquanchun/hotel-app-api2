using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hotel.App.Model.Store;
using Hotel.App.Data;
using Hotel.App.Data.Repositories;
using Hotel.App.Data.Abstract;

namespace Hotel.App.Data.Repositories
{
    public class KcAdddelRepository : EntityBaseRepository<kc_adddel>, IKcAdddelRepository
    {
        public KcAdddelRepository(HotelAppContext context)
            : base(context)
        { }
    }
}
