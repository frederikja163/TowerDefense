using System.Collections.Immutable;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using TowerDefense.Common;
using TowerDefense.Common.Game;
using TowerDefense.Platform.Rendering;

namespace TowerDefense.Platform
{
    public sealed class EnemyRenderer : IRenderer
    {
        private readonly Rect _rect;
        
        public EnemyRenderer()
        {
            _rect = new Rect();
            _rect.Transform.Scale = Vector2.One * 0.1f;
        }

        public void Render(in GameData game)
        {
            ImmutableArray<Enemy> enemies = game.Enemies;

            foreach (Enemy enemy in enemies)
            {
                _rect.Transform.Position = enemy.Position;
                _rect.Render();
            }
        }
    }
}