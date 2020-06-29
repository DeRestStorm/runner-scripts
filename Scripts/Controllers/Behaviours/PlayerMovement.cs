using System;
using System.Collections;
using System.Collections.Generic;
using Factories;
using Scripts.Enums;
using Scripts.Interfaces;
using Scripts.Models;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(CapsuleCollider))]
public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private CharacterController _characterController;
    private CapsuleCollider _collider;
    public float StartSpeed;
    [Range(0, 2)] public float HorisontalSpeedModifer = 1;
    [Range(0, 2)] public float HorisontalSpeedInJumpModifer = 1;
    public float MaxSpeed;
    [SerializeField] private float _speed;
    private float _additionalSpeed;
    [SerializeField] private float _speedRatio;
    private float _acceleration;
    public float Modifer = 1;
    private float _baseHeight;
    private Vector3 _baseCenter;
    private float _slideHeight;
    private Vector3 _oldPosition;
    private Vector3 _slideCenter;
    public AnimationCurve AcelecarionCurve;
    private float _speedGap;
    private bool _dashFlag;
    public float DashCost = 1f;
    [Inject] private IItemRepository<Item> _itemRepository;
    private bool _slide;

    public float DashDistance;

    public float JumpHeight;

    [Inject] private ScrapFactory _scrapFactory;

    [Inject(Id = "CharacterAnimator")] private Animator _animatorController;
    [Range(1, 15)] public float RotarionSpeed = 1;
    private Rigidbody _body;
    private Transform _groundChecker;
    private bool _isGrounded;
    private Vector3 _inputs;
    public LayerMask Ground;
    public float GroundDistance;

    void Start()
    {
        _collider = GetComponent<CapsuleCollider>();
        _speed = StartSpeed;
        _baseCenter = _collider.center;
        _baseHeight = _collider.height;
        _slideHeight = _baseHeight / 3;
        _slideCenter = new Vector3(_baseCenter.x, _baseCenter.y - (_slideHeight / 2), _baseCenter.z);
        _speedGap = MaxSpeed - StartSpeed;

        _body = GetComponent<Rigidbody>();
        _groundChecker = transform.GetChild(0);
    }

    private void SpeedUpdate()
    {
        if (_additionalSpeed > _speedGap)
            _additionalSpeed = _speedGap;
        _speed = StartSpeed + _additionalSpeed;


        if (_slide)
        {
            if (_speed - .3f > 0)
                _additionalSpeed -= .085f;
        }
        else
        {
            _speedRatio = _additionalSpeed / _speedGap;

            _additionalSpeed += AcelecarionCurve.Evaluate(_speedRatio);
        }
    }

    void Update()
    {
        _isGrounded = Physics.CheckSphere(_groundChecker.position, GroundDistance, Ground,
            QueryTriggerInteraction.Ignore);
        _animatorController.SetBool("ifGround", !_isGrounded);


        _inputs = Vector3.forward;

        _inputs.x = Input.GetAxis("Vertical") * -1 *
                    (_isGrounded ? HorisontalSpeedModifer : HorisontalSpeedInJumpModifer);

        _inputs = transform.TransformVector(_inputs);

        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _body.AddForce(Vector3.up * JumpHeight, ForceMode.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Dash();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            _slide = true;
            _collider.height = _slideHeight;
            _collider.center = _slideCenter;
            _animatorController.SetBool("Slide", _slide);
        }

        if (Input.GetKeyUp(KeyCode.C) || Input.GetKeyDown(KeyCode.Space))
        {
            _slide = false;

            _collider.height = _baseHeight;
            _collider.center = _baseCenter;
            _animatorController.SetBool("Slide", _slide);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (_itemRepository.Get(ItemType.Scrap).Count > 0)
            {
                _itemRepository.Add(new Item(ItemType.Scrap, -1));

                _scrapFactory.Create(transform.position);
            }
        }
    }


    private void Dash()
    {
        if (_dashFlag) return;

        var batteryCount = _itemRepository.Get(ItemType.Battery).Count;
        var balance = batteryCount - DashCost;
        if (balance < 0)
            return;

        _itemRepository.Add(new Item() {Type = ItemType.Battery, Count = -DashCost});
        _body.drag = 5;
        _dashFlag = true;

        Vector3 dashVelocity = Vector3.Scale(transform.forward,
            DashDistance * new Vector3((Mathf.Log(1f / (Time.deltaTime * _body.drag + 1)) / -Time.deltaTime), 0,
                (Mathf.Log(1f / (Time.deltaTime * _body.drag + 1)) / -Time.deltaTime)));
        _body.AddForce(dashVelocity, ForceMode.VelocityChange);
        StartCoroutine(nameof(DashCorutine));
    }


    IEnumerator DashCorutine()
    {
        yield return new WaitForSeconds(.3f);

        _body.drag = 0;
        _dashFlag = false;
    }


    void FixedUpdate()
    {
        _body.MovePosition(_body.position + _inputs * (_speed * Time.fixedDeltaTime));
        SpeedUpdate();
    }
}