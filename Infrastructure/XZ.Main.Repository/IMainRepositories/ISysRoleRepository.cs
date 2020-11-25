using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XZ.Infrastructure;
using XZ.Main.Domain;

namespace XZ.Main.Repository
{
    public interface ISysRoleRepository : IRepository<SysRole>
    {
        List<SysRole> GetSysRoles();
        SysRole GetSysRoleById(int roleId);
        List<SysRole> GetSysRoleByIds(int[] roleIds);
    }
}
