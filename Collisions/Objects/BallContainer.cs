﻿using Collisions.Objects.Balls;
using GameLibrary.AppObjects;
using GameLibrary.GameObjects;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Collisions.Objects
{
    class BallContainer : GameAgentObject
    {
        private readonly Point startPosition;
        private readonly BallFactory ballMaker;

        List<BaseBall> balls { get; set; }
        public List<BaseBall> Balls => this.balls;
        
        public BallContainer(Point startPosition, IEnumerable<BaseBall> StartingBalls, BallFactory ballMaker) : base(startPosition)
        {
            this.startPosition = startPosition;
            this.balls = new List<BaseBall>(StartingBalls);
            this.ballMaker = ballMaker;
        }

        public void CreateBasicBall(Vector2 direction, float speed, Point startPos)
        {
            var newBall = this.ballMaker.NewBall("BaseBall", direction, speed, startPos);
            this.Balls.Add(newBall);
        }

        public override void Draw(GameTime gameTime)
        {
            foreach(var ball in balls)
            {
                ball.Draw(gameTime);
            }
        }

        public override void Update(float mlSinceupdate, World theState)
        {
            var removeBalls = new List<BaseBall>();
            foreach (var ball in balls)
            {
                // Are we off the screen horizontal
                if (ball.CurrentPosition.X < 0f || ball.CurrentPosition.X > theState.ViewPort.Width)
                    removeBalls.Add(ball);
                else if (ball.CurrentPosition.Y < 0f || ball.CurrentPosition.Y > theState.ViewPort.Height)
                    removeBalls.Add(ball);
            }

            foreach (var ball in removeBalls) 
                balls.Remove(ball);

            foreach (var ball in balls)
            {
                ball.Update(mlSinceupdate);
            }
        }

        internal List<BaseBall> AgentCollisions(GameAgentObject agent)
        {
            var results = new List<BaseBall>();
            foreach (var ball in this.balls)
                if (GameLibrary.AppObjects.Collisions.AABBStruck(ball.Area, agent.Area))
                    results.Add(ball);

            return results;
        }
    }
}
