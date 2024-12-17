using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))] 
public class ThrowBall : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbbody;
    [SerializeField] private Transform _pointBall;
    [SerializeField] private Transform _target;
    [SerializeField] private Button _throwButton;

    private void Awake()
    {
        _rigidbbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _throwButton.onClick.AddListener(Throw);
    }

    private void Throw()
    {
        _rigidbbody.isKinematic = false;
        Vector3 directionThrow = _target.position - _pointBall.position;
        _rigidbbody.AddForce(directionThrow, ForceMode.Impulse);
    }
}
