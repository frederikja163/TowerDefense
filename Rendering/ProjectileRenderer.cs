using System.Collections.Immutable;
using OpenTK.Mathematics;
using TowerDefense.Common;
using TowerDefense.Common.Game;
using TowerDefense.Platform.Rendering;

namespace TowerDefense.Platform
{
    internal sealed class ProjectileRenderer : IRenderer
    {
        private readonly Rect _rect;

        public ProjectileRenderer()
        {
            _rect = new Rect(Vector2.Zero, Vector2.One * 0.01f, Color4.Green);
        }

        public void Render(in GameData lastTick, in GameData nextTick, float percentage)
        {
            ImmutableArray<Projectile> projectiles = nextTick.Projectiles;

            foreach (Projectile projectile in projectiles)
            {
                _rect.Transform.Position = projectile.Position;
                
                _rect.Render();
            }
        }
    }
}