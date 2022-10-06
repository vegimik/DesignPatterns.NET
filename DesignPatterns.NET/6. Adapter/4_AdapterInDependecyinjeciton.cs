using Autofac;
using Autofac.Features.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.NET._6._Adapter
{
    internal class _4_AdapterInDependecyinjeciton
    {
        public interface ICommand
        {
            void Execute();

        }

        public class SaveCommand : ICommand
        {
            public void Execute()
            {
                Console.WriteLine("Saving a file");
            }
        }

        public class OpenCommand : ICommand
        {
            public void Execute()
            {
                Console.WriteLine("Opening a file");
            }
        }

        public class Button
        {
            public ICommand Command;
            private string name;

            public Button(ICommand command, string name)
            {
                Command = command ?? throw new ArgumentNullException(paramName: nameof(command));
                this.name = name;
            }

            public void Click()
            {
                Command.Execute();
            }

            public void PrintMe()
            {
                Console.WriteLine($"I am a button called {name}");
            }
        }

        public class Editor
        {
            public IEnumerable<Button> buttons;

            public Editor(IEnumerable<Button> buttons)
            {
                this.buttons = buttons ?? throw new ArgumentNullException(paramName: nameof(buttons));
            }

            public void ClickAll()
            {
                foreach (var button in buttons)
                {
                    button.Click();

                }
            }
        }


        static void Drive()
        {
            var cb = new ContainerBuilder();
            cb.RegisterType<SaveCommand>().As<ICommand>()
                .WithMetadata("Name", "Save");
            cb.RegisterType<OpenCommand>().As<ICommand>().WithMetadata("Name", "Open");
            //cb.RegisterType<Button>();
            //cb.RegisterAdapter<ICommand, Button>(cmd => new Button(cmd));
            cb.RegisterAdapter<Meta<ICommand>, Button>(cmd => new Button(cmd.Value, (string)cmd.Metadata["Name"]));
            cb.RegisterType<Editor>();

            using (var container = cb.Build())
            {
                var editor = container.Resolve<Editor>();
                //editor.ClickAll();

                foreach (var button in editor.buttons)
                {
                    button.PrintMe();

                }

            }

        }


    }
}
