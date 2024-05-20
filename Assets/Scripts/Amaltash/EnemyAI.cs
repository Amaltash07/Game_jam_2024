using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public float detectionRange = 10f; // How close the player needs to be for the enemy to detect them
    public float grappleRange = 2f; // Range at which the enemy will grapple onto the player
    public float moveSpeed = 3.5f; // Movement speed of the enemy
    public float grappleSpeed = 10f; // Speed at which the enemy moves when grappling
    public float grappleDuration = 2f; // Duration of the grapple
    public float playerDamage = 30f; // Damage dealt to the player on grapple

    private bool isGrappling = false;
    private NavMeshAgent navMeshAgent;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        if (navMeshAgent == null)
        {
            Debug.LogError("NavMeshAgent component not found on the enemy.");
        }
        else
        {
            navMeshAgent.speed = moveSpeed;
        }
    }

    void Update()
    {
        if (isGrappling) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            if (distanceToPlayer <= grappleRange)
            {
                StartCoroutine(GrappleToPlayer());
            }
            else
            {
                // Move towards the player
                navMeshAgent.SetDestination(player.position);
            }
        }
        else
        {
            // Stop moving if the player is out of range
            navMeshAgent.ResetPath();
        }
    }

    private IEnumerator GrappleToPlayer()
    {
        isGrappling = true;
        navMeshAgent.enabled = false;

        float grappleStartTime = Time.time;
        Vector3 grappleStartPos = transform.position;

        while (Time.time < grappleStartTime + grappleDuration)
        {
            transform.position = Vector3.Lerp(grappleStartPos, player.position, (Time.time - grappleStartTime) / grappleDuration);
            yield return null;
        }

        // Deal damage to the player and destroy the enemy
        if (Vector3.Distance(transform.position, player.position) <= grappleRange)
        {
            HealthSystem playerHealth = player.GetComponent<HealthSystem>();
            if (playerHealth != null)
            {
                playerHealth.Damage(playerDamage);
            }
            Destroy(gameObject);
        }

        // Re-enable the NavMeshAgent if the enemy is not destroyed
        if (this != null)
        {
            navMeshAgent.enabled = true;
            isGrappling = false;
        }
    }
}
