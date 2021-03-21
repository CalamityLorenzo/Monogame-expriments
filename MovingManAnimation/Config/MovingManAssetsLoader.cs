using GameData.UserInput;
using GameLibrary.Animation;
using GameLibrary.Config.App;
using GameLibrary.Extensions;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Windows.Forms;

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

        public Texture2D PlayerAtlas() => this._graphics.FromFileName(_config.Get("PlayerAtlas"));

        public AnimationSet Animations()
        {
            IList<AnimationPhrase> allPhrases = new List<AnimationPhrase>
            {
                new AnimationPhrase(new List<AnimationFrames>{ _config.Get<AnimationFrames>("Left") },.200f,"Left"),
                new AnimationPhrase(new List<AnimationFrames>{ _config.Get<AnimationFrames>("Right") },.200f, "Right"),
                new AnimationPhrase(new List<AnimationFrames>{ _config.Get<AnimationFrames>("Standing") },.200f,"Standing"),
                new AnimationPhrase(new List<AnimationFrames>{ _config.Get<AnimationFrames>("AimHead")},.200f,"Head"),
                new AnimationPhrase(new List<AnimationFrames>{ _config.Get<AnimationFrames>("JumpLeft")},.200f,"JumpLeft"),
                new AnimationPhrase(new List<AnimationFrames>{ _config.Get<AnimationFrames>("JumpRight")},.200f,"JumpRight"),
                new AnimationPhrase(new List<AnimationFrames>{ _config.Get<AnimationFrames>("WaitLeft")},.200f,"WaitLeft"),
                new AnimationPhrase(new List<AnimationFrames>{ _config.Get<AnimationFrames>("WaitRight")},.200f,"WaitRight"),
            };

            var animSet = new AnimationSet(allPhrases, .200f);

            return animSet;
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
            var p1Controls = MapConfigToControls.MapKeyboard(p1ControlList);
            return p1Controls;
        }


    }
}
