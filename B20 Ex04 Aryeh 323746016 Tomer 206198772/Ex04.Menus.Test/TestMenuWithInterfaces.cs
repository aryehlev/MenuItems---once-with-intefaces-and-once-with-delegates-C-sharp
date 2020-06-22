using System;
using Ex04.Menus.Interfaces;

namespace Ex04.Menus.Test
{
    class TestMenuWithInterfaces
    {
        public static void ShowTest()
        {
            MainMenu mainMenu = new MainMenu("Menu With Interfaces");
            mainMenu.TryAddNonExecutableMenuItem("Version and Capitals");
            mainMenu.TryAddNonExecutableMenuItem("Show Date/Time");
            mainMenu.TraverseDown(0);
            mainMenu.TryAddExecutableMenuItem("Count Capitals", print);
            mainMenu.TryAddExecutableMenuItem("Show Version", print);
            mainMenu.TraverseUp();
            mainMenu.TraverseDown(1);
            mainMenu.TryAddExecutableMenuItem("Show Time", print);
            mainMenu.TryAddExecutableMenuItem("Show Date", print);
            mainMenu.Show();
        }

        public static void print()
        {
            Console.Out.WriteLine("123");
        }
    }
}
