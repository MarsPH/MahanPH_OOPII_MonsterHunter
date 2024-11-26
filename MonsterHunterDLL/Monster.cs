using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

////Revision history:
/// Mahan Poor Hamidian     2024/11/20      Created Monster Object 
/// /// Mahan Poor Hamidian     2024/11/23      Monster Bug Up/down fix
/// Mahan Poor Hamidian         2024/11/24  Changed Move Method

namespace MonsterHunterDLL
{

    public class Monster : Character
    {
        //constant
        private int FREEZE_TIME = 2000;
        public Direction monsterDirection;
        public int pixelsToMove = 50; //use constant
        public bool canAttack;
        public bool isRemoved = false;
        public int worth = 100;

        //constructor

        // The constructor should set the monster freeze time to 2 second. It should also receive a 
        //position(X, Y) and it should pass it to the base object constructor
        public Monster(int startX, int startY, int maximumX = 0, int maximumY = 0) : base(startX, startY)
        {
            FreezeTime = FREEZE_TIME;
            this.X = startX;
            this.Y = startY;
            this.maxX = maximumX;
            this.maxY = maximumY;
            this.HP = RandomSingleton.Next(1, 31);
            this.Armor = RandomSingleton.Next(1, 5);
        }

        //method to move the monster. The monster can only move where there is no wall and no 
        //hunter.When a monster moves, change immediately its position(X, Y).
        public override bool MoveCharacter(int Xvelocity, int Yvelocity, char[][] mapArray)

        {
            if (mapArray == null)
            {
                sValidationError = "Map is missing for Moving Hunter";
            }
            /*
            if (X < 0 || X >= MaxX) // if the x is 0 or it is bigger than the map
            {
                sValidationError = $"{X} is out of the boudnds";
            }
            if (Y < 0 || Y >= MaxY) //if the Y is 0 or it is bigger than the length of the map
            {
                sValidationError = $"{X} is out of the boudnds";
            }

            if (mapArray[Y][X] == '#')
            {
                sValidationError = "Hitting a wall.";
                return false; // if the hunter hitting a wall it will return false
            }

            if (mapArray[Y][X] == 'H')
            {
                canAttack = true;
                sValidationError = "Hunter Attemps to move into Monster";
                return false;

            }
            /* // i did till here


            else
            {
                canAttack = false;
            }
            // Ill put velcoity based on the direction.
            // if the plus numbered here is 
            // I will put clearing the position here, but it may be in moving
            /*
            mapArray[this.Y][this.X] = ' ';// it will set the previous position as empty
            this.X = X; 
            this.Y = Y;
            mapArray[Y][X] = 'H'; //and the new position as H
            Console.SetCursorPosition(X, Y);//set cursor as the Hunter
            */
            if (mapArray[this.Y + 1][this.X] == 'H' ||
               mapArray[this.Y - 1][this.X] == 'H' ||
               mapArray[this.Y][this.X + 1] == 'H' ||
               mapArray[this.Y][this.X - 1] == 'H') // if anywhere around the Monster there is a H. Perform for attack
            {
                canAttack = true;
                sValidationError = "Monster Attemps to move into Hunter";
                return false;
            }
            if (mapArray[this.Y + Yvelocity][this.X + Xvelocity] == '#')
            {
                sValidationError = "Hitting a wall.";
                return false; // if the hunter hitting a wall it will return false
            }


            else
            {
                mapArray[this.Y][this.X] = ' ';
                Console.SetCursorPosition(this.X, this.Y);
                Console.ForegroundColor = ConsoleColor.Gray;

                Console.Write(' ');

                this.X += Xvelocity;
                this.Y += Yvelocity;

                mapArray[this.Y][this.X] = 'M';
                Console.SetCursorPosition(this.X, this.Y);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write('M');
                return true;// then it returns true

            }

        }

        public void Attack(Hunter target, Shield shield = null)
        {
            //this.Strength = RandomSingleton.Next(0, 7);
            if (this.HP <= 0)
            {
                return;
            }
            if (target.HP <= 0)
            {
                


                Console.SetCursorPosition(target.X, target.Y);
                Console.ForegroundColor = ConsoleColor.Red;

                Console.Write('X');
                return;
            }
            if (target.shield == null)
            {
                target.Messages.Add($"Monster Attacked {target.Name}(HP:{target.HP}) with the power of {this.Strength}");
                target.HP = target.HP + target.Armor - this.Strength;
                //target.Messages.Add($"{target.Name} now has {target.HP} HP");

            }
            if (target.shield != null)
            {
                target.Messages.Add($"Monster Attacked {target.Name}(HP:{target.HP} Armor:{target.Armor}) with the power of {this.Strength}");
                target.HP = target.HP + target.shield.ShieldStrength + target.Armor - this.Strength;
                //target.Messages.Add($"{target.Name} now has {target.HP} HP");

                if (target.shield.DefendAndIsBroken() || target.shield.ShieldStrength <= 0)
                {
                    target.shield = null;
                    target.Messages.Add($"{target.Name}'s Shield Broken!");
                }
            }
            target.Messages.Add($"{target.Name} counterattacked Monster(HP:{this.HP}) with the power of {this.Strength}");
            if (!target.isAttacking)
            {
                this.HP = this.HP + this.Armor - target.Strength;

            }
            //target.Messages.Add($"Monster now has {this.HP}");
            target.Info = $"-{this.Strength} Damage!";
            this.canAttack = false;
        }


        public enum Direction
        {
            None, Up, Down, Left, Right, //0,1,2,3
        }
    }
}

