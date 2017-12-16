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
    public class CwInvoiceRepository : EntityBaseRepository<cw_invoice>, ICwInvoiceRepository
    {
        public CwInvoiceRepository(HotelAppContext context)
            : base(context)
        { }
    }
}
