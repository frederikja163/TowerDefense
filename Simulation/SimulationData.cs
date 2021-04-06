using System.Collections.Immutable;
using TowerDefense.Common.Game;

namespace TowerDefense.Simulation
{
    internal sealed class SimulationData
    {
        public ImmutableArray<Tower>.Builder Towers { get; }
        public ImmutableArray<Projectile>.Builder Projectiles { get; }
        public ImmutableArray<Enemy>.Builder Enemies { get; }
        public DraggableTower? DragTower { get; set; }
        public int TotalEntityCount { get; set; }
        public int Tick { get; }
        
        public SimulationData(GameData data)
        {
            Towers = data.Towers.ToBuilder();
            Projectiles = data.Projectiles.ToBuilder();
            Enemies = data.Enemies.ToBuilder();
            DragTower = data.DragTower;
            TotalEntityCount = data.TotalEntityCount;
            Tick = data.Tick + 1;
        }

        public GameData MakeGameData()
        {
            return new GameData(
                Enemies.ToImmutable(),
                Towers.ToImmutable(),
                Projectiles.ToImmutable(),
                DragTower,
                Tick,
                TotalEntityCount);
        }
    }
}