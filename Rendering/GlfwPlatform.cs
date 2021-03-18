using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Windowing.GraphicsLibraryFramework;
using TowerDefense.Common;

namespace TowerDefense.Platform
{
    internal sealed class GlfwPlatform : IPlatform
    {
        private readonly Window _window;

        public GlfwPlatform()
        {
            if (!GLFW.Init())
            {
                throw new Exception("GLFW failed to initialize.");
            }

            _window = new Window();
        }

        public void PollInput()
        {
            GLFW.PollEvents();
        }

        public void Dispose()
        {
            GLFW.Terminate();
        }
    }
}
