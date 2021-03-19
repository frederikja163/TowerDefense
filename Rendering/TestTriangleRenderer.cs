using System;
using OpenTK.Graphics.OpenGL;
using TowerDefense.Common;
using TowerDefense.Platform.Rendering.OpenGL;

namespace TowerDefense.Platform
{
    internal sealed class TestTriangleRenderer : IRenderer
    {
        public static readonly string VertexSrc = @"#version 330 core
in vec2 vPosition;

void main()
{
    gl_Position = vec4(vPosition / 2, 0, 1);
}";
        public static readonly string FragmentSrc = @"#version 330 core
out vec4 Color;

void main()
{
    Color = vec4(0, 1, 1, 1);
}";

        private static readonly uint[] Indices = new uint[]
        {
            0, 1, 2
        };

        private static readonly float[] Vertices = new float[]
        {
            0, 1,
            1, 1,
            0, 0,
        };

        private readonly VertexArray _vao;
        private readonly BufferObject<float> _vbo;
        private readonly BufferObject<uint> _ebo;
        private readonly ShaderProgram _shader;
        
        
        public TestTriangleRenderer()
        {
            using VertexShader vertex = new VertexShader(VertexSrc);
            using FragmentShader fragment = new FragmentShader(FragmentSrc);

            _shader = new ShaderProgram(vertex, fragment);

            _vbo = new BufferObject<float>(Vertices);
            _ebo = new BufferObject<uint>(Indices);
            
            _vao = new VertexArray(_ebo);
            
            int positionLoc = _shader.GetAttributeLocation("vPosition");
            _vao.AddVertexBuffer(_vbo, 2 * sizeof(float));
            _vao.AddVertexAttribute(_vbo, positionLoc, 2, VertexAttribType.Float);
            
            GL.ClearColor(1, 0, 1, 1);
        }
        
        
        public void Render(in GameData data)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            
            _vao.Bind();
            _shader.Bind();
            
            GL.DrawElements(PrimitiveType.Triangles, Indices.Length, DrawElementsType.UnsignedInt, IntPtr.Zero);
        }
    }
}