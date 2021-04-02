using GameLibrary.InputManagement;
using GameLibrary.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace GameLibrary.AppObjects
{

    /// <summary>
    /// Snapshot per update about the state of the world.
    /// Inputs, and objects worth bumping in to.
    /// </summary>
    public struct World
    {
        public CurrentInputState InputState { get; set; }
        public Vector2 ScreenResolution { get; set; }
        public Viewport ViewPort { get; set; }
        public IList<IInteractiveGameObject> InteractiveItems { get; set; }
        public IList<Rectangle> Map { get; set; }
        public Rectangle? MapCollision(Rectangle target) => Collisions.AABBCollisions(Map, target);
    }
}
