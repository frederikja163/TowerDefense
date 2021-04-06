using System.Collections.Immutable;
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
            _rect = new Rect(Color4.Blue);
            _rect.Transform.Scale = Vector2.One * 0.1f;
        }

        public void Render(in GameData lastTick, in GameData nextTick, float percentage)
        {
            ImmutableArray<Enemy> lastEnemies = lastTick.Enemies;
            ImmutableArray<Enemy> nextEnemies = nextTick.Enemies;

            
            for (int lastIndex = 0, nextIndex = 0;
                lastIndex < lastEnemies.Length && nextIndex < nextEnemies.Length;
                lastIndex++, nextIndex++)
            {
                Enemy lastEnemy = lastEnemies[lastIndex];
                Enemy nextEnemy = nextEnemies[nextIndex];
                if (lastEnemy.Id != nextEnemy.Id)
                {
                    lastIndex--;
                    continue;
                }
                
                _rect.Transform.Position = Vector2.Lerp(lastEnemy.Position, nextEnemy.Position, percentage);
                _rect.Render();
            }
        }
    }
}