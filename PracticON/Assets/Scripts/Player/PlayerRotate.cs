using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;

    private float _xEuler = 90;

    private void Update()
    {
        float _xAxis = Input.GetAxisRaw("Horizontal");

        if (_xAxis > 0)
        {
            _xEuler = 90;

        }
        else if (_xAxis < 0)
        {
            _xEuler = 270;
        }

        transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0, _xEuler, 0), Time.deltaTime * _rotationSpeed);
    }
}