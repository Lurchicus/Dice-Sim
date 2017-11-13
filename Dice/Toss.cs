using System;

namespace Dice
{
    /// <summary>
    /// CLI Dice throwing simulator
    /// </summary>
    //
    //  CLI Parse rule
    //
    //  [quantity int (optional: default="1")]
    //  d 
    //  [sides int (optional: default=6)]
    //  [
    //      [operator +|- (default="+")]
    //      [adjustment int (default="0")] 
    //      (optional)
    //  ]
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
        /// <param name="args">Command line arguments</param>
        static void Main(string[] args)
        {
            string Inp = "";
            Int32 Tot = 0;

            Wl("Dice Tosser v1.0 by Dan Rhea © 2017\n");
            Wl("q to quit, ? for help");
            if (args.Length > 0)
            {
                Inp = args[0];
            }
            else
            {
                W(">");
                Inp = R();
            }
            while (!Quit)
            {
                Parse(Inp);
                if (!Quit)
                {
                    Tot = 0;
                    dies dice = new dies(diceCount, adjust, sideCount);
                    Tot += dice.ThrowDice();
                    if (Debug)
                    {
                        for (Int32 Idx = 0; Idx < dice.Count; Idx++)
                        {
                            Wl("Die " + Idx + ": " + dice.Result(Idx));
                        }
                    }
                    dice.Empty();
                }
                Wl(Tot + " (" + diceCount + "d" + sideCount + (adjust >= 0 ? "+" : "") + adjust + ")");
                W(">");
                Inp = R();

            }
        }

        /// <summary>
        /// Parse and process individual CLI imputs
        /// </summary>
        /// <param name="arg">string Command line input</param>
        public static void Parse(string arg)
        {
            string arrg = arg.Trim().ToUpper();
            string[] parm1 = { "D" };
            string[] parm2 = { "+", "-" };
            string sside = "";
            switch (arg) 
            {
                case "-d":
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
                case "q":
                    Wl("Goodbye...");
                    Quit = true;
                    break;
                case "x":
                    Wl("Goodbye...");
                    Quit = true;
                    break;
                case "?":
                    Wl("[[[qty]D]sides][+|-][adj] (default \"1D6+0\") sides of 1 or 2 is a coin flip.");
                    Wl("\"-d\" Debug toggle (shows each die result)");
                    Wl("\"q\" or \"x\" to quit");
                    Wl("No input repeats last dice throw or defaults to \"1D6+0\" for first throw");
                    break;
                case "-?":
                    Wl("[[[qty]D]sides][+|-][adj] (default \"1D6+0\") sides of 1 or 2 is a coin flip.");
                    Wl("\"-d\" Debug toggle (shows each die result)");
                    Wl("\"q\" or \"x\" to quit");
                    Wl("No input repeats last dice throw or defaults to \"1D6+0\" for first throw");
                    break;
                default:
                    if (arrg.Length == 0)
                    {
                        if (diceCount == 0) { diceCount = 1; }
                        if (sideCount == 0) { sideCount = 6; }
                        if (adjust == 0) { adjust = 0; }
                        break;
                    }
                    if (arrg.StartsWith("D"))
                    {
                        if (arrg.Length == 1)
                        {
                            diceCount = 1;
                            sideCount = 6;
                            adjust = 0;
                            break;
                        }
                        else
                        {
                            diceCount = 1;
                            arrg = arrg.Substring(1, arrg.Length - 1);
                        }
                    }
                    else
                    {
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
                            sideCount = 6;
                            adjust = 0;
                            break;
                        }
                        else
                        {
                            arrg = ary[1];
                        }
                    }
                    string[] ary2 = arrg.Split(parm2, StringSplitOptions.RemoveEmptyEntries);
                    if (ary2.Length == 1)
                    {
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
                                    adjust = 0 - adjust;
                                }
                            }
                        }
                        if (ary2.Length <= 0)
                        {
                            sideCount = 6;
                            adjust = 0;
                        }
                    }
                    break;
            }
        }

        /// <summary>
        /// wrapper function for Console.WriteLine
        /// </summary>
        /// <param name="msg">string to output</param>
        public static void Wl(string msg) => Console.WriteLine(msg);

        /// <summary>
        /// Wrapper function for Console.Write
        /// </summary>
        /// <param name="msg">string to output</param>
        public static void W(string msg) => Console.Write(msg);

        /// <summary>
        /// Wrapper function for Console.ReadLine
        /// </summary>
        /// <returns>string input</returns>
        public static string R() => Console.ReadLine();
    }
}
