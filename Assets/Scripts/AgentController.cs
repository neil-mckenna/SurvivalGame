using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentController : MonoBehaviour
{
    public AgentMovement movement;
    public PlayerInput playerInput;

    private void Start() 
    {
        movement = (AgentMovement) GetComponent("AgentMovement");
        playerInput = (PlayerInput) GetComponent("PlayerInput");
    }

    private void Update() 
    {
        movement.HandleMovement(playerInput.MovementInputVector);
        movement.HandleMovementDirection(playerInput.MovementDirectionVector);

    }

}
