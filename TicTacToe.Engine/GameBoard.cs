using System;
using System.Linq;
using System.Text;

namespace TicTacToe.Engine
{
    public class GameBoard
    {
        private readonly BoardField[] _board;
        public GameBoard()
        {
            _board = new BoardField[9];
        }

        public BoardField GetField(int index)
        {
            if (index < 0 || index > 8)
            {
                throw new ArgumentException("Index not in array bounds: 0-8", "index");
            }
            return _board[index];
        }

        public GameStatus GetStaus()
        {
            for (int i = 0; i < 3; i++)
            {
                if (IsPlayerField(_board[3 * i]) && IsTrippleEquals(_board[3 * i], _board[3 * i + 1], _board[3 * i + 2]))
                {
                    return PlayerToGamesStatus(_board[3 * i]);
                }
            }

            for (int i = 0; i < 3; i++)
            {
                if (IsPlayerField(_board[i]) && IsTrippleEquals(_board[i], _board[3 + i], _board[6 + i]))
                {
                    return PlayerToGamesStatus(_board[i]);
                }

            }

            if (IsPlayerField(_board[0]) && IsTrippleEquals(_board[0], _board[4], _board[8]))
            {
                return PlayerToGamesStatus(_board[0]);
            }

            if (IsPlayerField(_board[2]) && IsTrippleEquals(_board[2], _board[4], _board[6]))
            {
                return PlayerToGamesStatus(_board[2]);
            }


            int emptyFields = _board.Count(x => x == BoardField.Empty);
            if (emptyFields == 0)
            {
                return GameStatus.Tie;
            }
            return GameStatus.PlayAlong;
        }


        private GameStatus PlayerToGamesStatus(BoardField boardField)
        {
            if (boardField != BoardField.One && boardField != BoardField.Two)
            {
                throw new ArgumentException("Boardfield is not connected with player", "boardField");
            }

            if (boardField == BoardField.One)
            {
                return GameStatus.PlayerOneWins;
            }
            else
            {
                return GameStatus.PlayerTwoWins;
            }
        }

        private bool IsPlayerField(BoardField boardField)
        {
            return boardField == BoardField.One || boardField == BoardField.Two;

        }

        private bool IsTrippleEquals(BoardField first, BoardField second, BoardField third)
        {
            return first == second && second == third;
        }

        public void MakeMove(int playerMove, BoardField who)
        {
            if (who != BoardField.One && who != BoardField.Two)
            {
                throw new ArgumentException("Not player move", "who");
            }

            if (playerMove < 0 || playerMove > 8)
            {
                throw new ArgumentException("PlayerMove not in array bounds: 0-8", "playerMove");
            }

            if (_board[playerMove] != BoardField.Empty)
            {
                throw new BoardFieldNotEmptyException();
            }

            _board[playerMove] = who;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < 9; i++)
            { 
                if (i != 0 && i %3 == 0 )
                {
                    sb.Append("\r\n");
                }
                if (_board[i] == BoardField.One)
                {
                    sb.Append("O");
                }
                else if(_board[i] == BoardField.Two)
                {
                    sb.Append("X");
                }
                else
                {
                    sb.Append("-");
                }

              
            }
            sb.AppendLine("\r\n===");
            return sb.ToString();
        }
    }
}
