using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_CealingTrapAnimator : MonoBehaviour
{

    private Animator myAnimator;
    private SpriteRenderer mySpriteRenderer;
    private SC_CeilingTrap1 myCeilingTrap1;
    private string currentState;

    private string TRAP_ACTIVE = "active";
    private string TRAP_INACTIVE = "idle";


    private void OnEnable()
    {
        myAnimator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myCeilingTrap1 = GetComponent<SC_CeilingTrap1>();
    }

    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        myAnimator.Play(newState);

        currentState = newState;
    }

    // Update is called once per frame
    void Update()
    {
        if (myCeilingTrap1.IsCooldown())
        {
            myAnimator.Play(TRAP_ACTIVE);
        }
        else if (!myCeilingTrap1.IsCooldown())
        {
            myAnimator.Play(TRAP_INACTIVE);
        }
    }
}
