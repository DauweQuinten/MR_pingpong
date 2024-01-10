using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetSpawner : MonoBehaviour
{
    [SerializeField] GameObject netPrefab;
    [SerializeField] BoxCollider tableCollider;
    GameObject net = null;

    private void OnEnable()
    {
        SpawnNet();
    }

    public void SpawnNet()
    {
        net = Instantiate(netPrefab, this.transform);
        tableCollider = transform.parent.GetComponentInChildren<BoxCollider>();
        Vector3 currentSize = net.transform.localScale;
        float scaleValue = tableCollider.size.z / currentSize.z;
        Vector3 newSize = new Vector3(currentSize.x * scaleValue, currentSize.y * scaleValue, currentSize.z * scaleValue);
        net.transform.localScale = newSize;
    }
}
