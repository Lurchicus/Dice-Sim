using System;
using System.Collections.Generic;

namespace Dice
{
    /// <summary>
    /// The dies class creates a collection of die objects. The dies are in a
    /// list object (imagine a dice cup). The Quantity is the number of dice,
    /// sides are the number of sides on the die and adjustment is a
    /// negative or positive value added to the sum of the rolls of the
    /// individual dies.
    /// </summary>
    internal class Dies
    {
        public static List<Die> Cup = new List<Die> { };

        public int Quantity1 { get; private set; }
        public int Adjustment1 { get; private set; }
        public int Sides1 { get; }

        /// <summary>
        /// Default constructor instantiates a single 6 sided die with no
        /// adjustment.
        /// </summary>
        public Dies()
        {
            Quantity1 = 1;
            Adjustment1 = 0;
            Sides1 = 6;
            List<Die> Cup = new List<Die> { };
            Die Stone = new Die(Sides1);
            Cup.Add(Stone);
        }

        /// <summary>
        /// Overload constructor instantiates "HowMany" dies with "NumSides"
        /// and a final adjustment of "TotalAdjustment"
        /// </summary>
        /// <param name="HowMany">Dies in list</param>
        /// <param name="TotalAdjustment">Adjustment to final result</param>
        /// <param name="NumSides">Number of sides on each die</param>
        public Dies(Int32 HowMany, Int32 TotalAdjustment, Int32 NumSides)
        {
            SetCount(HowMany);
            Adjustment1 = TotalAdjustment;
            Sides1 = NumSides;
            for (Int32 Idx = 0; Idx < HowMany; Idx++)
            {
                try
                {
                    Die Stone = new Die(Sides1);
                    if (Stone.Sides != Sides1)
                    {
                        //Make sure the Sides weren't overridden (such as
                        //someone declared a D0)
                        Sides1 = Stone.Sides;
                    }
                    Cup.Add(Stone);
                }
                catch (OutOfMemoryException e)
                {
                    Toss.Wl("Error! OutOfMemoryException: " + e.Message);
                    Cup.Clear();
                    throw new OutOfMemoryException();
                }
                catch (Exception e)
                {
                    Toss.Wl("Error! Undefined exception: " + e.Message);
                    Cup.Clear();
                }
            }
        }

        /// <summary>
        /// Throws each die in the "cup" and accumulates the results
        /// </summary>
        /// <returns>Int32 sum of the dies and any adjustment</returns>
        public Int32 ThrowDice()
        {
            Int32 Result = 0;
            for (Int32 Idx = 0; Idx < Cup.Count; Idx++)
            {
                Result += Cup[Idx].GetThrow();
            }
            return Result + GetAdjustments();
        }

        /// <summary>
        /// Pulls the result of a single die in the "cup"
        /// </summary>
        /// <param name="Idx">Int32 number of specific die</param>
        /// <returns>Int32 result of a given die</returns>
        public Int32 Result(Int32 Idx)
        {
            return Cup[Idx].Result;
        }

        /// <summary>
        /// Adjustment to final result
        /// </summary>
        public int GetAdjustments()
        {
            return Adjustment1;
        }

        /// <summary>
        /// Adjustment to final result
        /// </summary>
        public void SetAdjustments(int value)
        {
            Adjustment1 = value;
        }

        /// <summary>
        /// Number of dies
        /// </summary>
        public int GetCount()
        {
            return Quantity1;
        }

        /// <summary>
        /// Number of dies
        /// </summary>
        public void SetCount(int value)
        {
            Quantity1 = value;
        }

        /// <summary>
        /// Clears all of the dies in the dice "cup"
        /// </summary>
        public void Empty()
        {
            Cup.Clear();
        }
    }
}