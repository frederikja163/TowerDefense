using System;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using GlfwWindow = OpenTK.Windowing.GraphicsLibraryFramework.Window;

namespace TowerDefense.Platform.Glfw
{
    internal sealed class Window : IDisposable
    {
        internal readonly unsafe GlfwWindow* Handle;
        private readonly GLFWCallbacks.WindowCloseCallback _windowCloseCallback;
        
        public Window()
        {
            unsafe
            {
                Handle = GLFW.CreateWindow(800, 600, "Tower Defense", null, null);
                if (Handle == null)
                {
                    throw new Exception("Window failed to create.");
                }
                
                Keyboard = new Keyboard(this);
                Mouse = new Mouse(this);

                _windowCloseCallback = WindowCloseCallback;
                GLFW.SetWindowCloseCallback(Handle, _windowCloseCallback);
            }
        }

        private unsafe void WindowCloseCallback(GlfwWindow* window)
        {
            WindowClosed?.Invoke();
        }

        public delegate void WindowCloseEvent();
        public event WindowCloseEvent? WindowClosed;
        
        public Keyboard Keyboard { get; }
        public Mouse Mouse { get; }

        public void MakeCurrent()
        {
            unsafe
            {
                GLFW.MakeContextCurrent(Handle);
            }
        }

        public void SwapBuffers()
        {
            unsafe
            {
                GLFW.SwapBuffers(Handle);
            }
        }

        public Vector2i Size
        {
            get
            {
                unsafe
                {
                    GLFW.GetWindowSize(Handle, out int width, out int height);
                    return new Vector2i(width, height);
                }
            }
            set
            {
                unsafe
                {
                    GLFW.SetWindowSize(Handle, value.X, value.Y);
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
                GLFW.DestroyWindow(Handle);
            }
        }
    }
}
