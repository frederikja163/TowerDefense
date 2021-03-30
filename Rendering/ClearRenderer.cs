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
            GL.Enable(EnableCap.AlphaTest);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
        }

        public void Render(in GameData lastTick, in GameData nextTick, float percentage)
        {
            GL.Viewport(0, 0, _window.Width, _window.Height);
            GL.Clear(ClearBufferMask.ColorBufferBit);
        }
    }
}