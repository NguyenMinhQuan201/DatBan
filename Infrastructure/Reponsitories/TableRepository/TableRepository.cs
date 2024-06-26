﻿using Infrastructure.EF;
using Infrastructure.Entities;
using Infrastructure.Reponsitories.BaseReponsitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Reponsitories.TableReponsitory
{
    public class TableRepository : RepositoryBase<Table>, ITableRepository
    {
        public TableRepository(DatBanDbContext dbContext) : base(dbContext)
        {

        }
    }
}
