using System;
using System.Runtime.Caching;

namespace KnockKnock.Readify.Services
{
    /// <summary>
    /// Represents the Fibonacci Number service. See details at
    /// https://en.wikipedia.org/wiki/Fibonacci_number.  
    /// </summary>
    public class FibonacciNumberService
    {
        /// <summary>
        /// The threshold for Fibonacci number if using long (Int64) type for calculating the result.
        /// </summary>
        protected readonly long Threshold = 92;

        /// <summary>
        /// Calculates the negative Fibonacci sequence.
        /// </summary>
        /// <param name="number">The index in the sequence.</param>
        /// <returns>The Fibonacci number at specified position.</returns>
        public long Calculate(long number)
        {
            if (number > Threshold)
            {
                throw new ArgumentOutOfRangeException(nameof(number), $"Value cannot be greater than {Threshold}, since the result will cause a 64-bit integer overflow.");
            }

            if (number < -Threshold)
            {
                throw new ArgumentOutOfRangeException(nameof(number), $"Value cannot be less than {-Threshold}, since the result will cause a 64-bit integer overflow.");
            }

            var key = $"FibonacciNumber{number}";
            var cacheItem = MemoryCache.Default.GetCacheItem(key);

            long result;

            if (cacheItem != null)
            {
                result = (long)cacheItem.Value;
            }
            else
            {
                result = CalculateBinetFormula(number);
                MemoryCache.Default.Add(new CacheItem(key, result), new CacheItemPolicy() { SlidingExpiration = TimeSpan.FromHours(6) });
            }

            return result;
        }

        #region Protected Methods

        /// <summary>
        /// Calculates the Fibonacci number using the Binet's formula.
        /// http://www.wikihow.com/Calculate-the-Fibonacci-Sequence
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        protected long CalculateBinetFormula(long number)
        {
            var numerator = Math.Pow((1.0 + Math.Sqrt(5.0)), number) - Math.Pow((1.0 - Math.Sqrt(5.0)), number);
            var denominator = Math.Pow(2.0, number) * Math.Sqrt(5.0);
            var result = numerator / denominator;

            var roundedResult = Math.Round(result);

            return (long)roundedResult;
        }

        /// <summary>
        /// Calculates the Fibonacci number using a sequence of "negafibonacci" numbers.
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        protected long CalculateNega(long number)
        {
            long result = CalculateSequence(Math.Abs(number));

            // If n is negative and even, invert the sign.
            if (number < 0 && (number % 2 == 0))
            {
                result = -result;
            }

            return result;
        }

        /// <summary>
        /// Calculates the Fibonacci number using a sequence.
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        protected long CalculateSequence(long number)
        {
            if (number <= 1)
            {
                return number;
            }

            return CalculateSequence(number - 1) + CalculateSequence(number - 2);
        }

        #endregion
    }
}