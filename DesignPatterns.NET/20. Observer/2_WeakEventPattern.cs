using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DesignPatterns.NET._20._Observer
{
    internal class _2_WeakEventPattern
    {
        public class Button
        {
            public event EventHandler Clicked;
            public void Fire()
            {
                Clicked?.Invoke(this, EventArgs.Empty);
            }
        }

        public class Window
        {
            public Window(Button button)
            {
                button.Clicked += ButtonClicked;
                //WeakEventManager<Button, EventArgs>
                  //.AddHandler(button, "Clicked", ButtonClicked);
            }

            public void ButtonClicked(object sender, EventArgs eventArgs)
            {
                Console.WriteLine("Button clicked (window handler)");
            }
            ~Window()
            {
                Console.WriteLine("Window finalized");
            }
        }

        public static void Drive()
        {
            var btn = new Button();
            var window = new Window(btn);
            var windowRef = new WeakReference(window);
            btn.Fire();

            Console.WriteLine("Setting window to null");
            window = null;

            ForceCG();
            Console.WriteLine($"Is the window alive after GC? {windowRef.IsAlive}");
        }

        private static void ForceCG()
        {
            Console.WriteLine("Starting GC");
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            Console.WriteLine("GC is done!");
        }
    }
}
