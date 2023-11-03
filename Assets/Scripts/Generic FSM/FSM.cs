using System.Collections.Generic;

public class FSM<T>
{
    State _actualState;
    Dictionary<T, State> _states = new Dictionary<T, State>();

    public void ChangeState(T name)
    {
        if (!_states.ContainsKey(name)) return;
        _actualState?.OnExit();
        _actualState = _states[name];
        _actualState.OnEnter();
    }

    public void AddState(T name, State state)
    {
        if (!_states.ContainsKey(name))
            _states.Add(name, state);
        else
            _states[name] = state;

    }

    public void Update()
    {
        _actualState?.OnUpdate();
    }

    public void FixedUpdate()
    {
        _actualState?.OnFixedUpdate();
    }

}
