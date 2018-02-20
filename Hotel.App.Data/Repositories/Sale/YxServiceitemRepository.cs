using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hotel.App.Model.Sale;
using Hotel.App.Data;
using Hotel.App.Data.Repositories;
using Hotel.App.Data.Abstract;

namespace Hotel.App.Data.Repositories
{
    public class YxServiceitemRepository : EntityBaseRepository<yx_serviceitem>, IYxServiceitemRepository
    {
        public YxServiceitemRepository(HotelAppContext context)
            : base(context)
        { }
    }
}
