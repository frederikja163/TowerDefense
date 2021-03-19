using System;
using System.Collections.Generic;
using OpenTK.Graphics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using TowerDefense.Common;

namespace TowerDefense.Platform.Glfw
{
    internal sealed class GlfwPlatform : IPlatform
    {
        private readonly Window _window;
        private readonly Dictionary<Keys, Activity> _keysToActivities;

        public GlfwPlatform()
        {
            if (!GLFW.Init())
            {
                throw new Exception("GLFW failed to initialize.");
            }
            _keysToActivities = new Dictionary<Keys, Activity>();

            _window = new Window();
            
            _window.Keyboard.OnKeyPressed += KeyboardOnOnKeyPressed;
        }

        private void KeyboardOnOnKeyPressed(Keys key)
        {
            if (_keysToActivities.TryGetValue(key, out Activity? activity))
            {
                activity.Call();
            }
        }

        public void ImplementActivities(ActivityList activities)
        {
            _keysToActivities.Add(Keys.Escape, activities[Activities.ExitApplication]);
            _window.WindowClosed += activities[Activities.ExitApplication].Call;
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
