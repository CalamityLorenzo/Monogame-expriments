
using GameLibrary.AppObjects;
using GameLibrary.PlayerThings;
using InputTests;
using InputTests.KeyboardInput;
using InputTests.MovingMan;
using Microsoft.Xna.Framework;

namespace inputTests{
    public class SendKeyboardInput{
        private readonly PlayerControlKeys controls;
        private readonly InputManager inputManager;

        public SendKeyboardInput(PlayerControlKeys controls, InputManager keysManager)
        {
            this.controls = controls;
            this.inputManager = keysManager;
        }

        public void Update(GameTime time, float DeltaTime, IWalkingMan actor)
        {
            // the list of keys to work with.
            var currentKeys = inputManager.PressedKeys();
            var isUpKeys = inputManager.KeysUp();
            var isDownKeys = inputManager.KeysDown();
            var buttons = inputManager.PressedMouseButtons();

            if (isDownKeys.Contains(controls.Up) && !currentKeys.ContainsKey(controls.Down)) actor.MoveUp();
            else if (isDownKeys.Contains(controls.Down) && !currentKeys.ContainsKey(controls.Up)) actor.MoveDown();

            if (isDownKeys.Contains(controls.Left) && !currentKeys.ContainsKey(controls.Right)) actor.MoveLeft();


            if (isUpKeys.Contains(controls.Up) && !currentKeys.ContainsKey(controls.Up)) actor.Standing();
            else if (isUpKeys.Contains(controls.Down)) actor.Standing();
            
            if (isUpKeys.Contains(controls.Left) && !currentKeys.ContainsKey(controls.Right)) actor.Standing();
            else if (isUpKeys.Contains(controls.Right) && !currentKeys.ContainsKey(controls.Left)) actor.Standing();

            //if(currentKeys.ContainsKey(controls.Up)) actor.MoveUp();
            //if(currentKeys.ContainsKey(controls.Down)) actor.MoveDown();

            //if(currentKeys.ContainsKey(controls.Right)) actor.MoveRight();
            //if(currentKeys.ContainsKey(controls.Left)) actor.MoveLeft();

        }
    }
}