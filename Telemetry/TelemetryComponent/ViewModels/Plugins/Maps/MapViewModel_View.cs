namespace TelemetryComponent.ViewModels.Plugins.Maps
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows;
    using Infrastructure.Models;
    using OxyPlot;
    using OxyPlot.Axes;
    using OxyPlot.Series;

    public partial class MapViewModel : INotifyPropertyChanged
    {
        private Map _loadedMap;


        public Map LoadedMap
        {
            get => _loadedMap;
            set
            {
                if (value != null && !value.Equals(_loadedMap))
                {
                    _loadedMap = value;
                    DrawMap();
                    OnPropertyChanged();
                }
            }
        }

        public PlotModel MapPlot { get; set; }

        private void DrawMap()
        {
            MapPlot.Axes.Clear();
            MapPlot.Series.Clear();

            var points = LoadedMap.Points;

            double maxX = points.Max(p => p.X),
                minX = points.Min(p => p.X);

            double maxY = points.Max(p => p.Y),
                minY = points.Min(p => p.Y);

            MapPlot.Axes.Add(PrepareAxis(minX, maxX, AxisPosition.Bottom));
            MapPlot.Axes.Add(PrepareAxis(minY, maxY, AxisPosition.Left));

            MapPlot.Series.Add(PrepareSeries(points, null));

            MapPlot.InvalidatePlot(true);
        }

        private LinearAxis PrepareAxis(double min, double max, AxisPosition position)
        {
            // step is 10% of max value
            double step = Math.Abs(10 * max / 100f);

            return new LinearAxis
            {
                Position = position,
                Minimum = min - step,
                Maximum = max + step,
                MinorStep = step,
                IsAxisVisible = false,
                IsZoomEnabled = false,
                IsPanEnabled = false,
            };
        }

        private Series PrepareSeries(IList<Point> points, string title)
        {
            var series = new LineSeries
            {
                Selectable = false,
                Smooth = false,
                StrokeThickness = 12,
                MarkerType = MarkerType.Circle,
                Color = OxyColor.Parse("#484F57"),
                Title = title,
            };

            foreach (var p in points)
            {
                series.Points.Add(new DataPoint(p.X, p.Y));
            }

            return series;
        }

        private void DrawCurrentPosition(RequiredModel model)
        {
            if (LoadedMap == null || model == null)
            {
                return;
            }

            var previousPosition = MapPlot.Series.SingleOrDefault(s => s.TrackerKey == MapTrackers.CurrentPosition);
            if(previousPosition != null)
            {
                MapPlot.Series.Remove(previousPosition);
            }

            var point = new DataPoint(
                model.CarPosition.X,
                model.CarPosition.Y);

            MapPlot.Series.Add(new LineSeries
            {
                TrackerKey = "CurrentPosition",
                Color = OxyColors.Red,
                MarkerFill = OxyColor.Parse("#CDCED2"),
                MarkerStroke = OxyColor.Parse("#EBB401"),
                MarkerType = MarkerType.Circle,
                StrokeThickness = 0,
                MarkerSize = 4,
                Points = {point},
            });

            MapPlot.InvalidatePlot(true);
        }
    }
}
