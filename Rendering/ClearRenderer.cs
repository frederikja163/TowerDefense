using OpenTK.Graphics.OpenGL;
using TowerDefense.Common;
using TowerDefense.Common.Game;

namespace TowerDefense.Platform
{
    internal sealed class ClearRenderer : IRenderer
    {
        public void Render(in GameData game)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
        }
    }
}