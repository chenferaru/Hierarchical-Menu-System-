using System;
using System.Linq;

namespace Ex04.Menus.Interfaces
{
    public class MainMenu : ISelectedItem
    {
        private const string k_NotAMenuErrorMessage = "The parent item you inserted is not a menu! you can't create a sub menu for it.";
        private const string k_NotANumberErrorMessage = "Please enter a valid integer!";
        private const bool v_SelectMenuItem = true;
        private const bool v_IsMenu = true;
        private readonly MenuItem r_MainMenu;
        private MenuItem m_CurrentMenuLevel;

        public MainMenu(string i_Title)
        {
            r_MainMenu = new MenuItem(null, i_Title, v_IsMenu);
            r_MainMenu.AddListener(this);
            m_CurrentMenuLevel = r_MainMenu;
        }

        public MenuItem MenuItem
        {
            get
            {
                return r_MainMenu;
            }
        }

        public void AddMenuItem(MenuItem i_ParentMenuItem, MenuItem i_NewMenuItem)
        {
            if(i_ParentMenuItem.IsMenu)
            {
                i_ParentMenuItem.AddSubMenuItem(i_NewMenuItem);
                i_NewMenuItem.AddListener(this);
            }
            else
            {
                throw new ArgumentException(k_NotAMenuErrorMessage, i_ParentMenuItem.Title);
            }
        }

        void ISelectedItem.Selected(MenuItem i_Item)
        {
            if(i_Item.IsMenu)
            {
                m_CurrentMenuLevel = i_Item;
            }
            else
            {
                i_Item.Execute();
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
                    m_CurrentMenuLevel.Show();
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
            int numOfElementsInMenu = m_CurrentMenuLevel.GetSubMenuItems().Count;

            if(userChoiceParsedSuccessfully && userChoice >= 0 && userChoice <= numOfElementsInMenu)
            {
                if(userChoice == 0)
                {
                    if(m_CurrentMenuLevel.ParentNode == null)
                    {
                        userPressedExit = true;
                    }
                    else
                    {
                        m_CurrentMenuLevel.ParentNode.Selected = v_SelectMenuItem;
                    }
                }
                else
                {
                    m_CurrentMenuLevel.GetSubMenuItems().ElementAt(userChoice - 1).Selected = v_SelectMenuItem;
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
