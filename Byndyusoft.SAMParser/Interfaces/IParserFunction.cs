using System;
using System.Collections.Generic;
using System.Text;

namespace Byndyusoft.SAMParser
{
    /// <summary>
    /// Интерфейс пользовательской функции для расширения функционала класса Parser.     
    /// </summary>     
    public interface IParserFunction
    {
        /// <summary>
        /// Метод выполняет вычисление значения пользовательской функции.
        /// </summary>
        /// <returns>
        /// Результат вычисления.
        /// </returns>
        /// <param name="expression">Математическое выражение, передаваемое в пользовательскую функцию в качестве аргумента.</param>
        /// <param name="from">Позиция начала выражения аргумента.</param>
        /// <example>
        /// <code>
        /// public class AbsFunction : IParserFunction
        /// {
        ///     public double Evaluate(string expression, ref int from)
        ///     {
        ///         double arg = Parser.Calculate(expression, ref from, Parser.END_ARG);
        ///         return Math.Abs(arg);
        ///     }
        /// }
        /// </code>
        /// </example>
        /// <exception cref="System.ArgumentException">Возникает при неправильно заданном математическом выражении.</exception>
        public double Evaluate(string expression, ref int from);
    }
}
