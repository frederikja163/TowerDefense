using OpenTK.Graphics.OpenGL;
using TowerDefense.Common;
using TowerDefense.Common.Game;
using TowerDefense.Platform.Glfw;

namespace TowerDefense.Platform
{
    internal sealed class ClearRenderer : IRenderer
    {
        private readonly Window _window;

        public ClearRenderer(Window window)
        {
            _window = window;
        }

        public void Render(in GameData game)
        {
            GL.Viewport(0, 0, _window.Width, _window.Height);
            GL.Clear(ClearBufferMask.ColorBufferBit);
        }
    }
}