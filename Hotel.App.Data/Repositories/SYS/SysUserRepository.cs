﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hotel.App.Model.SYS;
using Hotel.App.Data;
using Hotel.App.Data.Repositories;
using Hotel.App.Data.Abstract;

namespace Hotel.App.Data.Repositories
{
    public class SysUserRepository : EntityBaseRepository<sys_user>, ISysUserRepository
    {
        public SysUserRepository(HotelAppContext context)
            : base(context)
        { }
    }
}
