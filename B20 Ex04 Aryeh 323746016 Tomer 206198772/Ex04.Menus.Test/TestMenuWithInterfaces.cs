using System;
using Ex04.Menus.Interfaces;

namespace Ex04.Menus.Test
{
    class TestMenuWithInterfaces
    {
        public static void ActivateTest()
        {
            MainMenu mainMenu = new MainMenu("Menu With Interfaces");
            mainMenu.TryAddNonExecutableMenuItem("Version and Capitals");
            mainMenu.TryAddNonExecutableMenuItem("Show Date/Time");
            mainMenu.TraverseDown(0);
            //mainMenu.TryAddExecutableMenuItem("Count Capitals", print);
            //mainMenu.TryAddExecutableMenuItem("Show Version", print);
            //mainMenu.TraverseUp();
            //mainMenu.TraverseDown(1);
            //mainMenu.TryAddExecutableMenuItem("Show Time", print);
            //mainMenu.TryAddExecutableMenuItem("Show Date", print);
            //mainMenu.Show();
        }

        class countCapitals : IExecutable
        {
            public void FunctionToExecute()
            {
                Console.Out.WriteLine("please enter a sentence in english");
                string input = Console.ReadLine();
                int numOfCapitalLetters = 0;
                if(input != null)
                {
                    foreach(char c in input)
                    {
                        numOfCapitalLetters += char.IsUpper(c) ? 1 : 0;
                    }
                }

                Console.Out.WriteLine($"There are {numOfCapitalLetters} Capital letters");
            }
        }

        class showVersion : IExecutable
        {
            public void FunctionToExecute()
            {
                Console.Out.WriteLine("Version: 20.2.4.30620");
            }
        }

        class showTime : IExecutable
        {
            public void FunctionToExecute()
            {
                Console.Out.WriteLine(DateTime.Now.TimeOfDay);
            }
        }

        class showDate : IExecutable
        {
            public void FunctionToExecute()
            {
                Console.Out.WriteLine(DateTime.Now.Date);
            }
        }
    }
}
