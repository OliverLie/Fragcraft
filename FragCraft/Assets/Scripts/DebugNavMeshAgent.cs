using UnityEngine;
using Unity.AI;
using UnityEngine.AI;
public class DebugNavMeshAgent : MonoBehaviour
{
    NavMeshAgent agent;
    public bool velocity;
    public bool desiredVelocity;
    public bool path;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
    }


    void OnDrawGizmos()
    {
        if (velocity)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, transform.position + agent.velocity);

        }

        if (desiredVelocity)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + agent.velocity);

        }

        if (path)
        {
            Gizmos.color = Color.black;
            var agentpath = agent.path;
            Vector3 prevcorner = transform.position;
            foreach (var corner in agentpath.corners)
            {
                Gizmos.DrawLine(prevcorner, corner);
                Gizmos.DrawSphere(corner, 0.1f);
                prevcorner = corner;

            }

        }



    }
    
        
}
