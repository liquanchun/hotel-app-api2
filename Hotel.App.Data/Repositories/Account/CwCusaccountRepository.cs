using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hotel.App.Model.Account;
using Hotel.App.Data;
using Hotel.App.Data.Repositories;
using Hotel.App.Data.Abstract;

namespace Hotel.App.Data.Repositories
{
    public class CwCusaccountRepository : EntityBaseRepository<cw_cusaccount>, ICwCusaccountRepository
    {
        public CwCusaccountRepository(HotelAppContext context)
            : base(context)
        { }
    }
}
