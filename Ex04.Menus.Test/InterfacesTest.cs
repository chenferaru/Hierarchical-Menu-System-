using System;
using Ex04.Menus.Interfaces;

namespace Ex04.Menus.Test
{
    internal class InterfacesTest
    {
        private const bool v_IsMenu = true;

        public MainMenu CreateMainMenu()
        {
            MainMenu mainMenu = new MainMenu("Interfaces Main Menu");
            MenuItem mainMenuItem = mainMenu.MenuItem;
            MenuItem versionsAndCapitalsMenu = new MenuItem(mainMenuItem, "Versions and Capitals", v_IsMenu);
            MenuItem showDateTimeMenu = new MenuItem(mainMenuItem, "Show Date/Time", v_IsMenu);
            MenuItem showVersion = new MenuItem(versionsAndCapitalsMenu, "Show Version", new ShowVersion());
            MenuItem CountCapitals = new MenuItem(versionsAndCapitalsMenu, "Count Capitals", new CountCapitals());
            MenuItem showTime = new MenuItem(showDateTimeMenu, "Show Time", new ShowTime());
            MenuItem showDate = new MenuItem(showDateTimeMenu, "Show Date", new ShowDate());

            mainMenu.AddMenuItem(mainMenuItem, versionsAndCapitalsMenu);
            mainMenu.AddMenuItem(mainMenuItem, showDateTimeMenu);
            mainMenu.AddMenuItem(versionsAndCapitalsMenu, showVersion);
            mainMenu.AddMenuItem(versionsAndCapitalsMenu, CountCapitals);
            mainMenu.AddMenuItem(showDateTimeMenu, showTime);
            mainMenu.AddMenuItem(showDateTimeMenu, showDate);

            return mainMenu;
        }        
    }

    public class ShowVersion : IMenuAction
    {
        void IMenuAction.Execute()
        {
            Console.WriteLine("App Version: 24.2.4.9504");
        }
    }

    public class CountCapitals : IMenuAction
    {
        void IMenuAction.Execute()
        {
            string userInput;
            int capitalLettersCounter = 0;

            Console.WriteLine("Please write a sentence:");
            userInput = Console.ReadLine();
            foreach(char character in userInput)
            {
                if(Char.IsUpper(character))
                {
                    capitalLettersCounter++;
                }
            }

            Console.WriteLine("Number of capital letter(s) in your sentence: {0}", capitalLettersCounter);
        }
    }

    public class ShowTime : IMenuAction
    {
        void IMenuAction.Execute()
        {
            Console.WriteLine("Current time: {0}", DateTime.Now.ToString("HH:mm:ss"));
        }
    }

    public class ShowDate : IMenuAction
    {
        void IMenuAction.Execute()
        {
            Console.WriteLine("Current Date: {0}", DateTime.Now.ToShortDateString());
        }
    }
}
