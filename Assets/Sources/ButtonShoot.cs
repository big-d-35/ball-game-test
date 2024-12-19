using UnityEngine;

public class ButtonShoot : MonoBehaviour
{
    public void OnClick(Ball ball)
    {
        ball.Shoot();
    }
}
