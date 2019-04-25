using SSS.Infrastructure.Seedwork.DbContext;
using SSS.Infrastructure.Seedwork.Repository;
using System;
using System.Linq;

namespace SSS.Infrastructure.Repository.Okex
{
    /// <summary>
    /// MacdRepository
    /// </summary>
    public class MacdRepository : Repository<SSS.Domain.Okex.Target.Macd>, IMacdRepository
    {
        public MacdRepository(DbcontextBase context) : base(context)
        {
        }

        public override void Add(SSS.Domain.Okex.Target.Macd macd)
        {
            var model = DbSet.Where(x => x.instrument.Equals(macd.instrument) && x.ktime.Equals(macd.ktime) && x.timetype.Equals(macd.timetype)).FirstOrDefault();
            if (model == null)
                DbSet.Add(macd);
            else
            {
                model.createtime = DateTime.Now;
                model.dea = macd.dea;
                model.dif = macd.dif;
                model.macd = macd.macd;
                model.ema12 = macd.ema12;
                model.ema26 = macd.ema26;
                DbSet.Update(model);
            } 
        }
    }

}
