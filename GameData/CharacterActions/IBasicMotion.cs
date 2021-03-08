namespace GameData.CharacterActions
{
    public interface IBasicMotion
    {
        void MoveLeft();
        void MoveRight();
        void MoveUp();
        void MoveDown();
        void EndMoveLeft();
        void EndMoveRight();
        void EndMoveDown();
        void EndMoveUp();
        void Jump();
    }
}
