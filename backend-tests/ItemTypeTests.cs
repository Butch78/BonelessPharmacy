using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bogus;
using BonelessPharmacyBackend;
using System;
using System.Linq;

namespace backend_tests
{
    [TestClass]
    public class ItemTypeTests
    {
        private Faker _gen;


        [TestInitialize]
        public void SetUp()
        {
            _gen = new Faker();
        }

        [TestMethod]
        public void AssertItemTypeHasName()
        {
            ItemType testItemType = new ItemType();
            string typeName = _gen.Commerce.ProductName();
            testItemType.Name = typeName;
            Assert.AreEqual(typeName, testItemType.Name);
        }

        [TestMethod]
        public void AssertItemTypeHasDescription()
        {
            ItemType testItemType = new ItemType();
            string typeDesc = _gen.Company.Bs();
            testItemType.Description = typeDesc;
            Assert.AreEqual(typeDesc, testItemType.Description);
        }
    }
}