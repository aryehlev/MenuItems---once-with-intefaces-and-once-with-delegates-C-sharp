using System;
using System.Collections.Generic;

namespace Ex04.Menus.Interfaces
{ 
    class MenuItem
        {
            private readonly string r_Title;
            private List<MenuItem> m_SubMenuItems;
            private readonly MenuItem r_ParentMenuItem;
            private readonly bool r_IsExecutable;
            private readonly int r_Level;
            private IExecutable m_Executable;

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

                if (!this.r_IsExecutable)
                {
                    MenuItem menuItemToAdd = new MenuItem(i_Title, this, this.r_Level + 1, false);
                    m_SubMenuItems.Add(menuItemToAdd);
                    wasSuccess = true;
                }

                return wasSuccess;
            }

            internal bool TryAddExecutableMenuItem(string i_Title, IExecutable i_ExecutableMenuItem)
            {
                bool wasSuccess = false;

                if (!this.r_IsExecutable)
                {
                    MenuItem menuItemToAdd = new MenuItem(i_Title, this, this.r_Level + 1, true);
                    menuItemToAdd.m_Executable = i_ExecutableMenuItem;
                    m_SubMenuItems.Add(menuItemToAdd);
                    wasSuccess = true;
                }

                return wasSuccess;
            }

            internal void OnExecutableItemWasChosen()
            {
                m_Executable?.FunctionToExecute();
            }
        }
    }


