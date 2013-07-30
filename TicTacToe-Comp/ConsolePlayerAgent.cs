using System;
using System.Globalization;
using TicTacToe.Engine;

namespace TicTacToe_Comp
{
    //class ConsolePlayerAgent : IPlayerAgent
    //{
    //    public string PlayerName { get { return "Console Player"; } }
    //    public int GetMove(IPlayerBoardAdapter playerBoardAdapter)
    //    {
    //        int move;
    //        do
    //        {
    //            ConsoleKeyInfo cki = Console.ReadKey(true);

    //            if (Char.IsDigit(cki.KeyChar))
    //            {
    //                move = int.Parse(cki.KeyChar.ToString(CultureInfo.InvariantCulture));
    //                if (playerBoardAdapter.GetField(move) == 0)
    //                {

    //                    break;
    //                }
    //            }

    //            Console.WriteLine("Move is incorrect!");
    //        } while (true);
    //        return move;
    //    }
    //}
}