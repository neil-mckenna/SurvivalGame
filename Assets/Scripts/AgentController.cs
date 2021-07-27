using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentController : MonoBehaviour
{
    public AgentMovement movement;
    public PlayerInput input;

    BaseState currentState;
    public readonly BaseState movementState = new MovementState();
    public readonly BaseState jumpState = new JumpState();

    private void OnEnable() 
    {
        movement = (AgentMovement) GetComponent("AgentMovement");
        input = (PlayerInput) GetComponent("PlayerInput");
        currentState = movementState;
        currentState.EnterState(this);
        AssignMovementInputListeners();
            
    }

    private void OnDisable() 
    {
        input.OnJump -= currentState.HandleJumpInput;
        
    }

    public void AssignMovementInputListeners()
    {
        input.OnJump += HandleJump;

    }

    private void HandleJump()
    {
        currentState.HandleJumpInput();
    }

    private void Update() 
    {
        currentState.Update();

    }

    public void TransitionToState(BaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

}
