using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XZ.Infrastructure;
using XZ.Main.Domain;

namespace XZ.Main.Repository
{
    public class MerchantRepository : Repository<Merchant, MainContext>, IMerchantRepository
    {
        public MerchantRepository(MainContext dbcontext) : base(dbcontext) { }

        public async Task<int> CreateMerchantAsync(Merchant merchant)
        {
           return await this.InsertAsync(merchant);
        }

        public async Task<Merchant> GetMerchantByMercvhantIdAsync(int merchantId)
        {
            return await this.FindAsync(merchantId);
        }
    }
}
