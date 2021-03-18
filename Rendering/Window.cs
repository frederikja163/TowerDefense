using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefense.Platform
{
    internal sealed class Window : IDisposable
    {

        public void MakeCurrent()
        {

        }

        public void SwapBuffers()
        {

        }

        public Vector2i Size { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }

        public float AspectRatio { get; }

        public void Dispose()
        {
        }
    }
}
