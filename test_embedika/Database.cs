using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace test_embedika
{
    public static class Database
    {
        private static readonly List<Car> Cars = new();
        private static readonly List<int> Ids = new();
        private static int nextId;
        private static DateTime? firstAdd;
        private static DateTime? lastAdd;

        static Database() => nextId = 0;
        
        public static IEnumerable<Car> GetCars(Func<Car,bool> filter) => Cars.Where(filter);

        public static void AddCar(Car car)
        {
            firstAdd ??= DateTime.Now;
            lastAdd = DateTime.Now;
            if (Cars.Contains(car)) throw new AlreadyExistInDatabase();
            Cars.Add(car);
            Ids.Add(nextId++);
        }

        public static void DeleteCar(int id)
        {
            var index = Ids.IndexOf(id);
            if (index == -1) throw new NotExistInDatabase();
            var find = Cars[index];
            Cars.Remove(find);
            Ids.Remove(id);
        }

        public static string MakeStatistic()
        {
            var builder = new StringBuilder();
            builder.Append($"Car count:{Cars.Count}\n");
            if (firstAdd == null) return builder.ToString();
            builder.Append($"First add:{firstAdd}\n");
            builder.Append($"Last add{lastAdd}");
            return builder.ToString();
        }
    }
}