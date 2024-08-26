using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Attack : MonoBehaviour
{
    // Variables a configurar desde el editor
    [Header("Configuracion")]
    [SerializeField] float puntos = 5f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SC_Life jugador = collision.gameObject.GetComponent<SC_Life>();
            jugador.ModificarVida(-puntos);
            Debug.Log(" PUNTOS DE DAÑO REALIZADOS AL JUGADOR " + puntos);
        }
    }
}