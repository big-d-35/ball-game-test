using System;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public event Action Fallen;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Floor>(out Floor floor))
        {
            Fallen?.Invoke();
        }
    }
}
