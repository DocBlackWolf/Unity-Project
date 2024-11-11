using System.Collections.Generic;
using UnityEngine;

public class SC_Bag : MonoBehaviour
{
    public List<GameObject> collectibles;
    private int collectedCount;

    void Start()
    {
        collectedCount = 0;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (collectibles.Contains(other.gameObject))
        {
            collectedCount++;
            collectibles.Remove(other.gameObject);
            Destroy(other.gameObject);

            Debug.Log("Objetos coleccionados: " + collectedCount);
        }
    }
}