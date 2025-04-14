using System;
using System.Collections.Generic;

namespace Ex04.Menus.Interfaces
{
    public class MenuItem
    {
        private List<ISelectedItem> m_SelectedListeners;
        private readonly MenuItem r_ParentNode;
        private readonly List<MenuItem> r_SubMenuItems;
        private readonly IMenuAction r_MenuAction;
        private readonly string r_Title;
        private readonly bool r_IsMenu;
        private const bool v_IsMenu = true;
        private const bool v_Selected = true;
        private bool m_Selected = !v_Selected;
        
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

        public MenuItem(MenuItem i_ParentNode, string i_Title, IMenuAction i_MenuAction) : this(i_ParentNode, i_Title, !v_IsMenu)
        {
            r_MenuAction = i_MenuAction;
        }

        internal string Title
        { 
            get 
            { 
                return r_Title; 
            }
        }

        internal bool IsMenu
        {
            get
            {
                return r_IsMenu;
            }
        }

        internal bool Selected
        {
            get
            {
                return m_Selected;
            }
            set
            {
                m_Selected = value;
                if(m_Selected)
                {
                    notifyAllListeners();
                }
            }
        }

        internal MenuItem ParentNode
        {
            get
            {
                return r_ParentNode;
            }
        }

        internal List<MenuItem> GetSubMenuItems()
        {
            return r_SubMenuItems;
        }

        internal void AddSubMenuItem(MenuItem i_MenuItem)
        {
            if(IsMenu)
            {
                r_SubMenuItems.Add(i_MenuItem);
            }
        }
        
        public void AddListener(ISelectedItem i_SelectedListener)
        {
            if(m_SelectedListeners == null)
            {
                m_SelectedListeners = new List<ISelectedItem>();
            }

            m_SelectedListeners.Add(i_SelectedListener);
        }

        public void RemoveListener(ISelectedItem i_SelectedListener)
        {
            m_SelectedListeners.Remove(i_SelectedListener);
        }
     
        private void notifyAllListeners()
        {
            foreach(ISelectedItem selectedListener in m_SelectedListeners)
            {
                selectedListener.Selected(this);
            }

            Selected = !v_Selected;
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

        internal void Execute()
        {
            if(!IsMenu && r_MenuAction != null)
            {
                r_MenuAction.Execute();
            }
        }
    }
}
