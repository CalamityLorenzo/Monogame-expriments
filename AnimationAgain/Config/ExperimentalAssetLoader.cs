using GameData.UserInput;
using GameLibrary.Animation;
using GameLibrary.Config.App;
using GameLibrary.Extensions;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

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

            var data = _config.RichPathQueryJsonConfig("Smoking:Gun:Age");
        }

        public Texture2D PlayerAtlas() => this._graphics.FromFileName(_config.Get("PlayerAtlas"));
        public List<Texture2D> BulletAtlases() => new List<Texture2D> {
                this._graphics.FromFileName(_config.Get("Bullet1")),
                this._graphics.FromFileName(_config.Get("Bullet2")),
                this._graphics.FromFileName(_config.Get("Bullet3")),

                    };



        public Dictionary<string, AnimationFramesCollection> Animations()
        {
            var frames = _config.Get<IEnumerable<AnimationFramesCollection>>("Frames").ToDictionary(a => a.Name, a => a);
            return frames;
        }

        public Dictionary<string, AnimationFramesCollection> GunAnimations()
        {
            var frames = _config.Get<IEnumerable<AnimationFramesCollection>>("Frames").ToDictionary(a => a.Name, a => a);
            return frames;
        }

        public PlayerKeyboardControls Player1KeyboardControls()
        {
            var p1ControlList = _config.Get<Dictionary<string, string>>("Player1Controls");
            var p1Controls = MapConfigToControls.Map(p1ControlList);
            return p1Controls;
        }

        internal Dictionary<string, AnimationFramesCollection> BulletAnimations()
        {
            var frames = _config.Get<IEnumerable<AnimationFramesCollection>>("Bullets:Frames").ToDictionary(a => a.Name, a => a);
            return frames;
        }
    }
}
