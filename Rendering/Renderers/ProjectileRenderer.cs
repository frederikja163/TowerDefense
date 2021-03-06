using System.Collections.Immutable;
using OpenTK.Mathematics;
using TowerDefense.Common.Game;
using TowerDefense.Platform.Rendering;

namespace TowerDefense.Platform.Renderers
{
    internal sealed class ProjectileRenderer
    {
        private readonly Rect _rect;

        public ProjectileRenderer()
        {
            _rect = new Rect(Vector2.Zero, Vector2.One * 0.01f, Color4.Green);
        }

        public void Render(RenderingData data)
        {
            foreach ((Projectile? lastProjectile, Projectile? nextProjectile) in data.Projectiles())
            {
                if (lastProjectile == null || nextProjectile == null)
                {
                    continue;
                }

                _rect.Transform.Position = Vector2.Lerp(lastProjectile.Position, nextProjectile.Position, data.Time);
                _rect.Render();
            }
        }
    }
}