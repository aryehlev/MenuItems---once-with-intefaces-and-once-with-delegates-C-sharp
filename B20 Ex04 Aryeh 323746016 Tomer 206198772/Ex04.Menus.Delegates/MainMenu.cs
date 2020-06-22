using System;
using System.Collections.Generic;
using System.Text;

namespace Ex04.Menus.Delegates
{
    class MainMenu
    {
        private MenuItem m_RootOfMenus;

        public MainMenu(string i_Title)
        {
            m_RootOfMenus = new MenuItem(i_Title, null, null, 0, false);
        }

        public MenuItem RootOfMenus { get;}

        public void Show()
        {
            MenuItem currentMenuBeingShown = RootOfMenus;
            while(true)
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
                    currentMenuBeingShown = currentMenuBeingShown.FatherItem;
                    continue;
                }

                MenuItem menuItemPicked = currentMenuBeingShown.SubMenuItems[userInput - 1];
                if(menuItemPicked.IsFinal)
                {
                    menuItemPicked.OnFinalItemWasChosen();
                }
                else
                {
                    currentMenuBeingShown = menuItemPicked;
                }
            }
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
            while(!int.TryParse(input, out menuItemIdx) || menuItemIdx < 0 || menuItemIdx > i_CurrentMenuBeingShown.SubMenuItems.Count)
            {
                Console.Out.WriteLine("Your input was not a valid menu index, please try again.");
                input = Console.ReadLine();
            }

            return menuItemIdx;
        }
    }
}
