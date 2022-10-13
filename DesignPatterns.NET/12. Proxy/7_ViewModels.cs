using JetBrains.Annotations;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DesignPatterns.NET._12._Proxy
{
    internal class _7_ViewModels
    {
        // MVVM: (Model, View, ViewModel)
        public class Person
        {
            public string FirstName, LastName;


        }

        public class PersonViewModel
            : INotifyPropertyChanged
        {
            private readonly Person person;

            public PersonViewModel(Person person)
            {
                this.person = person;

            }

            public string FirstName
            {
                get => person.FirstName;
                set
                {
                    if (person.FirstName == value) return;
                    person.FirstName = value;
                    OnPropertyChanged(nameof(FullName));
                }
            }
            public string LastName
            {
                get => person.LastName;
                set
                {
                    if (person.LastName == value) return;
                    person.LastName = value;
                    OnPropertyChanged(nameof(LastName));
                }
            }

            public string FullName
            {
                get => $"{person.FirstName} {person.LastName}".Trim();
                set
                {
                    if (value == null)
                    {
                        FirstName = LastName = null;
                        return;
                    }
                    var items = value.Split(' ');
                    if (items.Length > 0)
                        FirstName = items[0]; if (items.Length > 1)
                        LastName = items[1];
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;

            [NotifyPropertyChangedInvocator]
            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
