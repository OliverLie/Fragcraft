using UnityEngine;
public enum AiStateID
{
    ChasePlayer,
    Death,
    Idle,
    FindWeapon
}

public interface AiState
{
    AiStateID GetID();
    void Enter(AiAgent agent);
    void Update(AiAgent agent);
    void Exit(AiAgent agent);

}
