using System;
using TowerDefense.Common;

namespace TowerDefense
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            Application app = new Application();
            app.Run();
            app.Exit();
        }
    }
}