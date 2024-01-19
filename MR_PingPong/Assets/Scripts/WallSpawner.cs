using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSpawner : MonoBehaviour
{
    [SerializeField] GameObject wallPrefab;
    [SerializeField] BoxCollider tableCollider;
    GameObject wall = null;

    private void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.name == "Spawned wall")
            {
                wall = child.gameObject;
            }
        }
        if (wall == null)
        {
            SpawnNet();
        }
    }

    void SpawnNet()
    {
        wall = Instantiate(wallPrefab, this.transform);
        wall.name = "Spawned wall";
        MeshRenderer renderer = wall.GetComponentInChildren<MeshRenderer>();
        renderer.enabled = false;
        Rescale();
    }

    void Rescale()
    {
        if (wall != null)
        {
            Vector3 currentSize = wall.transform.localScale;
            float scaleValue = tableCollider.size.z / currentSize.z;
            Vector3 newSize = new Vector3(currentSize.x * scaleValue, currentSize.y * scaleValue, currentSize.z * scaleValue);
            wall.transform.localScale = newSize;
        }
    }
}
