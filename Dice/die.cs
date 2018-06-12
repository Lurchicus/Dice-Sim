using System;

namespace Dice
{
    /// <summary>
    /// Object describes a single die with "sides" sides or faces (1 or
    /// 2 sides indicates a coin which can return 0|1 or 1|2). By creating 
    /// individual die objects a true simulation of throwing one or 
    /// more dies can be accomplished.
    /// If "sides" is undefined or invalid a default of 6 is used.
    /// 
    /// 1.0.12.0 - 3/17/2018 - Removed the ability to change the roll result 
    ///                        from outside the class.
    /// </summary>
    class Die
    {
        private Int32 diceSides;
        private Int32 diceResult;
        
        public static Random chance = new Random();

        /// <summary>
        /// Default constructor (6 sides)
        /// </summary>
        public Die() => diceSides = 6;
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
            //If 0 sides are selected, override to a standard
            //size sided die (better than rolling 0 all the time)
            if (Sides == 0)
            {
                diceSides = 6;
            }
            //A d1 is coin that can return 0 or 1
            //A d2 is a coin (or 2 sided die) that can return 1 or 2
            if (Sides == 1)
            {
                //d1 is a 0 or 1 coin toss
                diceResult = chance.Next(0, Sides + 1);
            }
            else
            {
                //D2 to D(whatever doesn't overflow an Int32)
                diceResult = chance.Next(1, Sides + 1);
            }
            return Result;
        }

        /// <summary>
        /// Sides on the die 
        /// </summary>
        public Int32 Sides
        {
            get { return diceSides; }
        }

        /// <summary>
        /// Result of dice throw
        /// </summary>
        public Int32 Result
        {
            get { return diceResult; }
        }
    }
}
