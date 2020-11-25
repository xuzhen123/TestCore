
using XZ.Domain;

namespace XZ.Main.Domain
{
    public class MerchantCreatedEvent : IDomainEvent
    {
        public Merchant Merchant { get; set; }
        public MerchantCreatedEvent(Merchant merchant)
        {
            this.Merchant = merchant;
        }
    }
}
