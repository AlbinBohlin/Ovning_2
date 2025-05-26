using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using static Printmaster.Printr;

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
            switch (Prompt($"Please enter a number between 0 and {menu.Length- 1}.")[0])// First string in Prompt respons if more than one word.
            {

            case "0": running = false; print("Goodbye"); break;
            case "1": running = true; break;
            default: Console.WriteLine("Invalid input.\n");break;
        }
            
        }
        while (running);


    }

    public static string[] Prompt(string instruction)
    {
        Console.WriteLine(instruction);
        string? input;
        do input = Console.ReadLine(); 
        while (string.IsNullOrWhiteSpace(input));//Input validation
        return input.Split(" ");
    }

    //Python style print function giving a human readable representation of 
    //an object if present, otherwise its the same drab Object.tostring as Console.Writeline would produce.
    public static void print(Object s) {
        Printmaster.Printr.Print(s);
    }
    


    
}

