using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.NET.SOLID_Principles
{
    //Interface Segregation Principle
    internal class _4_I
    {
        public class Document
        {

        }

        public interface IMachine
        {
            void Print(Document d);
            void Scan(Document d);
            void Fax(Document d);
        }

        public class MultiFunctionPrinter : IMachine
        {
            public void Fax(Document d)
            {
                //
            }

            public void Print(Document d)
            {
                //
            }

            public void Scan(Document d)
            {
                //
            }
        }

        public class OldFashionPrinter : IMachine
        {
            public void Fax(Document d)
            {
                //
            }

            public void Print(Document d)
            {
                throw new NotImplementedException();
            }

            public void Scan(Document d)
            {
                throw new NotImplementedException();
            }
        }

        public interface IPrinter
        {
            void Print(Document d);
        }
        public interface IScanner
        {
            void Scan(Document d);
        }

        public class PhotoCopier : IPrinter, IScanner
        {
            public void Print(Document d)
            {
                throw new NotImplementedException();
            }

            public void Scan(Document d)
            {
                throw new NotImplementedException();
            }
        }

        public interface IMultiFunctionDevice : IScanner, IPrinter
        {

        }

        public class MultiFunctionMachine : IMultiFunctionDevice
        {
            private IPrinter printer;
            private IScanner scanner;

            public MultiFunctionMachine(IPrinter printer, IScanner scanner)
            {
                this.printer = printer ?? throw new ArgumentNullException(paramName: nameof(printer));
                this.scanner = scanner ?? throw new ArgumentNullException(paramName: nameof(scanner));
            }

            public override bool Equals(object obj)
            {
                return base.Equals(obj);
            }

            public override int GetHashCode()
            {
                return base.GetHashCode();
            }

            public void Print(Document d)
            {
                printer?.Print(d);
            }

            public void Scan(Document d)
            {
                scanner?.Scan(d);
            }

            public override string ToString()
            {
                return base.ToString();
            }
        }

        static void Drive()
        {


        }
    }
}
