using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bogus;
using BonelessPharmacyBackend;
using System;
using System.Linq;

namespace backend_tests
{
    [TestClass]
    public class StaffTests
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
        /// Ensuring that a Staff Member has a name
        /// </summary>
        [TestMethod]
        public void AssertStaffHasName()
        {
            Staff testItem  = new Staff(); 
            string staffName = _gen.Name.FindName();
            testItem.Name = staffName;
            Assert.AreEqual(staffName, testItem.Name);
        }

        //TODO Link Staff and Role
        [TestMethod]
        public void AssertSalesItemHasMeasurement()
        {
            using (var db = new Db())
            {

                Staff testItem = ModelFactory.Staff;
                Role testRole = ModelFactory.Role;

                testItem.RoleId = testRole.Id;
                db.Roles.Add(testRole);
                db.Staff.Add(testItem);

                //Get the staff's State
                var roles = (db.ChangeTracker.Context as Db).Roles.Local.ToList();
                Assert.AreEqual(testRole, roles.First().Name);
            }
            
        }

        /// <summary>
        /// Ensure the Staff has a password
        /// </summary>
        [TestMethod]
        public void AssertStaffHasPassword()
        {
            Staff testItem  = new Staff(); 
            string password = _gen.Internet.Password();
            testItem.Password = password;
            Assert.AreEqual(password, testItem.Password);
        }





    }
}
