namespace InputTests.MovingMan
{
    public  interface IWalkingMan
    {
        void MoveLeft();
        void MoveRight();
        void MoveUp();
        void MoveDown();
        void Fire();
        void DoubleClickFire();
        void Standing();
    }
}