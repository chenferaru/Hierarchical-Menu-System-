using System;
using System.Collections.Generic;

namespace Ex04.Menus.Events
{
    public class MenuItemSelectedEventArgs : EventArgs
    {
        public MenuItem SelectedMenuItem
        {
            get;
        }

        public MenuItemSelectedEventArgs(MenuItem i_SelectedMenuItem)
        {
            SelectedMenuItem = i_SelectedMenuItem;
        }
    }

    public delegate void MenuItemSelectedEventHandler(object sender, MenuItemSelectedEventArgs eventArgs);

    public class MenuItem
    {
        private readonly string r_Title;
        private readonly MenuItem r_ParentNode;
        private readonly bool r_IsMenu;
        private readonly List<MenuItem> r_SubMenuItems;
        private readonly Action r_MethodDelegate;
        private const bool v_Menu = true;

        public event MenuItemSelectedEventHandler MenuItemSelectOccurred;

        public MenuItem(MenuItem i_ParentNode, string i_Title, bool i_IsMenu)
        {
            r_ParentNode = i_ParentNode;
            r_Title = i_Title;
            r_IsMenu = i_IsMenu;

            if(i_IsMenu)
            {
                r_SubMenuItems = new List<MenuItem>();
            }
        }

        public MenuItem(MenuItem i_ParentNode, string i_Title, Action i_MenuAction) : this(i_ParentNode, i_Title, !v_Menu)
        {
            r_MethodDelegate = i_MenuAction;
        }

        internal string Title
        {
            get
            {
                return r_Title;
            }
        }

        internal List<MenuItem> GetSubMenuItems()
        {
            return r_SubMenuItems;
        }

        internal Action Method
        {
            get
            {
                return r_MethodDelegate;
            }
        }

        internal bool IsMenu
        {
            get
            {
                return r_IsMenu;
            }
        }

        internal MenuItem ParentNode
        {
            get
            {
                return r_ParentNode;
            }
        }

        internal void Show()
        {
            if(IsMenu)
            {
                int itemIndexInList = 1;
                string backOrExitPrompt = r_ParentNode == null ? "Exit" : "Back";

                Console.WriteLine(Title);
                Console.WriteLine("==============");
                foreach(MenuItem item in r_SubMenuItems)
                {
                    Console.WriteLine("{0}. {1}", itemIndexInList++, item.Title);
                }

                Console.WriteLine("0. {0}", backOrExitPrompt);
                Console.WriteLine("Please enter your choice (1-{0} or 0 for {1}):", itemIndexInList - 1, backOrExitPrompt);
            }
        }

        internal void AddSubMenuItem(MenuItem i_SubMenuItem)
        {
            if(r_IsMenu)
            {
                r_SubMenuItems.Add(i_SubMenuItem);
            }
        }

        internal void MenuItemSelected()
        {
            OnMenuItemSelectOccurred(new MenuItemSelectedEventArgs(this));
        }

        protected virtual void OnMenuItemSelectOccurred(MenuItemSelectedEventArgs i_EventArguments)
        {
            if(MenuItemSelectOccurred != null)
            {
                MenuItemSelectOccurred.Invoke(this, i_EventArguments);
            }
        }
    }
}
