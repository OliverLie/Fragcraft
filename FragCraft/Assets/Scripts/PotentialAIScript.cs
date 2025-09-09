using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;
using UnityEngine.Pool;

public class PotentialAIScript : MonoBehaviour
{
    private IObjectPool<PotentialAIScript> enemyPool;
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    public Transform weaponHandPosition;
    public Health EnemyAI;
    private GameObject _player;
    public Transform pelvisBone;

    Animator anim;

    // Patrolling
    Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    // Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;

    // States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    Health health;
    public Rigidbody[] rb;


    public void SetPool(IObjectPool<PotentialAIScript> pool)
    {
        enemyPool = pool;
    }
    private void Awake()
    {
        player = GameObject.Find("FirstPersonPlayer").transform;
        agent = GetComponent<NavMeshAgent>();
        health = GetComponent<Health>();
        anim = GetComponent<Animator>();
        _player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponentsInChildren<Rigidbody>();

    }

    private void Update()
    {
        if (health == null) return; // hvis Health-script mangler

        if (health.currentHealth <= 0f)
        {
            agent.isStopped = true;
            enemyPool.Release(this);  // stop AI'en
            return;                  // fjenden gør intet mere
        }

        // AI logik
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) Attacking();
        if (EnemyAI.currentHealth != 100) ChasePlayer();
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void Attacking()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);


            _player.GetComponent<PlayerHealth>().TakeDamage(10);


        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

    public void AirbenderForce(Vector3 ThrustForce) //Fix script så det virker
    {
        foreach (Rigidbody item in rb)
        {
            item.AddForce(transform.up + ThrustForce);
            Rigidbody targetRb = pelvisBone.GetComponent<Rigidbody>();
            if (targetRb != null)
            targetRb.AddForce(ThrustForce, ForceMode.VelocityChange);
        }
    }
    

        
}
