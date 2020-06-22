using System;
using System.Collections.Generic;
using System.Text;

namespace Ex04.Menus.Delegates
{
    public class MainMenu
    {
        private MenuItem m_CurrentMenuItem;
        private readonly MenuItem r_RootMenuItem;

        public MainMenu(string i_Title)
        {
            m_CurrentMenuItem = new MenuItem(i_Title, null, 0, false);
            r_RootMenuItem = m_CurrentMenuItem;

        }

        public bool TryAddMenuItem(string i_Title, Action i_ActionFunc = null)
        {
            bool wasSuccess = false;
            bool isExecutable = i_ActionFunc != null;

            if (!m_CurrentMenuItem.IsExecutable)
            {
                MenuItem menuItemToAdd = new MenuItem(i_Title, m_CurrentMenuItem, m_CurrentMenuItem.Level + 1, isExecutable, i_ActionFunc);
                m_CurrentMenuItem.SubMenuItems.Add(menuItemToAdd);
                if(isExecutable)
                {
                    menuItemToAdd.Chosen += MenuItem_OnChosenExecutable;
                }
                else
                {
                    menuItemToAdd.Chosen += MenuItem_OnChosenNonExecutable;
                }
                wasSuccess = true;
            }

            return wasSuccess;
        }

        public void TraverseUp()
        {
            MenuItem parentMenuItem = m_CurrentMenuItem.ParentMenuItem;
            if(parentMenuItem == null)
            {
                throw new InvalidOperationException("Root MenuItem is God");
            }

            m_CurrentMenuItem = parentMenuItem;
        }

        public void TraverseDown(int i_MenuItemIdx)
        {

            try
            {
                MenuItem childMenuItem = m_CurrentMenuItem.SubMenuItems[i_MenuItemIdx];
                m_CurrentMenuItem = childMenuItem;
            }
            catch(IndexOutOfRangeException e)
            {
                e.Data.Add("UserMessage", "This Is a leaf MenuItem with no children");
                throw;
            }

        }

        public void Show()
        {
            m_CurrentMenuItem = r_RootMenuItem;
            while(true)
            {
                Console.WriteLine(buildMenu(m_CurrentMenuItem));
                int userInput = getValidInputFromUser(m_CurrentMenuItem);
                if(userInput == 0)
                {
                    if(m_CurrentMenuItem.Level == 0)
                    {
                        Console.Out.WriteLine("BYEEEEEEEEEEEE");
                        break;
                    }

                    m_CurrentMenuItem = m_CurrentMenuItem.ParentMenuItem;
                    continue;
                }

                MenuItem menuItemPicked = m_CurrentMenuItem.SubMenuItems[userInput - 1];
                menuItemPicked.OnChosen();

                Console.Clear();
            }
        }

        private void MenuItem_OnChosenExecutable(MenuItem menuItemPicked)
        {
            menuItemPicked.OnExecute();
        }

        private void MenuItem_OnChosenNonExecutable(MenuItem menuItemPicked)
        {
            m_CurrentMenuItem = menuItemPicked;
        }


        private string buildMenu(MenuItem i_MenuItemToShow)
        {
            StringBuilder menu = new StringBuilder();
            menu.Append($"{i_MenuItemToShow.Title}: {i_MenuItemToShow.Level}\n");
            List<MenuItem> subMenuItems = i_MenuItemToShow.SubMenuItems;
            int optionNumber = 1;
            foreach(MenuItem menuItem in subMenuItems)
            {
                menu.Append($"{optionNumber} - {menuItem.Title}\n");
                optionNumber++;
            }

            string lastOption = i_MenuItemToShow.Level == 0 ? "Exit" : "Back";
            menu.Append($"0 - {lastOption}\n");
            return menu.ToString();
        }

        private int getValidInputFromUser(MenuItem i_CurrentMenuBeingShown)
        {
            string input = Console.ReadLine();
            int menuItemIdx = 0;
            while(!int.TryParse(input, out menuItemIdx) || menuItemIdx < 0
                                                        || menuItemIdx > i_CurrentMenuBeingShown.SubMenuItems.Count)
            {
                Console.Out.WriteLine("Your input was not a valid menu index, please try again.");
                input = Console.ReadLine();
            }

            return menuItemIdx;
        }
        
    }
}
