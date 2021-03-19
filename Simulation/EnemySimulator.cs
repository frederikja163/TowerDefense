using TowerDefense.Common;
using TowerDefense.Common.Game;

namespace TowerDefense.Simulation
{
    internal sealed class EnemySimulator : ISimulator
    {
        public GameData Tick(in GameData game)
        {
            return game;
        }
    }
}