using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefense.Common
{
    public interface IPlatform : IDisposable
    {
        void InitializeRendering();
        
        void PollInput();
    }
}
