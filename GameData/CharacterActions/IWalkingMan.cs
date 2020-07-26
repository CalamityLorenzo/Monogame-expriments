namespace GameData.CharacterActions
{
    public interface IWalkingMan
    {
        void MoveLeft();
        void MoveRight();
        void MoveUp();
        void MoveDown();
        void EndMoveLeft();
        void EndMoveRight();
        void EndMoveDown();
        void EndMoveUp();
        void Fire();
        void FireSpecial();
        void Standing();
    }
}