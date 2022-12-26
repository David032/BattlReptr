using BattleReporter.ListManagement;
using Moq;

namespace BattleReporter.ListManagement.Tests
{
    public class BasicTests
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

        [Test]
        public void CanParseOneDetachmentForceMarines() 
        {
            ListParser listParser = new();
            listParser.ParseForceFromFile("../net7.0/Resources/DemoMarineArmy.txt");
            listParser.ParseDetachments(listParser.GetParsedContents());
            var data = listParser.GetList();
            var detachment = data.Detachments[0];
            Assert.Multiple(() =>
            {
                Assert.That(data.Detachments, Has.Count.EqualTo(1));
                Assert.That(detachment.Units, Has.Count.EqualTo(3));
                Assert.That(detachment.Faction, Is.EqualTo("\t- Faction: Adeptus Astartes"));
                Assert.That(detachment.SubFaction, Is.EqualTo("\t- Sub-faction: Ultramarines"));
            });
            //Assert.Multiple(() =>
            //{
            //    Assert.That(detachment.Units, Contains.Item(new Unit() { Name = "Primaris Captain" }));
            //    Assert.That(detachment.Units, Contains.Item(new Unit() { Name = "Assault Intercessor Squad" }));
            //    Assert.That(detachment.Units, Contains.Item(new Unit() { Name = "Outrider Squad" }));

            //});
        }
        [Test]
        public void CanParseOneDetachmentForceNecrons()
        {
            ListParser listParser = new();
            listParser.ParseForceFromFile("../net7.0/Resources/DemoNecronArmy.txt");
            listParser.ParseDetachments(listParser.GetParsedContents());
            var data = listParser.GetList();
            var detachment = data.Detachments[0];
            Assert.Multiple(() =>
            {
                Assert.That(data.Detachments, Has.Count.EqualTo(1));
                Assert.That(detachment.Units, Has.Count.EqualTo(3));
                Assert.That(detachment.Faction, Is.EqualTo("\t- Faction: Necrons"));
                Assert.That(detachment.SubFaction, Is.EqualTo("\t- Sub-faction: Szarekhan"));
            });
            //Assert.Multiple(() =>
            //{
            //    Assert.That(detachment.Units, Contains.Item(new Unit() { Name = "Primaris Captain" }));
            //    Assert.That(detachment.Units, Contains.Item(new Unit() { Name = "Assault Intercessor Squad" }));
            //    Assert.That(detachment.Units, Contains.Item(new Unit() { Name = "Outrider Squad" }));

            //});
        }

        [Test]
        public void CanParseTwoDetachmentForceChaos()
        {
            ListParser listParser = new();
            listParser.ParseForceFromFile("../net7.0/Resources/DemoChaosArmy.txt");
            listParser.ParseDetachments(listParser.GetParsedContents());
            var data = listParser.GetList();
            var detachment = data.Detachments[0];

            Assert.Multiple(() =>
            {
                Assert.That(data.Detachments, Has.Count.EqualTo(2));
                //Assert.That(detachment.Units, Has.Count.EqualTo(3));
                //Assert.That(detachment.Faction, Is.EqualTo("\t- Faction: Necrons"));
                //Assert.That(detachment.SubFaction, Is.EqualTo("\t- Sub-faction: Szarekhan"));
            });
        }
    }
}