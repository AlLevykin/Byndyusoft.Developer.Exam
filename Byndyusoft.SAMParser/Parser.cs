using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleTo("Byndyusoft.SAMParser.Tests")]

namespace Byndyusoft.SAMParser
{
    public static class Parser
    {
        public const char START_ARG = '(';
        public const char END_ARG = ')';
        public const char END_LINE = '\n';

        private static Dictionary<char, MathOperation> _operations = new Dictionary<char, MathOperation>
        {
            ['^'] = new MathOperation { Priority = 4, Action = (o1, o2) => { return Math.Pow(o1, o2); } },
            ['*'] = new MathOperation { Priority = 3, Action = (o1, o2) => { return o1 * o2; } },
            ['/'] = new MathOperation
            {
                Priority = 3,
                Action = (o1, o2) =>
                {
                    if (o2 == 0) throw new ArgumentException("Division by zero");
                    return o1 / o2;
                }
            },
            ['+'] = new MathOperation { Priority = 2, Action = (o1, o2) => { return o1 + o2; } },
            ['-'] = new MathOperation { Priority = 2, Action = (o1, o2) => { return o1 - o2; } }
        };

        private static Dictionary<string, IParserFunction> _functions = new Dictionary<string, IParserFunction>();
        private static StrToDoubleFunction _strToDoubleFunction = new StrToDoubleFunction();
        private static IdentityFunction _identityFunction = new IdentityFunction();

        public static void AddFunction(string name, IParserFunction function)
        {
            _functions[name] = function;
        }

        public static double Process(string expression)
        {
            int from = 0;
            return Calculate(expression, ref from, END_LINE);
        }

        internal static bool ValidOperation(char ch)
        {
            return _operations.ContainsKey(ch);
        }

        internal static int GetPriority(char operation)
        {
            return _operations.ContainsKey(operation) ? _operations[operation].Priority : 0;
        }

        internal static bool CanMergeCells(Cell leftCell, Cell rightCell)
        {
            return GetPriority(leftCell.Operation) >= GetPriority(rightCell.Operation);
        }

        internal static bool StillCollecting(int stringLength, char currentChar, char to)
        {
            char stopCollecting = (to == END_ARG || to == END_LINE) ? END_ARG : to;
            return (stringLength == 0 && (currentChar == '-' || currentChar == END_ARG)) || !(ValidOperation(currentChar) || currentChar == START_ARG || currentChar == stopCollecting);
        }

        static char UpdateOperation(string item, ref int from, char ch, char to)
        {
            if (from >= item.Length || item[from] == END_ARG || item[from] == to)
            {
                return END_ARG;
            }

            int index = from;
            char res = ch;
            while (!ValidOperation(res) && index < item.Length)
            {
                res = item[index++];
            }

            from = ValidOperation(res) ? index
                                    : index > from ? index - 1
                                                   : from;
            return res;
        }

        static void MergeCells(Cell leftCell, Cell rightCell)
        {
            if (_operations.ContainsKey(leftCell.Operation)) leftCell.Value = _operations[leftCell.Operation].Action(leftCell.Value, rightCell.Value);
            leftCell.Operation = rightCell.Operation;
        }

        static double Merge(Cell current, ref int index, List<Cell> listToMerge,
                     bool mergeOneOnly = false)
        {
            while (index < listToMerge.Count)
            {
                Cell next = listToMerge[index++];

                while (!CanMergeCells(current, next))
                {
                    Merge(next, ref index, listToMerge, true);
                }
                MergeCells(current, next);
                if (mergeOneOnly)
                {
                    return current.Value;
                }
            }

            return current.Value;
        }

        public static double Calculate(string expression, ref int from, char to = END_LINE)
        {
            if (from >= expression.Length || expression[from] == to)
            {
                throw new ArgumentException("Loaded invalid data: " + expression);
            }

            List<Cell> listToMerge = new List<Cell>(16);
            StringBuilder item = new StringBuilder();

            do
            {
                char ch = expression[from++];
                if (StillCollecting(item.Length, ch, to))
                {
                    item.Append(ch);
                    if (from < expression.Length && expression[from] != to)
                    {
                        continue;
                    }
                }

                double value;

                if (item.Length == 0 && ch == Parser.START_ARG)
                {
                    value = _identityFunction.Evaluate(expression, ref from);
                }
                else if (_functions.ContainsKey(item.ToString()))
                {
                    IParserFunction func = _functions[item.ToString()];
                    value = func.Evaluate(expression, ref from);
                }
                else
                {
                    value = _strToDoubleFunction.Evaluate(item.ToString(), ref from);
                }

                char operation = ValidOperation(ch) ? ch
                                              : UpdateOperation(expression, ref from, ch, to);

                listToMerge.Add(new Cell(value, operation));
                item.Clear();

            } while (from < expression.Length && expression[from] != to);

            if (from < expression.Length &&
               (expression[from] == END_ARG || expression[from] == to))
            {
                from++;
            }

            Cell baseCell = listToMerge[0];
            int index = 1;

            return Merge(baseCell, ref index, listToMerge);
        }
    }
}
