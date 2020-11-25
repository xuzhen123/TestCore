using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XZ.Main.Domain;

namespace XZ.Css.Commands
{
    public class CreatedMerchantCommand : IRequest<int>
    {
        public Merchant _merchant { get; private set; }
        public CreatedMerchantCommand(Merchant merchant) => _merchant = merchant;

    }
}
