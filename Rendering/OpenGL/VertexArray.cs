using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;

namespace TowerDefense.Platform.OpenGL
{
    internal sealed class VertexArray : IDisposable
    {
        private readonly Dictionary<BufferObject, uint> _vbos;
        private uint _vboCount;
        internal readonly uint Handle;

        public VertexArray(BufferObject<uint> ebo) : this()
        {
            GL.VertexArrayElementBuffer(Handle, ebo.Handle);
        }

        public VertexArray()
        {
            Handle = GL.CreateVertexArray();
            _vbos = new Dictionary<BufferObject, uint>();
        }

        public void AddVertexAttribute(BufferObject buffer, VertexAttribType type, int attributeLocation, int size, int stride, int offset = 0)
        {
            if (!_vbos.TryGetValue(buffer, out uint bindingIndex))
            {
                bindingIndex = ++_vboCount;
                GL.VertexArrayVertexBuffer(Handle, bindingIndex, buffer.Handle, (IntPtr)offset, stride);
                _vbos.Add(buffer, bindingIndex);
            }
            
            GL.VertexArrayAttribBinding(Handle, (uint)attributeLocation, bindingIndex);
            GL.VertexArrayAttribFormat(Handle, (uint)attributeLocation, size, type, false, (uint)offset);
            GL.EnableVertexArrayAttrib(Handle, (uint)attributeLocation);
        }

        public void Bind()
        {
            GL.BindVertexArray(Handle);
        }
        
        public void Dispose()
        {
            GL.DeleteVertexArray(Handle);
        }
    }
}