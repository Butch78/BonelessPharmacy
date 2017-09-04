using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bogus;
using BonelessPharmacyBackend;

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
    }
}
