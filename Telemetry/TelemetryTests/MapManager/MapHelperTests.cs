namespace TelemetryTests.MapManager
{
    using System.Collections.Generic;
    using System.Windows;
    using Helpers;
    using NUnit.Framework;

    [TestFixture]
    public class MapHelperTests
    {
        [Test]
        public void NormalizePoints()
        {
            var points = new List<Point>
            {
                new Point(-5, -5),
                new Point(-4, -4),
                new Point(-3, -3),
                new Point(-2, -2),
                new Point(-1, -1),
                new Point(0, 0),
                new Point(1, 1),
                new Point(2, 2),
                new Point(3, 3),
                new Point(4, 4),
                new Point(5, 5),
            };


            var result = points.NormalizePoints();


            Assert.AreNotSame(result, points);
            Assert.AreEqual(result.Count, 11);

            var point = result[0];
            Assert.AreEqual(point.X, 0);
            Assert.AreEqual(point.Y, 0);

            point = result[1];
            Assert.AreEqual(point.X, 1);
            Assert.AreEqual(point.Y, 1);

            point = result[2];
            Assert.AreEqual(point.X, 2);
            Assert.AreEqual(point.Y, 2);

            point = result[3];
            Assert.AreEqual(point.X, 3);
            Assert.AreEqual(point.Y, 3);

            point = result[4];
            Assert.AreEqual(point.X, 4);
            Assert.AreEqual(point.Y, 4);

            point = result[5];
            Assert.AreEqual(point.X, 5);
            Assert.AreEqual(point.Y, 5);

            point = result[6];
            Assert.AreEqual(point.X, 6);
            Assert.AreEqual(point.Y, 6);

            point = result[7];
            Assert.AreEqual(point.X, 7);
            Assert.AreEqual(point.Y, 7);

            point = result[8];
            Assert.AreEqual(point.X, 8);
            Assert.AreEqual(point.Y, 8);

            point = result[9];
            Assert.AreEqual(point.X, 9);
            Assert.AreEqual(point.Y, 9);

            point = result[10];
            Assert.AreEqual(point.X, 10);
            Assert.AreEqual(point.Y, 10);
        }

        [Test]
        public void DistributePoints()
        {
            var expected = new List<Point>
            {
                new Point(-5, -5),
                new Point(-4, -4),
                new Point(-3, -3),
                new Point(-2, -2),
                new Point(-1, -1),
                new Point(0, 0),
                new Point(1, 1),
                new Point(2, 2),
                new Point(3, 3),
                new Point(4, 4),
                new Point(5, 5),
            };


            var result = expected.Distribute(4);


            Assert.AreNotSame(result, expected);
            Assert.AreEqual(4, result.Count);

            var actual = result[0];
            Assert.AreEqual(expected[0], actual);

            actual = result[1];
            Assert.AreEqual(expected[3], actual);

            actual = result[2];
            Assert.AreEqual(expected[6], actual);

            actual = result[3];
            Assert.AreEqual(expected[10], actual);
        }
    }
}