using System;
using System.Collections.Generic;
using System.Text;

namespace Ex04.Menus.Interfaces
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

            public bool TryAddNonExecutableMenuItem(string i_Title)
            {
                return m_CurrentMenuItem.TryAddNonExecutableMenuItem(i_Title);
            }

            public bool TryAddExecutableMenuItem(string i_Title, IExecutable i_ExecutableMenuItem)
            {
                return m_CurrentMenuItem.TryAddExecutableMenuItem(i_Title, i_ExecutableMenuItem);
            }

            public void TraverseUp()
            {
                MenuItem parentMenuItem = m_CurrentMenuItem.ParentMenuItem;
                if (parentMenuItem == null)
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
                catch (IndexOutOfRangeException e)
                {
                    e.Data.Add("UserMessage", "This Is a leaf MenuItem with no children");
                    throw;
                }

            }

            public void Show()
            {
                MenuItem currentMenuBeingShown = r_RootMenuItem;
                while (true)
                {
                    Console.WriteLine(buildMenu(currentMenuBeingShown));
                    int userInput = getValidInputFromUser(currentMenuBeingShown);
                    if (userInput == 0)
                    {
                        if (currentMenuBeingShown.Level == 0)
                        {
                            Console.Out.WriteLine("BYEEEEEEEEEEEE");
                            break;
                        }
                        currentMenuBeingShown = currentMenuBeingShown.ParentMenuItem;
                        continue;
                    }

                    MenuItem menuItemPicked = currentMenuBeingShown.SubMenuItems[userInput - 1];
                    if (menuItemPicked.IsExecutable)
                    {
                        menuItemPicked.OnItemWasChosen();
                        Console.WriteLine("please press enter to go back to last menu");
                        Console.ReadLine();
                    }
                    else
                    {
                        currentMenuBeingShown = menuItemPicked;
                    }
                    Console.Clear();

                }
            }

            private string buildMenu(MenuItem i_MenuItemToShow)
            {
                StringBuilder menu = new StringBuilder();
                menu.Append($"{i_MenuItemToShow.Title}: {i_MenuItemToShow.Level}\n");
                List<MenuItem> subMenuItems = i_MenuItemToShow.SubMenuItems;
                int optionNumber = 1;
                foreach (MenuItem menuItem in subMenuItems)
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
                while (!int.TryParse(input, out menuItemIdx) || menuItemIdx < 0 || menuItemIdx > i_CurrentMenuBeingShown.SubMenuItems.Count)
                {
                    Console.Out.WriteLine("Your input was not a valid menu index, please try again.");
                    input = Console.ReadLine();
                }

                return menuItemIdx;
            }
        }
}
