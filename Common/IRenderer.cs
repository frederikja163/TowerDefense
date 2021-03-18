using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefense.Common
{
    public interface IRenderer
    {
        void Render(in GameData data);
    }
}
