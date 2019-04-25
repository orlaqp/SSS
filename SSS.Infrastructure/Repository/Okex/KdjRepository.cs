using SSS.Infrastructure.Seedwork.DbContext;
using SSS.Infrastructure.Seedwork.Repository;
using System;
using System.Linq;

namespace SSS.Infrastructure.Repository.Okex
{
    /// <summary>
    /// KdjRepository
    /// </summary>
    public class KdjRepository : Repository<SSS.Domain.Okex.Target.Kdj>, IKdjRepository
    {
        public KdjRepository(DbcontextBase context) : base(context)
        {
        }

        public override void Add(SSS.Domain.Okex.Target.Kdj kdj)
        {
            var model = DbSet.Where(x => x.instrument.Equals(kdj.instrument) && x.ktime.Equals(kdj.ktime) && x.timetype.Equals(kdj.timetype)).FirstOrDefault();
            if (model == null)
                DbSet.Add(kdj);
            else
            {
                model.createtime = DateTime.Now;
                model.k = kdj.k;
                model.d = kdj.d;
                model.j= kdj.j; 
                DbSet.Update(model);
            }
        }
    }

}
