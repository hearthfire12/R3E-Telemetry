namespace Infrastructure.Management
{
    using System.Collections.Generic;
    using System.IO.Abstractions;
    using System.Linq;
    using System.Windows;
    using Configuration;
    using Helpers;
    using Models;

    public class MapManager
    {
        public MapManager(IFileSystem fileSystem)
        {
            FileSystem = fileSystem;
            _points = new List<Point>();
        }


        private IList<Point> _points;


        protected readonly IFileSystem FileSystem;


        public bool IsRecording { get; set; }


        public void Clear()
        {
            _points.Clear();
            IsRecording = false;
        }

        private FileInfoBase GetMapFile(string trackName, string layoutName)
        {
            return FileSystem.FileInfo.FromFileName(
                FileSystem.Path.Combine(ConfigMgr.MapsRootFolder, $"{trackName} {layoutName}.map"));
        }

        public ICollection<Map> LoadAll()
        {
            return Loader.LoadAll<Map>(FileSystem.DirectoryInfo.FromDirectoryName(ConfigMgr.MapsRootFolder)).ToList();
        }

        public Map LoadMap(string trackName, string layoutName)
        {
            return Loader.Load<Map>(GetMapFile(trackName, layoutName));
        }

        public void Record(Point point)
        {
            _points.Add(point);
        }

        public void Save(string trackName, string layoutName)
        {
            if (!IsRecording)
            {
                return;
            }

            _points = _points.Distribute(200);

            var mapFile = GetMapFile(trackName, layoutName);

            var map = new Map
            {
                Name = mapFile.Name,
                Points = _points
            };

            Saver.Save(map, mapFile);
            Clear();
        }
    }
}