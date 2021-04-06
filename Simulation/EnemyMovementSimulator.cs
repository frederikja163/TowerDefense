using System.Collections.Immutable;
using OpenTK.Mathematics;
using TowerDefense.Common;
using TowerDefense.Common.Game;

namespace TowerDefense.Simulation
{
    internal sealed class EnemyMovementSimulator
    {
        private const int TicksBetweenEnemySpawn = 20;
        private int _ticksForNextEnemy = TicksBetweenEnemySpawn;
        
        public void Tick(SimulationData data)
        {
            ImmutableArray<Enemy>.Builder? builder = data.Enemies;
            
            _ticksForNextEnemy--;
            if (_ticksForNextEnemy < 0)
            {
                _ticksForNextEnemy = TicksBetweenEnemySpawn;

                Enemy enemy = new Enemy(Vector2.Zero, data.TotalEntityCount++);
                builder.Add(enemy);
            }

            for (int i = 0; i < builder.Count; i++)
            {
                Enemy enemy = builder[i];
                builder[i] = enemy with {Position = enemy.Position + Vector2.One / 100};
            }
        }
    }
}