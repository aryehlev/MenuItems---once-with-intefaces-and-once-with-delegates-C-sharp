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
        public event Action FinalItemWasChosen;
        private readonly int m_Level;
        
        public MenuItem(string i_Title, List<MenuItem> i_SubMenuItems, MenuItem i_FatherItem, int i_Level, bool i_IsFinal)
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
                return r_FatherItem
            }
        }

        public bool TryAddMainMenuItem(string i_Title, bool i_ItemToAddIsFinal, out MenuItem i_MenuItemAdded, Action i_ActionOptionalIfFinal = null)
        {
            bool wasSuccess = false;
            i_MenuItemAdded = null;

            if (!this.r_IsFinal)
            {
                MenuItem menuItemToAdd = new MenuItem(i_Title, null, this, this.m_Level + 1, i_ItemToAddIsFinal);
                m_SubMenuItems.Add(menuItemToAdd);
                i_MenuItemAdded = menuItemToAdd;
                wasSuccess = true;
            }

            if(i_ItemToAddIsFinal)
            {
                i_MenuItemAdded.FinalItemWasChosen += i_ActionOptionalIfFinal;
            }

            return wasSuccess;
        } 
        
        protected virtual void OnFinalItemWasChosen()
        {
            FinalItemWasChosen?.Invoke();
        }
    }
}
