using GameLibrary.AppObjects;

namespace GameData
{
    /// <summary>
    /// You compose your object so this is what managers he velocity
    /// Then at update read the values.
    /// </summary>
    public class BasicVelocityManager : IVelocinator
    {

        public BasicVelocityManager(float startVelocityX, float startVelocityY)
        {
            VelocityY = startVelocityY;
            VelocityX = startVelocityX;
        }

        //public void SetVelocityX(float x) => this.VelocityX = x > MaxVelocity ? MaxVelocity : x;
        //public void SetVelocityY(float y) => this.VelocityY = y > MaxVelocity ? MaxVelocity : y;

        public void SetVelocityX(float x) => this.VelocityX = x; // > MaxVelocity ? MaxVelocity : x;
        public void SetVelocityY(float y) => this.VelocityY = y;// > MaxVelocity ? MaxVelocity : y;

        public float MaxVelocity { get; }
        public float VelocityY { get; internal set; }
        public float VelocityX { get; internal set; }
    }
}
