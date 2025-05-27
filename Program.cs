namespace Ovning_2;
internal class Program
{   /*All these exceptions are completly superfluous I'm just messing around with C# Syntax*/
    class InvalidResponseException : Exception { public override string ToString() { return "Invalid input"; } }//Now we are really messing around... deprecated
    //Python style print function giving a human readable representation of 
    //an object if possible, otherwise its the same drab Object.tostring as Console.Writeline would produce.
    public static void Print(Object s) { Printmaster.Printr.Print(s); }

    public enum Expected { Byte, String, Repeat }


    /*
     * A Prompt is an encouragement to provide input.
     * @instruction - an elucidation of the desired user input.
     * @expected_type - a typesafe declaration of acceptable user input.
     * @Return A validated respons for all instructions
     */
    public static string[] Prompt(string instruction, Expected expected_type)
    {
        Console.WriteLine(instruction);
        string? response;
        do response = Console.ReadLine(); while (string.IsNullOrWhiteSpace(response)); //Input validation not empty (string null only if terminal process killed with ctrl + c)

        if (expected_type == Expected.Byte) // If we asked user for an number. I.e. menu choice or age. *expect.equals(typeof( === false?!!?*
        {
            try
            {
                var expected = System.Byte.Parse(response.Trim().Split(" ")[0]); // First word as an Byte if eligable
                return [$"{expected}",];//Conform to method return type.
            }
            catch (OverflowException) { Print("I dont believe you are 255+ years old my friend."); Prompt(instruction, expected_type); }//retry
            catch (FormatException) { Print("Only whole positive numbers please"); Prompt(instruction, expected_type); }
            catch (Exception e) { Print($"If this message prints i missed something...\n{e.Message}"); Prompt(instruction, expected_type); }
        }
        else if (expected_type == Expected.Repeat) return ["1"]; // Repeat menu 1
        
            Print("Not implemented yet"); Prompt(instruction, expected_type);
        return [response];
    }

    public static void Main(string[] args)
    {
        var menu = new string[] { "0: Quit", "1: Request 1 ticket", "2: Request several tickets" };
        int repeat = -1; 
        int totalFee = 0;
        string[] response = ["",];
        //Main menu
        Print("Welcome to Excersice 2. Options below.");
        do
        {
            if (repeat <= 0)
            {
                totalFee = 0; 
                foreach (var option in menu) Print(option);
                // Convoluted by design.. Byte becasue neither age nor menuchoice should be negative or more than 8 bits
                response = Prompt($"Please enter a number between 0 and {menu.Length - 1}.", Expected.Byte);
            }

            switch (response[0])
            {       //Main loop is void. return breaks the while.
                case "0": Print("Goodbye"); return;
                    // Single customer pricing model
                case "1":
                    int age = int.Parse(Prompt("Please enter your age in whole years", Expected.Byte)[0]);
                    if (age < 5 || age > 100) { 
                                        Print("No fee: 0kr."); }
                    else if (age < 20 || age > 64) {
                        if (age < 20) { Print("Youth fee: 80kr."); totalFee += 80; }
                        else {          Print("Senior fee: 90kr."); totalFee += 90; }
                    }else {             Print("Regular fee: 120kr."); totalFee += 120; }
                    if (repeat > 1) response[0] = "2";
                    else 
                        Print($"Total fee is {totalFee}kr.\nNEXT!\n" );
                        repeat = 0;//somethings not quite right. 
                        break;
                    //Several cusomers will repeat case 1  
                case "2":
                    if (repeat <= 0) repeat = int.Parse(Prompt($"Please enter number of people.", Expected.Byte)[0]); //set repititions 
                    else repeat--;//return condition
                    response[0] = "1";
                    break;


                default: Console.WriteLine("This should never happen" + "\nInvalid input.\n"); break;
            }
        }
        while (true);
    }
}

