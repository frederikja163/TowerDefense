using System;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using TowerDefense.Common;
using TowerDefense.Platform.Rendering;
using TowerDefense.Platform.Rendering.OpenGL;

namespace TowerDefense.Platform
{
    internal sealed class TestTriangleRenderer : IRenderer
    {
        private readonly Quad _quad;
        
        
        public TestTriangleRenderer()
        {
            _quad = new Quad();
            
            GL.ClearColor(1, 0, 1, 1);
        }
        
        
        public void Render(in GameData data)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            
            _quad.Render();
        }
    }
}