using TowerDefense.Common.Game;

namespace TowerDefense.Common
{
    public interface ISimulator
    {
        GameData Tick(in GameData game);
    }
}
