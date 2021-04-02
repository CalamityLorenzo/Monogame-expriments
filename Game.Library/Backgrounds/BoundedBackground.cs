using GameLibrary.AppObjects;
using GameLibrary.Extensions;
using GameLibrary.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace GameLibrary.Drawing.Backgrounds
{
    /// <summary>
    /// Draws a background of a certain size, 
    /// but does not repeat it (hence bounde
    /// </summary>
    public class BoundedBackground
    {
        private readonly SpriteBatch spriteBatch;
        private Texture2D sprite;
        private Rectangle[] atlasRects;
        private List<int> map;
        private Dimensions tileDimensions;
        private Rectangle bounds;
        private readonly Rotator rotator;
        private readonly IVelocinator velocityManager;
        private Vector2 _currentPosition;
        private List<DisplayRectInfo> _sourceRects;
        private Vector2 _previousPosition;
        private Viewport viewport;
        private Dimensions mapDimensions;

        public BoundedBackground(SpriteBatch spriteBatch, Texture2D sprite, Rectangle[] atlasRects, List<int> map, Dimensions tileDimensions, Rectangle bounds, Rotator rotator, IVelocinator velocityManager, Vector2 backgroundStartPos, Viewport viewPort)
        {
            this.spriteBatch = spriteBatch;
            this.sprite = sprite;
            this.atlasRects = atlasRects;
            this.map = map;
            this.tileDimensions = tileDimensions;
            this.bounds = bounds;
            this.rotator = rotator;
            this.velocityManager = velocityManager;
            this._currentPosition = backgroundStartPos;
            this._previousPosition = Vector2.Add(_currentPosition, Vector2.One);
            this.viewport =  viewPort;
            this.viewport.Width += tileDimensions.Width;
            this.viewport.Height += tileDimensions.Height;
            // expresses the column/Rows in block count (How many rows, with how many cols)
            this.mapDimensions = new Dimensions(bounds.Width / tileDimensions.Width, bounds.Height / tileDimensions.Height);
        }

        public void Update(GameTime gameTime)
        {
            var delta = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            var currentDirection = GeneralExtensions.UnitAngleVector(rotator.CurrentAngle);
            var currentVelocity = velocityManager.VelocityX;

            this._currentPosition =_currentPosition.AddX(currentVelocity * delta)
                                    .AddY(velocityManager.VelocityY*delta);
            if (_currentPosition != _previousPosition)
            {
                //Debug.WriteLine(_currentPosition);
                var displayRects = GetDisplayInfo(_currentPosition);
                _sourceRects = displayRects;
                _previousPosition = _currentPosition;
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="backgroundTopLeft">That is the top left of the screen (this can be negative)</param>
        private List<DisplayRectInfo> GetDisplayInfo(Vector2 backgroundTopLeft)
        {
            var displayRects = new List<DisplayRectInfo>();

            // From the current Position, calculate the map index.
            // This can be negative (if the map is off screen, or offset for whatever reason)
            // so be careful
            // floatRoat
            //var mantissa = backgroundTopLeft.GetMantissa();
            // no floating info

            var integralPos = backgroundTopLeft;
            // The Fractional difference, if integralPos is not a factor of TileWidth, TileHeighr
            // Then these two bump the start position to the correct pixel.

            var moduloX = integralPos.X % tileDimensions.Width;
            var moduloY = integralPos.Y % tileDimensions.Height;
            // removed t
            var screenOffset = Vector2.Subtract(integralPos, new Vector2(moduloX, moduloY)).ToPoint();
            for (var y = 0; y < viewport.Height; y += tileDimensions.Height)
            {
                var bgNormRow = screenOffset.AddY(y);
                for (var x = 0; x < viewport.Width; x += tileDimensions.Width)
                {
                    var bgNormCols = bgNormRow.AddX(x);
                    // 0 and above we are in the map!
                    if (bgNormCols.X > -1) // Not off screen to the left. though I notice we don't tsop countin pass the righht hand side.
                    {
                        var mapIndexX = bgNormCols.X / tileDimensions.Width;
                        var mapIndexY = bgNormCols.Y / tileDimensions.Height;

                        if (mapIndexX > (this.mapDimensions.Width - 1) || mapIndexY > (mapDimensions.Height - 1))
                            break;
                        
                        var currentMapIndex = ((mapIndexY * this.mapDimensions.Width ) + (mapIndexX));

                        if (currentMapIndex >= 0 && currentMapIndex < this.map.Count)
                        {
                            var displRect = this.map[currentMapIndex];
                            var displayRectIndex = displRect;
                            if (displayRectIndex > -1)
                            {
                                displayRects.Add(new DisplayRectInfo(sprite, Rectangle.Empty,
                                                this.atlasRects[displayRectIndex]
                                                , new Vector2(x - moduloX, y - moduloY)));
                            }
                        }
                    }
                }
            }
            return displayRects;
        }

        public void Draw()
        {
            foreach (var rect in _sourceRects)
            {
                spriteBatch.Draw(rect.Texture, rect.DestinationStart, rect.SourceArea, Color.White);
            }
        }
    }
}