using UnityEngine;
using UnityEngine.AI;

public class ChasingSystem : MonoBehaviour
{
    public GameObject player; 
    public float chaseRange = 10f; 
    public float callRate = 1f;
    public float Damage = 10f;

    private NavMeshAgent agent;
    private bool isChasing = false;
    private float timer = 0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        agent.SetDestination(player.transform.position);
        // Check if the player.transform is within range
        if (Vector3.Distance(transform.position, player.transform.position) <= chaseRange)
        {
            isChasing = true;
        }
        else
        {
            isChasing = false;
        }

        // If chasing, set the destination to the player.transform position
        if (isChasing)
        {
            
            // Increment timer
            timer += Time.deltaTime;

            // Check if it's time to call the function
            if (timer >= callRate)
            {
                ExecuteInRangeFunction();
                timer = 0f; // Reset timer
            }
        }
    }

    // Function to execute when in range of the player.transform
    void ExecuteInRangeFunction()
    {
        // add particle effects
        Debug.Log("hit");
        player.GetComponent<HealthSystem>().Damage(Damage);
        
    }
}
