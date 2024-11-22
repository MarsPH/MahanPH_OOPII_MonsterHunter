using MonsterHunterDLL;
using System.Data;
using System.Xml.Linq;

public class Program
{
    static void Main(string[] args)
    {
        //initalization
        Hunter hunter = new Hunter(0,0);
        
        Map map = new Map(); //create a map
        if (map.ValidationError != "") // if map is empty somehow
        {
            Console.WriteLine("Searching for .map files in directory: " + Directory.GetCurrentDirectory());
            Console.WriteLine($"Error when loading map : {map.ValidationError}"); 
            return;
        }
            
        Monsters monsters = new Monsters();
        //variables
        string[] mapFiles = map.mapNames; //store the mapnames in mapfiles
        int mapNumber = 1; // index for the listing
        int selectedMapNumber;

        // loop until the 
        //name is valid.
        while (true)
        {
            Console.WriteLine("Please Type your name");
            hunter.Name = Console.ReadLine();

            if (hunter.ValidationError != "" )
            {
                Console.WriteLine($"Name is invalid {hunter.ValidationError}");
                continue;
            }
            else
            {
                break;
            }
            Console.WriteLine($"Hello {hunter.Name}");
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
            map.loadMapFromFile(selectedMap, hunter, monsters.monsters); //


            foreach (char[] row in map.mapArray)
            {
                Console.WriteLine(new string(row));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error{ex.Message}");
        }
        

    }
}