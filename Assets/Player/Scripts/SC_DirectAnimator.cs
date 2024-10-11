using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_DirectAnimator : MonoBehaviour
{
    private Animator miAnimator;
    private SpriteRenderer miSpriteRenderer;
    private SC_Movement movementScript;
    private string currentState;

    private string PLAYER_IDLE = "Idle";
    private string PLAYER_RUN = "Run";
    private string PLAYER_JUMP = "Jump";
    private string PLAYER_LATCH = "Latch";

    private void OnEnable()
    {
        miAnimator = GetComponent<Animator>();
        miSpriteRenderer = GetComponent<SpriteRenderer>();
        movementScript = GetComponent<SC_Movement>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        miAnimator.Play(newState);

        currentState = newState;
    }



    // Update is called once per frame
    void Update()
    {
        if (movementScript.Direction.x != 0 && !movementScript.Latching && movementScript.IsGrounded())
        {
            ChangeAnimationState(PLAYER_RUN);

        }

        if(movementScript.Direction.x == 0 && !movementScript.Latching && movementScript.IsGrounded())
        {

            ChangeAnimationState(PLAYER_IDLE);

        }

        if (!movementScript.Latching)
        {
            miSpriteRenderer.flipX = movementScript.Direction.x < 0;
        }

        if (!movementScript.IsGrounded() && !movementScript.Latching)
        {
            ChangeAnimationState(PLAYER_JUMP);
        }

        if (movementScript.Latching)
        {
            ChangeAnimationState(PLAYER_LATCH);
        }

    }
}
