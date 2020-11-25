
using XZ.Domain;

namespace XZ.Main.Domain
{
    public class MerchantUserCreatedEvent : IDomainEvent
    {
        public MerchantUser MerchantUser { get; set; }
        public MerchantUserCreatedEvent(MerchantUser merchantUser)
        {
            this.MerchantUser = merchantUser;
        }
    }
}
