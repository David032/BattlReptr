using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleReporter.ListManagement
{
    public static class ListRules
    {
        static string[] BattlefieldRoles = { "HQ", "Troops", "Elites", "Fast Attack", "Heavy Support" };

        public static bool IsBattlefieldRole(string contents) 
        {
            foreach (var item in BattlefieldRoles)
            {
                if (contents == item)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
