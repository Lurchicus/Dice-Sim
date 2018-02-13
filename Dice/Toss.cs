using System;
using System.IO;
using System.Reflection;

namespace Dice
{
    /// <summary>
    /// CLI Dice throwing simulator (Dice-Sim)
    /// </summary>
    //
    //  CLI Parse rule
    //
    //  [quantity int (optional: default="1")]
    //  d 
    //  [sides int (optional: default=6)]
    //  [ (optional, treated as 0 if omitted)
    //      [operator +|- (default="+")]
    //      [adjustment int (optional: default="0")] 
    //  ]
    //
    //  1.0.8.0 - 11/17/2017 - Stored to public GitHub repository
    //  1.0.9.0 - 11/18/2017 - Check for overlarge values that may cause
    //            Int32 overflows (most noticable by getting a negitive
    //            result.
    //          - Added a try/catch around the die creation code to catrch
    //            out of memory exceptions. Yes, you can have issues if 
    //            you try to do 1,000,000,000 coin flips.
    //          - Pull the real version number for the intro
    //          - Fix bug where nD1 stopped working (only returned zeros)
    //  1.0.10.0 - 2/12/2018 - Added a few alternate CLI directives to the
    //             parser so "-" and "/" prefixes are accepted. 
    //           - Added GC.Collect() after clearing the die objects
    //           - Added an "l" command to display the GNU GPL 3
    //             license.
    //           - Added a ShowFile() function to show the license file 
    //             (or any other file for that matter) paginated to fit
    //             the console screen.
    //
    class Toss
    {
        public static Int32 diceCount = 1;
        public static Int32 sideCount = 6;
        public static Int32 adjust = 0;
        public static bool Debug = false;
        public static bool Quit = false;

        /// <summary>
        /// Take optional initial CLI input then look for and process 
        /// addtional input 
        /// </summary>
        /// <param name="args">string[] Command line arguments</param>
        static void Main(string[] args)
        {
            string Inp = string.Empty;
            Int32 Tot = 0;

            Assembly thisAsbly = typeof(Toss).Assembly;
            AssemblyName thisAsblyName = thisAsbly.GetName();
            Version ver = thisAsblyName.Version;

            Wl("Dice Tosser "+ver+" by Dan Rhea © 2017, 2018\n");
            Wl("q to quit, ? for help");
            if (args.Length > 0)
            {
                //Pass on whatever we got from the command line
                Inp = args[0];
            }
            else
            {
                //Otherwise, prompt for input
                W(">");
                Inp = R();
            }
            while (!Quit)
            {
                //Parse the input
                Parse(Inp);
                //too big to roll?
                if ((diceCount * sideCount) + adjust > (Int32.MaxValue / 2) ||
                    diceCount > (Int32.MaxValue / 2) ||
                    sideCount > (Int32.MaxValue / 2) ||
                    adjust > (Int32.MaxValue / 2))
                {
                    Wl("Sorry, please reduce the number of dies, sides or the adjustment. It's just too big!");
                }
                else
                {
                    if (!Quit)
                    {
                        //If we are not quitting, instanciate dies and throw
                        Tot = 0;
                        Dies dice = new Dies(diceCount, adjust, sideCount);
                        Tot += dice.ThrowDice();
                        if (Debug)
                        {
                            //If debug is on, show the result on each die
                            for (Int32 Idx = 0; Idx < dice.GetCount(); Idx++)
                            {
                                Wl("Die " + Idx + ": " + dice.Result(Idx));
                            }
                        }
                        //Dispose of ther dies
                        dice.Empty();
                        GC.Collect();
                    }

                    if (Quit)
                    {
                        //If the quit flag is on, get out of here
                        break;                    }
                    else
                    {
                        //Otherwise, Display the result
                        Wl(Tot + " (" + diceCount + "d" + sideCount + (adjust >= 0 ? "+" : "") + adjust + ")");
                    }
                }
                if (!Quit)
                {
                    //Reprompt
                    W(">");
                    Inp = R();
                }
            }
        }

