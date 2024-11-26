using MonsterHunterDLL;
using System.Data;
using System.Diagnostics.Metrics;
using System.Threading;
using System.Xml.Linq;
using System;
////Revision history:
/// Mahan Poor Hamidian     2024/11/20      Asks Name / Draws Map  \
/// Mahan Poor Hamidian     2024/11/22      Map chars in colors
/// Mahan Poor Hamidian     2024/11/22      Info Board <summary>
/// Mahan Poor Hamidian     2024/11/23      Hunter can move in a boundary with the correct position
/// Mahan Poor Hamidian     2024/11/23      Hunter can move in a boundaries of # with the correct position
/// Mahan Poor Hamidian     2024/11/23      Monster can move withing the boundaries
/// Mahan Poor Hamidian     2024/11/23      Monster Bug Up/down fix / 
/// Mahan Poor Hamidian     2024/11/24      Changed Move Method
/// Mahan Poor Hamidian     2024/11/24      Board Info
/// Mahan Poor Hamidian     2024/11/24      Combat of Monster
/// Mahan Poor Hamidian     2024/11/24      Combot of Hunter
/// Mahan Poor Hamidian     2024/11/24      Red HP
/// Mahan Poor Hamidian     2024/11/24      InforBoard Bug Fixed
/// Mahan Poor Hamidian     2024/11/25      Monster can be dead!
/// Mahan Poor Hamidian     2024/11/25      Monster info will be added!
/// Mahan Poor Hamidian     2024/11/26      Hunter Death
/// Mahan Poor Hamidian     2024/11/26      play Again
/// 
/// 
/// </summary>
public class Program
{
    public static bool canMove = true;
    public static bool MapChosen;
    public static Map map = new Map(); //create a map
    public static Hunter hunter = new Hunter()
    {
        HP = 30,
        Score = 0,
    };
    public static int infoBoardStartLine;
    public static int selectedMapNumber;
    public static bool gameOver = false;
    public static int threadSleepTime = 3000;
    public static List<string> mapFiles = map.mapNames; //store the mapnames in mapfiles
    public static ConsoleKeyInfo keyPressed = new ConsoleKeyInfo();
    public static int firstLevelIndex;
    public static int secondLevelIndex;
    public static string selectedMap = "";

    static void Main(string[] args)
    {
        Console.SetWindowSize(100, 30); // Width: 100, Height: 40

        // Set the console buffer size
        Console.SetBufferSize(100, 30); // Width: 100, Height: 1000

        //variables
        //constants
        const int START_ROW = 3;
        //initalization

        if (map.ValidationError != "") // if map is empty somehow
        {
            Console.WriteLine("Searching for .map files in directory: " + Directory.GetCurrentDirectory());
            Console.WriteLine($"Error when loading map : {map.ValidationError}");
            return;
        }





        //Monsters monsters = new Monsters();
        //variables




        PlayGame(); // plays game
       

    }
    static void detectMonsters()
    {
        //at the top: static List<monster> allMonsters = new List<monster>();
        //in the project, it goes into gameENgine
        //          List<monster> allMonsters = new List<monster>
        //create a new monster
        foreach (Monster m in Monsters.monsters) // 
        {
            Console.SetCursorPosition(m.X, m.Y);
            Console.Write('M');
        }
        /*
        Monster newMonster = new Monster(7, 1);
        newMonster.monsterDirection = Monster.Direction.Left; //random in the project
                                                              //add it to the list of monsters
        Monsters.AddMonster(newMonster);

        //draw monster
        Console.SetCursorPosition(newMonster.X, newMonster.Y);
        Console.Write('M');
        //create a new monster
        newMonster = new Monster(5, 4);
        newMonster.monsterDirection = Monster.Direction.Right; //should be random at project
                                                               //add it to the list of monsters
        Monsters.AddMonster(newMonster);


        //draw player
        Console.SetCursorPosition(newMonster.X, newMonster.Y);
        Console.Write('M');
        */
        //Version 1, using a Thread
        //Start the thread
        Thread moveUpThread = new Thread(new ThreadStart(moveMonstersSlowlyInChildThread));//there is no this because it is static
        moveUpThread.IsBackground = true; // if close main thread, it will close the child thread
        moveUpThread.Start();
    }

