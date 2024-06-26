using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheckpoints : MonoBehaviour
{

    private Vector3 lastCheckpointPosition;
    private CharacterController characterController;

    void Start()
    {
        // Initialize the last checkpoint position to the player's starting position
        lastCheckpointPosition = transform.position;
        characterController = GetComponent<CharacterController>();

        if (characterController == null)
        {
            Debug.LogError("CharacterController component not found on the player.");
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
        // Disable CharacterController to allow direct position set
        characterController.enabled = false;

        // Move the player back to the last checkpoint position
        transform.position = lastCheckpointPosition;

        // Re-enable CharacterController
        characterController.enabled = true;

        Debug.Log("Player restarted from checkpoint: " + lastCheckpointPosition);
    }
}
