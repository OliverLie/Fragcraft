using UnityEngine;
using Unity.AI;
using UnityEngine.AI;
public class AiLocoMotion : MonoBehaviour
{

    NavMeshAgent agent;
    Animator anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.hasPath)
        {
            anim.SetFloat("Speed", agent.velocity.magnitude);
        }
        else
        {
            anim.SetFloat("Speed", 0);
        }
        
    }
}
