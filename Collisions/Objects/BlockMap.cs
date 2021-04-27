using CollisionsGame.Objects;
using GameLibrary.AppObjects;
using GameLibrary.Extensions;
using GameLibrary.GameObjects;
using GameLibrary.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Collisions.Objects
{
    public class BlockMap : GameAgentObject
    {
        private bool configureComplete = false;
        private List<GameBlock> gamesBlocks = new List<GameBlock>();
        private SpriteBatch _spriteBatch;

        public Dimensions BlockSize { get; }

        public BlockMap(SpriteBatch spriteBatch, Point StartPoint, Dimensions blockSize) : base(StartPoint)
        {
            this._spriteBatch = spriteBatch;
            BlockSize = blockSize;
        }

        private void Configuremap()
        {
            if (!this.configureComplete)
            {
                var blocksArray = new[]
                {
                this._spriteBatch.CreateFilledRectTexture(BlockSize, Color.DarkRed),
                this._spriteBatch.CreateFilledRectTexture(BlockSize, Color.Green),
                this._spriteBatch.CreateFilledRectTexture(BlockSize, Color.DarkGoldenrod),
                this._spriteBatch.CreateFilledRectTexture(BlockSize, Color.Purple),
                this._spriteBatch.CreateFilledRectTexture(BlockSize, Color.Pink),
                this._spriteBatch.CreateFilledRectTexture(BlockSize, Color.BurlyWood),
                };
                Random rnd = new Random();
                this.gamesBlocks = new List<GameBlock>();
                var topPos = 180;

                for (var x = 0; x < 6; ++x)
                {
                    var leftPos = 75;
                    for (var y = 0; y < 15; ++y)
                    {
                        var blkIdx = rnd.Next(0, blocksArray.Length);
                        this.gamesBlocks.Add(new GameBlock(this._spriteBatch, blocksArray[blkIdx], new Point(leftPos, topPos), BlockSize, 10f));
                        leftPos += BlockSize.Width + 5;
                    }

                    topPos += BlockSize.Height + 5;
                }


            }

            this.configureComplete = true;
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (var block in this.gamesBlocks)
            {
                block.Draw(gameTime);
            }
        }

        public void AgentCollisions(IInteractiveGameObject agent)
        {
            var results = new List<GameBlock>();
            foreach (var block in this.gamesBlocks)
                if (GameLibrary.AppObjects.Collisions.AABBStruck(block.Area, agent.Area))
                    results.Add(block);

            foreach (var item in results)
            {
                item.Hit(5);
            }
        }

        public override void Update(float mlSinceupdate, World theState)
        {
            this.Configuremap();
            // find all blocks to remove
            var removeMe = new List<GameBlock>();
            foreach (var item in gamesBlocks)
            {
                if (item.Health < 1f) { removeMe.Add(item); }
            }

            foreach (var del in removeMe)
            {
                gamesBlocks.Remove(del);
            }
        }

        internal List<(GameBlock game, T interactive)> InteractiveObjectCollisions<T>(IList<T> interactingObjects) where T : IInteractiveGameObject
        {

            List<(GameBlock game, T interactive)> results = new List<(GameBlock game, T interactive)>();
            foreach (var block in this.gamesBlocks)
                foreach (var bullet in interactingObjects)
                    if (GameLibrary.AppObjects.Collisions.AABBStruck(block.Area, bullet.Area))
                    {
                        results.Add((block, bullet));
                        //bullet.Struck();
                        //block.Hit();
                        break;
                    }
            return results;
        }

        internal void ObjectCollisions(IList<BaseBullet> firedBullets)
        {
            foreach (var block in this.gamesBlocks)
                foreach (var bullet in firedBullets)
                    if (GameLibrary.AppObjects.Collisions.AABBStruck(block.Area, bullet.Area))
                    {
                        bullet.Struck();
                        block.Hit(2);
                        break;
                    }
        }
    }
}
