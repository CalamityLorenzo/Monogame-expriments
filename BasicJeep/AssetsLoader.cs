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

            return new List<AnimationPhraseHost>{
               new AnimationPhraseHost(this.Config.Get<AnimationPhrase>("Up")),
               new AnimationPhraseHost(this.Config.Get<AnimationPhrase>("UpUpRight")),
               new AnimationPhraseHost(this.Config.Get<AnimationPhrase>("UpRight")),

               new AnimationPhraseHost(this.Config.Get<AnimationPhrase>("Right")),
               new AnimationPhraseHost(this.Config.Get<AnimationPhrase>("DownRight")),
               new AnimationPhraseHost(this.Config.Get<AnimationPhrase>("DownDownRight")),

               new AnimationPhraseHost(this.Config.Get<AnimationPhrase>("Down")),
               new AnimationPhraseHost(this.Config.Get<AnimationPhrase>("DownDownLeft")),
               new AnimationPhraseHost(this.Config.Get<AnimationPhrase>("DownLeft")),

               new AnimationPhraseHost(this.Config.Get<AnimationPhrase>("Left")),
               new AnimationPhraseHost(this.Config.Get<AnimationPhrase>("UpLeft")),
               new AnimationPhraseHost(this.Config.Get<AnimationPhrase>("UpUpLeft")),
            };
        }

        public Texture2D JeepAtlas() => this.dev.FromFileName(Config.Get("JeepAtlas"));

    }
}
