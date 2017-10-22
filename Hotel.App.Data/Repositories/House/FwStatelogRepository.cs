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
    public class FwStatelogRepository : EntityBaseRepository<fw_statelog>, IFwStatelogRepository
    {
        public FwStatelogRepository(HotelAppContext context)
            : base(context)
        { }
    }
}
