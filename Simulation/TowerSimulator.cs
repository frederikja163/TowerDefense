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
        private bool _towerOverlap = false;
        
        public TowerSimulator(ActivityList activities)
        {
            activities[Activities.PlaceTower].Callback += OnPlaceTower;
            activities[MovementActivities.DragTower].Callback += OnDragTower;
        }

        private void OnPlaceTower(Activities activities)
        {
            _placeTower = true;
            _dragTower = null;
        }

        private void OnDragTower(MovementActivities activities, Vector2 position)
        {
            _towerPosition = position;
            _dragTower = new DraggableTower(_towerPosition.Value, 0.1f, false);
        }

        public GameData Tick(in GameData game)
        {
            if (_towerPosition == null)
            {
                return game with {DragTower = null};
            }
            
            if (_dragTower != null)
            {
                foreach (Tower tower in game.Towers)
                {
                    Vector2 distance = new Vector2(Math.Abs(_dragTower.Position.X - tower.Position.X),
                        Math.Abs(_dragTower.Position.Y - tower.Position.Y));
                    
                    if (Math.Sqrt(distance.X * distance.X + distance.Y * distance.Y) < _dragTower.Radius + tower.Radius)
                    {
                        _towerOverlap = true;
                        break;
                    }
                    else
                    {
                        _towerOverlap = false;
                    }
                }
            }
            
            if (_placeTower && !_towerOverlap)
            {
                ImmutableArray<Tower> towers = game.Towers.Add(new Tower(_towerPosition.Value, 0.1f));
                _towerPosition = null;
                _placeTower = false;

                return game with {Towers = towers, DragTower = _dragTower};
            }

            return game with {DragTower = _dragTower};
        }
    }
}