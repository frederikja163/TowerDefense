using System.Collections.Immutable;
using OpenTK.Mathematics;
using TowerDefense.Common;
using TowerDefense.Common.Game;

namespace TowerDefense.Simulation
{
    internal sealed class TowerSimulator : ISimulator
    {
        private Vector2? _towerPosition;
        
        public TowerSimulator(ActivityList activities)
        {
            activities[MovementActivities.PlaceTower].Callback += OnPlaceTower;
        }

        private void OnPlaceTower(MovementActivities activities, Vector2 position)
        {
            _towerPosition = position;
        }

        public GameData Tick(in GameData game)
        {
            if (_towerPosition != null)
            {
                ImmutableArray<Tower> towers = game.Towers.Add(new Tower(_towerPosition.Value));
                _towerPosition = null;

                return game with {Towers = towers};
            }

            return game;
        }
    }
}