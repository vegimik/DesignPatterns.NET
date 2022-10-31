using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.NET._23._TemplateMethod
{
    public abstract class Game
    {
        protected int currentPLayer;
        protected readonly int numberOfPlayers;

        public Game(int numberOfPlayers)
        {
            this.numberOfPlayers = numberOfPlayers;
        }
        public void Run()
        {
            Start();
            while (!HaveWinner)
                TakeTurn();
            Console.WriteLine($"Player {WinningPlayer} wins");
        }

        protected abstract void Start();
        protected abstract void TakeTurn();
        protected abstract bool HaveWinner { get; }
        protected abstract int WinningPlayer { get; }
    }

    public class Chess : Game
    {
        public Chess() : base(2)
        {

        }
        protected override bool HaveWinner => turn == maxTurns;

        protected override int WinningPlayer => currentPLayer;

        protected override void Start()
        {
            Console.WriteLine($"Starting a game of chess with {numberOfPlayers} players.");
        }

        protected override void TakeTurn()
        {
            Console.WriteLine($"Turn {turn++} taken by player {currentPLayer}.");
            currentPLayer = (currentPLayer + 1) % numberOfPlayers;
        }
        private int turn = 1;
        private int maxTurns = 10;

    }

    internal class _1_TemplateMethod
    {
        public static void Drive()
        {
            var chess = new Chess();
            chess.Run();

        }
    }
}
