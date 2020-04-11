using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.EventSystems;

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
    public float Modifer = 1;
    public LayerMask IgnoreLayer;
    private float _distance;
    private float _tempModifer = 1;
    private float _modiferInterpolator = 0.02f;
    private float _speedInterpolator = 0.001f;

    void Start()
    {
        _distance = CharacterController.radius + 0.01F + CharacterController.skinWidth;
        _speed = StartSpeed;
    }

    void Update()
    {
        // if (Modifer != 1)
        _tempModifer = Modifer;
        // _tempModifer= Mathf.Lerp(1, Modifer, _modiferInterpolator);

        // Debug.Log(_tempModifer);

        if (CharacterController.isGrounded)
        {
            // We are grounded, so recalculate
            // move direction directly from axes

            moveDirection = new Vector3(Input.GetAxis("Vertical") * -1, 0.0f, 1 * _tempModifer);
            moveDirection *= _speed;

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = JumpSpeed;
            }
        }
        else
        {
            moveDirection.x = 0;
            moveDirection.z = _tempModifer * _speed;
        }


        // if (_inTriggerZone)
        // {
        //     moveDirection.y = 8 + Gravity * Time.deltaTime;
        // }

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer

        var rayOrigin = transform.position;
        rayOrigin.y -= .2f;


        if (Physics.Raycast(rayOrigin, transform.TransformDirection(Vector3.forward), out hit, _distance,
            IgnoreLayer))
        {
            Debug.DrawRay(rayOrigin, transform.TransformDirection(Vector3.forward) * hit.distance,
                Color.yellow);

            moveDirection.y = JumpSpeed;
            moveDirection.x += .5f * Time.deltaTime;
        }
        else
        {
            Debug.DrawRay(rayOrigin, transform.TransformDirection(Vector3.forward) * _distance, Color.white);
        }


        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        moveDirection.y -= Gravity * Time.deltaTime;

        var move = transform.TransformDirection(moveDirection);

        // Move the controller
        CharacterController.Move(move * Time.deltaTime);
    }

    // void OnCollisionEnter(Collision collision)
    // {
    //     foreach (ContactPoint contact in collision.contacts)
    //     {
    //         Debug.DrawRay(contact.point, contact.normal, Color.white);
    //     }
    //
    //     Debug.Log(collision.gameObject.name);
    //
    //     if (collision.gameObject.name == "Cube")
    //     {
    //         Debug.Log("Чпоньк");
    //
    //         GetComponent<Rigidbody>().AddForce(Vector3.up * 3, ForceMode.Impulse);
    //     }
    // }
}