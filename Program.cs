namespace Ovning_2;
internal class Program
{   /*All these exceptions are completly superfluous I'm just messing around with C# Syntax*/
    class InvalidResponseException : Exception { public override string ToString() { return "Invalid input"; } }//Now we are really messing around... deprecated
    //Python style print function giving a human readable representation of 
    //an object if possible, otherwise its the same drab Object.tostring as Console.Writeline would produce.
    public static void Print(Object s) { Printmaster.Printr.Print(s); }

    public enum Types { Choiche, Age, Tickets, Byte, String } //For input validation and switch fidelity

    public struct Response(Object data, Types dataType)
    {
        public Object Data { get; } = data;
        public Types DataType { get; } = dataType;
    }

    /*
     * A Prompt is an encouragement to provide input.
     * @instruction - an elucidation of the desired user input.
     * @expected_type - a typesafe declaration of acceptable user input.
     * @Return A validated response for all instructions
     */
    public static Response Prompt(string instruction, Types expected_type)
    {
        Console.WriteLine(instruction);
        string? response;
        do response = Console.ReadLine(); while (string.IsNullOrWhiteSpace(response)); //Input validation not empty (string null only if terminal process killed with ctrl + c)

        if (expected_type != Types.String) // If we asked user for an number. I.e. menu choice or age. 
        {
            try
            {
                return new Response(System.Byte.Parse(response.Trim().Split(" ")[0]), expected_type); // First word as an Byte if eligable
            }
            catch (OverflowException) { Print("Thats a big number, too big."); Prompt(instruction, expected_type); }//retry
            catch (FormatException) { Print("Only whole positive numbers please"); Prompt(instruction, expected_type); }
            catch (Exception e) { Print($"If this message prints i missed something...\n{e.Message}"); Prompt(instruction, expected_type); }
        }


        Print("Not implemented yet"); Prompt(instruction, expected_type);
        return new Response("dfs", Types.String);
    }

    public static void Main(string[] args)
    {
        int[] total = { 0, 0 };// Number of tickets and total cost
        //Main menu
        var menu = new string[] { "0: Quit", "1: Purchase tickets", "2: " };
        Print("Welcome to Excersice 2. Options below.");

        do
        {

        foreach (var option in menu) Print(option);
        Response response = Prompt("Choose option and press enter.", Types.Choiche);

            switch ((Byte)response.Data)
            {
                //Main loop is void. return breaks the while.
                case 0: Print("Goodbye"); return;
                // Purchase tickets
                case 1:
                    response = Prompt("Please enter the number of tickets", Types.Tickets);
                    for (Byte i = (Byte)response.Data; i > 0; i--)
                    {
                        response = Prompt("Please enter your age in whole years", Types.Age);
                        byte age = (Byte)response.Data;
                        if (age < 5 || age > 100)
                        {
                            Print("No fee: 0kr.");
                        }
                        else if (age < 20 || age > 64)
                        {
                            if (age < 20) { Print("Youth fee: 80kr."); total[0]++; total[1] += 80; }
                            else { Print("Senior fee: 90kr."); total[0]++; total[1] += 90; }
                        }
                        else { Print("Regular fee: 120kr."); total[0]++; total[1] += 120; }
                    }

                    Print($"Total tickets: {total[0]}, Total fee: {total[1]}kr. \nReturning to main menu..\n");
                    break;

                case 2:


                    break;

                case 3:



                default: Console.WriteLine("This should never happen" + "\nInvalid input.\n"); break;
            }
        }
        while (true);
    }
}

