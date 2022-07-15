using System;

namespace test_embedika
{
    [Serializable]
    public class Car
    {
        public string CarNumber { get; set; }
        public string Stamp { get; }
        public string Color { get; }
        public int YearOfRelease { get; }
        private readonly int hashCode;

        public Car(string number, string stamp, string color, int yearOfRelease)
        {
            CarNumber = number;
            Stamp = stamp;
            Color = color;
            YearOfRelease = yearOfRelease;
            hashCode = new ValueTuple<string, string, string, int>(CarNumber, Stamp, Color, YearOfRelease)
                .GetHashCode();
        }

        public override int GetHashCode() => hashCode;

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            return obj.GetType() == GetType() && Equals((Car)obj);
        }

        private bool Equals(Car obj)
        {
            if (hashCode != obj.hashCode) return false;
            return CarNumber.Equals(obj.CarNumber) && Stamp.Equals(obj.Stamp) && Color.Equals(obj.Color) &&
                   YearOfRelease.Equals(obj.YearOfRelease);
        }

        public override string ToString()
        {
            return $"Number:{CarNumber} Stamp:{Stamp} Color:{Color} Year of release:{YearOfRelease}";
        }
    }
}