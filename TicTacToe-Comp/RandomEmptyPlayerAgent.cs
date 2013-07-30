using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Engine;

namespace TicTacToe_Comp
{
    class RandomEmptyPlayerAgent : IPlayerAgent
    {
        public string PlayerName { get { return "EXAMPLE: EmptySpot Random"; } }

        static Random random = new Random();
        public int GetMove(IPlayerBoardAdapter playerBoardAdapter)
        {
            var emptySpots = new List<int>();
            for (int i = 0; i < 9; i++)
            {
                if (playerBoardAdapter.GetField(i) == 0)
                {
                    emptySpots.Add(i);
                }
            }

            return emptySpots[random.Next(0, emptySpots.Count)];
        }
    }
}
