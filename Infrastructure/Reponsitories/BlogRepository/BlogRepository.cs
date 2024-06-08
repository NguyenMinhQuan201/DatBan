using Infrastructure.EF;
using Infrastructure.Entities;
using Infrastructure.Reponsitories.BaseReponsitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Reponsitories.BlogRepository
{
    public class BlogRepository : RepositoryBase<Blog>, IBlogRepository
    {
        public BlogRepository(DatBanDbContext dbContext) : base(dbContext)
        {

        }
    }
}
