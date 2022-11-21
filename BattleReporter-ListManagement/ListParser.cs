using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BattleReporter.ListManagement
{
    public class ListParser
    {
        ListData list;
        List<string> contents;

        public ListParser()
        {
            list = new ListData();
            contents = new List<string>();
        }

        public void ParseForceFromFile(string pathToList)
        {
            contents = File.ReadAllLines(pathToList).ToList();
            contents.RemoveAll(content => content == string.Empty);
        }

        public void ParseForce(string list = "", string pathToListFile = "")
        {
            if (!string.IsNullOrEmpty(pathToListFile))
            {
                ParseForceFromFile(pathToListFile);
            }
            else
            {
                //File being passed in directly?
            }
            ParseTopLevelData(contents);
        }

        #region Getters
        public ListData GetList() { return list; }
        public List<string> GetParsedContents() { return contents; }
        #endregion

        public void ParseTopLevelData(List<string> contents)
        {
            list.Name = contents[0];
            list.ArmyFaction = contents[1];
            list.GameMode = contents[2];
            list.ArmySize = contents[3];
        }

        void RemoveSystemCharacters(string filepath)
        {
            string file = File.ReadAllText(filepath);
            file = file.Replace('\r', ' ');
            file = file.Replace("\t", string.Empty);
            file = file.Replace('\n', ' ');
            File.WriteAllText(filepath, file);
        }
    }
}