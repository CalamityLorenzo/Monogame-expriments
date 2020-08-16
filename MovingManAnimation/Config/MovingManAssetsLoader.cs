using GameData.UserInput;
using GameLibrary.Animation;
using GameLibrary.Config.App;
using GameLibrary.Extensions;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MovingManAnimation.Config
{
    internal class MovingManAssetsLoader
    {
        private ConfigurationData _config;
        private GraphicsDevice _graphics;

        public MovingManAssetsLoader(ConfigurationData Configuration, GraphicsDevice graphics)
        {
            this._config = Configuration;
            this._graphics = graphics;
        }

        public Texture2D PlayerAtlas()=>this._graphics.FromFileName(_config.Get("PlayerAtlas"));

        public IEnumerable<AnimationPhrase> Animations()
        {
            return new List<AnimationPhrase>
            {
                new AnimationPhrase(new List<AnimationFrames>{ _config.Get<AnimationFrames>("Left") },.200f),      // Up
                new AnimationPhrase(new List<AnimationFrames>{ _config.Get<AnimationFrames>("Right") },.200f),     // Down
                new AnimationPhrase(new List<AnimationFrames>{ _config.Get<AnimationFrames>("Left") },.200f),
                new AnimationPhrase(new List<AnimationFrames>{ _config.Get<AnimationFrames>("Right") },.200f),
                new AnimationPhrase(new List<AnimationFrames>{ _config.Get<AnimationFrames>("Standing") },.200f),
                new AnimationPhrase(new List<AnimationFrames>{ _config.Get<AnimationFrames>("AimHead") },.200f),
            };
        }

        public PlayerKeyboardControls Player1KeyboardControls()
        {
            var p1ControlList = _config.Get<Dictionary<string, string>>("Player1Controls");
            var p1Controls = MapConfigToControls.Map(p1ControlList);
            return p1Controls;
        }


    }
}
