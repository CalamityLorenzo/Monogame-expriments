using CollisionsGame.Objects;
using GameLibrary.Models;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace CollisionsGame
{
    partial class CollisionsGame
    {
        //private List<GameBlock> gamesBlocks;

        //public void CreateGamesBlock(Viewport viewPort)
        //{
        //    this.gamesBlocks = new List<GameBlock>();
        //    // al experimental
        //    // create a bunch o blocks of a certain width height.
        //    // drop copies of them in to the gameblock with relvant spancing.
        //    var blockSize = new Dimensions(120, 47);

        //    var blockArray = new[]
        //    {
        //        this._spriteBatch.CreateFilledRectTexture(blockSize, Color.DarkRed),
        //        this._spriteBatch.CreateFilledRectTexture(blockSize, Color.Green),
        //        this._spriteBatch.CreateFilledRectTexture(blockSize, Color.DarkGoldenrod)
        //    };

        //    var topPos = 180;

        //    Random rnd = new Random();
        //    for (var x = 0; x < 4; ++x)
        //    {
        //        var leftPos = 75;
        //        for (var y = 0; y < 7; ++y)
        //        {
        //            var blkIdx = rnd.Next(0, 3);
        //            gamesBlocks.Add(new GameBlock(this._spriteBatch, blockArray[blkIdx], blkIdx == 0? "Remove" : "Bounce",  new Point(leftPos, topPos), blockSize));
        //            leftPos += blockSize.Width + 5;
        //        }

        //        topPos += blockSize.Height + 5;
        //    }
        //}


        private void setScreenData(ScreenData data)
        {
            this._graphics.PreferredBackBufferWidth = data.ScreenWidth;
            this._graphics.PreferredBackBufferHeight = data.ScreenHeight;
            this._graphics.IsFullScreen = data.FullScreen;
            this._graphics.ApplyChanges();
        }

        private IEnumerable<GameBlock> GameObjectCollisions(IEnumerable<GameBlock> gameObjects, BatContainer playerObject)
        {
            var results = new List<GameBlock>();
            foreach (var block in gameObjects)
                if (GameLibrary.AppObjects.Collisions.AABBStruck(block.Area, playerObject.Area))
                    results.Add(block);
            return results;
        }

        private void DrawCollisionMap()
        {
            // The rectangle is the point of 'boop' for the aabb thingy.
            foreach (var item in this.TheState.Map)
            {
                this._spriteBatch.Draw(this.RedSquare, item, Color.White);
            }

            foreach (var item in this.boundaryMap.ViewPortCollisions)
            {
                this._spriteBatch.Draw(this.GreenSquare, item, Color.White);
            }
        }

    }
}
