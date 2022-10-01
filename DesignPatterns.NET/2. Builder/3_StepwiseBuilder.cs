using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.NET.Builder
{
    internal class StepwiseBuilder
    {
        public enum CarType
        {
            Sedan,
            Crossover
        }

        public class Car
        {
            public CarType Type;
            public int WheelSize { get; set; }

        }

        public interface ISpecifyCarType
        {
            ISpecifyWheenSize OfType(CarType type);
        }

        public interface ISpecifyWheenSize
        {
            IBuildCar WithWheels(int size);

        }
        public interface IBuildCar
        {
            public Car Build();
        }

        public class CarBuilder
        {
            private class Impl : ISpecifyCarType, ISpecifyWheenSize, IBuildCar
            {
                public Car car { get; set; } = new Car();


                public ISpecifyWheenSize OfType(CarType type)
                {
                    car.Type = type;
                    return this;
                }

                public IBuildCar WithWheels(int size)
                {
                    switch (car.Type)
                    {
                        case CarType.Sedan when size < 17 || size > 20:
                        case CarType.Crossover when size < 15 || size > 17:
                            throw new ArgumentException($"Wrong size of wheel for {car.Type}.");
                        default:
                            break;
                    }
                    car.WheelSize = size;
                    return this;
                }
                public Car Build()
                {
                    return car;
                }
            }
            public static ISpecifyCarType Create()
            {
                return new Impl();

            }
        }


        static void Drive()
        {
            var car = CarBuilder.Create()
              .OfType(CarType.Crossover)
              .WithWheels(18)
              .Build();
            Console.WriteLine(car);



        }
    }
}
