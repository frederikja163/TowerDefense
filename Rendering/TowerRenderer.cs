using OpenTK.Mathematics;
using TowerDefense.Common;
using TowerDefense.Common.Game;
using TowerDefense.Platform.Rendering;

namespace TowerDefense.Platform
{
    public sealed class TowerRenderer : IRenderer
    {
        private readonly Rect _rect;

        public TowerRenderer()
        {
            _rect = new Rect();
            _rect.Transform.Scale = Vector2.One * 0.1f;
        }
        
        public void Render(in GameData game)
        {
            DraggableTower dragTower = game.DragTower;
            
            if (dragTower != null)
            {
                _rect.Transform.Position = dragTower.Position;
                _rect.Render();
            }
            
            foreach (Tower tower in game.Towers)
            {
                _rect.Transform.Position = tower.Position;
                _rect.Render();
            }
        }
    }
}