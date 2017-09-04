using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bogus;
using BonelessPharmacyBackend;
using System;

namespace backend_tests
{
    [TestClass]
    public class SalesItemTests
    {
        private Faker _gen;

        /// <summary>
        /// The SetUp code for each test
        /// </summary>
        /// <remarks>
        /// Creating a new Faker instance every test is a good way to assure the data
        /// being generated is unique every time (despite it possibly not making a great)
        /// difference
        /// </remarks>
        [TestInitialize]
        public void SetUp()
        {
            _gen = new Faker();
        }

        /// <summary>
        /// Ensure that a SaleItem has a Name property
        /// </summary>
        [TestMethod]
        public void AssertSalesItemHasName()
        {
            SalesItem testItem = new SalesItem(); 
            string productName = _gen.Commerce.ProductName();
            testItem.Name = productName;
            Assert.AreEqual(productName, testItem.Name);
        }


        /// <summary>
        /// Ensure that a SalesItem has a Supplier Code associated with it
        /// </summary>
        [TestMethod]
        public void AssertSalesItemHasSupplierCode()
        {
            SalesItem testItem = new SalesItem();
            string supplierCode = _gen.Finance.Iban();
            testItem.SupplierCode = supplierCode;
            Assert.AreEqual(supplierCode, testItem.SupplierCode);
        }

        [TestMethod]
        public void AssertSalesItemHasPrice()
        {
            SalesItem testItem = new SalesItem();
            double price = double.Parse(_gen.Commerce.Price());
            testItem.Price = price;
            Assert.AreEqual(price, testItem.Price);
        }

        [TestMethod]
        public void AssertSalesItemHasSOH()
        {
            SalesItem testItem = new SalesItem();
            uint soh = (uint)(new Random().Next(1, 100));
            testItem.StockOnHand = soh;
            Assert.AreEqual(soh, testItem.StockOnHand);
        }

        [TestMethod]
        public void AssertSalesItemHasAmount()
        {
            SalesItem testItem = new SalesItem();
            int amount = new Random().Next(1,100);
            testItem.Amount = amount;
            Assert.AreEqual(amount, testItem.Amount);
        }

        [TestMethod]
        public void AssertSalesItemHasMeasurement()
        {
            using (var db = new Db())
            {
                SalesItem testItem = ModelFactory.SalesItem;
                Measurement testMeasurement = ModelFactory.Measurement;
                testItem.MeasurementId = testMeasurement.Id;
                db.Measurements.Add(testMeasurement);
                db.SalesItems.Add(testItem);
            }
            
        }
    }
}
