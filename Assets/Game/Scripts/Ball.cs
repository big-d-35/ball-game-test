using System.Collections;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private GameState _gameState;

    private Rigidbody _rigitbody;
    private bool _isOnGround = false;

    private void Awake()
    {
        _rigitbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!_isOnGround && collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            StartCoroutine(WaitStopMoving());
        }
    }

    private IEnumerator WaitStopMoving()
    {
        _isOnGround = true;

        yield return new WaitUntil(() => _rigitbody.linearVelocity.magnitude < 0.01 &&
                                         _rigitbody.angularVelocity.magnitude < 0.01);

        _gameState.RestartScene();
    }
}
