using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using OpenTK.Mathematics;
using TowerDefense.Common.Game;

namespace TowerDefense.Common.Extensions
{
    public static class ArrayOfEntityExtensions
    {
        public static bool CheckForCollision(this IReadOnlyCollection<IEntity> entities, Vector2 point, float distance)
        {
            if (entities.Count <= 0)
            {
                throw new ArgumentOutOfRangeException("No entities in collection!");
            }
            
            float distanceSqrd = distance * distance;
            foreach (IEntity entity in entities)
            {
                Vector2 distanceVec = entity.Position - point;
                if (distanceVec.LengthSquared < distanceSqrd)
                {
                    return true;
                }
            }

            return false;
        }

        public static float GetClosestDistanceSqrt(this IReadOnlyCollection<IEntity> entities, Vector2 point, out IEntity closestEntity, out int closestIndex)
        {
            if (entities.Count <= 0)
            {
                throw new ArgumentOutOfRangeException("No entities in collection!");
            }
            
            float smallestDistance = float.MaxValue;
            closestIndex = -1;
            int i = 0;
            closestEntity = null;
            foreach (IEntity entity in entities)
            {
                float distance = (entity.Position - point).LengthSquared;
                if (distance < smallestDistance)
                {
                    smallestDistance = distance;
                    closestEntity = entity;
                    closestIndex = i;
                }

                i++;
            }

            return smallestDistance;
        }
    }
}