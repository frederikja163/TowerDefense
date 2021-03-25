using OpenTK.Mathematics;

namespace TowerDefense.Common.Game
{
    public interface IEntity
    {
        Vector2 Position { get; }
    }
    
    public record Tower (
        Vector2 Position,
        int TickForNextShot)
        : IEntity 
    {
    }

    public record DraggableTower (
        Vector2 Position,
        bool Overlap)
        : IEntity
    {
    }
    
    public record Enemy (
        Vector2 Position)
        : IEntity
    {
        
    }
    
    public record Projectile (
        Vector2 Position,
        Vector2 Velocity)
        : IEntity
    {
        
    }
}