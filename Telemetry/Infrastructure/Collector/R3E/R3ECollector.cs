namespace Infrastructure.Collector.R3E
{
    using System;
    using System.IO;
    using System.IO.MemoryMappedFiles;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Windows;
    using Configuration;
    using Infrastructure.Collector.R3E.Data;
    using Logging;
    using Models;
    using Models.Car;
    using R3E;

    public class R3ECollector : ICollector
    {
        private Shared _data;
        private MemoryMappedFile _file;

        
        public bool TryCollect(out RequiredModel collected)
        {
            collected = null;

            if (!SupportedGames.IsR3ERunning)
            {
                return false;
            }

            try
            {
                _file = MemoryMappedFile.OpenExisting(Constant.SharedMemoryName);

                var view = _file.CreateViewStream();
                var stream = new BinaryReader(view);
                var buffer = stream.ReadBytes(Marshal.SizeOf(typeof(Shared)));
                var handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                _data = (Shared) Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(Shared));
                handle.Free();

                var sessionType = (Constant.Session) Enum.Parse(typeof(Constant.Session), _data.SessionType.ToString());

                if (sessionType == Constant.Session.Unavailable)
                {
                    return true;
                }

                collected = new RequiredModel
                {
                    // Availability
                    InMenus = _data.GameInMenus == 1,

                    // Events
                    Type = sessionType == Constant.Session.Unavailable
                        ? null
                        : sessionType.ToString(),
                    CarPosition = new Point(_data.Player.Position.X, _data.Player.Position.Z),
                    LapIsValid = _data.CurrentLapValid == 1,
                    LapNumber = _data.CompletedLaps,
                    SectorId = _data.TrackSector,
                    InPitlane = _data.InPitlane == 1,

                    // Map
                    TrackName = GetNameFromBytes(_data.TrackName),
                    LayoutName = GetNameFromBytes(_data.LayoutName),


                    // DashBoard
                    Tires = GetTires(),
                    Gear = _data.Gear,
                    MaxGear = _data.NumGears,
                    Rpm = Utilities.RpsToRpm(_data.EngineRps),
                    MaxRpm = Utilities.RpsToRpm(_data.MaxEngineRps),
                    Brakes = _data.BrakeRaw,
                    Throttle = _data.ThrottleRaw,
                    MaxFuel = _data.FuelCapacity,
                    Fuel = _data.FuelLeft,
                    PlayerPosition = _data.Position,
                    MaxPlayers = _data.NumCars,
                    MaxLaps = _data.NumberOfLaps,
                    BestLapLeader = _data.LapTimeBestLeader,
                    BestLapSelf = _data.LapTimeBestSelf,
                    LapDelta = _data.TimeDeltaBestSelf,
                };

                return true;
            }
            catch (Exception ex)
            {
                Logger.Fatal("Exception occured while collecting.", ex);
                return false;
            }
        }

        public void Dispose()
        {
            _file.Dispose();
        }

        private string GetNameFromBytes(byte[] name)
        {
            return Encoding.ASCII.GetString(name).Trim('\0');
        }


        //private Session GetSession()
        //{
        //    if (!SessionAvailable)
        //    {
        //        return null;
        //    }

        //    Enum.TryParse(((Constant.SessionPhase) _data.SessionPhase).ToString(), out SessionPhase phase);
        //    Enum.TryParse(((Constant.Session) _data.SessionType).ToString(), out SessionType type);


        //    var player = new PlayerInfo
        //    {
        //        Name = GetNameFromBytes(_data.PlayerName),
        //    };
        //    var lap = GetLap();

        //    return new Session
        //    {
        //        Lap = lap,
        //        Player = player,
        //        Phase = phase,
        //        Type = type,
        //        TotalLaps = _data.NumberOfLaps,
        //    };
        //}

        //public LapDelta GetLapDelta()
        //{
        //    if (!LapAvailable)
        //    {
        //        return null;
        //    }

        //    Enum.TryParse(((Constant.Control) _data.ControlType).ToString(), out Constant.Control control);

        //    var sector = GetSector();
        //    var car = GetCar();

        //    return new LapDelta
        //    {
        //        Sector = sector,
        //        Car = car,
        //    };
        //}

        //private CarInfo GetCar()
        //{
        //    var tires = GetTires();

        //    return new CarInfo
        //    {
        //        Throttle = _data.Throttle,
        //        Brake = _data.Brake,
        //        SteerInput = _data.SteerInputRaw,
        //        MaxGear = _data.NumGears,
        //        Gear = _data.Gear,
        //        MaxRpm = Utilities.RpsToRpm(_data.MaxEngineRps),
        //        Rpm = Utilities.RpsToRpm(_data.EngineRps),
        //        Speed = Utilities.MpsToKph(_data.CarSpeed),
        //        TurboPressure = _data.TurboPressure,
        //        Position = new Point(
        //            _data.Player.Position.X,
        //            _data.Player.Position.Z),
        //        Tires = tires,
        //    };
        //}

        //private Sector GetSector()
        //{
        //    return new Sector
        //    {
        //        Id = _data.TrackSector,
        //    };
        //}

        //public Lap GetLap()
        //{
        //    if (!LapAvailable)
        //    {
        //        return null;
        //    }

        //    string trackName = GetNameFromBytes(_data.TrackName);
        //    string layoutName = GetNameFromBytes(_data.LayoutName);

        //    return new Lap
        //    {

        //        BestSelf = TimeSpan.FromTicks((long) _data.LapTimeBestSelf),
        //        CurrentSelf = TimeSpan.FromTicks((long) _data.LapTimeCurrentSelf),
        //        Invalidated = _data.CurrentLapValid != 1,
        //        LapNumber = _data.CompletedLaps,
        //    };
        //}

        private Tires GetTires()
        {
            return new Tires
            {
                BrakeTemp = new Tires<float>
                {
                    FrontLeft = _data.BrakeTemp.FrontLeft.CurrentTemp,
                    FrontRight = _data.BrakeTemp.FrontRight.CurrentTemp,
                    RearLeft = _data.BrakeTemp.RearLeft.CurrentTemp,
                    RearRight = _data.BrakeTemp.RearRight.CurrentTemp,
                },
                TireGrip = new Tires<float>
                {
                    FrontLeft = _data.TireGrip.FrontLeft,
                    FrontRight = _data.TireGrip.FrontRight,
                    RearLeft = _data.TireGrip.RearLeft,
                    RearRight = _data.TireGrip.RearRight,
                },
                TireTemp = new Tires<float>
                {
                    FrontLeft = _data.TireTemp.FrontLeft.CurrentTemp.Center,
                    FrontRight = _data.TireTemp.FrontRight.CurrentTemp.Center,
                    RearLeft = _data.TireTemp.RearLeft.CurrentTemp.Center,
                    RearRight = _data.TireTemp.RearRight.CurrentTemp.Center,
                },
                TireWear = new Tires<float>
                {
                    FrontLeft = _data.TireWear.FrontLeft,
                    FrontRight = _data.TireWear.FrontRight,
                    RearLeft = _data.TireWear.RearLeft,
                    RearRight = _data.TireWear.RearRight,
                },
            };
        }
    }
}