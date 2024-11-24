using MonsterHunterDLL;
using System.Data;
using System.Diagnostics.Metrics;
using System.Threading;
using System.Xml.Linq;
////Revision history:
/// Mahan Poor Hamidian     2024/11/20      Asks Name / Draws Map  \
/// Mahan Poor Hamidian     2024/11/22      Map chars in colors
/// Mahan Poor Hamidian     2024/11/22      Info Board <summary>
/// Mahan Poor Hamidian     2024/11/23      Hunter can move in a boundary with the correct position
/// Mahan Poor Hamidian     2024/11/23      Hunter can move in a boundaries of # with the correct position
/// Mahan Poor Hamidian     2024/11/23      Monster can move withing the boundaries
/// </summary>
public class Program
{
    public static bool canMove = true;
    public static Map map = new Map(); //create a map

    static void Main(string[] args)
    {
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

        Hunter hunter = new Hunter()
        {
            HP = 30,
            Score = 0,
        };



        //Monsters monsters = new Monsters();
        //variables
        string[] mapFiles = map.mapNames; //store the mapnames in mapfiles
        int mapNumber = 1; // index for the listing
        int selectedMapNumber;
        ConsoleKeyInfo keyPressed;


        // loop until the 
        //name is valid.
        while (true)
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

        //Choosing the Map
        while (true)
        {
            mapNumber = 1;
            Console.WriteLine("\nPlease Choose a map:");

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
            if (selectedMapNumber > mapFiles.Length)
            {
                Console.WriteLine($"The number must not be more than the avaialable files");
                continue;
            }

            break;
        }

        //Map Loading
        string selectedMap = mapFiles[selectedMapNumber - 1];
        Console.WriteLine($"\nLoading Current Map {selectedMap}...");




        try
        {
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
            //action board to be added here..
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error{ex.Message}");
        }

        hunter = new Hunter(hunter.X, hunter.Y, map.Width, map.Height);
        Console.SetCursorPosition(hunter.X, hunter.Y);

        detectMonsters();

        while (true)//!gameOver)
        {

            keyPressed = Console.ReadKey();


            if (true)//canMove)
            {


                switch (keyPressed.Key)
                {
                    case ConsoleKey.LeftArrow:
                        if (hunter.X > 0)
                        {
                            if (hunter.MoveCharacter(hunter.X - 1, hunter.Y, map.mapArray)) // minused one to set the future step
                            {
                                //clear the actual player position
                                Console.SetCursorPosition(hunter.X, hunter.Y);
                                Console.Write(' ');

                                //Move the player to the left in memory
                                hunter.X--;

                                //draw the player at new position
                                Console.SetCursorPosition(hunter.X, hunter.Y);
                                Console.Write('H');
                                Console.ForegroundColor = ConsoleColor.Green;

                                startPalerSleepThread();
                            }
                        }
                        break;

                    case ConsoleKey.UpArrow:
                        if (hunter.Y > 0)
                        {
                            if (hunter.MoveCharacter(hunter.X, hunter.Y - 1, map.mapArray))
                            {
                                //clear the actual player position
                                Console.SetCursorPosition(hunter.X, hunter.Y);
                                Console.Write(' ');

                                //Move the player to the left in memory
                                hunter.Y--;

                                //draw the player at new position
                                Console.SetCursorPosition(hunter.X, hunter.Y);
                                Console.Write('H');
                                Console.ForegroundColor = ConsoleColor.Green;

                                startPalerSleepThread();
                            }
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        if (hunter.X < map.mapArray[hunter.Y].Length - 1)
                        {
                            if (hunter.MoveCharacter(hunter.X + 1, hunter.Y, map.mapArray))
                            {
                                //clear the actual player position
                                Console.SetCursorPosition(hunter.X, hunter.Y);
                                Console.Write(' ');

                                //Move the player to the left in memory
                                hunter.X++;

                                //draw the player at new position
                                Console.SetCursorPosition(hunter.X, hunter.Y);
                                Console.Write('H');
                                startPalerSleepThread();

                            }
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (hunter.Y < map.mapArray.Length - 1)
                        {

                            if (hunter.MoveCharacter(hunter.X, hunter.Y + 1, map.mapArray))
                            {

                                //clear the actual player position
                                Console.SetCursorPosition(hunter.X, hunter.Y);
                                Console.Write(' ');

                                //Move the player to the left in memory
                                hunter.Y++;

                                //draw the player at new position
                                Console.SetCursorPosition(hunter.X, hunter.Y);
                                Console.Write('H');
                                Console.ForegroundColor = ConsoleColor.Green;

                                startPalerSleepThread();

                            }

                        }
                        break;
                }

            }



        }

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
            m.monsterDirection = Monster.Direction.Left;
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
        //bool doneMoving = false;
        for (int index = 0; index < Monsters.monsters.Count; index++)
        {
            Monster thisMonster = Monsters.monsters[index];
            // if there are pixels left to move
            if (thisMonster.pixelsToMove > 0)
            {
                if (thisMonster.monsterDirection == Monster.Direction.Left)
                {
                    if (thisMonster.MoveCharacter(thisMonster.X - 1, thisMonster.Y, map.mapArray))
                    {
                        Console.SetCursorPosition(thisMonster.X, thisMonster.Y);
                        Console.Write(' ');

                        thisMonster.X--;

                        Console.SetCursorPosition(thisMonster.X, thisMonster.Y);
                        Console.Write('M');
                    }
                 
                }

                if (thisMonster.monsterDirection == Monster.Direction.Right)
                {
                    if (thisMonster.MoveCharacter(thisMonster.X + 1, thisMonster.Y, map.mapArray))
                    {
                        Console.SetCursorPosition(thisMonster.X, thisMonster.Y);
                        Console.Write(' ');

                        thisMonster.X++;

                        Console.SetCursorPosition(thisMonster.X, thisMonster.Y);
                        Console.Write('M');
                    }
                 
                }
                if (thisMonster.monsterDirection == Monster.Direction.Up)
                {
                    if (thisMonster.MoveCharacter(thisMonster.X, thisMonster.Y - 1, map.mapArray))
                    {
                        Console.SetCursorPosition(thisMonster.X, thisMonster.Y);
                        Console.Write(' ');

                        thisMonster.Y--;

                        Console.SetCursorPosition(thisMonster.X, thisMonster.Y);
                        Console.Write('M');
                    }
                 
                }
                if (thisMonster.monsterDirection == Monster.Direction.Down)
                {
                    if (thisMonster.MoveCharacter(thisMonster.X, thisMonster.Y + 1, map.mapArray))
                    {
                        Console.SetCursorPosition(thisMonster.X, thisMonster.Y);
                        Console.Write(' ');

                        thisMonster.Y++;

                        Console.SetCursorPosition(thisMonster.X, thisMonster.Y);
                        Console.Write('M');
                    }
               
                }

            }
            //freeze monsters for two seconds
            Thread.Sleep(100); //use constant in the proj 

        }
        foreach (Monster thisMonster in Monsters.monsters)
        {
            //TODOL choose Randomly a new direction (check walls collisions, re-roll if yes)
            thisMonster.monsterDirection = Monster.Direction.Left;
            int randomDirection = RandomSingleton.Next(0, 5);
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
        Thread.Sleep(3000); //skeeping the thread
        canMove = true;
        /*
         * keyPressed = Console.ReadKey();
         * if (canMove)
         * ...
         * right after you moved the player = startPlayerSleepThread()
         * 
         */
    }
}