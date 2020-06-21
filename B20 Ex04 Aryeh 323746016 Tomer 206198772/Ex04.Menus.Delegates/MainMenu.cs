using System;

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
                buildMenu(currentMenuBeingShown);
                if(checkIfGoodInput(out string inputFromUser))
                {
                    MenuItem menuItemPicked = getSubMenuFromUsr(currentMenuBeingShown, inputFromUser, out bool wantsToGoBack);
                    if(wantsToGoBack)
                    {
                        if(currentMenuBeingShown == RootOfMenus)
                        {
                            //print goodbye
                            break;
                        }
                        else
                        {
                            currentMenuBeingShown = currentMenuBeingShown.FatherItem;
                            continue;
                        }
                    }

                    if(menuItemPicked.IsFinal)
                    {
                        ///do action
                    }
                    else
                    {
                        currentMenuBeingShown = menuItemPicked;
                    }
                }
            }
        }
        
        private void buildMenu(MenuItem i_MenuItemToShow)
        {

        }

        private MenuItem getSubMenuFromUsr(MenuItem i_CurrentMenuItem, string i_NumOfSubMenuFromUser, out bool i_WantsToGoBack)
        {
            i_WantsToGoBack = false;
            return null;
        }

        private bool checkIfGoodInput(out string o_ErrorMsg)
        {
            o_ErrorMsg = "";
            return true;
        }
    }
}
