using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ThrowBall : MonoBehaviour
{
    private Rigidbody _rb;
    
    [SerializeField] private float force = 10f;
    

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.isKinematic = true;
    }

    public void Throw(Vector3 targetPosition)
    {
        if (_rb.isKinematic)
        {
            Vector3 throwDirection = (targetPosition - _rb.transform.position).normalized;
            _rb.isKinematic = false;
            _rb.AddForce(throwDirection * force + Vector3.up * (force / 2), ForceMode.Impulse);
        }
    }
    
    public void SetForce(float newForce)
    {
        if (newForce > 0)
        {
            force = newForce;
        }
    }
    
}
