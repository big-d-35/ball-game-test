using UnityEngine;
using UnityEngine.UI;
public class ThrowPower : MonoBehaviour
{
    private Slider slider;
    public Ball ball;
    private void Start()
    {
        slider = GetComponent<Slider>();
        ball.slider = slider;
    }
}