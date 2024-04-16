namespace Modules.Infrastructure
{
    public interface IStateManager
    {
        void SetState(StateMachine.States state);
    }
}