using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XZ.Infrastructure;
using XZ.Main.Domain;

namespace XZ.Main.Repository
{
    public interface ISysPermissionRepository : IRepository<SysPermission>
    {
        Task<List<SysPermission>> GetSysRoleAndAppsAsync(int roleId,string sysAppId);
    }
}
