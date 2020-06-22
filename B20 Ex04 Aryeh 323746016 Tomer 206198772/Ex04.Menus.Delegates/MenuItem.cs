using System;
using System.Collections.Generic;
using System.Linq;

namespace Ex04.Menus.Delegates
{
    class MenuItem
    {
        private readonly string r_Title;
        private List<MenuItem> m_SubMenuItems;
        private readonly MenuItem r_FatherItem;
        private readonly bool r_IsFinal;
        private readonly int r_Level;

        public event Action FinalItemWasChosen;

        

        internal MenuItem(
            string i_Title,
            List<MenuItem> i_SubMenuItems,
            MenuItem i_FatherItem,
            int i_Level,
            bool i_IsFinal)
        {
            r_Title = i_Title;
            m_SubMenuItems = i_SubMenuItems;
            r_FatherItem = i_FatherItem;
            m_Level = i_Level;
            r_IsFinal = i_IsFinal;
        }

        public string Title
        {
            get
            {
                return r_Title;
            }
        }
        public bool IsFinal
        {
            get
            {
                return r_IsFinal;
            }
        }

        public MenuItem FatherItem
        {
            get
            {
                return r_FatherItem;
            }
        }

        internal int Level
        {
            get
            {
                return r_Level;
            }
        }



        public List<MenuItem> SubMenuItems
        {
            get
            {
                return m_SubMenuItems;
            }
        }

        public bool TryAddMainMenuItem(string i_Title, out MenuItem o_MenuItemAdded)
        {
            bool wasSuccess = false;
            o_MenuItemAdded = null;

            if(!this.r_IsFinal)
            {
                MenuItem menuItemToAdd = new MenuItem(i_Title, null, this, this.m_Level + 1, false);
                m_SubMenuItems.Add(menuItemToAdd);
                o_MenuItemAdded = menuItemToAdd;
                wasSuccess = true;
            }

            return wasSuccess;
        }

        public bool TryAddMainMenuItem(string i_Title, Action i_ActionOptionalIfFinal, out MenuItem o_MenuItemAdded)
        {
            bool wasSuccess = false;
            o_MenuItemAdded = null;

            if(!this.r_IsFinal)
            {
                MenuItem menuItemToAdd = new MenuItem(i_Title, null, this, this.m_Level + 1, true);
                m_SubMenuItems.Add(menuItemToAdd);
                o_MenuItemAdded = menuItemToAdd;
                menuItemToAdd.FinalItemWasChosen += i_ActionOptionalIfFinal;
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
