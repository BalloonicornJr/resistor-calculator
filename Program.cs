using System;
using System.Threading;

namespace Resistors
{
    class Program
    {
        static void Main(string[] args)
        {
            bool runAgain = false;
            double carryOver = 0;
            int num = 0;
            double final = 0.0;
            Console.WriteLine("This program will add any number of resistors in series or in parallel.");
            Console.WriteLine("For series, input \"series\". For parallel, input \"parallel\". \nTo quit at any time, input \"exit\".");
            Begin:
            if (runAgain)
            {
                Console.WriteLine("Please input \"series\" or \"parallel\".");
            }
            String input = Console.ReadLine().ToLower();
            if (input == "series")
            {
                Console.WriteLine("You've selected a series.");
                if (!runAgain) Console.WriteLine("How many resistors are in series?");
                else Console.WriteLine("How many more resistors are in series? (Not counting the previous calculation)");
                sAgain:
                String simput = Console.ReadLine();
                if (simput.ToLower() == "exit")
                {
                    goto End;
                }
                bool stry = Int32.TryParse(simput, out num);
                if (!stry)
                {
                    Console.WriteLine("Invalid input. Please input an integer.");
                    goto sAgain;
                }
                if (stry && num <= 0)
                {
                    Console.WriteLine("Invalid input. Please input a positive nonzero integer. ");
                    goto sAgain;
                }
                //if (runAgain) num++;
                double[] sres = new double[num];
                // if (runAgain)
                // {
                //    sres[num-1] = carryOver;
                //  }
                Console.WriteLine("Please input the resistance of the first resistor.");
                for (int i = 0; i < num; i++)
                {
                    sBeginLoop:
                    if (i != 0)
                    {
                        Console.WriteLine("Please input the resistance of the next resistor.");
                    }
                    String slinput = Console.ReadLine();
                    if (slinput.ToLower() == "exit")
                    {
                        goto sfinal;
                    }
                    stry = Double.TryParse(slinput, out sres[i]);
                    if (!stry)
                    {
                        Console.WriteLine("Invalid input. Please input a number.");
                        goto sBeginLoop;
                    }
                    if (sres[i] < 0)
                    {
                        Console.WriteLine("Input cannot be negative. Please input a positive number.");
                        goto sBeginLoop;
                    }
                }
                sfinal:
                final = AddSeries(sres);
                if (runAgain)
                {
                    final += carryOver;
                }

            }
            else if (input == "parallel")
            {
                Console.WriteLine("You've selected parallel.");
                if (!runAgain) Console.WriteLine("How many resistors are in parallel?");
                else Console.WriteLine("How many more resistors are in series? (Not counting the previous calculation)");
                pAgain:
                String pinput = Console.ReadLine();
                if (pinput.ToLower() == "exit")
                {
                    goto End;
                }
                bool ptry = Int32.TryParse(pinput, out num); // to do: fail case
                if (!ptry)
                {
                    Console.WriteLine("Invalid input. Please input an integer.");
                    goto pAgain;
                }
                if (ptry && num <= 0)
                {
                    Console.WriteLine("Invalid input. Please input a positive nonzero integer. ");
                    goto pAgain;
                }
                if (runAgain) num++;
                double[] pres = new double[num];
                if (runAgain) num--;
                Console.WriteLine("Please input the resistance of the first resistor.");
                int pl = 0;
                for (pl = 0; pl < num; pl++)
                {
                    pBeginLoop:
                    if (pl != 0)
                    {
                        Console.WriteLine("Please input the resistance of the next resistor.");
                    }
                    String plinput = Console.ReadLine();
                    if (plinput.ToLower() == "exit")
                    {
                        //todo: create a new array of length only inputted numbers, transfer inputted numbers into that array, and calculate parallel with that array
                        double[] last = new double[pl];
                        ArrayStrip(pres, ref last, pl);
                        final = AddParallel(last);
                        goto Report;
                    }
                    ptry = Double.TryParse(plinput, out pres[pl]);
                    if (!ptry)
                    {
                        Console.WriteLine("Invalid input. Please input a number.");
                        goto pBeginLoop;
                    }
                    if (pres[pl] < 0)
                    {
                        Console.WriteLine("Input cannot be negative. Please input a positive number.");
                        goto pBeginLoop;
                    }
                    if (pres[pl] == 0)
                    {
                        Console.WriteLine("The equivalent resistance of resistors in parallel wherein one of the resistances is zero is 0 ohms.");
                        goto End;
                    }
                }
                if (runAgain) pres[num] = carryOver;
                final = AddParallel(pres);
            }
            else if (input == "exit")
            {
                goto End;
            }
            else
            {
                Console.WriteLine("Input not recognized. Please input again.");
                goto Begin;
            }
            Report:
            Console.WriteLine("The total resistance is " + final.ToString("0.##") + " ohms.");
            Thread.Sleep(1000);
            Console.WriteLine("Would you like to use this value in another calculation? (Y/N)");
            Retest:
            String input2 = Console.ReadLine();
            input2 = input2.ToLower();
            if (input2.Equals("yes") || input2.Equals("y"))
            {
                runAgain = true;
                carryOver = final;
                goto Begin;
            }
            else if (input2.Equals("no") || input2.Equals("n"))
            {
                runAgain = false;
            }
            else
            {
                Console.WriteLine("Invalid input. Please input \"Y\" or \"N\".");
                goto Retest;
            }
            End:
            Console.WriteLine("Press Enter to quit.");
            Console.ReadLine();
        }
        public static double AddParallel(double[] res)
        {
            double runTotal = 0.0;
            for (int i = 0; i < res.Length; i++)
            {
                runTotal += 1.0 / (res[i]);
            }
            return 1.0 / runTotal;
        }
        public static double AddSeries(double[] res)
        {
            double runTotal = 0.0;
            for (int i = 0; i < res.Length; i++)
            {
                runTotal += res[i];
            }
            return runTotal;
        }
        public static double[] ArrayStrip(double[] arrin, ref double[] arrout, int l)
        {
            for (int i = 0; i < l; i++)
            {
                arrout[i] = arrin[i];
            }
            return arrout;
        }
    }
}
