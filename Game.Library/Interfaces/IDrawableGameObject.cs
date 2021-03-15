using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Interfaces
{
    // Used for all the assets inside a 'proper'
    // IDrawing IUpdateable thingy.
    public interface IDrawableGameObject : IUpdateableGameObject
    {
        void Draw(GameTime gameTime);
    }

    public interface IGameContainerDrawing :IGameContainerUpdate
    {
        void Draw();
    }
}
