using OpenTK.Mathematics;

namespace TowerDefense.Common
{
    public class Transform
    {
        public Vector3 Position { get; set; }
        
        public Vector3 Scale { get; set; } = Vector3.One;
        
        public Quaternion Rotation { get; set; } = Quaternion.Identity;
    }
}