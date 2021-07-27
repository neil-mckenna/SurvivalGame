using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    public Vector2 MovementInputVector { get; private set; }

    public Vector3 MovementDirectionVector { get; private set; }

    public Action OnJump { get; set; } 

    private Camera mainCamera;

    private void Start() 
    {
        mainCamera = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
        
    }

    private void Update() 
    {
        GetMovementInput();
        GetMovementDirection();
        GetJumpInput();

    }

    public void GetMovementInput()
    {
        float horz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical"); 

        MovementInputVector = new Vector2(horz, vert);

        //Debug.Log(MovementInputVector);

    }

    public void GetMovementDirection()
    {
        var cameraForwardDirection = mainCamera.transform.forward;

        //Debug.DrawRay(mainCamera.transform.position, cameraForwardDirection * 10f , Color.red);

        MovementDirectionVector = Vector3.Scale(cameraForwardDirection, (Vector3.right + Vector3.forward));

        //Debug.DrawRay(mainCamera.transform.position, MovementDirectionVector * 10f , Color.green);


    }

    public void GetJumpInput()
    {
        if(Input.GetAxisRaw("Jump") > 0)
        {
            OnJump?.Invoke();

        }


    }


}
