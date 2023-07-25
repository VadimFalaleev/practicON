using UnityEngine;

[ExecuteAlways]
public class Follower : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _lerpRate;

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, _target.position, Time.deltaTime * _lerpRate);
    }
}