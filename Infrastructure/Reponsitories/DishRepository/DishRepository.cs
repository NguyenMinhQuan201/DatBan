using Infrastructure.EF;
using Infrastructure.Entities;
using Infrastructure.Reponsitories.BaseReponsitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Reponsitories.DishReponsitories
{
    public class DishRepository : RepositoryBase<Dish>, IDishRepository
    {
        public DishRepository(DatBanDbContext dbContext) : base(dbContext)
        {

        }
    }
}
