using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XZ.Infrastructure;
using XZ.Main.Domain;

namespace XZ.Main.Repository
{
    public class SysPermissionRepository : Repository<SysPermission, MainContext>, ISysPermissionRepository
    {
        public SysPermissionRepository(MainContext dbContext) : base(dbContext) { }
        public async Task<List<SysPermission>> GetSysRoleAndAppsAsync(int roleId, string sysAppId)
        {
            var list = this.DbContext.Set<SysPermission>().AsQueryable();

            if (roleId > 0)
            {
                list = list.Where(m => m.RoleId == roleId);
            }

            if (!string.IsNullOrWhiteSpace(sysAppId))
            {
                list = list.Where(m => m.SysAppId == sysAppId);
            }

            return await Task.FromResult(list.ToList());
        }
    }
}
