using System;
using System.Threading.Tasks;

namespace DesignPatterns.NET.Factories
{
    public class AsyncFactoryMethod
    {
        public class Foo
        {
            public Foo()
            {
            }

            public async Task<Foo> InitAsync()
            {
                await Task.Delay(1000);
                return this;
            }
            public static async Task<Foo> CreateAsync()
            {
                var foo = new Foo();
                return await foo.InitAsync();
            }
        }

        static async void Drive()
        {
            //var foo = new Foo();
            //await foo.InitAsync();

            var x = await Foo.CreateAsync();


        }
    }

}
