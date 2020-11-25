using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XZ.Infrastructure;
using XZ.Main.Domain;

namespace XZ.Main.Repository
{
    public class MerchantUserRepository : Repository<MerchantUser, MainContext>, IMerchantUserRepository
    {
        public MerchantUserRepository(MainContext dbContext) : base(dbContext) { }

        public async Task CreateUserAsync(MerchantUser merchantUser)
        {
            await this.InsertAsync(merchantUser);
        }

        public async Task<MerchantUser> GetMerchantUserById(int userId)
        {
            return await this.FindAsync(userId);
        }
    }
}
