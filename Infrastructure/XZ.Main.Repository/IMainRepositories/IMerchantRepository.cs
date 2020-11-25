using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XZ.Main.Domain;

namespace XZ.Main.Repository
{
    public interface IMerchantRepository
    {
        Task<int> CreateMerchantAsync(Merchant merchant);
        Task<Merchant> GetMerchantByMercvhantIdAsync(int merchantId);
    }
}
