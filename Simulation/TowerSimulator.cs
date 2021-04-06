using System;
using System.Collections.Immutable;
using OpenTK.Mathematics;
using TowerDefense.Common;
using TowerDefense.Common.Extensions;
using TowerDefense.Common.Game;

namespace TowerDefense.Simulation
{
    internal sealed class TowerSimulator : ISimulator
    {
        private const float TowerDiameter = 0.1f;
        private Vector2? _towerPosition;
        private bool _placeTower;
        
        public TowerSimulator(ActivityList activities)
        {
            activities[Activities.PlaceTower].Callback += OnPlaceTower;
            activities[MovementActivities.DragTower].Callback += OnDragTower;
        }

        private void OnPlaceTower(Activities activities)
        {
            _placeTower = true;
        }

        private void OnDragTower(MovementActivities activities, Vector2 position)
        {
            _towerPosition = position;
        }

        public GameData Tick(in GameData game)
        {
            ImmutableArray<Tower> towers = game.Towers;
            DraggableTower dragTower = game.DragTower;
            
            if (_towerPosition == null)
            {
                if (dragTower == null)
                {
                    return game;
                }
                return game with {DragTower = null};
            }
            
            bool overlap = towers.CheckForCollision(_towerPosition.Value, TowerDiameter);
            if (dragTower == null)
            {
                dragTower = new DraggableTower(_towerPosition.Value, overlap);
            }
            else
            {
                dragTower = dragTower with
                {
                    Overlap = overlap,
                    Position = _towerPosition.Value
                };
            }

            if (_placeTower)
            {
                int entityCount = game.TotalEntityCount;
                if (!dragTower.Overlap)
                {
                    entityCount++;
                    towers = game.Towers.Add(new Tower(_towerPosition.Value, game.Tick, entityCount));
                }
                _placeTower = false;
                _towerPosition = null;

                return game with {Towers = towers, DragTower = null, TotalEntityCount = entityCount};
            }
            return game with {DragTower = dragTower};
        }
    }
}