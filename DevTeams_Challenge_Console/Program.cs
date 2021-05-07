using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTeams_Challenge_Console
{
    class Program
    {
        //This class is the starting point for our Console Application. This main method is the first code to be run when we start the program. We have made the connection to the ProgramUI for you.
        static void Main(string[] args)
        {
            ProgramUI ui = new ProgramUI();
            ui.Run();
        }
    }
}
