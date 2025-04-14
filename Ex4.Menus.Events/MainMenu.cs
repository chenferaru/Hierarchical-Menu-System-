using System;
using System.Linq;

namespace Ex04.Menus.Events
{
    public class MainMenu
    {
        private readonly MenuItem r_MainMenu;
        private MenuItem m_CurrentItem;
        private const string k_NotAMenuErrorMessage = "The parent item you inserted is not a menu! you can't create a sub menu for it.";
        private const string k_NotANumberErrorMessage = "Please enter a valid integer!";
        public const bool v_IsMenu = true;

        public MainMenu(string i_Title)
        {
            r_MainMenu = new MenuItem(null, i_Title, v_IsMenu);
            r_MainMenu.MenuItemSelectOccurred += new MenuItemSelectedEventHandler(menuItemSelectOccurredHandler);
            m_CurrentItem = r_MainMenu;
        }

        public MenuItem MainMenuItem
        {
            get
            {
                return r_MainMenu;
            }
        }

        public void AddMenuItem(MenuItem i_ParentMenu, MenuItem i_SubMenuItem)
        {
            if(i_ParentMenu.IsMenu)
            {
                i_ParentMenu.AddSubMenuItem(i_SubMenuItem);
                i_SubMenuItem.MenuItemSelectOccurred += new MenuItemSelectedEventHandler(menuItemSelectOccurredHandler);
            }
            else
            {
                throw new ArgumentException(k_NotAMenuErrorMessage, i_ParentMenu.Title);
            }
        }

        private void menuItemSelectOccurredHandler(object i_Sender, MenuItemSelectedEventArgs i_EventArguments)
        {
            m_CurrentItem = i_EventArguments.SelectedMenuItem;
            if(!m_CurrentItem.IsMenu)
            {
                executeAction();
                if(m_CurrentItem.ParentNode != null)
                {
                    m_CurrentItem = m_CurrentItem.ParentNode;
                }
            }
        }

        private void executeAction()
        {
            if(m_CurrentItem.Method != null)
            {
                m_CurrentItem.Method.Invoke();
                delayScreenClear();
            }
        }

        public void Show()
        {
            bool userPressedExit;

            while(true)
            {
                try
                {
                    Console.Clear();
                    m_CurrentItem.Show();
                    userPressedExit = handleUserInput();
                    if(userPressedExit)
                    {
                        break;
                    }
                }
                catch (Exception thrownException)
                {
                    Console.WriteLine(thrownException.Message);
                    delayScreenClear();
                }
            }
        }

        private bool handleUserInput()
        {
            bool userPressedExit = false;
            bool userChoiceParsedSuccessfully = int.TryParse(Console.ReadLine(), out int userChoice);
            int numOfElementsInMenu = m_CurrentItem.GetSubMenuItems().Count;

            if(userChoiceParsedSuccessfully && userChoice >= 0 && userChoice <= numOfElementsInMenu)
            {
                if(userChoice == 0)
                {
                    if(m_CurrentItem.ParentNode == null)
                    {
                        userPressedExit = true;
                    }
                    else
                    {
                        m_CurrentItem = m_CurrentItem.ParentNode;
                    }
                }
                else
                {
                    m_CurrentItem.GetSubMenuItems().ElementAt(userChoice - 1).MenuItemSelected();
                }

                return userPressedExit;
            }
            else
            {
                if(!userChoiceParsedSuccessfully)
                {
                    throw new FormatException(k_NotANumberErrorMessage);
                }
                else
                {
                    throw new ArgumentException(string.Format("Your choice must be between 0 and {0}!", numOfElementsInMenu));
                }
            }
        }

        private void delayScreenClear()
        {
            Console.WriteLine("Press enter to continue");
            Console.ReadLine();
        }
    }
}
