using System;
using System.IO;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using Vector2 = System.Numerics.Vector2;

namespace TowerDefense.Platform.OpenGL
{
    internal abstract class ShaderBase : IDisposable
    {
        internal readonly uint Handle;
        
        public ShaderBase(string src, ShaderType type)
        {
            Handle = GL.CreateShader(type);
            GL.ShaderSource(Handle, src);
            GL.CompileShader(Handle);
            int infoLogLength = 0;
            GL.GetShaderiv(Handle, ShaderParameterName.InfoLogLength, ref infoLogLength);
            if (infoLogLength > 0)
            {
                int logLength = 0;
                string infoLog = GL.GetShaderInfoLog(Handle, infoLogLength, ref logLength);
                throw new Exception($"{type} shader failed to compile: {infoLog}");
            }
        }
        
        public void Dispose()
        {
            GL.DeleteShader(Handle);
        }
    }

    internal sealed class VertexShader : ShaderBase
    {
        public VertexShader(StreamReader stream) : this(stream.ReadToEnd())
        {
        }
        
        public VertexShader(string src) : base(src, ShaderType.VertexShader)
        {
        }
    }
    
    internal sealed class FragmentShader : ShaderBase
    {
        public FragmentShader(StreamReader stream) : this(stream.ReadToEnd())
        {
        }
        
        public FragmentShader(string src) : base(src, ShaderType.FragmentShader)
        {
        }
    }
    
    internal sealed class ShaderProgram : IDisposable
    {
        internal uint Handle;

        public ShaderProgram()
        {
            Handle = GL.CreateProgram();
        }

        public void Link(params ShaderBase[] shaders)
        {
            foreach (ShaderBase shader in shaders)
            {
                GL.AttachShader(Handle, shader.Handle);
            }
            
            GL.LinkProgram(Handle);
            int infoLogLength = 0;
            GL.GetProgramiv(Handle, ProgramPropertyARB.InfoLogLength, ref infoLogLength);
            if (infoLogLength > 0)
            {
                int logLength = 0;
                string infoLog = GL.GetProgramInfoLog(Handle, infoLogLength, ref logLength);
                throw new Exception($"Program link error {infoLog}");
            }
            
            foreach (ShaderBase shader in shaders)
            {
                GL.DetachShader(Handle, shader.Handle);
            }
        }

        public void Bind()
        {
            GL.UseProgram(Handle);
        }

        public int GetAttributeLocation(string attributeName)
        {
            return GL.GetAttribLocation(Handle, attributeName);
        }
        public int GetUniformLocation(string uniformName)
        {
            return GL.GetUniformLocation(Handle, uniformName);
        }

        public void SetUniform(int location, float value) => GL.ProgramUniform1f(Handle, location, value);
        public void SetUniform(int location, int value) => GL.ProgramUniform1i(Handle, location, value);
        
        public void SetUniform(int location, Vector2 value) => GL.ProgramUniform2f(Handle, location, value.X, value.Y);
        public void SetUniform(int location, Vector2i value) => GL.ProgramUniform2i(Handle, location, value.X, value.Y);
        
        public void SetUniform(int location, Vector3 value) => GL.ProgramUniform3f(Handle, location, value.X, value.Y, value.Z);
        public void SetUniform(int location, Vector3i value) => GL.ProgramUniform3i(Handle, location, value.X, value.Y, value.Z);
        
        public void SetUniform(int location, Vector4 value) => GL.ProgramUniform4f(Handle, location, value.X, value.Y, value.Z, value.W);
        public void SetUniform(int location, Vector4i value) => GL.ProgramUniform4i(Handle, location, value.X, value.Y, value.Z, value.W);
        
        public void SetUniform(int location, Matrix4 value) => GL.ProgramUniformMatrix4fv(Handle, location, 1, false, value.M11);
        
        public void Dispose()
        {
            GL.DeleteShader(Handle);
        }
    }
}