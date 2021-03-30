using System;
using System.Collections.Generic;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using GlfwWindow = OpenTK.Windowing.GraphicsLibraryFramework.Window;

namespace TowerDefense.Platform.Glfw
{
    public delegate void MouseButtonEvent();
    
    internal sealed class Mouse
    {
        public sealed class Button
        {
            public event MouseButtonEvent? Pressed;
            public event MouseButtonEvent? Released;
            public bool IsDown { get; set; }

            internal void OnPress()
            {
                Pressed?.Invoke();
            }

            internal void OnRelease()
            {
                Released?.Invoke();
            }
        }
        
        private readonly Dictionary<MouseButton, Button> _mouseButtons;
        private readonly Window _window;
        private readonly GLFWCallbacks.MouseButtonCallback _mouseButtonCallback;
        private readonly GLFWCallbacks.CursorPosCallback _cursorPosCallback;
        private readonly GLFWCallbacks.ScrollCallback _scrollCallback;

        internal Mouse(Window window)
        {
            _window = window;
            
            Array mouseButtonValues = Enum.GetValues(typeof(MouseButton));
            _mouseButtons = new Dictionary<MouseButton, Button>(mouseButtonValues.Length);
            foreach (MouseButton value in mouseButtonValues)
            {
                _mouseButtons.TryAdd(value, new Button());
            }
            
            unsafe
            {
                _mouseButtonCallback = MouseButtonCallback;
                _cursorPosCallback = CursorPosCallback;
                _scrollCallback = ScrollCallback;
                
                GLFW.SetMouseButtonCallback(window.Handle, _mouseButtonCallback);
                GLFW.SetCursorPosCallback(window.Handle, _cursorPosCallback);
                GLFW.SetScrollCallback(window.Handle, _scrollCallback);
            }
        }
        
        public delegate void MouseMovedEvent(Vector2 newPosition);
        public event MouseMovedEvent? MouseMoved;
        public Vector2 Position { get; private set; }
        
        public delegate void MouseScrollEvent(Vector2 newScroll);
        public event MouseScrollEvent? MouseScroll; 
        public Vector2 Scroll { get; private set; }
        
        public Button this[MouseButton button] => _mouseButtons[button];

        private unsafe void MouseButtonCallback(GlfwWindow* window, MouseButton button, InputAction action, KeyModifiers mods)
        {
            Button mouseButton = _mouseButtons[button];
            if (action == InputAction.Press)
            {
                mouseButton.IsDown = true;
                mouseButton.OnPress();
            }
            else if (action == InputAction.Release)
            {
                mouseButton.IsDown = false;
                mouseButton.OnRelease();
            }
        }

        private unsafe void CursorPosCallback(GlfwWindow* window, double x, double y)
        {
            Vector2 oldPosition = Position;
            Vector2i windowSize = _window.Size;
            Position = new Vector2((float)x / windowSize.X, (float)y / -windowSize.Y + 1);
            MouseMoved?.Invoke(Position);
        }

        private unsafe void ScrollCallback(GlfwWindow* window, double offsetx, double offsety)
        {
            // TODO: Check if this is delta scroll or if its the total scroll.
            // For now we assume total scroll, like with the cursor position.
            Vector2 oldScroll = Scroll;
            Scroll = new Vector2((float) offsetx, (float) offsety);
            MouseScroll?.Invoke(Scroll);
        }
    }
}