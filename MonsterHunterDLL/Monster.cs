using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

////Revision history:
/// Mahan Poor Hamidian     2024/11/20      Created Monster Object 

namespace MonsterHunterDLL
{

    public class Monster : Character
    {
        //constant
        private int FREEZE_TIME = 2000;
        public Direction monsterDirection;
        public int pixelsToMove = 50; //use constant


        //constructor

        // The constructor should set the monster freeze time to 2 second. It should also receive a 
        //position(X, Y) and it should pass it to the base object constructor
        public Monster(int startX, int startY) : base(startX, startY)
        {
            FreezeTime = FREEZE_TIME;
        }

        //method to move the monster. The monster can only move where there is no wall and no 
        //hunter.When a monster moves, change immediately its position(X, Y).
        public override bool MoveCharacter(int X, int Y, char[][] mapArray)

        {
            if (mapArray == null)
            {
                sValidationError = "Map is missing for Moving Monster";
            }

            if (X < 0 || X >= MaxX) // if the x is 0 or it is bigger than the map
            {
                sValidationError = $"monster attept to go {X} is out of the boudnds";
            }
            if (Y < 0 || Y >= MaxY) //if the Y is 0 or it is bigger than the length of the map
            {
                sValidationError = $" monster attempt to go{X} is out of the boudnds";
            }

            if (mapArray[Y][X] == '#')
            {
                sValidationError = "Monster Hitting a wall.";
                return false; // if the monster hitting a wall it will return false
            }

            if (mapArray[Y][X] == 'H')
            {
                sValidationError = "Monster Attemps to move into Hunter";
                return false;
            }

            // I will put clearing the position here, but it may be in moving

            mapArray[this.Y][this.X] = ' ';// it will set the previous position as empty
            this.X = X;
            this.Y = Y;
            mapArray[Y][X] = 'M'; //and the new position as H
            return true;// then it returns true
        }


        public enum Direction
        {
            None, Up, Down, Left, Right, //0,1,2,3
        }
    }
}

