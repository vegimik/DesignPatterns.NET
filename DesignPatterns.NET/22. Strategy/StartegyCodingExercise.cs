using NUnit.Framework;
using System;
using System.Numerics;

namespace DesignPatterns.NET._22._Strategy
{
    public interface IDiscriminantStrategy
    {
        double CalculateDiscriminant(double a, double b, double c);
    }

    public class OrdinaryDiscriminantStrategy : IDiscriminantStrategy
    {
        // todo
        public double CalculateDiscriminant(double a, double b, double c)
        {
            return Math.Pow(b, 2) - 4 * a * c;
        }
    }

    public class RealDiscriminantStrategy : IDiscriminantStrategy
    {
        // todo (return NaN on negative discriminant!)
        public double CalculateDiscriminant(double a, double b, double c)
        {
            var discriminant = Math.Pow(b, 2) - 4 * a * c;
            return discriminant >= 0 ? discriminant : double.NaN;
        }
    }

    public class QuadraticEquationSolver
    {
        private readonly IDiscriminantStrategy strategy;

        public QuadraticEquationSolver(IDiscriminantStrategy strategy)
        {
            this.strategy = strategy;
        }

        public Tuple<Complex, Complex> Solve(double a, double b, double c)
        {
            var disciminant = strategy.CalculateDiscriminant(a, b, c);
            var complexSquare = Complex.Sqrt(disciminant);
            return new Tuple<Complex, Complex>(((-1) * b + complexSquare) / (2 * a), ((-1) * b - complexSquare) / (2 * a));

            //var disc = new Complex(strategy.CalculateDiscriminant(a, b, c), 0);
            //var rootDisc = Complex.Sqrt(disciminant);
            //return Tuple.Create(
            //  (-b + rootDisc) / (2 * a),
            //  (-b - rootDisc) / (2 * a)
            //);
        }
    }

    internal class StartegyCodingExercise
    {


        public static void Drive()
        {

        }
    }

    [TestFixture]
    public class StartegyCodingExercise_TestSuits
    {
        [Test]
        public void PositiveTestOrdinaryStrategy()
        {
            var strategy = new OrdinaryDiscriminantStrategy();
            var solver = new QuadraticEquationSolver(strategy);
            var results = solver.Solve(1, 10, 16);
            Assert.That(results.Item1, Is.EqualTo(new Complex(-2, 0)));
            Assert.That(results.Item2, Is.EqualTo(new Complex(-8, 0)));
        }

        [Test]
        public void PositiveTestRealStrategy()
        {
            var strategy = new RealDiscriminantStrategy();
            var solver = new QuadraticEquationSolver(strategy);
            var results = solver.Solve(1, 10, 16);
            Assert.That(results.Item1, Is.EqualTo(new Complex(-2, 0)));
            Assert.That(results.Item2, Is.EqualTo(new Complex(-8, 0)));
        }

        [Test]
        public void NegativeTestOrdinaryStrategy()
        {
            var strategy = new OrdinaryDiscriminantStrategy();
            var solver = new QuadraticEquationSolver(strategy);
            var results = solver.Solve(1, 4, 5);
            Assert.That(results.Item1, Is.EqualTo(new Complex(-2, 1)));
            Assert.That(results.Item2, Is.EqualTo(new Complex(-2, -1)));
        }

        [Test]
        public void NegativeTestRealStrategy()
        {
            var strategy = new RealDiscriminantStrategy();
            var solver = new QuadraticEquationSolver(strategy);
            var results = solver.Solve(1, 4, 5);
            var complexNaN = new Complex(double.NaN, double.NaN);

            Assert.That(results.Item1, Is.EqualTo(complexNaN));
            Assert.That(results.Item2, Is.EqualTo(complexNaN));

            Assert.IsTrue(double.IsNaN(results.Item1.Real));
            Assert.IsTrue(double.IsNaN(results.Item1.Imaginary));
            Assert.IsTrue(double.IsNaN(results.Item2.Real));
            Assert.IsTrue(double.IsNaN(results.Item2.Imaginary));
        }
    }

}
