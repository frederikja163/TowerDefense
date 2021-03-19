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
        private readonly Dictionary<Keys, Activity> _keysToActivities;
        private MovementActivity _towerPlaceActivity;

        public GlfwPlatform()
        {
            if (!GLFW.Init())
            {
                throw new Exception("GLFW failed to initialize.");
            }
            _keysToActivities = new Dictionary<Keys, Activity>();

            _window = new Window();
            
            _window.Keyboard.OnKeyPressed += KeyboardOnKeyPressed;
            _window.Mouse.MouseButtonPressed += OnMouseButtonPressed;
        }

        private void OnMouseButtonPressed(MouseButton button)
        {
            if (button == MouseButton.Left)
            {
                _towerPlaceActivity.Call(new Vector2(_window.Mouse.Position.X / _window.Size.X,
                    _window.Mouse.Position.Y / -_window.Size.Y + 1));
            }
        }

        private void KeyboardOnKeyPressed(Keys key)
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
            
            _towerPlaceActivity = activities[MovementActivities.PlaceTower];
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
