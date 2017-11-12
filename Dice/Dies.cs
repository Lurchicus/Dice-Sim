using System;
using System.Collections.Generic;

namespace Dice
{
    class dies 
    {
        private Int32 Quantity;
        private Int32 Adjustment;
        private Int32 Sides;
        public static List<die> Cup = new List<die> { };

        public dies()
        {
            Quantity = 1;
            Adjustment = 0;
            Sides = 6;
            List<die> Cup = new List<die> { };
            die Stone = new die(Sides);
            Cup.Add(Stone);
        }

        public dies(Int32 HowMany, Int32 TotalAdjustment, Int32 NumSides)
        {
            Count = HowMany;
            Adjustment = TotalAdjustment;
            Sides = NumSides;
            for (Int32 Idx = 0; Idx < HowMany; Idx++)
            {
                die Stone = new die(Sides);
                Cup.Add(Stone);
            }
        }

        public Int32 throwDice()
        {
            Int32 Result = 0;
            for (Int32 Idx = 0; Idx < Quantity; Idx++)
            {
                Result += Cup[Idx].Throw();
            }
            return Result + Adjustments;
        }

        public Int32 Result(Int32 Idx)
        {
            return Cup[Idx].result;
        }

        public Int32 Adjustments
        {
            get { return Adjustment; }
            set { Adjustment = value; }
        }

        public Int32 Count
        {
            get { return Quantity; }
            set { Quantity = value; }
        }

        public void Empty()
        {
            Cup.Clear();
        }
    }
}
