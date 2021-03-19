using System.Collections.Immutable;
using OpenTK.Mathematics;
using TowerDefense.Common;
using TowerDefense.Common.Game;

namespace TowerDefense.Simulation
{
    internal sealed class TowerSimulator : ISimulator
    {
        private bool _shouldCreateTower = false;
        private Vector2 _towerPosition;
        
        public TowerSimulator(ActivityList activities)
        {
            activities[MovementActivities.PlaceTower].Callback += OnPlaceTower;
        }

        private void OnPlaceTower(MovementActivities activities, Vector2 position)
        {
            _shouldCreateTower = true;
            _towerPosition = position;
        }

        public GameData Tick(in GameData game)
        {
            if (_shouldCreateTower)
            {
                _shouldCreateTower = false;
                ImmutableArray<Tower> towers = game.Towers.Add(new Tower(_towerPosition));

                return game with {Towers = towers};
            }

            return game;
        }
    }
}