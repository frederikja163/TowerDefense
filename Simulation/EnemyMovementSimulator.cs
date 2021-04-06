using System.Collections.Immutable;
using OpenTK.Mathematics;
using TowerDefense.Common;
using TowerDefense.Common.Game;

namespace TowerDefense.Simulation
{
    internal sealed class EnemyMovementSimulator : ISimulator
    {
        private const int TicksBetweenEnemySpawn = 20;
        private int _ticksForNextEnemy = TicksBetweenEnemySpawn;
        
        public GameData Tick(in GameData game)
        {
            ImmutableArray<Enemy>.Builder? builder = game.Enemies.ToBuilder();
            int entityCount = game.TotalEntityCount;
            
            _ticksForNextEnemy--;
            if (_ticksForNextEnemy < 0)
            {
                _ticksForNextEnemy = TicksBetweenEnemySpawn;

                entityCount++;
                Enemy enemy = new Enemy(Vector2.Zero, entityCount);
                builder.Add(enemy);
            }

            for (int i = 0; i < builder.Count; i++)
            {
                Enemy enemy = builder[i];
                builder[i] = enemy with {Position = enemy.Position + Vector2.One / 100};
            }
            
            return game with {Enemies = builder.ToImmutable(), TotalEntityCount = entityCount};
        }
    }
}