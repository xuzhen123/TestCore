using System.Collections.Generic;
using System.Threading.Tasks;
using XZ.Infrastructure;
using XZ.Main.Domain;

namespace XZ.Main.Repository
{
    public interface IContractRepository : IRepository<Contract>
    {
        List<Contract> GetContracts();

        Contract GetContractById(int contractId);
    }
}
