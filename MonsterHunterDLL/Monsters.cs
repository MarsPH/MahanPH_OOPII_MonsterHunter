using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

////Revision history:
/// Mahan Poor Hamidian     2024/11/20      Created Monsters Object 

namespace MonsterHunterDLL
{
    public static class Monsters
    {
        public static List<Monster> monsters = new List<Monster>();
        public static string sValidationError { get; private set; }

        public static void AddMonster (Monster monster)
        {
            monsters.Add(monster);
        }

        public static List<Monster> FindMonstersByPosition (int X, int Y) {
            sValidationError = "";
            
            if (X < 0 || Y < 0)
            {
                sValidationError = "X or Y are less than 0 and invalid";
                return null;

            }

            List<Monster> foundMonsters = new List<Monster>();
            foreach (var monster in monsters)
            {
                if (monster.X == X && monster.Y == Y)
                {
                    foundMonsters.Add(monster);
                }
            }

            if (foundMonsters.Count == 0)
            {
                sValidationError = $"No monesters found at position ({X}, {Y}).";
            }
            return foundMonsters;
        }
    }
}
