using MonsterHunterDLL;

public class Program
{
    static void Main(string[] args)
    {
        Hunter hunter = new Hunter(0,0);
        while (true)
        {
            Console.WriteLine("Type the number of the map that you want to choose");
            hunter.Name = Console.ReadLine();

            if (hunter.ValidationError != "" )
            {
                Console.WriteLine($"Name is invalid {hunter.ValidationError}");
            }
            else
            {
                return;
            }
            Console.WriteLine($"Hello {hunter.Name}");
        }
    }
}