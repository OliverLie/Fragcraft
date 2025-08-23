using UnityEngine;
using Unity.AI;
using UnityEngine.AI;
public class AiLocoMotion : MonoBehaviour
{
    public Transform playerTransform;
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
        agent.destination = playerTransform.position;
        anim.SetFloat("Speed", agent.velocity.magnitude);
    }
}
