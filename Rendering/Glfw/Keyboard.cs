using System;
using System.Collections.Generic;
using OpenTK.Windowing.GraphicsLibraryFramework;
using TowerDefense.Common;
using GlfwWindow = OpenTK.Windowing.GraphicsLibraryFramework.Window;

namespace TowerDefense.Platform.Glfw
{
    internal sealed class Keyboard
    {
        private HashSet<Keys> _keyState = new HashSet<Keys>();

        internal unsafe Keyboard(GlfwWindow* handle)
        {
            GLFW.SetKeyCallback(handle, KeyCallback);
        }
        
        public delegate void KeyEvent(Keys key);
        public event KeyEvent OnKeyPressed;
        public event KeyEvent OnKeyReleased;
        
        public bool this[Keys key] => _keyState.Contains(key);
        
        private unsafe void KeyCallback(GlfwWindow* window, Keys key, int code, InputAction action, KeyModifiers mods)
        {
            switch (action)
            {
                case InputAction.Press:
                    _keyState.Add(key);
                    OnKeyPressed?.Invoke(key);
                    break;
                case InputAction.Release:
                    _keyState.Remove(key);
                    OnKeyReleased?.Invoke(key);
                    break;
                default:
                    break;
            }
        }
    }
}