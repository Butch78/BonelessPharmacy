using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bogus;
using BonelessPharmacyBackend;
using System;
using System.Linq;

namespace backend_tests
{
    [TestClass]
    public class RoleTests
    {
        private Faker _gen;


        [TestInitialize]
        public void SetUp()
        {
            _gen = new Faker();
        }

        [TestMethod]
        public void AssertRoleHasName()
        {
            Role testRole = new Role();
            string roleName = _gen.Commerce.ProductName();
            testRole.Name = roleName;
            Assert.AreEqual(roleName, testRole.Name);
        }

        [TestMethod]
        public void AssertRoleHasDescription()
        {
            Role testRole = new Role();
            string roleDesc = _gen.Company.Bs();
            testRole.Description = roleDesc;
            Assert.AreEqual(roleDesc, testRole.Description);
        }
    }
}