using System;
using System.Collections.Generic;
using OpenTK.Graphics;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using TowerDefense.Common;

namespace TowerDefense.Platform.Glfw
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

        public void ImplementActivities(ActivityList activities)
        {
            Keyboard keyboard = _window.Keyboard;
            Mouse mouse = _window.Mouse;
            
            keyboard[Keys.Escape].Pressed += activities[Activities.ExitApplication].Call;
            _window.WindowClosed += activities[Activities.ExitApplication].Call;
            
            mouse[MouseButton.Left].Released += activities[Activities.PlaceTower].Call;
            mouse[MouseButton.Left].Pressed += activities[Activities.BeginTower].Call;
            mouse.MouseMoved += (position) => activities[MovementActivities.DragTower].Call(position);
        }

        public void InitializeRendering()
        {
            _window.MakeCurrent();
            GLLoader.LoadBindings(new GLFWBindingsContext());
            
            Platformer.InitializeRenderers(_window);
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
