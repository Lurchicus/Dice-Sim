using System;

namespace Dice
{
    /// <summary>
    /// Object describes a single die with "sides" sides or faces (1 or
    /// 2 sides indicates a coin which can return 0 or 1). By creating 
    /// individual die objects a true simulation of throwing one or 
    /// more dies can be accomplished.
    /// </summary>
    class Die
    {
        private Int32 diceSides;
        private Int32 diceResult;
        
        public static Random chance = new Random();

        /// <summary>
        /// Default constructor (0 sides)
        /// </summary>
        public Die() => diceSides = 0;

        /// <summary>
        /// Overload constructor creates a die of "sides" sides
        /// </summary>
        /// <param name="sides">Int32 sides on die</param>
        public Die(Int32 sides) => diceSides = sides;

        /// <summary>
        /// Simulate throwing a die or coin
        /// </summary>
        /// <returns>Int32 result of die throw</returns>
        public int GetThrow()
        {
            //Random chance = new Random();
            if (Sides == 1) { Sides = 2; }
            if (Sides == 2)
            {
                //d1 or d2 is a 0 or 1 coin toss
                Result = chance.Next(0, Sides);
            }
            else
            {
                Result = chance.Next(1, Sides + 1);
            }
            diceResult = Result;
            return Result;
        }

        /// <summary>
        /// Sides on the die 
        /// </summary>
        public Int32 Sides
        {
            get { return diceSides; }
            set { diceSides = value; }
        }

        /// <summary>
        /// Result of dice throw
        /// </summary>
        public Int32 Result
        {
            get { return diceResult; }
            set { diceResult = value; }
        }
    }
}
