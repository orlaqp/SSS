using SSS.Infrastructure.Seedwork.DbContext;
using SSS.Infrastructure.Seedwork.Repository;
using System;
using System.Linq;

namespace SSS.Infrastructure.Repository.Okex
{
    /// <summary>
    /// EmaRepository
    /// </summary>
    public class MaRepository : Repository<SSS.Domain.Okex.Target.Ma>, IMaRepository
    {
        public MaRepository(DbcontextBase context) : base(context)
        {
        }

        public override void Add(SSS.Domain.Okex.Target.Ma ma)
        {
            var model = DbSet.Where(x => x.instrument.Equals(ma.instrument) && x.ktime.Equals(ma.ktime) && x.timetype.Equals(ma.timetype) && x.parameter.Equals(ma.parameter) && x.type.Equals(ma.type)).FirstOrDefault();
            if (model == null)
                DbSet.Add(ma);
            else
            {
                model.now_ma = ma.now_ma;
                model.createtime = DateTime.Now;
                DbSet.Update(model);
            }
        }
    }

}
