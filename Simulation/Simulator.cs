using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TowerDefense.Common;
using TowerDefense.Common.Game;

namespace TowerDefense.Simulation
{
    public sealed class Simulator
    {
        private readonly ProjectileShooterSimulator _projectileShooterSimulator;
        private readonly ProjectileMovementSimulator _projectileMovementSimulator;
        private readonly EnemyMovementSimulator _enemyMovementSimulator;
        private readonly EnemyDeathSimulator _enemyDeathSimulator;
        private readonly TowerSimulator _towerSimulator;
        
        public Simulator(ActivityList activities)
        {
            _projectileShooterSimulator = new ProjectileShooterSimulator();
            _projectileMovementSimulator = new ProjectileMovementSimulator();
            _enemyMovementSimulator = new EnemyMovementSimulator();
            _enemyDeathSimulator = new EnemyDeathSimulator();
            _towerSimulator = new TowerSimulator(activities);
        }

        public GameData Tick(in GameData data)
        {
            GameData d = data with {Tick = data.Tick + 1};
            d = _projectileShooterSimulator.Tick(d);
            d = _projectileMovementSimulator.Tick(d);
            d = _enemyMovementSimulator.Tick(d);
            d = _enemyDeathSimulator.Tick(d);
            d = _towerSimulator.Tick(d);
            return d;
        }
    }
}
