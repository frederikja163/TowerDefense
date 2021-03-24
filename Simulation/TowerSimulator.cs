using System;
using System.Collections.Immutable;
using OpenTK.Mathematics;
using TowerDefense.Common;
using TowerDefense.Common.Game;

namespace TowerDefense.Simulation
{
    internal sealed class TowerSimulator : ISimulator
    {
        private Vector2? _towerPosition;
        private bool _placeTower;
        private DraggableTower _dragTower;
        
        public TowerSimulator(ActivityList activities)
        {
            activities[Activities.BeginTower].Callback += OnBeginTower;
            activities[Activities.PlaceTower].Callback += OnPlaceTower;
            activities[MovementActivities.DragTower].Callback += OnDragTower;
        }

        private void OnBeginTower(Activities activities)
        {
            _dragTower = new DraggableTower(_towerPosition.Value);
        }

        private void OnPlaceTower(Activities activities)
        {
            _placeTower = true;
            _dragTower = null;
        }

        private void OnDragTower(MovementActivities activities, Vector2 position)
        {
            _towerPosition = position;

            if (_dragTower != null)
            {
                _dragTower = new DraggableTower(_towerPosition.Value);
            }
            
        }

        public GameData Tick(in GameData game)
        {
            if (_towerPosition == null)
            {
                return game with {DragTower = null};
            }

            if (_placeTower)
            {
                ImmutableArray<Tower> towers = game.Towers.Add(new Tower(_towerPosition.Value, 0));
                _towerPosition = null;
                _placeTower = false;
                
                return game with {Towers = towers, DragTower = _dragTower};
            }

            return game with {DragTower = _dragTower};
        }
    }
}