namespace TelemetryTests.Common
{
    using Helpers;
    using NUnit.Framework;

    [TestFixture]
    public class MathHelperTests
    {
        [TestCase(-7, -5, 5, -0.2)]
        [TestCase(-5, -5, 5, 0)]
        [TestCase(-3, -5, 5, 0.2)]
        [TestCase(-1, -5, 5, 0.4)]
        [TestCase(1, -5, 5, 0.6)]
        [TestCase(3, -5, 5, 0.8)]
        [TestCase(5, -5, 5, 1)]
        [TestCase(7, -5, 5, 1.2)]
        public void Normalize(double value, double min, double max, double expectedResult)
        {
            double result = value.Normalize(min, max);


            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}
