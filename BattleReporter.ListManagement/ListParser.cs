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
            ParseDetachments(contents);
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

        public void ParseDetachments(List<string> contents) 
        {
            var numberOfDetachments = contents.Count(x => x.Contains("DETACHMENT"));
            switch (numberOfDetachments)
            {
                case 0:
                    throw new Exception("NO DETACHMENTS?!");
                case 1:
                    int startPosition = contents.FindIndex(x => x.Contains("DETACHMENT"));
                    int endPosition = contents.FindIndex(x => x.Contains("Stratagems"));
                    if (endPosition == -1)
                    {
                        //No pre-game strats used
                        endPosition = contents.FindIndex(x => x.Contains("Total Command Points"));
                    }

                    List<string> MainDetachmentContents = new List<string>();
                    for (int i = startPosition; i < endPosition; i++)
                    {
                        MainDetachmentContents.Add(contents[i]);
                    }

                    var newDetachment = new DetachmentData();
                    list.Detachments.Add(newDetachment);
                    ParseDetachmentTopLevelData(MainDetachmentContents, 0);

                    var MainDetachment = list.Detachments[0];

                    ParseUnitsIntoDetachment(MainDetachmentContents, MainDetachment);

                    break;
                default:
                    //More than one
                    List<(int, string)> DetachmentMarkers = new List<(int, string)>();
                    
                    foreach (var item in contents)
                    {
                        if (item.Contains("DETACHMENT"))
                        {
                            var markerPos = contents.IndexOf(item);
                            var markerContents = item;
                            DetachmentMarkers.Add((markerPos,markerContents));
                        }
                    }

                    List<DetachmentData> detachments = new List<DetachmentData>();
                    for (int i = 0; i < DetachmentMarkers.Count; i++)
                    {
                        List<string> detachmentContents = new List<string>();
                        list.Detachments.Add(new DetachmentData());
                        var thisDetachment = list.Detachments[i];

                        int nextPos = -1;
                        if (i + 1 >= DetachmentMarkers.Count)
                        {
                            int stratPos = contents.FindIndex(x => x.Contains("Stratagems"));
                            nextPos = stratPos;

                            if (nextPos == -1)
                            {
                                //No pre-game strats used
                                int commandPos = contents.FindIndex(x => x.Contains("Total Command Points"));
                                nextPos = commandPos;
                            }
                        }
                        else
                        {
                            nextPos = DetachmentMarkers[i + 1].Item1;
                        }

                        for (int u = DetachmentMarkers[i].Item1; u < nextPos; u++)
                        {
                            detachmentContents.Add(contents[u]);
                        }

                        ParseDetachmentTopLevelData(detachmentContents, i);
                        ParseUnitsIntoDetachment(detachmentContents, thisDetachment);
                    }

                    break;
            }
        }

        private static void ParseUnitsIntoDetachment(List<string> DetachmentContents, DetachmentData Detachment)
        {
            string BattlefieldRole;
            for (int i = 3; i < DetachmentContents.Count; i++)
            {
                var unitData = new Unit();
                List<string>? unitContents = new List<string>();

                //Test for Battlefield role
                if (ListRules.IsBattlefieldRole(DetachmentContents[i]))
                {
                    BattlefieldRole = DetachmentContents[i];
                    var br = BattlefieldRole;
                }

                if (DetachmentContents[i].Contains("("))
                {
                    //Is a unit entry

                    int endOfUnit = -1;
                    var startPos = i + 1;
                    var detachCount = DetachmentContents.Count - 1;

                    for (int u = i + 1; u < DetachmentContents.Count - i ; u++) //NOT ENTERING HERE ON 2nd+ pass!!!!!
                    {
                        if (DetachmentContents[u].Contains('(')
                            || ListRules.IsBattlefieldRole(DetachmentContents[u]))
                        {
                            endOfUnit = u - 1;
                        }
                    }
                    if (endOfUnit == -1)
                    {
                        endOfUnit = i + 3; //Very hacky - assumes sergant, unit, no other entries
                    }

                    for (int z = i; z < endOfUnit; z++)
                    {
                        unitContents.Add(DetachmentContents[z]);
                    }
                    //Should have a string list of the unit's contents at this point
                    var b = unitContents[0];

                    unitData.Name = unitContents[0].Substring(0,unitContents[0].IndexOf('('));

                    if (!unitContents[0].Contains("random"))
                    {
                        var pointsEntry = unitContents[0].Substring(unitContents[0].IndexOf('(') + 1);
                        var pointsString = pointsEntry.Substring(0, pointsEntry.IndexOf(")"));

                        int pointsCost = int.Parse(pointsString);
                        unitData.PointsValue = pointsCost;
                    }

                    unitData.WarlordTraits = unitContents.Find(x => x.Contains("Traits"));
                    unitContents.RemoveAll(x => x.Contains("Traits"));
                    unitData.Relics = unitContents.Find(x => x.Contains("Relics"));
                    unitContents.RemoveAll(x => x.Contains("Relics"));
                    unitData.PsychicPowers = unitContents.Find(x => x.Contains("Psychic Powers"));
                    unitContents.RemoveAll(x => x.Contains("Psychic Powers"));
                    unitData.Prayers = unitContents.Find(x => x.Contains("Prayers"));
                    unitContents.RemoveAll(x => x.Contains("Prayers"));
                    unitContents.RemoveAll(x => x.Contains('('));


                    unitContents.Sort();
                    foreach (var item in unitContents)
                    {
                        unitData.Description += item + '\n';
                    }

                    Detachment.Units.Add(unitData);
                }
            }
        }

        private void ParseDetachmentTopLevelData(List<string> detachment, int detachmentIndex)
        {
            list.Detachments[detachmentIndex].DetachmentType = detachment[0];
            list.Detachments[detachmentIndex].Faction = detachment[1];
            if (!ListRules.IsBattlefieldRole(detachment[2]))
            {
                list.Detachments[detachmentIndex].SubFaction = detachment[2];
            }
        }
    }
}