namespace StateMachines
{
    public interface IState
    {
        /// <summary>
        /// Called by the owner state-machine when this state becomes the "Current State"
        /// This method is called before other state methods and is used to set up state requirements.
        /// </summary>
        void Enter();

        /// <summary>
        /// Called by the owner state-machine when this state becomes the "Current State"
        /// This coroutine is started after Enter() and contains the main logic of the state.
        /// </summary>
        /// <returns></returns>
        void Execute();

        /// <summary>
        /// Called by the owner state-machine when this state is not the "Current State" anymore and is moving to the next state.
        /// This method is used for cleaning up.
        /// </summary>
        void Exit();
    }
}