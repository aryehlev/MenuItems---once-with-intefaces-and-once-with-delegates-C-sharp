using System;
using System.Collections.Generic;
using System.Linq;

namespace Ex04.Menus.Delegates
{
    class MenuItem
    {
        private readonly string r_Title;
        private List<MenuItem> m_SubMenuItems;
        private readonly MenuItem r_ParentMenuItem;
        private readonly bool r_IsFinal;
        private readonly int r_Level;

        public event Action FinalItemWasChosen;

        

        internal MenuItem(
            string i_Title,
            List<MenuItem> i_SubMenuItems,
            MenuItem i_ParentMenuItem,
            int i_Level,
            bool i_IsFinal)
        {
            r_Title = i_Title;
            m_SubMenuItems = i_SubMenuItems;
            r_ParentMenuItem = i_ParentMenuItem;
            r_Level = i_Level;
            r_IsFinal = i_IsFinal;
        }

        internal string Title
        {
            get
            {
                return r_Title;
            }
        }
        internal bool IsFinal
        {
            get
            {
                return r_IsFinal;
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

        internal bool TryAddMenuItem(string i_Title)
        {
            bool wasSuccess = false;
            
            if(!this.r_IsFinal)
            {
                MenuItem menuItemToAdd = new MenuItem(i_Title, null, this, this.r_Level + 1, false);
                m_SubMenuItems.Add(menuItemToAdd);
                wasSuccess = true;
            }

            return wasSuccess;
        }

        internal bool TryAddMenuItem(string i_Title, Action i_ActionFunc)
        {
            bool wasSuccess = false;
            
            if(!this.r_IsFinal)
            {
                MenuItem menuItemToAdd = new MenuItem(i_Title, null, this, this.r_Level + 1, true);
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
