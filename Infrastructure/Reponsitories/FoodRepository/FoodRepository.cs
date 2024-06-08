using Infrastructure.EF;
using Infrastructure.Entities;
using Infrastructure.Reponsitories.BaseReponsitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Reponsitories.FoodReponsitories
{
    public class FoodRepository : RepositoryBase<Food>, IFoodRepository
    {
        public FoodRepository(DatBanDbContext dbContext) : base(dbContext)
        {

        }
    }
}
