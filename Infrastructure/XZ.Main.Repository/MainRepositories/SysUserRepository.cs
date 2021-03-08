using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZ.Infrastructure;
using XZ.Main.Domain;

namespace XZ.Main.Repository
{
    public class SysUserRepository : Repository<SysUser, MainContext>, ISysUserRepository
    {
        public SysUserRepository(MainContext domainContext) : base(domainContext) { }

        public SysUser GetSysUserByEmailAndPwd(string email, string password)
        {
            return this.DbContext.Set<SysUser>().Where(m => m.Email == email && m.Password == password).FirstOrDefault();
        }

        public List<SysUser> GetSysUsers()
        {
            return this.DbContext.Set<SysUser>().AsQueryable().ToList();
        }

        public SysUser GetUserById(int sysUserId)
        {
            return this.Find(sysUserId);
        }

        public async Task<SysUser> GetSysUserByNameAsync(string sysUserName)
        {
            return await Task.FromResult(this.DbContext.Set<SysUser>().AsQueryable().FirstOrDefault(m => m.SysUserName == sysUserName));
        }
    }
}
