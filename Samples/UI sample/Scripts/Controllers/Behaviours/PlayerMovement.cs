using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public CharacterController CharacterController;
    public float StartSpeed;
    public float MaxSpeed;
    public float BaseAcceleration;
    public float JumpSpeed;
    public float Gravity = 20.0f;
    private float _speed;
    private float _acceleration;
    private Vector3 moveDirection = Vector3.zero;
    void Start()
    {
        _speed = StartSpeed;
    }

    void Update()
    {
        if (CharacterController.isGrounded)
        {
            // We are grounded, so recalculate
            // move direction directly from axes

            moveDirection = new Vector3(Input.GetAxis("Vertical") * -1, 0.0f, 1);
            moveDirection *= _speed;

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = JumpSpeed;
            }
        }
        else
        {
            moveDirection.x = 0;
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        moveDirection.y -= Gravity * Time.deltaTime;

        var move = transform.TransformDirection(moveDirection);

        // Move the controller
        CharacterController.Move(move * Time.deltaTime);
    }
}
