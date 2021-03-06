using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementState : BaseState
{
    float fallingDelay = 0f;

    public override void EnterState(AgentController controller)
    {
        base.EnterState(controller);
        //controllerReference.agentAnimations.ResetTriggers();
        fallingDelay = 0.2f;
    }

    public override void HandleMovement(Vector2 input)
    {
        base.HandleMovement(input);
        controllerReference.movement.HandleMovement(input);
    }

    public override void HandleCameraDirection(Vector3 input)
    {
        base.HandleCameraDirection(input);
        controllerReference.movement.HandleMovementDirection(input);
    }

    public override void HandleJumpInput()
    {
        controllerReference.TransitionToState(controllerReference.jumpState);
    }

    public override void Update()
    {
        base.Update();

        HandleMovement(controllerReference.input.MovementInputVector);
        HandleCameraDirection(controllerReference.input.MovementDirectionVector);

        if(controllerReference.movement.IsGrounded() == false)
        {
            if(fallingDelay > 0)
            {
                fallingDelay -= Time.deltaTime;
                return;
            }

            controllerReference.TransitionToState(controllerReference.fallingState);
        }
    }

}
