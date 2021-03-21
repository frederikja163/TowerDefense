using System;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using TowerDefense.Common;
using TowerDefense.Platform.Extensions;
using TowerDefense.Platform.Rendering.OpenGL;

namespace TowerDefense.Platform.Rendering
{
    internal sealed class Quad
    {
        private static BufferObject<float> Vbo;
        private static BufferObject<uint> Ebo;
        private static VertexArray Vao;
        private static ShaderProgram Shader;
        private static int ModelLocation;

        static Quad()
        {
            Vbo = new BufferObject<float>(
                new[] {
                    0.5f,  0.5f, 0,
                    0.5f, -0.5f, 0,
                    -0.5f, -0.5f, 0,
                    -0.5f,  0.5f, 0,
                });

            Ebo = new BufferObject<uint>(new uint[]
            {
                0, 1, 2,
                0, 2, 3
            });

            using VertexShader vertex = new VertexShader(@"
            #version 330 core
            in vec3 vPosition;

            uniform mat4 uModel;

            void main()
            {
                gl_Position = uModel * vec4(vPosition, 1);
            }");

            using FragmentShader fragment = new FragmentShader(@"
            #version 330 core
            out vec4 Color;

            uniform vec4 uColor;

            void main()
            {
                Color = uColor; // vec4(0.0f, 1.0f, 0.0f, 1.0f);
            }
            ");
            Shader = new ShaderProgram(vertex, fragment);
            
            ModelLocation = Shader.GetUniformLocation("uModel");

            Vao = new VertexArray(Ebo);
            Vao.AddVertexBuffer(Vbo, sizeof(float) * 3);
            Vao.AddVertexAttribute(Vbo, Shader.GetAttributeLocation("vPosition"), 3, VertexAttribType.Float, 0);
            Vao.AddVertexAttribute(Vbo, Shader.GetAttributeLocation("Color"), 2, VertexAttribType.Float, 0); // Nut work
        }
        
        public Transform3D Transform { get; }
        
        public Quad()
        {
            Transform = new Transform3D();
        }

        public void Render()
        {
            Shader.SetUniform(ModelLocation, Transform.GetMatrix());
            Shader.SetUniform(Shader.GetAttributeLocation("uColor"), new Vector4(0.0f, 1.0f, 0.0f, 1.0f)); // fix
            
            Shader.Bind();
            Vao.Bind();
            
            GL.DrawElements(PrimitiveType.Triangles, 6, DrawElementsType.UnsignedInt, IntPtr.Zero);
        }
    }
}