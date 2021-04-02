using Microsoft.Xna.Framework;

namespace GameLibrary.Interfaces
{
    // Used for all the assets inside a 'proper'
    // IDrawing IUpdateable thingy.
    public interface IDrawableGameObject
    {
        void Draw(GameTime gameTime);
    }

    public interface IGameContainerDrawing :IGameContainerUpdate
    {
        void Draw();
    }

}
