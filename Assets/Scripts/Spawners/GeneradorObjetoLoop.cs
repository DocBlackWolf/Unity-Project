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

    // New rotation fields
    [SerializeField]
    private Vector3 rotationEulerAngles;


    void GenerarObjetoLoop()
    {
        // Use rotationEulerAngles to set the rotation of the spawned object
        Quaternion rotation = Quaternion.Euler(rotationEulerAngles);
        Instantiate(objetoPrefab, transform.position, rotation);
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
