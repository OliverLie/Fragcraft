using UnityEngine;

public class AiDeathState : AiState
{
    public Vector3 direction;
    public void Enter(AiAgent agent)
    {
        agent.ragdoll.ActiveRagdoll();
        direction.y = 1;
        agent.ragdoll.ApplyForce(direction * agent.config.dieForce);
        agent.ui.gameObject.SetActive(false);
        agent.mesh.updateWhenOffscreen = true;
    }

    public void Exit(AiAgent agent)
    {
        
    }

    public AiStateID GetID()
    {
        return AiStateID.Death;
    }

    public void Update(AiAgent agent)
    {
        
    }
}
