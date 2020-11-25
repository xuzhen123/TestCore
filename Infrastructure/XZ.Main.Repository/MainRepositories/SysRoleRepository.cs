using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZ.Infrastructure;
using XZ.Main.Domain;

namespace XZ.Main.Repository
{
    public class SysRoleRepository : Repository<SysRole, MainContext>, ISysRoleRepository
    {
        public SysRoleRepository(MainContext domainContext) : base(domainContext) { }

        public SysRole GetSysRoleById(int roleId)
        {
            return this.Find(roleId);
        }

        public List<SysRole> GetSysRoleByIds(int[] roleIds)
        {
            if (roleIds.Length == 0)
            {
                return new List<SysRole>();
            }

            if (roleIds.Length == 1)
            {
                int roleId = roleIds[0];

                return this.Table.Where(m => m.RoleId == roleId).ToList();
            }
            else
            {
                return this.Table.Where(m => roleIds.Contains(m.RoleId)).ToList();
            }
        }

        public List<SysRole> GetSysRoles()
        {
            return this.Table.AsQueryable().ToList();
        }
    }
}
