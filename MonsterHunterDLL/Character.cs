using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//////revision history
///Mahan Poor Hamidian 2024/11/18 Created Character Object

namespace MonsterHunterDLL
{
    public abstract class Character
    {   //private variables
        private int x;//x position
        private int y;// y position

        private int hp; // current hp
        private int armor;
        private int strength;

        protected int maxX; // map max width
        protected int maxY; // map max height

        //const
        private const int MINIMUM_SIZE = 0;
        private const int MAXIMUM_HP = 30;
        private const int MAX_STENGTH = 7;
        private const int MAX_ARMOR = 4;

        public int FreezeTime; //number of milisecond that the charater will be forzen.

        public string sValidationError = ""; // error message 
        public string ValidationError
        {
            get
            {
                return sValidationError;
            }
        }

        public int X
        {
            get { return x; }
            set
            {
                sValidationError = "";

                if (value < 0) // if value less than 0
                {
                    sValidationError = $"X value cannot be less than 0";
                }
                else if (maxX != 0 && value > maxX) //if value bigger than the max.Width
                {
                    sValidationError = $"X cannot be more than {maxX}";
                }
                else
                {
                    x = value;
                }
            }
        }
        public int Y
        {
            get { return y; }
            set
            {
                sValidationError = "";

                if (value < 0)
                {
                    sValidationError = $"Y value cannot be less than 0";
                }
                else if (maxY != Y && value > maxY)
                {
                    sValidationError = $"Y cannot be more than {maxY}";
                }
                else
                {
                    y = value;
                }
            }
        }

        public int MaxX
        {
            get { return maxX; }
            set
            {
                sValidationError = "";
                if (value <= 0)
                {
                    sValidationError = "MaxX must be greater than 0.";
                }
                else
                {
                    maxX = value;
                    if (X > maxX)
                    {
                        sValidationError = $"Current x = {X} surpasses the MaxX value of {maxX}";
                    }
                }
            }
        }

        public int MaxY
        {
            get { return maxY; }
            set
            {
                sValidationError = "";
                if (value <= 0)
                {
                    sValidationError = "MaxY must be greater than 0";
                }
                else
                {
                    maxY = value;

                    if (Y > maxY)
                    {
                        sValidationError = $"Current Y poisiton {y} surpassed the MaxY value of {maxY}; ";
                    }
                }
            }
        }
        
        public int HP
        {
            get { return hp; }
            set
            {
                sValidationError = "";
                if (value < 0)
                {
                    value = 0; // if the hp is less than 0 it will set as 0
                }
                if (value > MAXIMUM_HP)
                {
                    sValidationError = $"Character HP cannot be more than {MAXIMUM_HP}";
                }
                else
                {
                    hp = value;
                }
            }
        }

        public int Armor
        {
            get { return armor; }
            set
            {
                sValidationError = "";
                if (value < 0)
                {
                    sValidationError = $"Armor cannot be less than 0";
                }
                if (value > MAX_ARMOR)
                {
                    sValidationError = $"Armor cannot be more than {sValidationError}";
                }
                else
                {
                    armor = value;
                }
            }
        }
        public int Strength
        {
            get { return strength; }
            set
            {
                sValidationError = "";
                if (value < 0)
                {
                    sValidationError = "Strenth cannot be less than 0";
                }
                if (value > MAX_STENGTH)
                {
                    sValidationError = $"Strength cannot be more than {MAX_STENGTH}";
                }
                else
                {
                    strength = value;
                }
            }
        }

        protected abstract bool MoveCharacter (int X, int Y, char[][] mapArray); //abstract method to move the character

        //constructor
        public Character (int X, int Y, int maxX = 0, int maxY = 0) //
        {
            this.X = X;
            this.Y = Y;
            this.maxX = maxX;
            this.maxY = maxY;
        }

        public bool CheckIsCharacterDead() //check if the character is dead (HP lower or equal to zero).
        {
            if (HP == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }

    


}

