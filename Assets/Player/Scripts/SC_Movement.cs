using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Movement : MonoBehaviour
{
    [Header("Configuracion")]
    [SerializeField] float velocidad = 5f;
    [SerializeField] AudioClip walkingSound; 
    private AudioSource audioSource; 

    private Vector2 direccion;
    private Rigidbody2D miRigidbody2D;
    private Animator miAnimator; 
    private SpriteRenderer miSpriteRenderer;

    private void OnEnable()
    {
        miRigidbody2D = GetComponent<Rigidbody2D>();
        miAnimator = GetComponent<Animator>(); 
        miSpriteRenderer = GetComponent<SpriteRenderer>(); 
        audioSource = GetComponent<AudioSource>(); 
    }

    private void Update()
    {
        direccion = Vector2.zero;

 
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            direccion.x = 1f;
            miAnimator.SetFloat("Velocity", 2f);
            miSpriteRenderer.flipX = false;
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            direccion.x = -1f;
            miAnimator.SetFloat("Velocity", 2f);
            miSpriteRenderer.flipX = true;
        }
        else
        {
            miAnimator.SetFloat("Velocity", 0f);
        }

      
        if (direccion.x != 0 && !audioSource.isPlaying)
        {
            audioSource.clip = walkingSound; 
            audioSource.Play(); 
        }
        else if (direccion.x == 0 && audioSource.isPlaying)
        {
            audioSource.Stop(); 
        }
    }

    private void FixedUpdate()
    {
    
        if (direccion.x != 0)
        {
            miRigidbody2D.AddForce(direccion * velocidad);
        }
    }
}
