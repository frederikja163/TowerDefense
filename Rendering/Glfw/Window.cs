using System;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using GlfwWindow = OpenTK.Windowing.GraphicsLibraryFramework.Window;

namespace TowerDefense.Platform.Glfw
{
    internal sealed class Window : IDisposable
    {
        private readonly unsafe GlfwWindow* _handle;
        
        public Window()
        {
            unsafe
            {
                _handle = GLFW.CreateWindow(800, 600, "Tower Defensé", null, null);

                if (_handle == null)
                {
                    throw new Exception("Window failed to create.");
                }
            }
        }

        public void MakeCurrent()
        {
            unsafe
            {
                GLFW.MakeContextCurrent(_handle);
            }
        }

        public void SwapBuffers()
        {
            unsafe
            {
                GLFW.SwapBuffers(_handle);
            }
        }

        public Vector2i Size
        {
            get
            {
                unsafe
                {
                    GLFW.GetWindowSize(_handle, out int width, out int height);
                    return new Vector2i(width, height);
                }
            }
            set
            {
                unsafe
                {
                    GLFW.SetWindowSize(_handle, value.X, value.Y);
                }
            }
        }

        public int Width 
        {
            get
            {
                return Size.X;
            }
            set
            {
                Size = new Vector2i(value, Height);
            }
        }

        public int Height
        {
            get
            {
                return Size.Y;
            }
            set
            {
                Size = new Vector2i(Width, value);
            }
        }

        public float AspectRatio
        {
            get
            {
                return ((float)Width) / Height;
            }
        }

        public void Dispose()
        {
            unsafe
            {
                GLFW.DestroyWindow(_handle);
            }
        }
    }
}
