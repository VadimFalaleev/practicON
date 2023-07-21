using UnityEngine;

public class Follower : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _lerpRate;
 
    void Update()
    {
        transform.position = _target.position;
    }
}