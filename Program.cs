using System.Diagnostics;
using System.Linq.Expressions;

internal class Program
{

    //Python style print function giving a human readable representation of 
    //an object if possible, otherwise its the same drab Object.tostring as Console.Writeline would produce.
    public static void Print(Object s)
    {
        Printmaster.Printr.Print(s);
    }


    /*
     * A Prompt is an encouragement to provide input.
     * @instruction - an elucidation of the desired user input.
     * @expected_type - a typesafe declaration of acceptable user input.
     */
    public static string[] Prompt(string instruction, Type expected_type)
    {
        Console.WriteLine(instruction);
        string? input;
        //Input validation not empty (string null only if terminal process killed with ctrl + z
        do input = Console.ReadLine(); while (string.IsNullOrWhiteSpace(input));

        if (expected_type == typeof(System.Byte)) // If we asked user for an number. I.e. menu choice or age. *expect.equals(typeof( === false?!!?*
        {
            try
            {
                var expected = System.Byte.Parse(input.Split(" ")[0]); // First word as an int if eligable
                return [$"{expected}"];//Conform to method return type.
            }
            catch (OverflowException) { return ["I dont believe you are 255+ years old my friend."]; }
            catch (FormatException) { return ["Should've specified positive integers perhaps."]; }
        }

        return input.Split(" ");
    }


    public static void Main(string[] args)
    {


        //Main menu

        var menu = new string[] { "0: Quit", "1: Test" };
        //bool running = true; //Not neccesary, just return on Exit input.


        Print("Welcome to Excersice 2. Options below.");

        do
        {
            foreach (var item in menu)
            {
                Print(item);
            }
            var curious =  ""; //without this curious variable i cannot access the string that procced default 
            switch (curious = Prompt($"Please enter a number between 0 and {menu.Length - 1}.", typeof(System.Byte))[0]) // Convoluted by design.. Byte becasue neither age nor menuchoice should be negative or more than 8 bits
            {
                case "0": Print("Goodbye"); return;//Main loop is void. return breaks the while.

                case "1": Print("testing"); break;

                default: Console.WriteLine(curious + "\nInvalid input.\n"); break;
            }

        }
        while (true);


    }







}

