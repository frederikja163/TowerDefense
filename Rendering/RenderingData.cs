using System.Collections.Generic;
using System.Collections.Immutable;
using TowerDefense.Common.Game;

namespace TowerDefense.Platform
{
    internal sealed class RenderingData
    {
        public GameData LastTick { get; }
        public GameData NextTick { get; }
        public float Time { get; }

        public RenderingData(GameData lastTick, GameData nextTick, float time)
        {
            LastTick = lastTick;
            NextTick = nextTick;
            Time = time;
        }

        public IEnumerable<(Enemy?, Enemy?)> Enemies() => EntityEnumerable(LastTick.Enemies, NextTick.Enemies);
        public IEnumerable<(Tower?, Tower?)> Towers() => EntityEnumerable(LastTick.Towers, NextTick.Towers);
        public IEnumerable<(Projectile?, Projectile?)> Projectiles() => EntityEnumerable(LastTick.Projectiles, NextTick.Projectiles);
        
        private IEnumerable<(T?, T?)> EntityEnumerable<T>(ImmutableArray<T> last, ImmutableArray<T> next)
            where T : IEntity
        {
            int lastIndex = 0;
            int nextIndex = 0;

            while (lastIndex < last.Length && nextIndex < next.Length)
            {
                T? lastEntity = default;
                T? nextEntity = default;
                if (last[lastIndex].Id == next[nextIndex].Id)
                {
                    lastEntity = last[lastIndex++];
                    nextEntity = next[nextIndex++];
                }
                else if (last[lastIndex].Id < next[nextIndex].Id)
                {
                    lastEntity = last[lastIndex++];
                }
                else if (last[lastIndex].Id > next[nextIndex].Id)
                {
                    nextEntity = next[nextIndex++];
                }

                yield return (lastEntity, nextEntity);
            }

            while (lastIndex < last.Length)
            {
                T? entity = last[lastIndex++];
                T? nullVal = default;
                yield return (entity, nullVal);
            }

            while (nextIndex < next.Length)
            {
                T? entity = next[nextIndex++];
                T? nullVal = default;
                yield return (nullVal, entity);
            }
        }
    }
}