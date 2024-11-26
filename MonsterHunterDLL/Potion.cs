using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Note Tomyself. I can put all the potion amplifiers in constants
////revision history
///Mahan Poor Hamidian 2024/11/12 Created Potion Object
///Mahan Poor Hamidian 2024/11/25 Created Potion Interface
///

namespace MonsterHunterDLL
{
    interface IPotionStates
    {
        void StartPotion(Hunter hunter);
        void StopPotion(Hunter hunter);
    }
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
    public class PotionStrength : IPotionStates 
    {
        public void StartPotion(Hunter hunter)
        {
            hunter.Strength *= 2;
            hunter.Armor = (int)(hunter.Armor * 1.5);
            hunter.HP = hunter.maximumHP;
        }
        public void StopPotion(Hunter hunter)
        {
            hunter.Strength /= 2;
            hunter.Armor = (int)(hunter.Armor / 1.5);
            //no hp lowered as it is a oneTime thing,
        }
    }
    public class PotionPoison : IPotionStates
    {
        public void StartPotion(Hunter hunter)
        {
            hunter.Strength /= 2; //his/her attack and defense are decreased by 50%.
            hunter.HP -= 5;//his/her HP is dropped by 5
            hunter.FreezeTime = (int)(hunter.FreezeTime * 1.5); //FreezeTime by 25% increase
        }
        public void StopPotion(Hunter hunter)
        {
            hunter.Strength *= 2;
            hunter.FreezeTime = (int)(hunter.FreezeTime / 1.5); //FreezeTime by 25% decrease

            //no hp lowered as it is a oneTime thing,
        }
    }
    public class PotionInvisible : IPotionStates
    {
        public void StartPotion(Hunter hunter)
        {
            hunter.IsInvisible = true;
        }
        public void StopPotion(Hunter hunter)
        {
            hunter.IsInvisible = false;
        }
    }

    public class PotionFast : IPotionStates
    {
        public void StartPotion(Hunter hunter)
        {
            hunter.FreezeTime /= 2;
        }
        public void StopPotion(Hunter hunter)
        {
            hunter.FreezeTime *= 2;
        }
    }

    public class PotionNormal : IPotionStates
    {
        public void StartPotion(Hunter hunter)
        {
            //
        }
        public void StopPotion(Hunter hunter)
        {
            //
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
