using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachineShooter
{
    State _actualState;
    Dictionary<ShooterStates, State> _states = new Dictionary<ShooterStates, State>();

    public void ChangeState(ShooterStates name)
    {
        if (!_states.ContainsKey(name)) return;
        _actualState?.OnExit();
        _actualState = _states[name];
        _actualState.OnEnter();


    }

    public void AddState(ShooterStates name, State state)
    {
        if (!_states.ContainsKey(name))
            _states.Add(name, state);
        else
            _states[name] = state;

        state.fsmSh = this;
    }

    public void ArtificialUpdate()
    {
        _actualState?.OnUpdate();
    }

}
