using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachineNormal 
{
    State _actualState;
    Dictionary<NormalStates, State> _states = new Dictionary<NormalStates, State>();

    public void ChangeState(NormalStates name)
    {
        if (!_states.ContainsKey(name)) return;
        _actualState?.OnExit();
        _actualState = _states[name];
        _actualState.OnEnter();


    }

    public void AddState(NormalStates name, State state)
    {
        if (!_states.ContainsKey(name))
            _states.Add(name, state);
        else
            _states[name] = state;

        state.fsmNm = this;
    }

    public void ArtificialUpdate()
    {
        _actualState?.OnUpdate();
    }
}
