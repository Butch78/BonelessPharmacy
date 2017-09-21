using System;
using Bogus;

namespace BonelessPharmacyBackend
{
    /// <summary>
    /// Use ModelFactory to define fake data structures
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
        /// Generate a fake Measurement using contextual fitting data
        /// </summary>
        /// <returns></returns>
        public static Faker<Measurement> Measurement => new Faker<Measurement>()
            .RuleFor(s => s.Id, f => f.UniqueIndex)
            .RuleFor(s => s.Suffix, f => f.Company.CompanySuffix())
            .RuleFor(s => s.Description, f => f.Company.Bs());

        /// <summary>
        /// Generate a fake ItemType using contextual fitting data
        /// </summary>
        /// <returns></returns>
        public static Faker<ItemType> ItemType => new Faker<ItemType>()
            .RuleFor(s => s.Id, f => f.UniqueIndex)
            .RuleFor(s => s.Name, f => f.Commerce.ProductName())
            .RuleFor(s => s.Description, f => f.Company.Bs());
        public static Faker<Role> Role => new Faker<Role>()
            .RuleFor(s => s.Id, f => f.UniqueIndex)
            .RuleFor(s => s.Name, f => f.Commerce.ProductName())
            .RuleFor(s => s.Description, f => f.Company.Bs());


        public static Faker<Staff> Staff => new Faker<Staff>()    
            .RuleFor(s => s.Id, f => f.UniqueIndex)
            .RuleFor(s => s.Name, f => f.Name.FindName())
            .RuleFor(s => s.PhoneNumber, f => f.Person.Phone)
            .RuleFor(s => s.Password, f => f.Internet.Password());

        public static Faker<OrderItem> OrderItem => new Faker<OrderItem>()
            .RuleFor(s => s.Id, f => f.UniqueIndex)
            .RuleFor(s => s.Price, f => double.Parse(f.Commerce.Price()))
            .RuleFor(s => s.Quantity, f => new Random().Next(1, 100));

        public static Faker<Supplier> Suppliers => new Faker<Supplier>()
            .RuleFor(s => s.Id, f => f.UniqueIndex)
            .RuleFor(s => s.Name, f => f.Company.CompanyName())
            .RuleFor(s => s.ABN, f => f.Finance.Bic())
            .RuleFor(s => s.Address, f => f.Address.FullAddress())
            .RuleFor(s => s.Email, f => f.Internet.Email())
            .RuleFor(s => s.PhoneNumber, f => f.Person.Phone);

        public static Faker<Customer> Customers => new Faker<Customer>()
            .RuleFor(s => s.Id, f => f.UniqueIndex)
            .RuleFor(s => s.Name, f => f.Name.FindName())
            .RuleFor(s => s.PhoneNumber, f => f.Person.Phone);
            
        public static Faker<Order> Order => new Faker<Order>()
            .RuleFor(s => s.Id, f => f.UniqueIndex)
            .RuleFor(s => s.CreatedAt, f => f.Date.Recent())
            .RuleFor(s => s.OrderStatus, f => (OrderStatus)(Enum.GetValues(typeof(OrderStatus)).GetValue(new Random().Next(Enum.GetValues(typeof(OrderStatus)).Length))));
    }
}