﻿namespace GameData.CharacterActions
{
    public interface ICharacterActions
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
        void EndFireSpecial();
        void Action();
        void EndAction();
        void Jump();
        void Standing();
    }
}