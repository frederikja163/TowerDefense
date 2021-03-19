using System;
using System.Collections.Generic;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using GlfwWindow = OpenTK.Windowing.GraphicsLibraryFramework.Window;
using GlfwButton = OpenTK.Windowing.GraphicsLibraryFramework.MouseButton;

namespace TowerDefense.Platform.Glfw
{
    public enum MouseButton
    {
        Left = 0,
        Right = 1,
        Middle = 3,
        Button1 = 4,
        Button2 = 5,
        Button3 = 6,
        Button4 = 7,
    }
    
    internal sealed class Mouse
    {
        private readonly HashSet<MouseButton> _heldButtons;

        internal unsafe Mouse(Window window)
        {
            _heldButtons = new HashSet<MouseButton>();
            
            GLFW.SetMouseButtonCallback(window.Handle, MouseButtonCallback);
            GLFW.SetCursorPosCallback(window.Handle, CursorPosCallback);
            GLFW.SetScrollCallback(window.Handle, ScrollCallback);
        }

        public delegate void MouseButtonEvent(MouseButton button);
        public event MouseButtonEvent MouseButtonPressed;
        public event MouseButtonEvent MouseButtonReleased;

        public delegate void MouseMovedEvent(Vector2 newPosition, Vector2 oldPosition);
        public event MouseMovedEvent MouseMoved;
        public Vector2 Position { get; private set; }
        
        public delegate void MouseScrollEvent(Vector2 newScroll, Vector2 oldScroll);
        public event MouseScrollEvent MouseScroll; 
        public Vector2 Scroll { get; private set; }
        
        public bool this[MouseButton button] => _heldButtons.Contains(button);

        private unsafe void MouseButtonCallback(GlfwWindow* window, GlfwButton button, InputAction action, KeyModifiers mods)
        {
            MouseButton mouseButton = (MouseButton) button;
            if (action == InputAction.Press)
            {
                _heldButtons.Add(mouseButton);
                
                MouseButtonPressed?.Invoke(mouseButton);
            }
            else if (action == InputAction.Repeat)
            {
                _heldButtons.Remove(mouseButton);
                
                MouseButtonReleased?.Invoke(mouseButton);
            }
        }

        private unsafe void CursorPosCallback(GlfwWindow* window, double x, double y)
        {
            Vector2 oldPosition = Position;
            Position = new Vector2((float)x, (float)y);
            MouseMoved?.Invoke(Position, oldPosition);
        }

        private unsafe void ScrollCallback(GlfwWindow* window, double offsetx, double offsety)
        {
            // TODO: Check if this is delta scroll or if its the total scroll.
            // For now we assume total scroll, like with the cursor position.
            Vector2 oldScroll = Scroll;
            Scroll = new Vector2((float) offsetx, (float) offsety);
            MouseScroll?.Invoke(Scroll, oldScroll);
        }
    }
}