using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleReporter.ListManagement
{
    internal class ListData
    {
        public string Name;
        public string ArmyFaction;
        public string GameMode;
        public string ArmySize;
        public List<DetachmentData> Detachments;
    }

    class DetachmentData
    {
        public string DetachmentType;
        public string Faction;
        public string SubFaction;
    }
}
