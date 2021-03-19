using System;
using OpenTK.Graphics.OpenGL;
using TowerDefense.Common;
using TowerDefense.Platform.Extensions;
using TowerDefense.Platform.Rendering.OpenGL;

namespace TowerDefense.Platform.Rendering
{
    public sealed class Rect
    {
        private static BufferObject<float> Vbo;
        private static BufferObject<uint> Ebo;
        private static VertexArray Vao;
        private static ShaderProgram Shader;
        private static int ModelLocation;

        static Rect()
        {
            Vbo = new BufferObject<float>(
                new[] {
                    0.5f,  0.5f,
                    0.5f, -0.5f,
                    -0.5f, -0.5f,
                    -0.5f,  0.5f,
                });

            Ebo = new BufferObject<uint>(new uint[]
            {
                0, 1, 2,
                0, 2, 3
            });

            using VertexShader vertex = new VertexShader(@"
            #version 330 core
            in vec2 vPosition;

            uniform mat4 uModel;

            void main()
            {
                gl_Position = (uModel * vec4(vPosition, 0, 1)) * vec4(2, 2, 1, 1) + vec4(-1, -1, 0, 0);
            }");

            using FragmentShader fragment = new FragmentShader(@"
            #version 330 core
            out vec4 Color;

            void main()
            {
                Color = vec4(1, 1, 1, 1);
            }
            ");
            Shader = new ShaderProgram(vertex, fragment);
            
            ModelLocation = Shader.GetUniformLocation("uModel");

            Vao = new VertexArray(Ebo);
            Vao.AddVertexBuffer(Vbo, sizeof(float) * 2);
            Vao.AddVertexAttribute(Vbo, Shader.GetAttributeLocation("vPosition"), 2, VertexAttribType.Float, 0);
        }
        
        public Transform2D Transform { get; }
        
        public Rect()
        {
            Transform = new Transform2D();
        }

        public void Render()
        {
            Shader.SetUniform(ModelLocation, Transform.GetMatrix());
            
            Shader.Bind();
            Vao.Bind();
            
            GL.DrawElements(PrimitiveType.Triangles, 6, DrawElementsType.UnsignedInt, IntPtr.Zero);
        }
    }
}