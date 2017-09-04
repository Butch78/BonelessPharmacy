using System;
using Bogus;

namespace BonelessPharmacyBackend
{
    /// <summary>
    /// Use ModelFactory to define 
    /// </summary>
    /// <remarks>
    /// Ignore the intellisense issue, it's fine
    /// </summary>
    public class ModelFactory
    {
        public Faker<SalesItem> SalesItem => new Faker<SalesItem>()
            .RuleFor(s => s.Id, f => f.UniqueIndex)
            .RuleFor(s => s.Name, f => f.Commerce.ProductName())
            .RuleFor(s => s.Amount, f => new Random().Next(1, 100))
            .RuleFor(s => s.StockOnHand, f => (uint)(new Random().Next(1, 100)))
            .RuleFor(s => s.Price, f => double.Parse(f.Commerce.Price()))
            .RuleFor(s => s.SupplierCode, f => f.Finance.Iban());
    }
}