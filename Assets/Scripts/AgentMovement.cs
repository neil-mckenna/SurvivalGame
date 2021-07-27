using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentMovement : MonoBehaviour
{
    protected CharacterController characterController;
    protected HumanoidAnimations agentAnimations;
    public float movementSpeed;
    public float gravity;
    public float rotationSpeed;
    public float jumpSpeed;

    public int angleRotationThreshold;

    protected Vector3 moveDirection = Vector3.zero;
    protected float desiredRotationAngle = 0f;

    int inputVerticalDirection = 0;

    bool isJumping = false;

    private void Start() 
    {
        characterController = (CharacterController) GetComponent("CharacterController");
        agentAnimations = (HumanoidAnimations) GetComponent("HumanoidAnimations");
        
    }

    public void HandleMovement(Vector2 input)
    {
        if(characterController.isGrounded)
        {
            // move 
            if(input.y != 0f)
            {
                if(input.y > 0f)
                {
                    inputVerticalDirection = Mathf.CeilToInt(input.y);
                }
                else
                {
                    inputVerticalDirection = Mathf.FloorToInt(input.y);

                }
                
                moveDirection = input.y * transform.forward * movementSpeed;

            }
            // stop
            else
            {
                agentAnimations.SetMovementFloat(0f);
                moveDirection = Vector3.zero;


            }

        }

    }

    public void HandleMovementDirection(Vector3 input)
    {
        var forwardDir = transform.forward;

        desiredRotationAngle = Vector3.Angle(forwardDir, input);
        var crossProductY = Vector3.Cross(forwardDir, input).y; 

        if(crossProductY < 0f)
        {
            desiredRotationAngle *= -1;
        }
        
    }

    public void HandleJump()
    {
        if(characterController.isGrounded)
        {
            isJumping = true;
        }
    }

    public void RotateAgent()
    {
        if(desiredRotationAngle > angleRotationThreshold || desiredRotationAngle < -angleRotationThreshold)
        {
            transform.Rotate(Vector3.up * desiredRotationAngle * rotationSpeed * Time.deltaTime);
        }

    }

    private void Update() 
    {
        float delta = Time.deltaTime;

        if(characterController.isGrounded)
        {
            if(moveDirection.magnitude > 0)
            {
                var animationSpeedMultipler = agentAnimations.SetCorrectAnimation(desiredRotationAngle, angleRotationThreshold, inputVerticalDirection);
                RotateAgent();
                moveDirection *= animationSpeedMultipler;
            }
        }


        moveDirection.y -= gravity;
        if(isJumping)
        {
            isJumping = false;
            moveDirection.y = jumpSpeed;
            agentAnimations.SetMovementFloat(0f);
        }

        characterController.Move(moveDirection * delta);

    }


}
