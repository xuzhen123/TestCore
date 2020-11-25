using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using XZ.Main.Repository;

namespace XZ.Css.Commands
{
    public class CreatedMerchantCommandHandler : IRequestHandler<CreatedMerchantCommand, int>
    {
        private IMerchantRepository _merchantRepository;
        public CreatedMerchantCommandHandler(IMerchantRepository merchantRepository) => _merchantRepository = merchantRepository;
        public async Task<int> Handle(CreatedMerchantCommand request, CancellationToken cancellationToken)
        {
            return await _merchantRepository.CreateMerchantAsync(request._merchant);
        }
    }
}
