using System;
using OpenTK.Graphics.OpenGL;

namespace TowerDefense.Platform.Rendering.OpenGL
{
    internal abstract class BufferObject : IDisposable
    {
        internal readonly uint Handle;

        public BufferObject()
        {
            Handle = GL.CreateBuffer();
        }
        
        public void Dispose()
        {
            GL.DeleteBuffer(Handle);
        }
    }
    
    internal sealed class BufferObject<T> : BufferObject
        where T : unmanaged
    {
        public BufferObject(in T[] data) : base()
        {
            GL.NamedBufferStorage<T>(Handle, data, BufferStorageMask.DynamicStorageBit);
        }
    }
}