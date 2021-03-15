using GameData.UserInput;
using GameLibrary.Animation;
using GameLibrary.Animation.Utilities;
using GameLibrary.Config.App;
using GameLibrary.Extensions;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MovingManAnimation.Config
{
    internal class ExperimentalAssetLoader
    {
        private ConfigurationData _config;
        private GraphicsDevice _graphics;

        public ExperimentalAssetLoader(ConfigurationData Configuration, GraphicsDevice graphics)
        {
            this._config = Configuration;
            this._graphics = graphics;
        }

        public Texture2D PlayerAtlas() => this._graphics.FromFileName(_config.Get("PlayerAtlas"));
        public List<Texture2D> BulletAtlases() => new List<Texture2D> {
                this._graphics.FromFileName(_config.Get("Bullet1")),
                this._graphics.FromFileName(_config.Get("Bullet2")),
                this._graphics.FromFileName(_config.Get("Bullet3")),

                    };



        public Dictionary<string, AnimationFramesCollection> Animations()
        {

            var frames = _config.Get<Dictionary<string, AnimationFramesCollection>, AnimationFramesCollectionConverter>("Frames"); //, a => ("What", new Rectangle[])); ;
            return frames;
        }

        public Dictionary<string, AnimationFramesCollection> GunAnimations()
        {

            var frames = _config.Get<Dictionary<string, AnimationFramesCollection>, AnimationFramesCollectionConverter>("Frames"); //, a => ("What", new Rectangle[])); ;
            return frames;
        }

        public IDictionary<string, AnimationPhrase> AnimationPhrases() => new Dictionary<string, AnimationPhrase>{
            { "Left", new AnimationPhrase(new List<AnimationFrames>{ _config.Get<AnimationFrames>("Left") },.200f, "Left") },
            { "Right", new AnimationPhrase(new List<AnimationFrames>{ _config.Get<AnimationFrames>("Right") },.200f, "Right") },
            { "Standing", new AnimationPhrase(new List<AnimationFrames>{ _config.Get<AnimationFrames>("Standing") },.200f, "Standing") },
            { "AimHead", new AnimationPhrase(new List<AnimationFrames>{ _config.Get<AnimationFrames>("AimHead") },.200f, "AimHead") },
            { "JumpLeft", new AnimationPhrase(new List<AnimationFrames>{ _config.Get<AnimationFrames>("JumpLeft") },.200f, "JumpLeft") },
            { "JumpRight", new AnimationPhrase(new List<AnimationFrames>{ _config.Get<AnimationFrames>("JumpRight") },.200f, "JumpRight") },
            { "WaitLeft", new AnimationPhrase(new List<AnimationFrames>{ _config.Get<AnimationFrames>("WaitLeft") },.200f, "WaitLeft") },
            { "WaitLeft", new AnimationPhrase(new List<AnimationFrames>{ _config.Get<AnimationFrames>("WaitRight") },.200f, "WaitRight") }
            };

        public PlayerKeyboardControls Player1KeyboardControls()
        {
            var p1ControlList = _config.Get<Dictionary<string, string>>("Player1Controls");
            var p1Controls = MapConfigToControls.Map(p1ControlList);
            return p1Controls;
        }

        internal Dictionary<string, AnimationFramesCollection> BulletAnimations()
        {
            var frames = _config.Get<Dictionary<string, AnimationFramesCollection>, AnimationFramesCollectionConverter>("BulletFrames"); //, a => ("What", new Rectangle[])); ;
            return frames;
        }
    }
}
