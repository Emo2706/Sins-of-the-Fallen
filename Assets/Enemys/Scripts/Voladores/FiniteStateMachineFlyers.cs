using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachineFlyers 
{
    State _actualState;
    Dictionary<FlyersStates, State> _states = new Dictionary<FlyersStates, State>();

    public void ChangeState(FlyersStates name)
    {
        if (!_states.ContainsKey(name)) return;
        _actualState?.OnExit();
        _actualState = _states[name];
        _actualState.OnEnter();


    }

    public void AddState(FlyersStates name, State state)
    {
        if (!_states.ContainsKey(name))
            _states.Add(name, state);
        else
            _states[name] = state;

        state.fsmFly = this;
    }

    public void ArtificialUpdate()
    {
        _actualState?.OnUpdate();
    }
}
