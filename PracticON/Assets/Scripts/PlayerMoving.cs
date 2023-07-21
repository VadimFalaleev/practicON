using System.Threading;
using UnityEngine;

public class PlayerMoving : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpSpeed;
    [SerializeField] private float _friction;
    [SerializeField] private float _maxSpeed;

    [SerializeField] private Rigidbody _playerRigidbody;
    [SerializeField] private Animation _animation;

    private float _coyoteTime = 0.2f;
    private float _coyoteTimeCounter;
    private float _jumpBufferTime = 0.2f;
    private float _jumpBufferCounter;
    private float _jumpFramesTimer;
    private float _xAxis;
    private float _yEuler = 90;
    private bool _isGrounded;

    private void Update()
    {
        _jumpFramesTimer += Time.deltaTime;
        _xAxis = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        float input = Input.GetAxis("Horizontal");
        _playerRigidbody.AddForce(input * _moveSpeed, 0, 0, ForceMode.VelocityChange);
    }
}