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
    public class KcGoodsRepository : EntityBaseRepository<kc_goods>, IKcGoodsRepository
    {
        public KcGoodsRepository(HotelAppContext context)
            : base(context)
        { }
    }
}
