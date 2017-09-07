using Microsoft.VisualStudio.TestTools.UnitTesting;
using BonelessPharmacyBackend;

namespace backend_tests
{
    [TestClass]
    public class MeasurementTests
    {
        [DataTestMethod]
        [DataRow("mg")]
        [DataRow("capsules")]
        [DataRow("days")]
        public void TestMeasurementHasSuffix(string suffix)
        {
            Measurement testMeasurement = new Measurement();
            testMeasurement.Suffix = suffix;
            Assert.AreEqual(suffix, testMeasurement.Suffix);
        }
    }
}
