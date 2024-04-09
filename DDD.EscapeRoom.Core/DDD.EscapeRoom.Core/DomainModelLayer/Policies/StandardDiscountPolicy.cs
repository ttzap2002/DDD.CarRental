using DDD.EscapeRoom.Core.DomainModelLayer.Interfaces;
using DDD.SharedKernel.DomainModelLayer.Implementations;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.EscapeRoom.Core.DomainModelLayer.Policies
{
    // Standardowa polityka obliczania rabatu.
    // 
    // Polityki są mechanizmem rozszerzania funkcjonalności biznesowych.
    // Polityki są wstrzykiwane do agregatów.
    // Służą do modelowania tych aspektów biznesowych, 
    // które ulagają zmianie w czasie (np. polityka rabatowa)
    // i nie można na etapie projektowania agregatów 
    // przewidzieć dokładnie jaką bedą mieć postać. 
    // Można jednak ustalić ogólny interfejs współpracy, 
    // czyli np. ostateczna cena za wizytę w pokoju 
    // może być pomniejszona o rabat obliczony przez politykę rabatową
    // na podstawie takich danych jak: cena przed rabatem, 
    // liczba minut, cena jednostkowa
    public class StandardDiscountPolicy: IDiscountPolicy
    {
        public string Name { get; protected set; }

        public StandardDiscountPolicy()
        {
            this.Name = "Standard discount policy";
        }

        public Money CalculateDiscount(Money total, long numOfMinutes, Money unitPrice)
        {
            decimal percent = 0.01m;
            return total.MultiplyBy(percent);
        }
    }
}
