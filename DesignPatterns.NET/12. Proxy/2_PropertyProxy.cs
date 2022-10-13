using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.NET._12._Proxy
{
    public class Property<T> where T : new()
    {
        public T value { get; set; }
        public T Value
        {
            get
            {
                return value;
            }
            set
            {
                if (Equals(value, this.value)) return;
                Console.WriteLine($"Assigning vlaue to {value}");
                this.value = value;
            }
        }
        public Property() : this(default(T))   //:this(Activator.CreateInstance<T>())
        {

        }
        public Property(T value)
        {
            Value = value;
        }

        public static implicit operator T(Property<T> property)
        {
            return property.Value;
        }

        public static implicit operator Property<T>(T value)
        {
            return new Property<T>(value);
        }

        public bool Equals(Property<T> other)
        {
            if (ReferenceEquals(other, null)) return false;
            if (ReferenceEquals(other, this)) return true;
            if (other.GetType() != this.GetType())
                return false;
            return EqualityComparer<T>.Default.Equals(value, other.value);
        }
        public override bool Equals(object obj)
        {
            var other = (Property<T>)obj;
            if (ReferenceEquals(other, null)) return false;
            if (ReferenceEquals(other, this)) return true;
            if (other.GetType() != this.GetType())
                return false;
            return Equals((Property<T>)other);
        }

        public override int GetHashCode()
        {
            //return EqualityComparer<T>.Default.GetHashCode(value);
            return Value.GetHashCode();
        }

        public static bool operator ==(Property<T> left, Property<T> right)
        {
            return Equals(left, right);
        }
        public static bool operator !=(Property<T> left, Property<T> right)
        {
            return !Equals(left, right);
            //return !(left==right);
        }
    }

    public class Creature
    {
        private Property<int> agility;//= new Property<int>();
        public Property<int> Agility
        {
            get => agility;
            set => this.agility = value;
        }
        public Creature()
        {

        }
        public Creature(Property<int> agility)
        {
            Agility = agility;
        }
    }

    internal class _2_PropertyProxy
    {

        public static void Drive()
        {
            var creature = new Creature();
            creature.Agility = 10;

        }
    }
}
