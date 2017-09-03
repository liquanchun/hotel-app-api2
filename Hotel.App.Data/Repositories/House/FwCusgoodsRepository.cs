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
    public class FwCusgoodsRepository : EntityBaseRepository<fw_cusgoods>, IFwCusgoodsRepository
    {
        public FwCusgoodsRepository(HotelAppContext context)
            : base(context)
        { }
    }
}
