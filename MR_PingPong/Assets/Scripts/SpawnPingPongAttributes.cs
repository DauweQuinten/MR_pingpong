using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPingP : MonoBehaviour
{
    [SerializeField, Tooltip("Prefab of the ball to spawn")]
    GameObject ballPrefab;
    [SerializeField, Tooltip("Prefab of the bat to spawn")]
    GameObject batPrefab;
    
    GameObject SpawnObjectAtLocation(GameObject objectToSpawn, Transform spawnLocation)
    {
        if (objectToSpawn == null)
        {
            return null;
        }

        GameObject spawnedObject = Instantiate(objectToSpawn, spawnLocation.position, spawnLocation.rotation);
        return spawnedObject;
    }
}
