using GameLibrary.Animation;
using GameLibrary.Animation.Utilities;
using GameLibrary.Config.App;
using GameLibrary.Extensions;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace BasicJeep
{
    class BasicJeepAssetsLoader
    {
        private readonly GraphicsDevice dev;

        public ConfigurationData Config { get; }

        public BasicJeepAssetsLoader(ConfigurationData Configuration, GraphicsDevice dev)
        {
            Config = Configuration;
            this.dev = dev;
        }

        //public IEnumerable<IAnimationHost> JeepAnimations()
        //{
        //    return new List<AnimationPhrase>{
        //       new AnimationPhrase(new List<AnimationFrames>{ this.Config.Get<AnimationFrames>("Up") },.200f, "Up"),
        //       new AnimationPhrase(new List<AnimationFrames>{ this.Config.Get<AnimationFrames>("UpUpRight")},.200f, "UpUpRight"),
        //       new AnimationPhrase(new List<AnimationFrames>{ this.Config.Get<AnimationFrames>("UpRight")},.200f, "UpRight"),

        //       new AnimationPhrase(new List<AnimationFrames>{ this.Config.Get<AnimationFrames>("Right")},.200f, "Right"),
        //       new AnimationPhrase(new List<AnimationFrames>{ this.Config.Get<AnimationFrames>("DownRight")},.200f, "DownRight"),
        //       new AnimationPhrase(new List<AnimationFrames>{ this.Config.Get<AnimationFrames>("DownDownRight")},.200f, "DownDownRight"),

        //       new AnimationPhrase(new List<AnimationFrames>{ this.Config.Get<AnimationFrames>("Down")},.200f, "Down"),
        //       new AnimationPhrase(new List<AnimationFrames>{ this.Config.Get<AnimationFrames>("DownDownLeft")},.200f, "DownDownLeft"),
        //       new AnimationPhrase(new List<AnimationFrames>{ this.Config.Get<AnimationFrames>("DownLeft")},.200f, "DownLeft"),

        //       new AnimationPhrase(new List<AnimationFrames>{ this.Config.Get<AnimationFrames>("Left")},.200f, "Left"),
        //       new AnimationPhrase(new List<AnimationFrames>{ this.Config.Get<AnimationFrames>("UpLeft")},.200f, "UpLeft"),
        //       new AnimationPhrase(new List<AnimationFrames>{ this.Config.Get<AnimationFrames>("UpUpLeft")},.200f, "UpUpLeft"),
        //    };
        //}

        public Dictionary<string, AnimationFramesCollection> ModernJeepAnimations()
        {
            return this.Config.Get<IEnumerable<AnimationFramesCollection>, AnimationFramesCollectionConverter>("Frames").ToDictionary(a=>a.Name, a=>a);
        }

        public Texture2D JeepAtlas() => this.dev.FromFileName(Config.Get("JeepAtlas"));

    }
}
