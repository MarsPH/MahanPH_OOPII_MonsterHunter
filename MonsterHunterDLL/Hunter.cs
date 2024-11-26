using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
//////revision history
///Note to myself I can remove most of the "this" keywords, idk why i kept writing them
///Mahan Poor Hamidian 2024/11/18 Created Hunter Object
///Mahan Poor Hamidian 2024/11/20 Created Move Hunter Method
///Mahan Poor Hamidian 2024/11/24 Sword / SHeild / Pickaxe Added
///
namespace MonsterHunterDLL
{
    public class Hunter : Character
    {
        //variables
        private string _name;
        private int _score;
        private ConsoleKeyInfo keyPressed;
        public List<string> Messages = new List<string>();
        public bool isAttacking;
        public static List<Monster> FoundMonsters;
        public string Info;
        public int maximumHP = MAXIMUM_HP;
        //potions
        public bool IsInvisible = false;
        public ConsoleColor skinColor = ConsoleColor.Green; //default color green
        //constants
        private const int MAX_NAME_CHAR = 20;
        private const int MAX_SCORE = 100000;
        private const int FREEZE_TIME = 1000;
        private const int ITEM_SCORE = 50;
        private const int POTION_EFFECT_TIME = 10000;
        //Items
        public Shield shield;
        public Sword sword;
        public Pickaxe pickaxe;
        public Potion potion;
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
            if (mapArray[this.Y + YVelocity][this.X + XVelocity] == '#')// wall collision
            {
                sValidationError = "Hitting a wall.";
                isAttacking = false;
                if (pickaxe != null) //if pickaxe exist in the hand of the player
                {
                    if (!pickaxe.UseAndIsBroken()) //if pickaxe is healthy
                    {
                        mapArray[this.Y + YVelocity][this.X + XVelocity] = ' ';//make the place that player wants to break in the map as ' '
                        Console.SetCursorPosition(this.X + XVelocity, this.Y + YVelocity);// set the cursor in the target postion
                        Console.ForegroundColor = ConsoleColor.Gray;// set the foreground as gray and not green
                        Console.Write(' ');// remove the wall and set is as empty
                        Messages.Add($"{this.Name} destroyed a Wall!");//anounce
                    }
                    else //if pickaxe got the chance of breaking then make it null and break
                    {
                        pickaxe = null;//make it nukk
                        Messages.Add($"Pickaxe is Broken :(");//anounce

                    }
                }
                return false; // if the hunter hitting a wall it will return false
            }
            if (mapArray[this.Y + YVelocity][this.X + XVelocity] == 'M' && !IsInvisible) //fight Monsters if hunter not invisble
            {
                FoundMonsters = Monsters.FindMonstersByPosition(this.X + XVelocity, this.Y + YVelocity);
                sValidationError = "Hitting a Monster.";
                isAttacking = true;
                return false; // if the hunter hitting a wall it will return false
            }
         

            else
            {
                isAttacking = false;
                //Item-Findings O.O
                if (mapArray[this.Y + YVelocity][this.X + XVelocity] == 'h') //found a shield
                {
                    shield = null;//make the previous one vanish
                    shield = new Shield();// make a new one with new strnegth
                    Messages.Add($"{this.Name} got Shield! Shield Power: {shield.ShieldStrength} "); //announce it
                    Info = $"+{ITEM_SCORE}"; //announce it in the info in the board(not the actions)
                    Score += ITEM_SCORE;

                }
                if (mapArray[this.Y + YVelocity][this.X + XVelocity] == 'w') //found a sword
                {
                    sword = null;//make the previouis vanish
                    sword = new Sword();//make a new one with new strength --> I hate this word. always spell mistake
                    Messages.Add($"{this.Name} got Sword! Sword Power: {sword.SwordStrength} ");// announce it
                    Info = $"+{ITEM_SCORE}";//announce it in the info in the board(not the actions)
                    Score += ITEM_SCORE;

                }
                if (mapArray[this.Y + YVelocity][this.X + XVelocity] == 'x')//found a pickaxe
                {
                    pickaxe = null; //make the prevoius one vanish
                    pickaxe = new Pickaxe();
                    Messages.Add($"{this.Name} got Pickaxe!");// announce it
                    Info = $"+{ITEM_SCORE}";//announce it in the info in the board(not the actions)
                    Score += ITEM_SCORE;

                }
                if (mapArray[this.Y + YVelocity][this.X + XVelocity] == 'p')//found a potion
                {
                    potion = null; //make the prevoius one vanish
                    potion = new Potion();
                    Messages.Add($"{this.Name} Potion!");// announce it
                    Info = $"+{ITEM_SCORE}";//announce it in the info in the board(not the actions)
                    Score += ITEM_SCORE;

                    switch ((int)potion.huntersPotionType)
                    {
                        case 0://Strength
                            PotionStrength strengthPotion = new PotionStrength();
                            StartPotionSleep( strengthPotion );
                            break;
                        case 1: //Poison
                            PotionPoison poisonPotion = new PotionPoison();
                            StartPotionSleep(poisonPotion);

                            break;
                        case 2: //Invis
                            PotionInvisible potionInvisible = new PotionInvisible();
                            StartPotionSleep(potionInvisible);

                            break;
                        case 3: //Speed
                            PotionFast potionSpeed = new PotionFast();
                            potionSpeed.StartPotion(this);
                            StartPotionSleep(potionSpeed);

                            break;
                    }
                    
                }
                //Movement
                if (IsInvisible && mapArray[this.Y][this.X] == 'M')
                {

                }
                else
                {
                    mapArray[this.Y][this.X] = ' ';
                }

                Console.SetCursorPosition(this.X, this.Y);
                Console.ForegroundColor = ConsoleColor.Gray;

                Console.Write(' ');

                this.X += XVelocity;
                this.Y += YVelocity;

                mapArray[this.Y][this.X] = 'H';
                Console.SetCursorPosition(this.X, this.Y);
                Console.ForegroundColor = skinColor; //color of the skin when driniking potion!
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
                if (sword != null) {
                    if (!sword.AttackAndIsBroken())//if sword is healthy ==> DO a sowrd attack!
                    {
                        Messages.Add($"{this.Name} Attacked Sword the power of {this.Strength} + {sword.SwordStrength}");

                        FoundMonsters[i].HP -= this.Strength + sword.SwordStrength;

                    }
                    else
                    {
                        Messages.Add($"Sword Destroyed!");
                        sword = null; //make sword null


                    }
                }
                else
                {
                    FoundMonsters[i].HP -= this.Strength; //basic attack
                }
       
               
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
        void StartPotionSleep(IPotionStates potion)
        {
            Thread moveUpThread = new Thread(new ThreadStart(() => PotionSleep(potion)));
            moveUpThread.IsBackground = true; // if close main thread, it will close the child thread
            moveUpThread.Start();
        }
         void PotionSleep(IPotionStates potion)
        {
            potion.StartPotion(this);
            Thread.Sleep(POTION_EFFECT_TIME); //sleeping the thread
            potion.StopPotion(this);

            /*
             * keyPressed = Console.ReadKey();
             * if (canMove)
             * ...
             * right after you moved the player = startPlayerSleepThread()
             * 
             */
        }
    }

}
