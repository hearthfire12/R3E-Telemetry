namespace TelemetryTests.Common
{
    using System.Collections.Generic;
    using System.Windows;
    using Helpers;
    using Infrastructure.Models;
    using NUnit.Framework;

    [TestFixture]
    public class SerializeHelperTests
    {
        [Test]
        public void Copy()
        {
            var points = new List<Point>
            {
                new Point(1, 1),
                new Point(2, 2),
                new Point(3, 3),
            };
            var obj = new Map {Name = "track", Points = points,};


            var result = obj.Copy<Map>();


            Assert.That(result, Is.Not.SameAs(obj));
            Assert.That(result.Name, Is.EqualTo("track"));
            Assert.That(result.Points, Is.EqualTo(points));
        }
    }
}
