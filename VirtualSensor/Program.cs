using System;
using System.Threading;

namespace Sensors
{
    internal static class Program
    {
        private static readonly Random Random = new();

        private static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: TemperatureSimulator <SensorName>");
                return;
            }

            // TODO: needs to initialize name from file
            string sensorName = args[0];
            Console.WriteLine($"Simulation started for sensor : {sensorName}");


            using var context = new SensorContext();


            // simulation loop
            while (true)
            {
                double tempC = GenerateTemperature();
                Console.WriteLine($"{DateTime.Now:HH:mm:ss}  Temperature: {tempC:F1} °C");
                Thread.Sleep(1000);
                var reading = new Reading();
                reading.SensorName = sensorName;
                reading.DateTime = DateTime.Now;
                reading.Value = tempC;
                context.Readings.Add(reading);
                context.SaveChanges();

            }
        }

        private static double GenerateTemperature()
        {
            const double mean = 22.0;
            const double variation = 5.0;
            return mean + (Random.NextDouble() * 2 - 1) * variation;
        }
    }
}
