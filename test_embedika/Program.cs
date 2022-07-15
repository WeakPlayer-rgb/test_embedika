using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft;
using Newtonsoft.Json;

namespace test_embedika
{
    class Program
    {
        static void Main(string[] args)
        {
            AddCar(JsonConvert.SerializeObject(new Car("AAA","Megane","Red",2005)));
            AddCar(JsonConvert.SerializeObject(new Car("AAB","Renault","Blur",2006)));
            var str = Console.ReadLine();
            while (str != "Exit")
            {
                if (str == null) continue;
                if (str.StartsWith("Print database"))
                    GetCars(str.Skip("Print database".Length + 1).ParseToString());
                else if (str.StartsWith("Add car"))
                    AddCar(str.Skip("Add car".Length + 1).ParseToString());
                else if (str.StartsWith("Delete car"))
                    DeleteCar(int.Parse(str.Skip("Delete Car".Length + 1).ParseToString()));
                else if (str.StartsWith("Print statistic")) MakeStatistic();
                str = Console.ReadLine();
            }
        }

        private static void GetCars(string filter)
        {
            var example = (Car)JsonConvert.DeserializeObject(filter,typeof(Car));
            var func = new Func<Car, bool>(x =>
            {
                if (example == null) return true;
                return (example.Color == null || x.Color.Equals(example.Color)) &&
                       (example.Stamp == null || x.Stamp.Equals(example.Stamp)) &&
                       (example.CarNumber == null || x.CarNumber.Equals(example.CarNumber)) &&
                       (example.YearOfRelease == 0 || x.YearOfRelease == example.YearOfRelease);
            });
            var builder = new StringBuilder();
            foreach (var car in Database.GetCars(func))
            {
                builder.Append(car);
                builder.Append('\n');
            }
            if(builder.Length == 0) Out("There is nothing is database");
            Out(builder.ToString());
        }

        private static void AddCar(string jsonCar)
        {
            var convertedCar = JsonConvert.DeserializeObject(jsonCar, typeof(Car));
            if (convertedCar == null) throw new SerializationException();
            var car = (Car)convertedCar;
            Database.AddCar(car);
        }

        private static void DeleteCar(int id) => Database.DeleteCar(id);

        private static void MakeStatistic() => Out(Database.MakeStatistic());

        private static void Out(string arg) => Console.WriteLine(arg);
    }
}