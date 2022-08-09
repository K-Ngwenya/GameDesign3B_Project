using UnityEngine;
using UnityEngine.AI;

public class Enemy2 : MonoBehaviour
{
    //Code by DanCS

    public float health = 50f;
    public float walkPointRange;
    public float timeBetweenAttacks;
    public float sightRange;
    public float attackRange;
    
    [SerializeField] float walking = 2f;
    [SerializeField] float running = 5f;
    [SerializeField] float chilling = 0f;

    //private int ballCount = 0;

    //public GameObject projectile;
    private Animator animator;

    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public Vector3 walkPoint;

    public bool walkPointSet;
    public bool alreadyAttacked;
    public bool playerInSightRange;
    public bool playerInAttackRange;
    public bool dead = false;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        animator = GetComponent<Animator>();
        
        
    }

    private void Update() 
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if(!dead)
        {
            if(!playerInSightRange && !playerInAttackRange)
            {
                
                Patrolling();
                
                
            }

            if(playerInSightRange && !playerInAttackRange)
            {
                
                ChasePlayer();
                animator.ResetTrigger("walking");
                animator.SetTrigger("running");
                
            }

            if(playerInAttackRange && playerInSightRange)
            {
                AttackPlayer();
                
            }
        }
    }

    private void Patrolling()
    {
        
        if(!walkPointSet)
        {
            SearchWalkPoint();
        }

        if(walkPointSet)
        {
            agent.SetDestination(walkPoint);
            animator.ResetTrigger("running");
            animator.SetTrigger("walking");
        }

        transform.LookAt(walkPoint);
        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        

        if(distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    private void ChasePlayer()
    {
        transform.LookAt(player);
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(player);
        
        /*if(!alreadyAttacked)
        {

            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);
            alreadyAttacked = true;
            //ballCount += 1;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
            Invoke(nameof(BallRemover), 2f);
        }*/
    }

    /*private void BallRemover()
    {
        GameObject clutter = GameObject.Find("Bullet(Clone)");
        Destroy(clutter);
    }*/

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if(Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }

    /*public void TakeDamage(float amount)
    {
        health -= amount;
        if(health == 50f)
        {
            var colourThingy = gameObject.GetComponent<Renderer>();
            colourThingy.material.SetColor("_Color", Color.green);

        }
        if(health <= 0f)
        {
            Die();
        }
    }

    private void Die() {
        {
           Destroy(gameObject);
        }
    }*/
}

