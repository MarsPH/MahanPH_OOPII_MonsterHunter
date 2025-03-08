using MonsterHunterDLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
//revision history
////// Mahan Poor Hamidian     2024/11/27       Created GameForm 
////// Mahan Poor Hamidian     2024/11/27       Loadfromfile / drawing works! 
////// Mahan Poor Hamidian     2024/12/03       MonsterMovement 
///   
namespace MonsterHunterForms
{
    public partial class GameForm : Form
    {
        public string PlayerName { get; private set; }
        public string SelectedMapName { get; private set; }
        public Hunter Hunter { get; private set; }

        public static Map map = new Map(); // a map
        //consts
        public const int SQUARE_SIZE = 50;
        public const int PIXELS_TO_MOVE = 50;
        static char[][] mapArray = new char[][] { };//map as arrays
        private static bool gameOver = false;

        public GameForm(string playerName, string selectedMapName)
        {
            InitializeComponent();
            PlayerName = playerName;
            SelectedMapName = selectedMapName;

           
            Hunter = new Hunter { Name = PlayerName };
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            map.loadMapFromFile(SelectedMapName, Hunter, Monsters.monsters);
            foreach (Monster monster in Monsters.monsters)
            {
                monster.pixelsToMove = PIXELS_TO_MOVE;
                monster.monsterDirection = Monster.Direction.None;//(Monster.Direction)RandomSingleton.Next(1, 5);
            }
            DrawMap(map.mapArray);
            StartMonsterThread();
        }

