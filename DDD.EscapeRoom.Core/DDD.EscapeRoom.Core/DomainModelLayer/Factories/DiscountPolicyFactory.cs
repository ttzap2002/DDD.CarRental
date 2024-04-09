using DDD.EscapeRoom.Core.DomainModelLayer.Interfaces;
using DDD.EscapeRoom.Core.DomainModelLayer.Models;
using DDD.EscapeRoom.Core.DomainModelLayer.Policies;

namespace DDD.EscapeRoom.Core.DomainModelLayer.Factories
{
    public class DiscountPolicyFactory
    {
        public IDiscountPolicy Create(Player player)
        {
            IDiscountPolicy policy = new StandardDiscountPolicy();
            if (player.Name.Contains("a"))
                policy = new VipDiscountPolicy();

            return policy;
        }
    }
}
