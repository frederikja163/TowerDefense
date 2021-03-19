using OpenTK.Mathematics;

namespace TowerDefense.Common
{
    public class Transform3D
    {
        public Vector3 Position { get; set; }
        
        public Vector3 Scale { get; set; } = Vector3.One;
        
        public Quaternion Rotation { get; set; } = Quaternion.Identity;
    }
    
    
    public class Transform2D
    {
        public Vector2 Position { get; set; }
        
        public Vector2 Scale { get; set; } = Vector2.One;
        
        public float Rotation { get; set; } = 0;
    }
}