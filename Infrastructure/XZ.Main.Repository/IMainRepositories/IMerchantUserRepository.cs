using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XZ.Main.Domain;

namespace XZ.Main.Repository
{
    public interface IMerchantUserRepository
    {
        Task CreateUserAsync(MerchantUser merchantUser);

        Task<MerchantUser> GetMerchantUserById(int userId);
    }
}
