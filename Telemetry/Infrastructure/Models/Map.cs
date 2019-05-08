namespace Infrastructure.Models
{
    using System;
    using System.Collections.Generic;
    using System.Windows;

    [Serializable]
    public class Map
    {
        public Map()
        {
            Points = new List<Point>();
        }

        public Point this[int index]
        {
            get => Points[index];
            set => Points[index] = value;
        }

        public string Name { get; set; }
        public IList<Point> Points { get; set; }
    }
}
