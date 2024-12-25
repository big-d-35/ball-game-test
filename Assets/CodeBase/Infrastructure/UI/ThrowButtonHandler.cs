using CodeBase.Infrastructure.BallLogic;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Infrastructure.UI
{
    [RequireComponent(typeof(Button))]
    public class ThrowButtonHandler : MonoBehaviour
    {
        [SerializeField] private BallThrower ballThrower;
        private Button _button;

        private void Start()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnThrowButtonPressed);
        }

        private void OnThrowButtonPressed() =>
            ballThrower.ThrowBall();
    }
}