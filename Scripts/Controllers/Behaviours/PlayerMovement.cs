using UnityEditor.UIElements;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private CharacterController _characterController;
    public float StartSpeed;
    [Range(0, 2)] public float HorisontalSpeedModifer = 1;
    [Range(0, 2)] public float HorisontalSpeedInJumpModifer = 1;
    public float MaxSpeed;
    public float BaseAcceleration;
    public float JumpSpeed;
    public float Gravity = 20.0f;
    private float _speed;
    private float _additionalSpeed;
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
    public CurveField Acelecari1onCurve;
    
    // private bool _isGrounded;

    [Inject(Id = "CharacterAnimator")] private Animator _animatorController;
    [Range(1, 15)] public float RotarionSpeed = 1;

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _speed = StartSpeed;
        _baseCenter = _characterController.center;
        _baseHeight = _characterController.height;
        _slideHeight = _baseHeight / 3;
        _slideCenter = new Vector3(_baseCenter.x, _baseCenter.y - (_slideHeight / 2), _baseCenter.z);
        _oldPosition = transform.position;
    }

    void FixedUpdate()
    {
        _speed = StartSpeed + _additionalSpeed;
        if (_speed > MaxSpeed)
        {
            _speed = MaxSpeed;
            _additionalSpeed = MaxSpeed - _speed;
        }
    
        if (_speed < 3)
        {
            _speed = 3;
        }

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

        var slide = Input.GetKey(KeyCode.C);


        if (slide)
        {
            _characterController.height = _slideHeight;
            _characterController.center = _slideCenter;
            // Debug.Log(_characterController.velocity.normalized);

            if (_oldPosition.y - transform.position.y < -.001f)
            {
                _additionalSpeed += .01f;
            }
            else
            {
                _additionalSpeed -= .1f;
            }

        }
        else
        {
            _additionalSpeed += BaseAcceleration;
            // if (_speed * 2 < MaxSpeed)
            // {
                
                Debug.Log(_additionalSpeed);
                Debug.Log(AcelecarionCurve.Evaluate(_additionalSpeed));
                
                _additionalSpeed += AcelecarionCurve.Evaluate(_additionalSpeed);

            // }


            _characterController.height = _baseHeight;
            _characterController.center = _baseCenter;
        }

        _animatorController.SetBool("Slide", slide);
        _animatorController.SetFloat("speed", moveDirection.normalized.magnitude);

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        moveDirection.y -= Gravity * Time.deltaTime;

        var move = transform.TransformDirection(moveDirection);

        // Move the controller
        _characterController.Move(move * Time.deltaTime);
    }
}