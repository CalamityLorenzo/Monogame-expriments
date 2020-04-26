
using GameLibrary.PlayerThings;
using InputTests;
using InputTests.KeyboardInput;
using InputTests.MovingMan;
using Microsoft.Xna.Framework;

namespace inputTests{
    public class SendKeyboardInput{
        private readonly PlayerControlKeys controls;
        private readonly KeysManager keysManager;

        public SendKeyboardInput(PlayerControlKeys controls, KeysManager keysManager)
        {
            this.controls = controls;
            this.keysManager = keysManager;
        }

        public void Update(GameTime time, float DeltaTime, IWalkingMan actor)
        {
            // the list of keys to work with.
            var currentKeys = keysManager.PressedKeys;

            if(currentKeys.ContainsKey(controls.Up)) actor.MoveUp();
            if(currentKeys.ContainsKey(controls.Down)) actor.MoveDown();

            if(currentKeys.ContainsKey(controls.Right)) actor.MoveRight();
            if(currentKeys.ContainsKey(controls.Left)) actor.MoveLeft();
            
        }
    }
}