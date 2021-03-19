using OpenTK.Mathematics;
using TowerDefense.Common;

namespace TowerDefense.Platform.Extensions
{
    internal static class TransformExtension
    {
        public static Matrix4 GetMatrix(this Transform3D transform)
            => Matrix4.CreateScale(transform.Scale) *
               Matrix4.CreateFromQuaternion(transform.Rotation) *
               Matrix4.CreateTranslation(transform.Position);

        public static Matrix4 GetMatrix(this Transform2D transform)
            => Matrix4.CreateScale(transform.Scale.X, transform.Scale.Y, 1) *
               Matrix4.CreateRotationZ(transform.Rotation) *
               Matrix4.CreateTranslation(transform.Position.X, transform.Position.Y, 0);
    }
}