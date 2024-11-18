using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorObjetoLoop : MonoBehaviour
{
    [SerializeField] private GameObject objetoPrefab;

    [SerializeField]
    [Range(0.5f, 5f)]
    private float tiempoEspera;

    [SerializeField]
    [Range(0.5f, 5f)]
    private float tiempoIntervalo;

    [SerializeField] private Vector3 rotationEulerAngles;

    // New rocket-specific fields
    [SerializeField] private Vector2 rocketDirection = Vector2.right;
    [SerializeField] private float rocketSpeed = 5f;

    void GenerarObjetoLoop()
    {
        // Instantiate the prefab with the specified rotation
        Quaternion rotation = Quaternion.Euler(rotationEulerAngles);
        GameObject nuevoObjeto = Instantiate(objetoPrefab, transform.position, rotation);

        // Try to configure SC_Rocket if it's attached to the prefab
        SC_Rocket rocketScript = nuevoObjeto.GetComponent<SC_Rocket>();
        if (rocketScript != null)
        {
            rocketScript.movementDirection = rocketDirection; // Set direction
            rocketScript.speed = rocketSpeed;                 // Set speed
        }
    }

    private void OnBecameVisible()
    {
        InvokeRepeating(nameof(GenerarObjetoLoop), tiempoEspera, tiempoIntervalo);
    }

    private void OnBecameInvisible()
    {
        CancelInvoke(nameof(GenerarObjetoLoop));
    }
}
