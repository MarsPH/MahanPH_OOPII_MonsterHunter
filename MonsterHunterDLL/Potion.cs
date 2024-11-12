using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

////revision history
///Mahan Poor Hamidian 2024/11/12 Created Potion Object

namespace MonsterHunterDLL
{

    public class Potion
    {
        private const int MAX_ROLL = 0;

        public PotionType myPotionType { get; private set; }

        //constructor
        public Potion()
        { //selects a potion type based on the dice roll
            int rolledNumber = RandomSingleton.Next(1, MAX_ROLL);
            myPotionType = checkPotionType(rolledNumber);
        }

        private PotionType checkPotionType(int rolledNumber)
        {
            switch (rolledNumber)
            {
                case 1:
                    return PotionType.Poisoned;
                case 2:
                case 3:
                    return PotionType.Speed; 
                case 4:
                case 5:
                    return PotionType.Invisibility; 
                case 6:
                    return PotionType.Strength;
                default:
                    throw new ArgumentOutOfRangeException("rolledNumber", "Something wrong with the dice roll potion");
                   
            }
        }

    }

    public enum PotionType
    {
        Strength, //increase the hunter attacl /defense. also missing hp restored
        Poisoned, // decreases the hunter/defense and speed (freeze time)
        Invisibility, //makes the hunter able to walk through monsters without engaging
        Speed //makes the hunter 2 times faster
    }
}
