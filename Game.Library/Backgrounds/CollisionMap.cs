using GameLibrary.AppObjects;
using GameLibrary.Extensions;
using GameLibrary.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System.Collections.Generic;

namespace GameLibrary.Backgrounds
{
    public class CollisionMap : IUpdateableInteractiveGameObject
    {
        private readonly List<int> map;
        private readonly Dimensions tileDimensions;
        private Vector2 _currentPosition;
        private Vector2 _previousPosition;
        private readonly Dimensions mapDimensions;
        private Viewport viewPort; 
        private Rectangle[] _collisionRects; 
        /// <summary>
        /// Defines the stack of rectanges that are the collision map.
        /// The map starts at 0,0, the MapRelativeStartPOsitoi, allows you to start at any offset.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="tileDimensions"></param>
        /// <param name="MapRelativeStartPosition"></param>
        public CollisionMap(List<int> map, Dimensions tileDimensions, Vector2 MapRelativeStartPosition, Viewport currentView, Dimensions rowsAndCols)
        {
            this.map = map;
            this.tileDimensions = tileDimensions;
            _currentPosition = MapRelativeStartPosition;
            _previousPosition = _currentPosition.AddX(1);
            this.mapDimensions = rowsAndCols;

        }
        /// <summary>
        /// All rectangles for the current port
        /// </summary>
        public IEnumerable<Rectangle> ViewPortCollisions { get; private set; } 

        public void Update(float mlSinceLastUpdate, World theState)
        {
            this.viewPort = theState.ViewPort;

            if (_currentPosition != _previousPosition)
            {
                this._collisionRects = GetAvailableRects(_currentPosition, viewPort, this.tileDimensions);
                _previousPosition = _currentPosition;
                this.ViewPortCollisions = _collisionRects;
            }
        }

        private Rectangle[] GetAvailableRects(Vector2 topLeft, Viewport viewPort, Dimensions tileDimensions)
        {

            var displayRects = new List<Rectangle>();

            // From the current Position, calculate the map index.
            // This can be negative (if the map is off screen, or offset for whatever reason)
            // so be careful
            // floatRoat
            var mantissa = topLeft.GetMantissa();
            // no floating info
            var integralPos = topLeft;
            var moduloX = integralPos.X % tileDimensions.Width;
            var moduloY = integralPos.Y % tileDimensions.Height;
            // Using the modulo, we get the fractional values for the current position.
            var screenOffset = Vector2.Subtract(integralPos, new Vector2(moduloX, moduloY)).ToPoint();

            /// The actual drawing space we consider is 1 tile more than the width/height of screen (so blocks don't suddenly flash away)
            for (var y = 0; y < viewPort.Height+tileDimensions.Height ; y += tileDimensions.Height)
            {
                var bgNormRow = screenOffset.AddY(y);
                for (var x = 0; x < viewPort.Width + tileDimensions.Width; x += tileDimensions.Width)
                {
                    var bgNormCols = bgNormRow.AddX(x);
                    // 0 and above we are in the map!
                    if (bgNormCols.X > -1)
                    {
                        var mapIndexX = bgNormCols.X / tileDimensions.Width;
                        var mapIndexY = bgNormCols.Y / tileDimensions.Height;

                        if (mapIndexX > (viewPort.Bounds.Width - 1) || mapIndexY > (viewPort.Bounds.Height - 1))
                            break;

                        var currentMapIndex = ((mapIndexY * this.mapDimensions.Width) + (mapIndexX));

                        if (currentMapIndex >= 0 && currentMapIndex < this.map.Count)
                        {
                            var displRect = this.map[currentMapIndex];
                            var displayRectIndex = displRect;
                            if (displayRectIndex > -1)
                            {
                                displayRects.Add(new Rectangle(Vector2.Subtract(new Vector2(x - moduloX, y - moduloY),mantissa).ToPoint(), new Point(tileDimensions.Width, tileDimensions.Height)));
                            }
                        }
                    }
                }
            }
            return displayRects.ToArray();
        }
    }
}
