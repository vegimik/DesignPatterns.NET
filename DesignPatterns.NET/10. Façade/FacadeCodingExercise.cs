using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.NET._10._Façade
{
    internal class FacadeCodingExercise
    {
        public class Generator
        {
            private static readonly Random random = new Random();

            public List<int> Generate(int count)
            {
                return Enumerable.Range(0, count)
                  .Select(_ => random.Next(1, 6))
                  .ToList();
            }
        }

        public class Splitter
        {
            public List<List<int>> Split(List<List<int>> array)
            {
                var result = new List<List<int>>();

                var rowCount = array.Count;
                var colCount = array[0].Count;

                // get the rows
                for (int r = 0; r < rowCount; ++r)
                {
                    var theRow = new List<int>();
                    for (int c = 0; c < colCount; ++c)
                        theRow.Add(array[r][c]);
                    result.Add(theRow);
                }

                // get the columns
                for (int c = 0; c < colCount; ++c)
                {
                    var theCol = new List<int>();
                    for (int r = 0; r < rowCount; ++r)
                        theCol.Add(array[r][c]);
                    result.Add(theCol);
                }

                // now the diagonals
                var diag1 = new List<int>();
                var diag2 = new List<int>();
                for (int c = 0; c < colCount; ++c)
                {
                    for (int r = 0; r < rowCount; ++r)
                    {
                        if (c == r)
                            diag1.Add(array[r][c]);
                        var r2 = rowCount - r - 1;
                        if (c == r2)
                            diag2.Add(array[r][c]);
                    }
                }

                result.Add(diag1);
                result.Add(diag2);

                return result;
            }
        }

        public class Verifier
        {
            public bool Verify(List<List<int>> array)
            {
                if (!array.Any()) return false;

                var expected = array.First().Sum();

                return array.All(t => t.Sum() == expected);
            }
        }

        public class MagicSquareGenerator
        {
            public int Size { get; private set; }
            public List<List<int>> Square { get; private set; }

            protected Generator generator;
            protected Splitter splitter;
            protected Verifier verifier;

            public MagicSquareGenerator()
            {
                this.generator = new Generator();
                this.splitter = new Splitter();
                this.verifier = new Verifier();
            }


            public List<List<int>> Generate(int size)
            {
                this.Size = size;
                //var matrix = new List<List<int>>();
                //var splittedMatrix=new List<List<int>>();
                //do
                //{
                //    matrix = GenerateMatrix(size);
                //    splittedMatrix = splitter.Split(matrix);
                //} while (!verifier.Verify(splittedMatrix));
                ////var matrix = GenerateMatrix(size);
                ////var splittedMatrix = splitter.Split(matrix);
                ////return verifier.Verify(splittedMatrix) ? matrix : null;
                //return matrix;


                var square = new List<List<int>>();

                do
                {
                    square = new List<List<int>>();
                    for (int i = 0; i < size; ++i)
                        square.Add(generator.Generate(size));
                } while (!verifier.Verify(splitter.Split(square)));

                this.Square = square;
                return square;
            }

            public List<List<int>> GenerateMatrix(int size)
            {
                var generatedMatrix = new List<List<int>>();
                for (int i = 0; i < size; i++)
                    generatedMatrix.Add(generator.Generate(size));
                return generatedMatrix;
            }

            public override string ToString()
            {
                var sb = new StringBuilder();
                for (int i = 0; i < Size; i++)
                {
                    for (int j = 0; j < Size; j++)
                    {
                        sb.Append($"{Square[i][j]} ");
                    }
                    sb.AppendLine("");
                }
                return sb.ToString();
            }
        }


        public static void Drive()
        {
            var magicSquareGenerator = new MagicSquareGenerator();
            magicSquareGenerator.Generate(3);
            Console.WriteLine(magicSquareGenerator.ToString());

        }
    }
}
