namespace Infrastructure.Models
{
    using System;
    using System.Windows;
    using Car;

    [Serializable]
    public class RequiredModel
    {
        // Availability
        public bool InMenus { get; set; }
        public bool InPitlane { get; set; }


        // Events
        public string Type { get; set; }
        public Point CarPosition { get; set; }
        public int LapNumber { get; set; }
        public bool LapIsValid { get; set; }
        public int SectorId { get; set; }

        // Map
        public string TrackName { get; set; }
        public string LayoutName { get; set; }
        
        
        // Dashboard
        public Tires Tires { get; set; }
        public int Gear { get; set; }
        public int MaxGear { get; set; }
        public float Rpm { get; set; }
        public float MaxRpm { get; set; }
        public float Brakes { get; set; }
        public float Throttle { get; set; }
        public float MaxFuel { get; set; }
        public float Fuel { get; set; }
        public int PlayerPosition { get; set; }
        public int MaxLaps { get; set; }
        public int MaxPlayers { get; set; }
        public float BestLapSelf { get; set; }
        public float BestLapLeader { get; set; }
        public float LapDelta { get; set; }
    }
}
