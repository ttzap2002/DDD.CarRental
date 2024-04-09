using DDD.EscapeRoom.Core.DomainModelLayer.Interfaces;
using DDD.SharedKernel.DomainModelLayer.Implementations;

namespace DDD.EscapeRoom.Core.DomainModelLayer.Policies
{
    // Polityka obliczania rabatu dla VIPów.
    // 
    // Polityki są mechanizmem rozszerzania funkcjonalności biznesowych.
    // Polityki są wstrzykiwane do agragatów.
    // Służą do modelowania tych aspektów biznesowych, 
    // które ulagają zmianie w czasie (np. polityka rabatowa)
    // i nie można na etapie projektowania agregatów 
    // przewidzieć dokładnie jaką bedą mieć postać. 
    // Można jednak ustalić ogólny interfejs współpracy, 
    // czyli np. ostateczna cena za wizytę w pokoju 
    // może być pomniejszona o rabat obliczony przez politykę
    // na podstawie takich danych jak: cena przed rabatem, 
    // liczba minut, cena jednostkowa
    // Na etapie projektowania agregatu nie znamy konkretnej polityki,
    // ale nie przeszkadza to by zaimplementować obliczanie ceny końcowej.
    public class VipDiscountPolicy : IDiscountPolicy
    {
        public string Name { get; protected set; }

        public VipDiscountPolicy()
        {
            this.Name = "Vip discount policy";
        }

        public Money CalculateDiscount(Money total, long numOfMinutes, Money unitPrice)
        {
            decimal percent = 0.01m;
            if (numOfMinutes > 30)
                percent = 0.05m;

            return total.MultiplyBy(percent);
        }
    }
}
