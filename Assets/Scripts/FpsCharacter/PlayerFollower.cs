using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollower : MonoBehaviour
{
    public Transform targetTransform; // The target we want to lerp towards
    public float positionLerpSpeed = 5f; // Speed at which to interpolate position

    void Update()
    {
        if (targetTransform != null)
        {
            // Smoothly interpolate the position of the object towards the target's position
            transform.position = Vector3.Lerp(transform.position, targetTransform.position, Time.deltaTime * positionLerpSpeed);
        }
    }
}
