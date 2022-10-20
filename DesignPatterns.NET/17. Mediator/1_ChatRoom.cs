using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DesignPatterns.NET._17._Mediator._1_ChatRoom;

namespace DesignPatterns.NET._17._Mediator
{
    internal class _1_ChatRoom
    {
        public class Person
        {
            public string Name;
            public CharRoom Room;
            public List<string> chatLogs = new List<string>();

            public Person(string name)
            {
                Name = name;
            }

            public void Say(string message)
            {
                Room.Broadcast(Name, message);
            }

            public void PrivateMessage(string who, string message)
            {
                Room.Message(Name, who, message);
            }

            public void Receive(string sender, string message)
            {
                string s = $"{sender}: '{message}'";
                chatLogs.Add(s);
                Console.WriteLine($"[{Name}'s chat session] {s}");
            }
        }
        public class CharRoom
        {
            private List<Person> people = new List<Person>();

            public void Join(Person person)
            {
                string joinMsg = $"{person.Name} joins the chat";
                Broadcast("room", joinMsg);
                person.Room = this;
                people.Add(person);
            }

            public void Broadcast(string source, string message)
            {
                foreach (var person in people)
                {
                    if (person.Name != source)
                    {
                        person.Receive(source, message);
                    }
                }
            }

            public void Message(string source, string destinaction, string message)
            {
                people.FirstOrDefault(
                    x => x.Name == destinaction)?.Receive(source, message);
            }
        }

        public static void Drive()
        {
            var room = new CharRoom();

            var john = new Person("John");
            var jane = new Person("Jane");

            room.Join(john);
            room.Join(jane);

            john.Say("hi");
            jane.Say("oh, hey john");

            var simon = new Person("Simon");
            room.Join(simon);
            simon.Say("hi everyone!");

            jane.PrivateMessage("Simon", "glad you could join us!");


        }
    }
}
