using OpenTK.Mathematics;

namespace TowerDefense.Common.Game
{
    public record Tower (Vector2 Position, int TickForNextShot)
    {
        
    }

    public record DraggableTower (Vector2 Position, bool Overlap)
    {
        
    }
}