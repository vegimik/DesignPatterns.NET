using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.NET._12._Proxy
{
    internal class _1_ProtectionProxy
    {
        public interface ICar
        {
            void NotAllowedToDrive()
            {
                Console.WriteLine("Not allowed to drive");
            }
            void Drive();
            void Break();
        }

        public class Car : ICar
        {
            public void Break()
            {
                Console.WriteLine("Car is being driven");
            }

            public void Drive()
            {
                Console.WriteLine("Car is being breaken");
            }
        }

        public class Driver
        {
            public int Age { get; set; }
            public Driver()
            {

            }
            public Driver(int age)
            {
                Age = age;
            }
        }

        public class CarProxy : ICar
        {
            private Driver driver;
            private ICar car = new Car();

            public CarProxy(Driver driver)
            {
                this.driver = driver;
            }

            public void Break()
            {
                throw new NotImplementedException();
            }

            public void Drive()
            {
                if (driver.Age >= 16)
                    car.Drive();
                else
                    car.NotAllowedToDrive();
            }
        }

        public static void Drive()
        {
            ICar car = new CarProxy(new Driver(18));

        }

    }
}
