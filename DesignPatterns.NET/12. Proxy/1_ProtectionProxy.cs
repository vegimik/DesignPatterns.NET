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
            // Fixed: Corrected the swapped method implementations
            public void Break()
            {
                Console.WriteLine("Car is being braked");
            }

            public void Drive()
            {
                Console.WriteLine("Car is being driven");
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

            // Fixed: Implement proper delegation with age validation for Break method
            public void Break()
            {
                if (driver.Age >= 16)
                    car.Break();
                else
                    car.NotAllowedToDrive();
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
            
            // Fixed: Added demonstration of both methods working correctly
            car.Drive();
            car.Break();
            
            // Test with underage driver
            ICar underageCar = new CarProxy(new Driver(15));
            underageCar.Drive();
            underageCar.Break();
        }

    }
}
