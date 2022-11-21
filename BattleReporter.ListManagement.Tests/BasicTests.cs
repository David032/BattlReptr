using BattleReporter.ListManagement;
using Moq;

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
            ListParser listParser = new();
            listParser.ParseForceFromFile("../net7.0/Resources/DemoMarineArmy.txt");
            Assert.That(listParser.GetParsedContents(), Is.Not.Empty);
        }

        [Test]
        public void CanParseTopLevelForce()
        {
            ListParser listParser = new();
            listParser.ParseForceFromFile("../net7.0/Resources/DemoMarineArmy.txt");
            listParser.ParseTopLevelData(listParser.GetParsedContents());
            var data = listParser.GetList();

            Assert.Multiple(() =>
            {
                Assert.That(data.Name, Is.EqualTo("DEMO MARINE ARMY"));
                Assert.That(data.ArmyFaction, Is.EqualTo("Army Faction: Imperium"));
                Assert.That(data.GameMode, Is.EqualTo("\t- Game Mode: Eternal War"));
                Assert.That(data.ArmySize, Is.EqualTo("\t- Army Size: Combat Patrol"));
            });
        }
    }
}