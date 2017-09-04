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
        /// <summary>
        /// Generate a fake SalesItem using contextually fitting data
        /// </summary>
        /// <returns></returns>
        public static Faker<SalesItem> SalesItem => new Faker<SalesItem>()
            .RuleFor(s => s.Id, f => f.UniqueIndex)
            .RuleFor(s => s.Name, f => f.Commerce.ProductName())
            .RuleFor(s => s.Amount, f => new Random().Next(1, 100))
            .RuleFor(s => s.StockOnHand, f => (uint)(new Random().Next(1, 100)))
            .RuleFor(s => s.Price, f => double.Parse(f.Commerce.Price()))
            .RuleFor(s => s.SupplierCode, f => f.Finance.Iban());

        /// <summary>
        /// Generate a fake Measurement using contextuall fitting data
        /// </summary>
        /// <returns></returns>
        public static Faker<Measurement> Measurement => new Faker<Measurement>()
            .RuleFor(s => s.Id, f => f.UniqueIndex)
            .RuleFor(s => s.Suffix, f => f.Company.CompanySuffix())
            .RuleFor(s => s.Description, f => f.Company.Bs());
    }
}