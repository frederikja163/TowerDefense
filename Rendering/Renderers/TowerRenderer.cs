using System.Collections.Immutable;
using OpenTK.Mathematics;
using TowerDefense.Common.Game;
using TowerDefense.Platform.Rendering;

namespace TowerDefense.Platform.Renderers
{
    internal sealed class TowerRenderer
    {
        private readonly Rect _rect;
        private readonly Rect _draggableRect;

        public TowerRenderer()
        {
            _rect = new Rect();
            _rect.Transform.Scale = Vector2.One * 0.1f;

            _draggableRect = new Rect();
            _draggableRect.Transform.Scale = Vector2.One * 0.1f;
        }
        
        public void Render(RenderingData data)
        {
            ImmutableArray<Tower> towers = data.NextTick.Towers;
            DraggableTower? nextDragTower = data.NextTick.DragTower;
            DraggableTower? lastDragTower = data.LastTick.DragTower;
            
            foreach (Tower tower in towers)
            {
                _rect.Transform.Position = tower.Position;
                _rect.Render();
            }
            
            if (nextDragTower != null)
            {
                if (lastDragTower != null)
                {
                    _draggableRect.Transform.Position =
                        Vector2.Lerp(lastDragTower.Position, nextDragTower.Position, data.Time);
                }
                else
                {
                    _draggableRect.Transform.Position = nextDragTower.Position;
                }
                _draggableRect.Color = nextDragTower.Overlap ?
                    new Color4<Rgba>(0.8f, 0.2f, 0.2f, 0.8f) :
                    new Color4<Rgba>(1, 1, 1, 0.8f);
                _draggableRect.Render();
            }
        }
    }
}