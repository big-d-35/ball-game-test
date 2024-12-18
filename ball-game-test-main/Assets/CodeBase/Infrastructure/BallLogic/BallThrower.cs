using UnityEngine;

namespace CodeBase.Infrastructure.BallLogic
{
    public class BallThrower : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private float timeToTarget = 1f;
        private Rigidbody _rigidbody;

        private void Awake() => 
            _rigidbody = GetComponent<Rigidbody>();

        public void ThrowBall()
        {
            if (target == null)
                return;

            Vector3 initialVelocity = CalculateThrowVelocity(transform.position, target.position, timeToTarget);
            _rigidbody.isKinematic = false;
            _rigidbody.linearVelocity = initialVelocity;
        }

        private Vector3 CalculateThrowVelocity(Vector3 startPosition, Vector3 targetPosition, float time)
        {
            Vector3 distance = targetPosition - startPosition;
            Vector3 horizontalDistance = new Vector3(distance.x, 0, distance.z);

            Vector3 horizontalVelocity = horizontalDistance / time;

            float verticalVelocity = distance.y / time + 0.5f * Physics.gravity.magnitude * time;

            return horizontalVelocity + Vector3.up * verticalVelocity;
        }
    }
}