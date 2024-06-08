using Infrastructure.EF;
using Infrastructure.Entities;
using Infrastructure.Reponsitories.BaseReponsitory;
using Infrastructure.Reponsitories.UserReponsitories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Reponsitories.RoleReponsitories
{
    public class AreaRepository : RepositoryBase<Area>, IAreaRepository
    {
        private readonly DatBanDbContext _dbcontext;
        public AreaRepository(DatBanDbContext dbContext) : base(dbContext)
        {
            _dbcontext = dbContext;
        }
    }
}
