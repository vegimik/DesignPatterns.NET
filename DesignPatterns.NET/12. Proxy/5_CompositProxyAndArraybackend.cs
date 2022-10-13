using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.NET._12._Proxy
{
    public class MasonerySettings
    {
        //public bool Pillars, Walls, Floors;

        //public bool? All
        //{
        //    get
        //    {
        //        if (Pillars == Walls && Walls == Floors)
        //            return Pillars;
        //        return null;
        //    }
        //    set
        //    {
        //        if (value == null) return;
        //        Pillars = false;
        //        Walls = false;
        //        Floors = false;
        //    }
        //}
        private bool[] flags = new bool[3];
        public bool Pillars
        {
            get => flags[0];
            set => flags[0] = value;
        }
        public bool Walls
        {
            get => flags[1];
            set => flags[1] = value;
        }
        public bool Floor
        {
            get => flags[2];
            set => flags[2] = value;
        }

        public bool? All
        {
            get
            {
                if (flags.Skip(1).All(x => x == flags[0]))
                {
                    return flags[0];
                }
                return null;
            }
            set
            {
                if (value == null) return;
                for (int i = 0; i < flags.Length; i++)
                {
                    flags[i] = (bool)value;
                }
            }
        }
    }
    internal class _5_CompositProxyAndArraybackend
    {
    }
}
