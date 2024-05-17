using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheckpoints : MonoBehaviour
{

    private Vector3 lastCheckpointPosition;
    [SerializeField]
    private Rigidbody playerRigidbody;
    // Start is called before the first frame update
    void Start()
    {
        // Initialize the last checkpoint position to the player's starting position
        lastCheckpointPosition = transform.position;
        playerRigidbody = GetComponent<Rigidbody>();

        if (playerRigidbody == null)
        {
            Debug.LogError("Rigidbody component not found on the player.");
        }
    }

    void Update()
    {
        // For testing, you can press the R key to restart the player from the last checkpoint
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Restarting from last checkpoint...");
            RestartFromLastCheckpoint();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the player collides with a checkpoint
        if (other.CompareTag("Checkpoint"))
        {
            lastCheckpointPosition = other.transform.position;
            Debug.Log("Checkpoint updated to: " + lastCheckpointPosition);
        }
    }

    public void RestartFromLastCheckpoint()
    {
        // Move the player back to the last checkpoint position
        transform.position = lastCheckpointPosition;

        // Optionally reset the velocity if using a Rigidbody
        if (playerRigidbody != null)
        {
            playerRigidbody.velocity = Vector3.zero;
            playerRigidbody.angularVelocity = Vector3.zero;
        }

        Debug.Log("Player restarted from checkpoint: " + lastCheckpointPosition);
    }
}
