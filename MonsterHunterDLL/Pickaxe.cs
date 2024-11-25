using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

////revision history
///Mahan Poor Hamidian 2024/11/12 Created Pickaxe Object
namespace MonsterHunterDLL
{
    public class Pickaxe
    {
        //constants
        const int BREAK_CHANCE = 3;

        public bool UseAndIsBroken()
        {
            bool isBroken = RandomSingleton.Next(1, BREAK_CHANCE + 1) == 1; //check if the pickaxe is broken
            return isBroken;
        }
    }
}
