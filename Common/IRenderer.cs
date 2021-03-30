using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TowerDefense.Common.Game;

namespace TowerDefense.Common
{
    public interface IRenderer
    {
        void Render(in GameData lastTick, in GameData nextTick, float percentage);
    }
}
