using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySpawner : MonoBehaviour
{
    public int Cost = 10;
    public Transform spawnArea;
    public GameObject spawnAreaPrefab;
    // Start is called before the first frame update
    public void spawn()
    {
        Instantiate(spawnAreaPrefab, spawnArea.position, spawnArea.rotation);
    }
}
