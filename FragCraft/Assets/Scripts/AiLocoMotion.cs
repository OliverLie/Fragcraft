using UnityEngine;
using Unity.AI;
using UnityEngine.AI;
public class AiLocoMotion : MonoBehaviour
{
    public Transform playerTransform;
    public float maxTime = 1.0f;
    public float maxDistance = 1.0f;
    float timer = 0.0f;
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
        timer -= Time.deltaTime;
        if (timer < 0.0f)
        {
            float sqrdistance = (playerTransform.position - agent.destination).sqrMagnitude;
            if (sqrdistance > maxDistance*maxDistance)
            {
                agent.destination = playerTransform.position;
            }
            timer = maxTime;

        }
        anim.SetFloat("Speed", agent.velocity.magnitude);
    }
}
