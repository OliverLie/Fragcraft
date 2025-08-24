using UnityEngine;

public class AiStateMachine
{
    public AiState[] states;
    public AiAgent agent;
    public AiStateID currentState;
    public AiStateMachine(AiAgent agent)
    {
        this.agent = agent;
        int numberofstates = System.Enum.GetNames(typeof(AiStateID)).Length;
        states = new AiState[numberofstates];

    }

    public void RegisterState(AiState state)
    {
        int index = (int)state.GetID();
        states[index] = state;
    }


    public AiState GetState(AiStateID stateId)
    {
        int index = (int)stateId;
        return states[index];

    }
    public void Update()
    {
        GetState(currentState)?.Update(agent);


    }


    public void ChangeStates(AiStateID newState)
    {
        GetState(currentState)?.Exit(agent);
        currentState = newState;
        GetState(currentState)?.Enter(agent);

    }
}
