namespace GameLibrary.Character
{
    public interface IActorCommand<T>
    {
        public void Execute(T actor);
    }
}
