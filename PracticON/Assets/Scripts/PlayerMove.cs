using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _friction;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Animator _animator;
    //[SerializeField] private AudioSource jumpAudio;
    //[SerializeField] private AudioSource attackAudio;

    private float _coyoteTime = 0.2f;
    private float _jumpBufferTime = 0.2f; 
    private float _coyoteTimeCounter;
    private float _jumpBufferCounter;
    private float _jumpFramesTimer;
    private float _xAxis;
    private float _xEuler = 90;
    private bool _isGrounded;

    //public static PlayerMove Instance { get; private set; }

    //private void Awake()
    //{
    //    if (Instance == null)
    //    {
    //       Instance = this;
    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    private void Update()
    {
        _jumpFramesTimer += Time.deltaTime;
        _xAxis = Input.GetAxisRaw("Horizontal");

        _coyoteTimeCounter = _isGrounded ? _coyoteTimeCounter = _coyoteTime : _coyoteTimeCounter -= Time.deltaTime;
        _jumpBufferCounter = Input.GetKeyDown(KeyCode.Space) ? _jumpBufferCounter = _jumpBufferTime : _jumpBufferCounter -= Time.deltaTime;

        if (_coyoteTimeCounter > 0f && _jumpBufferCounter > 0f && _jumpFramesTimer > 0.4f)
        {
            _jumpFramesTimer = 0f;
            _jumpBufferCounter = 0f;
            Jump();
        }
        if (Input.GetKeyUp(KeyCode.Space) && _rigidbody.velocity.x > 0f)
        {
            _coyoteTimeCounter = 0f;
        }
        
        _animator.SetBool("Fall", !_isGrounded);

        Rotate();
        _animator.SetBool("Walk", Input.GetAxis("Horizontal") != 0);

        // if (EventSystem.current.IsPointerOverGameObject() == false)
        // {
        Attack();
        // }
    }

    private void Attack()
    {
        if (Input.GetMouseButtonDown(0) && _isGrounded && Input.GetAxis("Horizontal") == 0)
        {
            // attackAudio.Play();
            _animator.SetTrigger("Attack");
        }
    }

    private void Rotate()
    {
        if (_xAxis > 0)
            _xEuler = 270f;
        else if (_xAxis < 0)
            _xEuler = 90f;
        transform.localRotation = Quaternion.Euler(0, _xEuler, 0);
    }

    public void Jump()
    {
        //jumpAudio.Play();
        _animator.SetTrigger("Jump");
        _rigidbody.AddForce(0, _jumpForce, 0, ForceMode.VelocityChange);
    }

    private void FixedUpdate()
    {
        float speedMultiplierInAir = 1f;
        float input = Input.GetAxis("Horizontal");

        if (_isGrounded)
        {
            _rigidbody.AddForce(input * _moveSpeed, 0, 0, ForceMode.VelocityChange);
        }
        else
        {
            speedMultiplierInAir = 0.2f;
            if (_rigidbody.velocity.x > _maxSpeed && input > 0)
            {
                speedMultiplierInAir = 0;
            }
            if (_rigidbody.velocity.x < -_maxSpeed && input < 0)
            {
                speedMultiplierInAir = 0;
            }
            _rigidbody.AddForce(input * _moveSpeed * speedMultiplierInAir, 0, 0, ForceMode.VelocityChange);
        }
        _rigidbody.AddForce(-_rigidbody.velocity.x * _friction * speedMultiplierInAir, 0, 0, ForceMode.VelocityChange);
    }

    private void OnCollisionStay(Collision collision)
    {
        for (int i = 0; i < collision.contactCount; i++)
        {
            float angle = Vector3.Angle(collision.contacts[i].normal, Vector3.up);
            if (angle < 45f)
            {
                _isGrounded = true;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        _isGrounded = false;
    }

    //private void OnDestroy()
    //{
    //    if (Instance == this)
    //    {
    //        Instance = null;
    //    }
    //}
}