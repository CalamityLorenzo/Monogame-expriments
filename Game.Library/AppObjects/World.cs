using GameLibrary.InputManagement;
using Microsoft.Xna.Framework;

namespace GameLibrary.AppObjects
{

    /// <summary>
    /// Snapshot per update about the state of the world.
    /// Inputs, and objects worth bumping in to.
    /// </summary>
    public class World
    {
        public CurrentInputState InputState { get; set; }
        public Rectangle ScreenResolution { get; set; }
    }
}
