using GameLibrary.AppObjects;
using GameLibrary.Interfaces;
using Microsoft.Xna.Framework;

namespace GameLibrary.GameObjects
{
    public abstract class GameAgentObject : IInteractiveGameObject, IUpdateableGameAgent, IDrawableGameObject
    {
        internal Point _currentPosition;
        
        public Point CurrentPosition => _currentPosition;
        public virtual Rectangle Area { get;  set; }


        public GameAgentObject(Point startPosition)
        {
            this._currentPosition = startPosition;
        }

        public virtual void SetCurrentPosition(Point position)
        {
            if (_currentPosition != position) _currentPosition = position;
        }

        public abstract void Draw(GameTime gameTime);


        public abstract void Update(float mlSinceupdate, World theState);
    }
}
