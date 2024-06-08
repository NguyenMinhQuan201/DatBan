﻿using Infrastructure.Entities;
using Infrastructure.Reponsitories.BaseReponsitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Reponsitories.RoleReponsitories
{
    public interface IRoleRepository : IRepositoryBase<AppRole>
    {
        
    }
}
