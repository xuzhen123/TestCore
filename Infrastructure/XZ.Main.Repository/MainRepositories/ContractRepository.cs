
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XZ.Infrastructure;
using XZ.Main.Domain;

namespace XZ.Main.Repository
{
    public class ContractRepository : Repository<Contract, MainContext>, IContractRepository
    {
        public ContractRepository(MainContext dbContext) : base(dbContext) { }

        public List<Contract> GetContracts()
        {
            return this.DbContext.Set<Contract>().AsQueryable().ToList();
        }

        public Contract GetContractById(int contractId)
        {
            //using (var transaction = this.DbContext.BeginTransaction())
            //{
            //    this.DbContext.CommitTransaction(transaction);
            //}

            return this.Find(contractId);
        }
    }
}
