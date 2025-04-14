using System;
using Ex04.Menus.Events;

namespace Ex04.Menus.Test
{
    internal class EventsTest
    {
        private const bool v_IsMenu = true;

        public MainMenu CreateMainMenu()
        {
            Action showTimeAction = () => Console.WriteLine("Current time: {0}", DateTime.Now.ToString("HH:mm:ss"));
            Action showDateAction = () => Console.WriteLine("Current Date: {0}", DateTime.Now.ToShortDateString());
            Action showVersionAction = () => Console.WriteLine("App Version: 24.2.4.9504");
            Action showCapitalsAction = () => CountCapitalLetters();
            MainMenu mainMenu = new MainMenu("Delegates Main Menu");
            MenuItem mainMenuItem = mainMenu.MainMenuItem;
            MenuItem versionsAndCapitalsMenu = new MenuItem(mainMenuItem, "Versions and Capitals", v_IsMenu);
            MenuItem showDateTimeMenu = new MenuItem(mainMenuItem, "Show Date/Time", v_IsMenu);
            MenuItem showTime = new MenuItem(showDateTimeMenu, "Show Time", showTimeAction);
            MenuItem showDate = new MenuItem(showDateTimeMenu, "Show Date", showDateAction);
            MenuItem showVersion = new MenuItem(versionsAndCapitalsMenu, "Show Version", showVersionAction);
            MenuItem showCapitals = new MenuItem(versionsAndCapitalsMenu, "Count Capitals", CountCapitalLetters);

            mainMenu.AddMenuItem(mainMenuItem, versionsAndCapitalsMenu);
            mainMenu.AddMenuItem(mainMenuItem, showDateTimeMenu);
            mainMenu.AddMenuItem(versionsAndCapitalsMenu, showVersion);
            mainMenu.AddMenuItem(versionsAndCapitalsMenu, showCapitals);
            mainMenu.AddMenuItem(showDateTimeMenu, showTime);
            mainMenu.AddMenuItem(showDateTimeMenu, showDate);

            return mainMenu;
        }

        public static void CountCapitalLetters()
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
}
