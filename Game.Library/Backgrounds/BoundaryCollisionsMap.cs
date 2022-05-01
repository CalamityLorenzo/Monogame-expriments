using GameLibrary.AppObjects;
using GameLibrary.Extensions;
using GameLibrary.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace GameLibrary.Backgrounds
{
    // A limited collision map for handling boundries of large level like objects.
    // The benefit is that it calculates 1 Rect per Side, Unless you've poked a hole in it, where upon thats two rects, and a hole
    public class BoundaryCollisionsMap : IUpdateableInteractiveGameObject
    {
        private readonly List<int> map;
        private readonly Dimensions tileDimensions;
        private Vector2 _currentPosition;
        private Vector2 _previousPosition;
        private readonly Dimensions mapDimensions;
        private Viewport viewPort;
        private Rectangle[] _collisionRects;

        public IEnumerable<Rectangle> ViewPortCollisions { get; private set; }

        public BoundaryCollisionsMap(List<int> map, Dimensions tileDimensions, Vector2 MapRelativeStartPosition, Viewport currentView, Dimensions rowsAndCols)
        {
            this.map = map;
            this.tileDimensions = tileDimensions;
            _currentPosition = MapRelativeStartPosition;
            _previousPosition = _currentPosition.AddX(1);
            this.mapDimensions = rowsAndCols;

        }

        public void Update(float mlSinceLastUpdate, World theState)
        {
            this.viewPort = theState.ViewPort;

            if (_currentPosition != _previousPosition)
            {
                this._collisionRects = GetBoundaryRects(_currentPosition, viewPort, this.tileDimensions);
                _previousPosition = _currentPosition;
                this.ViewPortCollisions = _collisionRects;
            }
        }
        // always left to right
        private Rectangle[] horizontalScan(Point screenOffset, int startXPos, int startYPos)
        {
            // Top Row
            // Scan along the top row.
            int currentWidth = 0;
            int currentRectStartX = 0;
            Point screenIterator = new Point(screenOffset.X+startXPos, screenOffset.Y+ startYPos);
            var resultRects = new List<Rectangle>();
            for (var x = 0; x < viewPort.Width + tileDimensions.Width; x += tileDimensions.Width)
            {
                if (currentWidth == 0) currentRectStartX = x;

                var bgNormCols = screenIterator.AddX(x);
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

                        if (this.map[currentMapIndex] > -1)// Our ever increasaing size
                            currentWidth += tileDimensions.Width;
                        else // We have found the end of the rectangle
                        {
                            if (currentWidth > 0)
                            {
                                resultRects.Add(new Rectangle(new Point(currentRectStartX, startYPos), new Point(currentWidth, tileDimensions.Height)));
                                currentWidth = 0;
                            }
                        }
                    }
                }
            }

            // If we get here and there are we still have dimensions in the tank....
            if (currentWidth != 0)
                resultRects.Add(new Rectangle(new Point(currentRectStartX, startYPos), new Point(currentWidth, tileDimensions.Height)));


            return resultRects.ToArray();
        }

        private Rectangle[] verticalScan(Point screenOffset, int setXPos, int startYPos)
        {
            // Top Row
            // Scan along the top row.
            int currentHeight = 0;
            int currentRectStartY = 0;
            Point screenIterator = new Point(screenOffset.X +setXPos, screenOffset.Y + startYPos);
            var resultRects = new List<Rectangle>();
            /// The actual drawing space we consider is 1 tile more than the width/height of screen (so blocks don't suddenly flash away)
            for (var y = startYPos; y < viewPort.Height + tileDimensions.Height; y += tileDimensions.Height)
            {
                // REset the start point of the current rectangle
                if (currentHeight == 0) currentRectStartY = y;

                var bgNormRow = screenIterator.AddY(y);

                // 0 and above we are in the map!
                if (setXPos > -1)
                {
                    var mapIndexX = bgNormRow.X / tileDimensions.Width;
                    var mapIndexY = bgNormRow.Y / tileDimensions.Height;

                    if (mapIndexX > (viewPort.Bounds.Width - 1) || mapIndexY > (viewPort.Bounds.Height - 1))
                        break;

                    var currentMapIndex = ((mapIndexY * this.mapDimensions.Width) + (mapIndexX));

                    if (currentMapIndex >= 0 && currentMapIndex < this.map.Count)
                    {

                        if (this.map[currentMapIndex] > -1)// Our ever increasaing size
                            currentHeight += tileDimensions.Height;
                        else // We have found the end of the rectangle
                        {
                            if (currentHeight > 0)
                            {
                                resultRects.Add(new Rectangle(new Point(setXPos, currentRectStartY), new Point(tileDimensions.Width, currentHeight)));
                                currentHeight = 0;
                            }
                        }
                    }
                }

            }

            // If we get here and there are we still have dimensions in the tank....
            if (currentHeight != 0)
                resultRects.Add(new Rectangle(new Point(setXPos, currentRectStartY), new Point(tileDimensions.Width, currentHeight)));


            return resultRects.ToArray();
        }

        private Rectangle[] GetBoundaryRects(Vector2 topLeft, Viewport viewPort, Dimensions tileDimensions)
        {


            // From the current Position, calculate the map index.
            // This can be negative (if the map is off screen, or offset for whatever reason)
            // so be careful
            // floatRoat
            // no floating info
            var integralPos = topLeft;
            var moduloX = integralPos.X % tileDimensions.Width;
            var moduloY = integralPos.Y % tileDimensions.Height;
            // Using the modulo, we get the fractional values for the current position.
            var screenOffset = Vector2.Subtract(integralPos, new Vector2(moduloX, moduloY)).ToPoint();

            /// The actual drawing space we consider is 1 tile more than the width/height of screen (so blocks don't suddenly flash away)
            /// WE don't want these block intersecting across each other, so horizontal is ALL the across (Top and bottom)
            /// Vertical is Ignore first row, ignore lastrow everything else.
            var topRects = horizontalScan(screenOffset, 0,  tileDimensions.Height);
            var bottomRects = horizontalScan(screenOffset, 0, tileDimensions.Height * this.mapDimensions.Height);

            var left = verticalScan(screenOffset, 0, tileDimensions.Height * 2);
            var right = verticalScan(screenOffset, (tileDimensions.Width * mapDimensions.Width) - mapDimensions.Width , tileDimensions.Height*2);

            return topRects.Concat(bottomRects).Concat(left).Concat(right).ToArray();
        }
    }
}
