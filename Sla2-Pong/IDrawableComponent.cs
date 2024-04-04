using System;

namespace Sla2Pong
{
    // Interface for the drawable components, with Draw, Clear and Update methods.
    public interface IDrawableComponent
    {      
        void Draw();
        void Clear();
        void Update();
    }
}

