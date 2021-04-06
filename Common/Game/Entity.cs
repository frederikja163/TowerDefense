using OpenTK.Mathematics;

namespace TowerDefense.Common.Game
{
    public interface IEntity
    {
        Vector2 Position { get; }
        int Id { get; }
    }
    
    public record Tower (
        Vector2 Position,
        int TickForNextShot,
        int Id)
        : IEntity 
    {
    }

    public record DraggableTower (
        Vector2 Position,
        bool Overlap)
    {
    }
    
    public record Enemy (
        Vector2 Position,
        int Id)
        : IEntity
    {
        
    }
    
    public record Projectile (
        Vector2 Position,
        Vector2 Velocity,
        int Id)
        : IEntity
    {
        
    }
}