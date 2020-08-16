using GameData.UserInput;
using GameLibrary.Animation;
using GameLibrary.Config.App;
using GameLibrary.Extensions;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BasicJeep
{
    class AssetsLoader
    {
        private readonly GraphicsDevice dev;

        public ConfigurationData Config { get; }

        public AssetsLoader(ConfigurationData Configuration, GraphicsDevice dev)
        {
            Config = Configuration;
            this.dev = dev;
        }

        public IEnumerable<IAnimationHost> JeepAnimations()
        {

            return new List<AnimationPhrase>{
               new AnimationPhrase(new List<AnimationFrames>{ this.Config.Get<AnimationFrames>("Up") },.200f),
               new AnimationPhrase(new List<AnimationFrames>{ this.Config.Get<AnimationFrames>("UpUpRight")},.200f),
               new AnimationPhrase(new List<AnimationFrames>{ this.Config.Get<AnimationFrames>("UpRight")},.200f),

               new AnimationPhrase(new List<AnimationFrames>{ this.Config.Get<AnimationFrames>("Right")},.200f),
               new AnimationPhrase(new List<AnimationFrames>{ this.Config.Get<AnimationFrames>("DownRight")},.200f),
               new AnimationPhrase(new List<AnimationFrames>{ this.Config.Get<AnimationFrames>("DownDownRight")},.200f),

               new AnimationPhrase(new List<AnimationFrames>{ this.Config.Get<AnimationFrames>("Down")},.200f),
               new AnimationPhrase(new List<AnimationFrames>{ this.Config.Get<AnimationFrames>("DownDownLeft")},.200f),
               new AnimationPhrase(new List<AnimationFrames>{ this.Config.Get<AnimationFrames>("DownLeft")},.200f),

               new AnimationPhrase(new List<AnimationFrames>{ this.Config.Get<AnimationFrames>("Left")},.200f),
               new AnimationPhrase(new List<AnimationFrames>{ this.Config.Get<AnimationFrames>("UpLeft")},.200f),
               new AnimationPhrase(new List<AnimationFrames>{ this.Config.Get<AnimationFrames>("UpUpLeft")},.200f),
            };
        }

        public Texture2D JeepAtlas() => this.dev.FromFileName(Config.Get("JeepAtlas"));

    }
}
