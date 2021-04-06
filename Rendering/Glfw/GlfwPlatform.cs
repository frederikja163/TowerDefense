using System;
using System.Collections.Generic;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using TowerDefense.Common;
using TowerDefense.Common.Game;
using TowerDefense.Platform.Renderers;

namespace TowerDefense.Platform.Glfw
{
    public sealed class GlfwPlatform
    {
        private readonly Window _window;
        private readonly EnemyRenderer _enemyRenderer;
        private readonly TowerRenderer _towerRenderer;
        private readonly ProjectileRenderer _projectileRenderer;

        public GlfwPlatform(ActivityList activities)
        {
            if (!GLFW.Init())
            {
                throw new Exception("GLFW failed to initialize.");
            }
            _window = new Window();
            Window.MakeCurrent(_window);
            GLLoader.LoadBindings(new GLFWBindingsContext());
            
            _enemyRenderer = new EnemyRenderer();
            _towerRenderer = new TowerRenderer();
            _projectileRenderer = new ProjectileRenderer();
            Window.MakeCurrent(null);
            
            ImplementActivities(activities);
        }

        private void ImplementActivities(ActivityList activities)
        {
            Keyboard keyboard = _window.Keyboard;
            Mouse mouse = _window.Mouse;
            
            keyboard[Keys.Escape].Pressed += activities[Activities.ExitApplication].Call;
            _window.WindowClosed += activities[Activities.ExitApplication].Call;
            
            mouse[MouseButton.Left].Released += activities[Activities.PlaceTower].Call;
            mouse[MouseButton.Left].Pressed += () => activities[MovementActivities.DragTower].Call(mouse.Position);
            mouse.MouseMoved += (position) =>
            {
                if (mouse[MouseButton.Left].IsDown)
                {
                    activities[MovementActivities.DragTower].Call(position);
                }
            };
        }

        public void InitializeRendering()
        {
            Window.MakeCurrent(_window);
            
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
        }

        public void Render(GameData lastTick, GameData nextTick, float t)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Viewport(0, 0, _window.Width, _window.Height);
            RenderingData data = new RenderingData(lastTick, nextTick, t);
            
            _enemyRenderer.Render(data);
            _towerRenderer.Render(data);
            _projectileRenderer.Render(data);
        }

        public void PollInput()
        {
            GLFW.PollEvents();
        }

        public void SwapBuffers()
        {
            _window.SwapBuffers();
        }

        public void Dispose()
        {
            GLFW.Terminate();
        }
    }
}
