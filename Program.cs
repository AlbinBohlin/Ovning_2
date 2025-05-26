namespace Ovning_2;
internal class Program
{   /*All these exceptions are completly superfluous I'm just messing around with C# Syntax*/
    class InvalidResponseException : Exception { public override string ToString() { return "Invalid input"; } }//Now we are really messing around.
    //Python style print function giving a human readable representation of 
    //an object if possible, otherwise its the same drab Object.tostring as Console.Writeline would produce.
    public static void Print(Object s) { Printmaster.Printr.Print(s); }




    /*
     * A Prompt is an encouragement to provide input.
     * @instruction - an elucidation of the desired user input.
     * @expected_type - a typesafe declaration of acceptable user input.
     */
    public static string[] Prompt(string instruction, Type expected_type)
    {
        Console.WriteLine(instruction);
        string? response;
        do response = Console.ReadLine(); while (string.IsNullOrWhiteSpace(response)); //Input validation not empty (string null only if terminal process killed with ctrl + c)

        if (expected_type == typeof(System.Byte)) // If we asked user for an number. I.e. menu choice or age. *expect.equals(typeof( === false?!!?*
        {
            try// chatGTP thought i should use TryParse() but then i would'nt be able to catch the type of exception.
            {
                var expected = System.Byte.Parse(response.Trim().Split(" ")[0]); // First word as an Byte if eligable
                return [$"{expected}",];//Conform to method return type. Only 1 element in array on success, 2 on error. 
            }
            catch (OverflowException) { return ["I dont believe you are 255+ years old my friend.", (new InvalidResponseException()).ToString()]; }
            catch (FormatException) { return ["Should've specified positive integers perhaps.", (new InvalidResponseException()).ToString()]; }
            catch (Exception e) { return [$"If this message prints i missed something...\n{e.Message}", (new InvalidResponseException()).ToString()]; }
        }
        return response.Split(" ");
    }

    public static void Main(string[] args)
    {
        //Main menu
        Print("Welcome to Excersice 2. Options below.");
        var menu = new string[] { "0: Quit", "1: Request 1 ticket" };
        do
        {
            foreach (var option in menu) Print(option);//

            var curious = ""; //without this curious variable i cannot access the string that procced default 
            switch (curious = Prompt($"Please enter a number between 0 and {menu.Length - 1}.", typeof(System.Byte))[0]) // Convoluted by design.. Byte becasue neither age nor menuchoice should be negative or more than 8 bits
            {   //Main loop is void. return breaks the while.
                case "0": Print("Goodbye"); return;
                //Age Prompt
                case "1":
                    string[] response;
                    do response = Prompt("Please enter your age in whole years", typeof(System.Byte)); while (response.Length > 1);//Exceptions won't print here...
                    int age = int.Parse(response[0]);//this is validated int.. at least
                    if (age < 5 || age > 100)
                    {
                        Print("No charge for the very old and young.");
                    }
                    if (age < 20 || age > 64)
                    {
                        if (age < 20) { Print("Youth fee: 80kr."); } 
                        else { Print("Senior fee: 90kr."); }
                    }                    
                    else  { Print("Regular fee: 120kr."); }
                   

                    break;
                case "2":


                default: Console.WriteLine(curious + "\nInvalid input.\n"); break;
            }
        }
        while (true);
    }
}

