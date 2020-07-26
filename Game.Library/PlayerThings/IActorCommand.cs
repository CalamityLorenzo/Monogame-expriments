using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameLibrary.Character
{
    public interface IActorCommand<T>
    {
        public void Execute(T actor);
    }
}
