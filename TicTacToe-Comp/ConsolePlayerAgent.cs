using System;
using System.Globalization;
using TicTacToe.Engine;

namespace TicTacToe_Comp
{
    class ConsolePlayerAgent : IPlayerAgent
    {
        public string PlayerName { get { return "Console Player"; } }
        public int GetMove(IPlayerBoardAdapter playerBoardAdapter)
        {
            int move;
            do
            {
                Console.Write("Enter your move (0-8): ");
                ConsoleKeyInfo cki = Console.ReadKey(false);
                Console.WriteLine();
                if (Char.IsDigit(cki.KeyChar))
                {
                    move = int.Parse(cki.KeyChar.ToString(CultureInfo.InvariantCulture));

                    if (move <0 || move>8)
                    {
                        continue;
                    }

                    if (playerBoardAdapter.GetField(move) == 0)
                    {
                        break;
                    }
                }

                Console.WriteLine("Move is incorrect!");
            } while (true);
            return move;
        }
    }
}