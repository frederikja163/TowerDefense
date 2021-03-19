using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics;
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

        public void InitializeRendering()
        {
            _window.MakeCurrent();
            GLLoader.LoadBindings(new GLFWBindingsContext());
            
            Platformer.InitializeRenderers();
        }

        public void PollInput()
        {
            // TODO: This should not be called from poll input.
            _window.SwapBuffers();
            
            GLFW.PollEvents();
        }

        public void Dispose()
        {
            GLFW.Terminate();
        }
    }
}