    static void moveMonstersSlowlyInChildThread()
    {
        if (gameOver || !MapChosen)// if game over or map not chosens
        {
            return;
        }
        if (!gameOver || MapChosen)
        {
            //bool doneMoving = false;
            for (int index = 0; index < Monsters.monsters.Count; index++)
            {
                Monster thisMonster = Monsters.monsters[index];
                // if there are pixels left to move
                if (thisMonster.pixelsToMove > 0 && !thisMonster.CheckIsCharacterDead())
                {
                    if (thisMonster.monsterDirection == Monster.Direction.Left)
                    {
                        if (thisMonster.MoveCharacter(-1, 0, map.mapArray))
                        {
                            //Console.SetCursorPosition(thisMonster.X, thisMonster.Y);
                            //Console.ForegroundColor = ConsoleColor.Gray;

                            //Console.Write(' ');
                            //map.mapArray[thisMonster.Y][thisMonster.X] = ' ';// to remove the previous position in the map

                            //thisMonster.X--;

                            //Console.SetCursorPosition(thisMonster.X, thisMonster.Y);
                            //Console.ForegroundColor = ConsoleColor.Red;

                            //Console.Write('M');
                            //map.mapArray[thisMonster.Y][thisMonster.X] = 'M'; //to change the position in map

                            Thread.Sleep(thisMonster.FreezeTime);
                        }


                    }

                    if (thisMonster.monsterDirection == Monster.Direction.Right)
                    {
                        if (thisMonster.MoveCharacter(1, 0, map.mapArray))//(thisMonster.X + 1, thisMonster.Y, map.mapArray))
                        {
                            //Console.SetCursorPosition(thisMonster.X, thisMonster.Y);
                            //Console.ForegroundColor = ConsoleColor.Gray;

                            //Console.Write(' ');
                            //map.mapArray[thisMonster.Y][thisMonster.X] = ' ';

                            //thisMonster.X++;

                            //Console.SetCursorPosition(thisMonster.X, thisMonster.Y);
                            //Console.ForegroundColor = ConsoleColor.Red;

                            //Console.Write('M');
                            //map.mapArray[thisMonster.Y][thisMonster.X] = 'M'; //to change the position in map

                            Thread.Sleep(thisMonster.FreezeTime);
                        }


                    }
                    if (thisMonster.monsterDirection == Monster.Direction.Up)
                    {
                        if (thisMonster.MoveCharacter(0, -1, map.mapArray))//(thisMonster.X, thisMonster.Y - 1, map.mapArray))
                        {
                            //Console.SetCursorPosition(thisMonster.X, thisMonster.Y);
                            //Console.ForegroundColor = ConsoleColor.Gray;

                            //Console.Write(' ');
                            //map.mapArray[thisMonster.Y][thisMonster.X] = ' ';

                            //thisMonster.Y--;

                            //Console.SetCursorPosition(thisMonster.X, thisMonster.Y);
                            //Console.ForegroundColor = ConsoleColor.Red;

                            //Console.Write('M');
                            //map.mapArray[thisMonster.Y][thisMonster.X] = 'M'; //to change the position in map

                            Thread.Sleep(thisMonster.FreezeTime);
                        }


                    }
                    if (thisMonster.monsterDirection == Monster.Direction.Down)
                    {
                        if (thisMonster.MoveCharacter(0, 1, map.mapArray))//(thisMonster.X, thisMonster.Y + 1, map.mapArray))
                        {
                            //Console.SetCursorPosition(thisMonster.X, thisMonster.Y);
                            //Console.ForegroundColor = ConsoleColor.Gray;

                            //Console.Write(' ');
                            //map.mapArray[thisMonster.Y][thisMonster.X] = ' ';

                            //thisMonster.Y++;

                            //Console.SetCursorPosition(thisMonster.X, thisMonster.Y);
                            //Console.ForegroundColor = ConsoleColor.Red;

                            //Console.Write('M');
                            //map.mapArray[thisMonster.Y][thisMonster.X] = 'M'; //to change the position in map

                            Thread.Sleep(thisMonster.FreezeTime);
                        }


                    }

                    if (thisMonster.canAttack && !hunter.isAttacking && hunter.HP > 0 && !hunter.IsInvisible) //if hunter is not invisible
                    {
                        thisMonster.Attack(hunter); //shield to be added
                    }
                    if (!hunter.CheckIsCharacterDead() && !hunter.wonLevel)
                    {
                        UpdateInfoBoard();

                    }

                }
                else //if the monster dont have more movements or their heart is 0 they will disapper/dead
                {
                    if (!thisMonster.isRemoved) // Monster is already removed?
                    {
                        Console.SetCursorPosition(thisMonster.X, thisMonster.Y);//set cursor to the defeated monster
                        map.mapArray[thisMonster.Y][thisMonster.X] = ' '; // also remove the monster in the map
                        Console.Write(' ');// remove the dead monster on the map
                        hunter.Messages.Add($"Monster Destroyed +{thisMonster.worth} Score!");// set a new messge that mosnter is dead
                        hunter.Info = $"+{thisMonster.worth} Score!"; // I put this score added in the info
                        hunter.Score += thisMonster.worth;// add the hunter score to the worth of the monster (I can make it randomize but in the project is 100)
                        thisMonster.isRemoved = true; // this monster isremoved true so it wont constantly make this char in the map and console empty.
                    }

                }
            }
       
            //freeze monsters for two seconds
            //Thread.Sleep(thisMonster.FreezeTime); //use constant in the proj 

        }
        foreach (Monster thisMonster in Monsters.monsters)
        {
            //TODOL choose Randomly a new direction (check walls collisions, re-roll if yes)
            int randomDirection = RandomSingleton.Next(1, 5); // for now i omitted none moving
            switch (randomDirection)
            {
                case 0:
                    thisMonster.monsterDirection = Monster.Direction.None;
                    break;
                case 1:
                    thisMonster.monsterDirection = Monster.Direction.Up;//Up, Down, Left, Right,
                    break;
                case 2:
                    thisMonster.monsterDirection = Monster.Direction.Down;//Up, Down, Left, Right,
                    break;
                case 3:
                    thisMonster.monsterDirection = Monster.Direction.Left;//Up, Down, Left, Right,
                    break;
                case 4:
                    thisMonster.monsterDirection = Monster.Direction.Right;//Up, Down, Left, Right,
                    break;

            }

        }

        Thread moveUpThread = new Thread(new ThreadStart(moveMonstersSlowlyInChildThread));
        moveUpThread.IsBackground = true; // if close main thread, it will close the child thread
        moveUpThread.Start();

    }

