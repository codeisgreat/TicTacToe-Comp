using System;
using TicTacToe.Engine;

namespace TicTacToe_Comp
{
    class GameModule : Ninject.Modules.NinjectModule
    {
        private readonly Type _playerTwo;
        private readonly Type _playerOne;

        public GameModule(Type playerOne, Type playerTwo)
        {
            _playerOne = playerOne;
            _playerTwo = playerTwo;
        }
        public override void Load()
        {
            Bind<IGame>().To<Game>();
            Bind<IPlayerAgent>().To(_playerOne).Named("playerOne");
            Bind<IPlayerAgent>().To(_playerTwo).Named("playerTwo");
        }
    }
}