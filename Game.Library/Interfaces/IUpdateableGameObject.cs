using GameLibrary.AppObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameLibrary.Interfaces
{
    // unlike monogame object we only pass the detla times
    // not the full time.
    public interface IUpdateableGameObject
    {
        void Update(float mlSinceupdate);
    }

    public interface IUpdateableGameAgent
    {
        void Update(float mlSinceupdate, World theState);
    }

    public interface IGameContainerUpdate
    {
        void Update(GameTime time, KeyboardState keystate, GamePadState padState);
    }
}
