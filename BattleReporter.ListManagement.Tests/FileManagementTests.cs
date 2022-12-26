using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleReporter.ListManagement.Tests
{
    public class FileManagementTests
    {
        string testFile = "../net7.0/Resources/DemoMarineArmy.txt";
        string xmlTestFileOutput = "../net7.0/TestData/DemoMarineArmy.txt.xml";
        string jsonTestFileOutput = "../net7.0/TestData/DemoMarineArmy.txt.json";

        [Test]
        public void TestXMLGeneration() 
        {
            var processor = new ListFormatter();
            var output = processor.GetListAsXML(testFile);
            Assert.That(output, Is.Not.Null);
            var data = File.ReadAllText(xmlTestFileOutput);
            Assert.That(output, Is.EqualTo(data));
        }

        [Test]
        public void TestXMLWriting() 
        {
            var processor = new ListFormatter();
            var pathToFile = processor.OutputListAsXML(testFile);

            string fileContents = File.ReadAllText(pathToFile);
            var data = File.ReadAllText(xmlTestFileOutput);
            Assert.That(fileContents, Is.EqualTo(data));

        }

        [Test]
        public void TestJSONGeneration()
        {
            var processor = new ListFormatter();
            var output = processor.GetListAsJSON(testFile);
            Assert.That(output, Is.Not.Null);
            var data = File.ReadAllText(jsonTestFileOutput);
            Assert.That(output, Is.EqualTo(data));
        }

        [Test]
        public void TestJSONWriting()
        {
            var processor = new ListFormatter();
            var pathToFile = processor.OutputListAsJSON(testFile);

            string fileContents = File.ReadAllText(pathToFile);
            var data = File.ReadAllText(jsonTestFileOutput);
            Assert.That(fileContents, Is.EqualTo(data));

        }
    }
}
