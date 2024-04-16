namespace Modules.Infrastructure.States
{
    internal sealed class BootStrapState : IGameState
    {
        private readonly IStateManager _stateMachine;

        public BootStrapState(IStateManager stateMachine) =>
            _stateMachine = stateMachine;

        public void EnterState() => Init();

        private void Init() =>
            _stateMachine.SetState(StateMachine.States.LoadLevel);
    }
}