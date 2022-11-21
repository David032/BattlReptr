using BattleReporter.ListManagement;

namespace BattleReporter.ListManagement.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CanCreateParser()
        {
            ListParser listParser = new();
            Assert.That(listParser, Is.InstanceOf<ListParser>());
        }

        [Test]
        public void CanReadFileFromPath()
        {
        }
    }
}