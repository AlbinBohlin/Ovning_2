internal class Program
{



    public static void Main(string[] args)
    {


        //Main menu

        var menu = new string[] { "0: Quit", "1: Test" };
        bool running = true;


        print("Welcome to Excersice 2. Options below.");

        do
        {
            foreach (var item in menu)
            {
                print(item);
            }
            
            switch (Prompt($"Please enter a number between 0 and {menu.Length - 1}.", typeof(int))[0]) // Convoluted by design..
            {
                case "0": running = false; print("Goodbye"); break;

                case "1": print("testing"); break;

                default: Console.WriteLine("Invalid input.\n"); break;
            }

        }
        while (running);


    }


    public static string[] Prompt(string instruction, Type expected_type)
    {
        Console.WriteLine(instruction);
        string? input;
        do input = Console.ReadLine();

        while (string.IsNullOrWhiteSpace(input));//Input validation not empty (string null only if terminal process killed with ctrl + z

        if (expected_type == typeof(System.Int32)) // If we asked user for a menu choice or age?? *expect.equals is false*?!
        {
            try { return input.Split(" "); }
            catch (Exception) { return new string[] { "this indicator of invalid input will never be evaluated", }; } //the switch will switch on "this", which is default.
        }

        return input.Split(" ");
    }

    //Python style print function giving a human readable representation of 
    //an object if present, otherwise its the same drab Object.tostring as Console.Writeline would produce.
    public static void print(Object s)
    {
        Printmaster.Printr.Print(s);
    }




}