        private void DrawMap(char[][] map) // jagged array
        {
            //set the cursor to the top of the screen
            //Console.SetCursorPosition(0, 0);

            //Console.BackgroundColor = ConsoleColor.Yellow;

            //loop in the 1st dimensions of the array
            for (int Y = 0; Y < map.GetLength(0); Y++)
            {
                //loop in the 2nd dimension of the array
                for (int X = 0; X < map[Y].Length; X++)
                {
                    //If we're drawing the player
                    if (map[Y][X] == '#') // wall
                    {
                        PictureBox newWall = new PictureBox();
                        newWall.Name = "picWall-" + X + "-" + Y;
                        newWall.Image = Properties.Resources.wall;
                        newWall.Width = SQUARE_SIZE; //use constants
                        newWall.Height = SQUARE_SIZE; //use constants
                        newWall.SizeMode = PictureBoxSizeMode.StretchImage;
                        newWall.Left = X * SQUARE_SIZE; //use constant
                        newWall.Top = Y * SQUARE_SIZE; //use constant

                        this.Controls.Add(newWall);


                    }
                    if (map[Y][X] == 'H') // hunter
                    {
                        PictureBox newWall = new PictureBox();
                        newWall.Name = "Hunter-" + X + "-" + Y;
                        newWall.Image = Properties.Resources.Hunter;
                        newWall.Width = SQUARE_SIZE; //use constants
                        newWall.Height = SQUARE_SIZE; //use constants
                        newWall.SizeMode = PictureBoxSizeMode.StretchImage;
                        newWall.Left = X * SQUARE_SIZE; //use constant
                        newWall.Top = Y * SQUARE_SIZE; //use constant

                        this.Controls.Add(newWall);

                    }
                    if (map[Y][X] == 'h') //shield
                    {
                        PictureBox newWall = new PictureBox();
                        newWall.Name = "Hunter-" + X + "-" + Y;
                        newWall.Image = Properties.Resources.shield;
                        newWall.Width = SQUARE_SIZE; //use constants
                        newWall.Height = SQUARE_SIZE; //use constants
                        newWall.SizeMode = PictureBoxSizeMode.StretchImage;
                        newWall.Left = X * SQUARE_SIZE; //use constant
                        newWall.Top = Y * SQUARE_SIZE; //use constant

                        this.Controls.Add(newWall);

                    }
                    if (map[Y][X] == 'p') //potion
                    {
                        PictureBox newWall = new PictureBox();
                        newWall.Name = "Hunter-" + X + "-" + Y;
                        newWall.Image = Properties.Resources.potion;
                        newWall.Width = SQUARE_SIZE; //use constants
                        newWall.Height = SQUARE_SIZE; //use constants
                        newWall.SizeMode = PictureBoxSizeMode.StretchImage;
                        newWall.Left = X * SQUARE_SIZE; //use constant
                        newWall.Top = Y * SQUARE_SIZE; //use constant

                        this.Controls.Add(newWall);

                    }
                    if (map[Y][X] == 'w') //sword
                    {
                        PictureBox newWall = new PictureBox();
                        newWall.Name = "Hunter-" + X + "-" + Y;
                        newWall.Image = Properties.Resources.sword;
                        newWall.Width = SQUARE_SIZE; //use constants
                        newWall.Height = SQUARE_SIZE; //use constants
                        newWall.SizeMode = PictureBoxSizeMode.StretchImage;
                        newWall.Left = X * SQUARE_SIZE; //use constant
                        newWall.Top = Y * SQUARE_SIZE; //use constant

                        this.Controls.Add(newWall);

                    }
                    if (map[Y][X] == 'G') // Goal
                    {
                        PictureBox newWall = new PictureBox();
                        newWall.Name = "Hunter-" + X + "-" + Y;
                        newWall.Image = Properties.Resources.Goal;
                        newWall.Width = SQUARE_SIZE; //use constants
                        newWall.Height = SQUARE_SIZE; //use constants
                        newWall.SizeMode = PictureBoxSizeMode.StretchImage;
                        newWall.Left = X * SQUARE_SIZE; //use constant
                        newWall.Top = Y * SQUARE_SIZE; //use constant

                        this.Controls.Add(newWall);

                    }
                    if (map[Y][X] == 'x') // picaxke
                    {
                        PictureBox newWall = new PictureBox();
                        newWall.Name = "Hunter-" + X + "-" + Y;
                        newWall.Image = Properties.Resources.pickaxe;
                        newWall.Width = SQUARE_SIZE; //use constants
                        newWall.Height = SQUARE_SIZE; //use constants
                        newWall.SizeMode = PictureBoxSizeMode.StretchImage;
                        newWall.Left = X * SQUARE_SIZE; //use constant
                        newWall.Top = Y * SQUARE_SIZE; //use constant

                        this.Controls.Add(newWall);

                    }
                    if (map[Y][X] == 'M') // picaxke
                    {
                        PictureBox newMonsterPictureBox = new PictureBox();
                        newMonsterPictureBox.Name = "Monster-" + X + "-" + Y;
                        newMonsterPictureBox.Image = Properties.Resources.Monster;
                        newMonsterPictureBox.Width = SQUARE_SIZE; //use constants
                        newMonsterPictureBox.Height = SQUARE_SIZE; //use constants
                        newMonsterPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                        newMonsterPictureBox.Left = X * SQUARE_SIZE; //use constant
                        newMonsterPictureBox.Top = Y * SQUARE_SIZE; //use constant

                        this.Controls.Add(newMonsterPictureBox);

                        foreach( var m in Monsters.monsters) {
                        if (m.X == X  && m.Y == Y)
                            {
                                m.MonsterPictureBox = newMonsterPictureBox;
                            }
                        }
                    }
                    //Draw the char at this pos in the array
                    //Console.Write(map[Y][X]);

                    //Go back to gray
                    //Console.ForegroundColor = ConsoleColor.Gray;
                }
                Console.WriteLine();
            }
            //draw player
//            picPlayer.Left = Hunter.X * SQUARE_SIZE;
//            picPlayer.Top = Hunter.Y * SQUARE_SIZE;
            //draw monster
            //picMonster1.Left = monster1X * SQUARE_SIZE;
            // picMonster1.Top = monster1Y * SQUARE_SIZE;

        }
        //to communicate between the threads we need a delegate
        private delegate void moveMonsterBetweenThreads();
   