        /// <summary>
        /// Parse and process individual CLI imputs one at a time. By default
        /// parse out the individual dice command or default to 1d6+0
        /// </summary>
        /// <param name="arg">string Command line input</param>
        public static void Parse(string arg)
        {
            string arrg = arg.Trim().ToUpper();
            string[] parm1 = { "D" }; //Dies/sides delimiter
            string[] parm2 = { "+", "-" }; //Adjustment delimiter
            string sside = "";
            switch (arg) 
            {
                case "-d": //Toggle debug mode on and off
                case "/d": 
                    if (Debug)
                    {
                        Wl("Debug off");
                        Debug = false;
                    }
                    else
                    {
                        Wl("Debug on");
                        Debug = true;
                    }
                    break;
                case "q": //Set quit flag
                case "-q":
                case "/q":
                    Wl("Goodbye...");
                    Quit = true;
                    break;
                case "x": //Set quit flag
                case "-x":
                case "/x":
                    Wl("Goodbye...");
                    Quit = true;
                    break;
                case "?": //Display help
                case "-?":
                case "/?":
                    Wl("[[[qty]D]sides][+|-][adj] (default \"1D6+0\") sides of 1 or 2 is a coin flip");
                    Wl("where 1 rolls 1 or 0 and 2 rolls 1 or 2.");
                    Wl("\"-d\" Debug toggle (shows each die result)");
                    Wl("\"q\" or \"x\" to quit");
                    Wl("\"l\" to display the license file");
                    Wl("No input repeats last dice throw or defaults to \"1D6+0\" for first throw");
                    break;
                case "l": //Display license text
                case "-l":
                case "/l":
                    ShowFile("\\gnu_gpl3.txt");
                    break;
                default: //Parse individual dice roll command or default to 1d6+0
                    if (arrg.Length == 0)
                    {
                        //No argument so default 1d6+0
                        if (diceCount == 0) { diceCount = 1; }
                        if (sideCount == 0) { sideCount = 6; }
                        if (adjust == 0) { adjust = 0; }
                        break;
                    }
                    if (arrg.StartsWith("D"))
                    {
                        //Does the command start with "D"
                        if (arrg.Length == 1)
                        {
                            //Only a "D" so default 1d6+0
                            diceCount = 1;
                            sideCount = 6;
                            adjust = 0;
                            break;
                        }
                        else
                        {
                            //More to do here, default to one die
                            diceCount = 1;
                            arrg = arrg.Substring(1, arrg.Length - 1);
                        }
                    }
                    else
                    {
                        //Didn't start with a "D" split using "D" as a delimiter. The first argument
                        //should be the die count
                        string[] ary = arrg.Split(parm1, StringSplitOptions.RemoveEmptyEntries );
                        sside = ary[0];
                        try
                        {
                            diceCount = Convert.ToInt32(sside);
                        }
                        catch
                        {
                            diceCount = 1;
                        }
                        if (ary.Length == 1)
                        {
                            // Only die count, default to 6 sides die with no adjustment
                            sideCount = 6;
                            adjust = 0;
                            break;
                        }
                        else
                        {
                            //More to do after "D", pass it along
                            arrg = ary[1];
                        }
                    }
                    //Split the remaining parts using "+" or "-" as a delimiter
                    string[] ary2 = arrg.Split(parm2, StringSplitOptions.RemoveEmptyEntries);
                    if (ary2.Length == 1)
                    {
                        //We only got a single result which should be the number of sides 
                        //on the die with no adjustment
                        try
                        {
                            sideCount = Convert.ToInt32(ary2[0]);
                        }
                        catch
                        {
                            sideCount = 6;
                        }
                        adjust = 0;
                    }
                    else
                    {
                        //More than a single result, should have die side count and adjustment
                        if (ary2.Length > 1)
                        {
                            try
                            {
                                sideCount = Convert.ToInt32(ary2[0]);                               
                            }
                            catch
                            {
                                sideCount = 6;
                            }
                            try
                            {
                                adjust = Convert.ToInt32(ary2[1]);
                            }
                            catch
                            {
                                adjust = 0;
                            }
                            if (adjust > 0)
                            {
                                if (arrg.Contains("-"))
                                {
                                    //if the delimiter was a "-", negate the adjustment
                                    adjust = 0 - adjust;
                                }
                            }
                        }
                        if (ary2.Length <= 0)
                        {
                            //Nothing to see here, defaut six sided die and no adjustment
                            sideCount = 6;
                            adjust = 0;
                        }
                    }
                    break;
            }
        }

        /// <summary>
        /// Will display a paginated file to the Console. If a path
        /// is not supplied, the same path where the program resides
        /// is used
        /// </summary>
        /// <param name="File">File to display</param>
        /// <param name="Path">Path the file resides in (optional)</param>
        public static void ShowFile(String FileName, String PathName = null)
        {
            Int32 ScreenHeight = Console.WindowHeight;
            Int32 ScreenWidth = Console.WindowWidth;
            Int32 Row = 0;
            String Inp = "";
            String Nam = "";
            String AppPath = "";

            if (PathName == null)
            {
                AppPath = Assembly.GetExecutingAssembly().Location;
                Nam = Path.GetFileName(AppPath);
                AppPath = AppPath.Substring(0, AppPath.Length - Nam.Length);
                if (FileName.Substring(0, 2) != "\\")
                {
                    AppPath += "\\" + FileName;
                }
                else
                {
                    AppPath += FileName;
                }
            }
            else
            {
                if (FileName.Substring(0, 2) != "\\")
                {
                    AppPath += PathName + "\\" + FileName;
                }
                else
                {
                    AppPath = PathName + FileName;
                }
            }
            try
            {
                using (StreamReader sr = new StreamReader(AppPath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        Wl(line);
                        if (line.Length > ScreenWidth-1)
                        {
                            Row = Row + (Int32)(line.Length / ScreenWidth - 1);
                        }
                        else
                        {
                            Row++; 
                        }
                        if(Row >= ScreenHeight-1)
                        {
                            Row = 0;
                            W("Press Enter to continue (enter q to quit):");
                            Inp = R();
                            if (Inp == "q" || Inp == "Q")
                            {
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Wl("Could not read the the text in file "+FileName+".");
                Wl(e.Message);
            }
        }

        /// <summary>
        /// wrapper function for Console.WriteLine()
        /// </summary>
        /// <param name="Msg">string to output</param>
        public static void Wl(string Msg)
        {
            if (Msg != null)
            {
                Console.WriteLine(Msg);
            }
            else
            {
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Wrapper function for Console.Write()
        /// </summary>
        /// <param name="Msg">string to output</param>
        public static void W(string Msg)
        {
            if (Msg != null)
            {
                Console.Write(Msg);
            }
        }

        /// <summary>
        /// Wrapper function for Console.ReadLine
        /// </summary>
        /// <returns>string input</returns>
        public static string R()
        {
            return Console.ReadLine();
        }
    }
}
