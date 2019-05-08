namespace Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;

    public static class MapHelper
    {
        public static IList<Point> NormalizePoints(this IList<Point> points)
        {
            var normalizedPoints = new List<Point>(points.Count);

            double maxX = int.MinValue;
            double maxY = int.MinValue;

            double minX = int.MaxValue;
            double minY = int.MaxValue;

            foreach (var p in points)
            {
                if (p.X > maxX)
                {
                    maxX = p.X;
                }

                if (p.X < minX)
                {
                    minX = p.X;
                }

                if (p.Y > maxY)
                {
                    maxY = p.Y;
                }

                if (p.Y < minY)
                {
                    minY = p.Y;
                }
            }

            double distanceX = Math.Abs(maxX - minX);
            double distanceY = Math.Abs(maxY - minY);

            foreach (var p in points)
            {
                var point = new Point(
                    distanceX * p.X.Normalize(minX, maxX),
                    distanceY * p.Y.Normalize(minY, maxY)
                );

                normalizedPoints.Add(point);
            }

            return normalizedPoints;
        }

        public static IList<T> Distribute<T>(this IList<T> collection, int maxPoints)
        {
            if (collection.Count < maxPoints)
            {
                return collection;
            }

            var distributedPoints = new List<T>(maxPoints);

            int step = (int) Math.Ceiling(collection.Count / (double) maxPoints);

            int i;
            for (i = 0; i < collection.Count; i += step)
            {
                distributedPoints.Add(collection[i]);
            }

            if (i - step < collection.Count)
            {
                distributedPoints[distributedPoints.Count - 1] = collection[collection.Count - 1];
            }

            return distributedPoints;
        }
    }
}
