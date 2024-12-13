using UnityEngine;
using UnityEngine.UI;

public class Throwing : MonoBehaviour
{
    [SerializeField]
    private Button _throwButton;

    [SerializeField]
    private Transform _target;

    [SerializeField]
    private float _throwForce = 10f;

    [SerializeField]
    private float _verticalOffset = 5f;

    private Rigidbody _rigitBody;

    private void Awake()
    {
        _rigitBody = GetComponentInChildren<Rigidbody>();
    }

    private void Start()
    {
        _throwButton.onClick.AddListener(Throw);
    }

    private void Throw()
    {
        var directionThrow = _target.transform.position - transform.position;
        directionThrow.y += _verticalOffset;

        _rigitBody.useGravity = true;
        _rigitBody.AddForce(directionThrow * _throwForce, ForceMode.Impulse);

    }
}
