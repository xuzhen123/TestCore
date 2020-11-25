using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XZ.Infrastructure;
using XZ.Main.Domain;

namespace XZ.Main.Repository
{
    public interface ISysUserAndRoleRepository : IRepository<SysUserAndRole>
    {
        Task<List<SysUserAndRole>> GetRoleAndSysUsersAsync();
        Task<SysUserAndRole> GetRoleAndSysUserByUserIdAsync(int sysUserId);
        Task<SysUserAndRole> GetRoleAndSysUserById(int id);
    }
}
