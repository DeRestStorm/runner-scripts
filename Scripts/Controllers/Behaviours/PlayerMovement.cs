using System;
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
    public float StartSpeed;
    [Range(0, 2)] public float HorisontalSpeedModifer = 1;
    [Range(0, 2)] public float HorisontalSpeedInJumpModifer = 1;
    public float MaxSpeed;
    public float JumpSpeed;
    public float Gravity = 20.0f;
    [SerializeField] private float _speed;
    private float _additionalSpeed;
    [SerializeField] private float _speedRatio;
    private float _acceleration;
    private Vector3 moveDirection = Vector3.zero;
    public float Modifer = 1;
    private float _tempModifer = 1;
    private float _modiferInterpolator = 0.02f;
    private float _speedInterpolator = 0.001f;
    public float VerticalSpeed = 1;
    private float _baseHeight;
    private Vector3 _baseCenter;
    private float _slideHeight;
    private Vector3 _oldPosition;
    private Vector3 _slideCenter;
    public AnimationCurve AcelecarionCurve;
    private float _speedGap;
    public float Boost = 1;
    private bool _boostFlag;
    public float BoostCost = 0.01f;
    [Inject] private IItemRepository<Item> _itemRepository;
    private bool _slide;

    public float DashDistance;

    public float JumpHeight;

    [Inject] private ScrapFactory _scrapFactory;
    // private bool _isGrounded;

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
        // _characterController = GetComponent<CharacterController>();
        _speed = StartSpeed;
        // _baseCenter = _characterController.center;
        // _baseHeight = _characterController.height;
        // _slideHeight = _baseHeight / 3;
        // _slideCenter = new Vector3(_baseCenter.x, _baseCenter.y - (_slideHeight / 2), _baseCenter.z);
        _oldPosition = transform.position;
        _speedGap = MaxSpeed - StartSpeed;

        _body = GetComponent<Rigidbody>();
        _groundChecker = transform.GetChild(0);
    }

    void FixedUpdateOld()
    {
        if (_additionalSpeed > _speedGap)
            _additionalSpeed = _speedGap;

        // if (_additionalSpeed <= -StartSpeed)
        //     _additionalSpeed = .01f;

        _speed = StartSpeed + _additionalSpeed;

        var isGrounded = _characterController.isGrounded;
        _animatorController.SetBool("ifGround", !isGrounded);
        _tempModifer = Modifer;

        if (isGrounded)
        {
            // We are grounded, so recalculate
            // move direction directly from axes

            moveDirection = new Vector3((Input.GetAxis("Vertical") * -VerticalSpeed * HorisontalSpeedModifer), 0.0f,
                1 * _tempModifer);
            moveDirection *= _speed;

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = JumpSpeed;
            }
        }
        else
        {
            moveDirection.x = (Input.GetAxis("Vertical") * -VerticalSpeed) * HorisontalSpeedInJumpModifer * _speed;
            moveDirection.z = _tempModifer * _speed;
        }

        _speedRatio = _additionalSpeed / _speedGap;

        if (_slide)
        {
            _characterController.height = _slideHeight;
            _characterController.center = _slideCenter;
            // Debug.Log(_characterController.velocity.normalized);

            if (_oldPosition.y - transform.position.y < -.001f)
            {
                _additionalSpeed += .015f;
            }
            else
            {
                if (_speed - .3f > 0)
                    _additionalSpeed -= .085f;
                //     _additionalSpeed = .01f;
            }
        }
        else
        {
            // _additionalSpeed += BaseAcceleration;
            // if (_speed * 2 < MaxSpeed)
            // {
            _speedRatio = _additionalSpeed / _speedGap;

            _additionalSpeed += AcelecarionCurve.Evaluate(_speedRatio);

            // }


            _characterController.height = _baseHeight;
            _characterController.center = _baseCenter;
        }

        BoostMethod();


        _animatorController.SetBool("Slide", _slide);
        _animatorController.SetFloat("speed", moveDirection.normalized.magnitude);

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        // moveDirection.y -= Gravity * Time.deltaTime;

        var move = transform.TransformDirection(moveDirection);

        // Move the controller
        _characterController.Move(move * Time.deltaTime);
    }


    private void BoostMethod()
    {
        if (!_boostFlag) return;

        var batteryCount = _itemRepository.Get(ItemType.Battery).Count;

        var balance = batteryCount - BoostCost;


        var tempBalance = batteryCount - BoostCost;
        var abs = tempBalance % 1;

        if (abs <= BoostCost)
        {
            _boostFlag = false;
            Modifer -= Boost;
            _itemRepository.Add(new Item() {Type = ItemType.Battery, Count = -(batteryCount - (int) balance)});
            return;
        }

        _itemRepository.Add(new Item() {Type = ItemType.Battery, Count = -BoostCost});
    }


    void Update()
    {
        _isGrounded = Physics.CheckSphere(_groundChecker.position, GroundDistance, Ground,
            QueryTriggerInteraction.Ignore);


        _inputs = Vector3.forward;
        // _inputs.x = Input.GetAxis("Horizontal");
        _inputs.x = Input.GetAxis("Vertical") * -1 * HorisontalSpeedModifer;
        // _inputs = _inputs * transform.forward;
        // if (_inputs != Vector3.zero)
        //     transform.forward = _inputs;

        _inputs = transform.TransformVector(_inputs);
        
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _body.AddForce(Vector3.up * Mathf.Sqrt(JumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Vector3 dashVelocity = Vector3.Scale(transform.forward,
                DashDistance * new Vector3((Mathf.Log(1f / (Time.deltaTime * _body.drag + 1)) / -Time.deltaTime), 0,
                    (Mathf.Log(1f / (Time.deltaTime * _body.drag + 1)) / -Time.deltaTime)));
            _body.AddForce(dashVelocity, ForceMode.VelocityChange);
        }
    }


    void FixedUpdate()
    {
        _body.MovePosition(_body.position + _inputs * (_speed * Time.fixedDeltaTime));
    }

    private void UpdateOld()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !_boostFlag)
        {
            if (_itemRepository.Get(ItemType.Battery).Count >= 1)
            {
                _boostFlag = true;
                Modifer += Boost;
            }
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            _slide = true;
        }

        if (Input.GetKeyUp(KeyCode.C) || Input.GetKeyDown(KeyCode.Space))
        {
            _slide = false;
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
}