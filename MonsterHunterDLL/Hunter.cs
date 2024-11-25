﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
//////revision history
///Mahan Poor Hamidian 2024/11/18 Created Hunter Object
///Mahan Poor Hamidian 2024/11/20 Created Move Hunter Method
///
namespace MonsterHunterDLL
{
    public class Hunter : Character
    {
        //variables
        private string _name;
        private int _score;
        private ConsoleKeyInfo keyPressed;
        public List<String> Messages = new List<String>();
        public bool isAttacking;
        public static List<Monster> FoundMonsters;
        public string Info;
        //constants
        private const int MAX_NAME_CHAR = 20;
        private const int MAX_SCORE = 100000;
        private const int FREEZE_TIME = 1000;
        //Items
        public Shield shield;
        //Constructor
        //The constructor should set the hunter freeze time to 1 second. It should also receive a 
        //mandatory position(X, Y) and it should pass it to the base object constructor
        public Hunter(int startX = 0, int startY = 0, int maximumX = 0, int maximumY = 0) : base(startX, startY)
        {
            FreezeTime = FREEZE_TIME;
            this.X = startX;
            this.Y = startY;
            this.maxX = maximumX;
            this.maxY = maximumY;
            

        }

        public string Name //  name property which cannot hold more than 20 characters
        {
            get { return _name; }
            set
            {
                sValidationError = "";
                value = value.Trim();
                if (value.Trim() == "")
                {
                    sValidationError = "Name cannot be empty";
                }
                else if (value.Length > MAX_NAME_CHAR)
                {
                    sValidationError = $"Name cannot be more then {MAX_NAME_CHAR} characters";
                }
                else
                {
                    _name = value.Trim();
                }

            }
        }

        public int Score //  A property to hold the score of the hunter (maximum 100 000, no negative value
        {
            get { return _score; }
            set
            {
                sValidationError = "";
                if (value < 0)
                {
                    sValidationError = "Value cannot be less than 0";
                }
                else if (value > MAX_SCORE)
                {
                    sValidationError = $"The score is more then the allowed limit of {MAX_SCORE}";
                }
                else
                {
                    _score = value;
                }
            }
        }

        // A method to move the hunter. The hunter can only move where there is no wall and no 
        //monster. When a hunter moves, change immediately the position of the hunter (X, Y). 
        public override bool MoveCharacter(int XVelocity, int YVelocity, char[][] mapArray)

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

             if (mapArray[Y][X] == '#'){
                 sValidationError = "Hitting a wall.";
                 return false; // if the hunter hitting a wall it will return false
             }

             if (mapArray[Y][X] == 'M')
             {
                 sValidationError = "Hunter Attemps to move into Monster";
                 return false;
             }
            */
            // I will put clearing the position here, but it may be in moving
            /*
            mapArray[this.Y][this.X] = ' ';// it will set the previous position as empty
            this.X = X; 
            this.Y = Y;
            mapArray[Y][X] = 'H'; //and the new position as H
            Console.SetCursorPosition(X, Y);//set cursor as the Hunter
            */
            if (mapArray[this.Y + YVelocity][this.X + XVelocity] == '#')
            {
                sValidationError = "Hitting a wall.";
                isAttacking = false;
                return false; // if the hunter hitting a wall it will return false
            }
            if (mapArray[this.Y + YVelocity][this.X + XVelocity] == 'M')
            {
                FoundMonsters = Monsters.FindMonstersByPosition(this.X + XVelocity, this.Y + YVelocity);
                sValidationError = "Hitting a Monster.";
                isAttacking = true;
                return false; // if the hunter hitting a wall it will return false
            }
         

            else
            {
                isAttacking = false;

                if (mapArray[this.Y + YVelocity][this.X + XVelocity] == 'h')
                {
                    shield = null;
                    shield = new Shield();
                    Messages.Add($"{this.Name} got Shield! Shield Power: {shield.ShieldStrength} ");
                }
                mapArray[this.Y][this.X] = ' ';
                Console.SetCursorPosition(this.X, this.Y);
                Console.ForegroundColor = ConsoleColor.Gray;

                Console.Write(' ');

                this.X += XVelocity;
                this.Y += YVelocity;

                mapArray[this.Y][this.X] = 'H';
                Console.SetCursorPosition(this.X, this.Y);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write('H');
                return true;// then it returns true

            }
            

        }
        public void Attack()
        {
            
            //this.Strength = RandomSingleton.Next(0, 7);
            Messages.Add($"{this.Name} Attacked with the power of {this.Strength}");

            for (int i = 0; i < FoundMonsters.Count; i++)
            {

                FoundMonsters[i].HP -= this.Strength;
                if (FoundMonsters[i].CheckIsCharacterDead())
                {
                    FoundMonsters[i] = null;
                    continue;
                }
                FoundMonsters[i].Attack(this);
                

            }
            FoundMonsters.Clear();
            this.isAttacking = false;
        }
    }

}
