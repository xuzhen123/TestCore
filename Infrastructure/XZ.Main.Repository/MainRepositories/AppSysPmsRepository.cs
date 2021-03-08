using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XZ.Infrastructure;
using XZ.Main.Domain;

namespace XZ.Main.Repository
{
    public class AppSysPmsRepository : Repository<SysOperation, MainContext>, IAppSysPmsRepository
    {
        public AppSysPmsRepository(MainContext dbContext) : base(dbContext) { }

        public List<SysOperation> GetAppSysPms(string sysAppId, string name)
        {
            var list = this.DbContext.Set<SysOperation>().AsQueryable();

            if (!string.IsNullOrWhiteSpace(sysAppId))
            {
                list = list.Where(m => m.SysAppId == sysAppId);
            }

            if (!string.IsNullOrWhiteSpace(name))
            {
                list = list.Where(m => m.Name == name);
            }

            return list.ToList();
        }
    }
}
