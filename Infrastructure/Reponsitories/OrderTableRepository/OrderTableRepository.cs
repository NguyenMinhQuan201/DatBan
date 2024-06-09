using Infrastructure.EF;
using Infrastructure.Entities;
using Infrastructure.Reponsitories.BaseReponsitory;
using Infrastructure.Reponsitories.OrderReponsitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Reponsitories.OrderTableRepository
{
    public class OrderTableRepository : RepositoryBase<OrderTable>, IOrderTableRepository
    {
        private readonly DatBanDbContext _db;

        public OrderTableRepository(DatBanDbContext dbContext) : base(dbContext)
        {
            _db = dbContext;
        }
    }
}
