using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZ.Infrastructure;
using XZ.Main.Domain;

namespace XZ.Main.Repository
{

    public class SysUserAndRoleRepository : Repository<SysUserAndRole, MainContext>, ISysUserAndRoleRepository
    {
        public SysUserAndRoleRepository(MainContext domainContext) : base(domainContext) { }

        //TODO:??????
        public async Task<SysUserAndRole> GetRoleAndSysUserById(int id)
        {
            return await this.FindAsync(id);
        }

        public Task<List<SysUserAndRole>> GetRoleAndSysUsersAsync()
        {
            return Task.FromResult(this.DbContext.Set<SysUserAndRole>().AsQueryable().ToList());
        }
        public Task<SysUserAndRole> GetRoleAndSysUserByUserIdAsync(int sysUserId)
        {
            return Task.FromResult(this.DbContext.Set<SysUserAndRole>().AsQueryable().FirstOrDefault(m => m.SysUserId == sysUserId));
        }
    }
}
