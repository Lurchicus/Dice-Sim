using System;

namespace Dice
{
    class die
    {
        private Int32 diceSides;
        private Int32 diceResult;
        //public static Random chance = new Random((Int32)DateTime.Today.Ticks);
        public static Random chance = new Random();

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
            //Random chance = new Random();
            if (sides == 1) { sides = 2; }
            if (sides == 2)
            {
                //d1 or d2 is a 0 or 1 coing toss
                result = chance.Next(0, sides);
            }
            else
            {
                result = chance.Next(1, sides + 1);
            }
            diceResult = result;
            return result;
        }

        public Int32 sides
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
