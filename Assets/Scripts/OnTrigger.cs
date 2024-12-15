using UnityEngine;
using UnityEngine.Events;

public class OnTrigger : MonoBehaviour
{
    [SerializeField] private UnityEvent actionOfTriggerEnter;
    [SerializeField] private UnityEvent actionOfTriggerExit;
    [SerializeField] private UnityEvent actionOfCollisionEnter;
    [SerializeField] private UnityEvent actionOfCollisionExit;
    [SerializeField] private string tagOfObject;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(tagOfObject))
        {
            actionOfTriggerEnter?.Invoke();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(tagOfObject))
        {
            actionOfTriggerExit?.Invoke();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(tagOfObject))
        {
            actionOfCollisionEnter?.Invoke();
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag(tagOfObject))
        {
            actionOfCollisionExit?.Invoke();
        }
    }
}
