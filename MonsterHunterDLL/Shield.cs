using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterHunterDLL
{
    public class Shield
    {
        //constants
        const int MIN_DEFENSE = 3;
        const int MAX_DEFENSE = 6;
        const int BREAK_CHANCE = 4;

        // to store data privatley
        private readonly int shieldStrength;

        //set the public to the readonly
        public int ShieldStrength => shieldStrength;

        public Shield()// constructor that when the object is created it will set the shield strenght randomly
        {
            shieldStrength = RandomSingleton.Next(MIN_DEFENSE, MAX_DEFENSE + 1);
        }

        public bool DefendAndIsBroken()
        {
            bool isBroken = RandomSingleton.Next(1, BREAK_CHANCE+ 1) == 1;
            return isBroken;
        }
    }
}
