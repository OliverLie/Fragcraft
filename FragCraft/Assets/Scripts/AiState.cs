using UnityEngine;
public enum AiStateID
{
    ChasePlayer
}

public interface AiState
{
    AiStateID GetID();
    void Enter();
    void Update();
    void Exit();

}
