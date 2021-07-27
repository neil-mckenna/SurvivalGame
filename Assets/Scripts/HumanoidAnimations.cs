using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidAnimations : MonoBehaviour
{
    private Animator animator;

    private void Awake() 
    {
        animator = (Animator) GetComponent("Animator");
        
    }

    public void TriggerLandingAnimation()
    {
        animator.SetTrigger("Land");
    }

    public void TriggerJumpAnimation()
    {
        animator.SetTrigger("Jump");
    }

    public void TriggerFallAnimation()
    {
        animator.SetTrigger("Fall");
    }

    public void ResetTriggers()
    {
        animator.ResetTrigger("Jump");
        animator.ResetTrigger("Fall");
        animator.ResetTrigger("Land");

    }

    public void SetMovementFloat(float value)
    {
        animator.SetFloat("move", value);

    }

    public float SetCorrectAnimation(float desiredRotAngle, int angleRotationThreshold, int inputVerticalDirection)
    {
        float currentAnimSpeed = animator.GetFloat("move");

        if(desiredRotAngle > angleRotationThreshold || desiredRotAngle < -angleRotationThreshold)
        {
            if(Mathf.Abs(currentAnimSpeed) < 0.2f)
            {
                currentAnimSpeed += inputVerticalDirection * Time.deltaTime * 2f;
                currentAnimSpeed = Mathf.Clamp(currentAnimSpeed, -0.2f, 0.2f);

            }
            SetMovementFloat(currentAnimSpeed);
        }
        else
        {
            if(currentAnimSpeed < 1f)
            {
                currentAnimSpeed += inputVerticalDirection * Time.deltaTime * 2f;

            }
            SetMovementFloat(Mathf.Clamp(currentAnimSpeed, -1, 1));

        }

        return Mathf.Abs(currentAnimSpeed);
    }

}
