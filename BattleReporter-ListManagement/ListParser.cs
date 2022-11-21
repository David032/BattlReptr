using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleReporter.ListManagement
{
    public class ListParser
    {
        public ListParser()
        {

        }

        public void ParseForceFromList(string pathToList)
        {
            string contents = File.ReadAllText(pathToList);
        }

        public void ParseForce(string List)
        {
            throw new System.NotImplementedException();
        }
    }
}