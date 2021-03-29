using NUnit.Framework;
using EquipmentRentalApplication.Models;

namespace NUnitTestProject
{
    public class Tests
    {
        private RegularType _regular;
        private SpecializedType _specialized;
        private HeavyType _heavy;

        [SetUp]
        public void Setup()
        {
            _regular = new RegularType();
            _specialized = new SpecializedType();
            _heavy = new HeavyType();
        }

        [Test]
        public void RegularTest()
        {
            Assert.AreEqual(340.0M, _regular.CalculateRentalPrice(5));
        }

        [Test]
        public void SpecializedTest()
        {
            Assert.AreEqual(260.0M, _specialized.CalculateRentalPrice(5));
        }

        [Test]
        public void HeavyTest()
        {
            Assert.AreEqual(400.0M, _heavy.CalculateRentalPrice(5));
        }
    }
}