using System.Text.RegularExpressions;

namespace Ovning_2;
internal class Program
{
    public enum Types { Choiche, Age, Tickets, _Byte, _String, Sentance } //For input validation and switch fidelity


    /* A Prompt is an encouragement to provide input.
     @instruction - an elucidation of the desired user input.
     @expected_type - a typesafe declaration of acceptable user input.
     @Return Respons; a validated response for all instructions*/
    public static Response Prompt(string instruction, Types expected_type)
    {
        Console.WriteLine(instruction);
        string? response;
        do response = Console.ReadLine(); while (string.IsNullOrWhiteSpace(response)); //Input validation not empty (string null only if terminal process killed with ctrl + c)

        if (expected_type != Types._String && expected_type != Types.Sentance) // If we asked user for an number. I.e. menu choice or age. 
        {
            try// Trycatch is more clean but i want to know what exception was thrown
            {
                return new Response(System.Byte.Parse(response.Trim().Split(" ")[0]), expected_type); // First word as an Byte if eligable
            }
            catch (OverflowException) { Console.WriteLine("Thats a big number, too big."); return Prompt(instruction, expected_type); }//retry
            catch (FormatException) { Console.WriteLine("Only whole positive numbers please"); return Prompt(instruction, expected_type); }
            catch (Exception e) { Console.WriteLine($"If this message prints i missed something...\n{e.Message}"); return Prompt(instruction, expected_type); }
        }
        else if (expected_type == Types._String)// if expected is _String return the string

            return new Response(response, expected_type);

        else
        {
            try
            {
                string thirdWord = Regex.Replace(response.Trim(), @"\s+", " ").Split(" ")[2];//Google to the rescue
                return new Response(thirdWord, expected_type);
            }
            catch (IndexOutOfRangeException) {Console.WriteLine("Need atleast 3 words please."); return Prompt(instruction, expected_type);}
            catch (Exception ex) { Console.WriteLine("What could this be? " + ex.Message); return Prompt(instruction, expected_type); }           
        }
            
            
        
    }

    /* A Response is what a Prompt returns
    @ Data - data of arbitrary type
    @ DataType - A enum describing the type of Data.*/

    public struct Response(Object data, Types dataType)
    {
        public Object Data { get; } = data;
        public Types DataType { get; } = dataType;
    }
    public static void Main(string[] args)
    {
        int[] total = { 0, 0 };// Number of tickets and total cost
        //Main menu
        var menu = new string[] { "0: Quit", "1: Purchase tickets", "2: Get repetitive", "3: Third word selector" };
        Console.WriteLine("Welcome to Excersice 2. Options below.");

        do
        {   

            foreach (var option in menu) Console.WriteLine(option);
            Response response = Prompt("Choose option and press enter.", Types.Choiche);
            switch ((Byte)response.Data)
            {
                //Main loop is void. return breaks the while.
                case 0: Console.WriteLine("Goodbye"); return;
                // Purchase tickets
                case 1:
                    response = Prompt("Please enter the number of tickets", Types.Tickets);
                    for (Byte i = (Byte)response.Data; i > 0; i--)
                    {
                        response = Prompt("Please enter your age in whole years", Types.Age);
                        byte age = (Byte)response.Data;
                        if (age < 5 || age > 100)
                        {
                            Console.WriteLine("No fee: 0kr.");
                            total[0]++;
                        }
                        else if (age < 20 || age > 64)
                        {
                            if (age < 20) { Console.WriteLine("Youth fee: 80kr."); total[0]++; total[1] += 80; }
                            else { Console.WriteLine("Senior fee: 90kr."); total[0]++; total[1] += 90; }
                        }
                        else { Console.WriteLine("Regular fee: 120kr."); total[0]++; total[1] += 120; }
                    }

                    Console.WriteLine($"Total tickets: {total[0]}, Total fee: {total[1]}kr. \nReturning to main menu..\n");
                    total = [0, 0];
                    break;

                case 2: //Upprepa tio gånger
                    response = Prompt("Write whatever and press enter", Types._String);
                    for (Byte i = 0; i < 10; i++)
                    {
                        Console.Write((String)response.Data);
                    }
                    Console.WriteLine("\nReturning to main menu..\n");
                    break;

                case 3:
                    response = Prompt("Enter a message with atleast 3 words and press enter.", Types.Sentance);
                    Console.WriteLine($"Third word is; {(String)response.Data}.");
                    Console.WriteLine("\nReturning to main menu..\n");
                    break;

                default: Console.WriteLine($"\nInput must be a number between 0 and  {menu.Length - 1}"); break;
            }
        }
        while (true);
    }
}

