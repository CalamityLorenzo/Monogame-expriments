
using GameLibrary.AppObjects;
using GameLibrary.InputManagement;
using GameLibrary.PlayerThings;
using InputTests;
using InputTests.KeyboardInput;
using InputTests.MovingMan;
using Microsoft.Xna.Framework;

namespace inputTests
{

    /// <summary>
    ///  Is key Down, or is Key Up.
    /// </summary>
    public class IsDownIsUp
    {
        private readonly PlayerControlKeys controls;
        private readonly InputsStateManager inputManager;

        public IsDownIsUp(PlayerControlKeys controls, InputsStateManager keysManager)
        {
            this.controls = controls;
            this.inputManager = keysManager;
        }

        public void Update(GameTime time, float DeltaTime, IWalkingMan actor)
        {
            // Where the input manage is in the pipeline, changes the behaviour, so make sure you know!!

            // the list of keys to work with.
            var currentKeys = inputManager.PressedKeys();
            var isUpKeys = inputManager.KeysUp();
            var isDownKeys = inputManager.KeysDown();

            if (isDownKeys.Contains(controls.Up) && !currentKeys.ContainsKey(controls.Down)) actor.MoveUp();
            else if (isDownKeys.Contains(controls.Down) && !currentKeys.ContainsKey(controls.Up)) actor.MoveDown();

            if (isDownKeys.Contains(controls.Left)) actor.MoveLeft();
            else if (isDownKeys.Contains(controls.Right)) actor.MoveRight();

            if (isUpKeys.Contains(controls.Up)) actor.EndMoveUp();
            else if (isUpKeys.Contains(controls.Down)) actor.EndMoveDown();
            
            if (isUpKeys.Contains(controls.Left)) actor.EndMoveLeft();
            else if (isUpKeys.Contains(controls.Right)) actor.EndMoveRight();

        }
    }
}