using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public CharacterController CharacterController;
    public float StartSpeed;
    [Range(0,1)]
    public float HorisontalSpeedModifer = 1;
    public float MaxSpeed;
    public float BaseAcceleration;
    public float JumpSpeed;
    public float Gravity = 20.0f;
    private float _speed;
    private float _acceleration;
    private Vector3 moveDirection = Vector3.zero;
    public float Modifer = 1;
    private float _tempModifer = 1;
    private float _modiferInterpolator = 0.02f;
    private float _speedInterpolator = 0.001f;
    public float VerticalSpeed = 1;
    public CapsuleCollider _collider;
    private float _baseHeight;
    private Vector3 _baseCenter;
    private float _slideHeight;
    private Vector3 _slideCenter;
    // private bool _isGrounded;

    [Inject(Id = "CharacterAnimator")] private Animator _animatorController;

    void Start()
    {
        _speed = StartSpeed;
        _collider = GetComponent<CapsuleCollider>();
        _baseCenter = CharacterController.center;
        _baseHeight = CharacterController.height;
        _slideHeight = _baseHeight / 3;
        _slideCenter = new Vector3(_baseCenter.x, _baseCenter.y - (_slideHeight / 2), _baseCenter.z);
    }

    void Update()
    {
        var isGrounded = CharacterController.isGrounded;
        // var groundRay = new Ray(transform.position, Vector3.down);
        // RaycastHit rh;
        // _isGrounded = Physics.Raycast(groundRay, out rh, 0.5f);

        _animatorController.SetBool("ifGround", !isGrounded);
        // if (Modifer != 1)
        _tempModifer = Modifer;
        // _tempModifer= Mathf.Lerp(1, Modifer, _modiferInterpolator);

        if (isGrounded)
        {
            // We are grounded, so recalculate
            // move direction directly from axes

            moveDirection = new Vector3((Input.GetAxis("Vertical") * -VerticalSpeed  * HorisontalSpeedModifer), 0.0f, 1 * _tempModifer);
            moveDirection *= _speed;

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = JumpSpeed;
            }
        }
        else
        {
            moveDirection.x = (Input.GetAxis("Vertical") * -VerticalSpeed )  * HorisontalSpeedModifer * _speed; 
            moveDirection.z = _tempModifer * _speed;
        }

        Debug.Log(Input.GetKey(KeyCode.C));

        var slide = Input.GetKey(KeyCode.C);

        _animatorController.SetBool("Slide", slide);

        if (slide)
        {
            CharacterController.height = _slideHeight;
            CharacterController.center = _slideCenter;
        }
        else
        {
            CharacterController.height = _baseHeight;
            CharacterController.center = _baseCenter;
        }

        // _animatorController.SetBool("Slide", Input.GetKey(KeyCode.C));
        _animatorController.SetBool("Slide", Input.GetKey(KeyCode.C));

        _animatorController.SetFloat("speed", moveDirection.normalized.magnitude);

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