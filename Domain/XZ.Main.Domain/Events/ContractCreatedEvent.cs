using System;
using System.Collections.Generic;
using System.Text;
using XZ.Domain;

namespace XZ.Main.Domain
{
    public class ContractCreatedEvent : IDomainEvent
    {
        public Contract Contract { get; private set; }

        public ContractCreatedEvent(Contract contract)
        {
            this.Contract = contract;
        }
    }
}
