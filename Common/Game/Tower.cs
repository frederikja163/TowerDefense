using OpenTK.Mathematics;

namespace TowerDefense.Common.Game
{
    public record Tower (Vector2 Position, float Radius)
    {
        
    }

    public record DraggableTower (Vector2 Position, float Radius, bool Overlap)
    {
        
    }
}