        void moveMonsterByOneFrame()
        {
            bool doneMoving = false ;
            bool firstMove = false;
            if (gameOver)// if game over or map not chosens
            {
                return;
            }
            if (!gameOver)
            {
                //bool doneMoving = false;
                for (int index = 0; index < Monsters.monsters.Count; index++)
                {

                    Monster thisMonster = Monsters.monsters[index];
                    Console.WriteLine($"Monster {thisMonster} Direction: {thisMonster.monsterDirection}, PixelsToMove: {thisMonster.pixelsToMove}");

                    // if there are pixels left to move
                    if (thisMonster.pixelsToMove > 0 && !thisMonster.CheckIsCharacterDead())
                    {

                        switch (thisMonster.monsterDirection)
                        {
                            case Monster.Direction.Left:
                                 thisMonster.MonsterPictureBox.Left -= 1;
                                break;

                            case Monster.Direction.Right:
                                thisMonster.MonsterPictureBox.Left += 1;
                                break;

                            case Monster.Direction.Up:
                                thisMonster.MonsterPictureBox.Top -= 1;
                                break;

                            case Monster.Direction.Down:
                                
                                thisMonster.MonsterPictureBox.Top += 1;
                                break;
                            case Monster.Direction.None:
                                {
                                    break;
                                }


                        }
                        thisMonster.pixelsToMove--;

                        // If the move failed, calculate a new valid direction
               

                        if (thisMonster.canAttack && !Hunter.isAttacking && Hunter.HP > 0 && !Hunter.IsInvisible) //if hunter is not invisible
                        {
                            thisMonster.Attack(Hunter); //shield to be added
                        }
                        if (!Hunter.CheckIsCharacterDead() && !Hunter.wonLevel)
                        {
                            //UpdateInfoBoard();

                        }

                    }

                    else //if the monster dont have more movements or their heart is 0 they will disapper/dead
                    {
                        /*
                        if (!thisMonster.isRemoved) // Monster is already removed?
                        {
                            //Console.SetCursorPosition(thisMonster.X, thisMonster.Y);//set cursor to the defeated monster
                            map.mapArray[thisMonster.Y][thisMonster.X] = ' '; // also remove the monster in the map
                            //Console.Write(' ');// remove the dead monster on the map
                            Hunter.Messages.Add($"Monster Destroyed +{thisMonster.worth} Score!");// set a new messge that mosnter is dead
                            Hunter.Info = $"+{thisMonster.worth} Score!"; // I put this score added in the info
                            Hunter.Score += thisMonster.worth;// add the hunter score to the worth of the monster (I can make it randomize but in the project is 100)
                            thisMonster.isRemoved = true; // this monster isremoved true so it wont constantly make this char in the map and console empty.
                        } 
                        */
                        //thisMonster.monsterDirection = GetValidDirection(thisMonster);
                        //thisMonster.pixelsToMove = PIXELS_TO_MOVE; // Reset movement range
                    }
                    if (thisMonster.pixelsToMove == 0) //the child-thread ended
                    {
                        //specify that the mosnter is not going in any direction
                        //thisMonster.monsterDirection = Monster.Direction.None;
                        doneMoving = true;
                    }
                }

                if (doneMoving)
                {
                   foreach(Monster thisMonster in Monsters.monsters)
                    {
                        thisMonster.monsterDirection = GetValidDirection(thisMonster);
                        thisMonster.pixelsToMove = PIXELS_TO_MOVE;
                    }

                    Thread moveUpThread = new Thread(new ThreadStart(this.moveMonstersSlowlyInChildThread));
                    moveUpThread.IsBackground = true; // if close main thread, it will close the child thread
                    moveUpThread.Start();
                }
                //freeze monsters for two seconds
                //Thread.Sleep(thisMonster.FreezeTime); //use constant in the proj 

            }

        }
     

        private Monster.Direction GetValidDirection(Monster monster)
        {
            Monster.Direction newDirection = Monster.Direction.None;
            int maxRetries = 10; // Prevent infinite loops
            bool directionValid = false;

            for (int retry = 0; retry < maxRetries; retry++)
            {
                newDirection = monster.monsterDirection;
                if (retry >= 2)
                {
                newDirection = (Monster.Direction)RandomSingleton.Next(1, 5);

                }
                    switch (newDirection)
                {
                    case Monster.Direction.Left:
                        directionValid = monster.MoveCharacter(-1, 0, map.mapArray);
                        //if (directionValid)
                            //monster.X += 1;
                        break;
                    case Monster.Direction.Right:
                        directionValid = monster.MoveCharacter(1, 0, map.mapArray);
                        //if (directionValid)
                           //monster.X -= 1;

                        break;
                    case Monster.Direction.Up:
                        directionValid = monster.MoveCharacter(0, -1, map.mapArray);
                        //if (directionValid)
                            //monster.Y += 1;

                        break;
                    case Monster.Direction.Down:
                        directionValid = monster.MoveCharacter(0, 1, map.mapArray);
                        //if (directionValid)
                           // monster.Y -= 1;

                        break;
                }

                if (directionValid) break; // Found a valid direction
            }

            return directionValid ? newDirection : Monster.Direction.None;
        }
            

        private void moveMonstersSlowlyInChildThread()
        {
            int squareSize = SQUARE_SIZE; //use constants
                                          //use constant
            int sleepTime = 1000 / SQUARE_SIZE; //ise a var instead of 2000

            for (int i = 1; i <= squareSize; i++)
            {
                Thread.Sleep(sleepTime);
                //picPlayer.Top++; //will crash, cannot access controls from child threads
                //call the delgate ("bridge between the threads) to mvoe the picBox
                 Invoke(new moveMonsterBetweenThreads(moveMonsterByOneFrame));
            }
        }

        private void StartMonsterThread()
        {
            Thread monstersThread = new Thread(new ThreadStart(this.moveMonstersSlowlyInChildThread));
            monstersThread.IsBackground = true;
            monstersThread.Start();
        }
    }
}
