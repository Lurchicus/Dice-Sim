# Dice-Sim v1.0.8.0
CLI Dice Simulator (nDsides+|-Adj)
A dice simulator that works by creating a set of individual dies and rolling each one individually and returning the sum of the results
plus any adjustment.

For example, "3D6+1" will create 3 six sided die objects, roll each one, combine the results and add the +1 adjustment.

We also support a 0/1 coin flip "1D1" and a two sided die that returns 1 or 2 "1D2".

The default die is six sided, so you can use short cuts like entereing "D" or "1" to get a "1D6" roll. You can also enter a single number
like "3" to generate a "3D6" roll. 

Finally, the program remembers what you entered last, so pressing the enter key will repeat the last roll entered.
