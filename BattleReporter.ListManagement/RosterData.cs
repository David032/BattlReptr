using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleReporter.ListManagement
{
    public class ListData
    {
        public string Name;
        public string ArmyFaction;
        public string GameMode;
        public string ArmySize;
        public List<DetachmentData> Detachments = new List<DetachmentData>();
    }

    public class DetachmentData
    {
        public string DetachmentType;
        public string Faction;
        public string SubFaction;
        public List<Unit> Units = new List<Unit>();
    }

    public class Unit 
    {
        public string Name = "";
        public int PointsValue = 0;
        public string Description = "";
        public string? WarlordTraits;
        public string? Relics;
        public string? PsychicPowers;
        public string? Prayers;
        public bool Warlord;
    }
}
