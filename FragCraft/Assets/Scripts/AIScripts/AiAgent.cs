using UnityEngine;
using UnityEngine.AI;

public class AiAgent : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public AiStateMachine stateMachine;
    public AiStateID initialState;
    public AiAgentConfig config;
    public Ragdoll ragdoll;
    public SkinnedMeshRenderer mesh;
    public UiHealthbar ui;
    public Transform playerTransform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        ragdoll = GetComponent<Ragdoll>();
        mesh = GetComponentInChildren<SkinnedMeshRenderer>();
        ui = GetComponentInChildren<UiHealthbar>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;



        navMeshAgent = GetComponent<NavMeshAgent>();
        stateMachine = new AiStateMachine(this);
        stateMachine.RegisterState(new AiChasePlayerState());
        stateMachine.ChangeStates(initialState);
        stateMachine.RegisterState(new AiDeathState());
        stateMachine.RegisterState(new AiIdleState());
        
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
    }
}
