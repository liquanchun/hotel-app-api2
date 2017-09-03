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
    public class YxBookRepository : EntityBaseRepository<yx_book>, IYxBookRepository
    {
        public YxBookRepository(HotelAppContext context)
            : base(context)
        { }
    }
}
