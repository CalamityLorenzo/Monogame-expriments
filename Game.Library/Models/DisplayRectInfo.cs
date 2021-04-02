﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Models
{
    public struct DisplayRectInfo
    {

        public DisplayRectInfo(Texture2D texture2D, Rectangle destination, Rectangle source, Vector2 startPos) : this()
        {
            this.Texture = texture2D;
            this.SourceArea = source;
            this.DestinationArea = destination;
            this.DestinationStart = startPos;
        }

        public Texture2D Texture { get; }
        public Rectangle SourceArea { get; }
        public Rectangle DestinationArea { get; }
        public Vector2 DestinationStart { get; }

        public override string ToString()
        {
            return $"Start:{DestinationStart}";
        }
    }

    public class BaseRectInfo
    {
        public BaseRectInfo(Rectangle destination, Vector2 offSetDestination)
        {
            this.DestinationArea = destination;
            this.StartPos = offSetDestination;
        }

        public Rectangle DestinationArea { get; }
        public Vector2 StartPos { get; }
    }
}
