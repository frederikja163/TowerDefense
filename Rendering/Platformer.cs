using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TowerDefense.Common;

namespace TowerDefense.Platform
{
    public static class Platformer
    {
        public static IPlatform GetPlatform()
        {
            return new GlfwPlatform();
        }

        public static IReadOnlyCollection<IRenderer> GetRenderers()
        {
            return new IRenderer[]
            {

            };
        }
    }
}
