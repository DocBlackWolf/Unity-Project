using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAnimator : MonoBehaviour
{
    private Animator myAnimator;
    private SpriteRenderer mySpriteRenderer;
    private SC_PunchTrap1 myPunchTrap1;
    private string currentState;

    private string TRAP_ACTIVE = "trap";
    private string TRAP_INACTIVE = "idle"; 

    private void OnEnable()
    {
        myAnimator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myPunchTrap1 = GetComponent<SC_PunchTrap1>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
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
        if (myPunchTrap1.playerInside)
        {
            myAnimator.Play(TRAP_ACTIVE);
        }
        else if(!myPunchTrap1.isCooldown)
        {
            myAnimator.Play(TRAP_INACTIVE);
        }
        
    }


}
