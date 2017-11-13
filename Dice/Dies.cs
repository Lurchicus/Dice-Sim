using System;
using System.Collections.Generic;

namespace Dice
{
    /// <summary>
    /// The dies class creates a collection of die objects. The dies are in a
    /// list object (imagine a dice cup). The Quantity is the number of dice
    /// the sides are the number of sides on the die and adjustment is a 
    /// value added to the sum of the rolls of the individual dies.
    /// </summary>
    class dies 
    {
        private Int32 Quantity;
        private Int32 Adjustment;
        private Int32 Sides;
        public static List<die> Cup = new List<die> { };

        /// <summary>
        /// Default constructor instanciates a single 6 sided die with no 
        /// adjustment.
        /// </summary>
        public dies()
        {
            Quantity = 1;
            Adjustment = 0;
            Sides = 6;
            List<die> Cup = new List<die> { };
            die Stone = new die(Sides);
            Cup.Add(Stone);
        }

        /// <summary>
        /// Overload constructor instantiates "HowMany" dies with "NumSides"
        /// and a final adjustment of "TotalAdjustment"
        /// </summary>
        /// <param name="HowMany">Dies in list</param>
        /// <param name="TotalAdjustment">Adjustment to final result</param>
        /// <param name="NumSides">Number of sides on each die</param>
        public dies(Int32 HowMany, Int32 TotalAdjustment, Int32 NumSides)
        {
            SetCount(HowMany);
            Adjustment = TotalAdjustment;
            Sides = NumSides;
            for (Int32 Idx = 0; Idx < HowMany; Idx++)
            {
                die Stone = new die(Sides);
                Cup.Add(Stone);
            }
        }

        /// <summary>
        /// Throws each die in the "cup" and accumulates the results
        /// </summary>
        /// <returns>Int32 sum of the dies and any adjustment</returns>
        public Int32 ThrowDice()
        {
            Int32 Result = 0;
            for (Int32 Idx = 0; Idx < Quantity; Idx++)
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
        public Int32 Result(Int32 Idx) => Cup[Idx].Result;

        /// <summary>
        /// Adjustment to final result
        /// </summary>
        public int GetAdjustments()
        { return Adjustment; }

        /// <summary>
        /// Adjustment to final result
        /// </summary>
        public void SetAdjustments(int value)
        { Adjustment = value; }

        /// <summary>
        /// Number of dies
        /// </summary>
        public int Count => Quantity;

        /// <summary>
        /// Number of dies
        /// </summary>
        public void SetCount(int value)
        { Quantity = value; }

        /// <summary>
        /// Clears all of the dies in the dice "cup"
        /// </summary>
        public void Empty() => Cup.Clear();
    }
}
