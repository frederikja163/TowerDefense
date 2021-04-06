using System.Collections.Immutable;
using OpenTK.Mathematics;
using TowerDefense.Common;
using TowerDefense.Common.Game;
using TowerDefense.Platform.Rendering;

namespace TowerDefense.Platform
{
    public sealed class TowerRenderer
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
        
        public void Render(in GameData lastTick, in GameData nextTick, float percentage)
        {
            ImmutableArray<Tower> towers = nextTick.Towers;
            DraggableTower? dragTower = nextTick.DragTower;

            foreach (Tower tower in towers)
            {
                _rect.Transform.Position = tower.Position;
                _rect.Render();
            }
            
            if (dragTower != null)
            {
                _draggableRect.Transform.Position = dragTower.Position;
                _draggableRect.Color = dragTower.Overlap ?
                    new Color4<Rgba>(0.8f, 0.2f, 0.2f, 0.8f) :
                    new Color4<Rgba>(1, 1, 1, 0.8f);
                _draggableRect.Render();
            }
        }
    }
}