using SSS.Infrastructure.Seedwork.DbContext;
using SSS.Infrastructure.Seedwork.Repository;
using System;
using System.Linq;

namespace SSS.Infrastructure.Repository.Okex
{
    /// <summary>
    /// EmaRepository
    /// </summary>
    public class EmaRepository : Repository<SSS.Domain.Okex.Target.Ema>, IEmaRepository
    {
        public EmaRepository(DbcontextBase context) : base(context)
        {
        }

        public override void Add(SSS.Domain.Okex.Target.Ema ema)
        {
            var model = DbSet.Where(x => x.instrument.Equals(ema.instrument) && x.ktime.Equals(ema.ktime) && x.timetype.Equals(ema.timetype) && x.parameter.Equals(ema.parameter)).FirstOrDefault();
            if (model == null)
                DbSet.Add(ema);
            else
            {
                model.now_ema = ema.now_ema;
                model.createtime = DateTime.Now; 
                DbSet.Update(model);
            } 
        }
    }
}