    static void startPalerSleepThread()
    {
        Thread moveUpThread = new Thread(new ThreadStart(playerSleep));
        moveUpThread.IsBackground = true; // if close main thread, it will close the child thread
        moveUpThread.Start();
    }
    static void playerSleep()
    {
        canMove = false;
        Thread.Sleep(hunter.FreezeTime); //skeeping the thread
        canMove = true;
        /*
         * keyPressed = Console.ReadKey();
         * if (canMove)
         * ...
         * right after you moved the player = startPlayerSleepThread()
         * 
         */
    }
    static void UpdateInfoBoard()
    {
    
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.SetCursorPosition(0, infoBoardStartLine);
        Console.WriteLine($"Player: {hunter.Name}\t \bMap:{selectedMap}");
        if (hunter.HP <= 5)
        {
            ClearCurrentConsoleLine();
            Console.Write("HP: ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(hunter.HP);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine($"\t\tLevel:{hunter.level}");
        }
        else
        {
            ClearCurrentConsoleLine();
            Console.WriteLine($"HP: {hunter.HP}\t\tLevel:{hunter.level}");//currentLevel to be added?
        }
        Console.WriteLine($"Score: {hunter.Score} \tInfos:{hunter.Info}");//info to be added
        Console.WriteLine($"Strength: {hunter.Strength}\t \bArmor:{hunter.Armor}\t   MoveDelay:{hunter.FreezeTime}");
        ClearCurrentConsoleLine();
        Console.WriteLine(
    $"Sword: {(hunter.sword != null ? hunter.sword.SwordStrength.ToString() : "None")}  " +
    $"Shield: {(hunter.shield != null ? hunter.shield.ShieldStrength.ToString() : "None")}  " +
    $"Pickaxe: {(hunter.pickaxe != null ? "Has" : "None")}"
);
        Console.WriteLine();

        if (!gameOver)
        {
            if (hunter.Messages.Count != 0)
            {
                for (int i = hunter.Messages.Count - 3; i < hunter.Messages.Count; i++) // i can reverse this
                {
                    if (i >= 0)
                    {
                        Console.WriteLine($"- {hunter.Messages[i].PadRight(Console.WindowWidth - 2)}");
                    }
                }
            }
        }
       
        else
        {
            if (hunter.CheckIsCharacterDead() )
            {
                bool userGameOverInputIsValid = false;
                ConsoleKeyInfo ContinuekeyPressed;
                while (!userGameOverInputIsValid)
                {
                    Console.SetCursorPosition(0, infoBoardStartLine + 7);
                    ClearCurrentConsoleLine();
                    Console.SetCursorPosition(0, infoBoardStartLine + 8);
                    ClearCurrentConsoleLine();
                    Console.SetCursorPosition(0, infoBoardStartLine + 6);


                    Console.ForegroundColor = ConsoleColor.Gray;

                    Console.Write("Player Died");
                    Console.Write(". Do you want to play again?");
                    Console.WriteLine(" (y/n)".PadRight(Console.WindowWidth - 2));
                    ContinuekeyPressed = Console.ReadKey(true);
                    if (ContinuekeyPressed.Key != ConsoleKey.Y && ContinuekeyPressed.Key != ConsoleKey.N)
                    {
                        Console.SetCursorPosition(0, infoBoardStartLine + 7);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("Please either press y/n".PadRight(Console.WindowWidth - 2));
                        Thread.Sleep(1000);
                    }
                    else
                    {
                        switch (ContinuekeyPressed.Key)
                        {
                            case ConsoleKey.Y:
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Green;
                                gameOver = true;

                                Console.WriteLine("Restarting...");
                                Thread.Sleep(threadSleepTime);
                                Console.ForegroundColor = ConsoleColor.Gray;

                                PlayGame();

                                userGameOverInputIsValid = true;
                                break;
                            case ConsoleKey.N:
                                userGameOverInputIsValid = true;
                                Console.Clear();
                                Console.WriteLine("Thanks for playing, the scores are saved in the file");
                                Console.WriteLine("Press any key to exit");
                                Console.ReadLine();
                                Environment.Exit(0); // i may put it into menu again
                                break;
                        }
                    }

                }
            

            }
            else
            {
                hunter.Score += 500;
                hunter.Info = "Won This Level! +500";
                Thread.Sleep(threadSleepTime);
                Console.Clear();
                Console.SetCursorPosition(0, 0);
                Console.ForegroundColor = ConsoleColor.Green;
                
                Console.WriteLine($"GOING TO LEVEL {hunter.level}");
                //player won
                Thread.Sleep(threadSleepTime);
                PlayGame();

            }
        }


    }
    public static void ClearCurrentConsoleLine()
    {
        int currentLineCursor = Console.CursorTop;
        Console.SetCursorPosition(0, Console.CursorTop);
        Console.Write(new string(' ', Console.WindowWidth));
        Console.SetCursorPosition(0, currentLineCursor);
    }

    public static void RunMenu()
    {
        MapChosen = false;
        // loop until the 
        //name is valid.
        while (true)
        {
            if (hunter.Name == null)
            {
                Console.WriteLine("Please Type your name");
                hunter.Name = Console.ReadLine();

                if (hunter.ValidationError != "")
                {
                    Console.WriteLine($"Name is invalid {hunter.ValidationError}");
                    continue;
                }
                else
                {
                    Console.WriteLine($"Hello {hunter.Name}");
                    break;
                }
            }
            else
            {
                break;
            }

        }

        //Choosing the Map
        while (true)
        {
            int mapNumber; // index for the listing

            mapNumber = 1;
            Console.WriteLine("\nPlease Choose a map:");

            if (hunter.level == 2)
            {
                mapFiles.RemoveAt(firstLevelIndex);
            }
            if (hunter.level == 3)
            {
                
                mapFiles.RemoveAt(secondLevelIndex);
            }
            //list all the files on the screen:
            foreach (var eachFile in mapFiles)
            {
                Console.WriteLine(mapNumber + "" + eachFile);
                mapNumber++;

            }
            //loop until the user selects an exisitng map
            Console.WriteLine("Enter the number of the map you want to load:");
            string selectedMapString = Console.ReadLine();

            if (!int.TryParse(selectedMapString, out selectedMapNumber))
            {
                Console.WriteLine("Invalid Input, Please enter a valid number");
                continue;
            }
            if (selectedMapNumber < 1)
            {
                Console.WriteLine("The number should be 1 or higher");
                continue;
            }
            if (selectedMapNumber > mapFiles.Count)
            {
                Console.WriteLine($"The number must not be more than the avaialable files");
                continue;
            }

            break;
        }
    }

    public static void RunIntro()
    {

        Console.Clear();
        Console.Write("Please use ");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("Arrows");
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine(" to move the player");
        Thread.Sleep(threadSleepTime);
        MapChosen = true;
    }
    public static void RunGame()
    {
        try
        {
            
            if (firstLevelIndex == 0)
            {
                firstLevelIndex = selectedMapNumber - 1;
            }
            else
            {
                if (secondLevelIndex == 0)
                {
                    secondLevelIndex = selectedMapNumber - 1;
                }
            }
 
            //Map Loading
             selectedMap = mapFiles[selectedMapNumber - 1];
            Console.WriteLine($"\nLoading Current Map {selectedMap}...");

            Console.Clear();// to not rewrite the texts on it, i can also change the START_ROW
            map.loadMapFromFile(selectedMap, hunter, Monsters.monsters);


            //Display the info board
            //Console.SetCursorPosition(0, 0);
            //Console.WriteLine($"Player: {hunter.Name}\t \bMap:{selectedMap}");
            //Console.WriteLine($"hP: {hunter.HP}\t\tLevel:{currentLevel}");//currentLevel to be added?
            //Console.WriteLine($"Score: {hunter.Score} \tInfos:");//info to be added

            Console.SetCursorPosition(0, 0);
            foreach (char[] row in map.mapArray) // It will loop through the map as array and one by one check if it as M or H in that specfic Char then it will write based on the condition in color
            {
                foreach (char c in row)
                {
                    switch (c)
                    {
                        case 'M':
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write(c);
                            break;
                        case 'H':
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write(c);
                            break;
                        default:
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.Write(c);
                            break;
                    }
                }
                Console.WriteLine();
            }
            Console.ResetColor(); // after draw map the color will be resetted
            infoBoardStartLine = Console.CursorTop + 1;
            //action board to be added here..
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error{ex.Message}");
        }
        for (int i = 1; i < hunter.level; i++) { 
            foreach(Monster m in Monsters.monsters)
            {
                m.FreezeTime *= (int)(m.FreezeTime * 0.9);
            }
            hunter.FreezeTime *= (int)(hunter.FreezeTime * 0.95);
            hunter.HP += Math.Max(1, (Character.MAXIMUM_HP - hunter.HP) / 2);
        }
        hunter = new Hunter(hunter.X, hunter.Y, map.Width, map.Height)
        {
            Score = hunter.Score,
            Name = hunter.Name,
            HP = hunter.HP,
            Armor = hunter.Armor,
            Strength = hunter.Strength,
            level = hunter.level,
            shield = hunter.shield,
            sword = hunter.sword,
            pickaxe = hunter.pickaxe,
            potion = hunter.potion,
            wonLevel = false,
        };
        
        Console.SetCursorPosition(hunter.X, hunter.Y);

        detectMonsters();
    }
    public static void PlayerMovement()
    {
        if (gameOver == false)
        {
            gameOver = hunter.wonLevel;
        }
        if (hunter.wonLevel)
        {
            UpdateInfoBoard();
        }
        if (hunter.CheckIsCharacterDead())
        {
            map.mapArray[hunter.Y][hunter.X] = 'X';


            Console.SetCursorPosition(hunter.X, hunter.Y);
            Console.ForegroundColor = ConsoleColor.Red;

            Console.Write('X');
            gameOver = true;
            UpdateInfoBoard();
        }
        else
        {
            keyPressed = Console.ReadKey();

        }





        if (canMove && !hunter.CheckIsCharacterDead())//canMove and alive)
        {


            switch (keyPressed.Key)
            {
                case ConsoleKey.LeftArrow:
                    if (hunter.X > 0)
                    {
                        if (hunter.MoveCharacter(-1, 0, map.mapArray)) // minused one to set the future step
                        {
                            /*
                            //clear the actual player position
                            Console.SetCursorPosition(hunter.X, hunter.Y);
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.Write(' ');
                            map.mapArray[hunter.Y][hunter.X] = ' ';


                            //Move the player to the left in memory
                            hunter.X--;

                            //draw the player at new position
                            Console.SetCursorPosition(hunter.X, hunter.Y);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write('H');
                            map.mapArray[hunter.Y][hunter.X] = 'H';
                            */
                            startPalerSleepThread();
                        }
                    }
                    break;

                case ConsoleKey.UpArrow:
                    if (hunter.Y > 0)
                    {
                        if (hunter.MoveCharacter(0, -1, map.mapArray))
                        {
                            /*
                            //clear the actual player position
                            Console.SetCursorPosition(hunter.X, hunter.Y);
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.Write(' ');
                            map.mapArray[hunter.Y][hunter.X] = ' ';


                            //Move the player to the left in memory
                            hunter.Y--;

                            //draw the player at new position
                            Console.SetCursorPosition(hunter.X, hunter.Y);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write('H');
                            map.mapArray[hunter.Y][hunter.X] = 'H';
                            */
                            startPalerSleepThread();
                        }
                    }
                    break;
                case ConsoleKey.RightArrow:
                    if (hunter.X < map.mapArray[hunter.Y].Length - 1)
                    {
                        if (hunter.MoveCharacter(+1, 0, map.mapArray))
                        {
                            /*
                            //clear the actual player position
                            Console.SetCursorPosition(hunter.X, hunter.Y);
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.Write(' ');
                            map.mapArray[hunter.Y][hunter.X] = ' ';


                            //Move the player to the left in memory
                            hunter.X++;

                            //draw the player at new position
                            Console.SetCursorPosition(hunter.X, hunter.Y);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write('H');
                            map.mapArray[hunter.Y][hunter.X] = 'H';
                            */
                            startPalerSleepThread();

                        }
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (hunter.Y < map.mapArray.Length - 1)
                    {

                        if (hunter.MoveCharacter(0, +1, map.mapArray))
                        {
                            /*
                            //clear the actual player position
                            Console.SetCursorPosition(hunter.X, hunter.Y);
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.Write(' ');
                            map.mapArray[hunter.Y][hunter.X] = ' ';



                            //Move the player to the left in memory
                            hunter.Y++;

                            //draw the player at new position
                            Console.SetCursorPosition(hunter.X, hunter.Y);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write('H');
                            map.mapArray[hunter.Y][hunter.X] = 'H';
                            */
                            startPalerSleepThread();

                        }

                    }
                    break;
            }

        }
        if (hunter.isAttacking && !hunter.CheckIsCharacterDead())
        {
            hunter.Attack();
            startPalerSleepThread();

        }


    }
    public static void PlayGame()
    {
        map = new Map();
        Monsters.monsters.Clear();
        
        Console.Clear();
        Console.SetCursorPosition(0, 0);

        gameOver = false;
        RunMenu(); // it wil ask for name and map
        RunIntro(); // before running the game it will the tell the user to use Arraow keys
        RunGame(); // it will run the game


        while (!gameOver)//!gameOver)
        {
            PlayerMovement(); //
        }
    }
}