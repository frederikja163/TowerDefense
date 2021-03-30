using System;
using System.Collections.Generic;
using OpenTK.Windowing.GraphicsLibraryFramework;
using TowerDefense.Common;
using GlfwWindow = OpenTK.Windowing.GraphicsLibraryFramework.Window;

namespace TowerDefense.Platform.Glfw
{
    public delegate void KeyboardKeyEvent();
    internal sealed class Keyboard
    {
        public sealed class Key
        {
            public bool IsDown { get; private set; }
        
            public event KeyboardKeyEvent? Pressed;
            public event KeyboardKeyEvent? Released;

            internal void OnPress()
            {
                IsDown = true;
                Pressed?.Invoke();
            }

            internal void OnRelease()
            {
                IsDown = false;
                Released?.Invoke();
            }
        }
        
        private Dictionary<Keys, Key> _keys;
        private readonly Window _window;
        private readonly GLFWCallbacks.KeyCallback _keyCallback;

        internal Keyboard(Window window)
        {
            _window = window;
            
            Array keyValues = Enum.GetValues(typeof(Keys));
            _keys = new Dictionary<Keys, Key>(keyValues.Length);
            foreach (Keys value in keyValues)
            {
                _keys.TryAdd(value, new Key());
            }
            
            unsafe
            {
                _keyCallback = KeyCallback;
                GLFW.SetKeyCallback(window.Handle, _keyCallback);
            }
        }
        
        public Key this[Keys key] => _keys[key];
        
        private unsafe void KeyCallback(GlfwWindow* window, Keys keyRaw, int code, InputAction action, KeyModifiers mods)
        {
            Key key = _keys[keyRaw];
            if (action == InputAction.Press)
            {
                key.OnPress();
            }
            else if (action == InputAction.Release)
            {
                key.OnRelease();
            }
        }
    }
}