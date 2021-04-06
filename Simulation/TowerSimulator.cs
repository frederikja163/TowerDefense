using System;
using System.Collections.Immutable;
using OpenTK.Mathematics;
using TowerDefense.Common;
using TowerDefense.Common.Extensions;
using TowerDefense.Common.Game;

namespace TowerDefense.Simulation
{
    internal sealed class TowerSimulator
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

        public void Tick(SimulationData data)
        {
            ImmutableArray<Tower>.Builder towers = data.Towers;
            int tick = data.Tick;
            DraggableTower? dragTower = data.DragTower;

            if (_towerPosition == null)
            {
                data.DragTower = null;
                return;
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
            data.DragTower = dragTower;

            if (_placeTower)
            {
                if (!dragTower.Overlap)
                {
                    towers.Add(new Tower(_towerPosition.Value, tick, data.TotalEntityCount++));
                }
                _placeTower = false;
                _towerPosition = null;
            }
        }
    }
}