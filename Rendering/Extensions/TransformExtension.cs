using OpenTK.Mathematics;
using TowerDefense.Common;

namespace TowerDefense.Platform.Extensions
{
    internal static class TransformExtension
    {
        public static Matrix4 GetMatrix(this Transform transform)
            => Matrix4.CreateFromQuaternion(transform.Rotation) *
               Matrix4.CreateScale(transform.Scale) *
               Matrix4.CreateTranslation(transform.Position);
    }
}