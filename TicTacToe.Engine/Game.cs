using System;
using System.Diagnostics;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Ninject;

namespace TicTacToe.Engine
{
    public class Game : IGame
    {
        private readonly IPlayerAgent _playerOne;
        private readonly IPlayerAgent _playerTwo;
        private readonly GameBoard _gameBoard;
        private readonly IPlayerBoardAdapter _playerOneAdapter;
        private readonly IPlayerBoardAdapter _playerTwoAdapter;
        private bool _wasStarted;
        public Game([Named("playerOne")] IPlayerAgent playerOne, [Named("playerTwo")] IPlayerAgent playerTwo)
        {
            _playerOne = playerOne;
            _playerTwo = playerTwo;
            _gameBoard = new GameBoard();

            _playerOneAdapter = new PlayerBoardAdapter(_gameBoard, BoardField.One);
            _playerTwoAdapter = new PlayerBoardAdapter(_gameBoard, BoardField.Two);
        }

        public IObservable<GameBoard> GameLoop()
        {
            if (_wasStarted)
            {
                Debugger.Break();
            }
            _wasStarted = true;
            return Observable.Create((IObserver<GameBoard> observable) =>
                {
                    do
                    {
                        int playerOneMove = _playerOne.GetMove(_playerOneAdapter);
                        _gameBoard.MakeMove(playerOneMove, BoardField.One);
                        observable.OnNext(_gameBoard);
                        if (_gameBoard.GetStaus() != GameStatus.PlayAlong)
                        {
                            break;
                        }

                        int playerTwoMove = _playerTwo.GetMove(_playerTwoAdapter);
                        _gameBoard.MakeMove(playerTwoMove, BoardField.Two);
                        observable.OnNext(_gameBoard);
                    } while (_gameBoard.GetStaus() == GameStatus.PlayAlong);
                    observable.OnCompleted();
                    return Disposable.Empty;
                });
        }

        public GameStatus GameStatus { get { return _gameBoard.GetStaus(); } }
    }

    public interface IGame
    {
        IObservable<GameBoard> GameLoop();
        GameStatus GameStatus { get; }
    }
}
