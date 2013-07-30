using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using Ninject;
using TicTacToe.Engine;

namespace TicTacToe_Comp
{
    class Program
    {
        private static readonly ConcurrentDictionary<string, int> _scores = new ConcurrentDictionary<string, int>();
        static void Main(string[] args)
        {

            var playerAgents = GetAllPlayerAgents(typeof(IPlayerAgent)).ToList();
            var playersPairs = CreatePlayersPairs(playerAgents).ToList();
            //for (int i = 0; i < 100; i++)
            //{
            foreach (var playersPair in playersPairs)
            {
                PlayGame(playersPair.Item1, playersPair.Item2);
            }
            //}

            foreach (var score in _scores)
            {
                Console.WriteLine("{0} = {1}", score.Key, score.Value);
            }
            Console.ReadKey(true);

        }

        private static IEnumerable<Tuple<Type, Type>> CreatePlayersPairs(List<Type> playerAgents)
        {
            return from playerOne in playerAgents
                   from playerTwo in playerAgents
                   where playerOne != playerTwo
                   select new Tuple<Type, Type>(playerOne, playerTwo);
        }

        private static void PlayGame(Type playerOneType, Type playerTwoType)
        {
            IKernel kernel = new StandardKernel(new GameModule(playerOneType, playerTwoType));

            var game = kernel.Get<IGame>();
            var playerOneAgent = kernel.Get<IPlayerAgent>("playerOne");
            var playerTwoAgent = kernel.Get<IPlayerAgent>("playerTwo");

            game.GameLoop().Subscribe((x) => { Console.WriteLine(x); }, () =>
                {
                    if (game.GameStatus == GameStatus.PlayerOneWins)
                    {
                        _scores.AddOrUpdate(playerOneAgent.PlayerName,
                            str => 3,
                            (str, score) => score + 3);
                        Console.WriteLine("Player ONE Wins");
                    }
                    else if (game.GameStatus == GameStatus.PlayerTwoWins)
                    {
                        _scores.AddOrUpdate(playerTwoAgent.PlayerName,
                            str => 3,
                            (str, score) => score + 3);
                        Console.WriteLine("Player TWO  Wins");
                    }
                    else
                    {
                        _scores.AddOrUpdate(playerOneAgent.PlayerName,
                            str => 1,
                            (str, score) => ++score);
                        _scores.AddOrUpdate(playerTwoAgent.PlayerName,
                            str => 1,
                            (str, score) => ++score);
                        Console.WriteLine("TIE");
                    }
                });
        }

        public static IEnumerable<Type> GetAllPlayerAgents(Type baseType)
        {
            var types = AppDomain.CurrentDomain.GetAssemblies().ToList()
                .SelectMany(s => s.GetTypes())
                .Where(t => !t.IsInterface)
                .Where(baseType.IsAssignableFrom);
            return types;
        }
    }
}
