using KnockKnock.Readify.Services;
using Microsoft.ApplicationInsights;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace KnockKnock.Readify
{
    /// <inheritdoc />
    /// <summary>
    /// Represent the Red Pill service.
    /// </summary>
    [ServiceBehavior(Namespace = "http://KnockKnock.readify.net", IncludeExceptionDetailInFaults = true)]
    public class RedPill : IRedPill
    {
        // The Readify token associated with the nasr2ldin@gmail.com email.
        protected Guid Token = new Guid("79a2780b-a9cf-459d-b43e-5d7eb9fcae13");

        // Application Insights to send telemetry to the Azure Application Insights service.
        protected TelemetryClient Telemetry = new TelemetryClient();

        /// <summary>
        /// Whats the is your token.
        /// </summary>
        /// <returns>The Readify token.</returns>
        public Guid WhatIsYourToken()
        {
            var properties = new Dictionary<string, string> { { "Token", Token.ToString() } };
            Telemetry.TrackEvent("WhatIsYourToken", properties);

            return Token;
        }

        /// <summary>
        /// Generate the Fibonacci Number.
        /// </summary>
        /// <param name="number">Index in the sequence.</param>
        /// <returns>The number at n position in the Fibonacci sequence.</returns>
        public long FibonacciNumber(long number)
        {
            var properties = new Dictionary<string, string> { { "Argument 'n'", number.ToString() } };
            Telemetry.TrackEvent("FibonacciNumber", properties);

            long result = 0;

            try
            {
                result = new FibonacciNumberService().Calculate(number);
            }
            catch (ArgumentOutOfRangeException)
            {
                // The ArgumentOutOfRangeException is expected, therefore re-throw it further.
                throw;
            }
            catch (Exception exception)
            {
                Telemetry.TrackException(exception);
            }

            return result;
        }

        /// <summary>
        /// Reverses the words.
        /// </summary>
        /// <param name="words">The source string.</param>
        /// <returns>The source string where words are reversed.</returns>
        public string ReverseWords(string words)
        {
            var properties = new Dictionary<string, string> { { "Argument 's'", $"'{words ?? "null"}'" } };
            Telemetry.TrackEvent("ReverseWords", properties);

            string result = string.Empty;

            try
            {
                result = new StringReverseService().ReverseWords(words);
            }
            catch (ArgumentNullException)
            {
                // The ArgumentNullException is expected, therefore re-throw it further.
                throw;
            }
            catch (Exception exception)
            {
                Telemetry.TrackException(exception);
            }

            return result;
        }

        /// <summary>
        /// Classify the type of a triangle.
        /// </summary>
        /// <param name="a">Length of side 'a'.</param>
        /// <param name="b">Length of side 'b'.</param>
        /// <param name="c">Length of side 'c'.</param>
        /// <returns>The <see cref="TriangleType"/> type.</returns>
        public TriangleType WhatShapeIsThis(int a, int b, int c)
        {
            var properties = new Dictionary<string, string> { { "Argument 'a'", a.ToString() }, { "Argument 'b'", b.ToString() }, { "Argument 'c'", c.ToString() } };
            Telemetry.TrackEvent("WhatShapeIsThis", properties);

            TriangleType result = TriangleType.Error;

            try
            {
                result = new ShapeService().ClassifyTriangleType(a, b, c);
            }
            catch (Exception exception)
            {
                Telemetry.TrackException(exception);
            }

            return result;
        }
    }
}
