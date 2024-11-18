using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_DirectAnimator : MonoBehaviour
{
    private Animator miAnimator;
    private SpriteRenderer miSpriteRenderer;
    private SC_Movement movementScript;
    private SC_Life life_Script;
    private string currentState;

    private string PLAYER_IDLE = "Idle";
    private string PLAYER_RUN = "Run";
    private string PLAYER_JUMP = "Jump";
    private string PLAYER_LATCH = "Latch";
    private string PLAYER_DEATH = "Death";

    private void OnEnable()
    {
        miAnimator = GetComponent<Animator>();
        miSpriteRenderer = GetComponent<SpriteRenderer>();
        movementScript = GetComponent<SC_Movement>();
        life_Script = GetComponent<SC_Life>();

    }

    // Start is called before the first frame update


    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        miAnimator.Play(newState);

        currentState = newState;
    }



    // Update is called once per frame
    void Update()
    {


            if (!life_Script.IsAlive())
            {
                ChangeAnimationState(PLAYER_DEATH);
            }

        if (life_Script.IsAlive())
        {

            if (movementScript.Direction.x != 0 && !movementScript.Latching && movementScript.IsGrounded())
            {
                ChangeAnimationState(PLAYER_RUN);

            }

            if (movementScript.Direction.x == 0 && !movementScript.Latching && movementScript.IsGrounded())
            {

                ChangeAnimationState(PLAYER_IDLE);

            }

            if (!movementScript.Latching && movementScript.Direction.x != 0)
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
}
