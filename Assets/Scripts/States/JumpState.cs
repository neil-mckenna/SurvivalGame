using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : BaseState
{
    bool landingTrigger = false;
    float delay = 0;
    public override void EnterState(AgentController controller)
    {
        base.EnterState(controller);
        delay = 0.2f;
        landingTrigger = false;
        controllerReference.movement.HandleJump();
    }
    public override void Update()
    {
        base.Update();
        if(delay > 0)
        {
            delay -= Time.deltaTime;
            return;
        }
        Debug.Log("timer done");

        if(controllerReference.movement.IsGrounded())
        {
            if(landingTrigger == false)
            {
                landingTrigger = true;
                controllerReference.agentAnimations.TriggerLandingAnimation();
            }

            if(controllerReference.movement.HasFinishedJumping())
            {
                controllerReference.TransitionToState(controllerReference.movementState);

            }
        }
        
    }

}
