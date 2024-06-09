using Infrastructure.EF;
using Infrastructure.Entities;
using Infrastructure.Reponsitories.BaseReponsitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Reponsitories.OrderReponsitory
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        private readonly DatBanDbContext _db;

        public OrderRepository(DatBanDbContext dbContext) : base(dbContext)
        {
            _db = dbContext;
        }
        public async Task<Order> CreateAsyncFLByOrder(Order entity)
        {
            _db.Orders.Add(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        //public async Task<List<Notifi>> GetAllNoti()
        //{
        //    return _db.Notifis.OrderByDescending(x=>x.Id).ToList();
        //}

        //public async Task<bool> UpdateNoti(Notifi obj)
        //{
        //    _db.Notifis.Update(obj);
        //    await _db.SaveChangesAsync();
        //    return true;
        //}
    }
}
