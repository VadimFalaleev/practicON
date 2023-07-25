using UnityEngine;
using UnityEngine.Events;

public enum Direction
{
    Left,
    Right
}

public class Walker : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _stopTime;
    [SerializeField] private Transform _leftTarget;
    [SerializeField] private Transform _rightTarget;
    [SerializeField] private Transform _rayStart;
    [SerializeField] private LayerMask _layerMask;

    private bool _isStopped;

    public Direction CurrentDirecton;
    public UnityEvent EventOnLeftTarget;
    public UnityEvent EventOnRightTarget;


    private void Start()
    {
        _leftTarget.parent = null;
        _rightTarget.parent = null;
    }

    void Update()
    {
        if (_isStopped == true)
        {
            return;
        }

        if (CurrentDirecton == Direction.Left)
        {
            transform.position -= new Vector3(Time.deltaTime * _speed, 0f, 0f);

            if (transform.position.x < _leftTarget.position.x)
            {
                CurrentDirecton = Direction.Right;
                _isStopped = true;
                Invoke(nameof(ContinueWalk), _stopTime);
                EventOnLeftTarget.Invoke();
            }
        }
        else
        {
            transform.position += new Vector3(Time.deltaTime * _speed, 0f, 0f);

            if (transform.position.x > _rightTarget.position.x)
            {
                CurrentDirecton = Direction.Left;
                _isStopped = true;
                Invoke(nameof(ContinueWalk), _stopTime);
                EventOnRightTarget.Invoke();
            }
        }

        RaycastHit hit;

        if (Physics.Raycast(_rayStart.position, Vector3.down, out hit, 300, _layerMask, QueryTriggerInteraction.Ignore))
        {
            transform.position = hit.point;
        }
    }

    private void ContinueWalk()
    {
        _isStopped = false;
    }

}