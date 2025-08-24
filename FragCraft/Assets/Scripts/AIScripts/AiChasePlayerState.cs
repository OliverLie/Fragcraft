using UnityEngine;
using UnityEngine.AI;

public class AiChasePlayerState : AiState
{
    

   

    public float timer = 0.0f;
    public void Enter(AiAgent agent)
    {

    }

    public void Exit(AiAgent agent)
    {
        
    }

    public AiStateID GetID()
    {
        return AiStateID.ChasePlayer;
    }

    public void Update(AiAgent agent)
    {
        timer -= Time.deltaTime;
        if (timer < 0.0f)
        {
            float sqrDistance = (agent.playerTransform.position - agent.navMeshAgent.transform.position).sqrMagnitude;

            if (sqrDistance > agent.config.maxDistance*agent.config.maxDistance)
            {
                agent.navMeshAgent.destination = agent.playerTransform.position;
            }
            timer = agent.config.maxTime;

        }
    }
}
