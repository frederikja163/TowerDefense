using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TowerDefense.Common;
using TowerDefense.Platform.Glfw;

namespace TowerDefense.Platform
{
    public static class Platformer
    {
        private static IRenderer[] _renderers = new IRenderer[4];
        
        public static IPlatform GetPlatform()
        {
            return new GlfwPlatform();
        }

        public static IReadOnlyCollection<IRenderer> GetRenderers()
        {
            return _renderers;
        }

        internal static void InitializeRenderers(Window window)
        {
            _renderers[0] = new ClearRenderer(window);
            _renderers[1] = new EnemyRenderer();
            _renderers[2] = new TowerRenderer();
            _renderers[3] = new ProjectileRenderer();
        }
    }
}
