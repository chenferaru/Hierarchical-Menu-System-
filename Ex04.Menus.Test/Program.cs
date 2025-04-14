using System;

namespace Ex04.Menus.Test
{
    internal class Program
    {
        public static void Main()
        {
            Run();
        }
        
        public static void Run()
        {
            InterfacesTest interfacesTest = new InterfacesTest();
            EventsTest eventsTest = new EventsTest();

            try
            {
                Interfaces.MainMenu mainMenuInterfaces = interfacesTest.CreateMainMenu();
                mainMenuInterfaces.Show();

                Events.MainMenu mainMenuEvents = eventsTest.CreateMainMenu();
                mainMenuEvents.Show();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("{0}.", ex.Message, ex.ParamName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
