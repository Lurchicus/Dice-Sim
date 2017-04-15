using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dice
{
    //ToDo: Need a parser for dice notation
    //
    //  [quantity (optional: default="1")]
    //  d
    //  [sides (optional: default=6)]
    //  [
    //      [operator (default="+")]
    //      [adjustment (default="0")] 
    //      (optional)
    //  ]
    class Toss
    {
        static void Main(string[] args)
        {
            Int32 Tot = 0;
            //die D1 = new die(20, 5);
            //die D2 = new die(20, 0);
            //die D3 = new die(20, -5);
            //Tot += D1.Throw();
            //Tot += D2.Throw();
            //Tot += D3.Throw();
            dies dice = new dies(3, 5, 20);
            Tot += dice.throwDice();
            Console.WriteLine("3d20+5: "+ Tot);
            //Console.WriteLine("1d20  : " + D2.result);
            //Console.WriteLine("1d20-5: " + D3.result);
            Console.Write("Total: " + Tot);
            Console.ReadKey();
        }
    }

    public class dies
    {
        private Int32 Quantity;
        private Int32 Adjustment;
        private Int32 Sides;
        private List<die> Cup;

        public dies()
        {
            Quantity = 1;
            Adjustment = 0;
            Sides = 6;
            Cup.Add(new die(Sides));
        }

        public dies(Int32 HowMany, Int32 TotalAdjustment, Int32 NumSides)
        {
            Quantity = HowMany;
            Adjustment = TotalAdjustment;
            Sides = NumSides;
            for (Int32 Idx = 0; Idx < HowMany; Idx++)
            {
                Cup.Add(new die(Sides));
            }
        }

        public Int32 throwDice()
        {
            Int32 Result = 0;
            for (Int32 Idx=0; Idx<Quantity; Idx++)
            {
                Result += Cup[Idx].Throw();
            }
            return Result + Adjustments;
        }

        public Int32 Adjustments
        {
            get { return Adjustment;  }
            set { Adjustment = value; }
        }
    }

    public class die
    {
        private Int32 diceSides;
        private Int32 diceResult;

        public die()
        {
            diceSides = 0;
        }

        public die(Int32 sides)
        {
            diceSides = sides;
        }

        public Int32 Throw()
        {
            Random chance = new Random();
            result = chance.Next(1, sides);
            return result;
        } 

        public Int32  sides
        {
            get { return diceSides; }
            set { diceSides = value; }
        } 

        public Int32 result
        {
            get { return diceResult; }
            set { diceResult = value; }
        }
    }
}
