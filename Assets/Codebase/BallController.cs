using UnityEditor;
using UnityEngine;

namespace Codebase
{
    public class BallController : MonoBehaviour
    {
        // The length of the arrow, in meters
        [SerializeField] [Range(0.5F, 2)] private float arrowLength = 1.0F;

        [SerializeField] private Vector3 _throwVector = new Vector3(1, 1, 0);
        [SerializeField] private float _throwPower = 0;
    
        private Rigidbody _rigidbody;
    
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.isKinematic = true;
        }
        private void OnDrawGizmos()
        {
            Gizmos.DrawRay(transform.position, _throwVector);
            
            if (!Application.isPlaying) return;
    
            var position = transform.position;
            var velocity = _rigidbody.linearVelocity;

            if (velocity.magnitude >= 0.1f)
            {
                Handles.color = Color.red;
                Handles.ArrowHandleCap(0, position, Quaternion.LookRotation(velocity), arrowLength, EventType.Repaint);
            }
        }
        [ContextMenu("Throw")]
        public void ThrowBall()
        {
            _rigidbody.isKinematic = false;
            _rigidbody.AddForce(_throwVector*_throwPower, ForceMode.Impulse);
        }
    }
}