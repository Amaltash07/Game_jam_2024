using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Sha3dowEnemy : MonoBehaviour
{
   public NavMeshAgent agent;
   public Transform player;
   public LayerMask whatIsGround;
   public LayerMask whatIsPlayer;

    public float Health;
    Vector3 cameraDir;

    //Patrolling
   public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Atatcking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //States
    public float sightRange; 
    public float attackRange;
    public bool playerInSightRange;
    public bool playerInAttackRange;

    //Attack
    public GameObject projectile;

    private void Awake()
    {
        player = GameObject.Find("Sha3dowPlayer").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patrolling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) AttackPlayer();

        //Always Face Player
        cameraDir = Camera.main.transform.forward;
        cameraDir.y = 0;

        transform.rotation= Quaternion.LookRotation(cameraDir);
    }
    private void Patrolling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walpoint reached
        if (distanceToWalkPoint.magnitude<1f)
            walkPointSet = false;
    } 
    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }
    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player);
        ///Attack code here
      Rigidbody rb= Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * 32f, ForceMode.Impulse );
        rb.AddForce(transform.up * 8f, ForceMode.Impulse);
        ///

        if (!alreadyAttacked) 
        {
            alreadyAttacked = true;
            Invoke(nameof(ResetAttck), timeBetweenAttacks);
        }    



    }

    private void SearchWalkPoint()
    {
        //CAlculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ResetAttck()
    {
        alreadyAttacked = false;    
    }
    
    private void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health <= 0 ) Invoke(nameof(DestroyEnemyy), 5f);
    }

    private void DestroyEnemyy()
    {
        Destroy(gameObject);
    }

    //Debug
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
