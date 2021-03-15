﻿using Microsoft.Xna.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnimationAgain.Animation
{
    class AnimationFramesCollection : IEnumerable<Microsoft.Xna.Framework.Rectangle>
    {
        private static Lazy<AnimationFramesCollection> EmptySet = new Lazy<AnimationFramesCollection>(() => new AnimationFramesCollection("Null", false, 0, new List<Rectangle>()));

          public AnimationFramesCollection(string Name, bool IsRepeating, int StartFrame, IEnumerable<Rectangle> Frames)
        {
            this.Name = Name;
            this.IsRepeating = IsRepeating;
            this.StartFrame = StartFrame;
            this.Frames = Frames.ToArray();
        }
        public string Name { get; set; }
        public bool IsRepeating { get; set; }
        public int StartFrame { get; set; }
        public Rectangle[] Frames { get; private set; }
        
        public IEnumerator<Rectangle> GetEnumerator()
        {
            return this.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.Frames.GetEnumerator();
        }

        public override string ToString()
        {
            return $"{Name} Repeats:{IsRepeating} Frames:{Frames.Length}";
        }

        public static AnimationFramesCollection Empty => EmptySet.Value;
    }
}
