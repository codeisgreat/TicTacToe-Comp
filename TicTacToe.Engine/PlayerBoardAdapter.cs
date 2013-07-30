using System;

namespace TicTacToe.Engine
{
    public class PlayerBoardAdapter : IPlayerBoardAdapter
    {
        private BoardField _player;
        private GameBoard _gameBoard;

        public PlayerBoardAdapter(GameBoard gameBoard, BoardField player)
        {
            _player = player;
            _gameBoard = gameBoard;
        }

        public int GetField(int index)
        {
            if (index < 0 || index > 8)
            {
                throw new ArgumentException("Index not in array bounds: 0-8", "index");
            }

            if (_gameBoard.GetField(index) == _player)
            {
                return 1;
            }
            else if (_gameBoard.GetField(index) == BoardField.Empty)
            {
                return 0;
            }
            else
            {
                return -1;
            }

        }
    }
}