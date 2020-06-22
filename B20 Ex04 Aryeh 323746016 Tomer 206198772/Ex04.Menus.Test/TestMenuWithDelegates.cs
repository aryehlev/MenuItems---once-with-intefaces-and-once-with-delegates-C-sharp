using System;
using Ex04.Menus.Delegates;

namespace Ex04.Menus.Test
{
    class TestMenuWithDelegates
    {
        public static void ShowTest()
        {
            MainMenu mainMenu = new MainMenu("Menu With Delegates");
            mainMenu.TryAddMenuItem("Version and Capitals");
            mainMenu.TryAddMenuItem("Show Date/Time");
            mainMenu.TraverseDown(0);
            mainMenu.TryAddMenuItem("Count Capitals", print);
            mainMenu.TryAddMenuItem("Show Version", print);
            mainMenu.TraverseUp();
            mainMenu.TraverseDown(1);
            mainMenu.TryAddMenuItem("Show Time", print);
            mainMenu.TryAddMenuItem("Show Date", print);
            mainMenu.Show();
        }

        public static void print()
        {
            Console.Out.WriteLine("123");
        }
    }
}
