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

        public GameData Tick(GameData gameData)
        {
            SimulationData data = new SimulationData(gameData);
            _projectileShooterSimulator.Tick(data);
            _projectileMovementSimulator.Tick(data);
            _enemyMovementSimulator.Tick(data);
            _enemyDeathSimulator.Tick(data);
            _towerSimulator.Tick(data);
            return data.MakeGameData();
        }
    }
}
