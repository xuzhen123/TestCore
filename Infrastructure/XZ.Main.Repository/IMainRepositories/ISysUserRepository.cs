using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XZ.Infrastructure;
using XZ.Main.Domain;

namespace XZ.Main.Repository
{
    public interface ISysUserRepository : IRepository<SysUser>
    {
        List<SysUser> GetSysUsers();
        SysUser GetUserById(int sysUserId);
        Task<SysUser> GetSysUserByNameAsync(string sysUserName);
        SysUser GetSysUserByEmailAndPwd(string email, string password);
    }
}
