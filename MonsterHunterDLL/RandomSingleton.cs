using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
////Revision history:
////// Mahan Poor Hamidian     2024/11/12       Created Random Singleton Class like the battle game
namespace MonsterHunterDLL
{
    public sealed class RandomSingleton
    {
        private static readonly Random _random = new Random();

        private RandomSingleton() { } // constructor

        public static int Next(int min, int max)
        {
            return _random.Next(min, max);
        }

    }
}
