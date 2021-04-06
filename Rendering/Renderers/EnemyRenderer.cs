using System.Collections.Immutable;
using OpenTK.Mathematics;
using TowerDefense.Common.Game;
using TowerDefense.Platform.Rendering;

namespace TowerDefense.Platform.Renderers
{
    internal sealed class EnemyRenderer
    {
        private readonly Rect _rect;
        
        public EnemyRenderer()
        {
            _rect = new Rect(Color4.Blue);
            _rect.Transform.Scale = Vector2.One * 0.1f;
        }

        public void Render(RenderingData data)
        {
            foreach ((Enemy? lastEnemy, Enemy? nextEnemy) in data.Enemies())
            {
                if (lastEnemy == null || nextEnemy == null)
                {
                    continue;
                }

                _rect.Transform.Position = Vector2.Lerp(lastEnemy.Position, nextEnemy.Position, data.Time);
                _rect.Render();
            }
        }
    }
}