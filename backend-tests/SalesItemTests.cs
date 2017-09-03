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
        /// Ensure that a SalesItem has a PLU associated with it
        /// </summary>
        public void AssertSalesItemHasPLU()
        {
            SalesItem testItem = new SalesItem();
            string plu = _gen.Finance.Iban();
            testItem.PLU = plu;
            Assert.AreEqual(plu, testItem.PLU);
        }


    }
}
