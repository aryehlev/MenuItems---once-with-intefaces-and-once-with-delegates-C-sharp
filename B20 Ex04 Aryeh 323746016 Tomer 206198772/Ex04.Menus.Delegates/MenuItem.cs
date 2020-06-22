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

        public event Action<MenuItem> Chosen;

        public event Action Execute; 
        internal MenuItem(
            string i_Title,
            MenuItem i_ParentMenuItem,
            int i_Level,
            bool i_IsExecutable,
            Action i_ActionFunc = null)
        {
            r_Title = i_Title;
            m_SubMenuItems = new List<MenuItem>();
            r_ParentMenuItem = i_ParentMenuItem;
            r_Level = i_Level;
            r_IsExecutable = i_IsExecutable;
            Execute = i_ActionFunc;
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
        
        internal void OnChosen()
        {
            Chosen?.Invoke(this);
        }

        internal void OnExecute()
        {
            Execute?.Invoke();
            Console.WriteLine("please press enter to go back to last menu");
            Console.ReadLine();
        }
    }
}
