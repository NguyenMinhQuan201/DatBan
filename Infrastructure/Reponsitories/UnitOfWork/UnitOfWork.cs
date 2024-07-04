using Infrastructure.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Reponsitories.UnitOfWork
{
    public interface IDbFactory : IDisposable
    {
        DatBanDbContext Init();
    }
    public interface IUnitOfWork
    {
        void Commit();

        /// <summary>
        /// rollback lại những thay đổi trong ánh xạ của database
        /// </summary>
        void Rollback();
    }
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbFactory dbFactory;
        private DatBanDbContext dbContext;

        public UnitOfWork(IDbFactory dbFactory)
        {
            this.dbFactory = dbFactory;
        }

        public DatBanDbContext DbContext
        {
            get { return dbContext ?? (dbContext = dbFactory.Init()); }
        }

        public void Commit()
        {
            DbContext.SaveChanges();
        }

        public void Rollback()
        {
            DbContext.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
        }
    }
}
