using System;
using Byndyusoft.SAMParser;
using Byndyusoft.SAMParser.TrigFunctions;
using Byndyusoft.SAMParser.ElementaryFunctions;

namespace Byndyusoft.ConsoleCalculator
{
    class Program
    {
        static void calculate(string expr, double expected)
        {
            double result = Parser.Process(expr);

            string outcome = result == expected ? "OK" : "NOK " + expected.ToString();
            Console.WriteLine("{0} --> {1} ({2})", expr, result, outcome);
        }

        private static void ConfigureParser()
        {
            Parser.AddFunction("pi", new PiFunction());
            Parser.AddFunction("sin", new SinFunction());
            Parser.AddFunction("cos", new CosFunction());
            Parser.AddFunction("tg", new TangentFunction());
            Parser.AddFunction("abs", new AbsFunction());
            Parser.AddFunction("pow", new PowFunction());
            Parser.AddFunction("sqrt", new SqrtFunction());
            Parser.AddFunction("exp", new ExpFunction());
        }

        static void Main(string[] args)
        {
            ConfigureParser();

            calculate("1-2", 1 - 2);
            calculate("(1-(2))", (1 - (2)));
            calculate("3+2*6-1", 3 + 2 * 6 - 1);
            calculate("3-2*6-1", 3 - 2 * 6 - 1);
            calculate("1-2-3-(4-(5-(6-7)))", 1 - 2 - 3 - (4 - (5 - (6 - 7))));
            calculate("3-(5-6)-(2-(3-(1-2)))", 3 - (5 - 6) - (2 - (3 - (1 - 2))));
            calculate("3-(5-6)-(2-(3-(1+2)))+2-(-1+7)*(9-2)/((16-3)-3)+15/2*5", 3 - (5 - 6) - (2 - (3 - (1 + 2))) + 2 - (-1 + 7) * (9 - 2) / ((16.0 - 3) - 3.0) + 15 / 2.0 * 5);
            calculate("(-1+7)*(9-2)", (-1 + 7) * (9 - 2));
            calculate("((16-3)-3)+15/2*5", ((16 - 3) - 3) + 15 / 2.0 * 5);
            calculate("1+15/2*5", 1 + 15 / 2.0 * 5);
            calculate("3-2/6-1", 3 - 2 / 6.0 - 1);
            calculate("3*50-3*2^4*3", 3 * 50 - 3 * Math.Pow(2, 4) * 3);
            calculate("5-1/2^2-3", 5 - 1 / Math.Pow(2, 2) - 3);
            calculate("(((1/4/2-(8/2/3+5))))", (((1 / 4.0 / 2.0 - (8 / 2.0 / 3.0 + 5)))));

            calculate("sin(pi/2)", Math.Sin(Math.PI/2));
            calculate("cos(pi)", Math.Cos(Math.PI));
            calculate("tg(pi/4)", Math.Tan(Math.PI/4));
            calculate("2-3*sin(pi)", 2 - 3 * Math.Sin(Math.PI));

            calculate("abs(3*-50-2*3/4)/3*2", Math.Abs(3.0 * -50 - 2 * 3.0 / 4.0) / 3.0 * 2);
            calculate("pow(2/2+1,6/2)", Math.Pow(2, 3));
            calculate("1-(exp(10*7-sqrt((1+1)*20*10)))", 1 - (Math.Exp(10 * 7 - Math.Sqrt((1 + 1) * 20 * 10))));
        }
    }
}
