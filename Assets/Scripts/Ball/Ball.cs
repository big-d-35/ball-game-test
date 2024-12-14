using UnityEngine;

public class Ball : MonoBehaviour
{
    private ThrowBall _throwBall;
    
    private void Awake()
    {
        _throwBall = GetComponent<ThrowBall>();
    }
    
    public void Throw(Transform target)
    {
        _throwBall.Throw(target.position);
    }
    
}


