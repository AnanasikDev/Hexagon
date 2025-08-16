#nullable enable

using System;
using System.Collections.Generic;
using UnityEngine.Assertions;

using StateID = System.Int32;

namespace Hexagon.StateMachine
{
    [Serializable]
    public class StateMachine
    {
        public State _currentState { get; protected set; } = null!;
        public State? _targetState { get; protected set; } = null;
        public State _previousState { get; protected set; } = null!;
        public StateID _currentStateID { get; protected set; } = 0;
        protected Transition? _currentTransition = null;

        public readonly Dictionary<StateID, State> _enum2state = new();
        public readonly List<Transition> _transitions = new();

        public readonly Dictionary<StateID, List<Transition>> _stateTree = new();

        public bool _isTransitioning { get; protected set; } = false;
        public bool _isLocked { get; protected set; } = false;

        /// <summary>
        /// Whether the transition was started in the last call of Update (in the last frame).
        /// </summary>
        public bool _hasTransitionJustStarted { get; protected set; } = false;

        public delegate float GetCurrentTimeDelegate();
        public GetCurrentTimeDelegate GetCurrentTimeFunction = () => UnityEngine.Time.time;

        protected Queue<ExternalMachineEvent> _eventQueue = new();

        public event Action<State, State>? OnTransitionStartedEvent;
        public event Action<State, State>? OnTransitionEndedEvent;

        public virtual void Init<TStateEnum>(Dictionary<TStateEnum, State> enum2state, List<Transition>? transitions = null) where TStateEnum : Enum
        {
            foreach (var pair in enum2state)
            {
                _enum2state.Add(EnumToID(pair.Key), pair.Value);
                pair.Value._type = EnumToID(pair.Key);
                pair.Value.Init(this);

                if (pair.Value == null)
                {
                    throw new InvalidOperationException($"State {pair.Key} cannot be null.");
                }

                _stateTree[pair.Value._type] = new List<Transition>();
            }

            if (_enum2state.Count == 0)
            {
                throw new InvalidOperationException("StateMachine must have at least one state defined.");
            }

            _currentState = _enum2state[0];
            _previousState = _currentState;
            _currentState.OnTransitionToFinished();
        }

        public virtual void AddTransition(Transition transition)
        {
            if (transition == null)
            {
                throw new ArgumentNullException(nameof(transition), "Transition cannot be null.");
            }
            if (!_enum2state.ContainsKey(transition._from) || !_enum2state.ContainsKey(transition._to))
            {
                throw new ArgumentException($"Transition from {transition._from} to {transition._to} contains an invalid state.");
            }

            _transitions.Add(transition);
            transition.Init(this);
            _stateTree[transition._from].Add(transition);
        }

        public void AddTransitions(IEnumerable<Transition> transitions)
        {
            foreach (Transition transition in transitions)
            {
                AddTransition(transition);
            }
        }

        public void AddTransitions(params TransitionGroup[] inputs)
        {
            foreach (TransitionGroup input in inputs)
            {
                AddTransitions(input._transitions);
            }
        }

        public virtual async void Update()
        {
            _hasTransitionJustStarted = false;
            if (_isTransitioning && _currentTransition != null)
            {
                bool finished = await _currentTransition.Progress();

                if (!finished) return;

                FinishTransition();
            }

            (StateID state, Transition? transition) = GetNextState();

            if (transition != null)
            {
                BeginTransition(state, transition);
            }

            _currentState.OnUpdate();
        }

        public virtual void FixedUpdate()
        {
            _currentState.OnFixedUpdate();
        }

        public virtual (StateID, Transition?) GetNextState(Func<Transition, bool>? extraCondition = null)
        {
            StateID result = _currentState._type;

            if (_isLocked || _isTransitioning) return (result, null);

            Transition? transition = GetTransitionFromCurrent(extraCondition);
            if (_eventQueue.Count > 0)
            {
                _eventQueue.Dequeue();
            }
            if (transition == null) return (result, null);

            result = transition._to;
            return (result, transition);
        }

        protected virtual Transition? GetTransitionFromCurrent(Func<Transition, bool>? extraCondition = null)
        {
            foreach (Transition transition in _stateTree[_currentState._type])
            {
                bool eventExists = _eventQueue.TryPeek(out ExternalMachineEvent? currentEvent);
                if (transition._condition(_currentState, currentEvent) &&
                    (extraCondition?.Invoke(transition) ?? true) &&
                    _enum2state[transition._from].IsPossibleChangeFrom() &&
                    _enum2state[transition._to].IsPossibleChangeTo())
                {
                    return transition;
                }
            }

            return null;
        }

        public virtual bool RequestState(StateID newState)
        {
            if (_isLocked || _isTransitioning)
            {
                return false;
            }
            if (!_enum2state.ContainsKey(newState))
            {
                throw new ArgumentException($"State {newState} does not exist in the StateMachine.");
            }

            Transition? transition = GetTransitionFromCurrent(t => t._to == newState);

            if (transition == null)
            {
                return false;
            }

            BeginTransition(newState, transition);
            return false;
        }

        protected virtual void BeginTransition(StateID newState, Transition transition)
        {
            _isTransitioning = true;
            _hasTransitionJustStarted = true;
            _targetState = _enum2state[newState];
            _currentState.OnTransitionFromStarted();
            _targetState.OnTransitionToStarted();

            _currentTransition = transition;
            _currentTransition.Begin();

            OnTransitionStartedEvent?.Invoke(_currentState, _targetState);
        }

        public virtual State FinishTransition()
        {
            if (_targetState == null)
            {
                throw new InvalidOperationException($"{nameof(_targetState)} cannot be null on FinishTransition");
            }

            if (_currentTransition == null)
            {
                throw new InvalidOperationException($"{nameof(_currentTransition)} cannot be null on FinishTransition");
            }

            _currentState.OnTransitionFromFinished();
            _targetState.OnTransitionToFinished();

            _previousState = _currentState;
            _currentState = _enum2state[_targetState._type];

            _currentTransition.Finish();

            _targetState = null;
            _currentTransition = null;
            _isTransitioning = false;

            OnStateChanged(_previousState, _currentState);
            OnTransitionEndedEvent?.Invoke(_previousState, _currentState);
            return _currentState;
        }

        public virtual void Die()
        {
            _currentState?.OnTransitionFromStarted();
        }

        protected virtual void OnStateChanged(State from, State to) { }

        public static StateID EnumToID<TEnumState>(TEnumState value) where TEnumState : Enum
        {
            return Convert.ToInt32(value);
        }

        public static TEnumState IDToEnum<TEnumState>(StateID id) where TEnumState : Enum
        {
            if (!Enum.IsDefined(typeof(TEnumState), id))
            {
                throw new ArgumentOutOfRangeException(nameof(id), $"ID {id} is not defined in {typeof(TEnumState).Name}.");
            }
            return (TEnumState)Enum.ToObject(typeof(TEnumState), id);
        }

        public void PushEvent(ExternalMachineEvent @event)
        {
            Assert.IsNotNull(@event, "Event cannot be null.");
            _eventQueue.Enqueue(@event);
        }
    }

    [Serializable]
    public class StateMachine<TParent> : StateMachine where TParent : class
    {
        public TParent Parent { get; init; }

        public StateMachine(TParent parent)
        {
            Parent = parent;
            if (Parent == null)
            {
                throw new ArgumentNullException(nameof(parent), "Parent of a StateMachine<TParent> cannot be null.");
            }
        }
    }
}