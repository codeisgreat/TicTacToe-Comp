namespace TicTacToe.Engine
{
    public interface IPlayerAgent
    {
        /// <summary>
        /// Unique string identifiing PlayerAgent
        /// </summary>
        string PlayerName { get; }

        /// <summary>
        /// Method must return value in range 0-8 inclusive.
        /// 0 | 1 | 2
        /// ---------
        /// 3 | 4 | 5
        /// ---------
        /// 6 | 7 | 8
        /// </summary>
        /// <param name="playerBoardAdapter"></param>
        /// <returns></returns>
        int GetMove(IPlayerBoardAdapter playerBoardAdapter);
    }
}
