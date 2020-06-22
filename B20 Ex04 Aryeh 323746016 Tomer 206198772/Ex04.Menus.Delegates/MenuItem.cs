using System;
using System.Collections.Generic;

namespace Ex04.Menus.Delegates
{
    class MenuItem
    {
        private readonly string r_Title;
        private List<MenuItem> m_SubMenuItems;
        private readonly MenuItem r_ParentMenuItem;
        private readonly bool r_IsExecutable;
        private readonly int r_Level;

        public event Action FinalItemWasChosen;

        internal MenuItem(
            string i_Title,
            MenuItem i_ParentMenuItem,
            int i_Level,
            bool i_IsExecutable)
        {
            r_Title = i_Title;
            m_SubMenuItems = new List<MenuItem>();
            r_ParentMenuItem = i_ParentMenuItem;
            r_Level = i_Level;
            r_IsExecutable = i_IsExecutable;
        }

        internal string Title
        {
            get
            {
                return r_Title;
            }
        }
        internal bool IsExecutable
        {
            get
            {
                return r_IsExecutable;
            }
        }

        internal MenuItem ParentMenuItem
        {
            get
            {
                return r_ParentMenuItem;
            }
        }

        internal int Level
        {
            get
            {
                return r_Level;
            }
        }
        
        internal List<MenuItem> SubMenuItems
        {
            get
            {
                return m_SubMenuItems;
            }
        }

        internal bool TryAddNonExecutableMenuItem(string i_Title)
        {
            bool wasSuccess = false;
            
            if(!this.r_IsExecutable)
            {
                MenuItem menuItemToAdd = new MenuItem(i_Title, this, this.r_Level + 1, false);
                m_SubMenuItems.Add(menuItemToAdd);
                wasSuccess = true;
            }

            return wasSuccess;
        }

        internal bool TryAddExecutableMenuItem(string i_Title, Action i_ActionFunc)
        {
            bool wasSuccess = false;
            
            if(!this.r_IsExecutable)
            {
                MenuItem menuItemToAdd = new MenuItem(i_Title, this, this.r_Level + 1, true);
                m_SubMenuItems.Add(menuItemToAdd);
                menuItemToAdd.FinalItemWasChosen += i_ActionFunc;
                wasSuccess = true;
            }

            return wasSuccess;
        }

        internal void OnFinalItemWasChosen()
        {
            FinalItemWasChosen?.Invoke();
        }
    }
}
