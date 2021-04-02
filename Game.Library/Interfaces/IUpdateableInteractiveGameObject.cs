using GameLibrary.AppObjects;

namespace GameLibrary.Interfaces
{
    interface IUpdateableInteractiveGameObject
    {
        public void Update(float mlSinceLastUpdate, World theState);
    }
}
