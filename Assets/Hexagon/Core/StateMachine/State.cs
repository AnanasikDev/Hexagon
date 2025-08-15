namespace Hexagon.StateMachine
{
    /// <summary>
    /// Base class for a state within a state machine.
    /// </summary>
    public abstract class State
    {
        /// <summary>
        /// Reference to the owning state machine.
        /// </summary>
        public StateMachine _machine;

        /// <summary>
        /// Integer identifier for the state.
        /// </summary>
        public int _type;

        private float _startTime = 0;

        /// <summary>
        /// Time in seconds since the state became active.
        /// </summary>
        public float ActiveTime { get { return UnityEngine.Time.time - _startTime; } }

        /// <summary>
        /// Weight of the state in blending operations. Typically between 0 and 1.
        /// </summary>
        public float Weight { get; set; } = 1.0f;

        /// <summary>
        /// Initializes the state with a reference to its owning state machine.
        /// </summary>
        public virtual void Init(StateMachine machine)
        {
            _machine = machine;
            Init();
        }

        /// <summary>
        /// Called during initialization of the state.
        /// </summary>
        public abstract void Init();

        /// <summary>
        /// Called when state becomes the target state of the State Machine. Weight becomes 0. Transition has started.
        /// </summary>
        public abstract void OnTransitionToStarted();

        /// <summary>
        /// Called when state becomes the current state of the State Machine. Weight becomes 1. Transition has ended.
        /// </summary>
        public virtual void OnTransitionToFinished()
        {
            _startTime = UnityEngine.Time.time;
        }

        /// <summary>
        /// Called when state starts to lose weight to the target state of the State Machine. Weight becomes 1. Transition has started.
        /// </summary>
        public abstract void OnTransitionFromStarted();

        /// <summary>
        /// Called when state becomes the previous state of the State Machine. Weight becomes 0. Transition has ended.
        /// </summary>
        public virtual void OnTransitionFromFinished() { }

        /// <summary>
        /// Called once per frame to update state logic.
        /// </summary>
        public abstract void OnUpdate();

        /// <summary>
        /// Called once per physics update to update state logic.
        /// </summary>
        public virtual void OnFixedUpdate() { }

        /// <summary>
        /// Returns whether a transition from this state is allowed.
        /// </summary>
        public abstract bool IsPossibleChangeFrom();

        /// <summary>
        /// Returns whether a transition to this state is allowed.
        /// </summary>
        public abstract bool IsPossibleChangeTo();

        /// <summary>
        /// Called to draw debug gizmos for the state.
        /// </summary>
        public virtual void DrawGizmos() { }

        /// <summary>
        /// Checks if the machine is of a specific generic type.
        /// </summary>
        public bool IsMachine<TParent>() where TParent : class
        {
            return _machine is StateMachine<TParent>;
        }

        /// <summary>
        /// Returns the owning state machine cast to a specific generic type.
        /// </summary>
        public StateMachine<TParent> GetMachine<TParent>() where TParent : class
        {
            return _machine as StateMachine<TParent>;
        }
    }
}
