namespace Infrastructure.Models.Car
{
    using System;

    [Serializable]
    public struct Tires<T>
    {
        public T FrontLeft { get; set; }
        public T FrontRight { get; set; }
        public T RearLeft { get; set; }
        public T RearRight { get; set; }
    }

    [Serializable]
    public struct Tires
    {
        public string TireSubtype { get; set; }
        public Tires<float> TireWear { get; set; }
        public Tires<float> TireGrip { get; set; }
        public Tires<float> TireTemp { get; set; }
        public Tires<float> BrakeTemp { get; set; }
    }
}