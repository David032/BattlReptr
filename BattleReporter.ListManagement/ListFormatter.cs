using BattleReporter.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleReporter.ListManagement
{
    public class ListFormatter
    { 
        public string OutputListAsXML(string pathToFile) 
        {
            string? directory = Path.GetDirectoryName(pathToFile);
            string? fileName = Path.GetFileName(pathToFile);

            var Parser = new ListParser();
            Parser.ParseForce(null, pathToFile);
            var seriliser = new SerialiserXML();
            string path = $"{directory}/{fileName}.xml";
            seriliser.SerialiseToFile(path, Parser.GetList());
            return path;
        }

        public string GetListAsXML(string pathToFile) 
        {
            var Parser = new ListParser();
            Parser.ParseForce(null, pathToFile);
            var seriliser = new SerialiserXML();
            string output = seriliser.SerialiseToString(Parser.GetList());
            return output;
        }

        public string OutputListAsJSON(string pathToFile)
        {
            string? directory = Path.GetDirectoryName(pathToFile);
            string? fileName = Path.GetFileName(pathToFile);

            var Parser = new ListParser();
            Parser.ParseForce(null, pathToFile);
            var seriliser = new SerialiserJSON();
            string path = $"{directory}/{fileName}.json";
            seriliser.SerialiseToFile(path, Parser.GetList());
            return path;
        }

        public string GetListAsJSON(string pathToFile)
        {
            var Parser = new ListParser();
            Parser.ParseForce(null, pathToFile);
            var seriliser = new SerialiserJSON();
            string output = seriliser.SerialiseToString(Parser.GetList());
            return output;
        }

    }
}
